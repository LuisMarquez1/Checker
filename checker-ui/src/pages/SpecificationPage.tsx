import { useEffect, useState } from "react";
import type { Specification } from "../types/Specification";
import { getAllSpecifications } from "../services/specificationService";
import MainLayout from "../layouts/MainLayout";
import SpecificationsTable  from "../components/SpecificationsTable";

function SpecificationPage() {
    const [specification, setSpecification] = useState<Specification[]>([]);

    useEffect(() => {

        async function loadData(){
            const result = await getAllSpecifications();

            setSpecification(result);
        }

        loadData();
    }, []);

    return (
        <MainLayout>
            <h1>Specification Page</h1>

            <SpecificationsTable specifications={ specification}>
                
            </SpecificationsTable>
        </MainLayout>
    );
}

export default SpecificationPage;

