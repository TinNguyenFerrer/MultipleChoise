import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Result } from '../pages/quiz/model/exame.model';
import { Observable } from 'rxjs';
import { NbToastrService } from '@nebular/theme';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  private baseUrl: string = environment.apiUrl;
  constructor(
    private http: HttpClient,
    private toastrService: NbToastrService
  ) { }
  getResutlByQuiz(result: Result): Observable<Result> {
    return this.http.post<Result>(`${this.baseUrl}/Result`, result).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to get result!`, 'Notification');
        throw error;
      }
      ));
  }

  getAll(): Observable<Result[]> {
    return this.http.get<Result[]>(`${this.baseUrl}/Result`).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to get all result!`, 'Notification');
        throw error;
      }
      ))
  }

  getByQuizId(id: string): Observable<Result[]> {
    return this.http.get<Result[]>(`${this.baseUrl}/Result/quiz/${id}`).pipe(
      catchError((error: any) => {
        this.toastrService.danger(`Failed to get quiz!`, 'Notification');
        throw error;
      }
      ))
  }
}
