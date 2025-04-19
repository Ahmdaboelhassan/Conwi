export interface Chat {
  userId: string;
  userLastName: string;
  userFirstName: string;
  userName: string;
  userPhoto: string;
  lastMessage: string;
  lastMessageTime: Date;
  lastMessageId?: number;
  lastMessageRead?: boolean;
}
