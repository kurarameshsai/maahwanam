USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Others_Comments]    Script Date: 15/07/2016 03:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[MaaAahwanam_Others_Comments](@CommentId bigint)
as begin
select data1.CommentId,data1.Status,data2.BusinessName,data1.UpdatedDate from Comments data1 
join Vendormasters data2 
on data1.ServiceId=data2.id
where data1.CommentId=@CommentId
end