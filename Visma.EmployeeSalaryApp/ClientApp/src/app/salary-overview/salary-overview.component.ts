import { Component, OnInit } from '@angular/core';
import { EmployeeApiService } from '../api/employee-api.service';
import { FormControl, FormGroup } from '@angular/forms';
import { EmployeeShift } from '../models/employee-shift';
import { Observable, Subscription, switchMap, tap } from 'rxjs';

@Component({
  templateUrl: './salary-overview.component.html',
  styleUrls: ['./salary-overview.component.css']
})
export class SalaryOverviewComponent implements OnInit {

  public readonly dateFormat = 'MMM dd. HH:mm';

  public formGroup = new FormGroup<IFormGroup>({
    employeeId: new FormControl<number>(1, { nonNullable: true }),
    year: new FormControl<number>(2023, { nonNullable: true }),
    month: new FormControl<number>(9, { nonNullable: true })
  });

  public employeeShifts: EmployeeShift[] = [];
  public employeeSalaryRate: number = 0;
  private subscriptions: Subscription = new Subscription();

  constructor(private employeeApiService: EmployeeApiService) { }

  public ngOnInit(): void {
    this.refreshEmployeeShifts(this.formGroup.value).subscribe();
    this.refreshSalaryRate(this.formGroup.value?.employeeId ?? null).subscribe()
    this.setupSubscriptions();
  }

  private setupSubscriptions(): void {
    this.subscriptions.add(
      this.formGroup.valueChanges
      .pipe(
        switchMap(formValue => this.refreshEmployeeShifts(formValue))
      )
      .subscribe()
    );

    this.subscriptions.add(
      this.formGroup.controls.employeeId.valueChanges
      .pipe(
        switchMap(employeeId => this.refreshSalaryRate(employeeId))
      )
      .subscribe()
    );
  }

  public ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  private refreshEmployeeShifts(formValue : Partial<IFormData>): Observable<unknown> {
    if(!formValue.employeeId || !formValue.year || !formValue.month) {
      throw new Error('Invalid form value');
    }
  
    return this.employeeApiService.GetEmployeeShifts(formValue.employeeId, formValue.year, formValue.month)
    .pipe(
      tap((shifts) => {
        this.employeeShifts = shifts;
      })
    )
  }

  private refreshSalaryRate(employeeId: number | null): Observable<unknown> {
    if(!employeeId) {
      throw new Error('Invalid employeeId value');
    }

    return this.employeeApiService.GetEmployeeSalaryRate(employeeId)
    .pipe(
      tap((salaryRate) => {
        this.employeeSalaryRate = salaryRate;
      })
    );
  }
}

interface IFormData {
  employeeId: number;
  year: number;
  month: number;
}

type IFormGroup = {
  [k in keyof IFormData]: FormControl<IFormData[k]>
};
