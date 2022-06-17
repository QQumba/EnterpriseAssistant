import { Injectable, OnDestroy } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import { Store } from '@ngrx/store';
import { map, Observable, Subscription, take, tap } from 'rxjs';
import { selectEnterpriseId } from '../store/selectors/app-user.selector';

@Injectable({
  providedIn: 'root'
})
export class NewUserGuard implements CanActivate, OnDestroy {
  private subscription?: Subscription;

  $unauthorized = this.store
    .select(selectEnterpriseId)
    .pipe(map((id) => id == null));

  constructor(private store: Store, private router: Router) {}

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.$unauthorized.pipe(
      map((u) => {
        if (u) {
          console.log('authorized');
          return u;
        }
        return this.router.parseUrl('/enterprise');
      })
    );
  }
}
