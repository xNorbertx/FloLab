import { Component, EventEmitter, OnInit, Input, Output } from '@angular/core';
import { Selection } from '../_models/selection';
import { Months } from '../_util/months';
import '../_util/date.extensions';
import { AuthService } from '../_services/auth/auth.service';

@Component({
  selector: 'week-calendar',
  templateUrl: './week-calendar.component.html',
  styleUrls: ['./week-calendar.component.scss']
})
export class WeekCalendarComponent implements OnInit {
    @Output() event = new EventEmitter<string>();

    private _selection: Selection[];

    @Input() set selection(val: Selection[]) {
        this._selection = val;
        if (val) {
            this.setSelection(val);
        }
    }

    get selection(): Selection[] {
        return this._selection;
    }

    currentWeek: number;
    week: number;
    currentYear: number;
    year: number;
    date: Date;

    userId: string;
    weekDays: string[] = ["", "", "", "", "", "", ""];   

    setDates(d: Date): void {
        // Copy date so don't modify original
        d = new Date(Date.UTC(d.getFullYear(), 0, d.getDayOfYear()));
        // Get first day of year
        const yearStartDay = new Date(Date.UTC(d.getUTCFullYear(), 0, 1)).getDay();
        // Get first day of current week
        const firstDayCurWeek = 7 * (this.week - 1) - (2 - yearStartDay);
        // Set values for each day of the week
        let date;
        for (let i = 0; i < 7; i++) {
            date = new Date(this.year, 0, firstDayCurWeek + i);
            this.weekDays[i] = date.getDate() + " " + Months[date.getMonth()];
        }
    }

    constructor() { }

    ngOnInit() {
        this.date = new Date(Date.UTC(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()));
        this.year = this.currentYear = this.date.getFullYear();
        this.week = this.currentWeek = this.date.getWeek();
        this.setDates(this.date);
        this.activateDivs();
    }

    changeWeek(days: number): void {
        this.removeSelection();
        this.activateDivs();
        this.date = this.date.addDays(days);
        this.week = this.date.getWeek();
        this.year = this.date.getFullYear();
        this.setDates(this.date);
        this.event.next();
    }

    activateDivs() {
        let divs = document.querySelectorAll(".time-field");
        for (let i = 0; i < divs.length; i++) {
            divs[i].classList.remove('taken');
            divs[i].classList.add('active');
        }

        var self = this;
        let activeDivs = document.querySelectorAll(".active");
        for (let i = 0; i < activeDivs.length; i++) {
            activeDivs[i].addEventListener('mousedown', function (e) {
                const target = e.target as HTMLDivElement;
                target.setAttribute('style', 'background-color: red');
                self.select(target);
            });
        }
    }

    setSelection(val: Selection[]): void {
        for (let i = 0; i < val.length; i++) {
            let startHour = new Date(val[i].start).getHours();
            let endHour = new Date(val[i].end).getHours();
            startHour = startHour + (new Date(val[i].start).getMinutes() === 30 ? 0.5 : 0);
            endHour = endHour + (new Date(val[i].end).getMinutes() === 30 ? 0.5 : 0);
            let day = new Date(val[i].start).getDay();
            let dayChildren = document.getElementById("day-wrapper-" + day).children;
            for (let j = 0; j < dayChildren.length; j++) {
                if (parseInt(dayChildren[j].id) >= startHour && parseInt(dayChildren[j].id) <= endHour) {
                    dayChildren[j].classList.add("selected");
                }
            }
        }
    }

    removeSelection(): void {
        let selected = document.querySelectorAll(".selected");
        for (let i = 0; i < selected.length; i++) {
            selected[i].classList.remove("selected");
        }
    }

    select(el: HTMLDivElement): void {
        let day = parseInt(el.parentElement.id.substring(12, el.parentElement.id.length));
        let hours = parseInt(el.id);
        let start = this.date.addDays(day).addHours(hours); 
        let end = this.date.addDays(day).addHours(hours + 0.5);
    }

    fillInnerHtml(el: MouseEvent, utf8code: string) {
        el.srcElement.innerHTML = utf8code;
    }
}
