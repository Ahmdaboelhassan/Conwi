import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { AsideComponent } from './home/aside/aside.component';
import { AuthComponent } from './Register/Register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RoutingModule } from './Modules/routing.module';
import { ProfileComponent } from './home/profile/profile.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    AsideComponent,
    AuthComponent,
    ProfileComponent,
  ],
  imports: [
    BrowserModule,
    FontAwesomeModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    RoutingModule,

    ToastrModule.forRoot({
      maxOpened: 5,
      autoDismiss: false,
      closeButton: true,
      timeOut: 10000,
      progressBar: true,
      positionClass: 'toast-bottom-right',
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
