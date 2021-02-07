export interface DepartmentDetail {
  deptNo: string;
  deptName: string;
  deptEmp: any;
  deptManager: any;
}


export interface DepartmentFilter {
  empNo: string;
  firstName: string;
  lastName: string;
  salaryMin: string;
  salaryMax: string;
  title: string;
  deptName: string;
}
