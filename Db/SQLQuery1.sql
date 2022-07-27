CREATE TABLE SourceGroup  
(  
  GroupId VARCHAR(30),
  SourceName VARCHAR(30),
  Capacity Int,
  PRIMARY KEY(GroupId),

); 
CREATE TABLE Sources  
(  
  SourceId VARCHAR(30),
  PRIMARY KEY(SourceId)

); 

CREATE TABLE Employee  
(  
	EmployeeId VARCHAR(30),
	EmployeeName VARCHAR(30),
	PRIMARY KEY(EmployeeId)

); 



CREATE TABLE Owns  
(  
	SourceId VARCHAR(30),
	EmployeeId VARCHAR(30),
	FOREIGN KEY (EmployeeId) REFERENCES Employee (EmployeeId),
    FOREIGN KEY (SourceId) REFERENCES Sources (SourceId),
	PRIMARY KEY(EmployeeId,SourceId)
); 

CREATE TABLE Manipulates  
(  
	  GroupId VARCHAR(30),
	  EmployeeId VARCHAR(30),
	  FOREIGN KEY (GroupId) REFERENCES SourceGroup (GroupId),
      FOREIGN KEY (EmployeeId) REFERENCES Employee (EmployeeId),
	  PRIMARY KEY(GroupId,EmployeeId)
); 


Drop Table IF EXISTS Owns ;
Drop Table IF EXISTS SourceGroup ;
Drop Table IF EXISTS  Sources ;
Drop Table IF EXISTS Employee ;
Drop Table IF EXISTS Manipulates ;