   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  i9clr-namespace:Pandap.UI.Modules.PlanlamaModule.DokumBolum+Pandap.UI.Modules.PlanlamaModule.DokumBolum  S.clr-namespace:Pandap.UI.MyControls.UI_Controls Pandap.UI.MyControls.UI_Controls  ?    9Pandap.UI.Modules.PlanlamaModule.DokumBolum.DokumListView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     AcontrolHelpers&clr-namespace:Pandap.UI.ControlHelpers     9
converters"clr-namespace:Pandap.UI.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   -helperclr-namespace:Pandap.UI.Helper   Glocal9clr-namespace:Pandap.UI.Modules.PlanlamaModule.DokumBolum   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006   U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes 	 ?
uiControls.clr-namespace:Pandap.UI.MyControls.UI_Controls 	    MaterialDesignPaper#���     MaterialDesignFont#���  $��13(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=� � 	Resources .   (Pandap.UI.Converters.AlasimRenkConverter  alasimRenkConverter2   ,Pandap.UI.Converters.AlasimFontRenkConverter  alasimFontRenkConverter��%   &	       &	          2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Xpf.Core.DXMessageBoxService  $   Pandap.UI.Helper.ExportService  -     Name$ servis1��   View %  System.Windows.Markup.Reference  *View1+'  !DevExpress.Mvvm.UI.EventToCommand  �	  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]] 	  	EventName$ Loaded��+
  %DevExpress.Mvvm.UI.EventToCommandBase 
  Command �� *FormLoadedCommand+.���  � RowDefinitions �� $
��auto ��� $��* �.+�' 	 !MaterialDesignThemes.Wpf.Snackbar  -   	  Name$	 Snackbar��$��2��$	��800��$��50��$��Stretch=�$��Bottom=�
 K� ZIndex$

 1000��   MessageQueue �� *BoundMessageQueue+c� $��0��$��50��}�.    MaterialDesignToolBar#���  .@���   MaterialDesignFlatButton#���  ���� *BobinEkleCommand+.����  �� Orientation$ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ FileDocument=��� $��8,0,0,0q�$��Center=�$��Yeni Döküm Planı���� $
