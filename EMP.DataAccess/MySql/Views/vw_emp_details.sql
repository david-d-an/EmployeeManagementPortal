USE employees;

CREATE OR REPLACE VIEW vw_emp_details
AS
SELECT 
    e.emp_no,
    e.first_name,
    e.last_name,
    e.birth_date,
    e.hire_date,
    e.gender,
    vsc.salary,
    vtc.title,
    d.dept_no, 
    d.dept_name,
    em.first_name 'manager_first_name',
    em.last_name 'manager_last_name',
    em.emp_no 'manager_emp_no'
FROM 
    employees e 
    LEFT JOIN dept_emp_current vdec 
    ON 
        e.emp_no = vdec.emp_no
        AND vdec.to_date > CURDATE()
    LEFT JOIN departments d 
    ON vdec.dept_no = d.dept_no 
    LEFT JOIN dept_manager_current vdmc 
    ON 
        vdec.dept_no = vdmc.dept_no
        AND vdmc.to_date > CURDATE()
    LEFT JOIN employees em
    ON vdmc.emp_no = em.emp_no 
    LEFT JOIN salaries_current vsc
    ON 
        e.emp_no = vsc.emp_no
        AND vsc.to_date > CURDATE()
    LEFT JOIN titles_current vtc
    ON 
        e.emp_no = vtc.emp_no
        AND vtc.to_date > CURDATE()
;
