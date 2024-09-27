import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FontAwesomeModule,
    ToastrModule.forRoot({
      maxOpened: 3,
      autoDismiss: true,
      closeButton: true,
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
    }),
  ],
  exports: [CommonModule, FontAwesomeModule, ToastrModule],
})
export class SharedModule {}
