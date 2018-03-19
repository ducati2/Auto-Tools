import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Job } from '../model/job';
import { JobDetail } from '../model/job-detail';
import { OneFixCase } from '../model/onefix-detail';
import { Pattern } from '../model/pattern';

@Injectable()
export class JobService {
    private jobUrl = '/api/job';
    private caseDetailUrl = '/api/job/onefix';
    private patternListUrl = '/api/pattern';

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    create(job: Job): Promise<Job> {
        return this.http.post(this.jobUrl, JSON.stringify(job), { headers: this.headers })
            .toPromise()
            .then(res => res.json().data as Job);


    }

    getReportById(id: number): Promise<Job> {
        const url = `${this.jobUrl}/${id}`;

        return this.http.get(url)
            .toPromise().then(response => response.json() as Job);
    }

    getJobs(): Promise<JobDetail[]> {
        return this.http.get(this.jobUrl).
                toPromise().then(response => response.json() as JobDetail[]);
    }

    getOneFixCaseDetailById(lcid: string): Promise<OneFixCase> {
        const url = `${this.caseDetailUrl}/${lcid}`;

        return this.http.get(url)
            .toPromise().then(response => response.json() as OneFixCase);
    }


    //get the pattent id list by pattern list url.
    getAllPatternIdList(): Promise<Pattern[]>{
        
        return this.http.get(this.patternListUrl).
                toPromise().then(response => response.json() as Pattern[]);
    }
}