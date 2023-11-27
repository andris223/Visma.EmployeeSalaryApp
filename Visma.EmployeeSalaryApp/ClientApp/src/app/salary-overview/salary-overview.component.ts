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
  public employeeAmountEarnedInShift: number = 0;
  public employeeAmountEarnedInMonth: number = 0;
  private subscriptions: Subscription = new Subscription();

  constructor(private employeeApiService: EmployeeApiService) { }

  public ngOnInit(): void {
    this.refreshSalaryRate(this.formGroup.value?.employeeId ?? null).subscribe()
    this.refreshAmountEarnedInShift(this.formGroup.value).subscribe();
    this.refreshAmountEarnedInMonth(this.formGroup.value).subscribe();
    this.setupSubscriptions();
  }

  private setupSubscriptions(): void {

    this.subscriptions.add(
      this.formGroup.controls.employeeId.valueChanges
      .pipe(
        switchMap(employeeId => this.refreshSalaryRate(employeeId)),
      )
      .subscribe()
    );

    this.subscriptions.add(
      this.formGroup.valueChanges
        .pipe(
          switchMap(formValue => this.refreshAmountEarnedInShift(formValue))
        )
        .subscribe()
    );

    this.subscriptions.add(
      this.formGroup.valueChanges
        .pipe(
          switchMap(formValue => this.refreshAmountEarnedInMonth(formValue))
        )
        .subscribe()
    );
  }

  public ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
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

  private refreshAmountEarnedInShift(formValue : Partial<IFormData>): Observable<unknown> {
    if(!formValue.employeeId || !formValue.year || !formValue.month) {
      throw new Error('Invalid form value');
    }

    return this.employeeApiService.CalculateShiftAmountEarned(formValue.employeeId, formValue.year, formValue.month)
    .pipe(
      tap((shifts) => {
        this.employeeShifts = shifts;
        console.log(shifts);
      })
    )
  }

  private refreshAmountEarnedInMonth(formValue: Partial<IFormData>): Observable<unknown> {
    if (!formValue.employeeId || !formValue.year || !formValue.month) {
      throw new Error('Invalid form value');
    }

    return this.employeeApiService.CalculateMonthlyAmountEarned(formValue.employeeId, formValue.year, formValue.month)
      .pipe(
        tap((amount) => {
          this.employeeAmountEarnedInMonth = Math.round(amount);
        })
      )
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
