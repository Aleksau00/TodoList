export class UserResponseModel {
  id: string = '';
  email: string = '';
  role: number = 0;

  constructor(obj?: any) {
    if(obj) {
      this.id = obj.id;
      this.email = obj.email;
      this.role = obj.role;
    }
  }
}
