import {Component, Input} from '@angular/core';
import { Router } from '@angular/router';

import {NgbModal, NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'modal-content',
  template: `
    <div class="modal-header">
      <h4 class="modal-title">Information</h4>
      <!--<button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss('Cross click')">
        <span aria-hidden="true">&times;</span>
      </button>-->
    </div>
    <div class="modal-body">
      <p>Created new job successfully!</p>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-success" (click)="closeModal()">OK</button>
    </div>
  `
})
export class ModalComponent {
  @Input() name;

  constructor(public activeModal: NgbActiveModal, private router: Router) {

  }

  closeModal() {
    this.activeModal.close();

    this.router.navigate(["/jobs"]);
  }
}