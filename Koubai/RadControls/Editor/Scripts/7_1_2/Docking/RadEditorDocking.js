RadEditorNamespace.Docking={CurrentDockingZone:null,PendingDockingZonesArray:[],PendingDockableObjectsArray:[],MakeDockable:function(_1,_2,_3,_4,_5,_6){
if(!_1){
return;
}
this.RadMakeDockable(_1,(null==_2?true:_2),(null==_3?true:_3),_4,_5,_6);
this.RadTryDock(_1);
},RadTryDock:function(_7){
if(_7.DockingZone){
return;
}
var _8;
var _9=_7.getAttribute("dockingzone");
if(_9){
_8=document.getElementById(_9);
}else{
if(_7.parentNode.getAttribute("docking")){
_8=_7.parentNode;
}
}
if(_8&&typeof (_8.Dock)=="function"){
var _a=parseInt(_7.getAttribute("dockingorder"));
if(isNaN(_a)){
_a=null;
}
_8.Dock(_7,_a);
}else{
if(_7.parentNode!=document.body){
if("complete"==document.readyState){
_7.parentNode.removeChild(_7);
document.body.appendChild(_7);
}
if(_7.ShowOverlay){
_7.ShowOverlay();
}
}
}
},IsDocumentDockingReady:false,PrepareDocumentForDocking:function(_b){
if(_b){
var _c=this.GetOverlayImage();
if(_c){
_c.src=_b;
}
}
if(this.IsDocumentDockingReady){
return;
}
RadEditorNamespace.Utils.AttachEventEx(document,"onmousemove",RadEditorNamespace.Docking.RadGlobalMouseMoveHandler);
RadEditorNamespace.Utils.AttachEventEx(document,"onkeydown",RadEditorNamespace.Docking.RadGlobalKeyDownHandler);
this.IsDocumentDockingReady=true;
}};
RadEditorNamespace.Docking.RadGlobalMouseMoveHandler=function(e){
if(!RadEditorNamespace.Docking||!RadEditorNamespace.Docking.CurrentDragTarget){
return;
}
if(RadEditorNamespace.Docking.CurrentDragTarget.UseInternalMove){
return;
}
if(!e){
e=window.event;
}
RadEditorNamespace.Docking.CurrentDragTarget.DoDrag(e);
if(RadEditorNamespace.Docking.CurrentDragTarget.IsMoving()){
if(null==RadEditorNamespace.Docking.CurrentDragTarget.DockingZone||RadEditorNamespace.Docking.CurrentDragTarget.UndockOnDragEnd){
RadEditorNamespace.Docking.CurrentDockingZone=null;
var _e;
var _f=RadEditorNamespace.Docking.DockingZones;
for(var i=0;i<_f.length;i++){
_e=_f[i];
if(_e.HitTest(RadEditorNamespace.Docking.CurrentDragTarget,null==RadEditorNamespace.Docking.CurrentDockingZone,e)){
if(!RadEditorNamespace.Docking.CurrentDockingZone){
RadEditorNamespace.Docking.CurrentDockingZone=_e;
}
}
}
}else{
if(!RadEditorNamespace.Docking.CurrentDragTarget.UndockOnDragEnd){
RadEditorNamespace.Docking.CurrentDragTarget.Undock(e);
}
}
}
return RadEditorNamespace.Utils.CancelEvent(e);
};
RadEditorNamespace.Docking.RadGlobalKeyDownHandler=function(e){
if(!RadEditorNamespace.Docking.CurrentDragTarget){
return;
}
if(!e){
e=window.event;
}
if(27==e.keyCode){
if(CurrentDockingZone){
RadEditorNamespace.Docking.CurrentDockingZone.HitTest(RadEditorNamespace.Docking.CurrentDragTarget,false,e);
RadEditorNamespace.Docking.CurrentDockingZone=null;
}
RadEditorNamespace.Docking.CurrentDragTarget.CancelDrag(e);
}
};;RadEditorNamespace.Docking.WrapInDockingContainer=function(_1,_2,_3,_4,_5,_6,_7){
var _8=document;
var _9=_8.createElement("table");
_9.border=0;
_9.cellSpacing=0;
_9.cellPadding=0;
_9.setAttribute("unselectable","on");
_9.setAttribute("dockable","all");
var _a=_9.insertRow(-1);
var _b=_a.insertCell(-1);
var _c=_8.createElement("span");
_c.className="RadAutoDockButton";
_c.innerHTML="&nbsp;&nbsp;&nbsp;";
_c.setAttribute("autoDock","true");
_b.appendChild(_c);
_b.innerHTML+=(_7?"&nbsp;"+_7:"");
_b.colSpan=2;
_b.setAttribute("noWrap","true");
_b.setAttribute("titleGrip","autohide");
_b.className="RadETitleGrip";
_b.parentNode.style.display="none";
var _a=_9.insertRow(-1);
_b=_a.insertCell(-1);
_b.innerHTML="&nbsp;";
_b.colSpan=2;
_b.setAttribute("topSideGrip","autohide");
_b.className="RadESideGripVertical";
_a=_9.insertRow(-1);
_b=_a.insertCell(-1);
_b.innerHTML="&nbsp;&nbsp;&nbsp;";
_b.setAttribute("leftSideGrip","autohide");
_b.className="RadESideGripHorizontal";
_b=_a.insertCell(-1);
_b.appendChild(_1);
_9.RenderHorizontal=_3;
_9.RenderVertical=_4;
_9.HorizontalClassName=_5;
_9.VerticalClassName=_6;
var _d=document.all&&!window.opera?"inline":"";
_9.setAttribute("display",_d);
if(document.all&&!window.opera){
_9.style.display="inline";
}else{
_9.setAttribute("style","float:left");
}
return _9;
};
RadEditorNamespace.Docking.DisposeDockingObjects=function(){
try{
var _e=RadEditorNamespace.Docking.RadDockingObjects.length;
for(var _f=0;_f<_e;_f++){
var obj=RadEditorNamespace.Docking.RadDockingObjects[_f];
obj.DockingZone=null;
obj.LastDockingZone=null;
var arr=RadEditorNamespace.Utils.GetElementsByAttributeName(obj,"autodock",true);
for(var i=0;i<arr.length;i++){
arr[i].DockableObject=null;
arr[i].onclick=null;
}
var _13=[obj.rows[0].cells[0],obj.rows[1].cells[0],obj.rows[2].cells[0]];
for(var i=0;i<_13.length;i++){
delTd=_13[i];
if(delTd){
delTd.style.display="";
delTd.parentNode.deleteCell(delTd);
}
}
obj.onmousemove=null;
obj.onmouseout=null;
obj.onmousedown=null;
obj.LeftSideGrip=null;
obj.TopSideGrip=null;
obj.Title=null;
obj.RenderHorizontal=null;
obj.RenderVertical=null;
obj.HorizontalClassName=null;
obj.VerticalClassName=null;
obj.CanDockTo=null;
obj.CancelDrag=null;
obj.AutoDock=null;
obj.EndDrag=null;
obj.FixLayout=null;
obj.GetRect=null;
obj.GripHitTest=null;
obj.Hide=null;
obj.HideOverlay=null;
obj.Initialize=null;
obj.IsDocked=null;
obj.IsMoving=null;
obj.IsOverlayVisible=null;
obj.IsResizing=null;
obj.IsVisible=null;
obj.Nove=null;
obj.NoveBy=null;
obj.NoveTo=null;
obj.OnDragEnd=null;
obj.OnHide=null;
obj.OnShow=null;
obj.SetOnTop=null;
obj.SetPosition=null;
obj.SetSize=null;
obj.Show=null;
obj.ShowGrip=null;
obj.ShowOverlay=null;
obj.StartDrag=null;
obj.Undock=null;
obj.Overlay=null;
}
RadEditorNamespace.Docking.RadDockingObjects=null;
}
catch(e){
}
};
RadEditorNamespace.Docking.RadDockingObjects=[];
RadEditorNamespace.Docking.RadMakeDockable=function(obj,_15,_16,_17,_18,_19){
if(!obj||obj.Undock){
return;
}
this.MakeMoveable(obj,_15,_16,_17,(true==_19),_19);
RadEditorNamespace.Utils.ExtendObject(obj,RadEditorNamespace.Docking.RadDockableObject);
obj.UndockOnDragEnd=_15;
if(!obj.RenderVertical){
var _1a=obj.getAttribute("renderVertical");
obj.RenderVertical=(_1a?_1a:RadEditorNamespace.Docking.RenderVertical);
}
if(!obj.RenderHorizontal){
var _1a=obj.getAttribute("renderHorizontal");
obj.RenderHorizontal=(_1a?_1a:RadEditorNamespace.Docking.RenderHorizontal);
}
obj.DockingZone=null;
if(obj.Initialize){
obj.Initialize();
}
var _1b=RadEditorNamespace.Docking.RadDockingObjects;
_1b[_1b.length]=obj;
};
RadEditorNamespace.Docking.RadDockableObject={OnDock:null,OnUndock:null,UndockOnDragEnd:true,OnDragEnd:function(e){
if(this.UndockOnDragEnd){
this.Undock(e);
}
if(RadEditorNamespace.Docking.CurrentDockingZone){
RadEditorNamespace.Docking.CurrentDockingZone.Dock(this);
RadEditorNamespace.Docking.CurrentDockingZone=null;
}
},CanDockTo:function(_1d){
var _1e=this.getAttribute("dockable");
if("string"==typeof (_1e)){
_1e=_1e.toLowerCase();
}
if("all"==_1e){
return true;
}else{
return (_1e==_1d.DockType.toLowerCase());
}
},OnShow:function(){
if(this.ShowOverlay&&!this.IsDocked()){
this.ShowOverlay();
}
},Docked:function(){
this.LastDockingZone=this.DockingZone;
this.EnableResize=false;
if(document.all&&"none"!=this.style.display){
this.style.display="inline";
}
if(this.HideOverlay){
this.HideOverlay();
}
this.ShowGrip(this.Title,false);
this.ShowGrip(this.LeftSideGrip,!this.IsVertical);
this.ShowGrip(this.TopSideGrip,this.IsVertical);
this.FixLayout();
if(this.OnDock){
this.OnDock();
}
},Undock:function(e){
if(!this.DockingZone){
return;
}
this.DockingZone=null;
this.EnableResize=true;
this.parentNode.removeChild(this);
this.style.position="absolute";
this.ShowGrip(this.Title,true);
this.ShowGrip(this.LeftSideGrip,false);
this.ShowGrip(this.TopSideGrip,false);
document.body.appendChild(this);
this.SetOnTop();
if(this.ShowOverlay){
this.ShowOverlay();
}
if(this.OnUndock){
this.OnUndock();
}
},AutoDock:function(){
if(!this.LastDockingZone){
return;
}
this.LastDockingZone.Dock(this);
},IsDocked:function(){
return (null!=this.DockingZone);
},FixLayout:function(){
if(null!=this.DockingZone&&RadEditorNamespace.Utils.StartsWith(this.DockingZone.DockType,"vert")&&null!=this.RenderVertical){
if(this.IsVertical){
return;
}
try{
if(this.TopSideGrip){
this.TopSideGrip.style.display="";
}
if(this.LeftSideGrip){
this.LeftSideGrip.style.display="none";
}
this.className=this.VerticalClassName;
if(typeof (this.RenderVertical)=="function"){
this.RenderVertical();
}else{
if(typeof (this.RenderVertical)=="string"){
eval(this.RenderVertical);
}
}
}
catch(ex){
}
this.IsVertical=true;
}else{
if(this.IsVertical&&null!=this.RenderHorizontal){
try{
if(this.TopSideGrip){
this.TopSideGrip.style.display="none";
}
if(this.LeftSideGrip){
this.LeftSideGrip.style.display="";
}
this.className=this.HorizontalClassName;
if(typeof (this.RenderHorizontal)=="function"){
this.RenderHorizontal();
}else{
if(typeof (this.RenderHorizontal)=="string"){
eval(this.RenderHorizontal);
}
}
}
catch(ex){
}
this.IsVertical=false;
}
}
},GripHitTest:function(e){
var _21=RadEditorNamespace.Utils.GetEventSource(e);
return (null!=_21&&(null!=_21.getAttribute("grip")||null!=_21.getAttribute("titlegrip")||null!=_21.getAttribute("topsidegrip")||null!=_21.getAttribute("leftsidegrip")));
},Initialize:function(){
var arr=RadEditorNamespace.Utils.GetElementsByAttributeName(this,"leftSideGrip",true);
if(arr.length>0){
this.LeftSideGrip=arr[0];
this.LeftSideGrip.AlwaysVisible=(arr[0].getAttribute("leftSideGrip").toLowerCase()=="visible");
}
arr=RadEditorNamespace.Utils.GetElementsByAttributeName(this,"topSideGrip",true);
if(arr.length>0){
this.TopSideGrip=arr[0];
this.TopSideGrip.AlwaysVisible=(arr[0].getAttribute("topSideGrip").toLowerCase()=="visible");
}
arr=RadEditorNamespace.Utils.GetElementsByAttributeName(this,"titleGrip",true);
if(arr.length>0){
this.Title=arr[0];
var _23=(arr[0].getAttribute("titleGrip").toLowerCase()=="visible");
if(this.Title.tagName=="TD"||this.Title.tagName=="TH"){
this.Title=this.Title.parentNode;
}
this.Title.AlwaysVisible=_23;
}
this.ShowGrip(this.Title,true);
this.ShowGrip(this.LeftSideGrip,false);
this.ShowGrip(this.TopSideGrip,false);
arr=RadEditorNamespace.Utils.GetElementsByAttributeName(this,"autodock",true);
for(var i=0;i<arr.length;i++){
arr[i].DockableObject=this;
arr[i].onclick=function(){
this.DockableObject.AutoDock();
};
}
},ShowGrip:function(_25,_26){
if(_25&&!_25.AlwaysVisible){
_25.style.display=_26?"":"none";
}
}};;RadEditorNamespace.Docking.Rectangle=function(_1,_2,_3,_4){
this.left=(null!=_1?_1:0);
this.top=(null!=_2?_2:0);
this.width=(null!=_3?_3:0);
this.height=(null!=_4?_4:0);
this.right=_1+_3;
this.bottom=_2+_4;
};
RadEditorNamespace.Docking.Rectangle.prototype.Clone=function(){
return new RadEditorNamespace.Docking.Rectangle(this.left,this.top,this.width,this.height);
};
RadEditorNamespace.Docking.Rectangle.prototype.PointInRect=function(x,y){
return (this.left<=x&&x<=(this.left+this.width)&&this.top<=y&&y<=(this.top+this.height));
};
RadEditorNamespace.Docking.Rectangle.prototype.Intersects=function(_7){
if(null==_7){
return false;
}
if(this==_7){
return true;
}
return (_7.left<this.right&&_7.top<this.bottom&&_7.right>this.left&&_7.bottom>this.top);
};
RadEditorNamespace.Docking.Rectangle.prototype.ToString=function(){
return "left:"+this.left+" "+"right:"+this.right+" "+"top:"+this.top+" "+"bottom:"+this.bottom+" "+"("+this.width+" x "+this.height+")";
};
RadEditorNamespace.Docking.Rectangle.prototype.Intersection=function(_8){
if(null==_8){
return false;
}
if(this==_8){
return this.Clone();
}
if(!this.Intersects(_8)){
return new RadEditorNamespace.Docking.Rectangle();
}
var _9=Math.max(this.left,_8.left);
var _a=Math.max(this.top,_8.top);
var _b=Math.min(this.right,_8.right);
var _c=Math.min(this.bottom,_8.bottom);
return new RadEditorNamespace.Docking.Rectangle(_9,_b,_b-_9,_c-_a);
};
RadEditorNamespace.Docking.RadGetElementRect=function(_d){
if(!_d){
_d=this;
}
var _e=0;
var _f=0;
var _10=_d.offsetWidth;
var _11=_d.offsetHeight;
while(_d.offsetParent){
_e+=_d.offsetLeft;
_f+=_d.offsetTop;
_d=_d.offsetParent;
}
if(_d.x){
_e=_d.x;
}
if(_d.y){
_f=_d.y;
}
_e=RadEditorNamespace.Utils.GetIntValue(_e,0);
_f=RadEditorNamespace.Utils.GetIntValue(_f,0);
_10=RadEditorNamespace.Utils.GetIntValue(_10,0);
_11=RadEditorNamespace.Utils.GetIntValue(_11,0);
return new RadEditorNamespace.Docking.Rectangle(_e,_f,_10,_11);
};
RadEditorNamespace.Docking.GetScrollTop=function(){
if(document.documentElement&&document.documentElement.scrollTop){
return document.documentElement.scrollTop;
}else{
return document.body.scrollTop;
}
};
RadEditorNamespace.Docking.GetScrollLeft=function(){
if(document.documentElement&&document.documentElement.scrollLeft){
return document.documentElement.scrollLeft;
}else{
return document.body.scrollLeft;
}
};;RadEditorNamespace.Docking.DockingZones=[];
RadEditorNamespace.Docking.RadRegisterDockingZone=function(_1,_2){
if(!_1){
return;
}
RadEditorNamespace.Utils.ExtendObject(_1,RadEditorNamespace.Docking.DockingZone);
if(!_2){
_2=_1.getAttribute("docking");
}
_1.DockType=(_2?_2:"horiz");
RadEditorNamespace.Docking.DockingZones.push(_1);
};
RadEditorNamespace.Docking.DockingZone={Dock:function(_3,_4){
if(this==_3.DockingZone){
return;
}
if(null==_3.getAttribute("dockable")){
alert("Error: You are trying to dock non-dockable object");
return;
}
if(!_3.CanDockTo(this)){
alert("Error: You are not allowed to dock '"+_3.id+"' to '"+this.id+"' docking zone");
return;
}
_3.DockingZone=this;
_3.parentNode.removeChild(_3);
_3.style.position="";
var _5;
if(null!=_4){
_5=this.FindPosByDockingOrder(_4);
}else{
_5=(this.HoverElement!=this?this.HoverElement:null);
}
if(_5){
this.insertBefore(_3,_5);
}else{
this.appendChild(_3);
}
this.HighlightElement(this.HoverElement,false);
this.HoverElement=null;
_3.Docked();
},HitTest:function(_6,_7,_8){
if(!_6.CanDockTo(this)){
return false;
}
if(null==_7){
_7=true;
}
var _9=_6.GetRect();
var _a=this.GetRect();
var _b=RadEditorNamespace.Docking.GetScrollLeft();
var _c=RadEditorNamespace.Docking.GetScrollTop();
var _d=_8.clientX+_b;
var _e=_8.clientY+_c;
var _f=this.GetRect().PointInRect(_d,_e);
this.HoverElement=null;
var _10;
for(var i=0;i<this.childNodes.length;i++){
_10=this.childNodes[i];
if(1!=_10.nodeType){
continue;
}
if(!_10.DockingZone){
continue;
}
if(_10==_6){
continue;
}
if(!this.HoverElement&&_f&&_10.GetRect().PointInRect(_d,_e)){
this.HoverElement=_10;
}
this.HighlightElement(_10,_7&&_10==this.HoverElement);
}
if(!this.HoverElement){
this.HoverElement=(_f?this:null);
}
this.HighlightElement(this,_7&&this==this.HoverElement);
return _f;
},HighlightElement:function(_12,_13){
if(!_12){
return;
}
if(_13&&null==_12.OldCss){
_12.OldCss=_12.style.cssText;
_12.style.border="1px dashed #666666";
}else{
if(!_13&&null!=_12.OldCss){
_12.style.cssText=_12.OldCss;
_12.OldCss=null;
}
}
},FindPosByDockingOrder:function(_14){
if(0<=_14&&_14<this.childNodes.length){
return this.childNodes[_14];
}
return null;
},GetRect:function(){
return RadEditorNamespace.Docking.RadGetElementRect(this);
}};;RadEditor.prototype.FindModuleByTitle=function(_1){
var _2;
for(var j=0;j<this.Modules.length;j++){
_2=this.Modules[j];
if(_2.Title==_1){
return _2;
}
}
return null;
};
RadEditor.prototype.InitDocking=function(){
if(!this.EnableDocking){
return;
}
var _4=this.DockingZones;
for(var _5 in _4){
var _6=_4[_5];
if(_6&&_6.tagName!=null){
RadEditorNamespace.Docking.RadRegisterDockingZone(_6);
}
}
RadEditorNamespace.Docking.PrepareDocumentForDocking(this.SkinBasePath+"Buttons/transp.gif");
};
RadEditor.prototype.SerializeCookieName="RadEditorGlobalSerializeCookie";
RadEditor.prototype.SetCookie=function(_7,_8){
_7="["+this.Id+_7+"]";
var _9=this.getOnlyCookie(this.SerializeCookieName);
var _a="";
var _b="";
if(_9){
var _c=_9.split(_7);
if(_c&&_c.length>1){
_a=_c[0];
_b=_c[1].substr(_c[1].indexOf("#")+1);
}else{
_b=_9;
}
}
var _d=new Date();
_d.setFullYear(_d.getFullYear()+10);
document.cookie=this.SerializeCookieName+"="+(_a+_7+"-"+_8+"#"+_b)+";path=/;expires="+_d.toUTCString()+";";
};
RadEditor.prototype.GetCookie=function(_e){
_e="["+this.Id+_e+"]";
var _f=this.getOnlyCookie(this.SerializeCookieName);
if(!_f){
return null;
}
var _10=null;
var _11=_f.indexOf(_e);
if(_11>=0){
var _12=_11+_e.length+1;
_10=_f.substring(_12,_f.indexOf("#",_12));
}
return _10;
};
RadEditor.prototype.getOnlyCookie=function(_13){
var _14=document.cookie.split("; ");
for(var i=0;i<_14.length;i++){
var _16=_14[i].split("=");
if(_13==_16[0]){
return _16[1];
}
}
return null;
};
RadEditor.prototype.private_Serialize=function(_17){
if(!this.EnableClientSerialize||!this.EnableDocking){
return;
}
if(_17){
if(this.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.Default)){
var str="[";
var _19=false;
var _1a;
var _1b=this.GetHtmlToolbarElements();
for(var i=0;i<_1b.length;i++){
_1a=_1b[i];
var _1d=escape(_1a.getAttribute("title"));
var tmp=this.PersistDockableObject(_1a,_1d,true);
if(tmp){
if(_19){
str+=",";
}
str+=tmp;
_19=true;
}
}
str+="]";
this.SetCookie("Toolbars",str);
}
str="[";
_19=false;
var _1f;
for(var i=0;i<this.Modules.length;i++){
_1f=this.Modules[i];
var tmp=this.PersistDockableObject(_1f.GetTopElement(),_1f.Title,_1f.IsEnabled);
if(tmp){
if(_19){
str+=",";
}
str+=tmp;
_19=true;
}
}
str+="]";
this.SetCookie("Modules",str);
}else{
if(this.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.Default)){
var str=this.GetCookie("Toolbars");
if(null!=str){
var _20,_1a;
var _21=this.GetHtmlToolbarElements();
var _22=eval(str);
for(var i=0;i<_22.length;i++){
_20=_22[i];
var _1d=unescape(_20[0]);
_1a=this.FindToolbarByTitle(_21,_1d);
if(!_1a){
continue;
}
this.RestoreDockableObject(_1a,_20);
}
}
}
var str=this.GetCookie("Modules");
if(null!=str){
var _20,_1f;
var _22=eval(str);
for(var i=0;i<_22.length;i++){
_20=_22[i];
_1f=this.FindModuleByTitle(_20[0]);
if(!_1f){
continue;
}
var _23=this.RestoreDockableObject(_1f.GetTopElement(),_20);
_1f.SetEnabled(_23);
}
}
}
};
RadEditor.prototype.FindToolbarByTitle=function(_24,_25){
for(var j=0;j<_24.length;j++){
var _27=_24[j];
if(_27.getAttribute("title")==_25){
return _27;
}
}
return null;
};
RadEditor.prototype.PersistDockableObject=function(_28,_29,_2a){
if(!_28||!_28.Undock){
return null;
}
var _2b=_28.DockingZone;
var str="[";
str+="'"+_29+"'";
var _2d=(false!=_2a)?true:false;
str+=","+_2d;
var _2e=_2b?_2b.id:"";
if(!_2b&&_28.DockingZoneId){
_2e=_28.DockingZoneId;
}
str+=","+"'"+(_2e)+"'";
if(null!=_2b){
for(var j=0;j<_2b.childNodes.length;j++){
if(_28==_2b.childNodes[j]){
str+=(","+j);
break;
}
}
}else{
str+=",";
var rc=_28.GetRect();
str+=RadEditorNamespace.Utils.Format("[{0},{1}]",rc.left,rc.top);
}
str+="]";
return str;
};
RadEditor.prototype.RestoreDockableObject=function(_31,_32){
if(!_31||!_31.Undock){
return null;
}
var _33=_32[0];
var _34=_32[1];
var _35=_32[2];
var _36=null;
var _37=null;
var top=null;
if(_35){
_36=_32[3];
}else{
_37=_32[3][0];
top=_32[3][1];
}
if(false==_34){
_31.Hide();
}else{
_31.Show();
}
if(""==_35){
_31.Undock();
_31.MoveTo(_37,top);
}else{
if(null!=(dockingZone=document.getElementById(_35))&&null!=dockingZone.Dock){
dockingZone.Dock(_31,_36);
}
}
return _34;
};;RadEditorNamespace.Docking.CurrentDragTarget=null;
RadEditorNamespace.Docking.MakeMoveable=function(_1,_2,_3,_4,_5,_6){
if(!_1||_1.Move){
return;
}
RadEditorNamespace.Utils.ExtendObject(_1,RadEditorNamespace.Docking.RadMoveableObject);
if(_4!=false){
RadEditorNamespace.Utils.ExtendObject(_1,RadEditorNamespace.Docking.ResizableObject);
_1.InitResize();
}
_1.onmouseout=function(e){
if(""!=this.style.cursor){
this.style.cursor="";
}
};
_1.onmousedown=function(e){
if(!e){
e=window.event;
}
if(document.all&&!window.opera&&e.button!=1){
return;
}
if(this.SetOnTop){
this.SetOnTop();
}
this.DragMode="";
if(this.AllowResize&&this.ResizeDir){
this.DragMode="resize";
}else{
if(this.AllowMove&&this.GripHitTest(e)){
this.DragMode="move";
}
}
if(""!=this.DragMode){
this.StartDrag(e);
}
RadEditorNamespace.Utils.CancelEvent(e);
return false;
};
_1.onmousemove=function(e){
if(!e){
e=window.event;
}
if(!this.IsResizing()&&null!=this.CalcResizeDir){
this.ResizeDir=this.CalcResizeDir(e);
this.style.cursor=this.ResizeDir;
}
if(!this.ResizeDir&&this.GripHitTest(e)){
this.style.cursor="move";
}
};
var _a=navigator.userAgent.toLowerCase();
if(_3!=false&&null!=document.all&&_a.indexOf("msie 7.0")==-1){
this.EnableOverlay(_1);
}
_1.UseDragHelper=(_2!=false);
_1.UseInternalMove=(false!=_5);
};
RadEditorNamespace.Docking.RadMoveableObject={OnDragStart:null,OnDragEnd:null,AllowMove:true,AllowResize:true,UseDragHelper:true,UseInternalMove:true,StartDrag:function(_b){
this.MouseX=_b.clientX;
this.MouseY=_b.clientY;
RadEditorNamespace.Utils.AttachEventEx(document,"onmouseup",RadEditorNamespace.Docking.GeneralMouseUp);
if(this.UseInternalMove){
RadEditorNamespace.Utils.AttachEventEx(document,"onmousemove",RadEditorNamespace.Docking.GeneralMouseMove);
RadEditorNamespace.Utils.AttachEventEx(document,"onkeydown",RadEditorNamespace.Docking.GeneralKeyDown);
}
RadEditorNamespace.Docking.CurrentDragTarget=this;
if(this.UseDragHelper){
this.DragHelper=RadEditorNamespace.Docking.GetGlobalDragHelper();
this.DragHelper.Show(this.GetRect());
}
if(this.OnDragStart){
this.OnDragStart(_b);
}
RadEditorNamespace.Docking.ShowOverlayImage(this);
window.status="Hit Esc to cancel";
},EndDrag:function(_c){
if(this.DragHelper){
var rc=this.DragHelper.GetRect();
this.MoveTo(rc.left,rc.top);
if("resize"==this.DragMode){
this.SetSize(rc.width,rc.height);
}
}
this.CancelDrag(_c);
if(this.OnDragEnd){
this.OnDragEnd(_c);
}
},CancelDrag:function(_e){
RadEditorNamespace.Docking.CurrentDragTarget=null;
RadEditorNamespace.Docking.HideOverlayImage();
RadEditorNamespace.Utils.DetachEventEx(document,"onmouseup",RadEditorNamespace.Docking.GeneralMouseUp);
if(this.UseInternalMove){
RadEditorNamespace.Utils.DetachEventEx(document,"onmousemove",RadEditorNamespace.Docking.GeneralMouseMove);
RadEditorNamespace.Utils.DetachEventEx(document,"onkeydown",RadEditorNamespace.Docking.GeneralKeyDown);
}
if(this.DragHelper){
this.DragHelper.Hide();
this.DragHelper=null;
}
this.DragMode="";
window.status="";
if(this.Tooltip){
this.Tooltip.Hide();
}
},DoDrag:function(_f){
switch(this.DragMode){
case "move":
this.Move(_f);
break;
case "resize":
this.Resize(_f);
break;
}
this.MouseX=_f.clientX;
this.MouseY=_f.clientY;
},GripHitTest:function(_10){
var _11=RadEditorNamespace.Utils.GetEventSource(_10);
return (null!=_11&&null!=_11.getAttribute("grip"));
},Move:function(_12){
var dX=_12.clientX-this.MouseX;
var dY=_12.clientY-this.MouseY;
if(this.DragHelper){
this.DragHelper.MoveBy(dX,dY);
}else{
this.MoveBy(dX,dY);
}
},MoveBy:function(dX,dY){
if(!this.Left){
this.Left=parseInt(this.style.left);
}
if(!this.Top){
this.Top=parseInt(this.style.top);
}
this.MoveTo(this.Left+dX,this.Top+dY);
},MoveTo:function(x,y){
this.Left=x;
this.Top=y;
this.style.position="absolute";
this.style.left=this.Left+"px";
this.style.top=this.Top+"px";
if(this.NeedOverlay){
this.SetOverlayIframe();
this.NeedOverlay=false;
}
if(this.Overlay){
if(this.Overlay.style.display=="none"){
}
this.Overlay.style.top=this.style.top;
this.Overlay.style.left=this.style.left;
}
},SetSize:function(_19,_1a){
_19=parseInt(_19);
if(!isNaN(_19)&&_19>=0){
this.style.width=_19+"px";
if(this.Overlay){
this.Overlay.style.width=_19+"px";
}
}
_1a=parseInt(_1a);
if(!isNaN(_1a)&&_1a>=0){
this.style.height=_1a+"px";
if(this.Overlay){
this.Overlay.style.height=_1a+"px";
}
}
if(this.OnResize&&"function"==typeof (this.OnResize)){
this.OnResize();
}
},GetRect:function(){
if(this==RadEditorNamespace.Docking.CurrentDragTarget&&this.DragHelper&&this.DragHelper.IsVisible()){
return RadEditorNamespace.Docking.RadGetElementRect(this.DragHelper);
}else{
return RadEditorNamespace.Docking.RadGetElementRect(this);
}
},SetPosition:function(_1b){
if(_1b){
this.MoveTo(_1b.left,_1b.top);
this.SetSize(_1b.width,_1b.height);
}
},SetOnTop:function(){
var _1c=0;
var _1d=0;
var _1e=this.parentNode.childNodes;
var _1f;
for(var i=0;i<_1e.length;i++){
_1f=_1e[i];
if(1!=_1f.nodeType){
continue;
}
_1d=parseInt(_1f.style.zIndex);
if(_1d>_1c){
_1c=_1d;
}
}
this.style.zIndex=_1c+1;
},Show:function(_21){
if(this.IsVisible()){
return;
}
this.style.display=this.OldDisplayMode?this.OldDisplayMode:"";
if(null!=_21){
this.SetPosition(_21);
}
this.SetOnTop();
if(this.OnShow){
this.OnShow();
}
},Hide:function(){
if(!this.IsVisible()){
return;
}
this.OldDisplayMode=this.style.display;
this.style.display="none";
if(this.OnHide){
this.OnHide();
}
},OnShow:function(){
if(this.ShowOverlay){
this.ShowOverlay();
}
},OnHide:function(){
if(this.HideOverlay){
this.HideOverlay();
}
},IsVisible:function(){
return (this.style.display!="none");
},IsResizing:function(){
return ("resize"==this.DragMode);
},IsMoving:function(){
return ("move"==this.DragMode);
}};
RadEditorNamespace.Docking.GeneralMouseUp=function(_22){
if(!RadEditorNamespace.Docking.CurrentDragTarget){
return;
}
if(!_22){
_22=window.event;
}
RadEditorNamespace.Docking.CurrentDragTarget.EndDrag(_22);
};
RadEditorNamespace.Docking.GeneralMouseMove=function(_23){
if(!RadEditorNamespace.Docking.CurrentDragTarget){
return;
}
if(!_23){
_23=window.event;
}
RadEditorNamespace.Docking.CurrentDragTarget.DoDrag(_23);
RadEditorNamespace.Utils.CancelEvent(_23);
};
RadEditorNamespace.Docking.GeneralKeyDown=function(_24){
if(!RadEditorNamespace.Docking.CurrentDragTarget){
return;
}
if(!_24){
_24=window.event;
}
if(27==_24.keyCode){
RadEditorNamespace.Docking.CurrentDragTarget.CancelDrag(_24);
}
};
RadEditorNamespace.Docking.GlobalDragHelper=null;
RadEditorNamespace.Docking.GetGlobalDragHelper=function(){
if(RadEditorNamespace.Docking.GlobalDragHelper){
return RadEditorNamespace.Docking.GlobalDragHelper;
}
var _25=document.createElement("DIV");
document.body.appendChild(_25);
_25.setAttribute("style","-moz-opacity:0.3");
_25.style.border="1px dashed gray";
_25.style.backgroundColor="#cccccc";
_25.style.filter="progid:DXImageTransform.Microsoft.Alpha(opacity=50)";
_25.style.margin="0px 0px 0px 0px";
_25.style.padding="0px";
_25.style.position="absolute";
_25.style.top=10;
_25.style.left=10;
_25.style.width=100;
_25.style.height=100;
_25.style.zIndex=50000;
_25.style.overflow="hidden";
_25.style.display="none";
RadEditorNamespace.Docking.MakeMoveable(_25,false,false,true);
RadEditorNamespace.Docking.GlobalDragHelper=_25;
return _25;
};
RadEditorNamespace.Docking.EnableOverlay=function(obj){
obj.SetOverlayIframe=function(){
var frm=document.createElement("IFRAME");
frm.src="javascript:false";
frm.frameBorder=0;
frm.scrolling="no";
frm.style.overflow="hidden";
frm.style.display="inline";
frm.style.position="absolute";
try{
var _28=this.GetRect();
frm.style.width=_28.width;
frm.style.height=_28.height;
frm.style.left=_28.left;
frm.style.top=_28.top;
}
catch(ex){
}
this.parentNode.insertBefore(frm,this);
this.Overlay=frm;
};
obj.ShowOverlay=function(){
if(this.Overlay){
this.parentNode.insertBefore(this.Overlay,this);
this.Overlay.style.display="inline";
this.Overlay.style.position="absolute";
var _29=this.GetRect();
this.Overlay.style.width=_29.width;
this.Overlay.style.height=_29.height;
this.Overlay.style.left=_29.left;
this.Overlay.style.top=_29.top;
}
};
obj.HideOverlay=function(){
if(null!=this.Overlay&&null!=this.Overlay.parentNode){
this.Overlay.parentNode.removeChild(this.Overlay);
this.Overlay.style.display="none";
}
};
obj.IsOverlayVisible=function(){
return (this.Overlay&&this.Overlay.style.display!="none");
};
obj.NeedOverlay=true;
};
RadEditorNamespace.Docking.OverlayImage=null;
RadEditorNamespace.Docking.GetOverlayImage=function(){
if(!RadEditorNamespace.Docking.OverlayImage){
var img=document.createElement("IMG");
img.style.display="none";
img.setAttribute("unselectable","on");
var _2b=function(){
return false;
};
img.onselectstart=_2b;
img.ondragstart=_2b;
img.onmouseover=_2b;
img.onmousemove=_2b;
RadEditorNamespace.Docking.OverlayImage=img;
}
return RadEditorNamespace.Docking.OverlayImage;
};
RadEditorNamespace.Docking.ShowOverlayImage=function(_2c){
var _2d=this.GetOverlayImage();
if(_2d){
document.body.appendChild(_2d);
_2d.style.position="absolute";
_2d.style.display="";
_2d.style.left=_2d.style.top="0px";
_2d.style.width=parseInt(window.screen.width)-1;
_2d.style.height=parseInt(window.screen.height)-1;
}
};
RadEditorNamespace.Docking.HideOverlayImage=function(){
var _2e=this.GetOverlayImage();
if(_2e){
_2e.parentNode.removeChild(_2e);
_2e.style.display="none";
}
};;RadEditorNamespace.Docking.ThresholdX=5;
RadEditorNamespace.Docking.ThresholdY=5;
RadEditorNamespace.Docking.ResizableObject={EnableResize:true,CalcResizeDir:function(_1,_2,_3){
if(!this.EnableResize){
return "";
}
var _4=_1.srcElement?_1.srcElement:_1.target;
if(_4!=this){
return "";
}
var rc=this.GetRect();
var _6="";
if(null==_2){
_2=RadEditorNamespace.Docking.ThresholdX;
}
if(null==_3){
_3=RadEditorNamespace.Docking.ThresholdY;
}
var _7,_8;
if(null!=_1.offsetY){
_7=_1.offsetX;
_8=_1.offsetY;
}else{
if(null!=_1.layerY){
_7=_1.layerX;
_8=_1.layerY;
}
}
if(_8<=_3&&this.AllowNorth){
_6+="n";
}else{
if((rc.height-_8)<=_3&&this.AllowSouth){
_6+="s";
}
}
if(_7<=_2&&this.AllowWest){
_6+="w";
}else{
if((rc.width-_7)<=_2&&this.AllowEast){
_6+="e";
}
}
return (""!=_6?(_6+"-resize"):"");
},Resize:function(_9){
var dX=_9.clientX-this.MouseX;
var dY=_9.clientY-this.MouseY;
this.style.cursor=this.ResizeDir;
switch(this.ResizeDir){
case "n-resize":
this.Inflate(0,dY,null,null);
break;
case "s-resize":
this.Inflate(0,0,0,dY);
break;
case "w-resize":
this.Inflate(dX,0,null,null);
break;
case "e-resize":
this.Inflate(0,0,dX,0);
break;
case "ne-resize":
this.Inflate(0,dY,dX,null);
break;
case "nw-resize":
this.Inflate(dX,dY,null,null);
break;
case "se-resize":
this.Inflate(0,0,dX,dY);
break;
case "sw-resize":
this.Inflate(dX,0,null,dY);
break;
default:
break;
}
},Inflate:function(_c,_d,_e,_f){
var rc=this.GetRect();
var top=rc.top+_d;
var _12=rc.left+_c;
if(top<0){
_d=-rc.top;
}
if(_12<0){
_c=-rc.left;
}
top=rc.top+_d;
_12=rc.left+_c;
if(null==_e){
_e=-_c;
}
if(null==_f){
_f=-_d;
}
var _13=rc.width+_e;
var _14=rc.height+_f;
_13=Math.max(this.MinWidth,_13);
_13=Math.min(this.MaxWidth,_13);
_14=Math.max(this.MinHeight,_14);
_14=Math.min(this.MaxHeight,_14);
var _15=(this.DragHelper?this.DragHelper:this);
if(rc.width!=_13){
_15.MoveBy(_c,0);
_15.SetSize(_13,null);
}
if(rc.height!=_14){
_15.MoveBy(0,_d);
_15.SetSize(null,_14);
}
},SetResizeDirs:function(_16){
this.AllowNorth=(-1!=_16.indexOf("n"));
this.AllowSouth=(-1!=_16.indexOf("s"));
this.AllowEast=(-1!=_16.indexOf("e"));
this.AllowWest=(-1!=_16.indexOf("w"));
},InitResize:function(){
var _17=this.getAttribute("resize");
if("string"==typeof (_17)){
_17=_17.toLowerCase();
}else{
_17="nsew";
}
this.SetResizeDirs(_17);
this.MinWidth=RadEditorNamespace.Utils.GetIntValue(this.getAttribute("minWidth"));
this.MaxWidth=RadEditorNamespace.Utils.GetIntValue(this.getAttribute("maxWidth"),100000);
this.MinHeight=RadEditorNamespace.Utils.GetIntValue(this.getAttribute("minHeight"));
this.MaxHeight=RadEditorNamespace.Utils.GetIntValue(this.getAttribute("maxHeight"),100000);
}};;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
