import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { first, Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Store } from '@ngrx/store';
import { selectEnterpriseId } from '../store/selectors/app-user.selector';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: OidcSecurityService, private store: Store) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let token;
    this.authService
      .getAccessToken()
      .pipe(first())
      .subscribe((t) => {
        token = t;
      });
    let enterpriseId;
    this.store
      .select(selectEnterpriseId)
      .pipe(first())
      .subscribe((id) => (enterpriseId = id));
    let authHeaders = request.headers;

    if (token) {
      authHeaders = authHeaders.append('Authorization', 'Bearer ' + token);
    }

    let params = request.params;
    if (enterpriseId) {
      // params = params.append('auth_enterprise_id', enterpriseId);
      authHeaders = authHeaders.append('auth-enterprise', enterpriseId);
    }

    const apiRequest = request.clone({
      url: request.url,
      headers: authHeaders,
      params: params
    });

    return next.handle(apiRequest);
  }
}
