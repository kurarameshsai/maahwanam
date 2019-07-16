USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Others_TestimonialDetail]    Script Date: 20/07/2016 05:47:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[MaaAahwanam_Others_TestimonialDetail](@Id bigint)
as begin
select data1.Id,data1.UpdatedDate,data1.UpdatedBy,data2.FirstName,data2.LastName,data1.Email,data2.UserPhone,data2.Address,data1.Description,data3.ImagePath
from AdminTestimonials data1 
join UserDetails data2 on data1.UpdatedBy=data2.UserLoginId
join AdminTestimonialPaths data3 on data1.UpdatedBy=data3.UpdatedBy where data1.Id=@Id
end