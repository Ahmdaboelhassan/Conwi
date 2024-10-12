import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { provideToastr } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { loadingInterceptor } from './app/interceptor/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([loadingInterceptor])),
    provideRouter(routes),
    provideAnimations(),
    provideToastr({
      maxOpened: 3,
      autoDismiss: true,
      closeButton: true,
      timeOut: 3000,
      positionClass: 'toast-top-right',
    }),
  ],
};
