-- employees.vw_emp_details source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `employees`.`vw_emp_details` AS
select
    `e`.`emp_no` AS `emp_no`,
    `e`.`first_name` AS `first_name`,
    `e`.`last_name` AS `last_name`,
    `e`.`birth_date` AS `birth_date`,
    `e`.`hire_date` AS `hire_date`,
    `e`.`gender` AS `gender`,
    `vsc`.`salary` AS `salary`,
    `vtc`.`title` AS `title`,
    `d`.`dept_no` AS `dept_no`,
    `d`.`dept_name` AS `dept_name`,
    `em`.`first_name` AS `manager_first_name`,
    `em`.`last_name` AS `manager_last_name`,
    `em`.`emp_no` AS `manager_emp_no`
from
    ((((((`employees`.`employees` `e`
join `employees`.`dept_emp_current` `vdec` on
    ((`e`.`emp_no` = `vdec`.`emp_no`)))
join `employees`.`departments` `d` on
    ((`vdec`.`dept_no` = `d`.`dept_no`)))
join `employees`.`dept_manager_current` `vdmc` on
    ((`vdec`.`dept_no` = `vdmc`.`dept_no`)))
join `employees`.`employees` `em` on
    ((`vdmc`.`emp_no` = `em`.`emp_no`)))
join `employees`.`salaries_current` `vsc` on
    ((`e`.`emp_no` = `vsc`.`emp_no`)))
join `employees`.`titles_current` `vtc` on
    ((`e`.`emp_no` = `vtc`.`emp_no`)));