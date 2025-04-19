import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';
import { LoaderService } from '../_services/loader.service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loader = inject(LoaderService);
  if (!loader.isDisabled) {
    loader.StartLoading();
  }
  return next(req).pipe(finalize(() => loader.StopLoading()));
};
