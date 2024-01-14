import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';

export const onlyNotLoggedUsersGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  if (inject(AuthorizationService).isLoggedIn() === false) {
    return true;
  }

  inject(Router).navigate(['/how-to']);
  return false;
};

export const onlyLoggedUsersGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  return inject(AuthorizationService).isLoggedIn() === true;
};
