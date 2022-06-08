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

const BASE_URL = 'https://localhost:44374/';
const AUTH_URL = 'https://localhost:44373/';

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
    let url = request.url;
    url = BASE_URL + url;
    if (token) {
      authHeaders = authHeaders.append('Authorization', 'Bearer ' + token);
    }

    const apiRequest = request.clone({ url: url, headers: authHeaders });
    if (enterpriseId) {
      apiRequest.params.append('auth_enterprise_id', enterpriseId);
    }
    return next.handle(apiRequest);
  }
}
