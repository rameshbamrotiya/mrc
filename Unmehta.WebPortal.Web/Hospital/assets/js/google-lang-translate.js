function getCookie(cname) {
		var name = cname + "=";
		var ca = document.cookie.split(';');
		for (var i = 0; i < ca.length; i++) {
			var c = ca[i];
			while (c.charAt(0) == ' ') {
				c = c.substring(1);
			}
			if (c.indexOf(name) == 0) {
				return c.substring(name.length, c.length);
			}
		}
		return "";
	}
	$(document).ready(function () {
		var a = window.location;
		var url1 = 'https://dst.gujarat.gov.in';
		var url2 = 'https://dst.gujarat.gov.in/';
		var url3 = 'https://dst.gujarat.gov.in/index.htm';
			
		if(a==url1 || a==url2 || a==url3){
			$('#googleTranslatorModal').fadeIn();			
		}
		$('.closeBtn').click(function(){
			$('#googleTranslatorModal').hide();
			return false;
		});
		//alert('hi')
		
		var timeout = setInterval(function () {
			
			var options = $('select.goog-te-combo option');
			if (options.length > 0) {
				$('<label for="googleLangBox" class="displayNone">Google Language</label>').insertBefore($('select.goog-te-combo'));
				$('select.goog-te-combo').attr('id', 'googleLangBox');
				//alert('hi')
				options.each(function (e, res) {
					$(res).addClass("dev-" + e).addClass("primary");
					if ($(res).val().length > 0) {
						var li = $("<li class='skiptranslate' dev=" + e + " lang=" + $(res).val() + "></li>").text($(res).text());
						li.click(function () {
							$('select.goog-te-combo').val($(this).attr("lang"));
							/*if ("createEvent" in document) {
								var event = new Event('change');
								$('select.goog-te-combo')[0].dispatchEvent(event);
							}
							else
							$('select.goog-te-combo')[0].fireEvent("onchange");*/
							
							if ("createEvent" in document) {
								var evt = document.createEvent("HTMLEvents");
								evt.initEvent("change", false, true);
								$('select.goog-te-combo')[0].dispatchEvent(evt);
							}
							else
								$('select.goog-te-combo')[0].fireEvent("onchange");
								$("#googleTranslatorModal").hide();
						});
						$("#googleTranslatorModal > .box ul").append(li);
					}
				});
				var translateCookie = getCookie("googtrans");
				if (translateCookie == undefined || translateCookie == "" || translateCookie == "/auto/en") {
					$("#googleTranslatorModal").fadeIn();
					//$('.modal-backdrop').css("width", "100%");
					//$('.modal-backdrop').css("height", "100%");
					//$('.modal-backdrop').css("z-index", "20");
				}
				//iframe.css("display","block");
				clearInterval(timeout);
			}
		}, 500);
	});

