export class SasDtoModel {
  sas: string = '';
  attachmentId: string = '';

  constructor(obj?: any) {
    if(obj) {
      this.sas = obj.sas;
      this.attachmentId = obj.attachmentId;
    }
  }
}
