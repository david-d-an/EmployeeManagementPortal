
use employees ;


DROP PROCEDURE IF EXISTS employees.sp_insert_dept_manager;

DELIMITER $$

CREATE PROCEDURE employees.sp_insert_dept_manager(
	in empNo int, 
	in deptNo char(4)
)
BEGIN
-- 	110420	d004
	
	SELECT
		@emp_no := emp_no , 
		@from_Date := from_date, 
		@to_Date := to_date 
	FROM vw_dept_manager_current vdmc
	WHERE
		vdmc.dept_no = deptNo;


	IF empNo != @emp_no 
	THEN
		UPDATE dept_manager dm
		SET
			dm.to_Date = CURDATE() 
		WHERE
			dm.dept_no = deptNo
			AND dm.emp_no  = @emp_No
			AND dm.from_date = @from_Date
			AND dm.to_date = @to_Date;

		INSERT INTO dept_manager (
			emp_no, 
			dept_no, 
			from_date, 
			to_date
		)
		VALUES(
			empNo, 
			deptNo, 
			CURDATE(), 
			'9999-01-01'
		);
	
		UPDATE dept_manager_current dm
		SET
			dm.emp_no = empNo,
			dm.from_date = CURDATE(),
			dm.to_date = '9999-01-01'
		WHERE
			dm.dept_no = deptNo;

		SELECT 
			emp_No,
			dept_no,
			from_Date,
			to_Date
		FROM vw_dept_manager_current vdmc
		WHERE
			vdmc.emp_no = empNo
			AND vdmc.dept_no = deptNo;
	ELSE
		SELECT 
			emp_No,
			dept_no,
			from_Date,
			to_Date
		FROM vw_dept_manager_current vdmc
		WHERE
			FALSE;

	END IF;
	
END $$

DELIMITER ;
