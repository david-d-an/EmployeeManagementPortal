use employees;

CREATE OR REPLACE VIEW vw_emp_details_short
AS
SELECT 
    e.emp_no AS emp_no, 
    e.first_name AS first_name, 
    e.last_name AS last_name, 
    vtc.title AS title, 
    vsc.salary AS salary,
    d.dept_no AS dept_no, 
    d.dept_name AS dept_name
    -- em.emp_no AS manager_emp_no
FROM
    employees e
    INNER JOIN dept_emp_current vdec 
    ON e.emp_no = vdec.emp_no 
    INNER JOIN departments d 
    ON vdec.dept_no = d.dept_no
    INNER JOIN dept_manager_current vdmc 
    ON vdec.dept_no = vdmc.dept_no
    -- INNER JOIN employees em 
    -- ON vdmc.emp_no = em.emp_no
    LEFT JOIN salaries_current vsc 
    ON e.emp_no = vsc.emp_no 
    LEFT JOIN titles_current vtc 
    ON e.emp_no = vtc.emp_no;
