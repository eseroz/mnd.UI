   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:Pandap.UI.Modules._SatisModule.MusteriAramalar.Pandap.UI.Modules._SatisModule.MusteriAramalar  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  F    @Pandap.UI.Modules._SatisModule.MusteriAramalar.PTD_AramaEditView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Nlocal<clr-namespace:Pandap.UI.Modules._SatisModule.MusteriAramalar     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  3
converters"clr-namespace:Pandap.UI.Converters  ]	 YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 	     MaterialDesignBody#���   $��Regular"�$��14(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService  '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+  $ Unloaded�� �� *FormUnLoadedCommand+  $ Closing��   PassEventArgsToCommand$
 TrueX� �� *ClosingCommand+-  'DevExpress.Xpf.Core.DXMessageBoxService  &   DevExpress.Mvvm.UI.WindowService     WindowShowMode$ Dialog=�   WindowStyle ��  �� 
TargetType"	  DevExpress.Xpf.Core.DXWindow" 	 .��� 	 �� Property	 � ��
 �� Value
 ��  �� Source# Z�� �� 	Converter )
   #Pandap.UI.Converters.RatioConverter
   �� ConverterParameter$
 0,80���� 	 � ��
 �� # Z�� 
  $	 0,9��.��� $	��Top=� � RowDefinitions �� $��50 ��� $��* � � 	Resources ��%/   '��       �� " ��.��� 	 � ��$

 Bold"�.+�c�  +� IsFocusScope .  $��0��$��50��}�.    MaterialDesignToolBar#���  .@���   MaterialDesignFlatButton#���  ���� *KaydetCommand+.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� #���  ���� *IptalCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Cancel=��� $��8 0 0 0q�$��Center=�$��İptal��#  MaterialDesignThemes.Wpf.Card  $	��Top=�$��1��$��4q�$��4q�.���  ��%�   '��       �� " ��.��� 	 � ��$	
 125���� 	 � ��$

 Bold"��� 	 � ��$
 0,0,10,0q��� 	 �@��TextAlignment$
 Right=�.+��� $	��Top=�$��0��$��5q�.+�]� $��5q�]� $��5q�]� $��0,5,0,20q�.+��� $��Right=�$	��Id:��$��Center=��� $��Center=� �� TextAlignment$ Center=�$
��Left=�
�������$��50��$��25������ *EditModel.Id+]� $��0,5,0,8q�.+��� $��Right=�$��Arama Tarihi:��$��Center=�%  DevExpress.Xpf.Editors.DateEdit  %  DevExpress.Xpf.Editors.BaseEdit   AllowNullInput . $
��Left=�$	��200��   	EditValue �� *EditModel.Tarih+ �� Mode$ TwoWay=�]� $	��0,8q�.+��� $��Müşteri Ünvan:��$	��Top=�%  DevExpress.Xpf.Editors.TextEdit  )  #DevExpress.Xpf.Editors.TextEditBase"   VerticalScrollBarVisibility$
 Auto=�$	��Top=�   TextWrapping$
 Wrap=�$��14(� �� *EditModel.MusteriUnvan+]� $	��0,8q�.+��� $��Müşteri Grubu :��$��Center=�)  #DevExpress.Xpf.Editors.ComboBoxEdit  $	��200��$
��Left=�+  %DevExpress.Xpf.Editors.LookUpEditBase   ValueMember$ MusteriGrubuAdı��   DisplayMember$ MusteriGrubuAdı��   ItemsSource �� *MusteriGrupListesi+ �� *EditModel.MusteriGrubuAdı+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�]� $	��0,8q�.+��� $��Ülke:��$��Center=�  $	��200��$
��Left=�$ UlkeAdi��$ UlkeAdi�� �� *EditModel.UlkeAdi+$ TwoWay=�$ PropertyChanged=� �� *	Ulkeler+]� $	��0,8q�.+��� $��Konu Detay:��$	��Top=�  $
 Auto=�$	��150��   AcceptsReturn . $	��Top=�$
 Wrap=�$��14(� �� *EditModel.KonuDetay+