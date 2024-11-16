export interface Answer {
  id: number; // Mã định danh câu trả lời
  text: string; // Nội dung câu trả lời
  isCorrect: boolean; // Đánh dấu câu trả lời đúng hay sai
}

export interface Question {
  id: number; // Mã định danh câu hỏi
  text: string; // Nội dung câu hỏi
  answers: Answer[]; // Danh sách các đáp án
}

export interface Quiz {
  id: number; // Mã định danh bài trắc nghiệm
  title: string; // Tiêu đề bài trắc nghiệm
  description?: string; // Mô tả (tùy chọn)
  questions: Question[]; // Danh sách câu hỏi
}

export function createAnswer(answer: Partial<Answer>): Answer {
  return {
    id: answer.id ?? 0, // Giá trị mặc định là 0 nếu không truyền vào
    text: answer.text ?? "", // Giá trị mặc định là chuỗi rỗng
    isCorrect: answer.isCorrect ?? false, // Giá trị mặc định là false
  };
}

export function createQuestion(question: Partial<Question>): Question {
  return {
    id: question.id ?? 0, // Giá trị mặc định là 0
    text: question.text ?? "", // Giá trị mặc định là chuỗi rỗng
    answers: question.answers?.map(createAnswer) ?? [], // Mặc định là mảng rỗng nếu không truyền vào
  };
}

export function createQuiz(quiz: Partial<Quiz>): Quiz {
  return {
    id: quiz.id ?? 0, // Giá trị mặc định là 0
    title: quiz.title ?? "Untitled Quiz", // Tiêu đề mặc định
    description: quiz.description ?? "", // Mặc định chuỗi rỗng
    questions: quiz.questions?.map(createQuestion) ?? [], // Mặc định là mảng rỗng nếu không truyền vào
  };
}

