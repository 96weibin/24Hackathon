SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  View [dbo].[RW_LastSolutions]    Script Date: 10/24/2024 8:49:26 AM ******/

CREATE   view [dbo].[RW_LastSolutions]
as
select max(solutionId) as SolutionID, ModelName from RW_Solutions
group by RW_Solutions.ModelName
GO
------------------------------
CREATE   view [dbo].[VIEW_OBJ]
as
select RW_ObjectiveFunction.SolutionID, RW_ObjectiveFunction.CaseName,  RW_LastSolutions.ModelName, RW_ObjectiveFunction.ObjectiveFunction
from RW_ObjectiveFunction 
inner join RW_LastSolutions on RW_ObjectiveFunction.SolutionID = RW_LastSolutions.SolutionID
GO
------------------------------

CREATE   view [dbo].[VIEW_PrimalBasis]
as
SELECT 
RW_PrimalColumn.SolutionID,
RW_LastSolutions.ModelName,
RW_CaseDetails.CaseName,
 SUBSTRING(ColumnName, 1, LEN(ColumnName) - CHARINDEX(':', REVERSE(ColumnName))) as Basis,
SUBSTRING(ColumnName, LEN(ColumnName) - CHARINDEX(':', REVERSE(ColumnName)) + 2, LEN(ColumnName)) AS BasisFactor,
	MarginalValue as Margin, LoBound, HiBound, Cost
FROM 
    RW_PrimalColumn inner join RW_LastSolutions on RW_PrimalColumn.SolutionID = RW_LastSolutions.SolutionID
	left join RW_CaseDetails on RW_PrimalColumn.CaseID = RW_CaseDetails.CaseID and RW_PrimalColumn.SolutionID = RW_CaseDetails.SolutionID 
where MarginalValue <> 0 and
(ColumnName like 'SELL%'
or ColumnName like 'PURC%'
or ColumnName like 'CCAP%'
or ColumnName like 'ZLIM%'
)
GO
------------------------------


CREATE    PROCEDURE [dbo].[CompareObjInTwoCases]
    @ModelName NVARCHAR(100),
	@CaseName1 NVARCHAR(100),
	@CaseName2 NVARCHAR(100),
	@Obj1 float OUTPUT,
	@Obj2 float OUTPUT
AS
BEGIN
    -- Set the output parameter to a specific string value

	 select @Obj1 = ObjectiveFunction from VIEW_OBJ where ModelName = @ModelName and CaseName = @CaseName1
	  select @Obj2 = ObjectiveFunction from VIEW_OBJ where ModelName = @ModelName and CaseName = @CaseName2
  
END
GO

------------------------------

CREATE            PROCEDURE [dbo].[GetTopBasis]
    @TopCount INT,
	@modelName varchar(100),
	@CaseName varchar(100),
	@BasisType INT -- 0--all, 1 -purc; 2 - sell; 3-ccap; 4-zlim; 5 -ccap & zlim
AS
BEGIN
    -- Check if the input parameter is valid
    IF @TopCount <= 0
    BEGIN
        RAISERROR('The number of top must be greater than 0', 16, 1);
        RETURN;
    END
	    IF @BasisType NOT IN (0, 1, 2, 3, 4, 5)
    BEGIN
        RAISERROR('Invalid @BasisType option. Please use 0 for all, 1 for purc, 2 for sell, 3 for ccap, 4 for zlim', 16, 1);
        RETURN;
    END

	SELECT TOP (@TopCount) [Basis], [BasisFactor], Margin, LoBound, HiBound, Cost
		FROM dbo.VIEW_PrimalBasis
	   where ModelName  = @modelName  and CaseName = @CaseName
			and (
			@BasisType = 0
			or (@BasisType = 1 and Basis = 'PURC')
			or (@BasisType = 2 and Basis = 'SELL')
			or (@BasisType = 3 and Basis = 'CCAP')
			or (@BasisType = 4 and Basis = 'ZLIM')
			or (@BasisType = 5 and (Basis = 'ZLIM' or Basis = 'CCAP'))
			)


       ORDER BY abs(Margin) DESC;

END;

GO

------------------------------

CREATE            PROCEDURE [dbo].[GetDefaultModel]
    @ModelName NVARCHAR(100) OUTPUT
AS
BEGIN
    -- Set the output parameter to a specific string value

   select  top 1 @ModelName = modelName  from rw_LastSolutions order by solutionid desc
  
END
GO
