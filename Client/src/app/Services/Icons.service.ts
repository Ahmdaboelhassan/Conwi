import { Injectable } from '@angular/core';
import {
  faHouse,
  faMagnifyingGlass,
  faUser,
  faEnvelope,
  faBell,
  faRightFromBracket,
  faGear,
} from '@fortawesome/free-solid-svg-icons';

@Injectable({ providedIn: 'root' })
export class IconService {
  icons = {
    Home: faHouse,
    Profile: faUser,
    Search: faMagnifyingGlass,
    Message: faEnvelope,
    Notification: faBell,
    Logout: faRightFromBracket,
    Settings: faGear,
  };
}
