import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Quiz } from '../pages/customize-quiz/customize-model/customize-quiz.model';
import { catchError } from 'rxjs/operators';
import { NbToastrService } from '@nebular/theme';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private baseUrl: string = environment.apiUrl;
  constructor(
    private http: HttpClient,
    private toastrService: NbToastrService) { }
  getById(id: string) {
    return this.http.get<Quiz>(`${this.baseUrl}/Quiz/${id}`).pipe(
      catchError((error: any) => {
        this.toastrService.danger('Failed to get the quiz!', 'Notification');
        throw error;
      }
    )
  )}

  getByNumber(number: string) {
    return this.http.get<Quiz>(`${this.baseUrl}/Quiz/by-number/${number}`).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to get the quiz ${number} !`, 'Notification');
        throw error;
      }
    ))
  }

  getAll() {
    return this.http.get<Quiz[]>(`${this.baseUrl}/Quiz`).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to get all quizzes !`, 'Notification');
        throw error;
      }
    ))
  }

  create(quiz: Quiz) {
    return this.http.post<Quiz>(`${this.baseUrl}/Quiz`, quiz).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to create the quiz!`, 'Notification');
        throw error;
      }
    ))
  }

  update(quiz: Quiz) {
    return this.http.put<Quiz>(`${this.baseUrl}/Quiz/${quiz.id}`, quiz).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to update the quiz!`, 'Notification');
        throw error;
      }
    ))
  }

  delete(id: string) {
    return this.http.delete(`${this.baseUrl}/Quiz/${id}`).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to delete the quiz!`, 'Notification');
        throw error;
      }
    ))
  }
}
