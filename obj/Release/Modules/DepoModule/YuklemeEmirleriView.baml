   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Pandap.UI.Modules.DepoModulePandap.UI.Modules.DepoModule  M ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;*clr-namespace:System.Media;assembly=SystemSystem.Media ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  9!clr-namespace:Pandap.UI.BehaviorsPandap.UI.Behaviors  C&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  6    0Pandap.UI.Modules.DepoModule.YuklemeEmirleriView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:Pandap.UI.Modules.DepoModule     U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa	 ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  ]
 YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  ;sysmedia*clr-namespace:System.Media;assembly=System 
  5
converters"clr-namespace:Pandap.UI.Converters 
  3	behaviors!clr-namespace:Pandap.UI.Behaviors 
  =controlHelpers&clr-namespace:Pandap.UI.ControlHelpers 
  -     >�Name$  YuklemeView�� � 	Resources 5   /Pandap.UI.Converters.ImageFromAssemblyConverter   ImageFromAssemblyCnv �� 
TargetType  VisibleAnimation �� Property �� Value �� Triggers �� Binding �� Mode �� UpdateSourceTrigger	 �� ElementName
 �� Value N� EnterActions g� Duration	 t� To �� TargetProperty W� From	 W� To g� AutoReverse g� RepeatBehavior  PaletTemplate J� Opacity �� StringFormat �� Orientation'  !MaterialDesignThemes.Wpf.PackIcon   Kind �� TextAlignment  selectedAnimationItem �� RelativeSource �� Mode �� AncestorType �� CenterX �� CenterY �� 
TargetType �� CornerRadius �� Padding  J� SnapsToDevicePixels! �� Triggers" �� 
TargetName# P� Property$ P� Value% N� ExitActions��%l  &	        &	      &	 (    &	 c      �� " �.���  �@J�Opacity$ 0Q� ��  �� *DataContext.PaletBulunamadi+$ TwoWay=�$ PropertyChanged=�$	 YuklemeView��
 True �� .���� .��t� $ 	0:0:0.400D�$ 3X�$ BorderThickness�W� $ Opacity�$ 0X�$	 1.0X� . $	 10x��$ 0:0:1D��� .R��� .+��� $	��100��$��60��$)��#/Pandap;component/Content/palet.png��$	 0.8Q�$
��Fill=��� $��Center=�$
��Bold"�
���  ��$��14(����� *PaletNet_Kg+$ 	{0:n0} kg���� $��Center=�$ 
Horizontal=�.+�  $��Center=�$��20��$��20��$ Barcode=��� $��Center=�$��14(�$
��Bold"�
���   ����� *	PaletId+�� $ 
Horizontal=�$��Center=�.+��� $
��Bold"�$��12(����� *
Kalinlik+�� $��12(�$	�� x ���� $
��Bold"�$��12(����� *En+�� $ Center=�$	��100��$��10(�$��Center=����� *CariAd+�� " ��.���  � |� �� *1/DataContext.EkrandanYuklemeEmriDegistirebilirMi+ �� $ FindAncestor=�$ UserControlL���  �@�LayoutTransform �� $ 0Q�$ 0Q�scaleTransform����  � �� �� " ��.R��� $��Border��$��4q�$ 6��
�������$��1q�$ 6q�  . ._��� ! �� $

 True�� �� *YuklemeBittiMi+.a��� $" Border�� � �� �	#FF008272�� $" Border�� � ��$ 5q��� $" Border�� �@��CornerRadius$ 4��P� # �@��
IsSelected$ . . ��� $" Border�� � ��$	 180���� $" Border�� � �� �	#FF429CD6�� $" Border�� � ��$ 4q��� $" Border�� �@��CornerRadius$ 14���� $" Border�� � �� �	#FF92CAF4 �� .���� .��W� $ LayoutTransform.ScaleX�$	 1.2X�$ 0:0:.5D�W� $ LayoutTransform.ScaleY�$	 1.1X�$ 0:0:.5D�% �� .���� .��W� $ LayoutTransform.ScaleX�$	 1.0X�$ 0:0:.5D�W� $ LayoutTransform.ScaleY�$	 1.0X�$ 0:0:.5D�2  ,DevExpress.Mvvm.UI.Interactivity.Interaction&   	Behaviors& '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]'   	EventName$' Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase(   Command( �� *FormLoadedCommand+.���  $ 	 DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.BaseColumn$	  DevExpress.Xpf.Grid.ColumnBase��%�   '        �� "  .��� ( �@  HorizontalHeaderContentAlignment$ Center=���  �@	 ReadOnly . ��  �@ HeaderTemplate �� .R��� $��DemiBold"�$��30��$��Center=�$��Center=�$ Center=�$
��Wrap=����� ) � RowDefinitions) �� $
��auto ��� $
��auto ��� $��10 ��� $��* �.+�]� .+��� #��[ $��1������ *SevkiyatEmirleri+$ TwoWay=�$ PropertyChanged=�* �� SelectedItem* �� *SeciliSevkiyatEmri+$ TwoWay=�$ PropertyChanged=�& 2
   ,Pandap.UI.Behaviors.ScrollIntoViewForListBox
  + � LayoutTransform+ �� , �� ScaleX$, 1Q�- �� ScaleY$- 1Q�-   firma_scale������ .R��� $	��Top=�$ 
