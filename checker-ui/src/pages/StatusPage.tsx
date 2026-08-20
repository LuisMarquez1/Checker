import MainLayout from "../layouts/MainLayout";
import { Paper, Typography, Grid } from "@mui/material";

function StatusPage(){
    return(
        <MainLayout>
            <Typography variant="h4" gutterBottom>Status Page</Typography>

            <Paper sx={{ p: 4}}>
                <Grid container spacing={2}>
                    <Grid size={{ xs: 12, md: 6}}>
                        <Typography>Product Code</Typography>
                        <Typography> ABS1449 </Typography>
                    </Grid>

                    <Grid size={{ xs:12, md: 6}}>
                        <Typography>Catalog Listing</Typography>
                        <Typography>BA-2RV133-A4</Typography>
                    </Grid>

                    <Grid size={{xs: 12, md: 6}}>
                        <Typography>Driver</Typography>
                        <Typography>Simulation</Typography>
                    </Grid>
                </Grid>
            </Paper>
        </MainLayout>
    );
}

export default StatusPage;