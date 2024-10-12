export interface AuthResponseModel {
  messages?: string[];
  userName: string;
  email?: string;
  token?: string;
  id?: string;
  expireOn?: Date;
  isAuthenticated: boolean;
  isEmailConfrimed: boolean;
}
