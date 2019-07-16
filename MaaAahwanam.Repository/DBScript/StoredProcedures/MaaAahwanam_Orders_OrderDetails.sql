USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Orders_OrderDetails]    Script Date: 15/07/2016 01:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[MaaAahwanam_Orders_OrderDetails](@OrderNo bigint)
as begin
select data1.OrderId,data1.ServiceType,data1.Quantity,data2.BusinessName,data2.Address,data2.Landmark,data2.City,data1.UpdatedDate,data1.TotalPrice from OrderDetails data1 
join Vendormasters data2 
on data1.VendorId=data2.Id
where data1.OrderNo=@OrderNO and data1.Status='Active'
end