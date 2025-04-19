export interface Notification {
  id: number;
  title: string;
  message: string;
  time: Date;
  isRead: boolean;
  photo: string;
  type: NotificationTypes;
}

export enum NotificationTypes {
  Like = 1,
  Follow = 2,
}
