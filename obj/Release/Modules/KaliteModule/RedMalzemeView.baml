   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  O,clr-namespace:Pandap.UI.Modules.KaliteModulePandap.UI.Modules.KaliteModule  3    -Pandap.UI.Modules.KaliteModule.RedMalzemeView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     AcontrolHelpers&clr-namespace:Pandap.UI.ControlHelpers     9
converters"clr-namespace:Pandap.UI.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   :local,clr-namespace:Pandap.UI.Modules.KaliteModule   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006   U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes 	    MaterialDesignPaper#���     MaterialDesignFont#���  $��14(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService  '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+  $ Unloaded�� �� *FormUnLoadedCommand+  $ Closing��   PassEventArgsToCommand$
 TrueX� �� *ClosingCommand+-  'DevExpress.Xpf.Core.DXMessageBoxService  &   DevExpress.Mvvm.UI.WindowService     WindowShowMode$ Dialog=�   WindowStyle ��  �� 
TargetType"	  DevExpress.Xpf.Core.DXWindow" 	 .��� 	 �� Property	 � ��
 �� Value
 ��  �� Source# Z�� �� 	Converter )
   #Pandap.UI.Converters.RatioConverter
   �� ConverterParameter$
 0,80���� 	 � ��
 �� # Z�� 
  $	 0,9��.��� $	��Top=� � RowDefinitions �� $��50 ��� $��* � � 	Resources ��%/   '��       �� " ��.��� 	 � ��$

 Bold"�.+��� -   $��TempGorusmeKisi��$��0��$��4�� K� ZIndex$	 100��$y�	Collapsed=�0   *Pandap.UI.ControlHelpers.VisibilityHelpler   	IsVisible �� *IsOpenKisiEkleForm+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�����  �� Opacity$ .8Q� �� Color$ Black��._��� $��Center=�$	��Top=�
�������.+��� $	��200��$��20q�.+��� -   $
��txt1��) 	 #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ad Soyad������ *TempCariGorusmeKisi.AdSoyad+$ TwoWay=�$ PropertyChanged=��� $ Email������ *TempCariGorusmeKisi.Email+$ TwoWay=�$ PropertyChanged=��� $	 Tel������ *TempCariGorusmeKisi.Tel+$ TwoWay=�$ PropertyChanged=��� $��28��$��Stretch=�$ Ünvan�� �� DisplayMemberPath$ UnvanTr�� �� SelectedValue$ UnvanTr������ *
Unvanlar+ �� Text �� *TempCariGorusmeKisi.Unvan+$ TwoWay=�$ PropertyChanged=��� $��Right=� �� Orientation$ 
Horizontal=�.+��� $��5q�$��İptal��  MaterialDesignFlatButton#���  ���� *KisiEkleCancelCommand+�� $	��100��$��5q�$
��Ekle������ *KisiEkleOkCommand+c� $��0��$��50��}�.   +� IsFocusScope .    MaterialDesignToolBar#���  .@��� #���  ���� *YeniTeklifCommand+.���� $ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ FileDocument=��� $��8,0,0,0q�$��Center=�$
��Yeni���� #���  ���� *KaydetCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ ContentSave=��� $��8,0,0,0q�$��Center=�$��Kaydet��# 	 MaterialDesignThemes.Wpf.Card  $��1��$��4q�$��4q�$	��Top=�.���  ��%�   '��       �� " ��.��� 	 � ��$	
 125���� 	 � ��$

 Bold"��� 	 � ��$
 0,0,10,0q��� 	 �@��TextAlignment$
 Right=�.+��� $��0��$	��Top=�.+��� $ 
Horizontal=�.+�]� .+��� $��Center=�$��
Sıra No :���� $	��200��$
��Left=�
������� �� 
IsReadOnly . ���� *SeciliRedMalzeme.Id+]� $	��0,5q�.+��� $��Center=�$��Tarih :���� $	��200��$
��Left=�
������� . ���� *SeciliRedMalzeme.Tarih+�� $��0,10,0,0q�$ 
Horizontal=�.+�]� .+��� $��Center=�$��	Kart No :���� $	��200��
�������$��18(�$
��Bold"�
���  ������ *SeciliRedMalzeme.KartNo+]� .+��� $��Center=�$��Üretim Emri Kod :���� $	��200��
�������$��18(�$
��Bold"�
���  ������ * SeciliRedMalzeme.UretimEmriKod+]� $��5q�.+��� $��Center=�$��Müşteri :���� 
����������� *SeciliRedMalzeme.Musteri+]� $��5q�.+��� $��Center=�$��	Vardiya :���� 
����������� *SeciliRedMalzeme.Vardiya+]� $��5q�.+��� $��Center=�$��Operatör :���� 
����������� *SeciliRedMalzeme.Operator+]� $��5q�.+��� $��Center=�$��Kalite Teknisyeni :���� 
����������� *#!SeciliRedMalzeme.KaliteTeknisyeni++  %DevExpress.Xpf.Core.UniformStackPanel  $��0,30,0,5q�
�������.+�]� $��5q�.+��� $��Center=�$��
Bobin No :���� $	��100��
����������� *SeciliRedMalzeme.BobinNo+]� $��5q�.+��� $��Center=�$��Malzeme No :���� $	��100��
����������� *SeciliRedMalzeme.MalzemeNo+  
�������.+�]� $��5q�.+��� $��Center=�$
��En :���� $	��100��
����������� *SeciliRedMalzeme.En+]� $��5q�.+��� $��Center=�$��
Alaşım :���� $	��100��
����������� *SeciliRedMalzeme.Alasim+]� $��5q�.+��� $��Center=�$��Kondüsyon :���� $	��100��
����������� *SeciliRedMalzeme.Kondusyon+]� $��5,30,0,0q�.+��� $��Center=�$��
Red Yeri :���� 
����������� *SeciliRedMalzeme.RedYeri+]� $��5q�.+��� $��Center=�$��Red Nedeni :���� 
����������� *SeciliRedMalzeme.RedNedeni+]� $��5q�.+��� $��Center=�$��Red Miktarı (kg) :���� 
�������$��20(�$
��Bold"�
���  ������ *SeciliRedMalzeme.RedMiktarKg+