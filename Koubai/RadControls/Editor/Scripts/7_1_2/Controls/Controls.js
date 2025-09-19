function AccessibleTable(id,_2){
this.Id=id;
this.AlignmentSelector=_2;
this.Table=null;
this.HeadingRows=0;
this.HeadingColumns=0;
this.Caption="";
this.Summary="";
this.CaptionHalign="";
this.CaptionValign="";
this.SetCellID=false;
this.TxtRows=document.getElementById("TXT_HEAD_ROWS_"+this.Id);
this.TxtCols=document.getElementById("TXT_HEAD_COLS_"+this.Id);
this.TxtSummary=document.getElementById("TXT_SUMMARY"+this.Id);
this.TxtCaption=document.getElementById("TXT_CAPTION_"+this.Id);
this.ChbSetCellID=document.getElementById("SET_CELL_ID_"+this.Id);
this.MaxHeadRowsSpan=document.getElementById("MAX_HEAD_ROWS_"+this.Id);
this.MaxHeadColsSpan=document.getElementById("MAX_HEAD_COLS_"+this.Id);
this.IsIE=document.all?true:false;
this.TableDocument=document;
}
AccessibleTable.prototype.Initialize=function(_3,_4){
this.Table=_3;
if(_4){
this.TableDocument=_4;
}
if(this.Table){
if(this.Table.caption){
if(this.IsIE){
this.Caption=this.Table.caption.innerText;
this.CaptionHalign=(""==this.Table.caption.align?"left":this.Table.caption.align);
this.CaptionValign=(""==this.Table.caption.vAlign?"top":this.Table.caption.vAlign);
}else{
this.Caption=this.Table.caption.childNodes[0].nodeValue;
this.CaptionHalign="center";
this.CaptionValign=this.Table.caption.align;
}
}else{
this.Caption="";
this.CaptionHalign="left";
this.CaptionValign="top";
}
this.Summary=this.Table.summary;
this.HeadingRows=this.Table.tHead?this.Table.tHead.rows.length:0;
this.HeadingColumns=this.CountHeadingColumns();
this.SetCellID=this.CheckHeadersHasIds()||this.CheckCellsHasHeaders();
this.UpdateData(false);
}
};
AccessibleTable.prototype.CountHeadingColumns=function(){
var _5=0;
if(this.Table){
for(k=0;k<this.Table.tBodies.length;k++){
var _6=this.Table.tBodies[k];
for(i=0;i<_6.rows.length;i++){
var _7=_6.rows[i];
var _8=_7.getElementsByTagName("TH");
if(_8&&_8.length>_5){
_5=_8.length;
}
}
}
}
return _5;
};
AccessibleTable.prototype.UpdateData=function(_9){
if(_9){
this.HeadingRows=parseInt(this.TxtRows.value);
this.HeadingColumns=parseInt(this.TxtCols.value);
this.Caption=this.TxtCaption.value;
this.Summary=this.TxtSummary.value;
this.CaptionHalign=this.AlignmentSelector.GetAlign();
this.CaptionValign=this.AlignmentSelector.GetVAlign();
this.SetCellID=this.ChbSetCellID.checked;
}else{
this.TxtRows.value=this.HeadingRows;
this.TxtCols.value=this.HeadingColumns;
this.TxtCaption.value=this.Caption;
this.TxtSummary.value=this.Summary;
this.AlignmentSelector.SelectAlignment(this.CaptionHalign,this.CaptionValign);
this.ChbSetCellID.checked=this.SetCellID;
if(this.Table){
var _a=this.Table.rows.length;
var _b=(this.Table.rows.length>0)?this.Table.rows[0].cells.length:0;
this.MaxHeadRowsSpan.innerText="(Max. "+_a+")";
this.MaxHeadColsSpan.innerText="(Max. "+_b+")";
}
}
};
AccessibleTable.prototype.CheckCellsHasHeaders=function(){
if(this.Table){
for(k=0;k<this.Table.tBodies.length;k++){
var _c=this.Table.tBodies[k];
for(i=0;i<_c.rows.length;i++){
var _d=_c.rows[i];
for(j=0;j<_d.cells.length;j++){
var _e=_d.cells[j];
if(_e.getAttribute("headers")){
return true;
}
}
}
}
}
return false;
};
AccessibleTable.prototype.CheckHeadersHasIds=function(){
if(this.Table){
for(i=0;i<this.Table.rows.length;i++){
var _f=this.Table.rows[i];
for(j=0;j<_f.cells.length;j++){
var _10=_f.cells[j];
if("TH"==_10.tagName.toUpperCase()&&""!=_10.id){
return true;
}
}
}
}
return false;
};
AccessibleTable.prototype.GetTableColumns=function(){
var _11=0;
if(null!=this.Table&&this.Table.rows.length>0){
_11=this.Table.rows[0].cells.length;
}
return _11;
};
AccessibleTable.prototype.UpdateCaption=function(){
var _12=this.Table.caption;
if(this.IsIE){
if(!_12&&""!=this.Caption){
_12=this.Table.createCaption();
}
if(_12){
_12.innerText=this.Caption;
_12.align=this.CaptionHalign;
_12.vAlign=this.CaptionValign;
}
}else{
if(_12){
this.Table.removeChild(_12);
_12=null;
}
if(!_12&&""!=this.Caption){
var _12=this.TableDocument.createElement("CAPTION");
_12.innerHTML=this.Caption;
_12.setAttribute("align",this.CaptionValign);
this.Table.appendChild(_12);
}
}
};
AccessibleTable.prototype.UpdateThead=function(){
var _13=this.GetTableColumns();
var _14=this.GetThead();
if(!_14){
_14=this.Table.createTHead();
}
var _15=0;
for(i=0;i<this.Table.rows.length;i++){
var row=this.Table.rows[i];
var _17=row.parentNode;
var _18=_17.tagName.toUpperCase();
if(i<this.HeadingRows){
if("THEAD"!=_18){
var _19=_14.insertRow(_14.rows.length);
for(j=0;j<row.cells.length;j++){
var _1a=this.TableDocument.createElement("TH");
_1a.innerHTML=row.cells[j].innerHTML;
_19.insertBefore(_1a,null);
}
this.Table.deleteRow(i+1);
}
}else{
if("THEAD"==_18){
var _19=this.Table.tBodies[0].insertRow(_15++);
for(j=0;j<row.cells.length;j++){
var _1a=_19.insertBefore(this.TableDocument.createElement("TD"),null);
_1a.innerHTML=row.cells[j].innerHTML;
}
}
}
}
if(this.HeadingRows<_14.rows.length){
for(i=_14.rows.length-1;i>=this.HeadingRows;i--){
_14.deleteRow(i);
}
}
};
AccessibleTable.prototype.UpdateRows=function(){
if(this.Table){
for(k=0;k<this.Table.tBodies.length;k++){
var _1b=this.Table.tBodies[k];
for(i=0;i<_1b.rows.length;i++){
var row=_1b.rows[i];
for(j=0;j<row.cells.length;j++){
var _1d=j<this.HeadingColumns?"TH":"TD";
this.ChangeCellType(row.cells[j],_1d);
}
}
}
}
};
AccessibleTable.prototype.UpdateCellIDs=function(){
var _1e=this.CheckHeadersHasIds();
var _1f=this.CheckCellsHasHeaders();
if(this.Table&&this.Table.tHead&&(this.SetCellID||_1e||_1f)){
var _20=this.Table.id?this.Table.id:this.Table.uniqueID;
if(!_20){
_20="table";
}
var _21=null;
var _22=0;
if(_1e||this.SetCellID){
for(i=0;i<this.Table.tHead.rows.length;i++){
var row=this.Table.tHead.rows[i];
if(!_21){
_21=new Array(row.cells.length);
}
for(j=0;j<row.cells.length;j++){
var _24=row.cells[j];
if(this.SetCellID){
var id=_24.id?_24.id:(_20+"_heading_"+_22++);
_24.id=id;
if(!_21[j]){
_21[j]=id;
}
}else{
_24.id="";
}
}
}
}
for(k=0;k<this.Table.tBodies.length;k++){
var _26=this.Table.tBodies[k];
for(i=0;i<_26.rows.length;i++){
var row=_26.rows[i];
for(j=0;j<row.cells.length;j++){
var _24=row.cells[j];
var _27=_24.tagName.toUpperCase();
if("TH"==_27){
if(this.SetCellID){
_24.id=_24.id?_24.id:(_20+"_heading_"+_22++);
}else{
_24.id="";
}
}else{
if("TD"==_27&&this.Table.tHead.rows.length>0&&_21&&(_1f||this.SetCellID)){
if(this.SetCellID){
_24.setAttribute("headers",_21[j]);
}else{
_24.removeAttribute("headers",false);
}
}
}
}
}
}
}
};
AccessibleTable.prototype.UpdateTable=function(){
if(null!=this.Table){
this.UpdateData(true);
this.UpdateCaption();
if(this.Summary==""){
this.Table.removeAttribute("summary",false);
}else{
this.Table.summary=this.Summary;
}
this.UpdateThead();
this.UpdateRows();
this.UpdateCellIDs();
}
};
AccessibleTable.prototype.GetHeadingRows=function(){
return this.HeadingRows;
};
AccessibleTable.prototype.SetHeadingRows=function(_28){
this.HeadingRows=_28;
};
AccessibleTable.prototype.GetHeadingColumns=function(){
return this.HeadingColumns;
};
AccessibleTable.prototype.SetHeadingColumns=function(_29){
this.HeadingColumns=_29;
};
AccessibleTable.prototype.GetSummary=function(){
return this.Summary;
};
AccessibleTable.prototype.SetSummary=function(_2a){
this.Summary=_2a;
};
AccessibleTable.prototype.GetCaption=function(){
return this.Caption;
};
AccessibleTable.prototype.SetCaption=function(_2b){
this.Caption=_2b;
};
AccessibleTable.prototype.GetCaptionHalign=function(){
return this.CaptionHalign;
};
AccessibleTable.prototype.SetCaptionHalign=function(_2c){
this.CaptionHalign=_2c;
};
AccessibleTable.prototype.GetCaptionValign=function(){
return this.CaptionValign;
};
AccessibleTable.prototype.SetCaptionValign=function(_2d){
this.CaptionValign=_2d;
};
AccessibleTable.prototype.GetThead=function(){
var _2e=this.Table.getElementsByTagName("THEAD");
if(_2e.length>0){
return _2e[0];
}else{
return null;
}
};
AccessibleTable.prototype.ChangeCellType=function(_2f,_30){
var _31=false;
if(_2f.tagName.toUpperCase()!=_30.toUpperCase()){
var _32=this.TableDocument.createElement(_30.toUpperCase());
for(var _33 in _2f.attributes){
_2f.attrName=_2f.attributes[_33];
}
_32.colSpan=_2f.colSpan;
_32.rowSpan=_2f.rowSpan;
_32.innerHTML=_2f.innerHTML;
_2f.innerHTML=_2f.innerHTML;
_32.style.cssText=_2f.style.cssText;
_2f.parentNode.replaceChild(_32,_2f);
_31=true;
}
return _31;
};;var ImgAlignment=[["",""],["none",""],["",""],["",""],["top",""],["",""],["left",""],["absmiddle",""],["right",""],["",""],["bottom",""],["",""],["",""],["",""],["",""]];
var CellAlignment=[["",""],["none",""],["",""],["left","top"],["center","top"],["right","top"],["left","middle"],["center","middle"],["right","middle"],["left","bottom"],["center","bottom"],["right","bottom"],["",""],["",""],["",""]];
var TableAlignment=[["",""],["none",""],["",""],["left",""],["center",""],["right",""],["",""],["",""],["",""],["",""],["",""],["",""],["",""],["",""],["",""]];
var CaptionIEAlignment=[["",""],["none",""],["",""],["left","top"],["center","top"],["right","top"],["",""],["",""],["",""],["left","bottom"],["center","bottom"],["right","bottom"],["",""],["",""],["",""]];
var CaptionNSAlignment=[["",""],["none",""],["",""],["",""],["","top"],["",""],["",""],["",""],["",""],["",""],["","bottom"],["",""],["",""],["",""],["",""]];
var AlignmentImages=["x.gif","x.gif","x.gif","AlignTopLeft.gif","AlignTopCenter.gif","AlignTopRight.gif","AlignMiddleLeft.gif","AlignMiddleCenter.gif","AlignMiddleRight.gif","AlignBottomLeft.gif","AlignBottomCenter.gif","AlignBottomRight.gif","x.gif","x.gif","x.gif"];
function AlignmentSelector(id,_2,_3,_4,_5){
this.IsIE=document.all?true:false;
this.ID=id;
this.SkinPath=_2;
this.Mode="";
this.ActiveLookup=null;
this.Selection=null;
this.NoAlignmentIndex=-1;
this.ButtonImg=document.getElementById("MENU_BUTTON_IMG_"+this.ID);
this.DropDownMenu=_4;
this.DropDownMenu.MenuEventHandler=this;
this.SetMode(_3);
this.ClientClickString=_5;
}
AlignmentSelector.prototype.SetMode=function(_6){
this.Mode=_6;
this.ActiveLookup=this.GetLookupTableByMode(this.Mode);
this.SelectAlignmentByIndex(this.NoAlignmentIndex);
};
AlignmentSelector.prototype.GetLookupTableByMode=function(_7){
switch(_7.toUpperCase()){
case "IMG":
return ImgAlignment;
case "TABLE":
return TableAlignment;
case "TD":
return CellAlignment;
case "CAPTION":
return (this.IsIE?CaptionIEAlignment:CaptionNSAlignment);
default:
return null;
}
};
AlignmentSelector.prototype.OnShowMenu=function(_8){
this.ShowMode(this.Mode,_8);
};
AlignmentSelector.prototype.ShowMode=function(_9,_a){
var _b=document.getElementById("MENU_ELEMENT_TABLE_"+this.ID);
var _c=0;
for(var i=0;i<_b.rows.length;i++){
var _e=false;
for(var j=0;j<_b.rows[i].cells.length;j++){
var _10=_b.rows[i].cells[j];
var _11=_a&&this.IsAvailable(_c++);
_10.style.visibility=_11?"visible":"hidden";
_e|=_11;
}
if(null!=document.all){
_b.rows[i].style.display=_e?"":"none";
}
}
};
AlignmentSelector.prototype.IsAvailable=function(_12){
var _13=false;
if(this.ActiveLookup){
var _14=this.ActiveLookup[_12];
_13=((null!=_14)&&(""!=_14[0]||""!=_14[1]));
}
return _13;
};
AlignmentSelector.prototype.GetFirstAlignmentIndex=function(){
var _15=-1;
if(this.ActiveLookup){
for(i=0;i<this.ActiveLookup.length;i++){
if(this.IsAvailable(i)){
_15=i;
break;
}
}
}
return _15;
};
AlignmentSelector.prototype.SelectAlignmentByIndex=function(_16){
if(_16==this.NoAlignmentIndex){
this.Selection="";
this.SetButtonImage("x.gif");
}else{
if(this.ActiveLookup&&0<=_16&&_16<this.ActiveLookup.length){
this.Selection=this.ActiveLookup[_16];
this.SetButtonImage(AlignmentImages[_16]);
if(""!=this.ClientClickString){
eval(this.ClientClickString);
}
}
}
};
AlignmentSelector.prototype.SetButtonImage=function(_17){
this.ButtonImg.src=this.SkinPath+"Img/"+_17;
if("x.gif"==_17){
this.ButtonImg.style.marginLeft="0px";
}else{
this.ButtonImg.style.marginLeft="-2px";
}
};
AlignmentSelector.prototype.SelectAlignment=function(_18,_19){
if(""==_18){
_18="none";
}
if(!_19){
_19="";
}
if(this.ActiveLookup){
_18=_18.toUpperCase();
_19=_19.toUpperCase();
var _1a=-1;
for(i=0;i<this.ActiveLookup.length;i++){
if(this.IsAvailable(i)){
var ha=this.ActiveLookup[i][0].toUpperCase();
var va=this.ActiveLookup[i][1].toUpperCase();
if(-1==_1a){
_1a=i;
}
if((_18==ha||_18==va)&&(_19==ha||_19==va)){
this.SelectAlignmentByIndex(i);
return;
}
}
}
this.SelectAlignmentByIndex(_1a);
}
};
AlignmentSelector.prototype.GetAlign=function(){
var _1d=(this.Selection?this.Selection[0]:"");
if("none"==_1d){
_1d="";
}
return _1d;
};
AlignmentSelector.prototype.GetVAlign=function(){
var _1e=(this.Selection?this.Selection[1]:"");
if("none"==_1e){
_1e="";
}
return _1e;
};
AlignmentSelector.prototype.Toggle=function(){
if(null!=this.DropDownMenu){
this.DropDownMenu.Toggle();
if(window.event){
window.event.cancelBubble=true;
}
}
};
AlignmentSelector.prototype.Enable=function(_1f){
if(null!=this.DropDownMenu){
this.DropDownMenu.Enable(_1f);
}
};;function BrowserDialogBase(_1,_2,_3){
this.FolderPathBox=document.getElementById("FolderPathBox");
this.FileBrowser=_1;
this.Previewer=_2;
this.TabHolder=_3;
if(this.FileBrowser&&this.Previewer){
var _4=this.Previewer;
var _5=this.TabHolder;
var _6=this;
this.FileBrowser.OnClientClick=function(_7){
_6.ShowPath(_7.GetPath());
_4.ChangePreviewedObject(_7);
};
this.FileBrowser.OnFolderChange=function(_8){
_6.ShowPath(_8.GetPath());
_4.Clear();
_5.SetTabEnabled(1,_8.Permissions&this.UploadPermission);
};
if(this.FileBrowser.SelectedItem&&this.FileBrowser.SelectedItem.Type=="F"){
_4.ChangePreviewedObject(this.FileBrowser.SelectedItem);
}
}
}
BrowserDialogBase.prototype.ShowPath=function(_9){
this.FolderPathBox.value=_9;
};
BrowserDialogBase.prototype.Initialize=function(){
this.TabHolder.SetTabEnabled(1,this.FileBrowser.CurrentItem.Permissions&this.FileBrowser.UploadPermission);
this.TabHolder.SelectCurrentTab();
var _a=this.FileBrowser.SelectedItem!=null?this.FileBrowser.SelectedItem:this.FileBrowser.CurrentItem;
this.ShowPath(_a.GetPath());
};;function CellPropertiesControl(Id,_2,_3,_4,_5,_6){
this.Id=Id;
this.CssClassSelector=_2;
this.CellAlignmentSelector=_3;
this.BackgroundColorPicker=_4;
this.BgImageDialogCaller=_5;
this.StyleBuilderCaller=_6;
this.ColumnWidthHolder=document.getElementById(this.Id+"_columnWidth");
this.ColumnHeightHolder=document.getElementById(this.Id+"_columnHeight");
this.ColumnWidthBox=new PropertyTextBox(this.ColumnWidthHolder.id,"DIMENSION",localization["InvalidCellWidth"]);
this.ColumnHeightBox=new PropertyTextBox(this.ColumnHeightHolder.id,"DIMENSION",localization["InvalidCellHeight"]);
this.ColumnWrapHolder=document.getElementById(this.Id+"_columnWrap");
this.IdHolder=document.getElementById(this.Id+"_idHolder");
this.EditorObject=null;
this.Initialized=false;
}
CellPropertiesControl.prototype.Initialize=function(_7,_8,_9,_a,_b){
if(!this.Initialized){
this.EditorObject=_9;
this.CssClasses=_8;
this.CssClassSelector.Initialize(this.CssClasses);
this.StyleBuilderCaller.Initialize(this.EditorObject);
this.BgImageDialogCaller.Initialize(this.EditorObject);
this.BackgroundColorPicker.CanAddCustomColor=_b;
this.BackgroundColorPicker.CanAddHexColor=_b;
if(_a){
this.BackgroundColorPicker.SetColors(_a);
}
this.Initialized=true;
}
this.LoadPropertyValues(_7);
};
CellPropertiesControl.prototype.Clear=function(){
var _c=document.createElement("TD");
this.LoadPropertyValues(_c);
};
CellPropertiesControl.prototype.LoadPropertyValues=function(_d){
this.CellToModify=_d;
if(this.CellToModify.style.width==""){
this.ColumnWidthHolder.value=this.CellToModify.width;
}else{
this.ColumnWidthHolder.value=this.CellToModify.style.width;
}
if(this.CellToModify.style.height==""){
this.ColumnHeightHolder.value=this.CellToModify.height;
}else{
this.ColumnHeightHolder.value=this.CellToModify.style.height;
}
var _e=this.CellToModify.getAttribute("id");
if(_e){
this.IdHolder.value=_e;
}
this.CellAlignmentSelector.SelectAlignment(this.CellToModify.align,this.CellToModify.vAlign);
this.ColumnWrapHolder.checked=this.CellToModify.noWrap;
this.BackgroundColorPicker.SelectColor(this.CellToModify.bgColor);
this.CssClassSelector.SelectCssClass(this.CellToModify.className);
this.StyleBuilderCaller.SetStyledObject(this.CellToModify);
if(this.BgImageDialogCaller&&this.CellToModify){
var _f=this.CellToModify.getAttribute("background");
if(!_f){
_f="";
}
this.BgImageDialogCaller.SetImagePath(_f);
}
};
CellPropertiesControl.prototype.UpdateMultiple=function(_10){
for(var i=0;i<_10.length;i++){
if(!this.Update(_10[i])){
return false;
}
}
return true;
};
CellPropertiesControl.prototype.Update=function(_12){
if(typeof (_12)!="undefined"){
this.CellToModify=_12;
}
var _13=this.CellToModify;
_13.style.cssText=this.StyleBuilderCaller.GetStyleText();
if(_13.style.cssText==""){
_13.removeAttribute("style",false);
}
if(!this.ColumnWidthBox.IsValueValid()){
return false;
}
var _14=this.ColumnWidthBox.GetValue();
_13.removeAttribute("width",false);
_13.style.width=_14?ConvertIntToPixel(_14):"";
if(!this.ColumnHeightBox.IsValueValid()){
return false;
}
var _15=this.ColumnHeightBox.GetValue();
_13.removeAttribute("height",false);
_13.style.height=_15?ConvertIntToPixel(_15):"";
this.SetAttribValue("id",this.IdHolder.value);
this.SetAttribValue("align",this.CellAlignmentSelector.GetAlign());
this.SetAttribValue("vAlign",this.CellAlignmentSelector.GetVAlign());
this.SetAttribValue("bgColor",this.BackgroundColorPicker.SelectedColor);
this.SetAttribValue("background",this.BgImageDialogCaller.GetImagePath());
var _16=document.all?"className":"class";
this.SetAttribValue(_16,this.CssClassSelector.GetSelectedClassName());
_13.noWrap=this.ColumnWrapHolder.checked;
return true;
};
CellPropertiesControl.prototype.SetAttribValue=function(_17,_18,_19){
if(_18||(true==_19)){
this.CellToModify.setAttribute(_17,_18);
}else{
this.CellToModify.removeAttribute(_17,false);
}
};;function ColorPicker(id,_2,_3,_4){
this.IsIE=document.all?true:false;
this.ID=id;
this.SkinPath=_2+"Dialogs/";
this.DropDownMenu=_3;
this.ColorSampleElement=document.getElementById("MENU_BUTTON_SPAN_"+this.ID);
this.DefaultColors=_4;
this.ItemsPerRow=6;
this.CanAddCustomColor=true;
this.CanAddHexColor=true;
if(null!=this.DropDownMenu){
this.BuildColorTable();
}
this.SelectedColor="";
this.SelectColor("");
this.OnClientClick=null;
}
ColorPicker.prototype.SetColors=function(_5){
this.DefaultColors=_5;
this.BuildColorTable();
};
ColorPicker.prototype.GetXImageUrl=function(){
return "url("+this.SkinPath+"x.gif)";
};
ColorPicker.prototype.Enable=function(_6){
if(null!=this.DropDownMenu){
this.DropDownMenu.Enable(_6);
}
};
ColorPicker.prototype.Toggle=function(){
if(null!=this.DropDownMenu){
this.DropDownMenu.Toggle();
}
};
ColorPicker.prototype.SelectColor=function(_7,_8){
this.SelectedColor=_7;
if(null==_8){
_8=true;
}
if(_8&&null!=this.DropDownMenu&&this.DropDownMenu.IsVisible()){
this.DropDownMenu.SetVisible(false);
}
this.OnChangeSelection();
};
ColorPicker.prototype.OnChangeSelection=function(){
if(null!=this.DropDownMenu&&null!=this.ColorSampleElement){
if(""==this.SelectedColor){
this.ColorSampleElement.style.background=this.SelectedColor;
this.ColorSampleElement.style.backgroundImage=this.GetXImageUrl();
this.ColorSampleElement.style.backgroundPosition="center";
this.ColorSampleElement.style.backgroundRepeat="no-repeat";
}else{
this.ColorSampleElement.style.backgroundImage="";
this.ColorSampleElement.style.background=this.SelectedColor;
}
if(this.OnClientClick){
this.OnClientClick();
}
}
};
ColorPicker.prototype.BuildColorTable=function(){
var _9=document.getElementById("COLOR_TABLE_"+this.ID);
if(!_9){
return;
}
var _a=_9.rows.length;
for(var i=0;i<_a;i++){
_9.deleteRow(0);
}
var _c=null;
var _d=0;
for(i=0;i<this.DefaultColors.length;i++){
if(0==i%this.ItemsPerRow){
_c=_9.insertRow(-1);
_d=0;
}
_d++;
var _e=this.DefaultColors[i];
this.AddColorCell(_c,_e);
}
var _f=this.ItemsPerRow-_d-1;
if(_f>0){
for(var i=0;i<=_f;i++){
var _10=_c.insertCell(_c.cells.length);
_10.innerHTML="&nbsp;&nbsp;";
}
}
if(this.CanAddCustomColor){
if(this.IsIE){
this.AddCustomColorIE(_9);
}else{
this.AddHexColor(_9,localization["AddCustomColor"]);
}
}
if(this.IsIE&&this.CanAddHexColor){
this.AddHexColor(_9,localization["AddHexColor"]);
}
if(this.IsIE){
this.DropDownMenu.SetMenuInnerHtml(_9.outerHTML);
}
};
ColorPicker.prototype.AddCustomColorIE=function(_11){
var _12=this.ID+".OnAddCustomColorIE()";
var _13="<span class='Label'>"+localization["AddCustomColor"]+"</span>"+"<object id='DLG_SELECT_COLOR_"+this.ID+"' CLASSID='clsid:3050f819-98b5-11cf-bb82-00aa00bdce0b' width='0px' height='0px'></object>";
this.AddCustomColor(_11,_12,_13);
};
ColorPicker.prototype.AddHexColor=function(_14,_15){
var _16=this.ID+".OnAddHexColor()";
var _17="<span class='Label'>"+_15+"</span>";
this.AddCustomColor(_14,_16,_17);
};
ColorPicker.prototype.AddCustomColor=function(_18,_19,_1a){
var row=_18.insertRow(-1);
var _1c=row.insertCell(row.cells.length);
_1c.colSpan=this.ItemsPerRow;
_1c.noWrap=true;
_1c.setAttribute("onmouseover","this.className = 'Over'");
_1c.setAttribute("onmouseout","this.className = ''");
if(""!=_19){
_1c.setAttribute("onclick",_19);
}
_1c.innerHTML=_1a;
};
ColorPicker.prototype.OnAddCustomColorIE=function(){
var dlg=document.all["DLG_SELECT_COLOR_"+this.ID];
if(!dlg){
return;
}
var _1e=dlg.ChooseColorDlg();
_1e=this.ConvertColor(_1e);
this.OnAddCustomColor(_1e);
};
ColorPicker.prototype.ValidateColor=function(_1f){
if(null==_1f){
return "";
}
if(_1f.charAt(0)!="#"){
_1f="#"+_1f;
}
re=new RegExp("#[0-9a-fA-F]{6}","gi");
return re.exec(_1f)?_1f:"";
};
ColorPicker.prototype.toHex=function(_20){
var _21="0123456789ABCDEF";
_20=parseInt(_20);
if(_20<0){
_20=0;
}
if(_20>255){
_20=255;
}
var i=Math.floor(_20/16);
var j=_20%16;
return _21.charAt(i)+_21.charAt(j);
};
ColorPicker.prototype.OnAddCustomColor=function(_24){
_24=this.ValidateColor(_24);
this.SelectColor(_24,true);
if(""!=_24){
this.AddCustomColorToColorTable(_24);
}
};
ColorPicker.prototype.AddCustomColorToColorTable=function(_25){
var _26=document.getElementById("COLOR_TABLE_"+this.ID);
if(!_26){
return;
}
var _27=null;
var _28=0;
if(this.CanAddCustomColor){
_28++;
}
if(this.CanAddHexColor){
_28++;
}
if(_26.rows.length>((this.DefaultColors.length/this.ItemsPerRow)+_28)){
var _29=_26.rows[_26.rows.length-1];
if(_29.cells.length<this.ItemsPerRow){
_27=_29;
}
}
if(!_27){
_27=_26.insertRow(-1);
}
this.AddColorCell(_27,_25);
if(this.IsIE){
this.DropDownMenu.SetMenuInnerHtml(_26.outerHTML);
}
};
ColorPicker.prototype.AddColorCell=function(row,_2b){
var _2c=row.insertCell(row.cells.length);
_2c.setAttribute("onmouseover","this.className = 'Over'");
_2c.setAttribute("onmouseout","this.className = ''");
_2c.setAttribute("onclick",this.ID+".SelectColor('"+_2b+"')");
var _2d=document.createElement("DIV");
_2d.style.backgroundColor=_2b;
if(""==_2b){
_2c.style.backgroundImage=this.GetXImageUrl();
_2c.style.backgroundRepeat="no-repeat";
_2c.style.backgroundPosition="center";
}
_2c.appendChild(_2d);
};
ColorPicker.prototype.OnAddHexColor=function(){
var _2e=prompt(localization["PromptColor"],"#");
this.OnAddCustomColor(_2e);
};
ColorPicker.prototype.ConvertColor=function(_2f){
_2f=parseInt(_2f);
_2f=_2f.toString(16);
if(_2f.length<6){
var _30="000000".substring(0,(6-_2f.length));
_2f="#"+_30.concat(_2f).toUpperCase();
}else{
_2f="#"+_2f.toUpperCase();
}
return _2f;
};
ColorPicker.prototype.Toggle=function(){
if(null!=this.DropDownMenu){
this.DropDownMenu.Toggle();
if(window.event){
window.event.cancelBubble=true;
}
}
};;function ConvertIntToPixel(_1){
var _2=""+_1;
if(_2.indexOf("%")!=-1){
return _2;
}else{
_2=parseInt(_2);
if(!isNaN(_2)){
_2=_2+"px";
return _2;
}
}
return _1;
}
function GetCellIndex(_3){
var _4=_3?(_3.cellIndex):0;
var _5=navigator.userAgent.toLowerCase();
if(_5.indexOf("safari")>-1){
var oP=_3.parentNode;
for(var i=0;i<oP.cells.length;i++){
if(_3==oP.cells[i]){
_4=i;
break;
}
}
}
return _4;
}
function RadUtil_GetEventSource(e){
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
}
function buttonAction(_9,e){
if(window.event==null){
e.preventDefault();
if(e.stopPropagation){
e.stopPropagation();
}
}
eval(_9);
}
function showObject(id){
try{
document.getElementById(id).style.display="block";
}
catch(exc){
}
}
function hideObject(id){
try{
document.getElementById(id).style.display="none";
}
catch(exc){
}
}
function inArray(_d,_e){
for(var i=0;i<_d.length;i++){
if(_d[i]==_e){
return true;
}
}
return false;
}
function addOption(obj,_11,_12){
var _13=false;
_11=trim(_11);
_12=trim(_12);
var _14=obj.getElementsByTagName("OPTION");
for(var i=0;i<_14.length;i++){
if(_11.toUpperCase()==_14[i].value.toUpperCase()){
_13=true;
break;
}
}
if((!_13)&&(_12!="")){
var _16=document.createElement("OPTION");
_16.innerHTML=_12;
_16.value=_11;
obj.appendChild(_16);
}
}
function selectOption(_17,_18){
var _19=false;
if((_18!="")&&(_18!=null)){
var _1a=_17.getElementsByTagName("OPTION");
for(var i=0;i<_1a.length;i++){
if(_1a[i].value.toString().toUpperCase()==_18.toString().toUpperCase()){
try{
_1a[i].selected=true;
}
catch(exc){
}
_19=true;
break;
}
}
}else{
_17.selectedIndex=0;
}
return _19;
}
function changeColor(_1c,_1d,_1e){
if(_1c.value==_1d){
if(document.all){
var _1f=document.getElementById("dlgHelper").ChooseColorDlg();
if(_1f!=0){
_1f=_1f.toString(16);
if(_1f.length<6){
var _20="000000".substring(0,(6-_1f.length));
_1f="#"+_20.concat(_1f).toUpperCase();
}else{
_1f="#"+_1f.toUpperCase();
}
}else{
_1f=null;
}
}else{
_1f=prompt(localization["PromptColor"],"#");
}
var _21;
var _22=_1c.getElementsByTagName("OPTION");
if(_1f!=null){
if(_22.length==_1e){
_21=document.createElement("OPTION");
_1c.insertBefore(_21,_22[_1e-1]);
}else{
_21=_22[_1e-1];
}
_21.style.backgroundColor=_1f;
_21.innerHTML=localization["Customh"]+_1f;
_21.value=_1f;
}
_1c.selectedIndex=_22.length-2;
}
}
function trim(str){
return str.replace(/^\s{1,}/ig,"").replace(/\s{1,}$/ig,"");
}
function GetDialogArguments(){
if(window.radWindow){
return window.radWindow.Argument;
}else{
return null;
}
}
function AttachEvent(_24,_25,_26){
if(_24.attachEvent){
_24.attachEvent("on"+_25,_26);
}else{
if(_24.addEventListener){
_24.addEventListener(_25,_26,false);
}
}
}
var radEditorGlobalStyleElements=null;
function radEditorGetStyleElements(){
if(!radEditorGlobalStyleElements){
radEditorGlobalStyleElements=new Array("background-attachment","background-color","background-image","background-position-x","background-position-y","background-repeat","behavior","border-bottom-color","border-bottom-style","border-bottom-width","border-left-color","border-left-style","border-left-width","border-right-color","border-right-style","border-right-width","border-top-color","border-top-style","border-top-width","border-color","border-style","border-width","clear","clip","color","cursor","direction","display","filter","font-family","font-size","font-style","font-variant","font-weight","layout-flow","layout-grid-char","layout-grid-line","layout-grid-mode","layout-grid-type","letter-spacing","line-break","line-height","margin-bottom","margin-left","margin-right","margin-top","min-height","padding-bottom","padding-left","padding-right","padding-top","page-break-before","position","text-align","text-autospace","text-decoration","text-indent","text-justify","text-transform","text-underline-position","unicode-bidi","vertical-align","visibility","word-break","word-spacing","word-wrap","writing-mode","z-index","zoom");
}
return radEditorGlobalStyleElements;
};function CssClassSelector(id,_2,_3,_4,_5,_6){
this.IsIE=(null!=document.all&&!window.opera);
this.ID=id;
this.PopupWidth=parseInt(_4);
this.PopupHeight=parseInt(_5);
this.SkinPath=_6;
this.SelectedIndex=-1;
this.PopupWnd=null;
this.Document=null;
this.CssClassesHtmlTable=null;
this.ArrCssClasses=[];
this.Table=document.getElementById("CssClassSelector_Table_"+this.ID);
this.Label=document.getElementById("CssClassSelector_Label_"+this.ID);
this.RadCssClassArray=null;
this.ArrFilterTags=[];
this.Initialize(_2,_3);
this.SelectCssClassByIndex(-1);
if(window.attachEvent){
var _7=this;
window.attachEvent("onunload",function(){
_7.PopupWnd=null;
_7.Document=null;
_7.CssClassesHtmlTable=null;
_7.ArrCssClasses=null;
});
}
}
CssClassSelector.prototype.Initialize=function(_8,_9){
this.ArrCssClasses=_8;
if(!this.PopupWnd){
if(this.IsIE){
this.PopupWnd=window.createPopup();
if(this.PopupWnd){
this.Document=this.PopupWnd.document;
this.Document.body.style.border="0px";
this.Document.body.style.backgroundColor="#FFFFFF";
}
}else{
this.PopupWnd=new PopupWindow();
this.Document=document;
}
}
this.InitPopupWnd();
};
CssClassSelector.prototype.InitPopupWnd=function(){
if(this.IsIE){
if(this.Document){
this.InitPopup(this.Document.body);
}
}else{
this.InitPopup(this.PopupWnd.Popup);
}
};
CssClassSelector.prototype.InitPopup=function(_a){
if(this.Document&&_a){
_a.style.border="1px solid #999999";
this.BuildPopupHtml();
if(this.CssClassesHtmlTable){
while(_a.childNodes&&0<_a.childNodes.length){
_a.removeChild(_a.childNodes[0]);
}
var _b=this.Document.createElement("DIV");
_b.className="RadEDropDownTable";
_b.style.overflow="auto";
_b.style.height=this.PopupHeight;
_b.style.width=this.PopupWidth;
_b.appendChild(this.CssClassesHtmlTable);
_a.appendChild(_b);
}
}
};
CssClassSelector.prototype.BuildPopupHtml=function(){
this.CssClassesHtmlTable=this.CreateCssClassesTable();
try{
var _c=this.CssClassesHtmlTable;
this.AddCssClass(_c,"",null,localization["ClearStyle"],-1);
for(var _d=0;_d<this.ArrCssClasses.length;_d++){
this.AddRadCssClass(_c,this.ArrCssClasses[_d],_d);
}
}
catch(ex){
}
};
CssClassSelector.prototype.AddRadCssClass=function(_e,_f,_10){
this.AddCssClass(_e,_f.Tag,_f.Rule,_f.Alias,_10);
};
CssClassSelector.prototype.AddCssClass=function(_11,tag,_13,_14,_15){
if(tag){
tag=tag.toUpperCase();
}
var row=_11.insertRow(-1);
var _17=row.insertCell(-1);
_17.noWrap=true;
_17.onmouseover=new Function("this.style.border = '1px solid #cccccc';");
_17.onmouseout=new Function("this.style.border= '1px solid #AAAAAA';");
_17.style.font="normal 11px Tahoma";
_17.style.border="1px solid #AAAAAA";
_17.style.padding="2px 2px 1px 2px";
var _18=this.Document.createElement("SPAN");
var _19="";
if(_13){
_19=_13.selectorText;
}else{
_19=_14;
}
_17.setAttribute("title",_19);
var img=this.Document.createElement("IMG");
img.src=this.GetCssClassImageSrcByTag(tag);
img.align="middle";
img.style.cssText="margin-left:2px; margin-right:4px";
_17.appendChild(img);
switch(tag){
case "A":
var _1b=this.Document.createElement("A");
_1b.href="#";
_1b.onmouseover=new Function("window.status = ''; return false");
_1b.innerHTML=_14;
this.ApplyRule(_1b,_13);
_1b.style.cursor="default";
_18.appendChild(_1b);
break;
default:
_18.innerHTML=_14;
_18.style.font="icon";
this.ApplyRule(_18,_13);
_18.style.marginTop="2px";
break;
}
_17.appendChild(_18);
var _1c="";
if(this.IsIE){
_17.Parent=this;
_17.onclick=new Function("this.Parent.SelectCssClassByIndex("+_15+");");
}else{
_1c=this.ID+".SelectCssClassByIndex("+_15+");";
_17.setAttribute("onclick",_1c);
}
_18.style.overflowX="hidden";
};
CssClassSelector.prototype.GetCssClassImageSrcByTag=function(tag){
var _1e="";
switch(tag){
case "ALL":
case "A":
case "IMG":
case "TABLE":
case "P":
_1e=tag;
break;
default:
_1e="Custom";
break;
}
return this.SkinPath+"Img/class"+_1e+".gif";
};
CssClassSelector.prototype.ApplyRule=function(_1f,_20){
if(!_1f){
return;
}
if(!_20){
return;
}
_1f.style.cssText=_20.style.cssText;
var _21=_1f.style.backgroundColor.toLowerCase();
var _22=_1f.style.color.toLowerCase();
if((""==_21||"#ffffff"==_21||"white"==_21)&&("#ffffff"==_22||"white"==_22)){
_1f.style.backgroundColor="#aaaaaa";
}
_1f.style.width="";
_1f.style.height="";
};
CssClassSelector.prototype.CreateCssClassesTable=function(){
var _23=this.Document.createElement("TABLE");
_23.setAttribute("cellSpacing",3);
_23.setAttribute("cellPadding",3);
_23.style.width="100%";
_23.style.cursor="default";
return _23;
};
CssClassSelector.prototype.SelectCssClassByIndex=function(_24){
this.SelectedIndex=_24;
var _25=this.GetRadCssClass(this.SelectedIndex);
if(this.Label){
this.Label.innerHTML=_25?_25.Alias:localization["ClearStyle"];
}
if(this.PopupWnd){
this.PopupWnd.hide();
}
};
CssClassSelector.prototype.SelectCssClassByText=function(_26){
if(!_26){
_26="";
}
for(var i=0;this.ArrCssClasses&&i<this.ArrCssClasses.length;i++){
var _28=this.ArrCssClasses[i];
if(_28&&_28.Rule&&_28.Rule.selectorText.toLowerCase()==_26.toLowerCase()){
break;
}
}
this.SelectCssClassByIndex(i);
};
CssClassSelector.prototype.SelectCssClass=function(_29){
if(!_29){
_29="";
}
for(var i=0;this.ArrCssClasses&&i<this.ArrCssClasses.length;i++){
var _2b=this.ArrCssClasses[i];
if(_2b&&_2b.ClassName.toLowerCase()==_29.toLowerCase()){
break;
}
}
if(""!=_29&&this.ArrCssClasses.length==i){
var _2c=this.ArrCssClasses.length;
var _2b={Tag:"ALL",Rule:null,Alias:_29,ClassName:_29};
this.ArrCssClasses[_2c]=_2b;
i=_2c;
}else{
if(""==_29){
i=-1;
}
}
this.SelectCssClassByIndex(i);
};
CssClassSelector.prototype.GetRadCssClass=function(_2d){
var _2e=null;
if(this.ArrCssClasses&&0<=_2d&&_2d<this.ArrCssClasses.length){
_2e=this.ArrCssClasses[_2d];
}
return _2e;
};
CssClassSelector.prototype.GetSelectedClassName=function(){
var _2f=this.GetRadCssClass(this.SelectedIndex);
return (_2f?_2f.ClassName:"");
};
CssClassSelector.prototype.ShowPopup=function(_30){
if(null==_30){
_30=true;
}
if(this.IsIE){
if(_30){
if(this.PopupWnd){
this.PopupWnd.show(0,this.Table.offsetHeight,this.PopupWidth,this.PopupHeight,this.Table);
}
}else{
if(this.PopupWnd){
this.PopupWnd.hide();
}
}
}else{
if(_30){
if(this.PopupWnd){
this.PopupWnd.show(0,this.Table.offsetHeight,this.PopupWidth,this.PopupHeight,this.Table);
}
}else{
if(this.PopupWnd){
this.PopupWnd.hide();
}
}
}
};
CssClassSelector.prototype.TogglePopup=function(){
if(this.PopupWnd){
this.ShowPopup(!this.PopupWnd.isOpen);
}
};
function PopupWindow(){
this.Popup=document.createElement("SPAN");
this.isOpen=false;
if(this.Popup){
this.Popup.className="RadEDropDownTable";
this.Popup.style.backgroundColor="#FFFFFF";
this.Popup.style.position="absolute";
this.Popup.style.zIndex=51200;
this.Popup.style.overflow="hidden";
this.Popup.style.display="none";
document.body.appendChild(this.Popup);
}
}
PopupWindow.prototype.show=function(x,y,_33,_34,_35){
this.isOpen=true;
if(this.Popup){
var _36=0;
var top=0;
var _38=this.GetElementCoords(_35);
_36+=_38[0]+x;
top+=_38[1]+y;
this.Popup.style.position="absolute";
this.Popup.style.left=_36;
this.Popup.style.top=top;
this.Popup.style.width=parseInt(_33)+"px";
this.Popup.style.height=parseInt(_34)+"px";
this.Popup.style.display="";
document.body.addEventListener("click",PopupWindow_OnMouseDown,true);
window.ActivePopupWindow=this;
}
};
PopupWindow.prototype.hide=function(){
this.isOpen=false;
if(this.Popup){
document.body.removeEventListener("click",PopupWindow_OnMouseDown,true);
this.Popup.style.display="none";
}
window.ActivePopupWindow=null;
};
PopupWindow.prototype.GetElementCoords=function(_39){
var _3a=new Array(0,0);
if(_39&&_39.offsetParent){
while(_39.offsetParent){
_3a[0]+=_39.offsetLeft;
_3a[1]+=_39.offsetTop;
_39=_39.offsetParent;
if(_39==document.body){
_3a[0]-=_39.offsetLeft;
_3a[1]-=_39.offsetTop;
}
}
}
return _3a;
};
function PopupWindow_OnMouseDown(e){
var _3c=false;
var el=e.target;
while(!_3c&&el&&el!=el.parentNode){
_3c=(el==window.ActivePopupWindow.Popup);
el=el.parentNode;
}
if(window.ActivePopupWindow&&!_3c){
window.ActivePopupWindow.hide();
}
}
function CssClassSelectorNS(id,_3f,_40,_41,_42,_43){
this.ID=id;
this.DropDown=document.getElementById("CssClassSelector_"+id);
this.SelectedIndex=-1;
this.SkinPath=_43;
this.Document=null;
this.Initialize(_3f,_40);
}
CssClassSelectorNS.prototype.Initialize=function(_44,_45){
this.ArrCssClasses=_44;
this.Document=window.document;
this.InitOptions();
};
CssClassSelectorNS.prototype.InitOptions=function(){
while(this.DropDown.options.length>0){
this.DropDown.options[0]=null;
}
try{
this.AddCssClass("",null,localization["ClearStyle"],-1);
for(var i=0;this.ArrCssClasses&&i<this.ArrCssClasses.length;i++){
this.AddRadCssClass(this.ArrCssClasses[i],i);
}
}
catch(ex){
}
};
CssClassSelectorNS.prototype.AddRadCssClass=function(_47,_48){
this.AddCssClass(_47.Tag,_47.Rule,_47.Alias,_48);
};
CssClassSelectorNS.prototype.AddCssClass=function(tag,_4a,_4b,_4c){
var opt=this.Document.createElement("OPTION");
opt.text=_4b;
opt.value=_4c;
if(null!=_4a){
this.ApplyRule(opt,_4a);
if(""==opt.style.backgroundImage){
opt.style.backgroundImage="url("+this.GetCssClassImageSrcByTag(tag)+")";
opt.style.backgroundRepeat="no-repeat";
}
var _4e="";
if(_4a){
_4e=_4a.selectorText;
}else{
_4e=_4b;
}
opt.setAttribute("title",_4e);
}
this.DropDown.options.add(opt);
};
CssClassSelectorNS.prototype.ApplyRule=function(_4f,_50){
if(!_4f||!_50){
return;
}
_4f.style.cssText=_50.style.cssText;
var _51=_4f.style.backgroundColor.toLowerCase();
var _52=_4f.style.color.toLowerCase();
if((""==_51||"#ffffff"==_51||"white"==_51)&&("#ffffff"==_52||"white"==_52)){
_4f.style.backgroundColor="#aaaaaa";
}
_4f.style.width="";
_4f.style.height="";
};
CssClassSelectorNS.prototype.GetCssClassImageSrcByTag=function(tag){
var _54="";
if(tag){
tag=tag.toUpperCase();
}
switch(tag){
case "ALL":
case "A":
case "IMG":
case "TABLE":
case "P":
_54=tag;
break;
default:
_54="Custom";
break;
}
return this.SkinPath+"Img/class"+_54+".gif";
};
CssClassSelectorNS.prototype.SelectCssClass=function(_55){
var _56=null;
var opt=null;
var _58=null;
var _59=-1;
for(var i=0;i<this.DropDown.options.length;i++){
opt=this.DropDown.options[i];
_59=parseInt(opt.value);
if(0<=_59&&this.ArrCssClasses&&_59<this.ArrCssClasses.length){
_58=this.ArrCssClasses[opt.value].ClassName;
if(_58.toLowerCase()==_55.toLowerCase()){
_56=opt;
break;
}
}
}
if(null==_56&&""!=_55&&this.ArrCssClasses){
var _59=this.ArrCssClasses.length;
var _5b={Tag:"ALL",Rule:null,Alias:_55,ClassName:_55};
this.ArrCssClasses[_59]=_5b;
this.AddRadCssClass(this.ArrCssClasses[_59],_59);
_56=this.DropDown.options[this.DropDown.options.length-1];
}else{
if(""==_55){
_56=this.DropDown.options[0];
}
}
if(null!=_56){
_56.selected=true;
}
};
CssClassSelectorNS.prototype.GetSelectedClassName=function(){
var _5c=null;
if(this.DropDown.selectedIndex>-1){
_5c=this.DropDown.options[this.DropDown.selectedIndex];
}
var _5d="";
if(_5c){
var _5e=parseInt(_5c.value);
if(-1<_5e&&this.ArrCssClasses&&_5e<this.ArrCssClasses.length){
_5d=this.ArrCssClasses[_5c.value].ClassName;
}
}
return _5d;
};;;var fileName,pathName;
var deletePath=false;
var deleteCheck=false;
var selection=false;
var submitForUpload;
function submitDocumentsFile(_1){
submitForUpload=true;
var _2=document.getElementById(FileUploadID);
if(trim(_2.value)==""){
alert(localization["Alertfile"]);
_2.focus();
submitForUpload=false;
}else{
var _3=false;
fileName=_2.value;
if(fileName.lastIndexOf("/")>=0){
fileName=fileName.substr(fileName.lastIndexOf("/")+1);
}
if(fileName.lastIndexOf("\\")>=0){
fileName=fileName.substr(fileName.lastIndexOf("\\")+1);
}
for(var i=0;i<documentFilters.length;i++){
if(fileName.match(new RegExp(documentFilters[i].replace(/\./ig,"\\.").replace(/\*/ig,".+").replace(/\?/ig,".")+"$","i"))){
_3=true;
break;
}
}
if(!_3){
alert(localization["Alertvalid"]);
submitForUpload=false;
}else{
document.getElementById(fileDirID).value=_1.CurrentItem.GetPath();
document.getElementById("loader").innerHTML=localization["Uploading"];
showObject("loader");
}
}
}
function changeTarget(_5){
document.getElementById("linkTarget").value=_5.value;
_5.selectedIndex=0;
}
function DocumentPreviewer(){
this.DocumentPath="";
this.AltTextHolder=document.getElementById("tooltip");
}
DocumentPreviewer.prototype.Clear=function(){
};
DocumentPreviewer.prototype.GetHtml=function(){
if(this.DocumentPath){
return "<img src=\""+this.DocumentPath+"\" border=\"0\">";
}else{
return "";
}
};
DocumentPreviewer.prototype.LoadObjectFromPath=function(_6){
if(_6){
this.DocumentPath=_6;
}
};
DocumentPreviewer.prototype.GetAltText=function(){
return this.AltTextHolder.value;
};
DocumentPreviewer.prototype.SetAltText=function(_7){
this.AltTextHolder.value=_7;
};;if(typeof window.RadControlsNamespace=="undefined"){
window.RadControlsNamespace={};
}
RadControlsNamespace.DomEventsMixin=function(){
};
RadControlsNamespace.DomEventsMixin.Initialize=function(_1){
_1.AttachDomEvent=this.AttachDomEvent;
_1.DetachDomEvent=this.DetachDomEvent;
_1.DisposeDomEvents=this.DisposeDomEvents;
_1.ClearEventPointers=this.ClearEventPointers;
_1.RegisterForAutomaticDisposal=this.RegisterForAutomaticDisposal;
_1.AutomaticDispose=this.AutomaticDispose;
_1.CreateEventHandler=this.CreateEventHandler;
_1.private_AttachDomEvent=this.private_AttachDomEvent;
_1.ClearEventPointers();
};
RadControlsNamespace.DomEventsMixin.CreateEventHandler=function(_2){
var _3=this;
return function(e){
if(!e){
e=window.event;
}
return _3[_2](e);
};
};
RadControlsNamespace.DomEventsMixin.AttachDomEvent=function(_5,_6,_7){
var _8=this.CreateEventHandler(_7);
this._eventPointers[this._eventPointers.length]=[_5,_6,_8];
this.private_AttachDomEvent(_5,_6,_8);
};
RadControlsNamespace.DomEventsMixin.private_AttachDomEvent=function(_9,_a,_b){
if(_9.attachEvent){
_9.attachEvent("on"+_a,_b);
}else{
if(_9.addEventListener){
_9.addEventListener(_a,_b,false);
}
}
};
RadControlsNamespace.DomEventsMixin.DetachDomEvent=function(_c,_d,_e){
if(_c.detachEvent){
_c.detachEvent("on"+_d,_e);
}
};
RadControlsNamespace.DomEventsMixin.DisposeDomEvents=function(){
for(var i=0;i<this._eventPointers.length;i++){
this.DetachDomEvent(this._eventPointers[i][0],this._eventPointers[i][1],this._eventPointers[i][2]);
}
this.ClearEventPointers();
};
RadControlsNamespace.DomEventsMixin.RegisterForAutomaticDisposal=function(_10){
var me=this;
var _12=this.CreateEventHandler(_10);
var _13=function(){
_12();
me.DisposeDomEvents();
me=null;
};
this.private_AttachDomEvent(window,"unload",_13);
};
RadControlsNamespace.DomEventsMixin.ClearEventPointers=function(){
this._eventPointers=[];
};;if(!activeDropDown){
var activeDropDown=null;
}
if(!onClickHandlerAttached){
var onClickHandlerAttached=false;
}
function DropDownMenu(_1,_2){
this.MenuButton=_1;
this.MenuElement=_2;
this.MenuEventHandler=null;
var _3=this;
if(!onClickHandlerAttached){
if(document.all){
document.attachEvent("onclick",_3.BodyClickHandler);
}else{
document.addEventListener("click",_3.BodyClickHandler,true);
}
}
}
DropDownMenu.prototype.BodyClickHandler=function(e){
if(!e){
var e=window.event;
}
if(null!=activeDropDown){
activeDropDown.SetVisible(false);
}
};
DropDownMenu.prototype.Enable=function(_5){
if(this.IsValid()){
this.MenuButton.disabled=_5?false:true;
}
};
DropDownMenu.prototype.IsValid=function(){
return (null!=this.MenuButton)&&(null!=this.MenuElement);
};
DropDownMenu.prototype.IsVisible=function(){
return this.IsValid()&&"visible"==this.MenuElement.style.visibility;
};
DropDownMenu.prototype.Toggle=function(){
this.SetVisible(!this.IsVisible());
};
DropDownMenu.prototype.GetCoords=function(_6){
var _7=new Array(0,0);
if(_6.offsetParent){
while(_6.offsetParent){
_7[0]+=_6.offsetLeft;
_7[1]+=_6.offsetTop;
_6=_6.offsetParent;
if(_6==document.body){
_7[0]-=_6.offsetLeft;
_7[1]-=_6.offsetTop;
}
}
}
return _7;
};
DropDownMenu.prototype.SetVisible=function(_8){
if(this.IsValid()){
if(_8){
if(activeDropDown){
activeDropDown.SetVisible(false);
}
var _9=this.GetCoords(this.MenuButton);
this.MenuElement.style.left=_9[0]+"px";
this.MenuElement.style.top=(_9[1]+this.MenuButton.offsetHeight)+"px";
this.MenuElement.style.zIndex=51200;
this.MenuElement.style.overflow="hidden";
this.MenuElement.style.position="absolute";
this.MenuElement.style.visibility="visible";
}else{
this.MenuElement.style.visibility="hidden";
}
activeDropDown=_8?this:null;
if(null!=this.MenuEventHandler&&null!=this.MenuEventHandler.OnShowMenu){
this.MenuEventHandler.OnShowMenu(_8);
}
}
};
DropDownMenu.prototype.SetMenuInnerHtml=function(_a){
this.MenuElement.innerHTML=_a;
};;if(typeof (RadEditorNamespace)=="undefined"){
var RadEditorNamespace=new Object();
}
RadEditorNamespace.FileBrowserStaticPreloadedImages=["aif","aifc","aiff","asf","asx","au","avi","bmp","doc","file","fla","folder","gif","jpg","m1v","m3u","mid","midi","mp2","mp2v","mp3","mpa","mpe","mpeg","mpg","mpv2","pdf","png","rmi","snd","swf","tif","tiff","vm","wav","wax","wma","wmp","wmv","wmx","wvx"];
RadEditorNamespace.FileBrowserDisplayMode={};
RadEditorNamespace.FileBrowserDisplayMode.Tree=0;
RadEditorNamespace.FileBrowserDisplayMode.List=1;
RadEditorNamespace.FileBrowser=function(id,_2,_3,_4,_5,_6,_7,_8,_9,_a,_b,_c,_d,_e,_f,_10,_11){
RadControlsNamespace.DomEventsMixin.Initialize(this);
this.SelectedItemParentPathHolder=_4;
this.SelectedItemTypeHolder=_5;
this.SelectedItemNameHolder=_6;
this.SelectedItemTagHolder=_7;
this.FileListTable=_2;
this.FileListTableBody=_2.tBodies[0];
this.SkinPath=_3;
this.DisplayMode=RadEditorNamespace.FileBrowserDisplayMode.Tree;
this.RootItem=null;
this.CurrentItem=null;
this.SelectedItem=null;
this.private_BrowserTableElements=[];
this.private_PreloadedImages=[];
this.SortExpression="Name";
this.SortDirection="ASC";
this.SelectedRow=null;
this.PreloadImages();
this.OnClientClick=null;
this.OnFolderChange=null;
this.OnItemsRendered=null;
this.XmlRequest=null;
this.CurrentlyPolling=false;
this.NewFolderDiv=_8;
this.NewFolderNameHolder=_9;
this.NewFolderButton=_a;
this.DeleteButton=_b;
this.RefreshButtonLink=_f;
this.SortDirectionButtons=[];
this.SortDirectionButtons["Extension"]=_c;
this.SortDirectionButtons["Name"]=_d;
this.SortDirectionButtons["Size"]=_e;
this.Form=_11;
this.UniqueID=_10;
this.ReadPermission=1;
this.UploadPermission=2;
this.DeletePermission=4;
this.RegisterForAutomaticDisposal("Dispose");
};
RadEditorNamespace.FileBrowser.prototype={Dispose:function(){
this.DisposeElements(this.SortDirectionButtons);
this.DisposeElements(this);
this.SkinPath=null;
this.RootItem=null;
this.CurrentItem=null;
this.SelectedItem=null;
this.private_BrowserTableElements=null;
this.private_PreloadedImages=null;
this.SortExpression=null;
this.SortDirection=null;
this.SelectedRow=null;
this.OnClientClick=null;
this.OnFolderChange=null;
this.XmlRequest=null;
this.CurrentlyPolling=null;
this.SortDirectionButtons=null;
this.UniqueID=null;
this.ReadPermission=null;
this.UploadPermission=null;
this.DeletePermission=null;
},DisposeElements:function(_12){
for(var _13 in _12){
if(_12[_13]&&_12[_13].tagName){
_12[_13]=null;
}
}
},InitializeFileList:function(_14,_15){
this.DisplayMode=_15;
this.RootItem=new RadEditorNamespace.FileBrowserItem(_14);
this.CurrentItem=this.GetItem(this.RootItem.Children,this.SelectedItemParentPathHolder.value);
if(this.CurrentItem==null){
if(this.RootItem.Children.length==1){
this.CurrentItem=this.RootItem.Children[0];
}else{
this.CurrentItem=this.RootItem;
}
}
this.InitSelectedItem();
var _16=this.SelectedItem;
this.SetCurrentDirectory();
if(_16!=null){
this.SetSelectedItemData(_16);
}
this.RenderOnFolderChange(false);
if(this.SelectedItem){
this.SelectItem(this.SelectedItem);
}
},GetItem:function(_17,_18){
var _19=this.RootItem;
for(var i=0;i<_17.length;i++){
var _1b=_17[i];
var _1c=_1b.GetPath();
if(this.DisplayMode==RadEditorNamespace.FileBrowserDisplayMode.Tree){
if(_18.indexOf(_1c)==0){
if(_18==_1c){
return _1b;
}else{
return this.GetItem(_1b.Children,_18);
}
}
}else{
if(_18==_1c){
return _1b;
}
}
}
return null;
},Render:function(){
this.Clear();
if(this.CurrentItem.Parent!=null){
this.CreateRow("","..",this.private_PreloadedImages["FolderUp"].cloneNode(true),"&nbsp;","GoUp",null);
}
this.SortItems();
this.SetSortDirectionImage();
for(var i=0;i<this.CurrentItem.Children.length;i++){
this.CreateBrowserItem(this.CurrentItem.Children[i]);
}
},GoUp:function(e){
this.CurrentItem=this.CurrentItem.Parent;
this.SetCurrentDirectory();
this.RenderOnFolderChange(true);
},ChangeDirectory:function(e){
var _20=this.FindEventSender(e,"tr");
this.AddLoadingMessage(_20.cells[1]);
this.CurrentItem=_20.browserItem;
var me=this;
var _22=function(){
me.SetCurrentDirectory();
if(me.CurrentItem.Children.length==0){
me.DoCallback();
}else{
me.RenderOnFolderChange(true);
}
};
window.setTimeout(_22,1);
},OnSelectItem:function(e){
var _24=this.FindEventSender(e,"tr");
this.SelectRow(_24);
var _25=_24.browserItem;
this.SetSelectedItemData(_25);
this.SetButtonPermission(this.DeleteButton,this.DeletePermission,_25);
if(this.OnClientClick){
this.OnClientClick(_25);
}
},SelectItem:function(_26){
var _27=this.GetItemRow(_26);
if(_27){
this.SelectRow(_27);
}
},GetItemRow:function(_28){
for(var i=0;i<this.FileListTableBody.rows.length;i++){
var _2a=this.FileListTableBody.rows[i];
if(_2a.browserItem==_28){
return _2a;
}
}
return null;
},SelectRow:function(row){
if(this.SelectedRow){
this.SelectedRow.className="TreeNodeDefault";
}
row.className="TreeNodeSelected";
this.SelectedRow=row;
},InitSelectedItem:function(){
if(this.SelectedItem==null&&this.SelectedItemNameHolder.value!=""){
this.SelectedItem=this.GetChild(this.SelectedItemNameHolder.value);
}
return this.SelectedItem;
},GetChild:function(_2c){
for(var i=0;i<this.CurrentItem.Children.length;i++){
var _2e=this.CurrentItem.Children[i];
if(_2e.Name==_2c){
return _2e;
}
}
return null;
},SetSortDirectionImage:function(){
this.SetElementBackgroundImage(this.SortDirectionButtons["Extension"],this.SkinPath+"Dialogs/empty.gif");
this.SetElementBackgroundImage(this.SortDirectionButtons["Name"],this.SkinPath+"Dialogs/empty.gif");
this.SetElementBackgroundImage(this.SortDirectionButtons["Size"],this.SkinPath+"Dialogs/empty.gif");
this.SetElementBackgroundImage(this.SortDirectionButtons[this.SortExpression],this.SkinPath+"Dialogs/"+this.SortDirection+".gif");
},SetElementBackgroundImage:function(_2f,_30){
_2f.style.backgroundImage="url("+_30+")";
},SetSelectedItemData:function(_31){
this.SelectedItem=_31;
this.SelectedItemTypeHolder.value=_31.Type;
this.SelectedItemNameHolder.value=_31.Name;
this.SelectedItemTagHolder.value=_31.Tag;
},SetCurrentDirectory:function(){
this.SelectedItemParentPathHolder.value=this.CurrentItem.GetPath();
this.SelectedItemTagHolder.value=this.CurrentItem.Tag;
this.SelectedItem=this.CurrentItem;
this.SelectedItemTypeHolder.value="";
this.SelectedItemNameHolder.value="";
},FindEventSender:function(e,_33){
var _34=e.srcElement?e.srcElement:e.target;
while(_34){
if(_34.tagName&&_34.tagName.toLowerCase()==_33.toLowerCase()){
return _34;
}
_34=_34.parentNode;
}
return _34;
},CreateBrowserItem:function(_35){
if(_35.Type=="D"){
if(window.RadControlsNamespace.Browser.IsSafari){
this.CreateBrowserItemRow(_35,"ChangeDirectory",null);
return;
}
this.CreateBrowserItemRow(_35,"OnSelectItem","ChangeDirectory");
}else{
this.CreateBrowserItemRow(_35,"OnSelectItem",null);
}
},CreateNewFolder:function(){
if(this.CurrentItem.Permissions&this.UploadPermission){
if(this.NewFolderDiv.style.display!="none"){
this.CancelNewFolderCreation();
}else{
this.NewFolderDiv.style.position="relative";
if(!this.NewFolderDiv.style.top){
this.NewFolderDiv.style.top-=26;
}
this.FileListTable.style.position="relative";
this.FileListTable.style.top-=26;
if(this.NewFolderDiv.style.filter!=null&&this.NewFolderDiv.filters){
this.NewFolderDiv.style.filter="progid:DXImageTransform.Microsoft.Stretch(stretchstyle=PUSH)";
if(this.NewFolderDiv.filters[0]){
this.NewFolderDiv.filters[0].duration=0.2;
this.NewFolderDiv.filters[0].apply();
this.NewFolderDiv.style.display="block";
this.NewFolderDiv.filters[0].play();
}
}
this.NewFolderDiv.style.display="block";
this.NewFolderNameHolder.value="";
this.NewFolderNameHolder.focus();
}
}else{
alert(localization["CREATE_NOT_ALLOWED_ALERT"]);
}
return false;
},CancelNewFolderCreation:function(){
this.NewFolderNameHolder.value="";
if(this.NewFolderDiv.style.display!="none"){
this.NewFolderDiv.style.display="none";
this.FileListTable.style.top="";
}
return false;
},AddLoadingMessage:function(_36){
_36.innerHTML+="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='background-color:#ffffcc;border:1px solid black;'> Loading...</span>";
},SetButtonPermission:function(_37,_38,_39){
if(_39.Permissions&_38){
_37.disabled=false;
_37.parentNode.className="";
}else{
_37.disabled=true;
_37.parentNode.className="Disabled";
}
},ConfirmDeletion:function(){
if(this.SelectedItem){
if(!(this.CurrentItem.Permissions&&this.DeletePermission)){
alert(localization["DELETE_NOT_ALLOWED_ALERT"]);
return false;
}
if(this.CurrentItem.Parent==null){
alert(localization["ROOT_CANNOT_BE_DELETED_ALERT"]);
return false;
}
if(this.OnDelete!=null&&!this.OnDelete(this.SelectedItem)){
return false;
}
return confirm(localization[(this.SelectedItem.Type=="F")?"DELETE_FILE_ALERT":"DELETE_FOLDER_ALERT"]);
}else{
alert(localization["DELETE_NONE_ALERT"]);
return false;
}
},RenderOnFolderChange:function(_3a){
this.Render();
this.SetButtonPermission(this.DeleteButton,this.DeletePermission,this.CurrentItem);
this.SetButtonPermission(this.NewFolderButton,this.UploadPermission,this.CurrentItem);
if(_3a&&this.OnFolderChange){
this.OnFolderChange(this.CurrentItem);
}
if(this.OnItemsRendered){
this.OnItemsRendered(this.CurrentItem);
}
},GetBrowserItemImage:function(_3b){
var _3c=null;
if(_3b.Type=="D"){
_3c=this.private_PreloadedImages["folder"];
}else{
var _3d=_3b.Extension;
_3d=_3d.substring(_3d.lastIndexOf(".")+1).toLowerCase();
if(this.private_PreloadedImages[_3d]){
_3c=this.private_PreloadedImages[_3d];
}else{
_3c=this.private_PreloadedImages["file"];
}
}
return _3c.cloneNode(true);
},CreateBrowserItemRow:function(_3e,_3f,_40){
var _41=this.CreateRow(_3e.GetPath(),_3e.Name,this.GetBrowserItemImage(_3e),_3e.ShortSize,_3f,_40);
_41.browserItem=_3e;
},CreateRow:function(_42,_43,_44,_45,_46,_47){
var row=this.FileListTableBody.insertRow(this.FileListTableBody.rows.length);
row.className="TreeNodeDefault";
row.title=_42;
if(_46){
this.AttachDomEvent(row,"click",_46);
}
if(_47){
this.AttachDomEvent(row,"dblclick",_47);
}
var _49=row.insertCell(row.cells.length);
_49.align="middle";
_49.appendChild(_44);
var _4a=row.insertCell(row.cells.length);
_4a.innerHTML=_43;
_4a.setAttribute("unselectable","on");
var _4b=row.insertCell(row.cells.length);
_4b.noWrap=true;
_4b.innerHTML=_45;
return row;
},Clear:function(){
this.DisposeDomEvents();
var _4c=this.FileListTableBody.rows.length;
for(var i=0;i<_4c;i++){
var row=this.FileListTableBody.rows[0];
row.parentNode.removeChild(row);
}
},PreloadImages:function(){
var _4f=document.createElement("img");
var _50=RadEditorNamespace.FileBrowserStaticPreloadedImages;
for(var i=0;i<_50.length;i++){
this.private_PreloadedImages[_50[i]]=this.FormatImage(_4f.cloneNode(true),16,16,"Dialogs/ImgExt/"+_50[i]+".gif");
}
this.private_PreloadedImages["FolderUp"]=this.FormatImage(_4f.cloneNode(true),16,9,"Dialogs/FolderUp.gif");
},FormatImage:function(_52,_53,_54,_55){
_52.width=_53;
_52.height=_54;
_52.src=this.SkinPath+_55;
return _52;
},Sort:function(_56){
if(_56==this.SortExpression){
this.SortDirection=(this.SortDirection=="DESC")?"ASC":"DESC";
}else{
this.SortDirection="ASC";
this.SortExpression=_56;
}
this.Render();
},SortItems:function(){
var me=this;
var _58=function(_59,_5a){
return me.Compare(_59,_5a,me.SortDirection,me.SortExpression);
};
this.CurrentItem.Children.sort(_58);
},Compare:function(_5b,_5c,_5d,_5e){
var _5f=_5b.Type=="D"?1:0;
var _60=_5c.Type=="D"?1:0;
var _61=_60-_5f;
if(_61==0){
var _62=(_5f==1&&_60==1)?"Name":_5e;
var _63=(typeof (_5b[_62])=="string")?_5b[_62].toLowerCase():_5b[_62];
var _64=(typeof (_5c[_62])=="string")?_5c[_62].toLowerCase():_5c[_62];
if(_63>_64){
_61=1;
}else{
if(_63<_64){
_61=-1;
}else{
_61=0;
}
}
}
return _5d=="ASC"?_61:-_61;
},DoCallback:function(){
if(!this.CurrentlyPolling){
this.CurrentlyPolling=true;
var _65=this.GetXmlRequest();
if(_65){
var url=document.location.href;
_65.open("POST",url,true);
_65.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
var me=this;
_65.onreadystatechange=function(){
if(_65.readyState!=4){
return;
}
var _68=_65.responseText;
if(_68){
var _69="MESSAGETOKEN=";
var _6a=_68.indexOf(_69);
if(_6a>-1){
var _6b=_68.substring(_6a+_69.length);
alert(localization[_6b]);
me.RefreshButtonLink.click();
}else{
me.LoadChildNodes(eval(_68));
me.CurrentlyPolling=false;
me.RenderOnFolderChange(true);
}
}
};
_65.send(this.GetPostData());
}
}
},GetXmlRequest:function(){
if(this.XmlRequest){
}else{
if(typeof (XMLHttpRequest)!="undefined"){
this.XmlRequest=new XMLHttpRequest();
}
if(typeof (ActiveXObject)!="undefined"){
this.XmlRequest=new ActiveXObject("Microsoft.XMLHTTP");
}
}
return this.XmlRequest;
},GetPostData:function(){
var _6c=new RadEditorNamespace.PostData(this.Form);
return _6c.Get()+"&fileBrowserAjaxLoad=true";
},LoadChildNodes:function(_6d){
for(var i=0;i<_6d.length;i++){
this.CurrentItem.Children[this.CurrentItem.Children.length]=new RadEditorNamespace.FileBrowserItem(_6d[i],this.CurrentItem);
}
}};
RadEditorNamespace.PostData=function(_6f){
this.Form=_6f;
this.PostData="";
};
RadEditorNamespace.PostData.prototype={Get:function(){
if(this.PostData==""){
this.BuildPostData();
}
return this.PostData;
},private_AddToData:function(_70,_71){
if(this.PostData!=""){
this.PostData+="&";
}
this.PostData+=_70+"="+this.EncodeData(_71);
},BuildPostData:function(){
try{
for(var i=0;i<this.Form.elements.length;i++){
var _73=this.Form.elements[i];
var _74=_73.tagName.toLowerCase();
switch(_74){
case "input":
var _75=_73.type;
if((_75=="text"||_75=="hidden"||_75=="password"||((_75=="checkbox"||_75=="radio")&&_73.checked))){
this.private_AddToData(_73.name,_73.value);
}
break;
case "select":
for(var j=0;j<_73.childNodes.length;j++){
var _77=_73.childNodes[j];
if(!_77.tagName){
continue;
}
if((_77.tagName.toLowerCase()=="option")&&(_77.selected==true)){
this.private_AddToData(_73.name,_77.value);
}
}
break;
case "textarea":
this.private_AddToData(_73.name,_77.value);
break;
}
}
}
catch(e){
}
},EncodeData:function(_78){
return (encodeURIComponent)?encodeURIComponent(_78):escape(_78);
}};;if(typeof (RadEditorNamespace)=="undefined"){
var RadEditorNamespace=new Object();
}
RadEditorNamespace.FileBrowserItem=function(_1,_2){
this.Parent=_2;
this.Type=_1[0];
this.Permissions=_1[1];
this.Name=_1[2];
this.Path=_1[3];
this._url=_1[4]?_1[4]:null;
this.Extension=_1[5];
this.Size=_1[6];
this.Tag=_1[7];
this.ShortSize=(this.Type=="D")?"&nbsp;":this.private_GetShortSize(this.Size);
this.private_CombinedPath=null;
this.private_CombinedPath=_1[7]?_1[7]:null;
this.Attributes=_1[8];
this.Children=[];
var _3=_1[9];
for(var i=0;i<_3.length;i++){
this.Children[this.Children.length]=new RadEditorNamespace.FileBrowserItem(_3[i],this);
}
};
RadEditorNamespace.FileBrowserItem.prototype={GetPath:function(){
if(this.private_CombinedPath==null){
var _5=this.Path+((this.Type=="D")?this.EndWithSlash(this.Name):this.Name);
var _6=this.Parent;
while(_6&&_6.Parent!=null){
_5=_6.Path+this.EndWithSlash(_6.Name)+_5;
_6=_6.Parent;
}
this.private_CombinedPath=_5;
}
return this.private_CombinedPath;
},EndWithSlash:function(_7){
if(_7.lastIndexOf("/")==(_7.length-1)){
return _7;
}
return _7+"/";
},GetUrl:function(){
if(this._url==null){
return this.GetPath();
}else{
return this._url;
}
},IsThumbnail:function(_8){
return (this.GetOriginalImage(_8)!=null);
},GetOriginalImage:function(_9){
if(this.Parent){
for(var i=0;i<this.Parent.Children.length;i++){
var _b=this.Parent.Children[i];
if(this.Name.toLowerCase()==_b.GetThumbnailName(_9).toLowerCase()){
return _b;
}
}
}
return null;
},GetThumbnailName:function(_c){
return this.Name.substring(0,this.Name.lastIndexOf(this.Extension))+_c+this.Extension;
},private_GetShortSize:function(_d){
var _e=_d/1024;
var _f=_e/1024;
if(_f>0.8){
return ""+Math.round(_f*100)/100+" "+localization["Megabytes"];
}else{
if(_e>0.8){
return ""+Math.round(_e*100)/100+" "+localization["Kilobytes"];
}else{
return ""+_d+" "+localization["bytes"];
}
}
}};;function FindAndReplaceControl(id,_2){
this.SearchWord="";
this.ReplaceWord="";
var _3=_2.ownerDocument;
this.Window=_3.parentWindow?_3.parentWindow:_3.defaultView;
this.OriginalSelection=null;
this.SearchUp=false;
this.SelectionOnly=false;
this.WholeWord=false;
this.CaseSensitive=null;
this._replaceOccurances=0;
this._replace=false;
this._initSearchExecuted=false;
this._endRange=null;
var _4=localization;
this._messageReplaceComplete=_4["FinishedSearching"]+" "+_4["AndHasMade"]+" ";
this._messageReplacements=" "+_4["Replacements"];
this._messageSearchComplete=_4["FinishedSearching"];
this._messageNothingToSearch=_4["SearchCriteriaNotSpecified"];
this._messageSearchNotSupported="This browser does not support searching.";
this.ResetEngine=function(){
this._replace=false;
this._initSearchExecuted=false;
this._replaceOccurances=0;
this._flippedOverEnd=false;
};
this.FinishSearch=function(){
if(this._replace){
alert(this._messageReplaceComplete+this._replaceOccurances+this._messageReplacements);
}else{
alert(this._messageSearchComplete);
}
this.ResetEngine();
};
this.InitSearch=function(){
if(this._initSearchExecuted){
return;
}
this._initSearchExecuted=true;
if(!this.SearchWord){
alert(this._messageNothingToSearch);
return false;
}
var oR=this.GetRange();
if(!this.Window.find&&(oR&&!oR.findText)){
alert(this._messageSearchNotSupported);
return false;
}
this._endRange=this.OriginalSelection;
if(this._endRange){
this.SelectRange(this._endRange);
}
this.CollapseSelection(!this.SearchUp);
};
this.Replace=function(){
if(!this.ReplaceNext()){
this.FinishSearch();
}
};
this.Find=function(){
if(!this.FindNext()){
this.FinishSearch();
}
};
this.ReplaceAll=function(){
this._replaceOccurances=0;
var _6=this.InitSearch();
if(false==_6){
return _6;
}
while(this.ReplaceNext()){
}
this.CollapseSelection();
this.FinishSearch();
};
this.ReplaceNext=function(){
this._replace=true;
var _7=false;
var _8=false;
var _9=this.GetRange();
if(_9){
var _a=this.GetHtmlText(_9);
if(_a==this.SearchWord){
_7=true;
_8=true;
}
}
if(!_7){
_8=this.FindNext();
}
if(!_8){
return false;
}
this.DoReplace();
this._replaceOccurances++;
return true;
};
this.SelectRange=function(_b){
if(_b.select){
_b.select();
}else{
if(this.Window.getSelection){
theSelection=this.Window.getSelection();
theSelection.addRange(_b);
}
}
};
}
if(document.all&&!window.opera){
FindAndReplaceControl.prototype={GetHtmlText:function(_c){
return _c.htmlText;
},GetRange:function(){
if(this.Window.document.body.setActive){
this.Window.document.body.setActive();
}
var _d=this.Window.document.selection.createRange();
return _d;
},DoReplace:function(){
var _e=this.GetRange();
var _f=_e.duplicate();
var _10=_e.duplicate();
_f.collapse(true);
_10.collapse(false);
_e.pasteHTML(this.ReplaceWord);
_e.setEndPoint("StartToStart",_f);
_e.setEndPoint("EndToEnd",_10);
_e.select();
},FindNext:function(){
var _11=this.InitSearch();
if(false==_11){
return _11;
}
if(this.Window.document.body.setActive){
this.Window.document.body.setActive();
}
var rng=this.GetRange();
rng.collapse(this.SearchUp);
var _13=(this.WholeWord?2:0)+(this.CaseSensitive?4:0);
var _14=rng.findText(this.SearchWord,this.SearchUp?-1:1,_13);
if(_14){
rng.select();
}
var _15=this.GetRange();
var _16=this.SearchUp?-1:1;
if(this.SelectionOnly){
var dir=this.SearchUp?"StartToStart":"EndToEnd";
var _18=_15.compareEndPoints(dir,this._endRange);
if(_18==_16){
this.CollapseSelection();
return false;
}
}
if(!_14){
if(this._flippedOverEnd){
return _14;
}
_15=this.GetRange();
_15.moveToElementText(this.Window.document.body);
_15.collapse(!this.SearchUp);
_15.select();
this._flippedOverEnd=true;
_14=_15.findText(this.SearchWord,this.SearchUp?-1:1,_13);
}
var dir=this.SearchUp?"EndToStart":"StartToEnd";
var _18=_15.compareEndPoints(dir,this._endRange);
if(_18==_16&&this._flippedOverEnd){
_14=false;
}
if(_14&&_15){
_15.select();
}
return _14;
},CollapseSelection:function(dir){
var _1a=this.Window.document.selection.createRange();
_1a.collapse(dir);
_1a.select();
}};
}else{
if(window.find){
FindAndReplaceControl.prototype={GetHtmlText:function(rng){
var _1c=this.Window.document.createElement("div");
var _1d=rng.cloneContents();
_1c.appendChild(_1d);
return _1c.innerHTML;
},GetRange:function(){
var _1e=this.Window.getSelection();
var _1f=null;
if(_1e.getRangeAt){
try{
_1f=_1e.getRangeAt(0);
}
catch(e){
}
}
return _1f;
},DoReplace:function(){
var _20=this.GetRange();
if(_20){
_20.deleteContents();
}
var _21=this.Window.document.createTextNode(this.ReplaceWord);
var _22=_20.startContainer;
var _23=_20.startOffset;
if(_22.insertData){
_22.insertData(_23,_21.nodeValue);
_20.setEnd(_22,_23+_21.length);
_20.setStart(_22,_23);
}
},FindNext:function(){
var _24=this.InitSearch();
if(false==_24){
return _24;
}
var _25=this.SearchUp?-1:1;
var _26=this.SearchUp?1:-1;
var _27=this.GetRange();
this.CollapseSelection(this.SearchUp);
var _28=this.Window.find(this.SearchWord,this.CaseSensitive,this.SearchUp,true,false,true,false);
var _29=false;
if(_28){
_29=this.CheckWholeWord();
if(_29){
var _2a=this.GetRange();
var dir=this.SearchUp?Range.END_TO_END:Range.START_TO_START;
var _2c=_2a.compareBoundaryPoints(dir,this._endRange);
if(_2c==_26){
this._flippedOverEnd=true;
}else{
if(_2c==_25){
if(this._flippedOverEnd){
this.CollapseSelection();
return false;
}
}
}
var _2d=_27.compareBoundaryPoints(Range.START_TO_START,_2a);
var _2e=_27.compareBoundaryPoints(Range.END_TO_END,_2a);
if(_2d==0&&_2e==0){
return false;
}
}
}
if(_28&&!_29){
return this.FindNext();
}
var _2f=this.GetRange();
if(this.SelectionOnly){
var dir=this.SearchUp?Range.START_TO_START:Range.END_TO_END;
var _2c=_2f.compareBoundaryPoints(dir,this._endRange);
if(_2c==_25){
this.CollapseSelection();
return false;
}
}
return _28;
},CollapseSelection:function(dir){
var _31=this.Window.getSelection();
try{
if(true==dir){
_31.collapseToStart();
}else{
_31.collapseToEnd();
}
}
catch(e){
}
},CheckWholeWord:function(){
if(!this.WholeWord){
return true;
}
var _32=this.GetRange();
if(!_32){
return false;
}
var _33=_32.startContainer;
var _34=_32.startOffset;
var _35=_32.endOffset;
var _36=_33.nodeValue.charAt(_34-1);
var _37=_33.nodeValue.charAt(_35);
var _38=false;
_38=(null!=_36.match(/\s/)||null!=_36.match(/[^\w]/));
_38=_38&&(null!=_37.match(/\s/)||null!=_37.match(/[^\w]/));
if(!_38){
this.CollapseSelection();
}
return _38;
}};
}
};function submitFlashFile(_1){
submitForUpload=true;
var _2=document.getElementById(FileUploadID);
if(trim(_2.value)==""){
alert(localization["Alertfile"]);
_2.focus();
submitForUpload=false;
}else{
document.getElementById(fileDirID).value=fileBrowser.CurrentItem.GetPath();
document.getElementById("loader").innerHTML=localization["Uploading"];
showObject("loader");
}
}
var fileName,pathName;
var deletePath=false;
var selection=true;
var dropDownFlash;
var submitForUpload;
function additionalColor(_3){
var _4=document.getElementById("backgroundColor");
var _5=_4.getElementsByTagName("OPTION");
option=document.createElement("OPTION");
option.innerHTML=localization["Customh"]+_3;
option.value=_3;
option.selected=true;
option.style.backgroundColor=_3;
_4.insertBefore(option,_5[8]);
}
function getParameterValue(_6,_7){
for(var i=0;i<_6.childNodes.length;i++){
if((_6.childNodes[i].tagName.toUpperCase()=="PARAM")&&(_6.childNodes[i].name.toUpperCase()==_7.toUpperCase())){
return _6.childNodes[i].value;
}
}
return null;
}
function enableClass(_9){
document.getElementById("classID").disabled=!_9;
document.getElementById("version").disabled=!_9;
if(_9){
showObject("classIDRow1");
showObject("classIDRow2");
}else{
document.getElementById("classID").value="";
hideObject("classIDRow1");
hideObject("classIDRow2");
}
}
var isPreviewerInPreviewMode=false;
function FlashPreviewer(){
this.FlashPath="";
this.StripAbsolutePath=true;
}
FlashPreviewer.prototype.CreateParam=function(_a,_b,_c){
var _d="<param name=\""+_a+"\" value=\""+_b+"\">";
return _c?_d:document.createElement(_d);
};
FlashPreviewer.prototype.CreateFlashEmbed=function(_e,_f){
if(_e){
var _10="<embed ";
for(var _11 in _f){
_10+=" "+_11+"=\""+_f[_11]+"\"";
}
_10+="></embed>";
return _10;
}else{
var _12=document.createElement("EMBED");
for(var _11 in _f){
_12.setAttribute(_11,_f[_11]);
}
return _12;
}
};
FlashPreviewer.prototype.Clear=function(){
this.SetDefaultValues(document.getElementById("PropertiesPane"));
};
FlashPreviewer.prototype.GetHtml=function(){
var _13=fileBrowser.SelectedItem;
var _14=_13!=null?_13.GetUrl():null;
if(_14){
if(document.all&&(false==this.StripAbsolutePath)){
var _15=document.createElement("IMG");
_15.setAttribute("src",_14);
_14=_15.src;
}
var _16={};
_16["src"]=_14;
var _17=null;
_17=document.getElementById("loopYes");
_16["loop"]=_17.checked.toString();
_17=document.getElementById("menuYes");
_16["menu"]=_17.checked.toString();
_16["quality"]=document.getElementById("quality").value;
_16["salign"]=document.getElementById("flashAlign").value;
_16["align"]=document.getElementById("htmlAlign").value;
_16["bgcolor"]=document.getElementById("backgroundColor").value;
_16["width"]=document.getElementById("flashWidth").value;
_16["height"]=document.getElementById("flashHeight").value;
_16["type"]="application/x-shockwave-flash";
_16["pluginspage"]="http://www.macromedia.com/go/getflashplayer";
if(document.getElementById("transparentYes").checked){
_16["wmode"]="transparent";
}
if((document.all)&&(document.getElementById("classYes").checked)){
var _18={};
var _19={};
_18["classid"]=document.getElementById("classID").value;
_18["codebase"]="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version="+document.getElementById("version").value+",0,0,0\" ";
_18["width"]=document.getElementById("flashWidth").value;
_18["height"]=document.getElementById("flashHeight").value;
_18["align"]=document.getElementById("htmlAlign").value;
_19["movie"]=_14;
_19["src"]=_14;
_17=document.getElementById("playYes");
_19["play"]=_17.checked.toString();
_17=document.getElementById("loopYes");
_19["loop"]=_17.checked.toString();
_17=document.getElementById("menuYes");
_19["menu"]=_17.checked.toString();
_19["quality"]=document.getElementById("quality").value;
_19["salign"]=document.getElementById("flashAlign").value;
_19["bgcolor"]=document.getElementById("backgroundColor").value;
if(document.getElementById("transparentYes").checked){
_19["wmode"]="transparent";
}
var _1a="<object ";
for(var _1b in _18){
_1a+=" "+_1b+"=\""+_18[_1b]+"\"";
}
_1a+=">";
for(var _1b in _19){
_1a+=" "+this.CreateParam(_1b,_19[_1b],true);
}
_1a+=this.CreateFlashEmbed(true,_16)+"</object>";
return _1a;
}
return this.CreateFlashEmbed(true,_16);
}
return null;
};
FlashPreviewer.prototype.LoadObjectFromPath=function(_1c){
};
FlashPreviewer.prototype.SetDefaultValues=function(_1d){
if(!_1d){
return;
}
if(_1d.attributes&&_1d.attributes["defaultvalue"]){
var _1e=_1d.attributes["defaultvalue"].value;
if(_1d.tagName=="INPUT"){
var _1f=_1d.getAttribute("type",0).toLowerCase();
if(_1f=="checkbox"){
_1d.checked=eval(_1e);
}else{
if(_1f=="text"){
_1d.setAttribute("value",_1e);
}
}
}else{
if(_1d.tagName&&(_1d.tagName=="DIV"||_1d.tagName=="TD")){
_1d.innerHTML=_1e;
}else{
if(_1d.tagName&&_1d.tagName=="SELECT"){
selectOption(_1d,_1e);
}
}
}
}
if(_1d.childNodes&&_1d.childNodes.length>0){
for(var _20=0;_20<_1d.childNodes.length;_20++){
this.SetDefaultValues(_1d.childNodes[_20]);
}
}
};
FlashPreviewer.prototype.SwitchPreviewMode=function(_21,_22,_23){
isPreviewerInPreviewMode=!isPreviewerInPreviewMode;
this.Preview(_21,_22,_23);
};
FlashPreviewer.prototype.Preview=function(_24,_25,_26){
if((_24!=null)&&(_24.Type!="D")){
if(_25==_24.GetUrl()){
this.PreviewCurrentMode(true,_26);
}else{
this.PreviewCurrentMode(false,_26);
}
}else{
this.Clear();
document.getElementById("PreviewObjectHolder").innerHTML="";
document.getElementById("PropertiesPane").style.display="none";
document.getElementById("PreviewPane").style.display="none";
document.getElementById("EmptyPane").style.display="inline";
}
};
FlashPreviewer.prototype.PreviewCurrentMode=function(_27,_28){
document.getElementById("EmptyPane").style.display="none";
var _29=document.getElementById("PropertiesPane");
var _2a=document.getElementById("PreviewPane");
var _2b=document.getElementById("PreviewObjectHolder");
if(_27){
LoadFlashProperties(_28);
}else{
this.Clear();
}
_2b.innerHTML="";
if(isPreviewerInPreviewMode){
_29.style.display="none";
_2a.style.display="inline";
_2b.innerHTML=this.GetHtml();
}else{
_2a.style.display="none";
_29.style.display="inline";
}
};
function LoadFlashProperties(_2c){
var _2d=_2c.Flash;
var _2e=_2d.tagName.toUpperCase()=="OBJECT";
if(!_2e&&!_2d.tagName.toUpperCase()=="EMBED"){
return;
}
var _2f={};
_2f["loop"]=_2e?getParameterValue(_2d.Flash,"loop"):_2d.getAttribute("loop");
_2f["menu"]=_2e?getParameterValue(_2d,"menu"):_2d.getAttribute("menu");
_2f["quality"]=_2e?getParameterValue(_2d,"quality"):_2d.getAttribute("quality");
_2f["salign"]=_2e?getParameterValue(_2d,"salign"):_2d.getAttribute("salign");
_2f["bgcolor"]=_2e?getParameterValue(_2d,"bgcolor"):_2d.getAttribute("bgcolor");
_2f["wmode"]=_2e?getParameterValue(_2d,"wmode"):_2d.getAttribute("wmode");
_2f["width"]=_2d.width;
_2f["height"]=_2d.height;
_2f["align"]=_2d.align;
if(_2d.tagName.toUpperCase()=="OBJECT"){
document.getElementById("classYes").checked=true;
document.getElementById("classID").disabled=false;
document.getElementById("version").disabled=false;
document.getElementById("classID").value=_2d.classid;
if(_2d.codeBase.match(/(6,0,0,0)$/ig)){
document.getElementById("version").selectedIndex=1;
}
showObject("classIDRow");
}
document.getElementById("flashWidth").value=_2f["width"];
document.getElementById("flashHeight").value=_2f["height"];
selectOption(document.getElementById("htmlAlign"),_2f["align"]);
if(_2f["quality"]){
selectOption(document.getElementById("quality"),_2f["quality"]);
}
if(_2f["play"]){
document.getElementById("playYes").checked=eval(_2f["play"]);
}
if(_2f["loop"]){
document.getElementById("loopYes").checked=eval(_2f["loop"]);
}
if(_2f["menu"]){
document.getElementById("menuYes").checked=eval(_2f["menu"]);
}
if(_2f["wmode"]){
document.getElementById("transparentYes").checked=(_2f["wmode"]=="transparent"?true:false);
}
if(_2f["align"]){
selectOption(document.getElementById("flashAlign"),_2f["align"]);
}
if(_2f["bgcolor"]&&(!selectOption(document.getElementById("backgroundColor"),_2f["bgcolor"]))){
additionalColor(_2f["bgcolor"]);
}
};var SyntaxHighlighter={Brushes:{},Version:"1.3.0"};
var sh=SyntaxHighlighter;
sh.Match=function(_1,_2,_3){
this.value=_1;
this.index=_2;
this.length=_1.length;
this.cssStyle=_3;
};
sh.Highlighter=function(){
this.showLineNumbers=true;
this.tabsToSpaces=true;
};
sh.Highlighter.prototype.GetMatches=function(_4,_5){
var _6=0;
var _7=null;
while((_7=_4.exec(this.code))!=null){
this.matches[this.matches.length]=new sh.Match(_7[0],_7.index,_5);
}
};
sh.Highlighter.prototype.FormatChunk=function(_8,_9){
var _a=document.createElement("font");
_8=_8.replace(/&/g,"&amp;");
_8=_8.replace(/ /g,"&nbsp;");
_8=_8.replace(/</g,"&lt;");
_8=_8.replace(/\n/gm,"&nbsp;<br>");
if(_9!=null){
var _b=new RegExp("<br>","gi");
if(_b.test(_8)){
var _c=_8.split("&nbsp;<br>");
_8="";
for(var i=0;i<_c.length;i++){
this.container.innerHTML+="<font style = \""+_9+"\">"+_c[i]+"</font>";
if(i+1<_c.length){
this.container.appendChild(document.createElement("BR"));
}
}
}else{
_a.style.cssText=_9;
_a.innerHTML=_8;
this.container.appendChild(_a);
}
}else{
_a.style.cssText="font-size: 11px";
_a.innerHTML=_8;
this.container.appendChild(_a);
}
};
sh.Highlighter.prototype.IsInside=function(_e){
if(_e==null||_e.length==0){
return;
}
for(var i=0;i<this.matches.length;i++){
var c=this.matches[i];
if(c==null){
continue;
}
if((_e.index>c.index)&&(_e.index<=c.index+c.length)){
return true;
}
}
return false;
};
sh.Highlighter.prototype.ProcessRegexList=function(){
for(var i=0;i<this.regexList.length;i++){
this.GetMatches(this.regexList[i].regex,this.regexList[i].cssStyle);
}
};
sh.Highlighter.prototype.ProcessSmartTabs=function(_12){
var _13=_12.split("\n");
var _14="";
var _15=4;
var tab="\t";
function InsertSpaces(_17,pos,_19){
var _1a=_17.substr(0,pos);
var _1b=_17.substr(pos+1,_17.length);
var _1c="";
for(var i=0;i<_19;i++){
_1c+=" ";
}
return _1a+_1c+_1b;
}
function ProcessLine(_1e,_1f){
if(_1e.indexOf(tab)==-1){
return _1e;
}
var pos=0;
while((pos=_1e.indexOf(tab))!=-1){
var _21=_1f-pos%_1f;
_1e=InsertSpaces(_1e,pos,_21);
}
return _1e;
}
for(var i=0;i<_13.length;i++){
_14+=ProcessLine(_13[i],_15)+"\n";
}
return _14;
};
sh.Highlighter.prototype.SwitchToTable=function(){
var _23=this.container.innerHTML.replace(/<(br)\/?>/gi,"\n");
var _24=_23.split("\n");
var _25=[];
_25.push("<table cellpadding=0 cellspacing=0 style=\"width: 99%;\tmargin: 2px 0px 2px 0px;border-collapse: collapse;border-bottom: 2px solid #eee;background-color: #fff;\tborder-width:0px;\">");
if(this.showLineNumbers){
_25.push("<col style=\"font-family: Courier New;font-size: 11px;background-color: #eee;padding-right: 5px; padding-left: 10px; width: 5px; border-right: 1px solid gray; color: gray;text-align: right;vertical-align: top;\"/>");
}
_25.push("<col style=\"font-family: Courier New;font-size: 11px;padding-left: 10px;border-bottom: 1px solid #F7F7F7;white-space:nowrap;\"/>");
for(var i=0,_27=1;i<_24.length-1;i++,_27++){
_25.push("<tr>");
if(this.showLineNumbers){
_25.push("<td><nobr>"+_27+"</nobr></td>");
}
var _28="";
if((i%2+1)==2){
_28=" style=\"background-color: #F7F7F7;\"";
}
_25.push("<td "+_28+">"+_24[i]+"</td>");
_25.push("</tr>");
}
_25.push("</table>");
return _25.join("");
};
sh.Highlighter.prototype.Highlight=function(_29){
function Trim(str){
return str.replace(/^\s*(.*?)[\s\n]*$/g,"$1");
}
function Chop(str){
return str.replace(/\n*$/,"").replace(/^\n*/,"");
}
function Unindent(str){
var _2d=str.split("\n");
var _2e=new Array();
var _2f=new RegExp("^\\s*","g");
var min=1000;
for(var i=0;i<_2d.length&&min>0;i++){
if(Trim(_2d[i]).length==0){
continue;
}
var _32=_2f.exec(_2d[i]);
if(_32!=null&&_32.length>0){
min=Math.min(_32[0].length,min);
}
}
if(min>0){
for(var i=0;i<_2d.length;i++){
_2d[i]=_2d[i].substr(min);
}
}
return _2d.join("\n");
}
function Copy(_33,_34,_35){
return _33.substr(_34,_35-_34);
}
var pos=0;
this.originalCode=_29;
this.code=Chop(Unindent(_29));
this.code=_29;
this.container=document.createElement("DIV");
this.matches=new Array();
if(this.tabsToSpaces==true){
this.code=this.ProcessSmartTabs(this.code);
}
this.ProcessRegexList();
if(this.matches.length==0){
this.FormatChunk(this.code,null);
return this.SwitchToTable();
}
function SortCallback(m1,m2){
if(m1.index<m2.index){
return -1;
}else{
if(m1.index>m2.index){
return 1;
}else{
if(m1.length<m2.length){
return -1;
}else{
if(m1.length>m2.length){
return 1;
}
}
}
}
return 0;
}
this.matches=this.matches.sort(SortCallback);
for(var i=0;i<this.matches.length;i++){
if(this.IsInside(this.matches[i])){
this.matches[i]=null;
}
}
for(var i=0;i<this.matches.length;i++){
var _3a=this.matches[i];
if(_3a==null||_3a.length==0){
continue;
}
this.FormatChunk(Copy(this.code,pos,_3a.index),null);
this.FormatChunk(_3a.value,_3a.cssStyle);
pos=_3a.index+_3a.length;
}
this.FormatChunk(this.code.substr(pos),null);
return this.SwitchToTable();
};
sh.Highlighter.prototype.GetKeywords=function(str){
return "\\b"+str.replace(/ /g,"\\b|\\b")+"\\b";
};
sh.HighlightAll=function(_3c,_3d,_3e){
if(!sh.Brushes[_3d]){
return _3c;
}
var _3f=new sh.Brushes[_3d]();
_3f.showLineNumbers=(_3e)?true:false;
return _3f.Highlight(_3c);
};
sh.Brushes.CSharp=function(){
var _40="abstract as base bool break byte case catch char checked class const "+"continue decimal default delegate do double else enum event explicit "+"extern false finally fixed float for foreach get goto if implicit in int "+"interface internal is lock long namespace new null object operator out "+"override params private protected public readonly ref return sbyte sealed set "+"short sizeof stackalloc static string struct switch this throw true try "+"typeof uint ulong unchecked unsafe ushort using virtual void while";
this.regexList=[{regex:new RegExp("//.*$","gm"),cssStyle:"color: green;"},{regex:new RegExp("/\\*[\\s\\S]*?\\*/","g"),cssStyle:"color: green;"},{regex:new RegExp("\"(?:\\.|[^\\\"\"])*\"","g"),cssStyle:"color: blue;"},{regex:new RegExp("^\\s*#.*","gm"),cssStyle:"color: gray;"},{regex:new RegExp(this.GetKeywords(_40),"gm"),cssStyle:"color: blue;"}];
};
sh.Brushes.CSharp.prototype=new sh.Highlighter();
sh.Brushes.CSharp.Aliases=["c#","c-sharp","csharp"];
sh.Brushes.Delphi=function(){
var _41="abs addr and ansichar ansistring array as asm begin boolean byte cardinal "+"case char class comp const constructor currency destructor div do double "+"downto else end except exports extended false file finalization finally "+"for function goto if implementation in inherited int64 initialization "+"integer interface is label library longint longword mod nil not object "+"of on or packed pansichar pansistring pchar pcurrency pdatetime pextended "+"pint64 pointer private procedure program property pshortstring pstring "+"pvariant pwidechar pwidestring protected public published raise real real48 "+"record repeat set shl shortint shortstring shr single smallint string then "+"threadvar to true try type unit until uses val var varirnt while widechar "+"widestring with word write writeln xor";
this.regexList=[{regex:new RegExp("\\(\\*[\\s\\S]*?\\*\\)","gm"),cssStyle:"color: #008200; font-style: italic;"},{regex:new RegExp("{(?!\\$)[\\s\\S]*?}","gm"),cssStyle:"color: #008200; font-style: italic;"},{regex:new RegExp("//.*$","gm"),cssStyle:"color: #008200; font-style: italic;"},{regex:new RegExp("'(?:\\.|[^\\''])*'","g"),cssStyle:"color: blue;"},{regex:new RegExp("\\{\\$[a-zA-Z]+ .+\\}","g"),cssStyle:"color: #008284;"},{regex:new RegExp("\\b[\\d\\.]+\\b","g"),cssStyle:"color: blue;"},{regex:new RegExp("\\$[a-zA-Z0-9]+\\b","g"),cssStyle:"color: blue;"},{regex:new RegExp(this.GetKeywords(_41),"gm"),cssStyle:"font-weight: bold; color: navy;"}];
};
sh.Brushes.Delphi.prototype=new sh.Highlighter();
sh.Brushes.Delphi.Aliases=["delphi","pascal"];
sh.Brushes.JScript=function(){
var _42="abstract boolean break byte case catch char class const continue debugger innerHTML className document window getElementById"+"default delete do double else enum export extends false final finally float "+"for function goto if implements import in instanceof int interface long native "+"new null package private protected public return short static super switch "+"synchronized this throw throws transient true try typeof var void volatile while with";
this.regexList=[{regex:new RegExp("//.*$","gm"),cssStyle:"color: green;"},{regex:new RegExp("/\\*[\\s\\S]*?\\*/","g"),cssStyle:"color: green;"},{regex:new RegExp("\"(?:[^\"\n]|[\"])*?\"","g"),cssStyle:"color: blue;"},{regex:new RegExp("'(?:[^'\n]|['])*?'","g"),cssStyle:"color: blue;"},{regex:new RegExp("^\\s*#.*","gm"),cssStyle:"color: gray;"},{regex:new RegExp(this.GetKeywords(_42),"gm"),cssStyle:"color: blue;"}];
};
sh.Brushes.JScript.prototype=new sh.Highlighter();
sh.Brushes.JScript.Aliases=["js","jscript","javascript"];
sh.Brushes.CSS=function(){
var _43="ascent azimuth background-attachment background-color background-image background-position "+"background-repeat background baseline bbox border-collapse border-color border-spacing border-style border-top "+"border-right border-bottom border-left border-top-color border-right-color border-bottom-color border-left-color "+"border-top-style border-right-style border-bottom-style border-left-style border-top-width border-right-width "+"border-bottom-width border-left-width border-width border bottom cap-height caption-side centerline clear clip color "+"content counter-increment counter-reset cue-after cue-before cue cursor definition-src descent direction display "+"elevation empty-cells float font-size-adjust font-family font-size font-stretch font-style font-variant font-weight font "+"height letter-spacing line-height list-style-image list-style-position list-style-type list-style margin-top "+"margin-right margin-bottom margin-left margin marker-offset marks mathline max-height max-width min-height min-width orphans "+"outline-color outline-style outline-width outline overflow padding-top padding-right padding-bottom padding-left padding page "+"page-break-after page-break-before page-break-inside pause pause-after pause-before pitch pitch-range play-during position "+"quotes richness right size slope src speak-header speak-numeral speak-punctuation speak speech-rate stemh stemv stress "+"table-layout text-align text-decoration text-indent text-shadow text-transform unicode-bidi unicode-range units-per-em "+"vertical-align visibility voice-family volume white-space widows width widths word-spacing x-height z-index";
var _44="above absolute all always aqua armenian attr aural auto avoid baseline behind below bidi-override black blink block blue bold bolder "+"both bottom braille capitalize caption center center-left center-right circle close-quote code collapse compact condensed "+"continuous counter counters crop cross crosshair cursive dashed decimal decimal-leading-zero default digits disc dotted double "+"embed embossed e-resize expanded extra-condensed extra-expanded fantasy far-left far-right fast faster fixed format fuchsia "+"gray green groove handheld hebrew help hidden hide high higher icon inline-table inline inset inside invert italic "+"justify landscape large larger left-side left leftwards level lighter lime line-through list-item local loud lower-alpha "+"lowercase lower-greek lower-latin lower-roman lower low ltr marker maroon medium message-box middle mix move narrower "+"navy ne-resize no-close-quote none no-open-quote no-repeat normal nowrap n-resize nw-resize oblique olive once open-quote outset "+"outside overline pointer portrait pre print projection purple red relative repeat repeat-x repeat-y rgb ridge right right-side "+"rightwards rtl run-in screen scroll semi-condensed semi-expanded separate se-resize show silent silver slower slow "+"small small-caps small-caption smaller soft solid speech spell-out square s-resize static status-bar sub super sw-resize "+"table-caption table-cell table-column table-column-group table-footer-group table-header-group table-row table-row-group teal "+"text-bottom text-top thick thin top transparent tty tv ultra-condensed ultra-expanded underline upper-alpha uppercase upper-latin "+"upper-roman url visible wait white wider w-resize x-fast x-high x-large x-loud x-low x-slow x-small x-soft xx-large xx-small yellow";
var _45="[mM]onospace [tT]ahoma [vV]erdana [aA]rial [hH]elvetica [sS]ans-serif [sS]erif";
this.regexList=[{regex:new RegExp("/\\*[\\s\\S]*?\\*/","g"),cssStyle:"color: green;"},{regex:new RegExp("\"(?:[^\"\n]|[\"])*?\"","g"),cssStyle:"color: blue;"},{regex:new RegExp("'(?:[^'\n]|['])*?'","g"),cssStyle:"color: blue;"},{regex:new RegExp("\\#[a-zA-Z0-9]{3,6}","g"),cssStyle:"color: blue;"},{regex:new RegExp("(\\d+)(px|pt|:)","g"),cssStyle:"color: blue;"},{regex:new RegExp(this.GetKeywords(_43),"gm"),cssStyle:"color: red;"},{regex:new RegExp(this.GetKeywords(_44),"g"),cssStyle:"color: blue;"},{regex:new RegExp(this.GetKeywords(_45),"g"),cssStyle:"color: blue;"}];
};
sh.Brushes.CSS.prototype=new sh.Highlighter();
sh.Brushes.CSS.Aliases=["css"];
sh.Brushes.Php=function(){
var _46="and or xor __FILE__ __LINE__ array as break case "+"cfunction class const continue declare default die do echo else "+"elseif empty enddeclare endfor endforeach endif endswitch endwhile eval exit "+"extends for foreach function global if include include_once isset list "+"new old_function print require require_once return static switch unset use "+"var while __FUNCTION__ __CLASS__";
this.regexList=[{regex:new RegExp("//.*$","gm"),cssStyle:"color: green;"},{regex:new RegExp("/\\*[\\s\\S]*?\\*/","g"),cssStyle:"color: green;"},{regex:new RegExp("\"(?:[^\"\n]|[\"])*?\"","g"),cssStyle:"color: blue;"},{regex:new RegExp("'(?:[^'\n]|['])*?'","g"),cssStyle:"color: blue;"},{regex:new RegExp("\\$\\w+","g"),cssStyle:"color: #d00;"},{regex:new RegExp(this.GetKeywords(_46),"gm"),cssStyle:"color: blue;"}];
};
sh.Brushes.Php.prototype=new sh.Highlighter();
sh.Brushes.Php.Aliases=["php"];
sh.Brushes.Python=function(){
var _47="and assert break class continue def del elif else except exec "+"finally for from global if import in is lambda not or object pass print "+"raise return try yield while";
var _48="self __builtin__ __dict__ __future__ __methods__ __members__ __author__ __email__ __version__"+"__class__ __bases__ __import__ __main__ __name__ __doc__ __self__ __debug__ __slots__ "+"abs append apply basestring bool buffer callable chr classmethod clear close cmp coerce compile complex "+"conjugate copy count delattr dict dir divmod enumerate Ellipsis eval execfile extend False file fileno filter float flush "+"get getattr globals has_key hasarttr hash hex id index input insert int intern isatty isinstance isubclass "+"items iter keys len list locals long map max min mode oct open ord pop pow property range "+"raw_input read readline readlines reduce reload remove repr reverse round seek setattr slice sum "+"staticmethod str super tell True truncate tuple type unichr unicode update values write writelines xrange zip";
var _49="__abs__ __add__ __and__ __call__ __cmp__ __coerce__ __complex__ __concat__ __contains__ __del__ __delattr__ __delitem__ "+"__delslice__ __div__ __divmod__ __float__ __getattr__ __getitem__ __getslice__ __hash__ __hex__ __eq__ __le__ __lt__ __gt__ __ge__ "+"__iadd__ __isub__ __imod__ __idiv__ __ipow__ __iand__ __ior__ __ixor__ __ilshift__ __irshift__ "+"__invert__ __init__ __int__ __inv__ __iter__ __len__ __long__ __lshift__ __mod__ __mul__ __new__ __neg__ __nonzero__ __oct__ __or__ "+"__pos__ __pow__ __radd__ __rand__ __rcmp__ __rdiv__ __rdivmod__ __repeat__ __repr__ __rlshift__ __rmod__ __rmul__ "+"__ror__ __rpow__ __rrshift__ __rshift__ __rsub__ __rxor__ __setattr__ __setitem__ __setslice__ __str__ __sub__ __xor__";
var _4a="Exception StandardError ArithmeticError LookupError EnvironmentError AssertionError AttributeError EOFError "+"FutureWarning IndentationError OverflowWarning PendingDeprecationWarning ReferenceError RuntimeWarning "+"SyntaxWarning TabError UnicodeDecodeError UnicodeEncodeError UnicodeTranslateError UserWarning Warning "+"IOError ImportError IndexError KeyError KeyboardInterrupt MemoryError NameError NotImplementedError OSError "+"RuntimeError StopIteration SyntaxError SystemError SystemExit TypeError UnboundLocalError UnicodeError ValueError "+"FloatingPointError OverflowError WindowsError ZeroDivisionError";
var _4b="NoneType TypeType IntType LongType FloatType ComplexType StringType UnicodeType BufferType TupleType ListType "+"DictType FunctionType LambdaType CodeType ClassType UnboundMethodType InstanceType MethodType BuiltinFunctionType BuiltinMethodType "+"ModuleType FileType XRangeType TracebackType FrameType SliceType EllipsisType";
var _4c="anydbm array asynchat asyncore AST base64 binascii binhex bisect bsddb buildtools bz2 "+"BaseHTTPServer Bastion calendar cgi cmath cmd codecs codeop commands compiler copy copy_reg "+"cPickle crypt cStringIO csv curses Carbon CGIHTTPServer ConfigParser Cookie datetime dbhash "+"dbm difflib dircache distutils doctest DocXMLRPCServer email encodings errno exceptions fcntl "+"filecmp fileinput ftplib gc gdbm getopt getpass glob gopherlib gzip heapq htmlentitydefs "+"htmllib httplib HTMLParser imageop imaplib imgfile imghdr imp inspect itertools jpeg keyword "+"linecache locale logging mailbox mailcap marshal math md5 mhlib mimetools mimetypes mimify mmap "+"mpz multifile mutex MimeWriter netrc new nis nntplib nsremote operator optparse os parser pickle pipes "+"popen2 poplib posix posixfile pprint preferences profile pstats pwd pydoc pythonprefs quietconsole "+"quopri Queue random re readline resource rexec rfc822 rgbimg sched select sets sgmllib sha shelve shutil "+"signal site smtplib socket stat statcache string struct symbol sys syslog SimpleHTTPServer "+"SimpleXMLRPCServer SocketServer StringIO tabnanny tarfile telnetlib tempfile termios textwrap "+"thread threading time timeit token tokenize traceback tty types Tkinter unicodedata unittest "+"urllib urllib2 urlparse user UserDict UserList UserString warnings weakref webbrowser whichdb "+"xml xmllib xmlrpclib xreadlines zipfile zlib";
this.regexList=[{regex:new RegExp("#.*$","gm"),cssStyle:"color: green;"},{regex:new RegExp("^\\s*\"\"\"(.|\n)*?\"\"\"\\s*$","gm"),cssStyle:"color: brown;"},{regex:new RegExp("^\\s*'''(.|\n)*?'''\\s*$","gm"),cssStyle:"color: brown;"},{regex:new RegExp("\"\"\"(.|\n)*?\"\"\"","g"),cssStyle:"color: red;"},{regex:new RegExp("'''(.|\n)*?'''","g"),cssStyle:"color: red;"},{regex:new RegExp("\"(?:\\.|[^\\\"\"])*\"","g"),cssStyle:"color: red;"},{regex:new RegExp("'(?:\\.|[^\\''])*'","g"),cssStyle:"color: red;"},{regex:new RegExp(this.GetKeywords(_47),"gm"),cssStyle:"color: blue; font-weight: bold;"},{regex:new RegExp(this.GetKeywords(_48),"gm"),cssStyle:"color: #ff1493;"},{regex:new RegExp(this.GetKeywords(_49),"gm"),cssStyle:"color: #808080;"},{regex:new RegExp(this.GetKeywords(_4a),"gm"),cssStyle:"color: brown;"},{regex:new RegExp(this.GetKeywords(_4b),"gm"),cssStyle:"color: brown; font-style: italic;"},{regex:new RegExp(this.GetKeywords(_4c),"gm"),cssStyle:"color: #8A2BE2; font-style: italic;"}];
};
sh.Brushes.Python.prototype=new sh.Highlighter();
sh.Brushes.Python.Aliases=["py","python"];
sh.Brushes.Sql=function(){
var _4d="abs avg case cast coalesce convert count current_timestamp "+"current_user day isnull left lower month nullif replace right "+"session_user space substring sum system_user upper user year";
var _4e="absolute action add after alter as asc at authorization begin bigint "+"binary bit by cascade char character check checkpoint close collate "+"column commit committed connect connection constraint contains continue "+"create cube current current_date current_time cursor database date "+"deallocate dec decimal declare default delete desc distinct double drop "+"dynamic else end end-exec escape except exec execute false fetch first "+"float for force foreign forward free from full function global goto grant "+"group grouping having hour ignore index inner insensitive insert instead "+"int integer intersect into is isolation key last level load local max min "+"minute modify move name national nchar next no numeric of off on only "+"open option order out output partial password precision prepare primary "+"prior privileges procedure public read real references relative repeatable "+"restrict return returns revoke rollback rollup rows rule schema scroll "+"second section select sequence serializable set size smallint static "+"statistics table temp temporary then time timestamp to top transaction "+"translation trigger true truncate uncommitted union unique update values "+"varchar varying view when where with work";
var _4f="all and any between cross in join like not null or outer some";
this.regexList=[{regex:new RegExp("--(.*)$","gm"),cssStyle:"color: green;"},{regex:new RegExp("\"(?:\\.|[^\\\"\"])*\"","g"),cssStyle:"color: red;"},{regex:new RegExp("'(?:\\.|[^\\''])*'","g"),cssStyle:"color: red;"},{regex:new RegExp(this.GetKeywords(_4d),"gmi"),cssStyle:"color: #ff1493;"},{regex:new RegExp(this.GetKeywords(_4f),"gmi"),cssStyle:"color: #808080;"},{regex:new RegExp(this.GetKeywords(_4e),"gmi"),cssStyle:"color: blue; "}];
this.CssClass="dp-sql";
};
sh.Brushes.Sql.prototype=new sh.Highlighter();
sh.Brushes.Sql.Aliases=["sql"];
sh.Brushes.Vb=function(){
var _50="AddHandler AddressOf AndAlso Alias And Ansi As Assembly Auto "+"Boolean ByRef Byte ByVal Call Case Catch CBool CByte CChar CDate "+"CDec CDbl Char CInt Class CLng CObj Const CShort CSng CStr CType "+"Date Decimal Declare Default Delegate Dim DirectCast Do Double Each "+"Else ElseIf End Enum Erase Error Event Exit False Finally For Friend "+"Function Get GetType GoSub GoTo Handles If Implements Imports In "+"Inherits Integer Interface Is Let Lib Like Long Loop Me Mod Module "+"MustInherit MustOverride MyBase MyClass Namespace New Next Not Nothing "+"NotInheritable NotOverridable Object On Option Optional Or OrElse "+"Overloads Overridable Overrides ParamArray Preserve Private Property "+"Protected Public RaiseEvent ReadOnly ReDim REM RemoveHandler Resume "+"Return Select Set Shadows Shared Short Single Static Step Stop String "+"Structure Sub SyncLock Then Throw To True Try TypeOf Unicode Until "+"Variant When While With WithEvents WriteOnly Xor";
this.regexList=[{regex:new RegExp("'.*$","gm"),cssStyle:"color: green;"},{regex:new RegExp("\"(?:\\.|[^\\\"\"])*\"","g"),cssStyle:"color: blue;"},{regex:new RegExp("^\\s*#.*","gm"),cssStyle:"color: gray;"},{regex:new RegExp(this.GetKeywords(_50),"gm"),cssStyle:"color: blue;"}];
};
sh.Brushes.Vb.prototype=new sh.Highlighter();
sh.Brushes.Vb.Aliases=["vb","vb.net"];
sh.Brushes.Xml=function(){
};
sh.Brushes.Xml.prototype=new sh.Highlighter();
sh.Brushes.Xml.Aliases=["xml","xhtml","xslt","html","xhtml"];
sh.Brushes.Xml.prototype.ProcessRegexList=function(){
function push(_51,_52){
_51[_51.length]=_52;
}
var _53=0;
var _54=null;
var _55=null;
this.GetMatches(new RegExp("<\\!\\[[\\w\\s]*?\\[(.|\\s)*?\\]\\]>","gm"),"color: #ff1493;");
this.GetMatches(new RegExp("<!--\\s*.*\\s*?-->","gm"),"color: green;");
_55=new RegExp("([\\w-.]+)\\s*=\\s*(\".*?\"|'.*?'|\\w+)*","gm");
while((_54=_55.exec(this.code))!=null){
push(this.matches,new sh.Match(_54[1],_54.index,"color: red;"));
if(_54[2]!=undefined){
push(this.matches,new sh.Match(_54[2],_54.index+_54[0].indexOf(_54[2]),"color: blue;"));
}
}
this.GetMatches(new RegExp("</*\\?*(?!\\!)|/*\\?*>","gm"),"color: blue;");
_55=new RegExp("</*\\?*\\s*([\\w-.]+)","gm");
while((_54=_55.exec(this.code))!=null){
push(this.matches,new sh.Match(_54[1],_54.index+_54[0].indexOf(_54[1]),"color: black; font-weight: bold;"));
}
};;function ImageDialogCaller(id,_2){
this.Id=id;
this.AdditionalArguments=_2;
if(this.AdditionalArguments.length>0&&this.AdditionalArguments.indexOf("&")!=0){
this.AdditionalArguments="&"+this.AdditionalArguments;
}
this.resultHolderTextBox=document.getElementById(this.Id+"_resultTextBox");
this.dialogOpenerButton=document.getElementById(this.Id+"_dialogOpenerButton");
this.EditorObject=null;
this.onSrcChangeCallback=null;
}
ImageDialogCaller.prototype.Initialize=function(_3,_4,_5){
this.EditorObject=_3;
if(_4){
this.resultHolderTextBox.size=_4;
}
if(_5){
this.onSrcChangeCallback=_5;
}
};
ImageDialogCaller.prototype.SetImagePath=function(_6){
this.resultHolderTextBox.value=_6;
this.resultHolderTextBox.focus();
};
ImageDialogCaller.prototype.GetImagePath=function(){
return this.resultHolderTextBox.value;
};
ImageDialogCaller.prototype.CallImageDialog=function(){
var _7={imageDialogCaller:this};
var _8="";
if(this.EditorObject.UseSession==1){
var _9=this.EditorObject.RadControlsDir.substr(this.EditorObject.ApplicationPath.length);
_8=this.EditorObject.ApplicationPath+this.EditorObject.SessionID1+_9;
}else{
_8=this.EditorObject.RadControlsDir;
}
var _a={InternalParameters:this.EditorObject.GetDialogInternalParameters("ImageManager")};
if(RadEditorNamespace.IMAGE_MANAGER_DIALOG_NAME){
}else{
RadEditorNamespace.IMAGE_MANAGER_DIALOG_NAME="ImageManager";
}
this.EditorObject.ShowDialog(this.EditorObject.GetDialogUrl(RadEditorNamespace.IMAGE_MANAGER_DIALOG_NAME)+this.AdditionalArguments,_a,400,300,SetImageCallerValue,_7);
return false;
};
function SetImageCallerValue(_b,_c){
if(_b&&_b.imagePath){
_c.imageDialogCaller.SetImagePath(_b.imagePath);
}
document.body.focus();
if(_c.imageDialogCaller.onSrcChangeCallback!=null){
_c.imageDialogCaller.onSrcChangeCallback();
}
return false;
};function ImageInfoControl(){
}
ImageInfoControl.prototype.Initialize=function(_1){
this.OriginalImage=_1;
document.getElementById("radEditorOriginalWidth").innerHTML="&nbsp;"+this.OriginalImage.width;
document.getElementById("radEditorOriginalHeight").innerHTML="&nbsp;"+this.OriginalImage.height;
var _2=this.OriginalImage.src.substr(this.OriginalImage.src.lastIndexOf("/")+1);
if(_2.length>30){
_2="..."+_2.substr((_2.length-27),27);
}
document.getElementById("radEditorImageSrc").innerHTML="&nbsp;"+_2;
document.getElementById("radEditorImageSize").innerHTML="&nbsp;"+((this.OriginalImage.fileSize!=null)?(this.OriginalImage.fileSize+"&nbsp;"+localization["Bytes"]):"NA");
};;var Rect_Fields=["rect_x","rect_y","rect_width","rect_height"];
function ShowRectPropsDlg(_1){
var _2=document.forms[0];
for(var i=0;i<Rect_Fields.length;i++){
_2[Rect_Fields[i]].disabled=false;
}
_2["rect_x"].value=_1.x;
_2["rect_y"].value=_1.y;
_2["rect_width"].value=_1.width;
if(CurrentArea.Shape.Type==AREA_SHAPE_CONSTANTS.RECTANGLE_TYPE){
_2["rect_height"].value=_1.height;
}else{
if(CurrentArea.Shape.Type==AREA_SHAPE_CONSTANTS.CIRCLE_TYPE){
_2["rect_height"].value=_1.width;
_2["rect_height"].disabled=true;
}
}
}
function CloseRectPropsDlg(){
var _4=document.forms[0];
for(var i=0;i<Rect_Fields.length;i++){
_4[Rect_Fields[i]].value="";
_4[Rect_Fields[i]].disabled=true;
}
}
function UpdateRectPropsDlg(){
var _6=document.forms[0];
var _7={x:_6["rect_x"].value,y:_6["rect_y"].value,width:_6["rect_width"].value,height:_6["rect_height"].value};
if(!UpdateRectProps(_7)){
alert(localization.getText("AlertInvalidProperties"));
ShowRectProps();
return false;
}
return true;
}
var Area_Fields=["area_link","area_target","area_comment","area_target_selector"];
var Area_Buttons=["area_update_button","area_remove_button"];
function ShowAreaPropsDlg(_8){
var _9=document.forms[0];
for(var i=0;i<Area_Fields.length;i++){
_9[Area_Fields[i]].disabled=false;
}
for(var i=0;i<Area_Buttons.length;i++){
_9[Area_Buttons[i]].disabled=false;
}
if(_8.Link){
_9["area_link"].value=_8.Link;
}else{
_9["area_link"].value="http://";
}
_9["area_target"].value=_8.Target;
_9["area_target_selector"].value=_8.Target;
_9["area_comment"].value=_8.Comment;
ShowMapProps();
}
function UpdateAreaPropsDlg(){
var _b=document.forms[0];
if(_b["area_update_button"].disabled){
return true;
}
if(!UpdateRectPropsDlg()){
return false;
}
var _c={Link:_b["area_link"].value,Comment:_b["area_comment"].value,Target:_b["area_target"].value};
UpdateAreaProps(_c);
return true;
}
function CloseAreaPropsDlg(){
var _d=document.forms[0];
for(var i=0;i<Area_Fields.length;i++){
_d[Area_Fields[i]].value="";
_d[Area_Fields[i]].disabled=true;
}
for(var i=0;i<Area_Buttons.length;i++){
_d[Area_Buttons[i]].disabled=true;
}
CloseRectPropsDlg();
DeselectCurrentArea();
}
function RemoveAreaDlg(_f){
if(_f){
if(!window.confirm(localization.getText("AlertDeleteAllAreas"))){
return;
}
RemoveAllAreas();
}else{
if(!window.confirm(localization.getText("AlertDeleteArea"))){
return;
}
RemoveArea();
}
}
function ReturnNewImageMap(){
var _10=GetMapImageSrc();
if(_10){
if(UpdateAreaPropsDlg()){
var _11={};
_11.MapHtml=GetImageMapHTML();
_11.ImageSrc=_10;
CloseDlg(_11);
}
}else{
CloseDlg();
}
}
function GetMapImageSrc(){
var _12=document.getElementById("ImageMapFrame");
var _13=_12.contentWindow.document;
var _14=_13.getElementById("mappedImage");
if(_14!=null){
return _14.src;
}
return null;
}
function GetImageMapInitialProperties(){
return GetDialogArguments();
}
function InsertNewMapAreaDlg(){
InsertNewMapArea();
}
function InitImageMapFrame(){
var _15=document.getElementById("ImageMapHTML");
var _16=document.getElementById("ImageMapFrame");
var _17=_16.contentWindow.document;
_17.open();
_17.write("<html><head></head><body style = \"margin:0px 0px 0px 0px;font:11px Arial; color:red;background-color:white;\" oncontextmenu=\"return false\" unselectable=\"on\">"+_15.value+"</body></html>");
_17.close();
};function ImageMap(_1){
this.AreaCollection=[];
this.AreaIDCnt=0;
this.Name="ImageMap1";
this.Image=_1;
}
ImageMap.prototype.Initialize=function(_2){
if(_2){
if(!this._InitFromHTMLString(_2)){
return false;
}
}
return true;
};
ImageMap.prototype._InitFromHTMLString=function(_3){
var _4=IMDoc.createElement("SPAN");
_4.innerHTML=_3;
var _5=_4.getElementsByTagName("MAP");
if(_5.length!=1){
return false;
}
var _6=_5[0];
this.Name=_6.getAttribute("name");
var _7=_6.getElementsByTagName("area");
for(var i=0;i<_7.length;i++){
var _9=new MapArea(this.Image);
_9.ID=this.AreaIDCnt;
if(!_9.Initialize(_7[i])){
continue;
}
if(this.AddArea(_9)){
_9.Draw();
}
}
_4=null;
return true;
};
ImageMap.prototype.AddArea=function(_a){
if(!this.IsAreaValid(_a)){
return false;
}
this.AreaCollection.push(_a);
this.AreaIDCnt++;
return true;
};
ImageMap.prototype.AddNewArea=function(_b,x,y,_e,_f){
var _10=new MapArea(this.Image);
_10.ID=this.AreaIDCnt;
if(!_10.CreateDefaultShape(_b,x,y,_e,_f)){
return null;
}
this.AddArea(_10);
_10.Draw();
return _10;
};
ImageMap.prototype.GetAreaById=function(_11){
return this.AreaCollection[_11];
};
ImageMap.prototype.RemoveArea=function(_12){
var _13=this.GetAreaById(_12);
if(_13!=null){
_13.Dispose();
}
this.AreaCollection[_12]=null;
};
ImageMap.prototype.IsAreaOverlaps=function(_14){
return false;
};
ImageMap.prototype.IsAreaValid=function(_15){
return !this.IsAreaOverlaps(_15);
};
ImageMap.prototype.GetHTML=function(){
var _16="";
_16+="<map";
_16+=" name=\"#"+this.Name.replace("\"","&#34;")+"\"";
_16+=">";
for(var i=0;i<this.AreaCollection.length;i++){
if(this.AreaCollection[i]!=null){
_16+=this.AreaCollection[i].GetHTML();
}
}
_16+="</map>";
return _16;
};
ImageMap.prototype.GetAreasNumber=function(){
var nr=0;
for(var i=0;i<this.AreaCollection.length;i++){
if(this.AreaCollection[i]!=null){
nr++;
}
}
return nr;
};
function MapArea(_1a){
this.Comment="";
this.Link="";
this.Target="";
this.Image=_1a;
}
MapArea.prototype.GetHTML=function(){
var _1b="";
_1b+="<area";
_1b+=" shape=\""+this.Shape.Type+"\"";
_1b+=" coords=\""+this.Shape.GetCoords()+"\"";
if(this.Link.indexOf("about:blank")==0){
this.Link=this.Link.substr("about:blank".length);
}else{
if(this.Link.indexOf("about:")==0){
this.Link=this.Link.substr("about:".length);
}
}
_1b+=" href=\""+this.Link.replace("\"","&#34;")+"\"";
if(this.Target){
_1b+=" target=\""+this.Target.replace("\"","&#34;")+"\"";
}
if(this.Comment){
_1b+=" alt=\""+this.Comment.replace("\"","&#34;")+"\"";
}
_1b+="/>";
return _1b;
};
MapArea.prototype.Initialize=function(_1c){
var _1d=_1c.getAttribute("shape");
if(!_1d){
_1d=AREA_SHAPE_CONSTANTS.DEFAULT_SHAPE_TYPE;
}
this.Shape=CreateAreaShapeByType(_1d);
this.Shape.SetImage(this.Image);
this.Shape.SetAreaId(this.ID);
var _1e=_1c.getAttribute("coords");
if(!this.Shape.AreCoordsValid(_1e)){
return false;
}
this.Shape.SetCoords(_1e);
this.Comment=_1c.getAttribute("alt");
this.Link=_1c.getAttribute("href");
if(this.Link.indexOf("about:blank")==0){
this.Link=this.Link.substr("about:blank".length);
}else{
if(this.Link.indexOf("about:")==0){
this.Link=this.Link.substr("about:".length);
}
}
this.Target=_1c.getAttribute("target");
return true;
};
MapArea.prototype.Draw=function(){
this.Shape.Draw();
};
MapArea.prototype.Move=function(_1f){
var _20=this.Shape.GetShapeProperties();
if(_1f.x){
_20.x+=_1f.x;
}
if(_1f.y){
_20.y+=_1f.y;
}
var _21=this.Shape.ShapePropsToCoords(_20);
if(!this.Shape.AreCoordsValid(_21)){
return false;
}
this.Shape.SetCoords(_21);
this.Shape.Draw();
return true;
};
MapArea.prototype.SetSize=function(_22){
var _23=this.Shape.GetShapeProperties();
if(_22.width){
_23.width=_22.width;
}
if(_22.height){
_23.height=_22.height;
}
var _24=this.Shape.ShapePropsToCoords(_23);
if(!this.Shape.AreCoordsValid(_24)){
return false;
}
this.Shape.SetCoords(_24);
this.Shape.Resize(_22);
return true;
};
MapArea.prototype.CreateDefaultShape=function(_25,x,y,_28,_29){
this.Shape=CreateAreaShapeByType(_25);
this.Shape.SetImage(this.Image);
this.Shape.SetAreaId(this.ID);
var _2a=this.Shape.ShapePropsToCoords({x:x,y:y,width:_28,height:_29});
if(!this.Shape.AreCoordsValid(_2a)){
return null;
}
return this.Shape.SetCoords(_2a);
};
MapArea.prototype.GetProperties=function(){
return {Comment:this.Comment,Link:this.Link,Target:this.Target};
};
MapArea.prototype.SetProperties=function(_2b){
this.Comment=_2b.Comment;
this.Link=_2b.Link;
this.Target=_2b.Target;
};
MapArea.prototype.SetSelected=function(){
this.Shape.SetSelected();
};
MapArea.prototype.DeSelect=function(){
this.Shape.DeSelect();
};
MapArea.prototype.Dispose=function(){
this.Shape.Dispose();
this.Image=null;
};
var AREA_SHAPE_CONSTANTS={RECTANGLE_TYPE:"RECT",CIRCLE_TYPE:"CIRCLE",POLYGON_TYPE:"POLY"};
AREA_SHAPE_CONSTANTS.DEFAULT_SHAPE_TYPE=AREA_SHAPE_CONSTANTS.RECTANGLE_TYPE;
function AreaShape(){
this.Coords="";
this.Type="";
this.ResizeElementClassName="circ_shape_resizer";
}
AreaShape.prototype.GetType=function(){
return this.Type;
};
AreaShape.prototype.GetCoords=function(){
return this.Coords;
};
AreaShape.prototype.AreCoordsValid=function(_2c){
return false;
};
AreaShape.prototype.SetCoords=function(_2d){
if(!this.AreCoordsValid(_2d)){
return false;
}
this.Coords=_2d;
return true;
};
AreaShape.prototype.SetImage=function(_2e){
this.Image=_2e;
};
AreaShape.prototype.Resize=function(_2f){
};
AreaShape.prototype.SetAreaId=function(_30){
this.AreaID=_30;
};
AreaShape.prototype.CreateResizerElement=function(){
var _31=IMDoc.createElement("DIV");
_31.id="res_"+this.AreaID;
_31.className=this.ResizeElementClassName;
_31.style.width=5;
_31.style.height=5;
_31.style.position="absolute";
_31.style.display="none";
return IMDoc.body.appendChild(_31);
};
AreaShape.prototype.GetShapeProperties=function(){
return this.ShapeCoordsToProps(this.Coords);
};
AreaShape.prototype.SetSelected=function(){
};
AreaShape.prototype.DeSelect=function(){
};
AreaShape.prototype.Draw=function(){
};
AreaShape.prototype.OnSelect=function(){
};
AreaShape.prototype.Dispose=function(){
};
function CreateAreaShapeByType(_32){
var _33={};
_33[AREA_SHAPE_CONSTANTS.RECTANGLE_TYPE]=RectangleShape;
_33[AREA_SHAPE_CONSTANTS.POLYGON_TYPE]=PolygonShape;
_33[AREA_SHAPE_CONSTANTS.CIRCLE_TYPE]=CircleShape;
if(!_33[_32]){
_32=AREA_SHAPE_CONSTANTS.DEFAULT_SHAPE_TYPE;
}
if(_32==AREA_SHAPE_CONSTANTS.CIRCLE_TYPE&&!IsCircleShapeSupported()){
return null;
}
return new _33[_32]();
}
function RectangleShape(){
this.Type=AREA_SHAPE_CONSTANTS.RECTANGLE_TYPE;
this.VisualFillElement=null;
this.VisualBorderElement=null;
this.ResizeElement=null;
this.ResizeElementClassName="rect_shape_resizer";
this.SelectedClassName="rect_shape_selected";
this.NotSelectedClassName="rect_shape_not_selected";
this.BorderElementClass="rect_shape_border";
}
RectangleShape.prototype=new AreaShape();
RectangleShape.prototype.ContainerTitle="";
RectangleShape.prototype.Resize=function(_34){
if(_34.width){
var _35=Math.abs(_34.width);
this.VisualFillElement.style.width=_35-1;
this.VisualBorderElement.style.width=_35;
}
if(_34.height){
var _36=Math.abs(_34.height);
this.VisualFillElement.style.height=_36-1;
this.VisualBorderElement.style.height=_36;
}
this.SetResizeElementPosition();
};
RectangleShape.prototype.Draw=function(){
var _37=RadEditorNamespace.Utils.GetRect(this.Image);
if(this.VisualBorderElement){
IMDoc.body.removeChild(this.VisualBorderElement);
}
if(this.ResizeElement){
IMDoc.body.removeChild(this.ResizeElement);
}
var _38=IMDoc.createElement("DIV");
var _39=IMDoc.createElement("DIV");
_38.style.position="absolute";
_38.className=this.BorderElementClass;
_38.title=this.ContainerTitle;
var _3a=this.GetShapeProperties();
_38.style.top=_37.top+_3a.y;
_38.style.left=_37.left+_3a.x;
_38.style.width=_3a.width;
_38.style.height=_3a.height;
_39.className=this.NotSelectedClassName;
_39.style.width=_3a.width-1;
_39.style.height=_3a.height-1;
_38.id="rect_"+this.AreaID;
_38.ShapeObj=this;
_38.onclick=this.HandleClick;
_38.appendChild(_39);
IMDoc.body.appendChild(_38);
var _3b=this.CreateResizerElement();
this.VisualFillElement=_39;
this.VisualBorderElement=_38;
this.ResizeElement=_3b;
this.SetResizeElementPosition();
var _3c=this;
var _3d=function(){
var _3e=RadEditorNamespace.Utils.GetRect(_3c.VisualBorderElement);
var _3f=RadEditorNamespace.Utils.GetRect(_3c.Image);
_3c.SetResizeElementPosition();
var _40=_3c.ShapePropsToCoords({x:_3e.left-_3f.left,y:_3e.top-_3f.top,width:_3e.width,height:_3e.height});
if(_3c.AreCoordsValid(_40)){
_3c.SetCoords(_40);
}else{
_3c.Draw();
}
_3c.OnSelect();
};
var _41=function(){
_3c.SetResizeElementPosition();
};
var _42=function(fff,x,y){
_38.style.cursor="SE-resize";
var _46=RadEditorNamespace.Utils.GetRect(_38);
var _47=_46.left+_46.width+parseInt(_3c.ResizeElement.style.width);
var _48=_46.top+_46.height+parseInt(_3c.ResizeElement.style.height);
var _49=(_46.height+y-_48);
var _4a=(_46.width+x-_47);
var _4b={width:_4a,height:_49};
_3c.Resize(_4b);
};
var _4c=function(){
_38.style.cursor="move";
_3d();
};
var _t=new Draggable(_38,_38,null,_41,_3d);
var _t2=new Draggable(_3b,_3b,null,_42,_4c);
};
RectangleShape.prototype.SetResizeElementPosition=function(){
var _4f=this.VisualBorderElement.style;
this.ResizeElement.style.top=parseInt(_4f.top)+parseInt(_4f.height);
this.ResizeElement.style.left=parseInt(_4f.left)+parseInt(_4f.width);
this.ResizeElement.style.display="block";
};
RectangleShape.prototype.HandleClick=function(){
this.ShapeObj.OnSelect();
};
RectangleShape.prototype.SetSelected=function(){
this.VisualFillElement.className=this.SelectedClassName;
this.ResizeElement.style.display="block";
this.VisualBorderElement.style.zIndex=2;
this.ResizeElement.style.zIndex=2;
};
RectangleShape.prototype.DeSelect=function(){
this.VisualFillElement.className=this.NotSelectedClassName;
this.ResizeElement.style.display="none";
this.VisualBorderElement.style.zIndex=1;
this.ResizeElement.style.zIndex=1;
};
RectangleShape.prototype.ShapePropsToCoords=function(_50){
return _50.x+","+_50.y+","+(parseInt(_50.x)+parseInt(_50.width))+","+(parseInt(_50.y)+parseInt(_50.height));
};
RectangleShape.prototype.ShapeCoordsToProps=function(_51){
var _52=_51.split(",");
var _53=_52[0];
var _54=_52[1];
var _55=_52[2];
var _56=_52[3];
return {x:parseInt(_53),y:parseInt(_54),width:parseInt((_55-_53)),height:parseInt((_56-_54))};
};
RectangleShape.prototype.AreCoordsValid=function(_57){
var _58=_57.split(",");
if(_58.length!=4){
return false;
}
for(var i=0;i<_58.length;i++){
if(_58[i]<0){
return false;
}
}
var _5a=this.ShapeCoordsToProps(_57);
if(_5a.width<=0){
return false;
}
if(_5a.height<=0){
return false;
}
if((_5a.x+_5a.width)>this.Image.offsetWidth){
return false;
}
if((_5a.y+_5a.height)>this.Image.offsetHeight){
return false;
}
return true;
};
RectangleShape.prototype.Dispose=function(){
this.VisualFillElement=null;
if(this.VisualBorderElement){
IMDoc.body.removeChild(this.VisualBorderElement);
this.VisualBorderElement=null;
}
if(this.ResizeElement){
IMDoc.body.removeChild(this.ResizeElement);
this.ResizeElement=null;
}
};
function IsCircleShapeSupported(){
if(!document.all&&!IsFFCanvasSupported()){
return false;
}
return true;
}
function CircleShape(){
this.Type=AREA_SHAPE_CONSTANTS.CIRCLE_TYPE;
this.VisualElement=null;
this.ResizeElement=null;
this.SelectedClassName="circ_shape_selected";
this.NotSelectedClassName="circ_shape_not_selected";
}
CircleShape.prototype=new AreaShape();
CircleShape.prototype.ContainerTitle="";
CircleShape.prototype.Resize=function(_5b){
if(document.all){
if(_5b.width){
this.VisualElement.style.width=Math.abs(_5b.width);
this.VisualElement.style.height=Math.abs(_5b.width);
}
this.SetResizeElementPosition();
}else{
this.Draw();
}
};
CircleShape.prototype.Draw=function(_5c){
if(this.VisualElement){
IMDoc.body.removeChild(this.VisualElement);
}
if(this.ResizeElement){
IMDoc.body.removeChild(this.ResizeElement);
}
var _5d=RadEditorNamespace.Utils.GetRect(this.Image);
var _5e=this.GetShapeProperties();
var _5f=IMDoc.createElement("DIV");
if(document.all){
_5f.innerHTML="<"+"v:oval fillcolor=\""+CIRCLESHAPE_BCKG_COLOR_NOT_SELECTED+"\" style=\"position:absolute;\" ></v:oval>";
}else{
_5f.innerHTML="<canvas width=\""+_5e.width+"\" height=\""+_5e.width+"\"></canvas>";
}
_5f=_5f.childNodes[0];
_5f.style.position="absolute";
_5f.title=this.ContainerTitle;
_5f.style.top=_5d.top+_5e.y;
_5f.style.left=_5d.left+_5e.x;
if(!document.all){
var ctx=_5f.getContext("2d");
ctx.beginPath();
var _61=parseInt(_5e.width/2);
var x=_61;
var y=_61;
var _64=0;
var _65=360;
ctx.arc(x,y,_61,_64,_65,false);
if(!_5c){
_5c=CIRCLESHAPE_BCKG_COLOR_NOT_SELECTED;
}
ctx.fillStyle=_5c;
ctx.fill();
ctx.stroke();
}else{
}
_5f.style.width=_5e.width;
_5f.style.height=_5e.width;
_5f.className=this.NotSelectedClassName;
_5f.id="circ_"+this.AreaID;
_5f.ShapeObj=this;
_5f.onclick=this.HandleClick;
IMDoc.body.appendChild(_5f);
var _66=this.CreateResizerElement();
this.VisualElement=_5f;
this.ResizeElement=_66;
this.SetResizeElementPosition();
var _67=this;
var _68=function(){
var _69=RadEditorNamespace.Utils.GetRect(_67.VisualElement);
var _6a=RadEditorNamespace.Utils.GetRect(_67.Image);
_67.SetResizeElementPosition();
var _6b=_67.ShapePropsToCoords({x:_69.left-_6a.left,y:_69.top-_6a.top,width:_69.width});
if(_67.AreCoordsValid(_6b)){
_67.SetCoords(_6b);
}else{
_67.Draw();
}
_67.OnSelect();
};
var _6c=function(){
_67.SetResizeElementPosition();
};
var _6d=function(fff,x,y){
_5f.style.cursor="W-resize";
var _71=RadEditorNamespace.Utils.GetRect(_5f);
var _72=_71.left+_71.width+parseInt(_67.ResizeElement.style.width);
var _73=(_71.width+(x-_72));
_5f.style.width=(_73>0?_73:0)+"px";
_5f.style.height=_5f.style.width;
_67.SetResizeElementPosition();
};
var _74=function(){
_5f.style.cursor="move";
_68();
};
var _t=new Draggable(_5f,_5f,null,_6c,_68);
var _t2=new Draggable(_66,_66,null,_6d,_74);
};
CircleShape.prototype.SetResizeElementPosition=function(){
var _77=this.VisualElement.style;
var _78=parseInt(_77.width)/2;
var _79=parseInt(_77.width);
var _7a=parseInt(_77.top)+_78;
var _7b=parseInt(_77.left)+_79;
this.ResizeElement.style.top=_7a;
this.ResizeElement.style.left=_7b;
this.ResizeElement.style.display="block";
};
CircleShape.prototype.HandleClick=function(){
this.ShapeObj.OnSelect();
};
CircleShape.prototype.SetSelected=function(){
if(document.all){
this.VisualElement.fillColor=CIRCLESHAPE_BCKG_COLOR_SELECTED;
}else{
this.Draw(CIRCLESHAPE_BCKG_COLOR_SELECTED);
}
this.VisualElement.className=this.SelectedClassName;
this.ResizeElement.style.display="block";
this.VisualElement.style.zIndex=2;
this.ResizeElement.style.zIndex=2;
};
CircleShape.prototype.DeSelect=function(){
if(document.all){
this.VisualElement.fillColor=CIRCLESHAPE_BCKG_COLOR_NOT_SELECTED;
}else{
this.Draw(CIRCLESHAPE_BCKG_COLOR_NOT_SELECTED);
}
this.VisualElement.className=this.NotSelectedClassName;
this.ResizeElement.style.display="none";
this.VisualElement.style.zIndex=1;
this.ResizeElement.style.zIndex=1;
};
CircleShape.prototype.ShapePropsToCoords=function(_7c){
var _7d=parseInt(_7c.width)/2;
return (parseInt(_7c.x)+_7d)+","+(parseInt(_7c.y)+_7d)+","+_7d;
};
CircleShape.prototype.ShapeCoordsToProps=function(_7e){
var _7f=_7e.split(",");
var _80=parseInt(_7f[0]);
var _81=parseInt(_7f[1]);
var _82=parseInt(_7f[2]);
return {x:_80-_82,y:_81-_82,width:(_82*2)};
};
CircleShape.prototype.AreCoordsValid=function(_83){
var _84=_83.split(",");
if(_84.length!=3){
return false;
}
for(var i=0;i<_84.length;i++){
if(_84[i]<0){
return false;
}
}
var _86=this.ShapeCoordsToProps(_83);
if(_86.x<0){
return false;
}
if(_86.y<0){
return false;
}
if(_86.width<=0){
return false;
}
if((_86.x+_86.width)>this.Image.offsetWidth){
return false;
}
if((_86.y+_86.width)>this.Image.offsetHeight){
return false;
}
return true;
};
CircleShape.prototype.Dispose=function(){
if(this.VisualElement){
IMDoc.body.removeChild(this.VisualElement);
this.VisualElement=null;
}
if(this.ResizeElement){
IMDoc.body.removeChild(this.ResizeElement);
this.ResizeElement=null;
}
};
function PolygonShape(){
this.Type=AREA_SHAPE_CONSTANTS.POLYGON_TYPE;
}
PolygonShape.prototype=new AreaShape();
PolygonShape.prototype.Draw=function(){
alert("draw polygon");
};
if(typeof (RadEditorNamespace)=="undefined"){
RadEditorNamespace={};
}
RadEditorNamespace.Utils={GetRect:function(_87){
if(!_87){
_87=this;
}
var _88=0;
var top=0;
var _8a=_87.offsetWidth;
var _8b=_87.offsetHeight;
while(_87.offsetParent){
_88+=_87.offsetLeft;
top+=_87.offsetTop;
_87=_87.offsetParent;
}
if(_87.x){
_88=_87.x;
}
if(_87.y){
top=_87.y;
}
_88=RadEditorNamespace.Utils.GetIntValue(_88,0);
top=RadEditorNamespace.Utils.GetIntValue(top,0);
_8a=RadEditorNamespace.Utils.GetIntValue(_8a,0);
_8b=RadEditorNamespace.Utils.GetIntValue(_8b,0);
return new RadEditorNamespace.Utils.Rectangle(_88,top,_8a,_8b);
},Rectangle:function(_8c,top,_8e,_8f){
this.left=(null!=_8c?_8c:0);
this.top=(null!=top?top:0);
this.width=(null!=_8e?_8e:0);
this.height=(null!=_8f?_8f:0);
this.right=_8c+_8e;
this.bottom=top+_8f;
},AttachEventEx:function(_90,_91,_92){
_91=RadEditorNamespace.Utils.FixEventName(_91);
if(_90.attachEvent){
_90.attachEvent(_91,_92);
}else{
if(_90.addEventListener){
_90.addEventListener(_91,_92,true);
}
}
},DetachEventEx:function(_93,_94,_95){
_94=RadEditorNamespace.Utils.FixEventName(_94);
if(_93.detachEvent){
_93.detachEvent(_94,_95);
}else{
if(_93.addEventListener){
_93.removeEventListener(_94,_95,true);
}
}
},FixEventName:function(_96){
_96=_96.toLowerCase();
if(document.addEventListener&&RadEditorNamespace.Utils.StartsWith(_96,"on")){
return _96.substr(2);
}else{
if(document.attachEvent&&!RadEditorNamespace.Utils.StartsWith(_96,"on")){
return "on"+_96;
}else{
return _96;
}
}
},CancelEvent:function(_97){
if(!_97){
_97=IMWin.event;
}
if(!_97){
return false;
}
_97.returnValue=false;
_97.cancelBubble=true;
if(_97.stopPropagation){
_97.stopPropagation();
}
return false;
},GetIntValue:function(_98,_99){
if(!_99){
_99=0;
}
var _9a=parseInt(_98);
return (isNaN(_9a)?_99:_9a);
},StartsWith:function(_9b,_9c){
if(typeof (_9c)!="string"){
return false;
}
return (0==_9b.indexOf(_9c));
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
}};
function Draggable(obj,_9f,_a0,_a1,_a2){
var _9f=_9f!=null?_9f:obj;
this.obj=obj;
this.objToMove=_9f;
this.onDragStart=_a0;
this.onDrag=_a1;
this.onDragEnd=_a2;
this.zIndex=0;
var _a3=this;
obj.onmousedown=function(e){
if(_a3.onDragStart){
_a3.onDragStart(_9f);
}
_a3.elNode=_9f;
var _a5=GetCursorPos(e);
x=_a5[0];
y=_a5[1];
_a3.cursorStartX=x;
_a3.cursorStartY=y;
var _a6=RadEditorNamespace.Utils.GetRect(_9f);
_a3.elStartLeft=_a6.left;
_a3.elStartTop=_a6.top;
if(isNaN(_a3.elStartLeft)){
_a3.elStartLeft=0;
}
if(isNaN(_a3.elStartTop)){
_a3.elStartTop=0;
}
_9f.style.position="absolute";
_a3.elNode.style.zIndex=++_a3.zIndex;
RadEditorNamespace.Utils.AttachEventEx(IMDoc,"onmousemove",_a3.DragGo);
RadEditorNamespace.Utils.AttachEventEx(IMDoc,"onmouseup",_a3.DragStop);
RadEditorNamespace.Utils.CancelEvent(e?e:IMWin.event);
};
this.DragGo=function(e){
var _a8=GetCursorPos(e);
x=_a8[0];
y=_a8[1];
var _a9=(_a3.elStartLeft+x-_a3.cursorStartX);
var _aa=(_a3.elStartTop+y-_a3.cursorStartY);
_a3.elNode.style.left=_a9+"px";
_a3.elNode.style.top=_aa+"px";
RadEditorNamespace.Utils.CancelEvent(e?e:IMWin.event);
if(_a3.onDrag){
_a3.onDrag(_9f,x,y);
}
};
this.DragStop=function(_ab){
RadEditorNamespace.Utils.DetachEventEx(IMDoc,"onmousemove",_a3.DragGo);
RadEditorNamespace.Utils.DetachEventEx(IMDoc,"onmouseup",_a3.DragStop);
if(_a3.onDragEnd){
_a3.onDragEnd(_9f);
}
};
function GetCursorPos(e){
if(document.all){
x=IMWin.event.clientX+IMDoc.documentElement.scrollLeft+IMDoc.body.scrollLeft;
y=IMWin.event.clientY+IMDoc.documentElement.scrollTop+IMDoc.body.scrollTop;
}else{
x=e.clientX+IMWin.scrollX;
y=e.clientY+IMWin.scrollY;
}
return [x,y];
}
}
function ShowRectProps(){
var _ad=CurrentArea.Shape.GetShapeProperties();
ShowRectPropsDlg(_ad);
}
function UpdateRectProps(_ae){
if(!CurrentArea){
return;
}
var _af=CurrentArea.Shape.ShapePropsToCoords(_ae);
if(!CurrentArea.Shape.AreCoordsValid(_af)){
return false;
}
CurrentArea.Shape.SetCoords(_af);
CurrentArea.Shape.Draw();
return true;
}
var CurrentArea=null;
RectangleShape.prototype.OnSelect=function(){
if(CurrentArea){
CurrentArea.DeSelect();
}
CurrentArea=imageMap.GetAreaById(this.AreaID);
var _b0=CurrentArea.GetProperties();
ShowAreaPropsDlg(_b0);
ShowRectProps();
ShowAreaProps();
CurrentArea.SetSelected();
};
CircleShape.prototype.OnSelect=function(){
if(CurrentArea){
CurrentArea.DeSelect();
}
CurrentArea=imageMap.GetAreaById(this.AreaID);
var _b1=CurrentArea.GetProperties();
ShowAreaPropsDlg(_b1);
ShowRectProps();
ShowAreaProps();
CurrentArea.SetSelected();
};
function ShowAreaProps(){
}
function UpdateAreaProps(_b2){
CurrentArea.SetProperties(_b2);
CurrentArea.Shape.OnSelect();
}
function DeselectCurrentArea(){
if(CurrentArea){
CurrentArea.DeSelect();
}
}
function RemoveArea(){
if(!imageMap||!CurrentArea){
return;
}
imageMap.RemoveArea(CurrentArea.ID);
CurrentArea=null;
CloseAreaPropsDlg();
if(imageMap.GetAreasNumber()==0){
HideMapProps();
}
}
function RemoveAllAreas(){
for(var i=0;i<imageMap.AreaCollection.length;i++){
if(imageMap.AreaCollection[i]!=null){
imageMap.RemoveArea(imageMap.AreaCollection[i].ID);
}
}
CurrentArea=null;
CloseAreaPropsDlg();
HideMapProps();
}
function HideMapProps(){
document.getElementById("map_props").style.display="none";
}
function ShowMapProps(){
document.getElementById("map_props").style.display="";
}
function CheckKeyDown(e){
if(!CurrentArea){
return;
}
if(e.keyCode==46){
RemoveArea();
}else{
if(e.keyCode==37){
CurrentArea.Move({x:-1,y:0});
CurrentArea.Shape.OnSelect();
}else{
if(e.keyCode==40){
CurrentArea.Move({x:0,y:1});
CurrentArea.Shape.OnSelect();
}else{
if(e.keyCode==38){
CurrentArea.Move({x:0,y:-1});
CurrentArea.Shape.OnSelect();
}else{
if(e.keyCode==39){
CurrentArea.Move({x:1,y:0});
CurrentArea.Shape.OnSelect();
}
}
}
}
}
}
function GetImageMapHTML(){
return imageMap.GetHTML();
}
var imageMap;
var IMWin;
var IMDoc;
function InitializeImageMap(){
IMWin=document.getElementById("ImageMapFrame").contentWindow;
IMDoc=IMWin.document;
var _b5=GetImageMapInitialProperties();
var _b6=_b5.ImageSrc;
var _b7=function(){
var _b8=ImageDialogCaller.GetImagePath();
if(_b8){
var _b9=IMDoc.getElementById("mappedImage");
var _ba=IMDoc.getElementById("mappedImageText");
if(_ba!=null){
_ba.parentNode.removeChild(_ba);
}
if(_b9==null){
_b9=InsertImageForMap();
imageMap=new ImageMap(_b9);
}
_b9.src=_b8;
SetDummyLayerOverImage(_b9);
document.getElementById("area_controls").style.display="block";
}
};
ImageDialogCaller.Initialize(_b5.EditorObj,25,_b7);
function InsertImageForMap(){
ImageElement=IMDoc.createElement("IMG");
ImageElement.id="mappedImage";
ImageElement.setAttribute("unselectable","on");
IMDoc.body.appendChild(ImageElement);
return ImageElement;
}
function SetDummyLayerOverImage(_bb){
var _bc=RadEditorNamespace.Utils.GetRect(_bb);
var _bd=IMDoc.getElementById("dummy");
_bd.style.left=_bc.left;
_bd.style.top=_bc.top;
_bd.style.width=_bc.width;
_bd.style.height=_bc.height;
RadEditorNamespace.Utils.AttachEventEx(IMDoc,"keydown",CheckKeyDown);
RadEditorNamespace.Utils.AttachEventEx(_bd,"mousedown",ImageMapper_OnMouseDown);
}
var _be;
if(_b6){
_be=InsertImageForMap();
_be.src=_b6;
var _bf=_b5.ImageWidth;
if(_bf){
_be.style.width=_bf;
}
var _c0=_b5.ImageHeight;
if(_c0){
_be.style.height=_c0;
}
ImageDialogCaller.SetImagePath(_b6);
SetDummyLayerOverImage(_be);
imageMap=new ImageMap(_be);
var _c1=_b5.ImageMapHTML;
if(_c1){
imageMap.Initialize(_c1);
}
document.getElementById("area_controls").style.display="block";
}else{
var _c2=IMDoc.createElement("CENTER");
_c2.id="mappedImageText";
_c2.innerHTML=localization.getText("SelectImage");
_be=IMDoc.body.appendChild(_c2);
}
RectangleShape.prototype.ContainerTitle=localization.getText("ShapeAlt");
CircleShape.prototype.ContainerTitle=localization.getText("ShapeAlt");
if(imageMap&&imageMap.GetAreasNumber()>0){
ShowMapProps();
}
}
function GetSelectedShapeType(){
var _c3=document.forms[0];
var _c4=_c3["shape_type"];
for(var i=0;i<_c4.length;i++){
if(_c4[i].checked){
return _c4[i].value;
}
}
return AREA_SHAPE_CONSTANTS.DEFAULT_SHAPE_TYPE;
}
function InsertNewMapArea(x,y,_c8,_c9){
var _ca=GetSelectedShapeType();
var _cb={};
_cb[AREA_SHAPE_CONSTANTS.RECTANGLE_TYPE]=InsertNewMapAreaRect;
_cb[AREA_SHAPE_CONSTANTS.POLYGON_TYPE]=InsertNewMapAreaPoly;
_cb[AREA_SHAPE_CONSTANTS.CIRCLE_TYPE]=InsertNewMapAreaCirc;
if(!_cb[_ca]){
_ca=AREA_SHAPE_CONSTANTS.DEFAULT_SHAPE_TYPE;
}
return _cb[_ca](x,y,_c8,_c9);
}
function InsertNewMapAreaCirc(x,y,_ce,_cf){
if(typeof (x)=="undefined"){
var _d0=IMDoc.getElementById("mappedImage");
if(_d0.offsetWidth<40||_d0.offsetHeight<40){
x=0;
y=0;
_ce=1;
}else{
x=10;
y=10;
_ce=20;
}
}
IMDoc.body.scrollLeft=0;
IMDoc.body.scrollTop=0;
try{
IMDoc.body.focus();
}
catch(e){
}
var _d1=imageMap.AddNewArea(AREA_SHAPE_CONSTANTS.CIRCLE_TYPE,x,y,_ce);
if(_d1!=null){
_d1.Shape.OnSelect();
}
return _d1;
}
function InsertNewMapAreaRect(x,y,_d4,_d5){
if(typeof (x)=="undefined"){
var _d6=IMDoc.getElementById("mappedImage");
if(_d6.offsetWidth<40||_d6.offsetHeight<40){
x=0;
y=0;
_d4=1;
_d5=1;
}else{
x=10;
y=10;
_d4=20;
_d5=20;
}
}
IMDoc.body.scrollLeft=0;
IMDoc.body.scrollTop=0;
try{
IMDoc.body.focus();
}
catch(e){
}
var _d7=imageMap.AddNewArea(AREA_SHAPE_CONSTANTS.RECTANGLE_TYPE,x,y,_d4,_d5);
if(_d7!=null){
_d7.Shape.OnSelect();
}
return _d7;
}
function InsertNewMapAreaPoly(x,y,_da,_db){
}
function IsFFCanvasSupported(){
var _dc=document.createElement("canvas");
if(!_dc.getContext){
return false;
}
return true;
}
var InitialOffsetX=0;
var InitialOffsetY=0;
function ImageMapper_OnMouseDown(e){
var _de=(document.all)?IMWin.event:e;
var _df=RadEditorNamespace.Utils.GetRect(IMDoc.getElementById("mappedImage"));
var _e0=_df.left;
var _e1=_df.top;
InitialOffsetX=_de.clientX-_e0;
InitialOffsetY=_de.clientY-_e1;
IsMouseMoved=false;
if(CurrentArea){
CloseAreaPropsDlg();
CurrentArea=null;
}
RadEditorNamespace.Utils.AttachEventEx(IMDoc,"mousemove",ImageMapper_OnMouseMove);
RadEditorNamespace.Utils.AttachEventEx(IMDoc,"mouseup",ImageMapper_OnMouseUp);
RadEditorNamespace.Utils.CancelEvent(_de);
}
function ImageMapper_OnMouseMove(e){
var _e3=(document.all)?IMWin.event:e;
if(!CurrentArea){
var _e4=2;
var _e5=2;
CurrentArea=InsertNewMapArea(InitialOffsetX,InitialOffsetY,_e4,_e5);
if(!CurrentArea){
return;
}
}
IsMouseMoved=true;
var _e6=RadEditorNamespace.Utils.GetRect(IMDoc.getElementById("mappedImage"));
var _e7=_e6.left;
var _e8=_e6.top;
var _e9=_e3.clientX-_e7;
var _ea=_e3.clientY-_e8;
var _eb={width:Math.abs(_e9-InitialOffsetX),height:Math.abs(_ea-InitialOffsetY)};
CurrentArea.SetSize(_eb);
RadEditorNamespace.Utils.CancelEvent(_e3);
}
function ImageMapper_OnMouseUp(){
if(IsMouseMoved){
if(CurrentArea){
CurrentArea.Shape.OnSelect();
}
}
RadEditorNamespace.Utils.DetachEventEx(IMDoc,"mousemove",ImageMapper_OnMouseMove);
RadEditorNamespace.Utils.DetachEventEx(IMDoc,"mouseup",ImageMapper_OnMouseUp);
IsMouseMoved=false;
};var fileName,pathName;
var deletePath=-1;
var selectedControl=null;
var selectedItem="";
var selectedFolder="";
var disableThumb="Height";
var submitForUpload;
function submitImagesFile(_1){
submitForUpload=true;
var _2=document.getElementById(FileUploadID);
if(trim(_2.value)==""){
alert(localization["AlertImage"]);
_2.focus();
submitForUpload=false;
}else{
document.getElementById(fileDirID).value=_1.CurrentItem.GetPath();
document.getElementById("loader").innerHTML=localization["Uploading"];
showObject("loader");
}
}
function validateUnit(_3){
return _3.match(/(1|2|3|4|5|6|7|8|9)\d*%?$/ig);
}
function ImagePreviewer(Id,_5,_6,_7){
this.ImagePath="";
this.Id=Id;
this.ThumbnailCreator=_5;
this.ThumbnailCreatorHolder=document.getElementById(this.Id+"_thumbnailCreatorHolder");
this.ThumbnailSuffix=_6;
this.CanCreateThumbnail=_7;
this.PreviewAreaHolder=document.getElementById(this.Id+"_previewAreaHolder");
this.IsPreviewerShown=false;
this.AltTextRow=document.getElementById(this.Id+"_altTextRow");
this.AltTextRow.style.display="none";
this.AltTextHolder=document.getElementById(this.Id+"_altText");
this.ThumbnailCreatorLink=document.getElementById(this.Id+"_thumbnailCreatorLink");
this.Area=document.getElementById("PreviewArea");
if(this.CanCreateThumbnail!=true){
this.ThumbnailCreatorLink.className="Disabled";
}
if(typeof (GetDisposeManager)!="undefined"){
GetDisposeManager().Add(this);
}
}
ImagePreviewer.prototype.Dispose=function(){
this.ThumbnailCreator=null;
this.ThumbnailCreatorHolder=null;
this.PreviewAreaHolder=null;
this.AltTextRow=null;
this.AltTextHolder=null;
this.ThumbnailCreatorLink=null;
this.Area=null;
};
ImagePreviewer.prototype.Clear=function(){
this.Area.innerHTML="";
this.ImagePath="";
};
ImagePreviewer.prototype.GetHtml=function(){
if(this.ImagePath){
return "<img src=\""+this.ImagePath+"\" border=\"0\">";
}else{
return "";
}
};
ImagePreviewer.prototype.LoadObjectFromPath=function(_8){
this.SetAltText("");
if(_8){
if(document.all){
this.AltTextRow.style.display="";
}
this.ImagePath=_8;
this.Area.innerHTML="";
var _9=document.createElement("IMG");
_9=_9.cloneNode(true);
_9.src=_8;
if(_9.complete){
FitImage(_9);
this.Area.appendChild(_9);
}else{
if(document.all){
this.Area.innerHTML=localization["LoadingImagePrompt"];
this.Area.style.font="menu";
var _a=this;
_9.onload=function(){
_a.Area.innerHTML="";
FitImage(this);
_a.Area.appendChild(this);
this.onload=null;
};
}else{
this.Area.appendChild(_9);
}
}
if(this.CanCreateThumbnail){
this.ThumbnailCreator.Enable();
this.ThumbnailCreator.Initialize(_9,this.ThumbnailSuffix);
}
}else{
if(this.ThumbnailCreator){
this.ThumbnailCreator.Disable();
}
this.AltTextRow.style.display="none";
this.Area.innerHTML="<div style='text-align:center;color:#aaaaaa;font-family:Tahoma;'>x</div>";
}
};
ImagePreviewer.prototype.SetAltText=function(_b){
this.AltTextHolder.value=_b;
};
ImagePreviewer.prototype.GetAltText=function(){
return this.AltTextHolder.value;
};
ImagePreviewer.prototype.ShowThumbnailCreator=function(){
if(this.CanCreateThumbnail){
this.IsPreviewerShown=true;
this.PreviewAreaHolder.style.display="none";
this.ThumbnailCreatorHolder.style.display="";
}
};
ImagePreviewer.prototype.HideThumbnailCreator=function(){
this.IsPreviewerShown=false;
this.PreviewAreaHolder.style.display="";
this.ThumbnailCreatorHolder.style.display="none";
};
ImagePreviewer.prototype.SwitchThumbnailCreator=function(){
if(this.IsPreviewerShown){
this.HideThumbnailCreator();
}else{
this.ShowThumbnailCreator();
}
};
function FitImage(_c){
var _d=230;
var _e=240;
var _f=_c.height/_d;
var _10=_c.width/_e;
_c.setAttribute("OriginalWidth",_c.width);
_c.setAttribute("OriginalHeight",_c.height);
if(_c.width>_e&&_c.height>_d){
var _11=(_f>=_10?_f:_10);
_c.width=(_c.width/_11);
_c.height=(_c.height/_11);
}else{
if(_c.width>_e){
_c.width=(_c.width/_10);
_c.height=(_c.height/_10);
}else{
if(_c.height>_d){
_c.width=(_c.width/_f);
_c.height=(_c.height/_f);
}
}
}
_c.style.border="1px solid black";
}
function ShowActualImageSize(){
var img=GetPreviewedImage();
if(!img){
return;
}
var _13=img.getAttribute("OriginalWidth");
var _14=img.getAttribute("OriginalHeight");
if(_13){
}else{
_13=img.width;
}
if(_14){
}else{
_14=img.height;
}
if(_13&&_14){
img.width=_13;
img.height=_14;
}
}
function CallFitImage(){
var img=GetPreviewedImage();
if(!img){
return;
}
var _16=img.getAttribute("OriginalWidth");
var _17=img.getAttribute("OriginalHeight");
if(_16&&_16!=img.width){
img.width=_16;
}
if(_17&&_17!=img.height){
img.height=_17;
}
FitImage(img);
}
function GetPreviewedImage(){
var _18=document.getElementById("PreviewArea");
if(!_18){
return null;
}
var _19=_18.getElementsByTagName("IMG");
if(_19&&_19.length>0){
return _19.item(0);
}else{
return null;
}
}
function ScaleImage(_1a,_1b){
var img=GetPreviewedImage();
if(!img){
return;
}
_1a=_1a/100;
if(_1b){
img.width+=img.width*_1a;
img.height+=img.height*_1a;
}else{
img.width-=img.width*_1a;
img.height-=img.height*_1a;
}
};function ImagePropertiesControl(Id,_2,_3,_4,_5,_6,_7,_8){
this.Id=Id;
this.BorderColorPicker=_2;
this.AlignmentSelector=_3;
this.ConstrainerImage=_4;
this.BorderSizeSpinBox=_5;
this.HorizontalSpacingSpinBox=_6;
this.VerticalSpacingSpinBox=_7;
this.ChangeSourceImageDialogCaller=_8;
this.WidthHolder=document.getElementById(this.Id+"_width");
this.HeightHolder=document.getElementById(this.Id+"_height");
this.AltHolder=document.getElementById(this.Id+"_alt");
this.LongDescriptionHolder=document.getElementById(this.Id+"_longDescription");
this.OriginalImage=null;
this.ImageToModify=null;
this.ConstrainDimentions=false;
this.Ratio=0;
this.AllowedASCII=new Array(8,16,35,36,37,39,45,46);
}
ImagePropertiesControl.prototype.Initialize=function(_9,_a,_b,_c){
this.BorderColorPicker.CanAddCustomColor=_c;
this.BorderColorPicker.CanAddHexColor=_c;
if(_b){
this.BorderColorPicker.SetColors(_b);
}
this.ImageToModify=_9;
this.EditorObject=_a;
this.OriginalImage=new Image();
this.OriginalImage.src=this.ImageToModify.src;
this.ChangeSourceImageDialogCaller.Initialize(this.EditorObject,20);
var _d=this.ImageToModify.getAttribute("src");
this.ChangeSourceImageDialogCaller.SetImagePath(_d);
this.Ratio=this.ImageToModify.width/this.ImageToModify.height;
var _e=this.ImageToModify.getAttribute("alt");
if(_e){
this.AltHolder.value=_e;
}
var _f=this.ImageToModify.getAttribute("longDesc");
if(_f){
this.LongDescriptionHolder.value=_f;
}
this.BorderSizeSpinBox.Initialize(this.ImageToModify.border,20,2);
this.HorizontalSpacingSpinBox.Initialize(this.ImageToModify.getAttribute("hspace"),20,2);
this.VerticalSpacingSpinBox.Initialize(this.ImageToModify.getAttribute("vspace"),20,2);
this.AlignmentSelector.SelectAlignment(this.ImageToModify.align);
var _10=this.ImageToModify.style.borderColor.toUpperCase();
this.BorderColorPicker.SelectColor(_10);
var _11=this.ImageToModify.width<=0?this.OriginalImage.width:this.ImageToModify.width;
var _12=this.ImageToModify.height<=0?this.OriginalImage.height:this.ImageToModify.height;
this.WidthHolder.value=_11;
this.HeightHolder.value=_12;
};
ImagePropertiesControl.prototype.GetOriginalImage=function(){
return this.OriginalImage;
};
ImagePropertiesControl.prototype.ValidateNumber=function(e){
if(window.event!=null){
e=window.event;
}
if(((e.keyCode>=48)&&(e.keyCode<=57))||((e.keyCode>=96)&&(e.keyCode<=105))||(inArray(this.AllowedASCII,e.keyCode))){
return true;
}else{
if(e.preventDefault){
e.preventDefault();
e.stopPropagation();
}
return false;
}
};
ImagePropertiesControl.prototype.ValidateDimension=function(e,_15){
if(!this.ValidateNumber(e)){
return false;
}
if(this.ConstrainDimentions){
var _16=null;
var _17=null;
var _18=0;
if(_15){
_16=this.HeightHolder;
_17=this.WidthHolder;
_18=1/this.Ratio;
}else{
_16=this.WidthHolder;
_17=this.HeightHolder;
_18=this.Ratio;
}
_16.value=Math.ceil(_17.value*_18);
}
return true;
};
ImagePropertiesControl.prototype.GetUpdatedImage=function(){
if(!this.UpdatedImage){
this.UpdatedImage=this.ImageToModify.cloneNode(true);
}
var _19=this.BorderSizeSpinBox.GetCurrentSize();
if(_19<0||_19.toString()==""){
this.UpdatedImage.removeAttribute("border",false);
}else{
this.UpdatedImage.border=_19;
}
this.UpdatedImage.style.borderColor=this.BorderColorPicker.SelectedColor;
if(this.AltHolder.value){
this.UpdatedImage.setAttribute("alt",this.AltHolder.value);
}else{
this.UpdatedImage.removeAttribute("alt",false);
}
if(this.LongDescriptionHolder.value){
this.UpdatedImage.setAttribute("longDesc",this.LongDescriptionHolder.value,0);
}else{
this.UpdatedImage.removeAttribute("longDesc",false);
}
this.UpdatedImage.align=this.AlignmentSelector.GetAlign();
var _1a=this.HorizontalSpacingSpinBox.GetCurrentSize();
if(_1a<=0){
this.UpdatedImage.removeAttribute("hspace",false);
}else{
this.UpdatedImage.hspace=_1a;
}
var _1b=this.VerticalSpacingSpinBox.GetCurrentSize();
if(_1b<=0){
this.UpdatedImage.removeAttribute("vspace",false);
}else{
this.UpdatedImage.vspace=_1b;
}
this.UpdatedImage.src=this.ChangeSourceImageDialogCaller.GetImagePath();
this.UpdatedImage.removeAttribute("width");
this.UpdatedImage.removeAttribute("height");
if(this.UpdatedImage.style.removeAttribute){
this.UpdatedImage.style.removeAttribute("width",false);
this.UpdatedImage.style.removeAttribute("height",false);
}else{
this.UpdatedImage.style.width=null;
this.UpdatedImage.style.height=null;
}
var _1c=parseInt(this.WidthHolder.value);
if(_1c){
this.UpdatedImage.style.width=_1c+"px";
}
var _1d=parseInt(this.HeightHolder.value);
if(_1d){
this.UpdatedImage.style.height=_1d+"px";
}
if(this.UpdatedImage.style.cssText==""){
this.UpdatedImage.removeAttribute("style",false);
}
return this.UpdatedImage;
};
ImagePropertiesControl.prototype.ConstrainPropotions=function(){
this.ConstrainerImage.src=this.ConstrainerImage.src.substr(0,this.ConstrainerImage.src.length-(this.ConstrainDimentions?6:7))+(this.ConstrainDimentions?"Off.gif":"On.gif");
this.ConstrainDimentions=!this.ConstrainDimentions;
if(this.ConstrainDimentions){
this.HeightHolder.value=Math.ceil(this.WidthHolder.value/this.Ratio);
}
};;function LinkManager(id,_2,_3){
this.Id=id;
this.HyperlinkCssClassSelector=_2;
this.EmailCssClassSelector=_3;
this.LinkObject=null;
this.LinkVariant="link";
this.OnOkClicked=null;
this.OnCancelClicked=null;
this.AddressHolder=document.getElementById(this.Id+"_address");
this.SubjectHolder=document.getElementById(this.Id+"_subject");
this.LinkNameHolder=document.getElementById(this.Id+"_linkName");
this.LinkTextHolder=document.getElementById(this.Id+"_linkText");
this.LinkTextRow=document.getElementById(this.Id+"_rowLinkText");
this.LinkUrlHolder=document.getElementById(this.Id+"_linkUrl");
this.PageAnchorsHolder=document.getElementById(this.Id+"_pageAnchorsHolder");
this.LinkTypeHolder=document.getElementById(this.Id+"_linkType");
this.LinkTargetHolder=document.getElementById(this.Id+"_linkTarget");
this.LinkTargetSelector=document.getElementById(this.Id+"_linkTargetSelector");
this.TitleTextHolder=document.getElementById(this.Id+"_titleText");
this.EmailTextHolder=document.getElementById(this.Id+"_emailText");
this.EmailTextRow=document.getElementById(this.Id+"_rowEmailText");
}
LinkManager.prototype.Initialize=function(_4){
this.LinkObject=_4;
this.HyperlinkCssClassSelector.Initialize(this.LinkObject.CssClasses);
this.EmailCssClassSelector.Initialize(this.LinkObject.CssClasses);
if(this.LinkObject.SelectedTab&&this.LinkObject.SelectedTab>=0){
TabHolder.SetTabSelected(this.LinkObject.SelectedTab);
}else{
if(this.LinkObject.href&&this.LinkObject.href.match(/^(mailto:)([^\?&]*)/ig)){
this.SetLinkVariant("email");
TabHolder.SetTabSelected(2);
this.AddressHolder.value=RegExp.$2;
if(this.LinkObject.href.match(/(\?|&)subject=([^&]*)/ig)){
this.SubjectHolder.value=RegExp.$2;
}
this.EmailCssClassSelector.SelectCssClass(this.LinkObject.className);
}else{
if(trim(this.LinkObject.name)!=""){
this.SetLinkVariant("anchor");
TabHolder.SetTabSelected(1);
this.LinkNameHolder.value=this.LinkObject.name;
}else{
var _5="http://";
if(this.LinkObject.href){
_5=this.LinkObject.href;
}
this.LinkUrlHolder.value=_5;
var _6=this.LinkTypeHolder.getElementsByTagName("OPTION");
this.LinkTypeHolder.selectedIndex=0;
for(var i=1;i<_6.length;i++){
var re=new RegExp("^("+_6[i].value+")","gi");
if(re.test(_5)){
this.LinkTypeHolder.selectedIndex=i;
break;
}
}
for(var _9=0;_9<this.LinkObject.documentAnchors.length;_9++){
var _a=this.LinkObject.documentAnchors[_9];
var _b=new Option(_a.name,"#"+_a.name);
this.PageAnchorsHolder.options.add(_b);
if("#"+_a.name==this.LinkObject.href){
this.PageAnchorsHolder.selectedIndex=0;
_b.selected=true;
}
}
this.LinkTargetHolder.value=this.LinkObject.target;
for(var _c=0;_c<this.LinkTargetSelector.options.length;_c++){
if(this.LinkTargetHolder.value==this.LinkTargetSelector.options[_c].value){
this.LinkTargetSelector.options[_c].selected=true;
}
}
this.TitleTextHolder.value=this.LinkObject.title;
this.HyperlinkCssClassSelector.SelectCssClass(this.LinkObject.className);
}
}
}
if(this.LinkObject.showText){
this.LinkTextRow.style.display="";
this.EmailTextRow.style.display="";
this.LinkTextHolder.value=this.LinkObject.text;
this.EmailTextHolder.value=this.LinkObject.text;
}else{
this.LinkTextRow.style.display="none";
this.EmailTextRow.style.display="none";
this.LinkTextHolder.value="";
this.EmailTextHolder.value="";
}
};
LinkManager.prototype.SetLinkVariant=function(_d){
this.LinkVariant=_d;
};
LinkManager.prototype.ChangeLinkType=function(_e){
var _f=this.LinkUrlHolder.value;
var _10;
_10=_f.indexOf(":");
if(_10>=0){
_f=_f.substring(_10+1);
}
_10=_f.indexOf("//");
if(_10>=0){
_f=_f.substring(_10+2);
}
this.LinkUrlHolder.value=_e+_f;
};
LinkManager.prototype.ChangeLinkTarget=function(_11){
this.LinkTargetHolder.value=_11.value;
_11.selectedIndex=0;
};
LinkManager.prototype.SetLinkToAnchor=function(_12){
if(_12.selectedIndex!=0){
this.LinkUrlHolder.value=_12.value;
}
};
LinkManager.prototype.GetModifiedLinkObject=function(){
var _13={realLinkObject:this.LinkObject.realLinkObject,href:"",className:"",text:"",target:"",name:"",title:"",showText:false};
_13.text=this.LinkTextHolder.value;
if(this.LinkVariant=="link"){
_13.href=this.LinkUrlHolder.value;
_13.target=this.LinkTargetHolder.value;
_13.title=this.TitleTextHolder.value;
_13.className=this.HyperlinkCssClassSelector.GetSelectedClassName();
}else{
if(this.LinkVariant=="anchor"){
_13.name=this.LinkNameHolder.value;
}else{
_13.href="mailto:"+this.AddressHolder.value;
_13.mail=this.AddressHolder.value;
_13.text=this.EmailTextHolder.value;
if(this.SubjectHolder.value!=""){
_13.href+="?subject="+this.SubjectHolder.value;
}
_13.className=this.EmailCssClassSelector.GetSelectedClassName();
}
}
return _13;
};
LinkManager.prototype.OkClicked=function(){
if(this.OnOkClicked){
this.OnOkClicked();
}
};
LinkManager.prototype.CancelClicked=function(){
if(this.OnCancelClicked){
this.OnCancelClicked();
}
};;function Hashtable(){
this.keys=new Array();
this.values=new Array();
}
Hashtable.prototype.Add=function(_1,_2){
this.keys[this.keys.length]=_1;
this.values[this.values.length]=_2;
};
Hashtable.prototype.Item=function(_3){
for(var i=0;i<this.keys.length;i++){
if(this.keys[i]==_3){
return this.values[i];
}
}
return null;
};
Hashtable.prototype.Keys=function(){
return this.keys;
};
var properties=null;
function GetProperties(){
if(!properties){
properties=new Hashtable();
properties.Add("AllowChangeDisplaySize",new Array(localization["AllowChangeDisplaySize"],null,"radio","boolean","true"));
properties.Add("AllowScan",new Array(localization["AllowScan"],null,"radio","boolean","true"));
properties.Add("AnimationAtStart",new Array(localization["AnimationAtStart"],null,"radio","boolean","true"));
properties.Add("AudioStream",new Array(localization["AudioStream"],null,"text-regexp","^[0-9]*$",localization["ValidateNumber"],""));
properties.Add("AutoRewind",new Array(localization["AutoRewind"],null,"radio","boolean","true"));
properties.Add("AutoSize",new Array(localization["AutoSize"],null,"radio","boolean",""));
properties.Add("AutoStart",new Array(localization["AutoStart"],null,"radio","boolean","true"));
properties.Add("Balance",new Array(localization["Balance"],null,"text-range","({text} >= -10000) && ({text} <= 10000)",localization["ValidateNumber10000"],0));
properties.Add("CCActive",new Array(localization["CCActive"],null,"radio","boolean","false"));
properties.Add("ClickToPlay",new Array(localization["ClickToPlay"],null,"radio","boolean","true"));
properties.Add("ColorKey",new Array(localization["ColorKey"],null,"text","",localization["ValidateNumberHex"],""));
properties.Add("CurrentAngle",new Array(localization["CurrentAngle"],null,"select-range",1,9,1));
properties.Add("CurrentAudioStream",new Array(localization["CurrentAudioStream"],null,"text-range","((({text} >= 0) && ({text} <= 7)) || ({text} == '0xFFFFFFFF'))",localization["ValidateAudio"],0));
properties.Add("CurrentCCService",new Array(localization["CurrentCCService"],null,"select",new Array(0,1,2,3,4,5),new Array(localization["None"],localization.DefaultCaption1,localization.Caption2,localization.Text1,localization.Text2,localization.ExtendedDataServices),0));
properties.Add("CurrentMarker",new Array(localization["CurrentMarker"],null,"text-regexp","^[0-9]*$",localization["ValidateNumber"],0));
properties.Add("CurrentPosition",new Array(localization["CurrentPosition"],null,"text-regexp","^[0-9]*$",localization["ValidateNumber"],""));
properties.Add("CurrentSubpictureStream",new Array(localization["CurrentSubpictureStream"],null,"select-special",new Array(localization["Streamisvalid"],"range",0,31),new Array(localization.Nosubpicturestream,"value",63),0));
properties.Add("CursorType",new Array(localization["CursorType"],null,"select",new Array(32650,32512,32515,32513,32648,32646,32643,32645,32642,32644,32516,32514,1),new Array(localization["Arrowhourglass"],localization.Standardarrow,localization.Crosshair,localization.TextIbeam,localization.SlashedCircle,localization.Fourpointedarrow,localization.DoublepointedNESW,localization.DoublepointedNS,localization.DoublepointedNWSE,localization.DoublepointedWE,localization.Verticalarrow,localization.Hourglass,localization.Handpointing),""));
properties.Add("DefaultFrame",new Array(localization["DefaultFrame"],null,"text","","",""));
properties.Add("DisplayBackColor",new Array(localization["DisplayBackColor"],null,"select-color",16777215));
properties.Add("DisplayForeColor",new Array(localization["DisplayForeColor"],null,"select-color",16777215));
properties.Add("DisplayMode",new Array(localization["DisplayMode"],null,"radio","integer",0));
properties.Add("DisplaySize",new Array(localization["DisplaySize"],null,"select",new Array(0,1,2,3,4,5,6,7),new Array(localization["Samesize"],localization.HalfSourceImage,localization.DoubleSourceImage,localization.EntireScreen,localization.DesignTime,localization.SixteenthScreen,localization.QuarterScreen,localization.HalfScreen),""));
properties.Add("EnableContextMenu",new Array(localization["EnableContextMenu"],null,"radio","boolean","true"));
properties.Add("Enabled",new Array(localization["Enabled"],null,"radio","boolean","true"));
properties.Add("EnableFullScreenControls",new Array(localization["EnableFullScreenControls"],null,"radio","boolean","true"));
properties.Add("EnablePositionControls",new Array(localization["EnablePositionControls"],null,"radio","boolean","true"));
properties.Add("EnableTracker",new Array(localization["EnableTracker"],null,"radio","boolean","true"));
properties.Add("Hidden",new Array(localization["Hidden"],null,"radio","boolean","false"));
properties.Add("InvokeURLs",new Array(localization["InvokeURLs"],null,"radio","boolean","true"));
properties.Add("Language",new Array(localization["Language"],null,"text-regexp","^[0-9]*$",localization["ValidateLCID"],""));
properties.Add("Mute",new Array(localization["Mute"],null,"radio","boolean","true"));
properties.Add("PlayCount",new Array(localization["PlayCount"],null,"text-regexp","^[0-9]*$",localization["ValidateNumber"],1));
properties.Add("PreviewMode",new Array(localization["PreviewMode"],null,"radio","boolean","false"));
properties.Add("Rate",new Array(localization["Rate"],null,"text-range","({text} >= -10) && ({text} <= 10)",localization["ValidateNumber10"],1));
properties.Add("SelectionEnd",new Array(localization["SelectionEnd"],null,"text-range","((({text} >= Number.MIN_VALUE) && ({text} <= Number.MAX_VALUE)) || ({text} == ''))",localization["ValidateNumber"],""));
properties.Add("SelectionStart",new Array(localization["SelectionStart"],null,"text-range","({text} >= Number.MIN_VALUE) && ({text} <= Number.MAX_VALUE)",localization["ValidateNumber"],0));
properties.Add("SendErrorEvents",new Array(localization["SendErrorEvents"],null,"radio","boolean","true"));
properties.Add("SendKeyboardEvents",new Array(localization["SendKeyboardEvents"],null,"radio","boolean","false"));
properties.Add("SendMouseClickEvents",new Array(localization["SendMouseClickEvents"],null,"radio","boolean","false"));
properties.Add("SendMouseMoveEvents",new Array(localization["SendMouseMoveEvents"],null,"radio","boolean","false"));
properties.Add("SendOpenStateChangeEvents",new Array(localization["SendOpenStateChangeEvents"],null,"radio","boolean","true"));
properties.Add("SendPlayStateChangeEvents",new Array(localization["SendPlayStateChangeEvents"],null,"radio","boolean","true"));
properties.Add("SendWarningEvents",new Array(localization["SendWarningEvents"],null,"radio","boolean","true"));
properties.Add("ShowAudioControls",new Array(localization["ShowAudioControls"],null,"radio","boolean","true"));
properties.Add("ShowCaptioning",new Array(localization["ShowCaptioning"],null,"radio","boolean","false"));
properties.Add("ShowControls",new Array(localization["ShowControls"],null,"radio","boolean","true"));
properties.Add("ShowDisplay",new Array(localization["ShowDisplay"],null,"radio","boolean","false"));
properties.Add("ShowGotoBar",new Array(localization["ShowGotoBar"],null,"radio","boolean","false"));
properties.Add("ShowPositionControls",new Array(localization["ShowPositionControls"],null,"radio","boolean","true"));
properties.Add("ShowStatusBar",new Array(localization["ShowStatusBar"],null,"radio","boolean","false"));
properties.Add("ShowTracker",new Array(localization["ShowTracker"],null,"radio","boolean","true"));
properties.Add("SubpictureOn",new Array(localization["SubpictureOn"],null,"radio","boolean",""));
properties.Add("TransparentAtStart",new Array(localization["TransparentAtStart"],null,"radio","boolean","false"));
properties.Add("VideoBorder3D",new Array(localization["VideoBorder3D"],null,"radio","boolean","false"));
properties.Add("VideoBorderColor",new Array(localization["VideoBorderColor"],null,"select-color",0));
properties.Add("VideoBorderWidth",new Array(localization["VideoBorderWidth"],null,"text-regexp","^[0-9]*$",localization["ValidateNumber"],0));
properties.Add("Volume",new Array(localization["Volume"],null,"text-range","({text} >= -10000) && ({text} <= 10000)",localization["ValidateNumber10000"],-600));
}
return properties;
}
var fileName,pathName;
var deletePath=false;
var selection=true;
var dropDownMedia;
var submitForUpload;
function changeValue(_5){
var _6=GetProperties().Item(document.getElementById("property").value);
_6[1]=_5.value;
}
function checkMovieFile(_7){
if(_7.match(/(\.cda)|(\.ivf)|(\.aif)|(\.aifc)|(\.aiff)|(\.asf)|(\.asx)|(\.wax)|(\.wm)|(\.wma)|(\.wmd)|(\.wmv)|(\.wvx)|(\.wmp)|(\.wmx)|(\.avi)|(\.wav)|(\.wmz)|(\.wms)|(\.mpeg)|(\.mpg)|(\.m1v)|(\.mp2)|(\.mpa)|(\.mpe)|(\.mpe)|(\.mp2v)|(\.mpv2)|(\.mid)|(\.midi)|(\.rmi)|(\.au)|(\.snd)|(\.mp3)|(\.m3u)|(\.vob)$/ig)){
return true;
}else{
return false;
}
}
function createRadio(id,_9,_a){
var _b;
if(document.all){
_b=document.createElement("<INPUT NAME='booleanGroup' ONCLICK='changeValue(this)'"+((_9==_a.toString())?" CHECKED":"")+">");
}else{
_b=document.createElement("INPUT");
_b.name="booleanGroup";
_b.onclick=function(){
changeValue(this);
};
if(_9==_a.toString()){
_b.setAttribute("checked",true);
}
}
_b.type="radio";
_b.id=id;
_b.value=_9;
return _b;
}
function createText(_c,_d,_e){
var _f=document.createElement("INPUT");
_f.onkeydown=function(){
changeText(this.id);
};
_f.onblur=function(){
validateText(this,_d,_e);
};
_f.id="textProperty";
_f.type="text";
_f.value=_c;
_f.className="flatTextBox";
return _f;
}
function createLabel(_10,_11){
var _12=document.createElement("LABEL");
_12.htmlFor=_10;
_12.innerHTML=_11;
return _12;
}
function changeText(_13){
window.setTimeout("changeValue(document.getElementById('"+_13+"'))",100);
return true;
}
function changeProperty(_14){
var _15=document.getElementById("propertyValue");
var _16=GetProperties().Item(_14);
if(_14!=""){
var _17=document.createElement("SPAN");
_17.innerHTML=localization["Description"]+_16[0];
_15.innerHTML="";
_15.appendChild(_17);
switch(_16[2]){
case "radio":
var _18=((_16[1]==null)?_16[4]:_16[1]);
switch(_16[3]){
case "boolean":
var _19=new Array("true","false");
var _1a=new Array(localization["Yes"],localization["No"]);
break;
case "integer":
var _19=new Array(0,1);
var _1a=new Array(localization["Yes"],localization["No"]);
break;
}
var _1b=document.createElement("TABLE");
_1b.cellPadding="0";
_1b.cellSpacing="0";
var _1c=document.createElement("TBODY");
_1b.appendChild(_1c);
var row=document.createElement("TR");
for(var i=0;i<_19.length;i++){
var _1f=document.createElement("TD");
_1f.valign="middle";
_1f.innerHTML="&nbsp;";
_1f.appendChild(createRadio("booleanGroup"+i,_19[i],_18));
_1f.appendChild(createLabel("booleanGroup"+i,_1a[i]));
row.appendChild(_1f);
}
_1c.appendChild(row);
_15.insertBefore(_1b,_17);
break;
case "select":
var _18=(_16[1]==null)?_16[5]:_16[1];
var _20=document.createElement("SELECT");
_20.onchange=function(){
changeValue(this);
};
var _19=_16[3];
var _1a=_16[4];
for(var i=0;i<_19.length;i++){
var _21=document.createElement("OPTION");
_21.innerHTML=_1a[i];
_21.value=_19[i];
_20.appendChild(_21);
}
selectOption(_20,_18);
_15.insertBefore(_20,_17);
break;
case "select-range":
var _18=((_16[1]==null)?_16[5]:_16[1]);
var _20=document.createElement("SELECT");
_20.onchange=function(){
changeValue(this);
};
for(var i=_16[3];i<=_16[4];i++){
var _21=document.createElement("OPTION");
_21.innerHTML=i;
_21.value=i;
_20.appendChild(_21);
}
selectOption(_20,_18);
_15.insertBefore(_20,_17);
break;
case "select-special":
var _18=(_16[1]==null)?_16[5]:_16[1];
var _20=document.createElement("SELECT");
_20.onchange=function(){
changeValue(this);
};
for(var i=3;i<(_16.length-1);i++){
var _22=document.createElement("OPTGROUP");
_22.label=_16[i][0];
_20.appendChild(_22);
switch(_16[i][1]){
case "range":
for(var j=_16[i][2];j<=_16[i][3];j++){
var _21=document.createElement("OPTION");
_21.innerHTML=j;
_21.value=j;
_22.appendChild(_21);
}
break;
case "value":
var _21=document.createElement("OPTION");
_21.innerHTML=_16[i][2];
_21.value=_16[i][2];
_22.appendChild(_21);
break;
}
}
selectOption(_20,_18);
_15.insertBefore(_20,_17);
break;
case "select-color":
var _18=((_16[1]==null)?_16[3]:_16[1]);
var _20=document.createElement("SELECT");
_20.onchange=function(){
changeColor(this,"",8);
};
_20.style.width="140px";
var _19=new Array("000000","0000FF","008000","FFA500","FF0000","FFFFFF","FFFF00","");
var _1a=new Array(localization["Black"],localization["Blue"],localization["Green"],localization["Orange"],localization["Red"],localization["White"],localization["Yellow"],localization["Custom"]);
var _24=new Array();
for(var i=0;i<_19.length;i++){
var _21=document.createElement("OPTION");
if(_19[i]==""){
_21.value="";
}else{
_21.value=eval("0x"+_19[i]);
_21.style.backgroundColor="#"+_19[i];
}
_21.innerHTML=_1a[i];
_20.appendChild(_21);
_24[_24.length]=_21;
}
_15.insertBefore(_20,_17);
if(!selectOption(_20,parseInt(_18))){
_21=document.createElement("OPTION");
color=convertColor(_18);
_21.innerHTML=localization["Customh"]+color;
_21.value=_18;
_21.style.backgroundColor=color;
_20.insertBefore(_21,_24[7]);
_20.selectedIndex=7;
}
break;
case "text":
var _18=((_16[1]==null)?_16[5]:_16[1]);
var _25=createText(_18,"",false);
_15.insertBefore(_25,_17);
if(_16[4]!=""){
_17.innerHTML+=localization["Note"]+_16[4];
}
break;
case "text-regexp":
var _18=((_16[1]==null)?_16[5]:_16[1]);
var _25=createText(_18,_16[3],true);
_15.insertBefore(_25,_17);
_17.innerHTML+=localization["Note"]+_16[4];
break;
case "text-range":
var _18=((_16[1]==null)?_16[5]:_16[1]);
var _25=createText(_18,_16[3],false);
_15.insertBefore(_25,_17);
_17.innerHTML+=localization["Note"]+_16[4];
break;
}
}else{
_15.innerHTML=localization["NA"];
}
}
function convertColor(_26){
_26=parseInt(_26);
_26=_26.toString(16);
if(_26.length<6){
var _27="000000".substring(0,(6-_26.length));
_26="#"+_27.concat(_26).toUpperCase();
}else{
_26="#"+_26.toUpperCase();
}
return _26;
}
function validateText(_28,_29,_2a){
try{
if(_2a){
var re=new RegExp(_29,"gi");
if(re.test(_28.value)){
return true;
}
}else{
if(_29==""){
return true;
}else{
if(eval(_29.replace(/\{text\}/gi,_28.value))){
return true;
}
}
}
}
catch(e){
}
alert(localization["AlertValue"]);
_28.focus();
return false;
}
function getParameterValue(_2c,_2d){
for(var i=0;i<_2c.childNodes.length;i++){
if((_2c.childNodes[i].tagName.toUpperCase()=="PARAM")&&(_2c.childNodes[i].name.toUpperCase()==_2d.toUpperCase())){
return _2c.childNodes[i].value;
}
}
return null;
}
function createMediaEmbed(_2f,_30){
var _31=(_30.match(new RegExp(".mov$"))?"qt":"win");
var _32=document.createElement("EMBED");
_32.style.width=document.getElementById("mediaWidth").value;
_32.style.height=document.getElementById("mediaHeight").value;
if(_31=="qt"){
_32.setAttribute("type","movie/quicktime");
_32.setAttribute("pluginspage","http://www.apple.com/quicktime/download/");
}else{
_32.setAttribute("type","application/x-mplayer2");
_32.setAttribute("pluginspage","http://download.microsoft.com/download/winmediaplayer/nsplugin/6.4/WIN98/EN-US/wmpplugin.exe");
}
_32.setAttribute("src",_30);
_32.setAttribute("align",document.getElementById("mediaAlign").value);
var _33=GetProperties().Keys();
for(var i=0;i<_33.length;i++){
var _35=GetProperties().Item(_33[i]);
if(_35[1]!=null){
_32.setAttribute(_33[i],_35[1]);
}
}
if(_2f){
var _36=document.createElement("DIV");
_36.appendChild(_32);
return _36.innerHTML;
}else{
return _32;
}
}
function submitMediaFile(_37){
submitForUpload=true;
var _38=document.getElementById(FileUploadID);
if(trim(_38.value)==""){
alert(localization["AlertFile"]);
_38.focus();
submitForUpload=false;
}else{
document.getElementById(fileDirID).value=fileBrowser.CurrentItem.GetPath();
document.getElementById("loader").innerHTML=localization["Uploading"];
showObject("loader");
}
}
var isPreviewerInPreviewMode=false;
function MediaPreviewer(){
this.MediaPath="";
}
MediaPreviewer.prototype.Clear=function(){
this.SetDefaultValues(document.getElementById("PropertiesPane"));
};
MediaPreviewer.prototype.GetHtml=function(){
var _39="";
if(fileBrowser.SelectedItem){
_39=fileBrowser.SelectedItem.GetUrl();
}
if(_39!=""){
return createMediaEmbed(true,_39);
}else{
return null;
}
};
MediaPreviewer.prototype.LoadObjectFromPath=function(_3a){
};
MediaPreviewer.prototype.SetDefaultValues=function(_3b){
if(!_3b.attributes){
return;
}
if(_3b.attributes&&_3b.attributes["defaultvalue"]){
var _3c=_3b.attributes["defaultvalue"].value;
if(_3b.tagName&&_3b.tagName=="INPUT"){
var _3d=_3b.getAttribute("type",0).toLowerCase();
if(_3d=="radio"){
var _3e=_3b.getAttribute("name",2);
var _3f=document.getElementsByName(_3e);
for(i=0;i<_3f.length;i++){
var _40=_3f[i];
if(_40.getAttribute("value",2)==_3c){
_40.setAttribute("checked",true);
break;
}
}
}else{
if(_3d=="text"){
_3b.setAttribute("value",_3c);
}
}
}else{
if(_3b.tagName&&(_3b.tagName=="DIV"||_3b.tagName=="TD")){
_3b.innerHTML=_3c;
}else{
if(_3b.tagName&&_3b.tagName=="SELECT"){
selectOption(_3b,_3c);
}
}
}
}
if(_3b.childNodes&&_3b.childNodes.length>0){
for(var _41=0;_41<_3b.childNodes.length;_41++){
this.SetDefaultValues(_3b.childNodes[_41]);
}
}
};
MediaPreviewer.prototype.SwitchPreviewMode=function(_42,_43){
isPreviewerInPreviewMode=!isPreviewerInPreviewMode;
this.Preview(_42,_43);
};
MediaPreviewer.prototype.Preview=function(_44,_45){
if((_44!=null)&&(_44.Type!="D")){
if(_45==_44.GetUrl()){
this.PreviewCurrentMode(true);
}else{
this.PreviewCurrentMode(false);
}
}else{
this.Clear();
document.getElementById("PreviewObjectHolder").innerHTML="";
document.getElementById("PropertiesPane").style.display="none";
document.getElementById("PreviewPane").style.display="none";
document.getElementById("EmptyPane").style.display="inline";
}
};
MediaPreviewer.prototype.PreviewCurrentMode=function(_46){
document.getElementById("EmptyPane").style.display="none";
var _47=document.getElementById("PropertiesPane");
var _48=document.getElementById("PreviewPane");
var _49=document.getElementById("PreviewObjectHolder");
if(_46){
LoadMediaProperties();
}else{
this.Clear();
}
_49.innerHTML="";
if(isPreviewerInPreviewMode){
_47.style.display="none";
_48.style.display="inline";
_49.innerHTML=this.GetHtml();
}else{
_48.style.display="none";
_47.style.display="inline";
}
};
function LoadMediaProperties(){
if(dialogArgs.Media.tagName.toUpperCase()=="OBJECT"){
document.getElementById("mediaWidth").value=(dialogArgs.Media.width!="")?dialogArgs.Media.width:dialogArgs.Media.style.width;
document.getElementById("mediaHeight").value=(dialogArgs.Media.height!="")?dialogArgs.Media.height:dialogArgs.Media.style.height;
if(dialogArgs.Media.align!=null){
selectOption(document.getElementById("mediaAlign"),dialogArgs.Media.align);
}
for(var i=0;i<GetProperties().keys.length;i++){
var _4b=getParameterValue(dialogArgs.Media,GetProperties().keys[i]);
if(_4b!=null){
var _4c=parameters.Item(GetProperties.keys[i]);
_4c[1]=_4b;
}
}
}else{
if(dialogArgs.Media.tagName.toUpperCase()=="EMBED"){
document.getElementById("mediaWidth").value=(dialogArgs.Media.width!="")?dialogArgs.Media.width:dialogArgs.Media.style.width;
document.getElementById("mediaHeight").value=(dialogArgs.Media.height!="")?dialogArgs.Media.height:dialogArgs.Media.style.height;
if(dialogArgs.Media.align!=null){
selectOption(document.getElementById("mediaAlign"),dialogArgs.Media.align);
}
for(var i=0;i<GetProperties().keys.length;i++){
var _4b=dialogArgs.Media.getAttribute(GetProperties().keys[i]);
if(_4b!=null){
var _4c=GetProperties().Item(GetProperties().keys[i]);
_4c[1]=_4b;
}
}
}
}
}
function FitMedia(_4d){
var _4e=230;
var _4f=240;
var _50=_4d.height/_4e;
var _51=_4d.width/_4f;
_4d.setAttribute("OriginalWidth",_4d.width);
_4d.setAttribute("OriginalHeight",_4d.height);
if(_4d.width>_4f&&_4d.height>_4e){
var _52=(_50>=_51?_50:_51);
_4d.width=(_4d.width/_52);
_4d.height=(_4d.height/_52);
}else{
if(_4d.width>_4f){
_4d.width=(_4d.width/_51);
_4d.height=(_4d.height/_51);
}else{
if(_4d.height>_4e){
_4d.width=(_4d.width/_50);
_4d.height=(_4d.height/_50);
}
}
}
_4d.style.border="1px solid black";
}
function ShowActualMediaSize(){
var _53=GetPreviewedMedia();
if(!_53){
return;
}
_53.width=_53.getAttribute("OriginalWidth");
_53.height=_53.getAttribute("OriginalHeight");
}
function CallFitMedia(){
var _54=GetPreviewedMedia();
if(!_54){
return;
}
var _55=_54.getAttribute("OriginalWidth");
var _56=_54.getAttribute("OriginalHeight");
if(_55&&_55!=_54.width){
_54.width=_55;
}
if(_56&&_56!=_54.height){
_54.height=_56;
}
FitMedia(_54);
}
function GetPreviewedMedia(){
var _57=document.getElementById("PreviewArea");
if(!_57){
return null;
}
var _58=_57.getElementsByTagName("OBJECT");
if(_58&&_58.length>0){
return _58.item(0);
}else{
return null;
}
}
function ScaleMedia(_59,_5a){
var _5b=GetPreviewedMedia();
if(!_5b){
return;
}
_59=_59/100;
if(_5a){
_5b.width+=_5b.width*_59;
_5b.height+=_5b.height*_59;
}else{
_5b.width-=_5b.width*_59;
_5b.height-=_5b.height*_59;
}
};function PreviewerBase(){
this.PreviewedNode=null;
}
PreviewerBase.prototype.ChangePreviewedObject=function(_1){
this.PreviewedNode=_1;
};;function PropertyTextBox(id,_2,_3){
this.TextBox=document.getElementById(id);
this.Usage=_2;
this.Message=_3;
this.LastValidValue=this.TextBox.value;
if(!this.IsValueValid()){
this.TextBox.value="";
this.LastValidValue="";
}
var _4=this;
this.TextBox.onchange=function(e){
if(!e){
e=window.event;
}
if(!_4.ValidateText(e)){
return false;
}
};
}
PropertyTextBox.prototype.GetValue=function(){
return this.TextBox.value.replace(/^ */ig,"").replace(/ *$/ig,"");
};
PropertyTextBox.prototype.IsValueValid=function(){
var _6=this.GetValue();
var _7=true;
switch(this.Usage){
case "DIMENSION":
_7=(_6=="")||_6.match(/^[0-9]*((%)|(px))?$/ig);
break;
case "INT":
_7=(_6=="")||_6.match(/[0-9]/ig);
break;
case "FLOAT":
_7=!(isNaN(_6));
break;
}
return _7;
};
PropertyTextBox.prototype.ValidateText=function(e){
if(!this.IsValueValid()){
alert(this.Message);
this.TextBox.value=this.LastValidValue;
this.TextBox.focus();
if(e.stopPropagation){
e.stopPropagation();
}
e.returnValue=false;
e.cancelBubble=true;
return false;
}else{
this.LastValidValue=this.GetValue();
return true;
}
};;function SpinBox(_1){
this.ControlId=_1;
this.tbSize=document.getElementById(this.ControlId+"_textBox");
this.SizeValue="";
this.OnChangeValue=null;
this.AllowBlankValue=false;
}
SpinBox.prototype.Initialize=function(_2,_3,_4){
this.SizeValue=_2;
if(_2){
this.tbSize.value=_2;
}
if(_3){
this.tbSize.style.width=_3;
}
if(_4){
this.tbSize.maxLength=_4;
}
};
SpinBox.prototype.GetCurrentSize=function(){
return this.SizeValue;
};
SpinBox.prototype.SetSize=function(_5){
this.SizeValue=_5;
this.tbSize.value=_5;
this.RaiseChangeValue();
};
SpinBox.prototype.OnTextBoxKeyDown=function(e){
if(!e){
var e=window.event;
}
e.returnValue=this.IsKeyValid(e);
};
SpinBox.prototype.OnTextBoxKeyUp=function(e){
if(!e){
var e=window.event;
}
if(this.IsKeyValid(e,true)){
this.SizeValue=this.GetTextBoxValue();
this.RaiseChangeValue();
}
};
SpinBox.prototype.IsKeyValid=function(e,_9){
try{
if(!_9){
_9=false;
}
if(!e){
e=window.event;
}
var _a=((48<=e.keyCode&&e.keyCode<=57)||(96<=e.keyCode&&e.keyCode<=105)||(13==e.keyCode)||(8==e.keyCode)||(46==e.keyCode)||(9==e.keyCode));
if(!_9){
_a|=((35<=e.keyCode&&e.keyCode<=40));
}
return _a;
}
catch(ex){
return true;
}
};
SpinBox.prototype.GetTextBoxValue=function(){
var _b=-1;
if(null!=this.tbSize){
_b=parseInt(this.tbSize.value);
if(isNaN(_b)){
_b=-1;
}
}
return _b;
};
SpinBox.prototype.RaiseChangeValue=function(){
if(this.OnChangeValue){
this.OnChangeValue();
}
};
SpinBox.prototype.ModifyBorderSize=function(_c){
var _d=this.SizeValue;
var _e=this.GetTextBoxValue();
if(-1!=_e&&_d!=_e){
_d=_e;
}
if(_c&&_d<1000){
_d++;
}else{
if(!_c&&_d>0){
_d--;
}
}
this.SizeValue=_d;
this.tbSize.value=_d;
this.RaiseChangeValue();
};
SpinBox.prototype.ButtonOver=function(_f){
if(this.IsEnabled(_f)){
_f.className="Over";
}
};
SpinBox.prototype.ButtonOut=function(_10){
if(this.IsEnabled(_10)){
_10.className="";
}
};
SpinBox.prototype.IsEnabled=function(_11){
return (_11.className!="Disabled");
};;function StyleBuilder(){
this.StyleElements=radEditorGetStyleElements();
this.StyledObject=null;
}
StyleBuilder.prototype.Initialize=function(_1){
this.StyledObject=_1;
alert("Currently modified object tagName = "+this.StyledObject.tagName);
};
StyleBuilder.prototype.GetStyleText=function(){
return "border:1px solid red;";
};;function StyleBuilderCaller(Id){
this.Id=Id;
this.EditorObject=null;
this.StyledObject=null;
this.StyleTextHolder=document.getElementById(this.Id+"_styleTextHolder");
}
StyleBuilderCaller.prototype.Initialize=function(_2){
this.EditorObject=_2;
};
StyleBuilderCaller.prototype.SetStyledObject=function(_3){
this.StyledObject=_3;
this.StyleTextHolder.value=this.StyledObject.style.cssText;
};
StyleBuilderCaller.prototype.ShowDialog=function(){
var _4={StyleBuilderDialogCaller:this};
if(this.StyledObject==null){
alert("Object to set style to not set!!!");
return;
}
var _5={StyledObject:this.StyledObject};
var _6="";
if(this.EditorObject.UseSession==RadEditorNamespace.DIALOG_PARAMETERS_MODE_SESSION){
var _7=this.EditorObject.RadControlsDir.substr(this.EditorObject.ApplicationPath.length);
_6=this.EditorObject.ApplicationPath+this.EditorObject.SessionID1+_7;
}else{
_6=this.EditorObject.RadControlsDir;
}
return _6+this.EditorObject.ShowDialog(_6+"Editor/Dialog.aspx?dialog=StyleBuilderHolder"+"&editorID="+this.EditorObject.Id+"&skinPath="+this.EditorObject.SkinBasePath+"&useSession="+this.EditorObject.UseSession+"&sessionID2="+this.EditorObject.SessionID2+"&language="+this.EditorObject.Language,_5,400,300,StyleBuilderCallerSetCellStyleValue,_4);
return false;
};
StyleBuilderCaller.prototype.SetStyleText=function(_8){
this.StyleTextHolder.value=_8;
};
StyleBuilderCaller.prototype.GetStyleText=function(){
return this.StyleTextHolder.value;
};
function StyleBuilderCallerSetCellStyleValue(_9,_a){
if(_9){
_a.StyleBuilderDialogCaller.SetStyleText(_9);
}
window.focus();
};function Tab(){
this.Id="";
this.ElementId="";
this.Image=null;
this.ImageOver=null;
this.Text=null;
this.OnClientClick=null;
this.Enabled=null;
this.Selected=false;
this.Table=null;
}
function TabManager(_1){
this.Tabs=new Array();
this.SelectedTab=null;
this.ResizeControlId=_1;
this.Initialized=false;
}
TabManager.prototype={GetCoords:function(_2){
var _3=new Array(0,0);
if(_2.offsetParent){
while(_2.offsetParent){
_3[0]+=_2.offsetLeft;
_3[1]+=_2.offsetTop;
_2=_2.offsetParent;
if(_2==document.body){
_3[0]-=_2.offsetLeft;
_3[1]-=_2.offsetTop;
}
}
}
return _3;
},ResizeDialog:function(_4){
var _5=this.GetCoords(_4);
var _6=(_5[0]+_4.offsetWidth);
var _7=(_5[1]+_4.offsetHeight+45);
if(window.radWindow&&window.radWindow.SetWidth){
window.radWindow.SetSize(_6,_7);
try{
if(document.documentElement&&!document.all){
document.documentElement.scrollTop=0;
document.documentElement.scrollLeft=0;
document.body.scrollTop=0;
document.body.scrollLeft=0;
}
}
catch(e){
}
}else{
if(window.dialogWidth){
var _8=window.screenTop;
var _9=window.screenLeft;
window.dialogWidth=(_6+8)+"px";
window.dialogHeight=_7+"px";
window.dialogTop=_8-30;
window.dialogLeft=_9-4;
}else{
window.outerWidth=_6;
window.outerHeight=_7;
}
}
},GetTabByTable:function(_a){
for(var i=0;i<this.Tabs.length;i++){
if(this.Tabs[i].Id==_a.id){
if(!this.Tabs[i].Table){
this.Tabs[i].Table=_a;
}
return this.Tabs[i];
}
}
return null;
},AddTab:function(id,_d,_e,_f,_10,_11,_12){
var tab=new Tab();
tab.Id=id;
tab.ElementId=_d;
tab.Image=_e;
tab.ImageOver=_f;
tab.OnClientClick=_10;
tab.Enabled=_11;
tab.Selected=_12;
this.Tabs[this.Tabs.length]=tab;
},Initialize:function(){
if(this.Initialized){
return;
}
this.Initialized=true;
for(var i=0;i<this.Tabs.length;i++){
var _15=this.Tabs[i];
if(!_15.Table&&_15.Id){
_15.Table=document.getElementById(_15.Id);
}
if(_15.ElementId){
var _16=document.getElementById(_15.ElementId);
if(_16){
_16.style.display=(_15.Selected)?"block":"none";
}
}
}
this.SelectCurrentTab();
},SetTabEnabled:function(_17,_18){
if(!this.Initialized){
this.Initialize();
}
var tab=this.Tabs[_17];
if(!tab){
return;
}
if(_18){
tab.Enabled=true;
this.SetTabCss(tab,"");
}else{
tab.Enabled=false;
this.SetTabCss(tab,"Disabled");
}
},SetTabSelected:function(_1a){
if(!this.Initialized){
this.Initialize();
}
var tab=this.Tabs[_1a];
if(!tab){
return;
}else{
this.SelectTab(tab.Table);
}
},SetTabCss:function(tab,css){
if(!this.Initialized){
this.Initialize();
}
var _1e=tab.Table;
if(!_1e){
_1e=tab.Table=document.getElementById(tab.Id);
}
if(_1e){
var tds=_1e.getElementsByTagName("TD");
_1e.className="Tab"+css;
tds.item(0).className="TabLeft"+css;
tds.item(1).className="TabCenter"+css;
tds.item(2).className="TabRight"+css;
}
},ResizeWindow:function(){
var _20=document.getElementById(this.ResizeControlId);
if(_20){
this.ResizeDialog(_20);
}
},SelectTab:function(_21){
if(!this.Initialized){
this.Initialize();
}
var tab=this.GetTabByTable(_21);
if(!tab.Enabled){
return;
}
if(this.SelectedTab==tab){
return;
}
this.SetTabCss(tab,"Selected");
if(this.SelectedTab){
var _21=this.SelectedTab.Table;
var tds=_21.getElementsByTagName("TD");
this.SetTabCss(this.SelectedTab,"");
if(this.SelectedTab.ElementId){
var _24=document.getElementById(this.SelectedTab.ElementId);
if(_24){
_24.style.display="none";
}
}
}
this.SelectedTab=tab;
if(tab.ElementId){
var _24=document.getElementById(tab.ElementId);
if(_24){
_24.style.display="";
}
if(tab.OnClientClick){
eval(tab.OnClientClick);
}
this.ResizeWindow();
this.Refocus(_24);
}
},Refocus:function(_25){
try{
var _26=_25.getElementsByTagName("input");
var _27=null;
for(var i=0;i<_26.length;i++){
var inp=_26[i];
if(inp&&inp.type!="hidden"){
if(!_27){
_27=inp;
}
inp.setAttribute("tabindex",""+(i+1));
}
}
if(_27&&_27.focus){
_27.focus();
}
}
catch(ex){
}
},SelectCurrentTab:function(){
for(var i=0;i<this.Tabs.length;i++){
var _2b=this.Tabs[i];
if(_2b.Selected){
this.SelectTab(_2b.Table);
}
}
}};;function TableBorderControl(id,_2,_3){
this.Id=id;
this.TargetTable=null;
this.SpinBox=_3;
this.PreviewTable=document.getElementById(this.Id+"_PREVIEW");
this.BorderColorPicker=_2;
}
TableBorderControl.prototype.Initialize=function(_4,_5,_6){
this.TargetTable=_4;
if(this.TargetTable){
var _7=parseInt(this.TargetTable.border);
if(isNaN(_7)){
this.PreviewTable.removeAttribute("border");
this.PreviewTable.className="TableBorderControlPreviewNoBorder";
}else{
this.PreviewTable.border=_7;
}
}
this.PreviewTable.rules=this.TargetTable?this.TargetTable.rules:"all";
this.PreviewTable.frame=this.TargetTable?this.TargetTable.frame:"border";
var _8=(this.TargetTable?this.TargetTable.getAttribute("borderColor"):"");
if(!_8){
_8="";
}
this.PreviewTable.setAttribute("borderColor",_8);
this.SpinBox.Initialize(this.PreviewTable.border,18,4);
var _9=this;
this.SpinBox.OnChangeValue=function anon(){
_9.SetBorderSize(this.GetCurrentSize());
};
this.BorderColorPicker.CanAddCustomColor=_6;
this.BorderColorPicker.CanAddHexColor=_6;
this.BorderColorPicker.SetColors(_5);
this.BorderColorPicker.OnClientClick=function anon(){
_9.SetBorderColor(this.SelectedColor);
};
this.OnColorSelected=null;
this.BorderColorPicker.SelectColor(this.PreviewTable.getAttribute("borderColor"));
};
TableBorderControl.prototype.UpdateTarget=function(){
if(null!=this.TargetTable&&null!=this.PreviewTable){
var _a=this.GetFrame();
if(_a){
this.TargetTable.frame=_a;
}else{
this.TargetTable.removeAttribute("frame");
}
var _b=this.GetRules();
if(_b){
this.TargetTable.rules=_b;
}else{
this.TargetTable.removeAttribute("rules");
}
if(this.PreviewTable.border!=""){
this.TargetTable.border=this.PreviewTable.border;
}else{
this.TargetTable.removeAttribute("border");
}
var _c=this.PreviewTable.getAttribute("borderColor");
if(!_c){
_c="";
}
this.TargetTable.setAttribute("borderColor",_c);
}
};
TableBorderControl.prototype.SetBorderColor=function(_d){
if(null!=this.PreviewTable){
if(""!=_d&&0==parseInt(this.PreviewTable.border)){
this.SpinBox.SetSize(1);
}
this.PreviewTable.setAttribute("borderColor",_d);
}
};
TableBorderControl.prototype.GetFrame=function(){
if(null!=this.PreviewTable&&"border"!=this.PreviewTable.frame&&"box"!=this.PreviewTable.frame){
return this.PreviewTable.frame;
}else{
return "";
}
};
TableBorderControl.prototype.GetRules=function(){
if(null!=this.PreviewTable&&"all"!=this.PreviewTable.rules){
return this.PreviewTable.rules;
}else{
return "";
}
};
TableBorderControl.prototype.GetBorderSize=function(){
if(null!=this.PreviewTable){
return this.PreviewTable.border;
}else{
return 0;
}
};
TableBorderControl.prototype.SetFrame=function(_e){
if(null!=this.PreviewTable){
if("void"!=_e&&""!=_e&&0==parseInt(this.PreviewTable.border)){
this.SpinBox.SetSize(1);
}
this.PreviewTable.frame=_e;
}
};
TableBorderControl.prototype.SetRules=function(_f){
if(null!=this.PreviewTable){
if("none"!=_f&&""!=_f&&0==parseInt(this.PreviewTable.border)){
this.SpinBox.SetSize(1);
}
this.PreviewTable.rules=_f;
}
};
TableBorderControl.prototype.SetBorderSize=function(_10){
if(null!=this.PreviewTable){
if(_10<0){
this.PreviewTable.removeAttribute("border");
}else{
if(_10>1000){
alert(localization["BORDER_SIZE_OVERFLOW"]);
_10=1000;
}
this.PreviewTable.border=_10;
}
this.PreviewTable.className=(this.PreviewTable.border>0)?"TableBorderControlPreview":"TableBorderControlPreviewNoBorder";
}
};;function TableDesignControl(id){
this.Id=id;
this.SelectedCell=null;
this.CurrentRowSpan=1;
this.CurrentColSpan=1;
this.SelectedCell=null;
this.SelectedCellIndex=-1;
this.VictimColumns=new Array();
this.VictimRows=new Array();
this.RowsCount=0;
}
TableDesignControl.prototype.Initialize=function(_2,_3){
this.TableToModify=_2;
this.TablePreviewControl=new TablePreviewControl(document.getElementById(this.Id+"_PreviewTableHolder"));
this.RowsCount=this.TableToModify.rows.length;
this.SelectedCell=_3;
this.SelectedCellIndex=0;
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
var _4=this;
this.TablePreviewControl.OnSelectedCellChanged=function anon(){
_4.OnSelectedCellChanged();
};
this.CheckButtonAvailability();
};
TableDesignControl.prototype.OnSelectedCellChanged=function(){
this.SynchronizeSelectedCell();
this.CheckButtonAvailability();
};
TableDesignControl.prototype.SynchronizeSelectedCell=function(){
var _5=this.TablePreviewControl.GetSelectedCell();
var _6=this.TablePreviewControl.GetPreviewTable();
var _7=_6.rows;
this.SelectedCell=null;
this.SelectedCellIndex=-1;
for(var i=0;i<_7.length;i++){
var _9=_7[i].cells;
for(var j=0;j<_9.length;j++){
if(_9[j]==_5){
this.SelectedCell=this.TableToModify.rows[i].cells[j];
this.SelectedCellIndex=j;
return;
}
}
}
};
TableDesignControl.prototype.GetEquivalentModelTableCell=function(_b){
var _c=this.TablePreviewControl.GetPreviewTable();
var _d=_c.rows;
for(var i=0;i<_d.length;i++){
var _f=_d[i].cells;
for(var j=0;j<_f.length;j++){
if(_f[j]==_b){
return this.TableToModify.rows[i].cells[j];
}
}
}
};
TableDesignControl.prototype.GetRowSpan=function(_11){
if(!_11){
return;
}
return _11.rowSpan>0?_11.rowSpan:1;
};
TableDesignControl.prototype.GetColSpan=function(_12){
if(!_12){
return;
}
return _12.colSpan>0?_12.colSpan:1;
};
TableDesignControl.prototype.CheckButtonAvailability=function(){
var _13=this.TablePreviewControl.GetPreviewTable();
var _14=this.TablePreviewControl.GetSelectedCell();
if(this.GetNamedNodesLength(_13.firstChild,"TR")>1){
this.EnableControl(document.getElementById(this.Id+"_delRow"));
}else{
this.DisableControl(document.getElementById(this.Id+"_delRow"));
}
if(this.GetMaxColumns()>1){
this.EnableControl(document.getElementById(this.Id+"_delCol"));
}else{
this.DisableControl(document.getElementById(this.Id+"_delCol"));
}
var _15=this.FindNextNamedSibling(_14,"TD");
if((_14)&&(_15)&&(_14.offsetTop==_15.offsetTop)&&((_14.offsetLeft+_14.offsetWidth+1)==_15.offsetLeft)){
var i;
var _17=_13.rows;
var _18=_15;
this.CurrentColSpan=this.GetColSpan(_18);
this.VictimColumns=new Array(this.GetEquivalentModelTableCell(_18));
for(i=this.GetRowSpan(_18);i<this.GetRowSpan(_14);i+=this.GetRowSpan(_18)){
_18=this.GetCellByOffset(this.FindNextNamedSibling(_14.parentNode,"TR"),_15.offsetLeft);
this.VictimColumns[this.VictimColumns.length]=this.GetEquivalentModelTableCell(_18);
if(this.GetColSpan(_18)!=this.CurrentColSpan){
i=0;
break;
}
}
if(this.GetRowSpan(_14)==i){
this.EnableControl(document.getElementById(this.Id+"_addColSpan"));
}else{
this.DisableControl(document.getElementById(this.Id+"_addColSpan"));
}
}else{
this.DisableControl(document.getElementById(this.Id+"_addColSpan"));
}
if((_14)&&(_14.colSpan>1)){
this.EnableControl(document.getElementById(this.Id+"_delColSpan"));
}else{
this.DisableControl(document.getElementById(this.Id+"_delColSpan"));
}
try{
var row=_13.rows[this.GetNamedNodeIndex(_14.parentNode,"TR")+this.GetRowSpan(_14)];
if((_14)&&(row)){
var _18=this.GetCellByOffset(row,_14.offsetLeft);
if(_18){
var i;
var _17=_13.rows;
this.CurrentRowSpan=this.GetRowSpan(_18);
this.VictimRows=new Array(this.GetEquivalentModelTableCell(_18));
for(i=this.GetColSpan(_18);i<this.GetColSpan(_14);i+=this.GetColSpan(_18)){
_18=this.FindNextNamedSibling(_18,"TD");
this.VictimRows[this.VictimRows.length]=this.GetEquivalentModelTableCell(_18);
if(this.GetRowSpan(_18)!=this.CurrentRowSpan){
i=0;
break;
}
}
if(this.GetColSpan(_14)==i){
this.EnableControl(document.getElementById(this.Id+"_addRowSpan"));
}else{
this.DisableControl(document.getElementById(this.Id+"_addRowSpan"));
}
}else{
this.DisableControl(document.getElementById(this.Id+"_addRowSpan"));
}
}else{
this.DisableControl(document.getElementById(this.Id+"_addRowSpan"));
}
}
catch(exc){
this.DisableControl(document.getElementById(this.Id+"_addRowSpan"));
}
if((_14)&&(this.GetRowSpan(_14)>1)){
this.EnableControl(document.getElementById(this.Id+"_delRowSpan"));
}else{
this.DisableControl(document.getElementById(this.Id+"_delRowSpan"));
}
};
TableDesignControl.prototype.GetNamedNodesLength=function(_1a,_1b){
var _1c=0;
for(var i=0;i<_1a.childNodes.length;i++){
if(_1a.childNodes[i].nodeName==_1b){
_1c++;
}
}
return _1c;
};
TableDesignControl.prototype.EnableControl=function(_1e){
if(!this.IsEnabled(_1e)){
_1e.className="";
}
};
TableDesignControl.prototype.DisableControl=function(_1f){
if(this.IsEnabled(_1f)){
_1f.className="Disabled";
}
};
TableDesignControl.prototype.IsEnabled=function(_20){
if(_20.className!="Disabled"){
return true;
}else{
return false;
}
};
TableDesignControl.prototype.OnButtonOver=function(_21){
if(this.IsEnabled(_21)){
_21.className="Over";
}
};
TableDesignControl.prototype.OnButtonOut=function(_22){
if(this.IsEnabled(_22)){
_22.className="";
}
};
TableDesignControl.prototype.GetMaxColumns=function(){
var _23=0;
var _24=this.TableToModify.rows[0];
if(_24){
var _25=_24.cells;
for(var i=0;i<_25.length;i++){
var _27=_25[i];
var _28=_27.colSpan>0?_27.colSpan:1;
_23+=_28;
}
}
return _23;
};
TableDesignControl.prototype.FindFirstNamedChild=function(_29,_2a,_2b){
var _2c=_29.childNodes;
for(var i=_2a;i<_2c.length;i++){
if(_2c[i].nodeName==_2b){
return _2c[i];
}
}
return null;
};
TableDesignControl.prototype.FindNextNamedSibling=function(_2e,_2f){
if(_2e!=null){
var _30=_2e.nextSibling;
while(_30!=null){
if(_30.nodeName==_2f){
return _30;
}
_30=_30.nextSibling;
}
}
return null;
};
TableDesignControl.prototype.GetCellByOffset=function(row,_32){
var _33=row.cells;
for(var i=0;i<_33.length;i++){
if(_33[i].offsetLeft==_32){
return _33[i];
}
}
return null;
};
TableDesignControl.prototype.GetNamedNodeIndex=function(_35,_36){
if(_35.parentNode&&_35.parentNode.childNodes){
var _37=_35.parentNode.childNodes;
var _38=0;
for(var i=0;i<_37.length;i++){
if(_37[i]==_35){
return _38;
}else{
if(_37[i].nodeName==_36){
_38++;
}
}
}
}
return -1;
};
TableDesignControl.prototype.RemoveNamedChild=function(_3a,_3b,_3c){
var _3d=0;
for(var i=0;i<_3a.childNodes.length;i++){
if(_3a.childNodes[i].nodeName==_3b){
if((_3d==_3c)||(i==_3a.childNodes.length-1)){
_3a.removeChild(_3a.childNodes[i]);
break;
}
_3d++;
}
}
};
TableDesignControl.prototype.GetIndexByOffset=function(row,_40){
var _41=row.cells;
for(var i=0;i<_41.length;i++){
if(_40>=_41[i].offsetLeft){
return i;
}
}
return _41.length;
};
TableDesignControl.prototype.DeleteLastColumn=function(_43){
if(this.IsEnabled(_43)){
var _44=this.TablePreviewControl.GetPreviewTable().rows;
var _45=this.TableToModify.rows;
for(var i=0;i<this.RowsCount;i++){
var _47=_44[i].cells;
var _48=_45[i].cells;
if((_47.length>1)||((_47.length==1)&&(_47[0].offsetLeft!=(this.TablePreviewControl.GetPreviewTable().cellSpacing+1)))){
if(_47[_47.length-1].colSpan>1){
_47[_47.length-1].colSpan--;
_48[_48.length-1].colSpan--;
}else{
this.RemoveNamedChild(_44[i],"TD",_44[i].getElementsByTagName("TD").length-1);
this.RemoveNamedChild(_45[i],"TD",_45[i].getElementsByTagName("TD").length-1);
}
}
}
this.CheckButtonAvailability();
}
};
TableDesignControl.prototype.DeleteLastRow=function(_49){
if(this.IsEnabled(_49)){
var _4a=this.TablePreviewControl.GetPreviewTable();
var _4b=_4a.rows;
var _4c=this.TableToModify.rows;
for(var i=0;i<this.RowsCount;i++){
var _4e=_4b[i].cells;
var _4f=_4c[i].cells;
for(var j=0;j<_4e.length;j++){
if((this.GetRowSpan(_4e[j])>1)&&((i+this.GetRowSpan(_4e[j]))==this.RowsCount)){
if(_4e[j].rowSpan>0){
_4e[j].rowSpan--;
}
if(_4f[j].rowSpan>0){
_4f[j].rowSpan--;
}
}
}
}
this.RemoveNamedChild(_4a.firstChild,"TR",this.RowsCount-1);
this.RemoveNamedChild(this.TableToModify.firstChild,"TR",this.RowsCount-1);
this.RowsCount--;
this.CheckButtonAvailability();
}
};
TableDesignControl.prototype.AddNewColumn=function(){
var _51=this.TableToModify.rows;
for(var i=0;i<this.RowsCount;i++){
var _53=_51[i].insertCell((this.SelectedCellIndex==0)?this.SelectedCellIndex+1:this.SelectedCellIndex);
_53.innerHTML="&nbsp;";
}
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
this.CheckButtonAvailability();
};
TableDesignControl.prototype.AddNewRow=function(){
var _54=this.TableToModify.insertRow(-1);
for(var i=0;i<this.GetMaxColumns();i++){
var _56=_54.insertCell(-1);
_56.innerHTML="&nbsp;";
}
this.RowsCount++;
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
this.CheckButtonAvailability();
};
TableDesignControl.prototype.IncreaseColSpan=function(_57){
if(this.IsEnabled(_57)){
for(i=0;i<this.VictimColumns.length;i++){
var row=this.VictimColumns[i].parentNode;
var _59=this.GetNamedNodeIndex(this.VictimColumns[i],"TD");
if(_59>=0){
row.removeChild(row.childNodes[_59]);
}
}
var _5a=this.CurrentColSpan>0?this.CurrentColSpan:1;
_5a+=this.GetColSpan(this.SelectedCell);
this.SelectedCell.colSpan=_5a;
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
this.CheckButtonAvailability();
}
};
TableDesignControl.prototype.DecreaseColSpan=function(_5b){
if(this.IsEnabled(_5b)){
var row=this.SelectedCell.parentNode;
for(var i=0;i<this.GetRowSpan(this.SelectedCell);i++){
try{
row.insertCell(this.SelectedCellIndex==0?this.SelectedCellIndex+1:this.SelectedCellIndex);
}
catch(ex){
row.insertCell(0);
}
row=this.FindNextNamedSibling(row,"TR");
}
if(this.SelectedCell.colSpan>1){
this.SelectedCell.colSpan--;
}
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
this.CheckButtonAvailability();
}
};
TableDesignControl.prototype.IncreaseRowSpan=function(_5e){
if(this.IsEnabled(_5e)){
for(i=0;i<this.VictimRows.length;i++){
this.VictimRows[i].parentNode.removeChild(this.VictimRows[i]);
}
var _5f=this.CurrentRowSpan>0?this.CurrentRowSpan:1;
_5f+=this.GetRowSpan(this.SelectedCell);
this.SelectedCell.rowSpan=_5f;
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
this.CheckButtonAvailability();
}
};
TableDesignControl.prototype.DecreaseRowSpan=function(_60){
if(this.IsEnabled(_60)){
var row=this.TableToModify.rows[this.GetNamedNodeIndex(this.SelectedCell.parentNode,"TR")+this.GetRowSpan(this.SelectedCell)-1];
if(this.SelectedCell.rowSpan>1){
this.SelectedCell.rowSpan--;
}
for(var i=0;i<this.GetColSpan(this.SelectedCell);i++){
row.insertCell(0);
}
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
this.CheckButtonAvailability();
}
};;function TablePreviewControl(_1){
this.PreviewHolder=_1;
this.PreviewHolder.innerHTML="";
var _2=document.createElement("TABLE");
this.PreviewHolder.appendChild(_2);
this.PreviewTable=_2;
this.selectedCells=[];
this.AllowMultiCellSelection=false;
}
TablePreviewControl.prototype.UpdateTable=function(_3,_4){
var _5=document.createElement("TABLE");
_5.style.width="328px";
_5.style.height="250px";
_5.cellPadding=1;
_5.cellSpacing=1;
for(var i=0;i<_3.rows.length;i++){
var _7=_3.rows[i];
var _8=_5.insertRow(-1);
for(var j=0;j<_7.cells.length;j++){
var _a=_7.cells[j];
var _b=_8.insertCell(-1);
_b.rowSpan=_a.rowSpan;
_b.colSpan=_a.colSpan;
if(_a==_4){
this.SelectCell(_b);
}else{
this.DeSelectCell(_b);
}
_b.theTablePreviewControl=this;
_b.onclick=this.HandleCellClick;
_b.innerHTML="&nbsp;&nbsp;";
}
}
this.PreviewTable.parentNode.replaceChild(_5,this.PreviewTable);
this.PreviewTable=_5;
};
TablePreviewControl.prototype.ChangeSelectedCell=function(_c){
this.SetEditedCells(this.GetSelectedCells());
var _d=this.IsCellSelected(_c);
var _e=this.IsMultiCellSelection();
this.DeSelectAllCells();
if(!_d||_e){
this.SelectCell(_c);
}
if(this.OnSelectedCellChanged){
this.OnSelectedCellChanged();
}
};
TablePreviewControl.prototype.HandleCellClick=function(e){
if(!e){
e=window.event;
}
var _10=this.theTablePreviewControl;
var _11=RadUtil_GetEventSource(e);
if(_10.AllowMultiCellSelection&&e.ctrlKey){
if(_10.IsCellSelected(_11)){
_10.DeSelectCell(_11);
}else{
_10.SelectCell(_11);
}
}else{
_10.ChangeSelectedCell(_11);
}
};
TablePreviewControl.prototype.GetPreviewTable=function(){
return this.PreviewTable;
};
TablePreviewControl.prototype.SelectCell=function(_12){
this.selectedCell=_12;
var _13=_12.parentNode.rowIndex;
var _14=GetCellIndex(_12);
if(typeof (this.selectedCells[_13])=="undefined"){
this.selectedCells[_13]=[];
}
this.selectedCells[_13][_14]=_12;
_12.className="TableDialogSelectedCell";
};
TablePreviewControl.prototype.DeSelectCell=function(_15){
if(this.IsCellSelected(_15)){
var _16=_15.parentNode.rowIndex;
var _17=GetCellIndex(_15);
this.selectedCells[_16][_17]=null;
if(_15==this.selectedCell){
this.selectedCell=null;
}
}
_15.className="TableDialogCell";
};
TablePreviewControl.prototype.IsCellSelected=function(_18){
return (_18.className=="TableDialogSelectedCell");
};
TablePreviewControl.prototype.GetSelectedCell=function(){
var _19=this.GetSelectedCells();
if(_19&&_19.length>0){
return this.GetSelectedCells()[_19.length-1];
}
};
TablePreviewControl.prototype.SetEditedCells=function(_1a){
this.editedCells=_1a;
};
TablePreviewControl.prototype.GetEditedCells=function(){
return this.editedCells;
};
TablePreviewControl.prototype.GetSelectedCells=function(){
var _1b=[];
for(var _1c in this.selectedCells){
for(var _1d in this.selectedCells[_1c]){
_1b.push(this.selectedCells[_1c][_1d]);
}
}
return _1b;
};
TablePreviewControl.prototype.DeSelectAllCells=function(){
var _1e=this.GetSelectedCells();
for(var i=0;i<_1e.length;i++){
this.DeSelectCell(_1e[i]);
}
this.selectedCells=[];
};
TablePreviewControl.prototype.IsMultiCellSelection=function(){
return (this.GetSelectedCells().length>1);
};;function TablePropertiesControl(id,_2,_3,_4,_5,_6,_7,_8,_9,_a){
this.Id=id;
this.SkinPath=_2;
this.BgColorPicker=_3;
this.AlignmentSelector=_4;
this.TableBorderControl=_5;
this.BgImageDialogCaller=_6;
this.CssClassSelector=_7;
this.CellSpacingSpinBox=_8;
this.CellPaddingSpinBox=_9;
this.StyleBuilderCaller=_a;
this.ColorsArray=null;
this.AllowCustomColors=true;
this.TableWidthHolder=document.getElementById(this.Id+"_tableWidth");
this.TableHeightHolder=document.getElementById(this.Id+"_tableHeight");
this.TableWidthBox=new PropertyTextBox(this.TableWidthHolder.id,"DIMENSION",localization["InvalidTableWidth"]);
this.TableHeightBox=new PropertyTextBox(this.TableHeightHolder.id,"DIMENSION",localization["InvalidTableHeight"]);
this.TableStyleValueHolder=document.getElementById(this.Id+"_tableStyleValue");
this.IdHolder=document.getElementById(this.Id+"_idHolder");
this.Initialized=false;
}
TablePropertiesControl.prototype.Initialize=function(_b,_c,_d,_e,_f){
if(!this.Initialized){
this.ColorsArray=_e;
this.EditorObject=_d;
this.TableToModify=_b;
this.AvailableCssClasses=_c;
this.AllowCustomColors=_f;
this.CssClassSelector.Initialize(this.AvailableCssClasses);
this.BgImageDialogCaller.Initialize(this.EditorObject);
this.StyleBuilderCaller.Initialize(this.EditorObject);
this.BgColorPicker.CanAddCustomColor=this.AllowCustomColors;
this.BgColorPicker.CanAddHexColor=this.AllowCustomColors;
this.BgColorPicker.SetColors(this.ColorsArray);
this.Initialized=true;
this.LoadValues(this.TableToModify);
}
};
TablePropertiesControl.prototype.LoadValues=function(_10){
var _11=this.TableToModify=_10;
this.TableWidthHolder.value=_11.style.width?_11.style.width:(_11.width?_11.width:"");
this.TableHeightHolder.value=_11.style.height?_11.style.height:(_11.height?_11.height:"");
this.IdHolder.value=_11.getAttribute("id")?_11.getAttribute("id"):"";
this.AlignmentSelector.SelectAlignment(_11.align);
this.CellSpacingSpinBox.Initialize(_11.cellSpacing,50,4);
this.CellPaddingSpinBox.Initialize(_11.cellPadding,50,4);
this.BgColorPicker.Enable(true);
this.BgColorPicker.SelectColor(_11.bgColor.toUpperCase());
this.TableBorderControl.Initialize(_11,this.ColorsArray,this.AllowCustomColors);
this.CssClassSelector.SelectCssClass(_11.className);
this.StyleBuilderCaller.SetStyledObject(_11);
if(this.BgImageDialogCaller&&_11){
var _12=this.TableToModify.getAttribute("background");
if(!_12){
_12="";
}
this.BgImageDialogCaller.SetImagePath(_12);
}
};
TablePropertiesControl.prototype.UpdateTable=function(){
var _13=this.TableToModify;
_13.style.cssText=this.StyleBuilderCaller.GetStyleText();
if(_13.style.cssText==""){
_13.removeAttribute("style",false);
}
if(!this.TableWidthBox.IsValueValid()){
return false;
}
var _14=this.TableWidthBox.GetValue();
_13.removeAttribute("width",false);
_13.style.width=_14?ConvertIntToPixel(_14):"";
if(!this.TableHeightBox.IsValueValid()){
return false;
}
var _15=this.TableHeightBox.GetValue();
_13.removeAttribute("height",false);
_13.style.height=_15?ConvertIntToPixel(_15):"";
this.SetAttribValue("id",this.IdHolder.value);
this.SetAttribValue("align",this.AlignmentSelector.GetAlign());
var _16=this.CellSpacingSpinBox.GetCurrentSize();
this.SetAttribValue("cellSpacing",_16>=0?_16:"",(_16>=0));
var _17=this.CellPaddingSpinBox.GetCurrentSize();
this.SetAttribValue("cellPadding",_17>=0?_17:"",(_17>=0));
this.SetAttribValue("bgColor",this.BgColorPicker.SelectedColor);
this.SetAttribValue("background",this.BgImageDialogCaller.GetImagePath());
var _18=document.all?"className":"class";
this.SetAttribValue(_18,this.CssClassSelector.GetSelectedClassName());
this.TableBorderControl.UpdateTarget();
return true;
};
TablePropertiesControl.prototype.SetAttribValue=function(_19,_1a,_1b){
if(_1a||(true==_1b)){
this.TableToModify.setAttribute(_19,_1a);
}else{
this.TableToModify.removeAttribute(_19,false);
}
};;function TableWizard(id,_2,_3,_4,_5){
this.Id=id;
this.TableDesignControl=_2;
this.TablePropertiesControl=_3;
this.TablePreviewControl=new TablePreviewControl(document.getElementById(this.Id+"_CellPropertiesPreviewTableHolder"));
this.CellPropertiesControl=_4;
this.AccessibleTableControl=_5;
this.CurrentlyEditedCell=null;
this.CssClasses=null;
this.CellCssClasses=null;
this.EditorObject=null;
}
TableWizard.prototype={Initialize:function(_6,_7,_8,_9,_a,_b){
this.ColorsArray=_a;
this.AllowCustomColors=_b;
this.CssClasses=_7;
this.CellCssClasses=_8;
this.TableToModify=_6;
this.OriginalTableClone=_6.cloneNode(true);
this.SelectedCell=null;
this.SelectedCellIndex=-1;
this.EditorObject=_9;
this.InitTableProperties(this.EditorObject);
},GetCurrentlyEditedCell:function(){
return this.CurrentlyEditorCell;
},OnCellPropertiesSelectedCellChanged:function(){
var _c=this.TablePreviewControl.GetEditedCells();
this.CellPropertiesControl.UpdateMultiple(this.GetModifyCellsForPreviewCells(_c));
this.SynchronizeSelectedCell();
if(this.SelectedCell){
this.CurrentlyEditedCell=this.SelectedCell;
this.CellPropertiesControl.LoadPropertyValues(this.SelectedCell);
}
},OnCellPropertiesSelectedMultipleCells:function(){
this.CellPropertiesControl.Clear();
},SynchronizeSelectedCell:function(){
var _d=this.TablePreviewControl.GetSelectedCell();
var _e=this.TablePreviewControl.GetPreviewTable();
var _f=_e.rows;
this.SelectedCell=null;
this.SelectedCellIndex=-1;
for(var i=0;i<_f.length;i++){
var _11=_f[i].cells;
for(var j=0;j<_11.length;j++){
if(_11[j]==_d){
this.SelectedCell=this.TableToModify.rows[i].cells[j];
this.SelectedCellIndex=j;
return;
}
}
}
},GetModifyCellForPreviewCell:function(_13){
var _14=_13.parentNode.rowIndex;
var _15=GetCellIndex(_13);
return this.TableToModify.rows[_14].cells[_15];
},GetModifyCellsForPreviewCells:function(_16){
var _17=[];
for(var i=0;i<_16.length;i++){
_17.push(this.GetModifyCellForPreviewCell(_16[i]));
}
return _17;
},InitCellProperties:function(){
var _19=this;
this.TablePreviewControl.OnSelectedCellChanged=function anon(){
_19.OnCellPropertiesSelectedCellChanged();
};
this.TablePreviewControl.OnSelectedMultipleCells=function anon(){
_19.OnCellPropertiesSelectedMultipleCells();
};
this.TablePreviewControl.AllowMultiCellSelection=true;
this.InitSelectedCell();
this.TablePreviewControl.UpdateTable(this.TableToModify,this.SelectedCell);
this.CurrentlyEditedCell=this.SelectedCell;
this.CellPropertiesControl.Initialize(this.CurrentlyEditedCell,this.CellCssClasses,this.EditorObject,this.ColorsArray,this.AllowCustomColors);
},SaveLastEditedCellProperties:function(){
if(this.CurrentlyEditedCell){
var _1a=this.TablePreviewControl.GetSelectedCells();
if(!this.CellPropertiesControl.UpdateMultiple(this.GetModifyCellsForPreviewCells(_1a))){
return false;
}
this.SelectedCell=null;
this.SelectedCellIndex=-1;
}
return true;
},InitTableProperties:function(){
this.SaveLastEditedCellProperties();
this.TablePropertiesControl.Initialize(this.TableToModify,this.CssClasses,this.EditorObject,this.ColorsArray,this.AllowCustomColors);
},InitAccessibleTable:function(){
this.SaveLastEditedCellProperties();
this.AccessibleTableControl.Initialize(this.TableToModify,this.TableToModify.document);
},InitSelectedCell:function(){
if(this.TableToModify.rows.length>0){
if(this.TableToModify.rows[0].cells.length>0){
this.SelectedCell=this.TableToModify.rows[0].cells[0];
}
}
},InitDesigner:function(){
this.SaveLastEditedCellProperties();
this.InitSelectedCell();
this.TableDesignControl.Initialize(this.TableToModify,this.SelectedCell);
},InsertTable:function(){
if(!this.SaveLastEditedCellProperties()){
return;
}
this.AccessibleTableControl.UpdateTable();
if(!this.TablePropertiesControl.UpdateTable()){
return;
}
CloseDlg(this.TableToModify);
},RestoreOriginalTable:function(){
if(this.TableToModify&&this.TableToModify.parentNode){
this.TableToModify.parentNode.replaceChild(this.OriginalTableClone,this.TableToModify);
}
this.TableToModify=this.OriginalTableClone;
}};;TemplateManager.prototype=new BrowserDialogBase();
function TemplateManager(_1,_2,_3){
BrowserDialogBase.call(this,_1,_2,_3);
}
TemplateManager.prototype.GetReturnResult=function(){
return this.Previewer.GetTemplateHtml();
};
TemplateManager.prototype.IsTemplateChosen=function(){
return this.FileBrowser.SelectedItem.Type!="D";
};
function submitTemplateFile(_4){
submitForUpload=true;
var _5=document.getElementById(FileUploadID);
if(trim(_5.value)==""){
alert(localization["AlertFile"]);
_5.focus();
submitForUpload=false;
}else{
document.getElementById(fileDirID).value=fileBrowser.CurrentItem.GetPath();
document.getElementById("loader").innerHTML=localization["Uploading"];
showObject("loader");
}
};TemplatePreviewer.prototype=new PreviewerBase;
function TemplatePreviewer(Id){
this.Id=Id;
this.PreviewArea=document.getElementById(this.Id+"_previewArea");
PreviewerBase.call(this);
}
TemplatePreviewer.prototype.ChangePreviewedObject=function(_2){
this.PreviewedItem=_2;
if(this.PreviewedItem.Type=="D"){
this.Clear();
}else{
this.ShowTemplate(this.PreviewedItem.GetUrl());
}
};
TemplatePreviewer.prototype.ShowTemplate=function(_3){
this.PreviewArea.src=_3;
};
TemplatePreviewer.prototype.GetTemplateHtml=function(){
var _4=this.PreviewArea.document;
return this.PreviewArea.contentWindow.document.body.innerHTML;
};
TemplatePreviewer.prototype.Clear=function(){
this.PreviewArea.src="javascript:''";
};;function ThumbLinkOptionSetter(id){
this.Id=id;
this.MainTable=document.getElementById("MainTable_"+this.Id);
this.LinkSpecifier=document.getElementById("cbLink_"+this.Id);
this.TargetSpecifier=document.getElementById("cbTarget_"+this.Id);
this.SetVisibility(false);
this.TargetSpecifier.disabled=true;
var _2=this;
this.LinkSpecifier.onclick=function(){
_2.LinkSpecifier_Clicked();
};
if(typeof (GetDisposeManager)!="undefined"){
GetDisposeManager().Add(this);
}
}
ThumbLinkOptionSetter.prototype.Dispose=function(){
this.LinkSpecifier.onclick=null;
this.LinkSpecifier=null;
this.MainTable=null;
this.TargetSpecifier=null;
};
ThumbLinkOptionSetter.prototype.SetVisibility=function(_3){
this.MainTable.style.display=_3?"":"none";
this.IsVisible=_3;
};
ThumbLinkOptionSetter.prototype.LinkSpecifier_Clicked=function(){
this.TargetSpecifier.disabled=!this.LinkSpecifier.checked;
};
ThumbLinkOptionSetter.prototype.GetOptions=function(){
var _4={LinkToImage:this.IsVisible&&this.LinkSpecifier.checked,TargetToNew:this.IsVisible&&this.LinkSpecifier.checked&&this.TargetSpecifier.checked};
return _4;
};;function ThumbnailCreator(Id,_2,_3,_4,_5,_6,_7,_8,_9){
this.Id=Id;
this.RealImage="";
this.RealImagePath="";
this.ImageName="";
this.Width=0;
this.Height=0;
this.RealImageWidth=0;
this.RealImageHeight=0;
this.Constrain=true;
this.IsUsingPercents=false;
this.OverwriteExisting=false;
this.DimentionUnit="pixel";
this.WidthHolder=_2;
this.HeightHolder=_3;
this.NewImageNameHolder=_4;
this.OriginalFileLocationHolder=_5;
this.ConstrainHolder=_6;
this.DimentionUnitHolder=_7;
this.CreateButton=_8;
this.OverwriteExistingHolder=_9;
this.MessageHolderRow=document.getElementById(this.Id+"_htrMessage");
this.MessageHolderCell=document.getElementById(this.Id+"_htcMessage");
if(typeof (GetDisposeManager)!="undefined"){
GetDisposeManager().Add(this);
}
}
ThumbnailCreator.prototype.Dispose=function(){
this.WidthHolder=null;
this.HeightHolder=null;
this.NewImageNameHolder=null;
this.OriginalFileLocationHolder=null;
this.ConstrainHolder=null;
this.DimentionUnitHolder=null;
this.CreateButton=null;
this.MessageHolderRow=null;
this.MessageHolderCell=null;
};
ThumbnailCreator.prototype.Initialize=function(_a,_b){
this.RealImage=_a;
this.ThumbnailSuffix=_b;
this.RealImagePath=decodeURI(this.RealImage.src);
this.RealImageWidth=this.RealImage.width;
this.RealImageHeight=this.RealImage.height;
if(this.RealImageWidth==0){
this.RealImageWidth=35;
}
if(this.RealImageHeight==0){
this.RealImageHeight=35;
}
this.ResetDimentions();
var _c=this.RealImagePath.substr(this.RealImagePath.lastIndexOf("/")+1);
var _d=new RegExp("(.[ A-Za-z0-9_]*?)$","i");
_d.exec(_c);
this.NewImageNameHolder.value=_c.replace(_d,this.ThumbnailSuffix+RegExp.$1);
this.OriginalFileLocationHolder.value=this.RealImagePath;
this.Constrain=this.ConstrainHolder.checked;
this.DimentionUnitHolder.options[0].text=localization["Pixel"];
this.DimentionUnitHolder.options[1].text=localization["Percent"];
var _e=this;
this.HeightHolder.onkeyup=function(ev){
_e.SetValue(this,"height");
};
this.WidthHolder.onkeyup=function(ev){
_e.SetValue(this,"width");
};
this.ConstrainHolder.onclick=function(ev){
_e.SetConstrain(this.checked);
};
this.DimentionUnitHolder.onchange=function(ev){
_e.SetDimentionUnit();
};
var _13=this.CreateButton.onclick;
this.CreateButton.onclick=function(ev){
if(!_e.CheckValidDimension()){
return;
}
_13();
};
this.ResetMessage();
};
ThumbnailCreator.prototype.SetDimentionUnit=function(){
this.DimentionUnit=this.DimentionUnitHolder.options[this.DimentionUnitHolder.selectedIndex].value.toLowerCase();
this.ResetDimentions();
};
ThumbnailCreator.prototype.ResetMessage=function(){
this.MessageHolderRow.style.display="none";
this.MessageHolderCell.style.display="none";
this.MessageHolderCell.innerHTML="";
};
ThumbnailCreator.prototype.ResetDimentions=function(){
if(this.DimentionUnit=="pixel"){
this.Width=this.RealImageWidth;
this.Height=this.RealImageHeight;
}else{
this.Width=100;
this.Height=100;
}
this.WidthHolder.value=this.Width;
this.HeightHolder.value=this.Height;
};
ThumbnailCreator.prototype.SetConstrain=function(_15){
this.Constrain=_15;
};
ThumbnailCreator.prototype.ConstrainProportions=function(_16,_17){
var _18=0;
if(_17.toLowerCase()=="width"){
if(this.DimentionUnit=="pixel"){
if(this.RealImageWidth>0){
_18=_16/this.RealImageWidth;
this.Height=Math.round(this.RealImageHeight*_18);
}else{
this.Height=0;
}
}else{
this.Height=parseInt(_16);
}
}else{
if(_17.toLowerCase()=="height"){
if(this.DimentionUnit=="pixel"){
if(this.RealImageHeight>0){
_18=_16/this.RealImageHeight;
this.Width=Math.round(this.RealImageWidth*_18);
}else{
this.Width=0;
}
}else{
this.Width=parseInt(_16);
}
}
}
};
ThumbnailCreator.prototype.ApplyConstrain=function(_19){
if(this.Constrain){
if(_19.toLowerCase()=="width"){
this.ConstrainProportions(this.WidthHolder.value,_19);
this.HeightHolder.value=this.Height;
}else{
if(_19.toLowerCase()=="height"){
this.ConstrainProportions(this.HeightHolder.value,_19);
this.WidthHolder.value=this.Width;
}
}
}
};
ThumbnailCreator.prototype.SetValue=function(_1a,_1b){
if(!_1a.value.match(/^\d*$/ig)){
if(_1b.toLowerCase()=="width"){
_1a.value=this.Width;
}else{
if(_1b.toLowerCase()=="height"){
_1a.value=this.Height;
}
}
}else{
if(_1b.toLowerCase()=="width"){
if(_1a.value==""){
this.Width=0;
}else{
this.Width=parseInt(_1a.value);
}
this.ApplyConstrain(_1b);
this.HeightHolder.value=this.Height;
}else{
if(_1b.toLowerCase()=="height"){
if(_1a.value==""){
this.Height=0;
}else{
this.Height=parseInt(_1a.value);
}
this.ApplyConstrain(_1b);
this.WidthHolder.value=this.Width;
}
}
}
};
ThumbnailCreator.prototype.SetMessage=function(_1c){
this.MessageHolderRow.style.display="block";
this.MessageHolderCell.style.display="block";
this.MessageHolderCell.innerHTML=localization[_1c];
};
ThumbnailCreator.prototype.CheckValidDimension=function(){
if(this.Width<=0||this.Height<=0){
alert(localization["MessageNoValidDimensions"]);
return false;
}
return true;
};
ThumbnailCreator.prototype.Enable=function(){
this.private_SetEnabledState(true);
};
ThumbnailCreator.prototype.Disable=function(){
this.private_SetEnabledState(false);
};
ThumbnailCreator.prototype.private_SetEnabledState=function(_1d){
this.WidthHolder.value="";
this.HeightHolder.value="";
this.NewImageNameHolder.value="";
this.OriginalFileLocationHolder.value="";
this.CreateButton.disabled=!_1d;
this.WidthHolder.disabled=!_1d;
this.HeightHolder.disabled=!_1d;
this.NewImageNameHolder.disabled=!_1d;
this.ConstrainHolder.disabled=!_1d;
this.DimentionUnitHolder.disabled=!_1d;
this.OverwriteExistingHolder=!_1d;
};;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
