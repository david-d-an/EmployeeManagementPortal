
use employees ;


DROP PROCEDURE IF EXISTS employees.sp_insert_salary;

DELIMITER $$

CREATE PROCEDURE employees.sp_insert_salary(
	in empNo int, 
	in salary int
)
BEGIN
-- 	10001	d005
	
	SELECT
		@salary := salary, 
		@from_Date := from_date, 
		@to_Date := to_date 
	FROM vw_salary_current vsc
	WHERE
		vsc.emp_no = empNo;


	IF @salary IS NULL THEN
		-- DELETE FROM salaries 
        -- WHERE emp_no = empNo;

		DELETE FROM salaries_current 
        WHERE emp_no = empNo;

		INSERT INTO salaries (
			emp_no, 
			salary, 
			from_date, 
			to_date
		)
		VALUES(
			empNo, 
			salary, 
			CURDATE(), 
			'9999-01-01'
		);

		INSERT INTO salaries_current (
			emp_no, 
			salary, 
			from_date, 
			to_date
		)
		VALUES(
			empNo,
			salary,
			CURDATE(),
			'9999-01-01'
		);

        SELECT 
            emp_no, 
			salary, 
			from_date, 
			to_date
        FROM vw_salaries_current s
        WHERE
			s.emp_no = empNo;

	ELSEIF salary != @salary THEN
		UPDATE salaries s
		SET
			s.to_Date = CURDATE() 
		WHERE
			s.emp_no = empNo
			AND s.salary = @salary
			AND s.from_date = @from_Date
			AND s.to_date = @to_Date;

		INSERT INTO salaries (
			emp_no, 
			salary, 
			from_date, 
			to_date
		)
		VALUES(
			empNo, 
			salary, 
			CURDATE(), 
			'9999-01-01'
		);
	
		UPDATE salaries_current s
		SET
			s.salary = salary,
			s.from_date = CURDATE(),
			s.to_date = '9999-01-01'
		WHERE
			s.emp_no = empNo;

        SELECT 
            emp_no, 
			salary, 
			from_date, 
			to_date
        FROM vw_salaries_current s
        WHERE
			s.emp_no = empNo;

    ELSE
        SELECT 
            emp_no, 
			salary, 
			from_date, 
			to_date
        FROM vw_salaries_current s
        WHERE FALSE;

	END IF;
	
END $$

DELIMITER ;
