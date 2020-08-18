// JavaScript Document
$(document).ready(function() {
// Tao 2 mang chua ten ngay thang
var monthNames = [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" ];
var dayNames= ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"]
 
// Tao moi doi tuong Date()
var newDate = new Date();
// Lay gia tri thoi gian hien tai
newDate.setDate(newDate.getDate());
// Xuat ngay thang, nam
$('#Date').html(dayNames[newDate.getDay()] + " " + newDate.getDate() + ' ' + monthNames[newDate.getMonth()] + ' ' + newDate.getFullYear());
 
setInterval( function() {
    // lay gia tri giay trong doi tuong Date()
    var seconds = new Date().getSeconds();
    // Chen so 0 vao dang truoc gia tri giay
    $("#sec").html(( seconds < 10 ? "0" : "" ) + seconds);
    },1000);
 
setInterval( function() {
    // Tuong tu lay gia tri phut
    var minutes = new Date().getMinutes();
    // Chen so 0 vao dang truoc gia tri phut neu gia tri hien tai nho hon 10
    $("#min").html(( minutes < 10 ? "0" : "" ) + minutes);
    },1000);
 
setInterval( function() {
    // Lay gia tri gio hien tai
    var hours = new Date().getHours();
    // Chen so 0 vao truoc gia tri gio neu gia tri nho hon 10
    $("#hours").html(( hours < 10 ? "0" : "" ) + hours);
    }, 1000);
});
// Range
$(function() {
    $( "#slider-range" ).slider({
      range: true,
      min: 0,
      max: 500,
      values: [ 75, 300 ],
      slide: function( event, ui ) {
        $( "#amount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
      }
    });
    $( "#amount" ).val( "$" + $( "#slider-range" ).slider( "values", 0 ) +
      " - $" + $( "#slider-range" ).slider( "values", 1 ) );
  });
//zoom

