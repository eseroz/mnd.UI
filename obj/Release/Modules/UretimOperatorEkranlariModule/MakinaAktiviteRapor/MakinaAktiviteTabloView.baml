   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Nclr-namespace:Pandap.UI.Modules.UretimOperatorEkranlariModule.MakinaDurusRapor@Pandap.UI.Modules.UretimOperatorEkranlariModule.MakinaDurusRapor  3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  a    [Pandap.UI.Modules.UretimOperatorEkranlariModule.MakinaAktiviteRapor.MakinaAktiviteTabloView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     `localNclr-namespace:Pandap.UI.Modules.UretimOperatorEkranlariModule.MakinaDurusRapor     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   -helperclr-namespace:Pandap.UI.Helper   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes 	 <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars    � 	Resources     
ctemplate1 �� CornerRadius%  DevExpress.Xpf.Editors.TextEdit&   DevExpress.Xpf.Core.ThemeManager   	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter �� Triggers �� Value �� Binding �� Path �� 
TargetName �� Property	 �� Value  grayColumnHeader
 �� 
TargetType(  "DevExpress.Xpf.Grid.BaseGridHeader �� EndPoint �� 
StartPoint � Color � Offset/  )DevExpress.Xpf.Grid.LightweightCellEditor �� Triggers(  "DevExpress.Xpf.Grid.CellEditorBase P� Property P� Value$  DevExpress.Xpf.Grid.RowControl&   DevExpress.Xpf.Grid.DataViewBase��%�  &	        &	 �     '  �    '  �    �� .R��� $	��br1��
������ $ 1��$��1q�
����İ�$��1q�._�  $��PART_Editor��$��Center=�$	 XXX  �� $
 True�� �� $ RowData.IsSelected�.a��� $ PART_Editor�� � ��
	 �  ���� $	 br1�� � ��
	 ������� "
  .���  � ��	 �� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $ 	#FF5266BA��$ 1Q�� $ 	#FE4868C7��$ 0.137Q���  � ��
	 ������� "
   P�  �@ IsFocusedCell . . ���  �@ 
Background
	 ������� "
   P�  �@ IsFocusedRow . . ���  � ��
	 �������  � ��
	 �   �2	  ,DevExpress.Mvvm.UI.Interactivity.Interaction 	  	Behaviors $
   Pandap.UI.Helper.ExportService
   
  View %  System.Windows.Markup.Reference  *View1+'  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+��� $��28��$��10q� �� Orientation$ 
Horizontal=�$��1��.+��� $��14(�$
��Bold"�$��Center=�$��Tarih Aralığı : ��%  DevExpress.Xpf.Editors.DateEdit  $��14(�$
��Bold"�$	��120��%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *BaslamaTarih+  $��14(�$
��Bold"�$	��120�� �� *
BitisTarih+�� $��20,0,0,0q�$��14(�$
��Bold"�$��Center=�$��	Makina : ��)  #DevExpress.Xpf.Editors.ComboBoxEdit  $��14(�$
��Bold"�$y�	Collapsed=�+  %DevExpress.Xpf.Editors.LookUpEditBase   DisplayMember$ AdSoyad��   ValueMember$ AdSoyad��$��10,0,0,0q�$	��200��   NullText$ Operator�� �� *OperatorAdSoyad+ �� UpdateSourceTrigger$ PropertyChanged=�   ItemsSource �� *OperatorListe+  $��14(�$
��Bold"�$ MakinaAd��$ 	MakinaKod��'  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable$ FalseX�$��10,0,0,0q�$	��200��$ Makina�� �� *	MakinaKod+$ PropertyChanged=� �� *MakinaListe+�� $ 
Horizontal=�$��10,0,0,0q�.+��� $
��XLSX��  MaterialDesignFlatButton#���  ���� *ExcelExportCommand+.���� $ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ 	FileExcel=��� $��8 0 0 0q�$��Center=�   MaterialDesignBody1TextBlock#��[ $��Excele Aktar���� $��10,0,0,0q�$��14(�
���  ��$��Center=�$��DemiBold"����� *FormUyariMesaj+%  DevExpress.Xpf.Grid.GridControl  -   $��g1��)  #DevExpress.Xpf.Grid.DataControlBase    ClipboardCopyMode$  ExcludeHeader=�$��13(�$��10,10,10,9.6q�$��2��!   SelectionMode$
! Cell=�"   AutoGenerateColumns$
" None=�$��2��#   CurrentItem# �� *SeciliSatir+$ �� Mode$$ TwoWay=�$ PropertyChanged=�%   ItemsSource% �� *UretimTabloSatirlar+$$ TwoWay=�$ PropertyChanged=�&   TotalSummary& )  #DevExpress.Xpf.Grid.GridSummaryItem  )  #DevExpress.Xpf.Grid.SummaryItemBase'   	Alignment$' Default=�(   	FieldName$( SiraNo��)   SummaryType]
 YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5 
 /DevExpress.Data.Design.SummaryItemTypeConverter$) Count *   DisplayFormat$* n0��  $' Default=�$( GirişBobinAğırlığı��$	) Sum $* n0��  $' Default=�$( SüreDk��$	) Sum $* n0��  $' Default=�$* n2��$( Hurda��$	) Sum   $' Default=�$!( ÇıkışBobinAğırlığı��$	) Sum $* n0��  $' Default=�$( İşletmeFiresi��$	) Sum $* n0��    s1)  #DevExpress.Xpf.Grid.GridControlBand$  DevExpress.Xpf.Grid.BaseColumn*  $DevExpress.Xpf.Grid.GridColumnHeader+ �� ElementName$  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.ColumnBase  grayCell, �� BasedOn9  3DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension�   �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Grid.Themes.GridRowThemeKeys, DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]-    ResourceKey��%�  &	       '  S     '  �     '  �    &	 �    0�� *  $- LightweightCellStyle=�+1'  �    0�� *  $- LightweightCellStyle=�+1�� "
  .���  � ��
	 �������  � ��
	 �   ���  � ��$	 14(��� "
  .���  �@ HeaderTemplate	 �� .R��� 
���   �$
��Bold"����� �� "
   ��  �� $
+ win1��$ DataContext.MakinaGrupKod� SH.a���  � ��
	 ��Zj���  � ��
	 �������  �� $
+ win1��$ DataContext.MakinaGrupKod� FH.a���  � ��
	 �������  � ��
	 �   ���  �� $
+ win1��$ DataContext.MakinaGrupKod� SP.a���  � ��
	 ������  � ��
	 �   ���  �� $
+ win1��$ DataContext.MakinaGrupKod� DLM.a���  � ��
	 ��·���  � ��
	 �   ��� "
  .��� ( �@  HorizontalHeaderContentAlignment$	 Center=���  �@ FilterPopupMode$	 Excel=��� "
  , 2  ���  �@ 
Background
	 ����� P�  �@ SelectionState$ FocusedAndSelected=�. ���  �@ 
Background
	 ���� ��  � ��
	 �   ��� "
  , 2   P�  �@ SelectionState$ FocusedAndSelected=�. ���  �@ 
Background
	 ���� ��  � ��
	 �   �.   View. #!  DevExpress.Xpf.Grid.TableView!  -   / ! Name$/ View1��0   EnterMoveNextColumn0 .  1   NavigationStyle$
1 Cell=�2 !  AllowChangeBandParent2 . 3 !  AllowChangeColumnParent3 . 4 !  IndicatorWidth$4 30Q�5 !  	PasteMode$5 Update=�6   ClipboardCopyOptions$6 Excel=�7   AllowSorting7 .  8   AllowColumnFiltering8 . 9 !  NewItemRowPosition$
9 None=�:   ShowFocusedRectangle: . ;   EnableImmediatePosting; . < !  RowMinHeight$< 40Q�=   ShowTotalSummary= . >   ColumnFilterPopupMode$> Excel=�&"   DevExpress.Xpf.Grid.GridViewBase? "  ShowGroupPanel? .  @ !  	AutoWidth@ .   A   FocusedCellBorderTemplateA �� .R��� $��0q�
��� d �$	��1.5q�B !  FormatConditionsB )#  #DevExpress.Xpf.Grid.FormatCondition#  -$  'DevExpress.Xpf.Grid.FormatConditionBaseC $  
Expression$C [SüreProblemliMi] =1��D $  	FieldName$D SüreDk��1%  +DevExpress.Xpf.Grid.ExpressionConditionBaseE %  Format.E 6&  0DevExpress.Xpf.Core.ConditionalFormatting.Format&  F &  
Background
F �  ��G &  
Foreground
G �����H &  TextDecorationsH Z� I   ColumnHeaderTemplateI �� .R��� J �� TextAlignment$J Center=�$��Stretch=�$��DemiBold"�$��35��$
��Wrap=�$��Right=�����  K   RowCellMenuCustomizationsK ''  !DevExpress.Xpf.Bars.BarButtonItem'  !(  DevExpress.Xpf.Bars.BarItemL (  Content))  #DevExpress.Xpf.Core.ObjectConverter$L Kopyala) M (  Command$M ApplicationCommands.Copy��N (  GlyphN **  $DevExpress.Xpf.Core.DXImageExtension*  O *  Image1+  +DevExpress.Xpf.Core.Native.DXImageConverter$O Copy_16x16.png+ '  $L Yapıştır) $M ApplicationCommands.Paste��N *  $O Paste_16x16.png+ '  $L İçeriğiTemizle) M �� *(&View.DataContext.IcerigiTemizleCommand+P (  CommandParameterP �� $+ View1��'  $L -------------) '  $L Ekle...) M �� *'%View.DataContext.UsteSatirEkleCommand+P �� $+ View1��'  $L Sil...) N *  $O Delete_16x16.png+ M �� *View.DataContext.SilCommand+P �� $+ View1��Q   BandsQ   -   R  Name$R operatorBobin��S   Header$%S OPERATÖR / BOBİN BİLGİLERİ) T   Columns.T   -   $R id��U   VisibleU .  V   Width2,  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$V 45, W   MinWidth$W 25��X   FilterPopupMode$X Excel=�Y   	FieldName$Y Id��Z   CellTemplateZ �� .R��� .+�   �� *RowData.Row.Id+   �� *RowData.Row.KayitDurum+  -   $R tarih��[   	CellStyle#[ [ $S Tarih) $	V 100, $W 25��$X Excel=�$Y Tarih��  -   $R makinaAd��#[ [ $S 	Makina Ad) $	V 100, $W 25��$X Excel=�$Y MakinaKisaAd��  -   $R operator��#[ [ $S 	Operatör) $	V 100, $W 25��$X Excel=�$Y 	Operatör��  -   $R kafileNo��$S Giriş Kafile No) $V 75, $Y GirişKafileNo��\   EditSettings\ E-  ?DevExpress.Xpf.Editors.Settings.Extension.TextSettingsExtension-  E.  ?DevExpress.Xpf.Editors.Settings.Extension.BaseSettingsExtension!] .  HorizontalContentAlignment$] Center=�  -	   $R bobinNo��$S Giriş Bobin No) $V 70, $Y GirişBobinNo��\ -  $] Center=�^ -  DisplayFormat$^ n0��  -
   $R miktarKg��$S Giriş Bobin Ağ.) $V 70, $Y GirişBobinAğırlığı��\ 6/  0DevExpress.Xpf.Editors.Settings.CalcEditSettings/  60  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!_ 0  HorizontalContentAlignment$_ Center=�` 0  DisplayFormat$` n0��  -   $R cikisKafileNo��$V 75, $S Çıkış Kafile No) $Y ÇıkışKafileNo��\ -  $] Center=�  -   $R cikisBobinNo��$V 70, $S Çıkış BobinNo) $Y ÇıkışBobinNo��\ -  $] Center=�$^ n0��  -   $R cikisBobinAgirligi��$V 70, $S Çıkış Bobin Ağ.) $!Y ÇıkışBobinAğırlığı��\ /  $_ Center=�$` n0��  -   $R runSure��$S 	RUN SÜRE) a   HeaderStyle#a [ .T   -   $R baslamaZamani��b   CopyValueAsDisplayTextb .  #[ [ $S Başlama Zamanı) $V 70, $Y BaşlangıçSaati��\ 61  0DevExpress.Xpf.Editors.Settings.DateEditSettings1  62  0DevExpress.Xpf.Editors.Settings.TextEditSettingsc 2  Mask$c HH:mm��$_ Center=�d 0  AllowNullInputd . e 2  MaskType$e DateTimeAdvancingCaret=�f   DisplayTemplatef �� .R��� $��Center=�$��Center=�.+��� $
