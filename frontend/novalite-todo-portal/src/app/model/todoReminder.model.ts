export class TodoReminderModel {
  id: string = '';
  sent: boolean = false;
  timestamp: string = '';

  constructor(obj?: any) {
    if(obj) {
      this.id = obj.id;
      this.sent = obj.sent;
      this.timestamp = obj.timestamp;
    }
  }
}
