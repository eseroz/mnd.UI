   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:Pandap.UI.Modules._SatisModule.MusteriAramalar.Pandap.UI.Modules._SatisModule.MusteriAramalar  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  I    CPandap.UI.Modules._SatisModule.MusteriAramalar.MusteriAramaListView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Nlocal<clr-namespace:Pandap.UI.Modules._SatisModule.MusteriAramalar     a ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aGdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  3
converters"clr-namespace:Pandap.UI.Converters  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  $��14(�  � 	Resources   �� 
TargetType   MyLinkButton2 �� Property �� Value �� 
TargetType �� Orientation ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName��%}  &	        �� " ��.���  � ��
 ��  ���  � �� �� " ��.R��� $ 
Horizontal=�.+��� $ 	TextBlock��$��Center=�$��Center=����	#FF2B7AD2�� .F���  P�  �@J�IsMouseOver	 . . ��� $
 	TextBlock�� � ��$ 	Underline{���  �@�Cursor$
 Hand����  � ��
 �""��P�  � {�	 .  . ��� $
 	TextBlock�� � ��
 �����.���  � RowDefinitions �� $
��auto ��� $��* �.+�c� $��0��$��50��}�.    MaterialDesignToolBar#���  .@��� $��0��  MaterialDesignFlatButton#���  ���� *AramaAddEditCommand+.���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ FileDocument=��� $��8 0 0 0q�$��Center=�$��Yeni Arama Kaydı���� $��0q�#���  ���� *EkranYenileCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Refresh=��� $��8 0 0 0q�$��Center=�$��Ekran Yenile���� #���  $
��XLSX������ *ExcelExportCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ 	FileExcel=��� $��8 0 0 0q�$��Center=�$��Excele Aktar��%  DevExpress.Xpf.Grid.GridControl  $��1��)  #DevExpress.Xpf.Grid.DataControlBase   SelectedItem �� *SeciliArama+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   ItemsSource �� *MusteriAramalar+$ TwoWay=�$ PropertyChanged=�  *  $DevExpress.Xpf.Grid.GridColumnHeader&   System.Windows.Media.TextOptions$  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.ColumnBase$  DevExpress.Xpf.Grid.BaseColumn]	 YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5	 	 /DevExpress.Utils.Design.DefaultBooleanConverter��%�  '        '  �     �� "  .���  � ��
 �   ���  � ��
 �������  � ��$ 40����  � ��$
 Bold"���  �@ TextFormattingMode$ Ideal=��� "  .���  �@ ReadOnly . �� ( �@  HorizontalHeaderContentAlignment$ Center=���  �@ AllowSorting$ False	 ��  �@ AllowColumnFiltering$ False	 ��  �@ TextFormattingMode$ Ideal=�   View #
  DevExpress.Xpf.Grid.TableView
  -    
 Name$ View1�� 
  RowMinHeight$ 35Q� 
  AllowPerPixelScrolling .  
  	AutoWidth .   
  ShowAutoFilterRow . &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .  &   DevExpress.Xpf.Grid.DataViewBase   ShowTotalSummary .  
  FormatConditions )  #DevExpress.Xpf.Grid.FormatCondition  -  'DevExpress.Xpf.Grid.FormatConditionBase   
ApplyToRow .    
Expression$/ )[MusteriGrubuAdı] = 'Potansiyel Dışı'��   	FieldName$ MusteriGrubuAdı��1  +DevExpress.Xpf.Grid.ExpressionConditionBase   Format. 6  0DevExpress.Xpf.Core.ConditionalFormatting.Format     
Foreground
 �  ��   TextDecorations Z�     ColumnHeaderTemplate  �� .R��� ! �� TextAlignment$! Center=�$��Stretch=�$��DemiBold"�$��35��$
��Wrap=����� "   Columns."   #   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$# 40 $   Header)  #DevExpress.Xpf.Core.ObjectConverter$$ Id %   	FieldName$% Id��&   IsSmart& . '   EditSettings' E  ?DevExpress.Xpf.Editors.Settings.Extension.TextSettingsExtension  E  ?DevExpress.Xpf.Editors.Settings.Extension.BaseSettingsExtension!(   HorizontalContentAlignment$( Center=�  ')    HorizontalHeaderContentAlignment$) Center=�$# 70 & . *   CellTemplate* �� .R�]� .+��� #��[  $��4q�$��50��$��40��$��Düzenle��$��12(�$��Center=����� *&$View.DataContext.AramaAddEditCommand+���� *RowData.Row.Id+  $$ Tarih $% Tarih��& .   $$ Müşteri Grubu $% MusteriGrubuAdı��& .   $% MusteriUnvan��& .   $% UlkeAdi��& .   $% 	KonuDetay��& .   $% Ekleyen��