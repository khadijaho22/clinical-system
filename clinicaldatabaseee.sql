DROP DATABASE IF EXISTS clinicaldatabaseee;
CREATE DATABASE ClinicalDatabaseee;
USE ClinicalDatabaseee;

CREATE TABLE USER (
    UserID INT PRIMARY KEY,
    UserType VARCHAR(255),
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Gender VARCHAR(255),
    BirthDate DATE,
    Email VARCHAR(255),
    PhoneNumber VARCHAR(255),
    Address VARCHAR(255),
    About VARCHAR(255),
    Age INT,
    Username VARCHAR(255) UNIQUE,
    Password VARCHAR(255),
    HireDate DATE,
    image_name VARCHAR(255),
    image_data MEDIUMBLOB
);
DELIMITER //
CREATE TRIGGER calculate_age_user
BEFORE INSERT ON USER
FOR EACH ROW
BEGIN
    SET NEW.Age = TIMESTAMPDIFF(YEAR, NEW.BirthDate, CURDATE());
END;
//
DELIMITER ;


CREATE TABLE PATIENT (
    PatientID BIGINT PRIMARY KEY,
    PatientType VARCHAR(255),
    BirthDate DATE,
    Address VARCHAR(255),
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    NatinalID BIGINT,
    Gender varchar(225),
    OtherContact VARCHAR(255),
    ReferralDoctor VARCHAR(225),
    PhoneNumber BIGINT,
    WorkPlace varchar(225),
    Age INT
);
DELIMITER //
CREATE TRIGGER calculate_age_patient
BEFORE INSERT ON Patient
FOR EACH ROW
BEGIN
    SET NEW.Age = TIMESTAMPDIFF(YEAR, NEW.BirthDate, CURDATE());
END;
// 
DELIMITER ;

CREATE TABLE PATIENT_VISIT (
    VisitID BIGINT PRIMARY KEY,
    PatientID BIGINT,
    VisitDate DATE,
    VisitType VARCHAR(255),
    Diagnosis VARCHAR(255),
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID)
);


-- Create the REPORT table
CREATE TABLE IF NOT EXISTS REPORT (
    ReportID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID BIGINT,
    UserID INT,
    Title TEXT,
    Description VARCHAR(300),
    ReportDateAndTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    SessionNumber INT,
    Diagnosis VARCHAR(300),
    TreatmentStage VARCHAR(100),
    FOREIGN KEY (UserID) REFERENCES USER(UserID),
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID)
);

-- Create the CAMERA table
CREATE TABLE IF NOT EXISTS CAMERA (
    CameraID INT PRIMARY KEY AUTO_INCREMENT,
    Brand VARCHAR(100),
    Model VARCHAR(100),
    ViewDateAndTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ViewDescription VARCHAR(300)
);

-- Create the ATTENDANCE table
CREATE TABLE IF NOT EXISTS ATTENDANCE (
    AttendanceID INT PRIMARY KEY AUTO_INCREMENT,
    OwnerID INT,
    DoctorID INT,
    ReceptionID INT,
    TimeofArrival TIME,
    TimeofLeave TIME,
    DateofTheDay DATE,
    Absence VARCHAR(300),
    Excuse VARCHAR(300),
    TotalHoursWorking DOUBLE,
    FOREIGN KEY (OwnerID) REFERENCES USER(UserID),
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (ReceptionID) REFERENCES USER(UserID)
);


-- APPOINTMENTS table
CREATE TABLE IF NOT EXISTS APPOINTMENTS (
    AppointmentID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID BIGINT,
    DoctorID INT,
    AppointmentDate DATE,
    AppointmentTime TIME,
     SelectedColor VARCHAR(300),
    ReferralDoctor VARCHAR(300),
    ClinicReferralDoctorID INT,
    Insurance VARCHAR(300),
    StateOfPatient VARCHAR(100),
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (ClinicReferralDoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID)
);

CREATE TABLE IF NOT EXISTS Appointment (
    AppointmentID INT AUTO_INCREMENT PRIMARY KEY,
    Time DATETIME,
    PatientName VARCHAR(255),
    SelectedColor VARCHAR(50),
    SelectedDay VARCHAR(20)
);

-- ASSESSMENT table
CREATE TABLE IF NOT EXISTS ASSESSMENT (
    AssessmentID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID BIGINT,
    DoctorID INT,
    ReceptionID INT,
    PatientComplaint VARCHAR(300),
    BodyPart VARCHAR(100),
    InjuryType VARCHAR(100),
    TreatmentPlan VARCHAR(300),
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (ReceptionID) REFERENCES USER(UserID)
);

