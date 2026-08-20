export interface MeasurementLimit {
    measurementType: number;
    minimum?: number;
    maximum?: number;
}

export interface Specification {
    id: string;
    partNumber: string;
    revision: string;
    circuitType: number;
    limits: MeasurementLimit[];
}