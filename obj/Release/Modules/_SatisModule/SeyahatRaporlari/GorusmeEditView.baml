   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:Pandap.UI.Modules._SatisModule.SeyahatRaporlari/Pandap.UI.Modules._SatisModule.SeyahatRaporlari  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  C&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  E    ?Pandap.UI.Modules._SatisModule.SeyahatRaporlari.GorusmeEditView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Olocal=clr-namespace:Pandap.UI.Modules._SatisModule.SeyahatRaporlari     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  3
converters"clr-namespace:Pandap.UI.Converters  ]	 YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 	  =controlHelpers&clr-namespace:Pandap.UI.ControlHelpers 	     MaterialDesignBody#���   $��Regular"�$��14(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
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

 Bold"�.+��� -   $��TempGorusmeKisi��$��0��$��4�� K� ZIndex$	 100��$y�	Collapsed=�0   *Pandap.UI.ControlHelpers.VisibilityHelpler   	IsVisible �� *IsOpenKisiEkleForm+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�����  �� Color$ Black�� �� Opacity$ .8Q�._��� $	��Top=�$��Center=�.+��� $	��200��$��20q�.+��� -   $
��txt1��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ad Soyad������ *TempCariGorusmeKisi.AdSoyad+$ TwoWay=�$ PropertyChanged=��� $ Email������ *TempCariGorusmeKisi.Email+$ TwoWay=�$ PropertyChanged=��� $	 Tel������ *TempCariGorusmeKisi.Tel+$ TwoWay=�$ PropertyChanged=��� $��28��$ Ünvan�� �� DisplayMemberPath$ UnvanTr�� �� SelectedValue$ UnvanTr��$��Stretch=� �� Text �� *TempCariGorusmeKisi.Unvan+$ TwoWay=�$ PropertyChanged=����� *
Unvanlar+�� $��Right=� �� Orientation$ 
Horizontal=�.+��� $��5q�  MaterialDesignFlatButton#���  $��İptal������ *KisiEkleCancelCommand+�� $��5q�$	��100��$
��Ekle������ *KisiEkleOkCommand+c�  +� IsFocusScope .  $��0��$��50��}�.    MaterialDesignToolBar#���  .@��� #���  ���� *KaydetCommand+.���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� $��0q�#���  ���� *IptalCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Cancel=��� $��8 0 0 0q�$��Center=�$��İptal��#  MaterialDesignThemes.Wpf.Card  $	��Top=�$��1��$��4q�$��4q�.���  ��%�   '��       �� " ��.��� 	 � ��$	
 125���� 	 � ��$

 Bold"��� 	 � ��$
 0,0,10,0q��� 	 �@��TextAlignment$
 Right=�.+��� $	��Top=�$��0��$��5q�.+�]� $��0,5,0,20q�.+��� $��Right=�$��Görüşme Tarihi:��$��Center=�%  DevExpress.Xpf.Editors.DateEdit  %  DevExpress.Xpf.Editors.BaseEdit   AllowNullInput . $
��Left=�$	��200��   	EditValue �� *EditModel.Tarih+$ TwoWay=�]� .+��� $��Right=�$��Şehir:��$��Center=��� $	��200��$
��Left=����� *EditModel.Sehir+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Müşteri Ünvan:��$��Center=��� $��Stretch=����� *EditModel.MusteriUnvan+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Görüşülen Kişi :��$��Center=��� $	��200������ * EditModel.GorusulenKisiAdSoyad+�� $
��Left=�$��	Görevi :��$��Center=��� $
��Left=�$	��200������ *EditModel.GorusulenKisiGorev+]� $��5q�.+��� $��Tel :��$��Center=��� $	��200������ *EditModel.GorusulenKisiTel+�� $��Mail :��$��Center=��� $
��Left=�$	��200������ *EditModel.GorusulenKisiEmail+]� $
��5,20q�.+��� $��Konu Detay:��$	��Top=�%  DevExpress.Xpf.Editors.TextEdit  )  #DevExpress.Xpf.Editors.TextEditBase"   VerticalScrollBarVisibility$
 Auto=�$	��150��    AcceptsReturn  . $	��Top=�!   TextWrapping$
! Wrap=�$��14(� �� *EditModel.KonuDetay+]� $��5q�.+��� $��Alınacak Aksiyon:��$	��Top=�  $
 Auto=�$��80��  . $	��Top=�$
! Wrap=�$��14(� �� *EditModel.AlinacakAksiyon+