-- MEDICAL_HISTORY table
CREATE TABLE IF NOT EXISTS MEDICAL_HISTORY (
	MedicalHistoryID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID BIGINT,
    Surgeries VARCHAR(255),
    Illness VARCHAR(255),
    FamilyMedicalBackground VARCHAR(100),
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID)
);

-- PATIENT_DATA table
CREATE TABLE IF NOT EXISTS PATIENT_DATA (
	PatientDataID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID BIGINT,
    AssessmentID INT,
    MedicalHistoryID INT,
    ReportID INT,
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (AssessmentID) REFERENCES ASSESSMENT(AssessmentID),
    FOREIGN KEY (MedicalHistoryID) REFERENCES MEDICAL_HISTORY(MedicalHistoryID),
    FOREIGN KEY (ReportID) REFERENCES REPORT(ReportID)
);

-- RELATIONS
-- CHECK_TIMES table
CREATE TABLE IF NOT EXISTS CHECK_TIMES (
    AppointmentID INT,
    DoctorID INT,
    FOREIGN KEY (AppointmentID) REFERENCES APPOINTMENTS(AppointmentID),
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID)
);

-- RECORD_INFO table
CREATE TABLE IF NOT EXISTS RECORD_INFO (
    PatientID BIGINT,
    ReceptionID INT,
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (ReceptionID) REFERENCES USER(UserID)
);

-- VIEW_CAMERA table
CREATE TABLE IF NOT EXISTS VIEW_CAMERA (
    CameraID INT,
    OwnerID INT,
    ReceptionID INT,
    FOREIGN KEY (CameraID) REFERENCES CAMERA(CameraID),
    FOREIGN KEY (OwnerID) REFERENCES USER(UserID),
    FOREIGN KEY (ReceptionID) REFERENCES USER(UserID)
);

-- ADD_USER table
CREATE TABLE IF NOT EXISTS ADD_USER (
    OwnerID INT,
    DoctorID INT,
    ReceptionID INT,
    FOREIGN KEY (OwnerID) REFERENCES USER(UserID),
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (ReceptionID) REFERENCES USER(UserID)
);

-- RECORD_ATTENDANCE table
CREATE TABLE IF NOT EXISTS RECORD_ATTENDANCE (
    AttendanceID INT,
    DoctorID INT,
    ReceptionID INT,
    FOREIGN KEY (AttendanceID) REFERENCES ATTENDANCE(AttendanceID),
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (ReceptionID) REFERENCES USER(UserID)
);

-- VIEW_ATTENDANCE table
CREATE TABLE IF NOT EXISTS VIEW_ATTENDANCE (
    OwnerID INT,
    DoctorID INT,
    ReceptionID INT,
    AttendanceID INT,
    FOREIGN KEY (OwnerID) REFERENCES USER(UserID),
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (ReceptionID) REFERENCES USER(UserID),
    FOREIGN KEY (AttendanceID) REFERENCES ATTENDANCE(AttendanceID)
);

-- VIEW_PATIENT_DATA table
CREATE TABLE IF NOT EXISTS VIEW_PATIENT_DATA (
    DoctorID INT,
    PatientID BIGINT,
    FOREIGN KEY (DoctorID) REFERENCES USER(UserID),
    FOREIGN KEY (PatientID) REFERENCES PATIENT_DATA(PatientID)
);

