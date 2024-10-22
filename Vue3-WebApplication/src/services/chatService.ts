import { WebApi } from "./webApi";

export class ChatService {
    public postIntent(str: string){
        let url= `ai/intent`;
        return WebApi.post(url, str)
    }
}

export interface ChatResult {
    caseName?: string,
    category: number,
    confidenceScore: number,
    modelName: string,
    nonBasisType: number,
    question: string,
    topNumber: number
}