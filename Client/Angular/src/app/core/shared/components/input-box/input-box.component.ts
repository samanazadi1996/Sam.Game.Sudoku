import { Component, Input } from '@angular/core';
import { ControlValidationComponent } from '../control-validation/control-validation.component';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-input-box',
  templateUrl: './input-box.component.html',
  styleUrl: './input-box.component.scss',
    standalone: true,
  imports: [ReactiveFormsModule,ControlValidationComponent]

})
export class InputBoxComponent {

  @Input() fc: any;
  @Input() label: string = '';
  @Input() type: string = 'text';

}
