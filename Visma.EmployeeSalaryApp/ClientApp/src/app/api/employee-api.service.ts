import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeShift } from '../models/employee-shift';

@Injectable({
  providedIn: 'root'
})
export class EmployeeApiService {

  constructor(private httpClient: HttpClient) { 

  }

  public GetEmployeeShifts(employeeId: number, year: number, month: number): Observable<EmployeeShift[]> {
    return this.httpClient.get<EmployeeShift[]>(`/api/employees/${employeeId}/shifts/${year}-${month}`)
  }
  
  public GetEmployeeSalaryRate(employeeId: number): Observable<number> {
    return this.httpClient.get<number>(`/api/employees/${employeeId}/salary-rate`);
  }
}
