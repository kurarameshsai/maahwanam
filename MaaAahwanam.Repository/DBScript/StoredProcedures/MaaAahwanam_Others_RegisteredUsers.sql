USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Others_RegisteredUsers]    Script Date: 20/07/2016 03:52:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[MaaAahwanam_Others_RegisteredUsers]
as begin
select data1.UserLoginId,data2.FirstName,data2.LastName,data1.RegDate,data2.Address,data1.Status from UserLogins data1 join UserDetails data2 on data1.UserLoginId=data2.UserLoginId where data1.UserType='User'
end
