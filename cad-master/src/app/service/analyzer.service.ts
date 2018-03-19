import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { CDFLine } from '../model/cdf-line';

@Injectable()
export class AnalyzerService {
    private analyzerUrl = '/api/analyzer';

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    getCDFTraceByJobId(jobId: number, condition: object, startIndex: number, endIndex: number): Promise<CDFLine[]> {
        const url = `${this.analyzerUrl}/${jobId}`;

        return this.http.post(url, JSON.stringify({ "pagination": { "startIndex": startIndex, "endIndex": endIndex }, "condition": condition }), { headers: this.headers })
            .toPromise().then(response => response.json() as CDFLine[]);

    }

    getSourceByName(jobID: number, module: string, src: string): Promise<string> {
        const url = `${this.analyzerUrl}/source`;

        return this.http.post(url, JSON.stringify({ "ID": jobID, "Module": module, "Src": src }), { headers: this.headers })
            .toPromise().then(response => response.json() as string);

    }
}
