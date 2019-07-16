USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[AllVendorList]    Script Date: 28/07/2016 12:14:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure  [dbo].[AllVendorList](@ServicType nvarchar(200))
as
begin
	if(@ServicType='Venue')
	begin
	  select * from Vendormasters where ServicType='Venue'
	end
	if(@ServicType='Catering')
	begin
	  select * from Vendormasters where ServicType='Catering'
	end
		if(@ServicType='Photography')
	begin
	  select * from Vendormasters where ServicType='Photography'
	end
		if(@ServicType='InvitationCards')
	begin
	  select * from Vendormasters where ServicType='InvitationCard'
	end
		if(@ServicType='Decorator')
	begin
	  select * from Vendormasters where ServicType='Decorators'
	end
		if(@ServicType='Entertainment')
	begin
	  select * from Vendormasters where ServicType='Entertainment'
	end
		if(@ServicType='EventOrganisers')
	begin
	  select * from Vendormasters where ServicType='EventOrganiser'
	end
		if(@ServicType='BeautyServices')
	begin
	  select * from Vendormasters where ServicType='BeautyServices'
	end
		if(@ServicType='WeddingFavor')
	begin
	  select * from Vendormasters where ServicType='WeddingFavor'
	end
		if(@ServicType='TravelAccomodation')
	begin
	  select * from Vendormasters where ServicType='Travel&Accomadation'
	end
		if(@ServicType='Gifts')
	begin
	  select * from Vendormasters where ServicType='Gifts'
	end
		if(@ServicType='WeddingCollection')
	begin
	  select * from Vendormasters where ServicType='WeddingCollection'
	end
		if(@ServicType='Others')
	begin
	  select * from Vendormasters where ServicType='Other'
	end
end

