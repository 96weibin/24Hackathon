import { IPersonContract } from "./QuestionService";
import { WebApi } from "./webApi";

export class PeopleService {

    public Login(request: PeopleData) {
        let url = "People/Login";
        return WebApi.post<IPersonContract>(url, request)
    }
}

export interface PeopleData {
    name: string;
    password: string;
}