��XLSX��#���  ���� *ExcelExportCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ 	FileExcel=��� $��8,0,0,0q�$��Center=�$��Excele Aktar���� $��0q�#���  ���� *EkranYenileCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Refresh=��� $��8,0,0,0q�$��Center=�$��Yenile���� $��	100,0,0,0q�$ 
Horizontal=�.+��� $	��100��$��Center=�$��DemiBold"�
���  ������ *FormUyariMesaj+]� $��1��$��8q�.+�]� $��0,8,8,8q�
�������$	��Top=�.+��� $��10,0,0,0q�$
��Left=�$ 
Horizontal=�.+��� $	��100��$��Center=�$��16(�$��SemiBold"�$��Döküm Hattı :��)  #DevExpress.Xpf.Editors.ComboBoxEdit  $��80��$��20,0,0,0q�$��Center=�
�������$��14(�$��DemiBold"�
���  ��+  %DevExpress.Xpf.Editors.LookUpEditBase   ItemsSource �� *DokumHatlari+   SelectedItem �� *SeciliDokumHatti+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�  $	��100��$��8,0,0,0q�$��Center=�$��14(�$
��Bold"� �� *Yillar+ �� *	SeciliYil+$ TwoWay=�$ PropertyChanged=��� $��Right=�$ 
Horizontal=�%  DevExpress.Xpf.Grid.GridControl  $��8q�)  #DevExpress.Xpf.Grid.DataControlBase   ItemsSource �� *DokumKafileListe+   SelectedItem �� *SeciliKayit+$ PropertyChanged=�   DetailDescriptor 1  +DevExpress.Xpf.Grid.ContentDetailDescriptor  .  (DevExpress.Xpf.Grid.DetailDescriptorBase   ContentTemplate �� .R�  $	��500��   SelectionMode$
 Cell=� �� *DokumBobinIslemAdimlari+ *  $DevExpress.Xpf.Grid.GridColumnHeader �� 
TargetType �� Property �� Value$  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.BaseColumn��%�   '        '  �     �� "  .���  � ��
 � E����  � ��
 �������  � ��$ 40����  � ��$
 Bold"���  �@ TextFormattingMode$ Ideal=��� "  .��� ( �@  HorizontalHeaderContentAlignment$ Center=�   View #  DevExpress.Xpf.Grid.TableView     	AutoWidth .  &   DevExpress.Xpf.Grid.DataViewBase   NavigationStyle$
 Cell=�&   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     ShowIndicator .     FormatConditions )  #DevExpress.Xpf.Grid.FormatCondition  -  'DevExpress.Xpf.Grid.FormatConditionBase    
ApplyToRow  .  !   
Expression$! [AktifMi] = 'True'��"   	FieldName$" SiraNo��#   	ValueRule$# 
Expression=�$   Value1$ Z� 1  +DevExpress.Xpf.Grid.ExpressionConditionBase%   Format.% 6  0DevExpress.Xpf.Core.ConditionalFormatting.Format  &   FontSize$& 12Q�'   
FontWeight$' DemiBold"�(   
Foreground
( �  ��)   Icon) *  $DevExpress.Xpf.Core.IconSetExtension  *   Name$* ArrowsGrey5_3��+   TextDecorations+ Z� ,   ColumnHeaderTemplate, �� .R��� $��Right=�$��Stretch=�$��DemiBold"�- �� TextAlignment$- Center=�$
��Wrap=����� .   Columns.   /   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$/ 70 $  DevExpress.Xpf.Grid.ColumnBase0   	FieldName$0 SiraNo��1   Header)   #DevExpress.Xpf.Core.ObjectConverter$1 SıraNo  2   EditSettings2 E!  ?DevExpress.Xpf.Editors.Settings.Extension.TextSettingsExtension!  E"  ?DevExpress.Xpf.Editors.Settings.Extension.BaseSettingsExtension!3 "  HorizontalContentAlignment$3 Center=�  $	/ 120 $0 MakinaIslem��$1 Makina Islem    $	/ 180 $0 ProsesMetin��$1 Proses Parametreleri  2 !  $3 Center=�  $	/ 130 $0 	EzmeYuzde��$1 Ezme % (varsa)  2 6#  0DevExpress.Xpf.Editors.Settings.TextEditSettings#  6$  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!4 $  HorizontalContentAlignment$4 Center=�5 $  DisplayFormat$5 p0��  $/ * $0 Aciklama��6   TotalSummary6 )%  #DevExpress.Xpf.Grid.GridSummaryItem%  )&  #DevExpress.Xpf.Grid.SummaryItemBase7 &  DisplayFormat$7 
Say:{0:n0}��8 &  	FieldName$8 Id��9 &  SummaryType]
 YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5' 
 /DevExpress.Data.Design.SummaryItemTypeConverter$9 Count' %  $7 
Top:{0:n0}��$8 RedMiktarKg��$	9 Sum'    s0((  "DevExpress.Xpf.Grid.BaseGridHeader  s1 	 s2 
 
