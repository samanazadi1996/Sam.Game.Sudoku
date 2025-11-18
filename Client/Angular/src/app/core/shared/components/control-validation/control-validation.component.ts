import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-control-validation',
  templateUrl: './control-validation.component.html',
  styleUrl: './control-validation.component.scss',
  standalone: true,
  imports: [CommonModule]

})
export class ControlValidationComponent {
  @Input() fc: any;

}
