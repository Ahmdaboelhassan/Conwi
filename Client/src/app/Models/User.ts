export class User {
  constructor(
    public Username: string,
    public Email: string,
    private _token: string,
    public ExpireOn: Date,
    public IsEmailConfrimed: boolean
  ) {}

  get getToken() {
    let dateBetween = new Date(this.ExpireOn).getTime() - new Date().getTime();
    if (dateBetween <= 0) return null;
    return this._token;
  }
}
