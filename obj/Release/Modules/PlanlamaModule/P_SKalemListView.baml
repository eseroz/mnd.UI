   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  S.clr-namespace:Pandap.UI.Modules.PlanlamaModule Pandap.UI.Modules.PlanlamaModule  7    1Pandap.UI.Modules.PlanlamaModule.P_SKalemListView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9
converters"clr-namespace:Pandap.UI.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   f	 bDevExpress.Xpf.PivotGrid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxpg7http://schemas.devexpress.com/winfx/2008/xaml/pivotgrid 	 c
 _DevExpress.Xpf.Ribbon.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  
 -helperclr-namespace:Pandap.UI.Helper  
 <local.clr-namespace:Pandap.UI.Modules.PlanlamaModule  
 Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
 U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  -     >�Name$  UcPalanlamaTakip�� � 	Resources    checkTemplate �� Triggers �� Value �� Binding �� Path �� 
TargetName �� Value �� Source*  $DevExpress.Xpf.Core.DXImageExtension	   Image1  +DevExpress.Xpf.Core.Native.DXImageConverter
 �� Property  checkEditLockStyle �� 
TargetType&   DevExpress.Xpf.Editors.CheckEdit%  DevExpress.Xpf.Editors.BaseEdit  rowStyle!  DevExpress.Xpf.Grid.GridRow �� BasedOn9  3DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Grid.Themes.GridRowThemeKeys, DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   ResourceKey9   3Pandap.UI.Converters.NullOrEmptyVisibilityConverter $  myNullOrEmptyVisibilityConverter?	   9Pandap.UI.Converters.NullOrEmptyVisibilityInvertConverter * &myNullOrEmptyVisibilityInvertConverter/
   )Pandap.UI.Converters.CellBgColorConverter  myCellBgColorConverter  
ctemplate1 �� CornerRadius%  DevExpress.Xpf.Editors.TextEdit&   DevExpress.Xpf.Core.ThemeManager   	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter �� Triggers ^� 
Conditions �� Binding �� Value��%�  &	        &	 &    7   7   &	 t    0�� *  $ RowStyle=�+1&	 �    &	 �    &	 �    &	 �    �� .R��� $��PART_Editor��$��30��$��30�� �� $
 True�� �� $( "(dxe:BaseEdit.OwnerEdit).IsChecked�.a��� $ PART_Editor�� ��    $	 Encrypt_16x16.png 
 � ���� $ PART_Editor��
 �@�ToolTip$ Kapatılmış���� $ PART_Editor��
 � ��$ 20���� "  .��� 
 �@ DisplayTemplate8   �� 
 �@ EditTemplate8  �� "   2  .��� 
 � ��$ 30��  	  
  �� .R��� $	��br1��$��1q�
������ 
����İ�$��1q�$ 1��._�  $��PART_Editor��$��Center=�$	 XXX  ^�  ��  �� $ RowData.Row.UretimEmriKod�  .0��� $	 br1��
 � ��
 ���� �� $	 br1��
 � ��$ 0q��� $	 br1��
 � ��$ 10q��� $	 br1��
 � ��
 ���� �� $
 True�� �� $ RowData.IsSelected�.a��� $ PART_Editor��
 � ��
 �  ���� $	 br1��
 � ��
 �����2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors $   Pandap.UI.Helper.ExportService     View %  System.Windows.Markup.Reference  *View1+'  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+,  &DevExpress.Mvvm.UI.NotificationService  -     Name$ AppNotificationService��&   UseWin8NotificationsIfAvailable .  -  'DevExpress.Xpf.Core.DXMessageBoxService  .���  � RowDefinitions �� $
��Auto ��� $��* �.+�) 
 #DevExpress.Xpf.Ribbon.RibbonControl     AsyncMergingEnabled .  4  .System.Windows.Automation.AutomationProperties   IsRowHeader . $	��Top=�   MDIMergeStyle$ Never=�#   MinimizationButtonVisibility$ 	Collapsed=�   RibbonHeaderVisibility$ 	Collapsed=�    RibbonStyle$  
