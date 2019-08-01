import { Component, OnInit } from '@angular/core';
import { ModalService } from '../_services/modal/modal.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {

  headerMessage: string;
  bodyMessage: string;
  
  constructor(
    private modal: ModalService
  ) { }

  ngOnInit() {
  }

}
