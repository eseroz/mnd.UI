   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni.DepoCikis/Pandap.UI.Modules.SatinAlmaModuleYeni.DepoCikis  C    =Pandap.UI.Modules.SatinAlmaModuleYeni.DepoCikis.DepoCikisView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  Ilocal=clr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni.DepoCikis  U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 .���   � 	Resources     editText �� 
TargetType%  DevExpress.Xpf.Editors.TextEdit �� Triggers �� Binding �� RelativeSource �� Value �� Property �� Value��%v   &	        �� "   ��  �� *IsKeyboardFocusWithin+ �� *Self+ True.a���  � ��
 � �������  ��  �� *BarkodNoGir+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=� True.a���  �@+�FocusedElement �� 
 �� ElementName$
 	TxtBarkod����  �� *	MiktarGir+$ TwoWay=�$	 PropertyChanged=� True.a���  �@+�FocusedElement �� $
 	TxtMiktar�� � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+�`� $��0��.�c�   MaterialDesignToolBar#���  .@��� $��50��  MaterialDesignFlatButton#���  ���� *YeniDepoCikisFisiCommand+.����  �� Orientation$ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PackIcon  $��40��$��50��$��Center=�   Kind$ FileDocument=��� $��4,0,0,0q�$��Center=�$��22(�$��Yeni Çıkış Fişi���� $��50��#���  ���� *DepoCikisFisiKaydetCommand+.���� $ 
Horizontal=�.+�  $��40��$��50��$��Center=�$ ContentSave=��� $��4,0,0,0q�$��Center=�$��25(�$��Fişi Kaydet���� $��50��#���  ���� *DepoCikisFisiYazdirCommand+.���� $ 
Horizontal=�.+�  $��40��$��50��$��Center=�$ Printer=��� $��4,0,0,0q�$��Center=�$��25(�$��Yazdır��# 	 MaterialDesignThemes.Wpf.Card  $��1��$��4q�.��� $��5q�$��Stretch=� � ColumnDefinitions �� $
��auto ��� $
��auto ��� $��6* ��� $
��auto ��� $
��auto ��� $
��auto �.+�  $��0��$	��155��$
��Left=�
�������$��17(�%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly .    NullText$ Fiş No��   	EditValue �� *DepoCikisFisi.FisNo+$ TwoWay=�$	 PropertyChanged=�  $��1��$	��100��$
��Left=�
�������$��18(� . $ Tarih�� �� *DepoCikisFisi.FisTarihi+$ TwoWay=�$	 PropertyChanged=�+  %DevExpress.Xpf.Grid.LookUp.LookUpEdit  $��2��$��Stretch=�   AutoPopulateColumns .  +  %DevExpress.Xpf.Editors.LookUpEditBase   DisplayMember$ Tanim��   FilterCondition$
 LikeX�$��18(�$
��Bold"�   IsPopupAutoWidth .  $   IsSynchronizedWithCurrentItem . '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable$ FalseX�$ Masraf Merkezi��*  $DevExpress.Xpf.Editors.PopupBaseEdit   PopupHeight$	 400Q�   
PopupWidth$	 600Q� w� TextPath$ MasrafMerkezleri��   ValueMember$	 Kod�� �� * DepoCikisFisi.MasrafMerkeziKod+$	 PropertyChanged=�   ItemsSource �� *MasrafMerkezleri+   SelectedItem �� *SeciliMusteri+$ TwoWay=�$	 PropertyChanged=�   StyleSettings >	  8DevExpress.Xpf.Grid.LookUp.SearchLookUpEditStyleSettings	     PopupContentTemplate �� .R�%
  DevExpress.Xpf.Grid.GridControl
    
 Name$  PART_GridControl��$��Stretch=�$��Normal"�! 
  View! #  DevExpress.Xpf.Grid.TableView  "   	AutoWidth" .  &   DevExpress.Xpf.Grid.DataViewBase&#   SearchPanelCriteriaOperatorType$	# And=�$ 
  Columns.$ $  DevExpress.Xpf.Grid.GridColumn  $  DevExpress.Xpf.Grid.BaseColumn%   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$% * &   MinWidth$	& 240��$  DevExpress.Xpf.Grid.ColumnBase'   	FieldName$' Tanim��(   Header)  #DevExpress.Xpf.Core.ObjectConverter$( Masraf Merkezi   -   $��	talepeden��$��3��$	��180��$��Stretch=�$��22(�$
��Bold"�$ Talep Eden Kişi�� �� *DepoCikisFisi.TalepEdenKisi+$ TwoWay=�$	 PropertyChanged=�  -   $��
teslimAlan��$��4��$	��180��$��Stretch=�$��22(�$
��Bold"�)  #DevExpress.Xpf.Editors.TextEditBase)   	MaxLength$) 25��$ Teslim Alan Kişi�� �� *DepoCikisFisi.TeslimAlanKisi+$ TwoWay=�$	 PropertyChanged=�)  #DevExpress.Xpf.Editors.ComboBoxEdit  -   $��ilgiliÜnite��$��5��$	��200��$��Stretch=�$��18(�$
��Bold"�$) 25��$ Çıkış Ünite��$
 Text��$ Value�� �� *
UniteListe+ �� *%#DepoCikisFisi.IlgiliUniteVarsayilan+$	 PropertyChanged=�  $��2��$��4q�.���� .+��� $��8q�$
��Left=�$ 
Horizontal=�.+��� $��20,0,0,0q�$ 
Horizontal=�.+�  -   $��	TxtBarkod��$	��190��$��20,0,0,0q�$��22(�$
��Bold"�$ 	Barkod No��#��[   �� *
AramaMetin+$ TwoWay=�$	 PropertyChanged=�* �� Delay$
* 2000��  $��52��$��32��$��	-50,0,0,0q�$��Center=�$ Barcode=�  -   $��	TxtMiktar��$	��100��$��20,0,0,0q�$
��Left=�$��22(�$
��Bold"�$ Miktar��#��[   �� *FisKalemEdit.CikisMiktar+  $	��100��$��20,0,0,0q�$
��Left=�$��22(�$
��Bold"� . $ 
Ölçü Br�� �� *BulunanStok.OLCU_BR1+$	 PropertyChanged=�  $	��400��$��20,0,0,0q�$
��Left=�$��22(�$
��Bold"� . $ 	Stok Adı�� �� *BulunanStok.STOKADI_TR+$	 PropertyChanged=�$ TwoWay=��� $	��200��$��50��$��10,0,0,0q�$��Right=�$��22(�+ �� 	IsDefault+ . ���� *FisKalemEkleCommand+.���� $ 
Horizontal=�.+�  $��30��$��30��$��Center=�$ ArrowDownBoldBox=��� $��4,0,0,0q�$��Center=�$��Listeye ekle��  $��3��$��5q�$��5q�$��Stretch=�+ 	 %MaterialDesignThemes.Wpf.ShadowAssist,   ShadowDepth$, Depth1=�.��
  $��20(�)  #DevExpress.Xpf.Grid.DataControlBase-   ItemsSource- �� *DepoCikisFisi.KalemlerDTO+$ TwoWay=�$	 PropertyChanged=�  *  $DevExpress.Xpf.Grid.GridColumnHeader&   System.Windows.Media.TextOptions��%�   '        '  �     �� "  .���  � ��
 �   ���  � ��
 �������  � ��$ 40����  � ��$
 Bold"���  �@ TextFormattingMode$ Ideal=��� "  .��� ( �@  HorizontalHeaderContentAlignment$ Center=�. 
  TotalSummary. )  #DevExpress.Xpf.Grid.GridSummaryItem  )  #DevExpress.Xpf.Grid.SummaryItemBase/   	Alignment$/ Default=�0   	FieldName$0 	STOK_KODU��1   SummaryType]
 YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5 
 /DevExpress.Data.Design.SummaryItemTypeConverter$1 Count !   -   2  Name$2 View1��3   AllowPerPixelScrolling3 . " . 4   RowMinHeight$4 40Q�&   DevExpress.Xpf.Grid.GridViewBase5   ShowGroupPanel5 .  6   ShowIndicator6 .  7   ShowTotalSummary7 . 8   ColumnHeaderTemplate8 �� .R��� $��40��$��Right=�$��Stretch=�$��DemiBold"�9 �� TextAlignment$9 Center=�$
��Wrap=����� .$   $% 50 $' StokKodu��$( 	Stok Kodu :   IsSmart: . ;   EditSettings; 6  0DevExpress.Xpf.Editors.Settings.TextEditSettings  6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!<   HorizontalContentAlignment$< Center=�  $	% 100 $' StokAd��$( 	Stok Adı : . =   ReadOnly= .   $% 50 $' CikisMiktar��$( Çıkan Miktar : . = . ;   $< Center=�>   DisplayFormat$> n2��  $% 50 $' OlcuBirimAd��$( Ölçü Brm : . ;   $< Center=�  $% 50 $' IlgiliUnite��$( İlgili Ünite : . ;   $< Center=�  $% 30 : . ?   CellTemplate? �� .R��� $��60��$��40��$��0q�
������ 
���  ������ *" View.DataContext.KalemSilCommand+���� *RowData.Row+.���� $ 
Horizontal=�.+�  $��40��$��50��$��Center=�$ Delete=��� $��4,0,0,0q�$��Center=�$��22(