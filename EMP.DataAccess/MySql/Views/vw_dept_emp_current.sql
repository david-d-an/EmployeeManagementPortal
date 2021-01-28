USE employees;

CREATE OR REPLACE VIEW vw_dept_emp_current 
AS
SELECT
    de.emp_no AS emp_no,
    de.dept_no AS dept_no,
    de.from_date AS from_date,
    de.to_date AS to_date
FROM
    dept_emp de
    INNER JOIN (
        SELECT
            de_inside.emp_no AS emp_no,
            max(de_inside.to_date) AS MaxToDate
        FROM
            employees.dept_emp de_inside
        GROUP BY
            de_inside.emp_no
    ) de_agg 
    ON
        de.emp_no = de_agg.emp_no
        AND de.to_date = de_agg.MaxToDate
;