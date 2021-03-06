USE [thefuturenet]
GO
/****** Object:  Table [dbo].[accrualMethod]    Script Date: 3/24/2016 7:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[accrualMethod](
	[accrual_method_id] [int] IDENTITY(1,1) NOT NULL,
	[accrual_method_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_accrualMethod] PRIMARY KEY CLUSTERED 
(
	[accrual_method_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[advanceFee]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[advanceFee](
	[advance_fee_id] [int] IDENTITY(1,1) NOT NULL,
	[advance_fee_amount] [decimal](15, 2) NULL,
	[advance_fee_calculate_type] [varchar](10) NULL,
	[receipt] [bit] NOT NULL CONSTRAINT [DF_advanceFee_receipt]  DEFAULT ((0)),
	[payment_due_method] [varchar](15) NULL,
	[payment_due_date] [varchar](3) NULL,
	[auto_remind_dealer_email] [varchar](100) NULL,
	[delaer_remind_period] [int] NULL,
	[due_auto_remind_email] [varchar](100) NULL,
	[due_auto_remind_period] [int] NULL,
	[loan_id] [int] NOT NULL,
 CONSTRAINT [PK_advanceFee] PRIMARY KEY CLUSTERED 
(
	[advance_fee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[atvModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[atvModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[boatModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[boatModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[branch]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[branch](
	[branch_id] [int] IDENTITY(1,1) NOT NULL,
	[branch_code] [varchar](50) NOT NULL,
	[branch_name] [varchar](50) NOT NULL,
	[branch_address_1] [varchar](50) NOT NULL,
	[branch_address_2] [varchar](50) NULL,
	[state_id] [int] NOT NULL,
	[city] [varchar](50) NOT NULL,
	[zip] [varchar](50) NOT NULL,
	[email] [varchar](100) NULL,
	[phone_num_1] [varchar](15) NOT NULL,
	[phone_num_2] [varchar](15) NULL,
	[phone_num_3] [varchar](15) NULL,
	[fax] [varchar](15) NULL,
	[created_by] [int] NULL,
	[created_date] [datetime] NOT NULL,
	[company_id] [int] NULL,
 CONSTRAINT [PK_branch] PRIMARY KEY CLUSTERED 
(
	[branch_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[camperModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[camperModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[company]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[company](
	[company_id] [int] IDENTITY(1,1) NOT NULL,
	[company_name] [varchar](50) NOT NULL,
	[company_code] [varchar](50) NOT NULL,
	[company_address_1] [varchar](50) NOT NULL,
	[company_address_2] [varchar](50) NULL,
	[city] [varchar](50) NOT NULL,
	[stateId] [int] NOT NULL,
	[zip] [varchar](50) NOT NULL,
	[email] [varchar](100) NULL,
	[phone_num_1] [varchar](15) NOT NULL,
	[phone_num_2] [varchar](15) NULL,
	[phone_num_3] [varchar](15) NULL,
	[fax] [varchar](15) NULL,
	[website_url] [varchar](max) NULL,
	[created_by] [int] NULL,
	[created_date] [datetime] NULL,
	[company_type] [int] NOT NULL,
	[first_super_admin_id] [int] NULL,
	[company_status] [bit] NULL,
	[flag_value] [bit] NULL,
 CONSTRAINT [PK_company] PRIMARY KEY CLUSTERED 
(
	[company_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[companySetupStep]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[companySetupStep](
	[company_id] [int] NOT NULL,
	[branch_id] [int] NULL,
	[step_number] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[companyType]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[companyType](
	[company_type_id] [int] IDENTITY(1,1) NOT NULL,
	[company_type_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_companyType] PRIMARY KEY CLUSTERED 
(
	[company_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[curtailment]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[curtailment](
	[curtailment_id] [int] IDENTITY(1,1) NOT NULL,
	[time_period] [varchar](50) NOT NULL,
	[percentage] [int] NOT NULL,
	[loan_id] [int] NOT NULL,
	[payment_percentage] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_curtailment] PRIMARY KEY CLUSTERED 
(
	[curtailment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[curtailmentBackup]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[curtailmentBackup](
	[loan_id] [int] NOT NULL,
	[unit_id] [varchar](50) NOT NULL,
	[curt_number] [int] NOT NULL,
	[curt_amount] [decimal](18, 2) NOT NULL,
	[curt_due_date] [datetime] NOT NULL,
	[curt_payment_details] [varchar](100) NULL,
	[pay_date] [datetime] NOT NULL,
 CONSTRAINT [PK_curtailmentBackup] PRIMARY KEY CLUSTERED 
(
	[loan_id] ASC,
	[unit_id] ASC,
	[curt_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[curtailmentSchedule]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[curtailmentSchedule](
	[loan_id] [int] NOT NULL,
	[unit_id] [varchar](50) NOT NULL,
	[curt_number] [int] NOT NULL,
	[curt_amount] [decimal](18, 2) NOT NULL,
	[curt_due_date] [datetime] NOT NULL,
	[curt_status] [int] NOT NULL,
	[paid_date] [datetime] NULL,
 CONSTRAINT [PK_curtailmentSchedule] PRIMARY KEY CLUSTERED 
(
	[loan_id] ASC,
	[unit_id] ASC,
	[curt_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[forgotPasswordToken]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[forgotPasswordToken](
	[token_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[token] [nchar](100) NOT NULL,
	[expired_date] [datetime] NOT NULL,
 CONSTRAINT [PK_forgotPasswordToken] PRIMARY KEY CLUSTERED 
(
	[token_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[heavyEquipmentModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[heavyEquipmentModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[interest]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[interest](
	[interest_id] [int] IDENTITY(1,1) NOT NULL,
	[interest_rate] [decimal](5, 3) NULL,
	[paid_date] [varchar](3) NULL,
	[payment_period] [varchar](50) NOT NULL,
	[auto_remind_email] [varchar](100) NULL,
	[auto_remind_period] [int] NULL,
	[loan_id] [int] NOT NULL,
	[accrual_method_id] [int] NOT NULL,
 CONSTRAINT [PK_interest] PRIMARY KEY CLUSTERED 
(
	[interest_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[justAddedUnit]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[justAddedUnit](
	[just_added_unit_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[model] [varchar](50) NULL,
	[advance_amount] [decimal](12, 2) NULL,
	[is_advanced] [bit] NULL,
	[loan_id] [int] NOT NULL,
	[unit_id] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[loan]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[loan](
	[loan_id] [int] IDENTITY(1,1) NOT NULL,
	[loan_number] [varchar](100) NOT NULL,
	[loan_code] [varchar](100) NOT NULL,
	[loan_amount] [decimal](18, 2) NULL,
	[start_date] [datetime] NOT NULL,
	[maturity_date] [datetime] NOT NULL,
	[advance] [int] NULL,
	[is_edit_allowable] [bit] NOT NULL CONSTRAINT [DF_loan_is_edit_allowable_for_advanced_petcentage]  DEFAULT ((0)),
	[payment_method] [varchar](50) NOT NULL,
	[pay_off_period] [int] NOT NULL,
	[pay_off_type] [char](1) NOT NULL,
	[is_interest_calculate] [bit] NOT NULL,
	[default_unit_type] [int] NULL,
	[loan_status] [bit] NOT NULL,
	[created_date] [datetime] NULL,
	[modified_date] [datetime] NULL,
	[is_delete] [bit] NOT NULL,
	[has_advance_fee] [bit] NULL,
	[has_monthly_loan_fee] [bit] NULL,
	[has_lot_inspection_fee] [bit] NULL,
	[auto_remind_email] [varchar](100) NULL,
	[auto_remind_period] [int] NULL,
	[non_reg_branch_id] [int] NOT NULL,
	[curtailment_due_date] [varchar](3) NULL,
	[curtailment_auto_remind_email] [varchar](100) NULL,
	[curtailment_remind_period] [int] NULL,
	[curtailment_calculation_type] [char](1) NULL,
 CONSTRAINT [PK_loan] PRIMARY KEY CLUSTERED 
(
	[loan_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[loanDetails]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loanDetails](
	[loan_details_id] [int] IDENTITY(1,1) NOT NULL,
	[used_amount] [decimal](18, 2) NULL,
	[pending_amount] [decimal](18, 2) NULL,
	[loan_id] [int] NOT NULL,
 CONSTRAINT [PK_loanDetails] PRIMARY KEY CLUSTERED 
(
	[loan_details_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[loanSetupStep]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loanSetupStep](
	[company_id] [int] NOT NULL,
	[branch_id] [int] NOT NULL,
	[non_registered_branch_id] [int] NOT NULL,
	[loan_id] [int] NULL,
	[step_number] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[loanUnitType]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loanUnitType](
	[loan_id] [int] NOT NULL,
	[unit_type_id] [int] NOT NULL,
 CONSTRAINT [PK_loanUnitType] PRIMARY KEY CLUSTERED 
(
	[loan_id] ASC,
	[unit_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[lotInspectionFee]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[lotInspectionFee](
	[lot_inspection_fee_id] [int] IDENTITY(1,1) NOT NULL,
	[lot_inspection_amount] [decimal](15, 2) NULL,
	[receipt] [bit] NOT NULL CONSTRAINT [DF_lotInspectionFee_receipt]  DEFAULT ((0)),
	[payment_due_method] [varchar](15) NULL,
	[payment_due_date] [varchar](3) NULL,
	[auto_remind_dealer_email] [varchar](100) NULL,
	[delaer_remind_period] [int] NULL,
	[due_auto_remind_email] [varchar](100) NULL,
	[due_auto_remind_period] [int] NULL,
	[loan_id] [int] NOT NULL,
 CONSTRAINT [PK_lotInspectionFee] PRIMARY KEY CLUSTERED 
(
	[lot_inspection_fee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[monthlyLoanFee]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[monthlyLoanFee](
	[montly_loan_fee_id] [int] IDENTITY(1,1) NOT NULL,
	[monthly_loan_fee_amount] [decimal](15, 2) NULL,
	[receipt] [bit] NOT NULL CONSTRAINT [DF_monthlyLoanFee_receipt]  DEFAULT ((0)),
	[payment_due_method] [varchar](15) NULL,
	[payment_due_date] [varchar](3) NULL,
	[auto_remind_dealer_email] [varchar](100) NULL,
	[delaer_remind_period] [int] NULL,
	[due_auto_remind_email] [varchar](100) NULL,
	[due_auto_remind_period] [int] NULL,
	[loan_id] [int] NOT NULL,
 CONSTRAINT [PK_monthlyLoanFee] PRIMARY KEY CLUSTERED 
(
	[montly_loan_fee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[motorcyclesModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[motorcyclesModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[nonRegisteredBranch]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[nonRegisteredBranch](
	[non_reg_branch_id] [int] IDENTITY(1,1) NOT NULL,
	[branch_code] [varchar](50) NOT NULL,
	[branch_name] [varchar](50) NOT NULL,
	[branch_address_1] [varchar](50) NOT NULL,
	[branch_address_2] [varchar](50) NULL,
	[state_id] [int] NOT NULL,
	[city] [varchar](50) NOT NULL,
	[zip] [varchar](50) NOT NULL,
	[email] [varchar](100) NULL,
	[phone_num_1] [varchar](15) NOT NULL,
	[phone_num_2] [varchar](15) NULL,
	[phone_num_3] [varchar](15) NULL,
	[fax] [varchar](15) NULL,
	[created_by] [int] NULL,
	[created_date] [datetime] NOT NULL,
	[company_id] [int] NOT NULL,
	[branch_id] [int] NOT NULL,
 CONSTRAINT [PK_non_registered_branch] PRIMARY KEY CLUSTERED 
(
	[non_reg_branch_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[nonRegisteredCompany]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[nonRegisteredCompany](
	[company_id] [int] IDENTITY(1,1) NOT NULL,
	[company_name] [varchar](50) NOT NULL,
	[company_code] [varchar](50) NOT NULL,
	[company_address_1] [varchar](50) NOT NULL,
	[company_address_2] [varchar](50) NULL,
	[city] [varchar](50) NOT NULL,
	[stateId] [int] NOT NULL,
	[zip] [varchar](50) NOT NULL,
	[email] [varchar](100) NULL,
	[phone_num_1] [varchar](15) NOT NULL,
	[phone_num_2] [nchar](15) NULL,
	[phone_num_3] [nchar](15) NULL,
	[fax] [varchar](15) NULL,
	[website_url] [varchar](150) NULL,
	[created_by] [int] NULL,
	[created_date] [datetime] NULL,
	[company_type] [int] NOT NULL,
	[reg_company_id] [int] NOT NULL,
 CONSTRAINT [PK_nonRegisteredCompany] PRIMARY KEY CLUSTERED 
(
	[company_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[partialCurtailment]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partialCurtailment](
	[loan_id] [int] NOT NULL,
	[unit_id] [varchar](50) NOT NULL,
	[curt_number] [int] NOT NULL,
	[curt_partial_amount] [decimal](18, 2) NOT NULL,
	[paid_date] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rvModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rvModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[snowmobileModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[snowmobileModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[states]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[states](
	[state_id] [int] IDENTITY(1,1) NOT NULL,
	[state_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_states] PRIMARY KEY CLUSTERED 
(
	[state_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[step]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[step](
	[user_id] [int] NULL,
	[loan_id] [int] NULL,
	[step_id] [int] NULL,
	[branch_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[systemAdmin]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[systemAdmin](
	[system_admin_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[level_id] [int] NULL,
 CONSTRAINT [PK_systemAdmin] PRIMARY KEY CLUSTERED 
(
	[system_admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[systemAdminLevel]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[systemAdminLevel](
	[level_id] [int] IDENTITY(1,1) NOT NULL,
	[level_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_userLevel] PRIMARY KEY CLUSTERED 
(
	[level_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[title]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[title](
	[title_id] [int] IDENTITY(1,1) NOT NULL,
	[is_title_tracked] [bit] NOT NULL,
	[title_accept_method] [varchar](50) NULL,
	[title_received_time_period] [varchar](50) NULL,
	[auto_remind_email] [varchar](100) NULL,
	[is_receipt_required] [bit] NOT NULL,
	[receipt_required_method] [varchar](50) NULL,
	[loan_id] [int] NOT NULL,
 CONSTRAINT [PK_title] PRIMARY KEY CLUSTERED 
(
	[title_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[unit]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[unit](
	[unit_id] [varchar](50) NOT NULL,
	[identification_number] [varchar](20) NOT NULL,
	[year] [int] NOT NULL,
	[make] [varchar](20) NOT NULL,
	[model] [varchar](50) NOT NULL,
	[trim] [varchar](50) NULL,
	[miles] [decimal](13, 3) NULL,
	[color] [varchar](20) NULL,
	[new_or_used] [bit] NULL,
	[length] [decimal](10, 2) NULL,
	[hitch_style] [varchar](20) NULL,
	[speed] [decimal](7, 2) NULL,
	[trailer_id] [varchar](50) NULL,
	[engine_serial] [varchar](50) NULL,
	[cost] [decimal](12, 2) NOT NULL,
	[advance_amount] [decimal](12, 2) NOT NULL,
	[note] [varchar](250) NULL,
	[advance_date] [datetime] NOT NULL,
	[add_or_advance] [bit] NOT NULL,
	[is_advanced] [bit] NULL,
	[is_approved] [bit] NULL,
	[status] [varchar](20) NULL,
	[unit_status] [tinyint] NULL,
	[created_date] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[modified_date] [datetime] NULL,
	[modified_by] [int] NULL,
	[loan_id] [int] NULL,
	[unit_type_id] [int] NULL,
	[title_status] [tinyint] NULL,
 CONSTRAINT [PK_unit] PRIMARY KEY CLUSTERED 
(
	[unit_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[unitType]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[unitType](
	[unit_type_id] [int] IDENTITY(1,1) NOT NULL,
	[unit_type_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_unitType] PRIMARY KEY CLUSTERED 
(
	[unit_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[uploadTitle]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[uploadTitle](
	[upload_id] [int] IDENTITY(1,1) NOT NULL,
	[file_path] [varchar](100) NOT NULL,
	[unit_id] [varchar](50) NOT NULL,
	[original_file_name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_uploadTitle] PRIMARY KEY CLUSTERED 
(
	[upload_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[phone_no] [varchar](15) NULL,
	[status] [bit] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[modified_date] [datetime] NULL,
	[is_delete] [bit] NULL,
	[created_by] [int] NULL,
	[branch_id] [int] NULL,
	[role_id] [int] NOT NULL,
	[company_id] [int] NULL,
 CONSTRAINT [UK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[userActivation]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userActivation](
	[activation_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[activation_code] [uniqueidentifier] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_user_activation] PRIMARY KEY CLUSTERED 
(
	[activation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[userPermission]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[userPermission](
	[user_id] [int] NOT NULL,
	[right_id] [varchar](100) NULL,
	[modified_by] [int] NULL,
	[modify_date] [datetime] NULL,
 CONSTRAINT [PK_userPermission] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[userRights]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[userRights](
	[right_id] [varchar](10) NOT NULL,
	[description] [varchar](200) NULL,
 CONSTRAINT [PK_userRights] PRIMARY KEY CLUSTERED 
(
	[right_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[userRole]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[userRole](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_userRole] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vehicleModelYear]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vehicleModelYear](
	[year] [int] NULL,
	[make] [varchar](100) NULL,
	[model] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[branchCount]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[branchCount] AS

SELECT     branch.company_id ,COUNT(branch.branch_id) BranchCount
FROM         branch LEFT OUTER JOIN
                      company ON branch.company_id = company.company_id
where company.company_status='1'
GROUP BY branch.company_id
--ORDER BY company_id
                      

                                           
                                           
                      
                      
                      
                      
                      

GO
/****** Object:  View [dbo].[viewBranchNonRegBranch]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewBranchNonRegBranch]
AS
SELECT        B.branch_id, NRB.non_reg_branch_id, NRB.branch_code AS nr_branch_code, B.branch_code AS r_branch_code, C.company_code
FROM            dbo.branch AS B INNER JOIN
                         dbo.nonRegisteredBranch AS NRB ON B.branch_id = NRB.branch_id INNER JOIN
                         dbo.company AS C ON B.company_id = C.company_id

GO
/****** Object:  View [dbo].[viewLoanCurtailmentDetails]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE VIEW [dbo].[viewLoanCurtailmentDetails]
AS
SELECT         dbo.loan.loan_id,dbo.loan.pay_off_period, dbo.loan.pay_off_type, dbo.curtailment.time_period, dbo.curtailment.percentage, dbo.curtailment.curtailment_id,dbo.loan.curtailment_calculation_type,loan.advance
FROM            dbo.loan INNER JOIN
                         dbo.curtailment ON dbo.loan.loan_id = dbo.curtailment.loan_id









GO
/****** Object:  View [dbo].[viewLoanPaymentDetails]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewLoanPaymentDetails]
AS
SELECT        dbo.loan.loan_id, dbo.loan.loan_amount, dbo.loanDetails.used_amount, dbo.loanDetails.pending_amount
FROM            dbo.loanDetails INNER JOIN
                         dbo.loan ON dbo.loanDetails.loan_id = dbo.loan.loan_id


GO
/****** Object:  View [dbo].[viewUserDetails]    Script Date: 3/24/2016 7:01:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewUserDetails]
AS

SELECT     [user].user_id, [user].first_name, [user].last_name, [user].email, [user].phone_no, [user].status, [user].created_date, [user].modified_date, [user].is_delete, 
                      [user].created_by, [user].branch_id, [user].role_id, [user].user_name, [user].password, branch.branch_name, company.company_id, company.company_name
FROM         [user] INNER JOIN
                      company ON [user].company_id = company.company_id LEFT OUTER JOIN
                      branch ON [user].branch_id = branch.branch_id
                      
                      
--SELECT        dbo.[user].user_id, dbo.[user].first_name, dbo.[user].last_name, dbo.[user].email, dbo.[user].phone_no, dbo.[user].status, dbo.[user].created_date, dbo.[user].modified_date, dbo.[user].is_delete, 
--                         dbo.[user].created_by, dbo.[user].branch_id, dbo.[user].role_id, dbo.[user].user_name, dbo.[user].password, dbo.branch.branch_name, dbo.company.company_id, dbo.company.company_name
--FROM            dbo.[user] INNER JOIN
--                         dbo.branch ON dbo.[user].branch_id = dbo.branch.branch_id INNER JOIN
--                         dbo.company ON dbo.branch.company_id = dbo.company.company_id
GO
SET IDENTITY_INSERT [dbo].[accrualMethod] ON 

INSERT [dbo].[accrualMethod] ([accrual_method_id], [accrual_method_name]) VALUES (1, N'method1')
INSERT [dbo].[accrualMethod] ([accrual_method_id], [accrual_method_name]) VALUES (2, N'method2')
INSERT [dbo].[accrualMethod] ([accrual_method_id], [accrual_method_name]) VALUES (3, N'method3')
INSERT [dbo].[accrualMethod] ([accrual_method_id], [accrual_method_name]) VALUES (4, N'method4')
SET IDENTITY_INSERT [dbo].[accrualMethod] OFF
SET IDENTITY_INSERT [dbo].[advanceFee] ON 

INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (50, CAST(75.00 AS Decimal(15, 2)), NULL, 1, N'Once a Month', N'7', N'email@gmail.comm', 0, N'email@gmailcom.m', -10, 179)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (51, CAST(11111.00 AS Decimal(15, 2)), NULL, 0, N'Time of Advance', N'TOA', N'', 0, N'', 0, 182)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (53, CAST(10000.00 AS Decimal(15, 2)), N'Month', 1, N'Once a Month', N'2', N'', 0, N'', 0, 189)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (54, CAST(34534.00 AS Decimal(15, 2)), N'Month', 1, N'Time of Advance', N'ToA', N'', 0, N'', 0, 193)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (55, CAST(3544.00 AS Decimal(15, 2)), N'Month', 1, N'Time of Advance', N'ToA', N'', 0, N'', 0, 198)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (56, CAST(4522.00 AS Decimal(15, 2)), N'Month', 1, N'Once a Month', N'6', N'asa@gmail.com', 7, N'sdsas@gmail.com', 964, 204)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (57, CAST(225.00 AS Decimal(15, 2)), N'Month', 0, N'Once a Month', N'16', N'', 0, N'', 0, 218)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (59, CAST(50.00 AS Decimal(15, 2)), N'Month', 0, N'Once a Month', N'10', N'', 0, N'', 0, 220)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (60, CAST(568.00 AS Decimal(15, 2)), N'Month', 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 227)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (61, CAST(5747.00 AS Decimal(15, 2)), N'Month', 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 228)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (62, CAST(6767.00 AS Decimal(15, 2)), N'Month', 0, N'Once a Month', N'6', N'kasun2030@gmail.com', 6, N'kasun2030@gmail.com', 6, 205)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (63, CAST(50.00 AS Decimal(15, 2)), N'Month', 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 229)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (64, CAST(76767.00 AS Decimal(15, 2)), N'PayPeriod', 0, N'Once a Month', N'18', N'fhngh@ghgh.com', 43, N'', 0, 233)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (65, CAST(321.00 AS Decimal(15, 2)), N'Month', 0, N'Vehicle Payoff', N'VP', N'', 0, N'', 0, 235)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (66, CAST(50.00 AS Decimal(15, 2)), N'Month', 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 237)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (67, CAST(50.00 AS Decimal(15, 2)), N'PayPeriod', 0, N'Once a Month', N'16', N'', 0, N'', 0, 238)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (68, CAST(123.00 AS Decimal(15, 2)), N'Month', 0, N'Time of Advance', N'ToA', N'', 0, N'abcde@gghgh.com', 12, 239)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (69, CAST(232132.00 AS Decimal(15, 2)), N'Month', 0, N'Once a Month', N'17', N'qwerw@weqw.gfhg', 123, N'asda@weqr.hjh', 123, 244)
INSERT [dbo].[advanceFee] ([advance_fee_id], [advance_fee_amount], [advance_fee_calculate_type], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (70, CAST(556.00 AS Decimal(15, 2)), N'Month', 0, N'Time of Advance', N'ToA', N'', 0, N'abcd@gmail.com', 234, 247)
SET IDENTITY_INSERT [dbo].[advanceFee] OFF
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2001, N'Can Am', N'Outlander')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2001, N'Polaris', N'Sportsman')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2001, N'Arctic Cat', N'Thunder Cat')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2002, N'Can Am', N'Outlander')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2002, N'Polaris', N'Sportsman')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2001, N'Can Am', N'Outlander')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2001, N'Polaris', N'Sportsman')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2001, N'Arctic Cat', N'Thunder Cat')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2002, N'Can Am', N'Outlander')
INSERT [dbo].[atvModelYear] ([year], [make], [model]) VALUES (2002, N'Polaris', N'Sportsman')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2001, N'Poseidon', N'B Series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2001, N'Poseidon', N'C Series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2002, N'Mercury', N'A Series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2002, N'Mercury', N'C series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2002, N'Poseidon', N'A Series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2001, N'Poseidon', N'B Series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2001, N'Poseidon', N'C Series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2002, N'Mercury', N'A Series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2002, N'Mercury', N'C series')
INSERT [dbo].[boatModelYear] ([year], [make], [model]) VALUES (2002, N'Poseidon', N'A Series')
SET IDENTITY_INSERT [dbo].[branch] ON 

INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (8, N'code', N'clb', N'vlb', N'clb', 12, N'cfdgh', N'11111', NULL, N'1111111111', NULL, NULL, NULL, 5, CAST(N'2016-02-05 00:00:00.000' AS DateTime), 12)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (12, N'code2', N'kdy', N'sads', N'kandy', 13, N'vbfdh', N'22222', NULL, N'1111111111', NULL, NULL, NULL, 5, CAST(N'2016-02-05 00:00:00.000' AS DateTime), 12)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (35, N'ABC01_01', N'city1', N'address1', NULL, 1, N'city1', N'45678', NULL, N'0774567890', N'          ', N'          ', NULL, 17, CAST(N'2016-02-10 14:34:50.410' AS DateTime), 20)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (36, N'L_A01_01', N'city1', N'address1', N'address2', 3, N'city1', N'45678-7890', N'ABCD@gmail.com', N'0774567890', N'0774567890', N'0774567890', N'0774567890', 18, CAST(N'2016-02-11 11:53:05.883' AS DateTime), 21)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (37, N'COM03_01', N'Company 1 city', N'Company 1 address 1', N'Company 1 address 2', 12, N'Company 1 city', N'23123-1234', N'company1@me.com', N'1234568933', N'          ', N'          ', NULL, 25, CAST(N'2016-02-12 17:55:37.027' AS DateTime), 22)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (38, N'COM03_02', N'Company 1 Branch 2', N'Company 1 Branch 2 Address 1', N'Company 1 Branch 2 Address 2', 18, N'Company 1 Branch 2 City', N'23231', NULL, N'4545454534', NULL, NULL, NULL, 25, CAST(N'2016-02-12 17:57:01.563' AS DateTime), 22)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (39, N'TES01_01', N'city1', N'address1', N'address2', 4, N'city1', N'45678', NULL, N'0774567890', N'          ', N'          ', NULL, 44, CAST(N'2016-02-13 11:26:47.387' AS DateTime), 23)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (40, N'ASA01_01', N'a', N'a', N'a', 15, N'a', N'', N'', N'3222444444', N'', N'', N'', 0, CAST(N'2016-02-13 21:33:47.313' AS DateTime), 24)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (41, N'OWN01_01', N'KANDY', N'MULGAMPOLA', N'Kalmunai - 09', 5, N'KANDY', N'32300', NULL, N'1234567890', N'          ', N'          ', NULL, 46, CAST(N'2016-02-14 09:37:34.450' AS DateTime), 25)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (42, N'OWN01_02', N'COLOMBO', N'COLOMBO', N'Kalmunai - 09', 6, N'COLOMBO', N'32300', NULL, N'1234567890', NULL, NULL, NULL, 47, CAST(N'2016-02-14 09:48:21.593' AS DateTime), 25)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (43, N'SPR01_01', N'city1', N'street1', NULL, 18, N'city1', N'34567', NULL, N'0771119066', N'          ', N'          ', NULL, 50, CAST(N'2016-02-14 10:20:43.190' AS DateTime), 26)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (44, N'SPR01_02', N'testBranch3', N'street3', NULL, 1, N'city3', N'34567', NULL, N'0778001134', NULL, NULL, NULL, 54, CAST(N'2016-02-14 17:53:01.117' AS DateTime), 26)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (45, N'TES02_01', N'city2', N'street1', N'', 15, N'city2', N'', N'', N'0771119066', N'', N'', N'', 0, CAST(N'2016-02-14 18:37:47.207' AS DateTime), 27)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (46, N'ABC02_01', N'abc', N'abc', NULL, 3, N'abc', N'44444', NULL, N'0713444902', N'          ', N'          ', NULL, 58, CAST(N'2016-02-15 09:06:35.167' AS DateTime), 28)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (47, N'COM04_01', N'kandy', N'Company1', NULL, 7, N'kandy', N'11111', NULL, N'1111111111', N'          ', N'          ', NULL, 57, CAST(N'2016-02-15 09:17:24.237' AS DateTime), 29)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (48, N'LEN01_01', N'city1', N'address1', NULL, 5, N'city1', N'45678', NULL, N'0774567890', N'          ', N'          ', NULL, 61, CAST(N'2016-02-15 15:24:47.593' AS DateTime), 30)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (49, N'COM05_01', N'aaaa', N'aaaa', N'aaaa', 19, N'aaaa', N'12345', NULL, N'1111111111', N'          ', N'          ', NULL, 62, CAST(N'2016-02-17 00:35:05.983' AS DateTime), 31)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (50, N'COM06_01', N'Kandy', N'Nawam Mawatha', NULL, 18, N'Kandy', N'20189', NULL, N'0812333333', N'          ', N'          ', NULL, 64, CAST(N'2016-02-18 08:32:01.093' AS DateTime), 32)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (51, N'COM04_02', N'colombo', N'address', NULL, 5, N'kal', N'12345', NULL, N'1234567890', NULL, NULL, NULL, 57, CAST(N'2016-02-18 16:36:57.207' AS DateTime), 29)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (53, N'LEN01_02', N'dealerbranch1', N'stree1', NULL, 1, N'city1', N'12345', NULL, N'0344934799', NULL, NULL, NULL, 61, CAST(N'2016-03-01 10:25:01.433' AS DateTime), 30)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (54, N'TFN01_01', N'kandy', N'street1', NULL, 2, N'kandy', N'12345', NULL, N'0771118906', N'          ', N'          ', NULL, 68, CAST(N'2016-03-01 10:29:33.377' AS DateTime), 33)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (55, N'DAR01_01', N'Kurunegala', N'Maho', NULL, 11, N'Kurunegala', N'12345', NULL, N'0713444801', N'          ', N'          ', NULL, 69, CAST(N'2016-03-03 08:44:37.353' AS DateTime), 34)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (56, N'ASD01_01', N'ssdsa', N'sadsa', N'sad', 1, N'ssdsa', N'34233-3243', N'657567567@ssss.com', N'2342343266', N'3432666666', N'6666666666', N'6567567576', 72, CAST(N'2016-03-02 23:57:50.037' AS DateTime), 35)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (57, N'TES03_01', N'testbranch', N'testbranch', NULL, 10, N'testbranch', N'65565', NULL, N'4554545454', N'          ', N'          ', NULL, 74, CAST(N'2016-03-03 15:17:13.100' AS DateTime), 36)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (58, N'LEN01_03', N'city1', N'sad', N'asd', 17, N'sa', N'12122', N'sd@ss.com', N'1111111111', NULL, NULL, NULL, 61, CAST(N'2016-03-04 02:19:25.877' AS DateTime), 30)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (59, N'LEN01_04', N'city1', N'sad', N'asd', 17, N'sa', N'12122', N'sd@ss.com', N'1111111111', NULL, NULL, NULL, 61, CAST(N'2016-03-04 02:20:08.330' AS DateTime), 30)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (60, N'LEN01_05', N'ggg', N'fdg', N'dfg', 2, N'aaaa', N'12345-3243', NULL, N'1111111111', NULL, NULL, NULL, 61, CAST(N'2016-03-04 02:22:30.053' AS DateTime), 30)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (62, N'DEA01_01', N'City1', N'Street1', NULL, 4, N'City1', N'12345', NULL, N'0771119066', NULL, NULL, NULL, 78, CAST(N'2016-03-07 08:09:55.743' AS DateTime), 38)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (63, N'TTT01_01', N'Rrrrrrrr', N'Rrrrrrrrrrrr', N'Rrrrrrr', 16, N'Rrrrrrrrrr', N'80000', N'asanka@thefuturenet.com', N'0777714065', N'7714065555', N'7714065555', N'0777714065', 77, CAST(N'2016-03-07 08:48:31.830' AS DateTime), 37)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (64, N'COM10_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'5678901112', N'5678901112', N'2345678901', 82, CAST(N'2016-03-07 11:43:27.277' AS DateTime), 42)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (65, N'COM07_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'5678901121', N'5678901121', N'2345678901', 80, CAST(N'2016-03-07 11:59:43.673' AS DateTime), 39)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (69, N'KAS01_04', N'kascom', N'kascom', NULL, 3, N'kascom', N'43444', NULL, N'5454554545', NULL, NULL, NULL, 83, CAST(N'2016-03-07 17:55:17.877' AS DateTime), 43)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (70, N'TES04_01', N'City1', N'Street1', NULL, 1, N'City1', N'12345-2345', NULL, N'0778800113', NULL, NULL, NULL, 86, CAST(N'2016-03-08 07:55:58.910' AS DateTime), 45)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (71, N'COM12_01', N'Company1', N'Company1', N'Company1', 2, N'Company1', N'12345', NULL, N'0888888888', NULL, NULL, NULL, 88, CAST(N'2016-03-08 08:39:31.663' AS DateTime), 46)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (72, N'COM16_01', N'City4', N'Street4', NULL, 2, N'City4', N'12345-2345', NULL, N'0778001134', NULL, NULL, NULL, 93, CAST(N'2016-03-08 14:59:00.737' AS DateTime), 50)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (73, N'COM15_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'5678901123', N'5678901123', N'2345678901', 92, CAST(N'2016-03-08 15:09:39.443' AS DateTime), 49)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (74, N'_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', NULL, NULL, N'2345678901', 99, CAST(N'2016-03-08 15:56:33.273' AS DateTime), 55)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (75, N'COM24_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', NULL, NULL, N'2345678901', 103, CAST(N'2016-03-08 17:06:06.683' AS DateTime), 58)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (76, N'COM18_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', NULL, NULL, NULL, 96, CAST(N'2016-03-08 17:49:11.487' AS DateTime), 52)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (77, N'COM25_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'5678901123', N'5678901123', N'2345678901', 108, CAST(N'2016-03-08 17:58:47.077' AS DateTime), 60)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (78, N'COM13_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'5678901234', N'5678901343', N'2345678901', 85, CAST(N'2016-03-08 18:20:48.913' AS DateTime), 47)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (79, N'DEA03_01', N'City71', N'Street71', N'', 3, N'City71', N'12345', NULL, N'0777777777', N'', N'', N'', 116, CAST(N'2016-03-09 13:55:39.860' AS DateTime), 62)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (80, N'DEA03_02', N'City72', N'Street72', N'', 2, N'City72', N'12345', NULL, N'0771119066', N'', N'', N'', 116, CAST(N'2016-03-09 13:58:29.897' AS DateTime), 62)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (81, N'DEL01_01', N'Dell City', N'Dell 1', N'Dell 2', 8, N'Dell City', N'55555', N'test@gmail.com', N'8888888888', N'', N'', N'', 119, CAST(N'2016-03-09 14:16:01.870' AS DateTime), 63)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (82, N'LEN02_01', N'City8', N'Street8', N'', 2, N'City8', N'12345', NULL, N'0778881134', N'', N'', N'', 122, CAST(N'2016-03-09 15:15:57.397' AS DateTime), 64)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (83, N'LEN03_01', N'City9', N'Street9', N'', 1, N'City9', N'12345', NULL, N'0778001134', N'', N'', N'', 123, CAST(N'2016-03-09 15:46:55.033' AS DateTime), 65)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (84, N'LEN03_02', N'Branch9', N'Street91', N'', 2, N'City91', N'12345', NULL, N'0771119066', N'', N'', N'', 123, CAST(N'2016-03-09 15:48:18.873' AS DateTime), 65)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (85, N'COO01_01', N'Cool City', N'Cool Add1 ', N'Cool Add 2', 5, N'Cool City', N'10101', NULL, N'1234567890', N'', N'', N'', 126, CAST(N'2016-03-09 20:07:59.277' AS DateTime), 66)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (86, N'COO01_02', N'Cool City 1', N'Asdsad Sadsa ', N'', 9, N'City', N'98989', NULL, N'8888888880', N'', N'', N'', 126, CAST(N'2016-03-09 20:13:46.187' AS DateTime), 66)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (87, N'LEN04_01', N'City10', N'Street10', N'', 2, N'City10', N'12345', NULL, N'0111111111', N'', N'', N'', 129, CAST(N'2016-03-10 08:07:55.607' AS DateTime), 67)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (88, N'LEN04_02', N'Branch10', N'Street10', N'', 2, N'City10', N'12345', NULL, N'0771119066', N'', N'', N'', 129, CAST(N'2016-03-10 08:20:42.943' AS DateTime), 67)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (89, N'COM29_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'', N'', N'2345678901', 128, CAST(N'2016-03-10 08:41:52.670' AS DateTime), 70)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (90, N'COM30_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'', N'', N'2345678901', 134, CAST(N'2016-03-10 12:12:46.417' AS DateTime), 71)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (91, N'COM30_02', N'Alaska2', N'Company1', N'Street1', 2, N'Qwee', N'11111', NULL, N'2345678901', N'', N'', N'', 134, CAST(N'2016-03-10 12:16:04.043' AS DateTime), 71)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (92, N'DAR02_01', N'Darshanabranch', N'Darshanaadmin', N'Darshanaadmin', 3, N'trt', N'45545', N'fef@rtrt.fef', N'5343434434', N'3434434554', N'', N'', 135, CAST(N'2016-03-10 12:42:01.737' AS DateTime), 72)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (93, N'COM31_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'', N'', N'2345678901', 139, CAST(N'2016-03-10 13:49:59.327' AS DateTime), 73)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (94, N'LEN05_01', N'Branch111', N'Street111', N'', 1, N'City111', N'12345', NULL, N'0771119066', N'', N'', N'', 140, CAST(N'2016-03-10 13:57:39.863' AS DateTime), 74)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (95, N'LEN05_02', N'Branch112', N'Street112', N'', 4, N'City112', N'12345', NULL, N'0771119066', N'', N'', N'', 140, CAST(N'2016-03-10 13:58:54.717' AS DateTime), 74)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (96, N'COM31_02', N'Alaska', N'Company1', N'', 2, N'Alaska', N'11111', NULL, N'2345678901', N'', N'', N'', 139, CAST(N'2016-03-10 13:59:59.227' AS DateTime), 73)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (97, N'ASA02_01', N'Pannipitiya', N'Kottawa', N'Kottawa', 38, N'Pannipitiya', N'80000', NULL, N'3692581470', N'', N'', N'', 144, CAST(N'2016-03-10 22:57:46.240' AS DateTime), 75)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (98, N'BEL01_01', N'Bell State Msp', N'45 E Washington Ave', N'', 23, N'Minneapolis', N'55410', NULL, N'7639854658', N'', N'', N'', 146, CAST(N'2016-03-10 20:09:20.143' AS DateTime), 76)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (99, N'BEL01_02', N'Bell State Fargo', N'624', N'', 34, N'Fargo', N'58103', NULL, N'7015384952', N'', N'', N'', 146, CAST(N'2016-03-10 20:13:36.400' AS DateTime), 76)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (100, N'DEA04_01', N'Dealer Dapn', N'Street111', N'', 4, N'City111', N'12234', NULL, N'0778001134', N'', N'', N'', 149, CAST(N'2016-03-11 12:22:02.340' AS DateTime), 77)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (101, N'DEA04_02', N'Company DAPN - Branch 2', N'Street1110', N'', 2, N'City1110', N'12345', NULL, N'0778001134', N'', N'', N'', 149, CAST(N'2016-03-11 12:23:40.953' AS DateTime), 77)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (102, N'BAK01_01', N'Bakkkk Jjjjjj', N'Sdfsd sdfsd', N'Adsad asdas', 15, N'Sssssss Sssss', N'34234', NULL, N'3243243243', N'', N'', N'', 151, CAST(N'2016-03-11 14:40:08.707' AS DateTime), 78)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (103, N'COM32_01', N'Company1', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'5678901453', N'5678901234', N'2345678901', 152, CAST(N'2016-03-11 16:20:34.340' AS DateTime), 79)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (104, N'COM32_02', N'colombo', N'Company1', N'Street1', 3, N'Alaska', N'11111', NULL, N'2345678901', N'', N'', N'', 152, CAST(N'2016-03-11 16:21:37.717' AS DateTime), 79)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (105, N'DEA05_01', N'Dealer DAPN2', N'Street12', N'', 3, N'City12', N'12345', N'DealerDAPN2@hgg.com', N'0778881134', N'6457777374', N'6457777374', N'6457777374', 153, CAST(N'2016-03-11 16:38:45.907' AS DateTime), 80)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (107, N'ASA02_02', N'Pannipitiya01', N'Pa', N'As', 13, N'Pa', N'85596', NULL, N'9874566330', N'', N'', N'', 144, CAST(N'2016-03-12 10:37:56.340' AS DateTime), 75)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (108, N'DAP01_01', N'DAPND3', N'Street3', N'', 2, N'City3', N'12345', NULL, N'0778001134', N'', N'', N'', 156, CAST(N'2016-03-12 10:47:09.117' AS DateTime), 82)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (109, N'DAR03_01', N'darshanadealercompany', N'darshanadealercompany', N'', 6, N'darshanadealercompany', N'54545', NULL, N'5454455455', N'', N'', N'', 157, CAST(N'2016-03-12 11:17:37.330' AS DateTime), 83)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (110, N'EWT01_01', N'ewtert', N'treytrty', N'ryeyer7y', 4, N'trdftrf', N'44444', NULL, N'1111111111', N'', N'', N'', 159, CAST(N'2016-03-12 11:20:07.447' AS DateTime), 84)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (111, N'DEH01_01', N'Dehiwala', N'Dehi', N'Dehi', 6, N'Dehi', N'98745', NULL, N'9874563211', N'', N'', N'', 160, CAST(N'2016-03-12 11:38:06.613' AS DateTime), 85)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (112, N'TFN02_01', N'Tfn', N'Kandy', N'', 16, N'Kandy', N'54789', NULL, N'9685741233', N'', N'', N'', 161, CAST(N'2016-03-12 01:25:44.563' AS DateTime), 86)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (113, N'DAP02_01', N'DAPN15', N'Street15', N'', 2, N'City15', N'12345', NULL, N'0778001134', N'', N'', N'', 162, CAST(N'2016-03-12 15:31:09.630' AS DateTime), 87)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (114, N'COM33_01', N'Company1', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'5678901123', N'5678901123', N'2345678901', 164, CAST(N'2016-03-12 16:14:49.247' AS DateTime), 88)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (115, N'DAP03_01', N'DAPN16', N'Street156', N'', 3, N'City16', N'12334', NULL, N'0778001134', N'', N'', N'', 165, CAST(N'2016-03-12 17:01:14.877' AS DateTime), 89)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (116, N'TRE01_01', N'Treet1 XFDSGdsg Fhgh', N'Treet1', N'Treet1', 12, N'Treet1', N'11111-1111', N'adsd@4546.kii', N'1234567890', N'1234567890', N'', N'', 167, CAST(N'2016-03-12 18:01:38.217' AS DateTime), 90)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (117, N'TRE01_02', N'ASDfd', N'Axfdsvfds', N'', 2, N'Qeqw', N'11111', NULL, N'1111111111', N'', N'', N'', 167, CAST(N'2016-03-12 18:07:06.317' AS DateTime), 90)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (118, N'ABC04_01', N'ABCtesting-branch1', N'Badulla', N'Badulla', 9, N'Badulla', N'12121-2141', N'ABCtesting_branch1@info.com', N'1234567894', N'', N'', N'', 170, CAST(N'2016-03-13 13:57:48.553' AS DateTime), 92)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (119, N'DEA06_01', N'Dealer Company17', N'Street17', N'', 3, N'City17', N'12345', NULL, N'0778001134', N'', N'', N'', 173, CAST(N'2016-03-14 07:58:40.170' AS DateTime), 94)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (120, N'FIS01_01', N'FistSuperAdminC', N'FistSuperAdminC', N'FistSuperAdminC', 3, N'Asder', N'12345-6789', NULL, N'1111111111', N'', N'', N'', 172, CAST(N'2016-03-14 08:20:46.403' AS DateTime), 93)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (121, N'KAS02_01', N'Kasunnewcombranch', N'Kasunnewcombranch', N'', 5, N'Kasunnewcity', N'12378', NULL, N'0713456789', N'', N'', N'', 175, CAST(N'2016-03-14 09:13:46.833' AS DateTime), 95)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (122, N'COO01_03', N'Sdsd', N'Asd', N'Asd', 1, N'Asd', N'89566', NULL, N'4555554433', N'', N'', N'', 126, CAST(N'2016-03-17 19:59:14.483' AS DateTime), 66)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (123, N'COO01_04', N'Sdsd', N'Asd', N'Asd', 1, N'Asd', N'89566', NULL, N'4555554433', N'', N'', N'', 126, CAST(N'2016-03-17 19:59:25.067' AS DateTime), 66)
INSERT [dbo].[branch] ([branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id]) VALUES (124, N'LEN06_01', N'Lender Company18', N'Street18', N'', 2, N'City18', N'12354', NULL, N'0111111111', N'', N'', N'', 178, CAST(N'2016-03-23 08:27:53.190' AS DateTime), 96)
SET IDENTITY_INSERT [dbo].[branch] OFF
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2012, N'Cmake', N'CModel2')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2013, N'CMake3', N'Cmodel2')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2012, N'CMake2', N'CModel')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2013, N'Cmake3', N'Cmodel')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2013, N'CMake3', N'Cmodel3')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2012, N'Cmake', N'CModel2')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2013, N'CMake3', N'Cmodel2')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2012, N'CMake2', N'CModel')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2013, N'Cmake3', N'Cmodel')
INSERT [dbo].[camperModelYear] ([year], [make], [model]) VALUES (2013, N'CMake3', N'Cmodel3')
SET IDENTITY_INSERT [dbo].[company] ON 

INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (12, N'myLender', N'COM02', N'sfhdgfh', N'', N'clb', 12, N'11111', N'', N'', N'          ', N'          ', N'', N'', 5, NULL, 2, 5, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (20, N'ABCD', N'ABC01', N'address1', N'', N'city1', 1, N'45678', N'', N'0774567890', N'          ', N'          ', N'', N'', 17, CAST(N'2016-02-10 14:34:33.247' AS DateTime), 2, 17, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (21, N'L_ABCD', N'L_A01', N'address1', N'address2', N'city1', 3, N'45678-7890', N'ABCD@gmail.com', N'0774567890', N'0774567890', N'0774567890', N'0774567890', N'', 18, CAST(N'2016-02-11 11:50:40.593' AS DateTime), 1, 18, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (22, N'Company1', N'COM03', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901121', N'5678901121', N'2345678901', N'', 25, CAST(N'2016-02-12 17:54:44.900' AS DateTime), 1, 25, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (23, N'test_user4Company', N'TES01', N'address1', N'address2', N'city1', 4, N'45678', N'', N'0774567890', N'          ', N'          ', N'', N'', 44, CAST(N'2016-02-13 11:06:29.333' AS DateTime), 1, 44, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (24, N'asanka', N'ASA01', N'a', N'a', N'a', 15, N'33332', N'', N'3222444444', N'          ', N'          ', N'', N'', 0, CAST(N'2016-02-13 21:33:47.313' AS DateTime), 1, 45, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (25, N'OWNER-COMPANY', N'OWN01', N'MULGAMPOLA', N'Kalmunai - 09', N'KANDY', 5, N'32300', N'', N'1234567890', N'          ', N'          ', N'', N'', 46, CAST(N'2016-02-14 09:37:09.267' AS DateTime), 1, 46, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (26, N'Sprint3TestCompany', N'SPR01', N'street1', N'', N'city1', 18, N'34567', N'', N'0771119066', N'          ', N'          ', N'', N'', 50, CAST(N'2016-02-14 10:19:34.673' AS DateTime), 1, 50, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (27, N'testcompany3', N'TES02', N'street1', N'', N'city2', 15, N'34567', N'', N'0771119066', N'          ', N'          ', N'', N'', 0, CAST(N'2016-02-14 18:37:47.207' AS DateTime), 2, 0, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (28, N'ABC', N'ABC02', N'abc', N'', N'abc', 3, N'44444', N'', N'0713444902', N'          ', N'          ', N'', N'', 58, CAST(N'2016-02-15 09:06:18.310' AS DateTime), 1, 58, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (29, N'Company1', N'COM04', N'Company1', N'', N'kandy', 7, N'11111', N'', N'1111111111', N'          ', N'          ', N'', N'', 57, CAST(N'2016-02-15 09:16:53.970' AS DateTime), 1, 57, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (30, N'dealerPiyumi1', N'LEN01', N'address1', N'', N'city1', 5, N'45678', N'', N'0774567890', N'          ', N'          ', N'', N'', 61, CAST(N'2016-02-15 15:24:27.473' AS DateTime), 1, 61, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (31, N'com', N'COM05', N'aaaa', N'aaaa', N'aaaa', 19, N'12345', N'', N'1111111111', N'          ', N'          ', N'', N'', 62, CAST(N'2016-02-17 00:34:56.577' AS DateTime), 2, 62, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (32, N'Commercial Bank', N'COM06', N'Nawam Mawatha', N'', N'Kandy', 18, N'20189', N'', N'0812333333', N'          ', N'          ', N'', N'', 64, CAST(N'2016-02-18 08:31:51.937' AS DateTime), 2, 64, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (33, N'tfnkandyDelaer', N'TFN01', N'street1', N'', N'kandy', 2, N'12345', N'', N'0771118906', N'          ', N'          ', N'', N'', 68, CAST(N'2016-03-01 10:29:18.737' AS DateTime), 2, 68, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (34, N'DarshanaGroup', N'DAR01', N'Maho', N'', N'Kurunegala', 11, N'12345', N'', N'0713444801', N'          ', N'          ', N'', N'', 69, CAST(N'2016-03-03 08:44:22.487' AS DateTime), 1, 69, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (35, N'asdasd', N'ASD01', N'sadsa', N'sad', N'ssdsa', 1, N'34233-3243', N'657567567@ssss.com', N'2342343266', N'3432666666', N'6666666666', N'6567567576', N'', 72, CAST(N'2016-03-02 23:38:05.120' AS DateTime), 1, 72, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (36, N'testbranch', N'TES03', N'testbranch', N'', N'testbranch', 10, N'65565', N'', N'4554545454', N'          ', N'          ', N'', N'', 74, CAST(N'2016-03-03 15:17:07.683' AS DateTime), 1, 74, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (37, N'Company Setup', N'TTT01', N'Kottawa', N'Kot', N'Colombo', 15, N'80001-1234', N'', N'1234567899', N'', N'', N'', N'', 77, CAST(N'2016-03-06 20:45:40.063' AS DateTime), 1, 77, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (38, N'Dealer1', N'DEA01', N'Street1', N'', N'City1', 2, N'12345', N'', N'0771119066', N'', N'', N'', N'', 78, CAST(N'2016-03-07 08:09:29.930' AS DateTime), 2, 78, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (39, N'Company1', N'COM07', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901121', N'5678901121', N'2345678901', N'', 80, CAST(N'2016-03-07 11:18:35.883' AS DateTime), 1, 80, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (40, N'Company1', N'COM08', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901121', N'5678901121', N'2345678901', N'', 81, CAST(N'2016-03-07 11:38:02.067' AS DateTime), 1, 81, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (41, N'Company1', N'COM09', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901121', N'5678901121', N'2345678901', N'', 81, CAST(N'2016-03-07 11:39:12.957' AS DateTime), 1, 81, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (42, N'Company1', N'COM10', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901112', N'5678901112', N'2345678901', N'', 82, CAST(N'2016-03-07 11:42:19.527' AS DateTime), 1, 82, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (43, N'kascom', N'KAS01', N'kascom', N'', N'kascom', 3, N'43444', N'', N'5454554545', N'', N'', N'', N'', 83, CAST(N'2016-03-07 17:28:53.253' AS DateTime), 1, 83, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (44, N'Company1', N'COM11', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901122', N'5678901122', N'2345678901', N'', 84, CAST(N'2016-03-07 18:06:11.667' AS DateTime), 1, 84, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (45, N'TestCompany1', N'TES04', N'Street1', N'', N'City1', 1, N'12345-2345', N'', N'077880011', N'', N'', N'', N'', 86, CAST(N'2016-03-08 07:54:12.943' AS DateTime), 1, 86, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (46, N'Company1', N'COM12', N'Company1', N'Company1', N'Company1', 2, N'12345', N'', N'0888888888', N'', N'', N'', N'', 88, CAST(N'2016-03-08 08:39:23.240' AS DateTime), 1, 88, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (47, N'Company1', N'COM13', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901234', N'5678901343', N'2345678901', N'', 85, CAST(N'2016-03-08 14:12:43.207' AS DateTime), 1, 85, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (48, N'Company1', N'COM14', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901123', N'5678901123', N'2345678901', N'', 91, CAST(N'2016-03-08 14:17:50.433' AS DateTime), 1, 91, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (49, N'Company1', N'COM15', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901123', N'5678901123', N'2345678901', N'', 92, CAST(N'2016-03-08 14:44:03.433' AS DateTime), 1, 92, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (50, N'Company4', N'COM16', N'Street4', N'', N'City4', 2, N'12345-2345', N'', N'0778001134', N'', N'', N'', N'', 93, CAST(N'2016-03-08 14:56:52.430' AS DateTime), 1, 93, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (51, N'Company1', N'COM17', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901123', N'5678901123', N'2345678901', N'', 94, CAST(N'2016-03-08 15:43:39.807' AS DateTime), 1, 94, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (52, N'Company1', N'COM18', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'', N'', 96, CAST(N'2016-03-08 15:47:38.143' AS DateTime), 1, 96, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (53, N'Company1', N'COM19', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'', N'', 97, CAST(N'2016-03-08 15:49:36.623' AS DateTime), 1, 97, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (54, N'Company1', N'COM20', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'', N'', 98, CAST(N'2016-03-08 15:52:15.650' AS DateTime), 1, 98, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (55, N'Company1', N'COM21', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 99, CAST(N'2016-03-08 15:55:37.933' AS DateTime), 1, 99, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (56, N'Company1', N'COM22', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 101, CAST(N'2016-03-08 16:33:39.507' AS DateTime), 1, 101, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (57, N'Company1', N'COM23', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 102, CAST(N'2016-03-08 16:42:23.173' AS DateTime), 1, 102, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (58, N'Company1', N'COM24', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901123', N'5678901123', N'2345678901', N'', 103, CAST(N'2016-03-08 17:05:55.033' AS DateTime), 1, 103, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (59, N'Company1', N'COM25', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901123', N'5678901123', N'2345678901', N'', 108, CAST(N'2016-03-08 17:58:31.387' AS DateTime), 2, 108, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (60, N'Company1', N'COM26', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901123', N'5678901123', N'2345678901', N'', 110, CAST(N'2016-03-08 19:12:49.397' AS DateTime), 1, 110, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (61, N'Dealercompany7', N'DEA02', N'Street6', N'', N'City6', 2, N'12345-1234', N'', N'0888888888', N'', N'', N'', N'', 113, CAST(N'2016-03-09 13:22:46.770' AS DateTime), 2, 113, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (62, N'Dealercompany71', N'DEA03', N'Street71', N'', N'City71', 3, N'12345', N'', N'0777777777', N'', N'', N'', N'', 116, CAST(N'2016-03-09 13:52:37.397' AS DateTime), 2, 116, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (63, N'Cool', N'DEL01', N'Cool Add1', N'Cool Add2', N'Cool City', 5, N'12121', N'', N'7410852963', N'', N'', N'', N'', 119, CAST(N'2016-03-09 14:15:28.567' AS DateTime), 2, 119, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (64, N'Lendercompany8', N'LEN02', N'Street8', N'', N'City8', 2, N'12345', N'', N'0778881134', N'', N'', N'', N'', 122, CAST(N'2016-03-09 15:15:19.260' AS DateTime), 1, 122, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (65, N'Lendercompany9', N'LEN03', N'Street9', N'', N'City9', 1, N'12345', N'', N'0778001134', N'', N'', N'', N'', 123, CAST(N'2016-03-09 15:45:53.020' AS DateTime), 1, 123, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (66, N'Cool Com', N'COO01', N'Cool Add1 ', N'Cool Add 2', N'Kkkkk', 4, N'02368-6778', N'', N'1234567890', N'', N'', N'1223652433', N'', 126, CAST(N'2016-03-09 18:29:01.560' AS DateTime), 2, 126, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (67, N'Lendercompany10', N'LEN04', N'Street10', N'', N'City10', 2, N'12345', N'', N'0111111111', N'', N'', N'', N'', 129, CAST(N'2016-03-10 08:05:09.967' AS DateTime), 1, 129, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (68, N'Company1', N'COM27', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 128, CAST(N'2016-03-10 08:09:50.100' AS DateTime), 1, 128, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (69, N'Company1', N'COM28', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 128, CAST(N'2016-03-10 08:17:09.753' AS DateTime), 1, 128, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (70, N'Company1', N'COM29', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 128, CAST(N'2016-03-10 08:18:10.257' AS DateTime), 1, 128, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (71, N'Company1', N'COM30', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 134, CAST(N'2016-03-10 12:12:27.407' AS DateTime), 1, 134, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (72, N'Darshanaadmin', N'DAR02', N'Darshanaadmin', N'Darshanaadmin', N'trt', 3, N'45545', N'fef@rtrt.fef', N'5343434434', N'3434434554', N'', N'', N'', 135, CAST(N'2016-03-10 12:41:56.023' AS DateTime), 1, 135, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (73, N'Company1', N'COM31', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'', N'', N'2345678901', N'', 139, CAST(N'2016-03-10 13:49:48.147' AS DateTime), 2, 139, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (74, N'Abclender1', N'LEN05', N'Street1', N'', N'City1', 1, N'12345', N'', N'0111111111', N'', N'', N'', N'', 140, CAST(N'2016-03-10 13:56:00.887' AS DateTime), 1, 140, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (75, N'Asanka', N'ASA02', N'Kottawa', N'Kottawa', N'Pannipitiya', 38, N'80000', N'', N'3692581470', N'', N'', N'', N'', 144, CAST(N'2016-03-10 22:57:32.807' AS DateTime), 2, 144, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (76, N'Bell State', N'BEL01', N'45 E Washington Ave', N'', N'Minneapolis', 23, N'55410', N'', N'7639854658', N'', N'', N'', N'', 146, CAST(N'2016-03-10 20:08:02.527' AS DateTime), 1, 146, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (77, N'Dealer Dapn', N'DEA04', N'Street111', N'', N'City111', 4, N'12234', N'', N'0778001134', N'', N'', N'', N'', 149, CAST(N'2016-03-11 10:39:25.167' AS DateTime), 2, 149, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (78, N'Bakkkk Jjjjjj', N'BAK01', N'Sdfsd sdfsd', N'Adsad asdas', N'Sssssss Sssss', 15, N'34234', N'', N'3243243243', N'', N'', N'', N'', 151, CAST(N'2016-03-11 14:39:46.060' AS DateTime), 1, 151, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (79, N'Company1', N'COM32', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901453', N'5678901234', N'2345678901', N'', 152, CAST(N'2016-03-11 16:20:27.593' AS DateTime), 2, 152, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (80, N'Dealer DAPN2', N'DEA05', N'Street12', N'', N'City12', 3, N'12345', N'DealerDAPN2@hgg.com', N'0778881134', N'6457777374', N'6457777374', N'6457777374', N'', 153, CAST(N'2016-03-11 16:38:10.467' AS DateTime), 2, 153, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (82, N'DAPND3', N'DAP01', N'Street3', N'', N'City3', 2, N'12345', N'', N'0778001134', N'', N'', N'', N'', 156, CAST(N'2016-03-12 10:46:57.827' AS DateTime), 2, 156, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (83, N'darshanadealercompany', N'DAR03', N'darshanadealercompany', N'', N'darshanadealercompany', 6, N'54545', N'', N'5454455455', N'', N'', N'', N'', 157, CAST(N'2016-03-12 11:17:31.667' AS DateTime), 2, 157, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (84, N'ewtert', N'EWT01', N'treytrty', N'ryeyer7y', N'trdftrf', 4, N'44444', N'', N'1111111111', N'', N'', N'', N'', 159, CAST(N'2016-03-12 11:20:01.580' AS DateTime), 1, 159, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (85, N'Dehiwala', N'DEH01', N'Dehi', N'Dehi', N'Dehi', 6, N'98745', N'', N'9874563211', N'', N'', N'', N'', 160, CAST(N'2016-03-12 11:37:58.073' AS DateTime), 2, 160, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (86, N'Tfn', N'TFN02', N'Kandy', N'', N'Kandy', 16, N'54789', N'', N'9685741233', N'', N'', N'', N'', 161, CAST(N'2016-03-12 01:25:31.563' AS DateTime), 2, 161, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (87, N'DAPN15', N'DAP02', N'Street15', N'', N'City15', 2, N'12345', N'', N'0778001134', N'', N'', N'', N'', 162, CAST(N'2016-03-12 15:29:37.413' AS DateTime), 2, 162, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (88, N'Company1', N'COM33', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'5678901123', N'5678901123', N'2345678901', N'', 164, CAST(N'2016-03-12 16:14:44.070' AS DateTime), 2, 164, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (89, N'DAPN16', N'DAP03', N'Street156', N'', N'City16', 3, N'12334', N'', N'0778001134', N'', N'', N'', N'', 165, CAST(N'2016-03-12 17:00:41.563' AS DateTime), 2, 165, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (90, N'Treet1 XFDSGdsg Fhgh', N'TRE01', N'Treet1', N'Treet1', N'Treet1', 12, N'11111-1111', N'adsd@4546.kii', N'1234567890', N'1234567890', N'', N'', N'http://www.juik.kil', 167, CAST(N'2016-03-12 18:00:37.163' AS DateTime), 1, 167, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (91, N'ABCtesting', N'ABC03', N'Colombo', N'Colombo', N'Colombo', 40, N'45445-1234', N'adbtesting@info.com', N'1231231234', N'', N'', N'', N'', 0, CAST(N'2016-03-13 13:46:03.563' AS DateTime), 2, 0, 1, NULL)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (92, N'ABCtesting', N'ABC04', N'Main Street', N'', N'Colombo', 40, N'23451-1234', N'abctesting@info.com', N'1234567891', N'', N'', N'2132435465', N'http://abctesting.com', 170, CAST(N'2016-03-13 13:53:34.493' AS DateTime), 2, 170, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (93, N'FistSuperAdminC', N'FIS01', N'FistSuperAdminC', N'FistSuperAdminC', N'Asder', 3, N'12345-6789', N'', N'1111111111', N'', N'', N'', N'', 172, CAST(N'2016-03-14 07:54:31.557' AS DateTime), 2, 172, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (94, N'Dealer Company17', N'DEA06', N'Street17', N'', N'City17', 3, N'12345', N'', N'0778001134', N'', N'', N'', N'', 173, CAST(N'2016-03-14 07:58:27.033' AS DateTime), 1, 173, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (95, N'Kasunnewcom', N'KAS02', N'Kasunnewcom', N'', N'Kasunnewcity', 5, N'12378', N'', N'0713456789', N'', N'', N'', N'', 175, CAST(N'2016-03-14 09:13:14.313' AS DateTime), 1, 175, 1, 1)
INSERT [dbo].[company] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [first_super_admin_id], [company_status], [flag_value]) VALUES (96, N'Lender Company18', N'LEN06', N'Street18', N'', N'City18', 2, N'12354', N'', N'0111111111', N'', N'', N'', N'', 178, CAST(N'2016-03-23 08:27:45.430' AS DateTime), 1, 178, 1, 1)
SET IDENTITY_INSERT [dbo].[company] OFF
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (12, NULL, 4)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (38, 62, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (61, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (47, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (45, 70, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (46, 71, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (49, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (50, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (51, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (53, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (55, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (55, 74, 4)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (56, 74, 3)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (57, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (58, 75, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (60, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (62, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (64, 82, 3)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (62, 80, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (63, 81, 3)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (79, 104, 3)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (67, 87, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (68, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (73, 96, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (91, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (71, 91, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (66, 85, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (48, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (56, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (58, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (52, 76, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (59, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (64, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (49, 73, 3)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (52, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (54, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (57, 74, 3)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (59, 77, 4)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (74, 95, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (69, NULL, 2)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (79, 103, 5)
INSERT [dbo].[companySetupStep] ([company_id], [branch_id], [step_number]) VALUES (81, 106, 4)
SET IDENTITY_INSERT [dbo].[companyType] ON 

INSERT [dbo].[companyType] ([company_type_id], [company_type_name]) VALUES (1, N'Lender')
INSERT [dbo].[companyType] ([company_type_id], [company_type_name]) VALUES (2, N'Dealer')
SET IDENTITY_INSERT [dbo].[companyType] OFF
SET IDENTITY_INSERT [dbo].[curtailment] ON 

INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (95, N'3', 20, 179, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (96, N'6', 20, 179, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (97, N'9', 30, 179, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (98, N'11', 10, 179, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (110, N'1', 12, 187, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (118, N'1', 20, 184, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (119, N'2', 30, 184, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (120, N'3', 35, 184, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (129, N'4', 100, 181, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (130, N'1', 30, 190, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (131, N'2', 20, 190, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (132, N'3', 50, 190, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (133, N'7', 70, 192, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (134, N'8', 5, 192, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (136, N'2', 54, 191, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (168, N'1', 50, 218, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (169, N'4', 50, 218, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (195, N'2', 20, 220, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (196, N'5', 40, 220, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (197, N'9', 10, 220, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (198, N'12', 10, 220, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (203, N'1', 80, 221, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (204, N'1', 1, 226, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (211, N'5', 40, 228, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (212, N'7', 40, 228, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (213, N'1', 12, 219, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (214, N'2', 10, 229, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (215, N'4', 50, 229, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (216, N'6', 20, 229, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (217, N'1', 12, 230, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (218, N'3', 70, 227, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (219, N'9', 20, 227, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (220, N'3', 40, 225, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (221, N'7', 50, 225, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (222, N'3', 50, 237, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (223, N'5', 10, 237, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (224, N'8', 40, 237, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (228, N'4', 77, 232, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (229, N'3', 30, 238, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (230, N'6', 40, 238, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (231, N'9', 60, 238, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (232, N'6', 50, 239, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (233, N'8', 50, 239, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (234, N'4', 20, 240, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (235, N'6', 16, 240, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (236, N'1', 10, 241, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (237, N'2', 90, 241, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (253, N'1', 23, 251, CAST(30.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (254, N'5', 20, 257, CAST(25.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (255, N'7', 25, 257, CAST(31.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (256, N'8', 35, 257, CAST(43.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (257, N'3', 30, 263, CAST(37.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (258, N'6', 30, 263, CAST(37.000 AS Decimal(18, 3)))
INSERT [dbo].[curtailment] ([curtailment_id], [time_period], [percentage], [loan_id], [payment_percentage]) VALUES (259, N'9', 20, 263, CAST(25.000 AS Decimal(18, 3)))
SET IDENTITY_INSERT [dbo].[curtailment] OFF
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000005', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000005', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-27 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000005', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-30 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000005', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-04-01 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000006', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-02-27 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000006', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000006', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-04 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000006', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-06 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000008', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-18 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000008', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-21 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000008', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000008', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-26 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000012', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-18 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000012', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-21 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000012', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000012', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-26 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000015', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-21 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000015', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000015', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-27 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000015', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000017', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-21 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000017', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000017', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-27 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000017', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000019', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-21 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000019', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000019', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-27 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000019', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000021', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-21 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000021', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000021', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-27 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000021', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000022', 1, CAST(1.92 AS Decimal(18, 2)), CAST(N'2016-03-27 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000022', 2, CAST(1.92 AS Decimal(18, 2)), CAST(N'2016-03-30 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000022', 3, CAST(2.88 AS Decimal(18, 2)), CAST(N'2016-04-02 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000022', 4, CAST(2.88 AS Decimal(18, 2)), CAST(N'2016-04-04 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000023', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000023', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000023', 3, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000023', 4, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-04-11 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000024', 1, CAST(600.00 AS Decimal(18, 2)), CAST(N'2016-03-26 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000024', 2, CAST(600.00 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000024', 3, CAST(900.00 AS Decimal(18, 2)), CAST(N'2016-04-01 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000024', 4, CAST(900.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000025', 1, CAST(208.00 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000025', 2, CAST(208.00 AS Decimal(18, 2)), CAST(N'2016-04-01 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000025', 3, CAST(312.00 AS Decimal(18, 2)), CAST(N'2016-04-04 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000025', 4, CAST(312.00 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000026', 1, CAST(33.92 AS Decimal(18, 2)), CAST(N'2016-04-02 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000026', 2, CAST(33.92 AS Decimal(18, 2)), CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000026', 3, CAST(50.88 AS Decimal(18, 2)), CAST(N'2016-04-08 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000026', 4, CAST(50.88 AS Decimal(18, 2)), CAST(N'2016-04-10 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000027', 1, CAST(196.96 AS Decimal(18, 2)), CAST(N'2016-04-02 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000027', 2, CAST(196.96 AS Decimal(18, 2)), CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000027', 3, CAST(295.44 AS Decimal(18, 2)), CAST(N'2016-04-08 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000027', 4, CAST(295.44 AS Decimal(18, 2)), CAST(N'2016-04-10 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000028', 1, CAST(5141.12 AS Decimal(18, 2)), CAST(N'2016-04-02 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000028', 2, CAST(5141.12 AS Decimal(18, 2)), CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000028', 3, CAST(7711.68 AS Decimal(18, 2)), CAST(N'2016-04-08 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000028', 4, CAST(7711.68 AS Decimal(18, 2)), CAST(N'2016-04-10 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000029', 1, CAST(3.68 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000029', 2, CAST(3.68 AS Decimal(18, 2)), CAST(N'2016-04-01 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000029', 3, CAST(5.52 AS Decimal(18, 2)), CAST(N'2016-04-04 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000029', 4, CAST(5.52 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000030', 1, CAST(211.68 AS Decimal(18, 2)), CAST(N'2016-03-02 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000030', 2, CAST(211.68 AS Decimal(18, 2)), CAST(N'2016-03-05 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000030', 3, CAST(317.52 AS Decimal(18, 2)), CAST(N'2016-03-08 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000030', 4, CAST(317.52 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000031', 1, CAST(499.68 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000031', 2, CAST(499.68 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000031', 3, CAST(749.52 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000031', 4, CAST(749.52 AS Decimal(18, 2)), CAST(N'2016-04-11 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000032', 1, CAST(19.68 AS Decimal(18, 2)), CAST(N'2016-05-01 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000032', 2, CAST(19.68 AS Decimal(18, 2)), CAST(N'2016-05-04 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000032', 3, CAST(29.52 AS Decimal(18, 2)), CAST(N'2016-05-07 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000032', 4, CAST(29.52 AS Decimal(18, 2)), CAST(N'2016-05-09 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000033', 1, CAST(3722.40 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000033', 2, CAST(3722.40 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000033', 3, CAST(5583.60 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000033', 4, CAST(5583.60 AS Decimal(18, 2)), CAST(N'2016-04-11 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000034', 1, CAST(1969.92 AS Decimal(18, 2)), CAST(N'2016-03-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000034', 2, CAST(1969.92 AS Decimal(18, 2)), CAST(N'2016-03-26 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000034', 3, CAST(2954.88 AS Decimal(18, 2)), CAST(N'2016-03-29 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000034', 4, CAST(2954.88 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000035', 1, CAST(2114.24 AS Decimal(18, 2)), CAST(N'2016-03-17 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000035', 2, CAST(2114.24 AS Decimal(18, 2)), CAST(N'2016-03-20 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000035', 3, CAST(3171.36 AS Decimal(18, 2)), CAST(N'2016-03-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000035', 4, CAST(3171.36 AS Decimal(18, 2)), CAST(N'2016-03-25 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000036', 1, CAST(21141.44 AS Decimal(18, 2)), CAST(N'2016-03-09 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-19 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000036', 2, CAST(21141.44 AS Decimal(18, 2)), CAST(N'2016-03-12 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-19 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000036', 3, CAST(31712.16 AS Decimal(18, 2)), CAST(N'2016-03-15 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-19 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000036', 4, CAST(31712.16 AS Decimal(18, 2)), CAST(N'2016-03-17 00:00:00.000' AS DateTime), N'', CAST(N'2016-05-19 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000037', 1, CAST(197.44 AS Decimal(18, 2)), CAST(N'2016-02-25 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000037', 2, CAST(197.44 AS Decimal(18, 2)), CAST(N'2016-02-28 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000037', 3, CAST(296.16 AS Decimal(18, 2)), CAST(N'2016-03-02 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000037', 4, CAST(296.16 AS Decimal(18, 2)), CAST(N'2016-03-04 00:00:00.000' AS DateTime), N'', CAST(N'2016-04-29 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000038', 1, CAST(160.00 AS Decimal(18, 2)), CAST(N'2016-03-20 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000038', 2, CAST(160.00 AS Decimal(18, 2)), CAST(N'2016-03-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000038', 3, CAST(240.00 AS Decimal(18, 2)), CAST(N'2016-03-26 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000038', 4, CAST(240.00 AS Decimal(18, 2)), CAST(N'2016-03-28 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000039', 1, CAST(677.44 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000039', 2, CAST(677.44 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000039', 3, CAST(1016.16 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000039', 4, CAST(1016.16 AS Decimal(18, 2)), CAST(N'2016-04-11 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000040', 1, CAST(1970.08 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000040', 2, CAST(1970.08 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000040', 3, CAST(2955.12 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000040', 4, CAST(2955.12 AS Decimal(18, 2)), CAST(N'2016-04-11 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000041', 1, CAST(5142.88 AS Decimal(18, 2)), CAST(N'2016-04-02 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000041', 2, CAST(5142.88 AS Decimal(18, 2)), CAST(N'2016-04-05 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000041', 3, CAST(7714.32 AS Decimal(18, 2)), CAST(N'2016-04-08 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000041', 4, CAST(7714.32 AS Decimal(18, 2)), CAST(N'2016-04-10 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000042', 1, CAST(342.88 AS Decimal(18, 2)), CAST(N'2016-04-02 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000042', 2, CAST(342.88 AS Decimal(18, 2)), CAST(N'2016-04-05 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000042', 3, CAST(514.32 AS Decimal(18, 2)), CAST(N'2016-04-08 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000042', 4, CAST(514.32 AS Decimal(18, 2)), CAST(N'2016-04-10 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000043', 1, CAST(36.96 AS Decimal(18, 2)), CAST(N'2016-04-01 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000043', 2, CAST(36.96 AS Decimal(18, 2)), CAST(N'2016-04-04 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000043', 3, CAST(55.44 AS Decimal(18, 2)), CAST(N'2016-04-07 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000043', 4, CAST(55.44 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000045', 1, CAST(1969.92 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-28 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000045', 2, CAST(1969.92 AS Decimal(18, 2)), CAST(N'2016-04-06 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-28 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000045', 3, CAST(2954.88 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-28 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (179, N'123456-000045', 4, CAST(2954.88 AS Decimal(18, 2)), CAST(N'2016-04-11 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-28 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000008', 1, CAST(1.50 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000008', 2, CAST(48.50 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000009', 1, CAST(3.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000009', 2, CAST(97.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000010', 1, CAST(30.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000010', 2, CAST(20.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000010', 3, CAST(50.00 AS Decimal(18, 2)), CAST(N'2016-06-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000011', 1, CAST(45.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000011', 2, CAST(30.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000011', 3, CAST(75.00 AS Decimal(18, 2)), CAST(N'2016-06-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000012', 1, CAST(30.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000012', 2, CAST(20.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000012', 3, CAST(50.00 AS Decimal(18, 2)), CAST(N'2016-06-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000013', 1, CAST(75.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000013', 2, CAST(50.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000013', 3, CAST(125.00 AS Decimal(18, 2)), CAST(N'2016-06-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000014', 1, CAST(30.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000014', 2, CAST(20.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000014', 3, CAST(50.00 AS Decimal(18, 2)), CAST(N'2016-06-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000015', 1, CAST(30.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000015', 2, CAST(20.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000015', 3, CAST(50.00 AS Decimal(18, 2)), CAST(N'2016-06-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000016', 1, CAST(30.00 AS Decimal(18, 2)), CAST(N'2016-04-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000016', 2, CAST(20.00 AS Decimal(18, 2)), CAST(N'2016-05-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000016', 3, CAST(50.00 AS Decimal(18, 2)), CAST(N'2016-06-03 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000017', 1, CAST(8.18 AS Decimal(18, 2)), CAST(N'2016-04-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000017', 2, CAST(5.45 AS Decimal(18, 2)), CAST(N'2016-05-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000017', 3, CAST(13.63 AS Decimal(18, 2)), CAST(N'2016-06-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000018', 1, CAST(84.84 AS Decimal(18, 2)), CAST(N'2016-04-26 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000018', 2, CAST(56.56 AS Decimal(18, 2)), CAST(N'2016-05-26 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000018', 3, CAST(141.40 AS Decimal(18, 2)), CAST(N'2016-06-26 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000019', 1, CAST(9.84 AS Decimal(18, 2)), CAST(N'2016-04-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000019', 2, CAST(6.56 AS Decimal(18, 2)), CAST(N'2016-05-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000019', 3, CAST(16.40 AS Decimal(18, 2)), CAST(N'2016-06-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000020', 1, CAST(118.17 AS Decimal(18, 2)), CAST(N'2016-04-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000020', 2, CAST(78.78 AS Decimal(18, 2)), CAST(N'2016-05-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000020', 3, CAST(196.95 AS Decimal(18, 2)), CAST(N'2016-06-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000021', 1, CAST(8.18 AS Decimal(18, 2)), CAST(N'2016-04-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000021', 2, CAST(5.45 AS Decimal(18, 2)), CAST(N'2016-05-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000021', 3, CAST(13.63 AS Decimal(18, 2)), CAST(N'2016-06-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000022', 1, CAST(9.84 AS Decimal(18, 2)), CAST(N'2016-04-17 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-19 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000022', 2, CAST(6.56 AS Decimal(18, 2)), CAST(N'2016-05-17 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-19 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000022', 3, CAST(16.40 AS Decimal(18, 2)), CAST(N'2016-06-17 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-19 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000023', 1, CAST(0.99 AS Decimal(18, 2)), CAST(N'2016-04-19 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000023', 2, CAST(0.66 AS Decimal(18, 2)), CAST(N'2016-05-19 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000023', 3, CAST(1.65 AS Decimal(18, 2)), CAST(N'2016-06-19 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000024', 1, CAST(14.99 AS Decimal(18, 2)), CAST(N'2016-04-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000024', 2, CAST(9.99 AS Decimal(18, 2)), CAST(N'2016-05-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000024', 3, CAST(24.98 AS Decimal(18, 2)), CAST(N'2016-06-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000025', 1, CAST(8.33 AS Decimal(18, 2)), CAST(N'2016-04-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000025', 2, CAST(5.55 AS Decimal(18, 2)), CAST(N'2016-05-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000025', 3, CAST(13.88 AS Decimal(18, 2)), CAST(N'2016-06-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000026', 1, CAST(1.31 AS Decimal(18, 2)), CAST(N'2016-04-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000026', 2, CAST(0.87 AS Decimal(18, 2)), CAST(N'2016-05-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000026', 3, CAST(2.18 AS Decimal(18, 2)), CAST(N'2016-06-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000027', 1, CAST(8.18 AS Decimal(18, 2)), CAST(N'2016-04-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000027', 2, CAST(5.45 AS Decimal(18, 2)), CAST(N'2016-05-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000027', 3, CAST(13.63 AS Decimal(18, 2)), CAST(N'2016-06-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000028', 1, CAST(131.82 AS Decimal(18, 2)), CAST(N'2016-04-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000028', 2, CAST(87.88 AS Decimal(18, 2)), CAST(N'2016-05-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000028', 3, CAST(219.70 AS Decimal(18, 2)), CAST(N'2016-06-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000029', 1, CAST(8.18 AS Decimal(18, 2)), CAST(N'2016-04-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000029', 2, CAST(5.45 AS Decimal(18, 2)), CAST(N'2016-05-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000029', 3, CAST(13.63 AS Decimal(18, 2)), CAST(N'2016-06-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000030', 1, CAST(9.84 AS Decimal(18, 2)), CAST(N'2016-04-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-10 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000030', 2, CAST(6.56 AS Decimal(18, 2)), CAST(N'2016-05-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-10 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000030', 3, CAST(16.40 AS Decimal(18, 2)), CAST(N'2016-06-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-10 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000031', 1, CAST(11.51 AS Decimal(18, 2)), CAST(N'2016-04-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000031', 2, CAST(7.67 AS Decimal(18, 2)), CAST(N'2016-05-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000031', 3, CAST(19.18 AS Decimal(18, 2)), CAST(N'2016-06-18 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000032', 1, CAST(984.84 AS Decimal(18, 2)), CAST(N'2016-04-19 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000032', 2, CAST(656.56 AS Decimal(18, 2)), CAST(N'2016-05-19 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000032', 3, CAST(1641.40 AS Decimal(18, 2)), CAST(N'2016-06-19 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000033', 1, CAST(9.84 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000033', 2, CAST(6.56 AS Decimal(18, 2)), CAST(N'2016-05-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000033', 3, CAST(16.40 AS Decimal(18, 2)), CAST(N'2016-06-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000034', 1, CAST(9.84 AS Decimal(18, 2)), CAST(N'2016-04-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000034', 2, CAST(6.56 AS Decimal(18, 2)), CAST(N'2016-05-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (190, N'45678-000034', 3, CAST(16.40 AS Decimal(18, 2)), CAST(N'2016-06-09 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (192, N'loan1-000050', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-10-21 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (192, N'loan1-000050', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-11-21 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (192, N'loan1-000051', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-10-21 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (192, N'loan1-000051', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-11-21 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (192, N'loan1-000053', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-10-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (192, N'loan1-000053', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-11-25 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000001', 1, CAST(20000.00 AS Decimal(18, 2)), CAST(N'2016-08-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-23 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000001', 2, CAST(25000.00 AS Decimal(18, 2)), CAST(N'2016-10-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-23 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000001', 3, CAST(35000.00 AS Decimal(18, 2)), CAST(N'2016-11-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-23 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000002', 1, CAST(20000.00 AS Decimal(18, 2)), CAST(N'2016-08-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-04-01 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000002', 2, CAST(25000.00 AS Decimal(18, 2)), CAST(N'2016-10-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-04-01 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000002', 3, CAST(35000.00 AS Decimal(18, 2)), CAST(N'2016-11-23 00:00:00.000' AS DateTime), NULL, CAST(N'2016-04-01 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000003', 1, CAST(40000.00 AS Decimal(18, 2)), CAST(N'2016-08-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000003', 2, CAST(50000.00 AS Decimal(18, 2)), CAST(N'2016-10-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000003', 3, CAST(70000.00 AS Decimal(18, 2)), CAST(N'2016-11-23 00:00:00.000' AS DateTime), N'', CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000004', 1, CAST(8000.00 AS Decimal(18, 2)), CAST(N'2016-08-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000004', 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2016-10-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000004', 3, CAST(14000.00 AS Decimal(18, 2)), CAST(N'2016-11-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000006', 1, CAST(2000.00 AS Decimal(18, 2)), CAST(N'2016-08-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000006', 2, CAST(2500.00 AS Decimal(18, 2)), CAST(N'2016-10-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentBackup] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_payment_details], [pay_date]) VALUES (257, N'12345678901-000006', 3, CAST(3500.00 AS Decimal(18, 2)), CAST(N'2016-11-24 00:00:00.000' AS DateTime), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (179, N'123456-000044', 1, CAST(1954.08 AS Decimal(18, 2)), CAST(N'2016-04-02 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (179, N'123456-000044', 2, CAST(1954.08 AS Decimal(18, 2)), CAST(N'2016-04-05 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (179, N'123456-000044', 3, CAST(2931.12 AS Decimal(18, 2)), CAST(N'2016-04-08 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (179, N'123456-000044', 4, CAST(2931.12 AS Decimal(18, 2)), CAST(N'2016-04-10 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (190, N'45678-000035', 1, CAST(13.17 AS Decimal(18, 2)), CAST(N'2016-04-08 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (190, N'45678-000035', 2, CAST(8.78 AS Decimal(18, 2)), CAST(N'2016-05-08 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (190, N'45678-000035', 3, CAST(21.95 AS Decimal(18, 2)), CAST(N'2016-06-08 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (192, N'loan1-000054', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-10-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (192, N'loan1-000054', 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2016-11-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000005', 1, CAST(1000.00 AS Decimal(18, 2)), CAST(N'2016-08-23 00:00:00.000' AS DateTime), 1, CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000005', 2, CAST(1250.00 AS Decimal(18, 2)), CAST(N'2016-10-23 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000005', 3, CAST(1750.00 AS Decimal(18, 2)), CAST(N'2016-11-23 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000007', 1, CAST(8888.80 AS Decimal(18, 2)), CAST(N'2016-08-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000007', 2, CAST(11111.00 AS Decimal(18, 2)), CAST(N'2016-10-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000007', 3, CAST(15555.40 AS Decimal(18, 2)), CAST(N'2016-11-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000008', 1, CAST(909.00 AS Decimal(18, 2)), CAST(N'2016-08-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000008', 2, CAST(1136.25 AS Decimal(18, 2)), CAST(N'2016-10-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000008', 3, CAST(1590.75 AS Decimal(18, 2)), CAST(N'2016-11-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000009', 1, CAST(1000.00 AS Decimal(18, 2)), CAST(N'2016-08-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000009', 2, CAST(1250.00 AS Decimal(18, 2)), CAST(N'2016-10-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000009', 3, CAST(1750.00 AS Decimal(18, 2)), CAST(N'2016-11-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000010', 1, CAST(600.00 AS Decimal(18, 2)), CAST(N'2016-08-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000010', 2, CAST(750.00 AS Decimal(18, 2)), CAST(N'2016-10-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (257, N'12345678901-000010', 3, CAST(1050.00 AS Decimal(18, 2)), CAST(N'2016-11-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000001', 1, CAST(3000.00 AS Decimal(18, 2)), CAST(N'2016-06-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000001', 2, CAST(3000.00 AS Decimal(18, 2)), CAST(N'2016-09-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000001', 3, CAST(2000.00 AS Decimal(18, 2)), CAST(N'2016-12-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000002', 1, CAST(2400.00 AS Decimal(18, 2)), CAST(N'2016-06-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000002', 2, CAST(2400.00 AS Decimal(18, 2)), CAST(N'2016-09-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000002', 3, CAST(1600.00 AS Decimal(18, 2)), CAST(N'2016-12-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000003', 1, CAST(2566.50 AS Decimal(18, 2)), CAST(N'2016-06-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000003', 2, CAST(2566.50 AS Decimal(18, 2)), CAST(N'2016-09-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000003', 3, CAST(1711.00 AS Decimal(18, 2)), CAST(N'2016-12-24 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000004', 1, CAST(3000.00 AS Decimal(18, 2)), CAST(N'2016-06-01 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000004', 2, CAST(3000.00 AS Decimal(18, 2)), CAST(N'2016-09-01 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[curtailmentSchedule] ([loan_id], [unit_id], [curt_number], [curt_amount], [curt_due_date], [curt_status], [paid_date]) VALUES (263, N'asanka01-000004', 3, CAST(2000.00 AS Decimal(18, 2)), CAST(N'2016-12-01 00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2005, N'HE1', N'1series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2005, N'HE1', N'2series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2005, N'HE2', N'1series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2006, N'HE2', N'2series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2006, N'HE1', N'3series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2005, N'HE1', N'1series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2005, N'HE1', N'2series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2005, N'HE2', N'1series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2006, N'HE2', N'2series')
INSERT [dbo].[heavyEquipmentModelYear] ([year], [make], [model]) VALUES (2006, N'HE1', N'3series')
SET IDENTITY_INSERT [dbo].[interest] ON 

INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (2, CAST(5.000 AS Decimal(5, 3)), N'23', N'payment date to payment date', NULL, 0, 187, 4)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (3, CAST(45.000 AS Decimal(5, 3)), N'0', N'accumulated in prior month', NULL, 0, 181, 4)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (4, CAST(6.000 AS Decimal(5, 3)), N'12', N'accumulated in prior month', NULL, 0, 180, 3)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (5, CAST(10.000 AS Decimal(5, 3)), N'10', N'accumulated in prior month', N'piyumi@gmail.com', 12, 189, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (6, CAST(4.000 AS Decimal(5, 3)), N'12', N'accumulated in prior month', NULL, 0, 190, 2)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (7, CAST(12.000 AS Decimal(5, 3)), N'3', N'accumulated in prior month', N'piyumi@gmail.com', 20, 191, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (8, CAST(5.000 AS Decimal(5, 3)), N'EOM', N'payment date to payment date', NULL, 0, 192, 3)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (9, CAST(23.000 AS Decimal(5, 3)), N'120', N'accumulated in prior month', NULL, 0, 193, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (12, CAST(1.000 AS Decimal(5, 3)), N'1', N'accumulated in prior month', N'abcd@gmail.com', 1, 200, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (13, CAST(3.000 AS Decimal(5, 3)), N'2', N'accumulated in prior month', N'piyumi@gmail.com', 13, 201, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (14, CAST(12.000 AS Decimal(5, 3)), N'2', N'payment date to payment date', NULL, 0, 202, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (15, CAST(1.000 AS Decimal(5, 3)), N'1', N'accumulated in prior month', NULL, 0, 203, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (16, CAST(10.000 AS Decimal(5, 3)), N'13', N'accumulated in prior month', N'asa@gmail.com', 7, 204, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (17, CAST(2.000 AS Decimal(5, 3)), N'EOM', N'payment date to payment date', N'kasun2030@gmail.com', 3, 205, 3)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (18, CAST(13.000 AS Decimal(5, 3)), N'1', N'payment date to payment date', NULL, 0, 219, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (19, CAST(4.000 AS Decimal(5, 3)), N'3', N'accumulated in prior month', NULL, 0, 227, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (20, CAST(3.000 AS Decimal(5, 3)), N'10', N'accumulated in prior month', NULL, 0, 225, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (21, CAST(12.000 AS Decimal(5, 3)), N'2', N'accumulated in prior month', NULL, 0, 233, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (22, CAST(11.000 AS Decimal(5, 3)), N'12', N'accumulated in prior month', NULL, 0, 234, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (23, CAST(52.000 AS Decimal(5, 3)), N'15', N'payment date to payment date', NULL, 23213, 238, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (24, CAST(35.000 AS Decimal(5, 3)), N'EOM', N'accumulated in prior month', N'abcde@gghgh.com', 12, 239, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (25, CAST(2.000 AS Decimal(5, 3)), N'2', N'accumulated in prior month', NULL, 0, 240, 3)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (26, CAST(13.000 AS Decimal(5, 3)), N'EOM', N'payment date to payment date', N'ads@fer.ty', 33, 241, 2)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (27, CAST(5.567 AS Decimal(5, 3)), N'4', N'payment date to payment date', N'ads@fer.ty', 3, 242, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (28, CAST(4.456 AS Decimal(5, 3)), N'2', N'accumulated in prior month', NULL, 0, 243, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (29, CAST(0.600 AS Decimal(5, 3)), N'3', N'accumulated in prior month', N'piyumi@gmail.com', 12, 247, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (30, CAST(5.000 AS Decimal(5, 3)), N'9', N'payment date to payment date', NULL, 0, 249, 3)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (31, CAST(12.678 AS Decimal(5, 3)), N'7', N'payment date to payment date', NULL, 0, 198, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (32, CAST(6.000 AS Decimal(5, 3)), N'8', N'payment date to payment date', N'kasun2030@gmail.com', 6, 250, 3)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (33, CAST(10.000 AS Decimal(5, 3)), N'8', N'payment date to payment date', N'test@gmail.com', 5, 252, 2)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (34, CAST(11.000 AS Decimal(5, 3)), N'2', N'accumulated in prior month', NULL, 0, 257, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (35, CAST(50.000 AS Decimal(5, 3)), N'12', N'accumulated in prior month', NULL, 0, 260, 2)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (36, CAST(5.000 AS Decimal(5, 3)), N'5', N'accumulated in prior month', NULL, 0, 261, 1)
INSERT [dbo].[interest] ([interest_id], [interest_rate], [paid_date], [payment_period], [auto_remind_email], [auto_remind_period], [loan_id], [accrual_method_id]) VALUES (37, CAST(10.000 AS Decimal(5, 3)), N'12', N'payment date to payment date', NULL, 0, 263, 1)
SET IDENTITY_INSERT [dbo].[interest] OFF
SET IDENTITY_INSERT [dbo].[justAddedUnit] ON 

INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (339, 154, N'Electra', CAST(8000.00 AS Decimal(12, 2)), 1, 263, N'asanka01-000001')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (184, 149, N'Fillmore', CAST(30920.00 AS Decimal(12, 2)), 0, 225, N'3535659-000002')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (190, 150, N'1 Series', CAST(11100.00 AS Decimal(12, 2)), 0, 225, N'3535659-000005')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (189, 150, N'Fillmore', CAST(381800.00 AS Decimal(12, 2)), 0, 225, N'3535659-000004')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (191, 150, N'Mini', CAST(1080.00 AS Decimal(12, 2)), 0, 225, N'3535659-000006')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (242, 172, N'Mercury', CAST(9.60 AS Decimal(12, 2)), 1, 248, N'Dealer0001-000021')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (340, 154, N'Cam', CAST(6400.00 AS Decimal(12, 2)), 1, 263, N'asanka01-000002')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (243, 172, N'Poseidon', CAST(27.20 AS Decimal(12, 2)), 1, 248, N'Dealer0001-000022')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (341, 154, N'asdas', CAST(6844.00 AS Decimal(12, 2)), 1, 263, N'asanka01-000003')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (338, 57, N'SDA', CAST(9849.60 AS Decimal(12, 2)), 1, 179, N'123456-000045')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (337, 57, N'CModel', CAST(9770.40 AS Decimal(12, 2)), 1, 179, N'123456-000044')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (342, 154, N'Mercury', CAST(8000.00 AS Decimal(12, 2)), 1, 263, N'asanka01-000004')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (290, 61, N'600', CAST(100.00 AS Decimal(12, 2)), 1, 190, N'45678-000015')
INSERT [dbo].[justAddedUnit] ([just_added_unit_id], [user_id], [model], [advance_amount], [is_advanced], [loan_id], [unit_id]) VALUES (291, 61, N'600', CAST(100.00 AS Decimal(12, 2)), 1, 190, N'45678-000016')
SET IDENTITY_INSERT [dbo].[justAddedUnit] OFF
SET IDENTITY_INSERT [dbo].[loan] ON 

INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (179, N'123456', N'-123456', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-02-15 00:00:00.000' AS DateTime), CAST(N'2018-02-15 00:00:00.000' AS DateTime), 80, 1, N'Invoice/Check', 360, N'd', 1, 3, 1, CAST(N'2016-02-15 09:41:15.087' AS DateTime), NULL, 0, NULL, NULL, NULL, N'email@gmail.comm', 30, 20, N'28', NULL, NULL, N'a')
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (180, N'loan789', N'LEN01_01-loan789', CAST(1000.00 AS Decimal(18, 2)), CAST(N'2016-02-15 00:00:00.000' AS DateTime), CAST(N'2017-02-15 00:00:00.000' AS DateTime), 5, 0, N'Invoice/Check', 12, N'm', 1, 1, 1, CAST(N'2016-02-15 15:30:40.560' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 22, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (181, N'abc123', N'ABC02_01-abc123', CAST(45.77 AS Decimal(18, 2)), CAST(N'2016-02-15 00:00:00.000' AS DateTime), CAST(N'2016-03-25 00:00:00.000' AS DateTime), 4, 0, N'Invoice/Check', 0, N'd', 1, 3, 0, CAST(N'2016-02-15 15:39:00.193' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 21, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (182, N'11111', N'COM05_01-11111', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-02-17 00:00:00.000' AS DateTime), CAST(N'2017-02-16 00:00:00.000' AS DateTime), 10, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-02-17 00:38:43.303' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dsf@yy.com', 12, 23, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (183, N'sfsdfgfds', N'-sfsdfgfds', CAST(1231.00 AS Decimal(18, 2)), CAST(N'2016-02-18 00:00:00.000' AS DateTime), CAST(N'2016-02-27 00:00:00.000' AS DateTime), 12, 0, N'Invoice/Check', 2, N'd', 0, 4, 0, CAST(N'2016-02-18 08:25:26.707' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 20, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (184, N'00001', N'COM06_01-00001', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-02-18 00:00:00.000' AS DateTime), CAST(N'2018-02-18 00:00:00.000' AS DateTime), 85, 1, N'Auto Deduct/Deposit', 20, N'm', 0, 1, 0, CAST(N'2016-02-18 08:35:36.587' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 24, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (187, N'00001', N'COM04_01-00001', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-02-18 00:00:00.000' AS DateTime), CAST(N'2019-02-15 00:00:00.000' AS DateTime), 12, 0, N'Auto Deduct/Deposit', 23, N'm', 1, 7, 0, CAST(N'2016-02-18 16:31:51.367' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 25, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (188, N'00002', N'COM04_01-00002', CAST(20.00 AS Decimal(18, 2)), CAST(N'2016-02-02 00:00:00.000' AS DateTime), CAST(N'2016-02-25 00:00:00.000' AS DateTime), 21, 0, N'Invoice/Check', 3, N'd', 0, 1, 0, CAST(N'2016-02-23 11:23:32.870' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 25, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (189, N'testloan1', N'-testloan1', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-02-25 00:00:00.000' AS DateTime), CAST(N'2017-02-28 00:00:00.000' AS DateTime), 10, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 1, CAST(N'2016-02-29 18:05:54.183' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 22, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (190, N'45678', N'LEN01_01-45678', CAST(10000.00 AS Decimal(18, 2)), CAST(N'2016-03-03 00:00:00.000' AS DateTime), CAST(N'2017-03-22 00:00:00.000' AS DateTime), 5, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 1, CAST(N'2016-03-02 08:46:04.793' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 22, NULL, NULL, NULL, N'a')
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (191, N'34545', N'LEN01_01-34545', CAST(34345353.00 AS Decimal(18, 2)), CAST(N'2016-03-03 00:00:00.000' AS DateTime), CAST(N'2017-03-07 00:00:00.000' AS DateTime), 54, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 3, 1, CAST(N'2016-03-03 08:52:58.710' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 22, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (192, N'loan1', N'DAR01_01-loan1', CAST(50000.00 AS Decimal(18, 2)), CAST(N'2016-03-03 00:00:00.000' AS DateTime), CAST(N'2017-05-27 00:00:00.000' AS DateTime), 75, 1, N'Auto Deduct/Deposit', 10, N'm', 1, 3, 1, CAST(N'2016-03-03 08:55:33.000' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 26, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (193, N'fsdfsdf', N'-fsdfsdf', CAST(123123.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-23 00:00:00.000' AS DateTime), 12, 0, N'Auto Deduct/Deposit', 120, N'd', 1, 1, 0, CAST(N'2016-03-03 00:44:47.770' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 27, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (196, N'kas1', N'D02_01-loan1', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-23 00:00:00.000' AS DateTime), 23, 1, N'Invoice/Check', 12, N'c', 1, 2, 1, CAST(N'2016-03-03 00:00:00.000' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 28, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (197, N'rewre', N'TTT01_01-rewre', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-08 00:00:00.000' AS DateTime), CAST(N'2017-03-08 00:00:00.000' AS DateTime), 10, 0, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-08 22:50:54.283' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 47, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (198, N'78798709809', N'COM16_01-78798709809', CAST(6587676.00 AS Decimal(18, 2)), CAST(N'2016-02-02 00:00:00.000' AS DateTime), CAST(N'2016-03-19 00:00:00.000' AS DateTime), 6, 1, N'Auto Deduct/Deposit', 7, N'd', 1, 6, 0, CAST(N'2016-03-09 08:20:46.153' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 48, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (199, N'11111111111111111111111111111', N'COM25_01-11111111111111111111111111111', CAST(1223.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2016-03-30 00:00:00.000' AS DateTime), 1, 0, N'Invoice/Check', 23, N'd', 1, 1, 0, CAST(N'2016-03-09 14:01:33.570' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 49, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (200, N'1122232', N'DEA03_01-1122232', CAST(2000.00 AS Decimal(18, 2)), CAST(N'2016-03-09 00:00:00.000' AS DateTime), CAST(N'2016-03-11 00:00:00.000' AS DateTime), 2, 1, N'Auto Deduct/Deposit', 2, N'd', 1, 1, 0, CAST(N'2016-03-09 15:03:37.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'abcd@gmail.com', 1, 51, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (201, N'3252525', N'LEN03_01-3252525', CAST(23000.00 AS Decimal(18, 2)), CAST(N'2016-03-09 00:00:00.000' AS DateTime), CAST(N'2017-03-08 00:00:00.000' AS DateTime), 10, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-03-09 16:10:15.557' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 52, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (202, N'6778578', N'LEN03_01-6778578', CAST(50000.00 AS Decimal(18, 2)), CAST(N'2016-03-09 00:00:00.000' AS DateTime), CAST(N'2017-03-29 00:00:00.000' AS DateTime), 8, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-03-09 17:43:47.657' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 52, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (203, N'353565', N'LEN03_01-353565', CAST(120000.00 AS Decimal(18, 2)), CAST(N'2016-03-09 00:00:00.000' AS DateTime), CAST(N'2017-03-08 00:00:00.000' AS DateTime), 5, 1, N'Auto Deduct/Deposit', 10, N'm', 1, 1, 0, CAST(N'2016-03-09 18:11:09.617' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 53, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (204, N'Loan01', N'TTT01_01-Loan01', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-10 00:00:00.000' AS DateTime), 10, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-03-10 07:57:59.830' AS DateTime), NULL, 0, NULL, NULL, NULL, N'asa@gmail.com', 7, 47, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (205, N'kas123', N'DAR02_01-kas123', CAST(200000.00 AS Decimal(18, 2)), CAST(N'2016-03-09 00:00:00.000' AS DateTime), CAST(N'2016-09-16 00:00:00.000' AS DateTime), 90, 1, N'Invoice/Check', 5, N'm', 0, 4, 0, CAST(N'2016-03-10 12:57:15.843' AS DateTime), NULL, 0, NULL, NULL, NULL, N'kasun2030@gmail.com', 2, 58, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (206, N'cool01', N'COO01_01-cool01', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 16:01:28.340' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (207, N'3324324', N'COO01_01-3324324', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 16:47:40.717' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (208, N'546565', N'COO01_02-546565', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 10, 1, N'Invoice/Check', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 16:50:09.663' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (209, N'5453tytr', N'COO01_02-5453tytr', CAST(5654654.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2016-03-31 00:00:00.000' AS DateTime), 90, 1, N'Auto Deduct/Deposit', 10, N'd', 0, 1, 0, CAST(N'2016-03-10 17:03:39.670' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (210, N'3535653', N'LEN05_01-3535653', CAST(60000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-08 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 10, N'm', 1, 1, 0, CAST(N'2016-03-10 17:33:25.057' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 75, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (211, N'343234', N'COO01_01-343234', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 10, 1, N'Invoice/Check', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 18:22:32.750' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (212, N'654645', N'COO01_02-654645', CAST(1540000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 10, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 18:27:07.997' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 67, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (213, N'rewrew', N'COO01_01-rewrew', CAST(10000000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-30 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 18:51:24.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'sda@gmail.com', 7, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (214, N'yrtyr', N'COO01_02-yrtyr', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 18:53:50.820' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (215, N'rterte', N'COO01_02-rterte', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 80, 1, N'Invoice/Check', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 21:31:46.497' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (216, N'fgdfgfd', N'COO01_02-fgdfgfd', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 21:33:13.373' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (217, N'rytyt', N'COO01_01-rytyt', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 22:46:03.997' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (218, N'erwer', N'ASA02_01-erwer', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 10, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 23:02:29.153' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 76, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (219, N'111111111111111111111111111111', N'COM31_01-111111111111111111111111111111', CAST(1314324.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2016-03-17 00:00:00.000' AS DateTime), 12, 0, N'Auto Deduct/Deposit', 1, N'd', 1, 6, 1, CAST(N'2016-03-11 08:40:28.297' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aasw@dd.hyg', 6, 61, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (220, N'89568', N'BEL01_02-89568', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2015-01-01 00:00:00.000' AS DateTime), CAST(N'2017-01-01 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-10 20:28:59.357' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 78, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (221, N'rwerwer', N'ASA02_01-rwerwer', CAST(4324.00 AS Decimal(18, 2)), CAST(N'2016-03-10 00:00:00.000' AS DateTime), CAST(N'2016-03-21 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 10, N'd', 0, 1, 1, CAST(N'2016-03-10 22:08:19.467' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 76, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (222, N'21321', N'COM30_01-21321', CAST(32432.00 AS Decimal(18, 2)), CAST(N'2016-03-14 00:00:00.000' AS DateTime), CAST(N'2016-03-24 00:00:00.000' AS DateTime), 12, 0, N'Auto Deduct/Deposit', 1, N'd', 0, 4, 0, CAST(N'2016-03-11 12:24:13.767' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 59, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (223, N'6356536', N'DEA04_01-6356536', CAST(70000.00 AS Decimal(18, 2)), CAST(N'2016-03-18 00:00:00.000' AS DateTime), CAST(N'2017-03-29 00:00:00.000' AS DateTime), 60, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-03-11 14:07:47.600' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 79, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (224, N'3535650', N'DEA04_01-3535650', CAST(80000.00 AS Decimal(18, 2)), CAST(N'2016-03-30 00:00:00.000' AS DateTime), CAST(N'2017-04-30 00:00:00.000' AS DateTime), 8, 1, N'Invoice/Check', 12, N'm', 1, 1, 0, CAST(N'2016-03-11 14:27:07.497' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 79, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (225, N'3535659', N'DEA04_01-3535659', CAST(600000.00 AS Decimal(18, 2)), CAST(N'2016-03-15 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 90, 1, N'Auto Deduct/Deposit', 10, N'm', 1, 1, 1, CAST(N'2016-03-11 14:36:43.460' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 79, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (226, N'rtret', N'BAK01_01-rtret', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-03-12 00:00:00.000' AS DateTime), CAST(N'2017-03-11 00:00:00.000' AS DateTime), 1, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 1, CAST(N'2016-03-11 14:45:47.077' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 81, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (227, N'463777', N'DEA04_02-463777', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), CAST(N'2017-04-30 00:00:00.000' AS DateTime), 90, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 3, 1, CAST(N'2016-03-11 14:50:25.427' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 80, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (228, N'5737873', N'DEA05_01-5737873', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-24 00:00:00.000' AS DateTime), CAST(N'2017-03-24 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 1, CAST(N'2016-03-11 17:16:13.360' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 83, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (229, N'loanfull', N'BAK01_01-loanfull', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 0, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 1, CAST(N'2016-03-11 20:17:55.350' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 81, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (230, N'11111111111111111111111111111', N'COM31_01-11111111111111111111111111111', CAST(13224.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2016-03-25 00:00:00.000' AS DateTime), 12, 0, N'Auto Deduct/Deposit', 2, N'd', 0, 2, 1, CAST(N'2016-03-11 20:35:56.823' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 61, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (231, N'werwerwer', N'BAK01_01-werwerwer', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-24 00:00:00.000' AS DateTime), 80, 0, N'Invoice/Check', 12, N'm', 1, 3, 0, CAST(N'2016-03-12 08:37:10.730' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 81, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (232, N'darshana1', N'DAR03_01-darshana1', CAST(45000.00 AS Decimal(18, 2)), CAST(N'2016-03-14 00:00:00.000' AS DateTime), CAST(N'2016-05-12 00:00:00.000' AS DateTime), 77, 1, N'Invoice/Check', 2, N'm', 0, 4, 1, CAST(N'2016-03-12 11:36:10.357' AS DateTime), NULL, 0, NULL, NULL, NULL, N'kasunggg@gmail.com', 4, 87, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (233, N'426466346', N'EWT01_01-426466346', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), CAST(N'2017-04-30 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 2, 0, CAST(N'2016-03-12 11:42:23.133' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 86, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (234, N'32123', N'DEH01_01-32123', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 0, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-03-12 11:43:25.007' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 88, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (235, N'111111111111111111111111111111', N'COM32_02-111111111111111111111111111111', CAST(243124.00 AS Decimal(18, 2)), CAST(N'2016-03-07 00:00:00.000' AS DateTime), CAST(N'2016-03-31 00:00:00.000' AS DateTime), 12, 0, N'Auto Deduct/Deposit', 12, N'd', 0, 4, 0, CAST(N'2016-03-12 11:51:50.897' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 82, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (236, N'35356501', N'EWT01_01-35356501', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-23 00:00:00.000' AS DateTime), CAST(N'2016-03-31 00:00:00.000' AS DateTime), 30, 1, N'Auto Deduct/Deposit', 5, N'd', 0, 2, 0, CAST(N'2016-03-12 12:19:31.083' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 86, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (237, N'hhhh11', N'DEH01_01-hhhh11', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 0, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 1, CAST(N'2016-03-12 12:19:56.347' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 88, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (238, N'fdsfsd', N'TFN02_01-fdsfsd', CAST(4324344.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 0, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 1, CAST(N'2016-03-12 01:44:59.310' AS DateTime), NULL, 0, NULL, NULL, NULL, N'sdfsd@gmail.com', 33, 89, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (239, N'5775774', N'DAP02_01-5775774', CAST(7000000.00 AS Decimal(18, 2)), CAST(N'2016-03-23 00:00:00.000' AS DateTime), CAST(N'2018-03-20 00:00:00.000' AS DateTime), 100, 1, N'Auto Deduct/Deposit', 14, N'm', 1, 4, 1, CAST(N'2016-03-12 15:37:24.970' AS DateTime), NULL, 0, NULL, NULL, NULL, N'abcde@gghgh.com', 12, 90, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (240, N'3535653', N'DAP02_01-3535653', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), CAST(N'2016-04-30 00:00:00.000' AS DateTime), 36, 1, N'Auto Deduct/Deposit', 10, N'd', 1, 4, 1, CAST(N'2016-03-12 16:04:06.260' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 90, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (241, N'111111111111111111111111111111', N'COM33_01-111111111111111111111111111111', CAST(1321323.00 AS Decimal(18, 2)), CAST(N'2016-03-06 00:00:00.000' AS DateTime), CAST(N'2016-03-31 00:00:00.000' AS DateTime), 7, 1, N'Auto Deduct/Deposit', 12, N'd', 1, 5, 1, CAST(N'2016-03-12 16:28:42.943' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ads@fer.ty', 12, 91, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (242, N'65373577', N'DAP03_01-65373577', CAST(2000000.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), CAST(N'2021-03-31 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 13, N'm', 1, 2, 0, CAST(N'2016-03-12 17:17:27.107' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 92, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (243, N'353565545', N'DAP03_01-353565545', CAST(34345353.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), CAST(N'2016-04-30 00:00:00.000' AS DateTime), 44, 1, N'Auto Deduct/Deposit', 3, N'd', 1, 2, 0, CAST(N'2016-03-12 17:39:08.383' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 92, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (244, N'1234567890', N'TRE01_02-1234567890', CAST(241324.00 AS Decimal(18, 2)), CAST(N'2016-04-13 00:00:00.000' AS DateTime), CAST(N'2016-05-19 00:00:00.000' AS DateTime), 40, 1, N'Auto Deduct/Deposit', 1, N'm', 0, 3, 0, CAST(N'2016-03-13 10:07:28.773' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 93, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (245, N'1234567891', N'TRE01_02-1234567891', CAST(321321243.00 AS Decimal(18, 2)), CAST(N'2016-04-14 00:00:00.000' AS DateTime), CAST(N'2016-07-22 00:00:00.000' AS DateTime), 12, 0, N'Invoice/Check', 23, N'd', 0, 5, 1, CAST(N'2016-03-13 12:20:05.650' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 94, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (246, N'12345', N'DAR01_01-12345', CAST(50000.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-04-01 00:00:00.000' AS DateTime), 60, 0, N'Auto Deduct/Deposit', 9, N'm', 0, 1, 0, CAST(N'2016-03-13 18:20:57.170' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 26, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (247, N'35356532', N'DEA06_01-35356532', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-22 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 90, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 2, 1, CAST(N'2016-03-14 08:04:23.370' AS DateTime), NULL, 0, NULL, NULL, NULL, N'abcd@gmail.com', 23, 96, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (248, N'Dealer0001', N'FIS01_01-Dealer0001', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-15 00:00:00.000' AS DateTime), CAST(N'2017-03-16 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 11, N'm', 0, 5, 1, CAST(N'2016-03-14 08:23:56.717' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 97, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (249, N'LoanNum1', N'KAS02_01-LoanNum1', CAST(767677.00 AS Decimal(18, 2)), CAST(N'2016-03-15 00:00:00.000' AS DateTime), CAST(N'2017-05-12 00:00:00.000' AS DateTime), 77, 1, N'Invoice/Check', 99, N'd', 1, 6, 1, CAST(N'2016-03-14 09:19:50.450' AS DateTime), NULL, 0, NULL, NULL, NULL, N'kasunkasun@gmail.com', 5, 98, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (250, N'forme', N'KAS02_01-forme', CAST(45000.00 AS Decimal(18, 2)), CAST(N'2016-03-15 00:00:00.000' AS DateTime), CAST(N'2016-10-20 00:00:00.000' AS DateTime), 78, 1, N'Auto Deduct/Deposit', 88, N'd', 1, 4, 0, CAST(N'2016-03-14 10:34:57.070' AS DateTime), NULL, 0, NULL, NULL, NULL, N'kasun2030@gmail.com', 67, 98, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (251, N'213456789', N'TRE01_02-213456789', CAST(54345.00 AS Decimal(18, 2)), CAST(N'2016-04-14 00:00:00.000' AS DateTime), CAST(N'2016-05-27 00:00:00.000' AS DateTime), 23, 0, N'Auto Deduct/Deposit', 1, N'm', 0, 5, 1, CAST(N'2016-03-15 17:35:33.833' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (252, N'000253', N'BAK01_01-000253', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-17 00:00:00.000' AS DateTime), CAST(N'2016-03-31 00:00:00.000' AS DateTime), 5, 0, N'Auto Deduct/Deposit', 3, N'd', 1, 1, 0, CAST(N'2016-03-17 00:16:19.977' AS DateTime), NULL, 0, NULL, NULL, NULL, N'test@gmail.com', 2, 81, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (253, N'6346466', N'DEA06_01-6346466', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-22 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 2, 0, CAST(N'2016-03-21 16:47:42.823' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 96, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (254, N'1234567', N'DEA06_01-1234567', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-21 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 1, N'Invoice/Check', 12, N'm', 1, 3, 0, CAST(N'2016-03-21 16:50:36.887' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 96, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (255, N'asanka1', N'COO01_01-asanka1', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 0, 1, 0, CAST(N'2016-03-22 13:13:14.233' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (256, N'123456790', N'LEN06_01-123456790', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), CAST(N'2018-03-31 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 15, N'm', 1, 3, 0, CAST(N'2016-03-23 08:31:56.420' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 99, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (257, N'12345678901', N'LEN06_01-12345678901', CAST(500000.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime), CAST(N'2018-04-30 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 20, N'm', 1, 3, 1, CAST(N'2016-03-23 08:41:48.040' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 99, N'EoM', N'piyumi@gmail.com', 5, N'f')
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (258, N'tyrtyr', N'COO01_01-tyrtyr', CAST(456465464.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-31 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-03-24 03:38:07.663' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (259, N'erere', N'TTT01_01-erere', CAST(212321323.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-01 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 3, 0, CAST(N'2016-03-24 16:29:04.193' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ASA@GMAIL.COM', 2, 47, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (260, N'rtret', N'COO01_01-rtret', CAST(321312213.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-01 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 0, CAST(N'2016-03-24 04:05:50.007' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (261, N'0220225', N'COO01_02-0220225', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2016-03-15 00:00:00.000' AS DateTime), CAST(N'2017-11-30 00:00:00.000' AS DateTime), 5, 1, N'Auto Deduct/Deposit', 5, N'm', 1, 1, 0, CAST(N'2016-03-24 04:13:36.107' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 67, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (262, N'323324', N'BAK01_01-323324', CAST(12121.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-01 00:00:00.000' AS DateTime), 54, 0, N'Auto Deduct/Deposit', 12, N'm', 0, 3, 0, CAST(N'2016-03-24 17:15:32.463' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 81, NULL, NULL, NULL, NULL)
INSERT [dbo].[loan] ([loan_id], [loan_number], [loan_code], [loan_amount], [start_date], [maturity_date], [advance], [is_edit_allowable], [payment_method], [pay_off_period], [pay_off_type], [is_interest_calculate], [default_unit_type], [loan_status], [created_date], [modified_date], [is_delete], [has_advance_fee], [has_monthly_loan_fee], [has_lot_inspection_fee], [auto_remind_email], [auto_remind_period], [non_reg_branch_id], [curtailment_due_date], [curtailment_auto_remind_email], [curtailment_remind_period], [curtailment_calculation_type]) VALUES (263, N'asanka01', N'BAK01_01-asanka01', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2016-03-01 00:00:00.000' AS DateTime), CAST(N'2017-03-02 00:00:00.000' AS DateTime), 80, 1, N'Auto Deduct/Deposit', 12, N'm', 1, 1, 1, CAST(N'2016-03-24 18:43:14.643' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL, 0, 81, N'10', N'asa@gmail.com', 10, N'f')
SET IDENTITY_INSERT [dbo].[loan] OFF
SET IDENTITY_INSERT [dbo].[loanDetails] ON 

INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (1, CAST(820.00 AS Decimal(18, 2)), CAST(180.00 AS Decimal(18, 2)), 187)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (7, CAST(500001.00 AS Decimal(18, 2)), CAST(1856701.00 AS Decimal(18, 2)), 184)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (8, CAST(0.00 AS Decimal(18, 2)), CAST(779.00 AS Decimal(18, 2)), 180)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (9, CAST(182332.00 AS Decimal(18, 2)), CAST(-125500.00 AS Decimal(18, 2)), 189)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (11, CAST(33951.70 AS Decimal(18, 2)), CAST(5424306.67 AS Decimal(18, 2)), 192)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (12, CAST(380866.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 179)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (13, CAST(2144.20 AS Decimal(18, 2)), CAST(10000.00 AS Decimal(18, 2)), 190)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (14, CAST(3391.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 226)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (15, CAST(59497.80 AS Decimal(18, 2)), CAST(1600.00 AS Decimal(18, 2)), 229)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (16, CAST(0.00 AS Decimal(18, 2)), CAST(1087692.00 AS Decimal(18, 2)), 225)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (17, CAST(50400.00 AS Decimal(18, 2)), CAST(88000.00 AS Decimal(18, 2)), 237)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (18, CAST(8103.00 AS Decimal(18, 2)), CAST(26546.11 AS Decimal(18, 2)), 232)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (19, CAST(400000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 239)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (20, CAST(8624.91 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 241)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (21, CAST(297318.26 AS Decimal(18, 2)), CAST(-2700.46 AS Decimal(18, 2)), 247)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (27, CAST(0.00 AS Decimal(18, 2)), CAST(3000.00 AS Decimal(18, 2)), 182)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (30, CAST(36.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 248)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (31, CAST(42.35 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 249)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (32, CAST(397191.20 AS Decimal(18, 2)), CAST(-77600.00 AS Decimal(18, 2)), 257)
INSERT [dbo].[loanDetails] ([loan_details_id], [used_amount], [pending_amount], [loan_id]) VALUES (33, CAST(29244.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 263)
SET IDENTITY_INSERT [dbo].[loanDetails] OFF
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (50, 72, 48, 198, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (59, 77, 49, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (47, 78, 50, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (37, 63, 63, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (60, 77, 49, 199, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (62, 79, 51, 200, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (65, 83, 53, 203, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (63, 81, 81, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (70, 89, 56, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (67, 88, 57, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (72, 92, 58, 205, 4)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (66, 85, 85, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 102, 81, 252, 4)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (74, 94, 62, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (66, 86, 67, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (74, 94, 75, 210, 2)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (71, 90, 90, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (66, 86, 0, 216, 2)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (66, 85, 55, 217, 2)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (50, 77, 48, 245, 2)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (76, 98, 77, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (76, 99, 78, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (75, 107, 84, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (73, 93, 93, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (82, 108, 85, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (86, 112, 89, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 111, 88, 234, 3)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (90, 116, 94, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (92, 118, 95, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (95, 121, 98, 250, 3)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (90, 116, 116, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (37, 63, 47, 259, 2)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 124, 99, 256, 2)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 86, 67, 261, 3)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (65, 84, 52, 201, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (84, 110, 110, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 115, 92, 242, 3)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 119, 96, 253, 2)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (66, 86, 86, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (84, 110, 86, 233, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (89, 115, 92, 243, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 55, 26, 246, 5)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (90, 117, 117, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 85, 55, 255, 3)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (93, 120, 97, NULL, 1)
INSERT [dbo].[loanSetupStep] ([company_id], [branch_id], [non_registered_branch_id], [loan_id], [step_number]) VALUES (0, 104, 82, 235, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (179, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (179, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (179, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (179, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (179, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (180, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (181, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (182, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (182, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (182, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (183, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (184, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (187, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (187, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (188, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (189, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (189, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (189, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (190, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (191, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (192, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (193, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (193, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (193, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (193, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (197, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (197, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (198, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (199, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (200, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (201, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (202, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (203, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (204, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (204, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (204, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (205, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (205, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (205, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (205, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (205, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (206, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (206, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (207, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (207, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (208, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (208, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (209, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (210, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (210, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (210, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (211, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (211, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (212, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (212, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (213, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (213, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (214, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (214, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (215, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (215, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (216, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (216, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (216, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (217, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (217, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (218, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (218, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (218, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (219, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (219, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (220, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (220, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (221, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (222, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (223, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (223, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (223, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (224, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (224, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (224, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (225, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (225, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (225, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (226, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (226, 5)
GO
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (227, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (227, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (227, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (228, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (228, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (228, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (228, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (229, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (230, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (230, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (231, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (231, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (231, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (231, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (232, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (232, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (232, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (232, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (232, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (232, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (233, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (234, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (234, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (234, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (234, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (234, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (234, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (234, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (235, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (235, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (236, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (236, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (236, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (237, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (237, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (237, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (237, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (237, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (238, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (239, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (239, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (239, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (239, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (239, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (240, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (241, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (242, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (243, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (243, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (244, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (244, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (245, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (245, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (246, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (246, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (247, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (247, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (247, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (247, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (247, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 5)
GO
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (248, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (249, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (250, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (251, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (251, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (251, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (252, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (252, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (252, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (253, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (253, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (253, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (253, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (254, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (254, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (254, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (255, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (256, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (256, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (256, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (256, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (257, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (257, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (257, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (257, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (258, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (259, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (260, 8)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (261, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (261, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (262, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (262, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (262, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 1)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 2)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 3)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 4)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 5)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 6)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 7)
INSERT [dbo].[loanUnitType] ([loan_id], [unit_type_id]) VALUES (263, 8)
SET IDENTITY_INSERT [dbo].[lotInspectionFee] ON 

INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (41, CAST(50.00 AS Decimal(15, 2)), 0, N'Random', N'7', NULL, 0, N'emal@gmail.com', 0, 179)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (43, CAST(1200.00 AS Decimal(15, 2)), 1, N'Quaterly', NULL, N'', 0, N'', 0, 189)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (44, CAST(96.00 AS Decimal(15, 2)), 1, N'Quaterly', NULL, N'', 0, N'', 0, 204)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (45, CAST(33.00 AS Decimal(15, 2)), 0, N'Random', NULL, N'', 0, N'', 0, 218)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (47, CAST(575.00 AS Decimal(15, 2)), 0, N'Monthly', N'18', N'', 0, N'jfjf@fhfgh.com', 3, 228)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (48, CAST(8787878.00 AS Decimal(15, 2)), 0, N'Monthly', N'16', N'kasun2030@gmail.com', 7, N'kasun2030@gmail.com', 9, 205)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (49, CAST(50.00 AS Decimal(15, 2)), 0, N'Monthly', N'15', N'', 0, N'', 0, 229)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (50, CAST(5477.00 AS Decimal(15, 2)), 0, N'Monthly', N'19', N'', 0, N'', 0, 233)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (51, CAST(50.00 AS Decimal(15, 2)), 0, N'Monthly', N'15', N'', 0, N'', 0, 237)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (52, CAST(43243.00 AS Decimal(15, 2)), 0, N'Annually', NULL, N'', 0, N'', 0, 238)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (53, CAST(123.00 AS Decimal(15, 2)), 0, N'Monthly', N'EoM', N'', 0, N'abcde@gghgh.com', 12, 239)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (54, CAST(213123.00 AS Decimal(15, 2)), 0, N'Random', NULL, N'', 0, N'ewqq@fewf.hgjh', 123, 244)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (55, CAST(1132.00 AS Decimal(15, 2)), 0, N'Monthly', N'18', N'', 0, N'abcd@gmail.com', 4, 247)
INSERT [dbo].[lotInspectionFee] ([lot_inspection_fee_id], [lot_inspection_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (56, CAST(76767.00 AS Decimal(15, 2)), 0, N'Monthly', N'17', N'', 0, N'kasunkasun@gmail.com', 9, 249)
SET IDENTITY_INSERT [dbo].[lotInspectionFee] OFF
SET IDENTITY_INSERT [dbo].[monthlyLoanFee] ON 

INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (37, CAST(10.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'TOA', NULL, 0, N'emal@gmail.com', 0, 179)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (39, CAST(800.00 AS Decimal(15, 2)), 1, N'Time of Advance', N'ToA', N'', 0, N'', 0, 189)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (40, CAST(876697.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 193)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (41, CAST(588.00 AS Decimal(15, 2)), 1, N'Once a Month', N'18', N'', 0, N'', 0, 204)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (42, CAST(522.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 218)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (44, CAST(767.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 228)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (46, CAST(50.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 229)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (47, CAST(7676767.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'', 0, N'', 0, 233)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (48, CAST(50.00 AS Decimal(15, 2)), 0, N'Once a Month', N'17', N'', 0, N'', 0, 237)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (53, CAST(3434.00 AS Decimal(15, 2)), 0, N'Vehicle Payoff', N'VP', N'', 0, N'', 0, 232)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (54, CAST(23213.00 AS Decimal(15, 2)), 0, N'Once a Month', N'17', N'', 0, N'', 0, 238)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (55, CAST(12.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'', 0, N'abcde@gghgh.com', 12, 239)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (56, CAST(21423.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'wqr@wqrwe.gfjhg', 23, N'123@dff.jhgj', 123, 244)
INSERT [dbo].[monthlyLoanFee] ([montly_loan_fee_id], [monthly_loan_fee_amount], [receipt], [payment_due_method], [payment_due_date], [auto_remind_dealer_email], [delaer_remind_period], [due_auto_remind_email], [due_auto_remind_period], [loan_id]) VALUES (57, CAST(454.00 AS Decimal(15, 2)), 0, N'Time of Advance', N'ToA', N'', 0, N'abcd@gmail.com', 12, 247)
SET IDENTITY_INSERT [dbo].[monthlyLoanFee] OFF
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'Aprilia', N'RSV4 RF ')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'BMW', N'S1000RR')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'Suzuki', N'GSX-R1000 ')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'Royal Enfield ', N'Continental GT')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2011, N'Yamaha', N'FZ8')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'Aprilia', N'RSV4 RF ')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'BMW', N'S1000RR')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'Suzuki', N'GSX-R1000 ')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2017, N'Royal Enfield ', N'Continental GT')
INSERT [dbo].[motorcyclesModelYear] ([year], [make], [model]) VALUES (2011, N'Yamaha', N'FZ8')
SET IDENTITY_INSERT [dbo].[nonRegisteredBranch] ON 

INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (2, N'fsdfds', N'cgfdh', N'gdfg', N'gfgfdg', 12, N'NULLsdgfd', N'11111', NULL, N'1111111111', NULL, NULL, NULL, NULL, CAST(N'2016-05-02 00:00:00.000' AS DateTime), 5, 12)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (4, N'wqreqwr', N'rewt', N'fdgfdh', N'hfddhd', 13, N'gfdgfd', N'11111', NULL, N'1111111111', NULL, NULL, NULL, NULL, CAST(N'2016-02-05 00:00:00.000' AS DateTime), 5, 12)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (5, N'wrwqr', N'rewrwer', N'wettw', N'tewt', 11, N'fdsf', N'11111', NULL, N'1111111111', NULL, NULL, NULL, NULL, CAST(N'2016-02-05 00:00:00.000' AS DateTime), 5, 8)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (12, N'L_A01_01', N'city1', N'address1', NULL, 2, N'city1', N'56789-5678', NULL, N'0112345678', NULL, NULL, NULL, 17, CAST(N'2016-02-10 14:36:46.103' AS DateTime), 6, 35)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (13, N'D_A03_01', N'city3', N'address3', N'address4', 3, N'city3', N'56789', NULL, N'0112345678', N'          ', N'          ', NULL, 18, CAST(N'2016-02-11 12:56:05.243' AS DateTime), 7, 36)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (14, N'D_A04_01', N'D_ABCD2Branch1', N'address3', NULL, 2, N'city3', N'56789', NULL, N'0112345678', NULL, NULL, NULL, 19, CAST(N'2016-02-12 17:27:33.467' AS DateTime), 8, 36)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (15, N'DEA01_01', N'Dealer 1 branch1', N'Dealer 1 branch1', NULL, 6, N'Dealer 1 branch1', N'56545', NULL, N'5545454543', NULL, NULL, NULL, 25, CAST(N'2016-02-12 18:06:58.770' AS DateTime), 9, 37)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (16, N'DEA03_01', N'dealer1', N'dealer1', NULL, 7, N'city3', N'56789', NULL, N'0112345678', NULL, NULL, NULL, 44, CAST(N'2016-02-13 11:35:58.980' AS DateTime), 11, 39)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (17, N'DEA04_01', N'dealerbranch1', N'street2', NULL, 17, N'city2', N'12345', NULL, N'0778001134', NULL, NULL, NULL, 54, CAST(N'2016-02-14 10:36:00.630' AS DateTime), 12, 43)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (18, N'CLI01_01', N'rewrwer', N'myCompany', NULL, 3, N'sgfdsgfd', N'11111', NULL, N'1111111111', NULL, NULL, NULL, 48, CAST(N'2016-02-14 10:39:31.920' AS DateTime), 13, 41)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (19, N'DEA05_01', N'dealerbranch2', N'street2', NULL, 18, N'city2', N'12345', NULL, N'0778001134', NULL, NULL, NULL, 50, CAST(N'2016-02-14 18:06:51.027' AS DateTime), 14, 44)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (20, N'DEA06_01', N'daelerbranch', N'daelerbranch', NULL, 14, N'colombo', N'23412', NULL, N'1111111111', NULL, NULL, NULL, 57, CAST(N'2016-02-15 09:21:39.480' AS DateTime), 15, 47)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (21, N'ABC01_01', N'ABC_dealer', N'ABC_dealer', NULL, 11, N'ABC_dealer', N'08745', NULL, N'0713444902', NULL, NULL, NULL, 58, CAST(N'2016-02-15 15:22:05.513' AS DateTime), 17, 46)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (22, N'DEA07_01', N'branch1', N'address3', NULL, 1, N'city3', N'56789', NULL, N'0112345678', NULL, NULL, NULL, 61, CAST(N'2016-02-15 15:29:46.770' AS DateTime), 18, 48)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (23, N'DDD01_01', N'aaaa', N'aaaaa', NULL, 2, N'aaa', N'12345', NULL, N'1111111111', NULL, NULL, NULL, 62, CAST(N'2016-02-17 00:36:56.387' AS DateTime), 19, 49)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (24, N'IND01_01', N'Indra Kandy', N'Katugasthota Road', NULL, 18, N'Kandy', N'20189', NULL, N'0812678678', NULL, NULL, NULL, 64, CAST(N'2016-02-18 08:34:15.880' AS DateTime), 20, 50)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (25, N'DEA06_02', N'daelerbranch2', N'daelerbranch2', NULL, 4, N'daelerbranch2', N'11111', NULL, N'1234567890', NULL, NULL, NULL, 60, CAST(N'2016-02-18 16:35:02.340' AS DateTime), 15, 47)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (26, N'DEA09_01', N'dealer', N'kasun', NULL, 9, N'maho', N'12457', NULL, N'0713444801', NULL, NULL, NULL, 69, CAST(N'2016-03-03 08:53:01.847' AS DateTime), 23, 55)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (27, N'ERE01_01', N'aaaa', N'aaaaa', N'tert', 4, N'retret', N'34567', NULL, N'4444444444', NULL, NULL, NULL, 72, CAST(N'2016-03-03 00:11:53.990' AS DateTime), 24, 56)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (28, N'TES01_01', N'testbranchx', N'testbranch2', N'testbranch2', 0, N'testbranch', N'43434', NULL, N'4545545554', N'               ', N'               ', NULL, 74, CAST(N'2016-03-06 17:57:56.227' AS DateTime), 25, 57)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (36, N'LEN02_01', N'city1', N'street1', NULL, 4, N'city1', N'12345', NULL, N'0771119066', N'               ', N'               ', NULL, 78, CAST(N'2016-03-07 11:17:45.977' AS DateTime), 26, 62)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (41, N'TES02_01', N'City1', N'Street1', NULL, 1, N'City1', N'12345-1233', NULL, N'0771119066', N'               ', N'               ', NULL, 86, CAST(N'2016-03-08 08:25:40.063' AS DateTime), 27, 70)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (47, N'ABC02_01', N'WERWE', N'ERWE', N'', 15, N'ERWE', N'34343', N'', N'3434323244', N'', N'', N'', 77, CAST(N'2016-03-08 13:26:59.223' AS DateTime), 28, 63)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (48, N'LEN03_01', N'City41', N'Street41', NULL, 1, N'City41', N'12345', NULL, N'0778001133', NULL, NULL, NULL, 93, CAST(N'2016-03-08 16:28:44.133' AS DateTime), 29, 72)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (49, N'COM03_01', N'daelerbranch2', N'Company1', NULL, 2, N'Alaska', N'11111', NULL, N'2345678901', NULL, NULL, NULL, 108, CAST(N'2016-03-08 18:18:19.463' AS DateTime), 32, 77)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (50, N'COM04_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', NULL, N'2345678901', N'2345678901', N'2345678901', NULL, 85, CAST(N'2016-03-08 18:22:50.417' AS DateTime), 33, 78)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (51, N'LEN04_01', N'City71', N'Street71', N'', 1, N'City71', N'23456', N'', N'0771119066', N'               ', N'               ', N'', 117, CAST(N'2016-03-09 14:54:25.850' AS DateTime), 34, 79)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (52, N'DEA10_01', N'City9', N'Street9', N'', 3, N'City9', N'12345', N'', N'0771119066', N'', N'', N'', 123, CAST(N'2016-03-09 15:53:35.590' AS DateTime), 36, 83)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (53, N'DEA10_02', N'Partner Branch2', N'Street2', N'', 2, N'City2', N'56789', N'', N'0771119066', N'', N'', N'', 124, CAST(N'2016-03-09 17:25:40.470' AS DateTime), 36, 83)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (54, N'SAM01_01', N'Colombo', N'Col1', N'Col2', 5, N'Colombo', N'33333', N'', N'5555555555', N'               ', N'               ', N'', 119, CAST(N'2016-03-09 18:08:47.817' AS DateTime), 35, 81)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (55, N'LEN05_01', N'Lend', N'Len Add 1', N'L', 15, N'Lend', N'98989', N'', N'9685741236', N'               ', N'               ', N'', 126, CAST(N'2016-03-09 21:46:46.860' AS DateTime), 37, 85)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (56, N'COM05_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', N'', N'2345678901', N'2345678901', N'2345678901', N'', 128, CAST(N'2016-03-10 08:53:49.033' AS DateTime), 38, 89)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (57, N'DEA11_01', N'City71', N'Street1', N'', 1, N'City71', N'23456', N'', N'0771119066', N'               ', N'               ', N'', 129, CAST(N'2016-03-10 12:41:37.243' AS DateTime), 39, 88)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (58, N'DAR01_01', N'Erer', N'Darshanaadmincompany', N'Darshanaadmincompany', 7, N'Erer', N'45458-5458', N'', N'5666566566', N'               ', N'               ', N'', 136, CAST(N'2016-03-10 12:50:02.953' AS DateTime), 41, 92)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (59, N'COM06_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', N'', N'2345678901', N'2345678901', N'2345678901', N'', 134, CAST(N'2016-03-10 12:57:44.960' AS DateTime), 40, 90)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (61, N'COM07_01', N'Alaska', N'Company1', N'Street1', 2, N'Alaska', N'11111', N'', N'2345678901', N'2345678901', N'2345678901', N'', 139, CAST(N'2016-03-10 14:00:53.903' AS DateTime), 42, 93)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (62, N'DEA12_01', N'City11', N'Street11', N'', 1, N'City11', N'12345', N'', N'0771119066', N'', N'', N'', 140, CAST(N'2016-03-10 14:08:42.563' AS DateTime), 43, 94)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (67, N'LEN05_02', N'Aaa', N'Aa', N'Aaa', 13, N'Aa', N'98652', N'', N'9638524366', N'', N'', N'', 138, CAST(N'2016-03-10 14:56:51.977' AS DateTime), 37, 86)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (75, N'DEA12_02', N'Partner Branch2', N'Street2', N'', 18, N'City1', N'12345', N'', N'0771119066', N'', N'', N'', 140, CAST(N'2016-03-10 16:28:35.383' AS DateTime), 43, 94)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (76, N'SEN01_01', N'Ad', N'Ad', N'Ad', 35, N'Ad', N'78966', N'', N'7894561233', N'', N'', N'', 144, CAST(N'2016-03-10 23:01:33.417' AS DateTime), 44, 97)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (77, N'LUT01_01', N'Luther Family Msp', N'895 S 4th', N'', 23, N'Minneapolis', N'55410', N'', N'7635894568', N'', N'', N'', 146, CAST(N'2016-03-10 20:17:28.153' AS DateTime), 45, 98)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (78, N'LUT01_02', N'Luther Family Fargo', N'858 Ave S', N'', 34, N'Fargo', N'58104', N'', N'7015658954', N'', N'', N'', 146, CAST(N'2016-03-10 20:24:43.370' AS DateTime), 45, 99)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (79, N'LEN06_01', N'Lender DAPN', N'Street110', N'', 2, N'City110', N'12345', N'', N'0771119066', N'               ', N'               ', N'', 149, CAST(N'2016-03-11 13:07:07.457' AS DateTime), 46, 100)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (80, N'LEN06_02', N'Lender DAPN - Branch2', N'Street2', N'', 2, N'City2', N'12345', N'', N'0112345678', N'', N'', N'', 149, CAST(N'2016-03-11 13:08:14.597' AS DateTime), 46, 101)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (81, N'EEE01_01', N'Eeeee Eee', N'Wwweeee', N'Eeeee', 15, N'Eeeeee', N'33333', N'', N'4354354354', N'', N'', N'', 151, CAST(N'2016-03-11 14:41:25.030' AS DateTime), 47, 102)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (82, N'COM08_01', N'Company1', N'Company1', N'Street1', 2, N'Alaska', N'11111', N'', N'2345678901', N'2345678901', N'2345678901', N'', 152, CAST(N'2016-03-11 16:32:52.433' AS DateTime), 48, 104)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (83, N'LEN07_01', N'Lender DAPN2', N'Street71', N'', 1, N'City71', N'12345', N'LenderDAPN2@hh.com', N'0771119066', N'0771119063', N'0771119066', N'0771119066', 153, CAST(N'2016-03-11 17:06:39.400' AS DateTime), 49, 105)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (84, N'SEN01_02', N'Test1', N'sd', N'sdf', 17, N'Sdfs', N'33432', N'', N'3334443322', N'', N'', N'', 144, CAST(N'2016-03-12 10:40:41.467' AS DateTime), 44, 107)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (85, N'DAP01_01', N'DAPNL3', N'Street71', N'', 1, N'City71', N'12345', N'', N'0771119066', N'', N'', N'', 156, CAST(N'2016-03-12 11:07:51.173' AS DateTime), 50, 108)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (86, N'DEA13_01', N'Dealer Company2', N'Street71', N'', 3, N'City71', N'12345', N'', N'0771119066', N'', N'', N'', 159, CAST(N'2016-03-12 11:23:34.943' AS DateTime), 51, 110)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (87, N'SAM01_02', N'Sam', N'Rgfg', N'', 5, N'ciii', N'12345', N'', N'0773444903', N'', N'', N'', 157, CAST(N'2016-03-12 11:33:38.363' AS DateTime), 52, 109)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (88, N'SDF01_01', N'sdf dsf sdf sdf', N'Sdfsdf', N'dfsd', 15, N'fsd', N'23423', N'', N'2343243244', N'', N'', N'', 160, CAST(N'2016-03-12 11:41:04.590' AS DateTime), 53, 111)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (89, N'ERW01_01', N'Erwer', N'Ewrwer', N'Werwe', 14, N'Werew', N'42342', N'', N'3243242343', N'', N'', N'', 161, CAST(N'2016-03-12 01:41:20.507' AS DateTime), 54, 112)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (90, N'DAP02_01', N'DAPNL15', N'Street71', N'', 2, N'City71', N'12345', N'', N'0771119066', N'', N'', N'', 162, CAST(N'2016-03-12 15:35:29.440' AS DateTime), 55, 113)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (91, N'COM09_01', N'Company1', N'Company1', N'Street1', 2, N'Alaska', N'11111', N'', N'2345678901', N'2345678901', N'2345678901', N'', 164, CAST(N'2016-03-12 16:15:11.513' AS DateTime), 56, 114)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (92, N'LEN08_01', N'Lender DAPN316', N'Street71', N'', 2, N'City71', N'12345', N'', N'0771119066', N'', N'', N'', 165, CAST(N'2016-03-12 17:13:15.257' AS DateTime), 57, 115)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (93, N'ASD01_01', N'Asdewf', N'Afdsagfdg', N'', 5, N'Dsafdsf', N'22222-2222', N'', N'1234567890', N'', N'', N'', 167, CAST(N'2016-03-12 18:42:48.230' AS DateTime), 58, 117)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (94, N'ASD01_02', N'Fdsg', N'Dealer', N'Dealer', 3, N'Asf', N'22222', N'', N'1234567890', N'', N'', N'', 167, CAST(N'2016-03-12 18:43:19.927' AS DateTime), 58, 117)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (95, N'TES03_01', N'TestLender1', N'Colombo', N'', 5, N'Colombo', N'12345-6789', N'4testing@lender.com', N'6565678945', N'', N'', N'', 170, CAST(N'2016-03-13 14:22:45.703' AS DateTime), 59, 118)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (96, N'LEN09_01', N'Lender17', N'Street71', N'', 2, N'City71', N'12345', N'', N'0771119066', N'', N'', N'', 173, CAST(N'2016-03-14 08:00:42.567' AS DateTime), 60, 119)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (97, N'DEA14_01', N'Dealer', N'Dealer', N'Dealer', 3, N'Dealer', N'12345-6789', N'', N'1234567890', N'', N'', N'', 172, CAST(N'2016-03-14 08:21:44.283' AS DateTime), 61, 120)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (98, N'KAS01_01', N'Kasunnewnonregdea', N'Kasunnewnonregdea', N'', 11, N'Rererer', N'34343-4444', N'', N'0713444444', N'', N'', N'', 175, CAST(N'2016-03-14 09:17:47.283' AS DateTime), 62, 121)
INSERT [dbo].[nonRegisteredBranch] ([non_reg_branch_id], [branch_code], [branch_name], [branch_address_1], [branch_address_2], [state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [created_by], [created_date], [company_id], [branch_id]) VALUES (99, N'DEA15_01', N'Dealer Company18', N'Street18', N'', 2, N'City18', N'12345', N'', N'0771119066', N'', N'', N'', 178, CAST(N'2016-03-23 08:30:17.857' AS DateTime), 63, 124)
SET IDENTITY_INSERT [dbo].[nonRegisteredBranch] OFF
SET IDENTITY_INSERT [dbo].[nonRegisteredCompany] ON 

INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (5, N'NRC', N'NRC01', N'NRC01', N'NRC01', N'NRC01', 12, N'12334', N'', N'1211111111', N'               ', N'               ', N'', N'', 5, CAST(N'2016-10-02 00:00:00.000' AS DateTime), 2, 12)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (6, N'L_ABCD', N'L_A01', N'address1', N'', N'city1', 2, N'56789-5678', N'', N'0112345678', N'               ', N'               ', N'', N'', 17, CAST(N'2016-02-10 14:36:02.477' AS DateTime), 1, 20)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (7, N'D_ABCD', N'D_A03', N'address3', N'address4', N'city3', 3, N'56789', N'', N'0112345678', N'               ', N'               ', N'', N'', 18, CAST(N'2016-02-11 12:04:03.093' AS DateTime), 2, 21)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (8, N'D_ABCD2', N'D_A04', N'address2', N'', N'city2', 1, N'56789', N'', N'0112345678', N'               ', N'               ', N'', N'', 19, CAST(N'2016-02-12 17:25:54.877' AS DateTime), 2, 21)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (9, N'Dealer 1', N'DEA01', N'Dealer 1', N'', N'DF', 18, N'34343', N'', N'5654564256', N'               ', N'               ', N'', N'', 25, CAST(N'2016-02-12 18:01:03.280' AS DateTime), 2, 22)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (10, N'Dealer Company3', N'DEA02', N'Dealer Company3', N'Dealer Company3', N'Dealer Company3', 3, N'33333', N'', N'6565656666', N'               ', N'               ', N'', N'', 25, CAST(N'2016-02-12 18:02:29.333' AS DateTime), 2, 22)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (11, N'dealer1', N'DEA03', N'dealer1', N'', N'dealer1', 2, N'56789', N'', N'0112345678', N'               ', N'               ', N'', N'', 44, CAST(N'2016-02-13 11:34:50.660' AS DateTime), 2, 23)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (12, N'Dealer1', N'DEA04', N'street1', N'', N'city1', 18, N'12345', N'', N'0771119066', N'               ', N'               ', N'', N'', 54, CAST(N'2016-02-14 10:34:34.117' AS DateTime), 2, 26)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (13, N'client1', N'CLI01', N'client1', N'', N'client1', 6, N'11111', N'', N'1211111111', N'               ', N'               ', N'', N'', 48, CAST(N'2016-02-14 10:37:14.913' AS DateTime), 2, 25)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (14, N'Dealer2', N'DEA05', N'street2', N'', N'city2', 18, N'12345', N'', N'0771119066', N'               ', N'               ', N'', N'', 50, CAST(N'2016-02-14 18:05:36.027' AS DateTime), 2, 26)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (15, N'Dealer1', N'DEA06', N'Dealer1', N'', N'colombo', 6, N'12345', N'', N'1211111111', N'               ', N'               ', N'', N'', 57, CAST(N'2016-02-15 09:19:43.413' AS DateTime), 2, 29)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (17, N'ABC_dealer', N'ABC01', N'ABC_dealer', N'', N'ABC_dealer', 12, N'55454', N'', N'0713444902', N'               ', N'               ', N'', N'', 58, CAST(N'2016-02-15 15:21:07.373' AS DateTime), 2, 28)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (18, N'dealerPiyumi', N'DEA07', N'address2', N'', N'city2', 3, N'56789', N'', N'0112345678', N'               ', N'               ', N'', N'', 61, CAST(N'2016-02-15 15:29:03.210' AS DateTime), 2, 30)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (19, N'dddd', N'DDD01', N'ddd', N'dd', N'dd', 18, N'12345', N'', N'1111111111', N'               ', N'               ', N'', N'', 62, CAST(N'2016-02-17 00:36:24.297' AS DateTime), 2, 31)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (20, N'Indra Traders', N'IND01', N'Katugastota', N'', N'Kandy', 18, N'20189', N'', N'0812678678', N'               ', N'               ', N'', N'', 64, CAST(N'2016-02-18 08:32:48.223' AS DateTime), 2, 32)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (21, N'dealer2', N'DEA08', N'address1', N'', N'city1', 2, N'12345', N'', N'0771119066', N'               ', N'               ', N'', N'', 61, CAST(N'2016-03-01 10:21:50.020' AS DateTime), 2, 30)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (22, N'lendertfnkandy', N'LEN01', N'address1', N'', N'city1', 1, N'12345', N'', N'0771119066', N'               ', N'               ', N'', N'', 68, CAST(N'2016-03-01 10:30:05.597' AS DateTime), 1, 33)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (23, N'DealerKasun', N'DEA09', N'DealerKasun', N'', N'maho', 1, N'12348', N'', N'0713444801', N'               ', N'               ', N'', N'', 69, CAST(N'2016-03-03 08:52:00.070' AS DateTime), 2, 34)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (24, N'ere', N'ERE01', N'ertre', N'rte', N'er', 3, N'44654', N'', N'4353454445', N'5555554544     ', N'               ', N'', N'', 72, CAST(N'2016-03-03 00:10:57.000' AS DateTime), 2, 35)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (25, N'testbranch2', N'TES01', N'testbranch2', N'testbranch2', N'testbranch', 14, N'43434', N'', N'4545545554', N'               ', N'               ', N'', N'', 74, CAST(N'2016-03-03 15:18:23.763' AS DateTime), 2, 36)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (26, N'Lendercompany4', N'LEN02', N'Street4', N'', N'City4', 2, N'12345', N'', N'0771119066', N'               ', N'               ', N'', N'', 78, CAST(N'2016-03-07 08:19:57.003' AS DateTime), 1, 38)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (27, N'TestPartnerCompany1', N'TES02', N'Street1', N'', N'City1', 1, N'12345-1233', N'', N'0771119066', N'               ', N'               ', N'', N'', 0, CAST(N'2016-03-08 07:59:47.540' AS DateTime), 2, 45)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (28, N'Abcd', N'ABC02', N'Maharagama ', N'Colombo', N'Colombo', 16, N'95269', N'', N'789456120', N'               ', N'               ', N'', N'', 0, CAST(N'2016-03-08 09:59:16.997' AS DateTime), 2, 37)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (29, N'Lendercompany41', N'LEN03', N'Street41', NULL, N'City41', 1, N'12345', NULL, N'0778001133', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-03-08 00:00:00.000' AS DateTime), 1, 50)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (30, N'Company1', N'COM01', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'2345678901     ', N'2345678901     ', N'', N'', 103, CAST(N'2016-03-08 17:12:16.463' AS DateTime), 2, 58)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (31, N'Company1', N'COM02', N'Company1', N'Street1', N'Alaska', 2, N'11111-1111', N'', N'2345678901', N'2345678901     ', N'2345678901     ', N'', N'', 96, CAST(N'2016-03-08 17:52:13.443' AS DateTime), 2, 52)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (32, N'Company1', N'COM03', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'2345678901     ', N'2345678901     ', N'', N'', 108, CAST(N'2016-03-08 18:00:03.730' AS DateTime), 1, 59)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (33, N'Company1', N'COM04', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'2345678901     ', N'2345678901     ', N'', N'', 85, CAST(N'2016-03-08 18:21:34.830' AS DateTime), 1, 47)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (34, N'Lendercompany71', N'LEN04', N'Street71', N'', N'City71', 1, N'23456', N'', N'0771119066', N'               ', N'               ', N'', N'', 116, CAST(N'2016-03-09 14:10:22.580' AS DateTime), 1, 62)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (35, N'Sampath', N'SAM01', N'Col1', N'Col2', N'Colombo', 5, N'33333', N'', N'5555555555', N'               ', N'               ', N'', N'', 120, CAST(N'2016-03-09 14:26:42.463' AS DateTime), 1, 63)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (36, N'Dealer Company9', N'DEA10', N'Street9', N'', N'City9', 3, N'12345', N'', N'0771119067', N'               ', N'               ', N'', N'', 123, CAST(N'2016-03-09 15:52:50.513' AS DateTime), 2, 65)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (37, N'Lender Com', N'LEN05', N'Len Add 1', N'L', N'Lend', 15, N'98989', NULL, N'9685741236', N'               ', N'               ', NULL, NULL, 126, CAST(N'2016-03-09 20:28:53.940' AS DateTime), 2, 66)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (38, N'Company1', N'COM05', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'2345678901     ', N'2345678901     ', N'', N'', 128, CAST(N'2016-03-10 08:52:46.050' AS DateTime), 2, 70)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (39, N'Dealer Company100', N'DEA11', N'Street1', N'', N'City71', 1, N'23456', N'', N'0771119066', N'               ', N'               ', N'', N'', 129, CAST(N'2016-03-10 12:12:55.550' AS DateTime), 2, 67)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (40, N'Company1', N'COM06', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'2345678904     ', N'2345678901     ', N'', N'', 134, CAST(N'2016-03-10 12:13:26.563' AS DateTime), 2, 71)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (41, N'Darshanaadmincompany', N'DAR01', N'Darshanaadmincompany', N'Darshanaadmincompany', N'Erer', 7, N'45458-5458', N'', N'5666566566', N'               ', N'               ', N'', N'', 135, CAST(N'2016-03-10 12:44:49.287' AS DateTime), 2, 72)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (42, N'Company1', N'COM07', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'2345678901     ', N'2345678901     ', N'', N'', 139, CAST(N'2016-03-10 14:00:29.633' AS DateTime), 1, 73)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (43, N'Dealer11', N'DEA12', N'Street11', N'', N'City11', 1, N'12345', N'', N'0771119066', N'               ', N'               ', N'', N'', 140, CAST(N'2016-03-10 14:07:20.593' AS DateTime), 2, 74)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (44, N'Senarath', N'SEN01', N'Ad', N'Ad', N'Ad', 35, N'78966', N'', N'7894561233', N'               ', N'               ', N'', N'', 144, CAST(N'2016-03-10 23:01:06.167' AS DateTime), 1, 75)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (45, N'Luther Family', N'LUT01', N'895 S 4th', N'', N'Minneapolis', 23, N'55410', N'', N'7635894568', N'               ', N'               ', N'', N'', 146, CAST(N'2016-03-10 20:16:39.613' AS DateTime), 2, 76)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (46, N'Lender DAPN', N'LEN06', N'Street110', N'', N'City110', 2, N'12345', N'', N'0771119066', N'               ', N'               ', N'', N'', 149, CAST(N'2016-03-11 13:03:53.377' AS DateTime), 2, 77)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (47, N'Eeeee Eee', N'EEE01', N'Wwweeee', N'Eeeee', N'Eeeeee', 15, N'33333', N'', N'4354354354', N'               ', N'               ', N'', N'', 151, CAST(N'2016-03-11 14:41:02.253' AS DateTime), 2, 78)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (48, N'Company1', N'COM08', N'Company1', N'Street1', N'Alaska', 2, N'11111', N'', N'2345678901', N'2345678901     ', N'2345678901     ', N'', N'', 152, CAST(N'2016-03-11 16:21:08.853' AS DateTime), 1, 79)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (49, N'Lender DAPN2', N'LEN07', N'Street71', N'', N'City71', 1, N'12345', N'LenderDAPN2@hh.com', N'0771119066', N'0771119063     ', N'0771119066     ', N'0771119066', N'', 153, CAST(N'2016-03-11 16:41:44.077' AS DateTime), 1, 80)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (50, N'DAPNL3', N'DAP01', N'Street71', NULL, N'City71', 1, N'12345', NULL, N'0771119066', NULL, NULL, NULL, NULL, 156, CAST(N'2016-03-12 11:07:08.253' AS DateTime), 2, 82)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (51, N'Dealer Company2', N'DEA13', N'Street71', NULL, N'City71', 3, N'12345', NULL, N'0771119066', NULL, NULL, NULL, NULL, 159, CAST(N'2016-03-12 11:20:55.137' AS DateTime), 2, 84)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (52, N'Sam', N'SAM01', N'Rgfg', NULL, N'ciii', 5, N'12345', NULL, N'0773444903', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-04-12 00:00:00.000' AS DateTime), 1, 83)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (53, N'sdf dsf sdf sdf', N'SDF01', N'Sdfsdf', N'dfsd', N'fsd', 15, N'23423', NULL, N'2343243244', NULL, NULL, NULL, NULL, 160, CAST(N'2016-03-12 11:40:16.370' AS DateTime), 1, 85)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (54, N'Erwer', N'ERW01', N'Ewrwer', N'Werwe', N'Werew', 14, N'42342', NULL, N'3243242343', NULL, NULL, NULL, NULL, 161, CAST(N'2016-03-12 01:41:08.803' AS DateTime), 1, 86)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (55, N'DAPNL15', N'DAP02', N'Street71', NULL, N'City71', 2, N'12345', NULL, N'0771119066', NULL, NULL, NULL, NULL, 162, CAST(N'2016-03-12 15:34:46.880' AS DateTime), 1, 87)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (56, N'Company1', N'COM09', N'Company1', N'Street1', N'Alaska', 2, N'11111', NULL, N'2345678901', N'2345678901     ', N'2345678901     ', NULL, NULL, 164, CAST(N'2016-03-12 16:15:00.157' AS DateTime), 1, 88)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (57, N'Lender DAPN316', N'LEN08', N'Street71', NULL, N'City71', 2, N'12345', NULL, N'0771119066', NULL, NULL, NULL, NULL, 165, CAST(N'2016-03-12 17:12:32.337' AS DateTime), 2, 89)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (58, N'Asdewf', N'ASD01', N'Afdsagfdg', NULL, N'Dsafdsf', 5, N'11111-1111', NULL, N'1234567890', NULL, NULL, NULL, NULL, 167, CAST(N'2016-03-12 18:42:04.393' AS DateTime), 2, 90)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (59, N'TestLender1', N'TES03', N'Colombo', NULL, N'Colombo', 5, N'12345-6789', N'4testing@lender.com', N'6565678945', NULL, NULL, NULL, NULL, 170, CAST(N'2016-03-13 14:06:44.163' AS DateTime), 1, 92)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (60, N'Lender17', N'LEN09', N'Street71', NULL, N'City71', 2, N'12345', NULL, N'0771119066', NULL, NULL, NULL, NULL, 173, CAST(N'2016-03-14 07:59:55.940' AS DateTime), 1, 94)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (61, N'Dealer', N'DEA14', N'Dealer', N'Dealer', N'Dealer', 3, N'12345-6789', NULL, N'1234567890', NULL, NULL, NULL, NULL, 172, CAST(N'2016-03-14 08:21:37.927' AS DateTime), 1, 93)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (62, N'Kasunnewnonregdea', N'KAS01', N'Kasunnewnonregdea', NULL, N'Rererer', 11, N'34343-4444', NULL, N'0713444444', NULL, NULL, NULL, NULL, 175, CAST(N'2016-03-14 09:17:40.600' AS DateTime), 2, 95)
INSERT [dbo].[nonRegisteredCompany] ([company_id], [company_name], [company_code], [company_address_1], [company_address_2], [city], [stateId], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], [fax], [website_url], [created_by], [created_date], [company_type], [reg_company_id]) VALUES (63, N'Dealer Company18', N'DEA15', N'Street18', NULL, N'City18', 2, N'12345', NULL, N'0771119066', NULL, NULL, NULL, NULL, 178, CAST(N'2016-03-23 08:29:48.393' AS DateTime), 2, 96)
SET IDENTITY_INSERT [dbo].[nonRegisteredCompany] OFF
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (179, N'123456-000024', 3, CAST(100.00 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (190, N'45678-000001', 1, CAST(0.50 AS Decimal(18, 2)), CAST(N'2016-03-31 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (190, N'45678-000003', 1, CAST(4.00 AS Decimal(18, 2)), CAST(N'2016-03-23 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (190, N'45678-000007', 2, CAST(91.00 AS Decimal(18, 2)), CAST(N'2016-03-03 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (257, N'12345678901-000003', 1, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2016-06-30 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (179, N'123456-000034', 1, CAST(1909.92 AS Decimal(18, 2)), CAST(N'2016-05-27 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (179, N'123456-000034', 1, CAST(45.55 AS Decimal(18, 2)), CAST(N'2016-05-26 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (179, N'123456-000035', 1, CAST(2000.24 AS Decimal(18, 2)), CAST(N'2016-05-28 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (179, N'123456-000036', 2, CAST(211.44 AS Decimal(18, 2)), CAST(N'2016-04-27 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (179, N'123456-000037', 1, CAST(190.44 AS Decimal(18, 2)), CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[partialCurtailment] ([loan_id], [unit_id], [curt_number], [curt_partial_amount], [paid_date]) VALUES (179, N'123456-000037', 1, CAST(7.00 AS Decimal(18, 2)), CAST(N'2016-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2015, N'Make1', N'Model1')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2015, N'Make1', N'Model2')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2016, N'Make1', N'Model3')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2016, N'Make2', N'Model4')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2016, N'Make2', N'Model5')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2015, N'Make1', N'Model1')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2015, N'Make1', N'Model2')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2016, N'Make1', N'Model3')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2016, N'Make2', N'Model4')
INSERT [dbo].[snowmobileModelYear] ([year], [make], [model]) VALUES (2016, N'Make2', N'Model5')
SET IDENTITY_INSERT [dbo].[states] ON 

INSERT [dbo].[states] ([state_id], [state_name]) VALUES (1, N'Alabama')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (2, N'Alaska')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (3, N'Arizona')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (4, N'Arkansas')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (5, N'California')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (6, N'Colorado')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (7, N'Connecticut')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (8, N'Delaware')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (9, N'Florida')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (10, N'Georgia')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (11, N'Hawaii')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (12, N'Idaho')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (13, N'	Illinois')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (14, N'Indiana')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (15, N'Iowa')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (16, N'Kansas')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (17, N'Kentucky')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (18, N'Louisiana')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (19, N'	Maine')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (20, N'	Maryland')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (21, N'Massachusetts')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (22, N'	Michigan')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (23, N'Minnesota')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (24, N'	Mississippi')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (25, N'	Missouri')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (26, N'Montana')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (27, N'Nebraska')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (28, N'	Nevada')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (29, N'New Hampshire')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (30, N'New Jersey')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (31, N'New Mexico')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (32, N'New York')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (33, N'North Carolina')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (34, N'North Dakota')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (35, N'Ohio')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (36, N'Oklahoma')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (37, N'Oregon')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (38, N'Pennsylvania')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (39, N'Rhode Island')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (40, N'South Carolina')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (41, N'South Dakota')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (42, N'	Tennessee')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (43, N'Texas')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (44, N'Utah')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (45, N'Vermont')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (46, N'Virginia')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (47, N'Washington')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (48, N'West Virginia')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (49, N'Wisconsin')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (50, N'	Wyoming')
SET IDENTITY_INSERT [dbo].[states] OFF
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (67, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (77, 0, 3, 0)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (68, NULL, 5, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (80, 0, 2, 0)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (81, 0, 2, 0)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (82, NULL, 2, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (83, NULL, 2, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (84, 0, 2, 0)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (85, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (86, NULL, 6, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (88, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (93, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (94, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (97, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (98, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (99, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (108, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (113, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (116, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (119, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (122, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (123, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (126, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (128, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (129, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (134, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (139, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (140, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (144, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (148, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (151, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (152, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (153, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (157, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (165, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (173, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (175, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (178, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (62, 182, 10, 49)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (72, 193, 10, 0)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (2, 191, 10, 48)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (74, 14, 8, 57)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (78, NULL, 6, 62)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (92, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (143, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (159, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (161, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (167, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (91, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (101, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (102, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (103, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (111, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (114, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (115, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (96, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (172, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (135, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (146, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (156, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (149, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (162, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (170, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (160, NULL, 1, NULL)
INSERT [dbo].[step] ([user_id], [loan_id], [step_id], [branch_id]) VALUES (164, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[systemAdmin] ON 

INSERT [dbo].[systemAdmin] ([system_admin_id], [user_name], [password], [level_id]) VALUES (1, N'futurenet', N'futurenet123', NULL)
SET IDENTITY_INSERT [dbo].[systemAdmin] OFF
SET IDENTITY_INSERT [dbo].[systemAdminLevel] ON 

INSERT [dbo].[systemAdminLevel] ([level_id], [level_name]) VALUES (1, N'SuperAdmin')
INSERT [dbo].[systemAdminLevel] ([level_id], [level_name]) VALUES (2, N'Admin')
INSERT [dbo].[systemAdminLevel] ([level_id], [level_name]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[systemAdminLevel] OFF
SET IDENTITY_INSERT [dbo].[title] ON 

INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (18, 1, N'title present to advance', N'at advance date', N'email@gmail.comm', 1, N'physically and scan copy', 179)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (20, 1, N'title can arrive at any time', N'with in 7 days', N'piyumi@gmail.com', 1, N'physically', 180)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (22, 1, N'scanned title adequate', N'at advance date', N'email@gmail.comm', 1, N'physically and scan copy', 184)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (23, 1, N'scanned title adequate', N'with in 7 days', N'piyumi@gmail.com', 1, N'scan copy', 189)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (24, 1, NULL, NULL, NULL, 1, N'physically', 191)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (25, 1, N'scanned title adequate', N'with in 7 days', N'sds@ss.com', 1, N'scan copy', 193)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (27, 1, N'Scanned Title Adequate', N'At Advance Date', N'piyumi@gmail.com', 1, N'Physically', 198)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (28, 1, N'Title Present To Advance', NULL, N'abcd@gmail.com', 1, N'Physically', 200)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (29, 1, N'Title Present To Advance', NULL, NULL, 0, NULL, 201)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (30, 1, N'Scanned Title Adequate', N'At Any Time', NULL, 1, N'Physically', 202)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (31, 1, N'Scanned Title Adequate', N'At Any Time', NULL, 1, N'Physically', 203)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (32, 1, N'Scanned Title Adequate', N'At Any Time', N'4332', 1, N'Scan Copy', 218)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (33, 1, N'Title Can Arrive At Any Time', N'At Any Time', NULL, 1, N'Physically And Scan Copy', 220)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (34, 1, N'Title Present To Advance', NULL, NULL, 0, NULL, 221)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (35, 1, N'Scanned Title Adequate', N'at advance date', N'piyumi@gmail.com', 1, N'Scan Copy', 225)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (36, 1, N'Scanned Title Adequate', N'At Any Time', NULL, 1, N'Physically', 227)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (37, 1, N'Scanned Title Adequate', N'At Any Time', NULL, 1, N'Scan Copy', 229)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (39, 1, N'Scanned Title Adequate', N'At Any Time', NULL, 1, N'Scan Copy', 237)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (43, 1, N'Scanned Title Adequate', N'With In 7 Days', N'sdfsd@gmail.com', 1, N'Physically And Scan Copy', 238)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (44, 1, N'Scanned Title Adequate', N'At Advance Date', N'abcde@gghgh.com', 1, N'Physically And Scan Copy', 239)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (45, 1, N'Scanned Title Adequate', N'At Advance Date', N'hghgh@cch.bnv', 1, N'Physically', 243)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (46, 1, N'Scanned Title Adequate', N'At Advance Date', N'sad@rdrt.jk', 0, NULL, 244)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (47, 1, N'Scanned Title Adequate', N'At Advance Date', N'piyumi@gmail.com', 1, N'Physically', 247)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (48, 1, N'Scanned Title Adequate', N'With In 7 Days', N'kasunkasun@gmail.com', 1, N'Scan Copy', 249)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (49, 1, N'Scanned Title Adequate', N'At Advance Date', N'piyumi@gmail.com', 1, N'Physically', 257)
INSERT [dbo].[title] ([title_id], [is_title_tracked], [title_accept_method], [title_received_time_period], [auto_remind_email], [is_receipt_required], [receipt_required_method], [loan_id]) VALUES (50, 1, N'Title Present To Advance', NULL, NULL, 1, N'Physically', 263)
SET IDENTITY_INSERT [dbo].[title] OFF
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000007', N'JTDKN3DU3A0156107', 1983, N'Fordfdfdfdf', N'Mustangfdfdf', N'dfdf', CAST(5454.000 AS Decimal(13, 3)), N'Fordfdfdfdf', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(65656.00 AS Decimal(12, 2)), CAST(55808.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 14:08:58.910' AS DateTime), 64, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000008', N'JTDKN3DU3A0156107', 1983, N'Fillmore', N'Fillmore', N'554', CAST(45.000 AS Decimal(13, 3)), N'Fillmore', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(45545.00 AS Decimal(12, 2)), CAST(38713.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 16:05:19.173' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000009', N'JTDKN3DU3A0156107', 2004, N'Mercedes-Benz', N'W201', N'dfdf', CAST(65656.000 AS Decimal(13, 3)), N'Mercedes-Benz', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(76676.00 AS Decimal(12, 2)), CAST(65175.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 16:17:59.300' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000010', N'JTDKN3DU3A0156107', 1983, N'BMW', N'600', N'ffsf', CAST(5454.000 AS Decimal(13, 3)), N'BMW', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(122.00 AS Decimal(12, 2)), CAST(104.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 16:20:29.343' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000011', N'JTDKN3DU3A0156107', 1983, N'54545', N'545454', N'54545', CAST(54545.000 AS Decimal(13, 3)), N'54545', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(54545.00 AS Decimal(12, 2)), CAST(46363.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 17:18:17.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 17:21:23.723' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000012', N'JTDKN3DU3A0156107', 1983, N'5445455', N'54544', N'5454545gf-g', CAST(5454545.000 AS Decimal(13, 3)), N'5445455', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(454545.00 AS Decimal(12, 2)), CAST(386363.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 18:03:09.890' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000013', N'JTDKN3DU3A0156107', 1983, N'5454', N'545', N'5454', CAST(545454.000 AS Decimal(13, 3)), N'5454', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(463.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 19:04:49.243' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000014', N'JTDKN3DU3A0156107', 5454, N'55454', N'545', N'5454', CAST(5454.000 AS Decimal(13, 3)), N'55454', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(463.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 19:07:33.717' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000015', N'JTDKN3DU3A0156107', 1983, N'ytytyyt', N'tyyty', N'yttytyty', CAST(656566.000 AS Decimal(13, 3)), N'ytytyyt', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5656.00 AS Decimal(12, 2)), CAST(4808.00 AS Decimal(12, 2)), NULL, CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-02-29 19:40:25.650' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000016', N'WBXPC93408WJ04248', 2012, N'Citroën', N'2CV', N'4344343', CAST(43434.000 AS Decimal(13, 3)), N'434fdfdfdf', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(44343.00 AS Decimal(12, 2)), CAST(37692.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 00:26:11.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 00:27:25.683' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000017', N'JA3AJ26E54U014571', 2013, N'Chryslerdsd', N'Imperialddsd', N'dssd', CAST(433.000 AS Decimal(13, 3)), N'dsdsd', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(43434.00 AS Decimal(12, 2)), CAST(36919.00 AS Decimal(12, 2)), NULL, CAST(N'2016-01-03 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 00:31:25.113' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000018', N'JA3AJ26E54U014571', 4334, N'4343', N'4334', NULL, CAST(3443.000 AS Decimal(13, 3)), N'43', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(433.00 AS Decimal(12, 2)), CAST(368.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 00:34:45.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 00:38:14.990' AS DateTime), 2, NULL, NULL, 184, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000019', N'JA3AJ26E54U014572', 2013, N'Citroën', N'2CV', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(545.00 AS Decimal(10, 2)), N'54545', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(54545.00 AS Decimal(12, 2)), CAST(46363.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 00:38:20.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 00:39:46.907' AS DateTime), 2, NULL, NULL, 184, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000020', N'WBXPC93408WJ04248', 3322, N'rrrrr', N'33333rrrrrrrrrrrrrrr', N'r', CAST(33.000 AS Decimal(13, 3)), N'rrwww', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(22.00 AS Decimal(12, 2)), CAST(19.00 AS Decimal(12, 2)), NULL, CAST(N'2016-01-03 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 01:19:00.510' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000021', N'WBXPC93408WJ04248', 3232, N'ffe', N'rrrrrrrrrrrrr', NULL, CAST(4.000 AS Decimal(13, 3)), NULL, 1, CAST(4.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(44.00 AS Decimal(12, 2)), CAST(37.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 01:19:03.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 01:20:25.653' AS DateTime), 2, NULL, NULL, 184, 2, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000022', N'JA3AJ26E54U014572', 444, N'43434', N'ggggg', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(3.00 AS Decimal(10, 2)), N'4343', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3.00 AS Decimal(12, 2)), CAST(3.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 01:20:27.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 01:22:06.947' AS DateTime), 2, NULL, NULL, 184, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000023', N'WBXPC93408WJ04248', 3333, N'5t5t', N'hhhhhhhhhh', NULL, CAST(434.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4343.00 AS Decimal(12, 2)), CAST(3692.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 01:22:09.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 01:24:25.523' AS DateTime), 2, NULL, NULL, 184, 4, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000024', N'1G1PG5SCXC7300728', 1995, N'222222222', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'22222222222222', N'222222222222', CAST(3333.00 AS Decimal(12, 2)), CAST(2833.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 01:33:31.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-01 01:35:17.463' AS DateTime), 2, NULL, 62, 184, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000025', N'1G1PG5SCXC7300728', 1995, N'322323', N'eeeeeeeeee', NULL, CAST(3333.000 AS Decimal(13, 3)), N'Green333', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(333.00 AS Decimal(12, 2)), CAST(283.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 01:35:20.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 01:37:10.040' AS DateTime), 2, NULL, NULL, 184, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000026', N'1G1PG5SCXC7300728', 1995, N'Citroën', N'2CV', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(33232.00 AS Decimal(12, 2)), CAST(28247.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 01:37:12.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 01:38:38.210' AS DateTime), 2, NULL, NULL, 184, 7, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000027', N'dedd33', 3333, N'2222', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(333.00 AS Decimal(12, 2)), CAST(283.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 01:38:41.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 01:40:40.263' AS DateTime), 2, NULL, NULL, 184, 8, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000028', N'32232333222332232', 0, N'eeeeeeeeeeeee', N'eeeeeeeeeeeeeee', N'e3ee', CAST(4343.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3333.00 AS Decimal(12, 2)), CAST(2833.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 03:46:54.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 03:50:07.313' AS DateTime), 2, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000029', N'32232333222332232', 2014, N'BMWfefef', N'fefef', N'fefe', CAST(3434.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4434.00 AS Decimal(12, 2)), CAST(3769.00 AS Decimal(12, 2)), NULL, CAST(N'2016-01-03 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-01 05:23:09.553' AS DateTime), 2, NULL, 69, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000030', N'1G1PG5SCXC7300724', 2015, N'Austin', N'Mini', N'jhk', CAST(65000.000 AS Decimal(13, 3)), N'Black', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1323.00 AS Decimal(12, 2)), CAST(1125.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-09 18:27:29.443' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-09 18:27:29.443' AS DateTime), 69, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000031', N'JHLRD78863C039583', 2015, N'BMW', N'3 Series', N'122', CAST(65000.000 AS Decimal(13, 3)), N'Blue', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(312323.00 AS Decimal(12, 2)), CAST(265475.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-09 18:28:46.527' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-09 18:28:46.523' AS DateTime), 69, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000032', N'1G1PG5SCXC7300724', 2015, N'Austin', N'Mini Cooper', N'jhk', CAST(65000.000 AS Decimal(13, 3)), N'Black', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(417890.00 AS Decimal(12, 2)), CAST(355207.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-13 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-09 19:00:33.220' AS DateTime), 69, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000033', N'1G1PG5SCXC7300724', 2016, N'Austin', N'Mini', N'jhk', CAST(65000.000 AS Decimal(13, 3)), N'Blue', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(62542.00 AS Decimal(12, 2)), CAST(62542.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-13 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-09 19:02:45.530' AS DateTime), 69, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'00001-000034', N'1G1PG5SCXC7300724', 1995, N'Buick', N'Coachbuilder', N'jhk', CAST(65000.000 AS Decimal(13, 3)), N'Blue', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(419345.00 AS Decimal(12, 2)), CAST(356443.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-09 19:05:30.053' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-09 19:05:30.053' AS DateTime), 69, NULL, NULL, 184, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'11111-000001', N'1G1PG5SCXC7300728', 1995, N'Buick', N'Coachbuilder', N'546', CAST(0.000 AS Decimal(13, 3)), N'Blue', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(10000.00 AS Decimal(12, 2)), CAST(1000.00 AS Decimal(12, 2)), N'Ddfdsfsdf dsf dsfd', CAST(N'2016-03-13 22:38:37.880' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-13 22:38:37.880' AS DateTime), 62, NULL, NULL, 182, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'11111-000002', N'1G1PG5SCXC7300728', 1995, N'Buick', N'Estate', N'gfhgf', CAST(0.000 AS Decimal(13, 3)), N'453', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(20000.00 AS Decimal(12, 2)), CAST(2000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-13 22:39:10.273' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-13 22:39:10.273' AS DateTime), 62, NULL, NULL, 182, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'11111111111111111111', N'11223444566778788', 2013, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'42343543244232342', N'2143245325', CAST(123213.00 AS Decimal(12, 2)), CAST(8624.91 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 07:50:20.013' AS DateTime), 164, NULL, NULL, 241, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000001', N'1HGCM82633A004352', 2015, N'Austin', N'Mini Cooper', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'aew', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(98.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-04 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-04 18:10:46.250' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000002', N'1HGCM82633A004352', 2017, N'Audi', N'S4', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1223.00 AS Decimal(12, 2)), CAST(978.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 12:07:28.010' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000003', N'1HGCM82633A004352', 2014, N'BMW', N'5 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(132324.00 AS Decimal(12, 2)), CAST(105859.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 12:13:57.350' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000004', N'1HGCM82633A004352', 2015, N'BMW', N'3 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(98.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 13:19:03.957' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000005', N'1HGCM82633A004352', 2013, N'BMW', N'6 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1233.00 AS Decimal(12, 2)), CAST(986.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-07 15:00:33.297' AS DateTime), 57, NULL, 57, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000006', N'1HGCM82633A004352', 2010, N'Chevrolet', N'Corvette', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(123.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1234.00 AS Decimal(12, 2)), CAST(987.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 15:04:43.000' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-07 15:05:23.367' AS DateTime), 57, NULL, 57, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000007', N'1HGCM82633A004352', 2014, N'Buick', N'Coachbuilder', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1213.00 AS Decimal(12, 2)), CAST(970.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 15:10:48.910' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000008', N'1HGCM82633A004352', 2016, N'Buick', N'Electra', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(123.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12332.00 AS Decimal(12, 2)), CAST(9866.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-07 15:27:35.947' AS DateTime), 57, NULL, 57, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000009', N'1HGCM82633A004352', 2015, N'Austin', N'Mini', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(98.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 15:41:05.350' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000010', N'1HGCM82633A004352', 2015, N'Austin', N'Mini', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(98.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 15:41:38.807' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000011', N'1HGCM82633A004352', 2015, N'BMW', N'3 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(98.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 15:45:07.103' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000012', N'1HGCM82633A004352', 2016, N'Austin', N'Mini Cooper', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12344.00 AS Decimal(12, 2)), CAST(9875.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-07 15:55:44.410' AS DateTime), 57, NULL, 57, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000013', N'1HGCM82633A004352', 2016, N'Audi', N'S4', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1233.00 AS Decimal(12, 2)), CAST(986.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-07 17:26:12.893' AS DateTime), 57, NULL, 57, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000014', N'1HGCM82633A004352', 2015, N'BMW', N'1 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(123.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1234.00 AS Decimal(12, 2)), CAST(987.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-07 19:37:02.330' AS DateTime), 57, NULL, NULL, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000015', N'1HGCM82633A004352', 2015, N'BMW', N'3 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(121.00 AS Decimal(12, 2)), CAST(97.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-07 19:50:23.000' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-07 19:50:58.677' AS DateTime), 57, NULL, 57, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000016', N'1HGCM82633A004352', 2017, N'Austin', N'Mini', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(13213.00 AS Decimal(12, 2)), CAST(10570.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 15:19:07.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-10 15:20:08.113' AS DateTime), 57, NULL, NULL, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000017', N'1HGCM82633A004352', 2016, N'Audi', N'S4', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(123.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(14354.00 AS Decimal(12, 2)), CAST(11483.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-11 16:53:24.370' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-11 16:53:24.370' AS DateTime), 57, NULL, 57, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000018', N'1HGCM82633A004352', 2016, N'BMW', N'3 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(13214.00 AS Decimal(12, 2)), CAST(10571.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-11 16:56:55.393' AS DateTime), 57, NULL, NULL, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000019', N'1FAHP26W49G252740', 2015, N'Buick', N'LeSabre', N'23', CAST(123.000 AS Decimal(13, 3)), N'Blue', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(134.00 AS Decimal(12, 2)), CAST(107.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 17:30:55.533' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-12 17:30:55.533' AS DateTime), 57, NULL, 57, 179, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000020', N'qwertyuiop0987654', 2015, N'wqeqwr', N'rewtet', NULL, CAST(212.000 AS Decimal(13, 3)), NULL, 0, CAST(12.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(32144.00 AS Decimal(12, 2)), CAST(25715.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-16 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-18 15:15:05.260' AS DateTime), 57, NULL, NULL, 179, 2, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000021', N'qwertyuiop1234234', 2016, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(12.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(98.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 11:02:10.610' AS DateTime), 57, NULL, NULL, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000022', N'1FAHP26W49G252740', 2016, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(12.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12.00 AS Decimal(12, 2)), CAST(9.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 11:10:19.220' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-23 11:10:19.220' AS DateTime), 57, NULL, 57, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000023', N'kasunsamarawickra', 2017, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(123.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1234.00 AS Decimal(12, 2)), CAST(987.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 11:12:21.420' AS DateTime), 57, NULL, NULL, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000024', N'1FAHP26W49G252740', 2015, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(121.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(13123.00 AS Decimal(12, 2)), CAST(3000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 11:15:58.463' AS DateTime), 57, NULL, NULL, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000025', N'1FAHP26W49G252740', 2017, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(12.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1300.00 AS Decimal(12, 2)), CAST(1040.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-26 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 12:38:57.017' AS DateTime), 57, NULL, NULL, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000026', N'1FAHP26W49G252740', 2015, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(313.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(212.00 AS Decimal(12, 2)), CAST(169.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 12:43:02.507' AS DateTime), 57, NULL, NULL, 179, 3, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000027', N'1FAHP26W49G252740', 2016, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(13.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1231.00 AS Decimal(12, 2)), CAST(984.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 13:50:05.823' AS DateTime), 57, NULL, NULL, 179, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000028', N'1FAHP26W49G252749', 2016, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(313.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(32132.00 AS Decimal(12, 2)), CAST(25705.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:13:37.583' AS DateTime), 57, NULL, NULL, 179, 3, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000029', N'QWERTYIIOOKJHFGVC', 2015, N'Arctic Cat', N'Thunder Cat', NULL, CAST(23.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(23.00 AS Decimal(12, 2)), CAST(18.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-26 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:20:30.990' AS DateTime), 57, NULL, NULL, 179, 4, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000030', N'1FAHP26W49G252748', 2014, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(123.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1323.00 AS Decimal(12, 2)), CAST(1058.40 AS Decimal(12, 2)), NULL, CAST(N'2016-02-28 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:24:22.027' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000031', N'1FAHP26W49G252748', 2012, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3123.00 AS Decimal(12, 2)), CAST(2498.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:28:30.247' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000032', N'1FAHP26W49G252790', 2014, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(123.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(98.40 AS Decimal(12, 2)), NULL, CAST(N'2016-04-28 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:31:23.970' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000033', N'1FAHP26W49G252740', 2015, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(123.00 AS Decimal(10, 2)), N'Gooseneck', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(23265.00 AS Decimal(12, 2)), CAST(18612.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:36:12.523' AS DateTime), 57, NULL, NULL, 179, 3, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000034', N'1FAHP26W49G252740', 2008, N'aDSAD', N'AFDSAF', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(21.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12312.00 AS Decimal(12, 2)), CAST(9849.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:42:34.263' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000035', N'1FAHP26W49G252740', 2010, N'DSAF', N'FSDAF', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(123.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(13214.00 AS Decimal(12, 2)), CAST(10571.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:45:05.527' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000036', N'1FAHP26W49G252740', 2011, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(123.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(132134.00 AS Decimal(12, 2)), CAST(105707.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-06 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 14:47:07.143' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000037', N'1FAHP26W49G252740', 2015, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(123.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1234.00 AS Decimal(12, 2)), CAST(987.20 AS Decimal(12, 2)), NULL, CAST(N'2016-02-22 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 15:04:29.223' AS DateTime), 57, NULL, NULL, 179, 3, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000038', N'1FAHP26W49G252740', 2016, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(12.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1000.00 AS Decimal(12, 2)), CAST(800.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 15:08:08.093' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000039', N'1FAHP26W49G252740', 2015, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(233.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4234.00 AS Decimal(12, 2)), CAST(3387.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:54:11.887' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000040', N'1FAHP26W49G252740', 2017, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(12.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12313.00 AS Decimal(12, 2)), CAST(9850.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:55:00.787' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000041', N'1FAHP26W49G252740', 2015, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(2143.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(32143.00 AS Decimal(12, 2)), CAST(25714.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:59:02.720' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000042', N'1FAHP26W49G252740', 2012, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(33.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2143.00 AS Decimal(12, 2)), CAST(1714.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 18:10:44.350' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000043', N'1FAHP26W49G252748', 2015, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(12.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(231.00 AS Decimal(12, 2)), CAST(184.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-29 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 18:11:18.697' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000044', N'1FAHP26W49G252740', 2015, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12213.00 AS Decimal(12, 2)), CAST(9770.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-30 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-24 18:45:31.600' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'123456-000045', N'1FAHP26W49G252740', 2006, N'aDAS', N'SDA', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12312.00 AS Decimal(12, 2)), CAST(9849.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 18:46:00.600' AS DateTime), 57, NULL, NULL, 179, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000001', N'36264646464646455', 1995, N'yjjhh', N'hjhj', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(50000.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(100000.00 AS Decimal(12, 2)), CAST(80000.00 AS Decimal(12, 2)), N'Bbnbn', CAST(N'2016-03-23 10:52:11.067' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-23 10:52:11.067' AS DateTime), 178, CAST(N'2016-03-23 14:28:59.310' AS DateTime), 178, 257, 3, 2)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000002', N'57486686867574544', 2010, N'Buick', N'Electra', N' hgjhj', CAST(45000.000 AS Decimal(13, 3)), N'Blue', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(100000.00 AS Decimal(12, 2)), CAST(80000.00 AS Decimal(12, 2)), N'Jjklkl', CAST(N'2016-03-23 10:53:19.617' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-23 10:53:19.617' AS DateTime), 178, CAST(N'2016-03-23 11:49:50.023' AS DateTime), 178, 257, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000003', N'68658648864846883', 2009, N'hjhj', N'ghh', NULL, CAST(400000.000 AS Decimal(13, 3)), NULL, 1, CAST(30000.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(200000.00 AS Decimal(12, 2)), CAST(160000.00 AS Decimal(12, 2)), N'Gghgh', CAST(N'2016-03-23 10:54:29.297' AS DateTime), 0, 1, 0, NULL, 1, CAST(N'2016-03-23 10:54:29.297' AS DateTime), 178, NULL, 178, 257, 2, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000004', N'36264646464646450', 2008, N'hghgh', N'ghghf', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(24345.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(40000.00 AS Decimal(12, 2)), CAST(32000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 13:36:58.853' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-23 13:36:58.853' AS DateTime), 178, NULL, 178, 257, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000005', N'68658648864846883', 2008, N'gfg', N'gfgf', NULL, CAST(34.000 AS Decimal(13, 3)), NULL, 1, CAST(343.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(4000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 17:12:59.533' AS DateTime), 0, 1, 0, NULL, 1, CAST(N'2016-03-23 17:12:59.533' AS DateTime), 178, NULL, 178, 257, 2, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000006', N'68658648864846884', 2004, N'ghgh', N'ghg', NULL, CAST(24432.000 AS Decimal(13, 3)), NULL, 1, CAST(353535.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(10000.00 AS Decimal(12, 2)), CAST(8000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 12:02:09.327' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-24 12:02:09.327' AS DateTime), 178, NULL, 178, 257, 2, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000007', N'1HGCM82633A004357', 2002, N'uui', N'yuy', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(454654.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(44444.00 AS Decimal(12, 2)), CAST(35555.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 14:05:11.640' AS DateTime), 0, 1, 0, NULL, 1, CAST(N'2016-03-24 14:02:21.320' AS DateTime), 178, NULL, 178, 257, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000008', N'1HGCM82633A004358', 2007, N'vfcf', N'dfdf', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(454654.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4545.00 AS Decimal(12, 2)), CAST(3636.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 14:07:44.560' AS DateTime), 0, 1, 0, NULL, 1, CAST(N'2016-03-24 14:07:22.263' AS DateTime), 178, NULL, 178, 257, 3, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000009', N'58488686868555757', 2007, N'Fairthorpe', N'Rockette', N'yuyuyui', CAST(89.000 AS Decimal(13, 3)), N'Brown', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(4000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 15:11:56.600' AS DateTime), 0, 1, 0, NULL, 1, CAST(N'2016-03-24 15:11:56.600' AS DateTime), 178, NULL, 178, 257, 1, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'12345678901-000010', N'1G1PG5SCXC7300728', 1995, N'Chevrolet', N'Corvair 500', N'saxs', CAST(89.000 AS Decimal(13, 3)), N'Brown', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3000.00 AS Decimal(12, 2)), CAST(2400.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 16:03:32.803' AS DateTime), 0, 0, 0, NULL, 0, CAST(N'2016-03-24 16:03:04.790' AS DateTime), 178, NULL, NULL, 257, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'35356532-000001', N'68658648864846891', 2010, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'tytytyrrtrytrytyt', N'trtytryrtytryr', CAST(241343.00 AS Decimal(12, 2)), CAST(217208.70 AS Decimal(12, 2)), N'Rrtr', CAST(N'2016-03-14 08:18:49.207' AS DateTime), 0, 0, 0, NULL, 0, CAST(N'2016-03-14 08:18:49.207' AS DateTime), 173, CAST(N'2016-03-22 17:58:15.560' AS DateTime), 173, 247, 5, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'35356532-000002', N'68618248863846890', 2006, N'gfgg', N'hfhghg', NULL, CAST(12.000 AS Decimal(13, 3)), NULL, 1, CAST(474.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3000.60 AS Decimal(12, 2)), CAST(2700.54 AS Decimal(12, 2)), N'Hh', CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 0, 0, NULL, 0, CAST(N'2016-03-14 09:37:51.810' AS DateTime), 173, NULL, NULL, 247, 2, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'35356532-000003', N'63614248563846890', 2017, N'Can Am', N'Outlander', NULL, CAST(544.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3000.46 AS Decimal(12, 2)), CAST(2700.46 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-14 09:39:29.737' AS DateTime), 173, NULL, 173, 247, 4, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'35356532-000004', N'43624642624626446', 2000, N'tytytyt', N'tyuryutry', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(2222.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(20000.00 AS Decimal(12, 2)), CAST(18000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 17:28:44.797' AS DateTime), 0, 0, 0, NULL, 1, CAST(N'2016-03-15 17:28:44.797' AS DateTime), 173, NULL, 173, 247, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'35356532-000005', N'35252554234454354', 2016, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'tryryryttyryrtyrt', N'ytttytytyeyyytyyy', CAST(3000.00 AS Decimal(12, 2)), CAST(2700.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 17:29:29.443' AS DateTime), 0, 0, 0, NULL, 1, CAST(N'2016-03-15 17:29:29.443' AS DateTime), 173, NULL, 173, 247, 5, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'35356532-000006', N'45243624662462466', 2005, N'yurtuyu', N'tytyty', NULL, CAST(2333.000 AS Decimal(13, 3)), NULL, 1, CAST(35235.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(50009.00 AS Decimal(12, 2)), CAST(45008.10 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 0, 0, NULL, 1, CAST(N'2016-03-16 10:38:27.710' AS DateTime), 173, NULL, NULL, 247, 2, 2)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'35356532-000007', N'68658648864846890', 2001, N'hhhj', N'jnnn', NULL, CAST(7.000 AS Decimal(13, 3)), NULL, 1, CAST(8.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(7000.00 AS Decimal(12, 2)), CAST(6300.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-31 00:00:00.000' AS DateTime), 1, 0, 0, NULL, 0, CAST(N'2016-03-16 10:45:56.163' AS DateTime), 173, NULL, NULL, 247, 2, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'3535659-000001', N'58488686868555755', 1989, N'Plymouth', N'Fury', N'tyghghh', CAST(500.000 AS Decimal(13, 3)), N'Blue', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(300000.00 AS Decimal(12, 2)), CAST(270000.00 AS Decimal(12, 2)), N'trytyttty', CAST(N'2016-03-12 11:38:46.710' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 11:38:46.710' AS DateTime), 149, NULL, NULL, 225, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'3535659-000002', N'46464563573575375', 2006, N'Fillmore', N'Fillmore', N'bvffff', CAST(554.000 AS Decimal(13, 3)), N'Brown', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(34355.00 AS Decimal(12, 2)), CAST(30920.00 AS Decimal(12, 2)), N'yty', CAST(N'2016-03-12 12:52:01.440' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 12:52:01.440' AS DateTime), 149, NULL, NULL, 225, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'3535659-000003', N'56547577357357577', 1999, N'Mercedes-Benz', N'SL-Class', N'yuyuyui', CAST(89.000 AS Decimal(13, 3)), N'Silver', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(436436.00 AS Decimal(12, 2)), CAST(392792.00 AS Decimal(12, 2)), N'rtyryty', CAST(N'2016-03-12 14:07:18.023' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 14:07:18.023' AS DateTime), 150, NULL, NULL, 225, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'3535659-000004', N'47575373575757575', 2012, N'Fillmore', N'Fillmore', N'uitiiyti', CAST(89.000 AS Decimal(13, 3)), N'Gray', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(424222.00 AS Decimal(12, 2)), CAST(381800.00 AS Decimal(12, 2)), N'hghghgh', CAST(N'2016-03-12 14:22:51.143' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 14:22:51.143' AS DateTime), 150, NULL, NULL, 225, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'3535659-000005', N'33333333333333333', 2015, N'BMW', N'1 Series', N'54545', CAST(54545.000 AS Decimal(13, 3)), N'Black', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(12333.00 AS Decimal(12, 2)), CAST(11100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 14:27:12.097' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 14:27:12.097' AS DateTime), 150, NULL, NULL, 225, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'3535659-000006', N'33333333333333333', 2015, N'Austin', N'Mini', N'54545', CAST(54545.000 AS Decimal(13, 3)), N'Blue', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1200.00 AS Decimal(12, 2)), CAST(1080.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 14:28:14.077' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 14:28:14.077' AS DateTime), 150, NULL, NULL, 225, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000001', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'34234', CAST(1000.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1000.00 AS Decimal(12, 2)), CAST(50.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 03:44:49.163' AS DateTime), 61, NULL, NULL, 190, 1, 4)
GO
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000002', N'1G1PG5SCXC7300728', 1995, N'Buick', N'Coachbuilder', N'dsfsd', CAST(3424.000 AS Decimal(13, 3)), N'Gray', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 03:52:09.310' AS DateTime), 0, 1, 0, NULL, 1, CAST(N'2016-03-23 03:52:09.310' AS DateTime), 61, NULL, 61, 190, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000003', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'dsfsd', CAST(1000.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3000.00 AS Decimal(12, 2)), CAST(150.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 03:52:41.023' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-23 03:52:41.023' AS DateTime), 61, NULL, 61, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000004', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'34234', CAST(1000.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4000.00 AS Decimal(12, 2)), CAST(200.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 05:12:34.343' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000005', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'dsfsd', CAST(1000.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(250.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-23 05:16:20.013' AS DateTime), 61, NULL, NULL, 190, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000006', N'1G1PG5SCXC7300728', 1995, N'Austin', N'Mini Cooper', N'34234', CAST(1000.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(6000.00 AS Decimal(12, 2)), CAST(300.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 05:55:47.197' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000007', N'1G1PG5SCXC7300728', 1995, N'Austin', N'Mini Cooper', N'34234', CAST(1000.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(6000.00 AS Decimal(12, 2)), CAST(300.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 06:41:35.207' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000008', N'1G1PG5SCXC7300728', 1995, N'Buick', N'Electra', N'dsfsd', CAST(3424.000 AS Decimal(13, 3)), N'Green', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1000.00 AS Decimal(12, 2)), CAST(50.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 06:56:11.947' AS DateTime), 61, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000009', N'1G1PG5SCXC7300728', 1995, N'Buick', N'Coachbuilder', N'dsfsd', CAST(3424.000 AS Decimal(13, 3)), N'White', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 06:56:44.223' AS DateTime), 61, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000010', N'1G1PG5SCXC7300728', 2016, N'BMW', N'3 Series', N'dsfsd', CAST(1000.000 AS Decimal(13, 3)), N'Gray', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 07:08:00.843' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000011', N'1G1PG5SCXC7300728', 1995, N'Austin', N'Mini Cooper', N'dsfsd', CAST(1000.000 AS Decimal(13, 3)), N'Gray', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3000.00 AS Decimal(12, 2)), CAST(150.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 07:21:06.693' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000012', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'dsfsd', CAST(2.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 07:38:24.663' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000013', N'1G1PG5SCXC7300728', 2016, N'Austin', N'Mini Cooper S', N'dsfsd', CAST(3424.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(250.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 07:45:26.440' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000014', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'34234', CAST(3424.000 AS Decimal(13, 3)), N'Gray', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 08:01:40.633' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000015', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'34234', CAST(3424.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 08:06:06.660' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000016', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'34234', CAST(3424.000 AS Decimal(13, 3)), N'Brown', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-23 08:14:53.980' AS DateTime), 61, NULL, NULL, 190, 1, 4)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000017', N'54545454545454545', 2010, N'545', N'545', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(27.25 AS Decimal(12, 2)), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 15:53:57.777' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000018', N'44444444444444444', 1986, N'Chrysler', N'Imperial', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5656.00 AS Decimal(12, 2)), CAST(282.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-26 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 15:59:15.937' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000019', N'54545454544545454', 2011, N'Hillman', N'5454', NULL, CAST(5454.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(656.00 AS Decimal(12, 2)), CAST(32.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:02:24.397' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000020', N'87878787878787878', 8787, N'878', N'878', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(7878.00 AS Decimal(12, 2)), CAST(393.90 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 16:12:42.807' AS DateTime), 0, 1, 0, NULL, 2, CAST(N'2016-03-24 16:12:42.807' AS DateTime), 2, NULL, 2, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000021', N'44444444444444444', 5454, N'545', N'545', N'545', CAST(45.000 AS Decimal(13, 3)), N'545', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(27.25 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:21:13.257' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000022', N'65656566565656566', 6566, N'6566', N'656', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(656.00 AS Decimal(12, 2)), CAST(32.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:21:41.910' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000023', N'66666666666666666', 6666, N'66', N'66', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(66.00 AS Decimal(12, 2)), CAST(3.30 AS Decimal(12, 2)), NULL, CAST(N'2016-03-19 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:26:19.483' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000024', N'89999999999999999', 9999, N'9999', N'999', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(999.00 AS Decimal(12, 2)), CAST(49.95 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:26:45.123' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000025', N'55555555555555555', 5555, N'555', N'5', NULL, CAST(555.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(555.00 AS Decimal(12, 2)), CAST(27.75 AS Decimal(12, 2)), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:40:52.667' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000026', N'88888888888888888', 8777, N'8787', N'87', NULL, CAST(878.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(87.00 AS Decimal(12, 2)), CAST(4.35 AS Decimal(12, 2)), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:41:29.037' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000027', N'45555555555555555', 5455, N'45454', N'545', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(27.25 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:51:04.383' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000028', N'67676766666666666', 7676, N'6766', N'7677', NULL, CAST(67.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8788.00 AS Decimal(12, 2)), CAST(439.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 16:57:44.010' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000029', N'54545454545454545', 4545, N'545', N'545', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(27.25 AS Decimal(12, 2)), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:04:21.620' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000030', N'75565656565656666', 6565, N'656', N'656', NULL, CAST(5656.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(656.00 AS Decimal(12, 2)), CAST(32.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:07:11.793' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000031', N'56565656565656565', 6565, N'76', N'6776', NULL, CAST(767.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(767.00 AS Decimal(12, 2)), CAST(38.35 AS Decimal(12, 2)), NULL, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:16:26.227' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000032', N'65656565656565656', 6566, N'656', N'6565', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(65656.00 AS Decimal(12, 2)), CAST(3282.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-19 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:26:26.780' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000033', N'77777777777777777', 6565, N'6566', N'65', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(656.00 AS Decimal(12, 2)), CAST(32.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-09 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:29:11.487' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000034', N'88888888888888888', 6565, N'656', N'656', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(656.00 AS Decimal(12, 2)), CAST(32.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-09 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:29:31.057' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'45678-000035', N'99999999999999999', 8787, N'878', N'87', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(878.00 AS Decimal(12, 2)), CAST(43.90 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-24 17:30:05.213' AS DateTime), 2, NULL, NULL, 190, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'5775774-000001', N'53532525553533333', 2000, N'retrttr', N'rtrtrt', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(454654.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(400000.00 AS Decimal(12, 2)), CAST(400000.00 AS Decimal(12, 2)), N'fggf', CAST(N'2016-03-12 15:47:14.723' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-12 15:47:14.723' AS DateTime), 162, NULL, 162, 239, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'asanka01-000001', N'1GKEK13T61J189877', 2015, N'Buick', N'Electra', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(10000.00 AS Decimal(12, 2)), CAST(8000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-24 18:49:40.093' AS DateTime), 154, NULL, NULL, 263, 1, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'asanka01-000002', N'WQE21321323213213', 1999, N'Test', N'Cam', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), N'Gooseneck', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8000.00 AS Decimal(12, 2)), CAST(6400.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-24 18:50:41.897' AS DateTime), 154, NULL, NULL, 263, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'asanka01-000003', N'SADWEQEWQSADWQ231', 1984, N'samplw', N'asdas', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8555.00 AS Decimal(12, 2)), CAST(6844.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 18:51:25.557' AS DateTime), 0, 1, 0, NULL, 1, CAST(N'2016-03-24 18:51:25.557' AS DateTime), 154, NULL, 154, 263, 7, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'asanka01-000004', N'34324324FDFSDFSDF', 2014, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(10000.00 AS Decimal(12, 2)), CAST(8000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-01 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-24 18:55:39.083' AS DateTime), 154, NULL, NULL, 263, 5, 1)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'darshana1-000001', N'44343434434344444', 4343, N'434', N'434', NULL, CAST(43434.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4343.00 AS Decimal(12, 2)), CAST(3344.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 14:10:14.803' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-12 14:10:14.803' AS DateTime), 157, NULL, 157, 232, 4, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'darshana1-000002', N'REREREREREERERERE', 1994, N'rererer', N'rerererer', NULL, CAST(4545.000 AS Decimal(13, 3)), N'rerere', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(33433.00 AS Decimal(12, 2)), CAST(25743.41 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 21:47:22.340' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 21:47:22.340' AS DateTime), 157, NULL, NULL, 232, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'darshana1-000003', N'54545454545454554', 4545, N'988', N'9899', N'989', CAST(9898.000 AS Decimal(13, 3)), N'899', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(989.00 AS Decimal(12, 2)), CAST(761.53 AS Decimal(12, 2)), N'9989', CAST(N'2016-03-14 08:09:10.593' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-14 08:09:10.593' AS DateTime), 157, NULL, NULL, 232, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'darshana1-000004', N'8iiiiiiiii787', 2015, N'HE1', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(544.00 AS Decimal(12, 2)), CAST(418.88 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:10:35.350' AS DateTime), 157, NULL, NULL, 232, 8, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'darshana1-000005', N'55343434343434343', 4344, N'6556', N'4344', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4334.00 AS Decimal(12, 2)), CAST(4334.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:13:07.267' AS DateTime), 157, NULL, NULL, 232, 4, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'darshana1-000006', N'54545545555555555', 2014, N'Citroën', N'2CV', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(666.00 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 10:33:05.137' AS DateTime), 157, NULL, NULL, 232, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'darshana1-000007', N'54545454544555555', 2012, N'Yamaha', N'hfg', NULL, CAST(454.000 AS Decimal(13, 3)), N'gfgf', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(54.00 AS Decimal(12, 2)), CAST(41.58 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 08:28:35.717' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-15 08:28:35.717' AS DateTime), 157, NULL, NULL, 232, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000001', N'11111111111111111', 2015, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'11111111111111111', N'11111111111111111', CAST(125000.00 AS Decimal(12, 2)), CAST(100000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:26:14.277' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000002', N'11111111111111111', 2014, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'11111111111111111', N'11111111111111111', CAST(111.89 AS Decimal(12, 2)), CAST(89.51 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 08:27:18.393' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-14 08:27:18.393' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000003', N'5yy66464646466464', 6466, N'6466', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'64664644646665666', N'64466', CAST(66.00 AS Decimal(12, 2)), CAST(12.40 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:29:24.563' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000004', N'45454554545454545', 5455, N'7575', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, N'545', CAST(76.00 AS Decimal(12, 2)), CAST(6.55 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:31:03.137' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000005', N'11111111111111111', 6566, N'6566', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(66.00 AS Decimal(12, 2)), CAST(4.30 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:33:18.260' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000006', N'11111111111111111', 6566, N'6566', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(77.60 AS Decimal(12, 2)), CAST(7.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:35:32.080' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000007', N'11111111111111111', 2013, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'11111111111111111', N'11111111111111111', CAST(123.00 AS Decimal(12, 2)), CAST(11.41 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:39:10.853' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000008', N'11111111111111111', 2015, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'11111111111111111', N'11111111111111111', CAST(12300.00 AS Decimal(12, 2)), CAST(41.41 AS Decimal(12, 2)), NULL, CAST(N'2016-03-16 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:43:12.610' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000009', N'11111111111111111', 2016, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'11111111111111111', N'11111111111111111', CAST(12.00 AS Decimal(12, 2)), CAST(9.61 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:55:59.107' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000010', N'11111111111111111', 2016, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, N'123', CAST(12300.00 AS Decimal(12, 2)), CAST(9840.45 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:59:13.017' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000011', N'12222222222222222', 2016, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(23.00 AS Decimal(12, 2)), CAST(18.41 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:00:18.590' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000012', N'12222222222222222', 2013, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(12.00 AS Decimal(12, 2)), CAST(9.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-16 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:02:56.293' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000013', N'12222222222222222', 2015, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(34.00 AS Decimal(12, 2)), CAST(27.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-16 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:04:22.133' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000014', N'12222222222222222', 2015, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(12.00 AS Decimal(12, 2)), CAST(9.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:06:02.627' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000015', N'12222222222222222', 2015, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(34.00 AS Decimal(12, 2)), CAST(27.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-17 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:06:29.733' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000016', N'12222222222222222', 2015, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(12.00 AS Decimal(12, 2)), CAST(9.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:07:51.530' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000017', N'12222222222222222', 2016, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(34.00 AS Decimal(12, 2)), CAST(27.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-16 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:08:41.727' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000018', N'13333333333333333', 2016, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'13333333333333333', N'13333333333333333', CAST(12.00 AS Decimal(12, 2)), CAST(9.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:11:07.663' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000019', N'13333333333333333', 2011, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'13333333333333333', N'13333333333333333', CAST(34.00 AS Decimal(12, 2)), CAST(27.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:11:34.683' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000020', N'12222222222222222', 2014, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'12222222222222222', N'12222222222222222', CAST(12.00 AS Decimal(12, 2)), CAST(9.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:15:29.547' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000021', N'11111111111111111', 2014, N'Mercury', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'11111111111111111', N'11111111111111111', CAST(12.00 AS Decimal(12, 2)), CAST(9.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:19:29.617' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'Dealer0001-000022', N'11111111111111111', 2015, N'Poseidon', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'11111111111111111', N'11111111111111111', CAST(34.00 AS Decimal(12, 2)), CAST(27.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-16 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:20:03.487' AS DateTime), 172, NULL, NULL, 248, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'hhhh11-000001', N'1GKEK13T61J189877', 2000, N'BMW', N'600', N'rr', CAST(24324.000 AS Decimal(13, 3)), N'Gray', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(20000.00 AS Decimal(12, 2)), CAST(16000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 12:26:39.917' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 12:26:39.917' AS DateTime), 160, NULL, NULL, 237, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'hhhh11-000002', N'1GKEK13T61J189877', 2000, N'BMW', N'600', N'pp', CAST(3423.000 AS Decimal(13, 3)), N'Blue', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8000.00 AS Decimal(12, 2)), CAST(6400.00 AS Decimal(12, 2)), N'1555', CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 12:42:39.630' AS DateTime), 160, NULL, NULL, 237, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'hhhh11-000003', N'1GKEK13T61J189877', 2015, N'BMW', N'3 Series', N'ere', CAST(234324.000 AS Decimal(13, 3)), N'Gray', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(50000.00 AS Decimal(12, 2)), CAST(40000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 13:15:36.327' AS DateTime), 160, NULL, NULL, 237, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'hhhh11-000004', N'1GKEK13T61J189877', 2010, N'Chrysler', N'Imperial', N'erwe', CAST(3423.000 AS Decimal(13, 3)), N'Brown', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(90000.00 AS Decimal(12, 2)), CAST(72000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 13:17:20.467' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 13:17:20.467' AS DateTime), 160, NULL, NULL, 237, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'hhhh11-000005', N'1GKEK13T61J189877', 2015, N'BMW', N'1 Series', N'tt', CAST(22.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(4000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 08:52:24.117' AS DateTime), 160, NULL, NULL, 237, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000001', N'78787878787887777', 1748, N'Plymoutherer', N'Fury', NULL, CAST(5555.000 AS Decimal(13, 3)), N'Gray55', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(25000.00 AS Decimal(12, 2)), CAST(18750.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-03 09:11:50.860' AS DateTime), 69, NULL, NULL, 192, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000002', N'56565656565666666', 2004, N'Studebaker', N'Avanti', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(65665.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(9898.00 AS Decimal(12, 2)), CAST(7424.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-06 15:29:25.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-06 15:32:33.957' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000003', N'53553334345354444', 1994, N'Mercedes-Benz', N'S-Class', N'54545', CAST(45454.000 AS Decimal(13, 3)), N'Silver', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(54545.00 AS Decimal(12, 2)), CAST(40909.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 10:35:31.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 10:36:59.040' AS DateTime), 69, NULL, NULL, 192, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000004', N'33333333333333333', 1994, N'Mazda', N'929', N'77676', CAST(776767.000 AS Decimal(13, 3)), N'Silver', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(767.00 AS Decimal(12, 2)), CAST(575.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 10:39:16.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 10:41:32.870' AS DateTime), 69, NULL, NULL, 192, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000005', N'54545454545454555', 2010, N'Ford', N'LTD Crown Victoria', N'54545', CAST(54545.000 AS Decimal(13, 3)), N'White', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(54545.00 AS Decimal(12, 2)), CAST(40909.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 10:42:10.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 10:43:53.013' AS DateTime), 69, NULL, NULL, 192, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000006', N'54545454545454555', 2013, N'Hyundai', N'Genesis Coupe', N'trt', CAST(78.000 AS Decimal(13, 3)), N'trt', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(7676.00 AS Decimal(12, 2)), CAST(5757.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 10:53:45.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 10:55:12.330' AS DateTime), 69, NULL, NULL, 192, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000007', N'54545454545454555', 5444, N'45455', N'545', N'54545', CAST(454.000 AS Decimal(13, 3)), N'4545', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(54545.00 AS Decimal(12, 2)), CAST(40909.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 10:57:35.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 10:58:30.433' AS DateTime), 69, NULL, NULL, 192, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000012', N'33333333333333898', 6566, N'6566', N'656', N'65656', CAST(65656.000 AS Decimal(13, 3)), N'656', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(65656.00 AS Decimal(12, 2)), CAST(49242.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 13:27:56.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 13:31:27.277' AS DateTime), 69, NULL, NULL, 192, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000013', N'1GCHK23638F115208', 5656, N'Infiniti', N'4344', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(65656.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5656.00 AS Decimal(12, 2)), CAST(4242.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 15:38:05.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 15:51:47.063' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000014', N'65565656565656656', 2003, N'8787', N'67667', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(767.00 AS Decimal(12, 2)), CAST(575.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-08 16:40:29.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-08 16:50:03.847' AS DateTime), 69, NULL, NULL, 192, 7, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000015', N'54545454544555555', 6466, N'64646', N'64666', NULL, CAST(6466.000 AS Decimal(13, 3)), N'64646', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(6646.00 AS Decimal(12, 2)), CAST(4985.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-09 11:56:44.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-09 12:03:33.583' AS DateTime), 69, NULL, NULL, 192, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000016', N'12121212122121221', 2121, N'212', N'211', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(2122.00 AS Decimal(10, 2)), N'Gooseneck', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(21212.00 AS Decimal(12, 2)), CAST(15909.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-09 14:36:43.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-09 14:50:25.860' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000026', N'1HGCM82633A004352', 2016, N'Austin', N'Mini Cooper', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(250.00 AS Decimal(12, 2)), CAST(250.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-09 18:55:21.420' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000027', N'54545454545545455', 5454, N'54545', N'4545', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(54545.00 AS Decimal(10, 2)), N'Gooseneck', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(454545.00 AS Decimal(12, 2)), CAST(340909.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 12:24:31.230' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 12:24:31.230' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000028', N'65656565656565655', 6566, N'65656', N'65656', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(5656.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(565565.00 AS Decimal(12, 2)), CAST(4174.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 12:26:35.430' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 12:26:35.430' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000029', N'65656656556565665', 6566, N'656', N'6', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(5656.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(6656.00 AS Decimal(12, 2)), CAST(4992.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 12:30:03.017' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 12:30:03.017' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000030', N'JTEHF21AX30107111', 2003, N'BMW', N'600', N'342', CAST(35435432.000 AS Decimal(13, 3)), N'Silver', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(42342.00 AS Decimal(12, 2)), CAST(31757.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 15:12:31.370' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 15:12:31.370' AS DateTime), 69, NULL, NULL, 192, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000031', N'JTEHF21AX30107111', 2003, N'Mazda', N'929', NULL, CAST(3423423432.000 AS Decimal(13, 3)), NULL, 0, CAST(2234.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8555.00 AS Decimal(12, 2)), CAST(6416.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 15:18:33.170' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 15:18:33.170' AS DateTime), 69, NULL, NULL, 192, 2, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000032', N'JTEHF21AX30107111', 1995, N'fdgfd', N'fgfg', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(10000.00 AS Decimal(12, 2)), CAST(7500.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 15:22:58.230' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 15:22:58.230' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000033', N'1HGCM82633A004352', 2016, N'Audi', N'S4', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2213.00 AS Decimal(12, 2)), CAST(1660.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 16:09:14.037' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 16:09:14.037' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000034', N'1HGCM82633A004352', 2016, N'Austin', N'Mini', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2313.00 AS Decimal(12, 2)), CAST(1735.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-23 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-10 16:24:29.700' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000035', N'1HGCM82633A004352', 2016, N'Austin', N'Mini', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(1233.00 AS Decimal(12, 2)), CAST(3925.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 16:33:39.737' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-10 16:33:39.737' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000036', N'1HGCM82633A004352', 2016, N'Austin', N'Mini Cooper', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(1232.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(23456.00 AS Decimal(12, 2)), CAST(17592.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-28 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-10 16:34:35.033' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000037', N'55656565656555555', 7676, N'7676', N'6767', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(7676.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(7676.00 AS Decimal(12, 2)), CAST(5757.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-11 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-11 09:41:51.610' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000038', N'66445454545444444', 5454, N'565', N'6565', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(656.00 AS Decimal(10, 2)), N'Gooseneck', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5445.00 AS Decimal(12, 2)), CAST(4084.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-11 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-11 14:25:56.273' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000039', N'65656656565656565', 6566, N'gfgfg', N'rtrt', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(565.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(4554.00 AS Decimal(12, 2)), CAST(3415.50 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 18:38:47.787' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 18:38:47.787' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000040', N'RERERERERRERRRRRR', 1994, N'rreee', N'rerer', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(4343.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(433.00 AS Decimal(12, 2)), CAST(324.75 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 21:27:14.010' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 21:27:14.010' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
GO
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000041', N'43434343343434344', 4344, N'4344', N'43434', NULL, CAST(43434.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(44.00 AS Decimal(12, 2)), CAST(33.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 21:29:41.030' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 21:29:41.030' AS DateTime), 69, NULL, NULL, 192, 4, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000042', N'45454554545454545', 5455, N'5455', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'54545554545454545', N'54545', CAST(545.00 AS Decimal(12, 2)), CAST(408.75 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:16:07.970' AS DateTime), 69, NULL, NULL, 192, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000043', N'454545545454545', 5454, N'545454', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'54545454545454545', N'54545', CAST(554.00 AS Decimal(12, 2)), CAST(415.50 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:17:57.307' AS DateTime), 69, NULL, NULL, 192, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000044', N'65565656565656565', 6566, N'656', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'65666565656565656', N'656', CAST(56.00 AS Decimal(12, 2)), CAST(7.20 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:21:46.570' AS DateTime), 69, NULL, NULL, 192, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000045', N'66666666666666666', 2016, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(678.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(889.89 AS Decimal(12, 2)), CAST(667.42 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 08:23:30.227' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-14 08:23:30.227' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000046', N'5454545455454', 5455, N'54545', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, N'54545', CAST(5454.00 AS Decimal(12, 2)), CAST(4090.50 AS Decimal(12, 2)), NULL, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 10:30:28.597' AS DateTime), 69, NULL, NULL, 192, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000047', N'ttttttttttttttttt', 5666, N'tytyty', N'ytytyty', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(556.00 AS Decimal(12, 2)), CAST(417.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 16:33:51.627' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-15 16:33:51.627' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000048', N'YYYYYYYYYYYYYYYYY', 4454, N'4545', N'545', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(545.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(408.75 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 16:37:12.880' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-15 16:37:12.880' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000049', N'34343443434343434', 3434, N'4343', N'43434', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(43434.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(34.00 AS Decimal(12, 2)), CAST(25.50 AS Decimal(12, 2)), NULL, CAST(N'2016-03-21 10:02:42.737' AS DateTime), 0, 0, 0, NULL, 0, CAST(N'2016-03-21 10:02:42.737' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000050', N'54545545454545454', 5455, N'545', N'45445', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(545.00 AS Decimal(12, 2)), CAST(408.75 AS Decimal(12, 2)), NULL, CAST(N'2016-03-21 10:03:10.877' AS DateTime), 0, 0, 0, NULL, 2, CAST(N'2016-03-21 10:03:10.877' AS DateTime), 69, NULL, NULL, 192, 7, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000051', N'rerererererer', 3434, N'4343', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'rer43434343434343', N'4343', CAST(444.00 AS Decimal(12, 2)), CAST(333.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-21 10:04:39.140' AS DateTime), 0, 0, 0, NULL, 2, CAST(N'2016-03-21 10:04:39.140' AS DateTime), 69, NULL, NULL, 192, 5, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000052', N'yttytytytytytytyt', 2012, N'CMake2', N'CModel', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), N'Gooseneck', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(75.00 AS Decimal(12, 2)), CAST(56.25 AS Decimal(12, 2)), NULL, CAST(N'2016-03-21 10:05:37.010' AS DateTime), 0, 0, 0, NULL, 0, CAST(N'2016-03-21 10:05:37.010' AS DateTime), 69, NULL, NULL, 192, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000053', N'00000000000000000', 8888, N'8888', N'4555', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(888.00 AS Decimal(10, 2)), N'5th Wheel', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(777.00 AS Decimal(12, 2)), CAST(582.75 AS Decimal(12, 2)), NULL, CAST(N'2016-03-25 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 2, CAST(N'2016-03-24 17:55:24.197' AS DateTime), 69, NULL, NULL, 192, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loan1-000054', N'34343443434343434', 2015, N'Cmake', N'CModel2', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(12.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(123.00 AS Decimal(12, 2)), CAST(92.25 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime), 1, 1, 0, NULL, 1, CAST(N'2016-03-24 18:42:57.487' AS DateTime), 69, NULL, NULL, 192, 3, 0)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000001', N'1GKEK13T61J189877', 2016, N'Buick', N'Electra', N'LT', CAST(33242.000 AS Decimal(13, 3)), N'Black', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8000.00 AS Decimal(12, 2)), CAST(6400.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-10 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 11:18:56.173' AS DateTime), 154, NULL, NULL, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000002', N'1GKEK13T61J189877', 2015, N'Buick', N'LeSabre', N'ee', CAST(2132.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8000.00 AS Decimal(12, 2)), CAST(6400.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 11:22:19.663' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-12 11:22:19.663' AS DateTime), 154, NULL, 154, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000003', N'1GKEK13T61J189877', 1993, N'test', N'Test model', N'ttt', CAST(22222.000 AS Decimal(13, 3)), N'tte', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(4000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 11:23:21.770' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-12 11:23:21.770' AS DateTime), 154, NULL, 154, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000004', N'EREWREREWRWERWERE', 2016, N'BMW', N'erwerwerwerererverewr eerer', NULL, CAST(4.000 AS Decimal(13, 3)), N'Blue', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8501.00 AS Decimal(12, 2)), CAST(6800.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 19:47:54.047' AS DateTime), 154, NULL, NULL, 229, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000005', N'1GKEK13T61J189877', 2015, N'Austin', N'Mini', NULL, CAST(2.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(8521.00 AS Decimal(12, 2)), CAST(6816.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 19:50:08.277' AS DateTime), 154, NULL, NULL, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000006', N'8888888888888888', 2015, N'HE1', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(3522.00 AS Decimal(12, 2)), CAST(2817.60 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 19:51:54.950' AS DateTime), 154, NULL, NULL, 229, 8, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000007', N'1GKEK13T61J189877', 2010, N'Cadillac ', N'Fleetwood', N'ss', CAST(10.000 AS Decimal(13, 3)), N'Blue', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5621.00 AS Decimal(12, 2)), CAST(4496.80 AS Decimal(12, 2)), N'Ssss ssss ssss', CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 07:29:25.520' AS DateTime), 154, NULL, NULL, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000008', N'1GKEK13T61J189877', 2003, N'bu', N'r', N'lt', CAST(3.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, NULL, NULL, NULL, CAST(500.00 AS Decimal(12, 2)), CAST(400.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-03 00:00:00.000' AS DateTime), 154, NULL, NULL, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000009', N'1GKEK13T61J189877', 2016, N'BMW', N'3 Series', N'lt', CAST(3.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(4000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-19 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 22:46:16.917' AS DateTime), 154, NULL, NULL, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000010', N'QWEWEWQEWQEWQEWQE', 2016, N'ddfsd', N'dsfds', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(2.00 AS Decimal(10, 2)), N'Bumper Pull', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(4000.00 AS Decimal(12, 2)), N'Aasda', CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-12 22:47:35.763' AS DateTime), 154, NULL, NULL, 229, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000011', N'324324234234FDSFF', 2015, N'Make1', N'sdfsdf', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(4000.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 22:48:20.483' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-12 22:48:20.483' AS DateTime), 154, NULL, 154, 229, 7, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000012', N'423423432sdffdsfsdfd', 2015, N'HE2', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(2000.00 AS Decimal(12, 2)), CAST(1600.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 22:50:08.867' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-12 22:50:08.867' AS DateTime), 154, NULL, NULL, 229, 8, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000013', N'1GNDT13S682198375', 2015, N'BMW', N'3 Series', N'rr', CAST(3.000 AS Decimal(13, 3)), N'Brown', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(850.00 AS Decimal(12, 2)), CAST(680.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-11 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 08:29:17.703' AS DateTime), 154, NULL, NULL, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'loanfull-000014', N'1GNDT13S682198375', 2016, N'BMW', N'6 Series', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(9856.00 AS Decimal(12, 2)), CAST(7884.80 AS Decimal(12, 2)), NULL, CAST(N'2016-03-19 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 05:33:07.843' AS DateTime), 154, NULL, NULL, 229, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'LoanNum1-000001', N'hffffffffffffffff', 5455, N'5455', N'4555', NULL, CAST(5555.000 AS Decimal(13, 3)), N'5455', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(55.00 AS Decimal(12, 2)), CAST(42.35 AS Decimal(12, 2)), NULL, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-14 09:27:18.610' AS DateTime), 175, NULL, NULL, 249, 6, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'rtret-000001', N'1GNDT13S682198375', 2008, N'BMW', N'Navigator L', N'L', CAST(96325.000 AS Decimal(13, 3)), N'White', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(100000.00 AS Decimal(12, 2)), CAST(1000.00 AS Decimal(12, 2)), N'ss', CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-11 18:09:33.643' AS DateTime), 154, NULL, NULL, 226, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'rtret-000002', N'SASDADSALLKK', 1995, N'BMW', N'', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), N'wewq', N'e32', CAST(55312.00 AS Decimal(12, 2)), CAST(553.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-12 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-11 18:30:56.117' AS DateTime), 154, NULL, NULL, 226, 5, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'rtret-000003', N'1GNDT13S682198375', 2010, N'Mazda', N'626', N'T', CAST(3333.000 AS Decimal(13, 3)), N'Black', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(85231.00 AS Decimal(12, 2)), CAST(852.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-24 00:00:00.000' AS DateTime), 1, 1, 0, NULL, NULL, CAST(N'2016-03-11 18:34:44.630' AS DateTime), 154, NULL, NULL, 226, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'rtret-000004', N'1GNDT13S682198375', 2015, N'Buick', N'Electra', N'LK', CAST(522.000 AS Decimal(13, 3)), N'Black', 0, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(98566.00 AS Decimal(12, 2)), CAST(986.00 AS Decimal(12, 2)), NULL, CAST(N'2016-03-11 18:38:04.497' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-11 18:38:04.497' AS DateTime), 154, NULL, 154, 226, 1, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'testloan1-000001', N'1G1PG5SCXC7300728', 1995, N'Buick', N'Regal', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(3.00 AS Decimal(10, 2)), N'rgrg', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(43550.00 AS Decimal(12, 2)), CAST(4355.00 AS Decimal(12, 2)), N'gfhh', CAST(N'2016-03-01 07:33:28.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-01 07:36:46.997' AS DateTime), 2, NULL, 2, 189, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'testloan1-000002', N'1G1PG5SCXC7300728', 1995, N'Buick', N'Regal', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(3.00 AS Decimal(10, 2)), N'rgrg', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(43550.00 AS Decimal(12, 2)), CAST(4355.00 AS Decimal(12, 2)), N'gfhh', CAST(N'2016-03-01 07:33:28.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-01 07:36:59.953' AS DateTime), 2, NULL, 2, 189, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'testloan1-000003', N'1HGCM82633A004352', 2010, N'Chevrolet', N'Corvette', NULL, CAST(35.000 AS Decimal(13, 3)), NULL, 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(5000.00 AS Decimal(12, 2)), CAST(500.00 AS Decimal(12, 2)), N'iyuiu', CAST(N'2016-03-01 07:45:01.000' AS DateTime), 0, 0, 0, NULL, NULL, CAST(N'2016-03-01 07:47:24.457' AS DateTime), 2, NULL, NULL, 189, 4, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'testloan1-000004', N'1HGCM82633A004352', 2010, N'Chevrolet', N'Corvette', NULL, CAST(0.000 AS Decimal(13, 3)), NULL, 1, CAST(5.00 AS Decimal(10, 2)), N'hgh', CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(34000.00 AS Decimal(12, 2)), CAST(3400.00 AS Decimal(12, 2)), N'rtert', CAST(N'2016-03-01 07:55:50.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-03-01 07:56:48.853' AS DateTime), 61, NULL, 61, 189, 3, NULL)
INSERT [dbo].[unit] ([unit_id], [identification_number], [year], [make], [model], [trim], [miles], [color], [new_or_used], [length], [hitch_style], [speed], [trailer_id], [engine_serial], [cost], [advance_amount], [note], [advance_date], [add_or_advance], [is_advanced], [is_approved], [status], [unit_status], [created_date], [created_by], [modified_date], [modified_by], [loan_id], [unit_type_id], [title_status]) VALUES (N'testloan1-000005', N'1G1PG5SCXC7300728', 1995, N'BMW', N'600', N'45', CAST(2.000 AS Decimal(13, 3)), N'Brown', 1, CAST(0.00 AS Decimal(10, 2)), NULL, CAST(0.00 AS Decimal(7, 2)), NULL, NULL, CAST(222222.00 AS Decimal(12, 2)), CAST(22222.00 AS Decimal(12, 2)), N'rregte', CAST(N'2016-02-29 00:00:00.000' AS DateTime), 0, 1, 0, NULL, NULL, CAST(N'2016-02-29 20:55:45.777' AS DateTime), 2, NULL, 2, 189, 1, NULL)
SET IDENTITY_INSERT [dbo].[unitType] ON 

INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (1, N'Vehicles')
INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (2, N'RV')
INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (3, N'Camper')
INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (4, N'ATV')
INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (5, N'Boat')
INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (6, N'Motorcycle')
INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (7, N'Snowmobile')
INSERT [dbo].[unitType] ([unit_type_id], [unit_type_name]) VALUES (8, N'Heavy Equipment')
SET IDENTITY_INSERT [dbo].[unitType] OFF
SET IDENTITY_INSERT [dbo].[uploadTitle] ON 

INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (11, N'~/Images/UnitImages/DEH01//hhhh11-000003_01.jpg', N'hhhh11-000003', N'20160216_2000_Lexus_ES300_6.jpg')
INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (12, N'~/Images/UnitImages/DEH01//hhhh11-000003_02.jpg', N'hhhh11-000003', N'20160216_2000_Lexus_ES300_6.jpg')
INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (13, N'~/Images/UnitImages/DEH01//hhhh11-000003_03.jpg', N'hhhh11-000003', N'20160216_2000_Lexus_ES300_8.jpg')
INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (14, N'~/Images/UnitImages/DEH01//hhhh11-000003_04.jpg', N'hhhh11-000003', N'20160216_2000_Lexus_ES300_19.jpg')
INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (15, N'~/Images/UnitImages/DEH01//hhhh11-000004_01.jpg', N'hhhh11-000004', N'20160216_2000_Lexus_ES300_20.jpg')
INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (16, N'~/Images/UnitImages/DEA04//3535659-000004_01.jpg', N'3535659-000004', N'ERD-Revised-Sprint4-02_18_2016.jpg')
INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (17, N'~/Images/UnitImages/DEA04//3535659-000005_01.jpg', N'3535659-000005', N'WIN_20160223_09_33_49_Pro.jpg')
INSERT [dbo].[uploadTitle] ([upload_id], [file_path], [unit_id], [original_file_name]) VALUES (18, N'~/Images/UnitImages/DEA04//3535659-000006_01.jpg', N'3535659-000006', N'WIN_20160207_11_43_14_Pro.jpg')
SET IDENTITY_INSERT [dbo].[uploadTitle] OFF
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (2, N'tfndev', N'aX1HqWA0X/7zAL8DV3MWrw==:MHPZVXEU', N'tfndev', N'tfndev', N'tfndev@tfn.com', N'1111111111', 1, CAST(N'2016-02-05 10:56:30.343' AS DateTime), NULL, 0, 2, 48, 1, 30)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (57, N'FirstSA', N'Iuh1nctKXQ9FMXha0Z1/EA==:SDCHATCF', N'FirstSA', N'FirstSA', N'FirstSA@gmail.com', N'1111111111', 1, CAST(N'2016-02-15 09:02:22.700' AS DateTime), NULL, 0, 57, 47, 1, 29)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (59, N'kasun2', N'aX1HqWA0X/7zAL8DV3MWrw==:MHPZVXEU', N'abc', N'admin', N'abc@admin.com', N'0713444801', 0, CAST(N'2016-02-15 09:16:47.677' AS DateTime), NULL, 0, 58, 46, 1, 28)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (60, N'Admin1', N'AvA2d1qsdaSaVYObEAYq2Q==:JEELZAFX', N'Admin1', N'Admin1', N'irfanfuturenet@gmail.com', N'1111111111', 1, CAST(N'2016-02-15 09:18:11.990' AS DateTime), NULL, 0, 57, 47, 2, 29)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (61, N'piyumi', N'yrMwlgflG0FWyCWsiuCx4w==:QFYWMRIJ', N'piyumi', N'perera', N'piyumi@gmail.com', N'0111111111', 1, CAST(N'2016-02-15 15:22:40.910' AS DateTime), NULL, 0, 61, 60, 1, 30)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (62, N'nadeeka', N'WSjIXM0US9M41CumZ6g2yA==:YSLQIGKL', N'nadeeka p', N'p', N'pnadee2004@yahoo.com', N'0714588867', 1, CAST(N'2016-02-17 00:33:59.737' AS DateTime), NULL, 0, 62, 49, 1, 31)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (63, N'user', N'whAuMo8SN/vmPE/ctgtffQ==:HJAASNAY', N'uuu', N'uuu', N'hhhh@yahoo.com', N'1111111111', 0, CAST(N'2016-02-17 00:35:49.023' AS DateTime), NULL, 0, 62, 49, 1, 31)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (64, N'kanishkashm', N'cNKFJzZ4KXkAVeXIHDUY0Q==:XMLLVONB', N'Kanishka', N'Mahanama', N'kanishkashm@gmail.com', N'0778813290', 1, CAST(N'2016-02-18 08:30:40.007' AS DateTime), NULL, 0, 64, 50, 3, 32)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (65, N'asanka123', N'lEdgLSlOGYtmn44he3/Hdg==:IJIRSJIW', N'a', N'a', N'asa@gmail.com', N'1213213222', 0, CAST(N'2016-02-22 13:18:02.863' AS DateTime), NULL, 0, 2, 48, 1, 30)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (66, N'userSix', N'SnloGd+iADBamvtIez+6xA==:NDAPNVYK', N'asdf', N'asdf', N'asfg@gm.vt', N'1111111111', 1, CAST(N'2016-02-24 11:16:37.103' AS DateTime), CAST(N'2016-02-24 11:27:45.407' AS DateTime), 0, 57, 51, 3, 0)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (67, N'kaniasaasaasa', N'liitF32Sh/LOTMbCpHm0ug==:ZLHVFSMP', N'sasasa', N'sasasa', N'kaaaaaa@dsfcafas.v', N'1211212121', 1, CAST(N'2016-02-25 12:40:35.467' AS DateTime), NULL, 0, 67, 0, 1, 0)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (68, N'tfnKandy', N'5DG2lthql66XIA3XbIjk5Q==:WHQRFBZS', N'tfnKandy', N'tfnKandy', N'tfnKandy@gmail.com', N'0771111111', 1, CAST(N'2016-03-01 10:28:10.607' AS DateTime), NULL, 0, 68, 54, 1, 33)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (69, N'Darshana', N'CqE8DhmQm9ZR6l/7hzElDQ==:USOCDDRC', N'kasun', N'samarawickrama', N'kasun2030@gmail.com', N'0713444801', 1, CAST(N'2016-03-03 08:42:38.340' AS DateTime), NULL, 0, 69, 55, 1, 34)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (70, N'CompanyAdmin', N'Rb5XMXves8c2Przax6HHqg==:FGKDTMPC', N'admin', N'company', N'darshanacompany@gmail.com', N'0713444801', 0, CAST(N'2016-03-03 08:48:33.580' AS DateTime), NULL, 0, 69, 55, 2, 34)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (71, N'CompanyUser', N'bSxtVo7PFlyR30sUHH+O5A==:ABRUWEAP', N'company', N'user', N'kasunUser@gmail.com', N'0713444801', 0, CAST(N'2016-03-03 08:49:56.373' AS DateTime), NULL, 0, 69, 55, 3, 34)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (72, N'test', N'LS2iFZhRxWYvFcKHtshP8g==:XFDZTKSQ', N'dasd ashgakjgf', N'ada $^&$&$^8', N'aasfs@aa.com', N'0714588867', 1, CAST(N'2016-03-02 22:34:14.213' AS DateTime), NULL, 0, 72, 56, 1, 35)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (73, N'user1', N'F44cMqzJ/xSvXLqBP4Ru/w==:VWIVLHMF', N'fsdf', N'fsdfsd', N'dfdsf@ss.com', N'3242343243', 0, CAST(N'2016-03-03 00:05:46.433' AS DateTime), NULL, 0, 72, 56, 1, 35)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (74, N'testbranch', N'2LxZakqtfJegoqxtYTX+/g==:SSDHCGSV', N'testbranch', N'testbranch', N'testbranch@f.h', N'4455678654', 1, CAST(N'2016-03-03 15:15:25.517' AS DateTime), NULL, 0, 74, 57, 1, 36)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (75, N'piyumi2', N'DD/mqKk/A8xcbjPVcTZGtA==:YCJVNYMY', N'nadeeka', N'pp', N'pnadede2004@yahoo.com', N'0714588867', 0, CAST(N'2016-03-03 06:08:57.573' AS DateTime), NULL, 0, 61, 48, 1, 30)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (76, N'irfan123', N'J9QIBZFOg6jivoaEOpNLTg==:BFVELBPN', N'irfan123', N'irfan123', N'irfan123@dfg.juk', N'1234567890', 0, CAST(N'2016-03-04 14:24:11.173' AS DateTime), NULL, 0, 2, 53, 1, 30)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (77, N'aaaaaaaaaaaa', N'Bz9cOTU2RK8rfPUO5KzthA==:JPCUNOEM', N'aaaaaaaaaa', N'aaaaaaaaaaa', N'a@ddd.com', N'1222233333', 1, CAST(N'2016-03-05 09:39:40.700' AS DateTime), NULL, 0, 77, 63, 1, 37)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (78, N'piyumin', N'9GNzRIUxmqKbgwaEVZB5ZQ==:VAGLKYDZ', N'Piyumi', N'Perera', N'piyumin@gmail.com', N'0771119066', 1, CAST(N'2016-03-07 08:08:14.587' AS DateTime), NULL, 0, 78, 62, 1, 38)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (79, N'testAdmin', N'yCY7bZ+te4agty6MDPrGNA==:QGLQUHDN', N'testAdmin', N'testAdmin', N'testAdmin@gmail.com', N'0771111111', 0, CAST(N'2016-03-07 08:18:44.630' AS DateTime), NULL, 0, 78, 62, 2, 38)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (80, N'irfan12', N'uwn/2/QQYAml01PsmqAlZw==:ENULVWVK', N'irfan12', N'irfan12', N'irfan12@fde.hyj', N'1234567890', 1, CAST(N'2016-03-07 11:16:58.230' AS DateTime), NULL, 0, 80, 65, 1, 39)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (81, N'test12', N'IyzyDEPBtlWyATM7Mpx1aQ==:FJXMWVMX', N'test1', N'test1', N'test1@fg.jj', N'1234567890', 1, CAST(N'2016-03-07 11:29:07.490' AS DateTime), NULL, 0, 81, 0, 1, 41)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (82, N'test13', N'/Q/wLyQiFWmtiIGU13DUew==:BSHQULHR', N'test13', N'test13', N'test13@dhj.fd', N'2345678901', 1, CAST(N'2016-03-07 11:41:36.653' AS DateTime), NULL, 0, 82, 64, 1, 42)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (83, N'kascomstep', N'yxNcXeYDs93KaIqJP3KtzQ==:ACYKMPFA', N'Kasun', N'Darshana', N'kasun0001@gmail.com', N'0713444801', 1, CAST(N'2016-03-07 17:27:43.453' AS DateTime), NULL, 0, 83, 69, 1, 43)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (84, N'irfan22', N'Z6Qj4VvhkOYqiued9VpfQw==:JXHPNMMX', N'irfan22', N'irfan22', N'irfan22@eg.yy', N'2345678901', 1, CAST(N'2016-03-07 18:04:55.667' AS DateTime), NULL, 0, 84, 0, 1, 44)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (85, N'irfan321', N'K8Vol/i6u9kC3aoIsIke0g==:IEAFVRFS', N'irfan321', N'irfan321', N'irfan321@dfg.hyt', N'1211111111', 1, CAST(N'2016-03-08 07:24:38.703' AS DateTime), NULL, 0, 85, 0, 1, 47)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (86, N'piyumi3', N'7WVXZI3iGQLsET1sZRyrLQ==:WQUIAWET', N'piyumi3', N'piyumi3', N'piyumi3@gmail.com', N'0778001134', 1, CAST(N'2016-03-08 07:52:16.583' AS DateTime), NULL, 0, 86, 70, 1, 45)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (87, N'AdminPiyumi3', N'MWb9KDIT/kVLtyQOKdqLVg==:YCHFZWFZ', N'AdminPiyumi3', N'AdminPiyumi3', N'AdminPiyumi3@gmail.com', N'0771111111', 0, CAST(N'2016-03-08 07:58:26.960' AS DateTime), NULL, 0, 86, 70, 2, 45)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (88, N'testsuperadmin', N'8TMUpKpneX8qsW6BK1jaTQ==:ABCYGLSK', N'testsuperadmin', N'testsuperadmin', N'testsuperadmin@testsuperadmin.com', N'0999999999', 1, CAST(N'2016-03-08 08:38:16.837' AS DateTime), NULL, 0, 88, 71, 1, 46)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (89, N'admin10', N'OhS0NhsJ89MGeLx24ITQ/g==:XLIYHSFS', N'admin10', N'admin10', N'admin10@admin10.vo', N'0877777777', 0, CAST(N'2016-03-08 08:40:59.557' AS DateTime), NULL, 0, 88, 71, 2, 46)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (90, N'asanka', N'fjg56eL33egdN3FTK77FWQ==:OOETIOLD', N'Asanka', N'Senarathna', N'asanka@thefuturenet.com', N'0777714065', 1, CAST(N'2016-03-08 09:50:09.837' AS DateTime), NULL, 0, 77, 63, 2, 37)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (91, N'TEST122', N'c6JBfhYAPGVxX3dC3Y5yZg==:VNCIQCAI', N'Test12', N'Test12', N'TEST12@WE.TRHT', N'1111111111', 1, CAST(N'2016-03-08 14:17:15.963' AS DateTime), NULL, 0, 91, 0, 1, 48)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (92, N'test1223', N'Up36sfjZg39+oDsAqffI4w==:RPUFDVDG', N'Test1223', N'Test1223', N'test1223as@dsa.hhh', N'1234567890', 1, CAST(N'2016-03-08 14:32:36.967' AS DateTime), NULL, 0, 92, 0, 1, 49)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (93, N'piyumi4', N'KRjsRFUN/Y6YcTATK3Jx9Q==:SGQZEXGY', N'Piyumi4', N'Piyumi4', N'piyumi4@gmail.com', N'0111111111', 1, CAST(N'2016-03-08 14:33:28.867' AS DateTime), NULL, 0, 93, 0, 1, 50)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (94, N'test1224', N'SbuX1oZJW49Wh6pWCeVavw==:PVLZGQHF', N'Test1224', N'Test1224', N'test1224@FGT.JUI', N'1111111111', 1, CAST(N'2016-03-08 15:30:46.807' AS DateTime), NULL, 0, 94, 0, 1, 51)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (95, N'AdminPiyumi4', N'lA3x6ohEgdHiAN0uNBjOyg==:RBJQLORC', N'Adminpiyumi4', N'Adminpiyumi4', N'AdminPiyumi4@gmail.com', N'0877777777', 1, CAST(N'2016-03-08 15:39:29.817' AS DateTime), NULL, 0, 93, 72, 2, 50)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (96, N'test125', N'OUT26FXMUvdG4Qm/hKiRZQ==:LQLDPNWQ', N'Test125', N'Test125', N'test125@sf.kk', N'1111111111', 1, CAST(N'2016-03-08 15:45:54.977' AS DateTime), NULL, 0, 96, 0, 1, 52)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (97, N'test126', N'CjZrJOWjZYn325itn+6SRw==:IOOEXGSV', N'Test126', N'Test126', N'test126@test126.com', N'0999999999', 1, CAST(N'2016-03-08 15:49:04.637' AS DateTime), NULL, 0, 97, 0, 1, 53)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (98, N'test127', N'FkTMM0tBGbV1s8EPDDeBqA==:WRNRLZJO', N'Test127', N'Test127', N'test127@test127.n', N'0999999999', 1, CAST(N'2016-03-08 15:51:49.920' AS DateTime), NULL, 0, 98, 0, 1, 54)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (99, N'test128', N'7oAqtak9GID0jC+aPlxRGQ==:LQRFALVU', N'Test128', N'Test128', N'test128@f.k', N'1111111111', 1, CAST(N'2016-03-08 15:54:33.170' AS DateTime), NULL, 0, 99, 0, 1, 55)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (100, N'irfan12344', N'LR4xgQSn0XIrEP2TRQMMyQ==:ZLNAGTVL', N'Qaa', N'Asd', N'axadw@def.jhgj', N'2345678901', 0, CAST(N'2016-03-08 16:26:39.593' AS DateTime), NULL, 0, 99, 0, 1, 55)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (101, N'test129', N'J9o/fvqKoMXxbuVW36qO0g==:BHMZERRO', N'Test129', N'Test129', N'test129@asds.dfr', N'1111111111', 1, CAST(N'2016-03-08 16:33:00.517' AS DateTime), NULL, 0, 101, 0, 1, 56)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (102, N'test130', N'jA2bKviPD8T6lImKjDRC8w==:KGQPIARQ', N'Test130', N'Test130', N'test130@dfrt.j', N'1111111111', 1, CAST(N'2016-03-08 16:42:06.403' AS DateTime), NULL, 0, 102, 0, 1, 57)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (103, N'test131', N'oJrnsP1JdGizbhkpdMvjow==:EYLBBCVN', N'Test131', N'Test131', N'test131@sed.k', N'1111111111', 1, CAST(N'2016-03-08 17:05:12.780' AS DateTime), NULL, 0, 103, 0, 1, 58)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (108, N'test132', N'SCEu/q9TNSZf+yK+rpBArQ==:PDBYWJXK', N'Test132', N'Test132', N'test132@sd.ki', N'1234567890', 1, CAST(N'2016-03-08 17:27:31.567' AS DateTime), NULL, 0, 108, 0, 1, 59)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (109, N'test133', N'yU3CyF5b2QOvVEv2+6sTaA==:YPMBKRMS', N'Test133', N'Test133', N'test133@we.kuu', N'1234567890', 0, CAST(N'2016-03-08 17:51:40.503' AS DateTime), NULL, 0, 96, 76, 2, 52)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (110, N'irfan134', N'dfAbZ3dlJcLTpcU0HgWYjQ==:EHWQTFRL', N'Irfan134', N'Irfan134', N'irfan134@s.j', N'1111111111', 1, CAST(N'2016-03-08 17:59:23.323' AS DateTime), NULL, 0, 108, 77, 2, 60)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (111, N'kanishkashm1', N'wVmK85PNGRSvDiY5t4mqEw==:NKQJEEXO', N'Kanishka', N'Mahanama', N'kanishkashm1@gmail.com', N'0778813290', 1, CAST(N'2016-03-09 11:06:24.403' AS DateTime), NULL, 0, 111, 0, 1, 0)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (113, N'piyumi6', N'yfQ2fqKtAKKd9obs9h3aKA==:ABCTIUZS', N'Piyumi6', N'Piyumi6', N'piyumi6@gmail.com', N'4353546465', 1, CAST(N'2016-03-09 11:57:24.483' AS DateTime), NULL, 0, 113, 0, 1, 61)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (114, N'kanishkashm3', N'bQR8bZl2OIl10YDHleH/Vw==:QCWBFQNG', N'Kanishkashm3', N'Kanishkashm3', N'kanishkashm3@kanishkashm3.com', N'4343434343', 1, CAST(N'2016-03-09 12:04:40.890' AS DateTime), NULL, 0, 114, 0, 1, 0)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (115, N'kanishkashm2', N'bEQ3cMdxAZQVfWy3OZZVBg==:ZVTNZOTO', N'Kanishkashm2', N'Kanishkashm2', N'kanishkashm2@kanishkashm2.com', N'0778813290', 1, CAST(N'2016-03-09 12:08:47.923' AS DateTime), NULL, 0, 115, 0, 1, 0)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (116, N'piyumi7', N'3RFtz1oKqSenu4/i7gsiwA==:CQTKHDGI', N'Piyumi7', N'Piyumi7', N'piyumi7@ghghg.com', N'1111111111', 1, CAST(N'2016-03-09 13:51:03.357' AS DateTime), NULL, 0, 116, 0, 1, 62)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (117, N'adminpiyumi7', N'9btZU5pxALpW/fRzGe9amg==:CVBOSNHL', N'Adminpiyumi7', N'Adminpiyumi7', N'adminpiyumi7@ghgh.com', N'0771111111', 1, CAST(N'2016-03-09 13:59:42.423' AS DateTime), NULL, 0, 116, 79, 2, 62)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (118, N'AdminPiyumi71', N'viPwPhKU0z0XqJeoxsg7aQ==:WIGIOYXT', N'Adminpiyumi71', N'Adminpiyumi71', N'AdminPiyumi71@ghg.com', N'0771111111', 0, CAST(N'2016-03-09 14:03:56.567' AS DateTime), NULL, 0, 116, 79, 2, 62)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (119, N'cat', N'AYirNnfkismgAxzo8Z/KbQ==:RNZYZTES', N'Cat Cat', N'Dog', N'asanka@carmartnet.com', N'9876543210', 1, CAST(N'2016-03-09 14:13:43.427' AS DateTime), NULL, 0, 119, 0, 1, 63)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (120, N'badmin', N'JtJsb3JGUKO1+wffE/UF1w==:PKIEEUDE', N'Admin', N'Admin', N'sdsads@test.com', N'7777777777', 1, CAST(N'2016-03-09 14:19:31.193' AS DateTime), NULL, 0, 119, 81, 2, 63)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (121, N'buser', N'dXUr4ltaTDvoSSFLG59BNw==:GCGYTJSV', N'Asda', N'Ada', N'ads@gggg.com', N'3433333333', 1, CAST(N'2016-03-09 14:20:37.737' AS DateTime), NULL, 0, 119, 81, 3, 63)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (122, N'piyumi8', N'eqv6V6xNNSUdEjDxEQ/r2A==:PVOJWMMQ', N'Piyumi8', N'Piyumi8', N'piyumi8@gh.com', N'0771119066', 1, CAST(N'2016-03-09 15:12:47.430' AS DateTime), NULL, 0, 122, 0, 1, 64)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (123, N'piyumi9', N'+94Nm0/YXqwIvi6vstZ6Jg==:XWLRJJNX', N'Piyumi9', N'Piyumi9', N'piyumi9@gmail.com', N'0771111111', 1, CAST(N'2016-03-09 15:37:58.667' AS DateTime), NULL, 0, 123, 0, 1, 65)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (124, N'AdminPiyumi9', N'LiSqUq6klO3u/GcXp1hD1w==:ZAVJPDOW', N'Adminpiyumi9', N'Adminpiyumi9', N'AdminPiyumi9@gmail.com', N'0771111111', 1, CAST(N'2016-03-09 15:49:52.390' AS DateTime), NULL, 0, 123, 83, 2, 65)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (125, N'piyumiuser9', N'A8zn7hP4+mU3sURJe2Tt8g==:SKWGKKDA', N'Piyumiuser9', N'Piyumiuser9', N'piyumiuser9@jyj.com', N'0771111111', 0, CAST(N'2016-03-09 16:40:25.943' AS DateTime), NULL, 0, 124, 83, 3, 65)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (126, N'cool', N'pTw0Md0zeYRDciE9nH4aYA==:WSKYJUIG', N'Cool', N'Coollast', N'cool@gmail.com', N'9638527410', 1, CAST(N'2016-03-09 18:26:10.973' AS DateTime), NULL, 0, 126, 0, 1, 66)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (127, N'cadmin1', N'OQsUw4fUClXaSU+RHyjBiA==:SRUUBYJZ', N'Cadmin', N'Asd', N'rr@gmail.com', N'9638527410', 0, CAST(N'2016-03-09 20:16:34.140' AS DateTime), NULL, 0, 126, 85, 2, 66)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (128, N'irfan12345', N'qr/qWTn2QTW6EB7m25tkAA==:SMRIAJHA', N'Irfan12345', N'Irfan12345', N'irfan12345@gmail.com', N'0756220557', 1, CAST(N'2016-03-10 07:59:39.157' AS DateTime), NULL, 0, 128, 0, 1, 70)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (129, N'piyumi10', N'/8fPoElxLycx8Hjt6WZYEA==:EWKAKBEJ', N'Piyumi10', N'Piyumi10', N'piyumi10@tht.com', N'0771119066', 1, CAST(N'2016-03-10 08:01:48.847' AS DateTime), NULL, 0, 129, 0, 1, 67)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (130, N'adminpiyumi10', N'NZJyl/O9VVuVd4222ZpBZQ==:UHZXFBON', N'Adminpiyumi10', N'Adminpiyumi10', N'adminpiyumi10@fhgh.com', N'0771111111', 1, CAST(N'2016-03-10 08:33:33.243' AS DateTime), NULL, 0, 129, 87, 2, 67)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (131, N'irfan123456', N'zh/2E27sxs+DXIZ271vyrA==:UDZGZBXC', N'Irfan123456', N'Irfan123456', N'irfan123456@dfrt.jui', N'2345678901', 0, CAST(N'2016-03-10 08:49:15.027' AS DateTime), NULL, 0, 128, 89, 3, 70)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (132, N'adminpiyumi100', N'2S66lbo9Oit/c63z2zmB0w==:YXFFXPTJ', N'Adminpiyumi100', N'Adminpiyumi100', N'adminpiyumi100@hgh.com', N'0771111111', 0, CAST(N'2016-03-10 11:27:01.540' AS DateTime), NULL, 0, 129, 88, 2, 67)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (133, N'adminpiyumi101', N'qzc3tA39QtBmnw1zViNT9Q==:VZLJZDCO', N'Adminpiyumi101', N'Adminpiyumi101', N'adminpiyumi101@hjgjhj.com', N'0771111111', 0, CAST(N'2016-03-10 11:55:22.330' AS DateTime), NULL, 0, 129, 88, 2, 67)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (134, N'testpopSA', N'AZtEiMcjS5u5oViJ5V5Jbg==:UGVLJBFB', N'Testpopsa', N'Testpopsa', N'testpopSA@dfgh.jh', N'1111111111', 1, CAST(N'2016-03-10 12:11:50.813' AS DateTime), NULL, 0, 134, 0, 1, 71)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (135, N'darshanaadmin', N'SeCJaQRH0GLyy+TxMNJyNw==:DPRLKWRO', N'Darshanaadmin', N'Darshanaadmin', N'34343@gf.tr', N'4545454555', 1, CAST(N'2016-03-10 12:37:03.463' AS DateTime), NULL, 0, 135, 0, 1, 72)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (136, N'darshanaadminbranch', N'6DKTe70X8owqK1C7DtDWbA==:JZVRPWBF', N'Darshanaadminbranch', N'Darshanaadminbranch', N'darshanaadminbranch@grg.thyth', N'5343434434', 1, CAST(N'2016-03-10 12:43:55.660' AS DateTime), NULL, 0, 135, 92, 2, 72)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (137, N'darshanaadmin2', N'w8nk0a1THER7P2vmicPFYA==:UETAKXJO', N'44545545', N'45454545', N'5454@grgr.yuyu', N'6556565656', 1, CAST(N'2016-03-10 12:47:23.707' AS DateTime), NULL, 0, 135, 92, 2, 72)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (138, N'cadmin', N'8nW9TvQhYxN57z3O6AcN2Q==:HSMDUKSB', N'Asd', N'Asd', N'aaa@gmail.com', N'7896541230', 1, CAST(N'2016-03-10 13:20:33.493' AS DateTime), NULL, 0, 126, 86, 2, 66)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (139, N'superadmin1', N'gjlwjPjQqJAilNGvFT5BHg==:DNCHPAFC', N'Superadmin1', N'Superadmin1@fgh.kil', N'superadmin1@fgh.lkj', N'1111111111', 1, CAST(N'2016-03-10 13:49:09.127' AS DateTime), NULL, 0, 139, 0, 1, 73)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (140, N'piyumi11', N'nzDm5peXSuCd2MybsqtGqw==:ODGCYSPK', N'Piyumi11', N'Piyumi11', N'piyumi11@gmail.com', N'0771119066', 1, CAST(N'2016-03-10 13:54:44.027' AS DateTime), NULL, 0, 140, 0, 1, 74)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (141, N'irfan1234', N'4JB5Z4i+5PgWw/XrFGQELQ==:TPYEFCDL', N'Irfan1234', N'Irfan1234', N'irfan1234@kjk.h', N'2345678901', 1, CAST(N'2016-03-10 13:58:46.393' AS DateTime), NULL, 0, 139, 93, 2, 73)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (142, N'adminpiyumi11', N'8A4sXizqSvrgIB1daEJ09w==:NFWWMCIC', N'Adminpiyumi11', N'Adminpiyumi11', N'adminpiyumi11@hjj.com', N'0771111111', 1, CAST(N'2016-03-10 14:05:38.427' AS DateTime), NULL, 0, 140, 94, 2, 74)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (143, N'piyumi12', N'1Iz9wyveNRKAAMSPb/i+8A==:NYQSBHYY', N'Piyumi12', N'Piyumi12', N'piyumi12@cghg.com', N'0778001134', 1, CAST(N'2016-03-10 16:32:48.220' AS DateTime), NULL, 0, 143, 0, 1, 0)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (144, N'senarathna', N'2wuCzYsD4fJjro6U+SnlNQ==:GXUFECLT', N'Asanka', N'Senarathna', N'asanka@thefuturne.com', N'0777714065', 1, CAST(N'2016-03-10 22:56:04.763' AS DateTime), NULL, 0, 144, 0, 1, 75)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (145, N'asanka1', N'kNsBsovnT4aolPQpYzbXkA==:XOBHJCBN', N'Asanka', N'Asanka1', N'asa1@gmail.com', N'7444444555', 0, CAST(N'2016-03-10 23:00:16.220' AS DateTime), NULL, 0, 144, 97, 2, 75)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (146, N'Lyn', N'f1gKrTWVYlT2B0ZsM7WRxQ==:SZPVXPVP', N'Lyn', N'Ann', N'lyn@hotmail.com', N'1234567890', 1, CAST(N'2016-03-10 19:57:43.980' AS DateTime), NULL, 0, 146, 0, 1, 76)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (147, N'Ann', N'djZWNNmMI8+uRmDLvj8lSw==:YZQVDZRO', N'Ann', N'Lyn', N'ann@hotmail.com', N'7015654895', 0, CAST(N'2016-03-10 20:14:58.310' AS DateTime), NULL, 0, 146, 99, 2, 76)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (148, N'TestPasword', N'k9IQme4rXYosiE6BKaWAzQ==:LMXNCCIW', N'Testpasword', N'Testpasword', N'TestPasword@fgh.k', N'1111111111', 1, CAST(N'2016-03-11 10:19:33.213' AS DateTime), NULL, 0, 148, 0, 1, 0)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (149, N'dapnpn', N'u/ojxHZ12xTduQhPx0d21A==:ZCTVCSTC', N'Dapnpn', N'Dapnpn', N'dapnpn@gmail.com', N'0778001134', 1, CAST(N'2016-03-11 10:37:43.423' AS DateTime), NULL, 0, 149, 0, 1, 77)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (150, N'dapnadmin', N'ScXbDV9Lel/XfzumIJ/7sQ==:MDBLEYWG', N'dapnadmin', N'dapnadmin', N'dapnadmin@hdh.com', NULL, 1, CAST(N'2016-03-11 12:52:51.813' AS DateTime), NULL, 0, 149, 100, 2, 77)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (151, N'ball', N'UvpPEbTgQgWnYbJIEU+6EA==:JBQGCEKJ', N'Ball', N'Ball', N'ball@gmail.com', N'1231232131', 1, CAST(N'2016-03-11 14:37:43.917' AS DateTime), NULL, 0, 151, 0, 1, 78)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (152, N'test111', N'fJ0WSwA1X/ZsYh+RsAJung==:CTAPVYKQ', N'Test111', N'Test111', N'test111@fgt.kio', N'1234567890', 1, CAST(N'2016-03-11 16:19:47.850' AS DateTime), NULL, 0, 152, 0, 1, 79)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (153, N'dapnpn2', N'Syvh3bDc+De17/ktMHMdkA==:HGSIDYAX', N'Dapnpn2', N'Dapnpn2', N'dapnpn2@gj.com', N'0778001134', 1, CAST(N'2016-03-11 16:35:24.097' AS DateTime), NULL, 0, 153, 0, 1, 80)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (154, N'asa', N'cBmcKOHDrjC+OBmTch3wOQ==:FSMFQVOX', N'asa', N'Sena', N'asan@gmail.com', NULL, 1, CAST(N'2016-03-11 17:40:39.137' AS DateTime), NULL, 0, 151, 102, 2, 78)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (156, N'dapnpn3', N'cIDguUJSEhRo2XKFCpGiJQ==:NJYHXOKD', N'dapnpn3', N'dapnpn3', N'dapnpn3@ghfhgfh.com', N'0778001134', 1, CAST(N'2016-03-12 10:45:46.617' AS DateTime), NULL, 0, 156, 0, 1, 82)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (157, N'darshanadealer', N'xgC+oT91TOpIkJAkoX5pQA==:NWVQSHZC', N'darshanadealer', N'darshanadealer', N'darshanadealer@y.y', N'0713444892', 1, CAST(N'2016-03-12 11:16:54.163' AS DateTime), NULL, 0, 157, 0, 1, 83)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (158, N'darshanadealerAdmin', N'Q2wc0CKzgEvnr2eF/G3SAA==:FMYYUKGP', N'darshanadealerAdmin', N'darshanadealerAdmin', N'darshanadealerAdmin@hh.k', NULL, 0, CAST(N'2016-03-12 11:18:13.413' AS DateTime), NULL, 0, 157, 109, 2, 83)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (159, N'wertyyuui', N'tgxAVKEm8R+wQL0t59+jug==:HABWLWIC', N'wertyyuui', N'wertyyuui', N'wertyyuui@rdetr.hy', N'1111111111', 1, CAST(N'2016-03-12 11:19:19.870' AS DateTime), NULL, 0, 159, 0, 1, 84)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (160, N'dehiwala', N'DSr66u5xj1BCJcOhXzUGWw==:AOGONHWL', N'Dehiwala', N'Dehi', N'dehi@gmail.com', N'7894561230', 1, CAST(N'2016-03-12 11:36:49.920' AS DateTime), NULL, 0, 160, 0, 1, 85)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (161, N'sena', N'PxAEOyPKfkQUIH7m70MBkQ==:MUFMVRST', N'Asanka', N'Senaratn', N'asss@gmail.com', N'9874563210', 1, CAST(N'2016-03-12 01:09:04.627' AS DateTime), NULL, 0, 161, 0, 1, 86)
GO
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (162, N'piyumi15', N'unTKQQtl0JsBjdyvKTpV1w==:IFIKRMIH', N'piyumi15', N'piyumi15', N'piyumi15@ghgg.com', N'0771119066', 1, CAST(N'2016-03-12 15:28:16.000' AS DateTime), NULL, 0, 162, 0, 1, 87)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (163, N'adminpiyumi15', N'T2F+eeBJ1P+WuE/aU2VQfA==:AVEMJLNS', N'Adminpiyumi15', N'Adminpiyumi15', N'adminpiyumi15@hh.cjm', NULL, 0, CAST(N'2016-03-12 15:32:36.887' AS DateTime), NULL, 0, 162, 113, 2, 87)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (164, N'test1111', N'oZ2Q4pgCsGLUt3iXb8098g==:BIKTUXHP', N'test1111', N'test1111', N'test1111@dfg.fr', N'1111111111', 1, CAST(N'2016-03-12 16:12:47.727' AS DateTime), NULL, 0, 164, 0, 1, 88)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (165, N'piyumi16', N'+bliXazZ0Xlps5PSl53qTw==:QQUJVYVF', N'Piyumi16', N'Piyumi16', N'piyumi16@fff.com', N'0888888888', 1, CAST(N'2016-03-12 16:59:26.050' AS DateTime), NULL, 0, 165, 0, 1, 89)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (166, N'admin16', N'Rd1HhgDf+Wt2mW91VRoMLg==:AWIGDCVL', N'Admin16', N'Admin16', N'admin16@fghg.com', NULL, 0, CAST(N'2016-03-12 17:11:21.360' AS DateTime), NULL, 0, 165, 115, 2, 89)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (167, N'test1112', N'b4KYlkoIeGriL45P7nfsOQ==:DLRWJMJS', N'Test1112', N'Test1112', N'test1112@wer.kiu', N'2345678901', 1, CAST(N'2016-03-12 17:55:09.617' AS DateTime), NULL, 0, 167, 0, 1, 90)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (168, N'test1113', N'qJ27xYmjWpDE0WmB2BB/Qw==:YVXCCPSP', N'Test1113', N'Test1113', N'test1113@dfh.kiu', NULL, 1, CAST(N'2016-03-12 18:08:28.500' AS DateTime), NULL, 0, 167, 117, 2, 90)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (169, N'test1114', N'1P/b2miWH/k6RzkEr0G5Bg==:PDVWJZGG', N'Test1114', N'Test1114', N'test1114@ssde.ki', NULL, 1, CAST(N'2016-03-12 18:10:06.067' AS DateTime), NULL, 0, 167, 116, 2, 90)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (170, N'carmartnet', N'CMmW8Awk2T5X6CiQm52MtA==:UGPXWKVI', N'Carmart', N'Net', N'car@gmail.com', N'1234567890', 1, CAST(N'2016-03-13 13:21:58.880' AS DateTime), NULL, 0, 170, 0, 1, 92)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (171, N'ABCtestingUser1', N'qBPbMi1tBDQZrmrAQrHl2Q==:HLJOILZK', N'TestUser1', N'ABCcomp', N'abctestinguser1@user.com', N'0123547865', 0, CAST(N'2016-03-13 14:01:08.313' AS DateTime), NULL, 0, 170, 118, 3, 92)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (172, N'fistSuperAdmin', N'tJ36KAroah6SnhxeHNouhg==:XTGMZWXE', N'FistSuperAdmin', N'FistSuperAdmin', N'fistSuperAdmin@df.ki', N'1111111111', 1, CAST(N'2016-03-14 07:53:33.527' AS DateTime), NULL, 0, 172, 0, 1, 93)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (173, N'piyumi17', N'mHtEiuqQnlMOum3Nzi2UEw==:UDFJQZFZ', N'Piyumi17', N'Piyumi17', N'piyumi17@thjj.com', N'0778001134', 1, CAST(N'2016-03-14 07:56:40.780' AS DateTime), NULL, 0, 173, 0, 1, 94)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (174, N'admin17', N'xXZbybT9+iK4lL4vAJHyMw==:NMIJGTAW', N'Admin17', N'Admin17', N'admin17@thth.com', NULL, 0, CAST(N'2016-03-14 07:59:17.683' AS DateTime), NULL, 0, 173, 119, 2, 94)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (175, N'kasunnew', N'gqmSd5uF7agNWkgB/SwpFg==:WETBYOTL', N'Kasun', N'New', N'kasunkasun@gmail.com', N'0972344587', 1, CAST(N'2016-03-14 09:01:56.637' AS DateTime), NULL, 0, 175, 0, 1, 95)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (176, N'kasunnewsuper', N'Vpn9euH2+ZhHOz1J0sVyFw==:SKYOQMJV', N'656656', N'656565656', N'gfdfdfdfdfdf@gfg.fd', NULL, 0, CAST(N'2016-03-14 09:16:40.847' AS DateTime), NULL, 0, 175, 121, 2, 95)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (177, N'fgfdg', N'GcHKHp1Mjxz1LWXQCuhLLg==:KLNSEZYU', N'www', N'www', N'rewr@ffsd.com', NULL, 0, CAST(N'2016-03-22 23:49:06.193' AS DateTime), NULL, 0, 126, 85, 1, 66)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (178, N'piyumi18', N'ENWaPe9iQ6HlmbnnYs5+7A==:JPUJKCBA', N'Piyumi18', N'Piyumi18', N'piyumi18@hh.com', N'0771119066', 1, CAST(N'2016-03-23 08:26:29.540' AS DateTime), NULL, 0, 178, 0, 1, 96)
INSERT [dbo].[user] ([user_id], [user_name], [password], [first_name], [last_name], [email], [phone_no], [status], [created_date], [modified_date], [is_delete], [created_by], [branch_id], [role_id], [company_id]) VALUES (179, N'admin18', N'LpXXDfbWaDWEDyoDuzfOPA==:QEZRXLML', N'Admin18', N'Admin18', N'admin18@ff.com', NULL, 0, CAST(N'2016-03-23 08:28:41.413' AS DateTime), NULL, 0, 178, 124, 2, 96)
SET IDENTITY_INSERT [dbo].[user] OFF
SET IDENTITY_INSERT [dbo].[userActivation] ON 

INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (24, 59, N'dc6e449d-6854-42f4-b669-a299159f978f', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (25, 60, N'86e3227e-abe7-4031-8cd6-d2a1315b722f', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (26, 63, N'a0fe1868-5b26-42e2-ac87-0500eae46228', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (28, 65, N'a71a1b4e-3a93-4d6b-8220-e3f9b24cf995', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (29, 66, N'a3a16405-afa8-41fa-a7a1-3fe9463e8578', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (30, 70, N'e238f90d-e26c-4f77-b4e7-8b9c739a480e', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (31, 71, N'cbc0bb34-cf5f-413e-b771-f30666e3b960', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (32, 73, N'736cf8e7-f610-48e8-93a6-4d58b6b3e7d3', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (33, 75, N'3073911d-4483-42a7-8756-c8dc9d14f9ea', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (34, 76, N'f253e9a2-ef11-4479-8756-e5b72cb221bb', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (35, 79, N'a415132f-7be8-4276-b6f1-bcd186539e3e', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (36, 87, N'64bea5de-6735-4a73-94f4-05be2665899f', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (37, 89, N'429f1b34-eb8a-4244-9b96-be50341803ce', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (38, 90, N'bc95f333-0f14-4b10-b2d5-5e53cf98f2d6', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (39, 95, N'7018697e-edc8-4caa-98fb-ab026fc66fab', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (40, 100, N'3cce0d0c-8bae-4465-bd02-21bad1a7e0c9', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (41, 109, N'e1eeb706-1c02-4f41-b2ab-5603279de6ab', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (42, 110, N'e1b50340-6f52-4ea3-8bf5-aff0c771fc09', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (43, 117, N'e9dce219-03b1-42bb-a9a4-ed1249db7dd3', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (44, 118, N'ea719cd1-b202-4334-b91d-e7b8d29eeab0', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (45, 120, N'b44628d5-94a7-43d8-b03e-3aba8a8f3d2b', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (46, 121, N'623239ff-3fd5-4de9-9556-0b909a986a0e', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (47, 124, N'b3502c05-e294-42ba-ba48-1fc0f6f4ceeb', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (48, 125, N'b7ec1926-246a-422e-a6cf-694a56cae289', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (49, 127, N'77983c45-9737-4219-98ae-72c1ce1c4696', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (50, 130, N'3d437860-b484-4e55-8b53-e59bee252584', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (51, 131, N'57553c3d-ffaf-444b-8999-4cce601aa1f3', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (52, 132, N'fd98f6bc-fbae-49da-8cc6-80a434af3a5a', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (53, 133, N'6c7caf0f-2f72-4e02-a071-945ed17e8a14', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (54, 136, N'494f8bd8-658a-4dd4-9b90-d85c925e1008', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (55, 137, N'ce9dfb14-20fc-41eb-a8ce-3e7c95bc5e5f', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (56, 138, N'e06d5996-47ed-4157-9645-21cfd7a57e67', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (57, 141, N'154e1792-dd3b-4870-ac5d-876d6ed36f54', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (58, 142, N'627146cf-0298-4742-9710-7601c316f7c8', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (59, 145, N'754632c5-629a-40ac-8b50-51e7317eb5ac', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (60, 147, N'f79df3fb-6cdb-46a6-81d6-a21153965f6a', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (61, 150, N'63ed7a5f-7935-4020-b2a2-6dd963c8b17f', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (62, 154, N'52d07ae0-0c9d-4b7c-b6fe-8fb9fdabd63a', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (63, 158, N'64a6f608-4a99-4c5a-9824-d1e7e8370f8c', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (64, 163, N'b8926863-7b50-4ac9-83af-d39f50a2ee37', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (65, 166, N'05bab31d-a3e3-4c21-b2f1-1ed2bbd8455c', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (66, 166, N'd9d42edb-2445-4ca1-9145-ec92bc78e13a', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (67, 168, N'ced050e2-dabf-48f0-8dfc-11e0a25876ed', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (68, 169, N'0cd42de4-a297-45da-9d97-656ebbd41afe', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (69, 171, N'8d5ebf65-1925-46f5-af52-b978e47cd7ca', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (70, 174, N'f67d9768-edd3-45d2-9f88-d972c6d93960', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (71, 176, N'68c357ba-87bc-4753-9197-935fc61075c4', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (72, 177, N'44431d77-f756-4973-bb68-ecb47d932bdc', 0)
INSERT [dbo].[userActivation] ([activation_id], [user_id], [activation_code], [is_active]) VALUES (73, 179, N'73972182-67c4-4bdb-8107-134191f0489a', 0)
SET IDENTITY_INSERT [dbo].[userActivation] OFF
INSERT [dbo].[userPermission] ([user_id], [right_id], [modified_by], [modify_date]) VALUES (66, N'U001,U002,U004', 57, CAST(N'2016-02-24 12:06:28.443' AS DateTime))
INSERT [dbo].[userRights] ([right_id], [description]) VALUES (N'U001', N'Advance Add')
INSERT [dbo].[userRights] ([right_id], [description]) VALUES (N'U002', N'Title Add')
INSERT [dbo].[userRights] ([right_id], [description]) VALUES (N'U003', N'Pay Off Add')
INSERT [dbo].[userRights] ([right_id], [description]) VALUES (N'U004', N'Unit Add')
INSERT [dbo].[userRights] ([right_id], [description]) VALUES (N'U005', N'Curtailment Add')
SET IDENTITY_INSERT [dbo].[userRole] ON 

INSERT [dbo].[userRole] ([role_id], [role_name]) VALUES (1, N'SuperAdmin')
INSERT [dbo].[userRole] ([role_id], [role_name]) VALUES (2, N'Admin')
INSERT [dbo].[userRole] ([role_id], [role_name]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[userRole] OFF
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1909, N'Ford', N'Model T')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1926, N'Chrysler', N'Imperial')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1948, N'Citroën', N'2CV')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1953, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Cadillac', N'Fleetwood')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1956, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvair')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fillmore', N'Fillmore')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fairthorpe', N'Rockette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Buick', N'Special')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini Cooper S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Rambler', N'Classic')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvair 500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Plymouth', N'Fury')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Volkswagen', N'Beetle')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Falcon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Tempo')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Aerostar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Escort')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Ranger')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Probe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'LTD Crown Victoria')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco II')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Festiva')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'W201')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'928')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'944')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'LeSabre')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Regal')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Century')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Riviera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Skylark')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Coachbuilder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Estate')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Electra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Reatta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Sidekick')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Swift')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'SJ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Sable')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Topaz')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Grand Marquis')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Cougar')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Legacy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Justy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Loyale')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'XT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'626')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'929')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'C-MAX Hybrid')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Edge')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Flex')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus ST')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Transit Connect')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Veloster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Accent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Elantra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Equus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Genesis Coupe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Sonata')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Cruze')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Malibu')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Tahoe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Audi', N'S4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Sierra 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 2500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Infiniti', N'JX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Jaguar', N'XK Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Rio')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Sorento')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'GS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'LX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'RX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'CX-5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'MAZDA6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'GT-R')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Rolls-Royce', N'Phantom')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'FR-S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'tC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Subaru', N'BRZ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Land Cruiser')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Venza')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volkswagen', N'CC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayman')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'370Z')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Murano')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Armada')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Pathfinder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series Super Duty')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'1 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'3 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'5 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'6 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'7 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'Z4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'GL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLK-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'S-Class')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLS-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'C-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SLK55 AMG')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'R-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Lincoln', N'Navigator L')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S80')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V50')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S40')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1909, N'Ford', N'Model T')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1926, N'Chrysler', N'Imperial')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1948, N'Citroën', N'2CV')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1953, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Cadillac', N'Fleetwood')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1956, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvair')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fillmore', N'Fillmore')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fairthorpe', N'Rockette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1909, N'Ford', N'Model T')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1926, N'Chrysler', N'Imperial')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1948, N'Citroën', N'2CV')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1953, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Cadillac', N'Fleetwood')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1956, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvair')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fillmore', N'Fillmore')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fairthorpe', N'Rockette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Buick', N'Special')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini Cooper S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Rambler', N'Classic')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvair 500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Plymouth', N'Fury')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Volkswagen', N'Beetle')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Chevrolet', N'Corvette')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Falcon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Tempo')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Aerostar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Escort')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Ranger')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Probe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'LTD Crown Victoria')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco II')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Festiva')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'W201')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'928')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'944')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'LeSabre')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Regal')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Century')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Riviera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Skylark')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Coachbuilder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Estate')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Electra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Reatta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Sidekick')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Swift')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'SJ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Sable')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Topaz')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Grand Marquis')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Cougar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Legacy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Justy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Loyale')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'XT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'626')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'929')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'C-MAX Hybrid')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Edge')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Flex')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus ST')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Transit Connect')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Veloster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Accent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Elantra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Equus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Genesis Coupe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Sonata')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Cruze')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Malibu')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Tahoe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Audi', N'S4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Sierra 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 2500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Infiniti', N'JX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Jaguar', N'XK Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Rio')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Sorento')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'GS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'LX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'RX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'CX-5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'MAZDA6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'GT-R')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Rolls-Royce', N'Phantom')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'FR-S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'tC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Subaru', N'BRZ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Land Cruiser')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Venza')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volkswagen', N'CC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayman')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'370Z')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Murano')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Armada')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Pathfinder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series Super Duty')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'1 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'3 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'5 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'6 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'7 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'Z4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'GL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLK-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLS-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'C-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SLK55 AMG')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'R-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Lincoln', N'Navigator L')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S80')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V50')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S40')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Buick', N'Special')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini Cooper S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Rambler', N'Classic')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvair 500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Plymouth', N'Fury')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Volkswagen', N'Beetle')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Falcon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Tempo')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Aerostar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Escort')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Ranger')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Probe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'LTD Crown Victoria')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco II')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Festiva')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'W201')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'928')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'944')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'LeSabre')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Regal')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Century')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Riviera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Skylark')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Coachbuilder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Estate')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Electra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Reatta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Sidekick')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Swift')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'SJ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Sable')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Topaz')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Grand Marquis')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Cougar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Legacy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Justy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Loyale')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'XT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'626')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'929')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'C-MAX Hybrid')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Edge')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Flex')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus ST')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Transit Connect')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Veloster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Accent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Elantra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Equus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Genesis Coupe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Sonata')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Cruze')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Malibu')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Tahoe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Audi', N'S4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Sierra 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 2500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Infiniti', N'JX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Jaguar', N'XK Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Rio')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Sorento')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'GS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'LX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'RX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'CX-5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'MAZDA6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'GT-R')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Rolls-Royce', N'Phantom')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'FR-S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'tC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Subaru', N'BRZ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Land Cruiser')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Venza')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volkswagen', N'CC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Panamera')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayman')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'370Z')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Murano')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Armada')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Pathfinder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series Super Duty')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'1 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'3 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'5 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'6 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'7 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'Z4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'GL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLK-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLS-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'C-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SLK55 AMG')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'R-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Lincoln', N'Navigator L')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S80')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V50')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S40')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1909, N'Ford', N'Model T')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1926, N'Chrysler', N'Imperial')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1948, N'Citroën', N'2CV')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1953, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Cadillac', N'Fleetwood')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1956, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvair')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fillmore', N'Fillmore')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fairthorpe', N'Rockette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Buick', N'Special')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini Cooper S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Rambler', N'Classic')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvair 500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Plymouth', N'Fury')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Thunderbird')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Volkswagen', N'Beetle')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Falcon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Tempo')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Aerostar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Escort')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Ranger')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Probe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'LTD Crown Victoria')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco II')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Festiva')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'W201')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'928')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'944')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'LeSabre')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Regal')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Century')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Riviera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Skylark')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Coachbuilder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Estate')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Electra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Reatta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Sidekick')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Swift')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'SJ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Sable')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Topaz')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Grand Marquis')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Cougar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Legacy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Justy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Loyale')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'XT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'626')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'929')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'C-MAX Hybrid')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Edge')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Flex')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus ST')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Transit Connect')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Veloster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Accent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Elantra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Equus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Genesis Coupe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Sonata')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Cruze')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Malibu')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Tahoe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Audi', N'S4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Sierra 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 2500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Infiniti', N'JX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Jaguar', N'XK Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Rio')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Sorento')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'GS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'LX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'RX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'CX-5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'MAZDA6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'GT-R')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Boxster')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Rolls-Royce', N'Phantom')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'FR-S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'tC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Subaru', N'BRZ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Land Cruiser')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Venza')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volkswagen', N'CC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayman')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'370Z')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Murano')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Armada')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Pathfinder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series Super Duty')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'1 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'3 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'5 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'6 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'7 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'Z4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'GL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLK-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLS-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'C-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SLK55 AMG')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'R-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Lincoln', N'Navigator L')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S80')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V50')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S40')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1909, N'Ford', N'Model T')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1926, N'Chrysler', N'Imperial')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1948, N'Citroën', N'2CV')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1953, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Cadillac', N'Fleetwood')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1956, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvair')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fillmore', N'Fillmore')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fairthorpe', N'Rockette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1909, N'Ford', N'Model T')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1926, N'Chrysler', N'Imperial')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1948, N'Citroën', N'2CV')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1950, N'Hillman', N'Minx Magnificent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1953, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1954, N'Cadillac', N'Fleetwood')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1955, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1956, N'Chevrolet', N'Corvette')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1957, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1958, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1959, N'BMW', N'600')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvair')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fillmore', N'Fillmore')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1960, N'Fairthorpe', N'Rockette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Buick', N'Special')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini Cooper S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Rambler', N'Classic')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvair 500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Plymouth', N'Fury')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Volkswagen', N'Beetle')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Falcon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Tempo')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Aerostar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Escort')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Ranger')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Probe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'LTD Crown Victoria')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco II')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Festiva')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'W201')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'928')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'944')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'LeSabre')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Regal')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Century')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Riviera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Skylark')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Coachbuilder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Estate')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Electra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Reatta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Sidekick')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Swift')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'SJ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Sable')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Topaz')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Grand Marquis')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Cougar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Legacy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Justy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Loyale')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'XT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'626')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'929')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'C-MAX Hybrid')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Edge')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Explorer')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Flex')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus ST')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Transit Connect')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Veloster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Accent')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Elantra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Equus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Genesis Coupe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Sonata')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Cruze')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Malibu')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Tahoe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Audi', N'S4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Sierra 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 2500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Infiniti', N'JX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Jaguar', N'XK Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Rio')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Sorento')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'GS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'LX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'RX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'CX-5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'MAZDA6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'GT-R')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Rolls-Royce', N'Phantom')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'FR-S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'tC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Subaru', N'BRZ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Land Cruiser')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Venza')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volkswagen', N'CC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayman')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'370Z')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Murano')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Armada')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Pathfinder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series Super Duty')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'1 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'3 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'5 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'6 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'7 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'Z4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'GL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLK-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLS-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'C-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SLK55 AMG')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'R-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Lincoln', N'Navigator L')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S80')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V50')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C30')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S40')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1961, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1962, N'Buick', N'Special')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Austin', N'Mini Cooper S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Rambler', N'Classic')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvair 500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1963, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Plymouth', N'Fury')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Studebaker', N'Avanti')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1964, N'Austin', N'Mini Cooper')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'GTO')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Grand Prix')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'LeMans')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Bonneville')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Pontiac', N'Tempest')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Volkswagen', N'Beetle')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1965, N'Chevrolet', N'Corvette')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Galaxie')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Falcon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1966, N'Ford', N'Fairlane')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Thunderbird')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Tempo')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Aerostar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Escort')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Ranger')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Probe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'LTD Crown Victoria')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco II')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Bronco')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Ford', N'Festiva')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercedes-Benz', N'W201')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'928')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Porsche', N'944')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'LeSabre')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Regal')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Century')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Riviera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Skylark')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Coachbuilder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Estate')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Electra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Buick', N'Reatta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Sidekick')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'Swift')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Suzuki', N'SJ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Sable')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Topaz')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Grand Marquis')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mercury', N'Cougar')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Legacy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Justy')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'Loyale')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Subaru', N'XT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'626')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (1990, N'Mazda', N'929')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'C-MAX Hybrid')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Edge')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Flex')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Focus ST')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Ford', N'Transit Connect')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Veloster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Accent')
GO
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Elantra')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Equus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Genesis Coupe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Hyundai', N'Sonata')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Cruze')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Malibu')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Chevrolet', N'Tahoe')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Audi', N'S4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X5 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'BMW', N'X6 M')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Sierra 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 1500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'GMC', N'Yukon XL 2500')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Infiniti', N'JX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Jaguar', N'XK Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Rio')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Kia', N'Sorento')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'GS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'LX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lexus', N'RX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKS')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKT')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Lincoln', N'MKX')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'CX-5')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Mazda', N'MAZDA6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Nissan', N'GT-R')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Rolls-Royce', N'Phantom')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'FR-S')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Scion', N'tC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Subaru', N'BRZ')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Land Cruiser')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Toyota', N'Venza')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volkswagen', N'CC')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2013, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'911')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayenne')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Panamera')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Boxster')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Porsche', N'Cayman')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Altima')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'370Z')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Murano')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Armada')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2012, N'Nissan', N'Pathfinder')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Taurus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Escape')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series Super Duty')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Mustang')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fusion')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'E-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Fiesta')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Explorer')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'Focus')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2011, N'Ford', N'F-Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'1 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'3 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'5 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'6 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'7 Series')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'M6')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'Z4')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2010, N'BMW', N'X3')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'E-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'GL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLK-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'S-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CLS-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'CL-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'C-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'SLK55 AMG')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2009, N'Mercedes-Benz', N'R-Class')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Lincoln', N'Navigator L')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S80')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V50')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'C30')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'XC90')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S60')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'S40')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Volvo', N'V70')
INSERT [dbo].[vehicleModelYear] ([year], [make], [model]) VALUES (2008, N'Mercedes-Benz', N'SL-Class')
/****** Object:  Index [UK_UserId_Step]    Script Date: 3/24/2016 7:01:47 PM ******/
ALTER TABLE [dbo].[step] ADD  CONSTRAINT [UK_UserId_Step] UNIQUE NONCLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_unit]    Script Date: 3/24/2016 7:01:47 PM ******/
ALTER TABLE [dbo].[unit] ADD  CONSTRAINT [UK_unit] UNIQUE NONCLUSTERED 
(
	[unit_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_user_name]    Script Date: 3/24/2016 7:01:47 PM ******/
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [UK_user_name] UNIQUE NONCLUSTERED 
(
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[branch]  WITH CHECK ADD  CONSTRAINT [FK_branch_company] FOREIGN KEY([company_id])
REFERENCES [dbo].[company] ([company_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[branch] CHECK CONSTRAINT [FK_branch_company]
GO
ALTER TABLE [dbo].[branch]  WITH CHECK ADD  CONSTRAINT [FK_branch_states] FOREIGN KEY([state_id])
REFERENCES [dbo].[states] ([state_id])
GO
ALTER TABLE [dbo].[branch] CHECK CONSTRAINT [FK_branch_states]
GO
ALTER TABLE [dbo].[company]  WITH CHECK ADD  CONSTRAINT [FK_company_companyType] FOREIGN KEY([company_type])
REFERENCES [dbo].[companyType] ([company_type_id])
GO
ALTER TABLE [dbo].[company] CHECK CONSTRAINT [FK_company_companyType]
GO
ALTER TABLE [dbo].[company]  WITH CHECK ADD  CONSTRAINT [FK_company_states] FOREIGN KEY([stateId])
REFERENCES [dbo].[states] ([state_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[company] CHECK CONSTRAINT [FK_company_states]
GO
ALTER TABLE [dbo].[curtailment]  WITH CHECK ADD  CONSTRAINT [FK_curtailment_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[curtailment] CHECK CONSTRAINT [FK_curtailment_loan]
GO
ALTER TABLE [dbo].[curtailmentBackup]  WITH CHECK ADD  CONSTRAINT [FK_curtailmentBackup_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[curtailmentBackup] CHECK CONSTRAINT [FK_curtailmentBackup_loan]
GO
ALTER TABLE [dbo].[curtailmentBackup]  WITH CHECK ADD  CONSTRAINT [FK_curtailmentBackup_unit] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([unit_id])
GO
ALTER TABLE [dbo].[curtailmentBackup] CHECK CONSTRAINT [FK_curtailmentBackup_unit]
GO
ALTER TABLE [dbo].[curtailmentSchedule]  WITH CHECK ADD  CONSTRAINT [FK_curtailmentSchedule_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[curtailmentSchedule] CHECK CONSTRAINT [FK_curtailmentSchedule_loan]
GO
ALTER TABLE [dbo].[curtailmentSchedule]  WITH CHECK ADD  CONSTRAINT [FK_curtailmentSchedule_unit] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([unit_id])
GO
ALTER TABLE [dbo].[curtailmentSchedule] CHECK CONSTRAINT [FK_curtailmentSchedule_unit]
GO
ALTER TABLE [dbo].[forgotPasswordToken]  WITH CHECK ADD  CONSTRAINT [FK_forgotPasswordToken_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[forgotPasswordToken] CHECK CONSTRAINT [FK_forgotPasswordToken_user]
GO
ALTER TABLE [dbo].[interest]  WITH CHECK ADD  CONSTRAINT [FK_interest_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
GO
ALTER TABLE [dbo].[interest] CHECK CONSTRAINT [FK_interest_loan]
GO
ALTER TABLE [dbo].[justAddedUnit]  WITH CHECK ADD  CONSTRAINT [FK_justAddedUnit_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[justAddedUnit] CHECK CONSTRAINT [FK_justAddedUnit_loan]
GO
ALTER TABLE [dbo].[loanDetails]  WITH CHECK ADD  CONSTRAINT [FK_loanDetails_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[loanDetails] CHECK CONSTRAINT [FK_loanDetails_loan]
GO
ALTER TABLE [dbo].[loanUnitType]  WITH CHECK ADD  CONSTRAINT [FK_loanUnitType_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[loanUnitType] CHECK CONSTRAINT [FK_loanUnitType_loan]
GO
ALTER TABLE [dbo].[loanUnitType]  WITH CHECK ADD  CONSTRAINT [FK_loanUnitType_unitType] FOREIGN KEY([unit_type_id])
REFERENCES [dbo].[unitType] ([unit_type_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[loanUnitType] CHECK CONSTRAINT [FK_loanUnitType_unitType]
GO
ALTER TABLE [dbo].[lotInspectionFee]  WITH CHECK ADD  CONSTRAINT [FK_lotInspectionFee_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[lotInspectionFee] CHECK CONSTRAINT [FK_lotInspectionFee_loan]
GO
ALTER TABLE [dbo].[monthlyLoanFee]  WITH CHECK ADD  CONSTRAINT [FK_monthlyLoanFee_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[monthlyLoanFee] CHECK CONSTRAINT [FK_monthlyLoanFee_loan]
GO
ALTER TABLE [dbo].[nonRegisteredBranch]  WITH CHECK ADD  CONSTRAINT [FK_nonRegisteredBranch_branch] FOREIGN KEY([branch_id])
REFERENCES [dbo].[branch] ([branch_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[nonRegisteredBranch] CHECK CONSTRAINT [FK_nonRegisteredBranch_branch]
GO
ALTER TABLE [dbo].[nonRegisteredBranch]  WITH CHECK ADD  CONSTRAINT [FK_nonRegisteredBranch_nonRegisteredCompany] FOREIGN KEY([company_id])
REFERENCES [dbo].[nonRegisteredCompany] ([company_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[nonRegisteredBranch] CHECK CONSTRAINT [FK_nonRegisteredBranch_nonRegisteredCompany]
GO
ALTER TABLE [dbo].[nonRegisteredCompany]  WITH CHECK ADD  CONSTRAINT [FK_nonRegisteredCompany_company] FOREIGN KEY([reg_company_id])
REFERENCES [dbo].[company] ([company_id])
GO
ALTER TABLE [dbo].[nonRegisteredCompany] CHECK CONSTRAINT [FK_nonRegisteredCompany_company]
GO
ALTER TABLE [dbo].[partialCurtailment]  WITH CHECK ADD  CONSTRAINT [FK_partialCurtailment_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[partialCurtailment] CHECK CONSTRAINT [FK_partialCurtailment_loan]
GO
ALTER TABLE [dbo].[partialCurtailment]  WITH CHECK ADD  CONSTRAINT [FK_partialCurtailment_unit] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([unit_id])
GO
ALTER TABLE [dbo].[partialCurtailment] CHECK CONSTRAINT [FK_partialCurtailment_unit]
GO
ALTER TABLE [dbo].[step]  WITH CHECK ADD  CONSTRAINT [FK_step_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[step] CHECK CONSTRAINT [FK_step_user]
GO
ALTER TABLE [dbo].[systemAdmin]  WITH CHECK ADD  CONSTRAINT [FK_systemAdmin_systemAdminLevel] FOREIGN KEY([level_id])
REFERENCES [dbo].[systemAdminLevel] ([level_id])
GO
ALTER TABLE [dbo].[systemAdmin] CHECK CONSTRAINT [FK_systemAdmin_systemAdminLevel]
GO
ALTER TABLE [dbo].[title]  WITH CHECK ADD  CONSTRAINT [FK_title_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[title] CHECK CONSTRAINT [FK_title_loan]
GO
ALTER TABLE [dbo].[unit]  WITH CHECK ADD  CONSTRAINT [FK_unit_loan] FOREIGN KEY([loan_id])
REFERENCES [dbo].[loan] ([loan_id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[unit] CHECK CONSTRAINT [FK_unit_loan]
GO
ALTER TABLE [dbo].[uploadTitle]  WITH CHECK ADD  CONSTRAINT [FK_uploadTitle_uploadTitle] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([unit_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[uploadTitle] CHECK CONSTRAINT [FK_uploadTitle_uploadTitle]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_userRole] FOREIGN KEY([role_id])
REFERENCES [dbo].[userRole] ([role_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_userRole]
GO
ALTER TABLE [dbo].[userActivation]  WITH CHECK ADD  CONSTRAINT [FK_user_activation_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[userActivation] CHECK CONSTRAINT [FK_user_activation_user]
GO
ALTER TABLE [dbo].[userPermission]  WITH CHECK ADD  CONSTRAINT [FK_userPermission_user] FOREIGN KEY([modified_by])
REFERENCES [dbo].[user] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[userPermission] CHECK CONSTRAINT [FK_userPermission_user]
GO
/****** Object:  StoredProcedure [dbo].[spAdvanceAllSelectedItems]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 23/2/2016
-- Edited by:	Piyumi
-- Edite date:	03/08/2016
-- Description:	Advance all selected items
-- =============================================
CREATE PROCEDURE [dbo].[spAdvanceAllSelectedItems] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0, 
	@user_id int = 0,
	@advance_date datetime = null,
	@unit_id varchar(20) = null,
	@advance_amount decimal(12, 2) = 0.00,
	@modified_date datetime = null,

	@return int =0 output

AS
DECLARE @used_amount decimal(18, 2) ,@pending_amount decimal(18, 2) ,@loanAmount decimal(12, 2)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT unit_id FROM [dbo].[unit] WHERE unit_id = @unit_id)
	BEGIN

		IF EXISTS(SELECT loan_id FROM [dbo].[loanDetails] WHERE loan_id = @loan_id)
		BEGIN
			SELECT @used_amount = [used_amount], @pending_amount=[pending_amount] ,@loanAmount = [loan_amount]
			FROM [dbo].[viewLoanPaymentDetails] WHERE loan_id = @loan_id			

			IF(@loanAmount - @used_amount >= @advance_amount)
			BEGIN
				UPDATE [dbo].[unit]
				SET is_advanced = 1,
				modified_date = @modified_date,
				modified_by = @user_id,
				advance_amount = @advance_amount,
				unit_status = 1
				WHERE unit_id = @unit_id AND loan_id = @loan_id

				UPDATE [dbo].[loanDetails]
				SET used_amount = @used_amount+@advance_amount,	pending_amount = @pending_amount-@advance_amount	
				WHERE loan_id = @loan_id

				--update just added units
				IF EXISTS(SELECT unit_id FROM [dbo].[justAddedUnit] WHERE loan_id = @loan_id AND unit_id = @unit_id)
				BEGIN
	
					UPDATE [dbo].[justAddedUnit]
					SET advance_amount = @advance_amount,is_advanced = 1	
					WHERE loan_id = @loan_id AND unit_id = @unit_id
					--SET @return = 1
					--END	
				END
				SET @return = 1
			END
			ELSE
			BEGIN
				set @return = 2
			END	
		END
		
	END
	RETURN @return
END


GO
/****** Object:  StoredProcedure [dbo].[spCheckLoanIsInAdvanceFeeTable]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kasun samarawickrama
-- Create date: 2/10/2015
-- Description:	check Loan details are in Fees Tables
-- Return: if loan have fees details return them
-- =============================================
CREATE PROCEDURE [dbo].[spCheckLoanIsInAdvanceFeeTable] 
	@loan_id int,
	@return int =0 output
AS
BEGIN
IF (EXISTS(SELECT * FROM [dbo].[advanceFee] WHERE [loan_id] = @loan_id))
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *  FROM [dbo].[advanceFee] where [advanceFee].[loan_id] = @loan_id
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spCheckLoanIsInLotInspectionFeeTable]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kasun samarawickrama
-- Create date: 2/10/2015
-- Description:	check monthly Loan fee details available in table for the loan Id 
-- Return:		if table have monthly loan fees details return them
-- =============================================
CREATE PROCEDURE [dbo].[spCheckLoanIsInLotInspectionFeeTable] 
	@loan_id int,
	@return int =0 output
AS
BEGIN
IF (EXISTS(SELECT * FROM [dbo].[lotInspectionFee] WHERE [loan_id] = @loan_id))
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *  FROM [dbo].[lotInspectionFee] where [loan_id] = @loan_id
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spCheckLoanIsInMonthlyLoanFeeTable]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kasun samarawickrama
-- Create date: 2/10/2015
-- Description:	check monthly Loan fee details available in table for the loan Id 
-- Return:		if table have monthly loan fees details return them
-- =============================================
CREATE PROCEDURE [dbo].[spCheckLoanIsInMonthlyLoanFeeTable] 
	@loan_id int,
	@return int =0 output
AS
BEGIN
IF (EXISTS(SELECT * FROM [dbo].[monthlyLoanFee] WHERE [loan_id] = @loan_id))
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *  FROM [dbo].[monthlyLoanFee] where [loan_id] = @loan_id
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spCheckUserLoginWhileCompanySetup]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author     :	kasun samarawickrama
-- Create date: 1/26/2016
-- Description:	check the login user Company equal to a company which that company is on completing setup.
-- Return -1 -> login ok
-- Return 0  -> contact Admin
-- Return @stepId  -> login to step
-- =============================================
CREATE PROCEDURE [dbo].[spCheckUserLoginWhileCompanySetup]
(
@user_id int
)
AS
DECLARE @ReturnValue int 
DECLARE @CompanyId int 
DECLARE @RoleId int 
DECLARE @StepId int
DECLARE @OwnerId int
DECLARE @BranchId int
DECLARE @LoanBranchId int

BEGIN
SET NOCOUNT ON;
SET @ReturnValue =-1;
SET @CompanyId = -1;
SET @RoleId = -1;
SET @StepId = -1;
SET @OwnerId = -1;
SET @BranchId = -1;
SET @LoanBranchId = -1;


SET NOCOUNT ON;
create table #Temp1 ([user_id] int, company_id int, step_id int, branch_id int)

Insert Into #Temp1 Select [user].[user_id],company_id, step_id, [step].[branch_id] from [step] inner join [user] on [step].[user_id]=[user].[user_id]
Select @CompanyId = company_id, @RoleId = role_id, @BranchId = branch_id from [user] where [user].[user_id] = @user_id

IF EXISTS(SELECT * FROM #Temp1 where company_id = @CompanyId)
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
	IF @RoleId = 1
	BEGIN
		IF EXISTS(SELECT * FROM #Temp1 where (company_id = @CompanyId AND step_id > 6))
		BEGIN
			SELECT TOP 1 @ReturnValue = step_id ,@LoanBranchId = branch_id , @OwnerId = user_id FROM #Temp1 where( company_id = @CompanyId AND step_id > 6) ORDER BY step_id DESC
			UPDATE step set user_id = @user_id where (user_id = @OwnerId AND branch_id = @LoanBranchId)
		END
		ELSE
		BEGIN
			SELECT TOP 1 @ReturnValue = step_id , @OwnerId = user_id FROM #Temp1 where( company_id = @CompanyId AND step_id <= 6) ORDER BY step_id DESC
			UPDATE step set user_id = @user_id where (user_id = @OwnerId)
		END
	END
	ELSE IF @RoleId = 2
	BEGIN
		IF EXISTS(SELECT * FROM #Temp1 where (company_id = @CompanyId AND step_id <= 2))
		BEGIN
			Set @ReturnValue = 0
		END
		ELSE IF EXISTS(SELECT * FROM #Temp1 where (company_id = @CompanyId AND step_id <= 6))
		BEGIN
			SELECT TOP 1 @ReturnValue = step_id , @OwnerId = user_id  FROM #Temp1 where( company_id = @CompanyId AND step_id <= 6) ORDER BY step_id DESC
			UPDATE step set user_id = @user_id where (user_id = @OwnerId)
		END
		ELSE IF EXISTS(SELECT * FROM #Temp1 where (company_id = @CompanyId AND branch_id = @BranchId AND step_id > 6))
		BEGIN
			SELECT TOP 1 @ReturnValue = step_id, @LoanBranchId = branch_id , @OwnerId = user_id FROM #Temp1 where( company_id = @CompanyId AND step_id > 6) ORDER BY step_id DESC
			UPDATE step set user_id = @user_id where (user_id = @OwnerId AND branch_id = @LoanBranchId)
		END
		ELSE IF EXISTS(SELECT * FROM #Temp1 where (company_id = @CompanyId AND branch_id != @BranchId AND step_id > 6))
		BEGIN
			SET @ReturnValue = 6
			INSERT INTO step( user_id,step_id) values (@user_id,6)
		END
	END
	ELSE		
	BEGIN
		Set @ReturnValue = 0
	END
END
ELSE
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set @ReturnValue = -1
END
	

return @ReturnValue
end



GO
/****** Object:  StoredProcedure [dbo].[spCheckUserLoginWhileCompanySetup1]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author     :	Asanka Senarathna
-- Create date: 6/3/2016
-- Description:	check the Company Setup Step when User Login
-- =============================================
CREATE PROCEDURE [dbo].[spCheckUserLoginWhileCompanySetup1]
(
@company_id int,
@branch_id int
)
AS

BEGIN
SET NOCOUNT ON;
SELECT [step_number]
  FROM [dbo].[companySetupStep]
  where company_id = @company_id and branch_id=@branch_id
end

GO
/****** Object:  StoredProcedure [dbo].[spCheckUserLoginWhileCompanySetup2]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author     :	Asanka Senarathna
-- Create date: 9/3/2016
-- Description:	check the Company Setup Step when Super Admin Login
-- =============================================
CREATE PROCEDURE [dbo].[spCheckUserLoginWhileCompanySetup2]
(
@company_id int
)
AS

BEGIN
SET NOCOUNT ON;
DECLARE @branchCount int,@stepCount int

set @stepCount=(Select count([company_id])
      
  FROM [dbo].[companySetupStep]
  where [company_id]=@company_id
  group by [company_id])
  
set  @branchCount=(SELECT [BranchCount]
  FROM [dbo].[branchCount] 
  where [company_id]=@company_id)
  
  SELECT [step_number],@branchCount as branchCount ,@stepCount as stepCount FROM [dbo].[companySetupStep] where [company_id]=@company_id
  order by [step_number] DESC


end

GO
/****** Object:  StoredProcedure [dbo].[spCheckUserLoginWhileLoanSetup]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author     :	Asanka Senarathna
-- Create date: 7/3/2016
-- Description:	check the Loan Setup Step when User Login
-- =============================================
CREATE PROCEDURE [dbo].[spCheckUserLoginWhileLoanSetup]
(
@company_id int,
@branch_id int =null
)
AS

BEGIN
SET NOCOUNT ON;
DECLARE  @sql nvarchar(1000)
        ,@paramlist nvarchar(500)

SELECT @sql='SELECT [company_id],[branch_id],[step_number],[non_registered_branch_id] ,[loan_id]
FROM loanSetupStep
WHERE [company_id]=@xcompany_id'
  
IF @branch_id <> 0                                           
   SELECT @sql = @sql + ' AND [branch_id]=@xbranch_id'   


SELECT @paramlist = '@xcompany_id int,@xbranch_id int'


EXEC sp_executesql @sql ,@paramlist, @company_id,@branch_id

end


/*SELECT [step_number],[non_registered_branch_id] ,[loan_id]
FROM loanSetupStep as t1
WHERE loan_id = (SELECT MIN(loan_id)
             FROM loanSetupStep AS t2
             WHERE t2.loan_id = t1.loan_id) AND [company_id]=@xcompany_id'*/
GO
/****** Object:  StoredProcedure [dbo].[spCurtailmentsBackup]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spCurtailmentsBackup]
	-- Add the parameters for the stored procedure here
	@Input XML,
	@pay_date DATETIME,
	@title_status TINYINT,
	@ReturnValue int = 0 OUT
AS
BEGIN
	DECLARE @tableUnitIds table (lid INT, ui VARCHAR(50), balance DECIMAL(18, 2))
	DECLARE @tableDescription table (ui VARCHAR(50), curt_num INT, [desc] VARCHAR(1000))
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--INSERT INTO Test(unit_id)
	INSERT INTO @tableUnitIds(lid, ui, balance)
	SELECT
		XCol.value('(LoanId)[1]', 'INT'),
		XCol.value('(UnitId)[1]', 'varchar(50)'),
		XCol.value('(Balance)[1]', 'decimal(18, 2)')
	FROM
		@Input.nodes('/Units/Unit') AS XTbl(XCol)

	BEGIN TRANSACTION
		BEGIN TRY
			
			INSERT INTO @tableDescription(ui, curt_num, [desc])
			SELECT unit_id, curt_number, STUFF(
			(SELECT DISTINCT ';' + (CONVERT(varchar(100), curt_partial_amount) + ',' + CONVERT(varchar(100), paid_date))
			  FROM partialCurtailment
			  WHERE [unit_id] = PC.[unit_id] AND curt_number = PC.curt_number
			  FOR XML PATH (''))
			  , 1, 1, '')  AS URLList
			 FROM partialCurtailment AS PC
			GROUP BY unit_id, curt_number

			INSERT INTO curtailmentBackup (loan_id, unit_id, curt_number, curt_amount, curt_due_date, pay_date, curt_payment_details) 
			SELECT CS.loan_id, CS.unit_id, CS.curt_number, CS.curt_amount, CS.curt_due_date, @pay_date, TD.[desc] FROM curtailmentSchedule AS CS
			LEFT JOIN @tableDescription AS TD ON CS.unit_id = TD.ui AND CS.curt_number = TD.curt_num
			WHERE CS.unit_id in (select ui from @tableUnitIds) 
			GROUP BY  CS.loan_id, CS.unit_id, CS.curt_number, CS.curt_amount, CS.curt_due_date, TD.[desc]

			DELETE FROM curtailmentSchedule WHERE curtailmentSchedule.unit_id in (select ui from @tableUnitIds)
			DELETE FROM partialCurtailment WHERE partialCurtailment.unit_id in (select ui from @tableUnitIds)

			UPDATE unit
			SET unit_status = 2,
			title_status = @title_status
			WHERE unit_id in (select ui from @tableUnitIds)

			--Update loan details table
			UPDATE LD
			SET LD.used_amount = LD.used_amount - (select SUM(tU.balance) from @tableUnitIds AS tU WHERE tu.lid = LD.loan_id)
			FROM loanDetails AS LD
			WHERE LD.loan_id = (select TOP 1 lid from @tableUnitIds)

			--INNER JOIN @tableUnitIds AS TU ON LD.loan_id = TU.lid
			--DROP TABLE  #@tableUnitIds
			--DROP TABLE  #@tableDescription

			SET @ReturnValue = 1
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			SET @ReturnValue = -1
			ROLLBACK TRANSACTION
		END CATCH

	return @ReturnValue
END


GO
/****** Object:  StoredProcedure [dbo].[spDeleteCurtailment]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteCurtailment] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0,
	@return int = 1	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT [loan_id] FROM [curtailment] WHERE [loan_id] = @loan_id)
		BEGIN
			DELETE from [curtailment] where [loan_id] = @loan_id	
			SET @return = 2			
		END
		RETURN @return
END


GO
/****** Object:  StoredProcedure [dbo].[spDeleteJustAddedUnit]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka
-- Create date: 02/26/2016
-- Description:	Delete just added unit
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteJustAddedUnit]
	@user_id INT,
	@return INT = 0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT * FROM justAddedUnit WHERE user_id = @user_id)
	BEGIN 
		DELETE justAddedUnit WHERE user_id = @user_id
	END
	ELSE
	BEGIN 
		SET @return = -1
	END
END


GO
/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/18/2016
-- Description:	This SP is created to delete user
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteUser] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0,
	@return int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF EXISTS(SELECT user_id FROM [user] WHERE user_id = @user_id)
	BEGIN
	UPDATE [user] SET is_delete = 1 WHERE user_id = @user_id
	SET @return = 1
	END
	ELSE
   BEGIN
   SET @return = -1
   END
   RETURN @return
END



GO
/****** Object:  StoredProcedure [dbo].[spEmployeeLogin]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<kasun>
-- Create date: <1/13/2016>
-- Description:	<check Employee Logins>
-- =============================================
CREATE PROCEDURE [dbo].[spEmployeeLogin]
(
@userName NVARCHAR(50) 
)
AS
BEGIN
SET NOCOUNT ON;
SELECT  [system_admin_id], [password]
FROM [dbo].[systemAdmin]
WHERE [user_name]=@userName 

end


GO
/****** Object:  StoredProcedure [dbo].[spGetAllAccrualMethods]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/5/2016
-- Description:	get all accrual methods
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllAccrualMethods] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[accrualMethod]
END

GO
/****** Object:  StoredProcedure [dbo].[spGetAllCompanyType]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/13/2016
-- Description: retrieve all company type
-- =============================================

CREATE PROCEDURE [dbo].[spGetAllCompanyType]
AS
BEGIN
	SELECT company_type_id, company_type_name FROM companyType
END



GO
/****** Object:  StoredProcedure [dbo].[spGetAllUnitTypes]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 2/8/2015
-- Description:	get all unit types
-- Return: if unit type exists, return 1; if not exists return 0
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllUnitTypes] 
	
	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[unitType]  ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[unitType]  ORDER BY unit_type_id;
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spGetAllUserRole]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/16/2016
-- Description: get all user role
-- =============================================

CREATE PROCEDURE [dbo].[spGetAllUserRole]
AS
BEGIN
	SELECT role_id, role_name FROM userRole
END


GO
/****** Object:  StoredProcedure [dbo].[spGetAutoRemindEmailByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/8/2016
-- Description:	Get Auto Remind Email From loan table
-- =============================================
CREATE PROCEDURE [dbo].[spGetAutoRemindEmailByLoanId] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS (SELECT loan_id FROM [dbo].[loan] WHERE loan_id = @loan_id)
	BEGIN
	SELECT auto_remind_email FROM [dbo].[loan] WHERE loan_id = @loan_id
	END 
END

GO
/****** Object:  StoredProcedure [dbo].[spGetBranchByBranchId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Irfan
-- Create date: 1/11/2016
-- Description: get branch by branch id
-- =============================================
CREATE PROCEDURE [dbo].[spGetBranchByBranchId]
	-- Add the parameters for the stored procedure here
	@branch_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM branch
	WHERE branch_id = @branch_id
END



GO
/****** Object:  StoredProcedure [dbo].[spGetBranchesByCompanyCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kasun
-- Create date: 02/03/2016
-- Description:	get all branches of a company using company code
-- =============================================
CREATE PROCEDURE [dbo].[spGetBranchesByCompanyCode] 
	-- Add the parameters for the stored procedure here
	@company_code varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [branch].branch_id,
		   [branch].branch_name,
		   [branch].branch_code,
		   [branch].branch_address_1,
		   [branch].branch_address_2,
		   [branch].state_id,
		   [branch].city,
		   [branch].zip,
		   [branch].email,
		   [branch].phone_num_1,
		   [branch].phone_num_2,
		   [branch].phone_num_3,
		   [branch].fax

		   FROM [branch] INNER JOIN [company] ON [branch].company_id = [company].company_id WHERE [company].company_code = @company_code ORDER BY [branch].branch_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[spGetBranchesByCompanyId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/15/2015
-- Description:	get all branches by Company id
-- Return: if branches exist for company id, return 1; if not exists return 0
-- =============================================
CREATE PROCEDURE [dbo].[spGetBranchesByCompanyId] 
	@companyId int,
	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[branch] WHERE company_id = @companyId ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[branch] WHERE company_id = @companyId ORDER BY branch_name;
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spGetBranchIdByBranchCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/18/2016
-- Description: get branch id by branch code
-- =============================================
CREATE PROCEDURE [dbo].[spGetBranchIdByBranchCode]
	-- Add the parameters for the stored procedure here
	@branch_code VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT branch_id FROM branch
	WHERE branch_code = @branch_code
END



GO
/****** Object:  StoredProcedure [dbo].[spGetBranchIdByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/12/2016
-- Description:	get branch id from step table
-- =============================================
CREATE PROCEDURE [dbo].[spGetBranchIdByUserId] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0,
	@return int=0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT branch_id FROM [dbo].[step] WHERE user_id = @user_id)
	BEGIN
	SELECT @return = branch_id FROM [dbo].[step] WHERE user_id = @user_id
	END
	ELSE
	BEGIN
	SET @return = -1
	END
	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyCodebyCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 2016/01/17
-- Description:	Get company code 
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyCodebyCode]
	-- Add the parameters for the stored procedure here
	@company_code_prefix VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 company_code FROM company
	WHERE company_code like @company_code_prefix + '%'
	ORDER BY company_code DESC
	
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyCodeByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/17/2016
-- Description:	retrieve the company_id by user_id
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyCodeByUserId] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [company].company_id,[company].company_code FROM [user] INNER JOIN [branch] ON [user].branch_id = [branch].branch_id INNER JOIN [company] ON [branch].company_id = [company].company_id  WHERE [user].user_id = @user_id
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyDetailsBySUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 01/26/2016
-- Description:	Get company details by User id
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyDetailsBySUserId] 
	-- Add the parameters for the stored procedure here
	@user_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF EXISTS (SELECT * FROM company WHERE first_super_admin_id = @user_id)
		BEGIN
			SELECT * FROM company
			WHERE first_super_admin_id = @user_id
		END
	ELSE
		BEGIN
			SELECT * FROM company AS C
			INNER JOIN branch AS B ON C.company_id = B.company_id
			INNER JOIN [user] AS U ON B.branch_id = U.branch_id
			WHERE U.[user_id] = @user_id
		END
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyDetailsCompanyId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 01/26/2016
--
-- EditedBy:	Kanishka SHM
-- Edit date:   03/07/2016
--
-- Description:	Get company details by company id
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyDetailsCompanyId] 
	-- Add the parameters for the stored procedure here
	@company_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF EXISTS (SELECT * FROM company WHERE company_id = @company_id)
		BEGIN
			SELECT * FROM company
			WHERE company_id = @company_id
		END
	--ELSE
	--	BEGIN
	--		SELECT * FROM company AS C
	--		INNER JOIN branch AS B ON C.company_id = B.company_id
	--		INNER JOIN [user] AS U ON B.branch_id = U.branch_id
	--		WHERE U.[user_id] = @user_id
	--	END
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyEmployeeDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/20/2016
-- Description:	retrieve user_name and password from systemAdmin tbl
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyEmployeeDetails] 
	-- Add the parameters for the stored procedure here
	@system_admin_id int = 0,
	@return int=0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT system_admin_id FROM [systemAdmin] WHERE [systemAdmin].system_admin_id = @system_admin_id)
	BEGIN
	SELECT user_name,password FROM [systemAdmin] WHERE [systemAdmin].system_admin_id = @system_admin_id
	SET @return = 1
	END
	RETURN @return
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyIdByBranchId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 2016/01/17
-- Description:	Get company id by branch id
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyIdByBranchId]
	-- Add the parameters for the stored procedure here
	@branch_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT company_id FROM branch
	WHERE branch_id = @branch_id

END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyIdByCompanyCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/27/2016
-- Description:	get company Id by company code
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyIdByCompanyCode] 
	-- Add the parameters for the stored procedure here
	@company_code varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT company_id FROM [company] WHERE company_code = @company_code
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyIdByCompanyName]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 1/18/2016
-- Description:	get company id by company name
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyIdByCompanyName]
	@company_name VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT company_id FROM company
	WHERE company_name = @company_name
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCompanySetupStep]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetCompanySetupStep]   
 @company_id int
 ,@branch_id int= NULL
	   
AS
DECLARE  @sql nvarchar(1000)
        ,@paramlist nvarchar(500)

SELECT @sql='SELECT  [step_number]
  FROM [dbo].[companySetupStep] Where (company_id=@xcompany_id)'
                                                                 
IF @branch_id <> 0                                           
   SELECT @sql = @sql + ' AND (branch_id = @xbranch_id)'   

SELECT @sql = @sql + ' and step_number in(select max(step_number)from [dbo].[companySetupStep] group by step_number)'

SELECT @paramlist = '@xcompany_id int,@xbranch_id int'


EXEC sp_executesql @sql ,@paramlist, @company_id,@branch_id
GO
/****** Object:  StoredProcedure [dbo].[spGetCompanySetupStep_back]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----------------------------------------------------
-- Author  : Asanka Senarathna
-- Created : 03/03/2016
-- Purpase : Search Company Setup Step 
----------------------------------------------------

CREATE PROCEDURE [dbo].[spGetCompanySetupStep_back]   
 @company_id int
 ,@branch_id int= NULL
	   
AS
DECLARE  @sql nvarchar(1000)
        ,@paramlist nvarchar(500)

SELECT @sql='SELECT [step_number]
  FROM [dbo].[companySetupStep] Where company_id=@xcompany_id'
                                                                 
IF @branch_id <> 0                                           
   SELECT @sql = @sql + ' AND branch_id = @xbranch_id'   


SELECT @paramlist = '@xcompany_id int,@xbranch_id int'

EXEC sp_executesql @sql ,@paramlist, @company_id,@branch_id
GO
/****** Object:  StoredProcedure [dbo].[spGetCompanyTypeByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/27/2016
-- Description:	Get the company type of a user
-- =============================================
CREATE PROCEDURE [dbo].[spGetCompanyTypeByUserId] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT user_id FROM [user] WHERE user_id = @user_id)
	BEGIN
		SELECT C.[company_type] FROM [user] AS U
		INNER JOIN [company] AS C ON U.[company_id] = C.[company_id]
		--INNER JOIN [companyType] AS CT ON [company].company_type = CT.company_type_id 
		WHERE U.[user_id] = @user_id
	END
	 
END



GO
/****** Object:  StoredProcedure [dbo].[spGetCurtailmentSheduleByDueDate]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Irfan
-- Create date: 03/17/2016
-- Description:	get all curtailments shedule by due date
-- =============================================
CREATE PROCEDURE [dbo].[spGetCurtailmentSheduleByDueDate] 
	-- Add the parameters for the stored procedure here
	@loan_id int ,
	@due_date datetime

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- if curtailment not completed
	SELECT [curtailmentSchedule].loan_id,
		   [curtailmentSchedule].unit_id,
		   [curtailmentSchedule].curt_number,
		   [curtailmentSchedule].curt_amount,
		   [curtailmentSchedule].curt_due_date,
		   [curtailmentSchedule].curt_status,
		   [unit].advance_date,
		   [unit].identification_number,
		   [unit].year,
		   [unit].make,
		   [unit].model,
		   [unit].advance_date
		  

		   FROM [curtailmentSchedule] INNER JOIN [unit] ON [unit].unit_id = [curtailmentSchedule].unit_id AND [unit].unit_status = 1    
		   WHERE [curtailmentSchedule].loan_id = @loan_id AND [curtailmentSchedule].curt_due_date <= @due_date AND [curtailmentSchedule].curt_status = 0 

		   UNION
  -- if curtailment is partial completed
		   SELECT [curtailmentSchedule].loan_id,
		   [curtailmentSchedule].unit_id,
		   [curtailmentSchedule].curt_number,
		   ISNULL([curtailmentSchedule].curt_amount - TotPaid , [curtailmentSchedule].curt_amount ) AS curt_amount,
		   [curtailmentSchedule].curt_due_date,
		   [curtailmentSchedule].curt_status,
		   [unit].advance_date,
		   [unit].identification_number,
		   [unit].year,
		   [unit].make,
		   [unit].model,
		   [unit].advance_date
		  

		   FROM ([curtailmentSchedule] INNER JOIN [unit] ON [unit].unit_id = [curtailmentSchedule].unit_id AND [unit].unit_status = 1)
		    LEFT JOIN ( SELECT loan_id, unit_id, curt_number,  SUM([partialCurtailment].curt_partial_amount) as TotPaid FROM [partialCurtailment] GROUP BY [partialCurtailment].unit_id , [partialCurtailment].loan_id , [partialCurtailment].curt_number  ) as PC
			ON PC.loan_id = [curtailmentSchedule].loan_id  
		   AND PC.unit_id = [curtailmentSchedule].unit_id AND PC.curt_number = [curtailmentSchedule].curt_number
		   WHERE  [curtailmentSchedule].loan_id = @loan_id AND [curtailmentSchedule].curt_due_date <= @due_date AND [curtailmentSchedule].curt_status = 2 
		   
		   ORDER BY [curtailmentSchedule].curt_due_date ASC


END



GO
/****** Object:  StoredProcedure [dbo].[spGetInterestDetailsByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/7/2016
-- Description:	Get interest details of a loan
-- =============================================
CREATE PROCEDURE [dbo].[spGetInterestDetailsByLoanId] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT interest_id FROM [dbo].[interest] WHERE loan_id = @loan_id)
	BEGIN
	SELECT * FROM [dbo].[interest] INNER JOIN [dbo].[loan] on [dbo].[interest].[loan_id] = [dbo].[loan].[loan_id] WHERE [dbo].[interest].[loan_id] = @loan_id AND [dbo].[loan].[is_interest_calculate] = 1
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spGetJustAddedUnitDetailsByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/19/2016
-- Description:	Get just added unit details by loan id
-- =============================================
CREATE PROCEDURE [dbo].[spGetJustAddedUnitDetailsByLoanId] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0,
	@user_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[justAddedUnit] WHERE user_id = @user_id AND loan_id = @loan_id order by just_added_unit_id
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLatestUnitId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 02/24/2016
-- Description: Get latest unit id
-- =============================================
CREATE PROCEDURE [dbo].[spGetLatestUnitId]
	-- Add the parameters for the stored procedure here
	@loan_id INT,
	@return INT = 0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF EXISTS(SELECT unit_id FROM unit WHERE loan_id = @loan_id)
	BEGIN
		SELECT TOP 1 unit_id FROM unit WHERE loan_id = @loan_id ORDER BY unit_id DESC --created_date DESC
		SET @return =1
	END
	ELSE
	BEGIN
		SET @return =-1
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLatestUnitImageName]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka
-- Create date: 02/26/2016
-- Description:	Get latest unit image name
-- =============================================
CREATE PROCEDURE [dbo].[spGetLatestUnitImageName]
	@unit_id INT,
	@return INT = 0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT file_path FROM uploadTitle WHERE unit_id = @unit_id)
	BEGIN
		SELECT TOP 1 file_path FROM uploadTitle WHERE unit_id = @unit_id ORDER BY upload_id
		SET @return =1
	END
	ELSE
	BEGIN
		SET @return =-1
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanCurtailmentBreakdown]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		nadeeka
-- Create date: 3/15/2016
-- Description:	get curtailment breakdown
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanCurtailmentBreakdown] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	    
	IF EXISTS (SELECT loan_id FROM [dbo].[viewLoanCurtailmentDetails] WHERE loan_id = @loan_id)
	BEGIN
	SELECT * FROM [dbo].[viewLoanCurtailmentDetails] WHERE loan_id = @loan_id
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanDetailsByLoanCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka
-- Create date: 02/29/2016
-- EditedBy:    Kanishka   
-- Edited date: 03/04/2016
-- Description:	Get loan details by loan code
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanDetailsByLoanCode]
	-- Add the parameters for the stored procedure here
	@loan_code VARCHAR(100),
	@return INT = 0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT loan_code FROM loan WHERE loan_code = @loan_code)
	BEGIN

		--SELECT L.*, NRB.branch_id FROM loan AS L
		--INNER JOIN nonRegisteredBranch AS NRB ON L.non_reg_branch_id = NRB.non_reg_branch_id
		--WHERE loan_code = @loan_code

		SELECT L.*, VBNN.branch_id, VBNN.r_branch_code, VBNN.company_code FROM loan AS L
		INNER JOIN viewBranchNonRegBranch AS VBNN ON L.non_reg_branch_id = VBNN.non_reg_branch_id
		WHERE loan_code = @loan_code

		SET @return = 1
	END
	ELSE
	BEGIN
		SET @return = -1
	END

	return @return
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanDetailsByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanDetailsByLoanId]
	-- Add the parameters for the stored procedure here
	@loan_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM loan
	WHERE loan_id = @loan_id
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanDetailsByNonRegBranchId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Irfan>
-- Create date: <Create Date,2/25/2016,>
-- Description:	<Description,get the Loans by non reg branch id,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanDetailsByNonRegBranchId]
	-- Add the parameters for the stored procedure here
	@non_reg_branch_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM loan
	WHERE non_reg_branch_id = @non_reg_branch_id
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanIdByBranchId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/8/2016
-- Description:	Get LoanId By userId From step table
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanIdByBranchId] 
	-- Add the parameters for the stored procedure here
	--@user_id int = 0,
	@company_id int =0,
	@branch_id int = 0,
	@non_reg_branch_id int =0,
	@return int =0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT branch_id FROM [dbo].[loanSetupStep] WHERE company_id = @company_id AND branch_id = @branch_id AND non_registered_branch_id = @non_reg_branch_id)
	BEGIN
	SELECT TOP 1  @return = loan_id FROM [dbo].[loanSetupStep] WHERE company_id = @company_id AND branch_id = @branch_id AND non_registered_branch_id = @non_reg_branch_id ORDER BY loan_id DESC
	END

	ELSE
	BEGIN
	SET @return = -1
	END

	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanIdByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/8/2016
-- Description:	Get LoanId By userId From step table
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanIdByUserId] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0,
	@return int =0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT user_id FROM [dbo].[step] WHERE user_id = @user_id)
	BEGIN
	SELECT @return = loan_id FROM [dbo].[step] WHERE user_id = @user_id
	END

	ELSE
	BEGIN
	SET @return = -1
	END

	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanPaymentDetailsByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/19/2016
-- Description:	Get loan payment details by loan id
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanPaymentDetailsByLoanId] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT loan_id FROM [dbo].[loan] WHERE loan_id = @loan_id)
	BEGIN
	SELECT [dbo].[loan].[loan_amount],[dbo].[loanDetails].[used_amount],[dbo].[loanDetails].[pending_amount] 
	FROM [dbo].[loan] LEFT JOIN [dbo].[loanDetails] ON [dbo].[loan].[loan_id] = [dbo].[loanDetails].[loan_id] WHERE [dbo].[loan].[loan_id] = @loan_id
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[spGetLoanStepOneByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Irfan
-- Create date: 2/11/2016
-- Description: get loan step one by loan id
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanStepOneByLoanId]
	-- Add the parameters for the stored procedure here
	@loan_id int,
	@return int=0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF EXISTS(SELECT loan_id FROM [loan] WHERE loan_id = @loan_id)

	BEGIN
    -- Insert statements for procedure here
	SELECT * FROM loan
	WHERE loan_id = @loan_id
	set @return = 1

	END
	return @return
END



GO
/****** Object:  StoredProcedure [dbo].[spGetLoanUnitTypesByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Irfan
-- Create date: 2/11/2016
-- Description: get loan unit types by loan id
-- =============================================
CREATE PROCEDURE [dbo].[spGetLoanUnitTypesByLoanId]
	-- Add the parameters for the stored procedure here
	@loan_id int,
	@return int=0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF EXISTS(SELECT loan_id FROM [loanUnitType] WHERE loan_id = @loan_id)

	BEGIN
    -- Insert statements for procedure here
	SELECT * FROM loanUnitType JOIN unitType on loanUnitType.unit_type_id = unitType.unit_type_id
	WHERE loan_id = @loan_id
	set @return = 1

	END
	return @return
END



GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegBranchByNonRegBranchId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Irfan
-- Create date: 1/11/2016
-- Description: get non registered branch by non registerd branch id
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegBranchByNonRegBranchId]
	-- Add the parameters for the stored procedure here
	@non_reg_branch_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * , nonRegisteredCompany.company_name FROM nonRegisteredBranch , nonRegisteredCompany
	WHERE  nonRegisteredCompany.company_id = nonRegisteredBranch.company_id AND non_reg_branch_id = @non_reg_branch_id
END




GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegBranchesByCompanyId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 2/5/2015
-- Description:	get all non Registered company branches by Registered Company id
-- Return: if branches exist for company id, return 1; if not exists return 0
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegBranchesByCompanyId] 
	@companyId int,
	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT [nonRegisteredBranch].* FROM [dbo].[nonRegisteredBranch],[dbo].[branch],[dbo].[company] WHERE [nonRegisteredBranch].branch_id =[branch].branch_id AND [branch].company_id = @companyId ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [nonRegisteredBranch].* ,  [nonRegisteredCompany].company_name FROM [dbo].[nonRegisteredBranch],[dbo].[branch],[dbo].[company],[dbo].[nonRegisteredCompany] WHERE [nonRegisteredCompany].company_id = [nonRegisteredBranch].company_id AND  [nonRegisteredBranch].branch_id =[branch].branch_id AND [branch].company_id =[company].company_id AND [branch].company_id = @companyId ;
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END



GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegBranchesByNonRegCompanyId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka
-- Create date: 02/29/2016
-- Description:	get all non Registered company branches by non registered Company id
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegBranchesByNonRegCompanyId]
	-- Add the parameters for the stored procedure here
	@non_reg_companyId INT,
	@return INT = 0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF EXISTS(SELECT * FROM nonRegisteredBranch NRB INNER JOIN nonRegisteredCompany NRC ON NRB.company_id = NRC.company_id WHERE NRC.company_id = @non_reg_companyId)
	BEGIN
		SELECT NRB.*, NRC.* FROM nonRegisteredBranch NRB INNER JOIN nonRegisteredCompany NRC ON NRB.company_id = NRC.company_id WHERE NRC.company_id = @non_reg_companyId
		SET @return = 1
	END
	ELSE
	BEGIN
		set @return = 0
	END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegCompanyByCompanyId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 02/09/2016
-- Description:	Get non registered company details
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegCompanyByCompanyId]
	-- Add the parameters for the stored procedure here
	@company_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM nonRegisteredCompany
	WHERE company_id = @company_id
END

GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegCompanyByCreatedCompany]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 02/08/2016
-- Description:	Get Non Registered companies by created by company
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegCompanyByCreatedCompany] 
	-- Add the parameters for the stored procedure here
	@reg_company_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM nonRegisteredCompany
	WHERE reg_company_id = @reg_company_id
END

GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegCompanyCodebyCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 2016/02/08
-- Description:	Get company code 
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegCompanyCodebyCode]
	-- Add the parameters for the stored procedure here
	@company_code_prefix VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--SELECT company_code FROM nonRegisteredCompany
	--WHERE company_code like @company_code_prefix + '%'

	SELECT TOP 1 company_code FROM nonRegisteredCompany
	WHERE company_code like @company_code_prefix + '%'
	ORDER BY company_code DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegCompanyDetailsByRegCompanyId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 02/09/2016
-- Description:	Get non registered company details by registered company id
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegCompanyDetailsByRegCompanyId] 
	-- Add the parameters for the stored procedure here
	@reg_company_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM nonRegisteredCompany
	WHERE reg_company_id = @reg_company_id
END



GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegCompanyDetailsByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 01/27/2016
-- Description:	Get non registered company details by User id
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegCompanyDetailsByUserId] 
	-- Add the parameters for the stored procedure here
	@user_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM nonRegisteredCompany
	WHERE created_by = @user_id
END



GO
/****** Object:  StoredProcedure [dbo].[spGetNonRegCompanyIdByCompanyCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/27/2016
-- Description:	get company Id by company code
-- =============================================
CREATE PROCEDURE [dbo].[spGetNonRegCompanyIdByCompanyCode] 
	-- Add the parameters for the stored procedure here
	@company_code varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT company_id FROM [nonRegisteredCompany] WHERE company_code = @company_code
END



GO
/****** Object:  StoredProcedure [dbo].[spGetNotAdvancedUnitDetailsByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/19/2016
-- Description:	Get not advanced Unit details by loan id
-- =============================================
CREATE PROCEDURE [dbo].[spGetNotAdvancedUnitDetailsByLoanId] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--if add =0, if add and advance = 1
	IF EXISTS(SELECT loan_id FROM [dbo].[unit] WHERE loan_id = @loan_id)
	BEGIN
	SELECT [unit_id],[created_date],[identification_number],[year],[make],[model],[cost],[advance_amount] FROM [dbo].[unit] WHERE loan_id = @loan_id AND is_advanced = 0
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[spGetRights]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kasun
-- Create date: 1/16/2015
-- Description:	get all rights by user id
-- =============================================
Create PROCEDURE [dbo].[spGetRights] 
	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[userRights]) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[userRights];
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spGetRightsStringByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kasun
-- Create date: 1/17/2015
-- Description:	get rights Permision String by user id
-- =============================================
CREATE PROCEDURE [dbo].[spGetRightsStringByUserId] 
	@userId int,
	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[userPermission] where [user_id]=@userId ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[userPermission] where [user_id]=@userId ;
	set @return = 1
END
ELSE
BEGIN
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spGetState]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kasnishka SHM
-- Create date: 01/26/2016
-- Description:	Get all state
-- =============================================
CREATE PROCEDURE [dbo].[spGetState]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT state_id, state_name FROM states
END



GO
/****** Object:  StoredProcedure [dbo].[spGetStepNumberByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<kasun>
-- Create date: <1/25/2016>
-- Description:	<get User Existing Step number for a given userId>
-- =============================================
CREATE PROCEDURE [dbo].[spGetStepNumberByUserId]
(
@user_id int
)
AS
DECLARE @ReturnValue int
BEGIN
SET NOCOUNT ON;
IF EXISTS(SELECT user_id FROM [dbo].[step] WHERE user_id = @user_id)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT  @ReturnValue=[step_id]
	FROM [dbo].[step] WHERE [step].[user_id]=@user_id

END
ELSE
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
Set @ReturnValue =-1

END
return @ReturnValue
end



GO
/****** Object:  StoredProcedure [dbo].[spGetTitleDetailsByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/9/2016
-- Description:	GetTitleDetails By LoanId
-- =============================================
CREATE PROCEDURE [dbo].[spGetTitleDetailsByLoanId] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT title_id FROM [dbo].[title] WHERE loan_id = @loan_id)
	BEGIN
	SELECT * FROM [dbo].[title] WHERE loan_id = @loan_id 
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spGetTitlesByIdentificationNumber]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 03/16/2016
-- Description:	get titles by identification_number
-- =============================================
CREATE PROCEDURE [dbo].[spGetTitlesByIdentificationNumber] 
	-- Add the parameters for the stored procedure here
	@loan_code varchar(100) = null,
	@identification_number varchar(50) = null
AS
--DECLARE @loan_id int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--SELECT @loan_id = loan_id FROM [dbo].[loan] WHERE loan_code = @loan_code
	IF EXISTS(SELECT loan_id FROM [dbo].[loan] WHERE loan_code = @loan_code)
	BEGIN
		
		SELECT * FROM [dbo].[unit]  WHERE [dbo].[unit].[identification_number] LIKE '%'+@identification_number AND [dbo].[unit].[title_status] IS NOT NULL AND [dbo].[unit].[loan_id] = (SELECT loan_id FROM [dbo].[loan] WHERE loan_code = @loan_code)
		
		
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[spGetTopBranchIdByCompanyCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/17/2016
-- Description:	get all the branch codes of a company
-- =============================================
CREATE PROCEDURE [dbo].[spGetTopBranchIdByCompanyCode] 
	-- Add the parameters for the stored procedure here
	@company_code varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 [branch].branch_id,[branch].branch_code FROM [branch] INNER JOIN [company] ON [branch].company_id = [company].company_id WHERE [company].company_code = @company_code ORDER BY [branch].branch_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[spGetTopNonRegBranchIdByCompanyCode]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/17/2016
-- Description:	get all the branch codes of a company
-- =============================================
CREATE PROCEDURE [dbo].[spGetTopNonRegBranchIdByCompanyCode] 
	-- Add the parameters for the stored procedure here
	@company_code varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 [nonRegisteredBranch].branch_id,[nonRegisteredBranch].branch_code FROM [nonRegisteredBranch] INNER JOIN [nonRegisteredCompany] ON [nonRegisteredBranch].company_id = [nonRegisteredCompany].company_id WHERE [nonRegisteredcompany].company_code = @company_code ORDER BY [nonRegisteredBranch].branch_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[spGetUserDetailsById]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		piyumi
-- Create date: 1/15/2016
-- Edited by:	Kanishka
-- Edite date:  02/26/2016
--
-- Description:	retrieve details of a selected user
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserDetailsById] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0
	  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT U.user_id, U.first_name, U.last_name, U.email, U.phone_no, U.status, U.created_date, U.modified_date,
			UR.role_name,
			B.branch_name, B.branch_id, B.branch_code AS BranchCode, C.company_code AS CompanyCode
	FROM [user] AS U
	LEFT OUTER JOIN [userRole] AS UR ON U.role_id = UR.role_id 
	LEFT OUTER JOIN [branch] AS B ON U.branch_id = B.branch_id 
	INNER JOIN [company] AS C ON U.company_id = C.company_id
	WHERE U.user_id = @user_id AND U.is_delete = 0 
END


GO
/****** Object:  StoredProcedure [dbo].[spGetUserIdByemail]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/16/2016
-- Description:	retrieve user id using email
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserIdByemail] 
	-- Add the parameters for the stored procedure here
	(@email nvarchar(50)
	)
AS
DECLARE @sql nvarchar(4000), @paramlist nvarchar(500)
BEGIN
IF EXISTS(SELECT user_id FROM [dbo].[user] WHERE email = @email)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT @sql = 'SELECT TOP 1 user_id FROM [dbo].[user] WHERE email = @xemail'

END

SELECT @paramlist =  '@xemail nvarchar(50)'
print @sql
EXEC sp_executesql @sql  ,@paramlist, @email
END


GO
/****** Object:  StoredProcedure [dbo].[spGetUserIdByUserName]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/16/2016
-- Description: get user id by user name
-- =============================================

CREATE PROCEDURE [dbo].[spGetUserIdByUserName]
	@user_name VARCHAR(50)
AS
DECLARE @return_user_id int
BEGIN
	SELECT user_id FROM [user]
	WHERE user_name = @user_name
END


GO
/****** Object:  StoredProcedure [dbo].[spGetUserLevelByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<kasun>
-- Create date: <1/14/2016>
-- Description:	<get User Level for a given userId>

-- Update -kasun : <1/16/2016> level_id column remove from userRole table
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserLevelByUserId]
(
@userId int
)
AS
DECLARE @ReturnValue int
BEGIN
SET NOCOUNT ON;
SELECT  @ReturnValue=[role_id]
FROM [dbo].[user] 
WHERE [user].[user_id]=@userId
return @ReturnValue
end



GO
/****** Object:  StoredProcedure [dbo].[spGetUserLoginDetailsByType]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		piyumi
-- Create date: 1/16/2016
-- Description:	retrieve username and created person of a user
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserLoginDetailsByType] 
	-- Add the parameters for the stored procedure here
	@level_id int = 0,
	@user_id int = 0

AS
DECLARE @comp_id int,@user_role int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT @user_role = role_id FROM [user] WHERE user_id = @user_id
	SELECT @comp_id = [company].company_id FROM [company] RIGHT OUTER JOIN [branch] ON [company].company_id = [branch].company_id WHERE [branch].branch_id = (SELECT branch_id FROM [user] WHERE user_id = @user_id)
	IF(@level_id=1)
	BEGIN
	
	SELECT [user].user_id, [user].user_name,[user].password,[user].created_by,[user].role_id FROM [user] RIGHT OUTER JOIN [branch] ON [user].branch_id = [branch].branch_id where role_id = @level_id AND [branch].company_id = @comp_id AND [user].is_delete = 0 AND [user].user_id != @user_id
	END
	ELSE IF(@level_id=2)
	BEGIN
	SELECT [user].user_id, [user].user_name,[user].password,[user].created_by,[user].role_id FROM [user]  WHERE role_id = @level_id AND [user].is_delete = 0 AND [user].user_id != @user_id AND [user].branch_id = (SELECT branch_id FROM [user] WHERE [user].user_id = @user_id )
	END
	ELSE IF(@level_id=3)
	BEGIN
	IF(@user_role = 1)
	BEGIN
	SELECT [user].user_id, [user].user_name,[user].password,[user].created_by,[user].role_id FROM [user] INNER JOIN [branch] ON [user].branch_id = [branch].branch_id INNER JOIN [company] ON [branch].company_id = [company].company_id WHERE role_id = @level_id AND [company].company_id = @comp_id AND [user].is_delete = 0 AND [user].user_id != @user_id
	END
	ELSE IF(@user_role = 2)
	BEGIN
	SELECT [user].user_id, [user].user_name,[user].password,[user].created_by,[user].role_id FROM [user]  WHERE role_id = @level_id AND [user].is_delete = 0 AND [user].user_id != @user_id AND [user].branch_id = (SELECT branch_id FROM [user] WHERE [user].user_id = @user_id )
	END
	END
END


GO
/****** Object:  StoredProcedure [dbo].[spGetUserNameByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		piyumi
-- Create date: 17/1/2016
-- Description:	This SP is created for retrieving user name when user id is given
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserNameByUserId] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT user_name FROM [user] WHERE user_id = @user_id
END



GO
/****** Object:  StoredProcedure [dbo].[spGetUserRole]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 16/1/2016
-- Description:	
-- Modified : 20/1/2016  Irfan 
-- Reson : to access the Role Name
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserRole] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [user].role_id, role_name FROM [user],[userRole] WHERE [user].role_id = [userRole].role_id and user_id = @user_id
END



GO
/****** Object:  StoredProcedure [dbo].[spGetUsersbyCompany]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishak SHM
-- Create date: 2015-02-08
-- Description:	Get users by company
-- =============================================
CREATE PROCEDURE [dbo].[spGetUsersbyCompany]
	-- Add the parameters for the stored procedure here
	@company_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT U.* FROM [user] AS U
	INNER JOIN branch AS B ON U.branch_id = B.branch_id
	INNER JOIN company AS C ON B.company_id = C.company_id
	WHERE C.company_id = @company_id 
END

GO
/****** Object:  StoredProcedure [dbo].[spGetVehicleMakesByYear]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/25/2016
-- Description:	Get vehicle models by make
-- =============================================
CREATE PROCEDURE [dbo].[spGetVehicleMakesByYear] 
	-- Add the parameters for the stored procedure here
	@unit_type int = 0,
	@year int =0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@unit_type=1)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[vehicleModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[vehicleModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END
	
	IF(@unit_type=2)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[rvModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[rvModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END
	
	IF(@unit_type=3)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[camperModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[camperModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END

	IF(@unit_type=4)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[atvModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[atvModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END

	IF(@unit_type=5)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[boatModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[boatModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END

	IF(@unit_type=6)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[motorcyclesModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[motorcyclesModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END

	IF(@unit_type=7)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[snowmobileModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[snowmobileModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END

	IF(@unit_type=8)
	BEGIN
	IF EXISTS(SELECT make FROM [dbo].[heavyEquipmentModelYear] WHERE year <= @year +1)
	BEGIN
	SELECT DISTINCT make FROM [dbo].[heavyEquipmentModelYear] WHERE year <= @year +1  GROUP BY make;
	END
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spGetVehicleModelByMakeYear]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/25/2016
-- Description:	Get vehicle models by make
-- =============================================
CREATE PROCEDURE [dbo].[spGetVehicleModelByMakeYear] 
	-- Add the parameters for the stored procedure here
	@unit_type int = 0,
	@year int= 0,
	@make varchar(100) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@unit_type=1)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[vehicleModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[vehicleModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END
	
	IF(@unit_type=2)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[rvModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[rvModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END
	
	IF(@unit_type=3)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[camperModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[camperModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END

	IF(@unit_type=4)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[atvModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[atvModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END

	IF(@unit_type=5)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[boatModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[boatModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END

	IF(@unit_type=6)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[motorcyclesModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[motorcyclesModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END

	IF(@unit_type=7)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[snowmobileModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[snowmobileModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END

	IF(@unit_type=8)
	BEGIN
	IF EXISTS(SELECT model FROM [dbo].[heavyEquipmentModelYear] WHERE  year <= (@year +1 )AND make = @make )
	BEGIN
	SELECT DISTINCT model FROM [dbo].[heavyEquipmentModelYear] WHERE year <= (@year +1 )AND make = @make
	END
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spGetVehicleModelsByMake]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/25/2016
-- Description:	Get vehicle models by make
-- =============================================
CREATE PROCEDURE [dbo].[spGetVehicleModelsByMake] 
	-- Add the parameters for the stored procedure here
	@make varchar(100) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT make FROM [dbo].[vehicleModelYear] WHERE make = @make)
	BEGIN
	SELECT DISTINCT model FROM [dbo].[vehicleModelYear] WHERE make = @make
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertBranch]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/17/2016
-- Description:	insert branch details for a company
-- =============================================
CREATE PROCEDURE [dbo].[spInsertBranch] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0, 
	@branch_code varchar(50) = null,
	@branch_name varchar(50) = null,
	@branch_address_1 varchar(50) = null,
	@branch_address_2 varchar(50) = null,
	@state_id int = 0,
	@city varchar(50) = null,
	@zip varchar(50) = null,
	@email varchar(100) = null,
	@phone_num_1 varchar(50) = null,
	@phone_num_2 varchar(50) = null,
	@phone_num_3 varchar(50) = null,
	@fax varchar(50) = null,
	@company_id int =0,
	@created_date datetime = null,
	@return int=-1 output
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT branch_id FROM [branch] WHERE branch_code = @branch_code)
		BEGIN
		--SELECT @company_id = company_id FROM [user] WHERE user_id = @user_id
			INSERT INTO [branch]([branch_code],
			[branch_name],
			[branch_address_1],
			[branch_address_2],
			[state_id],
			[city],
			[zip],
			[email],
			[phone_num_1],
			[phone_num_2],
			[phone_num_3],
			[fax],
			[created_by],
			[created_date],
			[company_id])

			VALUES(@branch_code,
			@branch_name,
			@branch_address_1,
			@branch_address_2,
			@state_id,
			@city,
			@zip,
			@email,
			@phone_num_1,
			@phone_num_2,
			@phone_num_3,
			@fax,
			@user_id,
			@created_date,
			@company_id)
	
	SELECT @return = branch_id FROM [branch] WHERE branch_code = @branch_code
	
			
		END
	ELSE
		BEGIN
			UPDATE [branch]
			SET [branch_name] = @branch_name,
				[branch_address_1] = @branch_address_1,
				[branch_address_2] = @branch_address_2,
				[state_id] = @state_id,
				[city] = @city,
				[zip] = @zip,
				[email] = @email,
				[phone_num_1] = @phone_num_1,
				[phone_num_2] = @phone_num_2,
				[phone_num_3] = @phone_num_3,
				[fax] = @fax
			WHERE branch_code = @branch_code
			SELECT @return = branch_id FROM [branch] WHERE branch_code = @branch_code
			--set @return = 2
		END
	

RETURN @return
END



GO
/****** Object:  StoredProcedure [dbo].[spInsertCompany]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 02/03/2016
-- Description: insert new company
-- =============================================
CREATE PROCEDURE [dbo].[spInsertCompany]
	@company_name VARCHAR(50),
	@company_code VARCHAR(50),
	@company_address_1 VARCHAR(50),
	@company_address_2 VARCHAR(50),
	@stateId INT,
	@city VARCHAR(50),
	@zip VARCHAR(50),
	@email VARCHAR(50),
	@phone_num_1 VARCHAR(50),
	@phone_num_2 VARCHAR(50),
	@phone_num_3 VARCHAR(50),
	@fax VARCHAR(50),
	@website_url VARCHAR(50),
	@created_by INT,
	@created_date DateTime,
	@company_type INT,
	@first_super_admin_id INT,
	@company_status BIT,
	@transaction_type VARCHAR(50),
	@return INT = 0 OUT
AS
BEGIN
	IF (@transaction_type = 'UPDATE')
		BEGIN 
			UPDATE company
			SET company_name = @company_name,
				company_address_1 = @company_address_1,  
				company_address_2 = @company_address_2, 
				stateId = @stateId, 
				city = @city,
				zip = @zip, 
				email = @email, 
				phone_num_1 = @phone_num_1, 
				phone_num_2 = @phone_num_2, 
				phone_num_3 = @phone_num_3, 
				fax = @fax, 
				website_url = @website_url, 
				company_type = @company_type,
				company_status = @company_status
			WHERE company_code = @company_code

			SELECT @return = company_id FROM company WHERE company_code = @company_code

		END
	ELSE IF(@transaction_type = 'INSERT')
		BEGIN
			DECLARE @CompanyId int
			DECLARE @tableCompany table (cid int)

			INSERT INTO company(company_name, company_code, company_address_1, company_address_2, stateId, city, zip, email, phone_num_1, 
				phone_num_2, phone_num_3, fax, website_url, created_by, created_date, company_type, first_super_admin_id, company_status)

			OUTPUT Inserted.company_id into @tableCompany

			VALUES(@company_name, @company_code, @company_address_1, @company_address_2, @stateId, @city, @zip, @email, @phone_num_1, 
				@phone_num_2, @phone_num_3, @fax, @website_url, @created_by, @created_date, @company_type, @first_super_admin_id, @company_status)

			SELECT @CompanyId = cid from @tableCompany

			UPDATE [user]
			SET company_id = @CompanyId
			WHERE [user_id] = @first_super_admin_id
			SET @return = @CompanyId
		END

	RETURN @return
END



GO
/****** Object:  StoredProcedure [dbo].[spInsertCompanyNew]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 02/03/2016
-- Description: insert new company
-- =============================================
CREATE PROCEDURE [dbo].[spInsertCompanyNew]
	@company_name VARCHAR(50),
	@company_code VARCHAR(50),
	@company_address_1 VARCHAR(50),
	@company_address_2 VARCHAR(50),
	@stateId INT,
	@city VARCHAR(50),
	@zip VARCHAR(50),
	@email VARCHAR(50),
	@phone_num_1 VARCHAR(50),
	@phone_num_2 VARCHAR(50),
	@phone_num_3 VARCHAR(50),
	@fax VARCHAR(50),
	@website_url VARCHAR(50),
	@created_by INT,
	@created_date DateTime,
	@company_type INT,
	@first_super_admin_id INT,
	@company_status BIT,
	@transaction_type VARCHAR(50)
AS
BEGIN
	IF (@transaction_type = 'UPDATE')
		BEGIN 
			UPDATE company
			SET company_address_1 = @company_address_1,  
				company_address_2 = @company_address_2, 
				stateId = @stateId, 
				city = @city,
				zip = @zip, 
				email = @email, 
				phone_num_1 = @phone_num_1, 
				phone_num_2 = @phone_num_2, 
				phone_num_3 = @phone_num_3, 
				fax = @fax, 
				website_url = @website_url, 
				company_type = @company_type,
				company_status = @company_status
			WHERE company_code = @company_code
		END
	ELSE IF(@transaction_type = 'INSERT')
		BEGIN
			INSERT INTO company(company_name, company_code, company_address_1, company_address_2, stateId, city, zip, email, phone_num_1, 
				phone_num_2, phone_num_3, fax, website_url, created_by, created_date, company_type, first_super_admin_id, company_status)
			VALUES(@company_name, @company_code, @company_address_1, @company_address_2, @stateId, @city, @zip, @email, @phone_num_1, 
				@phone_num_2, @phone_num_3, @fax, @website_url, @created_by, @created_date, @company_type, @first_super_admin_id, @company_status)
		END
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertCurtailment]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nadeeka
-- Create date: 2/09/2016
-- Description:	Insert curtailment detail of the loan
-- =============================================
CREATE PROCEDURE [dbo].[spInsertCurtailment] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0, 
	@curtailment_id int = 0, 
	@time_period varchar(50) = null,
	@percentage int = 0,
	@payment_percentage DECIMAL(18,3),
	@return int=0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--IF EXISTS(SELECT [loan_id] FROM [curtailment] WHERE [loan_id] = @loan_id)
		--BEGIN
			--DELETE from [curtailment] where [loan_id] = @loan_id	
			--set @return = 2
		--END
	--ELSE 
		--BEGIN
			--set @return = 1
		--END
	
	BEGIN
		INSERT INTO [curtailment]([loan_id],
		[time_period],
		[percentage], [payment_percentage])

		VALUES(@loan_id,
		@time_period,
		@percentage, @payment_percentage)
		
		set @return = 1
		
	END
	

RETURN @return
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertCurtailmentSchedule]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nadeeka
-- Create date: 03/16/2016
-- Description: Insert curtailment schedule details
-- =============================================
CREATE PROCEDURE [dbo].[spInsertCurtailmentSchedule]
	@Input XML,
	@loan_id int = 0,
	@unit_id varchar
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT * FROM curtailmentSchedule WHERE loan_id = @loan_id and unit_id = @unit_id)
	BEGIN
		INSERT INTO curtailmentSchedule(loan_id, unit_id, curt_number,curt_amount,curt_due_date,curt_status)
		SELECT
			XCol.value('(LoanId)[1]', 'int'),
			XCol.value('(UnitId)[1]', 'varchar(50)'),
			XCol.value('(CurtNo)[1]', 'int'),
			XCol.value('(CurtAmount)[1]', 'decimal(18, 2)'),
			XCol.value('(CurtDueDate)[1]', 'datetime'),
			XCol.value('(CurtStatus)[1]', 'int')
		FROM
			@Input.nodes('/Curtailments/Curtailment') AS XTbl(XCol)
	END
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertFeesDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kasun
-- Create date: 2/7/2016
-- Description:	insert Fees Details to advance, lot inspection and monthly loan tables. 
-- =============================================
CREATE PROCEDURE [dbo].[spInsertFeesDetails] 
	-- Add the parameters for the stored procedure here
	@advance_fee_amount decimal(15, 2) = 0.0, 
	@advance_fee_calculate_type varchar(10) = NULL,
	@advance_receipt bit = 0,
	@advance_payment_due_method varchar(50) = null,
	@advance_payment_due_date varchar(50) = null,
	@advance_auto_remind_dealer_email varchar(100) = null,
	@advance_delaer_remind_period int = 0,
	@advance_due_auto_remind_email varchar(100) = null,
	@advance_due_auto_remind_period int = 0,

	@monthly_loan_fee_amount decimal = 0.0, 
	@monthly_loan_receipt bit = 0,
	@monthly_loan_payment_due_method varchar(50) = null,
	@monthly_loan_payment_due_date varchar(50) = null,
	@monthly_loan_auto_remind_dealer_email varchar(100) = null,
	@monthly_loan_delaer_remind_period int = 0,
	@monthly_loan_due_auto_remind_email varchar(100) = null,
	@monthly_loan_due_auto_remind_period int = 0,

	@lot_inspection_amount decimal = 0.0, 
	@lot_inspection_receipt bit = 0,
	@lot_payment_due_method varchar(50) = null,
	@lot_payment_due_date varchar(50) = null,
	@lot_inspection_auto_remind_dealer_email varchar(100) = null,
	@lot_inspection_delaer_remind_period int = 0,
	@lot_inspection_due_auto_remind_email varchar(100) = null,
	@lot_inspection_due_auto_remind_period int = 0,
	
	@loan_id int = 0
AS
DECLARE @ReturnValue bit 
Set @ReturnValue = 0
IF @advance_fee_amount > 0
BEGIN
	IF (EXISTS(SELECT * FROM [dbo].[advanceFee] WHERE [loan_id] = @loan_id))
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		UPDATE [dbo].[advanceFee] SET 
		[advance_fee_amount] = @advance_fee_amount,
		[advance_fee_calculate_type] = @advance_fee_calculate_type,
		[receipt] = @advance_receipt,
		[payment_due_method] = @advance_payment_due_method,
		[payment_due_date] = @advance_payment_due_date,
		[auto_remind_dealer_email] = @advance_auto_remind_dealer_email,
		[delaer_remind_period] = @advance_delaer_remind_period,
		[due_auto_remind_email] = @advance_due_auto_remind_email,
		[due_auto_remind_period] = @advance_due_auto_remind_period,
		[loan_id] = @loan_id WHERE [loan_id] = @loan_id

		Set @ReturnValue = 1
	END
	ELSE
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		INSERT INTO [dbo].[advanceFee](
		[advance_fee_amount],
		[advance_fee_calculate_type],
		[receipt],
		[payment_due_method],
		[payment_due_date],
		[auto_remind_dealer_email],
		[delaer_remind_period],
		[due_auto_remind_email],
		[due_auto_remind_period],
		[loan_id])
		VALUES(
		@advance_fee_amount,
		@advance_fee_calculate_type,
		@advance_receipt,
		@advance_payment_due_method,
		@advance_payment_due_date,
		@advance_auto_remind_dealer_email,
		@advance_delaer_remind_period,
		@advance_due_auto_remind_email,
		@advance_due_auto_remind_period,
		@loan_id)

		Set @ReturnValue = 1

	END
END
IF @advance_fee_amount <= 0
BEGIN
	IF (EXISTS(SELECT * FROM [dbo].[advanceFee] WHERE [loan_id] = @loan_id))
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		DELETE FROM [dbo].[advanceFee]  WHERE [loan_id] = @loan_id;

		Set @ReturnValue = 1
	END
END
IF @monthly_loan_fee_amount > 0
BEGIN
	IF (EXISTS(SELECT * FROM [dbo].[monthlyLoanFee] WHERE [loan_id] = @loan_id))
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		UPDATE [dbo].[monthlyLoanFee] SET
		[monthly_loan_fee_amount] = @monthly_loan_fee_amount,
		[receipt] = @monthly_loan_receipt,
		[payment_due_method] = @monthly_loan_payment_due_method,
		[payment_due_date] = @monthly_loan_payment_due_date,
		[auto_remind_dealer_email] = @monthly_loan_auto_remind_dealer_email,
		[delaer_remind_period] = @monthly_loan_delaer_remind_period,
		[due_auto_remind_email] = @monthly_loan_due_auto_remind_email,
		[due_auto_remind_period] = @monthly_loan_due_auto_remind_period,
		[loan_id] = @loan_id WHERE [loan_id] = @loan_id

		Set @ReturnValue = 1
	END
	ELSE
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		INSERT INTO [dbo].[monthlyLoanFee](
		[monthly_loan_fee_amount],
		[receipt],
		[payment_due_method],
		[payment_due_date],
		[auto_remind_dealer_email],
		[delaer_remind_period],
		[due_auto_remind_email],
		[due_auto_remind_period],
		[loan_id])
		VALUES(
		@monthly_loan_fee_amount,
		@monthly_loan_receipt,
		@monthly_loan_payment_due_method,
		@monthly_loan_payment_due_date,
		@monthly_loan_auto_remind_dealer_email,
		@monthly_loan_delaer_remind_period,
		@monthly_loan_due_auto_remind_email,
		@monthly_loan_due_auto_remind_period,
		@loan_id)

		Set @ReturnValue = 1
	END
END
IF @monthly_loan_fee_amount <= 0
BEGIN
	IF (EXISTS(SELECT * FROM [dbo].[monthlyLoanFee] WHERE [loan_id] = @loan_id))
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		DELETE FROM [dbo].[monthlyLoanFee] WHERE [loan_id] = @loan_id;

		Set @ReturnValue = 1
	END
END
IF @lot_inspection_amount > 0
BEGIN
	IF (EXISTS(SELECT * FROM [dbo].[lotInspectionFee] WHERE [loan_id] = @loan_id))
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		UPDATE [dbo].[lotInspectionFee] SET 
	
		[lot_inspection_amount] = @lot_inspection_amount,
		[receipt] = @lot_inspection_receipt,
		[payment_due_method] = @lot_payment_due_method,
		[payment_due_date] = @lot_payment_due_date,
		[auto_remind_dealer_email] = @lot_inspection_auto_remind_dealer_email,
		[delaer_remind_period] = @lot_inspection_delaer_remind_period,
		[due_auto_remind_email] = @lot_inspection_due_auto_remind_email,
		[due_auto_remind_period] = @lot_inspection_due_auto_remind_period,
		[loan_id] = @loan_id WHERE [loan_id] = @loan_id

		Set @ReturnValue = 1
	END
	ELSE
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		INSERT INTO [dbo].[lotInspectionFee](
		[lot_inspection_amount],
		[receipt],
		[payment_due_method],
		[payment_due_date],
		[auto_remind_dealer_email],
		[delaer_remind_period],
		[due_auto_remind_email],
		[due_auto_remind_period],
		[loan_id])
		VALUES(
		@lot_inspection_amount,
		@lot_inspection_receipt,
		@lot_payment_due_method,
		@lot_payment_due_date,
		@lot_inspection_auto_remind_dealer_email,
		@lot_inspection_delaer_remind_period,
		@lot_inspection_due_auto_remind_email,
		@lot_inspection_due_auto_remind_period,
		@loan_id)

		Set @ReturnValue = 1
	END
END
IF @lot_inspection_amount <= 0
BEGIN
	IF (EXISTS(SELECT * FROM [dbo].[lotInspectionFee] WHERE [loan_id] = @loan_id))
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		DELETE FROM [dbo].[lotInspectionFee] WHERE [loan_id] = @loan_id;
		Set @ReturnValue = 1
	END
END
Set @ReturnValue = 1
return @ReturnValue
GO
/****** Object:  StoredProcedure [dbo].[spInsertInterestDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 6/2/2016
-- Description:	insert interest details
-- =============================================
CREATE PROCEDURE [dbo].[spInsertInterestDetails] 
	-- Add the parameters for the stored procedure here
	@interest_rate decimal(5, 3) = 0.000, 
	@paid_date varchar(50) = null,
	@payment_period varchar(50) = null,
	@auto_remind_email varchar(100) = null,
	@auto_remind_period int = 0,
	@loan_id int = 0,
	@accrual_method_id int = 0,
	@transaction_type varchar(50) = null,
	@return int=-1 output
AS
--DECLARE @paid_date1 varchar
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF(@paid_date = 'payoff')
	BEGIN
	SELECT @paid_date = pay_off_period FROM [dbo].[loan] WHERE loan_id = @loan_id
	END

	--ELSE
	--BEGIN
	--@paid_date1 = @paid_date
	--END

	IF NOT EXISTS(SELECT interest_id FROM [dbo].[interest] WHERE loan_id = @loan_id)
	BEGIN
	INSERT INTO [dbo].[interest](
	[interest_rate],
	[paid_date],
	[payment_period],
	[auto_remind_email],
	[auto_remind_period],
	[loan_id],
	[accrual_method_id])
	VALUES(
	@interest_rate,
	@paid_date,
	@payment_period,
	@auto_remind_email,
	@auto_remind_period,
	@loan_id,
	@accrual_method_id)

	UPDATE [dbo].[loan]
	SET
	[is_interest_calculate] = 1
	WHERE loan_id = @loan_id

	SELECT @return = interest_id FROM [dbo].[interest] WHERE loan_id = @loan_id
	END

	ELSE IF EXISTS(SELECT interest_id FROM [dbo].[interest] WHERE loan_id = @loan_id)
	BEGIN
	UPDATE [dbo].[interest]
	SET 
	[interest_rate] = @interest_rate,
	[paid_date] = @paid_date,
	[payment_period] = @payment_period,
	[auto_remind_email] = @auto_remind_email,
	[auto_remind_period] = @auto_remind_period,
	[loan_id] = @loan_id,
	[accrual_method_id] = @accrual_method_id

	WHERE loan_id = @loan_id

	SET @return = 0
	END
	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertLoanStepOne]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Irfan
-- Create date: 2/11/2016
-- Description:	insert Step one Detail
-- =============================================
CREATE PROCEDURE [dbo].[spInsertLoanStepOne] 
	-- Add the parameters for the stored procedure here
	
	@loan_id int = 0,
	@advance decimal = 0.0, 
	@auto_remind_email varchar(100) = null,
	@auto_remind_period int = 0,
	@default_unit_type int = 0,
	@is_edit_allowable bit = 0,
	@loan_status bit = 0,
	@is_delete bit = 0,
	@is_interest_calculate bit = 0,
	@loan_amount decimal(18, 2) = 0.0, 
	@pay_off_type char,
	@loan_number varchar(100) = null,
	@maturity_date datetime ,
	@non_reg_branch_id int = 0,
	@payment_method varchar(50) = null,
	@pay_off_period int = 0,
	@loan_code varchar(100) = null,
	@start_date datetime ,
	@created_date datetime ,
    @return int =0 output

AS
--DECLARE @company_id int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF (@loan_id <= 0)
		BEGIN 
	INSERT INTO loan([loan_status],[is_delete],[advance], [auto_remind_email],[auto_remind_period],[default_unit_type],
	[is_edit_allowable],[is_interest_calculate],[loan_amount],[maturity_date],[non_reg_branch_id],[payment_method],[pay_off_period],[loan_code],[start_date],[created_date],[loan_number],[pay_off_type])
	VALUES(@loan_status,@is_delete,@advance, @auto_remind_email,@auto_remind_period,@default_unit_type,
	@is_edit_allowable,@is_interest_calculate,@loan_amount,@maturity_date,@non_reg_branch_id,@payment_method,@pay_off_period,@loan_code,@start_date,@created_date,@loan_number,@pay_off_type)
	;

	Delete from loanUnitType Where loan_id=@loan_id;
	SELECT @return = IDENT_CURRENT('loan');
	
	return @return;
	END

	ELSE

	BEGIN

		UPDATE loan 
		SET
		[advance] = @advance,
		[auto_remind_email] = @auto_remind_email,
		[auto_remind_period] = @auto_remind_period,
		[default_unit_type] = @default_unit_type,
		[is_edit_allowable] = @is_edit_allowable,
		[is_interest_calculate] = @is_interest_calculate,
		[loan_amount] = @loan_amount,
		[maturity_date] = @maturity_date,
		[non_reg_branch_id] = @non_reg_branch_id,
		[payment_method] = @payment_method,
		[pay_off_period] = @pay_off_period,
		[loan_code] = @loan_code,
		[start_date] = @start_date,
		[loan_number] = @loan_number,
		[pay_off_type] = @pay_off_type


		WHERE loan_id = @loan_id;

		Delete from loanUnitType Where loan_id=@loan_id;

		IF(@is_interest_calculate = 0)
		BEGIN
		DELETE FROM interest WHERE loan_id = @loan_id
		END
		return @loan_id;
	END

END


GO
/****** Object:  StoredProcedure [dbo].[spInsertLoanUniType]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Irfan
-- Create date: 2/11/2016
-- Description:	insert loan unit types
-- =============================================
CREATE PROCEDURE [dbo].[spInsertLoanUniType] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0, 
	
	@unit_type_id int =0,
	@return int=0 output
AS
--DECLARE @company_id int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT loan_id FROM [loanUnitType] WHERE loan_id = @loan_id and unit_type_id = @unit_type_id)
		BEGIN
			INSERT INTO [loanUnitType]([loan_id],
			
			[unit_type_id])

			VALUES(@loan_id,
			@unit_type_id
			
			)
	set @return = 1
		END


RETURN @return
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertNonRegisteredBranch]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Irfan
-- Create date: 1/27/2016
-- Description:	insert non Registered branch details for a non Register company
-- =============================================
CREATE PROCEDURE [dbo].[spInsertNonRegisteredBranch] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0, 
	@branch_code varchar(50) = null,
	@branch_name varchar(50) = null,
	@branch_address_1 varchar(50) = null,
	@branch_address_2 varchar(50) = null,
	@state_id int = 0,
	@city varchar(50) = null,
	@zip varchar(50) = null,
	@email varchar(100) = null,
	@phone_num_1 varchar(50) = null,
	@phone_num_2 varchar(50) = null,
	@phone_num_3 varchar(50) = null,
	@fax varchar(50) = null,
	@company_id int =0,
	@created_date datetime = null,
	@branch_id int = 0,
	@return int=-1 output
AS
--DECLARE @company_id int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT branch_id FROM [nonRegisteredBranch] WHERE branch_code = @branch_code)
	BEGIN
		INSERT INTO [nonRegisteredBranch]([branch_code],
		[branch_name],
		[branch_address_1],
		[branch_address_2],
		[state_id],
		[city],
		[zip],
		[email],
		[phone_num_1],
		[phone_num_2],
		[phone_num_3],
		[fax],
		[created_by],
		[created_date],
		[company_id],
		[branch_id])

		VALUES(@branch_code,
		@branch_name,
		@branch_address_1,
		@branch_address_2,
		@state_id,
		@city,
		@zip,
		@email,
		@phone_num_1,
		@phone_num_2,
		@phone_num_3,
		@fax,
		@user_id,
		@created_date,
		@company_id,
		@branch_id)
	
	SELECT @return = non_reg_branch_id FROM [nonRegisteredBranch] WHERE branch_code = @branch_code
	
	
	END
	ELSE
	BEGIN
		UPDATE [nonRegisteredBranch]
		SET [branch_name] = @branch_name,
		[branch_address_1] = @branch_address_1,
		[branch_address_2] = @branch_address_2,
		[state_id] = @state_id,
		[city] = @city,
		[zip] = @zip,
		[email] = @email,
		[phone_num_1] = @phone_num_1,
		[phone_num_2] = @phone_num_2,
		[phone_num_3] = @phone_num_3,
		[fax] = @fax,		
		[company_id] = @company_id,
		[branch_id] = @branch_id
		WHERE branch_code = @branch_code
		SELECT @return = branch_id FROM [nonRegisteredBranch] WHERE branch_code = @branch_code
	
	END

RETURN @return
END




GO
/****** Object:  StoredProcedure [dbo].[spInsertNonRegisteredCompany]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Irfan
-- Create date: 1/27/2016
-- Description: insert new nonregistered company
-- =============================================
CREATE PROCEDURE [dbo].[spInsertNonRegisteredCompany]
	@company_name VARCHAR(50) = null,
	@company_code VARCHAR(50)= null,
	@company_address_1 VARCHAR(50)= null,
	@company_address_2 VARCHAR(50)= null,
	@stateId INT,
	@city VARCHAR(50)= null,
	@zip VARCHAR(50)= null,
	@email VARCHAR(50)= null,
	@phone_num_1 VARCHAR(50)= null,
	@phone_num_2 VARCHAR(50)= null,
	@phone_num_3 VARCHAR(50)= null,
	@fax VARCHAR(50)= null,
	@website_url VARCHAR(50)= null,
	@created_by INT,
	@created_date DateTime,
	@company_type INT,
	@reg_company_id INT
	
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM nonRegisteredCompany WHERE company_code = @company_code)
	BEGIN
		INSERT INTO nonRegisteredCompany(company_name, company_code, company_address_1, company_address_2, stateId, city, zip, email, phone_num_1, 
			phone_num_2, phone_num_3, fax, website_url, created_by, created_date, company_type, reg_company_id)
		VALUES(@company_name, @company_code, @company_address_1, @company_address_2, @stateId, @city, @zip, @email, @phone_num_1, 
			@phone_num_2, @phone_num_3, @fax, @website_url, @created_by, @created_date, @company_type, @reg_company_id)
	END
	ELSE
	BEGIN
		UPDATE nonRegisteredCompany
		SET company_name = @company_name,
			company_address_1 = @company_address_1, 
			company_address_2 = @company_address_2, 
			stateId = @stateId, 
			city = @city, 
			zip = @zip, 
			email = @email, 
			phone_num_1 = @phone_num_1, 
			phone_num_2 = @phone_num_2, 
			phone_num_3 = @phone_num_3, 
			fax = @fax, 
			website_url = @website_url
		WHERE company_code = @company_code
	END	
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertTitleDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 6/2/2016
-- Description:	insert interest details
-- =============================================
CREATE PROCEDURE [dbo].[spInsertTitleDetails] 
	-- Add the parameters for the stored procedure here
	@is_title_tracked bit = 0, 
	@title_accept_method varchar(50) = null,
	@title_received_time_period varchar(50) = null,
	@auto_remind_email varchar(100) = null,
	@is_receipt_required bit = 0,
	@receipt_required_method varchar(50) = null,
	@loan_id int = 0,
	
	@return int=-1 output
AS
--DECLARE @paid_date1 varchar
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF NOT EXISTS(SELECT title_id FROM [dbo].[title] WHERE loan_id = @loan_id)
	BEGIN
	IF((@is_title_tracked = 1)OR (@is_receipt_required =1))
	BEGIN
	INSERT INTO [dbo].[title](
	[is_title_tracked],
	[title_accept_method],
	[title_received_time_period],
	[auto_remind_email],
	[is_receipt_required],
	[receipt_required_method],
	[loan_id])
	VALUES(
	@is_title_tracked,
	@title_accept_method,
	@title_received_time_period,
	@auto_remind_email,
	@is_receipt_required,
	@receipt_required_method,
	@loan_id)

	SELECT @return = title_id FROM [dbo].[title] WHERE loan_id = @loan_id
	END
	ELSE
	BEGIN
	SET @return = 0
	END
	END

	ELSE IF EXISTS(SELECT title_id FROM [dbo].[title] WHERE loan_id = @loan_id)
	BEGIN
	IF((@is_title_tracked = 1)OR (@is_receipt_required =1))
	BEGIN
	UPDATE [dbo].[title]
	SET 
	[is_title_tracked] = @is_title_tracked,
	[title_accept_method] = @title_accept_method,
	[title_received_time_period] = @title_received_time_period,
	[auto_remind_email] = @auto_remind_email,
	[is_receipt_required] = @is_receipt_required,
	[receipt_required_method] = @receipt_required_method,
	[loan_id] = @loan_id

	WHERE loan_id = @loan_id

	SELECT @return = title_id FROM [dbo].[title] WHERE loan_id = @loan_id
	END
	ELSE
	BEGIN
	DELETE FROM [dbo].[title] WHERE loan_id = @loan_id
	SET @return = 0
	END
	END
	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertTitleDocumentDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka
-- Create date: 02/26/2016
-- Description: Insert title document details
-- =============================================
CREATE PROCEDURE [dbo].[spInsertTitleDocumentDetails]
	@Input XML,
	@unit_id VARCHAR
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT * FROM uploadTitle WHERE unit_id = @unit_id)
	BEGIN
		INSERT INTO uploadTitle(file_path, unit_id, original_file_name)
		SELECT
			XCol.value('(FilePath)[1]', 'varchar(100)'),
			XCol.value('(UnitId)[1]', 'varchar(50)'),
			XCol.value('(OriginalFileName)[1]', 'varchar(100)')
		FROM
			@Input.nodes('/Titles/Title') AS XTbl(XCol)
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertUnitDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 2/19/2016
-- Edited by:	Kanishka
-- Edite date:	02/25/2016
-- Edited by:	Piyumi
-- Edite date:	03/08/2016
-- Edited by:	Piyumi
-- Edite date:	03/16/2016
-- Description:	Insert unit details
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUnitDetails] 
	-- Add the parameters for the stored procedure here
	@loan_id int = 0, 
	@user_id int = 0,
	@unit_id varchar(20) = null,
	@created_date datetime = null,
	@unit_type_id int =0,
	@identification_number varchar(20) = null,
	@year int =0,
	@make varchar(20) = null,
	@model varchar(50) = null,
	@color varchar(20) = null,
	@trim varchar(50) = null,
	@miles decimal(13, 3) = null,--0.00,
	@new_or_used bit = 0,
	@length decimal(10, 2) =null,--0.00,
	@hitch_style varchar(20) = null,
	@speed decimal(7, 2) = null,--0.00,
	@trailer_id varchar(50) = null,
	@engine_serial varchar(50) = null,
	@cost decimal(12, 2) = 0.00,
	@advance_amount decimal(12, 2) = 0.00,
	@title_status bit = 0,
	@note varchar(250) = null,
	@advance_date datetime = null,
	@add_or_advance bit = 0,
	@is_advanced bit = 0,
	@is_approved bit =0,
	@status varchar(20) = null,
	@unit_status tinyint = 0,

	@return int = 0 output
	 

AS

DECLARE @used_amount DECIMAL(18, 2), @pending_amount DECIMAL(18, 2)

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT unit_id FROM [dbo].[unit] WHERE unit_id = @unit_id)
	BEGIN
		INSERT INTO [dbo].[unit]
           ([unit_id]
           ,[identification_number]
           ,[year]
           ,[make]
           ,[model]
           ,[trim]
           ,[miles]
           ,[color]
           ,[new_or_used]
           ,[length]
           ,[hitch_style]
           ,[speed]
           ,[trailer_id]
           ,[engine_serial]
           ,[cost]
           ,[advance_amount]
           ,[title_status]
           ,[note]
           ,[advance_date]
           ,[add_or_advance]
           ,[is_advanced]
           ,[is_approved]
           ,[status]
		   ,[unit_status]
           ,[created_date]
           ,[created_by]
           ,[loan_id]
           ,[unit_type_id])

		   VALUES(
		   @unit_id,
		   @identification_number,
		   @year,
		   @make,
		   @model,
		   @trim,
		   @miles,
		   @color,
		   @new_or_used,
		   @length,
		   @hitch_style,
		   @speed,
		   @trailer_id,
		   @engine_serial,
		   @cost,
		   @advance_amount,
		   @title_status,
		   @note,
		   @advance_date,
		   @add_or_advance,
		   @is_advanced,
		   @is_approved,
		   @status,
		   @unit_status,
		   @created_date,
		   @user_id,
		   @loan_id,
		   @unit_type_id)

		   --Insert this unit to justAddedUnit table
		   
			

			IF (@unit_type_id = 5 OR @unit_type_id = 8)
			begin
			seT @model = @make
			end

			INSERT INTO justAddedUnit VALUES(@user_id, @model, @advance_amount, @is_advanced, @loan_id,@unit_id)

			--Insert or Update loan details
			IF NOT EXISTS(SELECT loan_id FROM loanDetails WHERE loan_id = @loan_id)			
			BEGIN
				IF(@is_advanced = 1)
				BEGIN
					INSERT loanDetails VALUES(@advance_amount, 0, @loan_id)
				END
				ELSE
				BEGIN
					INSERT loanDetails VALUES(0, @advance_amount, @loan_id)
				END
			
			END
			ELSE
			BEGIN 
				SELECT @used_amount = convert(decimal(12, 2), used_amount),@pending_amount=pending_amount
				FROM [dbo].[loanDetails] WHERE loan_id = @loan_id
				
				IF(@is_advanced = 1)
				BEGIN
					UPDATE loanDetails 
					SET used_amount = convert(decimal(18, 2), @used_amount + @advance_amount)			
					WHERE loan_id = @loan_id
				END
				ELSE
				BEGIN
					UPDATE loanDetails 
					SET pending_amount = @pending_amount + @advance_amount
					WHERE loan_id = @loan_id
				END				
			END

		   SET @return = 1 
		
	END
	ELSE
	BEGIN
		SET @return = 0 
	END
	
	RETURN @return	
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertUser]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/16/2016
-- Description: insert new user
-- =============================================

CREATE PROCEDURE [dbo].[spInsertUser]
	@user_id INT,
	@user_name VARCHAR(50),
	@password VARCHAR(50),
	@first_name VARCHAR(50),
	@last_name VARCHAR(50),
	@email VARCHAR(100),
	@phone_no VARCHAR(15) = null,
	@status BIT,
	@is_delete BIT,
	@created_by INT,
	@create_Date DateTime,
	@branch_id INT,
	@role_id INT,
	@Company_id INT
AS
BEGIN
	IF NOT EXISTS(SELECT user_id FROM [user] WHERE [user_name] = @user_name)
	BEGIN
		DECLARE @NewUserId int
		DECLARE @tableUser table (usid int)

		INSERT INTO [user]([user_name], [password], first_name, last_name, email, phone_no, [status], is_delete, created_by, created_date, branch_id, role_id, company_id)

		OUTPUT Inserted.user_id into @tableUser

		VALUES(@user_name, @password, @first_name, @last_name, @email, @phone_no, @status, @is_delete, @created_by, @create_Date, @branch_id, @role_id, @Company_id)

		SELECT @NewUserId = usid from @tableUser

		IF @created_by = 0
		BEGIN 
			UPDATE [user]
			SET created_by = @NewUserId
			WHERE [user_name] = @user_name
		END
	END
	ELSE
	BEGIN
		UPDATE [user]
		SET [user_name] = @user_name,
		first_name = @first_name,
		last_name = @last_name,
		email = @email,
		phone_no = @phone_no,
		branch_id = @branch_id,
		role_id = @role_id
		WHERE [user_name] = @user_name
	END
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertUserActivation]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 2016/01/21
-- Description:	Insert new member for activation
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUserActivation]
	-- Add the parameters for the stored procedure here
	@user_id int,
	@activation_code uniqueidentifier
AS
DECLARE @return_val INT
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO userActivation([user_id], activation_code, is_active)
	VALUES(@user_id, @activation_code, 0)
	SET @return_val = 1
RETURN @return_val
END



GO
/****** Object:  StoredProcedure [dbo].[spInsertUserLogin]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertUserLogin]
	@user_name VARCHAR(50),
	@password VARCHAR(50)
AS
BEGIN
	INSERT INTO userLogin([user_name], [password])
	VALUES(@user_name, @password)
END


GO
/****** Object:  StoredProcedure [dbo].[spIsUniqueCompanyName]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/17/2016
-- Description: check company name is unique
-- =============================================
CREATE PROCEDURE [dbo].[spIsUniqueCompanyName]
	@company_name VARCHAR(50)
AS
BEGIN
	SELECT company_id FROM company
	WHERE company_name = @company_name
END


GO
/****** Object:  StoredProcedure [dbo].[spIsUniqueEmail]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/24/2016
-- Description: Check user name is unique
-- =============================================

CREATE PROCEDURE [dbo].[spIsUniqueEmail]
	@email VARCHAR(50)
AS
BEGIN
	SELECT [email] FROM [user]
	WHERE [email] = @email
END


GO
/****** Object:  StoredProcedure [dbo].[spIsUniqueLoanNumberForBranch]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Irfan
-- Create date: 8/2/2016
-- Description: Check loan number is unique for one branch
-- =============================================

CREATE PROCEDURE [dbo].[spIsUniqueLoanNumberForBranch]
    @loan_id int = 0,
	@loan_number VARCHAR(50),
	@branch_id int
AS
BEGIN
	IF(@loan_id <= 0)
	BEGIN
	SELECT [loan_number] FROM [loan],[nonRegisteredBranch]
	WHERE [loan].non_reg_branch_id = [nonRegisteredBranch].non_reg_branch_id AND [nonRegisteredBranch].branch_id = @branch_id AND [loan].loan_number = @loan_number
	END
	ELSE
	BEGIN
	SELECT [loan_number] FROM [loan],[nonRegisteredBranch]
	WHERE [loan].non_reg_branch_id = [nonRegisteredBranch].non_reg_branch_id AND [nonRegisteredBranch].branch_id = @branch_id AND [loan].loan_number = @loan_number
	AND [loan].loan_id <> @loan_id
	

	END
END


GO
/****** Object:  StoredProcedure [dbo].[spIsUniqueUserName]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/16/2016
-- Description: Check user name is unique
-- =============================================

CREATE PROCEDURE [dbo].[spIsUniqueUserName]
	@user_name VARCHAR(50)
AS
BEGIN
	SELECT [user_name] FROM [user]
	WHERE [user_name] = @user_name
END


GO
/****** Object:  StoredProcedure [dbo].[spRetrieveCurtailmentByLoanId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nadeeka
-- Create date: 2/09/2016
-- Description:	retrieve curtailment list using loan_id
-- =============================================
CREATE PROCEDURE [dbo].[spRetrieveCurtailmentByLoanId] 
	-- Add the parameters for the stored procedure here
	(@loan_id int
	)
AS
DECLARE @sql nvarchar(4000), @paramlist nvarchar(500)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT loan_id FROM [dbo].[curtailmentSchedule] WHERE loan_id = @loan_id)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--SELECT @sql ='SELECT TOP 1 * FROM [dbo].[user], [dbo].[userLogin] WHERE [dbo].[user].user_id=[dbo].[userLogin].user_id AND [dbo].[userLogin].user_id = @xuser_id'
	SELECT @sql = 'SELECT * FROM [dbo].[curtailmentSchedule] WHERE loan_id = @xloan_id'

END

SELECT @paramlist =  '@xloan_id int'
print @sql
EXEC sp_executesql @sql  ,@paramlist, @loan_id
END


--///////////////////////////////////////////////////////


-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[spRetrieveUserByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/13/2016
-- Description:	retrieve userDetails using user Id
-- =============================================
CREATE PROCEDURE [dbo].[spRetrieveUserByUserId] 
	-- Add the parameters for the stored procedure here
	(@user_id int
	)
AS
DECLARE @sql nvarchar(4000), @paramlist nvarchar(500)
BEGIN
IF EXISTS(SELECT user_id FROM [dbo].[user] WHERE user_id = @user_id)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--SELECT @sql ='SELECT TOP 1 * FROM [dbo].[user], [dbo].[userLogin] WHERE [dbo].[user].user_id=[dbo].[userLogin].user_id AND [dbo].[userLogin].user_id = @xuser_id'
	SELECT @sql = 'SELECT TOP 1 * FROM [dbo].[viewUserDetails] WHERE user_id = @xuser_id'

END

SELECT @paramlist =  '@xuser_id int'
print @sql
EXEC sp_executesql @sql  ,@paramlist, @user_id
END


GO
/****** Object:  StoredProcedure [dbo].[spRetriveUnitPayoff]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Author,,Name>
-- Create date: <Create Date,,>
-- Description: <Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spRetriveUnitPayoff] 
 -- Add the parameters for the stored procedure here
 @loan_id INT,
 @return INT = 0 OUT 
AS

BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 IF EXISTS(SELECT * FROM curtailmentSchedule WHERE loan_id = @loan_id)
 BEGIN
	--SELECT C.unit_id AS UnitId, 
	-- (SELECT SUM(curt_amount) FROM curtailmentSchedule where C.unit_id = curtailmentSchedule.unit_id) AS TotalAmout, 

	-- (SELECT SUM(curt_partial_amount) FROM partialCurtailment WHERE C.unit_id = partialCurtailment.unit_id) AS PaidAmout,

	-- CASE WHEN (SELECT SUM(curt_partial_amount) FROM partialCurtailment WHERE C.unit_id = partialCurtailment.unit_id) >0
	--  THEN ((SELECT SUM(curt_amount) FROM curtailmentSchedule where C.unit_id = curtailmentSchedule.unit_id) - (SELECT SUM(curt_partial_amount) FROM partialCurtailment WHERE C.unit_id = partialCurtailment.unit_id))
	--  ELSE (SELECT SUM(curt_amount) FROM curtailmentSchedule where C.unit_id = curtailmentSchedule.unit_id) END AS Balance, 

	-- U.identification_number, U.year, U.make, U.model, U.created_date, U.cost FROM curtailmentSchedule C
	--LEFT JOIN partialCurtailment PC ON C.unit_id = PC.unit_id
	--INNER JOIN unit AS U ON C.unit_id = U.unit_id
	--WHERE C.loan_id = @loan_id AND U.unit_status = 1 GROUP BY C.unit_id, PC.unit_id, U.identification_number, U.year, U.make, U.model, U.created_date, U.cost

	DECLARE @Temp1 TABLE([uid] VARCHAR(50), curtNumber INT, partAmount DECIMAL(18, 2), curtStatus INT)

	INSERT INTO @Temp1([uid], curtNumber, partAmount, curtStatus)
	SELECT PC.unit_id, PC.curt_number, SUM(PC.curt_partial_amount), CS.curt_status
	FROM partialCurtailment PC
	INNER JOIN curtailmentSchedule CS ON PC.unit_id = CS.unit_id AND PC.curt_number = CS.curt_number
	GROUP BY PC.unit_id, PC.curt_number, CS.curt_status

	SELECT CS.unit_id AS UnitId, U.identification_number, U.year, U.make, U.model, U.created_date, U.cost, 
	
	(U.advance_amount - SUM(CASE WHEN CS.curt_status = 2 THEN T.partAmount
		 WHEN CS.curt_status = 1 THEN CS.curt_amount
		 ELSE 0
	END)) AS Balance
	FROM curtailmentSchedule AS CS
	LEFT JOIN @Temp1 AS T ON CS.unit_id = T.[uid] AND CS.curt_number = T.curtNumber
	LEFT JOIN unit AS U ON CS.unit_id = U.unit_id
	WHERE CS.loan_id = @loan_id
	GROUP BY CS.unit_id, U.identification_number, U.year, U.make, U.model, U.created_date, U.cost, U.advance_amount

	--SELECT * FROM @Temp1

  SET @return = 1
 END
 RETURN @return
END


GO
/****** Object:  StoredProcedure [dbo].[spSetupCompany]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/16/2016
-- Description: insert new company
-- =============================================

CREATE PROCEDURE [dbo].[spSetupCompany]
	@c_company_name VARCHAR(50),
	@c_company_code VARCHAR(50),
	@c_company_address_1 VARCHAR(50),
	@c_company_address_2 VARCHAR(50),
	@c_stateId INT,
	@c_city VARCHAR(50),
	@c_zip VARCHAR(50),
	@c_email VARCHAR(50),
	@c_phone_num_1 VARCHAR(50),
	@c_phone_num_2 VARCHAR(50),
	@c_phone_num_3 VARCHAR(50),
	@c_fax VARCHAR(50),
	@c_website_url VARCHAR(50),
	@c_created_by INT,
	@c_created_date DateTime,
	@c_company_type INT,
	@c_first_super_admin_id INT,
	@c_company_status BIT,

	@b_user_id int = 0, 
	@b_branch_code varchar(50) = null,
	@b_branch_name varchar(50) = null,
	@b_branch_address_1 varchar(50) = null,
	@b_branch_address_2 varchar(50) = null,
	@b_state_id INT = null,
	@b_city varchar(50) = null,
	@b_zip varchar(50) = null,
	@b_email varchar(100) = null,
	@b_phone_num_1 varchar(50) = null,
	@b_phone_num_2 varchar(50) = null,
	@b_phone_num_3 varchar(50) = null,
	@b_fax varchar(50) = null,
	@b_company_id int =0,
	@b_created_date datetime = null,

	@u_user_name VARCHAR(50),
	@u_password VARCHAR(50),
	@u_first_name VARCHAR(50),
	@u_last_name VARCHAR(50),
	@u_email VARCHAR(50),
	@u_phone_no VARCHAR(50),
	@u_status BIT,
	@u_is_delete BIT,
	@u_created_by INT,
	@u_create_Date DateTime,
	@u_branch_id INT,
	@u_role_id INT,
	@return int=-1 output
AS
BEGIN 
	BEGIN TRANSACTION
		BEGIN TRY
			DECLARE @CompanyId int
			DECLARE @tableCompany table (cid int)
			DECLARE @BranchId int
			DECLARE @tableBranch table (bid int)
			DECLARE @NewUserId int
			DECLARE @tableUser table (usid int)
	
			INSERT INTO company(company_name, company_code, company_address_1, company_address_2, 
				[stateId], city, zip, email, phone_num_1, phone_num_2, phone_num_3, fax, website_url, 
				created_by, created_date, company_type, first_super_admin_id, company_status)				

			OUTPUT Inserted.company_id into @tableCompany

			VALUES(@c_company_name, @c_company_code, @c_company_address_1, @c_company_address_2, 
				@c_stateId, @c_city, @c_zip, @c_email, @c_phone_num_1, @c_phone_num_2, @c_phone_num_3, @c_fax, @c_website_url,
				@c_created_by, @c_created_date, @c_company_type, @c_first_super_admin_id, @c_company_status)

			SELECT @CompanyId = cid from @tableCompany	

			INSERT INTO [branch]([branch_code], [branch_name], [branch_address_1], [branch_address_2], 
				[state_id], [city], [zip], [email], [phone_num_1], [phone_num_2], [phone_num_3], 
				[fax], [created_by], [created_date], [company_id])
			
			OUTPUT Inserted.branch_id into @tableBranch
			
			VALUES(@b_branch_code, @b_branch_name, @b_branch_address_1, @b_branch_address_2, 
				@b_state_id, @b_city, @b_zip, @b_email, @b_phone_num_1, @b_phone_num_2, @b_phone_num_3, 
				@b_fax, @b_user_id, @b_created_date, @CompanyId)
			
			SELECT @BranchId = bid from @tableBranch
			
			INSERT INTO [user]([user_name], [password], first_name, last_name, email, phone_no, [status], is_delete, created_by, created_date, branch_id, role_id)
			
			OUTPUT Inserted.[user_id] into @tableUser
			
			VALUES(@u_user_name, @u_password, @u_first_name, @u_last_name, @u_email, @u_phone_no, @u_status, @u_is_delete, @u_created_by, @u_create_Date, @BranchId, @u_role_id)
			
			SELECT @NewUserId = usid from @tableUser

			UPDATE [user] SET company_id = @CompanyId WHERE user_id = @NewUserId
			
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			SET @return = -1
			ROLLBACK TRANSACTION
		END CATCH
RETURN @return
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateBranchCompanyId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 1/18/2016
-- Description:	update company id and created by id in branch
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateBranchCompanyId]
	-- Add the parameters for the stored procedure here
	@company_id INT,
	@created_by INT,
	@branch_code VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE branch 
	SET company_id = @company_id,
	created_by = @created_by
	WHERE branch_code = @branch_code
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateBranchId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 1/27/2016
-- Description:	update branch id when branch is created at step 2
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateBranchId] 
	-- Add the parameters for the stored procedure here
	@user_id int = 0, 
	@branch_code varchar(50) = null,
	@return int = 0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT user_id FROM [user] WHERE user_id = @user_id)
	BEGIN
	UPDATE [user] SET [branch_id] = (SELECT [branch].[branch_id] FROM [branch] WHERE [branch].[branch_code] = @branch_code)
	WHERE [user].[user_id] = @user_id
	SET @return = 1
	END
	ELSE
	BEGIN
	SET @return = -1
	END
	RETURN @return
END



GO
/****** Object:  StoredProcedure [dbo].[spUpdateCompanySetupStep]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 3/3/2016
-- Description:	update company setup step numbers
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateCompanySetupStep] 
	-- Add the parameters for the stored procedure here
	@company_id int = 0, 
	@branch_id int = 0,
	@step_number int = 0,
	@return int =0 OUTPUT
AS
DECLARE @current_step int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT company_id FROM [dbo].[companySetupStep] WHERE company_id = @company_id )
	BEGIN

	IF(@step_number = 3)
	BEGIN
	IF EXISTS(SELECT branch_id FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id )
	BEGIN
	SELECT @current_step = step_number FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id

	IF(@step_number>@current_step) 
	BEGIN
	UPDATE [dbo].[companySetupStep]
	SET step_number = @step_number
	WHERE company_id = @company_id AND branch_id = @branch_id
	SET @return = 1
	END

	END

	ELSE IF EXISTS(SELECT company_id FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id IS NULL)
	BEGIN
	UPDATE [dbo].[companySetupStep]
	SET step_number = @step_number,
	branch_id = @branch_id
	WHERE company_id = @company_id
	SET @return = 1
	END

	ELSE IF(@branch_id>0)
	BEGIN
	INSERT INTO [dbo].[companySetupStep]
	VALUES (@company_id,@branch_id,@step_number)
	SET @return = 1
	END
	END

	ELSE IF(@step_number = 4)
	BEGIN
	IF(@branch_id>0)
	BEGIN
	IF EXISTS(SELECT branch_id FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id )
	BEGIN
	SELECT @current_step = step_number FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id

	IF(@step_number>@current_step) 
	BEGIN
	UPDATE [dbo].[companySetupStep]
	SET step_number = @step_number
	WHERE company_id = @company_id AND branch_id = @branch_id
	SET @return = 1
	END

	END
	END
	ELSE IF(@branch_id=0)
	BEGIN
	--SELECT DISTINCT TOP 1 @current_step = step_number FROM [dbo].[companySetupStep] WHERE company_id = @company_id ORDER BY step_number DESC
	
	UPDATE [dbo].[companySetupStep]
	SET step_number = @step_number
	WHERE company_id = @company_id AND step_number<4
	SET @return = 1
	

	
	
	END
	END

	ELSE IF(@step_number = 5)

	BEGIN
	--SELECT @current_step = step_number FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id

	--IF(@step_number>@current_step)
	--BEGIN 
	UPDATE [dbo].[companySetupStep]
	SET step_number = @step_number
	WHERE company_id = @company_id AND step_number <= 5
	SET @return = 1
	--END

	END


	END

	--if company not exists and step =2
	IF NOT EXISTS(SELECT company_id FROM [dbo].[companySetupStep] WHERE company_id = @company_id )
	BEGIN
	
	IF(@step_number = 2)
	BEGIN
	INSERT INTO [dbo].[companySetupStep]([company_id],[step_number])
	VALUES (@company_id,@step_number)
	SET @return = 1
	END
	END

    RETURN @return
	END
	

GO
/****** Object:  StoredProcedure [dbo].[spUpdateCompanySuperAdmin]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 1/18/2016
-- Description:	update first super admin of company
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateCompanySuperAdmin]
	-- Add the parameters for the stored procedure here
	@user_id INT,
	@company_name VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE company 
	SET first_super_admin_id = @user_id,
	created_by = @user_id
	WHERE company_name = @company_name
END



GO
/****** Object:  StoredProcedure [dbo].[spUpdateCurtailmentDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nadeeka
-- Create date: 2/09/2016
-- Description:	update curtailment details
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateCurtailmentDetails] 
	-- Add the parameters for the stored procedure here
	@curtailment_id int,
	@time_period nvarchar(50),
	@percentage float,
	
	@return int =0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT * FROM [dbo].[curtailment]
          WHERE curtailment_id = @curtailment_id ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[curtailment] SET [time_period] = @time_period, [percentage] = @percentage  WHERE curtailment_id= @curtailment_id;
	
	set @return = 1
END
ELSE
BEGIN
set @return = -1
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateCurtailmentSchedule]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Irfan
-- Create date: 03/17/2016
-- Description: Update curtailment schedule details
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateCurtailmentSchedule]
 @Input XML,
 @loan_id int = 0,
 @return varchar(50) = '' out
AS
BEGIN

	SET NOCOUNT ON;
	
	declare @unitId varchar(50);
			declare @curtNo int;
			declare @actCurtAmount decimal(18, 2);
			declare @curtAmount decimal(18, 2);
			declare @payDate datetime;
			declare @curtStatus int;
			declare @totalCurNo int;
	
	SELECT
   
				    @unitId =  XCol.value('(UnitId)[1]', 'varchar(50)'),
					@curtNo = XCol.value('(CurtNo)[1]', 'int'),
				    @curtAmount = XCol.value('(CurtAmount)[1]', 'decimal(18, 2)'),
				    @payDate = XCol.value('(PayDate)[1]', 'datetime')
				    FROM
					@Input.nodes('/Curtailments/CurtailmentShedule') AS XTbl(XCol);

    -- Insert statements for procedure here
	IF EXISTS(SELECT * FROM curtailmentSchedule WHERE loan_id = @loan_id and unit_id = @unitId)
	BEGIN
		  -- check input curtailment shedule count
		  declare @count int;
		  -- execute count
		  DECLARE @i int ;
		  SET @i = 1 
		  select @count = @Input.value('count(/Curtailments/CurtailmentShedule/id)', 'INT');
		  
		  -- list contain more than 1 curtailment shedule		 
		  WHILE (@i <= @count)
 
		  BEGIN

				SELECT
   
				    @unitId =  XCol.value('(UnitId)[1]', 'varchar(50)'),
					@curtNo = XCol.value('(CurtNo)[1]', 'int'),
				    @curtAmount = XCol.value('(CurtAmount)[1]', 'decimal(18, 2)'),
				    @payDate = XCol.value('(PayDate)[1]', 'datetime')
				    FROM
					@Input.nodes('/Curtailments/CurtailmentShedule') AS XTbl(XCol)Where XCol.value('(id)[1]', 'int') = @i;
			

				     SELECT  @actCurtAmount = curt_amount , @curtStatus = curt_status FROM curtailmentSchedule WHERE loan_id = @loan_id AND unit_id = @unitId AND curt_number = @curtNo;
					
					 -- if it is not completed and has partial amount
					   IF( @actCurtAmount > @curtAmount and @curtStatus = 0 )
					   BEGIN
						-- INSERT IN TO THE PARTIAL CURTAILMENT AND UPDATE THE SHEDULE TO PARTIAL COMPLETED
						INSERT INTO partialCurtailment VALUES (@loan_id , @unitId , @curtNo , @curtAmount,  @payDate ) ; 

						UPDATE curtailmentSchedule SET curt_status = 2 , paid_date = @payDate WHERE loan_id = @loan_id AND unit_id = @unitId AND curt_number = @curtNo;
						
						--Update loan details table
						UPDATE loanDetails SET used_amount = used_amount - @curtAmount WHERE loan_id = @loan_id		
					   END
										   
						-- if it is partial completed and partial amount
					   ELSE IF (@actCurtAmount > @curtAmount and @curtStatus = 2)
					    BEGIN
						-- take total amount of paid
						declare @totalPaid decimal(18, 2);

						SELECT @totalPaid = SUM(curt_partial_amount ) FROM  partialCurtailment WHERE loan_id = @loan_id and unit_id = @unitId  AND curt_number = @curtNo GROUP BY loan_id , unit_id , curt_number ;
								
								
								-- if total equal to actual amount
								IF( (@totalPaid + @curtAmount) = @actCurtAmount)
								 BEGIN
								-- take the last curtailment no
								SELECT @totalCurNo =count( curtailment_id ) FROM  curtailment WHERE loan_id =  @loan_id;

								-- INSERT IN TO THE PARTIAL CURTAILMENT AND UPDATE THE SHEDULE TO PARTIAL COMPLETED
								INSERT INTO partialCurtailment VALUES (@loan_id , @unitId , @curtNo , @curtAmount,  @payDate ) ; 

								--Update loan details table
								UPDATE loanDetails SET used_amount = used_amount - @curtAmount WHERE loan_id = @loan_id		

								UPDATE curtailmentSchedule SET curt_status = 1 , paid_date = @payDate WHERE loan_id = @loan_id AND unit_id = @unitId AND curt_number = @curtNo;
								-- IF IT IS LAST CUTAILMENT
								IF(@totalCurNo = @curtNo)
								BEGIN

								
								BEGIN TRANSACTION
								BEGIN TRY

								
							 -- check if there is no any uncompleted or partial for this curtailment
							
									IF NOT EXISTS(SELECT * FROM curtailmentSchedule WHERE loan_id = @loan_id and unit_id = @unitId  AND curt_number = @curtNo AND (curt_status = 0 OR curt_status = 2))
									BEGIN
												-- pay off
													-- fetch and create as comma seperated value of partal curtailmen table records
													 declare @paymentDescription varchar(100);
													 SELECT  @paymentDescription = STUFF(
													 (SELECT DISTINCT ';' + (CONVERT(varchar(100), curt_partial_amount) + ',' + CONVERT(varchar(100), paid_date))
													 FROM partialCurtailment
													 WHERE [unit_id] = PC.[unit_id] AND curt_number = PC.curt_number
													 FOR XML PATH (''))
													 , 1, 1, '') 
													 FROM partialCurtailment AS PC
													 GROUP BY unit_id, curt_number

												   INSERT INTO curtailmentBackup (loan_id, unit_id, curt_number, curt_amount, curt_due_date, pay_date, curt_payment_details) 
												   SELECT loan_id, unit_id, curt_number, curt_amount, curt_due_date, @payDate, @paymentDescription
												   FROM curtailmentSchedule AS CS
												   WHERE CS.unit_id = @unitId;

												   DELETE FROM curtailmentSchedule WHERE curtailmentSchedule.unit_id = @unitId;

												   UPDATE unit
												   SET unit_status = 2--,
												   --title_status = 4
												   WHERE unit_id = @unitId;

												   --SET @return = @unitId;
												   --SET @return = @return  +','+ @unitId;
												   declare @titleCount int;
												   select @titleCount = count(*) from title where loan_id=@loan_id

												   if @titleCount != 0 and @return is null										  
														SET @return = @unitId;
												   else if  @titleCount != 0 and @return is not null
														SET @return = @return  +','+ @unitId;
									END
									
								COMMIT TRANSACTION
								END TRY
								BEGIN CATCH
								
								ROLLBACK TRANSACTION
								END CATCH
								
								END



								END
								


								
								
								-- if total not equal to actual amount
								ELSE
								BEGIN
									INSERT INTO partialCurtailment VALUES (@loan_id , @unitId , @curtNo , @curtAmount,  @payDate ) ; 

									UPDATE curtailmentSchedule SET paid_date = @payDate WHERE loan_id = @loan_id AND unit_id = @unitId AND curt_number = @curtNo;
								
									--Update loan details table
									UPDATE loanDetails SET used_amount = used_amount - @curtAmount WHERE loan_id = @loan_id		
								END
						
						END
						
						-- if it is a full payment
						ELSE
						BEGIN
						    SELECT @totalCurNo =count( curtailment_id ) FROM  curtailment WHERE loan_id =  @loan_id;
							--SELECT @totalCurNo = MAX( curt_number ) FROM  curtailment WHERE loan_id = @loan_id AND unit_id = @unitId GROUP BY loan_id , unit_id  ;
								BEGIN TRANSACTION
								BEGIN TRY
							-- update curtailment table
								UPDATE curtailmentSchedule SET curt_status = 1 , paid_date = @payDate WHERE loan_id = @loan_id AND unit_id = @unitId AND curt_number = @curtNo;
							
								--Update loan details table
								UPDATE loanDetails SET used_amount = used_amount - @curtAmount WHERE loan_id = @loan_id		
							-- if it is last
							IF(@totalCurNo = @curtNo)
							BEGIN
								IF NOT EXISTS(SELECT * FROM curtailmentSchedule WHERE loan_id = @loan_id and unit_id = @unitId  AND curt_number = @curtNo AND (curt_status = 0 OR curt_status = 2))
									BEGIN
												-- pay off
												
													 SELECT  @paymentDescription = STUFF(
													 (SELECT DISTINCT ';' + (CONVERT(varchar(100), curt_partial_amount) + ',' + CONVERT(varchar(100), paid_date))
													 FROM partialCurtailment
													 WHERE [unit_id] = PC.[unit_id] AND curt_number = PC.curt_number
													 FOR XML PATH (''))
													 , 1, 1, '') 
													 FROM partialCurtailment AS PC
													 GROUP BY unit_id, curt_number
												   
												   INSERT INTO curtailmentBackup (loan_id, unit_id, curt_number, curt_amount, curt_due_date, pay_date, curt_payment_details) 
												   SELECT loan_id, unit_id, curt_number, curt_amount, curt_due_date, @payDate, @paymentDescription FROM curtailmentSchedule AS CS
												   WHERE CS.unit_id = @unitId;

												   DELETE FROM curtailmentSchedule WHERE curtailmentSchedule.unit_id = @unitId;

												   UPDATE unit
												   SET unit_status = 2--,
												   --title_status = 4
												   WHERE unit_id = @unitId;

												   -- declare @titleCount int;
												   select @titleCount = count(*) from title where loan_id=@loan_id

												   if @titleCount != 0 and @return is null										  
														SET @return = @unitId;
												   else if  @titleCount != 0 and @return is not null
														SET @return = @return  +','+ @unitId;
												   
									END
								
							END
							
								COMMIT TRANSACTION
								END TRY
								BEGIN CATCH
								
								ROLLBACK TRANSACTION
								END CATCH
							
							
							
						
						END

				SET @i = @i + 1
						
		END
		select @return;
		
		
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateLoanCurtailmentd]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka
-- Create date: 03/16/2017
-- Description:	update loan curtailment details by loan id 
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateLoanCurtailmentd] 
	-- Add the parameters for the stored procedure here
	@loan_id INT = 0,
	@loan_status BIT = 0,
	@curtailment_due_date VARCHAR(3) = NULL,
	@curtailment_auto_remind_email VARCHAR(100) = NULL,
	@curtailment_remind_period INT = 0,
	@curtailment_calculation_type CHAR,
	@return INT = 0 OUTPUT
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[loan]
          WHERE loan_id = @loan_id ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[loan] 
	SET  loan_status = @loan_status,
	curtailment_due_date = @curtailment_due_date,
	curtailment_auto_remind_email = @curtailment_auto_remind_email,
	curtailment_remind_period = @curtailment_remind_period,
	curtailment_calculation_type = @curtailment_calculation_type
	WHERE loan_id= @loan_id;
	
	set @return = 1
END
ELSE
BEGIN
set @return = -1
END
return @return
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateLoanSetupStep]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 3/4/2016
-- Description:	update loan setup step table
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateLoanSetupStep] 
	-- Add the parameters for the stored procedure here
	@company_id int = 0,
	@branch_id int = 0,
	@non_registered_branch_id int = 0, 
	@loan_id int = 0,
	@step_number int = 0,
	@return int = 0 OUTPUT
AS
DECLARE @current_step int,@step int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--check non_registered_branch_id is inserted in loanSetupStep table
	IF EXISTS(SELECT non_registered_branch_id FROM [dbo].[loanSetupStep] WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id)

	BEGIN
	--get current step of non_registered_branch
	SELECT @current_step = step_number FROM [dbo].[loanSetupStep] WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id
	--update step table if current_step<step_number
	IF((@step_number > @current_step) AND(@step_number = 2))
	BEGIN
	UPDATE [dbo].[loanSetupStep]
	SET loan_id= @loan_id,
	step_number = @step_number
	WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id
	SET @return = 1
	END
	--if step=3
	ELSE IF(@step_number = 3)
	BEGIN
	--check loan_id is existing or not
	--if exists that means completed step 2 and then come to step 3
	IF EXISTS(SELECT loan_id FROM [dbo].[loanSetupStep] WHERE non_registered_branch_id = @non_registered_branch_id AND loan_id = @loan_id AND company_id = @company_id AND branch_id = @branch_id)
	BEGIN
	--get current step which match loanid and nonregistered branchid
	SELECT @current_step = step_number FROM [dbo].[loanSetupStep] WHERE non_registered_branch_id = @non_registered_branch_id AND loan_id = @loan_id AND company_id = @company_id AND branch_id = @branch_id
	--if current step < step number
	IF(@step_number>@current_step)

	BEGIN
	UPDATE [dbo].[loanSetupStep]
	SET step_number = @step_number
	WHERE non_registered_branch_id = @non_registered_branch_id AND loan_id= @loan_id AND company_id = @company_id AND branch_id = @branch_id
	SET @return = 1
	END
	END
	
	--if not exists loan_id for particular non registered branch that means skip step 2
	ELSE
	BEGIN
	--get current step which match nonregistered branchid
	SELECT @current_step = step_number FROM [dbo].[loanSetupStep] WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id
	--if current step < step number
	IF(@step_number>@current_step)
	BEGIN
	UPDATE [dbo].[loanSetupStep]
	SET loan_id= @loan_id,
	step_number = @step_number
	WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id
	SET @return = 1
	END

	END
	END

	ELSE IF ((@step_number = 4) OR (@step_number = 5) )
	--related for step4 and step5
	BEGIN
	--get current step which match loanid and nonregistered branchid
	SELECT @current_step = step_number FROM [dbo].[loanSetupStep] WHERE non_registered_branch_id = @non_registered_branch_id AND loan_id = @loan_id AND company_id = @company_id AND branch_id = @branch_id
	--if current step < step number
	IF(@step_number>@current_step)
	BEGIN
	UPDATE [dbo].[loanSetupStep]
	SET step_number = @step_number
	WHERE non_registered_branch_id = @non_registered_branch_id AND loan_id= @loan_id AND company_id = @company_id AND branch_id = @branch_id
	SET @return = 1
	END

	END
	ELSE IF (@step_number = 6)
	BEGIN
	DELETE FROM [dbo].[loanSetupStep] WHERE non_registered_branch_id = @non_registered_branch_id AND loan_id= @loan_id AND company_id = @company_id AND branch_id = @branch_id
	SET @return = 1
	END

	
	END
	ELSE IF(@step_number = 1)
	BEGIN
	IF EXISTS(SELECT company_id FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id)
	BEGIN
	SELECT @step = step_number FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id 
	
	IF(@step = 5)
	BEGIN
	DELETE FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id

	UPDATE [dbo].[company]
	SET flag_value = 1
	WHERE company_id = @company_id

	INSERT INTO [dbo].[loanSetupStep]([company_id],[branch_id],[non_registered_branch_id],[step_number])
	VALUES (@company_id,@branch_id,@non_registered_branch_id,@step_number)
	SET @return = 1

	END

	END
	ELSE IF EXISTS(SELECT company_id FROM [dbo].[company] WHERE company_id = @company_id AND flag_value = 1)
	BEGIN
	INSERT INTO [dbo].[loanSetupStep]([company_id],[branch_id],[non_registered_branch_id],[step_number])
	VALUES (@company_id,@branch_id,@non_registered_branch_id,@step_number)
	SET @return = 1
	END

	
	--ELSE
	--BEGIN
	--SET @return = 0
	--END

	END

	ELSE IF((@step_number = 2) OR (@step_number = 3))
	BEGIN
	INSERT INTO [dbo].[loanSetupStep]([company_id],[branch_id],[non_registered_branch_id],[loan_id],[step_number])
	VALUES (@company_id,@branch_id,@non_registered_branch_id,@loan_id,@step_number)
	SET @return = 1
	END

	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spUpdateLoanSetupStep_asa]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 3/4/2016
-- Description:	update loan setup step table
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateLoanSetupStep_asa] 
	-- Add the parameters for the stored procedure here
	@company_id int = 0,
	@branch_id int = 0,
	@non_registered_branch_id int = 0, 
	@loan_id int = 0,
	@step_number int = 0,
	@return int = 0 OUTPUT

AS
DECLARE @current_step int,@step int
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--check non_registered_branch_id is inserted in loanSetupStep table
	IF EXISTS(SELECT non_registered_branch_id FROM [dbo].[loanSetupStep] 
	WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id)
	BEGIN
	
	iF not Exists (SELECT loan_id FROM [dbo].[loanSetupStep] 
	WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id)
	BEGIN
		UPDATE [dbo].[loanSetupStep]
	SET loan_id= @loan_id
	WHERE non_registered_branch_id = @non_registered_branch_id AND company_id = @company_id AND branch_id = @branch_id
	END
	
	SELECT @current_step = step_number FROM [dbo].[loanSetupStep] WHERE loan_id= @loan_id
	
	IF(@step_number > @current_step) 
	BEGIN
	UPDATE [dbo].[loanSetupStep]
	SET step_number = @step_number
	WHERE loan_id= @loan_id
	SET @return = 1
	END
	
	END
	
	ELSE IF(@loan_id>0)
	BEGIN
		INSERT INTO [dbo].[loanSetupStep]([company_id],[branch_id],[non_registered_branch_id],[loan_id],[step_number])
	VALUES (@company_id,@branch_id,@non_registered_branch_id,@loan_id,'2')
	SET @return = 1
	END
	
	ELSE
	BEGIN
	INSERT INTO [dbo].[loanSetupStep]([company_id],[branch_id],[non_registered_branch_id],[step_number])
	VALUES (@company_id,@branch_id,@non_registered_branch_id,@step_number)
	SET @return = 1
	END

	--END
	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spUpdateOrInsertToken]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/17/2015
-- Description:	update user details
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateOrInsertToken] 
	-- Add the parameters for the stored procedure here
	@user_id int,
	@token nvarchar(50),
	@expired_date datetime,
	@return int =0 out
	
	
AS
BEGIN
IF EXISTS(SELECT user_id FROM [dbo].[forgotPasswordToken] WHERE user_id = @user_id)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[forgotPasswordToken] SET token = @token, expired_date = @expired_date  WHERE user_id=@user_id
	set @return = 1
	END
	ELSE
	BEGIN
	INSERT INTO [dbo].[forgotPasswordToken]([user_id],[token],[expired_date]) VALUES (@user_id,@token,@expired_date)
	set @return = 2
	END
	return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdatePassword]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/17/2015
-- Description:	updating password using user id
-- =============================================
CREATE PROCEDURE [dbo].[spUpdatePassword] 
	-- Add the parameters for the stored procedure here
	@user_id int,
	@password nvarchar(50),
	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[user]
          WHERE user_id = @user_id ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[user] SET  password = @password WHERE user_id= @user_id;
	
	set @return = 1
END
ELSE
BEGIN
set @return = -1
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateProfileDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/16/2015
-- Description:	updating own user details
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateProfileDetails] 
	-- Add the parameters for the stored procedure here
	@user_id int,
	@user_name nvarchar(50),
	@first_name nvarchar(50),
	@last_name nvarchar(50),
	@email nvarchar(50),
	@phone_no nvarchar(50),
	@modified_date datetime,
	

	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[user]
          WHERE user_id = @user_id ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[user] SET user_name = @user_name, last_name = @last_name, first_name = @first_name, email = @email, phone_no = @phone_no, modified_date = @modified_date WHERE user_id= @user_id;
	
	set @return = 1
END
ELSE
BEGIN
set @return = -1
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateRightsStringByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		kasun
-- Create date: 1/17/2015
-- Description:	update rights Permision String by user id
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateRightsStringByUserId] 
	@editor_id int,
	@permission varchar(max),
	@modified_user int,
	@DateNow datetime,
	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[userPermission] where [user_id]=@editor_id ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[userPermission] SET right_id = @permission , modified_by = @modified_user, modify_date = @DateNow where [user_id]=@editor_id ;
	set @return = 1
END
ELSE
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    INSERT INTO [dbo].[userPermission] ( [user_id], right_id , modified_by , modify_date )values (@editor_id,@permission,@modified_user,@DateNow) ;
	set @return = 1
set @return = 0
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateStepAtStep5]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 3/4/2016
-- Description:	After complete step 5 delete record from company setup step and insert in to loan setup step
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateStepAtStep5] 
	-- Add the parameters for the stored procedure here
	@company_id int = 0, 
	@branch_id int = 0,
	@non_registered_branch_id int = 0,
	@step_number int = 0,
	@return int =0 OUTPUT

AS
DECLARE @step int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT @step = step_number FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id 
	
	IF(@step = 5)
	BEGIN
	DELETE FROM [dbo].[companySetupStep] WHERE company_id = @company_id AND branch_id = @branch_id

	UPDATE [dbo].[company]
	SET flag_value = 1
	WHERE company_id = @company_id

	INSERT INTO [dbo].[loanSetupStep]([non_registered_branch_id],[step_number])
	VALUES (@company_id,@step_number)
	SET @return = 1

	END

	ELSE
	BEGIN
	SET @return = 0
	END
	
	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spUpdateStepNumberByUserId]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<kasun>
-- Create date: <1/26/2016>
-- Description:	<Update User Existing Step nomber for a given userId>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateStepNumberByUserId]
(
@user_id int,
@step_id int,
@loan_id int = 0,
@branch_id int = 0
)
AS
DECLARE @ReturnValue bit
DECLARE @ExistingStepId INT
BEGIN
SET @ExistingStepId = 0;


IF EXISTS(SELECT user_id  FROM [dbo].[user] WHERE user_id = @user_id)
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
	IF EXISTS(SELECT [step_id] FROM [step] WHERE [user_id] = @user_id)
	BEGIN
		SELECT @ExistingStepId = [step_id] FROM [step] WHERE [user_id] = @user_id
		IF @ExistingStepId < @step_id
		BEGIN
			IF @step_id=11
			BEGIN
				-- SET NOCOUNT ON added to prevent extra result sets from
				-- interfering with SELECT statements.
				SET NOCOUNT ON;
				DELETE FROM [dbo].[step] WHERE [user_id] = @user_id
				Set @ReturnValue = 1
			END
			ELSE
			BEGIN
				IF @loan_id >0
				BEGIN
					UPDATE [dbo].[step] SET [step_id] = @step_id , [loan_id] = @loan_id, [branch_id] = @branch_id WHERE [user_id] = @user_id
					Set @ReturnValue = 1	
				END
				ELSE
				BEGIN
					UPDATE [dbo].[step] SET [step_id] = @step_id  WHERE user_id=@user_id
					Set @ReturnValue = 1	
				END
			END
		END

		-- MODIFIED BY IRFAN... TO UPDATE THE BRANCH ID ONLY
		ELSE IF @ExistingStepId = @step_id
		BEGIN			
			UPDATE [dbo].[step] SET [step_id] = @step_id , [loan_id] = @loan_id, [branch_id] = @branch_id WHERE [user_id] = @user_id
			Set @ReturnValue = 1
		END


		ELSE
		BEGIN			
			Set @ReturnValue = 1	
		END
	END	
	ELSE
	BEGIN
		IF @step_id=1
		BEGIN
			-- SET NOCOUNT ON added to prevent extra result sets from
			-- interfering with SELECT statements.
			SET NOCOUNT ON;
			INSERT INTO [dbo].[step]([user_id], [step_id]) VALUES(@user_id, @step_id)
			Set @ReturnValue = 1
		END
		ELSE
		BEGIN
			Set @ReturnValue = 0
		END
	END	
		 
END
ELSE
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
	Set @ReturnValue = 1

END
return @ReturnValue
end



GO
/****** Object:  StoredProcedure [dbo].[spUpdateTitleStatus]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Piyumi
-- Create date: 3/17/2016
-- Description:	update title status
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateTitleStatus] 
	-- Add the parameters for the stored procedure here
	@unit_id varchar(50) = null, 
	--@year int = 0,
	--@make varchar(20) = null, 
	--@model varchar(50) = null,
	@title_status int = 0,
	@loan_code varchar(100) = null,
	@user_id int =0,
	@modified_date datetime = null,
	@return int =0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT unit_id FROM [dbo].[unit] WHERE unit_id = @unit_id)
	BEGIN
		UPDATE [dbo].[unit]
		SET title_status = @title_status,modified_date =@modified_date, modified_by=@user_id
		WHERE unit_id = @unit_id AND loan_id = (SELECT loan_id FROM [dbo].[loan] WHERE loan_code = @loan_code)

		SET @return =  1
	END
	RETURN @return
END

GO
/****** Object:  StoredProcedure [dbo].[spUpdateUserCreatedById]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Kanishka
-- Create date: 1/18/2016
-- Description: update branch id of user 
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserCreatedById]
	-- Add the parameters for the stored procedure here
	@created_by INT,
	@user_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [user]
	SET created_by = @created_by
	WHERE user_id = @user_id
END



GO
/****** Object:  StoredProcedure [dbo].[spUpdateUserDetails]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/13/2015
-- Description:	update user details
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserDetails] 
	-- Add the parameters for the stored procedure here
	@user_id int,
	@user_name nvarchar(50),
	@first_name nvarchar(50),
	@last_name nvarchar(50),
	@email nvarchar(50),
	@phone_no nvarchar(50),
	@status bit,
	@modified_date datetime,
	@branch_id int,

	@return int =0 output
AS
BEGIN
IF EXISTS(SELECT * FROM [dbo].[user]
          WHERE user_id = @user_id ) 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[user] SET user_name = @user_name, last_name = @last_name, first_name = @first_name, email = @email, phone_no = @phone_no, status = @status, modified_date = @modified_date, branch_id = @branch_id WHERE user_id= @user_id;
	
	set @return = 1
END
ELSE
BEGIN
set @return = -1
END
return @return
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateUserStatus]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kanishka SHM
-- Create date: 2016/01/19
-- Description:	Update user status
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserStatus]
	-- Add the parameters for the stored procedure here
	@user_id INT,
	@activation_code uniqueidentifier
AS
DECLARE @return_val INT
BEGIN 
IF EXISTS(SELECT [user_id] FROM user_activation WHERE [user_id] = @user_id AND activation_code = @activation_code AND 
	is_active = 0)
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;
		--Update User Activation table
		UPDATE user_activation
		SET is_active = 1
		WHERE [user_id] = @user_id AND activation_code = @activation_code

		-- Update user table
		UPDATE [user]
		SET [status] = 1
		WHERE [user_id] = @user_id
		SET @return_val = 1
	END
	RETURN @return_val
END




GO
/****** Object:  StoredProcedure [dbo].[spUserLogin]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<kasun>
-- Create date: <1/13/2016>
-- Description:	<check User Logins>
-- =============================================
CREATE PROCEDURE [dbo].[spUserLogin]
(
@userName VARCHAR(50) 
)
AS
BEGIN
SET NOCOUNT ON;

SELECT  [user_id]
      ,[user_name]
      ,[password]
      ,[branch_id]
      ,[role_id]
      ,[company_id]
FROM [dbo].[user]
WHERE [user_name]=@userName COLLATE SQL_Latin1_General_CP1_CS_AS AND [status]=1 AND [is_delete]=0 




end

 /*SELECT     [user].user_id, [user].user_name, [user].password,[user].branch_id, [user].role_id, [user].company_id, company.company_type
FROM         [user] INNER JOIN
                      company ON [user].company_id = company.company_id
WHERE [user].user_name=@userName AND [user].status='1' AND [user].is_delete='0'   */
GO
/****** Object:  StoredProcedure [dbo].[spverifyAccountBytoken]    Script Date: 3/24/2016 7:01:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		irfan
-- Create date: 1/17/2015
-- Description:	verifying account by token
-- =============================================
CREATE PROCEDURE [dbo].[spverifyAccountBytoken] 
	-- Add the parameters for the stored procedure here
	@user_id int,
	@token nvarchar(50),
	@expired_date datetime,
	@return int =0 out
	
	
AS
BEGIN
IF EXISTS(SELECT user_id FROM [dbo].[forgotPasswordToken] WHERE user_id = @user_id AND token = @token AND expired_date >= @expired_date)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	set @return = 1 ;

	DELETE FROM [dbo].[forgotPasswordToken] WHERE user_id = @user_id;
	END
	return @return
END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NRB"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 135
               Right = 447
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 485
               Bottom = 135
               Right = 682
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewBranchNonRegBranch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewBranchNonRegBranch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[28] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "loan"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 202
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "curtailment"
            Begin Extent = 
               Top = 6
               Left = 283
               Bottom = 136
               Right = 453
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewLoanCurtailmentDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewLoanCurtailmentDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "loanDetails"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 253
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "loan"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 242
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewLoanPaymentDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewLoanPaymentDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "user"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "branch"
            Begin Extent = 
               Top = 95
               Left = 348
               Bottom = 225
               Right = 518
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "company"
            Begin Extent = 
               Top = 154
               Left = 32
               Bottom = 284
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewUserDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewUserDetails'
GO
