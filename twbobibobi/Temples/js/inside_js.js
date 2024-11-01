// JavaScript Document


	$(document).ready(function(){
		wrapper_fn();
		bg_put_fn();	
		hd_fn();	
		nav_fn();
		in_link_change_fn();	
		section_fn();		
		footer_fn();
		right_link_pos();
		$("#wrapper").css("visibility","visible");
		$("#bg_put").css("visibility","visible");
		$("header").css("visibility","visible");
		$("nav").css("visibility","visible");
		$("#in_about_link").css("visibility","visible");
		$("#in_soho_link").css("visibility","visible");
		$("section").css("visibility","visible");
		$("footer").css("visibility","visible");	
	})
		
	 $(window).resize(function () {
		wrapper_fn();
		bg_put_fn();		
		hd_fn();
		nav_fn();
		in_link_change_fn();
		section_fn();		
		footer_fn();
		right_link_pos();		 
    }).resize();

	$(document).mousemove(function (e) {		
			var e = event || window.event;
            var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
            var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
            var mx = e.pageX || e.clientX + scrollX;
            var my = e.pageY || e.clientY + scrollY;
			$("#spot_fire").css({
				"display":"block",
				"top":my+10 ,
				"left":mx+10
			})
		});
		
	var win_w = $(window).innerWidth();
   	var win_h = $(window).innerHeight();	
	function wrapper_fn(){
		var o_w = $("#wrapper").width();
        var o_h = $("#wrapper").height();
		
	   if ((win_w / win_h) > (o_w / o_h)) {
				$("#wrapper").css({
					"width": "100%",
					"height": "100%",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2
				});
			} else {
				$("#wrapper").css({
					"width": "100%",
					"height": "100%",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2 
				});
	 }
	
	}
	function bg_put_fn(){		
		var o_w = $("#bg_put").width();
        var o_h = $("#bg_put").height();
		
	   if ((win_w / win_h) > (o_w / o_h)) {
				$("#bg_put").css({
					"width": "1920px",
					"height": "1200px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2
				});
			} else {
				$("#bg_put").css({
					"width": "1920px",
					"height": "1200px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2
				});
	 }
	}
	function hd_fn(){		
		var o_w = $("header").width();
        var o_h = $("header").height();
		
	   if ((win_w / win_h) > (o_w / o_h)) {
				$("header").css({
					"width": "1920px",
					"height": "457px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2
				});
			} else {
				$("header").css({
					"width": "1920px",
					"height": "457px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2 -371,
					"background":"url(images/new_inside_img.jpg)"
				});
	 }
	 
	}	
	function nav_fn(){
		var o_w = $("nav").width();
        var o_h = $("nav").height();
		
	   if ((win_w / win_h) > (o_w / o_h)) {
				$("nav").css({
					"width": "1040px",
					"height": "30px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2 - 158
				});
			} else {
				$("nav").css({
					"width": "1040px",
					"height": "30px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2 - 158
				});
	 }
	}
	
	function in_link_change_fn(){
		$("#in_about_link").addClass("nav_menu_jq")
		
		$("#in_about_link").click(function(){
		
				$("#in_about_link_sel").fadeIn(350);
				$("#in_about_link").addClass("nav_menu_jq");
				$("#in_soho_link_sel").fadeOut(0);	
				$("#in_soho_link").removeClass("nav_menu_jq");
		});
		$("#in_soho_link").click(function(){
			
				$("#in_soho_link_sel").fadeIn(350);
				$("#in_about_link").removeClass("nav_menu_jq");
				$("#in_about_link_sel").fadeOut(0);	
				$("#in_soho_link").addClass("nav_menu_jq");
		});
	
		
	}
	
	function section_fn(){
		var o_w = $("section").width();
        var o_h = $("section").height();
		
	   if ((win_w / win_h) > (o_w / o_h)) {
				$("section").css({
					"width": "920px",
					"height": "500px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2
				});
			} else {
				$("section").css({
					"width": "920px",
					"height": "500px",
					"margin-left": 0 - o_w / 2,
					"margin-top": 0 - o_h / 2 
				});
	 }
	
	}
	function footer_fn(){
		var art_h = $("article").height();
		var wrap_h = $("#wrapper").height();
		if(wrap_h>980){
			$("footer").css({
			"top":art_h +510
		});
		}else{
			$("footer").css({
				"top":art_h +470
			});
		}
		//alert(wrap_h)
	}
	function right_link_pos(){
		var wp_h = $("#wrapper").height()
		$("#fb_blk").css("top",wp_h/2 - 90)
		$("#twtr_blk").css("top",wp_h/2 - 30)
		$("#glg_blk").css("top",wp_h/2 + 30)
		
	}
