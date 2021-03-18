import { getTestBed } from '@angular/core/testing';
import { EmployeeEditDetail } from './../../../models/EmployeeDetail';
import { FormBuilder, Validators, ValidationErrors } from '@angular/forms';
import { ModalManager } from 'ngb-modal';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';

import { DepartmentDetail } from 'src/app/models/DepartmentDetail';

import { SpinnerService } from './../../../shared/spinner.service';
import { EmployeeService } from './../../../employees/service/employee.service';
import { TitleService } from './../../../titles/service/title.service';
import { DepartmentService } from './../../../departments/service/department.service';
import { GenderService } from './../../../genders/service/gender.service';

import { positiveNumber } from 'src/app/Validators/Validators';

@Component({
  selector: 'app-employee-edit-modal',
  templateUrl: './employee-edit-modal.component.html',
  styleUrls: ['./employee-edit-modal.component.css']
})
export class EmployeeEditModalComponent implements OnInit {
  @ViewChild('editModal') editModal;
  @Output() saveEditEvent = new EventEmitter();

  modalRef: any;
  empNo: any;
  employeeData: any;
  empEditDetail: EmployeeEditDetail;
  departments: DepartmentDetail[];
  updatedEmployeeData: any;

  editFormGroup = this.formBuilder.group({
    empNo: ['', [
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
  genders: any;
  titles: string[];

  constructor(
    private formBuilder: FormBuilder,
    private modalService: ModalManager,
    private spinnerService: SpinnerService,
    private employeeService: EmployeeService,
    private titleService: TitleService,
    private genderService: GenderService,
    private departmentService: DepartmentService) { }

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

  isControlInvalid(controlName): boolean {
    const c = this.editFormGroup.get(controlName);
    return (c.touched || c.dirty) && !c.valid;
  }

  controlErrors(controlName): ValidationErrors {
    const c = this.editFormGroup.get(controlName);
    return c.errors;
  }

  openModal(empNo): void {
    if (empNo) {
      this.empNo = empNo;
      this.loadData();
      this.modalRef = this.modalService.open(this.editModal, {
          size: 'md',
          modalClass: '',
          hideCloseButton: false,
          centered: false,
          backdrop: 'static',
          keyboard: false,
          animation: true,
          closeOnOutsideClick: true,
          backdropClass: 'modal-backdrop'
      });
    }
  }

  loadData(): void {
    this.spinnerService.startLoading();
    this.employeeService.getOneEmployeeDetails(this.empNo)
      // .pipe(
      //   tap(data => console.log(data.length))
      // )
      .subscribe({
        next: data => {
          // this.employeeData = data;
          // console.log(data['deptName']);
          // console.log(data['deptNo']);
          this.empEditDetail = {
            empNo: data['empNo'],
            firstName: data['firstName'],
            lastName: data['lastName'],
            birthDate: new Date(data['birthDate']).toISOString().slice(0, 10),
            hireDate: new Date(data['hireDate']).toISOString().slice(0, 10),
            gender: data['gender'],
            salary: data['salary'],
            title: data['title'],
            deptNo: data['deptNo']
          };
          // console.log(this.empEditDetail);
          this.editFormGroup.patchValue(this.empEditDetail);
          this.spinnerService.stopLoading();
        },
        error: error => {
          // console.log(error);
          this.spinnerService.stopLoading();
        }
      });
  }

  closeModal() {
    this.modalService.close(this.modalRef);
  }

  saveEdit(): void {
    this.spinnerService.startLoading();
    this.modalService.close(this.modalRef);
    this.updatedEmployeeData = this.editFormGroup.getRawValue();
    console.log(JSON.stringify(this.updatedEmployeeData));
    this.employeeService
      .putOneEmployeeDetails(this.empNo, this.updatedEmployeeData)
      .subscribe({
        next: res => {
          this.spinnerService.stopLoading();
          console.log(JSON.stringify(res));
          this.saveEditEvent.next();
          // TO DO: Show message box for success
        },
        error: err => {
          alert('Error');
          this.spinnerService.stopLoading();
          console.log(err);
          // TO DO: Show message box for failure
        },
      });
  }

  getFulllGenderName(g: string): string {
    return this.genderService.getFulllGenderName(g);
  }

}
