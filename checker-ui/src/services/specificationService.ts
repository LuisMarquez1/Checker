import api from "../api/axios";
import type { Specification } from "../types/Specification";
import type { CreateSpecificationRequest } from "../types/CreateSpecificationRequest";

export async function getSpecification(partNumber: string) : Promise<Specification> {
    const response = await api.get<Specification>(`/specifications/${partNumber}`);

    return response.data;
}

export async function getAllSpecifications() : Promise<Specification[]> {
    const response = await api.get<Specification[]>(`/specifications`);

    return response.data;
}

export async function createSpecification(request: CreateSpecificationRequest): Promise<void>{
    await api.post("/specifications", request);
}

export async function GetSpecificationById(id: string): Promise<Specification>{
    const response = await api.get<Specification>(`/specifications/${id}`);

    return response.data;
}

export async function updateSpecification(id: string, request: CreateSpecificationRequest): Promise<void>{
    await api.put(`/specifications/${id}`, request)
}