IF NOT EXISTS (SELECT *
FROM sys.databases
WHERE name = 'Online_Courses')
BEGIN
    CREATE DATABASE Online_Courses;
END;
GO

USE Online_Courses;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [first_name] [varchar](200) NOT NULL,
    [last_name] [varchar](200) NOT NULL,
    [birth_date] [date] NOT NULL,
    [gender] [bit] NOT NULL,
    [user_name] [varchar](200) NOT NULL,
    [password_hash] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_user_name] ON [dbo].[User]
(
	[user_name] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 --> male       0 --> female' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'gender'
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_name] [varchar](200) NOT NULL,
	[description] [varchar](max) NOT NULL,
	[start_date] [date] NOT NULL,
	[end_date] [date] NOT NULL,
	[instructor_id] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Instructor_Id] FOREIGN KEY([instructor_id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Instructor_Id]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrolled](
	[student_id] [int] NOT NULL,
	[instructor_id] [int] NOT NULL,
	[course_id] [int] NOT NULL,
	[grade] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Enrolled]  WITH CHECK ADD  CONSTRAINT [FK_Enrolled_course_id] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([id])
GO
ALTER TABLE [dbo].[Enrolled] CHECK CONSTRAINT [FK_Enrolled_course_id]
GO
ALTER TABLE [dbo].[Enrolled]  WITH CHECK ADD  CONSTRAINT [FK_Enrolled_instructor_id] FOREIGN KEY([instructor_id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Enrolled] CHECK CONSTRAINT [FK_Enrolled_instructor_id]
GO
ALTER TABLE [dbo].[Enrolled]  WITH CHECK ADD  CONSTRAINT [FK_Enrolled_student_id] FOREIGN KEY([student_id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Enrolled] CHECK CONSTRAINT [FK_Enrolled_student_id]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[user_id] [int] NOT NULL,
	[role] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_user_id]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [CK_Role] CHECK  (([role]='Student' OR [role]='Instructor' OR [role]='Admin'))
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [CK_Role]
GO
