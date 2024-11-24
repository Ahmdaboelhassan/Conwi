import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';
import { LoaderService } from '../_services/loader.service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loader = inject(LoaderService);
  loader.StartLoading();
  return next(req).pipe(
    delay(1000),
    finalize(() => loader.StopLoading())
  );
};
