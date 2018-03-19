import { ProductComponent } from './product-component'
import { ProductVersion } from './product-version'

export class Product {
    ID: number;
    ProductLine: string;
    ProductName: string;
    Components: Array<ProductComponent>;
    Versions: Array<ProductVersion>;
}