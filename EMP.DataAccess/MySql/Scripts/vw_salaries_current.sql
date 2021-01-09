-- employees.vw_salaries_current source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `employees`.`vw_salaries_current` AS
select
    `s`.`emp_no` AS `emp_no`,
    `s`.`salary` AS `salary`,
    `s`.`from_date` AS `from_date`,
    `s`.`to_date` AS `to_date`
from
    (`employees`.`salaries` `s`
join (
    select
        `t_inside`.`emp_no` AS `emp_no`,
        max(`t_inside`.`from_date`) AS `MaxFromDate`,
        max(`t_inside`.`to_date`) AS `MaxToDate`
    from
        `employees`.`salaries` `t_inside`
    group by
        `t_inside`.`emp_no`) `em_agg` on
    (((`s`.`emp_no` = `em_agg`.`emp_no`)
    and (`s`.`from_date` = `em_agg`.`MaxFromDate`)
    and (`s`.`to_date` = `em_agg`.`MaxToDate`))));