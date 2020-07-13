
export interface ConversationList {
    id: number;
    partnerUserName: number;
    lastMessage: string;
}

export interface Conversation {
    id: number;
    partnerUserName: string;
    messages: Message[];
}

export interface Message {
    isSent: boolean;
    timeStamp: Date;
    message: string;
}

export interface User {
    id: number;
    name: string;
}
