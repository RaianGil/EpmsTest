USE [GuardianEpmsTest]
GO

/****** Object:  Table [dbo].[RES]    Script Date: 3/8/2021 1:30:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RES](
	[RESID] [int] IDENTITY(1,1) NOT NULL,
	[TaskControlID] [int] NULL,
	[TypePolicy] [int] NULL,
	[InsuredPremises] [varchar](100) NULL,
	[InsuredName] [varchar](100) NULL,
	[PartOccupied] [varchar](50) NULL,
	[Owner] [bit] NULL,
	[GeneralLesee] [bit] NULL,
	[Tenant] [bit] NULL,
	[Other] [bit] NULL,
	[BILimit] [int] NULL,
	[PDLimit] [int] NULL,
	[Individual] [bit] NULL,
	[Partnership] [bit] NULL,
	[Corporation] [bit] NULL,
	[JoinVenture] [bit] NULL,
	[OtherTI] [bit] NULL,
	[FireDamage] [varchar](20) NULL,
	[MedicalPayment] [varchar](20) NULL,
 CONSTRAINT [PK_RES] PRIMARY KEY CLUSTERED 
(
	[RESID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_Owner]  DEFAULT ((0)) FOR [Owner]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_GeneralLesee]  DEFAULT ((0)) FOR [GeneralLesee]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_Tenant]  DEFAULT ((0)) FOR [Tenant]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_Other]  DEFAULT ((0)) FOR [Other]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_BILimit]  DEFAULT ((0)) FOR [BILimit]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_PDLimit]  DEFAULT ((0)) FOR [PDLimit]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_MedicalPayment]  DEFAULT ((0)) FOR [MedicalPayment]
GO

ALTER TABLE [dbo].[RES] ADD  CONSTRAINT [DF_RES_FireDamage]  DEFAULT ((0)) FOR [FireDamage]
GO
/*

*/
USE [GuardianEpmsTest]
GO

/****** Object:  Table [dbo].[RESLiability]    Script Date: 3/8/2021 1:31:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RESLiability](
	[RLiabilityID] [int] IDENTITY(1,1) NOT NULL,
	[LimitDesc] [varchar](20) NULL,
	[Premium] [varchar](20) NULL,
	[isComercial] [bit] NULL,
 CONSTRAINT [PK_RESLiability] PRIMARY KEY CLUSTERED 
(
	[RLiabilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/*

*/
USE [GuardianEpmsTest]
GO

