CREATE DATABASE TEAM122
GO

USE TEAM122
GO

CREATE PROCEDURE createAllTables AS

CREATE TABLE sys_user(
username VARCHAR(20),
password VARCHAR(20) NOT NULL,
CONSTRAINT pk_user PRIMARY KEY(username)
);

CREATE TABLE system_admin(
id INT IDENTITY,
name VARCHAR(20) NOT NULL,
username VARCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_admin PRIMARY KEY(id),
CONSTRAINT fk_admin FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE fan(
national_id VARCHAR(20),
name VARCHAR(20) NOT NULL,
birth_date DATETIME NOT NULL,
address VARCHAR(20) NOT NULL,
phone_no INT NOT NULL UNIQUE,
status BIT NOT NULL,
username VARCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_fan PRIMARY KEY(national_ID),
CONSTRAINT fk_fan FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE association_manager(
id INT IDENTITY,
name VARCHAR(20) NOT NULL,
username VARCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_assoc_manager PRIMARY KEY(id),
CONSTRAINT fk_assoc_manager FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE club(
club_id INT IDENTITY,
name VARCHAR(20) NOT NULL UNIQUE,
location VARCHAR(20) NOT NULL,
CONSTRAINT pk_club PRIMARY KEY(club_id)
);

CREATE TABLE stadium(
stadium_id INT IDENTITY,
name VARCHAR(20) NOT NULL UNIQUE,
location VARCHAR(20) NOT NULL,
capacity INT NOT NULL,
status BIT NOT NULL,
CONSTRAINT pk_stadium PRIMARY KEY(stadium_id)
);

CREATE TABLE match(
match_id INT IDENTITY,
start_time DATETIME NOT NULL,
end_time DATETIME NOT NULL,
host_club_ID INT NOT NULL,
guest_club_ID INT NOT NULL,
stadium_ID INT,
CONSTRAINT u_match UNIQUE(start_time,host_club_ID,guest_club_ID),
CONSTRAINT pk_match PRIMARY KEY(match_ID),
CONSTRAINT fk1_match FOREIGN KEY(host_club_ID) REFERENCES club ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_match FOREIGN KEY(guest_club_ID) REFERENCES club ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT fk3_match FOREIGN KEY(stadium_ID) REFERENCES stadium ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE club_representative(
id INT IDENTITY,
name VARCHAR(20) NOT NULL,
club_id INT  UNIQUE NOT NULL,
username VARCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_club_rep PRIMARY KEY(id),
CONSTRAINT fk1_club_rep FOREIGN KEY(club_id) REFERENCES club ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_club_rep FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE stadium_manager(
id INT IDENTITY,
name VARCHAR(20) NOT NULL, 
stadium_id INT UNIQUE NOT NULL,
username VARCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_stadium_manager PRIMARY KEY(id),
CONSTRAINT fk1_stadium_manager FOREIGN KEY(stadium_id) REFERENCES stadium ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_stadium_manager FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE host_request(
id INT IDENTITY,
representative_id INT NOT NULL,
manager_id INT NOT NULL,
match_id INT NOT NULL,
status BIT,
CONSTRAINT pk_req PRIMARY KEY(id),
CONSTRAINT fk1_req FOREIGN KEY(representative_id) REFERENCES club_representative ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_req FOREIGN KEY(manager_id) REFERENCES stadium_manager ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT fk3_req FOREIGN KEY(match_id) REFERENCES match ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE ticket(
id INT IDENTITY,
status BIT NOT NULL,
match_id INT NOT NULL,
CONSTRAINT pk_ticket PRIMARY KEY(id),
CONSTRAINT fk_ticket FOREIGN KEY(match_id) REFERENCES match ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE ticket_buying_transaction(
fan_national_id VARCHAR(20),
ticket_id INT,
CONSTRAINT pk_transaction PRIMARY KEY(fan_national_id,ticket_id),
CONSTRAINT transaction_fk1 FOREIGN KEY(fan_national_id) REFERENCES fan ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT transaction_fk2 FOREIGN KEY(ticket_id) REFERENCES ticket ON DELETE CASCADE ON UPDATE CASCADE
);
GO

EXECUTE createAllTables
GO

CREATE PROCEDURE dropAllTables AS

DROP TABLE host_request

DROP TABLE ticket_buying_transaction

DROP TABLE club_representative

DROP TABLE stadium_manager

DROP TABLE fan

DROP TABLE ticket

DROP TABLE match

DROP TABLE club

DROP TABLE stadium

DROP TABLE system_admin

DROP TABLE association_manager

DROP TABLE sys_user
GO

CREATE PROCEDURE dropAllProceduresFunctionsViews
AS
DROP PROCEDURE createAllTables
DROP PROCEDURE dropAllTables
DROP PROCEDURE clearAllTables
DROP VIEW allAssocManagers
DROP VIEW allClubRepresentatives
DROP VIEW allStadiumManagers
DROP VIEW allFans
DROP VIEW allMatches
DROP VIEW allTickets
DROP VIEW allCLubs
DROP VIEW allStadiums
DROP VIEW allRequests
DROP PROCEDURE addAssociationManager
DROP PROCEDURE addNewMatch
DROP VIEW clubsWithNoMatches
DROP PROCEDURE deleteMatch
DROP PROCEDURE deleteMatchesOnStadium
DROP PROCEDURE addClub
DROP PROCEDURE addTicket
DROP PROCEDURE deleteClub
DROP PROCEDURE addStadium
DROP PROCEDURE deleteStadium
DROP PROCEDURE blockFan
DROP PROCEDURE unblockFan
DROP PROCEDURE addRepresentative
DROP FUNCTION viewAvailableStadiumsOn
DROP PROCEDURE addHostRequest
DROP FUNCTION allUnassignedMatches
DROP PROCEDURE addStadiumManager
DROP FUNCTION allPendingRequests
DROP PROCEDURE acceptRequest
DROP PROCEDURE rejectRequest
DROP PROCEDURE addFan
DROP FUNCTION upcomingMatchesOfClub
DROP FUNCTION availableMatchesToAttend
DROP PROCEDURE purchaseTicket
DROP PROCEDURE updateMatchHost
DROP VIEW matchesPerTeam
DROP VIEW clubsNeverMatched
DROP FUNCTION clubsNeverPlayed
DROP FUNCTION matchWithHighestAttendance
DROP FUNCTION matchesRankedByAttendance
DROP FUNCTION requestsFromClub
GO

CREATE PROCEDURE clearAllTables
AS

ALTER TABLE host_request
DROP CONSTRAINT fk1_req
ALTER TABLE host_request
DROP CONSTRAINT fk2_req
ALTER TABLE host_request
DROP CONSTRAINT fk3_req

TRUNCATE TABLE  host_request


ALTER TABLE ticket_buying_transaction
DROP CONSTRAINT transaction_fk1
ALTER TABLE ticket_buying_transaction
DROP CONSTRAINT transaction_fk2

TRUNCATE TABLE ticket_buying_transaction


ALTER TABLE club_representative
DROP CONSTRAINT fk1_club_rep
ALTER TABLE club_representative
DROP CONSTRAINT fk2_club_rep

TRUNCATE TABLE club_representative


ALTER TABLE stadium_manager
DROP CONSTRAINT fk1_stadium_manager
ALTER TABLE stadium_manager
DROP CONSTRAINT fk2_stadium_manager

TRUNCATE TABLE stadium_manager


ALTER TABLE fan
DROP CONSTRAINT fk_fan

TRUNCATE TABLE fan


ALTER TABLE ticket
DROP CONSTRAINT fk_ticket

TRUNCATE TABLE ticket


ALTER TABLE match
DROP CONSTRAINT fk1_match
ALTER TABLE match
DROP CONSTRAINT fk2_match
ALTER TABLE match
DROP CONSTRAINT fk3_match

TRUNCATE TABLE match


TRUNCATE TABLE club


TRUNCATE TABLE stadium


ALTER TABLE system_admin
DROP CONSTRAINT fk_admin

TRUNCATE TABLE system_admin


ALTER TABLE association_manager
DROP CONSTRAINT fk_assoc_manager

TRUNCATE TABLE association_manager


TRUNCATE TABLE sys_user


ALTER TABLE host_request
ADD 
CONSTRAINT fk1_req FOREIGN KEY(representative_id) REFERENCES club_representative ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_req FOREIGN KEY(manager_id) REFERENCES stadium_manager ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT fk3_req FOREIGN KEY(match_id) REFERENCES match ON DELETE NO ACTION ON UPDATE NO ACTION

ALTER TABLE ticket_buying_transaction
ADD
CONSTRAINT transaction_fk1 FOREIGN KEY(fan_national_id) REFERENCES fan ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT transaction_fk2 FOREIGN KEY(ticket_id) REFERENCES ticket ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE club_representative
ADD
CONSTRAINT fk1_club_rep FOREIGN KEY(club_id) REFERENCES club ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_club_rep FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE stadium_manager
ADD
CONSTRAINT fk1_stadium_manager FOREIGN KEY(stadium_id) REFERENCES stadium ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_stadium_manager FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE fan
ADD
CONSTRAINT fk_fan FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE ticket
ADD
CONSTRAINT fk_ticket FOREIGN KEY(match_id) REFERENCES match ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE match
ADD
CONSTRAINT fk1_match FOREIGN KEY(host_club_ID) REFERENCES club ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk2_match FOREIGN KEY(guest_club_ID) REFERENCES club ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT fk3_match FOREIGN KEY(stadium_ID) REFERENCES stadium ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE system_admin
ADD
CONSTRAINT fk_admin FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE association_manager
ADD
CONSTRAINT fk_assoc_manager FOREIGN KEY(username) REFERENCES sys_user ON DELETE CASCADE ON UPDATE CASCADE
GO

CREATE VIEW allAssocManagers AS
SELECT A.username, U.password, A.name
FROM association_manager A
INNER JOIN sys_user U ON A.username = U.username
GO

CREATE VIEW allClubRepresentatives AS
SELECT CR.username, U.password, CR.name, C.name AS club_name
FROM club C
INNER JOIN club_representative CR ON C.club_id = CR.club_id
INNER JOIN sys_user U ON CR.username = U.username
GO

CREATE VIEW allStadiumManagers AS
SELECT SM.username, U.password, SM.name, S.name AS stadium_name
FROM stadium S
INNER JOIN stadium_manager SM ON S.stadium_id = SM.stadium_id
INNER JOIN sys_user U ON SM.username = U.username
GO

CREATE VIEW allFans AS
SELECT F.username, U.password, F.name, F.national_id, F.birth_date, F.status
FROM fan F
INNER JOIN sys_user U ON F.username = U.username
GO

CREATE VIEW allMatches AS
SELECT C1.name AS host_name, C2.name AS guest_name, M.start_time, M.end_time, M.stadium_ID as stad
FROM club C1
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON M.guest_club_ID = C2.club_id
GO

CREATE VIEW allTickets AS
SELECT C1.name AS host_name, C2.name AS guest_name, S.name, M.start_time
FROM ticket T 
INNER JOIN match M ON T.match_id = M.match_id 
INNER JOIN club C1 ON M.host_club_ID = C1.club_id
INNER JOIN club C2 ON M.guest_club_ID = C2.club_id
INNER JOIN stadium S ON S.stadium_id = M.stadium_ID
GO

CREATE VIEW allCLubs AS
SELECT name, location
FROM club
GO

CREATE VIEW allStadiums AS
SELECT name, location, capacity, status
FROM stadium
GO

CREATE VIEW allRequests AS
SELECT CR.username AS rep_username, SM.username AS manager_username, R.status
FROM host_request R
INNER JOIN club_representative CR ON R.representative_id = CR.id
INNER JOIN stadium_manager SM ON R.manager_id = SM.id
GO

CREATE PROCEDURE addAssociationManager
@name VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
AS
INSERT INTO sys_user VALUES(@username,@password)
INSERT INTO association_manager VALUES(@name,@username)
GO

CREATE PROCEDURE addNewMatch 
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start DATETIME,
@end DATETIME
AS
DECLARE @host_id INT
SELECT @host_id = club_id FROM club
WHERE name = @host_name
DECLARE @guest_id INT
SELECT @guest_id = club_id FROM club
WHERE name = @guest_name

IF NOT EXISTS ( SELECT * FROM match WHERE (host_club_ID = @host_id OR guest_club_ID = @host_id OR host_club_ID = @guest_id OR guest_club_ID = @guest_id) AND DATEPART(YY,@start)=DATEPART(YY,start_time) AND DATEPART(MM,@start)=DATEPART(MM,start_time) AND DATEPART(DD,@start)=DATEPART(DD,start_time)) AND @end > @start
BEGIN
INSERT INTO match(start_time,end_time,host_club_ID,guest_club_ID) VALUES(@start,@end,@host_id,@guest_id)
END
ELSE
BEGIN
IF @start >= @end
BEGIN
RAISERROR('Start time cannot be less than end time',16,1)
END
ELSE
BEGIN
RAISERROR('One of the clubs has a match at this date',16,1)
END
END
GO


CREATE VIEW clubsWithNoMatches AS
SELECT C.name
FROM club C
WHERE C.club_id NOT IN
(
SELECT C2.club_id
FROM CLUB C2, match M
WHERE C2.club_id = M.host_club_ID OR C2.club_id = M.guest_club_ID
)
GO

CREATE PROCEDURE deleteMatch
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start DATETIME,
@end DATETIME
AS
DECLARE @host_id INT
SELECT @host_id = club_id FROM club
WHERE name = @host_name
DECLARE @guest_id INT
SELECT @guest_id = club_id FROM club
WHERE name = @guest_name
DECLARE @match_id INT
SELECT @match_id FROM match
WHERE host_club_ID = @host_id AND guest_club_ID = @guest_id
DELETE FROM host_request
WHERE match_id IN ( SELECT M.match_id FROM match M WHERE M.host_club_ID = @host_id AND M.guest_club_ID = @guest_id)
DELETE FROM match
WHERE host_club_ID = @host_id AND guest_club_ID = @guest_id AND start_time = @start AND end_time = @end
GO

CREATE PROCEDURE deleteMatchesOnStadium
@stadium_name VARCHAR(20)
AS
DECLARE @stadium_id INT
SELECT @stadium_id = stadium_id FROM stadium
WHERE name = @stadium_name
DELETE FROM host_request
WHERE match_id IN ( SELECT M.match_id FROM match M WHERE M.stadium_ID = @stadium_id AND M.start_time > CURRENT_TIMESTAMP)
DELETE FROM match
WHERE stadium_ID = @stadium_id AND start_time > CURRENT_TIMESTAMP
GO

CREATE PROCEDURE addClub
@name VARCHAR(20),
@location VARCHAR(20)
AS
INSERT INTO club VALUES(@name, @location)
GO

CREATE PROCEDURE addTicket
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start DATETIME
AS
DECLARE @host_id INT
SELECT @host_id = club_id FROM club
WHERE name = @host_name
DECLARE @guest_id INT
SELECT @guest_id = club_id FROM club
WHERE name = @guest_name
DECLARE @match_id INT
SELECT @match_id = match_id FROM match
WHERE host_club_ID = @host_id AND guest_club_ID = @guest_id AND start_time = @start
INSERT INTO ticket VALUES('1',@match_id)
GO

CREATE PROCEDURE deleteClub
@name VARCHAR(20)
AS
DECLARE @club_id INT
SELECT @club_id = club_id FROM club
WHERE name = @name
DECLARE @match_id INT
SELECT @match_id = match_id FROM match
WHERE guest_club_ID = @club_id
DELETE FROM host_request
WHERE match_id IN (SELECT M.match_id FROM match M WHERE M.guest_club_ID = @club_id)
DELETE FROM match
WHERE guest_club_ID = @club_id
DELETE FROM club
WHERE name = @name
GO

CREATE PROCEDURE addStadium
@name VARCHAR(20),
@location VARCHAR(20),
@capacity INT
AS
INSERT INTO stadium VALUES(@name,@location,@capacity,'1')
GO

CREATE PROCEDURE deleteStadium
@name VARCHAR(20)
AS
DECLARE @stadium_id INT
SELECT @stadium_id = stadium_id FROM stadium
DECLARE @manager_id INT
SELECT @manager_id = id FROM stadium_manager
WHERE stadium_id = @stadium_id
DELETE FROM stadium
WHERE name = @name
DELETE FROM host_request
WHERE manager_id = @manager_id
GO

CREATE PROCEDURE blockFan
@id VARCHAR(20)
AS
UPDATE fan
SET status = '0'
WHERE national_id = @id
GO

CREATE PROCEDURE unblockFan
@id VARCHAR(20)
AS
UPDATE fan
SET status = '1'
WHERE national_id = @id
GO

CREATE PROCEDURE addRepresentative
@name VARCHAR(20),
@club_name VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
AS
DECLARE @club_id INT
SELECT @club_id = club_id FROM club
WHERE name = @club_name
INSERT INTO sys_user VALUES(@username,@password)
INSERT INTO club_representative VALUES(@name,@club_id,@username)
GO


CREATE FUNCTION viewAvailableStadiumsOn(@date DATETIME)
RETURNS @result TABLE(name VARCHAR(20), location VARCHAR(20), capacity INT)
AS
BEGIN
INSERT INTO @result 
SELECT S.name, S.location, S.capacity 
FROM stadium S 
WHERE S.status = '1' AND NOT EXISTS(
	SELECT S2.stadium_id
	FROM stadium S2 
	INNER JOIN match M ON S2.stadium_id = M.stadium_ID
	WHERE S.stadium_id = S2.stadium_id AND
	(
	 DATEPART(YY,@date)=DATEPART(YY,M.start_time) AND 
	 DATEPART(MM,@date)=DATEPART(MM,M.start_time) AND
	 DATEPART(DD,@date)=DATEPART(DD,M.start_time)
	) 
)
RETURN
END
GO

CREATE PROCEDURE addHostRequest
@club_name VARCHAR(20),
@stadium_name VARCHAR(20),
@start DATETIME
AS
DECLARE @club_id INT
DECLARE @rep_id INT
SELECT @club_id = club_id FROM club C
WHERE name = @club_name
SELECT @rep_id = id FROM club_representative
WHERE club_id = @club_id
DECLARE @stadium_id INT
DECLARE @manager_id INT
SELECT @stadium_id = stadium_id FROM stadium
WHERE name = @stadium_name
SELECT @manager_id = id FROM stadium_manager
WHERE stadium_id = @stadium_id
DECLARE @match_id INT
SELECT @match_id = match_id FROM match
WHERE host_club_ID = @club_id AND start_time = @start
INSERT INTO host_request(representative_id,manager_id,match_id) VALUES(@rep_id,@manager_id,@match_id)
GO

CREATE FUNCTION allUnassignedMatches(@club_name VARCHAR(20))
RETURNS @result TABLE(name VARCHAR(20), start_time DATETIME)
AS
BEGIN
DECLARE @host_id INT
SELECT @host_id = club_id FROM club 
WHERE name = @club_name
INSERT INTO @result
SELECT C2.name, M.start_time
FROM club C1 
INNER JOIN match M ON C1.club_id = M.host_club_ID 
INNER JOIN club C2 ON C2.club_id = M.guest_club_ID 
WHERE M.stadium_ID IS NULL AND M.host_club_ID = @host_id AND M.start_time > CURRENT_TIMESTAMP
RETURN
END
GO

CREATE PROCEDURE addStadiumManager
@manager_name VARCHAR(20),
@stadium_name VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
AS
DECLARE @stadium_id INT
SELECT @stadium_id = stadium_id FROM stadium
WHERE name = @stadium_name
INSERT INTO sys_user VALUES(@username,@password)
INSERT INTO stadium_manager VALUES(@manager_name,@stadium_id,@username)
GO

CREATE FUNCTION allPendingRequests(@manager_username VARCHAR(20))
RETURNS @result TABLE(rep_name VARCHAR(20), club_name VARCHAR(20), start_time DATETIME)
AS
BEGIN
DECLARE @manager_id INT
SELECT @manager_id = id FROM stadium_manager
WHERE username = @manager_username
INSERT INTO @result 
SELECT CR.name, C.name as club_name, M.start_time 
	   FROM host_request R 
	   INNER JOIN club_representative CR ON R.representative_id = CR.id 
	   INNER JOIN match M ON M.match_id = R.match_id 
	   INNER JOIN club C ON C.club_id = M.guest_club_ID
	   WHERE R.manager_id = @manager_id AND R.status IS NULL
RETURN
END
GO
CREATE FUNCTION allRequestsPerManager(@manager_username VARCHAR(20))
RETURNS @result TABLE(rep_name VARCHAR(20), host_name VARCHAR(20), guest_name VARCHAR(20), start_time DATETIME, end_time DATETIME, status BIT)
AS
BEGIN
DECLARE @manager_id INT
SELECT @manager_id = id FROM stadium_manager
WHERE username = @manager_username
INSERT INTO @result 
SELECT CR.name, C1.name as host_name, C2.name as guest_name, M.start_time, M.end_time, R.status
	   FROM host_request R 
	   INNER JOIN club_representative CR ON R.representative_id = CR.id 
	   INNER JOIN match M ON M.match_id = R.match_id 
	   INNER JOIN club C1 ON C1.club_id = M.host_club_ID
	   INNER JOIN club C2 ON C2.club_id = M.guest_club_ID
	   WHERE R.manager_id = @manager_id
RETURN
END
GO

CREATE PROCEDURE acceptRequest
@manager_username VARCHAR(20),
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start DATETIME
AS
DECLARE @stadium_id INT
DECLARE @manager_id INT
SELECT @stadium_id = stadium_id FROM stadium_manager
WHERE username = @manager_username
SELECT @manager_id = id FROM stadium_manager
WHERE username = @manager_username
DECLARE @host_id INT
DECLARE @rep_id INT
SELECT @host_id = club_id FROM club
WHERE name = @host_name
SELECT @rep_id = id FROM club_representative
WHERE club_id = @host_id
DECLARE @guest_id INT
SELECT @guest_id = club_id FROM club
WHERE name = @guest_name
DECLARE @match_id INT
SELECT @match_id = match_id FROM match
WHERE host_club_ID = @host_id AND guest_club_ID = @guest_id AND start_time = @start
IF EXISTS(SELECT * FROM match WHERE stadium_ID = @stadium_id AND DATEPART(YY,start_time) = DATEPART(YY,@start) AND DATEPART(MM,start_time) = DATEPART(MM,@start) AND DATEPART(DD,start_time) = DATEPART(DD,@start))
BEGIN
RAISERROR('The stadium is hosting another match on that date',16,1)
END
ELSE
BEGIN
UPDATE host_request
SET status = '1'
WHERE match_id = @match_id AND manager_id = @manager_id AND representative_id = @rep_id
DECLARE @val BIT
SELECT @val = R.status FROM host_request R 
WHERE R.match_id = @match_id AND R.manager_id = @manager_id AND R.representative_id = @rep_id
IF @val='1'
BEGIN
UPDATE match
SET stadium_ID = @stadium_id
WHERE match_id = @match_id
END
END
GO


CREATE PROCEDURE rejectRequest
@manager_username VARCHAR(20),
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start DATETIME
AS
DECLARE @stadium_id INT
DECLARE @manager_id INT
SELECT @stadium_id = stadium_id FROM stadium_manager
WHERE username = @manager_username
SELECT @manager_id = id FROM stadium_manager
WHERE username = @manager_username
DECLARE @host_id INT
DECLARE @rep_id INT
SELECT @host_id = club_id FROM club
WHERE name = @host_name
SELECT @rep_id = id FROM club_representative
WHERE club_id = @host_id
DECLARE @guest_id INT
SELECT @guest_id = club_id FROM club
WHERE name = @guest_name
DECLARE @match_id INT
SELECT @match_id = match_id FROM match
WHERE host_club_ID = @host_id AND guest_club_ID = @guest_id AND start_time = @start
UPDATE host_request
SET status = '0'
WHERE match_id = @match_id AND manager_id = @manager_id AND representative_id = @rep_id
GO

CREATE PROCEDURE addFan
@name VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20),
@national_id VARCHAR(20),
@birth_date DATETIME,
@address VARCHAR(20),
@phone INT
AS
INSERT INTO sys_user VALUES(@username,@password)
INSERT INTO fan VALUES(@national_id,@name,@birth_date,@address,@phone,'1',@username)
GO

CREATE FUNCTION upcomingMatchesOfClub(@club_name VARCHAR(20))
RETURNS @result TABLE(host_name VARCHAR(20), guest_name VARCHAR(20), start_time DATETIME, end_time DATETIME, stadium_name VARCHAR(20))
AS
BEGIN
DECLARE @club_id INT
SELECT @club_id = club_id FROM club
WHERE name = @club_name
INSERT INTO @result
SELECT C1.name AS host, C2.name AS guest, M.start_time, M.end_time, S.name as stadium_name
FROM club C1 
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON C2.club_id = M.guest_club_ID
LEFT OUTER JOIN stadium S ON M.stadium_ID = S.stadium_id
WHERE M.start_time > CURRENT_TIMESTAMP AND M.host_club_ID = @club_id
UNION
SELECT C1.name AS host, C2.name As guest, M.start_time, M.end_time, S.name as stadium_name
FROM club C1
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON C2.club_id = M.guest_club_ID
LEFT OUTER JOIN stadium S ON M.stadium_ID = S.stadium_id
WHERE M.start_time > CURRENT_TIMESTAMP AND M.guest_club_ID = @club_id
ORDER BY M.start_time
RETURN
END
GO


CREATE FUNCTION availableMatchesToAttend(@date DATETIME)
RETURNS @result TABLE(host VARCHAR(20),guest VARCHAR(20), start_time DATETIME, stadium_name VARCHAR(20), stadium_location VARCHAR(20))
AS
BEGIN
INSERT INTO @result
SELECT C1.name as host, C2.name as guest,M.start_time, S.name, S.location
FROM club C1
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON C2.club_id = M.guest_club_ID
INNER JOIN stadium S ON M.stadium_ID = S.stadium_id
WHERE M.start_time > CURRENT_TIMESTAMP AND @date <= M.start_time AND EXISTS (
SELECT T.id FROM ticket T WHERE T.status = '1' AND T.match_id = M.match_id)
RETURN
END
GO

CREATE PROCEDURE purchaseTicket
@national_id VARCHAR(20),
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start DATETIME
AS
DECLARE @status BIT
SELECT @status = status FROM fan
WHERE national_id = @national_id
IF @status='1'
BEGIN
DECLARE @host_id INT
SELECT @host_id = club_id FROM club
WHERE name = @host_name
DECLARE @guest_id INT
SELECT @guest_id = club_id FROM club
WHERE name = @guest_name
DECLARE @match_id INT
SELECT @match_id = match_id FROM match
WHERE host_club_ID = @host_id AND guest_club_ID = @guest_id AND start_time = @start
DECLARE @ticked_id INT
SELECT top 1 @ticked_id = id FROM ticket
WHERE match_id = @match_id AND status = '1'
INSERT INTO ticket_buying_transaction VALUES(@national_id,@ticked_id)
UPDATE ticket
SET status = '0'
WHERE id = @ticked_id
END
GO

CREATE PROCEDURE updateMatchHost
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start DATETIME
AS
DECLARE @host_id INT
SELECT @host_id = club_id FROM club
WHERE name = @host_name
DECLARE @guest_id INT
SELECT @guest_id = club_id FROM club
WHERE name = @guest_name
DECLARE @match_id INT
SELECT @match_id = match_id FROM match
WHERE host_club_ID = @host_id AND guest_club_ID = @guest_id AND start_time = @start
UPDATE match
SET host_club_ID = @guest_id WHERE match_id = @match_id
UPDATE match
SET guest_club_ID = @host_id WHERE match_id = @match_id
UPDATE match
SET stadium_ID = NULL WHERE match_id = @match_id
DELETE FROM host_request
WHERE match_id = @match_id
DELETE FROM ticket
WHERE match_id = @match_id
GO

CREATE VIEW matchesPerTeam
AS
SELECT C.name , COUNT(M.match_id) AS matches_played FROM club C
LEFT OUTER JOIN match M ON C.club_id = M.host_club_ID OR C.club_id = M.guest_club_ID
WHERE M.end_time < CURRENT_TIMESTAMP OR M.end_time IS NULL
GROUP BY C.name
GO

CREATE VIEW clubsNeverMatched
AS
SELECT C1.name AS first_club, C2.name AS second_club FROM club C1, club C2
WHERE C1.club_id < C2.club_id AND NOT EXISTS
( 
	SELECT C3.name AS x, C4.name As y
	FROM club C3 
	INNER JOIN match M ON C3.club_id = M.host_club_ID 
	INNER JOIN club C4 ON C4.club_id = M.guest_club_ID
	WHERE 
	M.stadium_ID IS NOT NULL AND M.end_time < CURRENT_TIMESTAMP AND(
	(C3.club_id = C1.club_id AND C4.club_id = C2.club_id)
	OR
	(C3.club_id = C2.club_id AND C4.club_id = C1.club_id))
)
GO

CREATE FUNCTION clubsNeverPlayed(@club_name VARCHAR(20))
RETURNS @result TABLE(club_name VARCHAR(20))
AS
BEGIN
DECLARE @club_id INT
SELECT @club_id = club_id FROM club
WHERE name = @club_name
INSERT INTO @result
SELECT C.name
FROM club C
WHERE C.name <> @club_name
EXCEPT
(
SELECT DISTINCT C2.name
FROM club C1
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON C2.club_id = M.guest_club_ID
WHERE C1.club_id = @club_id AND M.end_time < CURRENT_TIMESTAMP AND M.stadium_ID IS NOT NULL

UNION

SELECT DISTINCT C4.name
FROM club C3
INNER JOIN match M2 ON C3.club_id = M2.guest_club_ID
INNER JOIN club C4 ON C4.club_id = M2.host_club_ID
WHERE C3.club_id = @club_id AND M2.end_time < CURRENT_TIMESTAMP AND M2.stadium_ID IS NOT NULL
)
RETURN
END
GO

CREATE FUNCTION matchWithHighestAttendance()
RETURNS @result TABLE(host VARCHAR(20), guest VARCHAR(20))
AS
BEGIN
INSERT INTO @result
SELECT C1.name, C2.name
FROM club C1 
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON M.guest_club_ID = C2.club_id
INNER JOIN ticket T ON T.match_id = M.match_id
WHERE T.status = '0'
GROUP BY C1.NAME, C2.NAME, M.match_id
HAVING COUNT(*) = 
(
	SELECT MAX(X.Y) FROM
	(
		SELECT M2.match_id, COUNT(T.id) AS Y FROM match M2 
		INNER JOIN ticket T ON M2.match_id = T.match_id WHERE T.status = '0'
		GROUP BY M2.match_id
	) AS X
)
RETURN
END
GO

CREATE FUNCTION matchesRankedByAttendance()
RETURNS @result TABLE(host VARCHAR(20), guest VARCHAR(20), sold INT)
AS
BEGIN
INSERT INTO @result
SELECT C1.name AS host, C2.name AS guest, COUNT(*) AS tickets_sold FROM club C1 
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON M.guest_club_ID = C2.club_id
INNER JOIN ticket T ON T.match_id = M.match_id
WHERE M.end_time < CURRENT_TIMESTAMP AND T.status = '0'
GROUP BY C1.NAME, C2.NAME, M.match_id
ORDER BY tickets_sold DESC
OFFSET 0 ROWS
RETURN
END
GO

CREATE FUNCTION requestsFromClub(@stadium_name VARCHAR(20), @club_name VARCHAR(20))
RETURNS @result TABLE(host VARCHAR(20), guest VARCHAR(20))
AS
BEGIN
INSERT INTO @result
SELECT C1.name AS host, C2.name AS guest FROM club C1
INNER JOIN match M ON C1.club_id = M.host_club_ID
INNER JOIN club C2 ON C2.club_id = M.guest_club_ID
INNER JOIN host_request R ON R.match_id = M.match_id
INNER JOIN stadium_manager SM ON R.manager_id = SM.id
INNER JOIN stadium S ON S.stadium_id = SM.stadium_id
WHERE S.name = @stadium_name AND C1.name = @club_name
RETURN
END
GO

CREATE PROCEDURE login
@username VARCHAR(20),
@password VARCHAR(20),
@user_type INT OUTPUT,
@flag BIT OUTPUT
AS
BEGIN
IF EXISTS( SELECT username, password FROM sys_user WHERE username = @username AND password = @password )
BEGIN
SET @flag = '1'
	IF EXISTS( SELECT username FROM system_admin WHERE username = @username )
	BEGIN
	SET @user_type = 1
	END
	ELSE
	BEGIN
		IF EXISTS( SELECT username FROM association_manager WHERE username = @username )
		BEGIN
		SET @user_type = 2
		END
		ELSE
		BEGIN
			IF EXISTS( SELECT username FROM club_representative WHERE username = @username )
			BEGIN
			SET @user_type = 3
			END
			ELSE
			BEGIN
				IF EXISTS( SELECT username FROM stadium_manager WHERE username = @username )
					BEGIN
					SET @user_type = 4
					END
					ELSE
					BEGIN
						IF EXISTS( SELECT username FROM fan WHERE username = @username )
							BEGIN
							SET @user_type = 5
							END
END
END
END
END
END
ELSE
SET @flag = '0'
END
GO


CREATE FUNCTION clubExists(@club_name VARCHAR(20))
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT name FROM club WHERE name = @club_name)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION stadiumExists(@stadium_name VARCHAR(20))
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT name FROM stadium WHERE name = @stadium_name)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION userExists(@username VARCHAR(20))
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT username FROM sys_user WHERE username = @username)
SET @out = '0'
ELSE
SET @out = '1'
RETURN @out
END
GO

CREATE FUNCTION fanExists(@id VARCHAR(20))
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT national_id FROM fan WHERE national_id = @id)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION fanStatus(@id VARCHAR(20))
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
SELECT @out = status FROM fan WHERE national_id = @id 
RETURN @out
END
GO

CREATE FUNCTION matchExists(@host VARCHAR(20),@guest VARCHAR(20),@start DATETIME, @end DATETIME)
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT * FROM match M inner join club C1 on C1.club_id = M.host_club_ID inner join club C2 on C2.club_id = M.guest_club_ID WHERE C1.name = @host AND C2.name = @guest AND M.start_time = @start AND M.end_time = @end)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION matchExists2(@host VARCHAR(20),@start DATETIME)
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT * FROM match M inner join club C1 on C1.club_id = M.host_club_ID WHERE C1.name = @host AND M.start_time = @start)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE PROCEDURE viewClubInfo(@rep_username VARCHAR(20))
AS
SELECT C.club_id,C.name,C.location FROM club C INNER JOIN club_representative CR ON C.club_id = CR.club_id
WHERE CR.username = @rep_username
GO

CREATE PROCEDURE viewStadiumInfo(@manager_username VARCHAR(20))
AS
SELECT S.stadium_id,S.name,S.location, S.capacity, S.status FROM Stadium S INNER JOIN stadium_manager SM ON S.stadium_id = SM.stadium_id
WHERE SM.username = @manager_username
GO

CREATE PROCEDURE viewFanInfo(@fan_username VARCHAR(20))
AS
SELECT national_id, status FROM Fan
WHERE username = @fan_username
GO

CREATE FUNCTION requestExists(@manager_username VARCHAR(20), @host_name VARCHAR(20), @guest_name VARCHAR(20), @start_time DATETIME)
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT *
	   FROM host_request R 
	   INNER JOIN match M ON M.match_id = R.match_id 
	   INNER JOIN club C1 ON C1.club_id = M.host_club_ID
	   INNER JOIN club C2 ON C2.club_id = M.guest_club_ID
	   INNER JOIN stadium_manager SM ON SM.id = R.manager_id
	   WHERE SM.username = @manager_username AND C1.name = @host_name AND C2.name = @guest_name AND M.start_time = @start_time)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION ticketExists(@host VARCHAR(20), @guest VARCHAR(20), @start DATETIME)
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT * FROM [dbo].availableMatchesToAttend(@start) WHERE host = @host AND guest = @guest AND start_time = @start )
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION requestExists(@manager_username VARCHAR(20), @host_name VARCHAR(20), @guest_name VARCHAR(20), @start_time DATETIME)
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT *
	   FROM [dbo].allRequestsPerManager(@manager_username)
	   WHERE host_name = @host_name AND guest_name = @guest_name AND start_time = @start_time AND status IS NULL)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION hasManager(@stad_name VARCHAR(20))
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT * FROM stadium_manager SM INNER JOIN stadium S ON S.stadium_id = SM.stadium_id WHERE S.name = @stad_name)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

CREATE FUNCTION hasRep(@club_name VARCHAR(20))
RETURNS BIT
AS
BEGIN
DECLARE @out BIT
IF EXISTS( SELECT * FROM club_representative CR INNER JOIN club C ON C.club_id = CR.club_id WHERE C.name = @club_name)
SET @out = '1'
ELSE
SET @out = '0'
RETURN @out
END
GO

INSERT INTO sys_user VALUES('admin','admin')
INSERT INTO system_admin VALUES('admin','admin')