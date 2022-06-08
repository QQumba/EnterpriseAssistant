import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Subscription } from 'rxjs';
import { AppUser } from './models/appState/app-user.model';
import { userAuthenticated } from './store/actions/appState.actions';

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
    private store: Store
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
          const enterpirseIds = userData.enterpirse_ids as string | undefined;
          if (enterpirseIds) {
            appUser.enterpriseIds = enterpirseIds?.split(' ');
          }
          console.log(appUser);
          this.store.dispatch(userAuthenticated(appUser));
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