Office2007=�!   RibbonTitleBarVisibility$! 	Collapsed=�"   ShowApplicationButton" .  #   ToolbarShowMode$
# Hide=�$   Items.$ 5 
 /DevExpress.Xpf.Ribbon.RibbonDefaultPageCategory  2 
 ,DevExpress.Xpf.Ribbon.RibbonPageCategoryBase%   Pages.% & 
  DevExpress.Xpf.Ribbon.RibbonPage  &   Caption)  #DevExpress.Xpf.Core.ObjectConverter$& Planla '   Groups.' + 
 %DevExpress.Xpf.Ribbon.RibbonPageGroup  (   Caption$( Planlama İşlemleri��)   Items.) '  !DevExpress.Xpf.Bars.BarButtonItem  !  DevExpress.Xpf.Bars.BarItem*   CommandParameter$* Duzenle��+   Content$+ 	Planla... ,   Command, �� *PlanlaCommand+-   Glyph-   $	 Edit_32x32.png .   
LargeGlyph.   $	 HistoryItem_32x32.png   $+ 
Kalemi Aç , �� *KalemiAcCommand+.   *0.SvgImages/Icon Builder/Actions_Arrow4Right.svg+  $+ Ekranı Güncelle , �� *EkraniYenileCommand+-   $$	 RefreshAllPivotTable_32x32.png .   $$	 RefreshAllPivotTable_32x32.png   $( Tablo İşlemleri��.)   $+ Excele Aktar , �� *ExcelExportCommand+-   $	 Print_32x32.png .   $	 ExportToXLS_32x32.png   $+ Yerleşimini Kaydet , �� *YerlesimKaydetCommand+.   $&	  VerticalGridlinesMajor_32x32.png '  !DevExpress.Xpf.Bars.BarStaticItem  /   ContentTemplate/ �� .R�(   "DevExpress.Xpf.Editors.ListBoxEdit   $��50��$��50��0    ItemsSource0 �� *DataContext.Yerlesimler+1 �� Mode$1 TwoWay=�2 �� RelativeSource2 �� 3 �� AncestorType$3 UserControlL�4    SelectedItem4 �� *DataContext.SeciliYerlesim+$1 TwoWay=�5 �� UpdateSourceTrigger$5 PropertyChanged=�2 �� $3 UserControlL�6    
ItemsPanel6 �� .R��� $��50��$��Center=�$��Stretch=�  $( Filtre��.) &!   DevExpress.Xpf.Bars.BarCheckItem!  $$+ Sadece Kapatılanları Göster 7 !  	IsChecked7 �� *SadeceKapatilanlariGoster+  $( Genel��.)   / �� .R��� $
��XLSX��  MaterialDesignFlatButton#���  ���� *DataContext.HesapDetayCommand+8 �� ElementName$8 UcPalanlamaTakip��.���� 9 �� Orientation$9 
Horizontal=�.+�'"  !MaterialDesignThemes.Wpf.PackIcon"  $��Center=�
��� � �: "  Kind$: Information=��� $��8,0,0,0q�$��Center=�$��Hesaplama Yöntemi��]� $��1��.+�%#  DevExpress.Xpf.Grid.GridControl#  -   ; # Name$; grdSiparisKalemleri��$��5q�$
 None 9$  3DevExpress.Xpf.Grid.DataControlSerializationOptions< $  AddNewColumns< . = $  RemoveOldColumns= . )%  #DevExpress.Xpf.Grid.DataControlBase> %  FilterString$)> #[SiparisDurum]=='MÜŞTERİ ONAYLI'��? %  CurrentItem? �� *SeciliPlanlamaTakipDto+$1 TwoWay=�$5 PropertyChanged=�@ %  ItemsSource@ �� *PlanlamaDtoListe+A %  SelectedItemsA �� *SeciliPlanlamaTakipDtos+$1 TwoWay=�$5 PropertyChanged=� $&  DevExpress.Xpf.Grid.GridColumn$'  DevExpress.Xpf.Grid.BaseColumn2(  ,DevExpress.Xpf.Grid.GridColumnWidthConverter  customCellStyle/)  )DevExpress.Xpf.Grid.LightweightCellEditorB �� 	Converter/*   )Pandap.UI.Converters.IntoToColorConverter 	 customCellStyleHesap3+  -DevExpress.Xpf.Grid.LightweightCellEditorBase��%M  '&        &	 )     0�� *  $ LightweightCellStyle=�+1&		 �     0�� *  $ LightweightCellStyle=�+1�� " & .��� 
 �@' Width$ 50( �� " )  2  .��� 
 �@) 
Background �� $ RowData.Row.UretimEmriKod�B *  �� " )  2  .��� 
 �@+ 
