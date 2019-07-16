USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Services_Bidding]    Script Date: 15/07/2016 01:47:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[MaaAahwanam_Services_Bidding](@RequestId bigint)
as begin
select data2.Id, data2.BusinessName, data1.VendorType, data1.Description, data1.Amount, data1.UpdatedDate,data1.RequestId,data2.Address from Vendormasters data2 
join ServiceResponses data1 
on data1.ResponseBy = data2.Id
where data1.RequestId=@RequestId and data2.Status='Active'
end