import { WebApi } from "./webApi";

export class ChatService {
    public postIntent(str: string){
        let url= `ai/intent`;
        return WebApi.post(url, str)
    }


    public graphQlTest(query: string) {
        let url = 'GraphQL/Execute'
        return WebApi.post(url, {ModelName: "Gulf Coast", QueryContent: `query { cases { items { name } } }`})
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