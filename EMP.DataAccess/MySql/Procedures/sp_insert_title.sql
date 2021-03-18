
use employees ;


DROP PROCEDURE IF EXISTS employees.sp_insert_title;

DELIMITER $$

CREATE PROCEDURE employees.sp_insert_title(
	in empNo int, 
	in title_new varchar(50)
)
BEGIN
	
	SELECT
		@id := id,
		@title := title, 
		@from_Date := from_date, 
		@to_Date := to_date 
	FROM titles t
	WHERE
		t.emp_no = empNo
		AND t.to_date = '9999-01-01'
	ORDER BY 
		id desc 
	LIMIT 1;


	IF @id IS NULL THEN

		DELETE FROM titles_current 
        WHERE emp_no = empNo;

		INSERT INTO titles (
			emp_no, 
			title, 
			from_date, 
			to_date
		)
		VALUES(
			empNo, 
			title_new, 
			CURDATE(), 
			'9999-01-01'
		);

		INSERT INTO titles_current (
			emp_no, 
			title, 
			from_date, 
			to_date
		)
		VALUES(
			empNo,
			title_new,
			CURDATE(),
			'9999-01-01'
		);

        SELECT 
            emp_no, 
			title, 
			from_date, 
			to_date
        FROM vw_titles_current s
        WHERE
			s.emp_no = empNo;

	ELSEIF title_new != @title THEN
		UPDATE titles t
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
			title_new, 
			CURDATE(), 
			'9999-01-01'
		);
	
		UPDATE titles_current t
		SET
			t.title = title_new,
			t.from_date = CURDATE(),
			t.to_date = '9999-01-01'
		WHERE
			t.emp_no = empNo;

        SELECT
            emp_no, 
			title, 
			from_date, 
			to_date
        FROM vw_titles_current t
		WHERE
			t.emp_no = empNo;

    ELSE
        SELECT
            emp_no, 
			title, 
			from_date, 
			to_date
        FROM vw_titles_current t
		WHERE FALSE;

	END IF;
	
END $$

DELIMITER ;
