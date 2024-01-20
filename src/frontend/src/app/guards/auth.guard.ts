import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
} from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';

export const onlyNotLoggedUsersGuard: CanActivateFn = (
  _next: ActivatedRouteSnapshot
) => {
  if (inject(AuthorizationService).isLoggedIn() === false) {
    return true;
  }

  inject(Router).navigate(['/errorpage']);
  return false;
};

export const onlyLoggedUsersGuard: CanActivateFn = (
  _next: ActivatedRouteSnapshot
) => {
  if (inject(AuthorizationService).isLoggedIn() === true) {
    return true;
  }

  inject(Router).navigate(['/login']);
  return false;
};