ctemplate1: �� CornerRadius%)  DevExpress.Xpf.Editors.TextEdit&*   DevExpress.Xpf.Core.ThemeManager; *  	ThemeName0+  *DevExpress.Xpf.Core.ThemeNameTypeConverter< �� Triggers= �� Value> �� Binding? �� Path@ �� 
TargetName  ctemplate1_tarih%,  DevExpress.Xpf.Editors.DateEditA )  MaskB )  MaskTypeC )  MaskUseAsDisplayFormat)-  #DevExpress.Xpf.Grid.GridControlBand��%  '        '  �     &	     &	 q    &		 �    &	
 /    &	 *    '-  ]    �� "  .���  � �� �	#FF673AB7��  � ��
 �������  � ��$ 40����  � ��$
 Bold"���  �@ TextFormattingMode$ Ideal=��� "  .��� ( �@  HorizontalHeaderContentAlignment$ Center=���  �@ ReadOnly . �� " ( .���  � ��
 �������  � ��
 �   ���  � ��$ SemiBold"���  � ��$ 14(��� " ( .���  � ��
 �������  � ��
 �   ���  � ��$
 Bold"���  � ��$ 14(��� " ( .���  � ��
 �������  � ��
 �   ���  � ��$ 14(��� .R��� $	��br1��$��4q�
������ 
��� � �$��1q�$: 2��._�)  $��PART_Editor��$��Center=�$	; XXX+ < �� $
= True��> �� $? RowData.IsSelected�.a��� $@ PART_Editor�� � ��
 �  ���� $	@ br1�� � ��
 ������� .R��� $	��br1��$��4q�
������ 
��� � �$��1q�$: 2��._�,  $��PART_Editor��$	��200��$	; XXX+ $A dd/MM/yyyy HH:mm��$B DateTimeAdvancingCaret=�C . < �� $
= True��> �� $? RowData.IsSelected�.a��� $@ PART_Editor�� � ��
 �  ���� $	@ br1�� � ��
 ������� " - .���  �@ HeaderTemplate �� .R��� $
��Bold"�
���   ����� D   BandsD -  $1 	PLANLANAN  E   HeaderStyle#E [ F -  Columns.F   G   MaxWidth$G 50Q�$0 
DH_SatirId��$1 Id  #E [ H   IsSmartH . 2 !  $3 Center=�  $	/ 200 I   AllowCellMerge$
I TrueX�$0 
PlanTarihi��$1 Tarih  #E [ H . 2 !  $3 Center=�J !  DisplayFormat$J d��  $0 BobinSiraNo��$1 	Bobin S/N  #E [ H . 2 !  $3 Center=�  $0 DokumHattiKod��$1 Dokum Hattı  #E [ H . 2 !  $3 Center=�  $0 PlanKalinlik��$1 
Kalınlık  #E [ H . 2 !  $3 Center=�  $0 PlanEn��$1 Döküm Eni  #E [ H . 2 !  $3 Center=�  $0 
PlanMiktar��$1 Miktar  #E [ H . 2 !  $3 Center=�$J n0��  K   MinWidth$K 89��$G 90Q�$0 AlasimTipKod��$1 Alaşım  #E [ H . L   CellTemplateL �� .R��� $��4q�
���   �$��1q�$: 2������ *RowData.Row.AlasimTipKod+M �� 	Converter#M [ ._�)  $��PART_Editor��$��Center=�$	; XXX+ $��DemiBold"����� *RowData.Row.AlasimTipKod+#M [   $K 80��$0 PlanBobinNo��$1 Bobin No  #E [ H . 2 !  $3 Center=�-  -   N  Name$N gerceklesen��$1 GERÇEKLEŞEN  #E [ .F   $	K 125��O   AllowEditing5. 
 /DevExpress.Utils.Design.DefaultBooleanConverter$
O true. $0 ReelBitisTarihi��$1 Başlama /Bitiş Tarihi  #E [ H . P   ReadOnlyP .  L �� .R��� $��4q�$��Center=�.+��� ���� *RowData.Row.ReelBaslamaTarihi+Q �� StringFormat$Q dd.MM.yyyy HH:mm���� 
���  ������ *RowData.Row.ReelBitisTarihi+$Q dd.MM.yyyy HH:mm��  $0 
ReelMiktar��$1 Miktar  #E [ H . P .   $
O true. $0 ReelKalinlik��$1 
Kalınlık  #E [ H . P .  2 !  $3 Center=�  $
O true. $0 ReelEn��$1 En  #E [ H . P .  2 !  $3 Center=�  $0 DokumOperatorAdSoyad��$1 Operator  #E [ H . P .  2 !  $3 Center=�-  $1 #  .F   $G 50Q�$
O True. $0 Aciklama��$	1 Not  #E [ P .  2 6/  0DevExpress.Xpf.Editors.Settings.MemoEditSettings/    $G 50Q�$0 MesajSayisi��$1 Mesaj  #E [   
MesajKolon#��[    -   $��View1��R   AllowPerPixelScrollingR .  . S   RowMinHeight$S 40Q�T   ShowAutoFilterRowT .  .  U   ShowTotalSummaryU . , �� .R��� $��30��$��Right=�$��Stretch=�$��DemiBold"�$- Center=�$
��Wrap=����� 