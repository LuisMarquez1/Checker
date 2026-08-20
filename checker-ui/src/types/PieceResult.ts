import type { MeasurementResult } from "./MeasurementResult";

export interface PieceResult {
    pieceNumber: number;
    passed: boolean;
    measurements: MeasurementResult[];
}