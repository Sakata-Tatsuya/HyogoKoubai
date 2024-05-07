function RadDigitMaskPart(){
}
RadDigitMaskPart.prototype=new RadMaskPart();
RadDigitMaskPart.prototype.GetValue=function(){
return this.value.toString();
};
RadDigitMaskPart.prototype.IsCaseSensitive=function(){
return true;
};
RadDigitMaskPart.prototype.GetVisValue=function(){
if(this.value.toString()==""){
return this.PromptChar;
}
return this.value.toString();
};
RadDigitMaskPart.prototype.CanHandle=function(_1,_2){
if(isNaN(parseInt(_1))){
this.controller._OnChunkError(this,this.GetValue(),_1);
return false;
}
return true;
};
RadDigitMaskPart.prototype.SetValue=function(_3,_4){
if(_3==""||_3==this.PromptChar||_3==" "){
this.value="";
return true;
}
if(this.CanHandle(_3,_4)){
this.value=parseInt(_3);
}
return true;
};;function RadEnumerationMaskPart(_1){
this.SetOptions(_1);
this.lastOffsetPunched=-1;
this.selectedForCompletion=0;
this.FlipDirection=0;
this.RebuildKeyBuff();
}
RadEnumerationMaskPart.prototype=new RadMaskPart();
RadEnumerationMaskPart.prototype.SetOptions=function(_2){
this.length=0;
this.Options=_2;
this.optionsIndex=[];
for(var i=0;i<this.Options.length;i++){
this.length=Math.max(this.length,this.Options[i].length);
this.optionsIndex[this.Options[i]]=i;
}
};
RadEnumerationMaskPart.prototype.CanHandle=function(){
return true;
};
RadEnumerationMaskPart.prototype.SetController=function(_4){
this.controller=_4;
this.InitializeSelection(_4.AllowEmptyEnumerations);
};
RadEnumerationMaskPart.prototype.InitializeSelection=function(_5){
if(_5){
this.value="";
this.selectedIndex=-1;
}else{
this.value=this.Options[0];
this.selectedIndex=0;
}
};
RadEnumerationMaskPart.prototype.RebuildKeyBuff=function(){
this.keyBuff=[];
for(i=0;i<this.length;i++){
this.keyBuff[i]="";
}
this.keyBuffRebuilt=true;
};
RadEnumerationMaskPart.prototype.IsCaseSensitive=function(){
return true;
};
RadEnumerationMaskPart.prototype.ShowHint=function(_6){
var _7=this;
for(var i=0;i<this.Options.length;i++){
var _9=document.createElement("a");
_9.index=i;
_9.onclick=function(){
_7.SetOption(this.index);
_7.controller._Visualise();
return false;
};
_9.innerHTML=this.Options[i];
_9.href="javascript:void(0)";
_6.appendChild(_9);
}
return true;
};
RadEnumerationMaskPart.prototype.ResetCompletion=function(){
this.selectedForCompetion=0;
};
RadEnumerationMaskPart.prototype.SelectNextCompletion=function(){
this.selectedForCompletion++;
};
RadEnumerationMaskPart.prototype.Store=function(_a,_b){
if(this.lastOffsetPunched==_b){
if(this.keyBuff[_b]==_a){
this.SelectNextCompletion();
}else{
this.RebuildKeyBuff();
}
}else{
this.ResetCompletion();
}
this.lastOffsetPunched=_b;
this.keyBuff[_b]=_a;
};
RadEnumerationMaskPart.prototype.SetNoCompletionValue=function(){
if(this.controller.AllowEmptyEnumerations){
this.SetOption(-1);
}
};
RadEnumerationMaskPart.prototype.SetValue=function(_c,_d){
_d-=this.offset;
this.Store(_c,_d);
var _e=new CompletionList(this.Options,this.PromptChar);
var _f=_e.GetCompletions(this.keyBuff,_d);
if(_f.length>0){
var _10=this.optionsIndex[_f[this.selectedForCompletion%_f.length]];
this.SetOption(_10);
}else{
this.SetNoCompletionValue();
return false;
}
return true;
};
RadEnumerationMaskPart.prototype.GetVisValue=function(){
var v=this.value;
while(v.length<this.length){
v+=this.PromptChar;
}
return v;
};
RadEnumerationMaskPart.prototype.GetLength=function(){
return this.length;
};
RadEnumerationMaskPart.prototype.GetSelectedIndex=function(){
return this.selectedIndex;
};
RadEnumerationMaskPart.prototype.SetOption=function(_12,up){
var _14=this.value;
if(this.controller.AllowEmptyEnumerations){
if(_12<-1){
_12=this.Options.length+_12+1;
this.FlipDirection=-1;
}else{
if(_12>=this.Options.length){
_12=_12-this.Options.length-1;
this.FlipDirection=1;
}
}
}else{
if(_12<0){
_12=this.Options.length+_12;
this.FlipDirection=-1;
}else{
if(_12>=this.Options.length){
_12=_12-this.Options.length;
this.FlipDirection=1;
}
}
}
this.selectedIndex=_12;
this.value=_12==-1?"":this.Options[_12];
if(typeof (up)!="undefined"){
if(up){
this.controller._OnMoveUp(this,_14,this.value);
}else{
this.controller._OnMoveDown(this,_14,this.value);
}
}
this.controller._OnEnumChanged(this,_14,this.value);
this.FlipDirection=0;
};
RadEnumerationMaskPart.prototype.HandleKey=function(e){
this.controller.CalculateSelection();
var _16=new MaskedEventWrap(e,this.controller.TextBoxElement);
if(_16.IsDownArrow()){
this.SetOption(this.selectedIndex+1,false);
this.controller._Visualise();
this.controller._FixSelection(_16);
return true;
}else{
if(_16.IsUpArrow()){
this.SetOption(this.selectedIndex-1,true);
this.controller._Visualise();
this.controller._FixSelection(_16);
return true;
}
}
};
RadEnumerationMaskPart.prototype.HandleWheel=function(e){
this.controller.CalculateSelection();
var _18=new MaskedEventWrap(e,this.controller.TextBoxElement);
this.SetOption(this.selectedIndex-e.wheelDelta/120);
this.controller._Visualise();
this.controller._FixSelection(_18);
return false;
};
function CompletionList(_19,_1a){
this.options=_19;
this.blankChar=_1a;
}
CompletionList.prototype.GetCompletions=function(_1b,_1c){
var _1d=this.options;
for(var _1e=0;_1e<=_1c;_1e++){
var _1f=_1b[_1e].toLowerCase();
_1d=this.FilterCompletions(_1d,_1e,_1f);
}
return _1d;
};
CompletionList.prototype.FilterCompletions=function(_20,_21,key){
var _23=[];
for(var _24=0;_24<_20.length;_24++){
var _25=_20[_24];
var _26=_25.charAt(_21).toLowerCase();
if(this.CharacterMatchesCompletion(key,_26)){
_23[_23.length]=_25;
}
}
return _23;
};
CompletionList.prototype.CharacterMatchesCompletion=function(_27,_28){
return _27==this.blankChar||_27==" "||_27==_28;
};;function RadFreeMaskPart(){
}
RadFreeMaskPart.prototype=new RadMaskPart();
RadFreeMaskPart.prototype.IsCaseSensitive=function(){
return true;
};
RadFreeMaskPart.prototype.GetVisValue=function(){
if(this.value.toString()==""){
return this.PromptChar;
}
return this.value;
};
RadFreeMaskPart.prototype.SetValue=function(_1,_2){
this.value=_1;
return true;
};;function RadInputHint(_1,_2){
this.textBox=_1;
this.skin=_2;
}
RadInputHint.prototype.Show=function(_3,_4){
if(_3){
var _5=this.GetRect(this.textBox.TextBoxElement);
this.Container=document.createElement("div");
if(_3.ShowHint(this.Container)){
this.Container.className="radHint_"+this.skin;
document.body.appendChild(this.Container);
this.Container.style.position="absolute";
if(_4){
this.Container.style.left=_4.left+this.BodyScrollWidth()+"px";
this.Container.style.top=_5.Y+_5.Height+"px";
}else{
this.Container.style.left=_5.X+"px";
this.Container.style.top=_5.Y+_5.Height+"px";
}
this.CreateOverlay();
this.textBox._OnShowHint(this);
}else{
this.Container=null;
}
}
};
RadInputHint.prototype.HideOverlay=function(){
if(this.shim){
this.shim.style.visibility="hidden";
}
};
RadInputHint.prototype.CreateOverlay=function(){
if(window.opera){
return;
}
if(!this.shim){
this.shim=document.createElement("IFRAME");
this.shim.src="javascript:false;";
this.shim.frameBorder=0;
this.shim.id=this.Container.parentNode.id+"Overlay";
this.shim.style.position="absolute";
this.shim.style.visibility="hidden";
this.shim.style.border="1px solid red";
this.shim.style.filter="progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0)";
this.shim.allowTransparency=false;
this.Container.parentNode.insertBefore(this.shim,this.Container);
}
var _6=this.GetRect(this.Container);
this.shim.style.cssText=this.Container.style.cssText;
this.shim.style.left=_6.X+"px";
this.shim.style.top=_6.Y+"px";
this.shim.style.width=_6.Width+"px";
this.shim.style.height=_6.Height+"px";
this.shim.style.visibility="visible";
};
RadInputHint.prototype.FindScrollPosX=function(_7){
var x=0;
var _9=_7;
while(_9.parentNode&&_9.parentNode.tagName!="BODY"){
if(typeof (_9.parentNode.scrollLeft)=="number"){
x+=_9.parentNode.scrollLeft;
}
_9=_9.parentNode;
}
return x;
};
RadInputHint.prototype.FindScrollPosY=function(_a){
var y=0;
var _c=_a;
while(_c.parentNode&&_c.parentNode.tagName!="BODY"){
if(typeof (_c.parentNode.scrollTop)=="number"){
y+=_c.parentNode.scrollTop;
}
_c=_c.parentNode;
}
return y;
};
RadInputHint.prototype.BodyScrollWidth=function(){
var _d=0;
if(typeof (document.body.scrollLeft)=="number"){
_d+=document.body.scrollLeft;
}
if(typeof (document.documentElement.scrollLeft)=="number"){
_d+=document.documentElement.scrollLeft;
}
return _d;
};
RadInputHint.prototype.BodyScrollHeight=function(){
var _e=0;
if(typeof (document.body.scrollTop)=="number"){
_e+=document.body.scrollTop;
}
if(typeof (document.documentElement.scrollTop)=="number"){
_e+=document.documentElement.scrollTop;
}
return _e;
};
RadInputHint.prototype.FindScrollPosXOpera=function(_f){
var x=0;
var _11=_f;
while(_11.offsetParent&&_11.offsetParent.tagName!="BODY"){
if(typeof (_11.offsetParent.scrollLeft)=="number"){
x+=_11.offsetParent.scrollLeft;
}
_11=_11.offsetParent;
}
return x;
};
RadInputHint.prototype.FindScrollPosYOpera=function(_12){
var y=0;
var _14=_12;
while(_14.offsetParent&&_14.offsetParent.tagName!="BODY"){
if(typeof (_14.offsetParent.scrollTop)=="number"){
y+=_14.offsetParent.scrollTop;
}
_14=_14.offsetParent;
}
return y;
};
RadInputHint.prototype.Hide=function(){
if(this.Container){
this.HideOverlay();
this.Container.parentNode.removeChild(this.Container);
this.Container=null;
}
};
RadInputHint.prototype.GetRect=function(_15){
var _16=_15.offsetWidth;
var _17=_15.offsetHeight;
var x=0;
var y=0;
var _1a=_15;
while(_1a.offsetParent){
x+=_1a.offsetLeft;
y+=_1a.offsetTop;
_1a=_1a.offsetParent;
}
var _1b=0;
var _1c=0;
if(window.opera){
x-=this.FindScrollPosXOpera(_15);
y-=this.FindScrollPosYOpera(_15);
}else{
x-=this.FindScrollPosX(_15);
y-=this.FindScrollPosY(_15);
}
return {X:x,Y:y,Width:_16,Height:_17};
};;function RadLiteralMaskPart(ch){
this.ch=ch;
}
RadLiteralMaskPart.prototype=new RadMaskPart();
RadLiteralMaskPart.prototype.GetVisValue=function(){
return this.ch;
};
RadLiteralMaskPart.prototype.GetLength=function(){
if(this.controller.mozilla){
return this.ch.length-(this.ch.split("\r\n").length-1);
}
return this.ch.length;
};
RadLiteralMaskPart.prototype.GetValue=function(){
return "";
};
RadLiteralMaskPart.prototype.IsCaseSensitive=function(){
if(this.NextChunk!=null){
return this.NextChunk.IsCaseSensitive();
}
};
RadLiteralMaskPart.prototype.SetValue=function(_2,_3){
_3-=this.offset;
return _2==this.ch.charAt(_3)||!_2;
};
RadLiteralMaskPart.prototype.CanHandle=function(_4,_5){
_5-=this.offset;
if(_4==this.ch.charAt(_5)){
return true;
}
if(!_4){
return true;
}
if(this.NextChunk!=null){
return this.NextChunk.CanHandle(_4,_5+this.GetLength());
}
};;function RadLowerMaskPart(){
}
RadLowerMaskPart.prototype=new RadMaskPart();
RadLowerMaskPart.prototype.CanHandle=function(_1,_2){
if(!RadMaskPart.IsAlpha(_1)){
this.controller._OnChunkError(this,this.GetValue(),_1);
return false;
}
return true;
};
RadLowerMaskPart.prototype.GetVisValue=function(){
if(this.value.toString()==""){
return this.PromptChar;
}
return this.value.toString();
};
RadLowerMaskPart.prototype.SetValue=function(_3,_4){
if(_3==""){
this.value="";
return true;
}
if(RadMaskPart.IsAlpha(_3)){
this.value=_3.toLowerCase();
}else{
this.controller._OnChunkError(this,this.GetValue(),_3);
}
return true;
};;if(typeof (window.RadInputNamespace)=="undefined"){
window.RadInputNamespace=new Object();
}
function RadMaskedTextBox(id,_2,_3){
RadTextBox.Extend(this);
this.CallBase("DisposeOldInstance",arguments);
this.Constructor(id);
this.Initialize(_2,_3);
}
RadMaskedTextBox.prototype={Constructor:function(id){
this.CallBase("Constructor",arguments);
},Initialize:function(_5,_6){
this.LoadCongfiguration(_5);
this.LoadClientEvents(_5);
this.parts=[];
this.partIndex=[];
this.displayPartIndex=[];
this.value="";
this.TextBoxElement.oldValue=this.TextBoxElement.value;
this.lastState=null;
this.length=0;
this.displayLength=0;
this.internalValueUpdate=false;
this.projectedValue="";
this.Hint=null;
this.isTextarea=this.TextBoxElement.tagName.toLowerCase()=="textarea";
this.safari=navigator.userAgent.indexOf("Safari")>-1;
this.mozilla=navigator.userAgent.indexOf("Gecko")>-1;
this.Hint=new RadInputHint(this,_5.Skin);
this.Styles=_6;
this.AttachEventHandlers();
this._FixAbsolutePositioning();
this._SetMask(this.InitialMasks);
if(this.InitialDisplayMasks.length){
this._SetDisplayMask(this.InitialDisplayMasks);
}
this.SetValue(this.TextBoxElement.value);
this._Visualise();
if(this.FocusOnStartup){
this.Focus();
}
this._RecordInitialState();
},AttachEventHandlers:function(){
this.AttachDomEvent(this.TextBoxElement.form,"reset","OnReset");
this.AttachToTextBoxEvent("keydown","TextBoxKeyDownHandler");
this.AttachToTextBoxEvent("keypress","TextBoxKeyPressHandler");
this.AttachToTextBoxEvent("keyup","TextBoxKeyUpHandler");
this.AttachToTextBoxEvent("focus","TextBoxFocusHandler");
this.AttachToTextBoxEvent("blur","TextBoxBlurHandler");
this.AttachToTextBoxEvent("mousedown","TextBoxMouseDownHandler");
this.AttachToTextBoxEvent("mouseover","TextBoxMouseOverHandler");
this.AttachToTextBoxEvent("mouseout","TextBoxMouseOutHandler");
this.AttachToTextBoxEvent("mouseup","TextBoxMouseUpHandler");
if(this._ShouldUseAttachEvent(this.TextBoxElement)){
this.AttachToTextBoxEvent("paste","_onPaste");
this.AttachToTextBoxEvent("propertychange","_onPropertyChange");
this.AttachToTextBoxEvent("mousewheel","_onMouseWheel");
}else{
this.AttachToTextBoxEvent("input","_onInput");
}
if(window.opera){
var _7=this;
var _8=function(){
return _7._ValueHandler({});
};
setInterval(_8,10);
}
},SetValue:function(_9){
this.internalValueUpdate=true;
this._UpdatePartsInRange(_9,0,this.length);
this.internalValueUpdate=false;
this._Visualise();
},Enable:function(){
this.TextBoxElement.disabled="";
this.Enabled=true;
},Disable:function(){
this.TextBoxElement.disabled="disabled";
this.Enabled=false;
},Focus:function(){
this.TextBoxElement.focus();
this.TextBoxElement.selectionStart=this.TextBoxElement.selectionEnd=0;
},GetValue:function(){
var _a=[];
for(var i=0;i<this.parts.length;i++){
_a[i]=this.parts[i].GetValue();
}
return _a.join("");
},UpdateDisplayValue:function(){
if(this.Focused){
if((this.HideOnBlur&&this.isEmpty())||this.displayParts){
this._Visualise();
this.TextBoxElement.select();
}
if(this.ResetCaretOnFocus){
this.ResetCursor();
}
}else{
this._Visualise();
}
},InitializeHiddenElement:function(id){
this.HiddenElement=document.getElementById(id+"_Value");
},InitializeValidationField:function(id){
this.validationField=document.getElementById(id);
},SetValidationField:function(_e){
if(this.isEmpty()){
this.validationField.value="";
}else{
this.validationField.value=this.GetValueWithLiterals();
}
},GetValidationField:function(_f){
return this.validationField;
},_onInput:function(e){
this._ValueHandler(e);
},_onMouseWheel:function(e){
return this._OnMouseWheel(event);
},_onPropertyChange:function(e){
this._OnPropertyChange();
},_onPaste:function(e){
if(this.ReadOnly){
return false;
}
if(this.selectionStart==this.value.length){
return false;
}
setTimeout(function(){
this._FakeOnPropertyChange();
},1);
},TextBoxBlurHandler:function(e){
this.Focused=false;
this.Hovered=false;
this.UpdateDisplayValue();
this.UpdateCssClass();
var _15=this;
window.setTimeout(function(){
if(_15.Hint){
_15.Hint.Hide();
}
},200);
if(this.AutoPostBack&&this._ValueHasChanged()){
this.RaisePostBackEvent();
}
this.TextBoxElement.oldValue=this.TextBoxElement.value;
},TextBoxMouseUpHandler:function(e){
this._FakeOnPropertyChange();
this._ValueHandler(e);
this._ActivityHandler(e);
this.DisplayHint();
},TextBoxMouseOutHandler:function(e){
this.Hovered=false;
this.UpdateCssClass();
},TextBoxMouseOverHandler:function(e){
this._FakeOnPropertyChange();
this.Hovered=true;
this.UpdateCssClass();
},TextBoxMouseDownHandler:function(e){
this._FakeOnPropertyChange();
this._ActivityHandler(e);
},TextBoxFocusHandler:function(e){
this.Focused=true;
this.UpdateDisplayValue();
this.UpdateCssClass();
this.UpdateSelectionOnFocus();
this._FakeOnPropertyChange();
this._ActivityHandler(e);
},TextBoxKeyUpHandler:function(e){
this._FakeOnPropertyChange();
this.DisplayHint();
},_OnActivity:function(e){
this.CalculateSelection();
this.lastState=new MaskedEventWrap(e,this.TextBoxElement);
},_OnPropertyChange:function(){
if(this.internalValueUpdate){
return;
}
if(event.propertyName=="value"){
var e=event;
var _1e=this;
var _1f=function(){
_1e._ValueHandler(e);
};
this.CalculateSelection();
if(this.TextBoxElement.selectionStart>0||this.TextBoxElement.selectionEnd>0){
_1f();
}else{
setTimeout(_1f,1);
}
}
},_OnMouseWheel:function(e){
if(this.ReadOnly){
return false;
}
this.CalculateSelection();
var _21=this.partIndex[this.TextBoxElement.selectionStart];
if(_21==null){
return true;
}
return _21.HandleWheel(e);
},UpdateSelectionOnFocus:function(){
switch(this.SelectionOnFocus){
case 0:
break;
case 1:
var _22=0;
var i;
for(i=0;i<this.partIndex.length;i++){
if(!this.partIndex[i].ch){
_22=i;
break;
}
}
this.SetCaretPosition(_22);
break;
case 2:
if(this.TextBoxElement.value.length>0){
this.SetCaretPosition(this.TextBoxElement.value.length);
}
break;
case 3:
this.SelectAllText();
break;
default:
this.SetCaretPosition(0);
break;
}
},TextBoxKeyDownHandler:function(e){
this._FakeOnPropertyChange();
if(this.InSelection(e)){
return true;
}
var _25=this.partIndex[this.TextBoxElement.selectionStart];
var _26=e.which?e.which:e.keyCode;
if(this.ReadOnly&&(_26==46||_26==8||_26==38||_26==40)){
RadControlsNamespace.DomEvent.PreventDefault(e);
return false;
}else{
if(_26==13){
return true;
}else{
if(_25==null&&_26!=8){
return true;
}else{
if(_25!=null){
if(_25.HandleKey(e)){
RadControlsNamespace.DomEvent.PreventDefault(e);
return false;
}
}
}
}
}
var _27=this.TextBoxElement.selectionEnd;
var _28=false;
if((_26==46)&&_27<this.TextBoxElement.value.length&&!window.opera){
_25.SetValue("",this.TextBoxElement.selectionStart);
_27++;
_28=true;
}else{
if(_26==8&&_27&&!window.opera){
this.partIndex[this.TextBoxElement.selectionStart-1].SetValue("",this.TextBoxElement.selectionStart-1);
_27--;
_28=true;
}
}
if(_28){
return this._UpdateAfterKeyHandled(e,_27);
}
this._OnActivity(e);
return true;
},TextBoxKeyPressHandler:function(e){
if(this.ReadOnly){
RadControlsNamespace.DomEvent.PreventDefault(e);
RadControlsNamespace.DomEvent.StopPropagation(e);
return false;
}
var _2a=/MSIE/.test(navigator.userAgent);
var _2b=_2a?e.keyCode:e.which;
if(!this.RaiseEvent("OnKeyPress",{"DomEvent":e,"KeyCode":_2b,"KeyCharacter":String.fromCharCode(_2b)})){
return RadControlsNamespace.DomEvent.PreventDefault(e);
}
if(this.InSelection(e)){
return true;
}
var _2c=this.partIndex[this.TextBoxElement.selectionStart];
if(_2c==null){
return true;
}
if(this.mozilla||window.opera){
if(e.which==8){
RadControlsNamespace.DomEvent.PreventDefault(e);
RadControlsNamespace.DomEvent.StopPropagation(e);
return false;
}
if(!e.which){
this._OnActivity(e);
RadControlsNamespace.DomEvent.StopPropagation(e);
return true;
}
}
var _2d=this.TextBoxElement.selectionEnd;
var _2b=e.which?e.which:e.keyCode;
if(_2b==13){
return true;
}
var ch=String.fromCharCode(_2b);
if(_2c.CanHandle(ch)){
while(_2d<this.TextBoxElement.value.length){
if(this.partIndex[_2d].SetValue(ch,_2d)){
_2d++;
break;
}
_2d++;
}
}
var _2f=this._UpdateAfterKeyHandled(e,_2d);
if(!_2f){
RadControlsNamespace.DomEvent.PreventDefault(e);
}
RadControlsNamespace.DomEvent.StopPropagation(e);
return _2f;
},_OnEnumChanged:function(_30,_31,_32){
this.RaiseEvent("OnClientEnumerationChanged",{"CurrentPart":_30,"OldValue":_31,"NewValue":_32});
},_OnMoveUp:function(_33,_34,_35){
this.RaiseEvent("OnClientMoveUp",{"CurrentPart":_33,"OldValue":_34,"NewValue":_35});
},_OnMoveDown:function(_36,_37,_38){
this.RaiseEvent("OnClientMoveDown",{"CurrentPart":_36,"OldValue":_37,"NewValue":_38});
},_OnValueChange:function(_39,_3a,_3b){
this.RaiseEvent("OnValueChanged",{"CurrentPart":_39,"OldValue":_3a,"NewValue":_3b});
},_OnShowHint:function(_3c){
this.RaiseEvent("OnClientShowHint",{"CurrentPart":_3c,"OldValue":this.TextBoxElement.value,"NewValue":this.TextBoxElement.value});
},_OnChunkError:function(_3d,_3e,_3f){
var _40={"CurrentPart":_3d,"OldValue":_3e,"NewValue":_3f};
this.RaiseErrorEvent(_40);
},_ShouldUseAttachEvent:function(_41){
return (_41.attachEvent&&!window.opera&&!window.netscape);
},_FixAbsolutePositioning:function(){
var f=this.TextBoxElement;
if(f.previousSibling&&f.previousSibling.tagName.toLowerCase()=="label"&&f.style.position=="absolute"){
f.style.position="static";
var _43=f.parentNode;
_43.style.position="absolute";
_43.style.top=f.style.top;
_43.style.left=f.style.left;
}
},_RecordInitialState:function(){
this.initialFieldValue=this.TextBoxElement.value;
},_PartAt:function(_44){
return this.partIndex[_44];
},_CreatePartCollection:function(_45,_46){
var _47;
var _48=[];
var _49=0;
for(var j=0;j<_45.length;j++){
_47=_45[j];
_47.PromptChar=_46;
_47.SetController(this);
_47.index=this.parts.length;
_48[_48.length]=_47;
if(_48.length>1){
_48[_48.length-2].NextChunk=_47;
}
_47.NextChunk=null;
var _4b=_47.GetLength();
_47.offset=_49;
_49+=_4b;
}
return _48;
},_SetMask:function(_4c){
this.parts=this._CreatePartCollection(_4c,this.PromptChar);
for(var i=0;i<this.parts.length;i++){
var _4e=this.parts[i].GetLength();
for(var j=this.length;j<this.length+_4e;j++){
this.partIndex[j]=this.parts[i];
}
this.length+=_4e;
}
},_SetDisplayMask:function(_50){
this.displayParts=this._CreatePartCollection(_50,this.DisplayPromptChar);
for(var i=0;i<this.displayParts.length;i++){
var _52=this.displayParts[i];
var _53=_52.GetLength();
if(_52.ch){
continue;
}
for(var j=this.displayLength;j<this.displayLength+_53;j++){
this.displayPartIndex[j]=this.displayParts[i];
}
this.displayLength+=_53;
}
},_SafariSelectionFix:function(e){
var _56=this._StrCompare(this.lastState.fieldValue,e.fieldValue);
e.selectionStart=_56[0];
e.selectionEnd=_56[0];
this.lastState.selectionStart=_56[1];
this.lastState.selectionEnd=_56[2];
},_HandleValueChange:function(e){
if(this.ReadOnly){
this._Visualise();
return false;
}
if(this.lastState==null){
return;
}
var i,j;
if(this.safari){
this._SafariSelectionFix(e);
}
if(this.lastState.fieldValue.length>e.fieldValue.length){
if(e.selectionStart==this.TextBoxElement.value.length){
this.partIndex[this.partIndex.length-1].SetValue("",this.partIndex.length-1);
}
if(this.lastState.selectionEnd>e.selectionStart){
i=this.lastState.selectionEnd;
while(i-->e.selectionStart){
this.partIndex[i].SetValue("",i);
}
}else{
i=this.lastState.selectionEnd+1;
while(i-->e.selectionStart){
this.partIndex[i].SetValue("",i);
e.selectionEnd++;
}
}
}
var _5a=this.lastState.selectionStart;
var _5b=Math.min(e.selectionStart,this.length);
var _5c=e.fieldValue.substr(_5a,_5b-_5a);
var _5d=this._UpdatePartsInRange(_5c,_5a,_5b);
e.selectionEnd+=_5d;
this._FixSelection(e);
},_SetPartValues:function(_5e,_5f,_60,_61,to){
var _63;
var i=0;
var j=_61;
var _66=0;
_60=_60.toString();
while(i<to-_61&&j<_5f){
_63=_60.charAt(i);
if(_63==this.PromptChar){
_63="";
}
if(_5e[j].SetValue(_63,j)){
i++;
}else{
_66++;
}
j++;
}
return _66;
},_UpdateDisplayPartsInRange:function(_67,_68,to){
this._SetPartValues(this.displayPartIndex,this.displayLength,_67,_68,to);
},_UpdatePartsInRange:function(_6a,_6b,to){
var _6d=this._SetPartValues(this.partIndex,this.length,_6a,_6b,to);
this._Visualise();
return _6d;
},SetCursorPosition:function(_6e){
if(!this.Focused){
return;
}
this.CalculateSelection();
if(document.all&&!window.opera){
this.TextBoxElement.select();
sel=document.selection.createRange();
var _6f=this.TextBoxElement.value.substr(0,_6e).split("\r\n").length-1;
sel.move("character",_6e-_6f);
sel.select();
}else{
this.TextBoxElement.selectionStart=_6e;
this.TextBoxElement.selectionEnd=_6e;
}
},_FixSelection:function(_70){
this.SetCursorPosition(_70.selectionEnd);
},GetValueWithLiterals:function(){
var _71=[];
for(var i=0;i<this.parts.length;i++){
_71[i]=this.parts[i].ch||this.parts[i].GetValue();
}
return _71.join("");
},_GetVisibleValues:function(_73){
var _74=[];
for(var i=0;i<_73.length;i++){
_74[i]=_73[i].GetVisValue();
}
return _74.join("");
},GetValueWithPromptAndLiterals:function(){
return this._GetVisibleValues(this.parts);
},GetPrompt:function(){
var _76=new RegExp(".","g");
var _77=[];
for(var i=0;i<this.parts.length;i++){
_77[i]=this.parts[i].ch||this.parts[i].GetVisValue().replace(_76,this.PromptChar);
}
return _77.join("");
},_Visualise:function(){
var _79=this.GetValueWithPromptAndLiterals();
var _7a=this.GetValue();
this.internalValueUpdate=true;
var _7b=this.projectedValue;
this._Render(_79);
this.UpdateCssClass();
this.value=_7a;
this.UpdateHiddenValue();
this.internalValueUpdate=false;
this.projectedValue=this.TextBoxElement.value;
if(_7b!=this.TextBoxElement.value){
this.TriggerDOMChangeEvent(this.GetValidationField());
this._OnValueChange(null,_7b,this.TextBoxElement.value);
}
},_Render:function(_7c){
this.IsEmptyMessage=false;
if(!this.Focused){
if(this.HideOnBlur&&this.isEmpty()){
this.IsEmptyMessage=true;
this.SetTextBoxValue(this.EmptyMessage);
}else{
if(this.displayParts&&this.displayParts.length){
this.SetTextBoxValue(this.GetDisplayValue());
}else{
this.SetTextBoxValue(_7c);
}
}
}else{
this.SetTextBoxValue(_7c);
}
},UpdateHiddenValue:function(){
return this.SetHiddenValue(this.GetValueWithPromptAndLiterals());
},OnReset:function(){
this.SetValue(this.initialFieldValue);
this._Visualise();
},_ValueHasChanged:function(){
return this.TextBoxElement.value!=this.TextBoxElement.oldValue;
},_FakeOnPropertyChange:function(){
if(document.createEventObject){
if(event){
var ev=document.createEventObject(event);
}else{
var ev=document.createEventObject();
}
ev.propertyName="value";
this.TextBoxElement.fireEvent("onpropertychange",ev);
}
},GetDisplayValue:function(){
var _7e=this.value;
while(_7e.length<this.displayLength){
if(this.DisplayFormatPosition){
_7e=this.PromptChar+_7e;
}else{
_7e+=this.PromptChar;
}
}
this._UpdateDisplayPartsInRange(_7e,0,this.displayLength);
return this._GetVisibleValues(this.displayParts);
},isEmpty:function(){
return this.value=="";
},ResetCursor:function(){
this.SetCursorPosition(0);
},_UpdateAfterKeyHandled:function(e,_80){
this._Visualise();
var _81=new MaskedEventWrap(e,this.TextBoxElement);
_81.selectionEnd=_80;
this._FixSelection(_81);
return false;
},InSelection:function(e){
this.CalculateSelection();
if(this.TextBoxElement.selectionStart!=this.TextBoxElement.selectionEnd){
this._OnActivity(e);
return true;
}
if(e.ctrlKey||e.altKey||this.safari){
this._OnActivity(e);
return true;
}
return false;
},_ValueHandler:function(e){
if(this.internalValueUpdate){
return true;
}
if(!e){
e=window.event;
}
this.CalculateSelection();
var _84=new MaskedEventWrap(e,this.TextBoxElement);
if(_84.fieldValue!=this.projectedValue){
this._HandleValueChange(_84);
}
return true;
},_ActivityHandler:function(e){
if(this.internalValueUpdate){
return true;
}
if(!e){
e=window.event;
}
this._OnActivity(e);
return true;
},DisplayHint:function(){
if(!this.ShowHint){
return;
}
this.CalculateSelection();
var _86=this.partIndex[this.TextBoxElement.selectionStart];
this.Hint.Hide();
var _87=null;
if(document.selection){
var _88=document.selection.createRange();
if(_88.getBoundingClientRect){
_87=_88.getBoundingClientRect();
}
}
this.Hint.Show(_86,_87);
},CalculateSelection:function(){
if(document.selection&&!window.opera){
var s1;
try{
s1=document.selection.createRange();
}
catch(error){
return;
}
if(s1.parentElement()!=this.TextBoxElement){
return;
}
var s=s1.duplicate();
if(this.isTextarea){
s.moveToElementText(this.TextBoxElement);
}else{
s.move("character",-this.TextBoxElement.value.length);
}
s.setEndPoint("EndToStart",s1);
this.TextBoxElement.selectionStart=s.text.length;
this.TextBoxElement.selectionEnd=this.TextBoxElement.selectionStart+s1.text.length;
if(this.isTextarea){
}
}
},_StrCompare:function(_8b,_8c){
var i;
var _8e,_8f,_90;
i=0;
while(_8b.charAt(i)==_8c.charAt(i)&&i<_8b.length){
i++;
}
_8f=i;
_8b=_8b.substr(_8f).split("").reverse().join("");
_8c=_8c.substr(_8f).split("").reverse().join("");
i=0;
while(_8b.charAt(i)==_8c.charAt(i)&&i<_8b.length){
i++;
}
_8e=_8f+_8c.length-i;
_90=_8b.length-i+_8f;
return [_8e,_8f,_90];
}};
function RadInputEventArgs(){
}
function MaskedEventWrap(e,_92){
this.event=e;
this.selectionStart=_92.selectionStart;
this.selectionEnd=_92.selectionEnd;
this.fieldValue=_92.value;
}
MaskedEventWrap.prototype.IsUpArrow=function(){
return this.event.keyCode==38;
};
MaskedEventWrap.prototype.IsDownArrow=function(){
return this.event.keyCode==40;
};
function rdmskd(){
return new RadDigitMaskPart();
}
function rdmskl(_93){
return new RadLiteralMaskPart(_93);
}
function rdmske(_94){
return new RadEnumerationMaskPart(_94);
}
function rdmskr(_95,_96,_97,_98){
return new RadNumericRangeMaskPart(_95,_96,_97,_98);
}
function rdmsku(){
return new RadUpperMaskPart();
}
function rdmsklw(){
return new RadLowerMaskPart();
}
function rdmskp(){
return new RadPasswordMaskPart();
}
function rdmskf(){
return new RadFreeMaskPart();
};function RadMaskPart(){
this.value="";
this.index=-1;
this.type=-1;
this.PromptChar="_";
}
RadMaskPart.prototype.HandleKey=function(ev){
return false;
};
RadMaskPart.prototype.HandleWheel=function(_2){
return true;
};
RadMaskPart.prototype.SetController=function(_3){
this.controller=_3;
};
RadMaskPart.prototype.GetValue=function(){
return this.value.toString();
};
RadMaskPart.prototype.GetVisValue=function(){
return "";
};
RadMaskPart.prototype.SetValue=function(_4,_5){
return true;
};
RadMaskPart.prototype.CanHandle=function(_6,_7){
return true;
};
RadMaskPart.prototype.IsCaseSensitive=function(){
return false;
};
RadMaskPart.prototype.ShowHint=function(_8){
return false;
};
RadMaskPart.prototype.GetLength=function(){
return 1;
};
RadMaskPart.IsAlpha=function(_9){
return _9.match(/[^\u005D\u005B\t\n\r\f\s\v\\!-@|^_`{-┬┐]{1}/)!=null;
};;function RadNumericRangeMaskPart(_1,_2,_3,_4){
this.upperLimit=_2;
this.lowerLimit=_1;
this.length=Math.max(this.lowerLimit.toString().length,this.upperLimit.toString().length);
this.leftAlign=_3;
this.zeroFill=_4;
this.minusIncluded=this.lowerLimit<0||this.upperLimit<0;
this.value=_1;
this.FlipDirection=0;
}
RadNumericRangeMaskPart.prototype=new RadMaskPart();
RadNumericRangeMaskPart.prototype.SetController=function(_5){
this.controller=_5;
this.GetVisValue();
};
RadNumericRangeMaskPart.prototype.IsCaseSensitive=function(){
return true;
};
RadNumericRangeMaskPart.prototype.CanHandle=function(_6,_7){
if((_6=="-"||_6=="+")&&this.lowerLimit<0){
return true;
}
if(isNaN(parseInt(_6))){
this.controller._OnChunkError(this,this.GetValue(),_6);
return false;
}
return true;
};
RadNumericRangeMaskPart.prototype.InsertAt=function(_8,_9){
return this.visValue.substr(0,_9)+_8.toString()+this.visValue.substr(_9+1,this.visValue.length);
};
RadNumericRangeMaskPart.prototype.ReplacePromptChar=function(_a){
var _b=this.leftAlign?"":"0";
while(_a.indexOf(this.PromptChar)>-1){
_a=_a.replace(this.PromptChar,_b);
}
return _a;
};
RadNumericRangeMaskPart.prototype.SetValue=function(_c,_d){
if(_c==""){
_c=0;
}
if(isNaN(parseInt(_c))&&_c!="+"&&_c!="-"){
return true;
}
_d-=this.offset;
var _e=this.InsertAt(_c,_d);
_e=this.ReplacePromptChar(_e);
if(_e.indexOf("-")!=-1&&_e.indexOf("-")>0){
_e=_e.replace("-","0");
}
if(isNaN(parseInt(_e))){
_e=0;
}
if(this.controller.RoundNumericRanges){
_e=Math.min(this.upperLimit,_e);
_e=Math.max(this.lowerLimit,_e);
this.setInternalValue(_e);
}else{
if(_e<=this.upperLimit&&_e>=this.lowerLimit){
this.setInternalValue(_e);
this.GetVisValue();
}else{
return false;
}
}
this.GetVisValue();
return true;
};
RadNumericRangeMaskPart.prototype.setInternalValue=function(_f){
var _10=this.value;
this.value=_f;
this.controller._OnEnumChanged(this,_10,_f);
if(_10>_f){
this.controller._OnMoveDown(this,_10,_f);
}else{
this.controller._OnMoveUp(this,_10,_f);
}
this.FlipDirection=0;
};
RadNumericRangeMaskPart.prototype.GetVisValue=function(){
var out="";
var _12=Math.abs(this.value).toString();
if(this.leftAlign){
if(this.value<0){
out+=this.PromptChar;
}
out+=_12;
while(out.length<this.length){
out+=this.controller.PromptChar;
}
}else{
var _13=this.zeroFill?"0":this.controller.PromptChar;
if(this.value<0){
_12="-"+_12;
}
while(out.length<this.length-_12.length){
out+=_13;
}
out+=_12;
}
this.visValue=out;
return out;
};
RadNumericRangeMaskPart.prototype.GetLength=function(){
return this.length;
};
RadNumericRangeMaskPart.prototype.HandleKey=function(e){
this.controller.CalculateSelection();
var _15=new MaskedEventWrap(e,this.controller.TextBoxElement);
if(_15.IsDownArrow()){
this.MoveDown();
this.controller._FixSelection(_15);
return true;
}else{
if(_15.IsUpArrow()){
this.MoveUp();
this.controller._FixSelection(_15);
return true;
}
}
};
RadNumericRangeMaskPart.prototype.MoveUp=function(){
var _16=this.value;
_16++;
if(_16>this.upperLimit){
_16=this.lowerLimit;
this.FlipDirection=1;
}
this.setInternalValue(_16);
this.controller._Visualise();
};
RadNumericRangeMaskPart.prototype.MoveDown=function(){
var _17=this.value;
_17--;
if(_17<this.lowerLimit){
_17=this.upperLimit;
this.FlipDirection=-1;
}
this.setInternalValue(_17);
this.controller._Visualise();
};
RadNumericRangeMaskPart.prototype.HandleWheel=function(e){
var _19=this.value;
_19=parseInt(_19)+parseInt(e.wheelDelta/120);
var _1a=new MaskedEventWrap(e,this.controller.TextBoxElement);
if(_19<this.lowerLimit){
_19=this.upperLimit-(this.lowerLimit-_19-1);
this.FlipDirection=-1;
}
if(_19>this.upperLimit){
_19=this.lowerLimit+(_19-this.upperLimit-1);
this.FlipDirection=1;
}
this.setInternalValue(_19);
this.controller._Visualise();
this.controller._FixSelection(_1a);
return false;
};;function RadPasswordMaskPart(){
}
RadPasswordMaskPart.prototype=new RadMaskPart();
RadPasswordMaskPart.prototype.IsCaseSensitive=function(){
return true;
};
RadPasswordMaskPart.prototype.GetVisValue=function(){
if(this.value.toString()==""){
return this.PromptChar;
}
return "*";
};
RadPasswordMaskPart.prototype.SetValue=function(_1,_2){
this.value=_1;
return true;
};;function RadUpperMaskPart(){
}
RadUpperMaskPart.prototype=new RadMaskPart();
RadUpperMaskPart.prototype.CanHandle=function(_1,_2){
if(!RadMaskPart.IsAlpha(_1)){
this.controller._OnChunkError(this,this.GetValue(),_1);
return false;
}
return true;
};
RadUpperMaskPart.prototype.GetVisValue=function(){
if(this.value.toString()==""){
return this.PromptChar;
}
return this.value.toString();
};
RadUpperMaskPart.prototype.SetValue=function(_3,_4){
if(_3==""){
this.value="";
return true;
}
if(RadMaskPart.IsAlpha(_3)){
this.value=_3.toUpperCase();
}else{
this.controller._OnChunkError(this,this.GetValue(),_3);
}
return true;
};;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
