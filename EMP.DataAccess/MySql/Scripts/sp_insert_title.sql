
use employees ;


DROP PROCEDURE IF EXISTS employees.sp_insert_title;

DELIMITER $$

CREATE PROCEDURE employees.sp_insert_title(
	in empNo int, 
	in title varchar(50)
)
BEGIN
-- 	10001	d005
	
	SELECT
		@title := title, 
		@from_Date := from_date, 
		@to_Date := to_date 
	FROM vw_titles_current vtc
	WHERE
		vtc.emp_no = empNo;


	IF title <> @title 
	THEN
		UPDATE titels t
		SET
			t.to_Date = CURDATE() 
		WHERE
			t.emp_no = empNo
			AND t.title  = @title
			AND t.from_date = @from_Date
			AND t.to_date = @to_Date;

		INSERT INTO titles (
			emp_no, 
			title, 
			from_date, 
			to_date
		)
		VALUES(
			empNo, 
			title, 
			CURDATE(), 
			'9999-01-01'
		);
	
		UPDATE titles_current t
		SET
			t.title = title,
			t.from_date = CURDATE(),
			t.to_date = '9999-01-01'
		WHERE
			t.emp_no = empNo;

	END IF;
	
END $$

DELIMITER ;


