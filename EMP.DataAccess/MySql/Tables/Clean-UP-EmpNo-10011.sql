USE employees;

-- BEGIN;
-- SELECT emp_no, salary, from_date, to_date
-- Delete
-- FROM employees.salaries 
-- where 
-- 	emp_no = 10011
-- 	and salary = 56753
-- 	and from_date = '2021-02-08'
-- 	and to_date = '9999-01-01'
-- ;

-- SELECT emp_no, salary, from_date, to_date
-- Delete
-- FROM employees.salaries_current 
-- where 
-- 	emp_no = 10011
-- 	and salary = 56753
-- 	and from_date = '2021-02-08'
-- 	and to_date = '9999-01-01'
-- ;
-- COMMIT;

-- BEGIN;
-- -- SELECT emp_no, title, from_date, to_date
-- Delete
-- FROM employees.titles
-- WHERE 
-- 	emp_no = 10011
-- 	and title = "Staff"
-- 	and from_date = '2021-02-08'
-- 	and to_date = '9999-01-01'
-- ;

-- -- SELECT emp_no, title, from_date, to_date
-- Delete
-- FROM employees.titles_current 
-- WHERE 
-- 	emp_no = 10011
-- 	and title = "Staff"
-- 	and from_date = '2021-02-08'
-- 	and to_date = '9999-01-01'
-- ;
-- COMMIT;


START TRANSACTION;

CREATE TABLE employees.NewTable (
	Column1 varchar(100) NULL
)
ENGINE=InnoDB
DEFAULT CHARSET=latin1
COLLATE=latin1_swedish_ci;

-- COMMIT;
