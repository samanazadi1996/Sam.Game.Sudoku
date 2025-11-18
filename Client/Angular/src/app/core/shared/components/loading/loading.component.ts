import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LoadingService } from '../../../services/loading.service';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrl: './loading.component.scss',
  standalone: true,
  imports: [CommonModule]

})
export class LoadingComponent {
  loading$: Observable<boolean> = this.loadingService.loading$;

  constructor(private loadingService: LoadingService) { }

}
