USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTestimonials]    Script Date: 22/07/2016 05:47:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[SP_GetTestimonials]
as
begin
SELECT   AdminTestimonials.Id,AdminTestimonials.Name,AdminTestimonials.Email, AdminTestimonials.Description,AdminTestimonialPaths.ImagePath
FROM     AdminTestimonials
JOIN     AdminTestimonialPaths
ON       AdminTestimonialPaths.ImagePath =
         (
         SELECT  TOP 1 ImagePath 
         FROM    AdminTestimonialPaths
         WHERE   id = AdminTestimonials.id
         )
end