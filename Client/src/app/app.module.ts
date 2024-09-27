import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { AuthComponent } from './Register/Register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RoutingModule } from './Modules/routing.module';
import { ProfileComponent } from './home/profile/profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './Modules/shared.module';
import { TimelineComponent } from './home/timeline/timeline.component';
import { NavigatorComponent } from './home/navigator/navigator.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    AuthComponent,
    ProfileComponent,
    TimelineComponent,
    NavigatorComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    RoutingModule,
    SharedModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
