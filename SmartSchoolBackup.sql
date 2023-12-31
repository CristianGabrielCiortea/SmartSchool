USE [master]
GO
/****** Object:  Database [SmartSchool]    Script Date: 5/22/2023 11:09:45 PM ******/
CREATE DATABASE [SmartSchool]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartSchool', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SmartSchool.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartSchool_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SmartSchool_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SmartSchool] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartSchool].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartSchool] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartSchool] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartSchool] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartSchool] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartSchool] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartSchool] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SmartSchool] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartSchool] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartSchool] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartSchool] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartSchool] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartSchool] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartSchool] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartSchool] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartSchool] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SmartSchool] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartSchool] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartSchool] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartSchool] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartSchool] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartSchool] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartSchool] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartSchool] SET RECOVERY FULL 
GO
ALTER DATABASE [SmartSchool] SET  MULTI_USER 
GO
ALTER DATABASE [SmartSchool] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartSchool] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartSchool] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartSchool] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SmartSchool] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SmartSchool] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SmartSchool] SET QUERY_STORE = OFF
GO
USE [SmartSchool]
GO
/****** Object:  Table [dbo].[Absences]    Script Date: 5/22/2023 11:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Absences](
	[AbsenceId] [int] IDENTITY(1,1) NOT NULL,
	[StudentSubjectId] [int] NULL,
	[Date] [date] NOT NULL,
	[IsJustified] [bit] NOT NULL,
	[Semester] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AbsenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[SpecializationId] [int] NULL,
	[FormTeacherId] [int] NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[GradeId] [int] IDENTITY(1,1) NOT NULL,
	[StudentSubjectId] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[IsSemesterExamGrade] [bit] NOT NULL,
	[Semester] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specializations]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specializations](
	[SpecializationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpecializationSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecializationSubject](
	[SpecializationSubjectId] [int] IDENTITY(1,1) NOT NULL,
	[SpecializationId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[HasSemesterExam] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SpecializationSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClass](
	[StudentClassId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[ClassId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubject](
	[StudentSubjectId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[SubjectId] [int] NULL,
	[IsFirstSemesterEnded] [bit] NOT NULL,
	[IsSecondSemesterEnded] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherClass](
	[TeacherClassId] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NULL,
	[ClassId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TeacherClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherSubject](
	[TeacherSubjectId] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NULL,
	[SubjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TeacherSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeachingMaterials]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeachingMaterials](
	[TeachingMaterialId] [int] IDENTITY(1,1) NOT NULL,
	[TeacherClassId] [int] NULL,
	[SubjectId] [int] NULL,
	[Text] [varchar](4000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TeachingMaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [uc_username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Absences]  WITH CHECK ADD FOREIGN KEY([StudentSubjectId])
REFERENCES [dbo].[StudentSubject] ([StudentSubjectId])
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD FOREIGN KEY([FormTeacherId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specializations] ([SpecializationId])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD FOREIGN KEY([StudentSubjectId])
REFERENCES [dbo].[StudentSubject] ([StudentSubjectId])
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[Classes] ([ClassId])
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([SubjectId])
GO
ALTER TABLE [dbo].[TeacherClass]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[Classes] ([ClassId])
GO
ALTER TABLE [dbo].[TeacherClass]  WITH CHECK ADD FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[TeacherSubject]  WITH CHECK ADD FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([SubjectId])
GO
ALTER TABLE [dbo].[TeacherSubject]  WITH CHECK ADD FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[TeachingMaterials]  WITH CHECK ADD FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([SubjectId])
GO
ALTER TABLE [dbo].[TeachingMaterials]  WITH CHECK ADD FOREIGN KEY([TeacherClassId])
REFERENCES [dbo].[TeacherClass] ([TeacherClassId])
GO
/****** Object:  StoredProcedure [dbo].[AddAbsence]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAbsence]
	@studentSubjectId int,
	@date date,
	@isJustified bit,
	@semester int
AS
BEGIN
	insert into Absences([StudentSubjectID],[Date],[IsJustified],[Semester]) 
	values(@studentSubjectId, @date, @isJustified, @semester)	
END
GO
/****** Object:  StoredProcedure [dbo].[AddClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddClass] (
    @formTeacherId INT,
	@specializationId INT,
    @name VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Classes (FormTeacherId, SpecializationId, Name)
    VALUES (@formTeacherId, @specializationId, @name)
END
GO
/****** Object:  StoredProcedure [dbo].[AddGrade]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddGrade]
	@studentSubjectID int,
	@value int,
	@date date,
	@isSemesterExamGrade bit,
	@semester int
AS
BEGIN
	insert into Grades([StudentSubjectId],[Value], [Date], [IsSemesterExamGrade],[Semester]) 
	values(@studentSubjectID, @value, @date, @isSemesterExamGrade, @semester)	
END
GO
/****** Object:  StoredProcedure [dbo].[AddSpecialization]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSpecialization]
(
    @name VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Specializations (Name)
    VALUES (@name);
END;
GO
/****** Object:  StoredProcedure [dbo].[AddSpecializationSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSpecializationSubject]
	@subjectId int,
	@specializationId int,
	@hasSemesterExam bit
AS
BEGIN
	insert into SpecializationSubject([SubjectId],[SpecializationId],[HasSemesterExam]) 
	values(@subjectId, @specializationId, @hasSemesterExam)	
END
GO
/****** Object:  StoredProcedure [dbo].[AddStudentClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStudentClass]
	@studentID int,
	@classID int
AS
BEGIN
	insert into StudentClass([StudentID],[ClassID]) values(@studentID, @classID)
END
GO
/****** Object:  StoredProcedure [dbo].[AddStudentSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStudentSubject]
	@studentId int,
	@subjectId int,
	@isFirstSemesterEnded bit,
	@isSecondSemesterEnded bit
AS
BEGIN
	insert into StudentSubject([StudentId],[SubjectId],[IsFirstSemesterEnded],[IsSecondSemesterEnded]) 
	values(@studentId, @subjectId, @isFirstSemesterEnded, @isSecondSemesterEnded)
END
GO
/****** Object:  StoredProcedure [dbo].[AddSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSubject]
	@name varchar(50)
AS
BEGIN
	insert into Subjects([Name]) values(@name)
END
GO
/****** Object:  StoredProcedure [dbo].[AddTeacherClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTeacherClass]
	@teacherID int,
	@classID int
AS
BEGIN
	insert into TeacherClass([TeacherID],[ClassID]) values(@teacherID, @classID)
END
GO
/****** Object:  StoredProcedure [dbo].[AddTeacherSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTeacherSubject]
	@teacherId int,
	@subjectId int
AS
BEGIN
	insert into TeacherSubject([TeacherId],[SubjectId]) values(@teacherId, @subjectId)
END
GO
/****** Object:  StoredProcedure [dbo].[AddTeachingMaterial]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTeachingMaterial]
    @teacherClassId INT,
    @subjectId INT,
    @text VARCHAR(4000)
AS
BEGIN
    INSERT INTO TeachingMaterials (TeacherClassId, SubjectId, Text)
    VALUES (@teacherClassId, @subjectId, @text)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
	@username varchar(50),
	@password varchar(50),
	@lastName varchar(50),
	@firstName varchar(50),
	@role int
AS
BEGIN
	insert into [Users]([Username],[Password],[LastName],[FirstName],[Role]) values(@username, @password, @lastName, @firstName, @role)	
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAbsence]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAbsence]
	@id int
AS
BEGIN
	delete from Absentee
	where AbsenteeID = @id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteClass] (
    @classId INT
)
AS
BEGIN
    DELETE FROM Classes
    WHERE ClassId = @classId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteGrade]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteGrade]
	@gradeId int
AS
BEGIN
	delete from Grades
	where GradeId = @gradeId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSpecialization]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSpecialization]
(
    @specializationId INT
)
AS
BEGIN
    DELETE FROM Specializations WHERE SpecializationId = @specializationId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteSpecializationSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSpecializationSubject]
	@specializationId int,
	@subjectId int
AS
BEGIN
	delete from SpecializationSubject
	where SpecializationId = @specializationId and SubjectId = @subjectId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudentClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStudentClass]
	@classId int,
	@studentId int
AS
BEGIN
	delete from StudentClass
	where StudentId = @studentId AND ClassId = @classId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudentSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStudentSubject]
	@studentId int,
	@subjectId int
AS
BEGIN
	delete from StudentSubject
	where SubjectId = @subjectId and StudentId = @studentId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSubject]
	@subjectId int
AS
BEGIN
	delete from Subjects
	where SubjectId = @subjectId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTeacherClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTeacherClass]
	@classId int,
	@teacherId int
AS
BEGIN
	delete from TeacherClass
	where ClassId = @classid and TeacherId = @teacherId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTeacherSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTeacherSubject]
	@teacherId int,
	@subjectId int
AS
BEGIN
	delete from TeacherSubject
	where TeacherId = @teacherId and SubjectId = @subjectId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTeachingMaterial]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTeachingMaterial]
    @teachingMaterialId INT
AS
BEGIN
    DELETE FROM TeachingMaterials
    WHERE TeachingMaterialId = @teachingMaterialId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteUser]
    @id INT
AS
BEGIN
    DELETE FROM users
    WHERE UserId = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllAbsences]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllAbsences]
AS
BEGIN
	select AbsenceId, [StudentSubjectID], [Date], [IsJustified], [Semester] from Absences
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllClasses]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllClasses]
AS
BEGIN
	select ClassID, [SpecializationID], [FormTeacherID], [Name] from Classes
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllGrades]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllGrades]
AS
BEGIN
	select GradeId, [StudentSubjectId], [Value], [Date], [IsSemesterExamGrade], [Semester] from Grades
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSpecializations]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllSpecializations]
AS
BEGIN
	select SpecializationId, [Name] from Specializations
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSpecializationSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllSpecializationSubject]
AS
BEGIN
	select SpecializationSubjectID, [SubjectId] ,[SpecializationId], [HasSemesterExam] from SpecializationSubject
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllStudentClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllStudentClass]
AS
BEGIN
	select StudentClassId, [StudentID] , [ClassID] from StudentClass
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllStudentSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllStudentSubject]
AS
BEGIN
	select StudentSubjectId, [StudentId] , [SubjectId], [IsFirstSemesterEnded], [IsSecondSemesterEnded]  from StudentSubject
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSubjects]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllSubjects]
AS
BEGIN
	select SubjectId, [Name] from Subjects
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllTeacherClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllTeacherClass]
AS
BEGIN
	select TeacherClassId, [TeacherID], [ClassID] from TeacherClass
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllTeacherSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllTeacherSubject]
AS
BEGIN
	select TeacherSubjectId, [TeacherId], [SubjectId] from TeacherSubject
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllTeachingMaterials]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllTeachingMaterials]
AS
BEGIN
    SELECT TeachingMaterialId, TeacherClassId, SubjectId, Text
    FROM TeachingMaterials
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SELECT * FROM users;
END;
GO
/****** Object:  StoredProcedure [dbo].[ModifyAbsence]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyAbsence]
	@absenceId int,
	@isJustified bit
AS
BEGIN
	update	Absences
	set		[IsJustified] = @isJustified
	where	AbsenceId = @absenceId
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyClass]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyClass]
	@classID int,
	@specializationId int,
	@formTeacherID int,
	@name varchar(50)
AS
BEGIN
	update	Classes
	set		[FormTeacherID] = @formTeacherID,
			[SpecializationId] = @specializationId,
			[Name] = @name
	where	ClassID = @classID
END
GO
/****** Object:  StoredProcedure [dbo].[ModifySpecialization]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifySpecialization]
(
    @specializationId INT,
    @name VARCHAR(50)
)
AS
BEGIN
    UPDATE Specializations
    SET Name = @name
    WHERE SpecializationId = @specializationId;
END;
GO
/****** Object:  StoredProcedure [dbo].[ModifyStudentSubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyStudentSubject]
	@studentId int,
	@subjectId int,
	@isFirstSemesterEnded bit,
	@isSecondSemesterEnded bit
AS
BEGIN
	update	StudentSubject
	set		[IsFirstSemesterEnded] = @isFirstSemesterEnded,
	        [IsSecondSemesterEnded] = @isSecondSemesterEnded
	where	StudentId = @studentId and SubjectId = @subjectId
END
GO
/****** Object:  StoredProcedure [dbo].[ModifySubject]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ModifySubject]
	@subjectId int,
	@name varchar(50)
AS
BEGIN
	update	Subjects
	set		[Name] = @name
	where	SubjectId = @subjectId
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyTeachingMaterial]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyTeachingMaterial]
    @teachingMaterialId INT,
    @teacherClassId INT,
    @subjectId INT,
    @text VARCHAR(4000)
AS
BEGIN
    UPDATE TeachingMaterials
    SET TeacherClassId = @teacherClassId,
        SubjectId = @subjectId,
        Text = @text
    WHERE TeachingMaterialId = @teachingMaterialId
END
GO
/****** Object:  StoredProcedure [dbo].[ModifyUser]    Script Date: 5/22/2023 11:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyUser]
	@userID int,
	@username varchar(50),
	@password varchar(50),
	@lastName varchar(50),
	@firstName varchar(50),
	@role int
AS
BEGIN
	update	[Users]
	set		[Username] = @username, 
			[Password] = @password,
			[LastName] = @lastName,
			[FirstName] = @firstName,
			[Role] = @role
	where	UserID = @userID
END
GO
USE [master]
GO
ALTER DATABASE [SmartSchool] SET  READ_WRITE 
GO
