import { Component, OnInit } from '@angular/core';
import {
  faAngleDown,
  faAt,
  faBell,
  faCaretDown,
  faCircleUser,
  faGear,
  faSignOut,
  faUser,
  faUserPlus
} from '@fortawesome/free-solid-svg-icons';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { map } from 'rxjs';
import { AvatarService } from 'src/app/services/avatar.service';
import { selectAppUser } from 'src/app/store/selectors/app-user.selector';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  // menu
  arrowDown = faAngleDown;

  // account
  account = faCircleUser;
  caretDown = faCaretDown;

  // account dropdown
  at = faAt;
  gear = faGear;
  invite = faUserPlus;
  signout = faSignOut;

  isMenuOpened = false;
  $appUser = this.store.select(selectAppUser);
  $isEnterpriseUser = this.$appUser.pipe(map((u) => !!u.enterpriseId));

  constructor(
    private translate: TranslateService,
    private store: Store,
    private authService: OidcSecurityService,
    public avatarService: AvatarService
  ) {}

  switchLang(): void {
    if (this.translate.currentLang === 'en') this.translate.use('ua');
    else this.translate.use('en');
  }

  toggleMenu() {
    this.isMenuOpened = !this.isMenuOpened;
  }

  logout() {
    this.authService.logoff();
  }
}
