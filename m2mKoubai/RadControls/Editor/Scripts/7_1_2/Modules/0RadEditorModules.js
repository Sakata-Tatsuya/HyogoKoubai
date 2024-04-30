function RadEditorModuleBase(_1){
if(!_1){
return;
}
this.Editor=_1.Editor;
this.IsIE=this.Editor.IsIE;
this.IsOpera=this.Editor.IsOpera;
this.Localization=this.Editor.Localization;
this.Document=_1.Document;
this.ModuleElement=_1.ModuleElement;
this.Title=_1.Title;
this.Id=_1.Id;
this.ModuleElement.className=_1.ClassName?_1.ClassName:"RadEModule";
this.TopElement=null;
this.EventHandlerQueue={};
this.IsCreated=false;
this.IsEnabled=false;
this.IsDockable=_1.IsDockable;
this.InitialDockingZoneId=_1.InitialDockingZoneId;
this.VisibleDisplay=null;
this.EnableMaxWidth=true;
}
RadEditorModuleBase.prototype.GetTopElement=function(){
if(!this.IsCreated){
this.Create();
}
return this.TopElement;
};
RadEditorModuleBase.prototype.SetVisible=function(_2){
if(_2&&!this.IsEnabled){
return;
}
var _3=this.GetTopElement();
if(_2){
if(_3.Show){
_3.Show();
}else{
_3.style.display=this.VisibleDisplay;
}
}else{
if(_3.Hide){
_3.Hide();
}else{
_3.style.display="none";
}
}
};
RadEditorModuleBase.prototype.OnModuleResize=function(){
if(!window.event){
return;
}
var _4=window.event.srcElement;
if(_4.tagName!="TABLE"){
return;
}
if(_4&&null!=_4.IsDocked){
if(!_4.IsDocked()){
_4.ShowOverlay();
}
}
};
RadEditorModuleBase.prototype.CreateDockableWrapper=function(){
var _5=RadEditorNamespace.Utils.GetPlainTable(document);
_5.insertRow(-1);
if(this.IsDockable){
_5.width="100%";
}
var _6=_5.rows[0].insertCell(-1);
_6.innerHTML="<span style='font-size:1px;line-height:0px;'>&nbsp;</span>";
_6.setAttribute("height","100%");
_6.appendChild(this.ModuleElement);
if(this.IsDockable&&RadEditorNamespace.Docking){
var _7=RadEditorNamespace.Docking.WrapInDockingContainer(_5,this.IsVertical,RadEditorNamespace.RadEditorModule_RenderHorizontal,RadEditorNamespace.RadEditorModule_RenderVertical,"RadEModuleTable","RadEModuleTable",this.Title);
_5=_7;
}
this.VisibleDisplay=this.IsIE&&!this.IsOpera?"inline":"";
_5.className="RadEModuleTable";
if(!this.IsIE){
_5.setAttribute("style","float:left");
}
if(this.Editor.IsIE&&_5.attachEvent){
_5.attachEvent("onresize",RadEditorModuleBase.prototype.OnModuleResize);
}
return _5;
};
RadEditorModuleBase.prototype.Dispose=function(){
for(var i in this.EventHandlerQueue){
this.DetachEventHandler(i);
this.EventHandlerQueue[i]=null;
}
this.EventHandlerQueue=null;
try{
if(this.OnDispose){
this.OnDispose();
}
}
catch(e){
alert("Dispose failed for "+this.Title+" - "+e.message);
}
if(this.TopElement){
this.TopElement.OnRenderVertical=null;
this.TopElement.OnRenderHorizontal=null;
this.TopElement.OnResize=null;
this.TopElement.OnUndock=null;
this.TopElement.OnDock=null;
this.TopElement=null;
}
this.ModuleElement=null;
this.Editor=null;
};
RadEditorModuleBase.prototype.SetEnabled=function(_9){
this.IsEnabled=_9;
this.SetVisible(_9);
};
RadEditorModuleBase.prototype.Create=function(){
this.TopElement=this.CreateDockableWrapper();
var _a=this;
this.IsCreated=true;
if(this.OnCreate){
this.OnCreate();
}
return this.TopElement;
};
RadEditorNamespace.RadEditorModule_RenderHorizontal=function(){
this.style.width="";
if(this.OnRenderHorizontal!=null){
this.OnRenderHorizontal();
}
};
RadEditorNamespace.RadEditorModule_RenderVertical=function(){
this.style.width="100px";
if(this.OnRenderVertical!=null){
this.OnRenderVertical();
}
};
RadEditorModuleBase.prototype.GetLocalizedString=function(_b,_c){
var _d=typeof (this).toString();
var _e=this.Localization[_b];
return _e!=null?_e:_c;
};
RadEditorModuleBase.prototype.AttachEventHandler=function(_f,_10){
this.EventHandlerQueue[_f]=_10;
this.Editor.AttachEventHandler(_f,this.EventHandlerQueue[_f]);
};
RadEditorModuleBase.prototype.DetachEventHandler=function(_11){
this.Editor.DetachEventHandler(_11,this.EventHandlerQueue[_11]);
};
RadEditorModuleBase.prototype.OnCreate=function(){
};
RadEditorModuleBase.prototype.OnDispose=function(){
};;RadEditor.prototype.SetModulesVisible=function(_1){
var _2=this.Modules;
for(var _3=0;_3<_2.length;_3++){
_2[_3].SetVisible(_1);
}
};
RadEditor.prototype.LoadModules=function(_4){
var _5=_4.DefaultModulesArray.length;
if(_5>0){
for(var i=0;i<_5;i++){
var _7=_4.DefaultModulesArray[i];
var _8=0;
var _9=_7[_8++];
var _a=_7[_8++];
var _b=_7[_8++];
var _c=_7[_8++];
var _d=_7[_8++];
var _e=_7[_8++];
var _f=_7[_8++];
var id=null;
try{
_4.LoadModule(_9,_a,_b,_c,id);
}
catch(e){
}
}
var _11=function(_12){
_12.AttachEventHandler(RadEditorNamespace.RADEVENT_MODE_CHANGED,function(){
var _13=(_12.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE);
_12.SetModulesVisible(_13);
});
_12.AttachEventHandler(RadEditorNamespace.RADEVENT_SIZE_CHANGED,function(){
var _14=_12.Modules.length;
for(var i=0;i<_14;i++){
var _16=_12.Modules[i];
if(!_12.IsSafari){
_16.UpdateDockedSize();
}
}
});
};
_11(_4);
}
};
RadEditor.prototype.LoadModule=function(_17,_18,_19,_1a,id){
var _1c=this.GetLocalizedString(_17,_17);
var _1d={Editor:this,Document:document,Title:_1c,Id:id,InitialDockingZoneId:_18,IsDockable:_19&&this.EnableDocking};
var _1e=eval(_17);
var _1f=new _1e(_1d);
this.Modules[this.Modules.length]=_1f;
var _20=this;
if(!_1f.IsCreated){
var _21=_1f.Create();
var _22=this.GetDockingZoneById(_1f.InitialDockingZoneId);
_22.appendChild(_21);
if(_1f.IsDockable){
_20.MakeDockable(_21);
}
_1f.SetEnabled(_1a);
_20.ResetSize();
}
_1f.TopElement.OnUndock=function(){
_1f.Editor.ResetSize();
_1f.TopElement.style.width="";
_1f.TopElement.width="";
};
_1f.TopElement.OnDock=function(){
_1f.Editor.ResetSize();
_1f.UpdateDockedSize();
};
_1f.UpdateDockedSize=function(){
var _23=this;
var _24=false;
if(!_23.IsDockable){
var _25=_23.GetTopElement();
if(_25&&_25.parentNode&&!_23.Editor.IsZoneVertical(_25.parentNode)){
_24=true;
}
}else{
var _25=_23.GetTopElement();
if(_25.IsDocked&&_25.IsDocked()&&!_25.IsVertical){
_24=true;
}
}
if(_24&&_23.EnableMaxWidth&&_23.TopElement){
var _25=_23.TopElement;
var _26=_25.parentNode;
if(_26&&_26.style.width=="100%"){
_25.parentNode.style.width="100%";
}
_25.style.width="100%";
if(_25.Show&&_25.IsVisible&&_25.IsVisible()){
_25.Show();
}
}
};
return _1f;
};
RadEditor.prototype.GetModules=function(){
return this.Modules;
};;RadEditorNamespace.RadAlignmentSelector={New:function(_1){
_1.ClassName="RadEToolLong";
_1.PopupWidth=73;
_1.PopupHeight=85;
_1.CellSpacing=2;
_1.CellPadding=2;
_1.PopupClassName="RadAlignmentSelectorTable";
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.TagName="";
_2.ActiveAlignmentArray=null;
_2.SelectedTuple=null;
_2.NoAlignmentIndex=-1;
_2.SkinBasePath=_1.SkinBasePath;
_2.ItemsPerRow=3;
_2.IsPopupScrollable=false;
return _2;
},ImgAlignment:[["",""],["none",""],["",""],["",""],["top",""],["",""],["left",""],["absmiddle",""],["right",""],["",""],["bottom",""],["",""]],CellAlignment:[["",""],["none",""],["",""],["left","top"],["center","top"],["right","top"],["left","middle"],["center","middle"],["right","middle"],["left","bottom"],["center","bottom"],["right","bottom"]],TableAlignment:[["",""],["none",""],["",""],["left",""],["center",""],["right",""],["",""],["",""],["",""],["",""],["",""],["",""]],CaptionIEAlignment:[["",""],["none",""],["",""],["left","top"],["center","top"],["right","top"],["",""],["",""],["",""],["left","bottom"],["center","bottom"],["right","bottom"]],CaptionNSAlignment:[["",""],["none",""],["",""],["",""],["","top"],["",""],["",""],["",""],["",""],["",""],["","bottom"],["",""]],AlignmentImages:["x.gif","x.gif","x.gif","AlignTopLeft.gif","AlignTopCenter.gif","AlignTopRight.gif","AlignMiddleLeft.gif","AlignMiddleCenter.gif","AlignMiddleRight.gif","AlignBottomLeft.gif","AlignBottomCenter.gif","AlignBottomRight.gif"],SetTagName:function(_3){
this.TagName=_3;
this.ActiveAlignmentArray=this.GetLookupTableByTagName(this.TagName);
this.OnCellClick(this.NoAlignmentIndex);
},GetLookupTableByTagName:function(_4){
switch(_4.toUpperCase()){
case "IMG":
return this.ImgAlignment;
case "TABLE":
return this.TableAlignment;
case "TD":
return this.CellAlignment;
case "TH":
return this.CellAlignment;
case "CAPTION":
return (this.IsIE?this.CaptionIEAlignment:this.CaptionNSAlignment);
default:
return null;
}
},ConfigureAlignmentTable:function(_5){
var _6=this.PopupBodyElement;
var _7=0;
for(var i=0;i<_6.rows.length;i++){
var _9=false;
for(var j=0;j<_6.rows[i].cells.length;j++){
var _b=_6.rows[i].cells[j];
var _c=this.IsAvailable(_7++);
_b.style.visibility=_c?"visible":"hidden";
_9|=_c;
}
if(null!=document.all){
_6.rows[i].style.display=_9?"":"none";
}
}
},IsAvailable:function(_d){
var _e=false;
if(this.ActiveAlignmentArray){
var _f=this.ActiveAlignmentArray[_d];
_e=((null!=_f)&&(""!=_f[0]||""!=_f[1]));
}
return _e;
},SetButtonImage:function(_10){
var _11=this.GetTopElement().getElementsByTagName("IMG")[0];
_11.src=this.SkinBasePath+"Img/"+_10;
_11.style.margin="4px";
},OnCellClick:function(_12){
if(_12==this.NoAlignmentIndex){
this.SelectedTuple="";
this.SetButtonImage("x.gif");
}else{
if(this.ActiveAlignmentArray&&0<=_12&&_12<this.ActiveAlignmentArray.length){
this.SelectedTuple=this.ActiveAlignmentArray[_12];
this.SetButtonImage(this.AlignmentImages[_12]);
if(""!=this.ClientClickString){
eval(this.ClientClickString);
}
}
}
},SelectAlignment:function(_13,_14){
_13=(""==_13||!_13)?"none":_13.toUpperCase();
_14=!_14?"":_14.toUpperCase();
if(this.ActiveAlignmentArray){
var _15=-1;
for(i=0;i<this.ActiveAlignmentArray.length;i++){
if(this.IsAvailable(i)){
var ha=this.ActiveAlignmentArray[i][0].toUpperCase();
var va=this.ActiveAlignmentArray[i][1].toUpperCase();
if(-1==_15){
_15=i;
}
if((_13==ha||_13==va)&&(_14==ha||_14==va)){
this.OnCellClick(i);
return;
}
}
}
this.OnCellClick(_15);
}
},SetValue:function(_18,_19){
this.SelectAlignment(_18,_19);
},GetAlign:function(){
var _1a=(this.SelectedTuple?this.SelectedTuple[0]:"");
if("none"==_1a){
_1a="";
}
return _1a;
},GetVAlign:function(){
var _1b=(this.SelectedTuple?this.SelectedTuple[1]:"");
if("none"==_1b){
_1b="";
}
return _1b;
},OnBeforeShowPopup:function(){
if(!this.ItemsCreated){
this.CreateItems();
this.ItemsCreated=true;
}
this.ConfigureAlignmentTable(this.TagName);
},CreateItems:function(){
var _1c=this.PopupBodyElement;
var _1d=null;
var _1e=0;
var _1f=this.AlignmentImages.length;
for(var i=0;i<_1f;i++){
if(0==i%this.ItemsPerRow){
_1d=_1c.insertRow(-1);
_1e=0;
}
_1e++;
this.AddAlignmentCell(_1d,i);
}
},AddAlignmentCell:function(_21,_22){
var _23=_21.insertCell(-1);
_23.RadClassOver="Over";
_23.RadClassOut="";
_23.onmouseout=RadEditorNamespace.OnItemMouseOut;
_23.onmouseover=RadEditorNamespace.OnItemMouseOver;
var _24=this.Popup.CreateElement("img");
_24.align="absMiddle";
_24.border="0";
_24.src=this.SkinBasePath+"Img/"+this.AlignmentImages[_22];
_23.appendChild(_24);
this.ConfigureCell(_23,this,_22);
return _23;
}};;RadEditorDomInspector.prototype=new RadEditorModuleBase();
RadEditorDomInspector.prototype.base=RadEditorModuleBase.prototype.constructor;
var DOM_INSPECTOR_HORIZONTAL_HEIGHT="20px";
var DOM_INSPECTOR_VERTICAL_HEIGHT="80px";
function RadEditorDomInspector(_1){
var _2=_1.Document.createElement("DIV");
_2.style.paddingTop="2px";
if(window.opera){
_2.style.height=DOM_INSPECTOR_HORIZONTAL_HEIGHT;
_2.style.lineHeight=DOM_INSPECTOR_HORIZONTAL_HEIGHT;
}
_1.ModuleElement=_2;
this.base(_1);
this.SelectedDomCouple=null;
this.RemoveElementString=this.GetLocalizedString("DomInspectorRemoveElement","Remove Element");
}
RadEditorDomInspector.prototype.OnDispose=function(){
this.Clear();
this.SelectedDomCouple=null;
this.SelectedElement=null;
};
RadEditorDomInspector.prototype.OnRenderHorizontal=function(){
this.style.height=DOM_INSPECTOR_HORIZONTAL_HEIGHT;
};
RadEditorDomInspector.prototype.OnRenderVertical=function(){
this.style.height=DOM_INSPECTOR_VERTICAL_HEIGHT;
};
RadEditorDomInspector.prototype.OnCreate=function(){
var _3=this;
this.AttachEventHandler(RadEditorNamespace.RADEVENT_SEL_CHANGED,function(){
_3.CreatePath();
});
var _4=this.TopElement;
_4.OnRenderVertical=this.OnRenderVertical;
_4.OnRenderHorizontal=this.OnRenderHorizontal;
_4.style.width=this.Editor.Width;
_4.style.height=DOM_INSPECTOR_HORIZONTAL_HEIGHT;
};
RadEditorDomInspector.prototype.CreatePath=function(){
if(!this.IsEnabled){
return;
}
var _5=this.Editor.GetSelectedElement();
if(!_5){
return;
}
var _6=this.Editor.ContentArea;
if(this.Editor.IsIE&&!_6.contains(_5)){
return;
}
var _7=[];
var _8=0;
while(_5!=_6&&null!=_5){
_7[_8++]=_5;
_5=_5.parentNode;
}
this.Clear();
var _9=null;
var _a;
var _b=false;
for(var i=_7.length-1;i>=0;i--){
_a=_7[i];
_b=(0==i)||(null!=this.SelectedElement&&_a==this.SelectedElement);
if(_a&&_a.tagName){
isSelected=(0==i)||(null!=this.SelectedElement&&_a==this.SelectedElement);
_9=this.AddDomCouple(_a,isSelected);
}
if(_b){
break;
}
}
this.SelectedElement=null;
this.SelectedDomCouple=_9;
if(this.SelectedDomCouple){
var _d=this.Document.createElement("A");
_d.innerHTML=this.RemoveElementString;
_d.href="javascript:void(0)";
_d.className="DomPathLink";
_d.Parent=this;
_d.onmousedown=new Function("this.Parent.RemoveSelectedElement();return false;");
this.ModuleElement.appendChild(_d);
}
};
RadEditorDomInspector.prototype.AddDomCouple=function(_e,_f){
var _10=this.Document.createElement("A");
this.ModuleElement.appendChild(_10);
var _11=this.Document.createElement("SPAN");
_11.innerHTML="&nbsp;> ";
this.ModuleElement.appendChild(_11);
var _12=new DomCouple(_10,_e,(_f?"DomPathLinkSelected":""),_f,this);
return _12;
};
RadEditorDomInspector.prototype.Clear=function(){
if(this.SelectedDomCouple){
this.SelectedDomCouple.Clear();
}
var _13=this.ModuleElement.getElementsByTagName("A");
for(i=0;i<_13.length;i++){
var _14=_13[i];
_14.Parent=null;
}
this.ModuleElement.innerHTML="";
};
RadEditorDomInspector.prototype.RemoveSelectedElement=function(){
if(!this.Editor.IsEditingEnabled()){
return;
}
if(!this.SelectedDomCouple||!this.SelectedDomCouple.DomElement){
return;
}
var _15=this.SelectedDomCouple.DomElement;
try{
if(_15.tagName=="TD"||_15.tagName=="TH"){
this.Editor.DeleteCell();
}else{
if(_15.tagName=="TR"){
this.Editor.DeleteRow();
}else{
if(_15.tagName=="TABLE"||_15.tagName=="TBODY"||_15.tagName=="THEAD"||_15.tagName=="TFOOT"||_15.tagName=="EMBED"||_15.tagName=="OBJECT"||_15.tagName=="INPUT"||_15.tagName=="IMG"||_15.tagName=="HR"){
var cmd=RadEditorNamespace.RadGenericCommand.New(this.RemoveElementString,this.Editor.ContentWindow);
var _17=_15.parentNode;
_17.removeChild(_15);
this.Editor.SetFocus();
this.Editor.ExecuteCommand(cmd);
_18.Select();
this.Editor.SetActive();
this.Editor.SetFocus();
}else{
if(_15.tagName!="BODY"){
var _18=RadEditorNamespace.RadCreateRestorePoint(this.Editor.ContentWindow);
var _17=_15.parentNode;
var cmd=RadEditorNamespace.RadGenericCommand.New(this.RemoveElementString,this.Editor.ContentWindow);
var _19="";
for(var i=0;i<_17.childNodes.length;i++){
if(_15!=_17.childNodes[i]){
_19+=RadEditorNamespace.Utils.GetOuterHtml(_17.childNodes[i]);
}else{
_19+=_15.innerHTML;
}
}
_17.innerHTML=_19;
this.Editor.SetFocus();
this.Editor.ExecuteCommand(cmd);
_18.Select();
this.Editor.SetActive();
this.Editor.SetFocus();
}
}
}
}
}
catch(ex){
}
this.Editor.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
};
RadEditorDomInspector.prototype.SelectElement=function(_1b){
try{
this.SelectedElement=_1b;
this.Editor.SelectElement(_1b);
this.SelectedElement=null;
}
catch(ex){
}
};
function DomCouple(_1c,_1d,_1e,_1f,_20){
this.RadEditorDomInspector=_20;
this.DomLink=_1c;
this.DomElement=_1d;
this.Selected=_1f;
if(_1c){
_1c.href="javascript:void(0);";
_1c.className=_1e?_1e:"DomPathLink";
_1c.Parent=this;
_1c.onclick=new Function("this.Parent.SelectElement();");
_1c.onmouseover=new Function("this.Parent.onmouseover();");
_1c.onmouseout=new Function("this.Parent.onmouseout();");
if(_1d!=null){
_1c.innerHTML=_1d.tagName;
}
}
}
DomCouple.prototype.Clear=function(){
this.RadEditorDomInspector.SelectedDomCouple=null;
this.DomLink.className="DomPathLink";
};
DomCouple.prototype.onmouseover=function(){
if(this.Selected){
return;
}
try{
this.DomElementClass=this.DomElement.className;
this.DomElement.className+=" RadEDomMouseOver";
}
catch(e){
}
};
DomCouple.prototype.onmouseout=function(){
if(null!=this.DomElementClass){
this.DomElement.className=this.DomElementClass;
}
try{
if(""==this.DomElement.className){
this.DomElement.removeAttribute("className",0);
this.DomElement.removeAttribute("class",0);
}
}
catch(e){
}
};
DomCouple.prototype.SelectElement=function(){
this.onmouseout();
if(this.RadEditorDomInspector.SelectedDomCouple){
this.RadEditorDomInspector.SelectedDomCouple.Clear();
}
this.DomLink.className="DomPathLinkSelected";
this.RadEditorDomInspector.SelectedDomCouple=this;
this.RadEditorDomInspector.SelectElement(this.DomElement);
};;RadEditorHtmlInspector.prototype=new RadEditorModuleBase();
RadEditorHtmlInspector.prototype.base=RadEditorModuleBase.prototype.constructor;
function RadEditorHtmlInspector(_1){
var _2=_1.Document.createElement("TEXTAREA");
_1.ModuleElement=_2;
_2.style.width="99%";
_1.ClassName="RadETextArea";
this.base(_1);
this.ModuleElement.setAttribute("rows","10");
this.ModuleElement.setAttribute("cols","80");
if(!document.all){
this.ModuleElement.onclick=function(){
this.focus();
};
}
}
RadEditorHtmlInspector.prototype.OnDispose=function(){
this.ModuleElement.onkeyup=null;
};
RadEditorHtmlInspector.prototype.IsSelChanged=function(_3){
if(_3==32||_3==9||_3==8||_3==46||_3==13){
return true;
}
if(_3==190){
return true;
}
if(_3>47&&_3<58){
return true;
}
return false;
};
RadEditorHtmlInspector.prototype.OnCreate=function(){
var _4=this;
this.AttachEventHandler(RadEditorNamespace.RADEVENT_SEL_CHANGED,function(){
_4.OnSelectionChanged();
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_CALLBACK_STARTED,function(){
_4.ModuleElement.value="";
});
this.ModuleElement.onkeyup=function anon(e){
if(!e){
e=window.event;
}
if(_4.IsSelChanged(e.keyCode)){
_4.UpdateContentArea(e);
}
};
var _6=this;
this.TopElement.OnResize=function(){
var _7=_6.ModuleElement;
_7.style.height=(parseInt(_6.TopElement.offsetHeight)-3)+"px";
_7.style.width=(parseInt(_6.TopElement.offsetWidth)-3)+"px";
};
};
RadEditorHtmlInspector.prototype.UpdateContentArea=function(e){
if(!this.IsEnabled){
return;
}
this.Flag=true;
this.Editor.SetHtml(this.ModuleElement.value,this.Localization["Typing"],false);
this.ModuleElement.focus();
};
RadEditorHtmlInspector.prototype.OnSelectionChanged=function(){
if(this.Flag){
this.Flag=false;
return;
}
this.ModuleElement.value=this.Editor.GetHtml();
};;RadEditorNamespace.RadSpinBox=function(_1){
this.Width=_1.Width?parseInt(_1.Width)+"px":"50px";
this.Title=_1.Title;
this.Controller=_1.Controller;
this.Name=_1.Name;
this.Document=_1.Document;
this.SelectedValue="";
this.ClassName="RadETextBox";
};
RadEditorNamespace.RadSpinBox.prototype.Dispose=function(){
this.Element.onchange=null;
this.Element.onkeypress=null;
this.Element.onclick=null;
this.Element.Parent=null;
this.Element=null;
};
RadEditorNamespace.RadSpinBox.prototype.GetTopElement=function(){
return this.Element;
};
RadEditorNamespace.RadSpinBox.prototype.GetSelectedValue=function(){
return this.SelectedValue;
};
RadEditorNamespace.RadSpinBox.prototype.SetValue=function(_2){
this.Element.value=_2;
};
RadEditorNamespace.RadSpinBox.prototype.Create=function(){
var _3=this.Document.createElement("INPUT");
_3.setAttribute("size","3");
_3.style.width=this.Width;
_3.className=this.ClassName;
_3.Parent=this;
var _4=this.Name;
var _5=function(e,_7,_8){
if(_8){
if(_7.Executed){
_7.Executed=false;
return RadEditorNamespace.Utils.CancelEvent(e);
}
}
_7.Executed=true;
_7.Parent.SelectedValue=_7.value;
_7.Parent.Controller.Fire(_4,_7.Parent);
return RadEditorNamespace.Utils.CancelEvent(e);
};
_3.onchange=function(e){
if(!e){
e=window.event;
}
return _5(e,this,true);
};
_3.onclick=function(e){
this.focus();
};
_3.onkeypress=function(e){
if(!e){
e=window.event;
}
if(e.keyCode==13){
return _5(e,this);
}
};
this.Element=_3;
return this.Element;
};
RadEditorNamespace.RadCheckBox=function(_c){
this.Title=_c.Title;
this.Controller=_c.Controller;
this.Name=_c.Name;
this.Document=_c.Document;
this.SelectedValue=false;
};
RadEditorNamespace.RadCheckBox.prototype.Dispose=function(){
this.Element.onclick=null;
this.Element.Parent=null;
this.Element=null;
};
RadEditorNamespace.RadCheckBox.prototype.GetTopElement=function(){
return this.Element;
};
RadEditorNamespace.RadCheckBox.prototype.GetSelectedValue=function(){
return this.SelectedValue;
};
RadEditorNamespace.RadCheckBox.prototype.SetValue=function(_d){
if(this.Element.checked!=_d){
this.Element.checked=_d;
}
};
RadEditorNamespace.RadCheckBox.prototype.Create=function(){
var _e=this.Document.createElement("INPUT");
_e.setAttribute("type","CHECKBOX");
_e.Parent=this;
_e.onclick=function(e){
var oP=this.Parent;
oP.SelectedValue=!oP.SelectedValue;
this.checked=oP.SelectedValue;
oP.SelectedValue=this.checked;
oP.Controller.Fire(oP.Name,oP);
};
this.Element=_e;
return this.Element;
};
RadEditorNamespace.RadTargetBox=function(_11){
this.Title=_11.Title;
this.Controller=_11.Controller;
this.Name=_11.Name;
this.Document=_11.Document;
this.Width=_11.Width?_11.Width:"95px";
this.TargetList=_11.TargetList;
this.SelectedValue="";
};
RadEditorNamespace.RadTargetBox.prototype.Dispose=function(){
this.Element.onchange=null;
this.Element.Parent=null;
this.TargetList=null;
this.Element=null;
};
RadEditorNamespace.RadTargetBox.prototype.GetTopElement=function(){
return this.Element;
};
RadEditorNamespace.RadTargetBox.prototype.GetSelectedValue=function(){
return this.SelectedValue;
};
RadEditorNamespace.RadTargetBox.prototype.SetValue=function(_12){
var _13=this.Element.options;
for(var i=0;i<_13.length;i++){
if(_13[i].value==_12){
this.Element.selectedIndex=i;
return;
}
}
this.Element.selectedIndex=-1;
};
RadEditorNamespace.RadTargetBox.prototype.Create=function(){
var _15=this.Document.createElement("SELECT");
_15.className="RadEDropDown";
_15.style.width=this.Width;
var _16=this.TargetList;
_15.options[0]=new Option("---","");
for(var _17 in _16){
if(typeof (_16[_17])=="string"){
_15.options[_15.options.length]=new Option(_16[_17],_17);
}
}
_15.Parent=this;
var _18=this.Name;
_15.onchange=function(){
this.Parent.SelectedValue=this.options[this.selectedIndex].value;
this.Parent.Controller.Fire(_18,this.Parent);
};
this.Element=_15;
return this.Element;
};
RadEditorNamespace.ATTRIBUTE_INSPECTOR_HORIZONTAL_HEIGHT="51px";
RadEditorNodeInspector.prototype=new RadEditorModuleBase();
RadEditorNodeInspector.prototype.base=RadEditorModuleBase.prototype.constructor;
function RadEditorNodeInspector(_19){
var _1a=window.opera?"SPAN":"DIV";
_19.ModuleElement=_19.Document.createElement(_1a);
this.base(_19);
this.ArrowDropdownUrl=this.Editor.SkinBasePath+"Buttons/arrowDropdown.gif";
this.ArrowIconUrl=this.Editor.SkinBasePath+"Buttons/arrowIcon.gif";
this.IsIE=document.all&&!window.opera?true:false;
this.Controls={};
this.ControlNames={};
this.SelectedElement=null;
this.SelectedElementString=this.GetLocalizedString("NodeInspectorSelectedElement","The selected element is ");
this.InvalidValueString=this.GetLocalizedString("NodeInspectorInvalidValue","Invalid value. Please enter a number.");
}
RadEditorNodeInspector.prototype.GetNamedCssForSelectedElement=function(_1b){
return this.Editor.GetNamedCssForSelectedElement(_1b);
};
RadEditorNodeInspector.prototype.OnDispose=function(){
var _1c=this.Controls;
for(var _1d in _1c){
var _1e=_1c[_1d];
if(_1e.Dispose){
_1e.Dispose();
}
}
var _1f=this.MainPanel;
if(_1f){
for(var _20=0;_20<_1f.rows.length;_20++){
var _21=_1f.rows[_20];
var len=_21.cells.length;
for(var i=0;i<len;i++){
var _24=_21.cells[0];
_24.style.display="";
_24.parentNode.deleteCell(_24);
}
}
}
this.Controls=null;
this.MainPanel=null;
};
RadEditorNodeInspectorAttributesArray=[["rows","NAME","width","cellSpacing","align","href","value","className",RadEditorNamespace.RADCOMMAND_SET_TABLE_PROPERTIES,RadEditorNamespace.RADCOMMAND_SET_CELL_PROPERTIES,RadEditorNamespace.RADCOMMAND_SET_LINK_PROPERTIES,RadEditorNamespace.RADCOMMAND_SET_IMAGE_PROPERTIES],["cols","id","height","action","cellPadding","borderColor","bgColor","border","alt","noWrap","target","title"]];
RadEditorNodeInspector.prototype.NodeAttributesArray={};
RadEditorNodeInspector.prototype.NodeAttributesArray["TABLE"]=["width","cellSpacing","bgColor","className",RadEditorNamespace.RADCOMMAND_SET_TABLE_PROPERTIES,"height","cellPadding","align","border"];
RadEditorNodeInspector.prototype.NodeAttributesArray["TH"]=RadEditorNodeInspector.prototype.NodeAttributesArray["TD"]=["width","bgColor","className",RadEditorNamespace.RADCOMMAND_SET_CELL_PROPERTIES,"height","align","noWrap"];
RadEditorNodeInspector.prototype.NodeAttributesArray["TR"]=["width","className","height"];
RadEditorNodeInspector.prototype.NodeAttributesArray["A"]=["href","className",RadEditorNamespace.RADCOMMAND_SET_LINK_PROPERTIES,"title","target"];
RadEditorNodeInspector.prototype.NodeAttributesArray["IMG"]=["width","borderColor","className",RadEditorNamespace.RADCOMMAND_SET_IMAGE_PROPERTIES,"height","align","border","alt"];
RadEditorNodeInspector.prototype.NodeAttributesArray["INPUT"]=["NAME","width","height","id","title","value","className"];
RadEditorNodeInspector.prototype.NodeAttributesArray["FORM"]=["className","width","height","NAME","action","id"];
RadEditorNodeInspector.prototype.NodeAttributesArray["TEXTAREA"]=["className","width","height","NAME","id","rows","cols"];
RadEditorNodeInspector.prototype.OnCreate=function(){
var _25=this;
this.AttachEventHandler(RadEditorNamespace.RADEVENT_SEL_CHANGED,function(){
_25.UpdateMainPanel();
});
var _26=this.TopElement;
_26.OnRenderVertical=RadEditorNamespace.AttributeInspector_OnRenderVertical;
_26.OnRenderHorizontal=RadEditorNamespace.AttributeInspector_OnRenderHorizontal;
_26.style.height=RadEditorNamespace.ATTRIBUTE_INSPECTOR_HORIZONTAL_HEIGHT;
_26.style.width=this.Editor.Width;
};
RadEditorNodeInspector.prototype.CreateMainPanel=function(){
var _27=this.Controls;
var _28=this.ControlNames;
var _29=RadEditorNodeInspectorAttributesArray;
var _2a=this.Document.createElement("TABLE");
_2a.border=0;
_2a.cellSpacing=0;
_2a.cellPadding=0;
for(var _2b=0;_2b<_29.length;_2b++){
var _2c=_29[_2b];
var _2d=_2a.insertRow(-1);
for(var i=0;i<_2c.length;i++){
var _2f=_2c[i];
var _30=_2d.insertCell(-1);
_30.style.display="none";
_30.setAttribute("controlName",_2f);
_30.innerHTML=this.GetLocalizedString(_2f,_2f);
_30.className="RadEToolText";
_30.style.paddingLeft="4px";
_30=_2d.insertCell(-1);
_30.style.display="none";
_30.setAttribute("controlHolder",_2f);
var _31=this.GetControlByName(_2f);
if(_31){
_27[_2f]=_31;
_30.appendChild(_31.GetTopElement());
}
}
}
return _2a;
};
RadEditorNodeInspector.prototype.UpdateMainPanel=function(){
if(!this.IsEnabled){
return;
}
if(!this.IsMainCreated){
this.MainPanel=this.CreateMainPanel();
this.MainPanel.style.display="none";
this.ModuleElement.appendChild(this.MainPanel);
this.IsMainCreated=true;
}
var _32=this.Editor.GetSelectedElement();
if(!_32||_32.tagName=="BODY"||_32.ownerDocument!=this.Editor.Document){
this.MainPanel.style.display="none";
return;
}
if(_32.tagName=="TBODY"&&this.Editor.IsOpera){
_32=_32.parentNode;
}
var _33=this.NodeAttributesArray[_32.tagName];
if(!_33){
var _34=RadEditorNamespace.Utils.GetElementParentByTag(_32,"A");
if(!_34){
_34=RadEditorNamespace.Utils.GetElementParentByTag(_32,"TD");
}
if(!_34){
_34=RadEditorNamespace.Utils.GetElementParentByTag(_32,"TH");
}
if(_34){
_32=_34;
}else{
this.MainPanel.style.display="none";
return;
}
}
var _35=null;
if(this.SelectedElement){
try{
_35=this.SelectedElement.tagName;
}
catch(e){
}
}
if(!this.SelectedElement||(_35!=_32.tagName)){
var _36=this.Editor;
var _37=this.Editor.GetDocument();
var _38=this.Controls["className"];
_38.IsCreated=false;
_38.GetDataFunction=function(_39){
return _36.GetCssClassesByTagName(_32.tagName,_36.Document);
};
this.Controls["align"].SetTagName(_32.tagName);
}
this.SelectedElement=_32;
this.UpdateControlValues(this.SelectedElement);
this.MainPanel.style.display="";
};
RadEditorNodeInspector.prototype.UpdateControlValues=function(_3a){
var _3b=this.NodeAttributesArray[_3a.tagName];
var _3c=this.MainPanel;
var _3d=this.Controls;
for(var _3e=0;_3e<_3c.rows.length;_3e++){
var _3f=_3c.rows[_3e];
for(var i=0;i<_3f.cells.length;i++){
var _41=_3f.cells[i];
var _42=_41.getAttribute("controlName");
if(_42){
_41.style.display=this.ArrayValueExists(_42,_3b)?"":"none";
}
var _43=_41.getAttribute("controlHolder");
if(_43){
_41.style.display=this.ArrayValueExists(_43,_3b)?"":"none";
if("none"==_41.style.display){
continue;
}
var _44=_3d[_43];
var _45=_3a.getAttribute?_3a.getAttribute(_43):"";
if(_43=="noWrap"){
_44.SetValue(_3a.noWrap);
}else{
if(_43=="align"){
_44.SetValue(_3a.getAttribute("align"),_3a.getAttribute("vAlign"));
}else{
if(_43=="borderColor"||_43=="width"||_43=="height"){
var _46=_3a.style[_43];
if(!_46){
_46=_3a.getAttribute(_43);
}
_44.SetValue(_46);
}else{
if("name"==_43.toLowerCase()){
_44.SetValue(_3a.name);
}else{
if(_45){
_44.SetValue(_45);
}else{
if(!this.IsIE&&"className"==_43){
var _45=_3a.getAttribute?_3a.getAttribute("class"):"";
if(_45){
_44.SetValue(_45);
}
}else{
if(_44.SetValue){
_44.SetValue("");
}
}
}
}
}
}
}
}
}
}
};
RadEditorNodeInspector.prototype.Fire=function(_47,_48){
if(!this.Editor.IsEditingEnabled()){
return;
}
var _49=this.GetLocalizedString(_47,_47);
var _4a=this.SelectedElement;
if(_47==RadEditorNamespace.RADCOMMAND_SET_CELL_PROPERTIES||_47==RadEditorNamespace.RADCOMMAND_SET_TABLE_PROPERTIES||_47==RadEditorNamespace.RADCOMMAND_SET_IMAGE_PROPERTIES||_47==RadEditorNamespace.RADCOMMAND_SET_LINK_PROPERTIES){
this.Editor.Fire(_47,this);
return;
}else{
if("align"==_47){
var _4b=_48.GetAlign();
var _4c=_48.GetVAlign();
this.Editor.ExecuteSetAttributeCommand(_4a,"align",_4b,_49);
_49=this.GetLocalizedString("vAlign","vAlign");
this.Editor.ExecuteSetAttributeCommand(_4a,"vAlign",_4c,_49);
}else{
if("borderColor"==_47){
var _4d=_48.GetSelectedValue();
this.Editor.ExecuteSetStyleRuleCommand(this.SelectedElement,"borderColor",_4d,_49);
}else{
if("width"==_47||"height"==_47){
var _4d=_48.GetSelectedValue();
if(!this.IsValidAttribValue(_4d)){
alert(this.InvalidValueString);
return;
}
function ConvertIntToPixel(_4e){
var _4f=""+_4e;
if(_4f.indexOf("%")!=-1){
return _4f;
}else{
_4f=parseInt(_4f);
if(!isNaN(_4f)){
_4f=_4f+"px";
return _4f;
}
}
return _4e;
}
_4d=ConvertIntToPixel(_4d);
if(this.SelectedElement.removeAttribute){
this.SelectedElement.removeAttribute(_47);
}
this.Editor.ExecuteSetStyleRuleCommand(this.SelectedElement,_47,_4d,_49);
}else{
var _50=_47;
var _51=_48.GetSelectedValue();
switch(_47){
case RadEditorNamespace.RADCOMMAND_APPLY_CLASS:
_50="className";
break;
case RadEditorNamespace.RADCOMMAND_BACKCOLOR:
_50="bgColor";
break;
case "value":
break;
case "noWrap":
if(_51){
_51="noWrap";
}else{
_51="";
}
break;
case "border":
case "cellSpacing":
case "cellPadding":
if(!this.IsValidAttribValue(_51)){
alert(this.InvalidValueString);
return;
}
break;
case "NAME":
if(!this.IsIE){
_50="name";
}
}
this.Editor.ExecuteSetAttributeCommand(_4a,_50,_51,_49);
}
}
}
}
this.Editor.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED);
};
RadEditorNodeInspector.prototype.GetSelectedValue=function(){
return this.SelectedElement;
};
RadEditorNodeInspector.prototype.GetIconUrl=function(_52){
return (this.Editor.SkinBasePath+"Buttons/"+_52);
};
RadEditorNodeInspector.prototype.ArrayValueExists=function(_53,_54){
for(var i=0;i<_54.length;i++){
if(_54[i]==_53){
return true;
}
}
return false;
};
RadEditorNodeInspector.prototype.IsValidAttribValue=function(_56){
if(null==_56){
return false;
}
_56=RadEditorNamespace.Utils.Trim(_56);
if(""==_56){
return true;
}
var _57=parseInt(_56);
if(isNaN(_57)){
return false;
}
return true;
};
RadEditorNodeInspector.prototype.GetControlByName=function(_58){
var _59=null;
var _5a=null;
var _5b=this.Editor;
var _5c=this;
var _5d=this.Localization;
var _5e=function(){
return _5c;
};
switch(_58){
case "className":
var _5f={GetController:_5e,Document:this.Document,Name:RadEditorNamespace.RADCOMMAND_APPLY_CLASS,Title:this.Localization[RadEditorNamespace.RADCOMMAND_APPLY_CLASS],ArrowUrl:this.ArrowDropdownUrl,Width:80,PopupWidth:180,PopupHeight:150,CellSpacing:2,CellPadding:2,UseCssArray:false,ClearStyleString:_5d["ClearStyle"],PopupIconPath:this.Editor.SkinBasePath+"Img/"};
_59=RadEditorNamespace.RadCssCombo.New(_5f);
_59.Create();
break;
case "borderColor":
case "bgColor":
var _5f={GetController:_5e,Document:this.Document,Name:"borderColor"==_58?"borderColor":RadEditorNamespace.RADCOMMAND_BACKCOLOR,AddCustomColor:_5d["AddCustomColor"],AddCustomHexColor:_5d["AddCustomHexColor"],PromptColor:_5d["PromptColor"],Title:this.GetLocalizedString(_58),ArrowUrl:this.ArrowIconUrl,PopupWidth:120,PopupHeight:120,CellSpacing:1,CellPadding:1,IconUrl:this.GetIconUrl("BackColor.gif"),AllowCustomColors:this.Editor.AllowCustomColors,GetDataFunction:function(_60){
return _5b.GetDataArrayForTool("BackColor");
}};
_59=RadEditorNamespace.RadColorPicker.New(_5f);
_59.Create();
break;
case "align":
var _5f={GetController:_5e,Document:this.Document,Name:"align",Title:this.GetLocalizedString("align","align"),ArrowUrl:this.ArrowIconUrl,SkinBasePath:this.Editor.SkinBasePath,IconUrl:this.GetIconUrl("../Img/AlignMiddleLeft.gif")};
_59=RadEditorNamespace.RadAlignmentSelector.New(_5f);
_59.Create();
break;
case RadEditorNamespace.RADCOMMAND_SET_CELL_PROPERTIES:
case RadEditorNamespace.RADCOMMAND_SET_TABLE_PROPERTIES:
case RadEditorNamespace.RADCOMMAND_SET_IMAGE_PROPERTIES:
case RadEditorNamespace.RADCOMMAND_SET_LINK_PROPERTIES:
var _5f={GetController:_5e,Document:this.Document,Name:_58,Title:this.Localization[_58],IconUrl:this.GetIconUrl(_58+".gif")};
_59=RadEditorNamespace.RadToolBase.New(_5f);
_59.Create();
break;
case "target":
var _61={_blank:this.GetLocalizedString("_blank","_blank"),_self:this.GetLocalizedString("_self","_self"),_parent:this.GetLocalizedString("_parent","_parent"),_top:this.GetLocalizedString("_top","_top"),_search:this.GetLocalizedString("_search","_search"),_media:this.GetLocalizedString("_media","_media")};
var _5f={TargetList:_61,Controller:this,Document:this.Document,Name:_58};
_59=new RadEditorNamespace.RadTargetBox(_5f);
_59.Create();
break;
case "noWrap":
var _5f={Controller:this,Document:this.Document,Name:_58};
_59=new RadEditorNamespace.RadCheckBox(_5f);
_59.Create();
break;
case "href":
_5a=200;
case "title":
case "value":
case "NAME":
case "action":
case "id":
if(!_5a){
_5a=110;
}
default:
var _5f={Controller:this,Document:this.Document,Name:_58,Title:this.Localization[_58],Width:_5a};
_59=new RadEditorNamespace.RadSpinBox(_5f);
_59.Create();
}
return _59;
};
RadEditorNamespace.AttributeInspector_OnRenderHorizontal=function(){
var _62=this.getElementsByTagName("TABLE")[0];
var _63=RadEditorNodeInspectorAttributesArray.length;
var _64=_62.rows.length;
for(var _65=0;_65<_63;_65++){
var _66=_62.insertRow(_62.rows.length);
var _67=RadEditorNodeInspectorAttributesArray[_65].length*2;
for(var i=0;i<_67;i++){
var _69=_62.rows[0];
var _6a=_69.cells[0];
_69.removeChild(_6a);
_69.parentNode.removeChild(_69);
_66.appendChild(_6a);
}
}
this.style.display="inline";
this.style.height=RadEditorNamespace.ATTRIBUTE_INSPECTOR_HORIZONTAL_HEIGHT;
};
RadEditorNamespace.AttributeInspector_OnRenderVertical=function(){
var _6b=this.getElementsByTagName("TABLE")[0];
var _6c=_6b.rows.length;
for(var i=0;i<_6c;i++){
var _6e=_6b.rows[0];
var _6f=_6e.cells.length;
for(var j=0;j<_6f;j++){
var _71=_6e.cells[0];
var _72=_6b.insertRow(_6b.rows.length);
_6e.removeChild(_71);
_72.appendChild(_71);
}
_6e.parentNode.removeChild(_6e);
}
this.style.display="block";
};;RadEditorStatistics.prototype=new RadEditorModuleBase();
RadEditorStatistics.prototype.base=RadEditorModuleBase.prototype.constructor;
function RadEditorStatistics(_1){
var _2=_1.Document.createElement("DIV");
_1.ModuleElement=_2.cloneNode(true);
this.base(_1);
this.EnableMaxWidth=false;
this.WordsString=this.GetLocalizedString("StatisticsWords","Words:");
this.CharactersString=this.GetLocalizedString("StatisticsCharacters","Characters:");
}
RadEditorStatistics.prototype.OnCreate=function(){
var _3=this;
this.AttachEventHandler(RadEditorNamespace.RADEVENT_SEL_CHANGED,function(){
_3.DoCount();
});
if(this.Editor.Document.body){
this.AttachEventHandler("onblur",function(){
_3.DoCount();
});
}
var _4=this.IsEnabled;
this.IsEnabled=true;
this.DoCount();
this.IsEnabled=_4;
};
RadEditorStatistics.prototype.DoCount=function(){
if(!this.IsEnabled||!this.Editor.Document.body){
return;
}
var _5=this.Editor.GetText();
var _6=0;
var _7=0;
if(_5){
punctRegX=/[!\.?;,:&_\-\\{\}\[\]\(\)~#'"]/g;
_5=_5.replace(punctRegX,"");
trimRegX=/(^\s+)|(\s+$)/g;
_5=_5.replace(trimRegX,"");
if(_5){
splitRegX=/\s+/;
var _8=_5.split(splitRegX);
_6=_8.length;
_7=_5.length;
}
}
this.ModuleElement.innerHTML="<span style='line-height:22px'>"+this.WordsString+" "+_6+" "+this.CharactersString+" "+_7+"&nbsp;</span>";
};;if(typeof (RadEditorXhtmlValidatorDocTypes)=="undefined"){
RadEditorXhtmlValidatorDocTypes={"XHTML 1.0 Strict":["!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"",false],"XHTML 1.0 Transitional":["!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"",false],"XHTML 1.1":["!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\"",false],"HTML 4.01":["!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\"",false]};
}
RadEditorXhtmlValidator.prototype=new RadEditorModuleBase();
RadEditorXhtmlValidator.prototype.base=RadEditorModuleBase.prototype.constructor;
function RadEditorXhtmlValidator(_1){
_1.ModuleElement=_1.Document.createElement("div");
this.base(_1);
}
RadEditorXhtmlValidator.prototype.OnCreate=function(){
this.CreateHeader();
this.CreateIframe();
this.ModuleElement.appendChild(this.Iframe);
};
RadEditorXhtmlValidator.prototype.OnDispose=function(){
if(this.ValidateButton){
this.ValidateButton.onclick=null;
this.ValidateButton=null;
}
if(this.ToggleCheckbox){
this.ToggleCheckbox.onclick=null;
this.ToggleCheckbox=null;
}
this.ContentField=null;
this.DoctypeField=null;
this.DoctypeSelect=null;
this.ValidateForm=null;
RadEditorNamespace.Utils.DetachEventEx(this.Iframe,"load",this.IframeLoadingFun);
this.Iframe=null;
};
RadEditorXhtmlValidator.prototype.Validate=function(){
oFinalContent="<div>"+this.Editor.GetHtml(true)+"</div>";
this.ContentField.value=oFinalContent;
this.ShowIframe(true);
if(this.ToggleCheckbox&&!this.ToggleCheckbox.checked){
this.ToggleCheckbox.checked=true;
}
if(this.DoctypeSelect&&this.DoctypeSelect.selectedIndex>-1){
this.DoctypeField.value="<"+this.DoctypeSelect.options[this.DoctypeSelect.selectedIndex].value+">";
}
this.ValidateForm.submit();
};
RadEditorXhtmlValidator.prototype.ShowIframe=function(_2){
var _3=this.Iframe;
if(_2){
_3.style.width="99%";
var _4=400;
_3.style.height=_4+"px";
this.Editor.FixIeHeight(_3,_4);
_3.style.border="1px ridge #aaaaaa";
}else{
_3.style.width="0px";
_3.style.height="0px";
_3.style.border="0px ridge #aaaaaa";
}
};
RadEditorXhtmlValidator.prototype.CreateIframe=function(){
var _5=document.createElement("iframe");
this.Iframe=_5;
_5.frameBorder="0";
_5.src=this.Editor.RadControlsDir+"Editor/Xhtml/XhtmlValidator.aspx";
_5.style.margin="1px";
this.ShowIframe(false);
var _6=this;
var _7=function(){
var _8=_6.Iframe.contentWindow.document;
var _9=_8.getElementsByTagName("style")[0];
if(_9){
var _a="http://validator.w3.org/base.css";
var _b=_8.getElementsByTagName("head")[0];
var _c=_8.createElement("link");
_c.setAttribute("rel","stylesheet",0);
_c.setAttribute("type","text/css",0);
_c.setAttribute("href",_a,0);
_b.appendChild(_c);
}
_6.ContentField=_8.getElementById("EditorContent");
_6.DoctypeField=_8.getElementById("EditorDoctype");
_6.ValidateForm=_8.forms["RadEditorXhtmlForm"];
};
this.IframeLoadingFun=_7;
RadEditorNamespace.Utils.AttachEventEx(_5,"load",this.IframeLoadingFun);
_5=null;
};
RadEditorXhtmlValidator.prototype.CreateHeader=function(){
var _d=this;
var _e=document.createElement("input");
_e.type="button";
_e.className="RadEXhtmlButton";
_e.style.width="100px";
_e.onclick=function(){
_d.Validate();
_d.CheckboxChecked=true;
};
_e.value=this.GetLocalizedString("ValidateXHTML","Validate XHTML");
var _f=document.createElement("INPUT");
_f.setAttribute("type","checkbox");
_f.style.marginBottom="2px";
this.CheckboxChecked=false;
_f.onclick=function(){
_d.CheckboxChecked=!_d.CheckboxChecked;
this.checked=_d.CheckboxChecked;
_d.ShowIframe(_d.CheckboxChecked);
};
this.ModuleElement.appendChild(_f);
this.ToggleCheckbox=_f;
var _10=document.createElement("span");
_10.style.height="16px";
_10.onclick=new Function("this.previousSibling.click()");
_10.innerHTML=this.GetLocalizedString("ExpandValidator","Expand/Collapse Validator")+" &nbsp; | &nbsp;";
this.ModuleElement.appendChild(_10);
var _10=document.createElement("span");
_10.innerHTML=this.GetLocalizedString("Doctype","Doctype")+":";
this.ModuleElement.appendChild(_10);
var _11=document.createElement("select");
_11.className="RadEDropDown";
_11.style.width="140px";
var _12=RadEditorXhtmlValidatorDocTypes;
for(var _13 in _12){
var _14=new Option(_13,_12[_13][0]);
if(_12[_13][1]){
_14.selected=true;
}
_11.options[_11.options.length]=_14;
}
_11.style.display="none";
if(this.Editor.IsIE&&"complete"!=document.readyState){
RadEditorNamespace.Utils.AttachEventEx(window,"load",function(){
_d.DoctypeSelect.style.display="";
});
}else{
_11.style.display="";
}
this.ModuleElement.appendChild(_11);
this.ModuleElement.appendChild(_e);
this.ValidateButton=_e;
this.DoctypeSelect=_11;
_e=null;
_10=null;
_f=null;
_11=null;
};;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
