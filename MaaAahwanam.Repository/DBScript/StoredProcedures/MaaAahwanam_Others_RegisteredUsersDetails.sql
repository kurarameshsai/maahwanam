USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Others_RegisteredUsersDetails]    Script Date: 20/07/2016 03:52:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[MaaAahwanam_Others_RegisteredUsersDetails](@UserLoginId bigint)
as begin
select data1.UserLoginId,data2.FirstName,data2.LastName,data1.RegDate,data2.Address,data2.City,data2.State,data1.Status,data1.UserName,data2.UserDetailId,data2.UserPhone from UserLogins data1 join UserDetails data2 on data1.UserLoginId=data2.UserLoginId where data1.UserType='User' and data1.UserLoginId = @UserLoginId
end