USE employees;

CREATE OR REPLACE VIEW vw_dept_manager_current 
AS
SELECT
    dm.emp_no AS emp_no,
    dm.dept_no AS dept_no,
    dm.from_date AS from_date,
    dm.to_date AS to_date
FROM
    employees.dept_manager dm
    INNER JOIN (
        SELECT
            dm_inside.dept_no AS dept_no,
            max(dm_inside.to_date) AS MaxToDate
        FROM
            employees.dept_manager dm_inside
        GROUP BY
            dm_inside.dept_no
    ) dm_agg 
    ON
        dm.dept_no = dm_agg.dept_no
        AND dm.to_date = dm_agg.MaxToDate
;