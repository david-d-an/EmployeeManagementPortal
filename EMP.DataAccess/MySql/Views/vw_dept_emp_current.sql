-- employees.vw_dept_emp_current source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `employees`.`vw_dept_emp_current` AS
select
    `de`.`emp_no` AS `emp_no`,
    `de`.`dept_no` AS `dept_no`,
    `de`.`from_date` AS `from_date`,
    `de`.`to_date` AS `to_date`
from
    (`employees`.`dept_emp` `de`
join (
    select
        `de_inside`.`emp_no` AS `emp_no`,
        max(`de_inside`.`to_date`) AS `MaxToDate`
    from
        `employees`.`dept_emp` `de_inside`
    group by
        `de_inside`.`emp_no`) `de_agg` on
    (((`de`.`emp_no` = `de_agg`.`emp_no`)
    and (`de`.`to_date` = `de_agg`.`MaxToDate`))));