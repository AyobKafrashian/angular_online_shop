import { env } from "process";
import { environment } from "src/environments/environment";

export const DomainName = environment.production ? 'http://ayobkfr.ir':'https://localhost:44302';

export const ImagePath = DomainName + '/Images/Products/Origin/';

export const ImageGalleryPath = DomainName + '/Images/Product-galleries/';