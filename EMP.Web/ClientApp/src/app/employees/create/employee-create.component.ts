import { DepartmentService } from './../../departments/service/department.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GenderService } from 'src/app/genders/service/gender.service';
import { DepartmentDetail } from 'src/app/models/DepartmentDetail';
import { EmployeeEditDetail } from 'src/app/models/EmployeeDetail';
import { SpinnerService } from 'src/app/shared/spinner.service';
import { TitleService } from 'src/app/titles/service/title.service';
import { positiveNumber } from 'src/app/Validators/Validators';
import { EmployeeService } from '../service/employee.service';
import { MessageModalComponent } from 'src/app/message-modal/message-modal.component';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {
  @ViewChild(MessageModalComponent) messageModalComponent: MessageModalComponent;

  pageTitle = 'Employee Creation';
  genders: any;
  titles: string[];
  empNo: any;
  employeeData: any;
  empEditDetail: EmployeeEditDetail;
  departments: DepartmentDetail[];
  updatedEmployeeData: any;

  createFormGroup = this.formBuilder.group({
    empNo: [-1, [
    ]],
    firstName: [ '', [
      Validators.required
    ]],
    lastName: [ '', [
      Validators.required
    ]],
    hireDate: ['', [
      Validators.required,
    ]],
    birthDate: ['', [
      Validators.required
    ]],
    gender: ['', [
      Validators.required
    ]],
    title: [ '', [
      Validators.required
    ]],
    deptNo: [ '', [
      Validators.required
    ]],
    salary: [ '', [
      Validators.maxLength(10),
      positiveNumber
    ]]
  }, {
    validators: [
      // salaryMinLessThanSalaryMax
  ]});


  constructor(
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private titleService: TitleService,
    private genderService: GenderService,
    private departmentService: DepartmentService,
    private spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.departmentService.getDepartmentDetails().subscribe(
      data => {
        this.departments = data;
        // console.log(this.departments);
      },
      error => console.log(error)
    );

    this.genderService.getGenders().subscribe(
      data => {
        this.genders = data;
        console.log(`Genders: ${JSON.stringify(this.genders)}`);
      },
      error => console.log(error)
    );

    this.titleService.getTitles().subscribe(
      data => {
        this.titles = data;
        console.log(`Titles: ${JSON.stringify(this.titles)}`);
      },
      error => console.log(error)
    );
  }

  goBack(): void {
    this.router.navigate(['/employees']);
  }

  isControlInvalid(controlName): boolean {
    const c = this.createFormGroup.get(controlName);
    return (c.touched || c.dirty) && !c.valid;
  }

  controlErrors(controlName): ValidationErrors {
    const c = this.createFormGroup.get(controlName);
    return c.errors;
  }

  save(): void {
    this.spinnerService.startLoading();
    this.updatedEmployeeData = this.createFormGroup.getRawValue();
    // console.log(JSON.stringify(this.updatedEmployeeData));
    this.employeeService
      .postOneEmployeeDetails(this.empNo, this.updatedEmployeeData)
      .subscribe({
        next: res => {
          this.spinnerService.stopLoading();
          console.log(JSON.stringify(res));
          this.messageModalComponent.openModal(
            'Complete',
            'User created successfully.',
            () => this.router.navigate(['/employees'])
          );
        },
        error: err => {
          this.spinnerService.stopLoading();
          console.log(err);
          this.messageModalComponent.openModal('Error', 'Error occurred while creating user.');
        },
      });
  }

  getFulllGenderName(g: string): string {
      return this.genderService.getFulllGenderName(g);
  }

}