Foreground
 �  ���� 
 �@) 
Background
 �����C #  TotalSummaryC ),  #DevExpress.Xpf.Grid.GridSummaryItem,  )-  #DevExpress.Xpf.Grid.SummaryItemBaseD -  DisplayFormat$D 
Say:{0:n0}��E -  	FieldName$E SiparisKalemKod��F -  SummaryType] YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5.  /DevExpress.Data.Design.SummaryItemTypeConverter$F Count. ,  $D {0:n0}��$E Bakiye��$	F Sum. ,  $D {0:n0}��$E KalemMiktar��$	F Sum. ,  $D {0:n0}��$E KaliteRedMiktar��$	F Sum. ,  $D {0:n0}��$E PaketKarantinaMiktar��$	F Sum. ,  $D {0:n0}��$E UretimdeYuruyenMiktar��$	F Sum. ,  $D {0:n0}��$E PlanlananMiktar��$	F Sum. ,  $D {0:n0}��$E PaketlenenMiktar��$	F Sum. G #  ViewG #/  DevExpress.Xpf.Grid.TableView/  -   H / Name$H View1��&I /  AllowAdvancedVerticalNavigationI . J /  AllowFixedColumnMenuJ . -K /  &AllowHorizontalScrollingVirtualizationK . L /  	AutoWidthL .  &0   DevExpress.Xpf.Grid.DataViewBaseM 0  ClipboardCopyOptions$M Excel=�N 0  ClipboardMode$N 	Formatted=�O 0  ColumnFilterPopupMode$O Excel=�"P 0  EnableSelectedRowAppearanceP . $Q /  ExtendScrollBarToFixedColumnsQ . R /  RowMinHeight$R 40Q�S /  ShowAutoFilterRowS . "T /  ShowCriteriaInAutoFilterRowT . &1   DevExpress.Xpf.Grid.GridViewBaseU 1  ShowGroupPanelU .  V 0  ShowTotalSummaryV . #W /  IsDetailButtonVisibleBindingW 12  +DevExpress.Xpf.DXBinding.DXBindingExtension2  *Row.UretimEmirleri.Count > 0+X 0  ColumnHeaderTemplateX �� .R�� 
������ .+��� $��35��$
��0,-4q�$��Stretch=�$��SemiBold"�Y �� TextAlignment$Y Center=�$
��Wrap=����� * +Z #  Columns.Z &  $3  DevExpress.Xpf.Grid.ColumnBase[ 3  	FieldName$[ KalemSiparisDurum��&  \ '  Width$\ 40( $[ PlanlamaVarMi��] 3  ReadOnly] . &  $\ 85( ^ '  Header$^ # _ 3  IsSmart_ . ` 3  CellTemplate` �� .R��� $9 
Horizontal=�.+��� $��Kapat��$��Kapat��#��[ ���� *$"View.DataContext.KapatIslemCommand+{��� *RowData.Row.KapatildiMi+B 34   -Pandap.UI.Converters.TrueFalseInvertConverter4  &  $[ Yuzde12_Kisit��$^ %12 Kısıtta Mı ] . #a 3  AutoFilterRowDisplayTemplatea �� &  $\ 50( $[ MesajSayisi��$^ Mesaj  
 
