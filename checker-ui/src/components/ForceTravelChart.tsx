import { ResponsiveContainer, LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip } from "recharts";
import type { ForceTravelChartPoints } from "../types/ForceTravelChartPoints";

interface Props{
    data: ForceTravelChartPoints[];
}

function ForceTravelChart({ data }: Props){
    return(
        <ResponsiveContainer width="100%" height={400}>
            <LineChart data={data}>
                <CartesianGrid />
                
                <XAxis dataKey="travel" />
                <YAxis />

                <Tooltip />

                <Line type="monotone" dataKey="force" stroke="#1976d2" strokeWidth={2} />
            </LineChart>
        </ResponsiveContainer>
    );
}

export default ForceTravelChart;