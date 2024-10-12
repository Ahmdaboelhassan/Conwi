import { HttpInterceptorFn } from '@angular/common/http';
import { LoaderService } from '../Services/loader.service';
import { inject } from '@angular/core'; // Use Injector
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loader = inject(LoaderService);
  loader.StartLoading();
  return next(req).pipe(
    delay(1000),
    finalize(() => loader.StopLoading())
  );
};
