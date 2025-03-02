import { QuizStatusIconRendererComponent } from "../../../@theme/components/quiz-status-icon-renderer-component/quiz-status-icon-renderer-component.component";
import { createAnswer, createQuestion, createQuiz } from "../../customize-quiz/customize-model/customize-quiz.model";
import { ColDef } from 'ag-grid-community';

export function createSimplequiz() {
  let q1 = createQuiz({
    title: "Bài kiểm tra lập trình",
    questions: [
      createQuestion({
        content: "Angular là gì?",
        answers: [
          createAnswer({ content: "Framework", isCorrect: true }),
          createAnswer({ content: "Library", isCorrect: false }),
          createAnswer({ content: "Tool", isCorrect: false }),
        ],
      }),
      createQuestion({
        id: 2,
        content: "Angular sử dụng ngôn ngữ nào?",
        answers: [
          createAnswer({ content: "JavaScript", isCorrect: false }),
          createAnswer({ content: "TypeScript", isCorrect: true }),
          createAnswer({ content: "Python", isCorrect: false }),
        ],
      }),
    ],
  });
  
  let q2 = createQuiz({
    title: "Bài kiểm tra kiến thức web",
    questions: [
      createQuestion({
        content: "Thẻ nào được sử dụng để tạo tiêu đề lớn nhất trong HTML?",
        answers: [
          createAnswer({ content: "<h1>", isCorrect: true }),
          createAnswer({ content: "<h2>", isCorrect: true }),
          createAnswer({ content: "<h3>", isCorrect: true }),
        ],
      }),
      createQuestion({
        content: "CSS viết tắt của từ gì?",
        answers: [
          createAnswer({ content: "Cascading Style Sheets", isCorrect: true }),
          createAnswer({ content: "Cascading Style Syntax", isCorrect: false }),
          createAnswer({ content: "Cascading Style Script", isCorrect: false }),
          createAnswer({ content: "Cascading Style Text", isCorrect: false }),
        ],
      }),
    ],
  });
  
  let q3 = createQuiz({
    title: "Bài kiểm tra kiến thức JavaScript",
    questions: [
      createQuestion({
        content: "Từ khóa nào được sử dụng để khai báo biến trong JavaScript?",
        answers: [
          createAnswer({ content: "var", isCorrect: true }),
          createAnswer({ content: "let", isCorrect: true }),
          createAnswer({ content: "both", isCorrect: true }),
        ],
      }),
      createQuestion({
        content: "Hàm nào dùng để in dữ liệu ra console?",
        answers: [
          createAnswer({ content: "console.log()", isCorrect: true }),
          createAnswer({ content: "console.write()", isCorrect: false }),
          createAnswer({ content: "console.writeLine()", isCorrect: false }),
        ],
      }),
    ],
  });
  
  return [q1, q2, q3];
  
}

export const defaultColDef: ColDef = {
  filter: true,
  sortable: true, resizable: true,
  
};

export const columnDefs: ColDef[] = [
  { headerName: 'Id', field: 'uniqueNumber', editable: false},
  { headerName: 'Status', field: 'status', editable: false,
    cellRenderer: QuizStatusIconRendererComponent
    // cellRenderer: (params: any) => {
    //   // if (params.value === QuizStatus.Published) {
    //   //   return `<nb-icon icon="alert-triangle" pack="eva" status="primary"></nb-icon>`;
    //   // }

    //   // return `<nb-icon icon="award" pack="eva" status="danger"></nb-icon>`

    //   const icon = params.value === 'Published'
    //     ? '<nb-icon icon="alert-triangle" pack="eva" status="primary"></nb-icon>'
    //     : '<nb-icon icon="award" pack="eva" status="danger"></nb-icon>';

    //   const div = document.createElement('div');
    //   div.innerHTML = icon;
    //   return div;
    // }
  },
  { headerName: 'Title', field: 'title', editable: true, },
  // { headerName: 'Description', field: 'description', editable: true, },
  { headerName: 'Time Limit (m)', field: 'durationInMinutes', editable: true },
  {
    headerName: 'Number of Questions',
    valueGetter: (params: any) => {      
      return params.data.questions.length
    },
    sortable: true,
    filter: false,
    resizable: true,
  }
];

export function actionCellRenderer(params: any): string {
  return `<button class="btn btn-primary" (click)="onEdit(${params.data.id})">Edit</button>
          <button class="btn btn-danger" (click)="onDelete(${params.data.id})">Delete</button>`;
}

export const MenuItems = [
  { title: 'Delete', icon: 'trash-2-outline', data: { action: 'delete' } },
  { title: 'Review', icon: 'eye-outline', data: { action: 'review' } },
];
