$(function() {

	// grab the initial top offset of the navigation 
	var sticky_navigation_offset_top = $('.sticky-navigation').offset().top;
	var sticky_navigation_offset_top2 = 270;
	
	// our function that decides weather the navigation bar should have "fixed" css position or not.
	var sticky_navigation = function(){
		var scroll_top = $(window).scrollTop(); // our current vertical position from the top
		
		// if we've scrolled more than the navigation, change its position to fixed to stick to top, otherwise change it back to relative
		if (scroll_top > sticky_navigation_offset_top) { 
			$('.sticky-navigation').css({ 'position': 'fixed', 'top':0, 'left':0});
			$('.logo-small').css({ 'display': 'block' });
			$('.logo1').css({ 'display': 'none' });
			$('.sticky-navigation .menu').addClass('MenuDiv');
		} else {
			$('.sticky-navigation').css({ 'position': 'relative', 'background':'none', 'border-bottom':'0' });
			$('.logo1').css({ 'display': 'block' });
			$('.logo-small').css({ 'display': 'none' });
			$('.sticky-navigation .menu').removeClass('MenuDiv');
		}  
		
		if (scroll_top > sticky_navigation_offset_top2) { 
			$('.sticky-navigation2').css({ 'position': 'fixed', 'top':'0', 'left':'auto' });
			
		} else {
			$('.sticky-navigation2').css({ 'position': 'relative' });
		}  
		//alert(sticky_navigation_offset_top); 
	};
	
	// run our function on load
	sticky_navigation();
	
	// and run it again every time you scroll
	$(window).scroll(function() {
		 sticky_navigation();
	});
	
	// NOT required:
	// for this demo disable all links that point to "#"
	$('a[href="#"]').click(function(event){ 
		event.preventDefault(); 
	});
	
});




// Menu SlideDown Effect
$(document).ready(function(){
	
	$('.menu li').hover(function(){
		$('.submenu', this).slideUp(0).stop(true, true).slideDown(500);
	}, function(){
		$('.submenu', this).css("display", "block").stop(true, true).delay(1).slideUp(0);
	});
	
	
	$(".menu li a").click(function(){
		//$(this).next('.subdiv').slideToggle(); 
	});    

});