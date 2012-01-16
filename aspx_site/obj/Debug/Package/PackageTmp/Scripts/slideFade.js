/*
 * Slide Fade Toggle plugin
 * Matthew Andrews 2011
 * Made in Shanghai, China
 */
jQuery.fn.slideFade=function(method,settings,callback){if(!method){var method='toggle';}
if(!callback){var callback=function(){}}
var settings=jQuery.extend({speed:500,easing:"swing"},settings);var c=this
var displayState=c.css('display');if((displayState=='none'&&method=='show')||(method=='toggle'&&displayState=='none')){c.css({'display':'none','opacity':0}).animate({opacity:0,height:'show'},settings.speed/2,settings.easing,function(){c.animate({opacity:1,},settings.speed/2,settings.easing,callback);});}else if((method=='hide'&&displayState!='none')||(method=='toggle'&&displayState!='none')){c.css({'opacity':1}).animate({opacity:0},settings.speed/2,settings.easing,function(){c.css({'display':displayState,'opacity':0}).animate({height:'hide'},settings.speed/2,settings.easing,callback);});}else{if(method=='show'){c.show(0,callback);}else{c.hide(0,callback);}}
return c;};