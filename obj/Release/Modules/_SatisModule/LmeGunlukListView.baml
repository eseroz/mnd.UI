   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Pandap.UI.Modules._SatisModulePandap.UI.Modules._SatisModule  6    0Pandap.UI.Modules._SatisModule.LmeGunlukListView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     >local,clr-namespace:Pandap.UI.Modules._SatisModule     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  .���   � RowDefinitions  �� $
��auto ��� $��* �.+�c� $��0��$��50��}�.     MaterialDesignToolBar#���   .@���   MaterialDesignFlatButton#���  ���� *YeniCommand+.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ FileDocument=��� $��8 0 0 0q�$��Center=�$
��Yeni���� #���  ���� *KaydetCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� $��0q�#���  ���� *EkranTazeleCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Refresh=��� $��8 0 0 0q�$��Center=�$��Ekran Tazele���� #���  $
��XLSX������ *ExcelExportCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ 	FileExcel=��� $��8 0 0 0q�$��Center=�$��Excele Aktar��%  DevExpress.Xpf.Grid.GridControl  $��1��)  #DevExpress.Xpf.Grid.DataControlBase   ItemsSource �� *LmeGunlukListe+   View #  DevExpress.Xpf.Grid.TableView     NewItemRowPosition$ Bottom=�&   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     AllowPerPixelScrolling . &   DevExpress.Xpf.Grid.DataViewBase   ShowTotalSummary . 	   Columns.	 $  DevExpress.Xpf.Grid.GridColumn  $  DevExpress.Xpf.Grid.ColumnBase
   	FieldName$
 Tarih��   IsSmart .    EditSettings 6	  0DevExpress.Xpf.Editors.Settings.DateEditSettings	  6
  0DevExpress.Xpf.Editors.Settings.TextEditSettings 
  MaskType$ DateTimeAdvancingCaret=� 
  MaskUseAsDisplayFormat .   $
 Lme_USD�� .  
  6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings   DisplayFormat$ n0��$ Numeric=�  $
 Lme_EUR�� .  
  $ n0��$ Numeric=�  $
 Lme_GBP�� 
  $ n0��$ Numeric=