INSERT INTO USER (UserID, UserType, FirstName, LastName, Gender, BirthDate, Email, PhoneNumber, Address, About, Age, Username, Password, HireDate, image_name, image_data)
VALUES
('1', NULL, 'Mohamed', 'Adel', 'Male', '1990-02-10', 'm.tariq2151@nu.edu.eg', '01022668018', 'October2', 'About Mohamed', '33', 'O.Mohamed1', 'PASSWORD1', NULL, 'wallpaperflare.com_wallpaper (1).jpg', NULL),
('2', NULL, 'Mahmoud', 'Nader', 'Male', '2000-02-21', 'Toka.amr29@gmail.com', '1012345678', 'October', 'About Mahmoud', '23', 'R.Mahmoud2', 'PASSWORD2', NULL, NULL, NULL),
('3', NULL, 'Rawan', 'Ibrahem', 'Female', '2004-02-21', 'minnahtariq9@gmail.com', '1012345678', 'October', 'About Dr.Rawan', '19', 'Dr.Rawan3', 'PASSWORD3', NULL, NULL, NULL),
('4', NULL, 'Jana', 'Aymen', 'Female', '2003-07-02', 'khadijashiref2003@gmail.com', '1012345678', 'October', 'About Dr.Jana', '20', 'Dr.Jana4', 'PASSWORD4', NULL, NULL, NULL),
('5', NULL, 'Hager', 'Ahemd', 'Female', '2003-01-08', 'khadijashreef00@gmail.com', '1012345678', 'October', 'About Dr.Hager', '21', 'Dr.Hager5', 'PASSWORD5', NULL, NULL, NULL),
('194', 'Owner', 'qqqqq', 'qqqqqqqq', 'Male', '2024-01-08', 'qqqq@', '1243567894', 'qqqqqqq', NULL, '0', 'O.qqqqq0194', '@W123456', '2024-01-08', NULL, NULL),
('226', 'Owner', 'qqqq', 'wwwwwww', 'Female', '2000-08-15', 'rrrrrrrrrrrr@', '1234567926', 'eeeeeeeeeeee', NULL, '23', 'O.qqqq226', 'O@123456', '2015-09-05', NULL, NULL),
('390', 'Doctor', 'wwww', 'wwwwwwwww', 'Male', '2024-01-08', '@', '1234567890', 'qqqqq', NULL, '0', 'Dr.wwww0390', 'O@123456789', '2024-01-08', NULL, NULL),
('399', 'Doctor', 'kkkkkkk', 'kkkkkkkkk', 'Female', '1965-06-25', '@', '1234567899', 'wwwwwwwwww', NULL, '58', 'Dr.kkkkkkk0399', 'Q@123456', '2021-04-15', NULL, NULL),
('10263', 'Owner', 'qqqqqqww', 'wwwwww', 'Female', '2258-09-23', 'eeeeeeeeeeeee@', '1064704463', 'ttttttttttt', NULL, '-234', 'O.qqqqqqww10263', 'F$598746', '2000-05-14', NULL, NULL),
('20052', 'Reception', 'RR', 'www', 'Male', '2024-01-08', '@', '1234567952', 'qqqqq', NULL, '0', 'R.RR20052', 'L2123456@', '2024-01-08', NULL, NULL),
('20225', 'Reception', 'rrr', 'tttttt', 'Female', '2000-09-07', '@llll', '1186902425', 'ggggggg', NULL, '23', 'R.rrr20225', 'Ss@12345', '2019-08-05', NULL, NULL),
('20319', 'Reception', 'gghg', 'lkjl', 'Female', '2023-12-26', 'gjh@', '1022668019', 'hgjhg', NULL, '0', 'R.gghg20319', 'K@hkj12563', '2024-01-08', NULL, NULL),
('30298', 'Doctor', 'qqqqq', 'wwwwwww', 'Male', '2012-09-15', 'pppppppp@', '1234567898', 'ooooooo', NULL, '11', 'Dr.qqqqq30298', 'K2@012345', '1908-05-04', NULL, NULL),
('30392', 'Doctor', 'lllllll', 'ppppp', 'Male', '2024-01-08', '@lllll', '1234567892', 'ooooooooo', NULL, '0', 'Dr.lllllll30392', '125678L2@', '2024-01-08', NULL, NULL),
('30419', 'Doctor', 'eeeee', 'jjjjjjjjjj', 'Male', '2024-01-08', '@', '1022668019', 'iiiiiiiiiii', NULL, '0', 'Dr.eeeee30419', 'D@1236985', '2024-01-08', NULL, NULL),
('30530', 'Doctor', 'ddddddd', 'yyyyyyyy', 'Male', '2024-01-08', 'sww@', '1122489530', '5ssss', NULL, '0', 'Dr.ddddddd30530', 'S#543167s8', '2024-01-08', NULL, NULL);

