import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { provideToastr } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { authInterceptor } from './app/_interceptor/auth.interceptor';
import { loadingInterceptor } from './app/_interceptor/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([authInterceptor, loadingInterceptor])),
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
