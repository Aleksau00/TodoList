export class AttachmentsDTOModel {
  id: string = '';
  fileName: string = '';

  constructor(obj?: any) {
    if(obj) {
      this.id = obj.id;
      this.fileName = obj.fileName;
    }
  }
}
