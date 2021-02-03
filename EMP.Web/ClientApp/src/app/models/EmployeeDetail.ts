export interface EmployeeDetail {
  empNo: number;
  firstName: string;
  lastName: string;
  // birthDate: string;
  // hireDate: string;
  // gender: string;
  // salary: number;
  title: string;
  // deptNo: string;
  // deptName: string;
  // managerEmpNo: number;
  // managerFirstName: string;
  // managerLastName: string;
}

export interface EmployeeFilter {
  empNo: string;
  firstName: string;
  lastName: string;
  salaryMin: string;
  salaryMax: string;
  title: string;
  deptName: string;
}
