import { walkFunctionParams } from "@vue/compiler-core";
import { WebApi } from "./webApi";
export class QuestionService {
    public getAllQuestionTypes() {
        let url = "AI/GetAllQuestionTypes";
        return WebApi.get<IQuestionType[]>(url);
    }
    public addOrUpdateQuestion(question: IAddOrUpdateQuestionRequest) {
        return WebApi.post<IQuestionContract>("AI/AddOrUpdateQuestion", question);
    }
    public deleteQuestion(questionId: number) {
        return WebApi.get<boolean>("AI/DeleteQuestion/" + questionId);
    }

    public getAllQuestionsCount() {
        return WebApi.get<number>("AI/GetAllQuestionsCount");
    }

    public getQuestions(userId: number, page: number, size: number, state: number, order: number) {
        return WebApi.get<IQuestionContract[]>("AI/GetQuestions/" + userId + '/' + page + '/' + size + '/' + state + '/' + order);
    }

    public getQuestionDetail(questionId: number) {
        return WebApi.get<IQuestionContract>("AI/GetQuestionDetail/" + questionId);
    }

    public addComment(comment: IAddCommentRequest) {
        return WebApi.post<ICommentContract>("AI/AddComment", comment);
    }

    public updateComment(comment: IUpdateCommentRequest) {
        return WebApi.post<ICommentContract>("AI/UpdateComment", comment);
    }

    public likeComment(commentId: number, personId: number) {
        return WebApi.get<boolean>("AI/LikeComment/" + commentId + '/' + personId);
    }

    public dislikeComment(commentId: number, personId: number) {
        return WebApi.get<boolean>("AI/DislikeComment/" + commentId + '/' + personId);
    }

    public getRNDPeople() {
        return WebApi.get<IPersonContract[]>("AI/GetRNDPeople");
    }

    public referPeople(questionId: number, peopleIds: number[]) {
        return WebApi.post<boolean>("AI/ReferPeople/" + questionId, peopleIds);
    }

    public updateQuestionState(questionId) {
        return WebApi.get<boolean>("AI/UpdateQuestionState/" + questionId);
    }

    public addSummaryToQuestion(summary: string, questionId: number) {
        return WebApi.post<IQuestionContract>("AI/AddSummaryToQuestion/" + questionId, summary)
    }

    public filterQuestionByProduct(questionId: number) {
        return WebApi.get<IQuestionContract[]>("AI/FilterQuestionByProduct/" + questionId)
    }

}
export interface IQuestionType {
    id: number;
    type: string;
    supportorId: number;
    family: IFamilyContract;
}

export interface IAddOrUpdateQuestionRequest {
    id?: number;
    userId: number;
    title: string;
    content: string;
    productId?: number;
    supportId?: number;
    isPrivate: boolean;
}

export interface IQuestionContract {
    id: number;
    typeId: number;
    title: string;
    content: string;
    questionTypeId: number;
    comments: ICommentContract[];
    personName: string;
    createDate: string;
    likeNumber: number;
    questionType: string;
    referPeople: IPersonContract[];
    summary: string;
    state: string;
    supportorId: number;
    familyName: string;
    isPrivate: boolean;
}

export interface ICommentContract {
    id: number;
    content: string;
    likeNumber: number;
    createDate: string;
    questionId: number;
    parentCommentId: number;
    personName: string;
    isRefer: boolean;
    isLikeComment: boolean;
    isDislikeComment: boolean
}

export interface IAddCommentRequest {
    userId: number;
    questionId: number;
    parentCommentId: number;
    content: string;
    isRefer: boolean;
}

export interface IUpdateCommentRequest {
    id: number;
    content: string;
}

export interface IPersonContract {
    id?: number;
    name: string;
    role: RoleType
}

export enum RoleType {
    RND = 1,
    Support = 2,
    Customer = 3
}

export interface IFamilyContract {
    id: number;
    name: string;
}