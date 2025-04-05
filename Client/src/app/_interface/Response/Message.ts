export interface Message {
  id?: number;
  content: string;
  senderId: string;
  revieverId: string;
  isReaded?: boolean;
  isDelivered?: boolean;
  isDeleted?: boolean;
  sendTime: Date;
  deliverTime?: Date;
}
