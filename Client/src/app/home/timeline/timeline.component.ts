import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html',
  styleUrls: ['./timeline.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class TimelineComponent {
  numbers2 = [...Array(50).keys()].map((i) => i + 1);
}
