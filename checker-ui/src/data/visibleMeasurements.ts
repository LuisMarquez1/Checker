export interface VisibleMeasurement{
    code: string;
    description: string;
}

const visibleMeasurements = [
    {
        code: "OF",
        description: "Operating Force"
    },
    {
        code: "RF",
        description: "Release Force"
    },
    {
        code: "DF",
        description: "Differential Force"
    },
    {
        code: "DT",
        description: "Differential Travel"
    },
    {
        code: "OT",
        description: "Over Travel"
    },
    {
        code: "DBNO",
        description: "Dead Break Normally Open"
    },
    {
        code: "DBNC",
        description: "Dead Break Normally Closed"
    }
];

export default visibleMeasurements;