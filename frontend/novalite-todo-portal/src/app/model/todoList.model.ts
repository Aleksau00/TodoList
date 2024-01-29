import {TodoReminderModel} from "./todoReminder.model";

export class TodoListModel {
  id: string = '';
  title: string = '';
  description: string = '';
  reminders: TodoReminderModel[] = [];

  constructor(obj?: any) {
    if(obj) {
      this.id = obj.id;
      this.title = obj.title;
      this.description = obj.description;
      this.reminders = obj.reminders;
    }
  }
}
