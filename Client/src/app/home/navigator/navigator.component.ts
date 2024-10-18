import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { IconService } from 'src/app/Services/Icons.service';

@Component({
  selector: 'app-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.scss'],
  imports: [FontAwesomeModule, RouterLink, RouterLinkActive],
  standalone: true,
})
export class NavigatorComponent {
  lastScrollTop: number = 0;
  @ViewChild('navigator', { static: true }) navigator: ElementRef;

  icons;
  constructor(private iconService: IconService) {
    this.icons = this.iconService.icons;
  }

  @HostListener('window:scroll', ['$event'])
  hideOnscroll() {
    const currentScrollTop = window.scrollY;
    const elementDisplay: string =
      currentScrollTop > this.lastScrollTop ? 'none' : 'flex';

    this.navigator.nativeElement.style.display = elementDisplay;

    this.lastScrollTop = currentScrollTop <= 0 ? 0 : currentScrollTop;
  }
}
