import { Component, ElementRef, Input, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Router } from '@angular/router';

import { FlareService } from './flare.service';
import { ReportService } from './service/report.service';
import { Report } from './model/report';
import 'rxjs/add/operator/switchMap';
import * as d3 from 'd3';
import { JOB_STATUS } from './constants';

@Component({
	selector: 'report',
	templateUrl: './report.component.html',
	styleUrls: ['./report.component.css']
})

export class ReportComponent implements OnInit {
	@Input() id: number;

	private parentNativeElement: any;

	private report: Report;

	private loading = true;

	private statusDict: Object;

	constructor(
		private route: ActivatedRoute,
		private flareService: FlareService,
		private reportService: ReportService,
		private router: Router
	) {
		//this.parentNativeElement = element;
	}

	ngOnInit(): void {
		this.statusDict = JOB_STATUS;
		this.route.params
			.subscribe(params => this.id = + params['id']);

		this.reportService.getReportById(this.id).then((function (report) {
			console.log(JSON.stringify(report));
			this.report = report;//{"Id":43,"LCID":"LC9999","Status":"8","Product":"XenDesktop 7.15","Component":"Graphics","Description":"TEST","TracePattern":"","NextStep":"","RelevantIssues":["LC8989    "],"RelevantJobs":[],"NodeTree":{"name":"Application launch Pattern","children":[{"name":"1: connection preparasion","children":[{"name":"ctx::Connection::GetClientData: Domain            dev","children":null,"traceline":115533,"debugging":false,"breaking":true},{"name":"ctx::Connection::GetClientData: UserName          administrator","children":null,"traceline":115534,"debugging":false,"breaking":false},{"name":"ctx::Connection::GetClientData: ClientAddress     10.158.209.106:53267","children":null,"traceline":115543,"debugging":true,"breaking":false},{"name":"ENTRY --- ctx::CWtsProtocolConnection::AuthenticateClientToSession: Connection Ticks 47","children":null,"traceline":116115,"debugging":false,"breaking":false},{"name":"RpmNotifyThread: NOTIFY_CONNECT, SessionId 0","children":null,"traceline":116207,"debugging":false,"breaking":false},{"name":"ENTRY --- ctx::CWtsProtocolConnection::ConnectNotify: Session Id 7, new Session Id 7, Connection Ticks 13057","children":null,"traceline":128560,"debugging":false,"breaking":false},{"name":"ENTRY --- ctx::CWtsProtocolConnection::IsUserAllowedToLogon: Session Id 7, new Session Id 7, Connection Ticks 42713","children":null,"traceline":168696,"debugging":false,"breaking":false},{"name":"ctx::Connection::IsUserAllowedToLogon: CTXLIC Client Name WIN7-4-SF","children":null,"traceline":168708,"debugging":false,"breaking":false},{"name":"ctx::Connection::IsUserAllowedToLogon: CTXLIC User Name administrator","children":null,"traceline":168709,"debugging":false,"breaking":false},{"name":"ctx::Connection::IsUserAllowedToLogon: Returning success","children":null,"traceline":168828,"debugging":false,"breaking":false},{"name":"ENTRY --- ctx::CWtsProtocolConnection::SessionArbitrationEnumeration: Session Id 7, Connection Ticks 43103","children":null,"traceline":170286,"debugging":false,"breaking":false},{"name":"ENTRY --- ctx::CWtsProtocolConnection::LogonNotify: Session Id 7, new Session Id 7, Connection Ticks 48095","children":null,"traceline":181744,"debugging":false,"breaking":false}],"traceline":0,"debugging":false,"breaking":false},{"name":"2: TD accepts connection","children":null,"traceline":0,"debugging":false,"breaking":false},{"name":"3: RPM connection initialization","children":null,"traceline":0,"debugging":false,"breaking":false},{"name":"4: Connection Validation","children":null,"traceline":0,"debugging":false,"breaking":false},{"name":"5: Session Creation","children":null,"traceline":0,"debugging":false,"breaking":false},{"name":"6: Application Startup","children":null,"traceline":0,"debugging":false,"breaking":false}],"traceline":0,"debugging":false,"breaking":false},"StartTime":"2017-11-17T11:07:28.6696397+08:00","EndTime":"2017-11-20T14:43:18+00:00","ParseStartTime":null,"ParseEndTime":"2017-11-17T11:12:17.8749931+08:00","AnalysisStartTime":"2017-11-20T14:43:16+00:00","AnalysisEndTime":"2017-11-20T14:43:18+00:00","StatusMsg":null,"RootCuase":"The connection sequence breaks around calling SHA_LaunchResolvedApp and printing Log : SHA_LaunchResolvedApp: The app being launched is \"\"DAEMON Tools Lite\"\", pCmdLine \"\"\"\"C:\\Windows\\notepad.exe\"\"\"\""};
			

			let margin = { top: 20, right: 120, bottom: 20, left: 120 },
				width = 600 - margin.right - margin.left,
				height = 800 - margin.top - margin.bottom;

			let i = 0, duration = 750, root;

			let tree = d3.layout.tree()
				.size([height, width]);

			let diagonal = d3.svg.diagonal()
				.projection(function (d) { return [d.y, d.x]; });

			let svg = d3.select("#patternChart").append("svg")
				.attr("width", "100%")
				.attr("height", height + margin.top + margin.bottom)
				.append("g")
				.attr("transform", "translate(" + margin.left + "," + margin.top + ")");

			function update(source) {

				// Compute the new tree layout.
				var nodes = tree.nodes(root).reverse(),
					links = tree.links(nodes);

				// Normalize for fixed-depth.
				nodes.forEach(function (d) { d.y = d.depth * 180; });

				// Update the nodes…
				var node = svg.selectAll("g.node")
					.data(nodes, function (d) { return d['id'] || (d['id'] = ++i); });

				// Enter any new nodes at the parent's previous position.
				var nodeEnter = node.enter().append("g")
					.attr("class", "node")
					.attr("transform", function (d) { return "translate(" + source.y0 + "," + source.x0 + ")"; })
					.on("click", click);

				var circleNode = nodeEnter.append("circle")
					.attr("r", 1e-6)
					.style("fill", function (d) { return d['_children'] ? "lightsteelblue" : "#fff"; });

				circleNode.append("title").text(d => d['name']);

				nodeEnter.append("text")
					.attr("x", function (d) { return d.children || d['_children'] ? -10 : 10; })
					.attr("dy", ".35em")
					.attr("text-anchor", function (d) { return d.children || d['_children'] ? "end" : "start"; })
					.text(function (d) { return d['name']; })
					.style("fill-opacity", 1e-6);

				// Transition nodes to their new position.
				var nodeUpdate = node.transition()
					.duration(duration)
					.attr("transform", function (d) { return "translate(" + d.y + "," + d.x + ")"; });
/* red
    background-color: #f8d7da;
	border-color: #f5c6cb;
	
	yellow
	background-color: #fff3cd;
    border-color: #ffeeba;
*/
				nodeUpdate.select("circle")
					.attr("r", 4.5)
					.style("fill", function (d) { 
						console.log(d["breaking"] + ' ' + d["debugging"]); 
						if (d['breaking']) {
							return "#f8d7da";
						} else if (d['debugging']) {
							return "#fff3cd";
						} else if (d['_children']) {
							return "lightsteelblue";
						} else {
							return "#fff"; 
						}
						
					})
					.style("stroke", d => {
						if (d['breaking']) {
							return "#f5c6cb";
						} else if (d['debugging']) {
							return "#ffeeba";
						} else {
							return "steelblue"; 
						}
					});

				nodeUpdate.select("text")
					.style("fill-opacity", 1);

				// Transition exiting nodes to the parent's new position.
				var nodeExit = node.exit().transition()
					.duration(duration)
					.attr("transform", function (d) { return "translate(" + source.y + "," + source.x + ")"; })
					.remove();

				nodeExit.select("circle")
					.attr("r", 1e-6);

				nodeExit.select("text")
					.style("fill-opacity", 1e-6);

				// Update the links…
				var link = svg.selectAll("path.link")
					.data(links, function (d) { return d.target['id']; });

				// Enter any new links at the parent's previous position.
				link.enter().insert("path", "g")
					.attr("class", function (d) {
						return d.target['breaking'] ? 'link breaking_color' : 'link';
					})
					.attr("d", function (d) {
						var o = { x: source.x0, y: source.y0 };
						return diagonal({ source: o, target: o });
					});

				// Transition links to their new position.
				link.transition()
					.duration(duration)
					.attr("d", diagonal);

				// Transition exiting nodes to the parent's new position.
				link.exit().transition()
					.duration(duration)
					.attr("d", function (d) {
						var o = { x: source.x, y: source.y };
						return diagonal({ source: o, target: o });
					})
					.remove();

				// Stash the old positions for transition.
				nodes.forEach(function (d) {
					d['x0'] = d.x;
					d['y0'] = d.y;
				});
			}

			let tt = this;
			function click(d) {
				if (d.children) {
					d._children = d.children;
					d.children = null;
				} else {
					d.children = d._children;
					d._children = null;
				}
				if (d.children) {
					update(d);
				} else {
					tt.router.navigate(["/analyzer", tt.id, d.traceline]);
				}
			}

			function collapse(d) {
				if (d.children) {
					d._children = d.children;
					d._children.forEach(collapse);
					d.children = null;
				}
			}

			root = this.report.NodeTree;// || {"name":"Empty pattern"};//this.flareService.getFlare();
			if (root) {
				root.x0 = height / 2;
				root.y0 = 0;
				root.children.forEach(collapse);
				update(root);
			}
			d3.select(self.frameElement).style("height", "800px");

			this.loading = false;
		}).bind(this));

	}

}