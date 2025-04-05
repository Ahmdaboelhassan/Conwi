import { Message } from './Message';

export interface PrivateChat {
  username: string;
  userId: string;
  userPhoto: string;
  firstName: string;
  lastName: string;
  messages: Message[];
}
