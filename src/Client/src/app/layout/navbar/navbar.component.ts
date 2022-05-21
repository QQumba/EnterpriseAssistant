import { Component, OnInit } from '@angular/core';
import {
  faAngleDown,
  faCaretDown,
  faCircleUser,
  faUser
} from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  account = faCircleUser;
  arrowDown = faAngleDown;

  caretDown = faCaretDown;

  constructor(private translate: TranslateService) {}

  switchLang(): void {
    if (this.translate.currentLang === 'en') this.translate.use('ua');
    else this.translate.use('en');
  }
}
