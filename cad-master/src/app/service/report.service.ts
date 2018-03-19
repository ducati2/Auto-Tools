import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Report } from '../model/report';

@Injectable()
export class ReportService {
    private reportUrl = '/api/report';

    constructor(private http: Http) {}

    getReportById(id: number): Promise<Report> {
        const url = `${this.reportUrl}/${id}`;

        return this.http.get(url)
            .toPromise().then(response => response.json() as Report);
    }
}