-- INSERT INTO PATIENT (PatientType, BirthDate, Address, FirstName, LastName, NatinalID, Gender, OtherContact, ReferralDoctor, PhoneNumber, WorkPlace)
-- VALUES 
-- ('Emergency', '1990-04-10', 'Address1', 'Michael', 'Johnson', 111122223333, 'Male', 'Contact1', 'Doctor1', 1111222233, 'Workplace1'),
-- ('Specialized Care', '1985-12-05', 'Address2', 'Sarah', 'Williams', 444455556666, 'Female', 'Contact2', 'Doctor2', 4444555566, 'Workplace2'),
-- ('Routine', '1975-09-18', 'Address3', 'Bob', 'Anderson', 888899990000, 'Male', 'Contact3', 'Doctor3', 8888999900, 'Workplace3'),
-- ('Emergency', '1992-06-25', 'Address4', 'Emily', 'Brown', 777788889999, 'Female', 'Contact4', 'Doctor4', 7777888899, 'Workplace4'),
-- ('Specialized Care', '1988-02-12', 'Address5', 'Daniel', 'Miller', 333344445555, 'Male', 'Contact5', 'Doctor5', 3333444455, 'Workplace5'),
-- ('Routine', '1972-11-08', 'Address6', 'Alice', 'Davis', 666677778888, 'Female', 'Contact6', 'Doctor6', 6666777788, 'Workplace6'),
-- ('Emergency', '1995-10-02', 'Address7', 'Christopher', 'Wilson', 222233334444, 'Male', 'Contact7', 'Doctor7', 2222333344, 'Workplace7'),
-- ('Specialized Care', '1980-07-15', 'Address8', 'Olivia', 'Johnson', 999900001111, 'Female', 'Contact8', 'Doctor8', 9999000011, 'Workplace8'),
-- ('Routine', '1968-04-30', 'Address9', 'William', 'Moore', 555566667777, 'Male', 'Contact9', 'Doctor9', 5555666677, 'Workplace9'),
-- ('Emergency', '1998-01-17', 'Address10', 'Sophia', 'Martin', 666677778888, 'Female', 'Contact10', 'Doctor10', 6666777788, 'Workplace10');

INSERT INTO PATIENT (PatientID, PatientType, BirthDate, Address, FirstName, LastName, NatinalID, Gender, OtherContact, ReferralDoctor, PhoneNumber, WorkPlace)
VALUES 
(1, 'Type1', '1990-03-15', '123 Main St', 'John', 'Doe', 123456789, 'Male', 'Jane Doe', 'Dr. Smith', 1234567890, 'Hospital ABC'),
(2, 'Type2', '1985-07-20', '456 Elm St', 'Jane', 'Smith', 987654321, 'Female', 'John Smith', 'Dr. Johnson', 9876543210, 'Clinic XYZ'),
(3, 'Type1', '1978-11-25', '789 Oak St', 'Alice', 'Johnson', 456123789, 'Female', 'Bob Johnson', 'Dr. Brown', 4561237890, 'Health Center 123'),
(4, 'Type2', '1982-09-10', '246 Maple St', 'Michael', 'Williams', 789456123, 'Male', 'Sarah Williams', 'Dr. Garcia', 7894561230, 'Medical Center DEF'),
(5, 'Type1', '1992-05-02', '135 Pine St', 'Emily', 'Brown', 654987321, 'Female', 'David Brown', 'Dr. Lee', 6549873210, 'Clinic UVW'),
(6, 'Type2', '1975-12-30', '369 Cedar St', 'Christopher', 'Jones', 321654987, 'Male', 'Jennifer Jones', 'Dr. Martinez', 3216549870, 'Hospital GHI'),
(7, 'Type1', '1988-08-17', '579 Walnut St', 'Jessica', 'Garcia', 987123654, 'Female', 'Daniel Garcia', 'Dr. Taylor', 9871236540, 'Health Center XYZ'),
(8, 'Type2', '1996-04-05', '753 Birch St', 'Matthew', 'Rodriguez', 159753468, 'Male', 'Rachel Rodriguez', 'Dr. White', 1597534680, 'Medical Center JKL'),
(9, 'Type1', '1983-10-28', '951 Ash St', 'Sarah', 'Hernandez', 357159846, 'Female', 'Michael Hernandez', 'Dr. Adams', 3571598460, 'Clinic MNO'),
(10, 'Type2', '1970-02-14', '147 Oak St', 'David', 'Lopez', 852369741, 'Male', 'Lisa Lopez', 'Dr. King', 8523697410, 'Hospital PQR');


-- Insert data into the REPORT table
INSERT INTO REPORT (Title, Description, SessionNumber, Diagnosis, TreatmentStage)
VALUES ('Medical Report 1', 'Detailed description of the medical report.', 1, 'Diagnosis details', 'Treatment in progress');

-- Insert data into the CAMERA table
INSERT INTO CAMERA (Brand, Model, ViewDescription)
VALUES ('BrandX', 'ModelY', 'Camera view description');

