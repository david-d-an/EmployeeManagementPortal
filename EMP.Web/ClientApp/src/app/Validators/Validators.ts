import {AbstractControl, FormControl, ValidationErrors, ValidatorFn} from '@angular/forms';

export function salaryMinLessThanSalaryMax(control: AbstractControl): ValidationErrors | null {
  if (control && control.get('salaryMin') && control.get('salaryMax')) {
    // console.log(`salaryMin: ${control.get('salaryMin').value}`);
    // console.log(`salaryMax: ${control.get('salaryMax').value}`);
    const salaryMin = +control.get('salaryMin').value;
    const salaryMax = +control.get('salaryMax').value;
    if (salaryMin && salaryMax && salaryMin > salaryMax) {
      return { salaryMinGreaterThanSalaryMaxError: true };
    }
  }
  return null;
}

export function positiveNumber(c: FormControl): ValidationErrors | null {
  if (c.value && c.value < 0) {
    return { negativeNumberError: true };
  }
  return null;
}

// export function positiveNumber(control: AbstractControl): ValidationErrors | null {
//   if (control && control.get('salaryMin') && control.get('salaryMax')) {
//     // console.log(`salaryMin: ${control.get('salaryMin').value}`);
//     // console.log(`salaryMax: ${control.get('salaryMax').value}`);
//     const salaryMin = +control.get('salaryMin').value;
//     const salaryMax = +control.get('salaryMax').value;
//     if (salaryMin && salaryMax && salaryMin > salaryMax) {
//       return { salaryMinGreaterThanSalaryMaxError: true };
//     }
//   }
//   return null;
// }

// export function blue(): ValidatorFn {
//     return (control: AbstractControl): { [key: string]: any } | null =>
//         control.value?.toLowerCase() === 'blue'
//             ? null : {wrongColor: control.value};
// }

// export function salaryMinLessThanSalaryMax(control: AbstractControl): ValidationErrors | null {
//   if (control && control.get('minSalary') && control.get('maxSalary')) {
//     const minSalary = control.get('minSalary').value;
//     const maxSalary = control.get('maxSalary').value;

//     console.log(`minSalary: ${+minSalary}`);
//     console.log(`maxSalary: ${+maxSalary}`);
//     console.log(`minSalary > maxSalary: ${+minSalary > +maxSalary}`);

//     if (+minSalary > +maxSalary) {
//       console.log(`return { scoreError: true }`);
//       return { scoreError: true };
//     } else {
//       console.log(`return null`);
//       return null;
//     }
//     // return (minSalary > maxSalary) ? { scoreError: true } : null;
//   }
//   // return { scoreError: true };
//   console.log(`return null2`);
//   return null;
// }