MesajKolon#��[
 &  $[ SiparisDurum��&  $[ KapasitifDurum��&  $[ 	MusteriAd��&  $[ 	SevkHafta��&  $[ 	SevkYilAy��&  $\ 80( b 3  AutoFilterCriteria$b ContainsX�$[ SiparisKalemKod��c 3  FilterPopupMode$c Excel=�$^ 	Kalem Kod &  d 3  AllowEditing55  /DevExpress.Utils.Design.DefaultBooleanConverter$d False5 e 3  	CellStyle#e [	 $[ KalemMiktar��$^ Sip.Kalem Miktar f 3  EditSettingsf 66  0DevExpress.Xpf.Editors.Settings.TextEditSettings6  67  0DevExpress.Xpf.Editors.Settings.BaseEditSettingsg 7  DisplayFormat$g n0��h '  HeaderStyleh �� (8  "DevExpress.Xpf.Grid.BaseGridHeader" 8 .��� 
 � ��
 �  ���� 
 � ��
 �����&  #e [	 $[ PlanlananMiktar��$^ Planlanan Miktar f 6  $g n0��h �� " 8 .��� 
 � ��
 �  ���� 
 � ��
 �����&  #e [	 $[ UretimdeYuruyenMiktar��$^ 	Üretimde f 6  $g n0��h �� " 8 .��� 
 � �� �	#FFC73535�� 
 � ��
 �����&  $d False5 #e [	 $[ PaketlenenMiktar��f 6  $g n0��h �� " 8 .��� 
 � �� �	#FFC73535�� 
 � ��
 �����&  $d False5 #e [	 $[ KaliteRedMiktar��f 6  $g n0��h �� " 8 .��� 
 � �� �� i �� Color$i 	#FFDD9797���� 
 � ��
 �����&  $d False5 #e [	 $[ PaketKarantinaMiktar��f 6  $g n0��h �� " 8 .��� 
 � �� �� $i 	#FFDD9797���� 
 � ��
 �����&  #e [	 $[ Bakiye��$^ Kalan Planlanacak f E9  ?DevExpress.Xpf.Editors.Settings.Extension.TextSettingsExtension9  j 9  DisplayFormat$j n0��h �� " 8 .��� 
 � ��
 �  ���� 
 � ��
 �����&  $[ KullanimAlani��&  $[ Alasim��&  $[ Kalinlik��&  $[ En��&  $[ IcCap��&  $[ MinCap��&  $[ MaxCap��&  $[ KaydiriciOraniMinMaxStr��&  $[ 	Kondusyon��&  $[ Ek��&  $[ Yuzey��&  $[ 
MasuraCins��&  $[ Metraj��&  $[ MetrajTolerans��&  $[ KalinlikToleransYuzde��&  $[ Ambalaj��&  $
d true5 $[ AmbalajKafesOlcu��$^ Kafes Ölçü &  $
d true5 $
[ Pas5��&  $
d true5 $
[ Pas4��&  $
d true5 $
[ Pas3��&  $
d true5 $
[ Pas2��&  $
d true5 $
[ Pas1��&  $[ FolyoHaddeToplam��&  $
d true5 $[ 
Seperator1��&  $
d true5 $[ 
Seperator2��&  $
d true5 $[ Dilme��&  $
d true5 $[ TavaGirecek��&  $
d true5 $[ Tavda��&  $
d true5 $[ TavdanCikan��&  $
d true5 $[ Paketlenecek��&  $
d true5 $[ Sp_Dilme_Cikis��k #  DetailDescriptork 5:  /DevExpress.Xpf.Grid.DataControlDetailDescriptor:  l :  ItemsSourcePath$l UretimEmirleri��m :  DataControl.m #  $	��500��$	��300��$��5q�C ,  $D {0:n0}��$E Uretim_PlanlananMiktar��$	F Sum. ,  $D {0:n0}��$E Uretim_PaketlenenMiktar��$	F Sum. ,  $D {0:n0}��$E KaliteRedMiktar��$	F Sum. ,  $D {0:n0}��$E PaketKarantinaMiktar��$	F Sum. ,  $D {0:n0}��$E UretimPlanYuruyen��$	F Sum. G /  -   $H View2��I . J . K . $M Excel=�$N 	Formatted=�$O Excel=�P . Q . $R 40Q�S .  T . U .  V . X �� .R�� 
������ .+��� $��35��$
��0,-4q�$��Stretch=�$��SemiBold"�$Y Center=�$
��Wrap=����� * +.Z &  $[ KapatildiMi��&  $	\ 100( $[ UretimEmriKod��&  $\ 80( $[ KartNo��&  $	\ 100( $[ Uretim_PlanlananMiktar��$^ 	Planlanan f 9  $j n0��&  $	\ 100( $[ Uretim_PaketlenenMiktar��$^ 
Paketlenen f 9  $j n0��&  $	\ 100( $[ KaliteRedMiktar��$^ 
Kalite Red f 9  $j n0��&  $	\ 100( $[ PaketKarantinaMiktar��$^ Paket Karantina f 9  $j n0��&  $	\ 100( $[ UretimPlanYuruyen��$^ 	Yürüyen f 9  $j n0��&  $\ 80( _ . ` �� .R��� $��50��$��4q�$��0q�
������ $	��Sil��$��14(�$��DemiBold"�
���  ������ *" DataContext.UretimEmriSilCommand+2 �� n �� Mode$n FindAncestor=�$3 UserControlL����� *RowData.Row+��Z� 