USE [ParkingGarage]
GO
/****** Object:  Table [dbo].[TbParkingAssignments]    Script Date: 16/09/2025 20:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbParkingAssignments](
	[AssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[ParkingLotId] [int] NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
	[CheckOutTime] [datetime] NULL,
 CONSTRAINT [PK_TbParkingAssignments] PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbParkingLots]    Script Date: 16/09/2025 20:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbParkingLots](
	[ParkingLotId] [int] IDENTITY(1,1) NOT NULL,
	[TicketTypeId] [int] NOT NULL,
	[IsOccupied] [bit] NOT NULL,
 CONSTRAINT [PK_TbParkingLots] PRIMARY KEY CLUSTERED 
(
	[ParkingLotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbVehicles]    Script Date: 16/09/2025 20:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbVehicles](
	[VehicleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LicensePlate] [nvarchar](20) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[VehicleTypeId] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[TicketTypeId] [int] NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TbVehicles] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TbParkingAssignments] ON 
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (1, 1, 31, CAST(N'2025-09-13T16:09:45.197' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (2, 5, 1, CAST(N'2025-09-15T06:46:47.573' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (3, 6, 2, CAST(N'2025-09-15T08:36:57.097' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (4, 0, 11, CAST(N'2025-09-15T09:31:43.593' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (5, 0, 2, CAST(N'2025-09-15T09:31:45.540' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (6, 0, 12, CAST(N'2025-09-15T09:31:46.680' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (7, 0, 13, CAST(N'2025-09-15T09:31:47.477' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (8, 11, 14, CAST(N'2025-09-16T13:20:18.087' AS DateTime), NULL)
GO
INSERT [dbo].[TbParkingAssignments] ([AssignmentId], [VehicleId], [ParkingLotId], [CheckInTime], [CheckOutTime]) VALUES (9, 12, 15, CAST(N'2025-09-16T13:23:04.693' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[TbParkingAssignments] OFF
GO
SET IDENTITY_INSERT [dbo].[TbParkingLots] ON 
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (1, 1, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (2, 1, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (3, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (4, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (5, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (6, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (7, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (8, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (9, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (10, 1, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (11, 2, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (12, 2, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (13, 2, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (14, 2, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (15, 2, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (16, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (17, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (18, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (19, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (20, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (21, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (22, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (23, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (24, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (25, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (26, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (27, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (28, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (29, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (30, 2, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (31, 3, 1)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (32, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (33, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (34, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (35, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (36, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (37, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (38, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (39, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (40, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (41, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (42, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (43, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (44, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (45, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (46, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (47, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (48, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (49, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (50, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (51, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (52, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (53, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (54, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (55, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (56, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (57, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (58, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (59, 3, 0)
GO
INSERT [dbo].[TbParkingLots] ([ParkingLotId], [TicketTypeId], [IsOccupied]) VALUES (60, 3, 0)
GO
SET IDENTITY_INSERT [dbo].[TbParkingLots] OFF
GO
SET IDENTITY_INSERT [dbo].[TbVehicles] ON 
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (1, N'דנה לוי', N'12-345-67', N'0501112222', 4, 1900, 2000, 4700, 2, CAST(N'2025-09-13T16:09:45.180' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (5, N'jsjsjsj', N'7968756', N'9384029405', 2, 1700, 500, 2000, 1, CAST(N'2025-09-15T06:46:36.347' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (6, N'דנה לוי', N'2345679', N'0501112222', 2, 1200, 456, 456, 1, CAST(N'2025-09-15T08:36:48.217' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (7, N'AutoGenerated', N'RAND-26055', N'0547479694', 3, 1859, 1506, 3361, 2, CAST(N'2025-09-15T09:31:40.373' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (8, N'AutoGenerated', N'RAND-83294', N'0564286990', 5, 2091, 2105, 3644, 1, CAST(N'2025-09-15T09:31:43.750' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (9, N'AutoGenerated', N'RAND-47231', N'0532613920', 5, 2360, 2391, 2589, 2, CAST(N'2025-09-15T09:31:45.563' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (10, N'AutoGenerated', N'RAND-73382', N'0579613533', 1, 2306, 1810, 2963, 2, CAST(N'2025-09-15T09:31:46.707' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (11, N'jjjjjj', N'11465711', N'0500000000', 2, 1600, 1800, 4300, 2, CAST(N'2025-09-16T13:20:00.300' AS DateTime))
GO
INSERT [dbo].[TbVehicles] ([VehicleId], [Name], [LicensePlate], [Phone], [VehicleTypeId], [Height], [Width], [Length], [TicketTypeId], [CheckInTime]) VALUES (12, N'Test', N'13245', N'05489652', 2, 1600, 1800, 4300, 2, CAST(N'2025-09-16T13:23:04.553' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TbVehicles] OFF
GO
ALTER TABLE [dbo].[TbParkingAssignments] ADD  DEFAULT (getdate()) FOR [CheckInTime]
GO
ALTER TABLE [dbo].[TbParkingLots] ADD  CONSTRAINT [DF__ParkingLo__IsOcc__3C69FB99]  DEFAULT ((0)) FOR [IsOccupied]
GO
ALTER TABLE [dbo].[TbVehicles] ADD  CONSTRAINT [DF__Vehicles__CheckI__38996AB5]  DEFAULT (getdate()) FOR [CheckInTime]
GO
/****** Object:  StoredProcedure [dbo].[GetVehiclesByTicketType]    Script Date: 16/09/2025 20:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Run in your DB (ParkingGarage)
CREATE   PROCEDURE [dbo].[GetVehiclesByTicketType]
  @TicketTypeId INT
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
      v.VehicleId,
      v.LicensePlate,
      v.Name,
      v.Phone,
      v.VehicleTypeId,
      v.TicketTypeId    AS VehicleTicketTypeId,
      a.AssignmentId,
      a.CheckInTime,
      l.ParkingLotId,
      l.TicketTypeId    AS LotTicketTypeId
  FROM TbParkingAssignments AS a
  INNER JOIN TbVehicles      AS v ON v.VehicleId = a.VehicleId
  INNER JOIN TbParkingLots   AS l ON l.ParkingLotId     = a.ParkingLotId
  WHERE a.CheckOutTime IS NULL
    AND l.TicketTypeId = @TicketTypeId
  ORDER BY l.ParkingLotId;
END
GO
