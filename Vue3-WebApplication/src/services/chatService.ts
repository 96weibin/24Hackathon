import { WebApi } from "./webApi";

export class ChatService {
    public postIntent(str: string) {
        let url = `ai/intent`;
        return WebApi.post(url, str)
    }
//  `query { cases { items { name } } }`
// mutation { cases { add(input: { name: "aae" parentCaseName: "Base Model" }){ id name } }}}
    public graphQlTest(query: string) {
        let url = 'GraphQL/Execute'
        return WebApi.post(url, { ModelName: "Gulf Coast", QueryContent: `mutation {\n           cases {\n               add(input: {\n                   name: \"aaR\"\n                   parentCaseName: \"Base Model\"\n               }){\n                 id\n                 name\n               }\n           }}`})
    }

    public addCase(caseInput: ICaseInputRequest) {
        let url = 'GraphQL/AddCase'
        return WebApi.post(url, caseInput)
    }

    public postFindTopMargin(req: ChatResult) {
        let url = `aupdb/FindTopMargin`;
        return WebApi.post<IFindTopResponse>(url, req)
    }

    public adjustMargin(req: AdjustMarginRequest) {
        let url = `aupdb/AdjustMargin`;
        return WebApi.post<AdjustMarginResponse>(url, req)
    }
}

export interface ChatResult {
    caseName: string,
    category?: number,
    confidenceScore?: number,
    modelName: string,
    nonBasisType: number,
    question: string,
    topNumber: number
}

export interface IFindTopResponse {
    intent: ChatResult,
    margins: any[]
}

export interface ICaseInputRequest {
    caseInput: CaseInput,
    salesInputs?: UpdateVaribelInput[],
    processLimitsInputs?: UpdateVaribelInput[],
    purchaseInputs?: UpdateVaribelInput[],
    capacitiesInputs?: UpdateVaribelInput[],
}

export interface UpdateVaribelInput {
    name: string,
    Inputs: FieldInput[]
}

export interface FieldInput {
    field: string,
    Max: number,
    Min: number,
}

export interface CaseInput {
    name: string,
    parentCaseName: string
}


export interface AdjustMarginRequest {
    caseName1: string,
    caseName2:string, 
    intent: ChatResult
}


export interface AdjustMarginResponse {
    obj1: number,
    obj2: number,
    intent: ChatResult
}