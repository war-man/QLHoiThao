jQuery(document).ready(function($) {	
  //selector đến menu cần làm việc
  var TopFixMenu = $("#fixNav");
  // dùng sự kiện cuộn chuột để bắt thông tin đã cuộn được chiều dài là bao nhiêu.
	$(window).scroll(function(){
    // Nếu cuộn được hơn 150px rồi
		if($(this).scrollTop()>150){
      // Tiến hành show menu ra    
	    TopFixMenu.show();
		}else{
      // Ngược lại, nhỏ hơn 150px thì hide menu đi.
			TopFixMenu.hide();
		}}
	)
})

$(function(){
    $('.target').scroll(function(){
        $("span").css("display", "inline").fadeOut("slow");
    });
});