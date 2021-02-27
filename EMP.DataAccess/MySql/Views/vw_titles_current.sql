USE employees;

CREATE OR REPLACE VIEW employees.vw_titles_current 
AS
SELECT
    t.emp_no AS emp_no,
    t.title AS title,
    t.from_date AS from_date,
    t.to_date AS to_date
FROM
    employees.titles t
    INNER JOIN (
        SELECT
            t_inside.emp_no AS emp_no,
            max(t_inside.from_date) AS MaxFromDate,
            max(t_inside.to_date) AS MaxToDate
        FROM
            employees.titles t_inside
        WHERE
            t_inside.to_date > CURDATE()
        GROUP BY
            t_inside.emp_no
    ) em_agg 
    ON
        t.emp_no = em_agg.emp_no
        and t.from_date = em_agg.MaxFromDate
        and t.to_date = em_agg.MaxToDate
;