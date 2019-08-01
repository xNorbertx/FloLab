import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  showModal(): void {
    document.getElementById('modal').style.visibility = 'visible';
  }

  closeModal(): void {
    document.getElementById('modal').style.visibility = 'hidden';
  }

  constructor() { }
}
