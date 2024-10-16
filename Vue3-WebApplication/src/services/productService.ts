import { WebApi } from "./webApi";

export class ProductService {

    public getProductList() {
        let url = "Product/GetProductList";
        return WebApi.get<[ProductData]>(url);
    }
}

export interface ProductData {
    id: number;
    name:string;
    familyId: number;
}
