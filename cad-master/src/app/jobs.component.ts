import { Component, OnInit } from '@angular/core';

import { JobService } from './service/job.service';
import { JobDetail } from './model/job-detail';

import { DictService } from './service/dict.service';

import { Product } from './model/product';
import { ProductVersion } from './model/product-version';
import { ProductComponent } from './model/product-component';
import { JOB_STATUS } from './constants';

@Component({
  selector: 'jobs',

  templateUrl: './jobs.component.html'
})

export class JobsComponent  {
  title: 'Jobs';
  jobDetails: JobDetail[];

  statusDict: Object;
  productDict: Object;
  productVersionDict: Object ;
  productComponentDict: Object;

  loading = true;

  jobSummary: Object;

  constructor(private jobService: JobService, private dictService: DictService) {}

  ngOnInit(): void {
    //this.jobService.getJobs().then(jobDetails => this.jobDetails = jobDetails);
    let tt = this;
    tt.statusDict = JOB_STATUS;
    tt.productDict = {};
    tt.productComponentDict = {};
    tt.productVersionDict = {};
    /* this.dictService.getProducts().then(products =>
      products.forEach(function(item) { tt.productDict[item.ID] = item.ProductName; console.log(JSON.stringify(tt.productDict));})
    ); //this.productDict = [{"ID":24,"ProductLine":"                                                  ","ProductName":"Access Gateway                                    "}];
    this.dictService.getComponents().then(components => components.forEach(item => tt.productComponentDict[item.ID] = item.Name));
    this.dictService.getVersions().then(versions => versions.forEach(item => tt.productVersionDict[item.ID] = item.Name));  */
    Promise.all([
      this.dictService.getProducts(),
      this.dictService.getComponents(),
      this.dictService.getVersions()]).then(data => {
        data[0].forEach(item => tt.productDict[item.ID] = item.ProductName);
        data[1].forEach(item => tt.productComponentDict[item.ID] = item.Name);
        data[2].forEach(item => tt.productVersionDict[item.ID] = item.Name);

        tt.jobService.getJobs().then(jobDetails => {
          tt.jobDetails = jobDetails;
          tt.loading = false;

          tt.jobSummary = {};
          tt.jobSummary["total"] = jobDetails.length;
          tt.jobSummary["success"] = jobDetails.filter(x => x.Status == 8).length;
          tt.jobSummary["failure"] = jobDetails.filter(x => x.Status == 99).length;
          tt.jobSummary["processing"] = tt.jobSummary["total"] - tt.jobSummary["success"];
        });
      })
  }
}