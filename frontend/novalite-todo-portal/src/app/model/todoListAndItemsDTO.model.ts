import {TodoListModel} from "./todoList.model";
import {TodoItemModel} from "./todoItem.model";

export class TodoListAndItemsDTOModel {
  todoList : TodoListModel = new TodoListModel();
  todoItems : TodoItemModel[] = [];

  constructor(obj?: any) {
    if(obj) {
      this.todoList = obj.todoList;
      this.todoItems = obj.todoItems;
    }
  }
}
