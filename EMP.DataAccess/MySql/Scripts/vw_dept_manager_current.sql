-- employees.vw_dept_manager_current source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `employees`.`vw_dept_manager_current` AS
select
    `dm`.`emp_no` AS `emp_no`,
    `dm`.`dept_no` AS `dept_no`,
    `dm`.`from_date` AS `from_date`,
    `dm`.`to_date` AS `to_date`
from
    (`employees`.`dept_manager` `dm`
join (
    select
        `dm_inside`.`dept_no` AS `dept_no`,
        max(`dm_inside`.`to_date`) AS `MaxToDate`
    from
        `employees`.`dept_manager` `dm_inside`
    group by
        `dm_inside`.`dept_no`) `dm_agg` on
    (((`dm`.`dept_no` = `dm_agg`.`dept_no`)
    and (`dm`.`to_date` = `dm_agg`.`MaxToDate`))));