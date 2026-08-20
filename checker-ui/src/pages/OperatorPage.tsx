import MainLayout from "../layouts/MainLayout";
import ForceTravelChart from "../components/ForceTravelChart";
import { Table, TableHead,TableBody, TableCell, TableRow, Typography, Paper } from "@mui/material";
import type { PieceResult } from "../types/PieceResult";
import ResultBanner from "../components/ResultBanner";
import visibleMeasurements from "../data/visibleMeasurements";
import { mockSessionResults } from "../data/mockSessionResults";

function OperatorPage(){
    
    const maxVisiblePieces = 5;

    const testResults = mockSessionResults;
    
    const currentPiece = testResults[testResults.length - 1];

    const passCount = testResults.filter(x => x.passed).length;
    const failCount = testResults.filter(x => !x.passed).length;
    {/*const yieldPercent = */}

    const visibleResults = testResults.slice(-maxVisiblePieces);

    const sampleData = [
        { travel: 0, force: 0},
        { travel: 1, force: 3},
        { travel: 2, force: 8},
        { travel: 3, force: 15},
        { travel: 4, force: 21},
        { travel: 5, force: 26},
        { travel: 6, force: 18},
        { travel: 7, force: 10},
        { travel: 8, force: 4},
    ];

    function getMeasurement(piece: PieceResult, code: string){
        return piece.measurements.find(x => x.code === code);
    }

    return(
        <MainLayout>
            <Typography variant="h4" gutterBottom>
                Operator Page
            </Typography>

            <ResultBanner passed={currentPiece.passed} />
            
            <Typography>Pass: {passCount}</Typography>
            <Typography>Fail: {failCount}</Typography>
            <Typography>Yield: 90.00%</Typography>
            
            <Typography variant="h6" sx={{ mt: 2}}>
                Current Piece: {currentPiece.pieceNumber}
            </Typography>

            <Paper sx={{ mt: 2}}>
                <Table size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ fontWeight: "bold"}}> Unit </TableCell>
                            {
                                visibleResults.map(result => (
                                    <TableCell key={result.pieceNumber} sx={{ fontWeight: "bold", textAlign: "center"}}>
                                        {result.pieceNumber}
                                    </TableCell>
                                ))
                            }
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {visibleMeasurements.map(measurement => (
                            <TableRow key={measurement}>
                                <TableCell>{measurement.code}</TableCell>
                                {
                                    visibleResults.map(result => {
                                        const item = getMeasurement(result, measurement.code);

                                        if(!item){
                                            return (
                                                <TableCell key={result.pieceNumber} sx={{ textAlign: "center"}}></TableCell>
                                            );
                                        }

                                        return (
                                            <TableCell key={result.pieceNumber} sx={{ textAlign:"center", backgroundColor: item.passed ? undefined : "#ff8a80"}}>
                                                {item.value}
                                            </TableCell>
                                        );
                                    })
                                }
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </Paper>

            <Paper sx={{p: 3}}>
                <Typography variant="h6" gutterBottom> Force Travel Curve </Typography>

                <ForceTravelChart data={sampleData} />
            </Paper>
        </MainLayout>
    );

}

export default OperatorPage;