   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  ]3clr-namespace:Pandap.UI.Modules.UretimIsletmeModule%Pandap.UI.Modules.UretimIsletmeModule  ?    9Pandap.UI.Modules.UretimIsletmeModule.MakinaVeriTabloView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   -helperclr-namespace:Pandap.UI.Helper   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006   U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes 	 MuretimIsletmeModule3clr-namespace:Pandap.UI.Modules.UretimIsletmeModule 	 -     >�Name$
  win1�� � 	Resources    
ctemplate1 �� CornerRadius%  DevExpress.Xpf.Editors.TextEdit&   DevExpress.Xpf.Core.ThemeManager   	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter �� Triggers �� Value �� Binding �� Path �� 
TargetName	 �� Property
 �� Value  grayColumnHeader �� 
TargetType(  "DevExpress.Xpf.Grid.BaseGridHeader �� 
StartPoint �� EndPoint � Offset � Color/  )DevExpress.Xpf.Grid.LightweightCellEditor �� Triggers(  "DevExpress.Xpf.Grid.CellEditorBase P� Property P� Value$  DevExpress.Xpf.Grid.RowControl&   DevExpress.Xpf.Grid.DataViewBase��%�  &	        &	 �     '  �    '  �    �� .R��� $	��br1��$��1q�
������ 
����İ�$��1q�$ 1��._�  $��PART_Editor��$��Center=�$	 XXX  �� $
 True�� �� $ RowData.IsSelected�.a��� $ PART_Editor��	 � ��

 �  ���� $	 br1��	 � ��

 ������� "  .��� 	 � ��
 �� $ 0.5,0&�$ 0.5,1&�.��� $ 0Q�$ Black��� $ 1Q�$ 	#FF5266BA��� $ 0.137Q�$ 	#FE4868C7���� 	 � ��

 ������� "   P�  �@ IsFocusedCell . . ��� 	 �@ 
Background

 ������� "   P�  �@ IsFocusedRow . . ��� 	 � ��

 ������� 	 � ��

 �   �2	  ,DevExpress.Mvvm.UI.Interactivity.Interaction 	  	Behaviors $
   Pandap.UI.Helper.ExportService
   
  View %  System.Windows.Markup.Reference  *View1+'  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+,  &DevExpress.Mvvm.UI.NotificationService  -     Name$ AppNotificationService��&   UseWin8NotificationsIfAvailable .  -  'DevExpress.Xpf.Core.DXMessageBoxService    $ PastingFromClipboard��   PassEventArgsToCommand$
 TrueX� �� *PastingFromClipboardCommand+   SourceObject ��  �� ElementName$ View1��  $ CellValueChanged��$
 TrueX� �� *CellValueChangedCommand+ �� $ View1��  $ PreviewKeyDown��$
 TrueX� �� *KeyDownCommand+ �� $ View1��.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�c� $��0��$��0,0,0,0q�}�.  .@��� $��0q�  MaterialDesignFlatButton#���  ���� *EkranTazeleCommand+.����  �� Orientation$ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ Refresh=��� $��8,0,0,0q�$��Center=�$��Ekran Yinele���� #���  $y�Visible=����� *YerlesimKaydetCommand+.���� $ 
