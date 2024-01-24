export class TodoItemModel {
  id: string = '';
  content: string = '';
  status: number = 0;

  constructor(obj?: any) {
    if(obj) {
      this.id = obj.id;
      this.content = obj.content;
      this.status = obj.status;
    }
  }
}
