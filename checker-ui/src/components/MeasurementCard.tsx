import { Card, CardContent, Typography } from "@mui/material";

interface MeasurementCardProps{
    code: string;
    description: string;
    value: string;
}

function MeasurementCard({ code, description, value }: MeasurementCardProps){
    return(
        <Card sx={{ minWidth: 180 }}>
            <CardContent>
                <Typography color="text.secondary" variant="overline">{code}</Typography>

                <Typography color="text.secondary" variant="h5">{value}</Typography>

                <Typography variant="body2">{description}</Typography>
            </CardContent>
        </Card>
    );
}

export default MeasurementCard;