CREATE TABLE [dbo].[tbl_Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[PumpID] [int] NULL,
	[FullName] [nvarchar](250) NULL,
	[Email] [nvarchar](350) NULL,
	[Phone] [nvarchar](350) NULL,
	[Designation] [nvarchar](350) NULL,
 CONSTRAINT [PK_tbl_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_EmployeeSalary]    Script Date: 1/24/2021 4:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_EmployeeSalary](
	[SalaryID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsPaid] [int] NULL,
 CONSTRAINT [PK_tbl_EmployeeSalary] PRIMARY KEY CLUSTERED 
(
	[SalaryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Employee_tbl_Pump] FOREIGN KEY([PumpID])
REFERENCES [dbo].[tbl_Pump] ([PumpID])
GO
ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [FK_tbl_Employee_tbl_Pump]
GO
ALTER TABLE [dbo].[tbl_EmployeeSalary]  WITH CHECK ADD  CONSTRAINT [FK_tbl_EmployeeSalary_tbl_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[tbl_Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[tbl_EmployeeSalary] CHECK CONSTRAINT [FK_tbl_EmployeeSalary_tbl_Employee]
GO
