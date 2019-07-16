USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Others_Tickets]    Script Date: 19/07/2016 05:57:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[MaaAahwanam_Others_Tickets](@TicketId bigint)
as begin
select data1.TicketId,data1.Status,data2.BusinessName,data1.UpdatedDate,data2.ServicType from IssueTickets data1 
join Vendormasters data2 
on data1.UserLoginId=data2.id
where data1.TicketId=@TicketId
end