Horizontal=�.+��� ��*  $DevExpress.Xpf.Core.DXImageExtension     Image1  +DevExpress.Xpf.Core.Native.DXImageConverter$&  VerticalGridlinesMajor_32x32.png (  "DevExpress.Xpf.Editors.ListBoxEdit  $��60��$��14(�$
��Bold"�$��Disabled=�    ItemsSource  �� *Yerlesimler+!   SelectedItem! �� *SeciliYerlesim+" �� Mode$" TwoWay=�# �� UpdateSourceTrigger$# PropertyChanged=�$   
ItemsPanel$ �� .R�$� % K� IsItemsHost% . & $� Orientation$& 
Horizontal=�'   ItemTemplate' �� .R��� $ 
Horizontal=�.+��� $��20��$��2,0,0,0q�$��Center=�( �� TextAlignment$( Center=����� �� $��8,0,0,0q�$��Center=�$��Yerleşim Kaydet���� $��1��$��28��$��10q�$ 
Horizontal=�.+��� $��Center=�$��14(�$
��Bold"�$��Tarih : ��%  DevExpress.Xpf.Editors.DateEdit  $	��120��$��14(�$
��Bold"�%  DevExpress.Xpf.Editors.BaseEdit)   	EditValue) �� *SeciliTarih+�� $��20,0,0,0q�$��Center=�$��14(�$
��Bold"�$��
Vardiya : ��)  #DevExpress.Xpf.Editors.ComboBoxEdit  $	��130��$��Center=�$��14(�$
��Bold"�'  !DevExpress.Xpf.Editors.ButtonEdit*   IsTextEditable$* FalseX�+  %DevExpress.Xpf.Editors.LookUpEditBase+   ItemsSource+ �� *
Vardiyalar+,   SelectedItem, �� *	Vardiya+$" TwoWay=�$# PropertyChanged=��� $��20,0,0,0q�$��Center=�$��14(�$
��Bold"�$��	Makina : ��  $	��200��$��10,0,0,0q�-   DisplayMember$- AdSoyad��$��14(�$
��Bold"�.   NullText$. Operator��/   ValueMember$/ AdSoyad��$y�	Collapsed=�) �� *OperatorAdSoyad+$# PropertyChanged=�+ �� *OperatorListe+  $	��200��$��10,0,0,0q�$- MakinaAd��$��14(�$
��Bold"�$* FalseX�$. Makina��$/ 	MakinaKod��) �� *	MakinaKod+$# PropertyChanged=�+ �� *MakinaListe+�� $��40,0,0,0q�$ 
Horizontal=�.+��� $	��200��$
��Auto��
�������$��Verileri Kontrol Et������ *VeriKontrolEtCommand+�� $	��200��$
��Auto��$��5,0,0,0q�$��Kaydet (Ctrl+S)������ *KaydetCommand+' 	 !MaterialDesignThemes.Wpf.Snackbar  -   0  Name$0 Snackbar��$��2��$��50��$��
0,0,0,-0.4q�$��Stretch=�$��Bottom=�1 K� ZIndex$
1 1000��2   MessageQueue2 �� *BoundMessageQueue+%  DevExpress.Xpf.Grid.GridControl  -   $��g1��$��2��$��2��$��10,10,10,9.6q�)  #DevExpress.Xpf.Grid.DataControlBase3   AutoGenerateColumns$
3 None=�4   ClipboardCopyMode$4 ExcludeHeader=�$��13(�5   SelectionMode$
5 Cell=�6   CurrentItem6 �� *SeciliSatir+$" TwoWay=�$# PropertyChanged=�7   ItemsSource7 �� *UretimTabloSatirlar+$" TwoWay=�$# PropertyChanged=�8   TotalSummary8 )  #DevExpress.Xpf.Grid.GridSummaryItem  )  #DevExpress.Xpf.Grid.SummaryItemBase9   	Alignment$9 Default=�:   DisplayFormat$: n0��;   	FieldName$; SiraNo��<   SummaryType]
 YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5 
 /DevExpress.Data.Design.SummaryItemTypeConverter$< Count   $9 Default=�$: n0��$; GirişBobinAğırlığı��$	< Sum   $9 Default=�$: n0��$; SüreDk��$	< Sum   $9 Default=�$: n2��$; Hurda��$	< Sum   $9 Default=�$: n0��$!; ÇıkışBobinAğırlığı��$	< Sum   $9 Default=�$: n0��$; İşletmeFiresi��$	< Sum    s1)   #DevExpress.Xpf.Grid.GridControlBand$!  DevExpress.Xpf.Grid.BaseColumn*"  $DevExpress.Xpf.Grid.GridColumnHeader$#  DevExpress.Xpf.Grid.GridColumn$$  DevExpress.Xpf.Grid.ColumnBase  grayCell= �� BasedOn9%  3DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension�&  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Grid.Themes.GridRowThemeKeys, DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]> &  ResourceKey��%�  &	       '   S     '"  �     '#  �    &	 �    0�� *%  $> LightweightCellStyle=�+1'  �    0�� *%  $> LightweightCellStyle=�+1�� "  .��� 	 � ��

 ������� 	 � ��

 �   ��� 	 � ��$
 14(��� "   .��� 	 �@! HeaderTemplate
 �� .R��� $
��Bold"�
���   ����� �� " "  ��  �� $
 win1��$ DataContext.MakinaGrupKod� SH.a��� 	 � ��

 ��Zj��� 	 � ��

 �������  �� $
 win1��$ DataContext.MakinaGrupKod� FH.a��� 	 � ��

 ������� 	 � ��

 �   ���  �� $
 win1��$ DataContext.MakinaGrupKod� SP.a��� 	 � ��

 ������ 	 � ��

 �   ���  �� $
 win1��$ DataContext.MakinaGrupKod� DLM.a��� 	 � ��

 ��·��� 	 � ��

 �   ��� " # .��� (	 �@!  HorizontalHeaderContentAlignment$
 Center=��� 	 �@$ FilterPopupMode$
 Excel=��� "  = 2  ��� 	 �@ 
Background

 ����� P�  �@ SelectionState$ FocusedAndSelected=�. ��� 	 �@ 
Background

 ���� �� 	 � ��

 �   ��� "  = 2   P�  �@ SelectionState$ FocusedAndSelected=�. ��� 	 �@ 
Background

 ���� �� 	 � ��

 �   �?   View? #'  DevExpress.Xpf.Grid.TableView'  -   @ ' Name$@ View1��A '  AllowChangeBandParentA . B '  AllowChangeColumnParentB . C   AllowColumnFilteringC . D   AllowSortingD .  E '  	AutoWidthE .  F   ClipboardCopyOptions$F Excel=�G   ColumnFilterPopupMode$G Excel=�H   EnableImmediatePostingH . I   EnterMoveNextColumnI .  J '  IndicatorWidth$J 30Q�K   NavigationStyle$
K Cell=�L '  NewItemRowPosition$
L None=�M '  	PasteMode$M Update=�N '  RowMinHeight$N 40Q�O   ShowFocusedRectangleO . &(   DevExpress.Xpf.Grid.GridViewBaseP (  ShowGroupPanelP .  Q   ShowTotalSummaryQ .  R   FocusedCellBorderTemplateR �� .R��� $��0q�
��� d �$	��1.5q�S '  FormatConditionsS ))  #DevExpress.Xpf.Grid.FormatCondition)  -*  'DevExpress.Xpf.Grid.FormatConditionBaseT *  
Expression$T [SüreProblemliMi] =1��U *  	FieldName$U SüreDk��1+  +DevExpress.Xpf.Grid.ExpressionConditionBaseV +  Format.V 6,  0DevExpress.Xpf.Core.ConditionalFormatting.Format,  W ,  
Background
W �  ��X ,  
Foreground
X �����Y ,  TextDecorationsY Z� Z   ColumnHeaderTemplateZ �� .R��� $��35��$��Right=�$��Stretch=�$��DemiBold"�$( Center=�$
��Wrap=�����  [   RowCellMenuCustomizations[ '-  !DevExpress.Xpf.Bars.BarButtonItem-  !.  DevExpress.Xpf.Bars.BarItem\ .  Command$\ ApplicationCommands.Copy��] .  Content)/  #DevExpress.Xpf.Core.ObjectConverter$] Kopyala/ ^ .  Glyph^   $ Copy_16x16.png -  $\ ApplicationCommands.Paste��$] Yapıştır/ ^   $ Paste_16x16.png -  $] İçeriğiTemizle/ \ �� *(&View.DataContext.IcerigiTemizleCommand+_ .  CommandParameter_ �� $ View1��-  $] -------------/ -  $] Ekle.../ \ �� *'%View.DataContext.UsteSatirEkleCommand+_ �� $ View1��-  $] Sil.../ \ �� *View.DataContext.SilCommand+_ �� $ View1��^   $ Delete_16x16.png "` '  RowIndicatorContentTemplate` �� a � 	Resourcesa B0   <Pandap.UI.Modules.UretimIsletmeModule.HandleToIndexConverter  conv��%   &	       0  .R��� $( Center=���`� b `� 	Converter#b [ .1��� $ RowHandle.Value��� $ 	View.Grid�c   Bandsc    -   d ! Name$d operatorBobin��e !  Header$%e OPERATÖR / BOBİN BİLGİLERİ/ f    Columns.f #  -   $d id��g !  Width21  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$g 451 h !  MinWidth$h 25��i $  	FieldName$i Id��j $  FilterPopupMode$j Excel=�k !  Visiblek .  l $  CellTemplatel �� .R��� .+�  ) �� *RowData.Row.Id+  $. Kayit Eklenme Tar��) �� * RowData.Row.KayitEklenmeTarihi+#  -   $d operator��$	g 1001 $h 25��m $  	CellStyle#m [ $i 	Operatör��$j Excel=�$e 	Operatör/ #  -	   $d kafileNo��$g 751 $i GirişKafileNo��$e Giriş Kafile No/ n $  EditSettingsn E2  ?DevExpress.Xpf.Editors.Settings.Extension.TextSettingsExtension2  E3  ?DevExpress.Xpf.Editors.Settings.Extension.BaseSettingsExtension!o 3  HorizontalContentAlignment$o Center=�#  -
   $d bobinNo��$g 701 $i GirişBobinNo��$e Giriş Bobin No/ n 2  $o Center=�p 2  DisplayFormat$p n0��#  -   $d miktarKg��$g 701 $i GirişBobinAğırlığı��$e Giriş Bobin Ağ./ n 64  0DevExpress.Xpf.Editors.Settings.CalcEditSettings4  65  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!q 5  HorizontalContentAlignment$q Center=�r 5  DisplayFormat$r n0��66  0DevExpress.Xpf.Editors.Settings.TextEditSettingss 6  Mask$
s ####��t 6  	MaxLength$t 4��#  -   $d cikisKafileNo��$g 751 $i ÇıkışKafileNo��$e Çıkış Kafile No/ n 2  $o Center=�#  -   $d cikisBobinNo��$g 701 $i ÇıkışBobinNo��$e Çıkış BobinNo/ n 2  $o Center=�$p n0��#  -   $d cikisBobinAgirligi��$g 701 $!i ÇıkışBobinAğırlığı��$e Çıkış Bobin Ağ./ n 4  $q Center=�$r n0��$
s ####��$t 4��   -   $d runSure��$e 	RUN SÜRE/ u !  HeaderStyle#u [ .f #  -   $d baslamaZamani��$g 701 #m [ v $  CopyValueAsDisplayTextv .  $i BaşlangıçSaati��$e Başlama Zamanı/ n 67  0DevExpress.Xpf.Editors.Settings.DateEditSettings7  $q Center=�w 5  AllowNullInputw . $s HH:mm��x 6  MaskType$x DateTimeAdvancingCaret=�y $  DisplayTemplatey �� .R��� $��Center=�$��Center=�.+��� $
��Bold"�$( Center=����� *RowData.Row.BaşlangıçSaati+z �� StringFormat$z HH:mm���� $��9(�$( Center=����� *RowData.Row.BaşlangıçSaati+$z 	dd/MM ddd��#  -   $d bitisZamani��$g 701 #m [ v .  $i BitişSaati��$e Bitiş Zamanı/ '{ !   HorizontalHeaderContentAlignment${ Center=�n 7  $q Center=�w . $r 	HH:mm ddd��$s HH:mm��$x DateTimeAdvancingCaret=�y �� .R��� $��Center=�$��Center=�.+��� $
��Bold"�$( Center=����� *RowData.Row.BitişSaati+$z HH:mm���� $��9(�$( Center=����� *RowData.Row.BitişSaati+$z 	dd/MM ddd��#  -   $d 	durusKodu��$g 701 #m [ $i 
DuruşKodu��n 6  $q Center=�w . #  -   $d durusAd��$	g 1401 #m [ $i MakinaDuruşAd��$e 	Duruş Ad/ | $  ReadOnly| . n 6  } 6  TextWrapping$
} Wrap=�#  -   $d sureDk��$g 601 #m [ $i SüreDk��| . n 2  $p n0��$o Center=�   -   $d 
BobinBoyut��$e KALINLIK / EN/ .f #  -   $d girisKalinlik��$g 701 $i GirişKalınlık��$e Giriş Kalınlık/ #  -   $d cikisKalinlik��$g 701 $i ÇıkışKalınlık��$e Çıkış Kalınlık/ #  -   $d girisEni��$g 701 $i 	GirişEni��$e 
Giriş Eni/ #  -   $d cikisEni��$g 701 $i ÇıkışEni��$e Çıkış Eni/ n 4  $q Center=�$r n2��   -   $d Diger��$e DİĞER/ .f #  -   $d 	kondusyon��$g 701 $i 
Kondüsyon��#  -   $d pasNo��$g 701 $i PasNo��#  -   $d nihaiSonPas��$g 701 $i NihaiSonPas��#  -   $d Alasim��$g 701 $i Alaşım��#  -   $	d cap��$g 701 $
i Çap��#  -    $d eksayisi��$g 701 $i 
EkSayısı��#  -!   $d eknedeni��$g 701 $i EkNedeni��#  -"   $d sonrakiIslem��$g 701 $i Sonrakiİşlem��#  -#   $d aciklama��$g 701 $i 
Açıklama��   -$   $d istatistikler��$e İSTATİSTİKLER/ #u [ .f #  -%   $d ortalamaKalinlikKatsayisi��$g 701 #m [ $ i Ort_Kalınlık_Katsayısı��$ e Ort.Kalınlık Katsayısı/ | . n 2  $p n0��#  -&   $d ortalamaEnKatsayisi��$g 701 #m [ $i Ort_EnKatsayısı��$j Excel=�$e Ort.En Katsayısı/ | . n 2  $p n0��#  -'   $d ortalamaKenarKesme��$g 701 #m [ $i KenarKesmeFiresi��| . n 2  $p n0��#  -(   $d isletmeFiresi��$g 701 #m [ $i İşletmeFiresi��$j Excel=�| . n 2  $p n0��#  -)   $d hurdaTon��$g 701 #m [ $i Hurda��$j Excel=�$e Hurda (ton)/ | . n 2  $p n3��