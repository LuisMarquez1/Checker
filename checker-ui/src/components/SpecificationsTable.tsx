import { Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import type { Specification } from "../types/Specification";
import { Link } from "react-router-dom";
interface Props { specifications: Specification[]; }

function SpecificationsTable({ specifications } : Props) {

    return (
        <TableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell> Part Number </TableCell>

                        <TableCell> Revision </TableCell>

                        <TableCell> Circuit Type</TableCell>

                        <TableCell> Limits </TableCell>

                        <TableCell>Actions</TableCell>
                    </TableRow>
                </TableHead>

                <TableBody>
                    {specifications.map(
                        specifications => (
                            <TableRow key={specifications.id} >
                                <TableCell> {specifications.partNumber} </TableCell>
                                <TableCell> {specifications.revision } </TableCell>
                                <TableCell> {specifications.circuitType } </TableCell>
                                <TableCell> {specifications.limits.length } </TableCell>
                                <TableCell>
                                    <Button component={Link} to={`/specifications/edit/${specifications.id}`}>Edit </Button>
                                </TableCell>
                            </TableRow>
                        )
                    )}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default SpecificationsTable;