import { WebApi } from "./webApi";

export class ChatService {
    public postIntent(str: string) {
        let url = `ai/intent`;
        return WebApi.post<IIntent>(url, str)
    }
    public graphQlTest(query: string) {
        let url = 'GraphQL/Execute'
        return WebApi.post(url, { ModelName: "Gulf Coast", QueryContent: `mutation{\n    runCases: caseExecution {\n      submitCaseStack(\n        input:{\n          name: \"Job\"\n          cases: [\n            {name: \"Cat Cracker RTT vs FDR Study (Base)\"}\n          ]\n        }\n      )\n      {\n        id\n      }\n    }\n}`})
    }

    public addCase(caseInput: ICaseInputRequest) {
        let url = 'GraphQL/AddCase'
        return WebApi.post(url, caseInput)
    }

    public postFindTopMargin(req: IIntent) {
        let url = `aup/FindTopMargin`;
        return WebApi.post<IFindTopResponse>(url, req)
    }

    public adjustMargin(req: IIntent) {
        let url = `aup/AdjustMargin`;
        return WebApi.post<AdjustMarginResponse>(url, req)
    }
}

export interface IIntent {
    caseName: string;
    category?: Icategory;
    confidenceScore?: number;
    modelName: string;
    nonBasisType: number;
    question: string;
    topNumber: number;
}

export enum Icategory {
    None,
    FindTopMargin,
    AdjustMargin
}

export interface IFindTopResponse {
    intent: IIntent,
    margins: any[]
}

export interface IVariableMargin
{
    nonBasisType: NonBasisType;
    variableName: string;
    margin: number;
    lowBound: number;
    highBound: number;
}

export enum NonBasisType
{
    All = 0,
    Purchase,
    Sales,
    Capacity,
    ProcLimit,
    Operation
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

export interface AdjustMarginResponse {
    obj1: number,
    obj2: number,
    intent: IIntent
}