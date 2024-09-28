import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    provideRouter(routes),
    provideToastr({
      maxOpened: 3,
      autoDismiss: true,
      closeButton: true,
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
    }),
  ],
};
