import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';
import { AppUser } from './models/appState/app-user.model';
import {
  enterpriseIdChanged,
  userAuthenticated
} from './store/actions/appState.actions';

const DefaultLang = 'en';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  private authSubscription?: Subscription;

  title = 'enterprise-assistant';
  authenticated = false;

  constructor(
    private translate: TranslateService,
    private authService: OidcSecurityService,
    private store: Store,
    private cookieService: CookieService
  ) {
    translate.setDefaultLang(DefaultLang);
    translate.use(DefaultLang);
  }

  ngOnInit(): void {
    this.authSubscription = this.authService
      .checkAuth()
      .subscribe(({ isAuthenticated, userData }) => {
        if (!isAuthenticated) {
          this.login();
        } else {
          this.authenticated = isAuthenticated;
          const appUser: AppUser = {
            userId: userData.user_id,
            name: userData.name,
            email: userData.email
          };
          console.log(appUser);

          const enterpirseIds = userData.enterprise_ids as string | undefined;
          if (enterpirseIds) {
            appUser.enterpriseIds = enterpirseIds?.split(' ');
            if (appUser.enterpriseIds.length == 1) {
              this.cookieService.set('enterpriseId', appUser.enterpriseIds[0]);
            }
          }

          this.store.dispatch(userAuthenticated(appUser));
          const enterpriseId = this.cookieService.get('enterpriseId');
          if (enterpriseId) {
            this.store.dispatch(enterpriseIdChanged({ enterpriseId }));
            console.log('user authorized in ' + enterpriseId);
          }
        }
      });
  }

  ngOnDestroy(): void {
    this.authSubscription?.unsubscribe();
  }

  login() {
    this.authService.authorize();
  }
}
