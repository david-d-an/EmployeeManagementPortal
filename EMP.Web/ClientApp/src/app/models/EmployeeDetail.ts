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
  // empNo: string;
  firstName: string;
  lastName: string;
  salaryMin: string;
  salaryMax: string;
  title: string;
  deptName: string;
}

export interface EmployeeEditDetail {
  empNo: number;
  firstName: string;
  lastName: string;
  birthDate: string;
  hireDate: string;
  gender: string;
  salary: number;
  title: string;
  // deptNo: string;
  deptNo: string;
  // managerEmpNo: number;
  // managerFirstName: string;
  // managerLastName: string;
}


export module EmployeeFilterAnnotation {
  const items = {
    'empNo': 'Employee ID',
    'firstName': 'First Name',
    'lastName': 'Last Name',
    'salaryMin': 'Minimum Salary',
    'salaryMax': 'Maximum Salary',
    'title': 'Title',
    'deptName': 'Department Name'
  };

  export function get(key: string): string {
    return items[key];
  }

  export function getAll(): any {
    return items;
  }
}
