import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import { Store } from '@ngrx/store';
import { map, Observable, take } from 'rxjs';
import { selectEnterpriseId } from '../store/selectors/app-user.selector';

@Injectable({
  providedIn: 'root'
})
export class NewUserGuard implements CanActivate {
  $newUser = this.store.select(selectEnterpriseId).pipe(
    take(1),
    map((id) => !!id)
  );

  constructor(private store: Store, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    let newUser = true;
    this.$newUser.subscribe((nu) => (newUser = nu)).unsubscribe();
    if (newUser) {
      return this.router.parseUrl('/enterprise');
    }
    return true;
  }
}
