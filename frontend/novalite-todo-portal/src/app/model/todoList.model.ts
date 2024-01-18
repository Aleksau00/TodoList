export class TodoListModel {
  id: string = '';
  title: string = '';
  description: string = '';

  constructor(obj?: any) {
    if(obj) {
      this.id = obj.id;
      this.title = obj.title;
      this.description = obj.description;
    }
  }
}
