import {Paper, Typography } from "@mui/material";

interface ResultBannerProps{
    passed: boolean;
}

function ResultBanner({ passed }: ResultBannerProps) {
    return (
        <Paper sx={{ mb: 2, p: 2, bgcolor: passed ? "#2e7d32" : "#d32f2f", color: "white"}}>
            <Typography variant="h4" align="center">
                {
                    passed ? "PASS" : "FAIL"
                }
            </Typography>
        </Paper>
    );
}

export default ResultBanner;