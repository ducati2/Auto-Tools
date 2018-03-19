import { Component, OnInit, Input, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Router } from '@angular/router';

import { NgForm } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './modal.component';

import { JobService } from './service/job.service';
import { DictService } from './service/dict.service';
import { Job } from './model/job';
import { Product } from './model/product';
import { ProductVersion } from './model/product-version';
import { ProductComponent } from './model/product-component';
import { OneFixCase } from './model/onefix-detail';
import { Pattern } from './model/pattern';

@Component({
  selector: 'new',
  templateUrl: './new.component.html'
})

export class NewComponent implements OnInit, AfterViewInit {
  @Input() lcid: string;

  @ViewChild('f') f: ElementRef;
  @ViewChild('productID') productID: ElementRef;
  @ViewChild('versionID') versionID: ElementRef;
  @ViewChild('componentID') componentID: ElementRef;
  @ViewChild('patternID') patternID: ElementRef;
  

  closeResult: string;

  productDict: Product[];
  productVersionDict: ProductVersion[];
  productComponentDict: ProductComponent[];
  patternDict: Pattern[];

  productiderror: boolean;
  versionerror: boolean;
  componenterror: boolean;
  traceerror: boolean;

  caseDetail: OneFixCase;

  constructor(private jobService: JobService,
    private modalService: NgbModal, 
    private dictService: DictService,
    private route: ActivatedRoute,
    private router: Router) { }

  addJob(f: NgForm): void {
    
    let job = f.value;
    if (this.lcid) {
      if (!f.value.Trace) {
        this.traceerror = f.form.controls['Trace'].hasError('required');
        return;
      }
      job["LCID"] = this.caseDetail.CaseID;
      job["ProductID"] = this.caseDetail.ServiceProduct.ID;
      job["VersionID"] = this.caseDetail.ProductVersion.ID;
      job["ComponentID"] = this.caseDetail.ProductVersion.ID;
      job["Description"] = f.value.Description || this.caseDetail.Description;
      job["PatternID"] = f.value.PatternID;
      job["Trace"] = this.caseDetail.Trace;

    } else {
      if (!f.valid) {
        this.productiderror = f.form.controls['ProductID'].hasError('required');
        this.versionerror = f.form.controls['VersionID'].hasError('required');
        this.componenterror = f.form.controls['ComponentID'].hasError('required');
        this.traceerror = f.form.controls['Trace'].hasError('required');
        return;
      }
    }
    console.log(JSON.stringify(job));
    this.jobService.create(job).then(job => {
      const modalRef = this.modalService.open(ModalComponent);
    }).catch(reason => console.log(reason));
  }

  queryCase(f: NgForm): void {
    let caseId = f.value['LCID'];
    if (caseId) {
      this.router.navigate(["/new", caseId]);
    }
  }

  ngOnInit(): void {
    this.route.params
      .subscribe(params => {
        this.lcid = (params['caseId'] ? ('' + params['caseId']) : '');
      });
    if (this.lcid) {
      this.jobService.getOneFixCaseDetailById(this.lcid).then((detail => {
        console.log(JSON.stringify(detail));
        this.caseDetail = detail;
        this.productDict = [this.caseDetail.ServiceProduct];
        this.productVersionDict = [this.caseDetail.ProductVersion];
        this.productComponentDict = [this.caseDetail.Component];
      
        
      }).bind(this));
    } else {
      this.dictService.getProducts().then(products => this.productDict = products);
      this.dictService.getComponents().then(components => this.productComponentDict = components);

    }

    //get the pattern list on init.
    console.log("get all pattern id list on init.");
    this.jobService.getAllPatternIdList().then(patterns => this.patternDict = patterns);
  }

  ngAfterViewInit() {
    if (this.lcid) {
      setTimeout(this.makeDefaultOptionSelected.bind(this).bind(this), 500);
    }
  }

  makeDefaultOptionSelected() {
    if (this.productID.nativeElement.options.length > 0) {
      this.productID.nativeElement.options[0].selected = true;
      this.versionID.nativeElement.options[0].selected = true;
      this.componentID.nativeElement.options[0].selected = true;
    } else {
      setTimeout(this.makeDefaultOptionSelected.bind(this), 500);
    }
  }

  onProductChange(productID): void {
    console.log('change product id = ' + productID);
    if (null != this.productDict) {
      let currentProduct = this.productDict.filter(x => x.ID == productID)[0];
      this.productComponentDict = currentProduct.Components;
      this.productVersionDict = currentProduct.Versions;
    }
  }

}