Horizontal=����� .R��� $	��Top=�$	��200��.+�]� .+��� 
���  ��$��DemiBold"�$��20(����� *Plaka+�� $��Right=�$��DemiBold"�$��20(����� *PaletNet_TKg+$ 	{0:N0} Kg��� .+�� $��pbStatus��$	��0,4q�. � Maximum$	. 100Q�$��Stretch=�$��20��/ � Value/ �� *SevkiyatYuklenmeOran+$ OneWay=��� 
�������$��18(�$
��Bold"�$��Center=�$��Center=����� *SevkiyatYuklemeMetin+$ OneWay=��� $��20��$
��Bold"����� *	Nakliyeci+�� $��20������ *AracSoforAd+�� $��10(�$��0,10,0,0q����� *SevkiyatEmriId+$ Sevkiyat No:{0}���� $	��100������ *CariIsimVeIsoList+���� " ��.���  � ��$ 32������ .R��� $��32��$��0q�$ 
Horizontal=�.+��� $��32��$��32������ *
UlkeKodIso+0 �� 	Converter#0 [  1 �� ConverterParameter$1 Binding  UlkeKodIso���� $��Center=�.+��� 2 �� Padding$2 0q����� *CariAd+�� $��8(�$2 0q����� *
UlkeIsim+�� $��Right=�$	��120��$ 10q�
������
��� � �$��3q�._��� .+��� $��Center=�$��12(�$��Günlük���� $��Center=�$��0,0,0,0q�$��12(�$��Yüklenecek���� $��Center=�$��20(�$
��Bold"�$��3.000 kg���� $��Center=�$��0,20,0,0q�$��12(�$��	Yüklenen���� $��Center=�$��20(�$
��Bold"�$	�� kg���� $��Center=�$��0,20,0,0q�$��12(�$��Aylık Yükleme���� $��Center=�$��(01-22).11.2018���� $��Center=�$��20(�$
��Bold"�$	�� kg���� $��Stretch=�
����+��$��2��$��2��� $��Stretch=�$��3��3 � ColumnDefinitions3 �� �� .+�� -   $��Popup0��$	��300��$	��200��4 � AllowsTransparency4 . $��Center=�$
��Fade=�5 � PlacementTarget5 �� $	 YuklemeView������ *DataContext.PaletBulunamadi+$ TwoWay=�$ PropertyChanged=�$	 YuklemeView��+ �� $, 2Q�$- 2Q�.���� #��[ 
���  ��$ 10q�._��� .+��� $	��200��$	��120��$)��#/Pandap;component/Content/palet.png��$
��Fill=��� 
�������$��30(�$��Center=�$��Center=�$��Palet Bulunamadı��� -   $��Popup1��$��Center=�$	��300��$	��210��4 .  $��Scroll=�5 �� $	 YuklemeView��6 � DataContext6 �� *DataContext.SeciliKalanPalet+$	 YuklemeView��$ TwoWay=����� *DataContext.PaletBulundu+$ TwoWay=�$ PropertyChanged=�$	 YuklemeView��+ �� $, 2Q�$- 2Q�.���� 
�������$ 10q�
������$��2q�._��� .+��� $	��200��$	��110��$)��#/Pandap;component/Content/palet.png��$	 0.8Q�$
��Fill=��� $��Center=�$
��Bold"�
���  ��$��14(����� *PaletNet_Kg+$ 	{0:n0} kg���� $��Center=�$ 
Horizontal=�.+�  $��Center=�$��20��$��20��$ Barcode=��� $��Center=�$��14(�$
��Bold"�
���   ����� *	PaletId+�� $ 
Horizontal=�$��Center=�.+��� $
��Bold"�$��12(����� *
Kalinlik+�� $��12(�$	�� x ���� $
��Bold"�$��Center=�$��12(����� *En+�� $
��Bold"�$��Center=�$��Center=�$��10(����� *MasuraSayisi+$  ms: {0:n0}���� $��2��$��50��7 K� ZIndex$	7 100��8 J� Effect8 3  -System.Windows.Media.Effects.DropShadowEffect  9   ShadowDepth$9 2Q�._��� $ 
Horizontal=�$��Center=�.+�%  DevExpress.Xpf.Editors.TextEdit  $��20,0,0,0q�$��22(�$
��Bold"�%  DevExpress.Xpf.Editors.BaseEdit:   NullText$: 	Barkod No��$	��190��;   	EditValue; �� *
AramaMetin+$ TwoWay=�$ PropertyChanged=�< �� Delay$
< 2000��  $��	-50,0,0,0q�$��Center=�$��32��$��52��$ Barcode=�#  MaterialDesignThemes.Wpf.Card  $��0��$��1��$��5q�$��Stretch=�$��Stretch=�.���� #��[ * �� *DataContext.SeciliKalanPalet+$ TwoWay=�$	 YuklemeView������ *" SeciliSevkiyatEmri.KalanPaletler+���� = �� ImageSource$*= $/Pandap;component/Content/Panda1.png��> �� Opacity$	> 0.1Q�? j� Stretch$? UniformToFill=�& 2   ,Pandap.UI.ControlHelpers.KeepSelectionInView  ���� .R��   $��1��$��1��$��5q�$��Stretch=�$��Stretch=�.���� #��[ ���� *%#SeciliSevkiyatEmri.YuklenenPaletler+���� $.= (/Pandap;component/Content/container1.jpg��$	> 0.4Q�$? UniformToFill=����� .R�� 