-- Insert data into the ATTENDANCE table
INSERT INTO ATTENDANCE (OwnerID, TimeofArrival, TimeofLeave, DateofTheDay, Absence, Excuse, TotalHoursWorking)
VALUES (1, '08:00:00', '17:00:00', '2023-01-10', 'No', 'No excuse', 8);
INSERT INTO ATTENDANCE (ReceptionID, TimeofArrival, TimeofLeave, DateofTheDay, Absence, Excuse, TotalHoursWorking)
VALUES (2, '08:00:00', '17:00:00', '2023-01-10', 'No', 'No excuse', 8);
-- Insert multiple rows into the ATTENDANCE table
INSERT INTO ATTENDANCE (DoctorID, TimeofArrival, TimeofLeave, DateofTheDay, Absence, Excuse, TotalHoursWorking)
VALUES 
  (3, '08:00:00', '17:00:00', '2023-01-10', 'No', 'No excuse', 8),
  (4, '09:30:00', '18:30:00', '2023-01-10', 'No', 'No excuse', 9),
  (5, '08:15:00', '16:45:00', '2023-01-10', 'No', 'No excuse', 8.5);

-- Insert data into the APPOINTMENTS table
INSERT INTO APPOINTMENTS (PatientID, DoctorID, AppointmentDate, AppointmentTime, ReferralDoctor, ClinicReferralDoctorID, Insurance, StateOfPatient)
VALUES 
  (1, 3, '2023-01-15', '10:00:00', 'Referral Dr. Smith', 3, 'Health Insurance Inc.', 'Scheduled'),
  (2, 5, '2023-01-16', '11:30:00', 'Referral Dr. Johnson', 3, 'MediCare Ltd.', 'Pending'),
  (3, 1, '2023-01-17', '14:45:00', 'Referral Dr. Brown', 3, 'CarePlus Insurance', 'Completed'),
  (4, 2, '2023-01-18', '09:15:00', 'Referral Dr. White', 3, 'Wellness Assurance', 'Scheduled'),
  (5, 4, '2023-01-19', '13:00:00', 'Referral Dr. Miller', 3, 'HealthGuard', 'Pending'),
  (1, 2, '2023-01-20', '16:30:00', 'Referral Dr. Taylor', 3, 'MediLife Insurance', 'Scheduled'),
  (2, 1, '2023-01-21', '08:45:00', 'Referral Dr. Anderson', 3, 'Sunshine Health', 'Pending'),
  (3, 3, '2023-01-22', '12:15:00', 'Referral Dr. Martin', 3, 'WellCare', 'Scheduled'),
  (5, 4, '2023-01-23', '14:00:00', 'Referral Dr. Harris', 3, 'Blue Cross Blue Shield', 'Completed'),
  (4, 5, '2023-01-24', '10:45:00', 'Referral Dr. Turner', 1, 'United HealthCare', 'Scheduled'),
  (1, 1, '2023-01-25', '11:30:00', 'Referral Dr. Hall', 3, 'Cigna', 'Pending'),
  (2, 3, '2023-01-26', '13:15:00', 'Referral Dr. Adams', 3, 'Humana', 'Scheduled'),
  (3, 2, '2023-01-27', '15:00:00', 'Referral Dr. Clark', 2, 'Aetna', 'Pending'),
  (4, 4, '2023-01-28', '08:30:00', 'Referral Dr. Turner', 3, 'Kaiser Permanente', 'Scheduled'),
  (5, 5, '2023-01-29', '10:15:00', 'Referral Dr. Lewis', 3, 'Molina Healthcare', 'Pending'),
  (1, 1, '2023-01-30', '12:00:00', 'Referral Dr. King', 2, 'Centene Corporation', 'Scheduled'),
  (2, 3, '2023-01-31', '14:30:00', 'Referral Dr. Wright', 3, 'Highmark Health', 'Pending'),
  (3, 2, '2023-02-01', '09:45:00', 'Referral Dr. Robinson', 1, 'Independence Blue Cross', 'Scheduled'),
  (4, 4, '2023-02-02', '11:00:00', 'Referral Dr. Walker', 1, 'MVP Health Care', 'Pending'),
  (5, 5, '2023-02-03', '13:45:00', 'Referral Dr. Morris', 3, 'Oscar Health', 'Scheduled');

-- Insert data into the ASSESSMENT table
INSERT INTO ASSESSMENT (PatientID, DoctorID, ReceptionID, PatientComplaint, BodyPart, InjuryType, TreatmentPlan)
VALUES (1, 3, 2, 'Back pain', 'Back', 'Chronic', 'Physical therapy and medication');

-- Insert data into the MEDICAL_HISTORY table
INSERT INTO MEDICAL_HISTORY (PatientID, Surgeries, Illness, FamilyMedicalBackground)
VALUES (1, 'Appendectomy', 'None', 'Heart disease');

-- Insert data into the PATIENT_DATA table
INSERT INTO PATIENT_DATA (PatientID, AssessmentID, MedicalHistoryID, ReportID)
VALUES (1, 1, 1, 1);

