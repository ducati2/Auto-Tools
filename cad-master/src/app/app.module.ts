import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule }   from '@angular/router';
import { HttpModule }    from '@angular/http';
import {NgbModal, NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { LoadingModule } from 'ngx-loading';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import { FlareService } from './flare.service';
import { ReportService } from './service/report.service';
import { JobService } from './service/job.service';
import { DictService } from './service/dict.service';
import { AnalyzerService } from './service/analyzer.service';

import { DownloadComponent } from './download.component';
import { AppComponent } from './app.component';
import { NewComponent } from './new.component';
import { DashboardComponent } from './dashboard.component';
import { JobsComponent } from './jobs.component';
import { ReportComponent } from './report.component';
import { AnalyzerComponent } from './analyzer.component';
import { SearchComponent } from './search.component';
import { ModalComponent } from './modal.component';
import { LoginComponent } from './login.component';
import { ContentComponent } from './content.component';


@NgModule({
  declarations: [
    AppComponent,
    NewComponent,
    DashboardComponent,
    JobsComponent,
    ReportComponent,
    AnalyzerComponent,
    SearchComponent,
    ModalComponent,
    DownloadComponent,
    LoginComponent,
    ContentComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
    {
      path: '',
      redirectTo: '/login',
      pathMatch: 'full'
    },
	  {
	    path: 'new',
	    component: NewComponent
    },
    {
	    path: 'new/:caseId',
	    component: NewComponent
	  },
	  {
	    path: 'dashboard',
	    component: DashboardComponent
	  },
	  {
	    path: 'jobs',
	    component: JobsComponent
	  },
	  {
	    path: 'report/:id',
	    component: ReportComponent
    },
    {
      path: 'analyzer/:id',
	    component: AnalyzerComponent
    },
    {
      path: 'analyzer/:id/:line',
	    component: AnalyzerComponent
    },
    {
      path: 'search',
      component: SearchComponent
    },
    {
      path: 'download',
      component: DownloadComponent
    }
   
    ]),
    LoadingModule,
    InfiniteScrollModule
  ],
  providers: [FlareService, ReportService, JobService, NgbModal, NgbActiveModal, DictService, AnalyzerService],
  bootstrap: [AppComponent],
  entryComponents: [ModalComponent]
})

export class AppModule { }
