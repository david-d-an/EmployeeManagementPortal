use employees;

set @empno := 500008;

-- select *
delete 
FROM salaries
WHERE
	emp_no = @empno;

-- select *
delete 
FROM salaries_current
WHERE
	emp_no = @empno;

-- select *
delete 
FROM titles
WHERE
	emp_no = @empno;

-- select *
delete 
FROM titles_current
WHERE
	emp_no = @empno;

-- select *
delete 
FROM dept_emp
WHERE
	emp_no = @empno;

-- select *
delete 
FROM dept_emp_current
WHERE
	emp_no = @empno;

-- select *
delete 
FROM dept_manager
WHERE
	emp_no = @empno;

-- select *
delete 
FROM dept_manager_current
WHERE
	emp_no = @empno;

-- select *
delete 
FROM employees
WHERE
	emp_no = @empno;



select *
FROM employees
where emp_no = @empno;