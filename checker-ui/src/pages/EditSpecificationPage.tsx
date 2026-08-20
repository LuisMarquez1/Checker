import { useParams } from "react-router-dom";
import { useState } from "react";
import { GetSpecificationById, updateSpecification } from "../services/specificationService";

import type { Specification } from "../types/Specification";

function EditSpecificationPage(){
    const [partNumber, setPartNumber] = useState<Specification[]>([]);
    const { id } = useParams();

    async function handlesave(){
        if(!id)
            return;

        await updateSpecification(id,{
            partNumber,
            revision,
            circuitType,
            limits
        });

        alert("Specification updated");
    }

    useEffect(() => {
        async function loadSpecification(){
            if(!id){
                return;
            }

            const result = await GetSpecificationById(id);

            setPartNumber(result.partNumber);
            setRevision(result.revision);
            setCircuitType(result.circuitType);
            setLimits(result.limits);
        }

        loadSpecification();
    }, [id]);
}

export default EditSpecificationPage;