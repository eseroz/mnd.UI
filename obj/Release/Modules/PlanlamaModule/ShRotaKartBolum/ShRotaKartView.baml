   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  s>clr-namespace:Pandap.UI.Modules.PlanlamaModule.ShRotaKartBolum0Pandap.UI.Modules.PlanlamaModule.ShRotaKartBolum  C    =Pandap.UI.Modules.PlanlamaModule.RotaKartBolum.ShRotaKartView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  j fDevExpress.Xpf.LayoutControl.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  +helperclr-namespace:Pandap.UI.Helper  Jlocal>clr-namespace:Pandap.UI.Modules.PlanlamaModule.ShRotaKartBolum  U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 -     �Name$  RotaKart��$	��968��$	��750��$��16(� � 	Resources    	ust_label �� 
TargetType �� Property �� Value-  'DevExpress.Xpf.LayoutControl.LayoutItem��%�   &	        '  A     �� " ��.���  � ��$ Center=���  � ��$ Center=��� "  .���  � ��
 ���� ��  �@ ElementSpace$ 0Q���  � ��$ Stretch=���  � ��$ Stretch=���  � �� .  ��  � ��$	 Top=�.��]� .+�(  "DevExpress.Xpf.Bars.ToolBarControl  $	��0,0q�
����� �,  &DevExpress.Xpf.Bars.ToolBarControlBase   BarItemDisplayMode$ Content=�$	��Top=�   Items. '  !DevExpress.Xpf.Bars.BarButtonItem  !  DevExpress.Xpf.Bars.BarItem   BarItemDisplayMode$ ContentAndGlyph=�   Content)  #DevExpress.Xpf.Core.ObjectConverter$ Kaydet   	   Command	 �� *GuncelleCommand+
   CommandParameter
 ��    Glyph *  $DevExpress.Xpf.Core.DXImageExtension     Image1  +DevExpress.Xpf.Core.Native.DXImageConverter$ SaveTheme_16x16.png    
LargeGlyph   $ SaveTheme_32x32.png   $ ContentAndGlyph=�$ Yazdır 	 �� *YazdirCommand+
 ��  4	  .DevExpress.Xpf.Core.DXImageOffice2013Extension	   	  Image;
  5DevExpress.Xpf.Core.Native.DXImageOffice2013Converter$ Copy_16x16.png
    $ Print_32x32.png �� 
�������.��# 	 MaterialDesignThemes.Wpf.Card  -     Name$ 
ciktiSayfa��$	��800��$��0,5,0,5q�$��20q�.���� .+��� $��30��$��10q�.+���  �� Orientation$ 
Horizontal=�.+��� $��Center=�$��20(�$��DemiBold"�$��	Kart No :���� $	��200��$��10,0,0,0q�$��0q�$��22(�$
��Bold"�
���  ������ *ShRotaKartModel.KartNo+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��
10,10,0,20q�$ 
Horizontal=�.+��� $	��200��.+��� $��36��$��Stretch=�$��Center=�
�������
�������$��1q�$��ALAŞIM��$��20(�$
��Bold"�%  DevExpress.Xpf.Editors.TextEdit  $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�   CharacterCasing$ Upper=�$��20(�$��DemiBold"�%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *ShRotaKartModel.AlasimKod+$ PropertyChanged=��� $	��200��.+��� $��36��$��Stretch=�$��Center=�
�������
�������$��1q�$
��KOND��$��20(�$��DemiBold"�  $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�$ Upper=�$��20(�$��DemiBold"� �� *ShRotaKartModel.Kondisyon+$ PropertyChanged=��� $	��200��.+��� $��36��$��Stretch=�$��Center=�
�������
�������$��1q�$��KALINLIK��$��20(�$��DemiBold"�  $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�$ Upper=�$��20(�$��DemiBold"� �� *ShRotaKartModel.Kalinlik+$ PropertyChanged=��� $	��200��.+��� $��36��$��Stretch=�$��Center=�
�������
�������$��1q�$��EN��$��20(�$��DemiBold"�  $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�$ Upper=�$��20(�$��DemiBold"� �� *ShRotaKartModel.En+$ PropertyChanged=��� $	��200��.+��� $��36��$��Stretch=�$��Center=�
�������
�������$��1q�$��BOBİN ADEDİ��$��20(�$��DemiBold"��� $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�$��20(�$��DemiBold"����� *!ShRotaKartModel.DokumBobinAdedi+%  DevExpress.Xpf.Grid.GridControl  $	��220��$
��1000��$��	10,0,0,10q�   BorderBrush
 �   �   BorderThickness$ 1q�$��14(�)  #DevExpress.Xpf.Grid.DataControlBase   
ShowBorder .    ItemsSource �� *ShRotaKartModel.DokumBobinler+$ TwoWay=�$ PropertyChanged=� $  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.ColumnBase$  DevExpress.Xpf.Grid.BaseColumn]
 YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5 
 /DevExpress.Utils.Design.DefaultBooleanConverter(  "DevExpress.Xpf.Grid.BaseGridHeader��%N  '        '  �     �� "  .���  �@ ReadOnly . �� ( �@  HorizontalHeaderContentAlignment$ Center=���  �@ AllowSorting$ False ��  �@ AllowColumnFiltering$ False �� "  .���  � ��
 �������  � ��
 �   ���  � ��$
 Bold"���  � ��$ 40����  � ��$ 14(�   View #  DevExpress.Xpf.Grid.TableView  &   DevExpress.Xpf.Grid.GridViewBase   AllowGrouping .  &   DevExpress.Xpf.Grid.DataViewBase   Header$ Seçilen Bobinler��   NewItemRowPosition$
 None=�   ShowGroupPanel .     ShowIndicator .     ColumnHeaderTemplate �� .R��� $��40��$��Center=�$��Stretch=�$��DemiBold"�  �� TextAlignment$  Center=�$
��Wrap=����� !   Columns.!   "   MaxWidth$" 70Q�#   	FieldName$# SiraNo��$   Header$$ SıraNo %   EditSettings% E  ?DevExpress.Xpf.Editors.Settings.Extension.TextSettingsExtension  E  ?DevExpress.Xpf.Editors.Settings.Extension.BaseSettingsExtension!&   HorizontalContentAlignment$& Center=�  $# BobinNo��$$ BobinNo %   $& Center=�  $" 90Q�$# DokumMiktari��$$ Döküm Mik (kg) %   $& Center=�  $" 90Q�$# DokumEni��$$ Döküm Eni (mm) %   $& Center=�  '   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$' * $# Aciklama��  $	��240��$
��1000��$��10,0,0,0q�$��14(� �� *ShRotaKartModel.Fazlar+$ TwoWay=�$ PropertyChanged=�   g2��%N  '        &	 �     �� "  .���  �@ ReadOnly . �� ( �@  HorizontalHeaderContentAlignment$ Center=���  �@ AllowSorting$ False ��  �@ AllowColumnFiltering$ False �� "  .���  � ��
 �������  � ��
 �   ���  � ��$
 Bold"���  � ��$ 40����  � ��$ 14(�    .  $% Makina Aşamaları (Reçeteden)��$
 None=�(   PrintBandHeaderStyle#( [  .   .   �� .R��� $��40��$��Center=�$��Stretch=�$��DemiBold"�$  Center=�$
��Wrap=����� .!   $" 70Q�$# SiraNo��$$ SıraNo %   $& Center=�  $' 80 $# MakinaIslem��$$ Makina Islem   $	' 120 $# ProsesMetin��$$ Proses Parametreleri %   $& Center=�  $	' 100 $# 	EzmeYuzde��$$ Ezme Oranı (varsa) % 6  0DevExpress.Xpf.Editors.Settings.TextEditSettings  6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!)   HorizontalContentAlignment$) Center=�*   DisplayFormat$* p0��  $' * $# Aciklama��