   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  U/clr-namespace:Pandap.UI.Modules.OperasyonModule!Pandap.UI.Modules.OperasyonModule  9    3Pandap.UI.Modules.OperasyonModule.YariMamulDataView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9
converters"clr-namespace:Pandap.UI.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   -helperclr-namespace:Pandap.UI.Helper   =local/clr-namespace:Pandap.UI.Modules.OperasyonModule   U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 2  ,DevExpress.Mvvm.UI.Interactivity.Interaction    	Behaviors  $   Pandap.UI.Helper.ExportService     View %  System.Windows.Markup.Reference  *View1+'  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+  $ Unloaded�� �� *FormUnLoadedCommand+ � 	Resources    
ctemplate1 �� CornerRadius%  DevExpress.Xpf.Editors.TextEdit&   DevExpress.Xpf.Core.ThemeManager   	ThemeName0	  *DevExpress.Xpf.Core.ThemeNameTypeConverter)
  #DevExpress.Xpf.Editors.TextEditBase 
  TextWrapping �� Triggers	 �� Value
 �� Binding �� Path �� 
TargetName �� Property �� Value��%  &	        �� .R��� $	��br1��$��5q�
������ 
����İ�$��1q�$ 1��._�  $��PART_Editor��$
��Left=�$	 XXX	 $
 Wrap=� �� $
	 True��
 �� $ RowData.IsSelected�.a��� $ PART_Editor�� � ��
 �  ���� $	 br1�� � ��
 �����.���  � RowDefinitions �� $
��auto ��� $��* �.+�' 	 !MaterialDesignThemes.Wpf.Snackbar  -     Name$ Snackbar��$��2��$	��800��$��50��$��Stretch=�$��Bottom=� K� ZIndex$
 1000��   MessageQueue �� *BoundMessageQueue+`� $��0��.�c� $��50��}�.    MaterialDesignToolBar#���  .@���   MaterialDesignFlatButton#���  ���� *KaydetCommand+.����  �� Orientation$ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PackIcon  -     Name$ PackIcon��$��Center=�   Kind$ ContentSave=��� $��8,0,0,0q�$��Center=�$��Kaydet���� �� $��Center=�$��14(�$
��Bold"�$��Gün : ��%  DevExpress.Xpf.Editors.DateEdit  $	��100��$��30��$��14(�$
��Bold"�   DateTime �� *	SeciliGun+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�# 	 MaterialDesignThemes.Wpf.Card  $��1��$��5q�$��5q�$��Stretch=�+ 	 %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=�.��%  DevExpress.Xpf.Grid.GridControl  $��13(�$��DemiBold"�)  #DevExpress.Xpf.Grid.DataControlBase   ItemsSource �� *YariMamulHatData+$ TwoWay=�$ PropertyChanged=�   GridControlBaslikSitil1 �� BasedOn*  $DevExpress.Xpf.Grid.GridColumnHeader �� 
TargetType.  (DevExpress.Xpf.Grid.BaseGridColumnHeader��%�   '        7  �� 8   "  .���  � ��$ 40����  � ��
 �������  �@ ShowFilterButtonOnHover .  ��  � ��$	 Top=�   TotalSummary )  #DevExpress.Xpf.Grid.GridSummaryItem  )  #DevExpress.Xpf.Grid.SummaryItemBase   DisplayFormat$ n0��   	FieldName$ Toplam��    SummaryType]
 YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5 
 /DevExpress.Data.Design.SummaryItemTypeConverter$	  Sum !   View! #  DevExpress.Xpf.Grid.TableView  -   "  Name$" View1��#   AllowPerPixelScrolling# . $   	AutoWidth$ . %   NewItemRowPosition$
% None=�&   RowMinHeight$& 26Q�&   DevExpress.Xpf.Grid.GridViewBase'   ShowGroupPanel' .  &   DevExpress.Xpf.Grid.DataViewBase(   ShowTotalSummary( . )   ColumnHeaderTemplate) �� .R��� $��30��$��Right=�$��Stretch=�$��DemiBold"�
�������$
��Wrap=����� *   Columns.* $  DevExpress.Xpf.Grid.GridColumn  $  DevExpress.Xpf.Grid.BaseColumn+   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$+ 70 $  DevExpress.Xpf.Grid.ColumnBase,   	FieldName$, Sirano��-   Header)  #DevExpress.Xpf.Core.ObjectConverter$
- S.No '.    HorizontalHeaderContentAlignment$. Center=�/   IsSmart/ . 0   EditSettings0 6  0DevExpress.Xpf.Editors.Settings.TextEditSettings  6   0DevExpress.Xpf.Editors.Settings.BaseEditSettings!1    HorizontalContentAlignment$1 Center=�  2   MinWidth$	2 200��$	, Hat��/ . 0   3   TextWrapping$
3 Wrap=�  $, _1050��$
- 1050 / . 0   $
3 Wrap=�  $, _1100��$
- 1100 / . 0   $
3 Wrap=�  $, _1200��$
- 1200 / . 0   $
3 Wrap=�  $, _3003��$
- 3003 / . 0   $
3 Wrap=�  $, _8011��$
- 8011 / . 0   $
3 Wrap=�  $, _8079��$
- 8079 / . 0   $
3 Wrap=�  $, _8156��$
- 8156 / . 0   $
3 Wrap=�  $, _8006F��$- 8006F / . 0   $
3 Wrap=�  $, _8006M��$- 8006M / . 0   $
3 Wrap=�  $, Toplam��$- Toplam / . 0   $
3 Wrap=