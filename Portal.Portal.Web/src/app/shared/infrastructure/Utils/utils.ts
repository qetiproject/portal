import { FileType } from '../PortalHttpClient';

const fileTypes = FileType;

export function getFileClassByExtension(fileExtension: string): string {
  switch (fileExtension) {
    case fileTypes['Word']:
      return 'k-i-file-word word-icon';
    case fileTypes['Excel']:
      return 'k-i-file-excel excel-icon';
    case fileTypes['PowerPoint']:
      return 'k-i-file-ppt powerpoint-icon';
    case fileTypes['PDF']:
      return 'k-i-file-pdf pdf-icon';
    case fileTypes['Image']:
      return 'k-i-file-image jpg-icon';
    case fileTypes['File']:
      return 'k-i-file file-icon';
    default:
      return 'k-i-file-vertical file-not-exist';
  }
}

export function assignFileTypeForUpload(file) {
  let fileExtensionOnUpload = file.target.files[0].name.split('.');
  fileExtensionOnUpload = fileExtensionOnUpload[fileExtensionOnUpload.length - 1].toLowerCase();
  switch (fileExtensionOnUpload) {
    case 'doc':
      return 'Word';
    case 'docx':
      return 'Word';
    case 'xls':
      return 'Excel';
    case 'xlsx':
      return 'Excel';
    case 'ppt':
      return 'PowerPoint';
    case 'pptx':
      return 'PowerPoint';
    case 'pdf':
      return 'PDF';
    case 'jpg':
      return 'Image';
    case 'jpeg':
      return 'Image';
    case 'png':
      return 'Image';
    case 'heic':
      return 'Image';
    default:
      return 'File';
  }
}

export function enumToArray(enumToTransform, resourceModel) {
    return Object.keys(enumToTransform).map(key => ({ value: key, text: resourceModel[key.charAt(0).toLowerCase() + key.slice(1)] }));
}
