export interface AuthResponseModel {
  messages?: string[];
  userName: string;
  email?: string;
  token?: string;
  expireOn?: Date;
  isAuthenticated: boolean;
  isEmailConfrimed: boolean;
}
