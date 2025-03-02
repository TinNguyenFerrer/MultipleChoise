import { Component, HostListener, OnInit } from '@angular/core';
import { Quiz } from '../customize-quiz/customize-model/customize-quiz.model';
import { actionCellRenderer, columnDefs, defaultColDef, MenuItems } from './quiz-model/quiz.model';
import { QuizService } from '../../service/quiz.service';
import { Router } from '@angular/router';
import { GridOptions } from 'ag-grid-community';
import { NbToastrService } from '@nebular/theme';




@Component({
  selector: 'ngx-quiz-management',
  templateUrl: './quiz-management.component.html',
  styleUrls: ['./quiz-management.component.scss'],
})
export class QuizManagementComponent implements OnInit {
  listQuiz: Quiz[] = [];
  gridApi: any;
  columnApi: any;
  themeClass: string = "ag-theme-quartz";
  gridOptions: GridOptions;

  public readonly defaultColDef = defaultColDef;
  public readonly columnDefs = columnDefs;
  public MenuItems = MenuItems;
  public rowSelection: "single" | "multiple" = "multiple";
  public paginationPageSize = 10;
  public paginationPageSizeSelector: number[] | boolean = [10, 25, 50];

  public actionCellRenderer = actionCellRenderer;

  constructor(
    private quizService: QuizService,
    private router: Router,
    private toastrService: NbToastrService,
  ) { }

  createFlagImg(flag: string) {
    return (
      '<img border="0" width="15" height="10" src="https://flags.fmcdn.net/data/flags/mini/' +
      flag +
      '.png"/>'
    );
  }

  

  ngOnInit(): void {
    this.columnApi?.autoSizeColumns(['ID']);

    this.getAllQuiz();

    this.gridOptions = {
      enableCellTextSelection: true,
    } as GridOptions;
  }

  private getAllQuiz() {
    this.quizService.getAll().subscribe((data) => {
      this.listQuiz = data;
    });
  }

  onGridReady(params: any): void {
    this.gridApi = params.api;
    this.columnApi = params.columnApi;
    this.columnApi.autoSizeAllColumns();
    this.gridApi.sizeColumnsToFit();
  }

  editRow(): void {
    if (!this.selectedRow || !this.selectedRow.id) {
      console.warn('No row selected or invalid data.');
      return;
    }
  
    this.router.navigate(['/pages/customize-quiz'], { queryParams: { id: this.selectedRow.id } });
  }

  deleteRow(id: any): void {
    if (!this.selectedRow || !this.selectedRow.id) {
      console.warn('No row selected or invalid data.');
      return;
    }

    this.quizService.delete(this.selectedRow.id).subscribe(() => {
      this.listQuiz = this.listQuiz.filter((q) => q.id !== this.selectedRow.id);
      this.getAllQuiz();
    }
    );
  }

  reportsRow(): void {
    if (!this.selectedRow || !this.selectedRow.id) {
      console.warn('No row selected or invalid data.');
      return;
    }
  
    this.router.navigate(['/pages/quiz-dashboard'], { queryParams: { id: this.selectedRow.id } });
  }

  onAddQuiz(): void {
    this.router.navigate(['/pages/customize-quiz']);
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    this.contextMenuVisible = false;
  }

  contextMenuVisible = false;
  contextMenuX = 0;
  contextMenuY = 0;
  selectedRow: any;
  onCellContextMenu(event: any): void {
    event.event.preventDefault();
    this.contextMenuX = event.event.clientX;
    this.contextMenuY = event.event.clientY;
    this.selectedRow = event.node.data;
    this.contextMenuVisible = true;
  }

  onWrapperContextMenu(event: MouseEvent): void {    
    event.preventDefault();
  }
}

const gridDiv = document.querySelector<HTMLElement>("#myGrid")!;

