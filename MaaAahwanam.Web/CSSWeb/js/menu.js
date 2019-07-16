(function ($) {
    $.fn.meanmenu = function (options) {
        var defaults = {
			meanMenuTarget: jQuery(this), // Target the current HTML markup you wish to replace
            //meanMenuContainer: 'body', // Choose where meanmenu will be placed within the HTML
            meanMenuClose: "X", // single character you want to represent the close menu button
            meanMenuCloseSize: "18px", // set font size of close button
            meanMenuOpen: "<span /><span /><span />", // text/markup you want when menu is closed
            meanRevealPosition: "right", // left right or center positions
            meanRevealPositionDistance: "0", // Tweak the position of the menu
            meanRevealColour: "", // override CSS colours for the reveal background
            meanRevealHoverColour: "", // override CSS colours for the reveal hover
            meanScreenWidth: "767", // set the screen width you want meanmenu to kick in at
            meanNavPush: "", // set a height here in px, em or % if you want to budge your layout now the navigation is missing.
            meanShowChildren: true, // true to show children in the menu, false to hide them
            meanExpandableChildren: true, // true to allow expand/collapse children
            meanExpand: "+", // single character you want to represent the expand for ULs
            meanContract: "-", // single character you want to represent the contract for ULs
            meanRemoveAttrs: false, // true to remove classes and IDs, false to keep them
            onePage: false, // set to true for one page sites
            removeElements: "", // set to hide page elements
			shrinkTarget: 'body'		
			
           
        };
        var options = $.extend(defaults, options);

        // get browser width
        currentWidth = jQuery(window).width();

        return this.each(function () {
			var meanMenu = options.meanMenuTarget;
            var meanContainer = options.meanMenuContainer;
            var meanReveal = options.meanReveal;
            var meanMenuClose = options.meanMenuClose;
            var meanMenuCloseSize = options.meanMenuCloseSize;
            var meanMenuOpen = options.meanMenuOpen;
            var meanRevealPosition = options.meanRevealPosition;
            var meanRevealPositionDistance = options.meanRevealPositionDistance;
            var meanRevealColour = options.meanRevealColour;
            var meanRevealHoverColour = options.meanRevealHoverColour;
            var meanScreenWidth = options.meanScreenWidth;
            var meanNavPush = options.meanNavPush;
			var meanTarget = jQuery(this);
            var meanRevealClass = ".meanmenu-reveal";
            var meanShowChildren = options.meanShowChildren;
            var meanExpandableChildren = options.meanExpandableChildren;
            var meanExpand = options.meanExpand;
            var meanContract = options.meanContract;
            var meanRemoveAttrs = options.meanRemoveAttrs;
            var onePage = options.onePage;
            var removeElements = options.removeElements;
			var shrinkTarget = options.shrinkTarget;
            meanMenuInitiated = false;
			
           
            //detect known mobile/tablet usage
            if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i)) || (navigator.userAgent.match(/Android/i)) || (navigator.userAgent.match(/Blackberry/i)) || (navigator.userAgent.match(/Windows Phone/i))) {
                var isMobile = true;
            }

            function meanCentered() {
                if (meanRevealPosition == "center") {
                    var newWidth = jQuery(window).width();
                    var meanCenter = ( (newWidth/2)-22 )+"px";
                    meanRevealPos = "left:" + meanCenter + ";right:auto;";

                    if (!isMobile) {                    
                        jQuery('.meanmenu-reveal').css("left",meanCenter); 
                    } else {
                        jQuery('.meanmenu-reveal').animate({
                            left: meanCenter
                        });
                    }
                }
            }

            var menuOn = false;
            var meanMenuExist = false;

            if (meanRevealPosition == "right") {
                meanRevealPos = "right:" + meanRevealPositionDistance + ";left:auto;";
            }
            if (meanRevealPosition == "left") {
                meanRevealPos = "left:" + meanRevealPositionDistance + ";right:auto;";
            } 
            // run center function  
            meanCentered();

            // set all styles for mean-reveal
            meanStyles = "background:"+meanRevealColour+";color:"+meanRevealColour+";"+meanRevealPos;

            function meanInner(navreveal) {
                // get last class name
                navOpen = navreveal.attr('class').split(' ').slice(-1);
                if (navOpen == "meanclose") {
                    navreveal.html(meanMenuClose);
                } else {
                    navreveal.html(meanMenuOpen);
                }
            }

            //re-instate original nav (and call this on window.width functions)
            function meanOriginal() {
                jQuery(shrinkTarget).find('.mean-bar,.mean-push').remove();
                jQuery(shrinkTarget).removeClass("mean-container");
                jQuery(meanMenu).show();
                menuOn = false;
                meanMenuExist = false;
				jQuery(removeElements).removeClass('mean-remove');
            }
			
			

            //navigation reveal 
            function showMeanMenu() {
                if (currentWidth <= meanScreenWidth) {
					 jQuery(removeElements).addClass('mean-remove'); 
                    meanMenuExist = true;
                    // add class to body so we don't need to worry about media queries here, all CSS is wrapped in '.mean-container'
                    var current = jQuery(shrinkTarget);
                    current.addClass("mean-container");
                    current.prepend('<div class="mean-bar"><a href="#nav" class="meanmenu-reveal" style="'+meanStyles+'">Show Navigation</a><nav class="mean-nav"></nav></div>');

                    //push meanMenu navigation into .mean-nav
                    var meanMenuContents = jQuery(meanMenu).html();
                    current.find('.mean-nav').html(meanMenuContents);

                    // remove all classes from EVERYTHING inside meanmenu nav
                    if(meanRemoveAttrs) {
                        current.find('nav.mean-nav *').each(function(index) {
                            jQuery(this).removeAttr("class");
                            jQuery(this).removeAttr("id");
                        });
                    }

                    // push in a holder div (this can be used if removal of nav is causing layout issues)
                    jQuery(meanMenu).before('<div class="mean-push" />');
                    jQuery('.mean-push').css("margin-top",meanNavPush);

                    // hide current navigation and reveal mean nav link
                    jQuery(meanMenu).hide();
                    current.find(".meanmenu-reveal").show();

                    // turn 'X' on or off 
                    current.find(meanRevealClass).html(meanMenuOpen);
                    var navreveal = current.find(meanRevealClass);

                    //hide mean-nav ul
                    current.find('.mean-nav ul').hide();
                    
					// hide sub nav
	                   if(meanShowChildren) {
	                   		// allow expandable sub nav(s)
	                       if(meanExpandableChildren){
		                       jQuery('.mean-nav ul ul').each(function() {
		                           if(jQuery(this).children().length){
		                               jQuery(this,'li:first').parent().append('<a class="mean-expand" href="#" style="font-size: '+ meanMenuCloseSize +'">'+ meanExpand +'</a>');                               
		                           }
		                       });
		                       jQuery('.mean-expand').on("click",function(e){
		                       		e.preventDefault();
		                       	   if (jQuery(this).hasClass("mean-clicked")) {
		                       	   		jQuery(this).text(meanExpand);
		                               jQuery(this).prev('ul').slideUp(300, function(){});
		                           } else {
		                           		jQuery(this).text(meanContract);
		                           		jQuery(this).prev('ul').slideDown(300, function(){});
		                           }   
		                           jQuery(this).toggleClass("mean-clicked"); 
		                       });     
	                       } else {
	                           jQuery('.mean-nav ul ul').show();   
	                       }
	                   } else {
	                       jQuery('.mean-nav ul ul').hide();
	                   }
					   
                    navreveal.removeClass("meanclose");
                    jQuery(navreveal).click(function(){
                        if(menuOn == false) {
                            navreveal.toggleClass("meanclose");
                            navreveal.css("text-align", "center");
                            navreveal.css("text-indent", "0");
                            navreveal.css("font-size", meanMenuCloseSize);
                            meanInner(navreveal);
                            current.find('.mean-nav ul').slideDown();
							current.find('.mean-nav ul ul').hide();  
                            menuOn = true;
                        } else {
                            navreveal.html(meanMenuOpen);
                            navreveal.toggleClass("meanclose");
                            meanInner(navreveal);
                            current.find('.mean-nav ul').slideUp();
                            menuOn = false;
                        }    
                    });


                } else {
                    meanOriginal();
                }   
            } 

            showMeanMenu();
            if (!meanMenuInitiated){
                meanMenuInitiated = true;
                if (!isMobile) {
                    //reset menu on resize above meanScreenWidth
                    jQuery(window).resize(function () {
                        currentWidth = jQuery(window).width();
                        meanOriginal(); 
                        if (currentWidth <= meanScreenWidth) {
                            showMeanMenu();
                            meanCentered();
                        }   
                    });
                }

                // adjust menu positioning on centered navigation     
                window.onorientationchange = function() {
                    meanCentered();
                    // get browser width
                    currentWidth = jQuery(window).width();
                    meanOriginal();
                    if (currentWidth <= meanScreenWidth) {
                        if (meanMenuExist == false) {
                            showMeanMenu();
                        }
                    }
                }
            }

        });
    };
})(jQuery);