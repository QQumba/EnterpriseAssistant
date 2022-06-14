import { Component, OnInit } from '@angular/core';
import { faAngleUp, faPlus } from '@fortawesome/free-solid-svg-icons';
import { Store } from '@ngrx/store';
import { CookieService } from 'ngx-cookie-service';
import { map } from 'rxjs';
import { enterpriseIdChanged } from 'src/app/store/actions/appState.actions';
import {
  selectAppUser,
  selectEnterpriseId
} from 'src/app/store/selectors/app-user.selector';

@Component({
  selector: 'app-enterprise-sidebar-enterprise-picker',
  templateUrl: './enterprise-sidebar-enterprise-picker.component.html',
  styleUrls: ['./enterprise-sidebar-enterprise-picker.component.scss']
})
export class EnterpriseSidebarEnterprisePickerComponent {
  // icons
  arrowUp = faAngleUp;
  plus = faPlus;

  isExpanded = false;

  $enterpriseId = this.store.select(selectEnterpriseId);
  $enterprises = this.store.select(selectAppUser).pipe(
    map((appUser) => {
      if (appUser.enterpriseId == null) {
        return appUser.enterpriseIds;
      }
      return appUser.enterpriseIds?.filter((id) => id != appUser.enterpriseId);
    })
  );

  constructor(private store: Store, private cookieService: CookieService) {}

  selectEnterprise(enterpriseId: string) {
    this.cookieService.set('enterpriseId', enterpriseId);
    this.store.dispatch(enterpriseIdChanged({ enterpriseId }));
  }
}
