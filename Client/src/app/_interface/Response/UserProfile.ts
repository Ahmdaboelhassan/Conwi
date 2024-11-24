import { ReadPost } from './ReadPost';

export interface UserProfile {
  firstName: string;
  lastName: string;
  country: string;
  city: string;
  email: string;
  userName: string;
  following: number;
  followers: number;
  dateOfBirth: Date;
  photoURL?: string;
  isFollowing?: boolean;
  userPosts?: ReadPost[];
}