��Bold"�$J Center=����� *RowData.Row.BaşlangıçSaati+g �� StringFormat$g HH:mm���� $��9(�$J Center=����� *RowData.Row.BaşlangıçSaati+$g 	dd/MM ddd��  -   $R bitisZamani��b .  #[ [ $S Bitiş Zamanı) $V 70, 'h    HorizontalHeaderContentAlignment$h Center=�$Y BitişSaati��\ 1  $_ Center=�$c HH:mm��d . $e DateTimeAdvancingCaret=�$` 	HH:mm ddd��f �� .R��� $��Center=�$��Center=�.+��� $
��Bold"�$J Center=����� *RowData.Row.BitişSaati+$g HH:mm���� $��9(�$J Center=����� *RowData.Row.BitişSaati+$g 	dd/MM ddd��  -   $R 	durusKodu��#[ [ $V 70, $Y 
DuruşKodu��\ 2  $_ Center=�d .   -   $R durusAd��#[ [ $S 	Duruş Ad) $	V 140, i   ReadOnlyi . $Y MakinaDuruşAd��\ 2  j 2  TextWrapping$
j Wrap=�  -   $R sureDk��#[ [ $V 60, i . $Y SüreDk��\ -  $^ n0��$] Center=�  -   $R 
BobinBoyut��$S KALINLIK / EN) .T   -   $R girisKalinlik��$S Giriş Kalınlık) $V 70, $Y GirişKalınlık��  -   $R cikisKalinlik��$S Çıkış Kalınlık) $V 70, $Y ÇıkışKalınlık��  -   $R girisEni��$S 
Giriş Eni) $V 70, $Y 	GirişEni��  -   $R cikisEni��$S Çıkış Eni) $V 70, $Y ÇıkışEni��\ /  $_ Center=�$` n2��  -   $R Diger��$S DİĞER) .T   -   $R 	kondusyon��$V 70, $Y 
Kondüsyon��  -   $R pasNo��$V 70, $Y PasNo��  -   $R nihaiSonPas��$V 70, $Y NihaiSonPas��  -   $R Alasim��$V 70, $Y Alaşım��  -   $	R cap��$V 70, $
Y Çap��  -   $R eksayisi��$V 70, $Y 
EkSayısı��  -    $R eknedeni��$V 70, $Y EkNedeni��  -!   $R sonrakiIslem��$V 70, $Y Sonrakiİşlem��  -"   $R aciklama��$V 70, $Y 
Açıklama��