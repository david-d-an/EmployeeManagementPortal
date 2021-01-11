-- employees.vw_titles_current source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `employees`.`vw_titles_current` AS
select
    `t`.`emp_no` AS `emp_no`,
    `t`.`title` AS `title`,
    `t`.`from_date` AS `from_date`,
    `t`.`to_date` AS `to_date`
from
    (`employees`.`titles` `t`
join (
    select
        `t_inside`.`emp_no` AS `emp_no`,
        max(`t_inside`.`from_date`) AS `MaxFromDate`,
        max(`t_inside`.`to_date`) AS `MaxToDate`
    from
        `employees`.`titles` `t_inside`
    group by
        `t_inside`.`emp_no`) `em_agg` on
    (((`t`.`emp_no` = `em_agg`.`emp_no`)
    and (`t`.`from_date` = `em_agg`.`MaxFromDate`)
    and (`t`.`to_date` = `em_agg`.`MaxToDate`))));