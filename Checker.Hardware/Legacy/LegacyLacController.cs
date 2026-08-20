using System.IO.Ports;
using System.Text;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public sealed class LegacyLacController : IMotionController, IDisposable
    {
        private const char Escape = (char)27;
        private const char CarriageReturn = '\r';

        private const byte InitLac = 0x10;

        private const byte TestSpeedDown = 0x30;
        private const byte TestSpeedUp = 0x40;

        private const byte OverTravelCommand = 0x60;

        private const byte HeadUpFast = 0x70;
        private const byte HeadDownFast = 0x80;

        private const byte HeadUpVariable = 0x90;
        private const byte HeadDownVariable = 0xA0;

        private const byte HeadUpToHome = 0xB0;

        private const byte MoveUpDistance = 0xC0;
        private const byte MoveDownDistance = 0xD0;

        private const byte VariableVelocityRegister = 3;
        private const byte RaiseLowerRegister = 16;

        private const double CountsPerInch = 254000.0;

        private readonly LegacyPcdio120Controller _pcdio;
        private readonly LacSerialOptions _options;

        private SerialPort? _serialPort;

        public LegacyLacController(LegacyPcdio120Controller pcdio, LacSerialOptions options)
        {
            _pcdio = pcdio;
            _options = options;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            OpenSerialPort();

            await SendCommandAsync("ms20", waitForPrompt: false, cancellationToken);
        }

        public async Task SetupAsync(string lacProgramPath, CancellationToken cancellationToken = default)
        {
            OpenSerialPort();

            await SendCommandAsync("rm", waitForPrompt: true, cancellationToken);

            await Task.Delay(200, cancellationToken);

            await SendCommandAsync("mf", waitForPrompt: true, cancellationToken);

            await Task.Delay(200, cancellationToken);

            if (!File.Exists(lacProgramPath))
                throw new FileNotFoundException("LAC program file was not found.", lacProgramPath);

            var lines = File.ReadAllLines(lacProgramPath);

            foreach (var line in lines)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.TrimStart().StartsWith(";"))
                    continue;

                await SendCommandAsync(line, waitForPrompt: true, cancellationToken);

                await Task.Delay(100, cancellationToken);
            }
        }

        public async Task InitializeToTopLimitAsync(CancellationToken cancellationToken = default)
        {
            await StartRunLoopAsync(cancellationToken);

            _pcdio.WriteLacCommand(InitLac);

            try
            {
                await Task.Delay(100, cancellationToken);

                var startedAt = DateTime.UtcNow;

                while (!_pcdio.LacInitialized())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if((DateTime.UtcNow - startedAt).TotalSeconds> 30)
                    {
                        await StopAsync();
                        throw new TimeoutException("The LAC did not reach the top-limit initialization state within 30 seconds.");
                    }

                    await Task.Delay(1, cancellationToken);
                }
            }
            finally
            {
                _pcdio.ClearLacCommand();
            }
        }

        public async Task MotorOnAsync(CancellationToken cancellationToken = default)
        {
            await SendCommandAsync("mn", waitForPrompt: true, cancellationToken);

            await Task.Delay(150, cancellationToken);

            await StartRunLoopAsync(cancellationToken);
        }

        public async Task MotorOffAsync(CancellationToken cancellationToken = default)
        {
            await SendCommandAsync("mf", waitForPrompt: true, cancellationToken);

            await Task.Delay(150, cancellationToken);

            await StartRunLoopAsync(cancellationToken);
        }

        public async Task DefineHomeAsync(CancellationToken cancellationToken = default)
        {
            await SendCommandAsync("dh", waitForPrompt: true, cancellationToken);

            await Task.Delay(150, cancellationToken);

            await StartRunLoopAsync(cancellationToken);
        }

        public Task MoveUpAsync(double speed)
        {
            return MoveVariableAsync(speed, HeadUpVariable);
        }

        public Task MoveDownAsync(double speed)
        {
            return MoveVariableAsync(speed, HeadDownVariable);
        }

        public async Task RaiseHeadAsync(double distance)
        {
            var counts = -ConvertDistanceToCounts(distance);

            await LoadRegisterAsync(RaiseLowerRegister, counts);

            _pcdio.WriteLacCommand(MoveUpDistance);

            await WaitForAckAsync();

            _pcdio.ClearLacCommand();
        }

        public async Task LowerHeadAsync(double distance)
        {
            var counts = ConvertDistanceToCounts(distance);

            await LoadRegisterAsync(RaiseLowerRegister, counts);

            _pcdio.WriteLacCommand(MoveDownDistance);

            await WaitForAckAsync();

            _pcdio.ClearLacCommand();
        }

        public async Task HeadUp100Async()
        {
            await StopAsync();

            _pcdio.WriteLacCommand(HeadUpFast);

            await Task.Delay(50);

            _pcdio.ClearLacCommand();
        }

        public async Task HeadDown100Async()
        {
            await StopAsync();

            _pcdio.WriteLacCommand(HeadDownFast);

            await Task.Delay(20);

            _pcdio.ClearLacCommand();

            await WaitForAckAsync();
        }

        public async Task TestSpeedUpAsync()
        {
            _pcdio.WriteLacCommand(TestSpeedUp);

            await WaitForAckAsync();

            _pcdio.ClearLacCommand();
        }

        public async Task TestSpeedDownAsync()
        {
            _pcdio.WriteLacCommand(TestSpeedDown);

            await WaitForAckAsync();

            _pcdio.ClearLacCommand();
        }

        public async Task MoveToOverTravelTorqueAsync()
        {
            _pcdio.WriteLacCommand(OverTravelCommand);

            await WaitForAckAsync();

            _pcdio.ClearLacCommand();
        }

        public async Task StopAsync()
        {
            _pcdio.TriggerMotorStop();

            await Task.Delay(10);
        }

        private async Task MoveVariableAsync(double speed, byte command)
        {
            var velocity = ConvertSpeedToLacVelocity(speed);

            await LoadRegisterAsync(VariableVelocityRegister, velocity);

            await StartRunLoopAsync();

            _pcdio.WriteLacCommand(command);

            await WaitForAckAsync();

            _pcdio.ClearLacCommand();
        }

        private Task StartRunLoopAsync(CancellationToken cancellationToken = default)
        {
            return SendCommandAsync("ms20", waitForPrompt: false, cancellationToken);
        }

        private async Task LoadRegisterAsync(byte registerNumber, long value, CancellationToken cancellationToken = default)
        {
            var command = $"al{value},ar{registerNumber}";

            await SendCommandAsync(command, waitForPrompt: true, cancellationToken);
        }

        private async Task SendCommandAsync(string command, bool waitForPrompt, CancellationToken cancellationToken = default)
        {
            OpenSerialPort();

            await SendEscapeAsync(cancellationToken);

            _serialPort!.Write(command + CarriageReturn);

            if (waitForPrompt)
                await WaitForPromptAsync(cancellationToken);
        }

        private async Task SendEscapeAsync(CancellationToken cancellationToken)
        {
            OpenSerialPort();

            _serialPort!.DiscardInBuffer();
            _serialPort.DiscardOutBuffer();

            _serialPort.Write(Escape.ToString());

            await WaitForPromptAsync(cancellationToken);
        }

        private async Task WaitForPromptAsync(CancellationToken cancellationToken)
        {
            var buffer = new StringBuilder();

            var startedAt = DateTime.UtcNow;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if ((DateTime.UtcNow - startedAt).TotalMilliseconds > _options.ReadTimeoutMiliseconds)
                    throw new TimeoutException($"LAC did not respond. Buffer: {buffer}");

                if (_serialPort!.BytesToRead <= 0)
                {
                    await Task.Delay(1, cancellationToken);
                    continue;
                }

                var character = (char)_serialPort.ReadChar();

                buffer.Append(character);

                if (character == '>' || character == '.')
                    return;

                if (character == '?')
                    throw new InvalidOperationException($"LAC reported command error. Buffer: {buffer}");
            }
        }

        private async Task WaitForAckAsync(CancellationToken cancellationToken = default)
        {
            while (!_pcdio.LacAcknowledged())
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(1, cancellationToken);
            }
        }

        private void OpenSerialPort()
        {
            if (_serialPort is not null && _serialPort.IsOpen)
                return;

            _serialPort = new SerialPort(
                    _options.PortName,
                    _options.BaudRate,
                    Parity.None,
                    8,
                    StopBits.One);

            _serialPort.ReadTimeout = _options.ReadTimeoutMiliseconds;

            _serialPort.WriteTimeout = _options.WriteTimeoutMiliseconds;

            _serialPort.NewLine = "\r";

            _serialPort.Open();
        }

        private static long ConvertDistanceToCounts(double inches)
        {
            return (long)Math.Truncate(inches * CountsPerInch);
        }

        private long ConvertSpeedToLacVelocity(double inchesPerSecond)
        {
            if (_options.SpeedMultiplier <= 0)
                throw new InvalidOperationException("The LAC speed multiplier has not been configured.");

            return (long)Math.Truncate(inchesPerSecond * _options.SpeedMultiplier);
        }

        public async Task DownloadVariablesAsync(CancellationToken cancellationToken = default)
        {
            ValidateStartupSettings();

            await LoadRegisterAsync(4, _options.Torque, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(5, _options.ProportionalGain, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(6, _options.IntegralGain, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(7, _options.DerivativeGain, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(8, _options.IntegralLimit, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(9, _options.CurrentGain, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(11, _options.FastVelocity, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(12, _options.MediumVelocity, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(13, _options.TestVelocity, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(14, _options.Acceleration, cancellationToken);

            await Task.Delay(100, cancellationToken);

            await LoadRegisterAsync(15, _options.OverTravelTorqueAdjust, cancellationToken);
        }

        private void ValidateStartupSettings()
        {
            if (_options.SpeedMultiplier <= 0)
                throw new InvalidOperationException("LAC SpeedMultiplier must be greater than zero.");

            if (_options.Torque < 0 || _options.Torque > 32767)
                throw new InvalidOperationException("LAC torque must be be between 0 and 32767.");

            if (_options.Acceleration <= 0)
                throw new InvalidOperationException("LAC acceleration must be greater than zero");

            if (_options.FastVelocity <= 0 || _options.MediumVelocity <= 0 || _options.TestVelocity <= 0)
                throw new InvalidOperationException("All LAC velocity values must be greater than zero.");


        }

        public void Dispose()
        {
            if (_serialPort is null)
                return;

            if (_serialPort.IsOpen)
                _serialPort.Close();

            _serialPort.Dispose();
        }

        public async Task TestConnectionAsync(CancellationToken cancellationToken = default)
        {
            OpenSerialPort();

            await SendEscapeAsync(cancellationToken);
        }
    }
}