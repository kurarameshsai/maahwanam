USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 15/07/2016 11:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetProducts]
(
@nType nvarchar(200)
)
as
begin
	if(@nType='Venue')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorVenues.VegLunchCost as Cost,(case when VendorVenues.Id is not null then (select top 1 imagename from VendorImages where VendorImages.VendorId=VendorVenues.Id) end) as image from [dbo].[VendorVenues] inner join [dbo].[Vendormasters] on VendorVenues.VendorMasterId=Vendormasters.Id
	end
	if(@nType='Catering')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsCaterings.NonVeg as cost,(case when  VendorsCaterings.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsCaterings.Id) end) as image from [dbo].[VendorsCaterings] inner join [dbo].[Vendormasters] on VendorsCaterings.VendorMasterId=Vendormasters.Id
	end
		if(@nType='Photography')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsPhotographies.StartingPrice as cost,(case when VendorsPhotographies.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsPhotographies.Id) end) as image from [dbo].[VendorsPhotographies] inner join [dbo].[Vendormasters] on VendorsPhotographies.VendorMasterId=Vendormasters.Id
	end
		if(@nType='InvitationCard')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsInvitationCards.CardCost as cost,(case when VendorsInvitationCards.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsInvitationCards.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsInvitationCards] inner join [dbo].[Vendormasters] on VendorsInvitationCards.VendorMasterId=Vendormasters.Id
	end
		if(@nType='Decorator')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsDecorators.StartingPrice as cost,(case when VendorsDecorators.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsDecorators.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsDecorators] inner join [dbo].[Vendormasters] on VendorsDecorators.VendorMasterId=Vendormasters.Id
	end
		if(@nType='Entertainment')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsEntertainments.StartingPrice as cost,(case when VendorsEntertainments.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsEntertainments.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsEntertainments] inner join [dbo].[Vendormasters] on VendorsEntertainments.VendorMasterId=Vendormasters.Id
	end
		if(@nType='Eventorganiser')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsEventOrganisers.StartingPrice as cost,(case when VendorsEventOrganisers.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsEventOrganisers.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsEventOrganisers] inner join [dbo].[Vendormasters] on VendorsEventOrganisers.VendorMasterId=Vendormasters.Id
	end
		if(@nType='BeautyService')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsBeautyServices.BridalMakeupStartsFrom as cost,(case when VendorsBeautyServices.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsBeautyServices.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsBeautyServices] inner join [dbo].[Vendormasters] on VendorsBeautyServices.VendorMasterId=Vendormasters.Id
	end
		if(@nType='WeddingFavor')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsWeddingCollections.UpdatedBy as cost,(case when VendorsWeddingCollections.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsWeddingCollections.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsWeddingCollections] inner join [dbo].[Vendormasters] on VendorsWeddingCollections.VendorMasterId=Vendormasters.Id
	end
		if(@nType='Travel')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsTravelandAccomodations.Startsfrom as cost,(case when VendorsTravelandAccomodations.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsTravelandAccomodations.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsTravelandAccomodations] inner join [dbo].[Vendormasters] on VendorsTravelandAccomodations.VendorMasterId=Vendormasters.Id
	end
		if(@nType='Gift')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsGifts.GiftCost as cost,(case when VendorsGifts.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsGifts.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsGifts] inner join [dbo].[Vendormasters] on VendorsGifts.VendorMasterId=Vendormasters.Id
	end
		if(@nType='WeddingCollection')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,0.00 as cost,(case when VendorsWeddingCollections.Id is not null then (select top 1  imagename from VendorImages where VendorImages.VendorId=VendorsWeddingCollections.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsWeddingCollections] inner join [dbo].[Vendormasters] on VendorsWeddingCollections.VendorMasterId=Vendormasters.Id
	end
		if(@nType='Other')
	begin
	  select Vendormasters.Id,Vendormasters.BusinessName,Vendormasters.Description,Vendormasters.ServicType,VendorsOthers.ItemCost as cost,(case when VendorsOthers.Id is not null then (select top 1 imagename from VendorImages where VendorImages.VendorId=VendorsOthers.Id order by VendorImages.VendorId desc) end) as image from [dbo].[VendorsOthers] inner join [dbo].[Vendormasters] on VendorsOthers.VendorMasterId=Vendormasters.Id
	end
end

