import { ModalManager } from 'ngb-modal';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-message-modal',
  templateUrl: './message-modal.component.html',
  styleUrls: ['./message-modal.component.css']
})
export class MessageModalComponent implements OnInit {
  @ViewChild('messageModal') messageModal;
  // @Output() applyFilterEvent = new EventEmitter();

  private modalRef;
  pageTitle = 'Message Modal';
  messageText: string;
  messageTitle: string;

  constructor(private modalService: ModalManager) { }

  ngOnInit(): void {
    // this.initializeDeptNameFilter('customer service');
  }

  openModal(messageTitle, messageText): void {
    this.messageTitle = messageTitle;
    this.messageText = messageText;

    this.modalRef = this.modalService.open(this.messageModal, {
        size: 'md',
        modalClass: '',
        hideCloseButton: false,
        centered: true,
        backdrop: 'static',
        keyboard: false,
        animation: true,
        closeOnOutsideClick: true,
    });
  }

  closeModal() {
    this.modalService.close(this.modalRef);
  }
}
