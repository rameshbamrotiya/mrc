declare @UnitRoleId int;

IF((SELECT COUNT(1) FROM dbo.RoleMaster WHERE Rolename='DoctorUnit')<=0)  
BEGIN
INSERT into RoleMaster(Rolename,IsActive,IsDelete) values ('DoctorUnit',1,0);
set  @UnitRoleId=SCOPE_IDENTITY();
END
ELSE 
BEGIN
 
IF((SELECT COUNT(1) FROM dbo.ConfigDetails WHERE ParameterName='UnitRoleId')>0)  
BEGIN
insert into ConfigDetails (ParameterName,ParameterValue) values ('UnitRoleId',@UnitRoleId);
END;

END;

SET @UnitRoleId=(SELECT TOP 1 ParameterValue FROM dbo.ConfigDetails WHERE ParameterName='UnitRoleId');

-- Merge statement to update existing records or insert new ones
MERGE INTO [dbo].[UsersMaster] AS target
USING (
    SELECT DISTINCT
        @UnitRoleId AS RoleId,
        dm.DeptId AS DepartmentId,
        LEFT(un.UnitName, 100) AS FirstName,
        LEFT(dm.DepartmentName, 100) AS LastName,
        '' AS Email,
        '' AS PhoneNo,
        REPLACE(LOWER(LEFT(LTRIM(RTRIM(dm.DepartmentName)) + LTRIM(RTRIM(un.UnitName)), 100)), ' ', '') AS UserName,
        '' AS Designation,
        'D7uoVlyRq3R+5zSX/2KwZQ==' AS UserPassword,
        1 AS IsActive,
        0 AS IsDelete,
        'Admin' AS CreateBy,
        GETDATE() AS CreateDate,
        un.[Id] AS DoctorId
    FROM tbl_ConfigUnit AS un
    JOIN DepartmentMasterDetails AS dm 
    ON dm.DeptId = un.SpecilizationId 
    AND ISNULL(dm.IsDelete, 0) = 0 
    AND dm.LanguageId = 1
    WHERE ISNULL(un.IsDelete, 0) = 0
) AS source
ON target.UserName = source.UserName

-- Update existing records
WHEN MATCHED THEN
    UPDATE SET 
        target.RoleId = source.RoleId,
        target.DepartmentId = source.DepartmentId,
        target.FirstName = source.FirstName,
        target.LastName = source.LastName,
        target.Email = source.Email,
        target.PhoneNo = source.PhoneNo,
        target.Designation = source.Designation,
        target.UserPassword = source.UserPassword,
        target.IsActive = source.IsActive,
        target.IsDelete = source.IsDelete,
        target.CreateBy = source.CreateBy,
        target.CreateDate = source.CreateDate,
        target.DoctorId = source.DoctorId

-- Insert new records
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([RoleId], [DepartmentId], [FirstName], [LastName], [Email], [PhoneNo], [UserName], [Designation], [UserPassword], [IsActive], [IsDelete], [CreateBy], [CreateDate], [DoctorId])
    VALUES (source.RoleId, source.DepartmentId, source.FirstName, source.LastName, source.Email, source.PhoneNo, source.UserName, source.Designation, source.UserPassword, source.IsActive, source.IsDelete, source.CreateBy, source.CreateDate, source.DoctorId);

GO

ALTER PROCEDURE [dbo].[GetSlotListFromWeekNoForAppointment]
    @WeekNoId int,
    @UnitId int,
    @AppointmentDate date,
    @DoctorId int
AS
BEGIN
    SET NOCOUNT ON;

    
    SELECT DISTINCT
        cusd.Id,
        cusd.SloteName,
        cusd.StartTimeHour,
        cusd.StartTimeMin,
        cusd.StartTimeTT,
        cusd.EndTimeHour,
        cusd.EndTimeMin,
        cusd.EndTimeTT,
        CASE 
            WHEN CountApp >= ISNULL(cusd.maxSlot, 0) THEN 'Unavailable'
            ELSE 'Available'
        END AS SlotAvailability
    FROM 
        tbl_ConfigUnit AS cu
    JOIN 
        tbl_ConfigUnitSlotDetail AS cusd ON cu.Id = cusd.UnitId
    LEFT JOIN (
        SELECT  
            adsasd.SlotId,
            COUNT(1) AS CountApp 
        FROM 
            PatientAppointmentMaster AS adsasd 
        WHERE 
            adsasd.AppointmentDate = @AppointmentDate
			and isnull(adsasd.IsDelete,0)=0
            --AND adsasd.DoctorId = @DoctorId 
        GROUP BY 
            adsasd.SlotId
    ) AS pam ON cusd.Id = pam.SlotId 
    WHERE 
		cusd.UnitId=@UnitId and 
        cusd.WeekNo = @WeekNoId 
        AND ISNULL(cu.IsDelete, 0) = 0
        AND ISNULL(cusd.IsDelete, 0) = 0
    ORDER BY 
        cusd.Id;
END

go 