/****** Object:  Table [dbo].[RESQuote]    Script Date: 3/8/2021 1:32:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RESQuote](
	[RESID] [int] IDENTITY(1,1) NOT NULL,
	[TaskControlID] [int] NULL,
	[TypePolicy] [int] NULL,
	[InsuredPremises] [varchar](100) NULL,
	[InsuredName] [varchar](100) NULL,
	[PartOccupied] [varchar](50) NULL,
	[Owner] [bit] NULL,
	[GeneralLesee] [bit] NULL,
	[Tenant] [bit] NULL,
	[Other] [bit] NULL,
	[BILimit] [int] NULL,
	[PDLimit] [int] NULL,
	[Individual] [bit] NULL,
	[Partnership] [bit] NULL,
	[Corporation] [bit] NULL,
	[JoinVenture] [bit] NULL,
	[OtherTI] [bit] NULL,
	[FireDamage] [varchar](20) NULL,
	[MedicalPayment] [varchar](20) NULL,
 CONSTRAINT [PK_RESQuote] PRIMARY KEY CLUSTERED 
(
	[RESID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_Owner]  DEFAULT ((0)) FOR [Owner]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_GeneralLesee]  DEFAULT ((0)) FOR [GeneralLesee]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_Tenant]  DEFAULT ((0)) FOR [Tenant]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_Other]  DEFAULT ((0)) FOR [Other]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_BILimit]  DEFAULT ((0)) FOR [BILimit]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_PDLimit]  DEFAULT ((0)) FOR [PDLimit]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_FireDamage]  DEFAULT ((0)) FOR [FireDamage]
GO

ALTER TABLE [dbo].[RESQuote] ADD  CONSTRAINT [DF_RESQuote_MedicalPayment]  DEFAULT ((0)) FOR [MedicalPayment]
GO

/****************************************************************************/
INSERT INTO PolicyClass(PolicyClassDesc, Available) values ('RES Policy', 1);
INSERT INTO PolicyType(PolicyTypeDesc, island) Values ('RES', 'PR');
INSERT INTO TaskControlType(TaskControlTypeDesc, Available) Values ('RES', 1);
INSERT INTO TaskControlType(TaskControlTypeDesc, Available) Values ('RES Quote', 1);
INSERT INTO CommissionAgent(PolicyClassID, AgentID, PolicyType, InsuranceCompanyID, CommissionRate, EffectiveDate, CommissionEntryDate, AgentLevel, RateAmt, CommissionAmount)	VALUES (29, 002, 'RES',	001, 10, '2020-01-01 00:00:00.000',	'2020-09-14 00:00:00.000',1, 1, 0)
/****************************************************************************/
/*Insetanto en la tabla de limites*/
/*Resideciales*/
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('50,000', '250', 0);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('100,000', '500', 0);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('300,000', '1,000', 0);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('500,000', '1,500', 0);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('1,000,000', '1,750', 0);
/*Comerciales*/
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('50,000', '500', 1);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('100,000', '1,000', 1);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('300,000', '1,250', 1);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('500,000', '1,750', 1);
INSERT INTO RESLiability(LimitDesc,Premium,isComercial) values ('1,000,000', '2,500', 1);
/*Para los dropdown - Si es residencial*/
/*******************************************************************************/
USE [GuardianEpmsTest]
GO
/****** Object:  StoredProcedure [dbo].[AddRES]    Script Date: 3/8/2021 2:02:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[AddRES](
	@TaskControlID int, 
	@InsuredPremises varchar(100), 
	@InsuredName varchar(100),
	@Owner int,
	@GeneralLesee int, 
	@Tenant int, 
	@Other int, 
	@Individual int,
	@Partnership int, 
	@Corporation int, 
	@JoinVenture int, 
	@OtherTI int,
	@PartOccupied varchar(50), 
	@BILimit int,
	@PDLimit int, 
	@MedicalPayment varchar(20), 
	@FireDamage varchar(20),
	@TypePolicy int)
as
begin
	
       IF((SELECT COUNT(*) FROM RES WHERE TaskControlID = @TaskControlID) = 0)
              BEGIN
                     INSERT INTO RES(
						TaskControlID, 
						InsuredPremises, 
						InsuredName,
						TypePolicy,
						"Owner", 
						GeneralLesee, 
						Tenant, 
						Other, 
						Individual, 
						Partnership, 
						Corporation, 
						JoinVenture, 
						OtherTI,
						PartOccupied, 
						BILimit,
						PDLimit, 
						MedicalPayment, 
						FireDamage)
                     VALUES(
						@TaskControlID,
						@InsuredPremises,
						@InsuredName,
						@TypePolicy,
						CONVERT(Bit,@Owner),
						CONVERT(Bit,@GeneralLesee),
						CONVERT(Bit,@Tenant),
						CONVERT(Bit,@Other),
						CONVERT(Bit,@Individual),
						CONVERT(Bit,@Partnership),
						CONVERT(Bit,@Corporation),
						CONVERT(Bit,@JoinVenture),
						CONVERT(Bit,@OtherTI),
						@PartOccupied,
						@BILimit,
						@PDLimit,
						@MedicalPayment,
						@FireDamage)

                     SELECT @@IDENTITY
              END
       ELSE
              BEGIN
                     UPDATE RES SET 
					 InsuredPremises = @InsuredPremises, 
					 InsuredName = @InsuredName,
					 TypePolicy = @TypePolicy,
					 "Owner" = CONVERT(Bit,@Owner), 
                     GeneralLesee = CONVERT(Bit,@GeneralLesee), 
					 Tenant = CONVERT(Bit,@Tenant), 
					 Other = CONVERT(Bit,@Other), 
					 Individual = CONVERT(Bit,@Individual), 
                     Partnership = CONVERT(Bit,@Partnership), 
                     Corporation = CONVERT(Bit,@Corporation), 
					 JoinVenture = CONVERT(Bit,@JoinVenture), 
					 OtherTI = CONVERT(Bit,@OtherTI), 
					 PartOccupied = @PartOccupied, 
					 BILimit = @BILimit,
					 PDLimit = @PDLimit,
					 MedicalPayment = @MedicalPayment, 
					 FireDamage = @FireDamage

                     WHERE TaskControlID = @TaskControlID
                     SELECT @TaskControlID
              END

End

USE [GuardianEpmsTest]
GO
/****** Object:  StoredProcedure [dbo].[AddRES]    Script Date: 3/8/2021 2:02:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[AddRESQuote](
	@TaskControlID int, 
	@InsuredPremises varchar(100), 
	@InsuredName varchar(100),
	@Owner int,
	@GeneralLesee int, 
	@Tenant int, 
	@Other int, 
	@Individual int,
	@Partnership int, 
	@Corporation int, 
	@JoinVenture int, 
	@OtherTI int,
	@PartOccupied varchar(50), 
	@BILimit int,
	@PDLimit int, 
	@MedicalPayment varchar(20), 
	@FireDamage varchar(20),
	@TypePolicy int)
as
begin
	
       IF((SELECT COUNT(*) FROM RES WHERE TaskControlID = @TaskControlID) = 0)
              BEGIN
                     INSERT INTO RESQuote(
						TaskControlID, 
						InsuredPremises, 
						InsuredName,
						TypePolicy,
						"Owner", 
						GeneralLesee, 
						Tenant, 
						Other, 
						Individual, 
						Partnership, 
						Corporation, 
						JoinVenture, 
						OtherTI,
						PartOccupied, 
						BILimit,
						PDLimit, 
						MedicalPayment, 
						FireDamage)
                     VALUES(
						@TaskControlID,
						@InsuredPremises,
						@InsuredName,
						@TypePolicy,
						CONVERT(Bit,@Owner),
						CONVERT(Bit,@GeneralLesee),
						CONVERT(Bit,@Tenant),
						CONVERT(Bit,@Other),
						CONVERT(Bit,@Individual),
						CONVERT(Bit,@Partnership),
						CONVERT(Bit,@Corporation),
						CONVERT(Bit,@JoinVenture),
						CONVERT(Bit,@OtherTI),
						@PartOccupied,
						@BILimit,
						@PDLimit,
						@MedicalPayment,
						@FireDamage)

                     SELECT @@IDENTITY
              END
       ELSE
              BEGIN
                     UPDATE RESQuote SET 
					 InsuredPremises = @InsuredPremises, 
					 InsuredName = @InsuredName,
					 TypePolicy = @TypePolicy,
					 "Owner" = CONVERT(Bit,@Owner), 
                     GeneralLesee = CONVERT(Bit,@GeneralLesee), 
					 Tenant = CONVERT(Bit,@Tenant), 
					 Other = CONVERT(Bit,@Other), 
					 Individual = CONVERT(Bit,@Individual), 
                     Partnership = CONVERT(Bit,@Partnership), 
                     Corporation = CONVERT(Bit,@Corporation), 
					 JoinVenture = CONVERT(Bit,@JoinVenture), 
					 OtherTI = CONVERT(Bit,@OtherTI), 
					 PartOccupied = @PartOccupied, 
					 BILimit = @BILimit,
					 PDLimit = @PDLimit,
					 MedicalPayment = @MedicalPayment, 
					 FireDamage = @FireDamage

                     WHERE TaskControlID = @TaskControlID
                     SELECT @TaskControlID
              END

End
/*******************************************************************/

