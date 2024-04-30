var RadTreeView_KeyboardHooked=false;
var RadTreeView_Active=null;
var RadTreeView_DragActive=null;
var RadTreeView_MouseMoveHooked=false;
var RadTreeView_MouseUpHooked=false;
var RadTreeView_MouseY=0;
var RadTreeViewGlobalFirstParam=null;
var RadTreeViewGlobalSecondParam=null;
var RadTreeViewGlobalThirdParam=null;
var RadTreeViewGlobalFourthParam=null;
var contextMenuToBeHidden=null;
var safariKeyDownFlag=true;
if(typeof (window.RadControlsNamespace)=="undefined"){
window.RadControlsNamespace=new Object();
}
RadControlsNamespace.AppendStyleSheet=function(_1,_2,_3){
if(!_3){
return;
}
if(!_1){
document.write("<"+"link"+" rel='stylesheet' type='text/css' href='"+_3+"' />");
}else{
var _4=document.createElement("LINK");
_4.rel="stylesheet";
_4.type="text/css";
_4.href=_3;
document.getElementById(_2+"StyleSheetHolder").appendChild(_4);
}
};
function RadTreeNode(){
this.Parent=null;
this.TreeView=null;
this.Nodes=new Array();
this.ID=null;
this.ClientID=null;
this.SignImage=null;
this.SignImageExpanded=null;
this.Image=0;
this.ImageExpanded=0;
this.Action=null;
this.Index=0;
this.Level=0;
this.Text=null;
this.Value=null;
this.Category=null;
this.NodeCss=null;
this.NodeCssOver=null;
this.NodeCssSelect=null;
this.ContextMenuName=null;
this.Enabled=true;
this.Expanded=false;
this.Checked=false;
this.Selected=false;
this.DragEnabled=1;
this.DropEnabled=1;
this.EditEnabled=1;
this.ExpandOnServer=0;
this.IsClientNode=0;
this.Attributes=new Array();
this.IsFetchingData=false;
this.CachedText="";
}
RadTreeNode.prototype.ScrollIntoView=function(){
var _5=this.TextElement();
var _6=document.getElementById(this.TreeView.Container);
_6.scrollTop=_5.offsetTop;
};
RadTreeNode.prototype.Next=function(){
var _7=(this.Parent!=null)?this.Parent.Nodes:this.TreeView.Nodes;
return (this.Index>=_7.length)?null:_7[this.Index+1];
};
RadTreeNode.prototype.Prev=function(){
var _8=(this.Parent!=null)?this.Parent.Nodes:this.TreeView.Nodes;
return (this.Index<=0)?null:_8[this.Index-1];
};
RadTreeNode.prototype.NextVisible=function(){
if(this.Expanded&&this.Nodes.length>0){
return this.Nodes[0];
}
if(this.Next()!=null){
return this.Next();
}
var _9=this;
while(_9.Parent!=null){
if(_9.Parent.Next()!=null){
return _9.Parent.Next();
}
_9=_9.Parent;
}
return null;
};
RadTreeNode.prototype.LastVisibleChild=function(_a){
var _b=_a.Nodes;
var _c=_b.length;
var _d=_b[_c-1];
var _e=_d;
if(_d.Expanded&&_d.Nodes.length>0){
_e=this.LastVisibleChild(_d);
}
return _e;
};
RadTreeNode.prototype.PrevVisible=function(){
var _f=this.Prev();
if(_f!=null){
if(_f.Expanded&&_f.Nodes.length>0){
return this.LastVisibleChild(_f);
}
return this.Prev();
}
if(this.Parent!=null){
return this.Parent;
}
return null;
};
RadTreeNode.prototype.Toggle=function(){
if(this.Enabled){
if(this.TreeView.FireEvent(this.TreeView.BeforeClientToggle,this)==false){
return;
}
(this.Expanded)?this.Collapse():this.Expand();
if(this.ExpandOnServer!=2){
this.TreeView.FireEvent(this.TreeView.AfterClientToggle,this);
}
}
};
RadTreeNode.prototype.CollapseNonParentNodes=function(){
for(var i=0;i<this.TreeView.AllNodes.length;i++){
if(this.TreeView.AllNodes[i].Expanded&&!this.IsParent(this.TreeView.AllNodes[i])){
this.TreeView.AllNodes[i].CollapseNoEffect();
}
}
};
RadTreeNode.prototype.EncodeURI=function(s){
try{
return encodeURIComponent(s);
}
catch(e){
return escape(s);
}
};
RadTreeNode.prototype.RaiseNoTreeViewOnServer=function(){
throw new Error("No RadTreeView instance has been created on the server.\n"+"Make sure that you have the control instance created.\n"+"Please review this article for additional information.");
};
RadTreeNode.prototype.FetchDataOnDemand=function(){
if(this.Checked==1){
this.Checked=true;
}
var url=this.TreeView.LoadOnDemandUrl+"&rtnClientID="+this.ClientID+"&rtnLevel="+this.Level+"&rtnID="+this.ID+"&rtnParentPosition="+this.GetParentPositions()+"&rtnText="+this.EncodeURI(this.Text)+"&rtnValue="+this.EncodeURI(this.Value)+"&rtnCategory="+this.EncodeURI(this.Category)+"&rtnChecked="+this.Checked;
var _13;
if(typeof (XMLHttpRequest)!="undefined"){
_13=new XMLHttpRequest();
}else{
_13=new ActiveXObject("Microsoft.XMLHTTP");
}
url=url+"&timeStamp="+encodeURIComponent((new Date()).getTime());
_13.open("GET",url,true);
_13.setRequestHeader("Content-Type","application/json; charset=utf-8");
var _14=this;
_13.onreadystatechange=function(){
if(_13.readyState!=4){
return;
}
var _15=_13.responseText;
if(_13.status==500){
alert("RadTreeView: Server error in the NodeExpand event handler, press ok to view the result.");
document.body.innerHTML=_15;
return;
}
var _16=_15.indexOf(",");
var _17=parseInt(_15.substring(0,_16));
var _18=_15.substring(_16+1,_17+_16+1);
var _19=_15.substring(_17+_16+1);
_14.LoadNodesOnDemand(_18,_13.status,url);
_14.ImageOn();
_14.SignOn();
_14.Expanded=true;
_14.ExpandOnServer=0;
var _1a=_14.TextElement().parentNode;
var _1b=_1a.parentNode;
switch(_14.TreeView.LoadingMessagePosition){
case 0:
case 1:
if(_1a.tagName=="A"){
_1a.firstChild.innerHTML=_14.CachedText;
}else{
_1b=_14.TextElement().parentNode;
if(_14.TextElement().innerText){
_14.TextElement().innerHTML=_14.CachedText;
}else{
_14.TextElement().innerHTML=_14.CachedText;
}
}
break;
case 2:
_1a.removeChild(document.getElementById(_14.ClientID+"Loading"));
_1b=_14.TextElement().parentNode;
break;
case 3:
_1b=_14.TextElement().parentNode;
}
if(_14.Nodes.length>0){
rtvInsertHTML(_1b,_19);
var _1c=_1b.getElementsByTagName("IMG");
for(var i=0;i<_1c.length;i++){
RadTreeView.AlignImage(_1c[i]);
}
var _1e=_1b.getElementsByTagName("INPUT");
for(var i=0;i<_1e.length;i++){
RadTreeView.AlignImage(_1e[i]);
}
}
_14.IsFetchingData=false;
_14.TreeView.FireEvent(_14.TreeView.AfterClientToggle,_14);
};
_13.send(null);
};
RadTreeNode.prototype.Expand=function(){
if(this.ExpandOnServer){
if(!this.TreeView.FireEvent(this.TreeView.BeforeClientToggle,this)){
return;
}
if(this.ExpandOnServer==1){
this.TreeView.PostBack("NodeExpand",this.ClientID);
return;
}
if(this.ExpandOnServer==2){
if(!this.IsFetchingData){
this.IsFetchingData=true;
this.CachedText=this.TextElement().innerHTML;
switch(this.TreeView.LoadingMessagePosition){
case 0:
this.TextElement().innerHTML="<span class="+this.TreeView.LoadingMessageCssClass+">"+this.TreeView.LoadingMessage+"</span> "+this.TextElement().innerHTML;
break;
case 1:
this.TextElement().innerHTML=this.TextElement().innerHTML+" "+"<span class="+this.TreeView.LoadingMessageCssClass+">"+this.TreeView.LoadingMessage+"</span> ";
break;
case 2:
rtvInsertHTML(this.TextElement().parentNode,"<div id="+this.ClientID+"Loading "+" class="+this.TreeView.LoadingMessageCssClass+">"+this.TreeView.LoadingMessage+"</div>");
break;
}
var _1f=this;
window.setTimeout(function(){
_1f.FetchDataOnDemand();
},20);
return;
}
}
}
if(!this.Nodes.length){
return;
}
if(this.TreeView.SingleExpandPath){
this.CollapseNonParentNodes();
}
var _20=document.getElementById("G"+this.ClientID);
if(this.TreeView.ExpandDelay>0){
_20.style.overflow="hidden";
_20.style.height="1px";
_20.style.display="block";
_20.firstChild.style.position="relative";
window.setTimeout("rtvNodeExpand(1,'"+_20.id+"',"+this.TreeView.ExpandDelay+");",20);
}else{
_20.style.display="block";
}
this.ImageOn();
this.SignOn();
this.Expanded=true;
if(!this.IsClientNode){
this.TreeView.UpdateExpandedState();
}
};
RadTreeNode.prototype.GetParentPositions=function(){
var _21=this;
var _22="";
while(_21!=null){
if(_21.Next()!=null){
_22=_22+"1";
}else{
_22=_22+"0";
}
_21=_21.Parent;
}
return _22;
};
RadTreeNode.prototype.Collapse=function(){
if(this.Nodes.length>0){
if(!this.TreeView.FireEvent(this.TreeView.BeforeClientToggle,this)){
return;
}
if(this.ExpandOnServer==1&&this.TreeView.NodeCollapseWired){
this.TreeView.PostBack("NodeCollapse",this.ClientID);
return;
}
if(this.TreeView.ExpandDelay>0){
var _23=document.getElementById("G"+this.ClientID);
if(_23.scrollHeight!="undefined"){
_23.style.overflow="hidden";
_23.style.display="block";
_23.firstChild.style.position="relative";
window.setTimeout("rtvNodeCollapse("+_23.scrollHeight+",'"+_23.id+"',"+this.TreeView.ExpandDelay+" );",20);
}else{
this.CollapseNoEffect();
}
}else{
this.CollapseNoEffect();
}
this.ImageOff();
this.SignOff();
this.Expanded=false;
this.TreeView.UpdateExpandedState();
}
};
RadTreeNode.prototype.CollapseNoEffect=function(){
if(this.Nodes.length>0){
var _24=document.getElementById("G"+this.ClientID);
_24.style.display="none";
this.ImageOff();
this.SignOff();
this.Expanded=false;
this.TreeView.UpdateExpandedState();
}
};
RadTreeNode.prototype.Highlight=function(e){
if(!this.Enabled){
return;
}
if(e){
if(this.TreeView.MultipleSelect&&(e.ctrlKey||e.shiftKey)){
if(this.Selected){
this.TextElement().className=this.NodeCss;
this.Selected=false;
if(this.TreeView.SelectedNode==this){
this.TreeView.SelectedNode=null;
}
this.TreeView.UpdateSelectedState();
return;
}
}else{
this.TreeView.UnSelectAllNodes();
}
}
this.TextElement().className=this.NodeCssSelect;
this.TreeView.SelectNode(this);
this.TreeView.FireEvent(this.TreeView.AfterClientHighlight,this);
};
RadTreeNode.prototype.ExecuteAction=function(e){
if(this.IsClientNode){
return;
}
if(this.TextElement().tagName=="A"){
this.TextElement().click();
}else{
if(this.Action){
this.TreeView.PostBack("NodeClick",this.ClientID);
}
}
if(e){
(document.all)?e.returnValue=false:e.preventDefault();
}
};
RadTreeNode.prototype.Select=function(e){
if(this.TreeView.FireEvent(this.TreeView.BeforeClientClick,this,e)==false){
return;
}
if(this.Enabled){
this.Highlight(e);
this.TreeView.LastHighlighted=this;
this.ExecuteAction();
}else{
(document.all)?e.returnValue=false:e.preventDefault();
}
this.TreeView.FireEvent(this.TreeView.AfterClientClick,this,e);
};
RadTreeNode.prototype.UnSelect=function(){
if(this.TextElement().parentNode&&this.TextElement().parentNode.tagName=="A"){
this.TextElement().parentNode.className=this.NodeCss;
}
this.TextElement().className=this.NodeCss;
this.Selected=false;
};
RadTreeNode.prototype.Disable=function(){
this.TextElement().className=this.TreeView.NodeCssDisable;
this.Enabled=false;
this.Selected=false;
if(this.CheckElement()!=null){
this.CheckElement().disabled=true;
}
};
RadTreeNode.prototype.Enable=function(){
this.TextElement().className=this.NodeCss;
this.Enabled=true;
if(this.CheckElement()!=null){
this.CheckElement().disabled=false;
}
};
RadTreeNode.prototype.Hover=function(e){
var _29=(e.srcElement)?e.srcElement:e.target;
if(this.TreeView.IsRootNodeTag(_29)){
this.TreeView.SetBorderOnDrag(this,_29,e);
return;
}
if(this.Enabled){
if(this.TreeView.FireEvent(this.TreeView.BeforeClientHighlight,this)==false){
return;
}
this.TreeView.LastHighlighted=this;
if(RadTreeView_DragActive!=null&&RadTreeView_DragActive.DragClone!=null&&(!this.Expanded)&&this.ExpandOnServer!=1){
var _2a=this;
window.setTimeout(function(){
_2a.ExpandOnDrag();
},1000);
}
if(!this.Selected){
this.TextElement().className=this.NodeCssOver;
if(this.Image){
this.ImageElement().style.cursor="hand";
}
}
this.TreeView.FireEvent(this.TreeView.AfterClientHighlight,this);
}
};
RadTreeNode.prototype.UnHover=function(e){
var _2c=(e.srcElement)?e.srcElement:e.target;
if(this.TreeView.IsRootNodeTag(_2c)){
this.TreeView.ClearBorderOnDrag(_2c);
return;
}
if(this.Enabled){
this.TreeView.LastHighlighted=null;
if(!this.Selected){
this.TextElement().className=this.NodeCss;
if(this.Image){
this.ImageElement().style.cursor="default";
}
}
this.TreeView.FireEvent(this.TreeView.AfterClientMouseOut,this);
}
};
RadTreeNode.prototype.ExpandOnDrag=function(){
if(RadTreeView_DragActive!=null&&RadTreeView_DragActive.DragClone!=null&&(!this.Expanded)){
if(RadTreeView_Active.LastHighlighted==this){
this.Expand();
}
}
};
RadTreeNode.prototype.CheckBoxClick=function(e){
if(this.Enabled){
if(this.TreeView.FireEvent(this.TreeView.BeforeClientCheck,this,e)==false){
(this.Checked)?this.Check():this.UnCheck();
return;
}
(this.Checked)?this.UnCheck():this.Check();
if(this.TreeView.AutoPostBackOnCheck){
this.TreeView.PostBack("NodeCheck",this.ClientID);
this.TreeView.FireEvent(this.TreeView.AfterClientCheck,this);
return;
}
this.TreeView.FireEvent(this.TreeView.AfterClientCheck,this);
}
};
RadTreeNode.prototype.Check=function(){
if(this.CheckElement()!=null){
this.CheckElement().checked=true;
this.Checked=true;
this.TreeView.UpdateCheckedState();
}
};
RadTreeNode.prototype.UnCheck=function(){
if(this.CheckElement()!=null){
this.CheckElement().checked=false;
this.Checked=false;
this.TreeView.UpdateCheckedState();
}
};
RadTreeNode.prototype.IsSet=function(a){
return (a!=null&&a!="");
};
RadTreeNode.prototype.ImageOn=function(){
var _2f=document.getElementById(this.ClientID+"i");
if(this.ImageExpanded!=0){
_2f.src=this.ImageExpanded;
}
};
RadTreeNode.prototype.ImageOff=function(){
var _30=document.getElementById(this.ClientID+"i");
if(this.Image!=0){
_30.src=this.Image;
}
};
RadTreeNode.prototype.SignOn=function(){
var _31=document.getElementById(this.ClientID+"c");
if(this.IsSet(this.SignImageExpanded)){
_31.src=this.SignImageExpanded;
}
};
RadTreeNode.prototype.SignOff=function(){
var _32=document.getElementById(this.ClientID+"c");
if(this.IsSet(this.SignImage)){
_32.src=this.SignImage;
}
};
RadTreeNode.prototype.TextElement=function(){
var _33=document.getElementById(this.ClientID);
var _34=_33.getElementsByTagName("span")[0];
if(_34==null){
_34=_33.getElementsByTagName("a")[0];
}
return _34;
};
RadTreeNode.prototype.ImageElement=function(){
return document.getElementById(this.ClientID+"i");
};
RadTreeNode.prototype.CheckElement=function(){
return document.getElementById(this.ClientID).getElementsByTagName("input")[0];
};
RadTreeNode.prototype.IsParent=function(_35){
var _36=this.Parent;
while(_36!=null){
if(_35==_36){
return true;
}
_36=_36.Parent;
}
return false;
};
RadTreeNode.prototype.StartEdit=function(){
if(this.EditEnabled){
var _37=this.Text;
this.TreeView.EditMode=true;
var _38=this.TextElement().parentNode;
this.TreeView.EditTextElement=this.TextElement().cloneNode(true);
this.TextElement().parentNode.removeChild(this.TextElement());
var _39=this;
var _3a=document.createElement("input");
_3a.setAttribute("type","text");
_3a.setAttribute("size",this.Text.length+3);
_3a.setAttribute("value",_37);
_3a.className=this.TreeView.NodeCssEdit;
var _3b=this;
_3a.onblur=function(){
_3b.EndEdit();
};
_3a.onchange=function(){
_3b.EndEdit();
};
_3a.onkeypress=function(e){
_3b.AnalyzeEditKeypress(e);
};
_3a.onsubmit=function(){
return false;
};
_38.appendChild(_3a);
this.TreeView.EditInputElement=_3a;
_3a.focus();
_3a.onselectstart=function(e){
if(!e){
e=window.event;
}
if(e.stopPropagation){
e.stopPropagation();
}else{
e.cancelBubble=true;
}
};
var _3e=0;
var _3f=this.Text.length;
if(_3a.createTextRange){
var _40=_3a.createTextRange();
_40.moveStart("character",_3e);
_40.moveEnd("character",_3f);
_40.select();
}else{
_3a.setSelectionRange(_3e,_3f);
}
}
};
RadTreeNode.prototype.EndEdit=function(){
this.TreeView.EditInputElement.onblur=null;
this.TreeView.EditInputElement.onchange=null;
var _41=this.TreeView.EditInputElement.parentNode;
this.TreeView.EditInputElement.parentNode.removeChild(this.TreeView.EditInputElement);
_41.appendChild(this.TreeView.EditTextElement);
if(this.TreeView.FireEvent(this.TreeView.AfterClientEdit,this,this.Text,this.TreeView.EditInputElement.value)!=false){
if(this.Text!=this.TreeView.EditInputElement.value){
var _42=this.ClientID+":"+this.TreeView.EscapeParameter(this.TreeView.EditInputElement.value);
this.TreeView.PostBack("NodeEdit",_42);
return;
}
}
this.TreeView.EditMode=false;
this.TreeView.EditInputElement=null;
this.TreeView.EditTextElement=null;
};
RadTreeNode.prototype.AnalyzeEditKeypress=function(e){
if(document.all){
e=event;
}
if(e.keyCode==13){
(document.all)?e.returnValue=false:e.preventDefault();
if(typeof (e.cancelBubble)!="undefined"){
e.cancelBubble=true;
}
this.EndEdit();
return false;
}
if(e.keyCode==27){
this.TreeView.EditInputElement.value=this.TreeView.EditTextElement.innerHTML;
this.EndEdit();
}
return true;
};
RadTreeNode.prototype.LoadNodesOnDemand=function(s,_45,url){
if(_45==404){
var _47="CallBack URL not found: \n\r\n\r"+url+"\n\r\n\rAre you using URL Rewriter? Please, try setting the AjaxUrl property to match the correct URL you need";
alert(_47);
this.TreeView.FireEvent(this.TreeView.AfterClientCallBackError,this.TreeView);
}else{
try{
eval(s);
var _48=window[this.ClientID+"ClientData"];
for(var i=0;i<_48.length;i++){
var _4a=_48[i][0];
var _4b=_4a.substring(0,_4a.lastIndexOf("_t"));
var _4c=this.TreeView.FindNode(_4b);
if(_4c){
this.TreeView.LoadNode(_48[i],null,_4c);
}else{
_48[i][17]=0;
this.TreeView.LoadNode(_48[i],null,this);
}
}
}
catch(e){
this.TreeView.FireEvent(this.TreeView.AfterClientCallBackError,this.TreeView);
}
}
};
function RadTreeView(_4d){
if(window.tlrkTreeViews==null){
window.tlrkTreeViews=new Array();
}
if(window.tlrkTreeViews[_4d]!=null){
oldTreeView=window.tlrkTreeViews[_4d];
oldTreeView.Dispose();
}
tlrkTreeViews[_4d]=this;
this.Nodes=new Array();
this.AllNodes=new Array();
this.ClientID=null;
this.SelectedNode=null;
this.DragMode=false;
this.DragSource=null;
this.DragClone=null;
this.LastHighlighted=null;
this.MouseInside=false;
this.HtmlElementID="";
this.EditMode=false;
this.EditTextElement=null;
this.EditInputElement=null;
this.BeforeClientClick=null;
this.BeforeClientHighlight=null;
this.AfterClientHighlight=null;
this.AfterClientMouseOut=null;
this.BeforeClientDrop=null;
this.AfterClientDrop=null;
this.BeforeClientToggle=null;
this.AfterClientToggle=null;
this.BeforeClientContextClick=null;
this.BeforeClientContextMenu=null;
this.AfterClientContextClick=null;
this.BeforeClientCheck=null;
this.AfterClientCheck=null;
this.AfterClientMove=null;
this.AfterClientFocus=null;
this.BeforeClientDrag=null;
this.AfterClientEdit=null;
this.AfterClientClick=null;
this.BeforeClientDoubleClick=null;
this.AfterClientCallBackError=null;
this.DragAndDropBetweenNodes=false;
this.AutoPostBackOnCheck=false;
this.CausesValidation=true;
this.ContextMenuVisible=false;
this.ContextMenuName=null;
this.ContextMenuNode=null;
this.SingleExpandPath=false;
this.ExpandDelay=2;
this.TabIndex=0;
this.AllowNodeEditing=false;
this.LoadOnDemandUrl=null;
this.LoadingMessage="(loading ...)";
this.LoadingMessagePosition=0;
this.LoadingMessageCssClass="LoadingMessage";
this.NodeCollapseWired=false;
this.LastBorderElementSet=null;
this.LastDragPosition="on";
this.LastDragNode=null;
this.IsBuilt=false;
}
RadTreeView.AlignImage=function(_4e){
_4e.align="absmiddle";
_4e.style.display="inline";
if(!document.all||window.opera){
if(_4e.nextSibling&&_4e.nextSibling.tagName=="SPAN"){
_4e.nextSibling.style.verticalAlign="middle";
}
if(_4e.nextSibling&&_4e.nextSibling.tagName=="INPUT"){
_4e.nextSibling.style.verticalAlign="middle";
}
}
};
RadTreeView.prototype.OnInit=function(){
var _4f=new Array();
this.PreloadImages(_4f);
GlobalTreeViewImageList=_4f;
var _50=document.getElementById(this.Container).getElementsByTagName("IMG");
for(var i=0;i<_50.length;i++){
var _52=_50[i].className;
if(_52!=null&&_52!=""){
_50[i].src=_4f[_52];
RadTreeView.AlignImage(_50[i]);
}
}
this.LoadTree(_4f);
var _53=document.getElementById(this.Container).getElementsByTagName("INPUT");
for(var i=0;i<_53.length;i++){
RadTreeView.AlignImage(_53[i]);
}
if(document.addEventListener&&(!RadTreeView_KeyboardHooked)){
RadTreeView_KeyboardHooked=true;
document.addEventListener("keydown",this.KeyDownMozilla,false);
}
if((!RadTreeView_MouseMoveHooked)&&(this.DragAndDrop)){
RadTreeView_MouseMoveHooked=true;
if(document.attachEvent){
document.attachEvent("onmousemove",rtvMouseMove);
}
if(document.addEventListener){
document.addEventListener("mousemove",rtvMouseMove,false);
}
}
if(!RadTreeView_MouseUpHooked){
RadTreeView_MouseUpHooked=true;
if(document.attachEvent){
document.attachEvent("onmouseup",rtvMouseUp);
}
if(document.addEventListener){
document.addEventListener("mouseup",rtvMouseUp,false);
}
}
this.AttachAllEvents();
this.IsBuilt=true;
};
RadTreeView.prototype.AttachAllEvents=function(){
var _54=this;
var _55=document.getElementById(this.Container);
_55.onfocus=function(e){
rtvDispatcher(_54.ClientID,"focus",e);
};
_55.onmouseover=function(e){
rtvDispatcher(_54.ClientID,"mover",e);
};
_55.onmouseout=function(e){
rtvDispatcher(_54.ClientID,"mout",e);
};
_55.oncontextmenu=function(e){
rtvDispatcher(_54.ClientID,"context",e);
};
_55.onscroll=function(e){
_54.Scroll();
};
_55.onclick=function(e){
rtvDispatcher(_54.ClientID,"mclick",e);
};
_55.ondblclick=function(e){
rtvDispatcher(_54.ClientID,"mdclick",e);
};
_55.onkeydown=function(e){
rtvDispatcher(_54.ClientID,"keydown",e);
};
_55.onselectstart=function(){
return false;
};
_55.ondragstart=function(){
return false;
};
if(this.DragAndDrop){
_55.onmousedown=function(e){
rtvDispatcher(_54.ClientID,"mdown",e);
};
}
if(window.attachEvent){
window.attachEvent("onunload",function(){
_54.Dispose();
});
}
this.RootElement=_55;
};
RadTreeView.prototype.Dispose=function(){
if(this.disposed){
return;
}
this.disposed=true;
try{
var _5f=this.RootElement;
if(_5f!=null){
for(var _60 in _5f){
if(typeof (_5f[_60])=="function"){
_5f[_60]=null;
}
}
for(var _60 in this){
if(_60!="Dispose"){
this[_60]=null;
}
}
}
this.RootElement=null;
}
catch(err){
}
};
RadTreeView.prototype.PreloadImages=function(_61){
var _62=window[this.ClientID+"ImageData"];
for(var i=0;i<_62.length;i++){
_61[i]=_62[i];
}
};
RadTreeView.prototype.FindNode=function(_64){
for(var i=0;i<this.AllNodes.length;i++){
if(this.AllNodes[i].ClientID==_64){
return this.AllNodes[i];
}
}
return null;
};
RadTreeView.prototype.FindNodeByText=function(_66){
for(var i=0;i<this.AllNodes.length;i++){
if(this.AllNodes[i].Text==_66){
return this.AllNodes[i];
}
}
return null;
};
RadTreeView.prototype.FindNodeByValue=function(_68){
for(var i=0;i<this.AllNodes.length;i++){
if(this.AllNodes[i].Value==_68){
return this.AllNodes[i];
}
}
return null;
};
RadTreeView.prototype.FindNodeByAttribute=function(_6a,_6b){
for(var i=0;i<tree.AllNodes.length;i++){
if(this.AllNodes[i].Attributes[_6a]==_6b){
return this.AllNodes[i];
}
}
return null;
};
RadTreeView.prototype.IsChildOf=function(_6d,_6e){
if(_6e==_6d){
return false;
}
while(_6e&&(_6e!=document.body)){
if(_6e==_6d){
return true;
}
try{
_6e=_6e.parentNode;
}
catch(e){
return false;
}
}
return false;
};
RadTreeView.prototype.GetTarget=function(e){
if(!e){
return null;
}
return e.target||e.srcElement;
};
RadTreeView.prototype.LoadTree=function(_70){
var cd=window[this.ClientID+"ClientData"];
for(var i=0;i<cd.length;i++){
this.LoadNode(cd[i],_70);
}
};
RadTreeView.prototype.LoadNode=function(cd,_74,_75){
var _76=new RadTreeNode();
_76.ClientID=cd[0];
_76.TreeView=this;
var _77=cd[17];
if(_77>0){
_76.Parent=this.AllNodes[_77-1];
}
if(_75!=null){
_76.Parent=_75;
}
_76.NodeCss=this.NodeCss;
_76.NodeCssOver=this.NodeCssOver;
_76.NodeCssSelect=this.NodeCssSelect;
_76.Text=cd[1];
_76.Value=cd[2];
_76.Category=cd[3];
if(_74!=null){
_76.SignImage=_74[cd[4]];
_76.SignImageExpanded=_74[cd[5]];
}else{
_76.SignImage=GlobalTreeViewImageList[cd[4]];
_76.SignImageExpanded=GlobalTreeViewImageList[cd[5]];
}
if(cd[6]>0){
_76.Image=_74[cd[6]];
}
if(cd[7]>0){
_76.ImageExpanded=_74[cd[7]];
}
_76.Selected=cd[8];
if(_76.Selected){
this.SelectedNode=_76;
}
_76.Checked=cd[9];
_76.Enabled=cd[10];
_76.Expanded=cd[11];
_76.Action=cd[12];
if(this.IsSet(cd[13])){
_76.NodeCss=cd[13];
}
if(this.IsSet(cd[14])){
_76.ContextMenuName=cd[14];
}
this.AllNodes[this.AllNodes.length]=_76;
if(_76.Parent!=null){
_76.Parent.Nodes[_76.Parent.Nodes.length]=_76;
}else{
this.Nodes[this.Nodes.length]=_76;
}
_76.Index=cd[16];
_76.DragEnabled=cd[18];
_76.DropEnabled=cd[19];
_76.ExpandOnServer=cd[20];
if(this.IsSet(cd[21])){
_76.NodeCssOver=cd[21];
}
if(this.IsSet(cd[22])){
_76.NodeCssSelect=cd[22];
}
_76.Level=cd[23];
_76.ID=cd[24];
_76.IsClientNode=cd[25];
_76.EditEnabled=cd[26];
_76.Attributes=cd[27];
};
RadTreeView.prototype.Toggle=function(_78){
this.FindNode(_78).Toggle();
};
RadTreeView.prototype.Select=function(_79,e){
this.FindNode(_79).Select(e);
};
RadTreeView.prototype.Hover=function(_7b,e){
var _7b=this.FindNode(_7b);
if(_7b){
_7b.Hover(e);
}
};
RadTreeView.prototype.UnHover=function(_7d,e){
var _7d=this.FindNode(_7d);
if(_7d){
_7d.UnHover(e);
}
};
RadTreeView.prototype.CheckBoxClick=function(_7f,e){
this.FindNode(_7f).CheckBoxClick(e);
};
RadTreeView.prototype.Highlight=function(_81,e){
this.FindNode(_81).Highlight(e);
};
RadTreeView.prototype.SelectNode=function(_83){
this.SelectedNode=_83;
_83.Selected=true;
this.UpdateSelectedState();
};
RadTreeView.prototype.GetSelectedNodes=function(){
var _84=new Array();
for(var i=0;i<this.AllNodes.length;i++){
if(this.AllNodes[i].Selected){
_84[_84.length]=this.AllNodes[i];
}
}
return _84;
};
RadTreeView.prototype.UnSelectAllNodes=function(_86){
for(var i=0;i<this.AllNodes.length;i++){
if(this.AllNodes[i].Selected&&this.AllNodes[i].Enabled){
this.AllNodes[i].UnSelect();
}
}
};
RadTreeView.prototype.KeyDownMozilla=function(e){
if(navigator.userAgent.toUpperCase().indexOf("SAFARI")!=-1&&e.keyCode!=32&&e.keyCode!=107&&e.keyCode!=109){
safariKeyDownFlag=!safariKeyDownFlag;
if(safariKeyDownFlag){
return;
}
}
var _89=RadTreeView_Active;
if(_89){
var _8a=_89.GetTarget(e);
if(_8a.tagName.toUpperCase()=="BODY"||_8a.tagName.toUpperCase()=="HTML"||_89.IsChildOf(_8a,_89.RootElement)||_8a==_89.RootElement){
if(!_89.IsBuilt){
return;
}
if(_89.SelectedNode!=null){
if(_89.EditMode){
return;
}
if(e.keyCode==107||e.keyCode==109||e.keyCode==37||e.keyCode==39){
_89.SelectedNode.Toggle();
}
if(e.keyCode==40&&_89.SelectedNode.NextVisible()!=null){
_89.SelectedNode.NextVisible().Highlight(e);
}
if(e.keyCode==38&&_89.SelectedNode.PrevVisible()!=null){
_89.SelectedNode.PrevVisible().Highlight(e);
}
if(e.keyCode==13){
if(_89.FireEvent(_89.BeforeClientClick,_89.SelectedNode,e)==false){
return;
}
_89.SelectedNode.ExecuteAction();
_89.FireEvent(_89.AfterClientClick,_89.SelectedNode,e);
}
if(e.keyCode==32){
_89.SelectedNode.CheckBoxClick();
}
if(e.keyCode==113&&_89.AllowNodeEditing){
_89.SelectedNode.StartEdit();
}
}else{
if(e.keyCode==38||e.keyCode==40||e.keyCode==13||e.keyCode==32){
_89.Nodes[0].Highlight();
}
}
}
}
};
RadTreeView.prototype.KeyDown=function(e){
if(this.EditMode){
return;
}
var _8c=this.SelectedNode;
if(_8c!=null){
if(e.keyCode==107||e.keyCode==109||e.keyCode==37||e.keyCode==39){
_8c.Toggle();
}
if(e.keyCode==40&&_8c.NextVisible()!=null){
_8c.NextVisible().Highlight(e);
}
if(e.keyCode==38&&_8c.PrevVisible()!=null){
_8c.PrevVisible().Highlight(e);
}
if(e.keyCode==13){
if(this.FireEvent(this.BeforeClientClick,this.SelectedNode,e)==false){
return;
}
_8c.ExecuteAction(e);
this.FireEvent(this.AfterClientClick,this.SelectedNode,e);
}
if(e.keyCode==32){
_8c.CheckBoxClick();
(document.all)?e.returnValue=false:e.preventDefault();
}
if(e.keyCode==113&&this.AllowNodeEditing){
_8c.StartEdit();
}
}else{
if(e.keyCode==38||e.keyCode==40||e.keyCode==13||e.keyCode==32){
this.Nodes[0].Highlight();
}
}
};
RadTreeView.prototype.UpdateState=function(){
this.UpdateExpandedState();
this.UpdateCheckedState();
this.UpdateSelectedState();
};
RadTreeView.prototype.UpdateExpandedState=function(){
var _8d="";
for(var i=0;i<this.AllNodes.length;i++){
var _8f=(this.AllNodes[i].Expanded)?"1":"0";
_8d+=_8f;
}
document.getElementById(this.ClientID+"_expanded").value=_8d;
};
RadTreeView.prototype.UpdateCheckedState=function(){
var _90="";
for(var i=0;i<this.AllNodes.length;i++){
var _92=(this.AllNodes[i].Checked)?"1":"0";
_90+=_92;
}
document.getElementById(this.ClientID+"_checked").value=_90;
};
RadTreeView.prototype.UpdateSelectedState=function(){
var _93="";
for(var i=0;i<this.AllNodes.length;i++){
var _95=(this.AllNodes[i].Selected)?"1":"0";
_93+=_95;
}
document.getElementById(this.ClientID+"_selected").value=_93;
};
RadTreeView.prototype.Scroll=function(){
for(var key in tlrkTreeViews){
if((typeof (tlrkTreeViews[key])!="function")&&tlrkTreeViews[key].ContextMenuVisible){
contextMenuToBeHidden=tlrkTreeViews[key];
window.setTimeout(function(){
if(contextMenuToBeHidden){
contextMenuToBeHidden.HideContextMenu();
}
},10);
}
}
document.getElementById(this.ClientID+"_scroll").value=document.getElementById(this.Container).scrollTop;
};
RadTreeView.prototype.ContextMenuClick=function(e,p1,p2,p3){
instance=this;
window.setTimeout(function(){
instance.HideContextMenu();
},10);
if(this.FireEvent(this.BeforeClientContextClick,this.ContextMenuNode,p1,p3)==false){
return;
}
if(p2){
var _9b=this.ContextMenuNode.ClientID+":"+this.EscapeParameter(p1)+":"+this.EscapeParameter(p3);
this.PostBack("ContextMenuClick",_9b);
}
};
RadTreeView.prototype.ContextMenu=function(e,_9d){
var src=(e.srcElement)?e.srcElement:e.target;
var _9f=this.FindNode(_9d);
if(_9f!=null&&this.BeforeClientContextMenu!=null){
var _a0=this.SelectedNode;
if(this.FireEvent(this.BeforeClientContextMenu,_9f,e,_a0)==false){
return;
}
this.Highlight(_9d,e,_a0);
}
if(_9f!=null&&_9f.ContextMenuName!=null&&_9f.Enabled){
if(!this.ContextMenuVisible){
this.ContextMenuNode=_9f;
if(!_9f.Selected){
this.Highlight(_9d,e);
}
this.ShowContextMenu(_9f.ContextMenuName,e);
document.all?e.returnValue=false:e.preventDefault();
}
}
};
RadTreeView.prototype.ShowContextMenu=function(_a1,e){
if(!document.readyState||document.readyState=="complete"){
var _a3="rtvcm"+this.ClientID+_a1;
var _a4=document.getElementById(_a3);
if(_a4){
var _a5=_a4.cloneNode(true);
_a5.id=_a3+"_clone";
document.body.appendChild(_a5);
_a5=document.getElementById(_a3+"_clone");
_a5.style.left=this.CalculateXPos(e)+"px";
_a5.style.top=this.CalculateYPos(e)+"px";
_a5.style.position="absolute";
_a5.style.display="block";
this.ContextMenuVisible=true;
this.ContextMenuName=_a1;
document.all?e.returnValue=false:e.preventDefault();
}
}
};
RadTreeView.prototype.CalculateYPos=function(e){
if(document.compatMode&&document.compatMode=="CSS1Compat"){
return (e.clientY+document.documentElement.scrollTop);
}
return (e.clientY+document.body.scrollTop);
};
RadTreeView.prototype.CalculateXPos=function(e){
if(document.compatMode&&document.compatMode=="CSS1Compat"){
return (e.clientX+document.documentElement.scrollLeft);
}
return (e.clientX+document.body.scrollLeft);
};
RadTreeView.prototype.HideContextMenu=function(){
if(!document.readyState||document.readyState=="complete"){
var _a8=document.getElementById("rtvcm"+this.ClientID+this.ContextMenuName+"_clone");
if(_a8){
document.body.removeChild(_a8);
}
this.ContextMenuVisible=false;
}
};
RadTreeView.prototype.MouseClickDispatcher=function(e){
var src=(e.srcElement)?e.srcElement:e.target;
var _ab=rtvGetNodeID(e);
if(_ab!=null&&src.tagName!="DIV"){
var _ac=this.FindNode(_ab);
if(_ac.Selected){
if(this.AllowNodeEditing){
_ac.StartEdit();
return;
}else{
this.Select(_ab,e);
}
}else{
this.Select(_ab,e);
}
}
if(src.tagName=="IMG"){
var _ad=src.className;
if(this.IsSet(_ad)&&this.IsToggleImage(_ad)){
this.Toggle(src.parentNode.id);
}
}
if(src.tagName=="INPUT"&&rtvInsideNode(src)){
this.CheckBoxClick(src.parentNode.id,e);
}
};
RadTreeView.prototype.IsToggleImage=function(n){
return (n==1||n==2||n==5||n==6||n==7||n==8||n==10||n==11);
};
RadTreeView.prototype.DoubleClickDispatcher=function(e,_b0){
var _b1=this.FindNode(_b0);
if(this.FireEvent(this.BeforeClientDoubleClick,_b1)==false){
return;
}
this.Toggle(_b0);
};
RadTreeView.prototype.MouseOverDispatcher=function(e,_b3){
this.Hover(_b3,e);
};
RadTreeView.prototype.MouseOutDispatcher=function(e,_b5){
this.UnHover(_b5,e);
this.LastDragNode=null;
this.LastHighlighted=null;
};
RadTreeView.prototype.MouseDown=function(e){
if(this.LastHighlighted!=null&&this.DragAndDrop){
if(this.FireEvent(this.BeforeClientDrag,this.LastHighlighted)==false){
return;
}
if(!this.LastHighlighted.DragEnabled){
return;
}
if(e.button==2){
return;
}
this.DragSource=this.LastHighlighted;
this.DragClone=document.createElement("div");
document.body.appendChild(this.DragClone);
RadTreeView_DragActive=this;
var res="";
if(this.MultipleSelect&&(this.SelectedNodesCount()>1)){
for(var i=0;i<this.AllNodes.length;i++){
if(this.AllNodes[i].Selected){
if(this.AllNodes[i].Image){
var img=this.AllNodes[i].ImageElement();
var _ba=img.cloneNode(true);
this.DragClone.appendChild(_ba);
}
var _bb=this.AllNodes[i].TextElement().cloneNode(true);
_bb.className=this.AllNodes[i].NodeCss;
_bb.style.color="gray";
this.DragClone.appendChild(_bb);
this.DragClone.appendChild(document.createElement("BR"));
}
res=res+"text";
}
}
if(res==""){
if(this.LastHighlighted.Image){
var img=this.LastHighlighted.ImageElement();
var _ba=img.cloneNode(true);
this.DragClone.appendChild(_ba);
}
var _bb=this.LastHighlighted.TextElement().cloneNode(true);
_bb.className=this.LastHighlighted.NodeCss;
_bb.style.color="gray";
this.DragClone.appendChild(_bb);
}
this.DragClone.style.position="absolute";
this.DragClone.style.display="none";
if(e.preventDefault){
e.preventDefault();
}
}
};
RadTreeView.prototype.SelectedNodesCount=function(){
var _bc=0;
for(var i=0;i<this.AllNodes.length;i++){
if(this.AllNodes[i].Selected){
_bc++;
}
}
return _bc;
};
RadTreeView.prototype.FireEvent=function(_be,a,b,c,d){
if(!_be){
return true;
}
RadTreeViewGlobalFirstParam=a;
RadTreeViewGlobalSecondParam=b;
RadTreeViewGlobalThirdParam=c;
RadTreeViewGlobalFourthParam=d;
var s=_be+"(RadTreeViewGlobalFirstParam, RadTreeViewGlobalSecondParam, RadTreeViewGlobalThirdParam, RadTreeViewGlobalFourthParam);";
return eval(s);
};
RadTreeView.prototype.Focus=function(e){
this.FireEvent(this.AfterClientFocus,this);
};
RadTreeView.prototype.IsSet=function(a){
return (a!=null&&a!="");
};
RadTreeView.prototype.GetX=function(obj){
var _c7=0;
if(obj.offsetParent){
while(obj.offsetParent){
_c7+=obj.offsetLeft;
obj=obj.offsetParent;
}
}else{
if(obj.x){
_c7+=obj.x;
}
}
return _c7;
};
RadTreeView.prototype.GetY=function(obj){
var _c9=0;
if(obj.offsetParent){
while(obj.offsetParent){
_c9+=obj.offsetTop;
obj=obj.offsetParent;
}
}else{
if(obj.y){
_c9+=obj.y;
}
}
return _c9;
};
RadTreeView.prototype.PostBack=function(_ca,_cb){
var _cc=_ca+"#"+_cb;
if(this.PostBackOptionsClientString){
var _cd=this.PostBackOptionsClientString.replace(/@@arguments@@/g,_cc);
if(typeof (WebForm_PostBackOptions)!="undefined"||_cd.indexOf("_doPostBack")>-1||_cd.indexOf("AsyncRequest")>-1||_cd.indexOf("AsyncRequest")>-1||_cd.indexOf("AjaxNS")>-1){
eval(_cd);
}
}else{
if(this.CausesValidation){
if(!(typeof (Page_ClientValidate)!="function"||Page_ClientValidate())){
return;
}
}
var _ce=this.PostBackFunction.replace(/@@arguments@@/g,_cc);
eval(_ce);
}
};
RadTreeView.prototype.EscapeParameter=function(_cf){
var _d0=_cf.replace(/'/g,"&squote");
_d0=_d0.replace(/#/g,"&ssharp");
_d0=_d0.replace(/:/g,"&scolon");
_d0=_d0.replace(/\\/g,"\\\\");
return _d0;
};
RadTreeView.prototype.IsRootNodeTag=function(_d1){
if(_d1&&_d1.tagName=="DIV"&&_d1.id.indexOf(this.ID)>-1){
return true;
}
return false;
};
RadTreeView.prototype.SetBorderOnDrag=function(_d2,_d3,e){
if(this.DragAndDropBetweenNodes&&this.IsDragActive()){
this.LastDragNode=_d2;
var _d5=this.CalculateYPos(e);
var _d6=this.GetY(_d3);
if(_d5<_d6+_d2.TextElement().offsetHeight){
_d3.style.borderTop="1px dotted black";
this.LastDragPosition="above";
}else{
_d3.style.borderBottom="1px dotted black";
this.LastDragPosition="below";
}
this.LastBorderElementSet=_d3;
}
};
RadTreeView.prototype.ClearBorderOnDrag=function(_d7){
if(_d7&&this.DragAndDropBetweenNodes&&this.IsDragActive()){
_d7.style.borderTop="";
_d7.style.borderBottom="";
this.LastDragPosition="over";
}
};
RadTreeView.prototype.IsDragActive=function(){
for(var key in tlrkTreeViews){
if((typeof (tlrkTreeViews[key])!="function")&&tlrkTreeViews[key].DragClone!=null){
return true;
}
}
return false;
};
function rtvIsAnyContextMenuVisible(){
for(var key in tlrkTreeViews){
if((typeof (tlrkTreeViews[key])!="function")&&tlrkTreeViews[key].ContextMenuVisible){
return true;
}
}
return false;
}
function rtvAdjustScroll(){
if(RadTreeView_DragActive==null||RadTreeView_DragActive.DragClone==null||RadTreeView_Active==null){
return;
}
var _da=RadTreeView_Active;
var _db=document.getElementById(RadTreeView_Active.Container);
var _dc,_dd;
_dc=_da.GetY(_db);
_dd=_dc+_db.offsetHeight;
if((RadTreeView_MouseY-_dc)<50&&_db.scrollTop>0){
_db.scrollTop=_db.scrollTop-10;
_da.Scroll();
RadTreeView_ScrollTimeout=window.setTimeout(function(){
rtvAdjustScroll();
},100);
}else{
if((_dd-RadTreeView_MouseY)<50&&_db.scrollTop<(_db.scrollHeight-_db.offsetHeight+16)){
_db.scrollTop=_db.scrollTop+10;
_da.Scroll();
RadTreeView_ScrollTimeout=window.setTimeout(function(){
rtvAdjustScroll();
},100);
}
}
}
function rtvMouseUp(e){
if(RadTreeView_Active==null){
return;
}
if(e&&!e.ctrlKey){
for(var key in tlrkTreeViews){
if((typeof (tlrkTreeViews[key])!="function")&&tlrkTreeViews[key].ContextMenuVisible){
contextMenuToBeHidden=tlrkTreeViews[key];
window.setTimeout(function(){
if(contextMenuToBeHidden){
contextMenuToBeHidden.HideContextMenu();
}
},10);
return;
}
}
}
if(RadTreeView_DragActive==null||RadTreeView_DragActive.DragClone==null){
return;
}
(document.all)?e.returnValue=false:e.preventDefault();
var _e0=RadTreeView_DragActive.DragSource;
var _e1=RadTreeView_Active.LastHighlighted;
var _e2=RadTreeView_Active;
var _e3="over";
var _e4;
if(_e2.LastBorderElementSet){
_e3=_e2.LastDragPosition;
_e4=_e2.LastDragNode;
_e2.ClearBorderOnDrag(_e2.LastBorderElementSet);
}
if(_e4){
_e1=_e4;
}
document.body.removeChild(RadTreeView_DragActive.DragClone);
RadTreeView_DragActive.DragClone=null;
if(_e1!=null&&_e1.DropEnabled==false){
return;
}
if(_e0==_e1){
return;
}
if(RadTreeView_DragActive.FireEvent(RadTreeView_DragActive.BeforeClientDrop,_e0,_e1,e,_e3)==false){
return;
}
if(_e0.IsClientNode||((_e1!=null)&&_e1.IsClientNode)){
return;
}
var _e5=RadTreeView_DragActive.ClientID+"#"+_e0.ClientID+"#";
var _e6="";
if(_e1==null){
_e6="null"+"#"+RadTreeView_DragActive.HtmlElementID;
}else{
_e6=_e2.ClientID+"#"+_e1.ClientID+"#"+_e3;
}
if(_e1==null&&RadTreeView_DragActive.HtmlElementID==""){
return;
}
var _e7=_e5+_e6;
RadTreeView_DragActive.PostBack("NodeDrop",_e7);
RadTreeView_DragActive.FireEvent(RadTreeView_DragActive.AfterClientDrop,_e0,_e1,e);
RadTreeView_DragActive=null;
}
function rtvMouseMove(e){
if(rtvIsAnyContextMenuVisible()){
return;
}
if(RadTreeView_DragActive!=null&&RadTreeView_DragActive.DragClone!=null){
var _e9,_ea;
_e9=RadTreeView_DragActive.CalculateXPos(e)+8;
_ea=RadTreeView_DragActive.CalculateYPos(e)+4;
RadTreeView_MouseY=_ea;
rtvAdjustScroll();
RadTreeView_DragActive.DragClone.style.zIndex=999;
RadTreeView_DragActive.DragClone.style.top=_ea+"px";
RadTreeView_DragActive.DragClone.style.left=_e9+"px";
RadTreeView_DragActive.DragClone.style.display="block";
RadTreeView_DragActive.FireEvent(RadTreeView_DragActive.AfterClientMove,e);
}
}
function rtvNodeExpand(a,id,_ed){
var _ee=document.getElementById(id);
var _ef=_ee.scrollHeight;
var _f0=(_ef-a)/_ed;
var _f1=a+_f0;
if(_f1>_ef-1){
_ee.style.height="";
_ee.firstChild.style.position="";
}else{
_ee.style.height=_f1+"px";
window.setTimeout("rtvNodeExpand("+_f1+","+"'"+id+"',"+_ed+");",5);
}
}
function rtvNodeCollapse(a,id,_f4){
var _f5=document.getElementById(id);
var _f6=_f5.scrollHeight;
var _f7=(_f6-Math.abs(_f6-a))/_f4;
var _f8=a-_f7;
if(_f8<=3){
_f5.style.height="";
_f5.style.display="none";
_f5.firstChild.style.position="";
}else{
_f5.style.height=_f8+"px";
window.setTimeout("rtvNodeCollapse("+_f8+","+"'"+id+"',"+_f4+" );",5);
}
}
function rtvGetNodeID(e){
if(RadTreeView_Active==null){
return;
}
var _fa=(e.srcElement)?e.srcElement:e.target;
if(_fa.nodeType==3){
_fa=_fa.parentNode;
}
if(_fa.tagName=="IMG"&&_fa.nextSibling){
var _fb=_fa.className;
if(_fb){
_fb=parseInt(_fb);
if(_fb>12){
_fa=_fa.nextSibling;
}
}
}
if(_fa.id==RadTreeView_Active.ID){
return null;
}
if(_fa.id.indexOf(RadTreeView_Active.ID)>-1&&_fa.tagName=="DIV"){
return _fa.id;
}
while(_fa!=null){
if((_fa.tagName=="SPAN"||_fa.tagName=="A")&&rtvInsideNode(_fa)){
return _fa.parentNode.id;
}
_fa=_fa.parentNode;
}
return null;
}
function rtvInsideNode(_fc){
if(_fc.parentNode&&_fc.parentNode.tagName=="DIV"&&_fc.parentNode.id.indexOf(RadTreeView_Active.ID)>-1){
return _fc.parentNode.id;
}
}
function rtvDispatcher(t,w,e,p1,p2,p3){
if(!e){
e=window.event;
}
if(tlrkTreeViews){
var _103=rtvGetNodeID(e);
var _104=tlrkTreeViews[t];
if(!_104.IsBuilt){
return;
}
if(rtvIsAnyContextMenuVisible()&&w!="mclick"&&w!="cclick"){
return;
}
if(_104.EditMode){
return;
}
RadTreeView_Active=_104;
var _105=window.netscape&&!window.opera;
var _106=(navigator.userAgent.toLowerCase().indexOf("safari")!=-1);
switch(w){
case "mover":
if(_103!=null){
_104.MouseOverDispatcher(e,_103);
}
break;
case "mout":
if(_103!=null){
_104.MouseOutDispatcher(e,_103);
}
break;
case "mclick":
_104.MouseClickDispatcher(e);
break;
case "mdclick":
if(_103!=null){
_104.DoubleClickDispatcher(e,_103);
}
break;
case "mdown":
_104.MouseDown(e);
break;
case "mup":
_104.MouseUp(e);
break;
case "context":
if(_103!=null){
_104.ContextMenu(e,_103);
return false;
}
break;
case "cclick":
_104.ContextMenuClick(e,p1,p2,p3);
break;
case "focus":
_104.Focus(e);
case "keydown":
if(!_105&&!_106){
_104.KeyDown(e);
}
}
}
}
function rtvAppendStyleSheet(_107,_108){
var _109=(navigator.appName=="Microsoft Internet Explorer")&&((navigator.userAgent.toLowerCase().indexOf("mac")!=-1)||(navigator.appVersion.toLowerCase().indexOf("mac")!=-1));
var _10a=(navigator.userAgent.toLowerCase().indexOf("safari")!=-1);
if(_109||_10a){
document.write("<"+"link"+" rel='stylesheet' type='text/css' href='"+_108+"'>");
}else{
var _10b=document.createElement("LINK");
_10b.rel="stylesheet";
_10b.type="text/css";
_10b.href=_108;
document.getElementById(_107+"StyleSheetHolder").appendChild(_10b);
}
}
function rtvInsertHTML(_10c,html){
if(_10c.tagName=="A"){
_10c=_10c.parentNode;
}
if(document.all){
_10c.insertAdjacentHTML("beforeEnd",html);
}else{
var r=_10c.ownerDocument.createRange();
r.setStartBefore(_10c);
var _10f=r.createContextualFragment(html);
_10c.appendChild(_10f);
}
}

//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
