export interface MeasurementDefinition{
    code: string;
    description: string;
    visible: boolean;
    order: number;
}

export interface PieceResult{
    pieceNumber: number;
    passed: boolean;
    measurements:{
        code: string;
        value: number;
        passed: boolean;
    }[];
}