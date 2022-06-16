import { Injectable, OnDestroy } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import { Store } from '@ngrx/store';
import { map, Observable, Subscription, take } from 'rxjs';
import { selectEnterpriseId } from '../store/selectors/app-user.selector';

@Injectable({
  providedIn: 'root'
})
export class NewUserGuard implements CanActivate, OnDestroy {
  private subscription?: Subscription;

  $newUser = this.store.select(selectEnterpriseId).pipe(map((id) => !!id));

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
    let newUser = true;
    this.subscription = this.$newUser.subscribe((nu) => (newUser = nu));
    if (newUser) {
      return this.router.parseUrl('/enterprise');
    }
    return true;
  }
}
