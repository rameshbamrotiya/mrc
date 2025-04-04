USE [UNMWeb]
GO
/****** Object:  StoredProcedure [dbo].[GetAppoinmentDetailList]    Script Date: 15-06-2024 03:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE dbo.PatientAppointmentMaster ADD IsVisit BIT DEFAULT 0

go
ALTER PROCEDURE [dbo].[GetAppoinmentDetailList]
    @UnitId NVARCHAR(MAX)=null,
    @DoctorId NVARCHAR(MAX),
    @Deptid NVARCHAR(MAX)=null,
    @fromdate NVARCHAR(MAX),
    @toDate NVARCHAR(MAX)
	as
BEGIN
    SET NOCOUNT ON;

    DECLARE @Qury NVARCHAR(MAX);
    DECLARE @prmtr NVARCHAR(MAX);

    SET @Qury = '
        SELECT pm.*, 
               dbo.fnGetVisitTypeNameById(pm.VisitTypeId) AS VisitTypeName, 
               dbo.fnGetSlotNameById(pm.SlotId) AS SloteName,
               sm.Id as deptid, 
               [dbo].fnGetDepartmentNameById(sm.Id) as DepartmentName,
               dbo.fnGetDoctorNameById(pm.DoctorId) AS DoctorName, 
               dbo.fnGetUnitNameById(pm.UnitId) AS UnitName,
               CONVERT(varchar(10), pm.AppointmentDate, 103) as strAppointmentDate
        FROM [PatientAppointmentMaster] AS pm
        INNER JOIN FacultyMasterDetails AS cu ON pm.DoctorId = cu.Id
        INNER JOIN DepartmentMasterDetails AS sm ON sm.id = cu.DepartmentId
        WHERE pm.IsDelete = 0 AND 1=1 ';

    IF CONVERT(int, @UnitId) > 0
    BEGIN
        SET @Qury = @Qury + ' AND pm.UnitId = @UnitId ';
    END

    IF CONVERT(int, @DoctorId) > 0
    BEGIN
        SET @Qury = @Qury + ' AND pm.DoctorId = @DoctorId ';
    END

    IF CONVERT(int, @Deptid) > 0
    BEGIN
        SET @Qury = @Qury + ' cu.DepartmentId = @Deptid ';
    END

    IF (@fromdate != '' AND @toDate != '')
    BEGIN
        SET @Qury = @Qury + ' AND pm.AppointmentDate BETWEEN @fromdate AND @toDate ';
    END

    EXEC sp_executesql @Qury,
                       N'@UnitId NVARCHAR(MAX), @DoctorId NVARCHAR(MAX), @Deptid NVARCHAR(MAX), @fromdate NVARCHAR(MAX), @toDate NVARCHAR(MAX)',
                       @UnitId, @DoctorId, @Deptid, @fromdate, @toDate;
END
go
CREATE PROCEDURE UpdatePatientVisitInMaster
	-- Add the parameters for the stored procedure here
	@PatientId BIGINT,
	@UpdateBy	NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.PatientAppointmentMaster SET IsVisit=1,UpdateBy=@UpdateBy,UpdateDate=GETDATE() WHERE Id=@PatientId;

END
GO