import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let authReq = req;
  const authService = inject(AuthService);
  authService.user$.subscribe((user) => {
    if (user.getToken) {
      authReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${user.getToken}`),
      });
    } else {
      authService.logout();
    }
  });
  return next(authReq);
};
