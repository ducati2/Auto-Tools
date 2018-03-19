import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { AnalyzerService } from './service/analyzer.service';
import { CDFLine } from './model/cdf-line';
import { SourceParam } from './model/source-param';
import { TRUNK_SIZE } from './constants';


@Component({
  selector: 'analyzer',
  templateUrl: './analyzer.component.html'
})

export class AnalyzerComponent implements OnInit {

  title: 'Analyzer';
  @Input() id: number;
  @Input() line: number;

  trace: CDFLine[];
  source: string;

  loading = true;

  jobId: number;
  condition;
  currentIndex: number;
  //TRUNK_SIZE = 500;

  incrementalLoading: boolean;

  currentSelectedCDFLine: CDFLine;
  lineList = null;

  constructor(
    private analyzerService: AnalyzerService,
    private route: ActivatedRoute
  ) {
    //this.parentNativeElement = element;
  }

  ngOnInit(): void {
    this.route.params
      .subscribe(params => {
        this.id = + params['id'];
        this.line = params['line'] ? params['line'] : 0; 
      });
    if (this.line < (TRUNK_SIZE / 2)) {
      this.currentIndex = 0;
    } else {
      this.currentIndex = (this.line - (TRUNK_SIZE / 2));
    }

    console.log("ngOnInit. line info: " + this.line);

    let endIndex = this.currentIndex + TRUNK_SIZE;
    this.incrementalLoading = true;
    this.analyzerService.getCDFTraceByJobId(this.id, this.condition, this.currentIndex, endIndex).then(trace => {
      this.trace = trace;

      this.loading = false;

      this.currentIndex += TRUNK_SIZE;
      this.incrementalLoading = false;

      function scrollSpecTraceLine(l) {
        let td = document.querySelector("td[line=\"" + l +"\"]");
        if (td) {
          td.parentElement.style.backgroundColor = '#fff3cd';
          td.parentElement.scrollIntoView(true);
        }
      }
      setTimeout(scrollSpecTraceLine, 1000, this.line);
    });

    this.source = "<div class=\"alert alert-info\" role=\"alert\">Please click the row to show corresponding source code.</div>";

  }

  onScroll() {
    console.log("scrolled");
    if (this.incrementalLoading)
      return;
    let endIndex = this.currentIndex + TRUNK_SIZE;

    console.log("currentIndex: " + this.currentIndex + " endIndex: " + endIndex);

    this.incrementalLoading = true;
    this.analyzerService.getCDFTraceByJobId(this.id, this.condition, this.currentIndex, endIndex).then((function (trace) {
      this.trace = this.trace.concat(trace);

      this.currentIndex += TRUNK_SIZE;
      this.incrementalLoading = false;

      function scrollSpecTraceLine(l) {
        console.log("[onScroll:scrollSpecTraceLine]");
        let td = document.querySelector("td[line=\"" + l +"\"]");
        if (td) {
          console.log("find the specified line" + this.line);
          td.parentElement.style.backgroundColor = '#fff3cd';
        }
      }
      
      console.log("onScroll::line info: " + this.line);
      setTimeout(scrollSpecTraceLine, 500, this.line);

    }).bind(this));
  }

  rowSelected(cdfLine: CDFLine) {
    console.log(JSON.stringify(cdfLine));
    if (this.currentSelectedCDFLine == null
      || !(this.currentSelectedCDFLine.ModuleName == cdfLine.ModuleName
        && this.currentSelectedCDFLine.Src === cdfLine.Src)) {
      this.currentSelectedCDFLine = cdfLine;
      this.loading = true;
      this.source = "";
      this.analyzerService.getSourceByName(cdfLine.JobID, cdfLine.ModuleName, cdfLine.Src).then(source => {
        this.source = "<pre class=\"prettyprint linenums lang-basic\">" + PR.prettyPrintOne(source, 'lang-basic', true) + "</pre>";
        this.loading = false;
        // scroll source code to selected line
        let tt = this;
        function scrollSelectedSrcLine() {
          tt.lineList = document.querySelectorAll("ol > li");
          if (tt.lineList.length == 0) {
            setTimeout(scrollSelectedSrcLine, 500);
          } else {
            if (tt.lineList.length >= cdfLine.LineNum) {
              tt.lineList[cdfLine.LineNum - 1].scrollIntoView(true);
            } else {
              tt.lineList[tt.lineList.length - 1].scrollIntoView(true);
            }
          }
        }
        setTimeout(scrollSelectedSrcLine, 500);
      }).catch(error => {
        console.log(error.status);
        if (error.status == 404) {
          this.source = "<div class=\"alert alert-warning\" role=\"alert\">Cannot find the corresponding source code.</div>";
          this.loading = false;
        }
      });
    }
    if (this.currentSelectedCDFLine != null
      && this.currentSelectedCDFLine.ModuleName == cdfLine.ModuleName
      && this.currentSelectedCDFLine.Src === cdfLine.Src
      && this.currentSelectedCDFLine.LineNum !== cdfLine.LineNum) {
        this.currentSelectedCDFLine = cdfLine;
        if (this.lineList.length >= cdfLine.LineNum) {
          this.lineList[cdfLine.LineNum - 1].scrollIntoView(true);
        } else {
          this.lineList[this.lineList.length - 1].scrollIntoView(true);
        }
      }
  }

  traceSearch(moduleName, src, func, message) {
    console.log(moduleName);
    this.condition = { module: moduleName, src: src, func: func, message: message };
   

    //added by wenpingx.
    //we need to confirm that if the highlight line is in the search result, we
    //still need to highlight it and make it in view.
  /*  if (this.line < (TRUNK_SIZE / 2)) {
      this.currentIndex = 0;
    } else {
      this.currentIndex = (this.line - (TRUNK_SIZE / 2));
    }
    let endIndex = this.currentIndex + TRUNK_SIZE;
    */
    //ended by wenpingx
   
    this.currentIndex = 0;
    let endIndex = this.currentIndex + TRUNK_SIZE;
   
    this.loading = true;
    this.incrementalLoading = true;
    this.analyzerService.getCDFTraceByJobId(this.id, this.condition, this.currentIndex, endIndex).then(trace => {
      this.trace = trace;
      this.loading = false;
      this.currentIndex += TRUNK_SIZE;
      this.incrementalLoading = false;

      function scrollSpecTraceLine(l) {
        let td = document.querySelector("td[line=\"" + l +"\"]");
        if (td) {
          td.parentElement.style.backgroundColor = '#fff3cd';
          //td.parentElement.scrollIntoView(true);
        }
      }
      
      console.log("traceSearch::line info: " + this.line);
      setTimeout(scrollSpecTraceLine, 500, this.line);

    });
  }
}