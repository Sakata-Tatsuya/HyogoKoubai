if(typeof (RadEditorNamespace)=="undefined"){
window.RadEditorNamespace={RADEDITOR_INDENT_STEP:"\xb7\xb7\xb7",RADEDITOR_OUTDENT_STEP:"\xb7",RADCOMMAND_STATE_DISABLED:-1,RADCOMMAND_STATE_OFF:0,RADCOMMAND_STATE_ON:1,RADEDITOR_DESIGN_MODE:1,RADEDITOR_HTML_MODE:2,RADEDITOR_PREVIEW_MODE:3,RADEVENT_CALLBACK_STARTED:"RADEVENT_CALLBACK_STARTED",RADEVENT_MODE_CHANGED:"RADEVENT_MODE_CHANGED",RADEVENT_CONTEXTMENU:"RADEVENT_CONTEXTMENU",RADEVENT_SEL_CHANGED:"RADEVENT_SEL_CHANGED",RADEVENT_SIZE_CHANGED:"RADEVENT_SIZE_CHANGED",RADEVENT_DISPOSE:"RADEVENT_DISPOSE",RADEVENT_SUBMIT:"RADEVENT_SUBMIT",RADEVENT_BEFORE_EDIT_FOCUS:"RADEVENT_BEFORE_EDIT_FOCUS",RADEVENT_KEYDOWN:"RADEVENT_KEYDOWN",RADEVENT_KEYUP:"RADEVENT_KEYUP",RADEVENT_MOUSEDOWN:"RADEVENT_MOUSEDOWN",RADEVENT_MOUSEUP:"RADEVENT_MOUSEUP",RADEVENT_CUT:"RADEVENT_CUT",RADEVENT_COPY:"RADEVENT_COPY",RADEVENT_PASTE:"RADEVENT_PASTE",RADEVENT_RESIZE_START:"RADEVENT_RESIZE_START",RADEVENT_RESIZE_END:"RADEVENT_RESIZE_END",RADEVENT_DRAG_START:"RADEVENT_DRAG_START",RADEVENT_DRAG_END:"RADEVENT_DRAG_END",RADEVENT_DROP:"RADEVENT_DROP",RADCOMMAND_BOLD:"Bold",RADCOMMAND_ITALIC:"Italic",RADCOMMAND_UNDERLINE:"Underline",RADCOMMAND_FORECOLOR:"ForeColor",RADCOMMAND_BACKCOLOR:"BackColor",RADCOMMAND_FONTNAME:"FontName",RADCOMMAND_FONTSIZE:"FontSize",RADCOMMAND_REAL_FONTSIZE:"RealFontSize",RADCOMMAND_CONVERT_TO_UPPER:"ConvertToUpper",RADCOMMAND_CONVERT_TO_LOWER:"ConvertToLower",RADCOMMAND_JUSTIFY_LEFT:"JustifyLeft",RADCOMMAND_JUSTIFY_RIGHT:"JustifyRight",RADCOMMAND_JUSTIFY_CENTER:"JustifyCenter",RADCOMMAND_JUSTIFY_FULL:"JustifyFull",RADCOMMAND_JUSTIFY_NONE:"JustifyNone",RADCOMMAND_INDENT:"Indent",RADCOMMAND_OUTDENT:"Outdent",RADCOMMAND_SUBSCRIPT:"Subscript",RADCOMMAND_SUPERSCRIPT:"Superscript",RADCOMMAND_STRIKETHROUGH:"StrikeThrough",RADCOMMAND_FORMAT_BLOCK:"FormatBlock",RADCOMMAND_CUT:"Cut",RADCOMMAND_COPY:"Copy",RADCOMMAND_PASTE:"Paste",RADCOMMAND_UNDO:"Undo",RADCOMMAND_REDO:"Redo",RADCOMMAND_SELECT_ALL:"SelectAll",RADCOMMAND_TYPE:"Typing",RADCOMMAND_BACK:"Back",RADCOMMAND_DELETE:"Delete",RADCOMMAND_INSERT_TABLE:"InsertTable",RADCOMMAND_TABLE_WIZARD:"TableWizard",RADCOMMAND_INSERT_IMAGE:"InsertImage",RADCOMMAND_INSERT_FLASH:"InsertFlash",RADCOMMAND_INSERT_MEDIA:"InsertMedia",RADCOMMAND_INSERT_DOCUMENT:"InsertDocument",RADCOMMAND_INSERT_SYMBOL:"InsertSymbol",RADCOMMAND_INSERT_SNIPPET:"InsertSnippet",RADCOMMAND_INSERT_FORM_ELEMENT:"InsertFormElement",RADCOMMAND_INSERT_DATE:"InsertDate",RADCOMMAND_INSERT_TIME:"InsertTime",RADCOMMAND_INSERT_ROW_ABOVE:"InsertRowAbove",RADCOMMAND_INSERT_ROW_BELOW:"InsertRowBelow",RADCOMMAND_DELETE_ROW:"DeleteRow",RADCOMMAND_INSERT_COLUMN_LEFT:"InsertColumnLeft",RADCOMMAND_INSERT_COLUMN_RIGHT:"InsertColumnRight",RADCOMMAND_DELETE_COLUMN:"DeleteColumn",RADCOMMAND_MERGE_COLUMNS:"MergeColumns",RADCOMMAND_MERGE_ROWS:"MergeRows",RADCOMMAND_SPLIT_CELL:"SplitCell",RADCOMMAND_DELETE_CELL:"DeleteCell",RADCOMMAND_SET_CELL_PROPERTIES:"SetCellProperties",RADCOMMAND_SET_TABLE_PROPERTIES:"SetTableProperties",RADCOMMAND_DELETE_TABLE:"DeleteTable",RADCOMMAND_TOGGLE_TABLE_BORDER:"ToggleTableBorder",RADCOMMAND_SET_IMAGE_PROPERTIES:"SetImageProperties",RADCOMMAND_SHOW_IMAGE_MAP_DIALOG:"ImageMapDialog",RADCOMMAND_FORMAT_CODE_BLOCK_DIALOG:"FormatCodeBlock",RADCOMMAND_SET_LINK_PROPERTIES:"SetLinkProperties",RADCOMMAND_STRIP_FORMAT:"FormatStripper",RADCOMMAND_SHOW_LINK_DIALOG:"LinkManager",RADCOMMAND_SHOW_IMAGE_DIALOG:"ImageManager",RADCOMMAND_SHOW_FLASH_DIALOG:"FlashManager",RADCOMMAND_SHOW_MEDIA_DIALOG:"MediaManager",RADCOMMAND_SHOW_DOCUMENT_DIALOG:"DocumentManager",RADCOMMAND_SHOW_FIND_DIALOG:"FindAndReplace",RADCOMMAND_SHOW_ABOUT_DIALOG:"AboutDialog",RADCOMMAND_SHOW_TEMPLATE_DIALOG:"TemplateManager",RADCOMMAND_HELP:"Help",RADCOMMAND_MANAGE_MODULE:"ModuleManager",RADCOMMAND_PAGE_PROPERTIES:"PageProperties",RADCOMMAND_PRINT:"Print",RADCOMMAND_SPELLCHECK:"SpellCheck",RADCOMMAND_PASTE_FROM_WORD:"PasteFromWord",RADCOMMAND_PASTE_FROM_WORD_ALL:"PasteFromWordNoFontsNoSizes",RADCOMMAND_PASTE_PLAIN_TEXT:"PastePlainText",RADCOMMAND_PASTE_AS_HTML:"PasteAsHtml",RADCOMMAND_ABSOLUTE_POSITION:"AbsolutePosition",RADCOMMAND_UNLINK:"Unlink",RADCOMMAND_INSERT_ORDERED_LIST:"InsertOrderedList",RADCOMMAND_INSERT_UNORDERED_LIST:"InsertUnorderedList",RADCOMMAND_INSERT_PARAGRAPH:"InsertParagraph",RADCOMMAND_INSERT_CUSTOM_LINK:"InsertCustomLink",RADCOMMAND_TOGGLE_SCREEN_MODE:"ToggleScreenMode",RADCOMMAND_TOGGLE_DOCKING:"ToggleDocking",RADCOMMAND_ZOOM:"Zoom",RADCOMMAND_APPLY_CLASS:"ApplyClass",RADCOMMAND_REPEAT_LAST_COMMAND:"RepeatLastCommand",RADCOMMAND_MOVE:"MoveCommand",RADCOMMAND_RESIZE:"ResizeCommand",RADCOMMAND_TAB:"EnableTab",DM_DELETE:"DELETE",DM_BACK:"BACKSPACE",KEY_F1:112,KEY_F2:113,KEY_F3:114,KEY_F4:115,KEY_F5:116,KEY_F6:117,KEY_F7:118,KEY_F8:119,KEY_F9:120,KEY_F10:121,KEY_F11:122,KEY_F12:123,KEY_CTRL:17,KEY_SHIFT:16,KEY_ALT:18,KEY_ENTER:13,KEY_HOME:36,KEY_END:35,KEY_LEFT:37,KEY_RIGHT:39,KEY_UP:38,KEY_DOWN:40,KEY_PAGEUP:33,KEY_PAGEDOWN:34,KEY_ESC:27,KEY_SPACE:32,KEY_TAB:9,KEY_BACK:8,KEY_DELETE:46,KEY_INSERT:45,KEY_CONTEXT_MENU:93,KF_CTRL:(1<<0),KF_SHIFT:(1<<2),KF_ALT:(1<<4),TOOL_BUTTON:"B",TOOL_COMBOBOX:"D",TOOL_DROP_BUTTON:"DB",TOOL_SEPARATOR:"S",TOOL_BUTTON_COMBOBOX:"TD",TOOL_CUSTOM:"CUSTOM",CLEAR_PASTE_FORMATTING_NONE:0,CLEAR_PASTE_FORMATTING_NONE_SUPRESS_MESSAGE:1,CLEAR_PASTE_FORMATTING_WORD:2,CLEAR_PASTE_FORMATTING_WORD_NO_FONTS:4,CLEAR_PASTE_FORMATTING_WORD_REMOVE_ALL:8,CLEAR_PASTE_FORMATTING_CSS:16,CLEAR_PASTE_FORMATTING_FONT:32,CLEAR_PASTE_FORMATTING_SPAN:64,CLEAR_PASTE_FORMATTING_ALL:128,DIALOG_PARAMETERS_MODE_JAVASCRIPT:0,DIALOG_PARAMETERS_MODE_SESSION:1,DIALOG_PARAMETERS_MODE_COOKIE:2,IMAGE_MANAGER_DIALOG_NAME:"ImageManager"};
};RadEditorNamespace.Utils={GetCellIndex:function(_1){
var _2=_1?(_1.cellIndex+1):0;
if(TelerikNamespace.Utils.DetectBrowser("safari")){
var oP=_1.parentNode;
for(var i=0;i<oP.cells.length;i++){
if(_1==oP.cells[i]){
_2=i+1;
break;
}
}
}
return _2;
},GetComputedStyle:function(_5,_6,_7){
if(_5.ownerDocument.defaultView&&_5.ownerDocument.defaultView.getComputedStyle){
try{
return _5.ownerDocument.defaultView.getComputedStyle(_5,_7||null)[_6];
}
catch(ev){
}
}else{
if(_5&&_5.currentStyle){
return _5.currentStyle[_6];
}
}
return null;
},ExtendObject:function(_8,_9){
for(var _a in _9){
_8[_a]=_9[_a];
}
},OnItemDragStart:function(){
return false;
},GetPlainTable:function(_b){
var _c=_b.createElement("table");
_c.cellSpacing=0;
_c.cellPadding=0;
_c.border=0;
_c.setAttribute("unselectable","on");
_c.style.cursor="default";
return _c;
},IsMouseInElement:function(e){
var _e=e.pageX?e.pageX:e.clientX;
var _f=e.pageY?e.pageY:e.clientY;
var _10=RadEditorNamespace.Utils.GetEventSource(e);
for(var i=1;i<arguments.length;i++){
var _12=arguments[i];
if(_12.componentFromPoint&&""==_12.componentFromPoint(_e,_f)){
return true;
}else{
if(_10&&(_12==_10||RadEditorNamespace.Utils.IsParentNode(_12,_10))){
return true;
}
}
}
return false;
},StringBuilder:function(_13){
this.length=0;
this.Append=function(_14){
this.length+=(this._parts[this._current++]=String(_14)).length;
this._string=null;
return this;
};
this.ToString=function(){
if(this._string!=null){
return this._string;
}
var s=this._parts.join("");
this._parts=[s];
this._current=1;
this.length=s.length;
return this._string=s;
};
this._current=0;
this._parts=[];
this._string=null;
if(_13!=null){
this.Append(_13);
}
},GetElementsByAttributeName:function(_16,_17,_18){
var arr=[];
if(null!=_16){
if(!_18&&null!=_16&&null!=_16.getAttribute&&null!=_16.getAttribute(_17)){
arr.push(_16);
}
for(var i=0;i<_16.childNodes.length;i++){
arr=arr.concat(this.GetElementsByAttributeName(_16.childNodes[i],_17));
}
}
return arr;
},SelectElement:function(_1b,_1c){
if(!_1c){
return;
}
var _1d=_1b.document;
if(_1d.selection&&!window.opera){
var _1e;
switch(_1c.tagName){
case "TABLE":
case "IMG":
case "HR":
case "INPUT":
_1e=_1d.body.createControlRange();
_1e.add(_1c);
break;
case "UL":
case "OL":
_1e=_1d.body.createTextRange();
_1e.moveToElementText(_1c);
var _1f=_1e.parentElement();
if(_1f.tagName!="UL"||_1f.tagName!="OL"){
_1e.moveEnd("character",-1);
}
break;
default:
_1e=_1d.body.createTextRange();
_1e.moveToElementText(_1c);
break;
}
if(_1e){
_1e.select();
return true;
}
}else{
if(_1b.getSelection){
var _1e=_1d.createRange();
_1e.selectNode(_1c);
if(window.opera){
_1e.selectNodeContents(_1c);
}
var _20=_1b.getSelection();
if(TelerikNamespace.Utils.DetectBrowser("safari")){
_20.setBaseAndExtent(_1e.startContainer,_1e.startOffset,_1e.endContainer,_1e.endOffset);
}else{
_20.removeAllRanges();
_20.addRange(_1e);
}
return true;
}
}
return false;
},MergeElementAttributes:function(_21,_22,_23){
if(!_21||!_22){
return;
}
if(_21.mergeAttributes){
_22.mergeAttributes(_21,_23);
}else{
}
},IsParentNode:function(_24,_25){
if(!_24||!_25){
return false;
}
var _26=_25.parentNode;
do{
if(_26==_24){
return true;
}
}while((_26=_26.parentNode)!=null);
return false;
},FindScrollPosY:function(_27){
var y=0;
var _29=_27;
while(_29.parentNode&&_29.parentNode.tagName!="BODY"){
if(typeof (_29.parentNode.scrollTop)=="number"){
y+=_29.parentNode.scrollTop;
}
_29=_29.parentNode;
}
return y;
},GetRect:function(_2a){
if(!_2a){
_2a=this;
}
var _2b=0;
var top=0;
var _2d=_2a.offsetWidth;
var _2e=_2a.offsetHeight;
if(_2a.x){
_2b=_2a.x;
top=_2a.y;
}else{
while(_2a!=null){
_2b+=_2a.offsetLeft;
top+=_2a.offsetTop;
_2a=_2a.offsetParent;
}
}
_2b=RadEditorNamespace.Utils.GetIntValue(_2b,0);
top=RadEditorNamespace.Utils.GetIntValue(top,0);
_2d=RadEditorNamespace.Utils.GetIntValue(_2d,0);
_2e=RadEditorNamespace.Utils.GetIntValue(_2e,0);
return new RadEditorNamespace.Utils.Rectangle(_2b,top,_2d,_2e);
},Rectangle:function(_2f,top,_31,_32){
this.left=(null!=_2f?_2f:0);
this.top=(null!=top?top:0);
this.width=(null!=_31?_31:0);
this.height=(null!=_32?_32:0);
this.right=_2f+_31;
this.bottom=top+_32;
},MakeSeparator:function(_33,_34){
if(!_33){
return;
}
_33.setAttribute("unselectable","on");
_33.className=_34?"RadESeparatorHorizontal":"RadESeparator";
},IsNull:function(_35,_36){
return (null==_35)?_36:_35;
},IsSystemKey:function(_37){
if(_37>=112&&_37<=123){
return true;
}
if(_37>=8&&_37<=27){
return true;
}
if(_37>=32&&_37<=46){
return true;
}
if(_37==93){
return true;
}
return false;
},Format:function(_38){
for(var i=1;i<arguments.length;i++){
_38=_38.replace(new RegExp("\\{"+(i-1)+"\\}","ig"),arguments[i]);
}
return _38;
},StartsWith:function(_3a,_3b){
if(typeof (_3b)!="string"){
return false;
}
return (0==_3a.indexOf(_3b));
},EndsWith:function(_3c,_3d){
if(typeof (_3d)!="string"){
return false;
}
return (_3c.lastIndexOf(_3d)+_3d.length==_3c.length-1);
},TrimLeft:function(_3e){
if(!_3e||!_3e.replace){
return _3e;
}
return _3e.replace(/^\s+/ig,"");
},TrimRight:function(_3f){
if(!_3f||!_3f.replace){
return _3f;
}
return _3f.replace(/\s+$/ig,"");
},Trim:function(_40){
return (RadEditorNamespace.Utils.TrimLeft(RadEditorNamespace.Utils.TrimRight(_40)));
},ArrayAdd:function(_41,_42){
_41[_41.length]=_42;
},ArrayRemove:function(_43,_44){
var _45=false;
for(var i=0;i<_43.length;i++){
if(_44==_43[i]){
_45=true;
}
if(_45){
_43[i]=_43[i+1];
}
}
if(_45){
_43.length-=1;
}
},AttachEventEx:function(_47,_48,_49){
_48=RadEditorNamespace.Utils.FixEventName(_48);
if(_47.attachEvent){
_47.attachEvent(_48,_49);
}else{
if(_47.addEventListener){
_47.addEventListener(_48,_49,false);
}
}
},DetachEventEx:function(_4a,_4b,_4c){
_4b=RadEditorNamespace.Utils.FixEventName(_4b);
if(_4a.detachEvent){
_4a.detachEvent(_4b,_4c);
}else{
if(_4a.addEventListener){
_4a.removeEventListener(_4b,_4c,false);
}
}
},FixEventName:function(_4d){
_4d=_4d.toLowerCase();
if(document.addEventListener&&RadEditorNamespace.Utils.StartsWith(_4d,"on")){
return _4d.substr(2);
}else{
if(document.attachEvent&&!RadEditorNamespace.Utils.StartsWith(_4d,"on")){
return "on"+_4d;
}else{
return _4d;
}
}
},GetEventSource:function(e){
if(null==e){
return null;
}
if(e.srcElement){
return e.srcElement;
}else{
if(e.target){
return e.target;
}else{
return null;
}
}
},CancelEvent:function(_4f){
if(!_4f){
_4f=window.event;
}
if(!_4f){
return false;
}
_4f.returnValue=false;
_4f.cancelBubble=true;
if(_4f.stopPropagation){
_4f.stopPropagation();
}
if(_4f.preventDefault){
_4f.preventDefault();
}
return false;
},GetElementParentByTag:function(_50,_51){
if(null==_50){
return null;
}
if(null==_51){
return _50;
}
try{
while(_50&&null!=_50.tagName&&_50.tagName!=_51){
_50=_50.parentNode;
}
return ((_50.tagName==_51)?_50:null);
}
catch(e){
return null;
}
},GetOuterHtml:function(_52){
if(_52.outerHTML){
return _52.outerHTML;
}else{
var _53={"IMG":true,"BR":true,"INPUT":true,"META":true,"LINK":true,"PARAM":true,"HR":true};
var _54=_52.cloneNode(true);
var _55=_52.ownerDocument.createElement("DIV");
_55.appendChild(_54);
return _55.innerHTML;
}
},GetIntValue:function(_56,_57){
if(!_57){
_57=0;
}
var _58=parseInt(_56);
return (isNaN(_58)?_57:_58);
},HasHtmlContent:function(_59){
if(!_59||!_59.match){
return _59;
}
return _59.match(/</);
},RemoveProtocolNameAndServerName:function(url){
var _5b=url.indexOf("//");
if(_5b>=0){
_5b=url.indexOf("/",_5b+2);
if(_5b>=0){
return url.substring(_5b);
}
}
return url;
},RemoveElementStyleAttribute:function(_5c,_5d){
if(_5c.style&&_5c.style[_5d]){
_5c.style[_5d]=null;
if(_5c.style.removeAttribute){
_5c.style.removeAttribute(_5d);
}
if(_5c.style.cssText){
}else{
_5c.removeAttribute("style");
}
}
},EscapeRegexSpecialChars:function(_5e){
_5e=_5e.replace(/\\/ig,"\\\\");
_5e=_5e.replace(/&/ig,"&amp;");
_5e=_5e.replace(/\?/ig,"\\?");
_5e=_5e.replace(/\+/ig,"\\+");
_5e=_5e.replace(/\(/ig,"\\(");
_5e=_5e.replace(/\)/ig,"\\)");
_5e=_5e.replace(/\[/ig,"\\[");
_5e=_5e.replace(/\]/ig,"\\]");
_5e=_5e.replace(/\^/ig,"\\^");
_5e=_5e.replace(/\$/ig,"\\$");
_5e=_5e.replace(/\./ig,"\\.");
_5e=_5e.replace(/\*/ig,"\\*");
_5e=_5e.replace(/\|/ig,"\\|");
return _5e;
}};;if("undefined"==typeof (window.RadEditorGlobalArray)){
window.RadEditorGlobalArray=[];
}
function GetRadEditor(_1){
try{
return eval("window['"+_1+"']");
}
catch(e){
return null;
}
}
RadEditorNamespace.ToolbarModesEnum={Default:1,Floating:2,PageTop:4,ShowOnFocus:8};
function RadEditorInitialize(_2,_3,_4,_5,_6,_7,_8,_9,_a,_b,_c,_d,_e,_f,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_1a,_1b,_1c,_1d,_1e,_1f,_20,_21,_22,_23,_24,_25,_26,_27,_28,_29,_2a,_2b,_2c,_2d,_2e,_2f,_30,_31,_32,_33,_34,_35,_36,_37,_38,_39,_3a,_3b,_3c,_3d,_3e,_3f,_40,_41,_42,_43,_44,_45,_46,_47,_48,_49,_4a,_4b,_4c,_4d,_4e,_4f,_50,_51){
var _52=new RadEditor(_2);
if(_42){
_52.AttachClientEvent("OnClientInit",_42);
}
_52.ExecuteClientEvent("OnClientInit");
_52.SpellAllowAddCustom=_47;
_52.FormID=_4a;
_52.SpellId=_22;
_52.AjaxSpellId=_23;
_52.SubmitFnStr=_3;
_52.CancelFnStr=_4;
_52.ApplicationPath=_5;
_52.RadControlsDir=_6?_6:"RadControls";
_52.SkinBasePath=_7?_7:"RadControls/Editor/Skins/Default/";
_52.Direction=_8;
_52.Width=_a?_a:"600";
_52.Height=_b?_b:"600";
_52.ClassName=_9?_9:"RadEContent";
_52.ToolsWidth=_c!=null?_c:_52.Width;
_52.SessionID1=_30;
_52.SessionID2=_31;
_52.ToolbarMode=_1b>-1?_1b:RadEditorNamespace.ToolbarModesEnum.Default;
_52.Language=_e?_e:"en_US";
eval(" var loc = localization_"+_52.Language);
_52.Localization=loc;
_52.AnchorPathToStrip=_12;
_52.ImagesPathToStrip=_13;
_52.UseEmbeddedScripts=_51;
_52.StripAbsoluteAnchorPaths=(true==_14);
_52.StripAbsoluteImagesPaths=(true==_15);
_52.ConvertFontToSpan=(true==_48);
_52.ConvertTagsToLower=(true==_16);
_52.AllowScripts=(true==_17);
_52.ConvertToXhtml=(true==_4c);
_52.FocusOnLoad=(true==_49);
_52.UseClassicDialogs=(true==_1a);
_52.EnableServerSideRendering=(true==_50);
_52.AtlasPartialRendering=(true==_1c);
_52.UseSession=_19;
_52.EnableContextMenus=_1d!=null?_1d:true;
_52.EnableEnhancedEdit=_1f!=null?_1f:true;
_52.EnableTab=_44!=null?_44:true;
_52.EnableHtmlIndentation=(true==_18);
_52.EnableClientSerialize=(true==_45);
_52.EnableDocking=(true==_4b);
if(_52.IsOpera){
_52.EnableDocking=false;
}
_52.ShowHtmlMode=_39!=null?_39:true;
_52.ShowPreviewMode=_3a!=null?_3a:true;
_52.ShowSubmitCancelButtons=_3b!=null?_3b:true;
_52.AllowCustomColors=_1e?_1e:false;
_52.NewLineBr=_21?_21:false;
_52.CausesValidation=_37!=null?_37:false;
_52.ClearPasteFormatting=_20?_20:RadEditorNamespace.CLEAR_PASTE_FORMATTING_NONE;
_52.SnippetsArray=_4d;
_52.MediaFilters=_32;
_52.DocumentFilters=_33;
_52.ImagesFilters=_34;
_52.TemplateFilters=_35;
_52.ThumbSuffix=_38;
_52.ValidationGroup=_4e;
_52.DialogInternalParameters=_4f;
if(_2b&&_2b.length>0){
_52.CssFilesArray=_2b;
}
if(_24&&_24.length>0){
_52.ColorsArray=_24;
}
if(_25&&_25.length>0){
_52.FontNamesArray=_25;
}
if(_26&&_26.length>0){
_52.FontSizesArray=_26;
}
if(_27&&_27.length>0){
_52.RealFontSizesArray=_27;
}
if(_29&&_29.length>0){
_52.ParagraphsArray=_29;
}
if(_28&&_28.length>0){
_52.SymbolsArray=_28;
}
if(_2c&&_2c.length>0){
_52.LinksArray=_2c;
}
if(_2e&&_2e.length>0){
_52.DialogParametersArray=_2e;
}
_52.ToolsArray=_2f;
_52.DefaultModulesArray=_2d;
_52.Languages=_43;
if(_2a&&_2a.length>0){
var _53={};
var _54=_2a.length;
for(var i=0;i<_54;i++){
var _56=_2a[i][1];
var _57=_2a[i][0];
_53[_56.replace(/(.*?)\./ig,function($1){
return $1.toUpperCase();
})]=_57;
}
_52.CssFiltersObject=_53;
}
var _59=_52.FindElement("RadEContentHiddenTextarea");
_59.style.display="none";
_59.setAttribute("id",_52.Id);
_52.ContentHiddenTextarea=_59;
var _5a=_52.FindElement("RadEContentTextarea");
_52.ContentTextarea=_5a;
_5a.removeAttribute("rows",0);
_5a.removeAttribute("cols",0);
_52.ContentAreaElement=_52.FindElement("RadEContentIframe");
_52.WrapperElement=_52.FindElement("RadEWrapper");
_52.DesignButton=_52.FindElement("RadEDesignButton");
_52.HtmlButton=_52.FindElement("RadEHtmlButton");
_52.PreviewButton=_52.FindElement("RadEPreviewButton");
_52.CancelButton=_52.FindElement("RadECancelButton");
_52.UpdateButton=_52.FindElement("RadEUpdateButton");
var _5b=document.getElementById(_52.Id+"_wrapper");
if(_5b){
if(_5b.tagName=="DIV"){
_5b.style.display="block";
}
if(_5b.tagName=="TABLE"){
_5b.style.width=_52.Width;
_5b.style.height=_52.Height;
}
}
_52.PasteContainer=_52.FindElement("PasteContainer");
var _5c=_52.DockingZones;
_5c.TopZone=_52.FindElement("Top");
_5c.LeftZone=_52.FindElement("Left");
_5c.RightZone=_52.FindElement("Right");
_5c.ModuleZone=_52.FindElement("Module");
_5c.BottomZone=_52.FindElement("Bottom");
if(_52.IsOpera&&_5c.BottomZone){
_5c.BottomZone.style.width="";
}
if(_5c.TopZone){
_5c.TopZone.setAttribute("docking","horizontal");
}
if(_5c.LeftZone){
_5c.LeftZone.setAttribute("docking","vertical");
}
if(_5c.RightZone){
_5c.RightZone.setAttribute("docking","vertical");
}
if(_5c.BottomZone){
_5c.BottomZone.setAttribute("docking","horizontal");
}
if(_5c.ModuleZone){
_5c.ModuleZone.setAttribute("docking","horizontal");
}
var _5d=[_52.DesignButton,_52.HtmlButton,_52.PreviewButton];
var _5e=[RadEditorNamespace.RADEDITOR_DESIGN_MODE,RadEditorNamespace.RADEDITOR_HTML_MODE,RadEditorNamespace.RADEDITOR_PREVIEW_MODE];
var _5f=["RADEDITOR_DESIGN_MODE","RADEDITOR_HTML_MODE","RADEDITOR_PREVIEW_MODE"];
var _60=[(_52.ShowHtmlMode||_52.ShowPreviewMode),_52.ShowHtmlMode,_52.ShowPreviewMode];
var _61=function(_62,_63,_64,_65,_66){
if(_63){
if(_66){
_63.style.display="none";
}else{
_63.onclick=function(){
_62.SetMode(_64);
return false;
};
_63.title=_62.Localization[_65];
var _67=_63.getElementsByTagName("IMG")[0];
if(_67){
_67.removeAttribute("alt");
}
_62.UtilButtons[_62.UtilButtons.length]=_63;
}
}
};
for(var i=0;i<_5d.length;i++){
_61(_52,_5d[i],_5e[i],_5f[i],!_60[i]);
}
var _68=function(_69,_6a,_6b,_6c){
if(!_6a){
return;
}
if("UPDATE"==_6b){
_6a.onclick=function(e){
_69.Submit();
return RadEditorNamespace.Utils.CancelEvent(e);
};
}else{
if("CANCEL"==_6b){
_6a.onclick=function(e){
_69.CancelEdit();
return RadEditorNamespace.Utils.CancelEvent(e);
};
}
}
if(_6c){
_6a.innerHTML=_6c;
}
_69.UtilButtons[_69.UtilButtons.length]=_6a;
};
if(_52.ShowSubmitCancelButtons){
_68(_52,_52.CancelButton,"CANCEL",_52.Localization["CancelButton"]);
_68(_52,_52.UpdateButton,"UPDATE",_52.Localization["UpdateButton"]);
}else{
if(_52.CancelButton){
_52.CancelButton.style.display="none";
}
if(_52.UpdateButton){
_52.UpdateButton.style.display="none";
}
}
if(_3c){
_52.AttachClientEvent("OnClientLoad",_3c);
}
if(_3d){
_52.AttachClientEvent("OnClientCommandExecuting",_3d);
}
if(_3e){
_52.AttachClientEvent("OnClientCommandExecuted",_3e);
}
if(_3f){
_52.AttachClientEvent("OnClientModeChange",_3f);
}
if(_40){
_52.AttachClientEvent("OnClientSubmit",_40);
}
if(_41){
_52.AttachClientEvent("OnClientCancel",_41);
}
if(_52.IsIE&&_52.NewLineBr){
_52.AddShortcut("Enter","ENTER");
_52.AddShortcut("ShiftEnter","SHIFT+ENTER");
_52.AddShortcut(RadEditorNamespace.RADCOMMAND_INSERT_PARAGRAPH,"CTRL+ENTER");
}else{
if(!_52.IsSafari&&!window.opera&&!_52.NewLineBr){
_52.AddShortcut("Enter","ENTER");
}
}
if(_52.EnableTab){
_52.AddShortcut(RadEditorNamespace.RADCOMMAND_TAB,"TAB");
}
var _6f=[[RadEditorNamespace.RADCOMMAND_UNDO,"CTRL+Z"],[RadEditorNamespace.RADCOMMAND_REDO,"CTRL+Y"],[RadEditorNamespace.RADCOMMAND_SELECT_ALL,"CTRL+A"],[RadEditorNamespace.RADCOMMAND_COPY,"CTRL+C"],[RadEditorNamespace.RADCOMMAND_PASTE,"CTRL+V"],[RadEditorNamespace.RADCOMMAND_CUT,"CTRL+X"],[RadEditorNamespace.RADCOMMAND_BOLD,"CTRL+B"],[RadEditorNamespace.RADCOMMAND_ITALIC,"CTRL+I"],[RadEditorNamespace.RADCOMMAND_UNDERLINE,"CTRL+U"],[RadEditorNamespace.RADCOMMAND_COPY,"CTRL+INS"],[RadEditorNamespace.RADCOMMAND_PASTE,"SHIFT+INS"],[RadEditorNamespace.RADCOMMAND_TOGGLE_SCREEN_MODE,"F11"],[RadEditorNamespace.RADCOMMAND_SHOW_LINK_DIALOG,"CTRL+K"],[RadEditorNamespace.RADCOMMAND_SHOW_IMAGE_DIALOG,"CTRL+G"]];
for(var i=0;i<_6f.length;i++){
_52.AddShortcut(_6f[i][0],_6f[i][1]);
}
RadEditorNamespace.RegisterInGlobalArray(_52);
var _70=RadEditorNamespace.GetRegisteredCallbackEventsType();
if(_70>0){
RadEditorNamespace.AttachCallbackEventHandlers(_52);
}else{
var _71=_52;
RadEditorNamespace.Utils.AttachEventEx(window,"load",function(){
RadEditorNamespace.AttachCallbackEventHandlers(_71);
});
}
if(_52.EnableContextMenus){
_52.ContextMenuArray=_46;
_52.ContextMenu=RadEditorNamespace.RadEditorContextMenu.New(_52,_52.ContextMenuArray);
}
var _72=window["RadEditorPopupInstance"];
_72.AddStyleSheet(_52.SkinBasePath+"Controls.css");
try{
_72.ShowDropdown(0,0,_52.CancelButton,false,false);
}
catch(e){
}
if(!_52.IsIE){
_72.Hide();
}
if(_52.ConvertFontToSpan){
_52.FiltersManager.Add(new RadFont2SpanFilter());
}
if(_52.ConvertToXhtml){
_52.FiltersManager.Add(new RadConvertToXhtmlFilter());
}
if(_52.StripAbsoluteAnchorPaths){
_52.FiltersManager.Add(new RadStripPathFilter("A",_52.AnchorPathToStrip));
}else{
if(_52.IsIE){
_52.FiltersManager.Add(new RadCleanAnchorsFilter());
}
}
if(_52.StripAbsoluteImagesPaths){
_52.FiltersManager.Add(new RadStripPathFilter("IMG",_52.ImagesPathToStrip));
}
if(_52.ConvertTagsToLower){
_52.FiltersManager.Add(new RadTagNameCaseFilter());
}
if(_52.AllowScripts!=true){
_52.FiltersManager.Add(new RadStripScriptsFilter());
}
if(_52.IsSafari){
_52.FiltersManager.Add(new RadSafariStripFilter());
}
if(!_52.IsIE&&!_52.IsOpera){
_52.FiltersManager.Add(new RadEditorNamespace.RadMozillaContentFilter());
_52.FiltersManager.AddAt(new RadEditorNamespace.RadEditorMozillaFlashStart(_52),0);
}
_52.private_EncodeHiddenAreaContent(false);
var _73=_52.GetHiddenTextareaValue();
_52.private_SetPageHtml(_73,true);
RadEditorNamespace.RunPageLoadCode(_52);
_52.SetEditable(true);
window.setTimeout(function(){
if(_52.FocusOnLoad){
_52.SetFocus();
}else{
if(_52.IsIE){
RadEditorNamespace.InitSetEditableIE(_52);
}
}
},0);
_52.ExecuteClientEvent("OnClientLoad");
return _52;
}
RadEditorNamespace.ConfigureMozillaEditMode=function(_74){
if(!_74.IsIE){
try{
_74.Document.execCommand("UseCSS",false,true);
}
catch(ex){
}
}
};
RadEditorNamespace.StoreBrowserPosition=function(){
var _75=document.body;
var _76=document.documentElement;
RadEditorNamespace.BrowserTop=_75.scrollTop>_76.scrollTop?_75.scrollTop:_76.scrollTop;
RadEditorNamespace.BrowserLeft=_75.scrollLeft>_76.scrollLeft?_75.scrollTop:_76.scrollLeft;
};
RadEditorNamespace.RestoreBrowserPosition=function(){
try{
var _77=document.body;
var _78=document.documentElement;
var top=RadEditorNamespace.BrowserTop;
var _7a=RadEditorNamespace.BrowserLeft;
_77.scrollTop=top;
_77.scrollLeft=_7a;
_78.scrollTop=top;
_78.scrollLeft=_7a;
}
catch(ex){
}
};
RadEditorNamespace.InitSetEditableIE=function(_7b){
if(_7b.IsOpera){
return;
}
var r=_7b.ContentArea.createTextRange();
try{
RadEditorNamespace.StoreBrowserPosition();
var _7d=document.body.createTextRange();
_7d.moveStart("textedit",_7d.text.length);
_7d.collapse(true);
_7d.select();
RadEditorNamespace.RestoreBrowserPosition();
}
catch(e){
}
};
RadEditorNamespace.GetRegisteredCallbackEventsType=function(){
if(typeof (RadCallbackNamespace)!="undefined"&&RadCallbackNamespace.attachEvent){
return 1;
}else{
if(window["OnCallbackRequestStart"]){
return 2;
}
}
return 0;
};
RadEditorNamespace.AttachCallbackEventHandlers=function(_7e){
var _7f=RadEditorNamespace.GetRegisteredCallbackEventsType();
if(_7f==0){
return;
}
var _80=function(){
try{
_7e.FireEvent(RadEditorNamespace.RADEVENT_CALLBACK_STARTED);
}
catch(e){
}
try{
RadEditorNamespace.SaveEditorValue(_7e);
}
catch(ex){
}
};
var _81=function(){
_7e.ValueSaved=false;
RadEditorNamespace.ValidationSucceeded=true;
};
if(_7f==1){
RadCallbackNamespace.attachEvent("onrequeststart",_80);
RadCallbackNamespace.attachEvent("onresponseend",_81);
}else{
if(_7f==2){
var _82=window.OnCallbackRequestStart;
var _83=window.OnCallbackResponseEnd;
window.OnCallbackRequestStart=function(){
_82();
_80();
};
window.OnCallbackResponseEnd=function(){
_83();
_81();
};
}
}
};
RadEditorNamespace.GetEditorPositionInGlobalArray=function(_84){
if("undefined"==typeof (RadEditorGlobalArray)){
window.RadEditorGlobalArray=[];
}
for(var i=0;i<RadEditorGlobalArray.length;i++){
if(RadEditorGlobalArray[i].Id==_84){
return i;
}
}
return -1;
};
RadEditorNamespace.RegisterInGlobalArray=function(_86){
if(0==RadEditorGlobalArray.length){
RadEditorNamespace.Utils.AttachEventEx(window,"unload",function(){
RadEditorNamespace.DisposeAllEditors();
});
}
var _87=RadEditorNamespace.GetEditorPositionInGlobalArray(_86.Id);
RadEditorNamespace.ValidationSucceeded=true;
if(_87==-1){
_86.PostBackRegisterEditor(_86);
RadEditorGlobalArray[RadEditorGlobalArray.length]=_86;
}else{
var _88=RadEditorGlobalArray[_87];
if(_88&&_88.Dispose){
_88.Dispose();
}
RadEditorGlobalArray[_87]=_86;
}
};
RadEditorNamespace.ReplaceAspNetSubmit=function(oID,_8a){
try{
RadEditorNamespace.ReplaceFormSubmit(oID,_8a);
RadEditorNamespace.ReplaceDoPostBack(oID);
}
catch(exc){
}
};
RadEditorNamespace.DoesAnyEditorCauseValidation=function(){
var _8b=RadEditorGlobalArray;
for(var i=0;i<_8b.length;i++){
if(_8b[i].CausesValidation){
return true;
}
}
return false;
};
RadEditorNamespace.ResetEditorSaveStatus=function(){
var _8d=RadEditorGlobalArray;
for(var i=0;i<_8d.length;i++){
_8d[i].ValueSaved=false;
}
};
RadEditorNamespace.SaveAllEditors=function(_8f){
var _90=true;
if(_8f&&RadEditorNamespace.DoesAnyEditorCauseValidation()&&(typeof (Page_ClientValidate)=="function")){
_90=Page_ClientValidate();
}
if(_90){
var _91=RadEditorGlobalArray;
for(var i=0;i<_91.length;i++){
RadEditorNamespace.SaveEditorValue(_91[i]);
}
window.setTimeout(function(){
RadEditorNamespace.ResetEditorSaveStatus();
},100);
}
return _90;
};
RadEditorNamespace.PrepareEditorsForValidation=function(){
var _93=RadEditorGlobalArray;
for(var i=0;i<_93.length;i++){
var _95=_93[i];
if(_95.IsIE&&!_95.HasContent()){
_95.SetHiddenTextareaValue("");
}else{
_95.SetHiddenTextareaValue(_95.GetHtml(true));
}
}
};
RadEditorNamespace.ReplaceDoPostBack=function(oID){
var _97=__doPostBack;
__doPostBack=function(_98,_99){
var _9a=RadEditorNamespace.SaveAllEditors();
if(_9a){
_97(_98,_99);
}
};
};
RadEditorNamespace.ReplaceFormSubmit=function(oID,_9c){
var _9d=_9c.submit;
_9c.submit=function(){
try{
RadEditorNamespace.SaveAllEditors();
var _9e=this.submit;
this.submit=_9d;
var _9f=this.submit();
this.submit=_9e;
}
catch(exc){
}
};
_9c=null;
};
RadEditorNamespace.ReplaceFormOnSubmit=function(_a0){
var _a1=_a0.onsubmit;
_a0.onsubmit=function(){
RadEditorNamespace.SaveAllEditors();
if(typeof (_a1)=="function"){
return _a1();
}
return true;
};
if("undefined"!=typeof (Sys)&&Sys.WebForms&&Sys.WebForms.PageRequestManager){
var _a2=Sys.WebForms.PageRequestManager._onFormSubmit;
Sys.WebForms.PageRequestManager._onFormSubmit=function(){
SaveAllEditors();
if(_a2){
_a2.call(Sys.WebForms.PageRequestManager);
}
};
}
_a0=null;
};
RadEditorNamespace.ReplacePage_ClientValidate=function(){
if(typeof (Page_ClientValidate)=="function"){
var _a3=Page_ClientValidate;
Page_ClientValidate=function(_a4){
RadEditorNamespace.PrepareEditorsForValidation();
RadEditorNamespace.ValidationSucceeded=_a3(_a4);
return RadEditorNamespace.ValidationSucceeded;
};
}
};
RadEditorNamespace.SaveEditorValue=function(_a5){
if(RadEditorNamespace.ValidationSucceeded){
}else{
return;
}
if(_a5.ValueSaved){
return;
}
_a5.private_EncodeHiddenAreaContent(true);
_a5.ValueSaved=true;
};
RadEditorNamespace.SetElementInnerHTML=function(_a6,_a7){
_a6.innerHTML="<span>&nbsp;</span>"+_a7;
_a6.removeChild(_a6.firstChild);
};
RadEditorNamespace.RunPageLoadCode=function(_a8){
if(_a8.InitDocking){
_a8.InitDocking();
}
_a8.LoadToolbars();
if(_a8.LoadModules){
_a8.LoadModules(_a8);
}
_a8.SetSize(_a8.Width,_a8.Height);
RadEditorNamespace.Utils.AttachEventEx(window,"onload",function(){
window.setTimeout(function(){
if(_a8.IsIE){
_a8.SetSize(_a8.Width,_a8.Height);
}
},70);
_a8.Serialize(false);
_a8.FireEvent(RadEditorNamespace.RADEVENT_SIZE_CHANGED);
if(_a8.IsIE||_a8.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.Default)){
try{
_a8.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED);
}
catch(e){
}
}
});
};
RadEditorNamespace.DisposeAllEditors=function(){
var _a9=RadEditorGlobalArray;
for(var _aa=0;_aa<_a9.length;_aa++){
try{
var _ab=_a9[_aa];
_ab.Dispose();
}
catch(e){
}
}
if(RadEditorNamespace.Docking&&RadEditorNamespace.Docking.DisposeDockingObjects){
RadEditorNamespace.Docking.DisposeDockingObjects();
}
};
RadEditorNamespace.ShowEditorStatusBar=function(_ac){
var _ad=document.getElementById("radEditorLoading"+_ac);
if(_ad){
_ad.style.display="block";
}
};
RadEditorNamespace.HideEditorStatusBar=function(_ae){
var _af=document.getElementById("radEditorLoading"+_ae);
if(_af){
_af.style.display="none";
}
};
RadEditorNamespace.GetIndentedElementContent=function(_b0,_b1,_b2,_b3){
if(!_b0){
return;
}
var _b4=_b0.nodeType;
if(1==_b4){
if(_b0.uniqueID){
if(this.UniqueIds[_b0.uniqueID]){
return;
}else{
this.UniqueIds[_b0.uniqueID]=true;
}
}
var _b5=_b0.tagName.toUpperCase();
if(_b0.tagName.charAt(0)=="/"){
return;
}
_b3.Append("\n"+_b1+"<"+(_b0.scopeName&&_b0.scopeName.toLowerCase()!="html"?_b0.scopeName+":":"")+_b5.toLowerCase());
var _b6=_b0.attributes;
var _b7=_b6.length;
if("IMG"==_b5&&_b0.mergeAttributes){
var _b8=document.createElement("IMG");
_b8.mergeAttributes(_b0);
if(_b8.width){
_b3.Append(" width=\""+_b8.width+"\"");
}
if(_b8.height){
_b3.Append(" height=\""+_b8.height+"\"");
}
}
for(var i=0;i<_b7;i++){
var _ba=_b6[i];
var _bb=_ba.nodeName.toUpperCase();
var _bc=_ba.nodeValue;
if((_bc&&_ba.specified)||("IMG"==_b5&&"ALT"==_bb&&_bc)||("INPUT"==_b5&&"VALUE"==_bb&&_bc)||("AREA"==_b5&&"SHAPE"==_bb&&_bc)||("AREA"==_b5&&"COORDS"==_bb&&_bc)){
if("AREA"==_b5&&"HREF"==_bb&&_bc&&_bc.length>0){
var _bd="about:blank";
if(0==_bc.indexOf(_bd)&&_bc.length>_bd.length){
_bc=_bc.substr(_bd.length);
}
}
_b3.Append(" "+_ba.nodeName+"=\""+_bc+"\"");
}
}
if(document.all){
var css=_b0.style.cssText;
if(css){
_b3.Append(" style=\""+css+"\"");
}
}
if(_b0.canHaveChildren||_b0.hasChildNodes()){
_b3.Append(">");
var _bf=_b0.childNodes.length;
var _c0=true;
if(!document.all&&"TD"==_b5){
_c0=false;
for(var i=0;i<_bf;i++){
if(1==_b0.childNodes[i].nodeType&&"BR"==_b0.childNodes[i].tagName){
continue;
}else{
if(3==_b0.childNodes[i].nodeType&&(!_b0.childNodes[0].nodeValue||0==RadEditorNamespace.Utils.Trim(_b0.childNodes[0].nodeValue).length)){
continue;
}else{
_c0=true;
break;
}
}
}
}
var _c1=0;
if(_c0){
for(_c1=0;_c1<_bf;_c1++){
RadEditorNamespace.GetIndentedElementContent(_b0.childNodes[_c1],_b1+_b2,_b2,_b3);
}
}
_b3.Append((_c1>0?"\n"+_b1:"")+"</"+_b5.toLowerCase()+">");
}else{
if(!document.all&&!_b0.hasChildNodes()&&"div"==_b5.toLowerCase()){
_b3.Append("></div>");
}else{
if(_b5=="SCRIPT"){
_b3.Append(">"+_b0.text+"</script>");
}else{
if(_b5=="TITLE"||_b5=="STYLE"||_b5=="COMMENT"||_b5=="IFRAME"){
_b3.Append(">"+_b0.innerHTML+"</"+_b5.toLowerCase()+">");
}else{
_b3.Append("/>");
}
}
}
}
}else{
if(3==_b4){
var str=_b0.data.replace(/[\n\r]/ig,"").replace(/\</ig,"&lt;").replace(/\>/ig,"&gt;");
if(!document.all){
var _c3=RadEditorNamespace.Utils.Trim(str);
if(_c3.length==0){
return _b3;
}
}
_b3.Append("\n"+_b1+str);
}else{
if(8==_b4){
_b3.Append("\n"+_b1+_b0.innerHTML);
}
}
}
return _b3;
};
function RadEditor(id){
this.ParagraphsArray=[["Normal","<p>"],["<h1>Heading 1</h1>","<h1>"],["<h2>Heading 2</h2>","<h2>"],["<h3>Heading 3</h3>","<h3>"],["<h4>Heading 4</h4>","<h4>"],["<h5>Heading 5</h5>","<h5>"],["<dir>Directory List</dir>","<dir>"],["<menu>Menu List</menu>","<menu>"],["<pre>Formatted</pre>","<pre>"],["<address>Address</address>","<address>"]];
this.Id=id;
this.SubmitFnStr="";
this.Mode=RadEditorNamespace.RADEDITOR_DESIGN_MODE;
this.IsIE=document.all&&!window.opera?true:false;
this.IsIE7=(true==(this.IsIE&&null!=window.XMLHttpRequest));
this.IsSafari=TelerikNamespace.Utils.DetectBrowser("safari");
this.IsOpera=window.opera?true:false;
this.ToolbarMode=RadEditorNamespace.ToolbarModesEnum.Default;
this.UseClassicDialogs=false;
this.IsUndoEnabled=true;
this.CausesValidation=false;
this.EnableContextMenus=true;
this.EnableTab=true;
this.ShowHtmlMode=true;
this.ShowPreviewMode=true;
this.ShowSubmitCancelButtons=true;
this.OnClientLoad=null;
this.OnClientCommandExecuting=null;
this.OnClientCommandExecuted=null;
this.OnClientModeChange=null;
this.OnClientSubmit=null;
this.OnClientCancel=null;
this.Document=null;
this.ContentWindow=null;
this.ContentArea=null;
this.ContentAreaElement=null;
this.ContentTextarea=null;
this.ContentHiddenTextarea=null;
this.WrapperElement=null;
this.UpdateButton=null;
this.CancelButton=null;
this.DesignButton=null;
this.HtmlButton=null;
this.PreviewButton=null;
this.IndentHtmlWrapper=null;
this.IndentHtmlElement=null;
this.Width=null;
this.Height=null;
this.ClassName="";
this.LastClassName="";
this.ApplicationPath="";
this.RadControlsDir="";
this.SkinBasePath="";
this.Overflow="";
this.ThumbSuffix="";
this.Direction="";
this.AnchorPathToStrip="";
this.ImagesPathToStrip="";
this.ConvertTagsToLower=false;
this.MediaFilters=null;
this.DocumentFilters=null;
this.ImagesFilters=null;
this.TemplateFilters=null;
this.CssFilesArray=[];
this.CssFiltersObject=null;
this.LinksArray=[];
this.DialogParametersArray=[];
this.DockingZones={};
this.DefaultModulesArray=[];
this.Modules=[];
this.Events=[];
this.Tools=[];
this.ToolsArray=[];
this.Toolbars=[];
this.Filters=[];
this.PageLoadHandlersArray=[];
this.ContentAreaEventHandlers=[];
this.UtilButtons=[];
this.Localization=null;
this.ContextMenu=null;
this.CommandsManager=RadEditorNamespace.RadCommandsManager.New(this);
this.KeyboardManager=RadEditorNamespace.RadKeyboardManager.New();
this.FiltersManager=RadEditorNamespace.RadEditorFiltersManager.New();
this.Disposed=false;
}
RadEditor.prototype={LoadToolbars:function(){
var _c5=this;
if(_c5.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.Floating)){
_c5.FloatingToolbarManager=RadEditorNamespace.FloatingToolbarMode.New(_c5);
}else{
if(_c5.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.PageTop)){
RadEditorNamespace.PageTopToolbarMode.New(_c5);
}else{
if(_c5.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.ShowOnFocus)){
RadEditorNamespace.ShowOnFocusToolbarMode.New(_c5);
}else{
var _c6=function(){
if(!_c5.IsIE){
}
var _c7=function(){
_c5.SetToolbarsVisible(_c5.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE);
};
_c5.AttachEventHandler(RadEditorNamespace.RADEVENT_MODE_CHANGED,_c7);
var _c8=_c5.DockingZones.TopZone;
_c5.SetToolbarsVisible(false);
var _c9=_c5.GetToolbars();
_c5.SetToolbarHolderWidth(_c8);
for(var i=0;i<_c9.length;i++){
var _cb=_c9[i];
var _cc=_c5.GetDockingZoneById(_cb.ZoneId);
if(!_cc){
_cc=_c5.DockingZones.TopZone;
}
_cb.IsVertical=_c5.IsZoneVertical(_cc);
var _cd=_cb.GetTopElement();
_cc.appendChild(_cd);
if(_cb.IsDockable){
_c5.MakeDockable(_cd,useDragHelper=true,useOverlay=true,resizable=false);
}
}
if(_c5.IsIE){
var _ce=_c5.DockingZones.TopZone;
topWidth=_ce.offsetWidth;
if(topWidth==0){
_ce.style.width="100px";
var _cf=window.setInterval(function(){
if(_ce.offsetWidth>0){
window.clearInterval(_cf);
_c5.WrapperElement.onresize();
}
},100);
var _d0=false;
_c5.WrapperElement.onresize=function(){
_d0=!_d0;
if(_d0){
_ce.style.width=_ce.offsetWidth+"px";
}
};
}
if(!_c5.EnableDocking){
var _d1=0;
var _d2=0;
var _d3=0;
RadEditorNamespace.Utils.AttachEventEx(window,"onresize",function(e){
var td=_ce;
td.style.height=td.offsetHeight;
_c5.SetToolbarsVisible(false);
_d3++;
if(!_d1){
if(!_c5||_c5.Disposed){
return;
}
_d1=window.setInterval(function(){
try{
if(_d3>_d2){
_d2=_d3+1;
return;
}
window.clearInterval(_d1);
_d1=0;
var _d6=_c5.GetWidth();
if(_d6>1){
var _d7=_d6-20;
if(_d7>=0){
td.style.width=_d7+"px";
}
}
if(_c5.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE){
_c5.SetToolbarsVisible(true);
}
td.style.height="";
}
catch(ex){
}
},3);
}
});
}
}
_c5.SetToolbarsVisible(true);
};
_c6();
}
}
}
},FindElement:function(_d8){
return document.getElementById(_d8+this.Id);
},PostBackRegisterEditor:function(_d9){
var oID=_d9.Id;
var _db=document.getElementById(_d9.FormID);
if(!_db){
_db=document.forms[0];
}
if(RadEditorGlobalArray.length==0){
RadEditorNamespace.ReplacePage_ClientValidate();
RadEditorNamespace.ReplaceFormOnSubmit(_db);
RadEditorNamespace.ReplaceAspNetSubmit(oID,_db);
}
RadEditorNamespace.Utils.AttachEventEx(_db,"onsubmit",function(){
var _dc=GetRadEditor(oID);
RadEditorNamespace.SaveEditorValue(_dc);
});
_db=null;
},IsToolbarModeEnabled:function(_dd){
return _dd&this.ToolbarMode?true:false;
},SetToolbarHolderWidth:function(_de){
if(this.ToolsWidth){
_de.style.width=this.ToolsWidth;
}else{
var _df=this.Width;
if(_df.indexOf("%")!=-1){
_df=this.GetWidth();
}
if(_df>0){
_de.style.width=parseInt(_df)-10;
}
}
},private_SetPageHtml:function(_e0,_e1){
var _e2=null;
if(-1!=_e0.toLowerCase().indexOf("<html")){
this.FullPage=true;
_e2=_e0;
var _e3=/(<!DOCTYPE(.|\n)*?>)(.|\n)*?/g;
this.DoctypeString=(_e2.match(_e3))?_e2.match(_e3)[0]:"";
}else{
this.FullPage=false;
}
if(null!=_e2||true==_e1){
var _e4=this;
var _e5=function(){
_e4.Document=_e4.ContentAreaElement.contentWindow.document;
_e4.ContentWindow=_e4.ContentAreaElement.contentWindow;
_e4.ContentArea=_e4.Document.body;
var _e6=RadEditorNamespace.GetCssClassServer();
_e6.Reset();
TelerikNamespace.Utils.AddStyleSheet(_e4.SkinBasePath+"EditorContentArea.css",_e4.Document);
if(_e4.CssFilesArray!=null&&_e4.CssFilesArray.length>0){
var arr=_e4.CssFilesArray;
for(var i=0;i<arr.length;i++){
var url=(arr[i].toLowerCase().indexOf("http")==0)||(arr[i].charAt(0)=="/")?arr[i]:_e4.ApplicationPath+arr[i];
if(url){
TelerikNamespace.Utils.AddStyleSheet(url,_e4.Document);
}
}
}else{
if(!_e4.FullPage){
_e6.CopyStyleSheets(document,_e4.Document,_e4.CustomStylesheetAttribute);
}
}
_e4.InitRadEvents();
_e4.EnableEnhancedEdit=!_e4.EnableEnhancedEdit;
_e4.ToggleEnhancedEdit();
if(_e4.FullPage){
_e4.SetHiddenTextareaValue(_ea.body.innerHTML);
}
};
RadEditorNamespace.Utils.AttachEventEx(this.ContentAreaElement,"load",function(){
if(_eb){
_e5();
}
});
if(!_e2){
_e2="<head><style></style></head><body>"+_e0+"</body>";
}
try{
var _ea=this.ContentAreaElement.contentWindow.document;
_ea.open();
_ea.write(_e2);
_ea.close();
var _eb=false;
if(_ea.body){
_e5();
}else{
_eb=true;
}
}
catch(e){
}
}else{
this.SetContent(_e0);
}
if(this.IsSafari){
if(this.Height&&this.Height.indexOf("%")==-1){
this.ContentAreaElement.style.height=this.Height;
}else{
var oTd=this.ContentAreaElement.parentNode;
var _ed=this.Document.createElement("div");
_ed.style.height="100%";
_ed.innerHTML="&nbsp;";
oTd.appendChild(_ed);
var _ee=RadEditorNamespace.Utils.GetRect(oTd).height;
_ed.parentNode.removeChild(_ed);
this.ContentAreaElement.style.height=_ee;
}
}
},SetContent:function(_ef){
try{
RadEditorNamespace.SetElementInnerHTML(this.ContentArea,_ef);
}
catch(e){
}
},GetPageHtml:function(){
var _f0=this;
if(!_f0.IsIE&&!_f0.IsOpera){
var _f1=new RadEditorNamespace.RadEditorMozillaFlashEnd(this);
_f1.MaintainPathsPartTwo(this.ContentArea);
}
if(this.FullPage&&this.Mode!=RadEditorNamespace.RADEDITOR_HTML_MODE){
var _f2=this.Document;
var _f3=_f2.getElementsByTagName("LINK");
var _f4=[];
for(var i=0;i<_f3.length;i++){
var _f6=_f3[i];
if(_f6.href){
if(_f6.href.indexOf("EditorContentArea.css")>0){
_f4[_f4.length]=_f6;
}else{
if(this.CssFilesArray&&this.CssFilesArray.length>0){
var arr=this.CssFilesArray;
for(var _f8=0;_f8<arr.length;_f8++){
if(_f6.href.indexOf(arr[_f8])>=0){
_f4[_f4.length]=_f6;
}
}
}
}
}
}
for(var i=0;i<_f4.length;i++){
_f4[i].parentNode.removeChild(_f4[i]);
}
var _f9=_f2.getElementsByTagName("BODY")[0];
if(_f9){
_f9.removeAttribute("contentEditable");
var _fa=_f9.className.toLowerCase();
if("radecontentbordered"==_fa||"radecontent"==_fa){
_f9.removeAttribute("class",0);
_f9.removeAttribute("classname",0);
}
}
var _fb=_f2.getElementsByTagName("HTML")[0];
var _fc=(this.DoctypeString?this.DoctypeString:"")+(_fb.outerHTML?_fb.outerHTML:RadEditorNamespace.Utils.GetOuterHtml(_fb));
return _fc;
}else{
return this.GetHtml();
}
},ColorsArray:["","#ffff00","#00ff00","#add8e6","#008000","#808080","#ffd700","#ffe4e1","#00ffff","#87ceeb","#0000ff","#a9a9a9","#ffa500","#ffc0cb","#a52a2a","#008080","#000080","#c0c0c0","#ff0000","#c71585","#8b0000","#4b0082","#000000","#ffffff"],FontNamesArray:["Times New Roman","MS Sans Serif","Tahoma","Verdana","Arial","Courier New"],FontSizesArray:[1,2,3,4,5,6,7],RealFontSizesArray:["8pt","9pt","10pt","11pt","12pt","14pt","16pt","18pt","20pt","22pt","24pt","26pt","28pt","36pt","48pt","72pt"],SymbolsArray:["&#8364;","&#162;","&#163;","&#165;","&#164;","&#169;","&#174;","&#8482;","&#177;","&ne;","&#8776;","&#8804;","&#8805;","&#247;","&#215;","&#8734;","&#189;","&#188;","&#190;","&#178;","&#179;","&#8240;","&#182;","&#167;","&#945;","&#946;","&#916;","&#181;","&#937;","&#8721;","&#216;","&ang;","&#186;","&#171;","&raquo;","&#183;","&#8226;","&#8224;","&#8225;","&#402;"],AttachClientEvent:function(_fd,_fe){
if(!_fe){
return;
}else{
this[_fd]=_fe;
}
},ExecuteClientEvent:function(_ff){
try{
var _100=this[_ff];
if(!_100){
return;
}
if(typeof (_100)=="string"){
_100=eval(_100);
this[_ff]=_100;
}
var _101=arguments.length;
if(_101<2){
return _100(this);
}else{
var _102=arguments;
return _100(this,_102[1],_102[2],_102[3]);
}
}
catch(e){
alert("Exception while executing client event "+_ff+" Error:"+e.message);
}
return true;
},HasContent:function(){
var _103=true;
try{
var _104=this.GetText();
_104=RadEditorNamespace.Utils.Trim(_104);
if(!_104){
_103=false;
var oDiv=document.createElement("DIV");
oDiv.innerHTML=this.GetHtml();
var _106=oDiv.childNodes;
for(var _107=0;_107<_106.length;_107++){
var _108=_106[_107];
if(_108&&_108.nodeType==1){
_103=true;
break;
}
}
}else{
_103=true;
}
}
catch(e){
}
return _103;
},SubmitPage:function(){
var _109=(this.CausesValidation&&(typeof (Page_ClientValidate)=="function"))?Page_ClientValidate(this.ValidationGroup):true;
if(_109&&this.SubmitFnStr){
eval(this.SubmitFnStr);
}
},Dispose:function(){
var _10a=this;
if(true==_10a.Disposed){
return;
}
_10a.Disposed=true;
_10a.Serialize(true);
if(_10a.WrapperElement){
_10a.WrapperElement.onresize=null;
}
try{
var _10b=_10a.Tools;
for(var i=0;i<_10b.length;i++){
if(_10b[i].Dispose){
_10b[i].Dispose();
}
_10b[i]=null;
}
}
catch(e){
}
try{
if(_10a.ContextMenu){
_10a.ContextMenu.Dispose();
}
}
catch(e){
}
try{
var _10d=_10a.Toolbars;
for(var i=0;i<_10d.length;i++){
if(_10d[i].Dispose){
_10d[i].Dispose();
}
}
}
catch(e){
}
try{
var _10e=_10a.Modules;
for(var i=0;i<_10e.length;i++){
if(_10e[i].Dispose){
_10e[i].Dispose();
}
}
}
catch(e){
}
var _10f=_10a.UtilButtons;
for(var i=0;i<_10f.length;i++){
_10f[i].onclick=null;
_10f[i]=null;
}
try{
_10a.FireEvent(RadEditorNamespace.RADEVENT_DISPOSE);
}
catch(e){
}
_10a.DetachBrowserEvents();
for(var prop in _10a){
if(typeof (_10a[prop])!="function"){
_10a[prop]=null;
}
}
},Serialize:function(_111){
if(this.private_Serialize){
this.private_Serialize(_111);
}
},MakeDockable:function(_112,_113,_114,_115){
if(!this.EnableDocking){
return;
}
RadEditorNamespace.Docking.MakeDockable(_112,_113,_114,_115);
},GetDockingZoneById:function(_116){
if(_116){
var _117=this.DockingZones;
switch(_116.toLowerCase()){
case "top":
return _117.TopZone;
case "left":
return _117.LeftZone;
case "right":
return _117.RightZone;
case "bottom":
return _117.BottomZone;
case "module":
return _117.ModuleZone;
default:
return document.getElementById(_116);
}
}
},IsZoneVertical:function(zone){
if(!zone){
return null;
}
var _119=zone.getAttribute("docking");
if(_119&&"vertical"==_119){
return true;
}
},ToggleEnhancedEdit:function(){
if(this.EnableEnhancedEdit){
this.SetClassName(this.ClassName);
this.EnableEnhancedEdit=false;
}else{
this.SetClassName("RadEContentBordered");
this.EnableEnhancedEdit=true;
}
var _11a=this.GetToolByName(RadEditorNamespace.RADCOMMAND_TOGGLE_TABLE_BORDER);
if(_11a&&_11a.SetState){
_11a.SetState(this.EnableEnhancedEdit?RadEditorNamespace.RADCOMMAND_STATE_ON:RadEditorNamespace.RADCOMMAND_STATE_OFF);
}
},SetClassName:function(_11b){
this.ContentArea.className=_11b;
this.LastClassName=_11b;
},GetLocalizedString:function(_11c,_11d){
var str=this.Localization[_11c];
if(!str){
return _11d;
}else{
return str;
}
},GetImageUrl:function(_11f){
return (this.SkinBasePath+"Buttons/"+_11f);
},EnableEditing:function(_120,_121,_122,_123,_124,_125,_126,_127){
this.EnableTools(!(!_120||false==_122),_121);
if(this.SetModulesVisible){
this.SetModulesVisible(!(!_120||false==_126));
}
this.DisableModeSwitching=(!_120||false==_127);
if(!_120||false==_123){
this.DisableTypingHandler=function(e){
return RadEditorNamespace.Utils.CancelEvent(e);
};
this.AttachEventHandler("onkeypress",this.DisableTypingHandler);
}else{
if(this.DisableTypingHandler){
this.DetachEventHandler("onkeypress",this.DisableTypingHandler);
}
}
if(!_120||false==_125){
this.EnableTab_temp=this.EnableTab;
this.EnableTab=false;
}else{
if(null!=this.EnableTab_temp){
this.EnableTab=this.EnableTab_temp;
this.EnableTab_temp=null;
}
}
if(!_120||false==_124){
this.EnableContextMenus_temp=this.EnableContextMenus;
this.EnableContextMenus=false;
}else{
if(null!=this.EnableContextMenus_temp){
this.EnableContextMenus=this.EnableContextMenus_temp;
this.EnableContextMenus_temp=null;
}
}
this.EditingEnabled=_120;
if(_120){
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED);
}
},IsEditingEnabled:function(){
return (false!=this.EditingEnabled);
},EnableTools:function(_129,_12a){
this.ToolsEnabled=_129;
var _12b=_129?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
var _12c=this.Tools;
for(var i=0;i<_12c.length;i++){
var _12e=_12c[i];
if(_12e.SetState){
if(!_12a||(_12a&&null==_12a[_12e.Name])){
_12e.SetState(_12b,true);
}
}
}
},SetEditable:function(_12f){
if(this.IsIE||this.IsOpera){
var oEd=this;
window.setTimeout(function(){
oEd.ContentArea.contentEditable=_12f;
try{
oEd.Document.execCommand("2D-Position",false,true);
}
catch(ev){
}
},0);
}else{
try{
this.Document["designMode"]=_12f?"on":"off";
RadEditorNamespace.ConfigureMozillaEditMode(this);
}
catch(e){
}
}
},GetText:function(){
if(this.Mode!=RadEditorNamespace.RADEDITOR_HTML_MODE){
var _131=this.ContentArea;
var _132="";
if(_131.innerText!=null){
_132=_131.innerText;
}else{
if(_131.textContent!=null){
_132=_131.textContent;
}else{
_132=_131.innerHTML.replace(/<\/?[^>]*>/ig,"");
}
}
return _132;
}else{
return this.ContentTextarea.value.replace(/<\/?[^>]*>/ig,"");
}
},IsVisible:function(){
return (this.WrapperElement.style.display!="none");
},Submit:function(){
if(false==this.ExecuteClientEvent("OnClientSubmit")){
return;
}
this.SubmitPage();
},CancelEdit:function(){
if(false==this.ExecuteClientEvent("OnClientCancel")){
return;
}
this.private_EncodeHiddenAreaContent(true);
if(!this.CancelFnStr){
this.CancelFnStr="history.back()";
}
eval(this.CancelFnStr);
},SetFocus:function(){
try{
if(this.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE){
this.ContentWindow.focus();
}else{
if(this.Mode==RadEditorNamespace.RADEDITOR_HTML_MODE){
this.ContentTextarea.focus();
}
}
}
catch(e){
}
},SetActive:function(){
if(this.IsIE){
var _133=this.ContentAreaElement;
if(_133&&_133.setActive){
_133.setActive();
}
}
},ResetSize:function(){
var _134=this;
var _135=_134.GetHeight();
if(_135>0){
_134.SetSize(_134.GetWidth(),_134.GetHeight()+1,false);
_134.SetSize(_134.GetWidth(),_134.GetHeight()-1,false);
}
},SetSize:function(_136,_137,_138){
_136=(""+_136);
_137=(""+_137);
if(-1==_136.indexOf("%")){
_136=parseInt(_136);
if(isNaN(_136)){
_136=300;
}
_136=_136+"px";
}
var _139=false;
if(-1==_137.indexOf("%")){
_137=parseInt(_137);
if(isNaN(_137)){
_137=300;
}
_137=_137+"px";
}else{
_139=true;
}
var _13a=this.WrapperElement;
if(false!=_138){
this.ProposedWidth=_136;
this.FireEvent(RadEditorNamespace.RADEVENT_SIZE_CHANGED);
this.ProposedWidth=null;
}
_13a.style.width=_136;
_13a.style.height=_137;
if(!_139){
this.FixIeHeight(_13a,_137);
}
},FixIeHeight:function(_13b,_13c){
if(this.IsIE&&"CSS1Compat"==document.compatMode){
var _13d=RadEditorNamespace.Utils.GetRect(_13b);
var _13e=(_13d.height-parseInt(_13b.style.height));
if(_13e>0){
var _13f=(parseInt(_13b.style.height)-_13e);
if(_13f>0){
_13b.style.height=_13f+"px";
}
}
}
},GetWidth:function(){
var _140=RadEditorNamespace.Utils.GetRect(this.WrapperElement);
return _140.width;
},GetHeight:function(){
var _141=RadEditorNamespace.Utils.GetRect(this.WrapperElement);
return _141.height;
},SetVisible:function(_142){
this.WrapperElement.style.display=(_142?"":"none");
if(_142&&!this.IsIE){
this.SetEditable(true);
}
if(this.IsSafari&&_142){
this._OnSafariShow();
}
},_OnSafariShow:function(){
var _143=this;
function makeeditableEditor(_144){
var _145=_144.GetHiddenTextareaValue();
try{
_145=_144.GetHtml(true);
}
catch(e){
}
_144.private_SetPageHtml(_145,true);
}
window.setTimeout(function(){
makeeditableEditor(_143);
},100);
},GetClipboardAsHtml:function(){
var _146=this.PasteContainer;
if(!this.PasteContainerMoved){
try{
_146.parentNode.removeChild(_146);
document.body.appendChild(_146);
}
catch(e){
}
this.PasteContainerMoved=true;
}
_146.contentEditable=true;
_146.innerHTML="";
RadEditorNamespace.StoreBrowserPosition();
_146.setActive();
document.execCommand("Paste",null);
RadEditorNamespace.RestoreBrowserPosition();
var _147=_146.innerHTML;
_146.innerHTML="";
return _147;
},GetSelectionHtml:function(){
return this.GetSelection().GetHtmlText();
},GetSelection:function(){
return RadEditorNamespace.RadSelection.New(this.ContentWindow);
},GetSelectedElement:function(){
return this.GetSelection().GetParentElement();
},GetContentArea:function(){
return this.ContentArea;
},GetDocument:function(){
return this.Document;
},GetMode:function(){
return this.Mode;
},GetToolByName:function(name){
var _149=this.Tools;
var _14a=_149.length;
for(var i=0;i<_14a;i++){
if(name==_149[i].Name){
return _149[i];
}
}
return null;
},PasteHtml:function(_14c,_14d,_14e,_14f,_150){
if(!this.IsEditingEnabled()){
return;
}
if(RadEditorNamespace.RADEDITOR_DESIGN_MODE==this.Mode){
this.SetFocus();
this.ExecuteCommand(RadEditorNamespace.RadPasteHtmlCommand.New(_14d,this.ContentWindow,_14c,_14e),null,_150);
if(_14f!=false){
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
}else{
if(RadEditorNamespace.RADEDITOR_HTML_MODE==this.Mode){
if(this.IsIE){
this.ContentTextarea.setActive();
var _151=document.selection.createRange();
_151.text=_14c;
}else{
var _152=this.ContentTextarea;
if(_152.setSelectionRange){
var _153=_152.selectionStart;
var _154=_152.selectionEnd;
var _155=_152.value.substring(_153,_154);
var _156=_14c;
_152.value=_152.value.substring(0,_153)+_156+_152.value.substring(_154);
_152.setSelectionRange(_153+_156.length,_153+_156.length);
this.SetFocus();
return false;
}
}
}
}
},CreateButtonTool:function(_157,_158,_159,_15a,_15b,_15c,_15d){
if(!_158){
_158=this;
}
if(!_159){
_159=this.Document;
}
var _15e=this.Localization[_157];
if(!_15e){
_15e=_157;
}
if(false!=_15b){
if(!_15a){
_15a=this.GetImageUrl(_157+".gif");
}
}
var _15f={GetController:function(){
return _158;
},Document:_159,Name:_157,Title:_15e,IconUrl:_15a,ShowIcon:(false==_15b?false:true),ShowText:(false==_15c?false:true),TextPosition:_15d};
tool=RadEditorNamespace.RadToolBase.New(_15f);
tool.Create();
return tool;
},GetHiddenTextareaValue:function(){
return this.ContentHiddenTextarea.value;
},SetHiddenTextareaValue:function(oVal){
if(this.IsSafari&&this.ContentHiddenTextarea.innerText!=null){
this.ContentHiddenTextarea.innerText=oVal;
}else{
this.ContentHiddenTextarea.value=oVal;
}
},SetMode:function(mode){
if(!this.IsEditingEnabled()||this.DisableModeSwitching){
return;
}
if(!mode){
mode=RadEditorNamespace.RADEDITOR_DESIGN_MODE;
}
if(mode==this.Mode||(mode!=RadEditorNamespace.RADEDITOR_HTML_MODE&&mode!=RadEditorNamespace.RADEDITOR_DESIGN_MODE&&mode!=RadEditorNamespace.RADEDITOR_PREVIEW_MODE)){
return;
}
if(false==this.ExecuteClientEvent("OnClientModeChange")){
return;
}
var _162=RadEditorNamespace.Utils.GetRect(this.WrapperElement);
var _163=_162.height;
var _164=_162.width;
var _165=this.GetPageHtml();
if(_165!=null){
this.SetHiddenTextareaValue(_165);
}
this.Mode=mode;
this.private_SetVisibleArea(mode==RadEditorNamespace.RADEDITOR_HTML_MODE?this.ContentTextarea:this.ContentAreaElement);
this.private_SetPressedButton(mode);
this.SetIndentButtonVisible(mode==RadEditorNamespace.RADEDITOR_HTML_MODE);
this.private_UpdateContentArea();
this.SetEditable(mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE);
if(mode!=RadEditorNamespace.RADEDITOR_HTML_MODE&&this.IsIE&&"CSS1Compat"==document.compatMode){
this.ContentTextarea.style.height="";
}
try{
this.FireEvent(RadEditorNamespace.RADEVENT_MODE_CHANGED);
}
catch(e){
}
this.SetSize(_164,_163,false);
if(mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE){
this.SetClassName(this.LastClassName);
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED);
}else{
if(mode==RadEditorNamespace.RADEDITOR_PREVIEW_MODE){
this.ContentArea.className=this.ClassName;
this.private_HandleLinksInDesignMode(this.Document,false);
}else{
if(mode==RadEditorNamespace.RADEDITOR_HTML_MODE&&this.IsIE&&"CSS1Compat"==document.compatMode){
var _166=this.ContentTextarea;
var _167=RadEditorNamespace.Utils.GetRect(this.DockingZones.LeftZone).height;
var _168=_167-10-RadEditorNamespace.Utils.GetRect(this.DockingZones.BottomZone).height;
if(_168>0){
_166.style.height=_168+"px";
this.FixIeHeight(_166,_168);
}
if(this.ToggleFullScreen){
_166.style.width="100%";
}else{
_166.style.width=RadEditorNamespace.Utils.GetRect(_166.parentNode).width-10;
}
}
}
}
this.SetFocus();
},private_HandleLinksInDesignMode:function(oDoc,_16a){
if(!_16a){
var _16b=oDoc.links;
var oFun=function(){
return false;
};
for(var i=0;i<_16b.length;i++){
_16b[i].onclick=oFun;
}
}
},private_SetPressedButton:function(_16e){
var _16f=[this.DesignButton,this.HtmlButton,this.PreviewButton];
for(var i=0;i<_16f.length;i++){
if(_16f[i]){
_16f[i].className=(i==(_16e-1))?"RadEToggleButtonPressed":"RadEToggleButton";
}
}
},private_SetVisibleArea:function(area){
var _172=(area==this.ContentAreaElement);
var _173=(_172?this.ContentAreaElement:this.ContentTextarea);
var _174=(_172?this.ContentTextarea:this.ContentAreaElement);
if(_173==this.ContentAreaElement){
_173.style.width="100%";
_173.style.height="100%";
}else{
_173.style.display="block";
}
if(_174==this.ContentAreaElement){
_174.style.width="0px";
_174.style.height="0px";
}else{
_174.style.display="none";
}
},SetHtml:function(_175,_176,_177){
if(!this.IsEditingEnabled()){
return;
}
var cmd=RadEditorNamespace.RadGenericCommand.New(_176,this.ContentWindow);
this.SetHiddenTextareaValue(_175);
this.private_UpdateContentArea();
this.SetEditable(true);
this.ExecuteCommand(cmd,_177);
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
},GetHtml:function(_179){
var _17a="";
if(this.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE||this.Mode==RadEditorNamespace.RADEDITOR_PREVIEW_MODE){
_17a=this.ContentArea.innerHTML;
if(this.IsIE){
if(this.ContentArea.firstChild&&("P"==this.ContentArea.firstChild.tagName)&&(this.ContentArea.childNodes.length==1)&&(this.ContentArea.innerHTML.substring(0,3).toLowerCase()=="<p>")){
_17a=this.ContentArea.firstChild.innerHTML;
}
}else{
if(this.ContentArea.childNodes.length==1&&this.ContentArea.firstChild.tagName&&"br"==this.ContentArea.firstChild.tagName.toLowerCase()){
_17a="";
}
}
}else{
if(this.Mode==RadEditorNamespace.RADEDITOR_HTML_MODE){
this.CleanIndent();
_17a=this.ContentTextarea.value;
}
}
if(true==_179){
_17a=this.FiltersManager.GetHtmlContent(_17a);
}
return _17a;
},private_UpdateContentArea:function(){
var _17b=this.GetHiddenTextareaValue();
if(this.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE||this.Mode==RadEditorNamespace.RADEDITOR_PREVIEW_MODE){
var _17c=this.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE?this.FiltersManager.GetDesignContent(_17b):this.FiltersManager.GetPreviewContent(_17b);
this.private_SetPageHtml(_17c);
}else{
if(RadEditorNamespace.RADEDITOR_HTML_MODE){
var _17c=this.FiltersManager.GetHtmlContent(_17b);
this.ContentTextarea.value=_17c;
}
}
this.ValueSaved=false;
},private_EncodeHiddenAreaContent:function(_17d){
if(_17d){
this.FireEvent(RadEditorNamespace.RADEVENT_SUBMIT);
var _17e=this.GetPageHtml();
try{
_17e=this.FiltersManager.GetHtmlContent(_17e);
}
catch(e){
}
var _17f=this.SymbolsArray;
var oDiv=document.createElement("div");
for(var i=0;i<_17f.length;i++){
oDiv.innerHTML=_17f[i];
if(oDiv.innerHTML){
var _182=_17e.split(oDiv.innerHTML);
_17e=_182.join(_17f[i]);
}
}
_17e=TelerikNamespace.Utils.EncodePostbackContent(_17e);
this.SetHiddenTextareaValue(_17e);
this.ContentTextarea.value="";
}else{
var _183=TelerikNamespace.Utils.DecodePostbackContent(this.GetHiddenTextareaValue());
try{
_183=this.FiltersManager.GetDesignContent(_183);
}
catch(e){
}
this.SetHiddenTextareaValue(_183);
}
},GetCssArrayForDocument:function(oDoc){
if(null==oDoc){
oDoc=document;
}
var _185=RadEditorNamespace.GetCssClassServer();
var _186=_185.GetCssArrayForDocument(oDoc);
var _186=this.GetFilteredCssClasses(_186);
return _186;
},GetCssClassesByTagName:function(oTag,_188){
var _189=RadEditorNamespace.GetCssClassServer();
var _18a=_189.GetCssClassesByTagName(oTag,_188);
_18a=this.GetFilteredCssClasses(_18a);
return _18a;
},GetFilteredCssClasses:function(_18b){
var _18c=[];
if(_18b&&this.CssFiltersObject){
for(var _18d=0;_18d<_18b.length;_18d++){
var _18e=_18b[_18d];
if(outAlias=this.CheckCssFilter(_18e.Rule.selectorText)){
_18e.Alias=outAlias;
_18c[_18c.length]=_18e;
}
}
return _18c;
}else{
return _18b;
}
},CheckCssFilter:function(_18f){
if(!_18f||!this.CssFiltersObject){
return null;
}
return this.CssFiltersObject[_18f.replace(/(.*?)\./ig,function($1){
return $1.toUpperCase();
})];
},GetNamedCssForSelectedElement:function(oVal){
var _192=this.CheckCssFilter("."+oVal);
if(!_192){
var _193=this.GetSelection().GetParentElement();
if(_193&&_193.tagName){
_192=this.CheckCssFilter(_193.tagName+"."+oVal);
}
}
return _192;
},Fire:function(_194,_195){
if(false==this.ExecuteClientEvent("OnClientCommandExecuting",_194,_195)){
return;
}
if(!this.IsEditingEnabled()&&_194!="Undo"&&_194!="Redo"){
return;
}
if(this.IsIE){
this.ContentArea.contentEditable=true;
}
this.PendingTextTypeCmd=null;
var _196=RadEditorCommandList[_194];
var _197=false;
if(_196){
_197=(false!=_196(_194,this,_195));
}else{
alert("Could not find the command "+_194+". Please update your command list.");
}
if(_197){
if(!this.IsOpera){
this.SetFocus();
}
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
this.ExecuteClientEvent("OnClientCommandExecuted",_194,_195);
},SetToolState:function(_198){
if(!this.IsEditingEnabled()||false==this.ToolsEnabled){
return;
}
this.ToolsUpdate=true;
var _199=null;
var oCmd=null;
var _19b=this.ContentWindow;
for(var i=0;i<_198.length;i++){
_199=_198[i];
var _19d=_199.Name;
oCmd=RadEditorNamespace.UpdateCommandsArray[_19d];
if(_19d==RadEditorNamespace.RADCOMMAND_UNDO){
_199.SetState(this.CommandsManager.GetUndoState());
}else{
if(_19d==RadEditorNamespace.RADCOMMAND_REDO){
_199.SetState(this.CommandsManager.GetRedoState());
}else{
if(_19d==RadEditorNamespace.RADCOMMAND_REPEAT_LAST_COMMAND){
_199.SetState(this.CommandsManager.CanRepeatLastCommand()?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
}else{
if(_199.SetState&&oCmd&&oCmd.GetState){
_199.SetState(oCmd.GetState(_19b));
}
}
}
}
if(_199.UpdateValue){
_199.UpdateValue(oCmd.GetValue(_19b));
}
_199=oCmd=null;
}
this.ToolsUpdate=false;
},RegisterTool:function(tool){
RadEditorNamespace.Utils.ArrayAdd(this.Tools,tool);
},Undo:function(_19f){
this.CommandsManager.Undo(_19f);
},Redo:function(_1a0){
this.CommandsManager.Redo(_1a0);
},MarkCurrentState:function(_1a1){
return RadEditorNamespace.RadGenericCommand.New(_1a1,this.ContentWindow);
},SaveCurrentState:function(_1a2){
this.ExecuteCommand(_1a2);
},ExecuteCommand:function(_1a3,_1a4,_1a5){
if(false!=_1a4&&!this.IsOpera){
this.SetFocus();
}
this.CommandsManager.Execute(_1a3,_1a5);
},ExecuteBrowserCommand:function(_1a6,_1a7,_1a8,_1a9){
var _1aa=this.Localization[_1a6];
this.ExecuteCommand(RadEditorNamespace.RadBrowserCommand.New(_1aa,_1a6,this.ContentWindow,_1a8));
this.SetActive();
this.SetFocus();
if(true==_1a9){
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
},ExecuteInsertObjectCommand:function(_1ab,_1ac){
this.SetFocus();
return this.ExecuteCommand(RadEditorNamespace.RadPasteHtmlCommand.New(_1ac,this.ContentWindow,RadEditorNamespace.Utils.GetOuterHtml(_1ab)));
},ExecuteFormatObjectCommand:function(_1ad,_1ae,_1af){
return this.ExecuteCommand(RadEditorNamespace.RadFormatObjectCommand.New(_1ae,this.ContentWindow,_1ad,_1af));
},ExecuteApplyCssClassCommand:function(_1b0,_1b1){
return this.ExecuteCommand(RadEditorNamespace.RadStyleCommand.New(_1b1,this.ContentWindow,_1b0));
},ExecuteSetAttributeCommand:function(_1b2,_1b3,_1b4,_1b5){
return this.ExecuteCommand(RadEditorNamespace.RadSetAttributeCommand.New(_1b5,this.ContentWindow,_1b2,_1b3,_1b4));
},ExecuteSetStyleRuleCommand:function(_1b6,_1b7,_1b8,_1b9){
return this.ExecuteCommand(RadEditorNamespace.RadSetStyleRuleCommand.New(_1b9,this.ContentWindow,_1b6,_1b7,_1b8));
},CreateElement:function(_1ba,_1bb,_1bc,sId,_1be,_1bf){
var _1c0=this.Document.createElement(_1ba);
_1c0.style.width=RadEditorNamespace.Utils.IsNull(_1bb,"");
_1c0.style.height=RadEditorNamespace.Utils.IsNull(_1bc,"");
if(null!=sId){
_1c0.id=sId;
}
if(null!=_1be){
_1c0.name=_1be;
}
if(null!=_1bf){
_1c0.value=_1bf;
}
return _1c0;
},SelectElement:function(_1c1,_1c2){
if(RadEditorNamespace.Utils.SelectElement(this.ContentWindow,_1c1)&&false!=_1c2){
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
},CreateRestorePoint:function(){
return RadEditorNamespace.RadCreateRestorePoint(this.ContentWindow);
},InsertImage:function(url){
if(this.IsSafari){
var oImg="<img src='"+url+"'/>";
this.PasteHtml(oImg);
return;
}
if(!this.StripAbsoluteImagesPaths){
var oImg=document.createElement("IMG");
oImg.setAttribute("src",url);
url=oImg.src;
}
this.ExecuteBrowserCommand(RadEditorNamespace.RADCOMMAND_INSERT_IMAGE,false,url,true);
},InsertLink:function(url,text,_1c7){
this.SetActive();
if(!_1c7){
_1c7={};
}
_1c7.href=url;
var _1c8=RadEditorNamespace.RadGenericCommand.New(this.Localization["CreateLink"],this.ContentWindow);
var oSel=this.GetSelection();
var _1ca;
var endR;
if(this.IsIE&&!oSel.IsControl()){
var _1cc=this.Document.selection.createRange();
_1ca=_1cc.duplicate();
endR=_1cc.duplicate();
_1ca.collapse();
endR.collapse(false);
}
var _1cd=RadEditorNamespace.Utils.GetElementParentByTag(oSel.GetParentElement(),"A");
var _1ce;
if(_1cd){
_1ce=_1cd;
this.SetLinkProperties(_1c7,"",_1ce);
}else{
if(oSel.GetText()!=""||oSel.GetParentElement().tagName=="IMG"){
this.ExecuteBrowserCommand(RadEditorNamespace.RADCOMMAND_UNLINK,true,null);
var _1cf=RadEditorNamespace.MarkEditorSelection(this);
var _1d0=_1cf.markedElements;
for(var i=0;i<_1d0.length;i++){
var _1d2=_1d0[i];
var _1d3=RadEditorNamespace.Utils.GetElementParentByTag(_1d2,"A");
if(_1d3){
if(_1d3.href!=_1c7.href){
this.SetLinkProperties(_1c7,"",_1d3);
}
continue;
}
var _1ce=this.Document.createElement("A");
_1ce.innerHTML=_1d2.innerHTML;
_1d2.innerHTML="";
if(this.IsSafari){
_1ce.href="#";
}
_1d2.appendChild(_1ce);
this.SetLinkProperties(_1c7,"",_1ce);
}
var _1d4=_1cf.newElements;
for(var i=0;i<_1d4.length;i++){
if(this.IsIE||this.IsOpera){
_1d4[i].removeNode(false);
}else{
var _1d5=document.createRange();
_1d5.selectNodeContents(_1d4[i]);
var _1d6=_1d5.extractContents();
_1d5.selectNode(_1d4[i]);
_1d5.deleteContents();
_1d5.insertNode(_1d6);
}
}
}else{
var _1d7="editor__tmp__id";
this.PasteHtml("<a href='#' id = '"+_1d7+"'>"+_1c7.text+"</a>");
var _1ce=this.Document.getElementById(_1d7);
_1ce.removeAttribute("id");
_1c7.text=(_1c7.text||_1c7.href||_1c7.mail);
this.SetLinkProperties(_1c7,"",_1ce);
}
}
if(this.IsIE){
try{
var _1d8=this.Document.selection.createRange();
_1d8.setEndPoint("StartToStart",_1ca);
_1d8.setEndPoint("EndToEnd",endR);
_1d8.select();
}
catch(e){
}
}else{
var _1d9=this.ContentWindow.getSelection();
if(!this.IsSafari){
var rng=_1d9.getRangeAt(0);
rng.collapse(true);
}
}
this.ExecuteCommand(_1c8);
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
},SetLinkProperties:function(_1db,_1dc,_1dd){
_1dc=_1dc||this.Localization[RadEditorNamespace.RADCOMMAND_SET_LINK_PROPERTIES];
var _1de=this.GetSelectedElement();
var _1dd=_1dd||RadEditorNamespace.Utils.GetElementParentByTag(_1de,"A");
var _1df=this.Document.createElement("A");
var _1e0=["href","name","title","target","className"];
for(var i=0;i<_1e0.length;i++){
if(_1db[_1e0[i]]&&RadEditorNamespace.Utils.Trim(_1db[_1e0[i]])){
_1df[_1e0[i]]=_1db[_1e0[i]];
}
}
if(_1db.text&&RadEditorNamespace.Utils.Trim(_1db.text)){
_1dd.innerHTML=_1db.text;
}
var _1e2=_1dd.innerHTML;
if(this.IsIE){
_1dd.clearAttributes();
_1dd.mergeAttributes(_1df);
}else{
this.ExecuteFormatObjectCommand(_1df,_1dc,_1dd);
}
if(!_1dd.href){
_1dd.removeAttribute("href");
}else{
if(!this.StripAbsoluteAnchorPaths){
_1dd.setAttribute("href",_1dd.href);
}
}
_1dd.innerHTML=_1e2;
if(this.IsIE&&_1df.name){
_1dd.removeAttribute("name");
_1dd.removeAttribute("NAME");
_1dd.name=null;
_1dd.name=_1df.name;
_1dd["NAME"]=_1df.name;
this.Document.execCommand("CreateBookmark",false,_1df.name);
}
var _1e3=_1dd.getElementsByTagName("IMG");
for(var i=0;i<_1e3.length;i++){
if(!_1e3[i].style.border&&!_1e3[i].border){
_1e3[i].border=0;
}
}
try{
this.SelectElement(_1dd,false);
}
catch(e){
}
this.SetFocus();
},InsertRow:function(_1e4){
var _1e5="Insert row";
if(_1e4=="above"){
_1e5=this.Localization[RadEditorNamespace.RADCOMMAND_INSERT_ROW_ABOVE];
}else{
if(_1e4=="below"){
_1e5=this.Localization[RadEditorNamespace.RADCOMMAND_INSERT_ROW_BELOW];
}
}
this.ExecuteCommand(RadEditorNamespace.RadTableInsertRow.New(_1e5,this.ContentWindow,_1e4));
},InsertColumn:function(_1e6){
var _1e7="Insert column";
if(_1e6=="left"){
_1e7=this.Localization[RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_LEFT];
}else{
if(_1e6=="right"){
_1e7=this.Localization[RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_RIGHT];
}
}
this.ExecuteCommand(RadEditorNamespace.RadTableInsertColumn.New(_1e7,this.ContentWindow,_1e6));
},DeleteRow:function(){
this.ExecuteCommand(RadEditorNamespace.RadTableDeleteRow.New(this.Localization[RadEditorNamespace.RADCOMMAND_DELETE_ROW],this.ContentWindow));
},DeleteColumn:function(){
this.ExecuteCommand(RadEditorNamespace.RadTableDeleteColumn.New(this.Localization[RadEditorNamespace.RADCOMMAND_DELETE_COLUMN],this.ContentWindow));
},DeleteCell:function(){
this.ExecuteCommand(RadEditorNamespace.RadTableDeleteCell.New(this.Localization[RadEditorNamespace.RADCOMMAND_DELETE_CELL],this.ContentWindow));
},MergeColumns:function(){
this.ExecuteCommand(RadEditorNamespace.RadTableMergeColumns.New(this.Localization[RadEditorNamespace.RADCOMMAND_MERGE_COLUMNS],this.ContentWindow));
},MergeRows:function(){
this.ExecuteCommand(RadEditorNamespace.RadTableMergeRows.New(this.Localization[RadEditorNamespace.RADCOMMAND_MERGE_ROWS],this.ContentWindow));
},SplitCell:function(){
this.ExecuteCommand(RadEditorNamespace.RadTableSplitCell.New(this.Localization[RadEditorNamespace.RADCOMMAND_SPLIT_CELL],this.ContentWindow));
},AddShortcut:function(_1e8,_1e9){
if(this.KeyboardManager){
this.KeyboardManager.AddShortcut(_1e8,_1e9);
}
},RemoveShortcut:function(_1ea){
if(this.KeyboardManager){
this.KeyboardManager.RemoveShortcut(_1ea);
}
},SetShortcut:function(_1eb,_1ec){
if(this.KeyboardManager){
this.KeyboardManager.SetShortcut(_1eb,_1ec);
}
},GetDialogParameters:function(_1ed){
var args=null;
var _1ef=this.DialogParametersArray;
for(var i=0;i<_1ef.length;i++){
var _1f1=_1ef[i];
if(_1ed==_1f1[0]){
args={};
dialogArguments=_1f1[1];
for(var j=0;j<dialogArguments.length;j++){
args[dialogArguments[j][0]]=dialogArguments[j][1];
}
}
}
return args;
},ShowDialog:function(url,_1f4,_1f5,_1f6,_1f7,_1f8,_1f9){
var re=this;
if(!_1f8){
_1f8={};
}
_1f8.editor=this;
if(this.Document.selection){
_1f8.rngSelection=this.Document.selection.createRange();
}
_1f8.callBackFn=_1f7;
if(document.selection&&document.selection.type.toLowerCase()=="control"){
document.selection.empty();
document.body.focus();
window.focus();
}
if(!_1f4){
_1f4={};
}
var _1fb=this.Id;
_1f4.editorID=_1fb;
_1f4.HideEditorStatusBar=RadEditorNamespace.HideEditorStatusBar;
_1f4.ColorsArray=this.ColorsArray;
_1f4.CanAddCustomColors=this.AllowCustomColors;
_1f4.StripAbsoluteImagesPaths=this.StripAbsoluteImagesPaths;
_1f4.CommonInternalParameters=this.GetDialogInternalParameters("CommonDialogParameters");
RadEditorNamespace.ShowEditorStatusBar(_1fb);
window.setTimeout("RadEditorNamespace.HideEditorStatusBar('"+_1fb+"')",1000);
var rwi=new RadWindowInfo();
rwi.Url=url;
rwi.Width=_1f5;
rwi.Height=_1f6;
rwi.Caption=(_1f9?_1f9:"");
rwi.IsVisible=true;
rwi.Argument=_1f4;
rwi.CallbackFunc=function(_1fd,_1fe){
if(_1fe.rngSelection&&_1fe.rngSelection.select){
_1fe.rngSelection.select();
}
_1fe.editorID=null;
_1fe.CanAddCustomColors=null;
_1fe.StripAbsoluteImagesPaths=null;
if(_1fe.callBackFn){
var oRes=_1fe.callBackFn(_1fd,_1fe);
if(false==oRes){
return;
}
}
if(null!=_1fd){
_1fe.editor.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
_1fe.editor.SetFocus();
};
rwi.OnLoadFunc=null;
rwi.Param=_1f8;
rwi.Resizable=true;
rwi.Movable=true;
rwi.UseClassicDialogs=re.UseClassicDialogs;
rwi.BlankIFrameLocation=re.BlankIFrameLocation;
wnd=GetEditorRadWindowManager().ShowModalWindow(rwi);
return wnd;
},CleanIndent:function(){
var _200=this.ContentTextarea.value;
if(this.EnableHtmlIndentation&&_200.indexOf(RadEditorNamespace.RADEDITOR_OUTDENT_STEP)>-1){
var os=RadEditorNamespace.RADEDITOR_OUTDENT_STEP;
var re=new RegExp(os+"+","ig");
_200=_200.replace(re,"");
}
this.ContentTextarea.value=_200;
},GetIndentedHtml:function(){
RadEditorNamespace.UniqueIds={};
var elem=document.createElement("DIV");
RadEditorNamespace.SetElementInnerHTML(elem,this.ContentTextarea.value);
var _204=elem.innerHTML;
if(_204){
var _205=new RadEditorNamespace.Utils.StringBuilder("");
RadEditorNamespace.GetIndentedElementContent(elem,"",RadEditorNamespace.RADEDITOR_INDENT_STEP,_205);
_204=_205.ToString();
_204=_204.substring(7,_204.length-7);
this.FiltersManager.EnableDomFilters(false);
var _204=this.FiltersManager.GetHtmlContent(_204);
this.FiltersManager.EnableDomFilters(true);
}
this.ContentTextarea.value=_204;
},SetIndentButtonVisible:function(_206){
if(!this.EnableHtmlIndentation){
return;
}
if(this.FullPage){
return;
}
if(!this.privateIndentButtonProcessed){
var _207=this;
var _208=_207.FindElement("RadEIndentHtml");
_208.innerHTML+=this.Localization["IndentHtml"];
_208.onclick=new Function("this.getElementsByTagName(\"INPUT\")[0].click()");
var _209=_208.getElementsByTagName("INPUT")[0];
_209.onclick=function(e){
if(this.checked){
_207.GetIndentedHtml();
}else{
_207.CleanIndent();
}
if(e&&e.stopPropagation){
e.stopPropagation();
}
};
_207.IndentHtmlWrapper=_208;
_207.IndentHtmlElement=_209;
this.privateIndentButtonProcessed=true;
}
if(null!=this.IndentHtmlWrapper){
this.IndentHtmlWrapper.style.display=_206?"block":"none";
this.IndentHtmlElement.checked=false;
}
},SetOverlay:function(){
var _20b=this.ContentArea;
var shim=document.createElement("IMG");
shim.src=this.SkinBasePath+"Buttons/transp.gif";
shim.style.position="absolute";
shim.style.zIndex=50000;
shim.style.width=parseInt(window.screen.width);
shim.style.height=parseInt(window.screen.height);
shim.style.top=0;
shim.style.left=0;
shim.id="shim"+this.Id;
shim.style.display="block";
document.body.appendChild(shim);
return shim.id;
},ClearOverlay:function(){
var shim=this.FindElement("shim");
if(shim){
shim.parentNode.removeChild(shim);
}
},GetDialogUrl:function(_20e){
var url="";
if(this.UseSession==RadEditorNamespace.DIALOG_PARAMETERS_MODE_SESSION){
var _210=this.RadControlsDir.substr(this.ApplicationPath.length);
url=this.ApplicationPath+this.SessionID1+_210;
}else{
url=this.RadControlsDir;
}
var _211=url+"Editor/Dialog.aspx?dialog="+_20e+"&editorID="+this.Id+"&useSession="+this.UseSession+"&sessionID2="+this.SessionID2+"&language="+this.Language+"&UseEmbeddedScripts="+this.UseEmbeddedScripts;
var _212=this.GetDialogParameters(_20e);
for(var _213 in _212){
_211+="&"+_213+"="+_212[_213];
}
return _211;
},GetDialogInternalParameters:function(_214){
return this.DialogInternalParameters[_214];
}};;RadEditor.prototype.ArrowDropdown="arrowDropdown.gif";
RadEditor.prototype.ArrowIcon="arrowIcon.gif";
RadEditor.prototype.GetToolbars=function(){
if(true==this.EnableServerSideRendering&&true!=this.ServerSideInitialized){
this.ServerSideInitialized=true;
var _1=this.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.Default);
RadEditorNamespace.ServerRenderingInitializer(this,_1);
}else{
this.CreateEditorToolbars(this.ToolsArray);
}
return this.Toolbars;
};
RadEditor.prototype.GetHtmlToolbarElements=function(){
var _2=[];
var _3=this.Id+"Toolbar";
var _4=null;
var _5=0;
while(null!=(_4=document.getElementById(_3+_5))){
_2[_5]=_4;
_5++;
}
return _2;
};
RadEditor.prototype.SetToolbarsVisible=function(_6){
var _7=this.GetHtmlToolbarElements();
for(var i=0;i<_7.length;i++){
this.SetToolbarVisible(_7[i],_6);
}
};
RadEditorNamespace.DockableObjectDisplay=document.all&&!window.opera?"inline":"";
RadEditor.prototype.SetToolbarVisible=function(_9,_a){
if(_a){
if(_9.Show){
_9.Show();
}else{
_9.style.display=RadEditorNamespace.DockableObjectDisplay;
}
}else{
if(_9.Hide){
_9.Hide();
}else{
_9.style.display="none";
}
}
};
RadEditor.prototype.CreateEditorToolbar=function(_b,_c,_d,_e,_f,_10){
var _11=RadEditorNamespace.RadToolbar.New({Id:_b,Document:document,Title:_c,IsDockable:_e&&this.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.Default),ZoneId:_d,IsRibbon:_10});
this.Toolbars[this.Toolbars.length]=_11;
return _11;
};
RadEditor.prototype.CreateEditorToolbars=function(_12){
if(this.ToolbarsCreated){
return;
}else{
this.ToolbarsCreated=true;
}
for(var i=0;i<_12.length;i++){
var arg=0;
var _15=_12[i];
if(_15.length<=1){
continue;
}
var _16=_15[arg++];
var _17=_15[arg++];
var _18=_15[arg++];
var _19=_15[arg++];
var _1a=_15[arg++];
var _1b=_15[arg++];
if(_17){
var _1c=this.Localization[_17];
_17=_1c?_1c:_17;
}
var _1d=this.CreateEditorToolbar(_16,_17,_18,_19&&this.EnableDocking,_1a,_1b);
var _1e=_15[arg++];
for(var j=0;j<_1e.length;j++){
var _20=_1e[j];
var _21=this.CreateEditorTool(_20);
if(_21){
_1d.AddTool(_21);
this.RegisterTool(_21);
}else{
if(_20&&_20[0]==RadEditorNamespace.TOOL_SEPARATOR){
_1d.AddSeparator();
}
}
}
}
};
RadEditor.prototype.CreateEditorTool=function(_22){
var _23=this.Localization;
var _24=this;
var _25=null;
if(_22){
arg=0;
var _26=_22[arg++];
var _27=_22[arg++];
var _28=_22[arg++];
var _29={};
_29.GetController=function(){
return _24;
};
_29.Document=document;
_29.Name=_27;
_29.Type=_26;
_29.Shortcut=_28;
if(_28){
this.SetShortcut(_27,_28);
}
_29.ShowText=_22[arg++];
_29.ShowIcon=_22[arg++];
_29.TextPosition=_22[arg++];
var _2a=_22[arg++];
var _2b=_2a?_2a:this.GetImageUrl(_27+".gif");
_29.IconUrl=_2b;
var _2c=_23[_27];
if(null==_2c){
_2c=_27;
}
_29.Title=_2c;
if(_26==RadEditorNamespace.TOOL_BUTTON){
_25=RadEditorNamespace.RadToolBase.New(_29);
}else{
if(_26==RadEditorNamespace.TOOL_COMBOBOX||_26==RadEditorNamespace.TOOL_BUTTON_COMBOBOX||_26==RadEditorNamespace.TOOL_DROP_BUTTON){
_29.GetDataFunction=function(_2d){
return _24.GetDataArrayForTool(_2d);
};
_29.ArrowUrl=this.GetImageUrl(this.ArrowIcon);
_29.ItemsPerRow=_22[arg++];
_29.PopupWidth=_22[arg++];
_29.PopupHeight=_22[arg++];
var _2e=_22[arg++];
switch(_27){
case RadEditorNamespace.RADCOMMAND_UNDO:
case RadEditorNamespace.RADCOMMAND_REDO:
_29.CellSpacing=1;
_29.PopupWidth=130;
_29.PopupHeight=130;
_29.PopupClassName="RadESymbolPicker";
_29.ClassName="RadEToolLong";
_25=RadEditorNamespace.RadUndoRedoCombo.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_STRIP_FORMAT:
_29.CellSpacing=1;
_29.CellPadding=1;
_29.PopupWidth=180;
_29.PopupHeight=138;
_29.ClassName="RadEToolLong";
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_MANAGE_MODULE:
_29.PopupWidth=180;
_29.PopupHeight=150;
_29.ClassName="RadEToolLong";
_29.SkinBasePath=_24.SkinBasePath;
_25=RadEditorNamespace.RadModuleManagerCombo.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_INSERT_SNIPPET:
_29.PopupWidth=180;
_29.PopupHeight=150;
_29.ClassName="RadEToolLong";
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_INSERT_FORM_ELEMENT:
_29.PopupWidth=180;
_29.PopupHeight=200;
_29.CellPadding=0;
_29.ClassName="RadEToolLong";
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_ZOOM:
if(document.all){
_29.PopupWidth=60;
_29.PopupHeight=175;
_29.ArrowUrl=this.GetImageUrl(this.ArrowDropdown);
_29.IconUrl=null;
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
}
break;
case RadEditorNamespace.RADCOMMAND_INSERT_CUSTOM_LINK:
_29.Width=80;
_29.PopupWidth=220;
_29.PopupHeight=220;
_29.IconUrl=null;
_29.ArrowUrl=this.GetImageUrl(this.ArrowDropdown);
_29.BasePath=this.SkinBasePath;
_25=RadEditorNamespace.RadInsertLinkCombo.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_INSERT_TABLE:
_29.CellSpacing=2;
_29.CellPadding=2;
_29.PopupWidth=122;
_29.PopupHeight=226;
_29.ClassName="RadEToolLong";
_29.CancelLabel=_23["CancelTable"];
_29.TableWizardLabel=_23["TableWizard"];
_29.TableLabel=_23["Table"];
_29.Localization=_23;
_29.IconBasePath=this.SkinBasePath+"Buttons/";
_25=RadEditorNamespace.RadInsertTableCombo.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK:
_29.CellSpacing=2;
_29.CellPadding=2;
_29.PopupWidth=260;
_29.PopupHeight=250;
_29.Width=80;
_29.IconUrl=null;
_29.ArrowUrl=this.GetImageUrl(this.ArrowDropdown);
_29.UpdateValue=function(_2f){
if(!_2f){
return;
}
this.SelectedValue=_2f;
try{
this.HeaderElement.innerHTML=_2f;
}
catch(e){
}
};
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
var _30=_25.OnBeforeShowPopup;
_25.OnBeforeShowPopup=function(){
var _31=this.Popup.GetDocument();
if(!this.HasCopiedCss){
var _32=RadEditorNamespace.GetCssClassServer();
_32.CopyStyleSheets(_24.Document,_31);
this.HasCopiedCss=true;
}
if(_30){
_30.call(this);
}
};
break;
case RadEditorNamespace.RADCOMMAND_FORECOLOR:
case RadEditorNamespace.RADCOMMAND_BACKCOLOR:
_29.AllowCustomColors=this.AllowCustomColors;
_29.AddCustomColor=_23["AddCustomColor"];
_29.AddCustomHexColor=_23["AddCustomHexColor"];
_29.PromptColor=_23["PromptColor"];
_25=RadEditorNamespace.RadColorPicker.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_FONTSIZE:
case RadEditorNamespace.RADCOMMAND_FONTNAME:
var _33=150;
var _34=115;
var _35=80;
if(_27==RadEditorNamespace.RADCOMMAND_FONTSIZE){
_35=21;
_33=100;
_34=180;
}
var _36=function(_37){
this.SelectedValue=_37;
try{
if(!_37){
_37=this.Title;
}
this.HeaderElement.innerHTML=(""+_37).replace(/\s+/ig,"&nbsp;");
}
catch(e){
}
};
_29.CellSpacing=0;
_29.PopupWidth=_33;
_29.PopupHeight=_34;
_29.PopupClassName="RadEDropDownFont";
_29.ArrowUrl=this.GetImageUrl(this.ArrowDropdown);
_29.IconUrl=null;
_29.Width=_35;
_29.UpdateValue=_36;
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_REAL_FONTSIZE:
var _36=function(_38){
var o=this.GetController().GetSelectedElement();
if(o){
var _3a=RadEditorNamespace.Utils.GetComputedStyle(o,"fontSize");
if(_3a){
newSize=parseFloat(_3a);
if(newSize>parseInt(_3a)){
if(_3a.indexOf("px")>-1){
_3a=parseInt(_3a)+"px";
}
}
}
var _3b=_3a?_3a:this.Title;
try{
this.HeaderElement.innerHTML=_3b;
}
catch(e){
}
}
};
_29.CellSpacing=0;
_29.Width=40;
_29.PopupClassName="RadEDropDownFont";
_29.PopupWidth=50;
_29.PopupHeight=170;
_29.ArrowUrl=this.GetImageUrl(this.ArrowDropdown);
_29.IconUrl=null;
_29.UpdateValue=_36;
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_INSERT_SYMBOL:
_29.PopupWidth=195;
_29.PopupHeight=102;
_29.CellSpacing=0;
_29.CellPadding=0;
_29.Width=40;
_29.IsPopupScrollable=false;
_29.ClassName="RadEToolLong";
_29.PopupClassName="RadESymbolPicker";
_29.ItemsPerRow=8;
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_APPLY_CLASS:
_29.PopupWidth=180;
_29.PopupHeight=150;
_29.Width=80;
_29.IconUrl=null;
_29.ArrowUrl=this.GetImageUrl(this.ArrowDropdown);
_29.PopupIconPath=this.SkinBasePath+"Img/";
_29.ClearStyleString=_23["ClearStyle"];
_25=RadEditorNamespace.RadCssCombo.New(_29);
break;
case RadEditorNamespace.RADCOMMAND_SPELLCHECK:
case "AjaxSpellCheck":
if(!this.Languages||0==this.Languages.length){
_25=RadEditorNamespace.RadToolBase.New(_29);
}else{
_29.ClassName="RadEToolLong";
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
}
break;
default:
var _3c=function(_3d){
return function(){
return _3d;
};
};
_29.GetDataFunction=_3c(_2e);
if(_2a){
_29.IconUrl=_2b;
_29.ClassName="RadEToolLong";
}else{
_29.IconUrl=null;
_29.Width=80;
_29.ArrowUrl=this.GetImageUrl(this.ArrowDropdown);
}
if(_26=="TD"){
_25=RadEditorNamespace.RadEditorButtonComboBox.New(_29);
}else{
_29.CellSpacing=1;
_29.CellPadding=1;
_25=RadEditorNamespace.RadEditorComboBox.New(_29);
}
break;
}
}else{
if(_26==RadEditorNamespace.TOOL_CUSTOM&&RadEditorToolInitializer&&RadEditorToolInitializer[_27]){
_25=RadEditorToolInitializer[_27](_29);
}
}
}
}
return _25;
};
RadEditor.prototype.GetDataArrayForTool=function(_3e){
if(!this.RawDataArrays){
this.RawDataArrays={};
var _3f=this;
var _40=_3f.Localization;
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_UNDO]=function(){
return _3f.CommandsManager.GetCommandsToUndo();
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_REDO]=function(){
return _3f.CommandsManager.GetCommandsToRedo();
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_MANAGE_MODULE]=function(){
return _3f.Modules;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_INSERT_CUSTOM_LINK]=function(){
return _3f.LinksArray;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_INSERT_SNIPPET]=function(){
return _3f.SnippetsArray;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_FORECOLOR]=this.RawDataArrays[RadEditorNamespace.RADCOMMAND_BACKCOLOR]=function(){
return _3f.ColorsArray;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_SPELLCHECK]=this.RawDataArrays["AjaxSpellCheck"]=function(){
return _3f.Languages;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_ZOOM]=function(){
return [["10%","10%"],["20%","20%"],["50%","50%"],["100%","100%"],["150%","150%"],["200%","200%"],["300%","300%"],["500%","500%"]];
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_STRIP_FORMAT]=function(){
return [["ALL",_40["ClearAllHtmlTags"],_3f.GetImageUrl("StripAll.gif")],["WORD",_40["ClearWordFormatting"],_3f.GetImageUrl("StripWord.gif")],["CSS",_40["ClearCssFormatting"],_3f.GetImageUrl("StripCss.gif")],["FONT",_40["ClearFontTags"],_3f.GetImageUrl("StripFont.gif")],["SPAN",_40["ClearSpanTags"],_3f.GetImageUrl("StripSpan.gif")]];
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_INSERT_FORM_ELEMENT]=function(){
return [["FORM",_40["FormForm"],_3f.GetImageUrl("InsertFormForm.gif")],["BUTTON",_40["FormButton"],_3f.GetImageUrl("InsertFormButton.gif")],["CHECKBOX",_40["FormCheckbox"],_3f.GetImageUrl("InsertFormCheckbox.gif")],["HIDDEN",_40["FormHidden"],_3f.GetImageUrl("InsertFormHidden.gif")],["RADIO",_40["FormRadio"],_3f.GetImageUrl("InsertFormRadio.gif")],["PASSWORD",_40["FormPassword"],_3f.GetImageUrl("InsertFormPassword.gif")],["RESET",_40["FormReset"],_3f.GetImageUrl("InsertFormReset.gif")],["SELECT",_40["FormSelect"],_3f.GetImageUrl("InsertFormSelect.gif")],["SUBMIT",_40["FormSubmit"],_3f.GetImageUrl("InsertFormSubmit.gif")],["TEXT",_40["FormText"],_3f.GetImageUrl("InsertFormText.gif")],["TEXTAREA",_40["FormTextarea"],_3f.GetImageUrl("InsertFormTextarea.gif")]];
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK]=function(){
var _41=_3f.ParagraphsArray;
if(_41){
var x;
for(var k=0;k<_41.length;k++){
x=_41[k][0];
_41[k][0]=_41[k][1];
_41[k][1]=x;
}
}
return _41;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_FONTSIZE]=function(){
var _44=_3f.FontSizesArray;
if(_44){
_44=_44.concat([]);
for(var _45=0;_45<_44.length;_45++){
var _46=_44[_45];
_44[_45]=[_46,RadEditorNamespace.Utils.Format("<font size={0}>",_46)+_46+"</font>"];
}
}
return _44;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_REAL_FONTSIZE]=function(){
var _47=_3f.RealFontSizesArray;
if(_47){
_47=_47.concat([]);
for(var _48=0;_48<_47.length;_48++){
var _49=_47[_48];
_47[_48]=[_49,_49];
}
}
return _47;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_FONTNAME]=function(){
var _4a=_3f.FontNamesArray;
if(_4a){
_4a=_4a.concat([]);
for(var _4b=0;_4b<_4a.length;_4b++){
var _4c=_4a[_4b];
_4a[_4b]=[_4c,RadEditorNamespace.Utils.Format("<span style='font:normal 13px {0};'>",_4c)+_4c+"</span>"];
}
}
return _4a;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_INSERT_SYMBOL]=function(){
var _4d=_3f.SymbolsArray;
if(_4d){
_4d=_4d.concat([]);
}
for(var _4e=0;_4e<_4d.length;_4e++){
var _4f=_4d[_4e];
_4f=("&"==_4f?"&amp;":_4f);
_4d[_4e]=[_4f,_4f];
}
return _4d;
};
this.RawDataArrays[RadEditorNamespace.RADCOMMAND_APPLY_CLASS]=function(){
return _3f.GetCssArrayForDocument(_3f.Document);
};
}
if(this.RawDataArrays[_3e]){
return this.RawDataArrays[_3e]();
}
};;RadEditorNamespace.RadCommandsManager={New:function(_1){
var _2={};
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.Commands=[];
_2.CurrentCommandIndex=-1;
_2.EventDispatcher=_1;
return _2;
},Execute:function(_3,_4){
if(_3&&_3.Execute){
var _5=_3.Execute();
if(false==_4){
return false;
}
if(_5&&_3.CanUnexecute){
this.ClearCommandsToRedo();
RadEditorNamespace.Utils.ArrayAdd(this.Commands,_3);
this.CurrentCommandIndex=this.Commands.length-1;
return true;
}
}
return false;
},RemoveCommandAt:function(_6){
this.Commands.splice(_6,1);
if(this.CurrentCommandIndex>=_6){
this.CurrentCommandIndex--;
}
},ClearCommandsToRedo:function(){
if(this.IsRedoAvailable()){
this.Commands.splice(this.CurrentCommandIndex+1,this.Commands.length-this.CurrentCommandIndex);
}
},Undo:function(_7){
_7=Math.min(RadEditorNamespace.Utils.IsNull(_7,0),this.Commands.length);
var _8=0;
var _9=null;
while(0<_7--&&0<=this.CurrentCommandIndex&&this.CurrentCommandIndex<this.Commands.length){
_9=this.Commands[this.CurrentCommandIndex--];
if(_9){
_9.Unexecute();
_8++;
}
}
},Redo:function(_a){
_a=Math.min(RadEditorNamespace.Utils.IsNull(_a,0),this.Commands.length);
var _b=0;
var _c=null;
var _d=this.CurrentCommandIndex+1;
while(0<_a--&&0<=_d&&_d<this.Commands.length){
_c=this.Commands[_d];
if(_c){
_c.Execute();
this.CurrentCommandIndex=_d;
_b++;
}
_d++;
}
},IsUndoAvailable:function(){
return (-1<this.CurrentCommandIndex);
},IsRedoAvailable:function(){
return (this.CurrentCommandIndex<this.Commands.length-1);
},GetUndoState:function(){
return this.IsUndoAvailable()?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
},GetRedoState:function(){
return this.IsRedoAvailable()?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
},GetCommandsToUndo:function(){
if(this.IsUndoAvailable()){
return (this.Commands.slice(0,this.CurrentCommandIndex+1)).reverse();
}else{
return [];
}
},GetCommandsToRedo:function(){
if(this.IsRedoAvailable()){
return this.Commands.slice(this.CurrentCommandIndex+1);
}else{
return [];
}
},CanRepeatLastCommand:function(){
return ((this.CurrentCommandIndex==this.Commands.length-1)&&null!=this.Commands[this.CurrentCommandIndex]&&("function"==typeof (this.Commands[this.CurrentCommandIndex].Clone)));
},RepeatLastCommand:function(){
if(this.CanRepeatLastCommand()){
var _e=this.Commands[this.CurrentCommandIndex].Clone();
this.Execute(_e);
}
}};;RadEditorNamespace.RadEditorContextMenu={New:function(_1,_2){
var _3={};
RadEditorNamespace.Utils.ExtendObject(_3,this);
_3.Editor=_1;
_1.AttachEventHandler(RadEditorNamespace.RADEVENT_CONTEXTMENU,function(_4,e){
if(_1.EnableContextMenus&&_1.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE){
_3.Show(e);
e.cancelBubble=true;
return false;
}
});
_3.Localization=_3.Editor.Localization;
_3.IsIE=_3.Editor.IsIE;
_3.ImagesPath=_3.Editor.SkinBasePath+"Buttons/";
_3.Popup=window["RadEditorPopupInstance"];
_3.Popup.AddStyleSheet(_3.Editor.SkinBasePath+"Editor.css");
_3.SelectedValue=null;
_3.IsCreated=false;
_3.EnabledContextMenus={};
_3.ContextMenusArray=[["TABLE",true,[["B","ToggleTableBorder",""],["B","SetTableProperties",""],["B","DeleteTable",""]]],["TD",true,[["B","InsertRowAbove",""],["B","InsertRowBelow",""],["B","DeleteRow",""],["B","InsertColumnLeft",""],["B","InsertColumnRight",""],["B","DeleteColumn",""],["B","MergeColumns",""],["B","MergeRows",""],["S","",""],["B","SplitCell",""],["B","DeleteCell",""],["B","SetCellProperties",""],["B","SetTableProperties",""],["B","ToggleTableBorder",""]]],["IMG",true,[["B","SetImageProperties",""],["B","ImageMapDialog",""]]],["A",true,[["B","SetLinkProperties",""],["B","Unlink",""]]],["*",true,[["B","Cut",""],["B","Copy",""],["B","Paste",""],["B","PasteFromWord",""],["B","PastePlainText",""],["B","PasteAsHtml",""]]]];
if(_2&&_2.length>0){
_3.ContextMenusArray=_3.ContextMenusArray.concat(_2);
}
_3.ContextMenus={};
return _3;
},IsMenuDisabled:function(_6){
return (false==this.EnabledContextMenus[_6]);
},Create:function(){
var _7=[];
for(var i=0;i<this.ContextMenusArray.length;i++){
var _9=this.ContextMenusArray[i];
var _a=_9[0].toUpperCase();
this.EnabledContextMenus[_a]=_9[1];
if(false==_9[1]){
continue;
}else{
var _b=_9[2];
var _c=_7[_a];
if(_c){
this.DisposeContextMenu(_a,_c);
}
_7[_a]=this.CreateContextMenu(_b);
}
}
this.ContextMenus=_7;
},CreateContextMenu:function(_d){
if(!_d||_d.length==0){
return null;
}
var _e=this.Popup.GetDocument();
var _f=RadEditorNamespace.Utils.GetPlainTable(_e);
_f.style.width=165;
_f.className="RadEContextMenu";
var _10=[];
var _11=0;
for(var i=0;i<_d.length;i++){
var _13=_d[i];
var _14=_13[0];
var _15=_13[1];
var row=_f.insertRow(-1);
var _17=row.insertCell(0);
if(_14==RadEditorNamespace.TOOL_SEPARATOR){
RadEditorNamespace.Utils.MakeSeparator(_17,true);
_17.setAttribute("align","center");
_11++;
}else{
var _18=this.Editor.CreateButtonTool(_15,this,_e,null,null,true);
_17.appendChild(_18.GetTopElement());
_10[_10.length]=_18;
}
}
height=3+(_10.length*24)+(_11*5);
var _19={Tools:_10,Width:170,Height:height,TopElement:_f};
return _19;
},DisposeContextMenu:function(_1a,_1b){
if(_1b){
if(_1b.Tools){
for(var i=0;i<_1b.Tools.length;i++){
var _1d=_1b.Tools[i];
if(_1d&&_1d.Dispose){
_1d.Dispose();
}
}
_1b.Tools=null;
}
_1b.TopElement=null;
}
if(this.ContextMenus){
this.ContextMenus[_1a]=null;
}
},Dispose:function(){
for(var _1e in this.ContextMenus){
var _1f=this.ContextMenus[_1e];
this.DisposeContextMenu(_1e,_1f);
}
this.Popup=null;
this.ContextMenus=null;
this.ContextMenusArray=null;
this.EnabledContextMenus=null;
this.SelectedValue=null;
this.Editor=null;
},Fire:function(_20,_21){
_21.SetState(RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
this.Popup.Hide();
this.Editor.Fire(_20,this);
},GetSelectedValue:function(){
var _22=this.SelectedValue;
this.SelectedValue=null;
return _22;
},Show:function(e){
if(!this.IsCreated){
this.Create();
this.IsCreated=true;
}
var _24=e.srcElement?e.srcElement:e.target;
var _25=_24.tagName;
if(this.IsMenuDisabled(_25)){
return;
}
var _26=this.ContextMenus[_25];
if("TH"==_25&&!_26){
_26=this.ContextMenus["TD"];
}
var _27="";
if(!_26){
var _28=RadEditorNamespace.Utils.GetElementParentByTag(_24,"A");
if(!_28){
_28=RadEditorNamespace.Utils.GetElementParentByTag(_24,"TD");
}
if(!_28){
_28=RadEditorNamespace.Utils.GetElementParentByTag(_24,"TABLE");
}
if(_28){
_27=_28.tagName;
_24=_28;
}else{
_27="*";
}
_26=this.ContextMenus[_27];
}
if(_26&&_25=="IMG"&&_24&&"true"==_24.getAttribute("isflash")){
_26=null;
}
if(_26){
this.SelectedValue=_24;
var _29=this.Editor.ToolsUpdate;
this.Editor.ToolsUpdate=true;
for(var i=0;i<_26.Tools.length;i++){
var _2b=_26.Tools[i];
var _2c=RadEditorNamespace.UpdateCommandsArray[_2b.Name];
if(_2b.SetState&&_2c&&_2c.GetState){
_2b.SetState(_2c.GetState(this.Editor.ContentWindow),true);
}else{
if(_2b.Name&&_2b.Name.indexOf("Paste")==0){
var _2d=RadEditorNamespace.UpdateCommandsArray["Paste"];
if(_2d){
_2b.SetState(_2d.GetState(this.Editor.ContentWindow),true);
}
}else{
_2b.SetState(RadEditorNamespace.RADCOMMAND_STATE_OFF,true);
}
}
}
this.Editor.ToolsUpdate=_29;
if(_26.TopElement){
window["RadEditorPopupInstance"].SetTopElement(_26.TopElement);
}
this.Popup.ShowContextMenu(e,_26.Width,_26.Height,this.Editor.ContentArea);
}else{
if(!this.IsIE){
this.Popup.Hide();
e.preventDefault();
}
return false;
}
},GetImageUrl:function(_2e){
return (this.ImagesPath+_2e+".gif");
}};;RadEditorNamespace.GetCssClassServer=function(){
return RadEditorNamespace.RadCssClassServer;
};
RadEditorNamespace.RadCssClassServer={IsIE:(document.all&&!window.opera?true:false),DocumentArray:[],Reset:function(){
this.DocumentArray=[];
},AddStyleSheet:function(_1,_2){
TelerikNamespace.Utils.AddStyleSheet(_1,_2);
},CopyStyleSheets:function(_3,_4){
if(null==_3&&null==_4){
return;
}
var _5=0;
var _6=null;
if(_4.styleSheets.length==0){
if(_4.createStyleSheet){
_4.createStyleSheet();
}else{
css=_4.createElement("style");
css.media="all";
css.type="text/css";
var _7=_4.getElementsByTagName("head")[0];
_7.appendChild(css);
_6=css;
}
}
if(_4.styleSheets[0]){
_6=_4.styleSheets[0];
}
for(var i=0;i<_3.styleSheets.length;i++){
try{
var _9=_3.styleSheets[i];
var _a=_9.href;
if(_a&&_a.indexOf("Editor/Skins")>0){
continue;
}
if(_a&&(_a.indexOf("Spell/Skins")>0)&&(_a.indexOf("Main.css")>0)){
continue;
}
var _b=(_9.rules)?_9.rules:_9.cssRules;
for(var j=0;j<_b.length;j++){
var _d=_b[j];
if(_6.addRule){
var _e=_d.selectorText;
var _f=_d.style.cssText;
if(_f&&_e){
_6.addRule(_e,_f,_5);
}
}else{
if(_6.insertRule){
_6.insertRule(_d.cssText,_5);
}else{
var _f=_d.selectorText+"{"+_d.style.cssText+"}";
var _10=_4.createTextNode(_f);
_6.appendChild(_10);
}
}
_5++;
}
}
catch(exc){
}
}
},GetCssArrayForDocument:function(_11,_12){
var _13=_11!=null?_11:document;
var _14=this.GetServerObjectForDocument(_11);
if(_14&&true!=_12){
return _14.CssClassArray;
}
if(true==_12){
for(var _15=0;_15<this.DocumentArray.length;_15++){
var _16=this.DocumentArray[_15];
if(_16.Document==_13){
this.DocumentArray.splice(_15,1);
break;
}
}
}
var _17=[];
for(var i=0;i<_13.styleSheets.length;i++){
try{
var _19=_13.styleSheets[i];
var _1a=_19.href?_19.href:"";
if(_1a.indexOf("Editor/Skins")>0){
continue;
}
if((_1a.indexOf("Spell/Skins")>0)&&(_1a.indexOf("Main.css")>0)){
continue;
}
var _1b=(this.IsIE)?_19.rules:_19.cssRules;
for(var j=0;j<_1b.length;j++){
var _1d=RadEditorNamespace.RadCssClass.New(_1b[j]);
_17[_17.length]=_1d;
}
}
catch(ex){
}
}
this.DocumentArray[this.DocumentArray.length]={Document:_13,CssClassArray:_17};
return _17;
},GetCssClassesByTagName:function(_1e,_1f){
var _20=this.GetServerObjectForDocument(_1f);
if(!_20){
this.GetCssArrayForDocument(_1f);
_20=this.GetServerObjectForDocument(_1f);
}
if(!_1e){
return _20.CssClassArray;
}
_1e=_1e.toUpperCase();
var _21=_20[_1e];
if(_21!=null){
try{
if(_21[0]){
var _22=_21[0].Rule.selectorText;
}
return _21;
}
catch(e){
this.GetCssArrayForDocument(_1f,true);
_20=this.GetServerObjectForDocument(_1f);
}
}
var arr=[];
for(var i=0;i<_20.CssClassArray.length;i++){
var rcc=_20.CssClassArray[i];
if(rcc.Tag.toUpperCase()==_1e||rcc.Tag=="ALL"){
arr[arr.length]=rcc;
}
}
arr.sort(RadEditorNamespace.SortRadCssClassesArrayByTagAlias);
_20[_1e]=arr;
return arr;
},GetServerObjectForDocument:function(_26){
var _27=_26!=null?_26:document;
for(var _28=0;_28<this.DocumentArray.length;_28++){
var _29=this.DocumentArray[_28];
if(_29.Document==_27){
return _29;
}
}
return null;
}};
RadEditorNamespace.SortRadCssClassesArrayByTagSelectorText=function(_2a,_2b){
if(!_2a&&!_2b){
return 0;
}
if(!_2b){
return 1;
}
if(!_2a){
return -1;
}
return _2a.CompareByTagSelectorText(_2b);
};
RadEditorNamespace.SortRadCssClassesArrayByTagAlias=function(_2c,_2d){
if(!_2c&&!_2d){
return 0;
}
if(!_2d){
return 1;
}
if(!_2c){
return -1;
}
return _2c.CompareByTagAlias(_2d);
};
RadEditorNamespace.RadCssClass={New:function(_2e){
var obj={};
RadEditorNamespace.Utils.ExtendObject(obj,this);
obj.Rule=_2e;
obj.Tag=obj.GetClassTag(obj.Rule);
obj.Alias=obj.GetDisplayName(obj.Rule);
obj.ClassName=obj.GetClassName(obj.Rule);
return obj;
},GetClassTag:function(_30){
var str=_30?_30.selectorText:"";
var _32=str.lastIndexOf(".");
if(_32==0){
return "ALL";
}
var _33=str.lastIndexOf(" ",_32);
return str.substring((_33+1),_32);
},GetDisplayName:function(_34){
if(!_34){
return "";
}
var _35=_34.selectorText;
var _36=_35.indexOf(".");
if(-1==_36){
_36=0;
}else{
_36+=1;
}
var _37=_35.indexOf(":");
if(-1==_37){
_37=_35.length;
}
return _35.substring(_36,_37);
},GetClassName:function(_38){
var str=_38.selectorText;
var _3a=str.lastIndexOf(".");
if(_3a==-1){
return "";
}
var _3b=str.indexOf(" ",_3a);
if(-1==_3b){
_3b=str.indexOf(":",_3a);
}
if(-1==_3b){
_3b=str.length;
}
return str.substring((_3a+1),_3b);
},CompareByTag:function(_3c){
if(this.Tag!=_3c.Tag){
if("ALL"==this.Tag.toUpperCase()){
return 1;
}else{
if("ALL"==_3c.Tag.toUpperCase()){
return -1;
}
}
}
if(this.Tag>_3c.Tag){
return 1;
}else{
if(this.Tag<_3c.Tag){
return -1;
}
}
return 0;
},CompareByTagSelectorText:function(_3d){
var res=this.CompareByTag(_3d);
if(0!=res){
return res;
}
if(this.selectorText>_3d.selectorText){
return 1;
}else{
if(this.selectorText<_3d.selectorText){
return -1;
}else{
return 0;
}
}
},CompareByTagAlias:function(_3f){
var res=this.CompareByTag(_3f);
if(0!=res){
return res;
}
if(this.Alias>_3f.Alias){
return 1;
}else{
if(this.Alias<_3f.Alias){
return -1;
}else{
return 0;
}
}
}};;RadEditorNamespace.HighLevelEvents={};
RadEditorNamespace.HighLevelEvents[RadEditorNamespace.RADEVENT_MODE_CHANGED]=RadEditorNamespace.RADEVENT_MODE_CHANGED;
RadEditorNamespace.HighLevelEvents[RadEditorNamespace.RADEVENT_SEL_CHANGED]=RadEditorNamespace.RADEVENT_SEL_CHANGED;
RadEditorNamespace.HighLevelEvents[RadEditorNamespace.RADEVENT_CONTEXTMENU]=RadEditorNamespace.RADEVENT_CONTEXTMENU;
RadEditorNamespace.HighLevelEvents[RadEditorNamespace.RADEVENT_SIZE_CHANGED]=RadEditorNamespace.RADEVENT_SIZE_CHANGED;
RadEditorNamespace.HighLevelEvents[RadEditorNamespace.RADEVENT_CALLBACK_STARTED]=RadEditorNamespace.RADEVENT_CALLBACK_STARTED;
RadEditorNamespace.HighLevelEvents[RadEditorNamespace.RADEVENT_DISPOSE]=RadEditorNamespace.RADEVENT_DISPOSE;
RadEditorNamespace.HighLevelEvents[RadEditorNamespace.RADEVENT_SUBMIT]=RadEditorNamespace.RADEVENT_SUBMIT;
RadEditorNamespace.BrowserEvents={};
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_KEYDOWN]="onkeydown";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_KEYUP]="onkeyup";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_PASTE]="onpaste";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_RESIZE_START]="onresizestart";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_RESIZE_END]="onresizeend";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_BEFORE_EDIT_FOCUS]="onbeforeeditfocus";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_DRAG_START]="ondragstart";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_DRAG_END]="ondragend";
RadEditorNamespace.BrowserEvents[RadEditorNamespace.RADEVENT_DROP]="ondrop";
RadEditor.prototype.DetachBrowserEvents=function(){
var _1=this.IsIE?this.Document.body:this.Document;
var _2=this.ContentAreaEventHandlers;
for(var _3 in _2){
if(typeof (_2[_3])=="function"){
RadEditorNamespace.Utils.DetachEventEx(_1,_3,_2[_3]);
}
}
this.ContentAreaEventHandlers=null;
};
RadEditor.prototype.AttachEventHandler=function(_4,_5){
if(RadEditorNamespace.HighLevelEvents[_4]){
var _6=this.Events[_4];
if(null==_6){
this.Events[_4]=[];
_6=this.Events[_4];
}
if(null!=_6){
if(null!=_5&&"function"==typeof (_5)){
_6[_6.length]=_5;
}
}
}else{
var _7=RadEditorNamespace.BrowserEvents[_4]?RadEditorNamespace.BrowserEvents[_4]:_4;
var _8=this.IsIE?this.Document.body:this.Document;
this.ContentAreaEventHandlers[_7]=_5;
RadEditorNamespace.Utils.AttachEventEx(_8,_7,_5);
}
};
RadEditor.prototype.DetachEventHandler=function(_9,_a){
if(RadEditorNamespace.HighLevelEvents[_9]){
var _b=this.Events[_9];
RadEditorNamespace.Utils.ArrayRemove(_b,_a);
}else{
var _c=this.IsIE?this.Document.body:this.Document;
var _d=RadEditorNamespace.BrowserEvents[_9]?RadEditorNamespace.BrowserEvents[_9]:_9;
RadEditorNamespace.Utils.DetachEventEx(_c,_d,_a);
}
};
RadEditor.prototype.FireEvent=function(_e,e){
var _10=this.Events[_e];
if(null!=_10){
for(var i=0;i<_10.length;i++){
_10[i](this,e);
}
}
};
RadEditor.prototype.IsShortCutHit=function(e){
return this.KeyboardManager.HitTest(e.keyCode,e.ctrlKey,(null!=e.ctrlLeft?e.ctrlLeft:e.ctrlKey),e.shiftKey,(null!=e.shiftLeft?e.shiftLeft:e.shiftKey),e.altKey,(null!=e.altLeft?e.altLeft:e.altKey));
};
RadEditor.prototype.IsCursorMovingKey=function(_13){
if(_13>=33&&_13<=40){
return true;
}
return false;
};
RadEditor.prototype.SaveTypedContent=function(_14,_15){
if(this.PendingTextTypeCmd){
this.PendingTextTypeCmd.Update();
}
if(true!=_14){
this.PendingTextTypeCmd=null;
}
};
RadEditor.prototype.InitRadEvents=function(){
var _16=this;
var _17=_16.Fire;
_16.Fire=function(_18,_19){
if(_18!="Copy"){
_16.SaveTypedContent(true,"editor.Fire "+_18+" executing");
}
_17.call(_16,_18,_19);
try{
if(_18=="Undo"&&!_16.IsIE&&RadEditorNamespace.Utils.Trim(_16.ContentArea.innerHTML.toLowerCase())=="<br>"){
_16.Document.body.innerHTML="<br>";
}
}
catch(e){
}
};
_16.AttachEventHandler("onmousedown",function(){
_16.SaveTypedContent(false,"Saving typed content onmousedown");
});
_16.AttachEventHandler("onclick",function(e){
_16.SaveTypedContent(false,"Saving typed content because of oclick (somewhere else)");
if(_16.GetSelectionHtml()){
_16.PendingTextTypeCmd=RadEditorNamespace.RadTextTypeCommand.New(_16.Localization["Typing"],_16.ContentWindow);
_16.ExecuteCommand(_16.PendingTextTypeCmd,false);
}
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_SEL_CHANGED,function(_1b,e){
_1b.OnSelectionChanged(e);
});
this.AttachEventHandler("onmouseup",function(e){
_16.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
});
this.AttachEventHandler("oncontextmenu",function(e){
_16.FireEvent(RadEditorNamespace.RADEVENT_CONTEXTMENU,e);
if(_16.IsIE&&true==e.cancelBubble){
return false;
}
});
this.AttachEventHandler("ondrop",function(e){
_16.OnDrop(e);
});
this.AttachEventHandler("dragdrop",function(e){
_16.OnDrop(e);
});
RadEditorNamespace.Utils.AttachEventEx(document.body,"ondragend",function(e){
_16.OnDocumentDragEnd(e);
});
if(!this.IsIE){
_16.AttachEventHandler("onkeypress",function(e){
if(_16.OnKeyPressed(e)){
e.preventDefault();
return false;
}
});
_16.AttachEventHandler("onclick",function(e){
if(!_16.IsEditingEnabled()){
return;
}
if("off"==_16.Document["designMode"]){
window.setTimeout(function(){
_16.SetEditable(true);
_16.SetFocus();
},100);
}
});
}
this.AttachEventHandler(RadEditorNamespace.RADEVENT_KEYDOWN,function(e){
if(_16.IsOpera){
return _16.OnKeyDown(e);
}else{
_16.OnKeyDown(e);
}
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_KEYUP,function(e){
_16.OnKeyUp(e);
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_RESIZE_START,function(e){
_16.OnResizeStart(e);
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_RESIZE_END,function(e){
_16.OnResizeEnd(e);
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_BEFORE_EDIT_FOCUS,function(e){
_16.OnBeforeEditFocus(e);
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_DRAG_START,function(e){
_16.OnDragStart(e);
});
this.AttachEventHandler(RadEditorNamespace.RADEVENT_DRAG_END,function(e){
_16.OnDragEnd(e);
});
if(!this.IsIE7){
this.AttachEventHandler("onpaste",function(e){
return _16.OnPaste(e);
});
}
if(this.IsIE7){
this.AttachEventHandler("onbeforepaste",function(e){
_16.OnBeforePaste(e);
});
}
};
RadEditor.prototype.OnSelectionChanged=function(e){
this.SetToolState(this.Tools);
};
RadEditor.prototype.OnKeyDown=function(e){
var _2f=e.keyCode;
switch(_2f){
case RadEditorNamespace.KEY_DELETE:
if(this.IsOpera){
this.Document.execCommand("Delete");
this.ContentArea.contentEditable=true;
this.Document.designMode="on";
this.ContentArea.focus();
return RadEditorNamespace.Utils.CancelEvent(e);
}
case RadEditorNamespace.KEY_BACK:
this.ShortcutHit=false;
if(this.GetSelectionHtml()){
this.SaveTypedContent(false,"Saving typed content before allowing delete to proceed..");
this.PendingTextTypeCmd=RadEditorNamespace.RadTextTypeCommand.New(this.Localization["Typing"],this.ContentWindow);
this.HasDeleteExecuted=true;
}
if(this.IsIE){
var _30=this;
var _31=function(){
var _32=_30.GetSelectedElement();
if(_32&&_32.tagName=="EMBED"){
_32.setAttribute("hidden","true");
_32.setAttribute("id","FileToDelete");
window.setTimeout(function(){
var _33=_30.Document.getElementById("FileToDelete");
if(_33.parentNode&&_33.parentNode.removeChild){
_33.parentNode.removeChild(_33);
}
},100);
RadEditorNamespace.Utils.CancelEvent(e);
return true;
}
};
var _34=_31();
if(_34){
return false;
}
try{
var _35=_30.Document.selection.createRange();
var _36=null;
if(_35&&_35.duplicate){
_36=_35.duplicate();
}
if(RadEditorNamespace.KEY_BACK==e.keyCode){
_35.moveStart("character",-1);
}else{
_35.moveEnd("character",1);
}
if(_35.parentElement().tagName=="EMBED"){
_35.select();
_31();
if(_36&&_36.select){
_36.select();
}
}
}
catch(ex){
}
if(this.Document.selection&&this.Document.selection.createRange){
var _37=this.Document.selection.createRange();
if(_37.length>0){
var _38=_37(0);
_37.execCommand(RadEditorNamespace.RADCOMMAND_DELETE);
RadEditorNamespace.Utils.CancelEvent(e);
}
}
}
return;
}
var _39=RadEditorNamespace.Utils.GetEventSource(e);
if(this.KeyboardManager&&_39&&"INPUT"!=_39.tagName){
var _3a=this.IsShortCutHit(e);
if(null!=_3a&&_3a.Name==RadEditorNamespace.RADCOMMAND_COPY){
this.ShortcutHit=false;
return;
}
this.ShortcutHit=(null!=_3a);
if(this.ShortcutHit){
this.Fire(_3a.Name);
}else{
if(this.IsCursorMovingKey(e.keyCode)){
if(!this.HasCursorMoved){
this.SaveTypedContent(false,"Saving typed content before letting the cursor move");
}
this.HasCursorMoved=true;
return;
}
if(RadEditorNamespace.KEY_SPACE==e.keyCode||RadEditorNamespace.KEY_ENTER==e.keyCode||!RadEditorNamespace.Utils.IsSystemKey(e.keyCode)){
if(this.HasCursorMoved){
this.PendingTextTypeCmd=RadEditorNamespace.RadTextTypeCommand.New(this.Localization["Typing"],this.ContentWindow);
this.ExecuteCommand(this.PendingTextTypeCmd);
this.HasCursorMoved=false;
return;
}
if(!this.PendingTextTypeCmd){
this.PendingTextTypeCmd=RadEditorNamespace.RadTextTypeCommand.New(this.Localization["Typing"],this.ContentWindow);
this.ExecuteCommand(this.PendingTextTypeCmd);
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
}
}
if(this.ShortcutHit){
if(this.IsIE){
if(_3a.Name==RadEditorNamespace.RADCOMMAND_CUT||_3a.Name==RadEditorNamespace.RADCOMMAND_PASTE){
return;
}
e.keyCode=123;
e.returnValue=!this.ShortcutHit;
}
}
}
};
RadEditor.prototype.OnKeyUp=function(e){
if(this.HasDeleteExecuted){
this.ExecuteCommand(this.PendingTextTypeCmd);
this.PendingTextTypeCmd=null;
this.HasDeleteExecuted=false;
return;
}
if(this.PendingTextTypeCmd){
return;
}
if(this.PendingCommand){
this.ExecuteCommand(this.PendingCommand);
this.PendingCommand=null;
}
if(this.ShortcutHit){
return false;
}
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
};
RadEditor.prototype.OnKeyPressed=function(e){
if(this.PendingTextTypeCmd){
}else{
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
if(this.ShortcutHit&&!this.PendingCommand){
return true;
}
};
RadEditorNamespace.CleanPastedContent=function(_3d,_3e){
var _3f=_3e;
if(_3d.ClearPasteFormatting==RadEditorNamespace.CLEAR_PASTE_FORMATTING_NONE){
if((_3e.match(/style="[^"]*?mso[^"]*?"/ig)||_3e.match(/class="?[^"]*?mso[^"]*?"?/ig))&&confirm(_3d.Localization["AskWordCleaning"])){
_3f=RadEditorNamespace.StripFormatting(_3e,"WORD");
}
}else{
if(_3d.ClearPasteFormatting&RadEditorNamespace.CLEAR_PASTE_FORMATTING_ALL){
_3f=RadEditorNamespace.StripFormatting(_3e,"ALL");
}else{
if(_3d.ClearPasteFormatting&RadEditorNamespace.CLEAR_PASTE_FORMATTING_WORD_REMOVE_ALL){
_3e=RadEditorNamespace.StripFormatting(_3e,"WORD_ALL");
}
if(_3d.ClearPasteFormatting&RadEditorNamespace.CLEAR_PASTE_FORMATTING_WORD_NO_FONTS){
_3e=RadEditorNamespace.StripFormatting(_3e,"WORD_NO_FONTS");
}
if(_3d.ClearPasteFormatting&RadEditorNamespace.CLEAR_PASTE_FORMATTING_WORD){
_3e=RadEditorNamespace.StripFormatting(_3e,"WORD");
}
if(_3d.ClearPasteFormatting&RadEditorNamespace.CLEAR_PASTE_FORMATTING_CSS){
_3e=RadEditorNamespace.StripFormatting(_3e,"CSS");
}
if(_3d.ClearPasteFormatting&RadEditorNamespace.CLEAR_PASTE_FORMATTING_FONT){
_3e=RadEditorNamespace.StripFormatting(_3e,"FONT");
}
if(_3d.ClearPasteFormatting&RadEditorNamespace.CLEAR_PASTE_FORMATTING_SPAN){
_3e=RadEditorNamespace.StripFormatting(_3e,"SPAN");
}
_3f=_3e;
}
}
return _3f;
};
RadEditorNamespace.InsertPastedContent=function(_40,_41,_42,_43,_44){
var _45=RadEditorNamespace.CleanPastedContent(_41,_42);
if(_40){
_40.Select();
}
if(_45){
window.setTimeout(function(){
if(_44&&_40){
_40.Select();
}
_41.PasteHtml(_45);
},5);
return RadEditorNamespace.Utils.CancelEvent(_43);
}
return true;
};
RadEditor.prototype.OnPaste=function(_46){
if(this.ClearPasteFormatting==RadEditorNamespace.CLEAR_PASTE_FORMATTING_NONE_SUPRESS_MESSAGE){
return;
}
var _47=this.CreateRestorePoint();
var _48=this.GetSelectionHtml()?true:false;
var _49=this.GetClipboardAsHtml();
return RadEditorNamespace.InsertPastedContent(_47,this,_49,_46,_48);
};
RadEditor.prototype.OnBeforePaste=function(_4a){
if(_4a){
if(this.ToolsUpdate){
return;
}
if(this.ClearPasteFormatting==RadEditorNamespace.CLEAR_PASTE_FORMATTING_NONE_SUPRESS_MESSAGE){
return;
}
var _4b=RadEditorNamespace.RadGenericCommand.New("Paste",this.ContentWindow);
var _4c=this.Document.getElementById("tmpPasteIE");
if(_4c){
_4c.removeAttribute("id");
return false;
}
var _4d=this.Document.selection.createRange();
var _4e="tmpPasteIE"+(new Date()-100);
if(_4d.pasteHTML){
_4d.pasteHTML("&nbsp;<font id='"+_4e+"'>&nbsp;</font>");
}
var _4f=this.PasteContainer;
_4f.contentEditable=true;
_4f.innerHTML="";
_4f.setActive();
var _50=this;
window.setTimeout(function(){
var _51=_4f.innerHTML;
var _52=RadEditorNamespace.CleanPastedContent(_50,_51);
var _53=_50.Document.getElementById(_4e);
_50.SetActive(true);
_50.PendingCommand=null;
var _54=_50.Document.body.createTextRange();
_54.moveToElementText(_53);
_54.moveStart("character",-1);
_54.select();
_54.pasteHTML(_52);
_50.ExecuteCommand(_4b);
_50.SetActive(true);
_50.SetFocus(true);
},0);
}else{
}
};
RadEditor.prototype.OnResizeStart=function(e){
var _56=this.GetSelectedElement();
if(_56==e.srcElement){
this.PendingResizeCmd=RadEditorNamespace.RadGenericCommand.New(this.Localization[RadEditorNamespace.RADCOMMAND_RESIZE],this.ContentWindow);
}
};
RadEditor.prototype.OnResizeEnd=function(e){
if(this.PendingResizeCmd){
this.ExecuteCommand(this.PendingResizeCmd);
}
};
RadEditor.prototype.OnBeforeEditFocus=function(e){
if(e&&e.srcElement&&"BODY"!=e.srcElement.tagName){
if(null!=(this.TargetEditElement=this.GetSelectedElement())){
this.oldEditValue=this.TargetEditElement.value;
}
this.StartEditElementText=(null!=this.TargetEditElement&&null!=this.oldEditValue);
}else{
if(this.StartEditElementText){
}
}
};
RadEditor.prototype.OnDragStart=function(e){
this.PendingMoveCommand=RadEditorNamespace.RadGenericCommand.New(this.Localization[RadEditorNamespace.RADCOMMAND_MOVE],this.ContentArea);
this.startRange=null;
if(!e.ctrlKey&&!e.ctrlLeft){
this.startRange=this.ContentWindow.document.selection.createRange();
if(this.startRange.length){
var rng=this.ContentWindow.document.body.createTextRange();
var _5b=this.startRange.item(0);
if("IMG"==_5b.tagName&&"A"==_5b.parentNode.tagName){
_5b=_5b.parentNode;
}
rng.moveToElementText(_5b);
this.startRange=rng;
}
}
};
RadEditor.prototype.OnDragEnd=function(e){
if(this.PendingMoveCommand){
if(this.startRange){
var _5d=this.ContentWindow.document.selection.createRange();
if(_5d.length){
var rng=this.ContentWindow.document.body.createTextRange();
rng.moveToElementText(_5d.item(0));
_5d=rng;
}
try{
if(_5d.compareEndPoints&&0!=_5d.compareEndPoints("StartToStart",this.startRange)&&0!=_5d.compareEndPoints("EndToEnd",this.startRange)){
this.startRange.execCommand("Delete",false,null);
}
}
catch(e){
}
}
this.ExecuteCommand(this.PendingMoveCommand);
this.PendingMoveCommand=null;
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
}
};
RadEditor.prototype.OnDrop=function(e){
this.SaveTypedContent(false,"on drop operation executing");
var _60=this.Localization[RadEditorNamespace.RADEVENT_DROP]||"Drop external content";
this.PendingDockCommand=RadEditorNamespace.RadGenericCommand.New(_60,this.ContentWindow);
};
RadEditor.prototype.OnDocumentDragEnd=function(e){
if(!this.PendingDockCommand){
return;
}
this.ExecuteCommand(this.PendingDockCommand);
this.PendingDockCommand=null;
this.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED,null);
};;RadEditorNamespace.RadEditorFiltersManager={New:function(){
var _1={};
RadEditorNamespace.Utils.ExtendObject(_1,this);
_1.Filters=[];
_1.IsEnabled=true;
return _1;
},EnableDomFilters:function(_2){
for(var i=0;i<this.Filters.length;i++){
var _4=this.Filters[i];
if(_4.IsDom){
_4.IsEnabled=_2;
}
}
},SetEnabled:function(_5){
this.IsEnabled=_5;
},Clear:function(){
this.Filters=[];
},Add:function(_6){
this.Filters[this.Filters.length]=_6;
},AddAt:function(_7,_8){
this.Filters.splice(_8,0,_7);
},RemoveAt:function(_9){
return this.Filters.splice(_9,1);
},GetFilterAt:function(_a){
return this.Filters[_a];
},GetFilterByName:function(_b){
for(var i=0;i<this.Filters.length;i++){
var _d=this.Filters[i];
if(_d&&_b==_d.Name){
return _d;
}
}
},GetDesignContent:function(_e){
if(!this.IsEnabled){
return _e;
}
var _f=_e;
for(var i=0;i<this.Filters.length;i++){
var _11=this.Filters[i];
if((false!=_11.IsEnabled)&&_11.GetDesignContent){
_f=_11.GetDesignContent(_f);
}
}
return _f;
},GetHtmlContent:function(_12){
if(!this.IsEnabled){
return _12;
}
var _13=_12;
for(var i=0;i<this.Filters.length;i++){
var _15=this.Filters[i];
if((false!=_15.IsEnabled)&&_15.GetHtmlContent){
_13=_15.GetHtmlContent(_13);
}
}
return _13;
},GetPreviewContent:function(_16){
if(!this.IsEnabled){
return _16;
}
var _17=_16;
for(var i=0;i<this.Filters.length;i++){
var _19=this.Filters[i];
if((false!=_19.IsEnabled)&&_19.GetPreviewContent){
_17=_19.GetPreviewContent(_17);
}
}
return _17;
}};
RadEditorNamespace.RadEditorMozillaFlashStart=function(_1a){
this.MaintainPathsPartOne=function(_1b){
_1b=_1b.replace(new RegExp("<embed(\\s([^>])*?)?>","ig"),"<img isflash=\"true\" $1 />");
return _1b;
};
this.GetDesignContent=function(_1c){
window.setTimeout(function(){
var _1d=_1a.ContentArea.getElementsByTagName("IMG");
for(var i=0;i<_1d.length;i++){
var _1f=_1d[i];
var _20=_1f.getAttribute("isflash");
if(_20!=null){
_1f.setAttribute("flashSrc",_1f.getAttribute("src"));
_1f.setAttribute("src",_1a.GetImageUrl("FlashManager.gif"));
}
}
},10);
return this.MaintainPathsPartOne(_1c);
};
};
RadEditorNamespace.RadEditorMozillaFlashEnd=function(_21){
this.MaintainPathsPartTwo=function(_22){
var _23=_22.getElementsByTagName("IMG");
for(var i=0;i<_23.length;i++){
var _25=_23[i];
var _26=_25.getAttribute("isflash");
if(_26!=null){
var _27=_25.getAttribute("flashSrc");
var _28=RadEditorNamespace.Utils.GetOuterHtml(_25);
_28=_28.replace("<img","<embed");
var _29=_25.ownerDocument.createElement("DIV");
_29.innerHTML=_28;
newNode=_29.firstChild;
newNode.src=_27;
newNode.removeAttribute("flashSrc");
newNode.removeAttribute("isflash");
var _2a=_25.parentNode;
_2a.insertBefore(newNode,_25);
_2a.removeChild(_25);
i--;
}
}
return _22;
};
this.GetHtmlContent=function(_2b){
return _2b;
};
};
RadEditorNamespace.RadMozillaContentFilter=function(){
this.GetDesignContent=function(_2c){
var _2d=_2c.replace(new RegExp("<strong(\\s([^>])*?)?>","ig"),"<b$1>");
_2d=_2d.replace(new RegExp("</strong(\\s([^>])*?)?>","ig"),"</b$1>");
_2d=_2d.replace(new RegExp("<em(\\s([^>])*?)?>","ig"),"<i$1>");
_2d=_2d.replace(new RegExp("</em(\\s([^>])*?)?>","ig"),"</i$1>");
return _2d;
};
this.GetHtmlContent=function(_2e){
var _2f=_2e.replace(new RegExp("<b(\\s([^>])*?)?>","ig"),"<strong$1>");
_2f=_2f.replace(new RegExp("</b(\\s([^>])*?)?>","ig"),"</strong$1>");
_2f=_2f.replace(new RegExp("<i(\\s([^>])*?)?>","ig"),"<em$1>");
_2f=_2f.replace(new RegExp("</i(\\s([^>])*?)?>","ig"),"</em$1>");
return _2f;
};
};
function RadSafariStripFilter(){
this.Name="RadSafariStripFilter";
this.GetDesignContent=null;
this.GetPreviewContent=null;
this.GetHtmlContent=function(_30){
var _31=_30;
var re=new RegExp(" class=\"khtml-block-placeholder\"","ig");
_31=_31.replace(re,"");
_31=_31.replace(new RegExp(" class=\"khtml-block-placeholder\"","ig"),"");
_31=_31.replace(new RegExp(" class=\"Apple-style-span\"","ig"),"");
return _31;
};
}
function RadTagNameCaseFilter(){
this.Name="RadTagNameCaseFilter";
this.Description="This filter converts the upper case tagnames in IE to XHTML lowercase tagnames";
this.GetDesignContent=null;
this.GetPreviewContent=null;
this.GetHtmlContent=function(_33){
if(!document.all){
return _33;
}
var _34=/<\/?([A-Z][A-Z0-9]*)/g;
str=_33;
var _35=null;
var _36=new RadEditorNamespace.Utils.StringBuilder("");
var _37=0;
try{
while((_35=_34.exec(str))!=null){
var _38=_35[0];
var _39=str.substring(_37,_34.lastIndex-_38.length);
_36.Append(_39);
_36.Append(_38.toLowerCase());
_37=_34.lastIndex;
}
if(_37<str.length){
_39=str.substring(_37);
_36.Append(_39);
}
}
catch(e){
}
if(_36){
return _36.ToString();
}else{
return _33;
}
};
}
function RadCleanAnchorsFilter(){
this.GetHtmlContent=function(_3a){
var _3b=_3a;
var _3c=document.location.href;
var re=new RegExp("(<A[^<>]*?(href)\\s*=\\s*['\"])("+_3c+")(\\#[^'\"]*?['\"][^>]*?>)","ig");
_3b=_3b.replace(re,"$1$4");
return _3b;
};
}
function RadStripPathFilter(_3e,_3f){
this.Name="RadStripPathFilter";
this.Description="This filter strips an image or an anchor path";
this.TagName=_3e;
this.PathToStrip=_3f;
this.GetHtmlContent=function(_40){
if(!document.all){
return _40;
}else{
var _41=StripAbsolutePaths(_40,this.TagName,this.PathToStrip);
return _41;
}
return _40;
};
function StripAbsolutePaths(_42,_43,_44){
var _45=function(_46,_47,_48,_49){
_49=RadEditorNamespace.Utils.EscapeRegexSpecialChars(_49);
var _4a=new RegExp("(<"+_47+"[^<>]*?("+_48+")\\s*=\\s*['\"])("+_49+")([^'\"]*?['\"][^>]*?>)","ig");
return _46.replace(_4a,"$1$4");
};
var _4b=function(_4c,_4d,_4e){
if(_4d=="A"){
var _4f=document.location.href;
_4f=RadEditorNamespace.Utils.EscapeRegexSpecialChars(_4f);
var re=new RegExp("(<A[^<>]*?(href)\\s*=\\s*['\"])("+_4f+")(\\#[^'\"]*?['\"][^>]*?>)","ig");
_4c=_4c.replace(re,"$1$4");
}
var _51=_4c;
var _52=(_4d=="A"?"href":"src");
for(var i=0;i<_4e.length;i++){
if(_4e[i]){
_51=_45(_51,_4d,_52,_4e[i]);
}
}
return _51;
};
var _54=[];
if(_44){
if(_44.indexOf(" ")>-1){
_54=_44.split(" ");
}else{
_54[0]=_44;
}
}else{
var _55=window.location;
_54[0]=_55.protocol+"//"+_55.host+(_55.port?":"+_55.port:"");
var _56=_55.pathname;
var _57=_56.lastIndexOf("/");
if(_57>-1){
_54[1]=_56.substr(0,_57+1);
}
}
return _4b(_42,_43,_54);
}
}
function RadStripScriptsFilter(){
this.Name="RadStripScriptTagsFilter";
this.Description="Strips SCRIPT tags in the editor content";
this.GetPreviewContent=null;
this.PerformStripping=function(_58){
var re=new RegExp("<(SCRIPT)([^>]*)/>","ig");
_58=_58.replace(re,"");
re=new RegExp("<(SCRIPT)([^>]*)>[\\s\\S]*?</(SCRIPT)([^>]*)>","ig");
_58=_58.replace(re,"");
return _58;
};
this.GetHtmlContent=function(_5a){
return this.PerformStripping(_5a);
};
this.GetDesignContent=function(_5b){
return this.PerformStripping(_5b);
};
this.GetPreviewContent=function(_5c){
return this.PerformStripping(_5c);
};
}
function RadFont2SpanFilter(){
this.IsDom=true;
this.Name="RadFont2SpanFilter";
this.Description="Converts FONTs to SPANs";
this.GetPreviewContent=null;
this.FontSizes=["8pt","10pt","12pt","14pt","18pt","24pt","36pt"];
this.FontSizesRev=[];
for(var i=0;i<this.FontSizes.length;i++){
this.FontSizesRev[parseInt(this.FontSizes[i])]=i;
}
this.GetHtmlContent=function(_5e){
var _5f=document.createElement("SPAN");
RadEditorNamespace.SetElementInnerHTML(_5f,_5e);
var _60=document.createElement("SPAN");
var _61,_62,_63;
var _64=_5f.getElementsByTagName("FONT");
while(_64.length>0){
_62=_64[0];
_63=_62.parentNode;
_61=_60.cloneNode(false);
_61.style.cssText=_62.style.cssText;
if(_62.className){
_61.className=_62.className;
}
if(_62.face){
_61.style.fontFamily=_62.face;
}
var _65=0;
if(_62.style.fontSize){
_61.style.fontSize=_62.style.fontSize;
}else{
if(!isNaN(_65=parseInt(_62.size))){
try{
_61.style.fontSize=this.FontSizes[_65-1];
}
catch(ex){
_61.style.fontSize=this.FontSizes[3];
}
}
}
if(_62.color){
_61.style.color=_62.color;
}
if(document.all&&_62.innerHTML==" "){
_61.innerText=_62.innerHTML;
}else{
RadEditorNamespace.SetElementInnerHTML(_61,_62.innerHTML);
}
_63.replaceChild(_61,_62);
_64=_5f.getElementsByTagName("FONT");
}
return _5f.innerHTML;
};
this.GetDesignContent=function(_66){
var _67=document.createElement("DIV");
RadEditorNamespace.SetElementInnerHTML(_67,_66);
var _68=document.createElement("FONT");
var _69,_6a,_6b;
var _6c=_67.getElementsByTagName("SPAN");
while(_6c.length>0){
_69=_6c[0];
_6b=_69.parentNode;
_6a=_68.cloneNode(false);
_6a.style.cssText=_69.style.cssText;
if(_69.className){
_6a.className=_69.className;
}
if(_69.style.fontFamily){
_6a.face=_69.style.fontFamily;
if(document.all){
_6a.style.removeAttribute("fontFamily");
}else{
_6a.style.fontFamily=null;
}
}
if(_69.style.fontSize){
var _6d=3;
var _6e=this.FontSizesRev[parseInt(_69.style.fontSize)];
if(typeof (_6e)!="undefined"){
_6a.size=_6e+1;
RadEditorNamespace.Utils.RemoveElementStyleAttribute(_6a,"fontSize");
}
}
if(_69.style.color){
if(document.all){
_6a.color=_69.style.color;
_6a.style.removeAttribute("color");
}
}
if(document.all&&_69.innerHTML==" "){
_6a.innerText=_69.innerHTML;
}else{
RadEditorNamespace.SetElementInnerHTML(_6a,_69.innerHTML);
}
_6b.replaceChild(_6a,_69);
_6c=_67.getElementsByTagName("SPAN");
}
return _67.innerHTML;
};
}
function RadConvertToXhtmlFilter(){
this.Name="RadConvertToXhtmlFilter";
this.IsDom=true;
this.Description="Converts HTML to XHTML";
this.GetHtmlContent=function(_6f){
if(!_6f){
return "";
}
this.UniqueIds={};
var _70=document.createElement("DIV");
RadEditorNamespace.SetElementInnerHTML(_70,_6f);
var _71=this.GetXhtml(_70);
var _72=_71.substring(5,_71.length-6);
return _72;
};
}
RadConvertToXhtmlFilter.prototype.GetXhtml=function(_73){
var sb=new RadEditorNamespace.Utils.StringBuilder();
this.AppendNodeXhtml(_73,sb);
return sb.ToString();
};
RadConvertToXhtmlFilter.prototype.ConvertAttribute=function(s){
return String(s).replace(/\&/g,"&amp;").replace(/</g,"&lt;").replace(/\"/g,"&quot;");
};
RadConvertToXhtmlFilter.prototype.GetAttributeValue=function(_76,_77,sb){
var _79=_76.nodeName;
var _7a=_76.nodeValue;
if(document.all&&_79=="type"||_79=="value"){
if(!_7a){
return;
}
}else{
if(!_76.specified){
return;
}
}
var _7b=_77.tagName;
if(_79!="style"){
if(!_7a){
return;
}
if(!isNaN(_7a)){
_7a=_77.getAttribute(_79);
}
sb.Append(" "+(_76.expando?_79:_79.toLowerCase())+"=\""+this.ConvertAttribute(_7a)+"\"");
}else{
sb.Append(" style=\""+this.ConvertAttribute(_77.style.cssText)+"\"");
}
};
RadConvertToXhtmlFilter.prototype.AppendNodeXhtml=function(_7c,sb){
if(_7c.uniqueID){
if(this.UniqueIds[_7c.uniqueID]){
return;
}else{
this.UniqueIds[_7c.uniqueID]=true;
}
}
switch(_7c.nodeType){
case 1:
if(_7c.tagName.charAt(0)=="/"){
return;
}
if(_7c.nodeName=="!"){
sb.Append(_7c.text);
break;
}
var _7e=_7c.nodeName;
if(_7c.scopeName){
if(_7c.scopeName=="HTML"){
_7e=_7e.toLowerCase();
}else{
_7e=_7c.scopeName+":"+_7e;
}
}else{
_7e=_7e.toLowerCase();
}
if("td"==_7e||"tr"==_7e||"tbody"==_7e||"table"==_7e){
sb.Append("\n");
}
sb.Append("<"+_7e);
if(document.all){
if("img"==_7e){
var _7f=document.createElement("IMG");
_7f.mergeAttributes(_7c);
if(_7f.width){
sb.Append(" width=\""+_7f.width+"\"");
}
if(_7f.height){
sb.Append(" height=\""+_7f.height+"\"");
}
if(_7f.getAttribute("alt").length==0){
sb.Append(" alt=\""+_7f.getAttribute("alt")+"\"");
}
}else{
if("area"==_7e){
if(_7c.shape){
sb.Append(" shape=\""+_7c.shape+"\"");
}
if(_7c.coords){
sb.Append(" coords=\""+_7c.getAttribute("coords")+"\"");
}
if(_7c.href){
var _80=_7c.href.replace("about:blank","");
_80=_80.replace("about:","");
sb.Append(" href=\""+_80+"\"");
_7c.removeAttribute("href",0);
}
}
}
}
var _81=_7c.attributes;
var l=_81.length;
for(var i=0;i<l;i++){
this.GetAttributeValue(_81[i],_7c,sb);
}
if(_7c.canHaveChildren||_7c.hasChildNodes()){
sb.Append(">");
var cs=_7c.childNodes;
l=cs.length;
for(var i=0;i<l;i++){
this.AppendNodeXhtml(cs[i],sb);
}
sb.Append("</"+_7e+">");
}else{
if(_7e=="script"){
sb.Append(">"+_7c.text+"</"+_7e+">");
}else{
if(_7e=="textarea"){
sb.Append(">"+_7c.value+"</"+_7e+">");
}else{
if(_7e=="title"||_7e=="style"||_7e=="comment"||_7e=="noscript"){
sb.Append(">"+_7c.innerHTML+"</"+_7e+">");
}else{
if(_7e=="iframe"){
sb.Append("></iframe>");
}else{
sb.Append(" />");
}
}
}
}
}
break;
case 3:
sb.Append(String(_7c.nodeValue).replace(/\&/g,"&amp;").replace(/</g,"&lt;"));
break;
case 4:
sb.Append("<![CDA"+"TA[\n"+_7c.nodeValue+"\n]"+"]>");
break;
case 8:
var _85=_7c.text;
if(!_7c.text&&_7c.nodeValue){
_85="<!--"+_7c.nodeValue+"-->";
}
sb.Append(_85);
if(/(^<\?xml)|(^<\!DOCTYPE)/.test(_85)){
sb.Append("\n");
}
break;
}
};;RadEditorNamespace.RadKeyboardManager={New:function(_1){
var _2={};
_2.Shortcuts=[];
RadEditorNamespace.Utils.ExtendObject(_2,this);
return _2;
},AddShortcut:function(_3,_4){
var rs=RadEditorNamespace.RadShortcut.New(_3,_4);
rs.HashValue=this.GetShortcutHashValue(rs);
this.Shortcuts[rs.HashValue]=rs;
},RemoveShortcut:function(_6){
var _7=this.FindByName(_6);
if(_7){
this.Shortcuts[_7.HashValue]=null;
}
},SetShortcut:function(_8,_9){
this.RemoveShortcut(_8);
this.AddShortcut(_8,_9);
},HitTest:function(_a,_b,_c,_d,_e,_f,_10){
var _11=this.GetHashValue(_a,_b,_c,_d,_e,_f,_10);
return this.Shortcuts[_11];
},GetHashValue:function(_12,_13,_14,_15,_16,_17,_18){
var _19=_12&65535;
var _1a=0;
_1a|=(_13?RadEditorNamespace.KF_CTRL:0);
_1a|=(_15?RadEditorNamespace.KF_SHIFT:0);
_1a|=(_17?RadEditorNamespace.KF_ALT:0);
_19|=(_1a<<16);
return _19;
},GetShortcutHashValue:function(_1b){
return this.GetHashValue(_1b.KeyCode,_1b.CtrlKey,_1b.LeftCtrlKey,_1b.ShiftKey,_1b.LeftShiftKey,_1b.AltKey,_1b.LeftAltKey);
},FindByName:function(_1c){
var _1d;
for(var _1e in this.Shortcuts){
_1d=this.Shortcuts[_1e];
if(null!=_1d&&_1d.Name==_1c){
return _1d;
}
}
return null;
}};
RadEditorNamespace.RadShortcut={New:function(_1f,_20){
var obj={};
RadEditorNamespace.Utils.ExtendObject(obj,this);
obj.Name=_1f;
obj.SetShortcut(_20);
return obj;
},CtrlKey:false,LeftCtrlKey:false,ShiftKey:false,LeftShiftKey:false,AltKey:false,LeftAltKey:false,KeyCode:0,SetShortcut:function(_22){
this.ParseShortcutString(_22);
},ParseShortcutString:function(_23){
if("string"==typeof (_23)){
this.CtrlKey=false;
this.LeftCtrlKey=false;
this.ShiftKey=false;
this.LeftShiftKey=false;
this.AltKey=false;
this.LeftAltKey=false;
this.KeyCode=0;
_23=_23.replace(/\s*/gi,"");
_23=_23.replace(/\+\+/gi,"+PLUS");
var _24=_23.split("+");
var _25="";
for(var i=0;i<_24.length;i++){
_25=_24[i].toUpperCase();
switch(_25){
case "LCTRL":
this.LeftCtrlKey=true;
case "CTRL":
this.CtrlKey=true;
break;
case "LSHIFT":
this.LeftShiftKey=true;
case "SHIFT":
this.ShiftKey=true;
break;
case "LALT":
this.LeftAltKey=true;
case "ALT":
this.AltKey=true;
break;
case "F1":
this.KeyCode=RadEditorNamespace.KEY_F1;
break;
case "F2":
this.KeyCode=RadEditorNamespace.KEY_F2;
break;
case "F3":
this.KeyCode=RadEditorNamespace.KEY_F3;
break;
case "F4":
this.KeyCode=RadEditorNamespace.KEY_F4;
break;
case "F5":
this.KeyCode=RadEditorNamespace.KEY_F5;
break;
case "F6":
this.KeyCode=RadEditorNamespace.KEY_F6;
break;
case "F7":
this.KeyCode=RadEditorNamespace.KEY_F7;
break;
case "F8":
this.KeyCode=RadEditorNamespace.KEY_F8;
break;
case "F9":
this.KeyCode=RadEditorNamespace.KEY_F9;
break;
case "F10":
this.KeyCode=RadEditorNamespace.KEY_F10;
break;
case "F11":
this.KeyCode=RadEditorNamespace.KEY_F11;
break;
case "F12":
this.KeyCode=RadEditorNamespace.KEY_F12;
break;
case "ENTER":
this.KeyCode=RadEditorNamespace.KEY_ENTER;
break;
case "HOME":
this.KeyCode=RadEditorNamespace.KEY_HOME;
break;
case "END":
this.KeyCode=RadEditorNamespace.KEY_END;
break;
case "LEFT":
this.KeyCode=RadEditorNamespace.KEY_LEFT;
break;
case "RIGHT":
this.KeyCode=RadEditorNamespace.KEY_RIGHT;
break;
case "UP":
this.KeyCode=RadEditorNamespace.KEY_UP;
break;
case "DOWN":
this.KeyCode=RadEditorNamespace.KEY_DOWN;
break;
case "PAGEUP":
this.KeyCode=RadEditorNamespace.KEY_PAGEUP;
break;
case "PAGEDOWN":
this.KeyCode=RadEditorNamespace.KEY_PAGEDOWN;
break;
case "SPACE":
this.KeyCode=RadEditorNamespace.KEY_SPACE;
break;
case "TAB":
this.KeyCode=RadEditorNamespace.KEY_TAB;
break;
case "BACK":
this.KeyCode=RadEditorNamespace.KEY_BACK;
break;
case "CONTEXT":
this.KeyCode=RadEditorNamespace.KEY_CONTEXT_MENU;
break;
case "ESCAPE":
case "ESC":
this.KeyCode=RadEditorNamespace.KEY_ESC;
break;
case "DELETE":
case "DEL":
this.KeyCode=RadEditorNamespace.KEY_DELETE;
break;
case "INSERT":
case "INS":
this.KeyCode=RadEditorNamespace.KEY_INSERT;
break;
case "PLUS":
this.KeyCode="+".charCodeAt(0);
break;
default:
this.KeyCode=_25.charCodeAt(0);
break;
}
}
}else{
throw {description:"Invalid shortcut string"};
}
}};;RadEditorNamespace.RadEditorPopup=function(){
this.Window=window;
this.IsIE=(document.all?true:false);
this.IsSafari=TelerikNamespace.Utils.DetectBrowser("safari");
this.ContextMenuClass="RadEContextMenu";
this.DropdownClass="RadEDropdownMenu";
this.LastDropdownOwner=null;
this.CurrentTopElement=null;
this.Popup=null;
this.PopupBody=null;
this.Disposed=false;
this.Create();
};
RadEditorNamespace.RadEditorPopup.prototype={Dispose:function(){
if(this.Disposed){
return;
}
if(this.Document.body){
this.Document.body.innerHTML="";
}
this.Popup=null;
this.PopupBody=null;
this.Document=null;
this.LastDropdownOwner=null;
this.CurrentTopElement=null;
this.Disposed=true;
},GetDocument:function(){
if(!this.Document){
this.Create();
}
return this.Document;
},CreatePopup:function(){
return this;
},SetClassName:function(_1){
this.DropdownClass=_1;
},IsVisible:function(){
if(this.Popup.isOpen!=null){
return this.Popup.isOpen;
}else{
if(this.Popup&&this.Popup.style){
return (parseInt(this.Popup.style.width)>0);
}
return false;
}
},CreateElement:function(_2){
return this.GetDocument().createElement(_2);
},SetTopElement:function(_3){
this.CurrentTopElement=_3;
},AddStyleSheet:function(_4){
TelerikNamespace.Utils.AddStyleSheet(_4,this.GetDocument());
},ShowContextMenu:function(e,_6,_7,_8){
var x=this.IsIE?e.screenX:e.clientX;
var y=this.IsIE?e.screenY:e.clientY;
x+=10;
y+=10;
this.OnBeforeShow(false);
if(this.Popup.show&&this.Popup.tagName!="IFRAME"){
this.Popup.show(x,y,_6,_7);
}else{
var _b=this.GetElementPosition(_8);
x+=_b.X;
y+=_b.Y;
if(_8&&_8.ownerDocument&&_8.ownerDocument.defaultView&&_8.ownerDocument.defaultView.frameElement){
y-=RadEditorNamespace.Utils.FindScrollPosY(_8.ownerDocument.defaultView.frameElement);
}
this.ShowPopupMozilla(x,y,_6,_7,false);
}
if(e&&e.preventDefault){
e.preventDefault();
}
return false;
},ShowDropdown:function(_c,_d,_e,_f,_10){
var x=0;
var y=_e?_e.offsetHeight:0;
this.OnBeforeShow(true,_c,_d);
if(this.Popup.show&&this.Popup.tagName!="IFRAME"){
if(!_f){
if(this.CurrentTopElement&&this.CurrentTopElement.parentNode){
this.CurrentTopElement.parentNode.style.overflow="hidden";
}
}
this.Popup.show(x,y,_c,_d,_e);
}else{
if(this.LastDropdownOwner==_e){
this.LastDropdownOwner=null;
if(!this.IsIE&&this.Popup&&parseInt(this.Popup.style.width)>0){
this.Hide();
}
return false;
}
this.LastDropdownOwner=_e;
var _13=this.GetElementPosition(_e);
x+=_13.X;
var _14=this.MozillaFindFixedParent(_e);
var _15=0;
if(_14){
if(document.body.scrollTop){
_15=document.body.scrollTop;
}
if(document.documentElement&&document.documentElement.scrollTop){
_15=document.documentElement.scrollTop;
}
}
y+=_15+_13.Y;
if(document!=_e.ownerDocument){
y-=_e.ownerDocument.body.scrollTop;
}
this.ShowPopupMozilla(x,y,_c+2,_d+2,_f);
}
return false;
},MozillaFindFixedParent:function(_16){
if(!_16){
return null;
}
do{
var _17=document.defaultView.getComputedStyle(_16,null);
if(_17&&"fixed"==_17.position){
return _16;
}
}while((_16=_16.parentNode)!=null&&_16.tagName!="BODY");
return null;
},OnBeforeShow:function(_18,_19,_1a){
var _1b=this.PopupBody;
if(_1b&&_1b.childNodes.length>0){
var _1c=_1b.childNodes;
for(var i=0;i<_1c.length;i++){
_1b.removeChild(_1c[i]);
}
}
var div=this.Document.createElement("DIV");
div.className="ContentElement";
if(this.CurrentTopElement){
div.appendChild(this.CurrentTopElement);
}
if(_18){
_1b.className=this.DropdownClass;
if(this.IsIE){
div.style.height=_1a;
div.style.width=_19;
div.style.overflow="auto";
}
}else{
_1b.className=this.ContextMenuClass;
div.style.overflow="hidden";
}
if(this.IsIE){
_1b.style.border="1px solid #777777";
}
_1b.appendChild(div);
},Create:function(){
if(this.Window.createPopup){
this.Popup=this.Window.createPopup();
this.Document=this.Popup.document;
this.PopupBody=this.Document.body;
}else{
this.CreatePopupMozilla();
}
},CreatePopupMozilla:function(){
var _1f=this.Window.document.createElement("iframe");
var _20=this.Window.document.createElement("div");
var _21=null;
if(this.IsSafari){
_21=_20;
_20.appendChild(_1f);
this.Window.document.body.appendChild(_20);
}else{
_21=_1f;
this.Window.document.body.appendChild(_1f);
_1f.src="about:blank";
}
_1f.frameBorder="0";
_1f.style.width="100%";
_1f.style.height="100%";
_21.style.position="absolute";
_21.style.zIndex=51200;
_21.style.width="0px";
_21.style.height="0px";
_21.className="RadEMozillaDropdownIframe";
var doc=_1f.contentWindow.document;
doc.open();
doc.writeln("<head><style></style></head><body></body>");
doc.close();
this.Popup=_21;
this.Document=_1f.contentWindow.document;
this.ContentWindow=_1f.contentWindow;
var _23=this.Document;
if(!this.Document.body){
oBody=_23.createElement("body");
_23.appendChild(oBody);
this.PopupBody=this.Document.getElementsByTagName("body")[0];
}else{
this.PopupBody=this.Document.body;
}
if(_23.getElementsByTagName("head").length<1){
var _24=_23.createElement("head");
_24.style.visibility="hidden";
this.PopupBody.parentNode.insertBefore(_24,this.PopupBody);
}
this.PopupBody.style.margin="0px";
this.PopupBody.style.padding="0px";
},ShowPopupMozilla:function(x,y,_27,_28,_29){
this.Popup.style.zIndex=51200;
this.Popup.style.left=x+"px";
this.Popup.style.top=y+"px";
_27=parseInt(_27)+"px";
_28=parseInt(_28)+"px";
this.Popup.width=_27;
this.Popup.height=_28;
this.Popup.style.width=_27;
this.Popup.style.height=_28;
borderWidth=(true==this.ShownAlready)?1:0;
this.ShownAlready=true;
this.Popup.style.border=borderWidth+"px solid black";
if(false==_29){
if(this.Popup.clientHeight>this.PopupBody.firstChild.scrollHeight&&this.PopupBody.firstChild.scrollHeight>0){
var _2a=this.PopupBody.firstChild.scrollHeight+"px";
this.Popup.height=_2a;
this.Popup.style.height=_2a;
}
this.Popup.style.overflow="hidden";
this.PopupBody.style.overflow="hidden";
var _2b=this.PopupBody.firstChild.scrollWidth+"px";
if(parseInt(_2b)>0){
this.Popup.width=_2b;
this.Popup.style.width=_2b;
}
}else{
var _2c=this;
var _2d=function(){
_2c.Popup.style.overflow="hidden";
if(_2c.Popup.clientHeight>=_2c.PopupBody.scrollHeight){
_2c.PopupBody.style.overflow="hidden";
}else{
if(_2c.PopupBody.firstChild){
_2c.PopupBody.firstChild.style.overflow="auto";
}
if(_2c.IsSafari){
_2c.PopupBody.style.overflow="scroll";
}
}
};
_2d();
if(!this.ResizeHandlerAttached&&_29!=false){
this.PopupBody.addEventListener("mousedown",function(e){
window.setTimeout(_2d,2000);
},true);
this.ResizeHandlerAttached=true;
}
}
if(this.IsSafari&&!this.SafariHandlerAttached){
var _2f=this.ContentWindow;
_2f.addEventListener("mousedown",function(e){
return RadEditorNamespace.Utils.CancelEvent(e);
},true);
this.SafariHandlerAttached=true;
}
},Hide:function(){
if(this.Popup.hide&&this.Popup.tagName!="IFRAME"){
this.Popup.hide();
}else{
this.LastDropdownOwner=null;
this.Popup.style.width="0px";
this.Popup.style.height="0px";
this.Popup.style.border="0px solid red";
this.PopupBody.innerHTML="";
}
},IsMozillaPopupVisible:function(e){
var _32=this.Popup;
if(!_32){
return false;
}
if(0==parseInt(_32.style.width)){
return false;
}
if((e.pageX<parseInt(_32.style.left))||(e.pageX>parseInt(_32.style.left)+parseInt(_32.style.width))||(e.pageY<parseInt(_32.style.top))||(e.pageY>parseInt(_32.style.top)+parseInt(_32.style.height))){
this.Hide();
}
},GetElementPosition:function(el){
var _34=el;
var c={X:0,Y:0};
while(el){
c.X+=el.offsetLeft;
c.Y+=el.offsetTop;
if(el.offsetParent==null&&el.ownerDocument.defaultView!=this.Window){
el=el.ownerDocument.defaultView.frameElement;
}else{
el=el.offsetParent;
}
}
if(window.opera){
return c;
}
try{
c.Y-=RadEditorNamespace.Utils.FindScrollPosY(_34);
}
catch(e){
}
return c;
},GetCoords:function(_36){
var _37=new Array(0,0);
if(_36.offsetParent){
while(_36.offsetParent){
_37[0]+=_36.offsetLeft;
_37[1]+=_36.offsetTop;
_36=_36.offsetParent;
if(_36==document.body){
_37[0]-=_36.offsetLeft;
_37[1]-=_36.offsetTop;
}
}
}
return _37;
}};
if(!window["RadEditorPopupInstance"]){
window["RadEditorPopupInstance"]=new RadEditorNamespace.RadEditorPopup();
}
if(window.addEventListener){
var RadEditorPopupGlobalHanlder=function(e){
window["RadEditorPopupInstance"].IsMozillaPopupVisible(e);
};
var attachedFrames=[];
function HasAttachedHandler(_39){
for(var i=0;i<attachedFrames.length;i++){
if(attachedFrames[i]==_39){
return true;
}
}
return false;
}
function mouseDownHandler(){
var _3b=window.frames;
for(var i=0;i<_3b.length;i++){
if(window["RadEditorPopupInstance"].ContentWindow==_3b[i]){
continue;
}else{
if(HasAttachedHandler(window["RadEditorPopupInstance"].ContentWindow)){
continue;
}
}
_3b[i].window.addEventListener("mousedown",RadEditorPopupGlobalHanlder,true);
attachedFrames[attachedFrames.length]=_3b[i].window;
}
if(window["RadEditorPopupInstance"].HasMozillaHandlerAttached){
return;
}
window["RadEditorPopupInstance"].HasMozillaHandlerAttached=true;
window.document.addEventListener("mousedown",RadEditorPopupGlobalHanlder,false);
}
window.addEventListener("load",mouseDownHandler,false);
mouseDownHandler();
}
RadEditorNamespace.Utils.AttachEventEx(window,"unload",function(){
var _3d=window["RadEditorPopupInstance"];
if(_3d&&_3d.Dispose){
_3d.Dispose();
}
});;RadEditorNamespace.RadCreateRestorePoint=function(_1){
if(!_1){
return null;
}
if(_1.document.all&&!window.opera){
return RadEditorNamespace.RadRestorePointIE.New(_1);
}else{
return RadEditorNamespace.RadRestorePointMoz.New(_1);
}
};
RadEditorNamespace.RadRestorePointIE={New:function(_2){
var _3={};
RadEditorNamespace.Utils.ExtendObject(_3,this);
_3.Document=_2.document;
_3.Update();
return _3;
},Update:function(){
this.HtmlText=this.Document.body.innerHTML;
var _4=this.Document.selection.createRange();
if(_4.length){
this.SourceIndex=_4.item(0).sourceIndex;
}else{
this.StartBookmark=_4.getBookmark();
}
},Restore:function(_5){
RadEditorNamespace.SetElementInnerHTML(this.Document.body,this.HtmlText);
this.Select(_5);
},Select:function(_6){
if(null!=this.SourceIndex){
var _7=this.Document.body.createControlRange();
_7.addElement(this.Document.all(this.SourceIndex));
_7.select();
}else{
if(null!=this.StartBookmark){
var _7=this.Document.body.createTextRange();
_7.moveToBookmark(this.StartBookmark);
_7.select();
if(true==_6&&_7.collapse){
_7.collapse();
}
}
}
}};
RadEditorNamespace.RadRestorePointMoz={New:function(_8){
var _9={};
RadEditorNamespace.Utils.ExtendObject(_9,this);
_9.Window=_8;
_9.Update();
return _9;
},Restore:function(){
try{
this.Window.document.body.innerHTML=this.HtmlText;
this.Select();
}
catch(e){
}
},Select:function(){
try{
this.Window.focus();
this.MoveToBookmark(this.Window.getSelection(),this.Bookmark);
}
catch(ex){
}
},BookmarkSelection:function(_a){
return {anchorNodeBookmark:RadEditorNamespace.RadNodeBookmark.New(this.Window,_a.anchorNode),anchorOffset:_a.anchorOffset,focusNodeBookmark:RadEditorNamespace.RadNodeBookmark.New(this.Window,_a.focusNode),focusOffset:_a.focusOffset,isCollapsed:_a.isCollapsed};
},MoveToBookmark:function(_b,_c){
var _d=_c.anchorNodeBookmark.Select();
var _e=_c.focusNodeBookmark.Select();
_b.collapse(_d,_c.anchorOffset);
if(!_c.isCollapsed){
_b.extend(_e,_c.focusOffset);
}
},Update:function(){
try{
this.HtmlText=this.Window.document.body.innerHTML;
this.Bookmark=this.BookmarkSelection(this.Window.getSelection());
}
catch(e){
}
}};
RadEditorNamespace.RadNodeBookmark={New:function(_f,_10){
var _11={};
RadEditorNamespace.Utils.ExtendObject(_11,this);
_11.Window=_f;
_11.NodePath=_11.FindNodePath(_11.Window.document.body,_10);
return _11;
},Select:function(){
var _12=this.FindNode(this.Window.document.body,this.NodePath);
try{
RadEditorNamespace.Utils.SelectElement(this.Window,_12);
}
catch(ex){
}
return _12;
},FindNodePath:function(_13,_14){
var n,res;
for(var i=0;i<_13.childNodes.length;i++){
n=_13.childNodes[i];
res=this.FindNodePath(n,_14);
if(""!=res){
return ""+i+","+res;
}
if(n==_14){
return ""+i;
}
}
return "";
},FindNode:function(_18,_19){
var arr=_19.split(",");
for(var i=0;i<arr.length;i++){
_18=_18.childNodes[arr[i]];
}
return _18;
}};;RadEditorNamespace.RadSelection={New:function(_1){
var _2={};
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.Window=_1;
return _2;
},GetRange:function(){
if(this.Window.document.selection&&!window.opera){
return this.Window.document.selection.createRange();
}else{
if(this.Window.getSelection){
var _3=this.Window.getSelection();
if(!_3||_3.rangeCount<1){
return null;
}
var _4=null;
if(_3.getRangeAt){
_4=_3.getRangeAt(0);
}else{
_4=this.Window.document.createRange();
}
return _4;
}
}
},GetParentElement:function(){
var _5=this.GetRange();
if(!_5){
return null;
}
if(_5.commonAncestorContainer){
var _6=this.Window.getSelection();
var _7=_5.startContainer?_5.startContainer:_6.baseNode;
var _8=_5.endContainer?_5.endContainer:_6.extentNode;
var _9=_5.startOffset!=null?_5.startOffset:_6.baseOffset;
var _a=_5.endOffset!=null?_5.endOffset:_6.extentOffset;
if(_7==_8&&(_a-_9)==1){
return _6.anchorNode.childNodes[_6.anchorOffset];
}else{
if(!_5.commonAncestorContainer.tagName){
if(this.Window.document==_5.commonAncestorContainer&&_6.baseNode){
return _6.baseNode.parentNode;
}
return _5.commonAncestorContainer.parentNode;
}else{
return _5.commonAncestorContainer;
}
}
}else{
if(_5.length){
return _5.item(0);
}else{
if(_5.parentElement){
return _5.parentElement();
}else{
return null;
}
}
}
},IsControl:function(){
if(this.Window.document.selection){
return (this.Window.document.selection.type=="Control");
}else{
var _b=this.Window.getSelection();
if(_b.toString()!=""){
return false;
}
var _c=_b.focusNode;
if(_c.nodeType==1){
return false;
}
return (_c.tagName=="IMG");
}
},GetText:function(){
if(this.Window.document.selection){
var _d=this.Window.document.selection.createRange();
if(_d.length){
return "";
}else{
if(null!=_d.text){
return _d.text;
}
}
}else{
if(this.Window.getSelection){
return this.Window.getSelection().toString();
}else{
return "";
}
}
},GetHtmlText:function(){
if(this.Window.document.selection&&!window.opera){
var _e=this.Window.document.selection.createRange();
if(_e.length){
return _e.item(0).outerHTML;
}else{
if(_e.htmlText){
return _e.htmlText;
}else{
return "";
}
}
}else{
if(this.Window.getSelection){
var _f=this.Window.getSelection();
var _e=null;
if(_f.getRangeAt){
_e=_f.getRangeAt(0);
var _10=this.Window.document.createElement("div");
var _11=_e.cloneContents();
_10.appendChild(_11);
return _10.innerHTML;
}else{
return _f;
}
}else{
return "";
}
}
},Collapse:function(_12){
_12=(_12==true);
if(this.Window.document.selection){
var rng=this.Window.document.selection.createRange();
if(rng.collapse){
rng.collapse(_12);
rng.select();
}
}else{
if(this.Window.getSelection){
var _14=this.Window.getSelection();
if(!_14.isCollapsed){
if(_12){
_14.collapseToStart();
}else{
_14.collapseToEnd();
}
}
}
}
}};;RadEditorNamespace.ServerRenderingInitializer=function(_1,_2){
_1.Tools=[];
_1.Toolbars=[];
_1.ToolbarsCreated=false;
var _3=_1.CreateEditorToolbar;
var _4=_1.CreateEditorTool;
var _5=null;
var _6=0;
var _7=false;
var _8=0;
_1.CreateEditorToolbar=function(_9,_a,_b,_c,_d,_e){
var _f=_3.call(_1,_9,_a,_b,_c,_d);
_5=document.getElementById(_9);
_5.setAttribute("unselectable","on");
_5.onselectstart=new Function("return false;");
_5.ondragstart=new Function("return false;");
if(_1.IsSafari){
_5.setAttribute("onmousedown","return false;");
}
_5.style.display=RadEditorNamespace.DockableObjectDisplay;
_8=0;
_6=0;
_7=_e;
_f.Create(_5);
if(_e||(_1.EnableDocking&&_c&&(false!=_2))){
_f.IsRibbon=_e;
_f.IsDockable=_e?false:_c;
var _10=_5.parentNode;
var _11=_f.ConfigureToolbarWrapper();
}
return _f;
};
var _12=function(){
if(_5.rows.length==1){
return _5.rows[0].cells[_6].firstChild;
}else{
if(_7){
var _13=_5.rows[0].cells.length;
if(_8>_13-1){
var _14=_5.rows[1].cells[_8-_13];
oChild=_14.firstChild;
}else{
oChild=_5.rows[0].cells[_8].firstChild;
}
_8++;
return oChild;
}else{
return _5.rows[_6].cells[0].firstChild;
}
}
};
_1.CreateEditorTool=function(_15){
var _16=null;
var _17=_15[0];
var _18=_15[1];
if(_17!="S"){
var _16=_4.call(_1,_15);
var _19=_12();
if(_16){
_16.Create(_19);
if(_17!="B"){
if(_19&&_19.rows&&_19.rows[0]&&_19.rows[0].cells){
var _1a=_19.rows[0];
if(_1a.cells[0]){
var _1b=_1a.cells[0];
var oFC=_1b.firstChild;
_16.HeaderElement=oFC;
oFC.setAttribute("unselectable","on");
_16.IconContainer=_1b;
_16.IconContainer.onclick=RadEditorNamespace.OnComboHeaderClick;
}
if(_1a.cells[1]){
var _1d=_1a.cells[1];
_16.ArrowElement=_1d;
_1d.setAttribute("unselectable","on");
_16.ArrowElement.onclick=RadEditorNamespace.OnComboArrowClick;
}
}
if(_17==RadEditorNamespace.TOOL_CUSTOM&&RadEditorToolInitializer&&RadEditorToolInitializer[_18]){
var _1e={};
_1e.GetController=function(){
return _1;
};
try{
tool=RadEditorToolInitializer[_18](_1e);
var _1f=tool.Create();
_19.parentNode.replaceChild(_1f,_19);
}
catch(e){
}
}
}else{
_16.Element.onclick=new Function("RadEditorNamespace.OnToolClick.call(this); return false;");
}
}
}
_6++;
return _16;
};
var _20=_1.DockingZones.TopZone;
_20.style.width=_20.offsetWidth+"px";
_1.CreateEditorToolbars(_1.ToolsArray);
var _21=_1.WrapperElement.getElementsByTagName("img");
for(var i=0;i<_21.length;i++){
var _23=_21[i];
_23.setAttribute("unselectable","on");
_23.ondragstart=new Function("return false;");
}
_1.CreateEditorToolbar=_3;
_1.CreateEditorTool=_4;
};;RadEditorNamespace.RadToolbar={New:function(_1){
var _2={};
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.Document=_1.Document;
_2.Id=_1.Id;
_2.Title=_1.Title;
_2.IsDockable=_1.IsDockable!=null?_1.IsDockable:false;
_2.ZoneId=_1.ZoneId!=null?_1.ZoneId:"";
_2.Tools=[];
_2.IsRibbon=(_1.IsRibbon==true);
return _2;
},Dispose:function(){
this.Document=null;
this.Element=null;
},WrapInRibbonContainer:function(_3,_4){
var _5=document;
var _6=_5.createElement("table");
_6.border=0;
_6.cellSpacing=0;
_6.cellPadding=0;
_6.setAttribute("unselectable","on");
_7=_6.insertRow(-1);
_8=_7.insertCell(-1);
_8.appendChild(_3);
var _7=_6.insertRow(-1);
var _8=_7.insertCell(-1);
var _9=_5.createElement("span");
_9.className="RadAutoDockButton";
_9.innerHTML="&nbsp;&nbsp;&nbsp;";
_8.appendChild(_9);
_8.innerHTML+=(_4?"&nbsp;"+_4:"");
_8.setAttribute("noWrap","true");
_8.className="RadETitleGrip";
return _6;
},GetRibbonToolbarLength:function(_a){
var _b=0;
for(var i=0;i<_a.length;i++){
if(!_a[i].Create){
continue;
}
_b++;
}
_b=Math.ceil(_b/2);
return _b;
},Create:function(_d){
if(this.IsRibbon){
this.IsVertical=false;
this.IsDockable=false;
}
if(null==_d&&!this.Element){
var _e=RadEditorNamespace.Utils.GetPlainTable(this.Document);
_e.setAttribute("onmousedown","return false;");
if(!this.IsVertical){
_e.insertRow(-1);
}
var _f=this.Tools;
var _10=this.GetRibbonToolbarLength(_f);
var _11=0;
for(var i=0;i<_f.length;i++){
var _13=null;
var _14=_f[i];
if(this.IsRibbon&&!_14.Create){
continue;
}else{
_11++;
}
_13=this.CreateToolCell(_e);
if(!_14.Create){
_13.innerHTML="&nbsp;";
RadEditorNamespace.Utils.MakeSeparator(_13,this.IsVertical);
}else{
var _15=_14.Create();
_13.appendChild(_15);
}
if(this.IsRibbon&&(_11==_10)){
_e.insertRow(-1);
}
}
this.Element=_e;
this.ConfigureToolbarWrapper();
}else{
if(null!=_d){
this.Element=_d;
}
}
return this.Element;
},GetTopElement:function(){
this.Create();
return this.Element;
},ConfigureToolbarWrapper:function(){
var _16=null;
var _17="RadEToolbar";
if(this.IsRibbon){
this.Element.className="RadEToolbar";
_16=this.WrapInRibbonContainer(this.Element,this.Title);
_17="RadERibbon";
}else{
if(this.IsDockable&&RadEditorNamespace.Docking){
_16=RadEditorNamespace.Docking.WrapInDockingContainer(this.Element,this.IsVertical,RadEditorNamespace.RadEditorToolbar_RenderHorizontal,RadEditorNamespace.RadEditorToolbar_RenderVertical,"RadEToolbar","RadEToolbarVertical",this.Title);
this.Element.className="";
_17=this.IsVertical?"RadEToolbarVertical":"RadEToolbar";
}
}
this.Element.removeAttribute("id");
if(_16){
this.Element=_16;
}
if(_17){
this.Element.className=_17;
}
this.Element.style.display=RadEditorNamespace.DockableObjectDisplay;
if(!document.all){
this.Element.setAttribute("style","float:left");
}
this.Element.setAttribute("id",this.Id);
this.Element.setAttribute("title",this.Title);
return this.Element;
},AddSeparator:function(){
this.Tools[this.Tools.length]="SEPARATOR";
},AddTool:function(_18){
this.Tools[this.Tools.length]=_18;
},CreateToolCell:function(_19){
var _1a=this.IsVertical?_19.insertRow(-1):_19.rows[_19.rows.length-1];
var _1b=_1a.insertCell(-1);
_1b.setAttribute("unselectable","on");
return _1b;
}};
RadEditorNamespace.RadEditorToolbar_RenderHorizontal=function(){
var _1c=this.getElementsByTagName("TABLE")[0];
var _1d=_1c.rows[0];
while(_1c.rows.length>1){
var row=_1c.rows[1];
var _1f=row.cells[0];
if(_1f.className=="RadESeparatorHorizontal"){
_1f.className="RadESeparator";
}
_1d.appendChild(_1f);
row.parentNode.removeChild(row);
}
_1c.VerticalRows=0;
};
RadEditorNamespace.RadEditorToolbar_RenderVertical=function(){
var _20=this.getElementsByTagName("TABLE")[0];
var _21=_20.rows[0].cells;
var _22=_21.length-1;
for(var i=0;i<_22;i++){
var row=_20.insertRow(_20.rows.length);
var _25=_21[1];
_25.parentNode.removeChild(_25);
if(_25.className=="RadESeparator"){
_25.className="RadESeparatorHorizontal";
}
row.appendChild(_25);
}
};;RadEditorNamespace.ToolbarModeBase={Editor:null,GetToolbarManagerFn:null,New:function(_1){
var _2={};
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.Editor=_1;
_2.InitializeEditor();
return _2;
},InitializeEditor:function(){
var _3=this;
var _4=this.Editor;
var _5=this.GetToolbarManagerFn();
_5.Add(this);
_4.AttachEventHandler(RadEditorNamespace.RADEVENT_MODE_CHANGED,function(){
var _6=_4.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE;
_4.SetToolbarsVisible(_6);
var _7=(_4.GetMode()==RadEditorNamespace.RADEDITOR_DESIGN_MODE);
_3.GetToolbarManagerFn().ShowToolbarHolder(_7);
});
var _8=function(){
var _9=_3.GetToolbarManagerFn();
_9.SetEditorFocus(_3);
};
if(_4.IsIE){
var _a=function(){
window.setTimeout(function(){
_4.AttachEventHandler(RadEditorNamespace.RADEVENT_BEFORE_EDIT_FOCUS,_8);
},0);
};
if(document.all&&"complete"!=document.readyState){
RadEditorNamespace.Utils.AttachEventEx(window,"onload",_a);
}else{
_a();
}
}else{
_4.AttachEventHandler(RadEditorNamespace.RADEVENT_SEL_CHANGED,_8);
}
_4.AttachEventHandler(RadEditorNamespace.RADEVENT_CALLBACK_STARTED,function(){
var _b=_3.GetToolbarManagerFn();
if(_4==_b.CurrentEditor){
_b.ShowToolbarHolder(false);
}
});
},GetToolbar:function(){
if(!this.ToolbarHolderElement){
var _c=document.createElement("table");
var _d=_c.insertRow(-1);
var _e=_d.insertCell(-1);
this.ToolbarHolderElement=_c;
if(this.Editor.ToolsWidth){
this.Editor.SetToolbarHolderWidth(_e);
}else{
_e.style.width=parseInt(this.Editor.GetWidth())+"px";
}
var _f=this.Editor.GetToolbars();
for(var i=0;i<_f.length;i++){
_e.appendChild(_f[i].GetTopElement());
}
}
return this.ToolbarHolderElement;
}};
RadEditorNamespace.ToolbarManagerBase={ToolbarFlavors:[],CurrentEditor:null,ToolbarHolder:null,OverlayFrame:null,Add:function(_11){
this.ToolbarFlavors[this.ToolbarFlavors.length]=_11;
},Dispose:function(){
this.ToolbarFlavors=null;
},GetToolbarByEditor:function(_12){
var _13=this.ToolbarFlavors;
for(var i=0;i<_13.length;i++){
if(_12==_13[i].Editor){
return _13[i].GetToolbar();
}
}
return null;
},SetEditorTopMargin:function(_15,_16){
if(_15){
if(_16.NewMarginTop!=null){
_16.WrapperElement.style.marginTop=_16.NewMarginTop;
return;
}
var _17=RadEditorNamespace.Utils.GetRect(this.GetToolbarHolder());
var _18=RadEditorNamespace.Utils.GetRect(_16.WrapperElement);
if(_17.height>_18.top){
_16.RealMarginTop=_16.WrapperElement.style.marginTop;
var _19=_17.height-_18.top;
_16.NewMarginTop=_19+"px";
_16.WrapperElement.style.marginTop=_16.NewMarginTop;
}
}else{
if(_16&&_16.RealMarginTop!=null){
_16.WrapperElement.style.marginTop=_16.RealMarginTop;
}
}
},ShowToolbarHolder:function(_1a){
if(this.GetToolbarHolder()){
this.GetToolbarHolder().style.display=_1a?"":"none";
}
if(this.OverlayFrame){
this.OverlayFrame.style.display=_1a?"inline":"none";
}
if(!_1a){
this.SetEditorTopMargin(false,this.CurrentEditor);
this.CurrentEditor=null;
}
},HideToolbarHolder:function(e){
if(document.readyState&&document.readyState!="complete"){
return;
}
var _1c=this.GetToolbarHolder();
if(!_1c||"none"==_1c.style.display||!this.CurrentEditor){
return;
}
if(!RadEditorNamespace.Utils.IsMouseInElement(e,_1c,this.CurrentEditor.WrapperElement)){
this.ShowToolbarHolder(false);
}
},GetToolbarHolder:function(){
if(!this.ToolbarHolder){
var _1d=document.createElement("table");
_1d.cellSpacing=0;
_1d.cellPadding=0;
_1d.style.display="none";
_1d.className=this.HolderCss;
row=_1d.insertRow(-1);
cell=row.insertCell(-1);
cell.setAttribute("height","100%");
_1d.setAttribute("id","RadEditorRelativeToolbarHolder");
document.body.appendChild(_1d);
this.ToolbarHolder=_1d;
}
return this.ToolbarHolder;
},SetEditorFocus:function(_1e){
var _1f=this.ToolbarFlavors;
var _20=false;
for(var i=0;i<_1f.length;i++){
if(_1f[i]==_1e){
_20=true;
break;
}
}
if(!_20){
this.Add(_1e);
}
var _22=_1e.Editor;
if(true==_22.ToggleFullScreen){
if(this.OverlayFrame){
this.OverlayFrame.style.display="none";
}
return;
}else{
if(_22==this.CurrentEditor){
this.SetEditorTopMargin(true,this.CurrentEditor);
return;
}
}
this.ShowToolbarHolder(true);
var _23=this.GetToolbarByEditor(_22);
if(_23){
this.SetEditorTopMargin(false,this.CurrentEditor);
this.CurrentEditor=_22;
var _24=this.GetToolbarHolder().rows[0].cells[0];
if(_24.firstChild){
_24.removeChild(_24.firstChild);
}
_24.appendChild(_23);
if(this.OnSetEditorFocus!=null&&typeof (this.OnSetEditorFocus)=="function"){
this.OnSetEditorFocus();
}
this.SetOverlay(this.GetToolbarHolder());
this.SetEditorTopMargin(true,_22);
}
},SetOverlay:function(_25){
if("complete"!=document.readyState&&!document.all){
return;
}
if(!this.OverlayFrame){
var frm=document.createElement("IFRAME");
frm.id="OverlayFrame";
frm.src="javascript:''";
frm.className=this.HolderCss;
frm.frameBorder=0;
frm.scrolling="no";
frm.style.overflow="hidden";
frm.style.display="inline";
frm.style.position="absolute";
this.OverlayFrame=frm;
_25.parentNode.insertBefore(this.OverlayFrame,_25);
}
var frm=this.OverlayFrame;
try{
var _27=RadEditorNamespace.Utils.GetRect(_25);
frm.style.display="inline";
frm.style.width=_27.width+"px";
frm.style.height=_27.height+"px";
frm.style.left=_25.style.left;
frm.style.top=_25.style.top;
frm.className=this.HolderCss;
frm.style.borderWidth=0+"px";
}
catch(ex){
}
}};
RadEditorNamespace.GetPageTopToolbarManager=function(){
if(!RadEditorNamespace.PageTopToolbarManagerObject){
var _28=RadEditorNamespace.PageTopToolbarManager.New();
RadEditorNamespace.Utils.AttachEventEx(window,"onunload",function(){
_28.Dispose();
});
RadEditorNamespace.Utils.AttachEventEx(document,"click",function(e){
_28.HideToolbarHolder(e);
});
RadEditorNamespace.PageTopToolbarManagerObject=_28;
}
return RadEditorNamespace.PageTopToolbarManagerObject;
};
RadEditorNamespace.PageTopToolbarManager={New:function(){
var _2a={};
RadEditorNamespace.Utils.ExtendObject(_2a,RadEditorNamespace.ToolbarManagerBase);
RadEditorNamespace.Utils.ExtendObject(_2a,this);
_2a.ToolbarFlavors=[];
_2a.HolderCss=(document.all)?"RadEFixedToolbarHolderIE":"RadEFixedToolbarHolderMozilla";
return _2a;
}};
RadEditorNamespace.PageTopToolbarMode={};
RadEditorNamespace.Utils.ExtendObject(RadEditorNamespace.PageTopToolbarMode,RadEditorNamespace.ToolbarModeBase);
RadEditorNamespace.PageTopToolbarMode.GetToolbarManagerFn=RadEditorNamespace.GetPageTopToolbarManager;
RadEditorNamespace.GetShowOnFocusToolbarManager=function(){
if(!RadEditorNamespace.ShowOnFocusToolbarManagerObject){
var _2b=RadEditorNamespace.ShowOnFocusToolbarManager.New();
RadEditorNamespace.Utils.AttachEventEx(window,"onunload",function(){
_2b.Dispose();
});
RadEditorNamespace.Utils.AttachEventEx(document,"click",function(e){
_2b.HideToolbarHolder(e);
});
RadEditorNamespace.ShowOnFocusToolbarManagerObject=_2b;
}
return RadEditorNamespace.ShowOnFocusToolbarManagerObject;
};
RadEditorNamespace.ShowOnFocusToolbarManager={New:function(){
var _2d={};
RadEditorNamespace.Utils.ExtendObject(_2d,RadEditorNamespace.ToolbarManagerBase);
RadEditorNamespace.Utils.ExtendObject(_2d,this);
_2d.HolderCss="RadERelativeToolbarHolder";
_2d.ToolbarFlavors=[];
return _2d;
},OnSetEditorFocus:function(){
var _2e=RadEditorNamespace.Utils.GetRect(this.CurrentEditor.WrapperElement);
this.GetToolbarHolder().style.width=_2e.width+"px";
this.PositionToolbar();
},PositionToolbar:function(){
var _2f=this.GetToolbarHolder();
var _30=RadEditorNamespace.Utils.GetRect(_2f);
var _31=RadEditorNamespace.Utils.GetRect(this.CurrentEditor.WrapperElement);
var _32=RadEditorNamespace.Utils.FindScrollPosY(this.CurrentEditor.WrapperElement);
var _33=_31.top-_30.height-_32;
_33=_33<0?0:_33;
var _34=_31.left;
_34=_34<0?0:_34;
_2f.style.display="";
_2f.style.position="absolute";
_2f.style.left=_34+"px";
_2f.style.top=_33+"px";
}};
RadEditorNamespace.ShowOnFocusToolbarMode={};
RadEditorNamespace.Utils.ExtendObject(RadEditorNamespace.ShowOnFocusToolbarMode,RadEditorNamespace.ToolbarModeBase);
RadEditorNamespace.ShowOnFocusToolbarMode.GetToolbarManagerFn=RadEditorNamespace.GetShowOnFocusToolbarManager;
RadEditorNamespace.FloatingToolbarMode={EditorMode:RadEditorNamespace.RADEDITOR_DESIGN_MODE,Editor:null,Localization:null,ToolbarImage:null,New:function(_35){
var obj={};
RadEditorNamespace.Utils.ExtendObject(obj,this);
obj.Editor=_35;
obj.Localization=obj.Editor.Localization;
obj.EditorMode=obj.Editor.Mode;
var _37=document.createElement("IMG");
_37.src=obj.Editor.SkinBasePath+"Img/toolbar.gif";
_37.style.border="1px solid white";
_37.onmouseover=new Function("this.style.border = '1px outset';");
_37.onmouseout=new Function("this.style.border = '1px solid white';");
var _38=obj;
_37.Toolbar=obj;
_37.onclick=function(){
this.style.border="1px inset";
_38.ToggleFloatingToolbar();
};
obj.Editor.DockingZones.TopZone.appendChild(_37);
obj.ToolbarImage=_37;
var _39=function(){
_38.OnModeChanged();
};
obj.Editor.AttachEventHandler(RadEditorNamespace.RADEVENT_MODE_CHANGED,_39);
_35.AttachEventHandler(RadEditorNamespace.RADEVENT_CALLBACK_STARTED,function(){
if(_38.ToolbarWnd){
_38.ToolbarWnd.ShowWindow(false);
}
});
_35.AttachEventHandler(RadEditorNamespace.RADEVENT_DISPOSE,function(){
_38.ToolbarHolderElement=null;
_38.Editor=null;
if(_38.ToolbarImage){
_38.ToolbarImage.onclick=null;
}
_38.ToolbarImage=null;
if(_38.ToolbarWnd){
_38.ToolbarWnd.OnClientClosing=null;
}
_38.ToolbarWnd=null;
});
return obj;
},OnModeChanged:function(){
var _3a=(this.Editor.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE);
this.ToolbarImage.style.display=_3a?"":"none";
if(this.EditorMode==RadEditorNamespace.RADEDITOR_DESIGN_MODE){
this.IsToolbarWndPrevVisible=this.ToolbarWnd?this.ToolbarWnd.IsVisible():null;
}
if(this.ToolbarWnd){
this.ToolbarWnd.ShowWindow(this.IsToolbarWndPrevVisible&&_3a);
}
this.EditorMode=this.Editor.Mode;
},ToggleFloatingToolbar:function(_3b){
var x,y;
if(!this.ToolbarWnd){
var _3e=document.createElement("table");
var _3f=_3e.insertRow(0);
var _40=_3f.insertCell(0);
this.ToolbarHolderElement=_3e;
if(this.Editor.ToolsWidth){
this.Editor.SetToolbarHolderWidth(_40);
}else{
_40.style.width=parseInt(this.Editor.GetWidth())+"px";
}
var _41=this.Editor.GetToolbars();
for(var i=0;i<_41.length;i++){
_40.appendChild(_41[i].GetTopElement());
}
var rwi=new RadWindowInfo();
if(document.all){
rwi.Url="javascript:''";
rwi.Width=1;
rwi.Height=1;
}else{
rwi.Url="";
rwi.InnerHtml="";
rwi.Width=this.Editor.ToolsWidth||"600px";
}
rwi.Caption=this.Localization["MainToolbar"];
rwi.IsVisible=false;
rwi.Argument=null;
rwi.Movable=true;
rwi.Resizable=true;
rwi.UseClassicDialogs=false;
this.ToolbarWnd=GetEditorRadWindowManager().ShowModallessWindow(rwi);
this.ToolbarWnd.OnClientClosing=function(_44){
this.ShowWindow(false);
return false;
};
this.ToolbarWnd.ContentWindow.innerHTML="";
this.ToolbarWnd.ContentWindow.appendChild(this.ToolbarHolderElement);
var rc=RadEditorNamespace.Utils.GetRect(this.Editor.WrapperElement);
x=rc.left;
y=rc.top;
}
if(_3b){
var rc=RadEditorNamespace.Utils.GetRect(this.Editor.WrapperElement);
x=rc.left;
y=rc.top;
}
this.ToolbarWnd.ShowWindow(!this.ToolbarWnd.IsVisible(),x,y);
}};;;RadEditorNamespace.RadBrowserCommand={New:function(_1,_2,_3,_4){
var _5=RadEditorNamespace.RadCommandBase.New((_1||_2),_6,_3);
RadEditorNamespace.Utils.ExtendObject(_5,this);
_5.CommandID=_2;
_5.Value=_4;
var _6=true;
switch(_5.CommandID){
case "Copy":
case "SelectAll":
case "Print":
_6=false;
break;
}
return _5;
},Clone:function(){
return RadEditorNamespace.RadBrowserCommand.New(this.Title,this.CommandID,this.Window,this.Value);
},GetState:function(_7){
try{
_7=_7||this.Window;
var _8=_7.document;
if(null==_8){
return RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
}
if(!window.opera&&null!=_8.queryCommandEnabled&&!_8.queryCommandEnabled(this.CommandID)){
return RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
}
return _8.queryCommandState(this.CommandID)?RadEditorNamespace.RADCOMMAND_STATE_ON:RadEditorNamespace.RADCOMMAND_STATE_OFF;
}
catch(ex){
return RadEditorNamespace.RADCOMMAND_STATE_OFF;
}
},GetValue:function(_9){
try{
_9=_9||this.Window;
return _9.document.queryCommandValue(this.CommandID);
}
catch(ex){
}
return null;
},OnExecute:function(){
if(RadEditorNamespace.RADCOMMAND_ABSOLUTE_POSITION==this.CommandID){
this.Window.document.execCommand("2D-Position",false,true);
}
var _a=true;
if(this.CommandID==RadEditorNamespace.RADCOMMAND_BACKCOLOR&&(window.opera||(!document.all&&!document.body.outerHTML))){
this.CommandID="HiliteColor";
_a=false;
}
if(this.CommandID==RadEditorNamespace.RADCOMMAND_FONTSIZE&&TelerikNamespace.Utils.DetectBrowser("safari")){
var _b=parseInt(this.Value);
switch(_b){
case 1:
this.Value="8pt";
break;
case 2:
this.Value="10pt";
break;
case 3:
this.Value="12pt";
break;
case 4:
this.Value="14pt";
break;
case 5:
this.Value="18pt";
break;
case 6:
this.Value="24pt";
break;
case 7:
this.Value="36pt";
break;
}
}
if(false==_a){
this.Window.document.execCommand("UseCSS",false,false);
}
var _c=this.Window.document.execCommand(this.CommandID,false,this.Value);
if(false==_a){
this.Window.document.execCommand("UseCSS",false,true);
}
return _c;
}};
RadEditorNamespace.RadGenericCommand={New:function(_d,_e){
var _f=RadEditorNamespace.RadCommandBase.New(_d,true,_e);
RadEditorNamespace.Utils.ExtendObject(_f,this);
_f.RestorePoint1=RadEditorNamespace.RadCreateRestorePoint(_f.Window);
return _f;
},Execute:function(){
if(null==this.RestorePoint2){
this.RestorePoint2=RadEditorNamespace.RadCreateRestorePoint(this.Window);
}else{
this.RestorePoint2.Restore();
}
return true;
},Unexecute:function(){
this.RestorePoint1.Restore(true);
}};;RadEditorNamespace.RadCommandBase={New:function(_1,_2,_3){
var _4={};
RadEditorNamespace.Utils.ExtendObject(_4,this);
_4.IsSafari=TelerikNamespace.Utils.DetectBrowser("safari");
_4.Title=_1;
_4.CanUnexecute=(_2!=false);
_4.Window=_3;
return _4;
},GetState:function(_5){
return RadEditorNamespace.RADCOMMAND_STATE_OFF;
},GetValue:function(_6){
return null;
},Execute:function(){
this.IsExecuted=false;
if(null==this.OnExecute||null==this.Window){
return false;
}
try{
if(!this.RestorePoint1){
this.RestorePoint1=RadEditorNamespace.RadCreateRestorePoint(this.Window);
}else{
this.RestorePoint1.Select();
}
return (this.IsExecuted=this.OnExecute());
}
catch(ex){
}
return false;
},OnExecute:function(){
if(null!=document.selection&&null!=this.OnExecuteIE&&!window.opera){
return this.OnExecuteIE();
}else{
if(null!=window.getSelection&&null!=this.OnExecuteMoz){
return this.OnExecuteMoz();
}
}
return false;
},Unexecute:function(){
try{
if(this.CanUnexecute&&this.IsExecuted){
this.RestorePoint1.Restore();
}
}
catch(ex){
}
}};;;RadEditorNamespace.RadFormatBlockCommand={New:function(_1,_2,_3){
var _4=RadEditorNamespace.RadCommandBase.New((_1||"Format Block"),true,_2);
RadEditorNamespace.Utils.ExtendObject(_4,this);
_4.FormatValue=_3;
return _4;
},Clone:function(){
return RadEditorNamespace.RadFormatBlockCommand.New(this.Title,this.Window,this.FormatValue);
},GetValue:function(_5){
try{
_5=_5||this.Window;
var _6=_5.document.queryCommandValue(RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK);
if(!document.all){
switch(_6){
case "x":
case "":
_6="Normal";
break;
}
}
return _6;
}
catch(ex){
}
return null;
},OnExecuteIE:function(){
var _7=this.Window.document;
if("<p>"==this.FormatValue.toLowerCase()){
return _7.execCommand(RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK,false,"<p>")&&_7.execCommand("RemoveFormat");
}
var _8=_7.selection.createRange();
var _9=false;
var _a=RadEditorNamespace.Utils.Trim(_8.htmlText);
if(_a&&_a.length>2&&_a.substr(0,2).toLowerCase()=="<p"){
_9=true;
}
if(!_9&&""!=_8.text){
var _b=this.FormatValue.substring(1,this.FormatValue.length-1);
_7.execCommand(RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK,false,"<p>");
_7.execCommand("RemoveFormat");
var _c=_7.createElement(_b);
_c.innerHTML=_8.htmlText;
_8.pasteHTML(_c.outerHTML);
return true;
}else{
return _7.execCommand(RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK,false,this.FormatValue);
}
},OnExecuteMoz:function(){
var _d=this.FormatValue.substring(1,this.FormatValue.length-1);
var _e=this.Window.document;
var _f=RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK;
var _10=("body"==this.FormatValue.toLowerCase()||"normal"==this.FormatValue.toLowerCase());
var _11=null;
var _12=_d.indexOf(" ");
if(_12!=-1){
_11=_d.substring(_12+1);
_d=_d.substring(0,_12);
}
if(this.IsSafari){
if(_10){
}else{
var _13=_e.createElement(_d);
if(_11){
var _14="";
_14=_11.replace(/class\=[\'|\"]?([^\'|^\"]+)[\'|\"]?/gi,"$1");
if(_14.length>0){
_13.className=_14;
}
}
var _15=RadEditorNamespace.RadSelection.New(this.Window);
_13.innerHTML=_15.GetHtmlText();
var _16=RadEditorNamespace.RadPasteHtmlCommand.New(this.Title,this.Window,_13.outerHTML,true);
_16.Execute();
}
return;
}
if(_10){
return _e.execCommand(_f,false,"Normal");
}
var _17=this.Window.getSelection();
if(_17.rangeCount<1){
return false;
}
var _18=_17.getRangeAt(0);
function isFormatBlockElement(_19){
if(!_19||!_19.tagName){
return false;
}
var _1a=_19.tagName;
if(_1a=="H1"||_1a=="H2"||_1a=="H3"||_1a=="H4"||_1a=="H5"||_1a=="H6"||_1a=="H7"||_1a=="ADDRESS"||_1a=="PRE"){
return true;
}
}
var _1b=RadEditorNamespace.RadSelection.New(this.Window).GetParentElement();
if(_18.toString()!=""&&!isFormatBlockElement(_1b)){
try{
var _13=_e.createElement(_d);
if(_11){
var _14="";
_14=_11.replace(/class\=[\'|\"]?([^\'|^\"]+)[\'|\"]?/gi,"$1");
if(_14.length>0){
_13.className=_14;
}
}
_13.appendChild(_18.extractContents());
_18.insertNode(_13);
return true;
}
catch(ex){
return false;
}
}else{
return _e.execCommand(RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK,false,this.FormatValue);
}
return false;
}};;RadEditorNamespace.RadFormatObjectCommand={New:function(_1,_2,_3,_4){
var _5=RadEditorNamespace.RadCommandBase.New(_1,true,_2);
RadEditorNamespace.Utils.ExtendObject(_5,this);
_5.TargetObjectBookmark=RadEditorNamespace.RadNodeBookmark.New(_2,_4);
_5.Diff(_3,_4);
return _5;
},Execute:function(){
try{
var _6=this.TargetObjectBookmark.Select();
this.IsExecuted=this.SourceValues.ApplyTo(_6);
}
catch(ex){
this.IsExecuted=false;
}
return this.IsExecuted;
},Unexecute:function(){
var _7=this.TargetObjectBookmark.Select();
this.TargetValues.ApplyTo(_7);
},Diff:function(_8,_9){
this.SourceValues=new RadEditorNamespace.DiffObjectInfo();
this.TargetValues=new RadEditorNamespace.DiffObjectInfo();
var _a=this.DiffAttributes(_8,_9,this.SourceValues.Attributes,this.TargetValues.Attributes);
if(_8.style.cssText!=_9.style.cssText){
this.SourceValues.Style=_8.style.cssText;
this.TargetValues.Style=_9.style.cssText;
_a=true;
}
if(_8.value!=_9.value){
this.SourceValues.Value=_8.value;
this.TargetValues.Value=_9.value;
_a=true;
}
if(_8.className!=_9.className){
this.SourceValues.ClassName=_8.className;
this.TargetValues.ClassName=_9.className;
_a=true;
}
if(_8.innerHTML!=_9.innerHTML){
this.SourceValues.InnerHtml=_8.innerHTML;
this.TargetValues.InnerHtml=_9.innerHTML;
this.TargetValues.InnerHtml=_9.innerHTML;
_a=true;
}
return _a;
},DiffAttributes:function(_b,_c,_d,_e){
if(document.all&&!window.opera){
return this.DiffAttributesIE(_b,_c,_d,_e);
}else{
return this.DiffAttributesMoz(_b,_c,_d,_e);
}
},DiffAttributesIE:function(_f,_10,_11,_12){
var _13,_14,_15,_16,_17;
for(var i=0;i<_10.attributes.length;i++){
try{
_15=_10.attributes[i];
_13=_15.nodeName;
switch(_13.toLowerCase()){
case "style":
case "value":
case "classname":
continue;
case "name":
_13="NAME";
break;
}
_17=_15.nodeValue;
_16=_f.getAttribute(_13);
if(!_17){
_17="";
}
if(!_16){
_16="";
}
if(_17!=_16){
_11[_11.length]={Name:_13,Value:_16};
_12[_12.length]={Name:_13,Value:_17};
}
}
catch(ex){
}
}
return (_11.length>0);
},DiffAttributesMoz:function(_19,_1a,_1b,_1c){
var _1d,_1e,_1f;
for(var i=0;i<_19.attributes.length;i++){
try{
_1d=_19.attributes[i];
_1f=_1d.nodeName;
_1e=_1a.attributes[_1f];
if(null!=_1e&&_1d.nodeValue==_1e.nodeValue){
continue;
}else{
_1b[_1b.length]={Name:_1f,Value:_1d.nodeValue};
if(_1e){
_1c[_1c.length]={Name:_1f,Value:_1e.nodeValue};
}else{
_1c[_1c.length]={Name:_1f,Value:""};
}
}
}
catch(ex){
}
}
return (_1b.length>0);
}};
RadEditorNamespace.DiffObjectInfo=function(){
this.Attributes=[];
this.Style=null;
this.Value=null;
this.ClassName=null;
this.InnerHtml=null;
};
RadEditorNamespace.DiffObjectInfo.prototype.ApplyTo=function(_21){
this.ApplyAttributes(_21);
if(null!=this.Style){
_21.style.cssText=this.Style;
}
if(null!=this.Value){
_21.setAttribute("value",this.Value);
}
if(null!=this.ClassName){
_21.className=this.ClassName;
}
if(null!=this.InnerHtml){
_21.innerHTML=this.InnerHtml;
}
return true;
};
RadEditorNamespace.DiffObjectInfo.prototype.ApplyAttributes=function(_22){
if(this.Attributes){
var _23=null;
for(var i=0;i<this.Attributes.length;i++){
_23=this.Attributes[i];
if(null==_23.Value||""==_23.Value){
_22.removeAttribute(_23.Name);
}else{
if("function"==typeof (_23.Value)){
continue;
}else{
_22.setAttribute(_23.Name,_23.Value);
}
}
}
}
};;RadEditorNamespace.RadPasteHtmlCommand={New:function(_1,_2,_3,_4){
var _5=RadEditorNamespace.RadCommandBase.New((_1||"Insert Html"),true,_2);
RadEditorNamespace.Utils.ExtendObject(_5,this);
_5.HtmlText=_3;
_5.SelectText=(true==_4);
_5.IsSafari=TelerikNamespace.Utils.DetectBrowser("safari");
return _5;
},Clone:function(){
return RadEditorNamespace.RadPasteHtmlCommand.New(this.Title,this.Window,this.HtmlText);
},OnExecute:function(){
if(document.all&&!window.opera){
return this.OnExecuteIE();
}else{
return this.OnExecuteMoz();
}
},OnExecuteIE:function(){
var _6=this.Window.document;
if(_6.selection.type.toLowerCase()!="none"){
_6.selection.createRange().execCommand("Delete");
}
if(_6.selection.type.toLowerCase()!="none"){
_6.execCommand("Delete");
}
_6.body.setActive();
selRange=_6.selection.createRange();
if(selRange&&selRange.length){
var _7=selRange.item(0);
if(_7&&_7.tagName=="BODY"){
var _8=_7.getElementsByTagName("FORM")[0];
if(_8){
_8.innerHTML+=this.HtmlText;
}
}
}else{
var _9=selRange.duplicate();
_9.collapse(true);
selRange.pasteHTML(this.HtmlText);
if(this.SelectText){
_9.setEndPoint("EndToEnd",selRange);
_9.select();
}
}
return true;
},OnExecuteMoz:function(){
var _a=this.Window.document;
var _b=this.Window.document.createElement("SPAN");
_b.innerHTML=this.HtmlText;
var _c=this.SelectText;
this.SelectText=true;
this.InsertNodeAtSelection(this.Window,_b);
if(!this.IsSafari){
var _d=_a.createRange();
_d.selectNodeContents(_b);
var _e=_d.extractContents();
_d.selectNode(_b);
_d.deleteContents();
this.SelectText=_c;
this.InsertNodeAtSelection(this.Window,_e);
}
this.SelectText=_c;
return true;
},InsertNodeAtSelection:function(_f,_10){
var _11=_f.getSelection();
if(this.IsSafari){
_f.document.execCommand("Delete");
}
var _12=this.IsSafari?_f.document.createRange():_11.getRangeAt(0);
if(!this.IsSafari){
_11.removeAllRanges();
}
_12.deleteContents();
var _13=this.IsSafari?_11.baseNode:_12.startContainer;
var _14=this.IsSafari?_11.baseOffset:_12.startOffset;
_12=_f.document.createRange();
if((_13.nodeType==3)&&(_10.nodeType==3)){
_13.insertData(_14,_10.nodeValue);
_12.setEnd(_13,_14+_10.length);
if(this.SelectText){
_12.setStart(_13,_14);
}else{
_12.setStart(_13,_14+_10.length);
}
}else{
var _15;
if(_13.nodeType==3){
var _16=_13;
_13=_16.parentNode;
var _17=_16.nodeValue;
var _18=_17.substr(0,_14);
var _19=_17.substr(_14);
var _1a=_f.document.createTextNode(_18);
var _15=_f.document.createTextNode(_19);
_13.insertBefore(_15,_16);
_13.insertBefore(_10,_15);
try{
_13.insertBefore(_1a,_10);
}
catch(exc){
}
_13.removeChild(_16);
}else{
if(_13.childNodes.length>0){
_15=_13.childNodes[_14];
_13.insertBefore(_10,_15);
}else{
if(_13.tagName!="BODY"){
_13=_13.parentNode;
}
_13.appendChild(_10);
}
}
try{
if(this.SelectText){
_12.setStart(_10,0);
_12.setEnd(_15,0);
}else{
_12.setEnd(_15,0);
_12.setStart(_15,0);
}
}
catch(exc){
}
}
try{
_11.addRange(_12);
}
catch(exc){
}
}};;RadEditorNamespace.RadSetAttributeCommand={New:function(_1,_2,_3,_4,_5){
var _6=RadEditorNamespace.RadCommandBase.New(_1,true,_2);
RadEditorNamespace.Utils.ExtendObject(_6,this);
if(!_3){
var _7=RadEditorNamespace.RadSelection.New(_6.Window);
_3=_7.GetParentElement();
}
_6.NodeBookmark=RadEditorNamespace.RadNodeBookmark.New(_6.Window,_3);
_6.AttribName=_4;
_6.NewValue=_5;
return _6;
},Clone:function(){
return RadEditorNamespace.RadSetAttributeCommand.New(this.Title,this.Window,null,this.AttribName,this.NewValue);
},Execute:function(){
var _8=this.NodeBookmark.Select();
if(!_8){
return false;
}
if(!this.IsExecuted){
this.OldValue=_8.getAttribute(this.AttribName);
}
if(this.AttribName&&this.AttribName.toLowerCase()=="name"&&document.all){
_8.name=this.NewValue;
_8.removeAttribute("name");
_8.removeAttribute("NAME");
}
var _9=RadEditorNamespace.Utils.Trim(this.NewValue);
if(""==_9){
_8.removeAttribute(this.AttribName,0);
if("className"==this.AttribName){
_8.removeAttribute("class",0);
}
}else{
_8[this.AttribName]=this.NewValue;
if(this.AttribName.toLowerCase()=="nowrap"){
_8.setAttribute(this.AttribName,this.NewValue);
}
}
this.IsExecuted=true;
return true;
},Unexecute:function(){
var _a=this.NodeBookmark.Select();
_a[this.AttribName]=this.OldValue;
}};;RadEditorNamespace.RadSetStyleRuleCommand={New:function(_1,_2,_3,_4,_5){
var _6=RadEditorNamespace.RadCommandBase.New(_1,true,_2);
RadEditorNamespace.Utils.ExtendObject(_6,this);
if(!_3){
var _7=RadEditorNamespace.RadSelection.New(_6.Window);
_3=_7.GetParentElement();
}
_6.NodeBookmark=RadEditorNamespace.RadNodeBookmark.New(_6.Window,_3);
_6.StyleAttributeName=_4;
_6.NewValue=_5;
return _6;
},Clone:function(){
return RadEditorNamespace.RadSetStyleRuleCommand.New(this.Title,this.Window,null,this.StyleAttributeName,this.NewValue);
},Execute:function(){
var _8=this.NodeBookmark.Select();
if(!_8){
return false;
}
if(!this.IsExecuted){
this.OldValue=_8.style[this.StyleAttributeName];
}
_8.style[this.StyleAttributeName]=this.NewValue;
this.IsExecuted=true;
return true;
},Unexecute:function(){
var _9=this.NodeBookmark.Select();
_9.style[this.StyleAttributeName]=this.OldValue;
}};;RadEditorNamespace.RadStyleCommand={New:function(_1,_2,_3){
var _4=RadEditorNamespace.RadCommandBase.New(_1,true,_2);
RadEditorNamespace.Utils.ExtendObject(_4,this);
_4.ClassName=_3;
return _4;
},Clone:function(){
return RadEditorNamespace.RadStyleCommand.New(this.Title,this.Window,this.ClassName);
},GetValue:function(_5){
_5=_5||this.Window;
var _6=_5.document;
if(_6.all){
return this.GetValueIE(_6);
}else{
return this.GetValueMoz(_5);
}
},GetValueIE:function(_7){
var _8=_7.selection;
if(!_8){
return "";
}
var _9=_8.createRange();
var _a=(_9.length>0?_9(0):_9.parentElement());
if("BODY"==_a.tagName){
return "";
}else{
return (""==_a.className?"":_a.className);
}
},OnExecuteIE:function(){
if(""==this.ClassName){
return this.ClearStyleIE();
}else{
return this.ApplyStyleIE(this.ClassName);
}
},OnExecuteMoz:function(){
if(""==this.ClassName){
return this.ClearStyleMoz();
}else{
return this.ApplyStyleMoz(this.ClassName);
}
},ClearStyleIE:function(){
var _b=RadEditorNamespace.RadSelection.New(this.Window);
var _c=_b.GetParentElement();
if(_c.tagName!="FONT"&&_c.tagName!="BODY"){
_c.removeAttribute("classname",0);
return true;
}else{
return this.Window.document.execCommand("RemoveFormat");
}
},ApplyStyleIE:function(_d){
try{
var _e=this.Window.document.selection.createRange();
var _f=(_e.length>0?_e(0):_e.parentElement());
if(_f.tagName=="LI"){
var _10=_e.htmlText;
var _11=_10.match(/<LI\/?>/gi);
var _12=_11?_11.length:0;
if(_12>1){
_f=_f.parentNode;
}else{
if(_12==0&&(_f.innerHTML==_e.htmlText)&&_f.parentNode.childNodes.length==1){
_f=_f.parentNode;
}
}
}
if(_e.length>0||_f.tagName=="OL"||_f.tagName=="UL"){
this.SetElementClassName(_f,_d);
}else{
if(""!=_e.htmlText){
var _13=_e.duplicate();
var _14=_e.duplicate();
_13.collapse(true);
_14.collapse(false);
var _15=_13.parentElement();
if(_15.parentNode==_f){
_15=_f;
}
var _16=_14.parentElement();
if(_15==_16){
var _17=this.Window.document.body.createTextRange();
_17.moveToElementText(_15);
var _18=_e.htmlText;
var _19=false;
var _1a=_15.outerHTML.replace(/[\r\n\t]/ig,"");
var _1b=_15.innerHTML.replace(/[\r\n\t]/ig,"");
var _18=_18.replace(/[\r\n\t]/ig,"");
if(_1a==_18||_1b==_18){
_19=true;
}
if(_19||(0==_17.compareEndPoints("StartToStart",_e)&&0==_17.compareEndPoints("EndToEnd",_e))){
if("BODY"!=_f.tagName){
this.SetElementClassName(_f,_d);
}else{
this.ProcessTextSelection(_d,_e);
}
}else{
this.ProcessTextSelection(_d,_e);
}
}else{
if(_f==_16&&_f.parentNode==_15){
this.SetElementClassName(_f,_d);
}else{
if(_f==_16&&_f==_15.parentNode){
this.ProcessElementTextSelection(_d,_e,_15,_16);
}else{
if(_f==_15&&_f==_16.parentNode){
this.ProcessTextElementSelection(_d,_e,_15,_16);
}else{
if(_f!=_15&&_f!=_16){
this.ProcessElementElementSelection(_d,_e,_15,_16);
}
}
}
}
}
}
}
return true;
}
catch(ex){
return false;
}
},GetFormatHtml:function(_1c,_1d){
var _1e=this.Window.document.createElement("FONT");
_1e.innerHTML=_1c;
_1e.className=_1d;
return _1e.outerHTML;
},SetElementClassName:function(_1f,_20){
if(!_1f){
return "";
}
if(""==RadEditorNamespace.Utils.IsNull(_20,"")){
_1f.className="";
}else{
var _21=_1f.className;
_1f.className=_20;
return _21;
}
},ProcessTextSelection:function(_22,_23){
var _24=_23.htmlText;
_23.pasteHTML("");
var _25=_23.duplicate();
_25.collapse();
_23.pasteHTML(this.GetFormatHtml(_24,_22));
_25.setEndPoint("EndToEnd",_23);
},ProcessElementTextSelection:function(_26,_27,_28,_29){
var _2a=_27.duplicate();
_2a.moveToElementText(_28);
var _2b=_2a.duplicate();
var _2c=(0!=_27.compareEndPoints("StartToStart",_2a));
var _2d=_27.duplicate();
if(_2c){
_2d.setEndPoint("StartToStart",_2a);
}
var _2e=this.GetFormatHtml(_27.htmlText,_26);
_27.pasteHTML("");
_27.pasteHTML(_2e);
_2b.setEndPoint("EndToEnd",_27);
},ProcessTextElementSelection:function(_2f,_30,_31,_32){
var _33=_30.duplicate();
_33.moveToElementText(_32);
var _34=(0!=_30.compareEndPoints("EndToEnd",_33));
var _35=_30.duplicate();
if(_34){
_35.setEndPoint("EndToEnd",_33);
}
_33.setEndPoint("EndToEnd",_30);
var _36=_30.duplicate();
_36.setEndPoint("EndToStart",_33);
var _37=_36.htmlText+_33.htmlText;
_30.pasteHTML("");
_30.moveEnd("character",-1);
_30.moveStart("character",1);
var _38=_30.duplicate();
_38.collapse();
_30.pasteHTML(this.GetFormatHtml(_37,_2f));
_38.setEndPoint("EndToEnd",_30);
},ProcessElementElementSelection:function(_39,_3a,_3b,_3c){
var _3d=_3a.duplicate();
_3d.moveToElementText(_3b);
var _3e=_3a.duplicate();
_3e.moveToElementText(_3c);
var _3f=(0!=_3a.compareEndPoints("StartToStart",_3d));
var _40=(0!=_3a.compareEndPoints("EndToEnd",_3e));
var _41=_3a.duplicate();
_41.setEndPoint("StartToStart",_3d);
_41.setEndPoint("EndToEnd",_3e);
if(!_3f&&!_40){
var _42=_3a.htmlText;
_3a.pasteHTML("");
var _43=_3a.duplicate();
_43.collapse();
_3a.pasteHTML(this.GetFormatHtml(_42,_39));
_41.setEndPoint("EndToEnd",_3a);
}else{
var _44=_3a.htmlText;
_3a.pasteHTML("");
var _41=_3a.duplicate();
_41.collapse();
_3e.setEndPoint("StartToStart",_3a);
var _45=_3e.htmlText;
_3e.pasteHTML("");
_3a.pasteHTML(this.GetFormatHtml(_44,_39)+_45);
_41.setEndPoint("EndToEnd",_3a);
}
},ClearStyleMoz:function(){
var _46=RadEditorNamespace.RadSelection.New(this.Window);
var _47=_46.GetParentElement();
if(_47.tagName!="FONT"){
_47.className="";
return true;
}else{
return this.Window.document.execCommand("RemoveFormat",false,null);
}
},GetValueMoz:function(_48){
if(!_48){
return "";
}
var _49=_48.getSelection();
if(!_49){
return;
}
if(_49.rangeCount!=1){
return "";
}
var _4a=(_49.focusNode.nodeType!=3?_49.focusNode:_49.focusNode.parentNode);
var _4b=(_49.anchorNode.nodeType!=3?_49.anchorNode:_49.anchorNode.parentNode);
if(_4a!=_4b){
return "";
}
var _4c=RadEditorNamespace.RadSelection.New(_48);
var _4d="",_4e;
if(null!=_4c&&null!=(_4e=_4c.GetParentElement())){
_4d=_4e.className;
}
return (""==_4d?"":_4d);
},ApplyStyleSafari:function(_4f){
var _50=this.Window.getSelection();
var _51=RadEditorNamespace.RadSelection.New(this.Window).GetRange();
var _52=_50.baseNode?_50.baseNode:_51.startContainer;
var _53=_50.extentNode?_50.extentNode:_51.endContainer;
if(_52.nodeType==3||_53.nodeType==3){
var _54=this.Window.document.createElement("span");
_54.className=_4f;
var _55=RadEditorNamespace.RadSelection.New(this.Window).GetHtmlText();
_54.innerHTML=_55;
RadEditorNamespace.RadPasteHtmlCommand.New().InsertNodeAtSelection(this.Window,_54);
}else{
var _56=RadEditorNamespace.RadSelection.New(this.Window);
_56.GetParentElement().className=_4f;
}
return true;
},ApplyStyleMoz:function(_57){
try{
if(TelerikNamespace.Utils.DetectBrowser("safari")){
return this.ApplyStyleSafari(_57);
}
}
catch(ex){
}
var _58=this.Window.getSelection();
if(_58.rangeCount<1){
return;
}
var _59=_58.getRangeAt(0);
var _5a=_59.commonAncestorContainer.parentNode;
var _5b=this.Window.document.createElement("SPAN");
_5b.appendChild(_59.cloneContents());
var _5c=_5b.innerHTML;
var _5d=true;
if(_5c==_5a.innerHTML){
_5d=false;
}
var _5e=_59.commonAncestorContainer;
if(_5e.tagName=="UL"||_5e.tagName=="OL"){
var _5f=_5e.innerHTML;
var _60=_5f.match(/<LI\/?>/gi).length;
var _61=_60?_60.length:0;
if(_61>1){
RadEditorNamespace.Utils.SelectElement(this.Window,_5e);
_5d=false;
}
}else{
if(_5a.tagName=="LI"){
if(_5c==_5a.innerHTML&&_5a.parentNode.childNodes.length==1){
RadEditorNamespace.Utils.SelectElement(this.Window,_5a.parentNode);
_5d=false;
}
}
}
if(_5d&&(_59.startContainer.nodeType==3||_59.endContainer.nodeType==3)){
var _62=this.Window.document.createElement("SPAN");
_62.className=_57;
_62.appendChild(_59.extractContents());
_59.insertNode(_62);
}else{
var _63=RadEditorNamespace.RadSelection.New(this.Window);
_63.GetParentElement().className=_57;
}
return true;
}};;RadEditorNamespace.RadTableCommandBase={New:function(_1,_2,_3){
var _4=RadEditorNamespace.RadCommandBase.New(_1,_2,_3);
RadEditorNamespace.Utils.ExtendObject(_4,this);
return _4;
},GetState:function(_5){
return this.GetSelectedCell(_5)?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
},GetSelectedCell:function(_6){
var _7=RadEditorNamespace.RadSelection.New(_6||this.Window);
var _8;
if(_7){
_8=_7.GetParentElement();
}
while(null!=_8&&_8.tagName!="TD"&&_8.tagName!="TH"&&_8.tagName!="BODY"){
_8=_8.parentNode;
}
if(!_8||!_8.tagName){
return null;
}
return (_8.tagName=="TD"||_8.tagName=="TH"?_8:null);
},GetSelectedRow:function(_9){
var _a=RadEditorNamespace.RadSelection.New(_9||this.Window);
var _b;
if(_a){
_b=_a.GetParentElement();
}
if(!_b){
return null;
}
while(null!=_b&&_b.tagName!="TR"&&_b.tagName!="BODY"){
_b=_b.parentNode;
}
return (_b&&_b.tagName=="TR"?_b:null);
},GetParentTable:function(_c){
if(!_c){
return null;
}
while(null!=_c&&_c.parentNode!=_c&&"TABLE"!=_c.tagName){
_c=_c.parentNode;
}
return (_c&&_c.tagName=="TABLE"?_c:null);
},GetNextSiblingCell:function(_d){
if(!_d){
return null;
}
var _e=_d.parentNode;
var _f=_d.cellIndex+1;
if(0<=_f&&_f<_e.cells.length){
return _e.cells[_f];
}
return null;
},GetPreviouseSiblingCell:function(_10){
if(!_10){
return null;
}
var row=_10.parentNode;
var _12=_10.cellIndex-1;
if(0<=_12&&_12<row.cells.length){
return row.cells[_12];
}
return null;
}};
RadEditorNamespace.RadTableInsertRow={New:function(_13,_14,_15){
var obj=RadEditorNamespace.RadTableCommandBase.New((_13||"Insert row"),true,_14);
RadEditorNamespace.Utils.ExtendObject(obj,this);
obj.Direction=_15||"above";
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableInsertRow.New(this.Title,this.Window,this.Direction);
},GetState:function(_17){
return (this.GetSelectedRow(_17)?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
},OnExecute:function(){
var _18=this.GetSelectedCell();
if(!_18){
return false;
}
var row=_18.parentNode;
var _1a=row.rowIndex;
if("below"==this.Direction){
_1a++;
}
var _1b=this.GetParentTable(row);
if(!_1b){
return false;
}
var _1c=_1b.insertRow(_1a);
if(!_1c){
return false;
}
RadEditorNamespace.Utils.MergeElementAttributes(row,_1c);
var _1d;
for(var i=0;i<row.cells.length;i++){
_18=row.cells[i];
_1d=_1c.insertCell(_1c.cells.length);
_1d.colSpan=_18.colSpan;
RadEditorNamespace.Utils.MergeElementAttributes(_18,_1d);
_1d.innerHTML=document.all?"":"&nbsp;";
}
return true;
}};
RadEditorNamespace.RadTableDeleteRow={New:function(_1f,_20){
var obj=RadEditorNamespace.RadTableCommandBase.New(_1f,true,_20);
RadEditorNamespace.Utils.ExtendObject(obj,this);
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableDeleteRow.New(this.Title,this.Window);
},GetState:function(_22){
return (this.GetSelectedRow(_22)?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
},OnExecute:function(){
var row=this.GetSelectedRow();
if(!row){
return false;
}
row.parentNode.removeChild(row);
return true;
}};
RadEditorNamespace.RadTableInsertColumn={New:function(_24,_25,_26){
var obj=RadEditorNamespace.RadTableCommandBase.New((_24||"Insert column"),true,_25);
RadEditorNamespace.Utils.ExtendObject(obj,this);
obj.Direction=_26||"left";
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableInsertColumn.New(this.Title,this.Window,this.Direction);
},GetState:function(_28){
return (this.GetSelectedCell(_28)?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
},OnExecute:function(){
var _29=this.GetSelectedCell();
if(!_29){
return false;
}
var _2a=_29.cellIndex;
if("right"==this.Direction){
_2a++;
}
var _2b=this.GetParentTable(_29);
if(!_2b){
return false;
}
var _2c=_2b.rows;
var _2d;
for(var i=0;i<_2c.length;i++){
_2d=_2c[i].insertCell(_2a);
RadEditorNamespace.Utils.MergeElementAttributes(_29,_2d);
_2d.innerHTML=document.all?"":"&nbsp;";
}
return true;
}};
RadEditorNamespace.RadTableDeleteColumn={New:function(_2f,_30){
var obj=RadEditorNamespace.RadTableCommandBase.New(_2f,true,_30);
RadEditorNamespace.Utils.ExtendObject(obj,this);
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableDeleteColumn.New(this.Title,this.Window);
},GetState:function(_32){
return (this.GetSelectedCell(_32)?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
},OnExecute:function(){
var _33=this.GetSelectedCell();
if(!_33){
return false;
}
var _34=_33.cellIndex;
var _35=this.GetParentTable(_33);
if(!_35){
return false;
}
var _36=_35.rows;
for(var i=0;i<_36.length;i++){
_33=_36[i].cells[_34];
if(_33){
_33.parentNode.removeChild(_33);
}
}
return true;
}};
RadEditorNamespace.RadTableMergeRows={New:function(_38,_39){
var obj=RadEditorNamespace.RadTableCommandBase.New(_38,true,_39);
RadEditorNamespace.Utils.ExtendObject(obj,this);
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableMergeRows.New(this.Title,this.Window);
},GetState:function(_3b){
var _3c=this.GetSelectedCell(_3b);
if(null!=_3c&&null!=this.GetLowerCell(_3c)&&1==_3c.colSpan){
return RadEditorNamespace.RADCOMMAND_STATE_OFF;
}else{
return RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
}
},OnExecute:function(){
var _3d=this.GetSelectedCell();
if(!_3d){
return false;
}
var _3e=this.GetLowerCell(_3d);
if(!_3e){
return false;
}
if(""!=_3e.innerHTML){
if(""!=_3d.innerHTML){
_3d.innerHTML+="<br>";
}
_3d.innerHTML+=_3e.innerHTML;
}
_3d.rowSpan+=_3e.rowSpan;
_3e.parentNode.removeChild(_3e);
return true;
},GetLowerCell:function(_3f){
if(!_3f){
return null;
}
var _40=this.GetParentTable(_3f);
var row=_3f.parentNode;
var _42=_40.rows[row.rowIndex+_3f.rowSpan];
if(!_42){
return null;
}
var _43=_42.cells[_3f.cellIndex];
return _43;
}};
RadEditorNamespace.RadTableMergeColumns={New:function(_44,_45){
var obj=RadEditorNamespace.RadTableCommandBase.New(_44,true,_45);
RadEditorNamespace.Utils.ExtendObject(obj,this);
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableMergeColumns.New(this.Title,this.Window);
},GetState:function(_47){
var _48=this.GetSelectedCell(_47);
if(null!=_48&&null!=this.GetNextSiblingCell(_48)){
return RadEditorNamespace.RADCOMMAND_STATE_OFF;
}else{
return RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
}
},OnExecute:function(){
var _49=this.GetSelectedCell();
if(null==_49){
return false;
}
var _4a=this.GetNextSiblingCell(_49);
if(!_4a){
return false;
}
_49.colSpan+=_4a.colSpan;
if(""!=_4a.innerHTML){
if(""!=_49.innerHTML){
_49.innerHTML+="<br>";
}
_49.innerHTML+=_4a.innerHTML;
}
_4a.parentNode.removeChild(_4a);
return true;
}};
RadEditorNamespace.RadTableSplitCell={New:function(_4b,_4c){
var obj=RadEditorNamespace.RadTableCommandBase.New(_4b,true,_4c);
RadEditorNamespace.Utils.ExtendObject(obj,this);
obj.NewRows=2;
obj.NewColumns=2;
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableSplitCell.New(this.Title,this.Window);
},GetState:function(_4e){
var _4f=this.GetSelectedCell(_4e);
if(!_4f){
return RadEditorNamespace.RADCOMMAND_STATE_DISABLED;
}
return ((_4f.colSpan>1||_4f.rowSpan>1)?RadEditorNamespace.RADCOMMAND_STATE_OFF:RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
},OnExecute:function(){
var _50=this.GetSelectedCell();
if(!_50){
return false;
}
var _51=this.GetParentTable(_50);
if(!_51){
return false;
}
var row=_50.parentNode;
var _53=("THEAD"==row.parentNode.tagName.toUpperCase()?"TH":"TD");
if(_50.colSpan>1){
for(i=1;i<this.NewColumns;i++){
var _54=this.Window.document.createElement(_53);
_54.innerHTML=document.all?"":"&nbsp;";
RadEditorNamespace.Utils.MergeElementAttributes(_50,_54);
_54.colSpan=1;
if(_50.cellIndex+1<row.cells.length){
row.insertBefore(_54,row.cells[_50.cellIndex+1]);
}else{
row.appendChild(_54);
}
_50.colSpan--;
}
}
if(_50.rowSpan>1){
for(i=1;i<this.NewRows;i++){
var _55=_51.rows[row.rowIndex+_50.rowSpan-1];
if(!_55||0==_55.cells.length){
break;
}
var _54=this.Window.document.createElement(_53);
_54.innerHTML=document.all?"":"&nbsp;";
RadEditorNamespace.Utils.MergeElementAttributes(_50,_54);
_54.rowSpan=1;
if(_50.cellIndex+1<_55.cells.length){
_55.insertBefore(_54,_55.cells[_50.cellIndex+1]);
}else{
_55.appendChild(_54);
}
_50.rowSpan--;
}
}
return true;
}};
RadEditorNamespace.RadTableDeleteCell={New:function(_56,_57){
var obj=RadEditorNamespace.RadTableCommandBase.New(_56,true,_57);
RadEditorNamespace.Utils.ExtendObject(obj,this);
return obj;
},Clone:function(){
return RadEditorNamespace.RadTableDeleteCell.New(this.Title,this.Window);
},OnExecute:function(){
var _59=this.GetSelectedCell();
if(!_59){
return false;
}
_59.parentNode.removeChild(_59);
return true;
}};;RadEditorNamespace.RadTextTypeCommand={New:function(_1,_2){
var _3=RadEditorNamespace.RadGenericCommand.New((_1||"Typing"),_2);
RadEditorNamespace.Utils.ExtendObject(_3,this);
return _3;
},Update:function(){
if(this.RestorePoint2){
this.RestorePoint2.Update();
}
}};;RadEditorNamespace.UpdateCommandsArray={};
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_INSERT_ORDERED_LIST]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_INSERT_ORDERED_LIST);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_INSERT_UNORDERED_LIST]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_INSERT_UNORDERED_LIST);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_UNLINK]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_UNLINK);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_BOLD]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_BOLD);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_ITALIC]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_ITALIC);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_UNDERLINE]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_UNDERLINE);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_FORECOLOR]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_FORECOLOR);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_BACKCOLOR]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_BACKCOLOR);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_FONTNAME]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_FONTNAME);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_FONTSIZE]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_FONTSIZE);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_REAL_FONTSIZE]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_REAL_FONTSIZE);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_PASTE]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_PASTE);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_CUT]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_CUT);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_COPY]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_COPY);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_JUSTIFY_LEFT]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_JUSTIFY_LEFT);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_JUSTIFY_RIGHT]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_JUSTIFY_RIGHT);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_JUSTIFY_CENTER]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_JUSTIFY_CENTER);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_JUSTIFY_NONE]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_JUSTIFY_NONE);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_INDENT]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_INDENT);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_OUTDENT]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_OUTDENT);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_UNLINK]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_UNLINK);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK]=RadEditorNamespace.RadFormatBlockCommand.New(null,null,null);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_APPLY_CLASS]=RadEditorNamespace.RadStyleCommand.New();
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_INSERT_ROW_ABOVE]=RadEditorNamespace.RadTableInsertRow.New(null,null,"above");
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_INSERT_ROW_BELOW]=RadEditorNamespace.RadTableInsertRow.New(null,null,"below");
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_LEFT]=RadEditorNamespace.RadTableInsertColumn.New(null,null,"left");
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_RIGHT]=RadEditorNamespace.RadTableInsertColumn.New(null,null,"right");
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_DELETE_ROW]=RadEditorNamespace.RadTableDeleteRow.New(null,null);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_DELETE_COLUMN]=RadEditorNamespace.RadTableDeleteColumn.New(null,null);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_DELETE_CELL]=RadEditorNamespace.RadTableDeleteCell.New(null,null);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_MERGE_COLUMNS]=RadEditorNamespace.RadTableMergeColumns.New(null,null);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_MERGE_ROWS]=RadEditorNamespace.RadTableMergeRows.New(null,null);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_SPLIT_CELL]=RadEditorNamespace.RadTableSplitCell.New(null,null);
if("undefined"==typeof (RadEditorToolInitializer)){
var RadEditorToolInitializer={};
}
if("undefined"==typeof (RadEditorCommandList)){
var RadEditorCommandList={};
}
RadEditorCommandList["InsertAnchor"]=RadEditorCommandList["InsertEmailLink"]=function(_1,_2,_3){
var _3={SelectedTab:("InsertAnchor"==_1?1:2)};
_2.Fire("LinkManager",_3);
};
RadEditorCommandList["IncreaseSize"]=RadEditorCommandList["DecreaseSize"]=function(_4,_5,_6){
if(true==_5.ToggleFullScreen){
return;
}
var _7=70;
var _8=(_4=="IncreaseSize");
var _9=RadEditorNamespace.Utils.GetRect(_5.WrapperElement);
var _a=_9.width+(_8?_7:-_7);
var _b=_9.height+(_8?_7:-_7);
if(_a<0||_b<0){
return;
}
_5.SetSize(_a,_b);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_TAB]=function(_c,_d,_e){
if(!_d.EnableTab){
return;
}
var _f=_d.GetSelectedElement();
if(_f.tagName=="LI"){
_d.Fire(RadEditorNamespace.RADCOMMAND_INDENT);
}else{
_d.PasteHtml(" &nbsp;&nbsp;&nbsp;&nbsp;");
}
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_BOLD]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_ITALIC]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_UNDERLINE]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_JUSTIFY_LEFT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_JUSTIFY_RIGHT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_JUSTIFY_CENTER]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_JUSTIFY_NONE]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INDENT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_OUTDENT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SELECT_ALL]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_UNLINK]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_JUSTIFY_FULL]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_STRIKETHROUGH]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SUBSCRIPT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SUPERSCRIPT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_ABSOLUTE_POSITION]=function(_10,_11,_12){
var _13=RadEditorNamespace.RADCOMMAND_SELECT_ALL!=_10;
_11.SetActive();
if(_11.Document.selection){
var rng=_11.Document.selection.createRange();
rng.select();
}
_11.ExecuteBrowserCommand(_10,_13,null);
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_FORECOLOR]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_BACKCOLOR]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_FONTNAME]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_FONTSIZE]=function(_15,_16,_17){
var _18=_17.GetSelectedValue();
var _19=_16.GetSelection().GetParentElement();
if(_15==RadEditorNamespace.RADCOMMAND_FONTSIZE&&_19&&_19.tagName=="FONT"){
RadEditorNamespace.Utils.RemoveElementStyleAttribute(_19,"fontSize");
}
_16.ExecuteBrowserCommand(_15,true,_18);
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_REAL_FONTSIZE]=function(_1a,_1b,_1c){
var _1d=_1c.GetSelectedValue();
var _1e=RadEditorNamespace.RadGenericCommand.New("Set real font size",_1b.ContentWindow);
var _1f=RadEditorNamespace.MarkEditorSelection(_1b);
var _20=_1f.markedElements;
var _21=_1b.CreateRestorePoint();
var _22="";
var _23=_1b.GetSelection().GetParentElement();
if(_1b.IsIE&&_20.length==0&&!_1b.GetSelectionHtml()&&(_23.tagName=="FONT"||_23.tagName=="SPAN")){
_1b.Document.execCommand("RemoveFormat",null,false);
}
if(_20.length==0){
_1b.PasteHtml(_22+"<font style='font-size:"+_1d+"' id='radERealFont'>&nbsp;</font>");
var _24=_1b.Document.getElementById("radERealFont");
_24.removeAttribute("id");
if(_1b.IsIE){
_1b.SelectElement(_24);
_1b.GetSelection().Collapse();
_24.innerHTML="";
}else{
if(_1b.ContentWindow.getSelection){
var _25=_1b.ContentWindow.getSelection();
var _26=_1b.GetSelection().GetRange();
_25.removeAllRanges();
if(_26&&_26.selectNodeContents){
_26.selectNodeContents(_24);
}
_25.addRange(_26);
}
}
return;
}
for(var i=0;i<_20.length;i++){
_20[i].style.fontSize=_1d;
_20[i].removeAttribute("size");
}
if(_21){
_21.Select();
}
_1b.ExecuteCommand(_1e);
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_CONVERT_TO_LOWER]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_CONVERT_TO_UPPER]=function(_28,_29,_2a){
var _2b;
var _2c;
if(document.all){
if(_29.Document.selection.type.toLowerCase()=="control"){
return;
}
var _2d=_29.Document.selection.createRange();
_2b=_2d.duplicate();
_2c=_2d.duplicate();
_2b.collapse();
_2c.collapse(false);
}
var _2e=RadEditorNamespace.MarkEditorSelection(_29);
var _2f=(RadEditorNamespace.RADCOMMAND_CONVERT_TO_LOWER==_28)?"Convert to lower":"Convert to upper";
var _30=RadEditorNamespace.RadGenericCommand.New(_2f,_29.ContentWindow);
var _31=_2e.markedElements;
var _32=_2e.newElements;
for(var i=0;i<_31.length;i++){
changeChildNodesCase(_31[i]);
}
for(var i=0;i<_32.length;i++){
if(document.all){
_32[i].removeNode(false);
}else{
var _34=document.createRange();
_34.selectNodeContents(_32[i]);
_32[i].parentNode.replaceChild(_34.extractContents(),_32[i]);
}
}
if(document.all&&!window.opera){
var _35=_29.Document.selection.createRange();
_35.setEndPoint("StartToStart",_2b);
_35.setEndPoint("EndToEnd",_2c);
_35.select();
}else{
var _36=_29.ContentWindow.getSelection();
var rng=_36.getRangeAt(0);
rng.collapse(true);
}
_29.ExecuteCommand(_30);
function changeChildNodesCase(_38){
var _39=_38.childNodes;
for(var i=0;i<_39.length;i++){
if(_39[i].nodeType==3){
_39[i].nodeValue=(RadEditorNamespace.RADCOMMAND_CONVERT_TO_LOWER==_28)?_39[i].nodeValue.toLowerCase():_39[i].nodeValue.toUpperCase();
}else{
if(_39[i].nodeType==1&&_39[i].tagName.toUpperCase()!="FONT"){
changeChildNodesCase(_39[i]);
}
}
}
}
};
RadEditorNamespace.MarkEditorSelection=function(_3b){
if(_3b.GetHtml()==""){
return {markedElements:[],newElements:[]};
}
var _3c="AZBY";
var _3d="_cm";
var _3e=[];
var _3f=[];
var _40=[];
var _41=_3b.GetSelection();
if(_41.IsControl()){
var _42=_41.GetParentElement();
var _43=_3b.Document.createElement("FONT");
_43.appendChild(_42.cloneNode(true));
_42.parentNode.replaceChild(_43,_42);
return {markedElements:[_43],newElements:[_43]};
}
var _44=TelerikNamespace.Utils.DetectBrowser("safari")?"span":"font";
keepFontNames();
var _45=_3b.ContentArea;
if(!document.all){
_45.ownerDocument.execCommand("UseCSS",false,true);
}
_45.ownerDocument.execCommand("FontName",false,_3c);
var _46=_45.getElementsByTagName(_44);
for(var i=0;i<_46.length;i++){
var _43=_46[i];
if(_43.getAttribute("face")==_3c||_43.style.fontFamily==_3c){
_43.removeAttribute("face");
if(_43.style.fontFamily==_3c){
_43.style.fontFamily="";
}
_3f.push(_43);
var _48=_43.getElementsByTagName(_44);
for(var j=0;j<_48.length;j++){
var _o=_48[j];
if(_o.getAttribute("face")!=_3c){
_3f.push(_o);
}
}
if(!_43.getAttribute(_3d)){
_40.push(_43);
}
}
_43.removeAttribute(_3d);
}
restoreFontNames();
if(!document.all){
_45.ownerDocument.execCommand("UseCSS",false,false);
}
function keepFontNames(){
var _4b=_3b.ContentArea.getElementsByTagName(_44);
for(var i=0;i<_4b.length;i++){
var _4d=_4b[i];
if(_4b[i].face){
_4d.setAttribute("_face",_4d.face);
_3e.push(_4d);
}
_4d.setAttribute(_3d,1);
}
}
function restoreFontNames(){
for(var i=0;i<_3e.length;i++){
_3e[i].face=_3e[i].getAttribute("_face");
_3e[i].removeAttribute("_face");
}
_3e=[];
}
return {markedElements:_3f,newElements:_40};
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_COPY]=function(_4f,_50,_51){
if(null!=_51){
var _52=window.opera?false:true;
if(_52){
try{
document.queryCommandEnabled(_4f);
}
catch(e){
_52=false;
}
}
if(_52){
_50.Document.execCommand(_4f,false,null);
}else{
alert(_50.Localization["UseCtrl_C"]);
}
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_CUT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_PASTE]=function(_53,_54,_55){
if(null!=_55){
var _56=window.opera?false:true;
if(_56){
try{
_54.ToolsUpdate=true;
document.queryCommandEnabled(_53);
_54.ToolsUpdate=false;
}
catch(e){
_56=false;
}
}
if(_53==RadEditorNamespace.RADCOMMAND_PASTE&&_56&&_54.IsIE7&&_54.Document.createEventObject){
_54.Document.body.fireEvent("onbeforepaste");
}
if(_56){
var _57=RadEditorNamespace.RadGenericCommand.New(_54.Localization[_53],_54.ContentWindow);
_54.Document.execCommand(_53,false,null);
_54.ExecuteCommand(_57);
}else{
var id=(_53==RadEditorNamespace.RADCOMMAND_CUT?"UseCtrl_X":"UseCtrl_V");
alert(_54.Localization[id]);
}
}else{
_54.PendingCommand=RadEditorNamespace.RadGenericCommand.New(_54.Localization[_53],_54.ContentWindow);
}
};
RadEditorCommandList["Enter"]=function(_59,_5a,_5b){
if(!_5a.NewLineBr){
if(_5a.IsIE){
_5a.ShortcutHit=false;
return false;
}else{
if(!_5a.IsSafari&&!window.opera){
var _5c=_5a.GetSelectedElement();
if("LI"==_5c.tagName||RadEditorNamespace.Utils.GetElementParentByTag(_5c,"LI")!=null){
_5a.ShortcutHit=false;
return false;
}
var _5d=_5a.ContentWindow;
var _5e=_5d.document;
function checkParent(_5f,_60){
_60[_60.length]=_5f;
while(_5f!=null&&_5f.tagName!="P"){
if(_5f.tagName=="TD"){
return null;
}
_5f=_5f.parentNode;
_60[_60.length]=_5f;
}
return _5f;
}
var _61=_5d.getSelection();
var _62=_61.getRangeAt(0);
var _63=_62.cloneRange();
_62.deleteContents();
var _64=_62.startOffset;
var _65=_62.startContainer;
var _66="";
var _67=[];
var inP=checkParent(_65,_67);
if(inP){
var _69=(inP.innerHTML=="");
var _6a=_62.cloneRange();
_6a.setStart(inP,0);
_6a.setEnd(_65,_64);
var _6b=_6a.cloneContents();
var _6c=_62.cloneRange();
_6c.setStart(_65,_64);
if(inP.lastChild){
_6c.setEndAfter(inP.lastChild);
}else{
_6c.setEnd(inP,0);
}
var _6d=_6c.cloneContents();
_62.selectNode(inP);
_61.removeAllRanges();
_61.addRange(_62);
inP=inP.cloneNode(true);
inP.innerHTML="";
var _6e=inP.cloneNode(true);
if(_69){
_6e.innerHTML="&nbsp;";
}else{
_6e.appendChild(_6b);
}
endPar=inP.cloneNode(true);
endPar.appendChild(_6d);
var _6f=inP.cloneNode(true);
var _70=_6f;
if(_67.length>0){
for(var i=_67.length;i>0;i--){
var _72=_67[i];
var _73=_72&&_72.cloneNode?_72.cloneNode(false):null;
if(_73&&_73.tagName!="P"){
_70.appendChild(_73);
_70=_73;
}
}
}
_70.innerHTML="&nbsp;";
_70.setAttribute("id","radETempNode");
var _74=_5e.createElement("div");
_74.appendChild(_6e);
_74.appendChild(_6f);
if(!_69){
_74.appendChild(endPar);
}
_66=_74.innerHTML;
}else{
_66="<p id='radETempNode'>&nbsp;</p>";
}
RadEditorNamespace.RadPasteHtmlCommand.New("NewLineBr",_5a.ContentWindow,_66,false).OnExecuteMoz();
oP=_5e.getElementById("radETempNode");
if(oP){
oP.removeAttribute("id",0);
var _61=_5d.getSelection();
var _62=_5e.createRange();
_62.selectNodeContents(oP);
_61.removeAllRanges();
_61.addRange(_62);
}
var ps=_5e.getElementsByTagName("P");
for(var i=0;i<ps.length;i++){
var _76=ps[i].innerHTML;
if(_76==""||RadEditorNamespace.Utils.Trim(_76).toLowerCase()=="<br>"){
ps[i].parentNode.removeChild(ps[i]);
}
}
return false;
}
}
}else{
if(_5a.IsIE){
try{
var _5c=_5a.GetSelectedElement();
if("LI"==_5c.tagName||RadEditorNamespace.Utils.GetElementParentByTag(_5c,"LI")!=null){
_5a.ShortcutHit=false;
return false;
}
var _77=RadEditorNamespace.RadGenericCommand.New("Enter Pressed",_5a.ContentWindow);
var _62=_5a.Document.selection.createRange();
if(_62.pasteHTML){
_62.pasteHTML("<br>");
_62.select();
_62.moveEnd("character",1);
_62.moveStart("character",1);
_62.collapse(false);
}else{
if(_62(0)){
_62.execCommand("Delete");
}
}
_5a.ExecuteCommand(_77);
}
catch(exc){
alert(exc.message);
}
}
}
};
RadEditorCommandList["ShiftEnter"]=function(_78,_79,_7a){
if(!_79.NewLineBr||!document.all){
_79.ShortcutHit=false;
return false;
}
var _7b=_79.GetSelectedElement();
if("LI"==_7b.tagName||RadEditorNamespace.Utils.GetElementParentByTag(_7b,"LI")!=null){
var _7c=RadEditorNamespace.RadGenericCommand.New("Enter Pressed",_79.ContentWindow);
var _7d=_79.Document.selection.createRange();
_7d.pasteHTML("<br>");
_7d.select();
_7d.moveEnd("character",1);
_7d.moveStart("character",1);
_7d.collapse(false);
_79.ExecuteCommand(_7c);
return false;
}
_79.ShortcutHit=false;
return false;
};
RadEditorNamespace.RadExpandSelection=function(_7e,_7f,_80){
var _81=_7e.duplicate();
var _82=null;
var _83=null;
for(var i=_7f.length-1;i>=0;i--){
_81.moveToElementText(_7f[i]);
var _85=_7e.compareEndPoints("StartToStart",_81);
if(-1==_85||0==_85){
if(_80[_7f[i].tagName]!=null){
_83=_7f[i];
}
}else{
if(_80[_7f[i].tagName]!=null){
_82=_7f[i];
break;
}
}
}
if(_82){
_81.moveToElementText(_82);
_7e.setEndPoint("StartToEnd",_81);
}else{
var _86=_7e.parentElement();
_81.moveToElementText(_86);
_7e.setEndPoint("StartToStart",_81);
}
if(_83){
_81.moveToElementText(_83);
if("BR"==_83.tagName){
_7e.setEndPoint("EndToEnd",_81);
}else{
_7e.setEndPoint("EndToStart",_81);
}
}else{
var _86=_7e.parentElement();
_81.moveToElementText(_86);
_7e.setEndPoint("EndToEnd",_81);
}
_7e.select();
};
RadEditorNamespace.HandleEmptyListSelection=function(_87,_88,_89){
var _8a=_87.parentElement();
var _8b=(_8a&&"P"==_8a.tagName.toUpperCase());
var brs=_8a.getElementsByTagName("BR");
if(_8b&&brs.length==0){
RadEditorNamespace.RadEditorInsertList(_89,_88);
return false;
}else{
var _8d={};
_8d["BR"]="";
_8d["TD"]="";
_8d["OL"]="";
_8d["UL"]="";
_8d["TABLE"]="";
_8d["DIV"]="";
_8d["IMG"]="";
_8d["OBJECT"]="";
var _8e=_87.parentElement().getElementsByTagName("*");
RadEditorNamespace.RadExpandSelection(_87,_8e,_8d);
if(_87.htmlText){
_88.Fire(_89);
}
}
};
RadEditorNamespace.RadEditorInsertList=function(_8f,_90,_91){
var _92=true;
_90.SetActive();
_90.ExecuteBrowserCommand(_8f,_92,null);
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_ORDERED_LIST]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_UNORDERED_LIST]=function(_93,_94,_95){
_94.SetFocus();
var _96=_93;
if(!_94.NewLineBr||!_94.IsIE){
RadEditorNamespace.RadEditorInsertList(_96,_94);
return false;
}
var _97=_94.GetSelectedElement();
var _98=_94.Document.selection.createRange();
if("OL"==_97.tagName||RadEditorNamespace.Utils.GetElementParentByTag(_97,"OL")!=null||"UL"==_97.tagName||RadEditorNamespace.Utils.GetElementParentByTag(_97,"UL")!=null){
RadEditorNamespace.RadEditorInsertList(_96,_94);
return false;
}else{
if("TD"==_97.tagName||"TR"==_97.tagName||"TBODY"==_97.tagName||"TABLE"==_97.tagName){
var _98=_94.Document.selection.createRange();
var _99=_98.parentElement().getElementsByTagName("TD");
for(var i=_99.length-1;i>=0;i--){
brRange=_98.duplicate();
brRange.moveToElementText(_99[i]);
if(_98.inRange(brRange)&&_99[i].innerHTML!=""){
_98.moveToElementText(_99[i]);
}
}
}
}
_94.ContentArea.setActive();
var _9b=document.selection;
var _9c=(_96=="InsertOrderedList")?"OL":"UL";
if(_9b.type=="Control"){
var _9d=_94.Document.body.createTextRange();
_9d.moveToElementText(_98(0));
if((RadEditorNamespace.Utils.Trim(_9d.parentElement().tagName.toLowerCase())=="table")||(RadEditorNamespace.Utils.Trim(_9d.parentElement().tagName.toLowerCase())=="tbody")){
var _9e=_9d.parentElement().parentNode.outerHTML;
_9d.parentElement().parentNode.outerHTML="<"+_9c+"><LI>"+_9e+"</LI></"+_9c+">";
}else{
var _9e=_9d.htmlText;
_94.PasteHtml("<"+_9c+"><LI>"+_9e+"</LI></"+_9c+">");
}
}else{
var _9f=_94.Document.createElement("SPAN");
_9f.innerHTML=_98.htmlText;
if(_98.htmlText==""){
RadEditorNamespace.HandleEmptyListSelection(_98,_94,_96);
}else{
if(_9f.getElementsByTagName("P").length>0){
RadEditorNamespace.RadEditorInsertList(_96,_94);
return false;
}else{
var _a0;
if(_98.parentElement().tagName.toUpperCase()=="LI"){
_a0=_98.parentElement().parentNode;
}else{
_a0=_98.parentElement();
}
if(_a0.tagName.toUpperCase()=="OL"||_a0.tagName.toUpperCase()=="UL"){
var _a1=_a0.tagName.toUpperCase();
if(_9c==_a1){
if(_94.NewLineBr){
var _a2=_98.duplicate();
var _a3=_98.duplicate();
_a3.moveToElementText(_a0);
var _a4=_98.duplicate();
var _a5=_a0.getElementsByTagName("LI");
var _a6=0;
var _a7=_a5.length-1;
var _a8=_98.duplicate();
_a8.moveToElementText(_a5[0]);
var _a9=_98.duplicate();
_a9.moveToElementText(_a5[_a5.length-1]);
_a4.setEndPoint("EndToEnd",_a9);
_a4.setEndPoint("StartToStart",_a8);
while((_a6<_a5.length)&&(_a4.compareEndPoints("StartToStart",_a2)<=0)){
_a4.moveToElementText(_a5[_a6]);
_a4.setEndPoint("EndToEnd",_a9);
_a6++;
}
_a6-=2;
while((_a7>0)&&(_a4.compareEndPoints("EndToEnd",_a2)>=0)){
_a4.moveToElementText(_a5[_a7]);
_a4.setEndPoint("StartToStart",_a8);
_a7--;
}
_a7+=2;
var _aa=_98.duplicate();
var _ab=_98.duplicate();
_aa.moveToElementText(_a5[_a6]);
_aa.collapse(true);
_aa.setEndPoint("StartToStart",_a3);
_ab.moveToElementText(_a5[_a7]);
_ab.collapse(false);
_ab.setEndPoint("EndToEnd",_a3);
_98.setEndPoint("StartToEnd",_aa);
_98.setEndPoint("EndToStart",_ab);
var _ac="";
var _ad=false;
var _ae=false;
if(_aa.htmlText.replace(/<(.*?)>/)!=""){
_ac+="<"+_a1+">"+_aa.htmlText+"</"+_a1+">";
}else{
_ad=true;
}
_ac+=_98.htmlText.replace(/<LI\/?>/gi,"<BR>").replace(/<\/LI>/gi,"").replace(/^\s*<BR\/?>/gi,"").replace(/<BR\/?>\s*$/gi,"");
if(_ab.htmlText.replace(/<(.*?)>/)!=""){
_ac+="<"+_a1+">"+_ab.htmlText+"</"+_a1+">";
}else{
_ae=true;
_ac+="<BR>";
}
if(_ad&&_ae){
_ac=_ac.replace(new RegExp("</?"+_a1+"/?>","gi"),"").replace(/^\s*<BR\/?>/gi,"");
}
if(_a0.parentNode.childNodes[0]==_a0){
_a3.collapse();
_a0.parentNode.removeChild(_a0,_ac);
}else{
_a3.moveStart("character",-1);
}
_94.PasteHtml(_ac);
}else{
RadEditorNamespace.RadEditorInsertList(_96,_94);
return false;
}
}else{
RadEditorNamespace.RadEditorInsertList(_96,_94);
return false;
}
}else{
var _af=RadEditorNamespace.RadGenericCommand.New(_93,_94.ContentWindow);
var _b0=_98.duplicate();
_b0.collapse(false);
_b0.moveEnd("character",1);
if(_b0.htmlText.match(/<BR\/?>/gi)){
_98.moveEnd("character",1);
}
var _b1="<"+_9c+"><LI>"+_98.htmlText.replace(/(<BR\s*>\s*)*$/gi,"").replace(/<BR\/?>$/gi,"").replace(/<BR\/?>/gi,"</LI><LI>")+"</LI></"+_9c+">";
try{
_98.pasteHTML(_b1);
}
catch(e){
}
_94.ExecuteCommand(_af);
}
}
}
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_FORMAT_BLOCK]=function(_b2,_b3,_b4){
_b3.ExecuteCommand(RadEditorNamespace.RadFormatBlockCommand.New(_b3.Localization[_b2]||_b2,_b3.ContentWindow,_b4.GetSelectedValue()));
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_UNDO]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_REDO]=function(_b5,_b6,_b7){
var _b8=(_b7!=null&&_b7.GetSelectedValue)?_b7.GetSelectedValue():1;
if(_b5==RadEditorNamespace.RADCOMMAND_REDO){
_b6.Redo(_b8);
}else{
_b6.Undo(_b8);
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_APPLY_CLASS]=function(_b9,_ba,_bb){
var _bc=_bb.GetSelectedValue();
_ba.ExecuteApplyCssClassCommand(_bc,_ba.Localization[_bb.Name]);
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_TOGGLE_TABLE_BORDER]=function(_bd,_be,_bf){
_be.ToggleEnhancedEdit();
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_STRIP_FORMAT]=function(_c0,_c1,_c2){
var _c3=_c2.GetSelectedValue();
var _c4="";
try{
_c4=_c1.GetSelection().GetHtmlText();
}
catch(e){
}
var _c5=_c1.Document.selection?_c1.Document.selection:_c1.ContentWindow.getSelection();
var _c6=_c5.type?_c5.type.toLowerCase()=="none":_c5.isCollapsed;
if(_c6||_c4==""||_c1.GetHtml()==_c4){
_c1.SetHtml(RadEditorNamespace.StripFormatting(_c1.GetHtml(),_c3),_c1.Localization[_c0]+" "+_c3);
}else{
if(_c4!=null){
_c1.PasteHtml(RadEditorNamespace.StripFormatting(_c4,_c3));
}
}
};
RadEditorCommandList["StripAll"]=RadEditorCommandList["StripCss"]=RadEditorCommandList["StripFont"]=RadEditorCommandList["StripSpan"]=RadEditorCommandList["StripWord"]=function(_c7,_c8,_c9){
var _ca=_c7.substring(5);
_ca=_ca.toUpperCase();
var _c9={GetSelectedValue:function(){
return _ca;
}};
_c8.Fire(RadEditorNamespace.RADCOMMAND_STRIP_FORMAT,_c9);
return false;
};
RadEditorNamespace.RadEditorCreateTable=function(_cb,_cc,_cd){
var _ce=_cb.CreateElement("TABLE");
for(var r=0;r<_cc;r++){
oRow=_ce.insertRow(-1);
for(var c=0;c<_cd;c++){
oCell=oRow.insertCell(-1);
oCell.innerHTML="&nbsp;";
}
}
return _ce;
};
RadEditorNamespace.StripFormatting=function(_d1,_d2){
switch(_d2){
case "ALL":
_d1=_d1.replace(/<\/?[^>]*>/ig,"");
break;
case "WORD":
case "WORD_ALL":
case "WORD_NO_FONTS":
_d1=RadEditorNamespace.StripWordFormatting(_d1,_d2);
break;
case "CSS":
_d1=_d1.replace(new RegExp("(<[^>]+) class=[^ |^>]*([^>]*>)","ig"),"$1 $2");
_d1=_d1.replace(/(<[^>]+) style="[^"]*"([^>]*>)/ig,"$1 $2");
break;
case "FONT":
_d1=_d1.replace(/<\/?font[^>]*>/ig,"");
break;
case "SPAN":
_d1=_d1.replace(/<\/?span[^>]*>/ig,"");
break;
default:
break;
}
return _d1;
};
RadEditorNamespace.ReplaceNewLineWithBr=function(_d3){
try{
_d3=_d3.replace(/\n/g,"<br>");
return _d3;
}
catch(exc){
}
};
RadEditorNamespace.ConvertText2Html=function(_d4){
try{
_d4=_d4.replace(/</g,"&lt;");
_d4=_d4.replace(/>/g,"&gt;");
_d4=_d4.replace(/\n/g,"<br>");
return _d4;
}
catch(exc){
}
};
RadEditorNamespace.ClearWordAttributesInElement=function(_d5,_d6){
var _d7=document.all?_d5.all:_d5.getElementsByTagName("*");
for(var i=0;i<_d7.length;i++){
var _d9=_d7[i];
var _da=new RegExp("mso","gi");
if(_d9.nodeType==1){
if(_da.exec(_d9.className)){
_d9.className="";
}
_d9.removeAttribute("lang","",0);
_d9.removeAttribute("stylw","",0);
_d9.style.cssText=_d9.style.cssText.replace(/(([\w-]*?mso[\w-]*?):(.+?)([;^$]|$))/gi,"");
if(document.all){
_d9.style.removeAttribute("tab-stops",0);
_d9.style.removeAttribute("textIndent",0);
}
_d9.style.margin="";
if(document.all&&(_d6=="WORD_NO_FONTS"||_d6=="WORD_ALL")){
_d9.style.removeAttribute("fontFamily",0);
_d9.removeAttribute("face",0);
}
for(j=_d9.attributes.length-1;j>=0;j--){
var _db=_d9.attributes[j];
if("null"!=_db.value&&""!=_db.value){
if(_da.exec(_db.name)||_da.exec(_db.value)){
_d9.removeAttribute(_db.name);
}
}
}
}
}
};
RadEditorNamespace.StripWordFormatting=function(_dc,_dd){
if(_dd=="WORD_ALL"){
var _de=/<SPAN[^>]*?>([\s\S]*?)<\/SPAN[^>]*?>/ig;
while(_dc.match(_de)){
_dc=_dc.replace(_de,"$1");
}
var _df=/<FONT[^>]*?>([\s\S]*?)<\/FONT[^>]*?>/ig;
while(_dc.match(_df)){
_dc=_dc.replace(_df,"$1");
}
}
var _e0=document.createElement("DIV");
_e0.innerHTML=_dc;
RadEditorNamespace.ClearWordAttributesInElement(_e0,_dd);
var _e1=_e0.innerHTML;
_e1=_e1.replace(/\t/g," ");
_e1=_e1.replace(/<\/?\w+:[^>]*>/gi,"");
_e1=_e1.replace(/<\\?\??xml[^>]>/gi,"");
_e1=_e1.replace(/<p>&nbsp;<\/p>/gi,"<BR><BR>");
_e1=_e1.replace(/[ ]+/g," ");
_e1=_e1.replace(/<(\/)?strong>/ig,"<$1B>");
_e1=_e1.replace(/<(\/)?em>/ig,"<$1I>");
_e1=_e1.replace(/^\s/i,"");
_e1=_e1.replace(/\s$/i,"");
_e1=_e1.replace(/<o:[pP]>&nbsp;<\/o:[pP]>/gi,"");
_e1=_e1.replace(/<st1:.*?>/gi,"");
_e1=_e1.replace(/<font>([^<>]+)<\/font>/gi,"$1");
_e1=_e1.replace(/<span>([^<>]+)<\/span>/gi,"$1");
_e1=_e1.replace(/<\?xml[^>]*>/ig,"");
_e1=_e1.replace(/<\/?[a-z]+:[^>]*>/ig,"");
_e1=_e1.replace(/class=""/ig,"");
_e1=_e1.replace(/class=''/ig,"");
_e1=_e1.replace(/<span[^>]*>\s*<\/span[^>]*>/ig," ");
_e1=_e1.replace(/<font[^>]*>\s*<\/font[^>]*>/ig," ");
_e1=_e1.replace(/\s+/ig," ");
_e1=_e1.replace(/<span><span>/ig,"<span>");
_e1=_e1.replace(/<\/span><\/span>/ig,"</span>");
return _e1;
};
RadEditorNamespace.CheckHtmlTagExistance=function(_e2){
return _e2.match(/[<>]/ig);
};
RadEditorNamespace.GetBaseUrl=function(){
var _e3=document.location.href;
var _e4=document.getElementsByTagName("HEAD")[0];
for(var i=0;i<_e4.childNodes.length;i++){
if(_e4.childNodes[i].nodeType==1&&_e4.childNodes[i].tagName.toLowerCase()=="base"){
_e3=_e4.childNodes[i].getAttribute("href");
break;
}
}
_e3=_e3.replace(/&/ig,"&amp;");
_e3=_e3.replace(/\?/ig,"?");
return _e3;
};
RadEditorNamespace.GetAnchorToCurrentPage=function(_e6,_e7,_e8){
if(_e8){
var _e9=_e7.href;
var _ea=RadEditorNamespace.GetBaseUrl();
if(_e9.indexOf(_ea)==0){
var _eb=_e9.lastIndexOf("#");
if(-1>_eb){
_e9=_e9.substr(_eb);
}
}else{
if(_e6.StripAbsoluteAnchorPaths){
var _ec=_e6.FiltersManager.GetFilterByName("RadStripPathFilter");
if(_ec&&_e7){
var _ed=_ec.GetHtmlContent(_e7.outerHTML);
var _ee=new RegExp("href=\"([^\"]*)\"","ig");
var _ef=_ee.exec(_ed);
if(_ef&&_ef[1]){
_e9=_ef[1];
}
}
}
}
return _e9;
}else{
return _e7.getAttribute("href");
}
};
RadEditorNamespace.GetSelectionLinkArgument=function(_f0,_f1){
_f0.SetFocus();
documentAnchors=_f0.Document.getElementsByTagName("A");
var _f2=new Array();
for(var i=0;i<documentAnchors.length;i++){
if(documentAnchors[i].name){
_f2[_f2.length]=documentAnchors[i];
}
}
var _f4={realLinkObject:null,href:"",className:"",text:"",target:"",name:"",title:"",showText:false,documentAnchors:_f2,CssClasses:[]};
if(null!=_f1){
_f4.SelectedTab=_f1;
}
var _f5=_f0.GetSelectedElement();
while(_f5!=null){
try{
if((_f5.tagName!=null)&&((_f5.tagName.toLowerCase()=="a")||(_f5.tagName.toLowerCase()=="img"))){
break;
}
_f5=_f5.parentNode;
}
catch(exc){
break;
}
}
if(_f5&&_f5.tagName=="A"){
_f4.realLinkObject=_f5;
_f4.href=RadEditorNamespace.GetAnchorToCurrentPage(_f0,_f5,_f0.IsIE);
_f4.className=_f5.className;
_f4.text=_f5.innerHTML;
_f4.target=_f5.target;
_f4.name=_f5.name;
_f4.title=_f5.title;
_f0.SelectElement(_f5);
}else{
if(_f5&&_f5.tagName=="IMG"){
if(_f5.parentNode&&_f5.parentNode.tagName=="A"){
var _f6=_f5.parentNode;
_f4.realLinkObject=_f6;
_f4.href=RadEditorNamespace.GetAnchorToCurrentPage(_f0,_f6,_f0.IsIE);
_f4.className=_f6.className;
_f4.text=_f5.parentNode.innerHTML;
_f4.target=_f6.target;
_f4.name=_f6.name;
_f4.title=_f6.title;
_f0.SelectElement(_f5);
}else{
_f4.text=RadEditorNamespace.Utils.GetOuterHtml(_f5);
}
}else{
var _f7=_f0.GetSelection().GetText();
if(_f7){
_f7=_f0.GetSelectionHtml();
}
_f4.text=_f7;
}
}
_f4.CssClasses=_f0.GetCssClassesByTagName("A",_f0.Document);
if(!RadEditorNamespace.Utils.Trim(_f4.text)){
_f4.text="";
}
_f4.showText=!RadEditorNamespace.Utils.HasHtmlContent(_f4.text);
return _f4;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_ROW_ABOVE]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_ROW_BELOW]=function(_f8,_f9,_fa){
_f9.InsertRow(_f8==RadEditorNamespace.RADCOMMAND_INSERT_ROW_ABOVE?"above":"below");
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_LEFT]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_RIGHT]=function(_fb,_fc,_fd){
_fc.InsertColumn(_fb==RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_LEFT?"left":"right");
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_DELETE_ROW]=function(_fe,_ff,tool){
_ff.DeleteRow();
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_DELETE_COLUMN]=function(_101,_102,tool){
_102.DeleteColumn();
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_DELETE_CELL]=function(_104,_105,tool){
_105.DeleteCell();
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_MERGE_COLUMNS]=function(_107,_108,tool){
_108.MergeColumns();
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_MERGE_ROWS]=function(_10a,_10b,tool){
_10b.MergeRows();
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SPLIT_CELL]=function(_10d,_10e,tool){
_10e.SplitCell();
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_DELETE_TABLE]=function(_110,_111,_112){
var _113=_112.GetSelectedValue();
if(_113&&"TABLE"!=_113.tagName){
_113=RadEditorNamespace.Utils.GetElementParentByTag(_113,"TABLE");
}
if(_113){
_111.SelectElement(_113);
_111.ExecuteBrowserCommand(RadEditorNamespace.RADCOMMAND_DELETE);
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_TABLE]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_TABLE_WIZARD]=function(_114,_115,_116){
if(RadEditorNamespace.RADCOMMAND_INSERT_TABLE==_114){
var _117=null;
var _118=_116.GetSelectedValue();
if(_118){
_117=RadEditorNamespace.RadEditorCreateTable(_115,_118.RowsCount,_118.ColumnsCount);
if(_117){
_115.ExecuteInsertObjectCommand(_117,_115.Localization[_114]);
}
}
}else{
if(RadEditorNamespace.RADCOMMAND_TABLE_WIZARD==_114){
var _119=(_116&&"function"==typeof (_116)?_116:RadEditorNamespace.radEditorInsertTable);
var _11a=_115.GetCssClassesByTagName("TABLE",_115.Document);
var _11b=_115.GetCssClassesByTagName("TD",_115.Document);
var _11c={tableToModify:RadEditorNamespace.RadEditorCreateTable(_115,2,2),CssClasses:_11a,CellCssClasses:_11b,EditorObj:_115,InternalParameters:_115.GetDialogInternalParameters(_114)};
_115.ShowDialog(_115.GetDialogUrl(_114),_11c,400,300,_119,null,_115.Localization[_114]);
return false;
}
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SET_TABLE_PROPERTIES]=function(_11d,_11e,_11f){
_11d=RadEditorNamespace.RADCOMMAND_TABLE_WIZARD;
var _120=null;
if(!_11f.GetSelectedValue){
_120=_11e.GetSelectedElement();
}else{
_120=_11f.GetSelectedValue();
}
if(_120&&"TABLE"!=_120.tagName){
_120=RadEditorNamespace.Utils.GetElementParentByTag(_120,"TABLE");
}
if(!_120){
alert(_11e.Localization["TableWarning"]);
return;
}
var _121=_11e.GetCssClassesByTagName("TABLE",_11e.Document);
var _122=_11e.GetCssClassesByTagName("TD",_11e.Document);
var _123={tableToModify:_120,EditorObj:_11e,CssClasses:_121,CellCssClasses:_122,tableDocument:_11e.Document,InternalParameters:_11e.GetDialogInternalParameters(_11d)};
_11e.ShowDialog(_11e.GetDialogUrl(_11d),_123,400,300,null,null,_11e.Localization[_11d]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SET_CELL_PROPERTIES]=function(_124,_125,_126){
var _127=null;
if(!_126.GetSelectedValue){
_127=_125.GetSelectedElement();
}else{
_127=_126.GetSelectedValue();
}
if(_127&&"TD"!=_127.tagName&&"TH"!=_127.tagName){
var _128=RadEditorNamespace.Utils.GetElementParentByTag(_127,"TD");
if(!_128){
_128=RadEditorNamespace.Utils.GetElementParentByTag(_127,"TH");
}
_127=_128;
}
if(!_127){
alert(_125.Localization["CellWarning"]);
return;
}
var _129=_125.GetCssClassesByTagName(_127.tagName,_125.Document);
var _12a={cellToModify:_127,EditorObj:_125,CssClasses:_129,InternalParameters:_125.GetDialogInternalParameters(_124)};
_125.ShowDialog(_125.GetDialogUrl(_124),_12a,400,300,null,null,_125.Localization[_124]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SET_IMAGE_PROPERTIES]=function(_12b,_12c,_12d){
var oImg=_12d.GetSelectedValue();
var _12f=_12c.GetCssClassesByTagName("IMG",_12c.Document);
var _130={imageToModify:oImg,EditorObj:_12c,CssClasses:_12f,ThumbnailSuffix:_12c.ThumbSuffix,InternalParameters:_12c.GetDialogInternalParameters(_12b)};
var _131={CommandTitle:_12c.Localization[_12b],OriginalImage:oImg};
_12c.ShowDialog(_12c.GetDialogUrl(_12b),_130,400,300,RadEditorNamespace.radEditorSetImageProperties,_131,_12c.Localization[_12b]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_FORMAT_CODE_BLOCK_DIALOG]=function(_132,_133,_134){
_133.ShowDialog(_133.GetDialogUrl(_132),null,700,570,RadEditorNamespace.radEditorFormatCodeBlock,null,_133.Localization[_132]);
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_IMAGE_MAP_DIALOG]=function(_135,_136,_137){
var _138={};
_138.InternalParameters=_136.GetDialogInternalParameters(_135);
_138.EditorObj=_136;
var _139=_136.GetSelectedElement();
if(_139&&_139.tagName=="IMG"){
var _13a=_139;
_138.ImageSrc=_13a.src;
_138.ImageWidth=(_13a.style.width)?_13a.style.width:_13a.width;
_138.ImageHeight=(_13a.style.height)?_13a.style.height:_13a.height;
if(document.all){
var oRng=_136.Document.body.createTextRange();
oRng.collapse();
oRng.moveToElementText(_13a);
oRng.select();
}
if(_13a.useMap){
var _13c=_13a.getAttribute("useMap").substr(1);
var _13d="";
var Map=RadEditorNamespace.GetImageMapByName(_136,_13c);
if(Map!=null){
_13d="<map name = \""+_13c+"\">"+Map.innerHTML+"</map>";
}
_138.ImageMapHTML=_13d;
_13a.style.width=_138.ImageWidth;
_13a.style.height=_138.ImageHeight;
}
}
var _13f=(document.all)?700:730;
var _140=(document.all)?450:470;
_136.ShowDialog(_136.GetDialogUrl(_135),_138,_13f,_140,RadEditorNamespace.radEditorSetImageMapProperties,null,_136.Localization[_135]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SET_LINK_PROPERTIES]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_LINK_DIALOG]=function(_141,_142,_143){
var _144=null;
if(_143&&_143.SelectedTab){
_144=_143.SelectedTab;
}
var args=RadEditorNamespace.GetSelectionLinkArgument(_142,_144);
var _146=(null==args.realLinkObject?RadEditorNamespace.radEditorCreateLink:RadEditorNamespace.radEditorSetLinkProperties);
if("function"==typeof (_143)){
_146=_143;
}
_142.ShowDialog(_142.GetDialogUrl(RadEditorNamespace.RADCOMMAND_SHOW_LINK_DIALOG),args,400,300,_146,{cmdName:_142.Localization[_141]},_142.Localization[_141]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_IMAGE_DIALOG]=function(_147,_148,_149){
var _14a=(_149&&"function"==typeof (_149)?_149:RadEditorNamespace.radEditorCreateImage);
var _14b={};
_14b.InternalParameters=_148.GetDialogInternalParameters(_147);
var _14c=_148.GetDialogUrl(_147);
var _14d=_148.GetSelectedElement();
if(_14d&&_14d.tagName&&_14d.tagName.toLowerCase()=="img"){
_14c+="&selectedObjectPath="+RadEditorNamespace.Utils.RemoveProtocolNameAndServerName(_14d.src);
}
_148.ShowDialog(_14c,_14b,400,300,_14a,null,_148.Localization[_147]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_TEMPLATE_DIALOG]=function(_14e,_14f,_150){
var _151=(_150&&"function"==typeof (_150)?_150:RadEditorNamespace.radEditorInsertTemplate);
var _152={};
_152.InternalParameters=_14f.GetDialogInternalParameters(_14e);
_14f.ShowDialog(_14f.GetDialogUrl(_14e),_152,400,300,_151,null,_14f.Localization[_14e]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_ABOUT_DIALOG]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_HELP]=function(_153,_154,_155){
var _156=RadEditorNamespace.RADCOMMAND_SHOW_ABOUT_DIALOG==_153?300:570;
var _157=RadEditorNamespace.RADCOMMAND_SHOW_ABOUT_DIALOG==_153?160:400;
_154.ShowDialog(_154.GetDialogUrl(_153),null,_156,_157,null,null,_154.Localization[_153]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_PAGE_PROPERTIES]=function(_158,_159,_15a){
var _15b={CssClasses:_159.GetCssClassesByTagName("BODY",_159.Document),EditorObj:_159};
_159.ShowDialog(_159.GetDialogUrl(_158),_15b,480,400,null,null,_159.GetLocalizedString(_158,_158));
return false;
};
RadEditorNamespace.ShowCleanFormattingDialogMozilla=function(_15c,_15d){
var _15e={commandName:_15d,CommandTitle:_15c.Localization[_15d]};
_15c.ShowDialog(_15c.GetDialogUrl("MozillaPasteHelperDlg"),{GetPlainText:(RadEditorNamespace.RADCOMMAND_PASTE_PLAIN_TEXT==_15d?true:false)},400,300,RadEditorNamespace.PasteCleanedTextMozilla,_15e,_15c.Localization[_15d]);
return false;
};
RadEditorNamespace.PasteCleanedTextMozilla=function(_15f,_160){
if(_15f){
cleanedText=_15f;
switch(_160.commandName){
case RadEditorNamespace.RADCOMMAND_PASTE_FROM_WORD:
cleanedText=RadEditorNamespace.StripFormatting(cleanedText,"WORD");
break;
case RadEditorNamespace.RADCOMMAND_PASTE_FROM_WORD_ALL:
cleanedText=RadEditorNamespace.StripFormatting(cleanedText,"WORD_ALL");
break;
case RadEditorNamespace.RADCOMMAND_PASTE_AS_HTML:
cleanedText=RadEditorNamespace.ConvertText2Html(cleanedText);
break;
case RadEditorNamespace.RADCOMMAND_PASTE_PLAIN_TEXT:
cleanedText=RadEditorNamespace.ReplaceNewLineWithBr(cleanedText);
break;
}
_160.editor.PasteHtml(cleanedText);
}
};
RadEditorNamespace.radEditorCreateLink=function(_161,_162){
if(!_161){
return;
}
if(_162.editor.IsIE&&(_162.editor.StripAbsoluteAnchorPaths==false)&&_161.href){
var _163=document.createElement(_163);
_163.innerHTML="<a href='"+_161.href+"'>temp</a>";
_161.href=_163.childNodes[0].href;
}
_162.editor.InsertLink(_161.href,_161.text,_161);
};
RadEditorNamespace.radEditorSetLinkProperties=function(_164,_165){
if(_164){
_165.editor.SetLinkProperties(_164);
}
};
RadEditorNamespace.radEditorSetImageProperties=function(_166,_167){
if(_166){
_167.editor.ExecuteFormatObjectCommand(_166,_167.CommandTitle,_167.OriginalImage);
}
};
RadEditorNamespace.radEditorCreateMedia=function(_168,_169){
if(_168){
if(TelerikNamespace.Utils.DetectBrowser("safari")){
window.setTimeout(function(){
_169.editor.PasteHtml(_168);
},0);
return;
}
_169.editor.PasteHtml(_168);
}
};
RadEditorNamespace.radEditorCreateFlash=function(_16a,_16b){
if(_16a){
_16b.editor.PasteHtml(_16a);
var _16c=_16b.editor;
if(!_16c.IsIE&&!_16c.IsOpera){
var _16d=new RadEditorNamespace.RadEditorMozillaFlashStart(_16c).GetDesignContent(_16c.GetHtml());
_16c.ContentArea.innerHTML=_16d;
}
}
};
RadEditorNamespace.radEditorCreateImage=function(_16e,_16f){
if(!_16e||!_16e.imagePath){
return;
}
var _170=_16f.editor;
_170.InsertImage(_16e.imagePath);
var _171=_170.GetSelectedElement();
if(_171&&_171.tagName.toLowerCase()=="img"){
_171.alt=_16e.imageAltText;
}
if(_16e.linkImagePath){
if(_171.tagName.toUpperCase()=="IMG"){
_171.style.border="0";
}
var _172={};
if(_16e.targetToNew){
_172.text=_170.GetSelectionHtml();
_172.href=_16e.linkImagePath;
_172.target="_blank";
}
_170.InsertLink(_16e.linkImagePath,null,_172);
}
};
RadEditorNamespace.radEditorInsertTable=function(_173,_174){
if(_173){
_174.editor.ExecuteInsertObjectCommand(_173,"Insert Table");
}
};
RadEditorNamespace.radEditorInsertTemplate=function(_175,_176){
if(_175){
_176.editor.PasteHtml(_175);
}
};
RadEditorNamespace.radEditorFormatCodeBlock=function(_177,_178){
if(!_177.formattedCode){
return;
}
var _179=_178.editor;
var _17a=_177.formattedCode;
if(_17a){
_179.PasteHtml(_17a);
}
};
RadEditorNamespace.radEditorSetImageMapProperties=function(_17b,_17c){
if(!_17b){
return;
}
var _17d=_17b.MapHtml;
var _17e=_17b.ImageSrc;
var _17f=_17c.editor;
var _180=_17f.GetSelectedElement();
var _181;
if(_180&&_180.tagName=="IMG"){
_181=_180;
if(_17e!=_181.src){
_181.src=_17e;
}
}else{
if(!_17e){
return;
}
var Html="<img src=\""+_17e+"\" id = \"__tmp__\">";
_17f.PasteHtml(Html);
_181=_17f.Document.getElementById("__tmp__");
_181.removeAttribute("id");
if(document.all){
var oRng=_17f.Document.body.createTextRange();
oRng.collapse();
oRng.moveToElementText(_181);
oRng.select();
}
}
var _tmp=document.createElement("SPAN");
_tmp.innerHTML=_17d;
var _185=_tmp.getElementsByTagName("map");
if(_185.length==0){
return;
}
var _186=_185[0].innerHTML;
_tmp=null;
if(_186){
var _187="";
var Map=null;
var _189=_181.getAttribute("useMap");
if(_189){
_187=_189.substr(1);
Map=RadEditorNamespace.GetImageMapByName(_17f,_187);
}
if(Map==null){
var _cnt=0;
var _18b="rade_img_map_"+_17f.Id+"_";
var _18c=_18b+_cnt;
while(RadEditorNamespace.GetImageMapByName(_17f,_18c)!=null){
_cnt++;
_18c=_18b+_cnt;
}
Map=_17f.Document.createElement("map");
Map.id=_18c;
Map.name=_18c;
_187=_18c;
Map=_17f.Document.body.appendChild(Map);
_181.setAttribute("useMap","#"+_18c);
_181.setAttribute("border","0");
}
if(document.all){
Map.outerHTML="<map id=\""+_187+"\" name=\""+_187+"\">"+_186+"</map>";
}else{
Map.innerHTML=_186;
}
}else{
_181.removeAttribute("useMap");
}
};
RadEditorNamespace.GetImageMapByName=function(_18d,_18e){
var _18f=_18d.Document.getElementsByTagName("map");
if(_18d.Document.getElementById(_18e)!=null){
return _18d.Document.getElementById(_18e);
}
for(var i=0;i<_18f.length;i++){
if(_18f[i].getAttribute("name")==_18e){
return _18f[i];
}
}
return null;
};;RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_STRIKETHROUGH]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_STRIKETHROUGH);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_SUPERSCRIPT]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_SUPERSCRIPT);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_SUBSCRIPT]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_SUBSCRIPT);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_JUSTIFY_FULL]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_JUSTIFY_FULL);
RadEditorNamespace.UpdateCommandsArray[RadEditorNamespace.RADCOMMAND_ABSOLUTE_POSITION]=RadEditorNamespace.RadBrowserCommand.New(null,RadEditorNamespace.RADCOMMAND_ABSOLUTE_POSITION);
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_ABSOLUTE_POSITION]=function(_1,_2,_3){
var _4=false;
try{
_4=oDocument.queryCommandEnabled(_1);
}
catch(ev){
}
if(_4){
_2.ExecuteBrowserCommand(_1,true,null);
}else{
var _5=_2.GetSelection().GetParentElement();
if(_5&&_5.style){
var _6=_5.style.position;
_5.style.position=(_6=="absolute")?"":"absolute";
}
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_FORM_ELEMENT]=function(_7,_8,_9){
var _a=_9.GetSelectedValue();
var _b=null;
var _a=_a.toLowerCase();
switch(_a){
case "form":
_b=_8.CreateElement("form","150px","150px");
_b.innerHTML="&nbsp;";
break;
case "textarea":
_b=_8.CreateElement("textarea");
break;
case "select":
_b=_8.CreateElement("select","100px","22px");
break;
case "checkbox":
case "radio":
_b=_8.CreateElement("input");
_b.setAttribute("type",_a);
break;
case "button":
case "reset":
case "submit":
_b=_8.CreateElement("input","50px","22px");
_b.setAttribute("type",_a);
break;
case "hidden":
case "password":
case "text":
_b=_8.CreateElement("input","100px","22px");
_b.setAttribute("type",_a);
break;
}
if(_b){
var id=(new Date()-100);
_b.setAttribute("id",id);
_8.ExecuteInsertObjectCommand(_b,_8.Localization[_7]);
var _d=_8.Document.getElementById(id);
if(_d){
_d.removeAttribute("id");
if(_d.setActive){
_d.setActive();
}
return false;
}
}
};
RadEditorCommandList["InsertFormForm"]=RadEditorCommandList["InsertFormButton"]=RadEditorCommandList["InsertFormCheckbox"]=RadEditorCommandList["InsertFormHidden"]=RadEditorCommandList["InsertFormImageButton"]=RadEditorCommandList["InsertFormPassword"]=RadEditorCommandList["InsertFormRadio"]=RadEditorCommandList["InsertFormReset"]=RadEditorCommandList["InsertFormSelect"]=RadEditorCommandList["InsertFormSubmit"]=RadEditorCommandList["InsertFormTextarea"]=RadEditorCommandList["InsertFormText"]=function(_e,_f,_10){
var _11=_e.substring(10);
var _10={GetSelectedValue:function(){
return _11;
}};
_f.Fire(RadEditorNamespace.RADCOMMAND_INSERT_FORM_ELEMENT,_10);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_PASTE_PLAIN_TEXT]=function(_12,_13,_14){
if(_13.IsIE){
var _15=window.clipboardData.getData("Text");
var _16=RadEditorNamespace.ReplaceNewLineWithBr(_15);
if(_16){
_13.PasteHtml(_16);
}
}else{
return RadEditorNamespace.ShowCleanFormattingDialogMozilla(_13,_12);
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_PASTE_FROM_WORD]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_PASTE_FROM_WORD_ALL]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_PASTE_AS_HTML]=function(_17,_18,_19){
if(_18.IsIE){
var _1a=_18.CreateRestorePoint();
var _1b=_18.GetClipboardAsHtml();
var _1c="";
if(_17==RadEditorNamespace.RADCOMMAND_PASTE_FROM_WORD){
_1c=RadEditorNamespace.StripFormatting(_1b,"WORD");
}else{
if(_17==RadEditorNamespace.RADCOMMAND_PASTE_FROM_WORD_ALL){
_1c=RadEditorNamespace.StripFormatting(_1b,"WORD_ALL");
}else{
_1c=RadEditorNamespace.ConvertText2Html(_1b);
}
}
_1a.Select();
if(_1c){
_18.PasteHtml(_1c);
}
}else{
return RadEditorNamespace.ShowCleanFormattingDialogMozilla(_18,_17);
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_FIND_DIALOG]=function(_1d,_1e,_1f){
var _20={area:(_1e.Mode==RadEditorNamespace.RADEDITOR_DESIGN_MODE?_1e.ContentArea:_1e.ContentTextarea)};
_1e.ShowDialog(_1e.GetDialogUrl(_1d),_20,400,300,null,null,_1e.Localization[_1d]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_DOCUMENT_DIALOG]=function(_21,_22,_23){
var _24=RadEditorNamespace.GetSelectionLinkArgument(_22);
_24.InternalParameters=_22.GetDialogInternalParameters(_21);
var _25=(null==_24.realLinkObject?RadEditorNamespace.radEditorCreateLink:RadEditorNamespace.radEditorSetLinkProperties);
if("function"==typeof (_23)){
_25=_23;
}
_22.ShowDialog(_22.GetDialogUrl(_21),_24,400,300,_25,null,_22.Localization[_21]);
return false;
};
RadEditorNamespace.GetObjectParamValue=function(_26,_27){
for(var i=0;i<_26.childNodes.length;i++){
if((_26.childNodes[i].tagName.toUpperCase()=="PARAM")&&(_26.childNodes[i].name.toUpperCase()==_27.toUpperCase())){
return _26.childNodes[i].value;
}
}
return null;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_MEDIA_DIALOG]=function(_29,_2a,_2b){
_2a.SetFocus();
var _2c={};
_2c.InternalParameters=_2a.GetDialogInternalParameters(_29);
var _2d=_2a.GetSelectedElement();
_2c.Media=_2d;
var _2e=null;
if(_2d&&_2d.tagName){
_2e=(_2d.tagName.toUpperCase()=="OBJECT")?RadEditorNamespace.GetObjectParamValue(_2d,"URL"):_2d.src;
}
_2c.MediaPath=_2e;
var _2f=(_2b&&"function"==typeof (_2b)?_2b:RadEditorNamespace.radEditorCreateMedia);
var _30=_2a.GetDialogUrl(_29);
if(_2e){
_30+="&selectedObjectPath="+_2e;
}
_2a.ShowDialog(_30,_2c,400,300,_2f,null,_2a.Localization[_29]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SHOW_FLASH_DIALOG]=function(_31,_32,_33){
_32.SetFocus();
var _34={};
_34.StripAbsoluteImagesPaths=_32.StripAbsoluteImagesPaths;
_34.InternalParameters=_32.GetDialogInternalParameters(_31);
var _35=_32.GetSelectedElement();
_34.Flash=_35;
var _36=null;
if(_35){
_36=(_35.tagName.toUpperCase()=="OBJECT")?RadEditorNamespace.GetObjectParamValue(_35,"movie"):_35.src;
}
_34.FlashPath=_36;
var _37=(_33&&"function"==typeof (_33)?_33:RadEditorNamespace.radEditorCreateFlash);
var _38=_32.GetDialogUrl(_31);
if(_36){
_38+="&selectedObjectPath="+_36;
}
_32.ShowDialog(_38,_34,400,300,_37,null,_32.Localization[_31]);
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_MANAGE_MODULE]=function(_39,_3a,_3b){
var _3c=_3b.GetSelectedValue();
if(_3c){
var _3d=!_3c.IsEnabled;
_3c.SetEnabled(_3d);
if(!_3a.IsIE&&!_3d){
_3a.ResetSize();
}
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_TOGGLE_DOCKING]=function(_3e,_3f,_40){
if(_3f.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.PageTop)){
var _41=_3f.FloatingToolbarManager;
if(_41){
_41.ToggleFloatingToolbar();
_41.ToggleFloatingToolbar(true);
}
}else{
var _42=_3f.GetHtmlToolbarElements();
for(var i=0;i<_42.length;i++){
var _44=_42[i];
if(_44&&_44.AutoDock){
_44.AutoDock(true);
}
}
var _45=_3f.Modules;
for(var i=0;i<_45.length;i++){
var _44=_45[i].GetTopElement();
if(_44&&_44.AutoDock){
_44.AutoDock(true);
}
}
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_TOGGLE_SCREEN_MODE]=function(_46,_47,_48){
var _49=_47.WrapperElement;
if(_47.ToggleFullScreen){
if(_47.OnResizeHanlder){
RadEditorNamespace.Utils.DetachEventEx(window,"resize",_47.OnResizeHanlder);
_47.OnResizeHanlder=null;
}
var _4a=_47.DockingZones.TopZone;
_4a.style.width=_47.TopZoneWidth;
_47.SetSize(_47.Width,_47.Height,false);
document.body.scroll="";
document.documentElement.style.overflow="";
_47.ToggleFullScreen=false;
try{
document.body.style.margin="";
}
catch(e){
}
if(true==_47.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.PageTop)||true==_47.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.ShowOnFocus)){
var _4b=_47.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.PageTop)?RadEditorNamespace.GetPageTopToolbarManager():RadEditorNamespace.GetShowOnFocusToolbarManager();
if(_4b){
var _4c=_4b.GetToolbarByEditor(_47);
if(_4c&&_4c.parentNode){
_4c.parentNode.removeChild(_4c);
}
_4b.CurrentEditor=null;
_47.SetFocus();
}
}
}else{
_47.Fire(RadEditorNamespace.RADCOMMAND_TOGGLE_DOCKING);
if(true==_47.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.PageTop)||true==_47.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.ShowOnFocus)){
var _4b=_47.IsToolbarModeEnabled(RadEditorNamespace.ToolbarModesEnum.PageTop)?RadEditorNamespace.GetPageTopToolbarManager():RadEditorNamespace.GetShowOnFocusToolbarManager();
if(_4b){
var _4c=_4b.GetToolbarByEditor(_47);
if(_4c){
_4b.ShowToolbarHolder(false);
_4c.parentNode.removeChild(_4c);
_47.GetDockingZoneById("Top").appendChild(_4c);
}
}
}
document.body.scroll="no";
document.body.style.margin="0px";
if(document.documentElement){
document.documentElement.style.overflow="hidden";
}
var _4a=_47.DockingZones.TopZone;
_47.TopZoneWidth=_4a.offsetWidth;
if(_47.IsIE&&"CSS1Compat"==document.compatMode){
_47.Height=_49.clientHeight;
_49.style.width=document.documentElement.clientWidth;
_49.style.height=document.documentElement.clientHeight;
var _4d=RadEditorNamespace.Utils.GetRect(_49);
var _4e=_4d.height-parseInt(_49.style.height);
if(_4e>0){
_49.style.height=(parseInt(_49.style.height)-_4e)+"px";
}
}else{
_49.style.width=window.innerWidth?parseInt(window.innerWidth)+"px":parseInt(document.body.clientWidth)+"px";
var _4f=parseInt(document.body.clientHeight);
_49.style.height=window.innerHeight?parseInt(window.innerHeight)+"px":_4f+"px";
}
var _4d=RadEditorNamespace.Utils.GetRect(_49);
var _50=_4d.left;
var top=_4d.top;
document.body.scrollTop=top;
document.body.scrollLeft=_50;
document.documentElement.scrollTop=top;
document.documentElement.scrollLeft=_50;
_47.ToggleFullScreen=true;
if(!_47.OnResizeHanlder){
_47.OnResizeHanlder=function(){
_47.ToggleFullScreen=false;
_47.Fire(RadEditorNamespace.RADCOMMAND_TOGGLE_SCREEN_MODE);
};
RadEditorNamespace.Utils.AttachEventEx(window,"resize",_47.OnResizeHanlder);
}
}
_47.SetEditable(true);
_47.SetFocus();
if(!_48){
_48=_47.GetToolByName(_46);
}
if(_48){
_48.SetState(_47.ToggleFullScreen?RadEditorNamespace.RADCOMMAND_STATE_ON:RadEditorNamespace.RADCOMMAND_STATE_OFF);
}
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_ZOOM]=function(_52,_53,_54){
var _55=_54.GetSelectedValue();
_54.HeaderElement.innerHTML=_55;
_53.ContentArea.style.zoom=_55;
return false;
};
RadEditorCommandList["InsertHorizontalRule"]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_PARAGRAPH]=function(_56,_57,_58){
if("InsertHorizontalRule"==_56){
_57.ExecuteInsertObjectCommand(_57.CreateElement("HR"),_57.Localization[_56]);
}else{
_57.ExecuteBrowserCommand("InsertParagraph",false);
if(_57.IsIE){
var _59=_57.Document;
var _5a=_59.selection.createRange();
var _5b=_59.body.getElementsByTagName("P");
var oP=null;
for(var i=_5b.length-1;i>=0;i--){
tempRange=_5a.duplicate();
tempRange.moveToElementText(_5b[i]);
var _5e=_5a.compareEndPoints("StartToEnd",tempRange);
if(1==_5e){
oP=_5b[i];
break;
}
}
if(oP){
var _5f=RadEditorNamespace.RadGenericCommand.New(_56,_57.ContentWindow);
_5a.moveToElementText(oP);
_5a.collapse(false);
_5a.pasteHTML("&nbsp;");
_57.ExecuteCommand(_5f);
_5a.moveStart("character",-1);
_5a.moveToElementText(oP);
_5a.moveStart("character",1);
_5a.select();
_5a.collapse(true);
}
}
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_PRINT]=function(_60,_61,_62){
if(_61.IsIE){
_61.ExecuteBrowserCommand(_60,false,null);
}else{
if(_61.ContentWindow.print){
_61.ContentWindow.print();
}
}
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_REPEAT_LAST_COMMAND]=function(_63,_64,_65){
_64.SetFocus();
_64.CommandsManager.RepeatLastCommand();
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_SNIPPET]=function(_66,_67,_68){
var _69=_68.GetSelectedValue();
var _6a=document.getElementById(_69);
var _6b=TelerikNamespace.Utils.DecodePostbackContent(_6a.innerHTML,false);
if(_6a){
_67.PasteHtml(_6b,_67.Localization[_68.Name]);
}
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_CUSTOM_LINK]=function(_6c,_6d,_6e){
var _6f=_6e.GetSelectedValue();
var _70=_6d.GetSelectedElement();
var _71=RadEditorNamespace.Utils.GetElementParentByTag(_70,"A");
var _72="";
if(!_71&&_6d.GetSelection().GetHtmlText()==""){
_72=_6f.Text;
}
var _73={href:_6f.Href,title:_6f.Title,target:_6f.Target,text:_72};
_6d.InsertLink(_6f.Href,_72,_73);
};
RadEditorCommandList["InsertGroupbox"]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_DATE]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_TIME]=RadEditorCommandList[RadEditorNamespace.RADCOMMAND_INSERT_SYMBOL]=function(_74,_75,_76){
var _77="";
switch(_74){
case RadEditorNamespace.RADCOMMAND_INSERT_SYMBOL:
var _77=_76.GetSelectedValue();
break;
case RadEditorNamespace.RADCOMMAND_INSERT_DATE:
var now=new Date();
_77="&nbsp;"+now.toLocaleDateString();
break;
case RadEditorNamespace.RADCOMMAND_INSERT_TIME:
var now=new Date();
_77="&nbsp;"+now.toLocaleTimeString();
break;
default:
_77="<fieldset style='WIDTH: 200px; HEIGHT: 76px'> <legend>Title</legend>Content... </fieldset> ";
}
_75.PasteHtml(_77,_76!=null?_75.Localization[_76.Name]:"");
};
RadEditorCommandList["AjaxSpellCheck"]=function(_79,_7a,_7b){
function AjaxSpellCheckController(_7c){
this.Editor=_7c;
this.OriginalHtml=null;
this.StartCheckMessage=_7c.GetLocalizedString("SpellCheck","Check spelling");
this.FinalCheckMessage=_7c.GetLocalizedString("SpellCheckEnd","Finish spellchecking");
this.CompleteMessage=_7c.GetLocalizedString("SpellCheckComplete","Spellchecking complete!");
this.CancelMessage=_7c.GetLocalizedString("Cancel","Cancel");
this.AddWordSuccessMessage=_7c.GetLocalizedString("AddCustomWordSuccess");
this.SpellingInProgressMessage=_7c.GetLocalizedString("SpellingInProgress");
this.SpellingModeMessage=_7c.GetLocalizedString("SpellingMode");
this.NoSpellingMistakesMessage=_7c.GetLocalizedString("NoSpellingMistakes","No mistakes found.");
this.LoadingIcon=_7c.GetImageUrl("../Img/loadingspell.gif");
this.CreateUI();
}
AjaxSpellCheckController.prototype.GetSpellService=function(){
var _7d=this.Editor.AjaxSpellId;
var _7e=GetSpellCheckService(_7d);
var _7f=null;
if(_7b&&_7b.GetSelectedValue){
_7f=_7b.GetSelectedValue();
}else{
if(!_7e.DictionaryLanguage){
_7f=_7a.Language.replace(/_/,"-");
}
}
if(_7f){
_7e.DictionaryLanguage=_7f;
}
return _7e;
};
AjaxSpellCheckController.prototype.CreateUI=function(){
var _80=document;
var _81=_80.createElement("table");
_81.cellSpacing=2;
_81.cellPadding=0;
_81.className="RadEModuleTable";
_81.style.width="100%";
_81.style.backgroundColor="#ffffcc";
_81.style.borderBottom="1px solid #adadad";
_81.insertRow(-1);
var _82=_81.rows[0].insertCell(-1);
_82.style.width="100%";
var _83=_80.createElement("button");
_83.className="RadEXhtmlButton";
_82=_81.rows[0].insertCell(-1);
this.FinishButton=_83.cloneNode(true);
this.FinishButton.Parent=this;
this.FinishButton.innerHTML=this.FinalCheckMessage;
this.FinishButton.onclick=new Function("this.Parent.FinishSpellcheck();return false;");
_82.appendChild(this.FinishButton);
_82=_81.rows[0].insertCell(-1);
this.CancelButton=_83.cloneNode(true);
this.CancelButton.Parent=this;
this.CancelButton.innerHTML=this.CancelMessage;
this.CancelButton.onclick=new Function("this.Parent.CancelSpellcheck();return false;");
_82.appendChild(this.CancelButton);
this.TopElement=_81;
};
AjaxSpellCheckController.prototype.Dispose=function(){
if(this.MultiDropdown&&this.MultiDropdown.Dispose){
this.MultiDropdown.Dispose();
}
if(this.CancelButton){
this.CancelButton.Parent=null;
this.CancelButton.onclick=null;
}
this.CancelButton=null;
if(this.FinishButton){
this.FinishButton.Parent=null;
this.FinishButton.onclick=null;
}
this.FinishButton=null;
if(this.SpellEngineUI_Instance){
this.SpellEngineUI_Instance.Dispose();
}
this.Editor=null;
this.TopElement=null;
this.LoadingIcon=null;
this.SpellIconHtml=null;
};
AjaxSpellCheckController.prototype.CancelSpellcheck=function(){
this.SetVisible(false);
this.SpellEngineUI_Instance.Finalize(false);
if(null!=this.OriginalHtml){
this.Editor.ContentArea.innerHTML=this.OriginalHtml;
this.OriginalHtml=null;
}
var _84=this.Editor.CommandsManager;
var _85=_84.Commands;
_84.RemoveCommandAt(_85.length-1);
this.Editor.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED);
};
AjaxSpellCheckController.prototype.FinishSpellcheck=function(){
this.SetVisible(false);
this.SpellEngineUI_Instance.Finalize();
this.OriginalHtml=null;
};
AjaxSpellCheckController.prototype.AddCustomWord=function(_86){
var _87=this.GetSpellService();
var _88=this;
var _89=this.AddWordSuccessMessage;
_87.AddCustomWord(_86,function(_8a,_8b){
alert(_86+" "+_89);
_88.SpellEngineUI_Instance.ClearWrongWords(_86,_86);
},null);
};
AjaxSpellCheckController.prototype.SetVisible=function(_8c){
this.TopElement.style.display=_8c?"":"none";
};
AjaxSpellCheckController.prototype.EnableButtons=function(_8d){
var _8e=this.TopElement.getElementsByTagName("button");
for(var i=0;i<_8e.length;i++){
_8e[i].disabled=!_8d;
}
};
AjaxSpellCheckController.prototype.SetLoadingIconVisible=function(_90){
var _91=this.TopElement.rows[0].cells[0];
_91.innerHTML="";
if(_90){
var _92=document.createElement("IMG");
_92.src=this.LoadingIcon;
_92.align="absmiddle";
_91.innerHTML="<label class='RadEToolText'>"+this.SpellingInProgressMessage+"</label>";
_91.appendChild(_92);
}else{
_91.innerHTML="<label class='RadEToolText'>"+this.SpellingModeMessage+"</label>";
}
};
AjaxSpellCheckController.prototype.BeginSpellcheck=function(_93){
this.SetLoadingIconVisible(false);
this.EnableButtons(true);
if(_93.BadWords.length==0){
alert(this.NoSpellingMistakesMessage);
this.FinishSpellcheck();
}else{
this.SpellEngineUI_Instance.Initialize(_93,this.OriginalHtml);
}
};
AjaxSpellCheckController.prototype.MakeSpellcheckRequest=function(){
var _94=this;
if(this.SpellEngineUI_Instance){
var _95=this.SpellEngineUI_Instance.SpellcheckComplete;
if(!_95){
this.FinishSpellcheck();
}
}else{
this.SpellEngineUI_Instance=new RadEditorSpellEngineUI(this.Editor);
this.SpellEngineUI_Instance.OnRaiseSpellcheckDone=function(){
_94.FinishSpellcheck();
alert(_94.CompleteMessage);
};
this.SpellEngineUI_Instance.OnRaiseAddCustomWord=function(_96){
_94.AddCustomWord(_96);
};
}
this.SetVisible(true);
this.SetLoadingIconVisible(true);
this.EnableButtons(false);
this.OriginalHtml=this.Editor.IsIE?this.Editor.GetHtml():this.Editor.ContentArea.innerHTML;
var _97=this.GetSpellService();
var _98=function(_99,_9a){
_94.BeginSpellcheck(_9a);
};
_97.SpellCheck(this.SpellEngineUI_Instance.EscapeNewLines(this.OriginalHtml),_98,null);
};
if(!_7a.AjaxSpellController_Instance){
_7a.AjaxSpellController_Instance=new AjaxSpellCheckController(_7a);
var _9b=_7a.AjaxSpellController_Instance.TopElement;
if(!_9b.parentNode||!_9b.parentNode.tagName){
if(_7a.IsIE){
_7a.DockingZones.TopZone.appendChild(_9b);
}else{
_7a.ContentAreaElement.parentNode.insertBefore(_9b,_7a.ContentAreaElement);
}
}
}
_7a.AjaxSpellController_Instance.MakeSpellcheckRequest();
return false;
};
RadEditorCommandList[RadEditorNamespace.RADCOMMAND_SPELLCHECK]=function(_9c,_9d,_9e){
var _9f=_9d.GetSelection();
var _a0=_9f.GetText();
var _a1=(""==_a0||null==_a0);
var _a2={restorePoint:_9d.CreateRestorePoint(),GetText:function(){
return this.getText();
},SetText:function(_a3){
this.setText(_a3);
},getText:function(){
if(_a1){
return _9d.GetPageHtml();
}else{
return _9f.GetHtmlText();
}
},setText:function(_a4){
if(_a1){
_9d.SetHtml(_a4,(_9d.Localization["CorrectSpelling"]||"Spelling Changes"));
}else{
this.restorePoint.Select();
_9d.PasteHtml(_a4,(_9d.Localization["CorrectSpelling"]||"Spelling Changes"));
}
}};
var _a5=(_9e&&_9e.GetSelectedValue);
var _a6=_a5?_9e.GetSelectedValue():_9d.Language.replace(/_/,"-");
var _a7=_9d.SpellId;
try{
var _a8=GetRadSpell(_a7);
_a8.Skin=_9d.SkinBasePath;
_a8.UseClassicDialogs=_9d.UseClassicDialogs;
if(_a8.Language=="RadEditor_Default"||_a5){
_a8.Language=_a6;
}
if(_a8.DictionaryLanguage=="RadEditor_Default"||_a5){
_a8.DictionaryLanguage=_a6;
}
_a8.SetTextSource(_a2);
_a8.StartSpellCheck();
}
catch(spellError){
alert("The spellchecker has not been found."+"\nPlease ensure that you have a compatible RadSpell.dll assembly in your web application's bin folder"+"\nError message: "+spellError.message);
}
return false;
};;;function RadEditorSpellEngineUI(_1){
this.Editor=_1;
this.RadSpellData=null;
this.SuggestionDropdown=null;
this.WrongWordCounter=0;
this.WrongWordsArray=null;
this.SpanId="RadESpellError_";
this.SelectedEditorElement=null;
this.SpellcheckComplete=true;
this.AutomaticAdvance=true;
this.LocalizedCommandName=this.Editor.GetLocalizedString("SpellCheck","Check spelling");
this.LocalizedName=this.Editor.GetLocalizedString("SpellingChange","Spelling Change");
this.NoSuggestionsString=this.Editor.GetLocalizedString("NoSuggestions","(no suggestions)");
this.ChangeWordString=this.Editor.GetLocalizedString("ChangeWordString","Change");
this.IgnoreAllString=this.Editor.GetLocalizedString("IgnoreAllString","Ignore All");
this.IgnoreString=this.Editor.GetLocalizedString("IgnoreString","Ignore");
this.MoreThanOnceMessage=this.Editor.GetLocalizedString("MoreThanOnceMessage","This word occurs more than once in the text. Would you like to replace all instances?");
this.UndoDisabledMessage=this.Editor.GetLocalizedString("UndoDisabledMessage","You cannot undo further while in spellcheck mode. Please finish spellchecking first.");
this.AddToDictionaryString=this.Editor.GetLocalizedString("AddToDictionary","Add to dictionary");
this.IgnoreIcon=this.Editor.GetImageUrl("../Img/SpellIgnore.gif");
this.OkIcon=this.Editor.GetImageUrl("../Img/SpellChange.gif");
this.AddIcon=this.Editor.GetImageUrl("../Img/SpellDictionary.gif");
this.SuggestionBox=null;
this.OnRaiseAddCustomWord=function(_2){
};
this.OnRaiseSpellcheckDone=function(){
};
this.Dispose=function(){
this.Editor=null;
this.SearchEngine=null;
this.RadSpellData=null;
this.OnRaiseSpellcheckDone=null;
if(this.SuggestionDropdown){
this.SuggestionDropdown.Dispose();
}
if(this.SuggestionBox){
this.SuggestionBox.Dispose();
}
this.SuggestionBox=null;
};
this.ConfigureUndo=function(_3){
if(_3){
this.Editor.EnableEditing(true);
var _4=this.Editor.CommandsManager;
var _5=_4.Commands;
var i=this.CurrentUndoIndex;
while(i<_5.length){
_4.RemoveCommandAt(_5.length-1);
}
this.Editor.ExecuteCommand(this.SaveStateCmd);
this.Editor.OnClientCommandExecuting=this.OnClientCommandExecuting;
this.OnClientCommandExecuting=null;
}else{
var _7=this;
this.OnClientCommandExecuting=this.Editor.OnClientCommandExecuting;
this.CurrentUndoIndex=this.Editor.CommandsManager.GetCommandsToUndo().length;
this.Editor.OnClientCommandExecuting=function(_8,_9,_a){
if(_9=="Undo"&&_7.CurrentUndoIndex>=_8.CommandsManager.GetCommandsToUndo().length){
alert(_7.UndoDisabledMessage);
return false;
}
};
this.Editor.EnableEditing(true,{"Undo":true,"Redo":true,"AjaxSpellCheck":true},false,true,false,false,false,false);
}
};
this.Finalize=function(_b){
if(true!=this.Initialized){
return;
}
if(false!=_b){
this.ClearWrongWords();
}
var _c=this.Editor;
if(this.OnMouseHandler){
_c.DetachEventHandler("click",this.OnMouseHandler);
}
if(this.OnKeyDownHandler){
_c.DetachEventHandler("keydown",this.OnKeyDownHandler);
}
if(this.OnContextMenu){
_c.DetachEventHandler(RadEditorNamespace.RADEVENT_CONTEXTMENU,this.OnContextMenu);
}
if(this.OnEditorSubmit){
_c.DetachEventHandler(RadEditorNamespace.RADEVENT_SUBMIT,this.OnEditorSubmit);
}
this.ConfigureUndo(true);
_c.SetFocus();
_c.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED);
this.WrongWordsArray=null;
this.SpellcheckComplete=true;
if(this.SuggestionDropdown){
this.SuggestionDropdown.Dispose();
}
if(this.SuggestionBox){
this.SuggestionBox.Dispose();
}
this.SuggestionBox=null;
this.Initialized=false;
};
this.Initialize=function(_d,_e){
this.Initialized=true;
this.WrongWordsArray=_d.BadWords;
this.WordOffsets=_d.WordOffsets;
var _f=this.Editor;
this.SaveStateCmd=RadEditorNamespace.RadGenericCommand.New(this.LocalizedCommandName,_f.ContentWindow);
this.MarkWrongWords(_e);
this.SpellcheckComplete=false;
this.WrongWordCounter=0;
var _10=this;
this.OnEditorSubmit=function(){
_10.Finalize();
};
this.Editor.AttachEventHandler(RadEditorNamespace.RADEVENT_SUBMIT,this.OnEditorSubmit);
this.OnMouseHandler=function(e){
_10.ShowSuggestionDropdown();
return RadEditorNamespace.Utils.CancelEvent(e);
};
var _12=this.Editor.IsIE;
this.OnKeyDownHandler=function(e){
if(e.keyCode==9){
_10.MoveToNextWrongWord();
return RadEditorNamespace.Utils.CancelEvent(e);
}
var _14=false;
if(_10.SuggestionDropdown){
var _15=_10.SuggestionDropdown;
var _16=(_10.SuggestionDropdown.Popup&&_10.SuggestionDropdown.Popup.IsVisible());
var _17=e.keyCode;
if(_16){
if(38==_17){
_15.SelectPreviousItem();
_14=true;
}else{
if(40==_17){
_15.SelectNextItem();
_14=true;
}else{
if(13==_17){
_15.ShowPopup(false);
_10.Fire("",_15);
try{
e.keyCode=123;
}
catch(e){
}
_14=true;
}else{
if(27==_17&&!_12){
_15.ShowPopup(false);
}
}
}
}
_14=true;
}
}
if(_14){
RadEditorNamespace.Utils.CancelEvent(e);
return false;
}
};
this.OnContextMenu=function(_18,e){
if(_12){
_10.OnMouseHandler(e);
}
e.cancelBubble=true;
if(_12){
return false;
}
};
window.setTimeout(function(){
var ed=_10.Editor;
ed.AttachEventHandler("click",_10.OnMouseHandler);
ed.AttachEventHandler("keydown",_10.OnKeyDownHandler);
ed.AttachEventHandler(RadEditorNamespace.RADEVENT_CONTEXTMENU,_10.OnContextMenu);
if(_10.AutomaticAdvance){
if(_12){
ed.SetFocus();
}
_10.MoveToNextWrongWord();
}
},50);
this.ConfigureUndo(false);
};
this.MoveToNextWrongWord=function(){
var dir=-1;
var _1c=this.Editor.GetSelection();
var _1d=_1c.GetParentElement();
var _1e=null;
if(this.IsHighlightedWord(_1d)&&!this.Editor.GetSelectionHtml()){
_1e=_1d;
}else{
_1c.Collapse();
var _1f=this.Editor.Document;
var _20=this.Editor;
var _21=this.Editor.IsIE;
var _22=this;
var _23=null;
function getWrongWord(){
var _24=_20.Document.getElementsByTagName("SPAN");
var i=0;
var _26=_24[i];
_23=_20.GetSelection().GetRange();
while(_26!=null){
if(_22.IsHighlightedWord(_26)){
var _27=null;
if(_21){
if(_23.duplicate){
tempRange=_23.duplicate();
}else{
tempRange=_20.ContentArea.createTextRange();
}
if(tempRange.moveToElementText){
tempRange.moveToElementText(_26);
}
if(!_23.compareEndPoints){
break;
}
_27=_23.compareEndPoints("EndToStart",tempRange);
if(0==_27&&_22.SuggestionDropdown&&_22.SuggestionDropdown.Popup&&!_22.SuggestionDropdown.Popup.IsVisible()){
_1e=_26;
break;
}
}else{
tempRange=_23.cloneRange();
tempRange.selectNodeContents(_26);
_27=_23.compareBoundaryPoints(Range.END_TO_START,tempRange);
}
if(dir==_27){
_1e=_26;
break;
}
}
i++;
_26=_24[i];
}
return _26;
}
var _1e=getWrongWord();
if(!_1e){
var _28=_20.ContentArea;
if(_28&&_28.createTextRange){
var _29=_28.createTextRange();
_29.moveToElementText(_28);
_29.collapse(true);
_29.select();
}else{
var _2a=_20.Document.getElementsByTagName("SPAN")[0];
if(_2a){
_20.SelectElement(_2a);
}
}
try{
var _23=_20.GetSelection().GetRange();
if(_23&&_23.moveStart){
_23.moveStart("character",-1);
_23.select();
}
}
catch(e){
}
_20.GetSelection().Collapse(true);
_1e=getWrongWord();
}
}
if(_1e){
this.Editor.SelectElement(_1e);
var _2b=this.Editor.GetSelection().GetRange();
if(_2b&&_2b.scrollIntoView&&_2b.select){
_2b.scrollIntoView(true);
_2b.select();
}else{
if(_1e.scrollIntoView){
_1e.scrollIntoView(false);
}
}
this.ShowSuggestionDropdown();
}
};
this.ClearWrongWords=function(_2c,_2d){
var _2e=this.Editor.Document.getElementsByTagName("SPAN");
for(var i=0;i<_2e.length;i++){
var _30=_2e[i];
if(this.IsHighlightedWord(_30)){
if(_2c){
if(_30.innerHTML==_2c){
this.ClearHighlightedElement(_30,_2d);
i--;
}
continue;
}else{
this.ClearHighlightedElement(_30);
}
i--;
}
}
};
this.IsHighlightedRemaining=function(){
var _31=this.Editor.Document.getElementsByTagName("SPAN");
for(var i=0;i<_31.length;i++){
var _33=_31[i];
if(this.IsHighlightedWord(_33)){
return true;
}
}
return false;
};
this.GetCurrentWrongWord=function(){
var _34=this.SelectedEditorElement;
var _35=_34.innerHTML.replace(/<\/?[^>]*>/ig,"");
return _35;
};
this.IsHighlightedWord=function(_36){
if(!_36||!_36.getAttribute){
return false;
}
var id=_36.getAttribute("id");
if(id&&id.indexOf(this.SpanId)>-1){
return true;
}
return false;
};
this.ClearHighlightedElement=function(_38,_39){
var _3a=_39?_39:_38.innerHTML.replace(/<\/?[^>]*>/ig,"");
var _3b=_39?true:false;
this.Editor.SelectElement(_38);
var _3c=RadEditorNamespace.RadGenericCommand.New(this.LocalizedName,this.Editor.ContentWindow);
var _3d=this.Editor.Document.createTextNode(_3a);
_38.parentNode.replaceChild(_3d,_38);
if(this.Editor.IsIE){
range=this.Editor.Document.body.createTextRange();
range.findText(_3d.data);
range.select();
}else{
this.Editor.SelectElement(_3d);
}
this.Editor.GetSelection().Collapse();
if(_3b){
this.Editor.ExecuteCommand(_3c);
}
this.SelectedEditorElement=null;
};
this.MarkWrongWords=function(_3e){
var _3f=function(_40,_41,_42,_43){
this.BadWords=_40;
this.WordOffsets=_41;
this.SpanId=_42;
this.Content=_43;
this.CurrentWordIndex=0;
this.Result=null;
};
_3f.prototype={GetSplitContent:function(){
var _44=new Array(this.BadWords.length*2+1);
for(var i=0;i<this.BadWords.length;i++){
var _46=i*2;
_44[_46]=this.GetBeforeText(i);
_44[_46+1]=this.BadWords[i].wordString;
}
_44[_44.length-1]=this.GetLastText();
return _44;
},GetMarkedSplitContent:function(_47){
for(var i=1;i<_47.length;i+=2){
_47[i]=this.GetMarkedWord(_47[i]);
this.CurrentWordIndex++;
}
return _47;
},GetBeforeText:function(_49){
var _4a=0;
var _4b=this.GetWordStartIndex(this.BadWords[_49]);
if(_49!=0){
badWordBefore=this.BadWords[_49-1];
_4a=this.GetWordEndCharIndex(badWordBefore);
}
return this.Content.substring(_4a,_4b);
},GetLastText:function(){
var _4c=this.BadWords[this.BadWords.length-1];
var _4d=this.GetWordEndCharIndex(_4c);
var _4e=this.Content.length;
return this.GetSubContent(_4d,_4e);
},GetWordEndCharIndex:function(_4f){
return this.GetWordStartIndex(_4f)+_4f.wordString.length;
},GetSubContent:function(_50,_51){
return this.Content.substring(_50,_51);
},GetWordStartIndex:function(_52){
return this.WordOffsets[_52.textOffset];
},GetMarkedWord:function(_53){
return "<span class='RadEWrongWord' id='"+this.SpanId+this.CurrentWordIndex+"'>"+_53+"</span>";
},GetResult:function(){
if(this.Result==null){
this.Result=this.GetMarkedSplitContent(this.GetSplitContent()).join("");
}
return this.Result;
}};
var _54=new _3f(this.WrongWordsArray,this.WordOffsets,this.SpanId,this.EscapeNewLines(_3e));
RadEditorNamespace.SetElementInnerHTML(this.Editor.Document.body,this.UnEscapeNewLines(_54.GetResult()));
};
this.EscapeNewLines=function(_55){
var _56=_55.replace(/\n/gi,"<telerikcr />");
_56=_56.replace(/\r/gi,"<teleriklf />");
return _56;
};
this.UnEscapeNewLines=function(_57){
var _58=_57.replace(/\<telerikcr\s*\/\>/gi,"\n");
var _58=_58.replace(/\<teleriklf\s*\/\>/gi,"\r");
return _58;
};
this.GetSuggestionsForWord=function(_59){
var _5a=this.WrongWordsArray;
for(var i=0;i<_5a.length;i++){
var _5c=_5a[i].wordString;
if(_5c==_59){
var _5d=_5a[i].suggestionsString;
if(_5d.length==0){
return [["",this.NoSuggestionsString]];
}else{
_5d=_5d.concat([]);
for(var j=0;j<_5d.length;j++){
_5d[j]=[_5d[j],_5d[j]];
}
}
return _5d;
}
}
return [];
};
this.GetFooterItems=function(_5f){
var _60=[];
if(this.Editor.SpellAllowAddCustom){
_60.splice(0,0,["rade_add_to_dictionary",this.AddToDictionaryString,this.AddIcon]);
}
_60.splice(0,0,["rade_change",this.ChangeWordString,this.OkIcon]);
if(this.OccursMoreThanOnce(_5f)){
_60.splice(0,0,["rade_ignore_all",this.IgnoreAllString,this.IgnoreIcon]);
}
_60.splice(0,0,["",this.IgnoreString,this.IgnoreIcon]);
return _60;
};
this.OccursMoreThanOnce=function(_61){
var _62=this.Editor.GetText();
var re=new RegExp("(\\b)"+_61+"(\\b)","g");
var res=_62.match(re);
return (res&&res.length>1);
};
this.Fire=function(_65,_66){
var _67=_66.SelectedValue;
var _68=this.SelectedEditorElement.innerHTML;
if(_67=="rade_add_to_dictionary"){
this.OnRaiseAddCustomWord(this.GetCurrentWrongWord());
}else{
if(_67=="rade_ignore_all"){
this.ClearWrongWords(_68,"");
}else{
if(_67=="rade_change"){
this.ShowSuggestionBox(this.SelectedEditorElement);
return;
}else{
var _69=false;
if(_67){
var _6a=this.OccursMoreThanOnce(_68);
if(_6a){
var _6b=confirm(this.MoreThanOnceMessage);
if(_6b){
this.ClearWrongWords(_68,_67);
_69=true;
}
}
}
if(!_69){
this.ClearHighlightedElement(this.SelectedEditorElement,_67);
}
}
}
}
this.SuggestionDropdown.Dispose();
this.SuggestionDropdown=null;
var res=this.IsHighlightedRemaining();
if(!res){
this.OnRaiseSpellcheckDone();
return;
}
this.Editor.FireEvent(RadEditorNamespace.RADEVENT_SEL_CHANGED);
if(this.AutomaticAdvance){
this.MoveToNextWrongWord();
}
};
this.ShowSuggestionDropdown=function(){
var _6d=this.Editor;
var _6e=_6d.GetSelection().GetParentElement();
if(!this.IsHighlightedWord(_6e)){
return;
}
this.SelectedEditorElement=_6e;
var _6f=160;
var _70=this.GetCurrentWrongWord();
if(this.SuggestionDropdown){
this.SuggestionDropdown.Dispose();
}
var _71=this;
var _72={};
_72.IsPopupScrollable=false;
_72.GetController=function(){
return _71;
};
_72.Type=RadEditorNamespace.TOOL_COMBOBOX;
_72.PopupClassName="RadESpellChecker";
_72.Name=this.LocalizedName;
_72.PopupWidth=_6f;
_72.WrongWordWrapper=_6e;
_72.GetDataFunction=function(){
var _73=_71.GetCurrentWrongWord();
return _71.GetSuggestionsForWord(_73);
};
_72.FooterItems=this.GetFooterItems(_70);
var _74=RadEditorNamespace.RadEditorSpellSuggestionBox.New(_72);
this.SuggestionDropdown=_74;
_74.ShowPopup(true);
_74.SelectNextItem();
if(_6d.IsIE){
_6d.SetFocus();
}
};
this.ShowSuggestionBox=function(_75){
var _76=this;
if(!this.SuggestionBox){
this.SuggestionBox=new RadEditorNamespace.RadEditorSuggestionTextBox(this.OkIcon,this.AddIcon);
}
var _77=this.SuggestionBox;
var _78=RadEditorNamespace.Utils.GetRect(this.Editor.ContentAreaElement);
var _79=RadEditorNamespace.Utils.GetRect(_75);
var _7a=this.Editor.ContentArea.scrollTop;
var _7b=this.GetCurrentWrongWord();
_77.ValueChanged=function(val){
_76.Editor.SetFocus();
_76.Fire("",{SelectedValue:val});
};
window.setTimeout(function(){
_77.SetValue(_7b);
_77.SetRect(_78.left+_79.left,_78.top+_79.top-_7a,_79.width+20,_79.height,_75);
},10);
};
}
RadEditorNamespace.RadEditorSpellSuggestionBox={New:function(_7d){
var obj=RadEditorNamespace.RadEditorComboBox.New(_7d);
RadEditorNamespace.Utils.ExtendObject(obj,this);
obj.SelectedValue="";
obj.SelectedIndex=-1;
obj.WrongWordWrapper=_7d.WrongWordWrapper;
obj.FooterItems=_7d.FooterItems;
obj.MaxItemSize=5;
return obj;
},OnDispose:function(){
this.WrongWordWrapper=null;
},GetPopupBodyElement:function(){
var _7f=this.Popup.CreateElement("DIV");
_7f.className=this.PopupClassName;
var _80=this.Popup.CreateElement("DIV");
_80.style.overflow="auto";
_7f.appendChild(_80);
return _7f;
},OnBeforeShowPopup:function(){
this.SelectedIndex=-1;
this.Element=this.WrongWordWrapper;
if(!this.IsCreated){
var _81=this.PopupBodyElement;
var _82=this.GetDefaultPopupTable("SuggestionTable",this.CellSpacing,this.CellPadding,"100%","");
this.PopupBodyElement=_82;
this.CreateItems();
var _83=22;
var _84=this.ItemsArray.length>this.MaxItemSize?this.MaxItemSize:this.ItemsArray.length;
var _85=_84>1?(_84*_83):25;
var _86=(this.FooterItems&&this.FooterItems.length)?this.FooterItems.length*_83:0;
var _87=this.GetDefaultPopupTable("ButtonTable",1,1,"100%","");
this.PopupBodyElement=_87;
this.CreatePopupFooter();
this.PopupBodyElement=_81;
var _88=this.PopupBodyElement.firstChild;
_88.appendChild(_82);
_88.style.height=_85+"px";
this.PopupBodyElement.appendChild(_87);
this.PopupHeight=_85+_86;
this.IsCreated=true;
}
},CreatePopupFooter:function(){
var _89=this.FooterItems;
var _8a=this.ItemsArray.length;
for(var i=0;i<_89.length;i++){
var _8c=this.AddRow();
var _8d=this.AddCell(_8c);
this.CreateCellContent(_8d,_89[i],_8a);
this.ConfigureCell(_8d,this,_8a);
_8a++;
}
this.ItemsArray=this.ItemsArray.concat(this.FooterItems);
},OnCellClick:function(_8e){
this.SetSelectedItem(_8e);
this.SelectUIItem();
},SelectPreviousItem:function(){
var _8f=this.SelectedIndex;
if(_8f-1>=0){
this.SelectedIndex--;
this.SetSelectedItem(this.SelectedIndex);
this.SelectUIItem();
}
},SelectNextItem:function(){
var _90=this.SelectedIndex;
if(_90+1<this.ItemsArray.length){
this.SelectedIndex++;
this.SetSelectedItem(this.SelectedIndex);
this.SelectUIItem();
}
},SelectUIItem:function(){
var _91=this.PopupBodyElement.getElementsByTagName("TD");
var len=_91.length;
var _93=this.SelectedIndex;
var _94=null;
for(var i=0;i<len;i++){
var _96=_91[i];
if(_96.className==_96.RadClassOver){
_96.className=_96.RadClassOut;
}
if(_96.Index==_93){
_96.className=_96.RadClassOver;
_94=_96;
}
}
if(_94){
if(_94.scrollIntoView){
_94.scrollIntoView(false);
}
}
}};
RadEditorNamespace.RadEditorSuggestionTextBox=function(_97){
this.Document=document;
this.ClassName="RadETextBox";
this.ButtonClassName="RadEXhtmlButton";
this.OkIcon=_97;
this.ValueChanged=function(_98){
};
var _99=this;
this.GlobalMouseHanlder=function(e){
var _9b=RadEditorNamespace.Utils.GetEventSource(e);
if(_9b&&RadEditorNamespace.Utils.IsParentNode(_99.Element,_9b)){
return;
}
_99.SetVisible(false);
};
this.RegisterMouseHandlers=function(_9c){
var _9d=function(_9e){
var _9f=window.frames;
for(var i=0;i<_9f.length;i++){
var _a1=null;
try{
_a1=_9f[i].window.document;
}
catch(ex){
continue;
}
if(_9e){
RadEditorNamespace.Utils.AttachEventEx(_a1,"mousedown",_99.GlobalMouseHanlder);
}else{
RadEditorNamespace.Utils.DetachEventEx(_a1,"mousedown",_99.GlobalMouseHanlder);
}
}
if(_9e){
RadEditorNamespace.Utils.AttachEventEx(window.document,"mousedown",_99.GlobalMouseHanlder);
}else{
RadEditorNamespace.Utils.DetachEventEx(window.document,"mousedown",_99.GlobalMouseHanlder);
}
};
_9d(_9c);
};
this.Dispose=function(){
this.RegisterMouseHandlers(false);
this.SetVisible(false);
if(null!=this.Element){
this.Element.onchange=null;
this.Element.onkeypress=null;
this.Element.onclick=null;
this.Element.Parent=null;
}
this.ValueChanged=null;
this.Element=null;
};
this.SetVisible=function(_a2){
if(this.Element){
this.Element.style.display=_a2?"":"none";
}
};
this.SetValue=function(_a3){
if(this.TextElement){
this.TextElement.value=_a3;
}
};
this.Fire=function(){
this.ValueChanged(this.TextElement.value);
this.SetVisible(false);
};
this.SetRect=function(x,y,_a6,_a7,_a8){
if(!this.Element||!this.Element.style){
return;
}
var oSt=this.Element.style;
oSt.position="absolute";
oSt.left=parseInt(x)+"px";
oSt.top=parseInt(y)+"px";
var _a8=this.TextElement;
_a8.style.width=parseInt(_a6)+"px";
_a8.style.height=parseInt(_a7)+"px";
oSt.width=parseInt(_a6)+100+"px";
oSt.height=_a8.style.height;
this.SetVisible(true);
try{
if(_a8){
_a8.focus();
}
if(_a8&&_a8.setActive){
_a8.setActive();
var _aa=document.selection.createRange();
_aa.moveStart("word",1);
_aa.select();
_aa.collapse();
}
}
catch(e){
}
};
this.Create=function(){
this.CreateTextBox();
this.RegisterMouseHandlers(true);
this.Document.body.appendChild(this.Element);
};
this.CreateTextBox=function(){
var _ab=this.Document.createElement("INPUT");
_ab.setAttribute("size","20");
_ab.className=this.ClassName;
_ab.Parent=this;
var _ac=function(e,_ae,_af){
if(_af){
if(_ae.Executed){
_ae.Executed=false;
return RadEditorNamespace.Utils.CancelEvent(e);
}
}
_ae.Executed=true;
_ae.Parent.Fire();
return RadEditorNamespace.Utils.CancelEvent(e);
};
_ab.onclick=new Function("this.focus();");
_ab.onkeypress=function(e){
if(!e){
e=window.event;
}
if(e&&e.keyCode==13){
return _ac(e,this);
}
};
this.TextElement=_ab;
var _b1=this.Document.createElement("div");
var _b2=this.Document.createElement("button");
_b2.style.height="22px";
_b2.style.width="22px";
_b2.className=this.ButtonClassName;
_b2.onmousedown=new Function("e","return false;");
_b2.onclick=new Function("e","this.parentNode.getElementsByTagName('input')[0].Parent.Fire();return false;");
_b2.innerHTML="<img align='absmiddle' src='"+this.OkIcon+"' border='0'>";
_b1.appendChild(_ab);
_b1.appendChild(_b2);
this.Element=_b1;
return this.Element;
};
this.Create();
};;RadEditorNamespace.OnToolClick=function(e){
RadEditorNamespace.Utils.CancelEvent(e);
this.Tool.OnElementClick();
};
RadEditorNamespace.RadToolBase={New:function(_2){
var _3=new RadEditorNamespace.RadEditorButton();
_3.GetController=_2.GetController;
_3.Name=_2.Name;
_3.Shortcut=_2.Shortcut;
_3.Type=_2.Type;
_3.IconUrl=_2.IconUrl;
_3.Title=_2.Title;
if(null!=_2.ShowIcon){
_3.ShowIcon=_2.ShowIcon;
}
if(null!=_2.ShowText){
_3.ShowText=_2.ShowText;
}
if(null!=_2.TextPosition){
_3.TextPosition=_2.TextPosition;
}
if(null!=_2.Document){
_3.Document=_2.Document;
}
return _3;
}};
RadEditorNamespace.RadEditorButton=function(){
this.Document=null;
this.Name=null;
this.State=null;
this.Element=null;
this.Document=document;
this.Type="B";
this.ClassName="RadETool";
this.State=RadEditorNamespace.RADCOMMAND_STATE_OFF;
this.ShowIcon=true;
this.ShowText=false;
this.TextPosition="right";
};
RadEditorNamespace.RadEditorButton.prototype.Dispose=function(){
var _4=this.Element;
if(_4){
_4.onclick=null;
_4.Tool=null;
}
this.Element=null;
this.Document=null;
};
RadEditorNamespace.RadEditorButton.prototype.GetButtonTable=function(_5,_6){
var _7=RadEditorNamespace.Utils.GetPlainTable(_5);
if(_6){
_7.className=_6;
}
return _7;
};
RadEditorNamespace.RadEditorButton.prototype.GetDefaultDiv=function(_8,_9,_a){
var _b=_8.createElement("DIV");
if(_a){
_b.style.whiteSpace="nowrap";
}
_b.setAttribute("unselectable","on");
var _c=_b.cloneNode(true);
return _c;
};
RadEditorNamespace.RadEditorButton.prototype.GetDefaultImage=function(_d){
return _d.createElement("IMG");
};
RadEditorNamespace.RadEditorButton.prototype.GetToolButton=function(){
var _e=null;
if(this.ShowIcon){
var _f=this.GetDefaultImage(this.Document);
_f.src=this.IconUrl;
_f.align="absmiddle";
_f.ondragstart=RadEditorNamespace.Utils.OnItemDragStart;
_e=_f;
}
if(this.ShowText&&this.Title){
var _10=this.GetButtonTable(this.Document,"");
_10.setAttribute("align","center");
_10.style.width="100%";
var _11=_10.insertRow(-1);
if(_e){
var _12=_11.insertCell(-1);
_12.appendChild(_f);
_12.align="center";
_12.setAttribute("unselectable","on");
}
if("bottom"==this.TextPosition){
_11=_10.insertRow(-1);
}
var _12=_11.insertCell(-1);
if(this.TextPosition!="right"){
_12.align="center";
}
_12.setAttribute("width","100%");
_12.noWrap=true;
_12.innerHTML=this.Title;
_12.className="RadEToolText";
_12.setAttribute("unselectable","on");
_e=_10;
}
return _e;
};
RadEditorNamespace.RadEditorButton.prototype.Create=function(_13){
if(null==_13&&this.OnCreate){
this.OnCreate();
}else{
this.Element=_13;
}
this.Element.Tool=this;
if(!this.Element.title){
this.Element.title=this.Title+(this.Shortcut?" ("+this.Shortcut+")":"");
}
this.UpdateState();
return this.Element;
};
RadEditorNamespace.RadEditorButton.prototype.OnCreate=function(){
this.Element=this.GetToolButton();
this.Element.onclick=new Function("RadEditorNamespace.OnToolClick.call(this); return false;");
};
RadEditorNamespace.RadEditorButton.prototype.GetTopElement=function(){
return this.Element;
};
RadEditorNamespace.RadEditorButton.prototype.SetState=function(_14,_15){
if(_14==this.State&&(true!=_15)){
return;
}
this.State=_14;
this.UpdateState();
};
RadEditorNamespace.RadEditorButton.prototype.GetState=function(_16){
return this.State;
};
RadEditorNamespace.RadEditorButton.prototype.UpdateState=function(){
var _17=this.Element;
var _18=this.ClassName;
if(RadEditorNamespace.RADCOMMAND_STATE_DISABLED==this.State){
_17.className=_18+"Disabled";
_17.onmouseover=null;
_17.onmouseout=null;
_17.onmouseup=null;
_17.onmousedown=null;
}else{
_17.classNameOut=_17.className=_18+(RadEditorNamespace.RADCOMMAND_STATE_OFF==this.State?"Off":"On");
_17.classNameOver=_17.className+"Over";
_17.onmouseover=new Function("this.className = this.classNameOver;");
_17.onmouseout=new Function("this.className = this.classNameOut;");
if("B"==this.Type){
_17.onmousedown=new Function("this.classNameUp = this.className; this.className = 'RadEToolDown';");
_17.onmouseup=new Function("this.className = this.classNameUp;");
}
}
};
RadEditorNamespace.RadEditorButton.prototype.GetIcon=function(){
return this.GetTopElement();
};
RadEditorNamespace.RadEditorButton.prototype.OnElementClick=function(){
if(RadEditorNamespace.RADCOMMAND_STATE_DISABLED==this.State){
return;
}
this.GetController().Fire(this.Name,this);
};;RadEditorNamespace.RadEditorButtonComboBox={New:function(_1){
_1.PopupClassName="RadEContextMenu";
_1.CellSpacing=0;
_1.CellPadding=0;
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.Tools=[];
_2.FireOnClose=false;
return _2;
},OnBeforeShowPopup:function(){
if(!this.ItemsCreated){
this.CreateItems();
this.ItemsCreated=true;
}
var _3=this.ItemsArray.length;
var _4=_3/this.ItemsPerRow+(_3%this.ItemsPerRow?1:0);
this.PopupHeight=2+(_4*24);
this.GetController().SetToolState(this.Tools,true);
},CreateCellContent:function(_5,_6,_7){
var _8=_6;
var _9=this.Popup.GetDocument();
var _a=this.GetController().CreateButtonTool(_8[1],null,_9,null,null,true);
_5.appendChild(_a.GetTopElement());
this.Tools[this.Tools.length]=_a;
}};;RadEditorNamespace.RadColorPicker={New:function(_1){
_1.ClassName="RadEToolLong";
_1.PopupWidth=120;
_1.PopupHeight=120;
_1.CellSpacing=1;
_1.CellPadding=1;
_1.PopupClassName="RadEColorPicker";
_1.PopupTableWidth="";
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.AllowCustomColors=_1.AllowCustomColors!=null?_1.AllowCustomColors:true;
_2.ItemsPerRow=6;
_2.ImageX="x.gif";
_2.AddCustomColorLabel=_1.AddCustomColor?_1.AddCustomColor:"Add Custom Color";
_2.AddCustomHexColorLabel=_1.AddCustomHexColor?_1.AddCustomHexColor:"Add Hex Color";
_2.PromptColorMessage=_1.PromptColor?_1.PromptColor:"Hex color:";
_2.CustomColorsRow=null;
_2.IsPopupScrollable=false;
return _2;
},OnHeaderElementClick:function(){
this.FireOnClose=true;
if(this.SelectedValue!=null){
this.OnPopupClick();
return false;
}
},OnCustomColorAdded:function(_3){
this.ItemsArray[this.ItemsArray.length]=_3;
this.SetValue(_3);
this.OnPopupClick();
},OnCellClick:function(_4){
this.SetValue(this.ItemsArray[_4]);
},SetValue:function(_5){
this.SelectedValue=_5;
if(this.OnValueSet){
this.OnValueSet();
}
},OnValueSet:function(){
if(""==this.SelectedValue||null==this.SelectedValue){
this.HeaderElement.style.borderBottom="";
}else{
this.HeaderElement.style.borderBottom="3px solid "+this.SelectedValue;
}
},OnBeforeShowPopup:function(){
if(!this.ItemsCreated){
this.CreateItems();
this.CreatePopupFooter();
this.ItemsCreated=true;
}
this.FireOnClose=true;
},CreatePopupFooter:function(){
if(this.AllowCustomColors){
this.AddCustomColorButton();
this.AddHexColorButton();
}
},AddEmptyCell:function(_6){
this.AddCell(_6);
},AddColorCell:function(_7,_8){
if(null==_8){
_8=this.ItemsArray.length;
}
var _9=this.AddCell(_7);
this.ConfigureCell(_9,this,_8);
return _9;
},AddCustomColorButton:function(){
if(document.addEventListener){
return;
}
var _a=this.AddRow();
var _b=this.AddTextCell(_a,this.AddCustomColorLabel);
this.CustomColorDlg=this.Popup.CreateElement("OBJECT");
this.CustomColorDlg.classid="clsid:3050f819-98b5-11cf-bb82-00aa00bdce0b";
this.CustomColorDlg.style.width=0;
this.CustomColorDlg.style.height=0;
_b.appendChild(this.CustomColorDlg);
_b.Parent=this;
_b.onclick=new Function("this.Parent.OnAddCustomColor()");
},AddHexColorButton:function(){
var _c=this.AddRow();
var _d=this.AddTextCell(_c,this.AddCustomHexColorLabel);
_d.Parent=this;
_d.onclick=new Function("this.Parent.OnAddHexColor()");
},CreateItems:function(){
var _e=null;
var _f=0;
this.ItemsArray=this.GetDataFunction(this.Name);
if(this.ItemsArray&&this.ItemsArray.length){
this.ItemsArray=this.ItemsArray.concat([]);
}
for(var i=0;i<this.ItemsArray.length;i++){
if(0==i%this.ItemsPerRow){
_e=this.AddRow();
_f=0;
}
_f++;
var _11=this.AddColorCell(_e,i);
this.CreateCellContent(_11,this.ItemsArray[i],i);
}
var _12=this.ItemsPerRow-_f-1;
if(_12>0){
for(var i=0;i<=_12;i++){
this.AddEmptyCell(_e);
}
}
},CreateCellContent:function(_13,_14,_15){
if(""==_14){
_13.style.backgroundRepeat="no-repeat";
_13.style.backgroundPosition="center";
}
var _16=this.Popup.CreateElement("div");
_16.style.backgroundColor=_14;
if(TelerikNamespace.Utils.DetectBrowser("safari")){
_16.style.innerHTML="&nbsp;";
_16.style.height="14px";
_16.style.width="14px";
}
_13.appendChild(_16);
_13.Value=_14;
_13.setAttribute("title",_14);
},AddTextCell:function(_17,_18){
var _19=this.AddCell(this.AddRow());
_19.colSpan=this.ItemsPerRow;
_19.noWrap=true;
_19.innerHTML=_18;
return _19;
},AddCustomColor:function(_1a){
if(!this.CustomColorsRow||this.CustomColorsRow.cells.length==this.ItemsPerRow){
this.CustomColorsRow=this.AddRow();
this.PopupHeight+=17;
}
var _1b=this.AddColorCell(this.CustomColorsRow);
this.CreateCellContent(_1b,_1a);
},OnAddCustomColor:function(){
if(!this.CustomColorDlg){
return;
}
var _1c=this.CustomColorDlg.ChooseColorDlg();
if(_1c){
_1c=this.ConvertColor(_1c);
this.AddCustomColor(_1c);
this.OnCustomColorAdded(_1c);
}else{
this.FireOnClose=false;
}
},OnAddHexColor:function(){
var _1d=prompt(this.PromptColorMessage,"#");
_1d=this.ValidateColor(_1d);
if(""!=_1d){
this.AddCustomColor(_1d);
this.OnCustomColorAdded(_1d);
}else{
this.FireOnClose=false;
}
},ConvertColor:function(_1e){
_1e=parseInt(_1e);
_1e=_1e.toString(16);
if(_1e.length<6){
var _1f="000000".substring(0,(6-_1e.length));
_1e="#"+_1f.concat(_1e).toUpperCase();
}else{
_1e="#"+_1e.toUpperCase();
}
return _1e;
},ValidateColor:function(_20){
if(null==_20){
return "";
}
if(_20.charAt(0)!="#"){
_20="#"+_20;
}
re=new RegExp("#[0-9a-fA-F]{6}","gi");
return re.exec(_20)?_20:"";
}};;RadEditorNamespace.RadEditorComboBox={New:function(_1){
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.SelectedValue="";
_2.SelectedIndex=-1;
return _2;
},OnBeforeShowPopup:function(){
this.SelectedIndex=-1;
if(!this.ItemsCreated){
this.CreateItems();
this.ItemsCreated=true;
}
},SetSelectedItem:function(_3){
var _4=this.ItemsArray[_3];
if(_4){
this.SelectedValue=_4[0];
this.SelectedIndex=_3;
}
},OnCellClick:function(_5){
this.SetSelectedItem(_5);
},CreateCellContent:function(_6,_7,_8){
var _9=this.Popup.GetDocument();
var _a=_9.createElement("span");
if(_7){
var _b=_7[1];
var _c=_7[2];
if(_c){
var _d=_9.createElement("img");
_d.src=_c;
_d.style.marginRight="5px";
_d.setAttribute("align","absmiddle");
_a.appendChild(_d);
}
if(_b){
_a.innerHTML+=_b;
_a.noWrap=true;
}
}
_6.appendChild(_a);
}};;RadEditorNamespace.OnItemMouseOver=function(){
this.className=this.RadClassOver;
};
RadEditorNamespace.OnItemMouseOut=function(){
this.className=this.RadClassOut;
};
RadEditorNamespace.OnComboHeaderClick=function(){
var _1=this;
var _2=null;
while(null==(_2=_1.Tool)){
_1=_1.parentNode;
}
_2.HeaderElementClick();
return false;
};
RadEditorNamespace.OnComboArrowClick=function(){
var _3=this;
var _4=null;
while(null==(_4=_3.Tool)){
_3=_3.parentNode;
}
_4.OnArrowClick();
return false;
};
RadEditorNamespace.RadComboBoxBase={IsPopupScrollable:true,RecreateBeforeShow:false,HeaderElement:null,ArrowElement:null,PopupBodyElement:null,Popup:null,ClassName:"",PopupWidth:0,PopupHeight:0,ItemsPerRow:1,SelectedValue:null,IconContainer:null,ArrowContainer:null,FireOnClose:true,GetDataFunction:null,AutomaticHeight:false,New:function(_5){
var _6=RadEditorNamespace.RadToolBase.New(_5);
RadEditorNamespace.Utils.ExtendObject(_6,this);
if(_5.UpdateValue!=null){
_6.UpdateValue=_5.UpdateValue;
}
_6.ClassName=_5.ClassName?_5.ClassName:"RadEDropDown";
_6.ItemsPerRow=_5.ItemsPerRow?_5.ItemsPerRow:1;
_6.ArrowUrl=_5.ArrowUrl;
_6.GetDataFunction=_5.GetDataFunction?_5.GetDataFunction:function(){
return [];
};
_6.PopupWidth=parseInt(_5.PopupWidth);
if(isNaN(_6.PopupWidth)){
_6.PopupWidth=100;
}
_6.PopupHeight=parseInt(_5.PopupHeight);
if(isNaN(_6.PopupHeight)){
_6.PopupHeight=100;
}
_6.Width=_5.Width?_5.Width:"30px";
_6.CellSpacing=_5.CellSpacing!=null?_5.CellSpacing:2;
_6.CellPadding=_5.CellPadding!=null?_5.CellPadding:2;
_6.PopupClassName=_5.PopupClassName?_5.PopupClassName:"";
_6.PopupTableWidth=_5.PopupTableWidth;
_6.IsPopupScrollable=(_5.IsPopupScrollable!=false);
_6.AutomaticHeight=(_5.AutomaticHeight==true);
_6.Popup=window["RadEditorPopupInstance"];
return _6;
},CreateItems:function(){
this.ItemsArray=this.GetDataFunction(this.Name);
var _7=this.ItemsArray;
var _8=null;
for(var i=0;i<_7.length;i++){
if(0==(i%this.ItemsPerRow)){
_8=this.AddRow();
}
var _a=this.AddCell(_8);
this.ConfigureCell(_a,this,i);
this.CreateCellContent(_a,_7[i],i);
}
},ConfigureCell:function(_b,_c,i){
_b.Index=i;
_b.Parent=_c;
_b.onclick=new Function(" if (this.Parent.OnCellClick) this.Parent.OnCellClick(this.Index, this);"+"this.className = this.RadClassOut;");
},Dispose:function(){
if(this.Element){
this.Element.onclick=null;
this.Element.Tool=null;
}
this.Element=null;
this.Popup=null;
this.ArrowElement=null;
if(this.IconContainer){
this.IconContainer.onclick=null;
this.IconContainer=null;
}
if(this.ArrowContainer){
this.ArrowContainer.onclick=null;
this.ArrowContainer=null;
}
if(this.OnDispose!=null&&typeof (this.OnDispose)=="function"){
try{
this.OnDispose();
}
catch(e){
}
}
if(this.PopupBodyElement!=null){
try{
}
catch(e){
}
}
this.PopupBodyElement=null;
},OnCreate:function(){
var _e=this.GetButtonTable(this.Document,"RadEDropDownOff");
_e.setAttribute("title",this.Title);
var _f=_e.insertRow(-1);
var _10=_f.insertCell(-1);
_10.setAttribute("unselectable","on");
if(this.IconUrl){
this.ShowIcon=true;
var _11=this.GetToolButton();
_10.appendChild(_11);
this.HeaderElement=_11;
}else{
var _12=this.CreateHeaderElement();
if(null==_12){
_12=this.GetDefaultDiv(this.Document);
_12.innerHTML=this.Title;
_12.style.whiteSpace="nowrap";
if(!document.all){
_12.style.overflow="hidden";
}
_12.style.width=this.Width;
}
this.HeaderElement=_12;
_10.appendChild(this.HeaderElement);
var _13=this.Document.createElement("colgroup");
var col=this.Document.createElement("col");
col.setAttribute("width",this.Width);
_13.appendChild(col);
col=this.Document.createElement("col");
col.setAttribute("width","14px");
_13.appendChild(col);
_e.insertBefore(_13,_e.firstChild);
_e.style.tableLayout="fixed";
}
this.IconContainer=_10;
_10.onclick=RadEditorNamespace.OnComboHeaderClick;
this.ArrowElement=this.CreateArrowElement();
if(this.ArrowElement){
var _15=_f.insertCell(-1);
_15.appendChild(this.ArrowElement);
_15.onclick=RadEditorNamespace.OnComboArrowClick;
this.ArrowContainer=_15;
}
this.Element=_e;
},CreateArrowElement:function(){
if(this.ArrowUrl){
var _16=this.GetDefaultImage(this.Document);
_16.src=this.ArrowUrl;
_16.ondragstart=RadEditorNamespace.Utils.OnItemDragStart;
_16.border=0;
if(!document.all){
_16.setAttribute("align","absbottom");
}
return _16;
}
},ShowPopup:function(_17){
if(_17){
if(!this.PopupDocument){
this.PopupDocument=this.Popup.GetDocument();
}
if(!this.PopupBodyElement||this.RecreateBeforeShow){
this.PopupBodyElement=this.GetPopupBodyElement();
}
this.Popup.SetClassName(this.IsPopupScrollable?"RadEDropdownMenu":"RadEDropdownMenuNonScrollable");
this.OnBeforeShowPopup();
var _18=this;
this.PopupBodyElement.onclick=function(){
if(_18.OnPopupClick){
_18.OnPopupClick();
}
};
this.Popup.SetTopElement(this.PopupBodyElement);
this.Popup.ShowDropdown(this.PopupWidth,this.PopupHeight,this.Element,this.IsPopupScrollable,this.AutomaticHeight);
this.PopupDocument=null;
}else{
this.Popup.Hide();
}
},GetDefaultPopupTable:function(_19,_1a,_1b,_1c,_1d){
var _1e=RadEditorNamespace.Utils.GetPlainTable(this.Popup.GetDocument());
_1e.cellSpacing=_1a?_1a:0;
_1e.cellPadding=_1b?_1b:0;
_1e.className=_19?_19:"";
_1e.style.width=_1c!=null?_1c:"100%";
_1e.style.height=_1d!=null?_1d:"100%";
return _1e;
},GetPopupBodyElement:function(){
return this.GetDefaultPopupTable(this.PopupClassName?this.PopupClassName:"RadEDropDownTable",this.CellSpacing,this.CellPadding,this.PopupTableWidth,"");
},AddRow:function(){
return (this.PopupBodyElement.insertRow(-1));
},AddCell:function(_1f){
var _20=_1f.insertCell(-1);
_20.RadClassOut="";
_20.RadClassOver="Over";
_20.onmouseover=RadEditorNamespace.OnItemMouseOver;
_20.onmouseout=RadEditorNamespace.OnItemMouseOut;
return _20;
},OnPopupClick:function(){
if(true==this.CancelHide){
return;
}
this.Popup.Hide();
if(this.FireOnClose){
this.GetController().Fire(this.Name,this);
}
},HeaderElementClick:function(){
if(RadEditorNamespace.RADCOMMAND_STATE_DISABLED==this.State){
return;
}
var _21=true;
if(this.OnHeaderElementClick!=null){
_21=this.OnHeaderElementClick();
}
if(false!=_21){
this.ShowPopup(true);
}
},OnArrowClick:function(){
if(RadEditorNamespace.RADCOMMAND_STATE_DISABLED==this.State){
return;
}
this.ShowPopup(!this.Popup.IsVisible());
},OnElementClick:function(){
},GetSelectedValue:function(){
return this.SelectedValue;
},CreateHeaderElement:function(){
return null;
},OnBeforeShowPopup:function(){
}};;RadEditorNamespace.RadCssCombo={New:function(_1){
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.PopupIconPath=_1.PopupIconPath;
_2.ClearStyleString=_1.ClearStyleString;
return _2;
},SetValue:function(_3){
this.UpdateValue(_3);
},UpdateValue:function(_4){
try{
var _5=this.GetController();
if(!_4){
this.HeaderElement.innerHTML=this.Title;
return;
}else{
if(!document.all&&_5.GetSelectedElement){
var _6=_5.GetSelectedElement();
if(_6&&_6.tagName=="BODY"){
this.HeaderElement.innerHTML=this.Title;
return;
}
}
}
var _7=_5.GetNamedCssForSelectedElement(_4);
if(_7){
_4=_7;
}
}
catch(e){
}
this.HeaderElement.innerHTML=_4;
},OnDispose:function(){
this.UniqueIndexer=null;
this.CssArray=null;
},OnCellClick:function(_8){
if(_8<0){
this.SelectedValue="";
}else{
this.SelectedValue=this.CssArray[_8].ClassName;
}
},CreatePopupHeader:function(){
oRow=this.AddRow(-1);
oCell=this.AddCell(oRow);
oCell.noWrap=true;
oCell.innerHTML=this.ClearStyleString;
this.ConfigureCell(oCell,this,-1);
return true;
},OnBeforeShowPopup:function(){
if(!this.IsCreated){
this.UniqueIndexer=[];
this.CreatePopupHeader();
this.CssArray=this.GetDataFunction(this.Name);
this.CreateItems();
this.IsCreated=true;
}
},CreateItems:function(){
if(this.PopupBodyElement.rows.length>0){
var _9=this.PopupBodyElement;
if(_9.parentNode&&_9.parentNode.removeChild){
_9.parentNode.removeChild(_9);
}
this.PopupBodyElement=this.GetPopupBodyElement();
this.CreatePopupHeader();
}
var _a=0;
for(var i=0;i<this.CssArray.length;i++){
var _c=this.AddRow();
var _d=this.AddCell(_c);
_d.noWrap=true;
this.ConfigureCell(_d,this,i);
var _e=this.CreateCellContent(_d,this.CssArray[i],i);
if(false==_e){
_d.parentNode.removeChild(_d);
}else{
_a++;
}
}
return _a;
},CreateCellContent:function(_f,_10,_11){
var _12=_10;
var tag=_12.Tag;
var _14=_12.Rule;
var _15=_12.Alias;
if(!tag){
return false;
}else{
tag=tag.toUpperCase();
}
var _16=true;
if(_14){
if(this.UniqueIndexer[_14.selectorText]){
_f=this.UniqueIndexer[_14.selectorText];
_f.innerHTML="";
_16=false;
}else{
this.UniqueIndexer[_14.selectorText]=_f;
}
}
this.FillCell(_f,tag,_14,_15);
return _16;
},FillCell:function(_17,tag,_19,_1a){
var _1b=this.GetCssClassIcon(tag);
_17.appendChild(_1b);
var _1c=this.PopupDocument.createElement("SPAN");
switch(tag){
case "A":
var _1d=this.PopupDocument.createElement("A");
_1d.href="#";
_1d.onmouseover="window.status = ''; return false;";
_1d.onclick=new Function("return false;");
_1d.innerHTML=_1a;
this.ApplyRule(_1d,_19);
_1d.style.cursor="default";
_1c.appendChild(_1d);
break;
default:
_1c.innerHTML=_1a;
_1c.style.font="icon";
this.ApplyRule(_1c,_19);
_1c.style.marginTop="2px";
break;
}
_1c.style.position="";
_1c.style.marginLeft="0px";
_1c.style.overflowX="hidden";
_17.appendChild(_1c);
_17.setAttribute("title",(_19)?_19.selectorText:_1a);
},ApplyRule:function(_1e,_1f){
if(!_1e||!_1f){
return;
}
_1e.style.cssText=_1f.style.cssText;
var _20=_1e.style.backgroundColor.toLowerCase();
var _21=_1e.style.color.toLowerCase();
if((""==_20||"#ffffff"==_20||"white"==_20)&&("#ffffff"==_21||"white"==_21)){
_1e.style.backgroundColor="#aaaaaa";
}
_1e.style.width="";
_1e.style.height="";
},GetCssClassIcon:function(tag){
if(!this.__defaultImg){
var _23=this.PopupDocument.createElement("IMG");
_23.style.marginRight="5px";
_23.style.width="12px";
_23.style.height="13px";
_23.setAttribute("align","absmiddle");
this.__defaultImg=_23;
}
var _24=this.__defaultImg.cloneNode(false);
_24.src=this.GetCssClassImageSrcByTag(tag);
return _24;
},GetCssClassImageSrcByTag:function(tag){
var _26="";
switch(tag){
case "ALL":
case "A":
case "IMG":
case "TABLE":
case "P":
_26=tag;
break;
default:
_26="Custom";
break;
}
return this.PopupIconPath+"class"+_26+".gif";
}};;RadEditorNamespace.RadInsertTableCombo={New:function(_1){
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.Localization=_1.Localization;
_2.CancelLabel=_1.CancelLabel?_1.CancelLabel:"Cancel";
_2.TableWizardLabel=_1.TableWizardLabel?_1.TableWizardLabel:"Table Wizard";
_2.TableLabel=_1.TableLabel?_1.TableLabel:"Table";
_2.IconBasePath=_1.IconBasePath;
_2.TableTools=[];
_2.SetCellPropsTool=null;
_2.SetTablePropsTool=null;
_2.IsPopupScrollable=false;
_2.ItemsPerRow=6;
return _2;
},Fire:function(_3,_4){
_4.SetState(RadEditorNamespace.RADCOMMAND_STATE_DISABLED);
this.GetController().Fire(_3,_4);
},GetPopupBodyElement:function(){
this.WizardTable=this.GetDefaultPopupTable("RadETablePicker",this.CellSpacing,this.CellPadding,null,"");
var _5=this.Popup.CreateElement("div");
_5.appendChild(this.WizardTable);
var _6=this.WizardTable;
_6.style.overflowY="hidden";
_6.Parent=this;
_6.onmouseout=new Function("this.Parent.OnSampleTableMouseOut();");
return _5;
},OnBeforeShowPopup:function(){
this.FireOnClose=false;
if(!this.IsCreated){
this.CreateItems(this.WizardTable);
this.IsCreated=true;
this.CreatePopupFooter();
}
this.GetController().SetToolState(this.TableTools);
if(this.SetCellPropsTool){
this.SetCellPropsTool.SetState(RadEditorNamespace.RADCOMMAND_STATE_OFF);
}
if(this.SetTablePropsTool){
this.SetTablePropsTool.SetState(RadEditorNamespace.RADCOMMAND_STATE_OFF);
}
this.UpdateSampleTable(0,0);
},OnDispose:function(){
this.WizardTable=null;
this.TableInfoLabel=null;
var _7=this.TableTools;
if(_7&&_7.length>0){
for(var i=0;i<_7.length;i++){
if(_7[i].Dispose){
_7[i].Dispose();
}
}
}
_7=null;
this.TableTools=null;
this.SetCellPropsTool=null;
this.SetTablePropsTool=null;
},CreateItems:function(_9){
for(var i=0;i<36;i++){
if(0==i%this.ItemsPerRow){
oRow=_9.insertRow(-1);
curRowItems=0;
}
oCell=oRow.insertCell(-1);
oCell.width=10;
oCell.height=10;
oCell.innerHTML="&nbsp;";
oCell.style.fontSize="5pt";
oCell.Parent=this;
oCell.onmouseover=new Function("this.Parent.OnSampleTableCellOver(this);");
oCell.onclick=new Function("this.Parent.OnCellClick(this);");
}
},CreatePopupFooter:function(){
var _b=this.WizardTable;
oRow=_b.insertRow(-1);
oCell=oRow.insertCell(-1);
oCell.colSpan=this.ItemsPerRow;
oCell.className="Counter";
oCell.innerHTML=this.CancelLabel;
this.TableInfoLabel=oCell;
oRow=_b.insertRow(-1);
oCell=oRow.insertCell(-1);
oCell.colSpan=this.ItemsPerRow;
oCell.className="Wizard";
oCell.Parent=this;
oCell.onmouseover=new Function("this.className = 'WizardOver'");
oCell.onmouseout=new Function("this.className = 'Wizard'");
oCell.onclick=new Function("this.Parent.StartTableWizard();this.className = 'Wizard';");
oCell.innerHTML=this.TableWizardLabel;
this.CreateTableButtons();
},CreateTableButtons:function(){
var _c=4;
toolNamesArray=[RadEditorNamespace.RADCOMMAND_INSERT_ROW_ABOVE,RadEditorNamespace.RADCOMMAND_INSERT_ROW_BELOW,RadEditorNamespace.RADCOMMAND_DELETE_ROW,RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_LEFT,RadEditorNamespace.RADCOMMAND_INSERT_COLUMN_RIGHT,RadEditorNamespace.RADCOMMAND_DELETE_COLUMN,RadEditorNamespace.RADCOMMAND_MERGE_COLUMNS,RadEditorNamespace.RADCOMMAND_MERGE_ROWS,RadEditorNamespace.RADCOMMAND_SPLIT_CELL,RadEditorNamespace.RADCOMMAND_DELETE_CELL,RadEditorNamespace.RADCOMMAND_SET_CELL_PROPERTIES,RadEditorNamespace.RADCOMMAND_SET_TABLE_PROPERTIES];
this.ToolsTable=this.GetDefaultPopupTable("RadETablePickerToolTable",0,0,null,"");
var _d=this.ToolsTable;
var _e=_d.insertRow(-1);
var _f=this.Popup.GetDocument();
for(var i=0;i<toolNamesArray.length;i++){
var _11=toolNamesArray[i];
var _12=this.IconBasePath+_11+".gif";
var _13=this.GetController().CreateButtonTool(_11,this,_f,_12,true,false,null);
this.TableTools[this.TableTools.length]=_13;
if(i%_c==0){
_e=_d.insertRow(-1);
}
var _14=_e.insertCell(-1);
_14.appendChild(_13.GetTopElement());
}
this.SetCellPropsTool=this.TableTools[this.TableTools.length-2];
this.SetTablePropsTool=this.TableTools[this.TableTools.length-1];
this.PopupBodyElement.appendChild(this.ToolsTable);
_f=null;
},OnSampleTableMouseOut:function(){
if(!this.OnSampleTable){
this.UpdateSampleTable(0,0);
}
this.OnSampleTable=false;
},OnSampleTableCellOver:function(_15){
this.OnSampleTable=true;
var _16=RadEditorNamespace.Utils.GetCellIndex(_15);
var _17=_15?(_15.parentNode.rowIndex+1):0;
this.UpdateSampleTable(_16,_17);
},OnCellClick:function(_18){
var _19=RadEditorNamespace.Utils.GetCellIndex(_18);
var _1a=_18?(_18.parentNode.rowIndex+1):0;
this.SelectedValue={RowsCount:_1a,ColumnsCount:_19};
this.FireOnClose=true;
},UpdateSampleTable:function(_1b,_1c){
for(var i=0;i<this.ItemsPerRow;i++){
var row=this.WizardTable.rows[i];
if(!row){
return;
}
for(var j=0;j<row.cells.length;j++){
var _20=row.cells[j];
_20.className=(i<_1c&&j<_1b)?"Over":"";
}
}
this.UpdateTableInfoLable(_1b,_1c);
},UpdateTableInfoLable:function(_21,_22){
var _23=this.CancelLabel;
if(_21>0&&_22>0){
_23=RadEditorNamespace.Utils.Format("{0} x {1} {2}",_22,_21,this.TableLabel);
}
this.TableInfoLabel.innerHTML=_23;
},StartTableWizard:function(){
this.SelectedValue=null;
var _24=this.GetController();
if(_24){
_24.Fire(RadEditorNamespace.RADCOMMAND_TABLE_WIZARD,this);
}
}};;RadEditorNamespace.RadInsertLinkCombo={New:function(_1){
_1.PopupClassName="RadELinks";
_1.CellSpacing=0;
_1.CellPadding=0;
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.BasePath=_1.BasePath;
return _2;
},OnBeforeShowPopup:function(){
if(!this.DropdownCreated){
this.CreateDropdown();
this.DropdownCreated=true;
}
},CreateDropdown:function(){
var _3=this.GetDataFunction(this.Name);
this.LinkCounter=0;
this.FlatLinksArray=[];
for(var i=0;i<_3.length;i++){
var _5=_3[i];
this.ParseLinkSubtree(_5,this.PopupBodyElement);
}
},OnCellClick:function(_6,_7){
var _8=this.FlatLinksArray[_6];
if(_8.length>4){
if(_8[1]==""){
this.ExpandCategory(_7);
}else{
this.InsertLink(_7);
}
}else{
this.InsertLink(_7);
}
},ParseLinkSubtree:function(_9,t){
var _b=this.LinkCounter++;
this.FlatLinksArray[_b]=_9;
var tr=t.insertRow(-1);
var tc=tr.insertCell(-1);
tc.width=9;
tc.noWrap=true;
tc.Parent=this;
if(_9.length>4){
tc.onclick=function(){
this.Parent.ExpandCategory(this);
};
}
tc.innerHTML=_9.length>4?"<img align=absmiddle src='"+this.BasePath+"Img/linksPlus.gif'>":"&nbsp;";
tc=tr.insertCell(tr.cells.length);
tc.LinkItem=_9;
tc.innerHTML=_9[0];
if(_9[1]!=""){
tc.onmouseover=new Function("this.className = 'Over'");
tc.onmouseout=new Function("this.className = ''");
}
tc.width="100%";
this.ConfigureCell(tc,this,_b);
if(_9.length>4){
tr=t.insertRow(-1);
tc=tr.insertCell(-1);
tc=tr.insertCell(-1);
var _e=this.Popup.GetDocument().createElement("TABLE");
_e.cellPadding=0;
_e.cellSpacing=0;
_e.style.width="100%";
tc.appendChild(_e);
var _f=_9.length;
if(_f>4){
for(var i=0;i<_9[4].length;i++){
var _11=_9[4][i];
this.ParseLinkSubtree(_11,_e);
}
}
tr.style.display="none";
}
},InsertLink:function(_12){
_12.className="";
var _13=_12.LinkItem;
if(!_13[1]){
this.CancelHide=true;
return;
}
this.CancelHide=false;
this.SelectedValue={Text:_13[0],Href:_13[1],Target:_13[2],Title:_13[3]};
},ExpandCategory:function(_14){
this.CancelHide=true;
var _15=_14.parentNode.nextSibling;
var _16=(_15.style.display=="none")?"":"none";
_15.style.display=_16;
var _17=_14.parentNode.getElementsByTagName("IMG");
var _18=_17&&_17.length>0?_17.item(0):null;
if(!_18){
return;
}
var re=new RegExp("links((Plus)|(Minus))(1|2)?\\.gif$","ig");
re.exec(_18.src);
_18.src=_18.src.replace(re,"links"+((RegExp.$1=="Plus")?"Minus":"Plus")+RegExp.$4+".gif");
}};;RadEditorNamespace.RadModuleManagerCombo={New:function(_1){
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.ItemsArray=[];
_2.RecreateBeforeShow=true;
_2.SkinBasePath=_1.SkinBasePath;
return _2;
},OnBeforeShowPopup:function(){
this.CreateItems();
},OnCellClick:function(_3){
this.SelectedValue=this.ItemsArray[_3];
},CreateCellContent:function(_4,_5,_6){
if(_5){
var _7=_5.IsEnabled;
var _8=this.Popup.CreateElement("img");
_8.src=this.SkinBasePath+(_7?"Img/moduleEnabled.gif":"Img/moduleDisabled.gif");
_8.style.marginRight="5px";
_8.setAttribute("align","absmiddle");
_4.appendChild(_8);
_4.innerHTML+=_5.Title;
}
},OnPopupClick:function(){
this.Popup.Hide();
this.GetController().Fire(this.Name,this);
if(this.SelectedValue){
var _9=this.SelectedValue.IsEnabled;
var _a=this.ItemsArray;
var _b=false;
for(var i=0;i<_a.length;i++){
if(_a[i].IsEnabled!=_9){
_b=true;
break;
}
}
if(_b){
this.HeaderElementClick();
}
}
}};;RadEditorNamespace.RadUndoRedoCombo={New:function(_1){
var _2=RadEditorNamespace.RadComboBoxBase.New(_1);
RadEditorNamespace.Utils.ExtendObject(_2,this);
_2.TopTable=null;
return _2;
},OnHeaderElementClick:function(){
this.SelectedValue=1;
this.GetController().Fire(this.Name,this);
return false;
},GetPopupBodyElement:function(){
var _3=this.Popup.CreateElement("DIV");
var _4=this.Popup.CreateElement("DIV");
_4.style.height="107px";
_4.style.overflow="auto";
_3.appendChild(_4);
return _3;
},OnBeforeShowPopup:function(){
this.SelectedValue=0;
var _5=this.GetDefaultPopupTable("RadETablePicker",this.CellSpacing,this.CellPadding,"100%","");
this.TopTable=_5;
var _6=this.GetDataFunction(this.Name);
this.CreateItems(_5,_6);
var _7=this.PopupBodyElement.firstChild;
_7.innerHTML="";
_7.appendChild(_5);
if(!this.IsCreated){
this.CreatePopupFooter();
this.IsCreated=true;
}
},OnDispose:function(){
this.TopTable=null;
this.TableInfoLabel=null;
},CreateItems:function(_8,_9){
if(_9.length>0){
var _a=null;
var _b=null;
for(var i=0;i<_9.length;i++){
_a=_8.insertRow(-1);
_b=_a.insertCell(-1);
_b.onmouseover=new Function("this.Parent.OnCellOver(this);");
this.ConfigureCell(_b,this,i);
this.CreateCellContent(_b,_9[i],i);
}
}
},CreateCellContent:function(_d,_e,_f){
_d.innerHTML=_e.Title;
},CreatePopupFooter:function(){
var _10=this.GetDefaultPopupTable("RadETablePicker",1,1,"100%","");
oRow=_10.insertRow(-1);
oCell=oRow.insertCell(-1);
oCell.innerHTML=this.Name;
oCell.className="Wizard";
oCell.onmouseover=new Function("this.className = 'WizardOver'");
oCell.onmouseout=new Function("this.className = 'Wizard'");
oCell.onclick=new Function("return false;");
this.TableInfoLabel=oCell;
this.PopupBodyElement.appendChild(_10);
},OnCellClick:function(_11){
this.SelectedValue=_11+1;
},OnCellOver:function(_12){
this.NumRowsSelected=_12?(_12.parentNode.rowIndex+1):0;
this.UpdateSampleTable();
},UpdateSampleTable:function(){
if(this.TopTable){
var _13=this.TopTable.rows;
for(var i=0;i<_13.length;i++){
cell=_13[i].cells[0];
cell.className=(i<this.NumRowsSelected)?"Over":"";
}
this.UpdateTableInfoLable();
}
},UpdateTableInfoLable:function(){
var _15=this.Name;
if(RadEditorNamespace.Utils.IsNull(this.NumRowsSelected,0)>0){
_15=this.Name+RadEditorNamespace.Utils.Format(" {0}",this.NumRowsSelected)+" actions";
}
this.TableInfoLabel.innerHTML=_15;
}};;;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