USE [GuardianEpmsTest]
GO
/****** Object:  StoredProcedure [dbo].[GetRESLiability]    Script Date: 3/8/2021 2:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[GetRESLiability]
	as
	Begin
		Select RliabilityID, LimitDesc, Premium from RESLiability;
	End


USE [GuardianEpmsTest]
GO
/****** Object:  StoredProcedure [dbo].[GetRESLiabilityCommercial]    Script Date: 3/8/2021 2:07:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[GetRESLiabilityCommercial]
	as
	Begin
		Select RliabilityID, LimitDesc, Premium from RESLiability Where isComercial = 1;
	End
	
USE [GuardianEpmsTest]
GO
/****** Object:  StoredProcedure [dbo].[GetRESLiabilityResidential]    Script Date: 3/8/2021 2:10:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[GetRESLiabilityResidential]
	as
	Begin
		Select RliabilityID, LimitDesc, Premium from RESLiability Where isComercial = 0;
	End
	
USE [GuardianEpmsTest]
GO
/****** Object:  StoredProcedure [dbo].[GetRESsByTaskControlID]    Script Date: 3/8/2021 2:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRESsByTaskControlID]
@TaskControlID int,
@isQuote BIT
AS

IF @isQuote = 1
	BEGIN
		SELECT *
		FROM RESQuote
		WHERE  TaskControlID = @TaskControlID
	END
ELSE
	BEGIN
		SELECT *
		FROM RES
		WHERE  TaskControlID = @TaskControlID
	END

		
RETURN