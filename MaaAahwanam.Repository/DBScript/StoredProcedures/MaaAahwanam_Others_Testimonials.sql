USE [MaaAahwanam]
GO
/****** Object:  StoredProcedure [dbo].[MaaAahwanam_Others_Testimonials]    Script Date: 20/07/2016 04:39:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[MaaAahwanam_Others_Testimonials]
as begin
select data1.Id,data1.UpdatedDate,data2.FirstName,data2.LastName,data2.Address,data1.Description,data1.Email,data1.Status from AdminTestimonials data1 join UserDetails data2 on data1.UpdatedBy=data2.UserLoginId
end
