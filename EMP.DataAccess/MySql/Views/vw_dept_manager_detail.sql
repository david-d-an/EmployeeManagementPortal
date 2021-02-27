CREATE VIEW vw_dept_manager_detail
AS
SELECT 
    d.dept_no,
    d.dept_name,
    dmc.from_date,
    dmc.to_date,
    e.emp_No,
    e.first_name,
    e.last_name
FROM
    departments d
    INNER JOIN dept_manager_current dmc
    ON 
        d.dept_no = dmc.dept_no
        AND dmc.to_date > CURDATE()
    INNER JOIN employees e
    ON dmc.emp_No = e.emp_No;
