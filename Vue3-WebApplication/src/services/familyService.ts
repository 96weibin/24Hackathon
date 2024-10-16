import { WebApi } from "./webApi";

export class FamilyService {

    public getFamilyList() {
        let url = "Family/GetFamilyList";
        return WebApi.get<[FamilyData]>(url);
    }
}

export interface FamilyData {
    id: number;
    name:string;
}

export interface Tree {
    id?: number;
    label?: string;
    children?: Tree[];
  }
