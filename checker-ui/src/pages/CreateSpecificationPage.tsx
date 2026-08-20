import {Button, Paper, Stack, TextField, Typography, FormControl, InputLabel, Select, MenuItem } from "@mui/material";
import MainLayout from "../layouts/MainLayout";
import { useState } from "react";
import { createSpecification } from "../services/specificationService";
import type { CreateMeasurementLimitRequest } from "../types/CreateSpecificationRequest";
import { measurementTypes } from "../constants/measurementTypes";

function CreateSpecificationPage(){
    const [partNumber, setPartNumber] = useState("");
    const [revision, setRevision] = useState("");
    const [circuitType, setCircuitType] = useState(0);
    const [limits, setLimits] = useState<CreateMeasurementLimitRequest[]>([]);

    function addLimit(){
        setLimits([...limits, {
            measurementType: 0,
            minimum: 0,
            maximum: 0
        }]);
    }

    function removeLimit(index: number){
        setLimits(limits.filter((_, i) => i !== index));
    }

    function updateMeasurementType(index: number, value: number){
        const updated = [...limits]

        updated[index].measurementType = value;

        setLimits(updated)
    }

    function updateMinimum(index: number, value: number){
        const updated = [...limits]

        updated[index].minimum = value;

        setLimits(updated);
    }

    function updateMaximum(index: number, value: number){
        const updated = [...limits]

        updated[index].maximum = value;

        setLimits(updated);
    }

    async function handleSave(){
        await createSpecification({
            partNumber, revision, circuitType, limits
        });

        alert("Specifications Created");
    }


    return(
        <MainLayout>
            <Paper sx={{ p:4, maxWidth: 700 }}>
                <Typography variant="h5" gutterBottom>
                    Create Specification
                </Typography>

                <Stack spacing={2}>
                    <TextField label="Part Number" value={partNumber} onChange={(e) => setPartNumber(e.target.value)} />
                    <TextField label="Revision" value={revision} onChange={(e) => setRevision(e.target.value)} />
                    <FormControl fullWidth>
                        <InputLabel>Circuit Type</InputLabel>

                        <Select value={circuitType} label="Circuit Type" onChange={(e) => setCircuitType(Number(e.target.value))}>
                            <MenuItem value={0}>SPDT</MenuItem>
                            <MenuItem value={1}>NO Only</MenuItem>
                            <MenuItem value={2}>NC Only</MenuItem>
                        </Select>
                    </FormControl>
                    <Typography variant="h6">Measurement Limits</Typography>
                    {limits.map((limit, index) => (
                        <Paper key={index} sx={{ p:2, display: "flex", flexDirection: "column", gap: 2 }}>
                            <Typography>Limit #{index + 1}</Typography>
                            <FormControl fullWidth>
                                <InputLabel>Measurement</InputLabel>

                                <Select value={limit.measurementType} label="Measurement" onChange={(e) => updateMeasurementType(index, Number(e.target.value))}>
                                    {measurementTypes.map(item => (
                                        <MenuItem key={item.value} value={item.value}>
                                            {item.label}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>

                            <TextField label="Minimum" type="number" value={limit.minimum} onChange={(e) => updateMinimum(index, Number(e.target.value))} />
                            <TextField label="Maximum" type="number" value={limit.maximum} onChange={(e) => updateMaximum(index, Number(e.target.value))} />

                            <Button color="error" onClick={() => removeLimit(index)}> Delete </Button>


                        </Paper>
                    ))}
                    <Button variant="outlined" onClick={addLimit}>Add limit</Button>
                    <Button variant="contained" onClick={handleSave}> Save </Button>
                </Stack>
            </Paper>
        </MainLayout>
    );
}

export default CreateSpecificationPage;
