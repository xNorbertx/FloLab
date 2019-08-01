import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'month-calendar',
  templateUrl: './month-calendar.component.html',
  styleUrls: ['./month-calendar.component.scss']
})
export class MonthCalendarComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


    fillInnerHtml(el: MouseEvent, utf8code: string) {
        el.srcElement.innerHTML = utf8code;
    }
}
