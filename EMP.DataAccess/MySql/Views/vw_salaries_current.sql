USE employees;

CREATE OR REPLACE VIEW employees.vw_salaries_current 
AS
SELECT
    s.emp_no AS emp_no,
    s.salary AS salary,
    s.from_date AS from_date,
    s.to_date AS to_date
FROM
    employees.salaries s
    INNER JOIN (
        SELECT
            t_inside.emp_no AS emp_no,
            max(t_inside.from_date) AS MaxFromDate,
            max(t_inside.to_date) AS MaxToDate
        FROM
            employees.salaries t_inside
        GROUP BY
            t_inside.emp_no
    ) em_agg 
    ON
        s.emp_no = em_agg.emp_no
        and s.from_date = em_agg.MaxFromDate
        and s.to_date = em_agg.MaxToDate
;