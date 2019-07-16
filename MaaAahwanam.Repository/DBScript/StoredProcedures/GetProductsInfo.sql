USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[GetProductsInfo]    Script Date: 19/07/2016 11:19:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetProductsInfo]
(
@vid int,
@nType nvarchar(200)
)
as
begin
	if(@nType='Venue')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorVenues.VegLunchCost as cost,
	  VendorVenues.NonVegLunchCost as cost1,
	  VendorVenues.VegDinnerCost as cost2,
	  VendorVenues.NonVegDinnerCost as cost3,
	  VendorVenues.MaxOrder,VendorVenues.MinOrder,(case when VendorVenues.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorVenues.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorVenues] inner join [dbo].[Vendormasters] on VendorVenues.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
	if(@nType='Catering')
	begin
	 select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	 VendorsCaterings.Veg as cost,
	 VendorsCaterings.NonVeg as cost1,
	 0.00 as cost2,
	 0.00 as cost3,
	 VendorsCaterings.MaxOrder,VendorsCaterings.MinOrder,(case when  VendorsCaterings.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsCaterings.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsCaterings] inner join [dbo].[Vendormasters] on VendorsCaterings.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='Photography')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsPhotographies.StartingPrice as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  '0' as MaxOrder,'10' as MinOrder,(case when VendorsPhotographies.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsPhotographies.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsPhotographies] inner join [dbo].[Vendormasters] on VendorsPhotographies.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='InvitationCard')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsInvitationCards.CardCost as cost,
	  VendorsInvitationCards.CardCostWithPrint as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  VendorsInvitationCards.MinOrder,'10' as MaxOrder,(case when VendorsInvitationCards.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsInvitationCards.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsInvitationCards] inner join [dbo].[Vendormasters] on VendorsInvitationCards.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='Decorator')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsDecorators.StartingPrice as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  '0' as MaxOrder,'10' as MinOrder,(case when VendorsDecorators.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsDecorators.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsDecorators] inner join [dbo].[Vendormasters] on VendorsDecorators.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='Entertainment')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsEntertainments.StartingPrice as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  '0' as MaxOrder,'10' as MinOrder,(case when VendorsEntertainments.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsEntertainments.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsEntertainments] inner join [dbo].[Vendormasters] on VendorsEntertainments.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='Eventorganiser')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsEventOrganisers.StartingPrice as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  '0' as MaxOrder,'10' as MinOrder,(case when VendorsEventOrganisers.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsEventOrganisers.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsEventOrganisers] inner join [dbo].[Vendormasters] on VendorsEventOrganisers.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='BeautyService')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsBeautyServices.PartyMakeupStartsFrom as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  '0' as MaxOrder,'10' as MinOrder,(case when VendorsBeautyServices.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsBeautyServices.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsBeautyServices] inner join [dbo].[Vendormasters] on VendorsBeautyServices.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
	--	if(@nType='WeddingFavor')
	--begin
	--  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,VendorsWeddingCollections,(case when VendorsWeddingCollections.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorId  =VendorsWeddingCollections.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsWeddingCollections] inner join [dbo].[Vendormasters] on VendorsWeddingCollections.VendorMasterId=Vendormasters.Id where VendorsWeddingCollections.Id=@vid
	--end
		if(@nType='Travel')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  0.00 as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  '0' as MaxOrder,'10' as MinOrder,(case when VendorsTravelandAccomodations.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsTravelandAccomodations.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsTravelandAccomodations] inner join [dbo].[Vendormasters] on VendorsTravelandAccomodations.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='Gift')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsGifts.GiftCost as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  VendorsGifts.MinOrder,VendorsGifts.MaxOrder,(case when VendorsGifts.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsGifts.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsGifts] inner join [dbo].[Vendormasters] on VendorsGifts.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='WeddingCollection')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  0.00 as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  '0' as MaxOrder,'10' as MinOrder,(case when VendorsWeddingCollections.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsWeddingCollections.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsWeddingCollections] inner join [dbo].[Vendormasters] on VendorsWeddingCollections.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
		if(@nType='Other')
	begin
	  select Vendormasters.Id,Vendormasters.Address,Vendormasters.BusinessName,Vendormasters.City,Vendormasters.ContactNumber,Vendormasters.ContactPerson,Vendormasters.Description,Vendormasters.Landmark,Vendormasters.ServicType,Vendormasters.State,Vendormasters.ZipCode,
	  VendorsOthers.ItemCost as cost,
	  0.00 as cost1,
	  0.00 as cost2,
	  0.00 as cost3,
	  VendorsOthers.MaxOrder,VendorsOthers.MinOrder,(case when VendorsOthers.Id is not null then STUFF((SELECT ', ' + ImageName FROM VendorImages WHERE Vendorimages.VendorMasterId  =VendorsOthers.Id FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') end) as image from [dbo].[VendorsOthers] inner join [dbo].[Vendormasters] on VendorsOthers.VendorMasterId=Vendormasters.Id where Vendormasters.Id=@vid
	end
end

