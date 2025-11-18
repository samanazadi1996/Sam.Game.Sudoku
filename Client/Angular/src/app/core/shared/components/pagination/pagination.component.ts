import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss',
  standalone:true,
  imports: [CommonModule, FormsModule],
})
export class PaginationComponent implements OnInit, OnChanges {
  ngOnInit(): void {
    this.generate();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['totalPages'] && !changes['totalPages'].firstChange) {
      this.generate();
    }
  }
  @Input() pageNumber?: number;
  @Input() pageSize?: number;
  @Input() totalPages?: number;
  @Input() pageSizes?: number[] = [10, 20, 50, 100, 200, 500];

  @Output() pageChanged = new EventEmitter<any>();
  pages: number[] = [];
  goToPage(page: number): void {
    if (this.totalPages && this.pageNumber) {
      this.pageNumber = page;
      if (this.pageSize)
        this.pageChanged.emit({ page: page, pageSize: Number(this.pageSize) });
      else this.pageChanged.emit({ page: page });
      this.generate();
    }
  }
  generate() {
    if (this.totalPages && this.pageNumber) {
      var startpage = this.pageNumber - 4;

      if (startpage <= 0) startpage = 1;

      var endpage = startpage + 9;
      if (endpage > this.totalPages) {
        startpage -= endpage - this.totalPages - 1;
        if (startpage <= 0) startpage = 1;
        endpage = this.totalPages + 1;
      }

      this.pages = Array.from(
        { length: endpage - startpage },
        (_, index) => startpage + index
      );
    }
  }
}
