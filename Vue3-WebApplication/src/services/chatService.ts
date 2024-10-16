import { WebApi } from "./webApi";

export class ChatService {


    public getChatToken(p: Products, userid: number) {
        let url = `Chat/GetChatToken/${p}/${userid}`;
        return WebApi.get(url);
    }

    public saveChatRecord(request: ChatData) {
        let url = "Chat/SaveChatRecord";
        return WebApi.post(url, request)
    }

    public postQuestion(request: QuestionData) {
        let url = "Chat/PostQuestion";
        return WebApi.post(url, request)
    }
}
export enum Products {
    Unknow,
    PE,
    MSC,
    APM,
    AOTH
}

export interface ChatData {
    isQuestion: boolean;
    timeStamp: string;
    content: string;
    userId: number;
}

export interface QuestionData {
    userId: number;
    title: string;
    chats: ChatData[];
}