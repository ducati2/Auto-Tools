import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Product } from '../model/product';
import { ProductVersion } from '../model/product-version';
import { ProductComponent } from '../model/product-component';

@Injectable()
export class DictService {
    private dictUrl = '/api/dict';

    constructor(private http: Http) {}

    getProducts(): Promise<Product[]> {
        const url = `${this.dictUrl}/products`;

        return this.http.get(url)
            .toPromise().then(response => response.json() as Product[]);
    }

    getVersions(): Promise<ProductVersion[]> {
        const url = `${this.dictUrl}/versions`;

        return this.http.get(url)
            .toPromise().then(response => response.json() as ProductVersion[]);
    }

    getComponents(): Promise<ProductComponent[]> {
        const url = `${this.dictUrl}/components`;

        return this.http.get(url)
            .toPromise().then(response => response.json() as ProductComponent[]);
    }
}