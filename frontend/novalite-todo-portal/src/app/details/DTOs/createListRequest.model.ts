export class CreateListRequestModel {
  title: string = '';
  description: string = '';

  constructor(obj?: any) {
    if(obj) {
      this.title = obj.title;
      this.description = obj.description;
    }
  }
}
