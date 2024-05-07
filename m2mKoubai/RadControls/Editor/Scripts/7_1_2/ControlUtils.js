if(typeof (TelerikNamespace)=="undefined"){
var TelerikNamespace=new Object();
}
TelerikNamespace.Utils={private_EncodeSubmitContent:function(_1,_2){
var _3=new Array("%","<",">","!","\"","#","$","&","'","(",")",",",":",";","=","?","[","\\","]","^","`","{","|","}","~","+");
var _4=_1;
if(_2){
for(var i=0;i<_3.length;i++){
_4=_4.replace(new RegExp("\\x"+_3[i].charCodeAt(0).toString(16),"ig"),"%"+_3[i].charCodeAt(0).toString(16));
}
}else{
for(var i=_3.length-1;i>=0;i--){
_4=_4.replace(new RegExp("%"+_3[i].charCodeAt(0).toString(16),"ig"),_3[i]);
}
}
return _4;
},EncodePostbackContent:function(_6){
return TelerikNamespace.Utils.private_EncodeSubmitContent(_6,true);
},DecodePostbackContent:function(_7){
return TelerikNamespace.Utils.private_EncodeSubmitContent(_7,false);
},AppendStyleSheet:function(_8,_9){
var _a=(navigator.userAgent.toLowerCase().indexOf("safari")!=-1);
if(_a){
TelerikNamespace.Utils.AddStyleSheet(_9,document);
}else{
var _b=document.createElement("LINK");
_b.rel="stylesheet";
_b.type="text/css";
_b.href=_9;
document.getElementById(_8+"StyleSheetHolder").appendChild(_b);
}
},AddStyleSheet:function(_c,_d){
var _e=_d!=null?_d:document;
var _f=_e.createElement("link");
_f.setAttribute("href",_c,0);
_f.setAttribute("type","text/css");
_f.setAttribute("rel","stylesheet",0);
var _10=_e.getElementsByTagName("head")[0];
if(TelerikNamespace.Utils.DetectBrowser("safari")){
var _11=function(){
_10.appendChild(_f);
};
window.setTimeout(_11,200);
}else{
_10.appendChild(_f);
}
},DetectBrowser:function(_12){
_12=_12.toLowerCase();
if("ie"==_12){
_12="msie";
}else{
if("mozilla"==_12||"firefox"==_12){
_12="compatible";
}
}
var _13=navigator.userAgent.toLowerCase();
place=_13.indexOf(_12)+1;
if(place){
return true;
}else{
return false;
}
}};

//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
