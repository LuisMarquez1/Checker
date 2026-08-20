import type { PieceResult } from "../types/PieceResult";

export const mockSessionResults: PieceResult[] = [
    {
        pieceNumber: 1,
        passed: true,
        measurements: [
            {
                code: "OF", value: 8.2, passed: true
            },
            {
                code: "RF", value: 6.4, passed: true
            },
            {
                code: "DT", value: 0.00016, passed: true
            }
        ]

    },
    {
        pieceNumber: 2,
        passed: true,
        measurements: [
            {
                code: "OF", value: 8.2, passed: false
            },
            {
                code: "RF", value: 6.4, passed: true
            },
            {
                code: "DT", value: 0.00016, passed: true
            }
        ]
    },
    {
        pieceNumber: 3,
        passed: true,
        measurements: [
            {
                code: "OF", value: 8.2, passed: true
            },
            {
                code: "RF", value: 6.4, passed: true
            },
            {
                code: "DT", value: 0.00016, passed: true
            }
        ]
    },
    {
        pieceNumber: 4,
        passed: true,
        measurements: [
            {
                code: "OF", value: 8.2, passed: true
            },
            {
                code: "RF", value: 6.4, passed: true
            },
            {
                code: "DT", value: 0.00016, passed: true
            }
        ]
    },
    {
        pieceNumber: 5,
        passed: true,
        measurements: [
            {
                code: "OF", value: 8.2, passed: true
            },
            {
                code: "RF", value: 6.4, passed: true
            },
            {
                code: "DT", value: 0.00016, passed: true
            }
        ]
    },
    {
        pieceNumber: 6,
        passed: true,
        measurements: [
            {
                code: "OF", value: 8.2, passed: true
            },
            {
                code: "RF", value: 6.4, passed: true
            },
            {
                code: "DT", value: 0.00016, passed: true
            }
        ]
    },  
];