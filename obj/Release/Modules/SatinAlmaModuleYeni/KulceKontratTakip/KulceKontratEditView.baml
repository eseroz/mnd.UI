   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Eclr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip7Pandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip  R    LPandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip.KulceKontratEditView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     WlocalEclr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes    � WindowStartupLocation$  CenterScreen=�   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Title$ KulceKontratEdit��$��396.4��$��673.092��2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService  '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+-  'DevExpress.Xpf.Core.DXMessageBoxService  .���  � RowDefinitions �� $
��auto ��� $
��auto � � 	Resources 	 �� 
TargetType
 �� Property �� Value��%/   '��       �� "	 ��.��� 
 � ��$
 Bold"�.+�c�  +� IsFocusScope .  $��0��$��50��}�.    MaterialDesignToolBar#���  .@���   MaterialDesignFlatButton#���  ���� *KaydetCommand+.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� #���  ���� *TeklifFormGosterCommand+.���� $ 
Horizontal=�.+�  $��Center=�$��Right=�$ Cancel=��� $��8 0 0 0q�$��Center=�$��İptal��#	  MaterialDesignThemes.Wpf.Card	  $��1��$��4q�$��4q�.���  ��%�   '��       �� "	 ��.��� 
 � ��$	 125���� 
 � ��$
 Bold"��� 
 � ��$ 0,0,10,0q��� 
 �@��TextAlignment$ Right=�.+��� $��0��$��5q�.+�]� $��5q�.+��� $��Cari Kod / Ad :��$��Center=��� $ 
Horizontal=�.+��� $	��100��$��30��
�������$��12(����� *KulceKontrat.CariKod+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��0q�$��32��#��[ ���� *MusteriSecCommand+.��  $ Magnify=�$��24��$��24���� ���	#FFA88CEA
�������$��12(����� *KulceKontrat.CariIsim+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Kontrat No / Tarih :��$��Center=��� $	��100��$
��Left=����� *KulceKontrat.KontratNo+(
  "System.Windows.Controls.DatePicker
  $��10,0,0,0q�$
��Left=�$	��100�� 
  Text �� *KulceKontrat.KontratTarihi+]� $��5q�.+��� $��Vergi Durum (DUTY) :��$��Center=��� $	��200��$
��Left=� �� SelectedValuePath$	 Tag�� �� SelectedValue �� *KulceKontrat.VergiDurum+$ TwoWay=�$ PropertyChanged=�.@��� $	��DTU��
 � Tag$	 DTU���� $	��DTP��$	 DTP��]� $��5q�.+��� $��Başlama Yıl/Ay:��$��Center=��� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Yıl��$
��Left=�$��50��
����������� *KulceKontrat.PartiDonemYil+$ TwoWay=�$ PropertyChanged=��� $ Dönem Başlangıç Ay��$
��Left=�$��20,0,0,0q�$��50��
����������� *$"KulceKontrat.PartiDonemBaslangicAy+$ TwoWay=�$ PropertyChanged=��� $��Parti Dönem Sayısı :��$��20,0,0,0q�$��Center=��� $ Ay��$
��Left=�$��10,0,0,0q�$��50��
����������� *KulceKontrat.PartiDonemAy+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Prim (USD):��$��Center=��� $
��Left=�$	��200��
����������� *KulceKontrat.Prim+$ TwoWay=�$ PropertyChanged=� �� StringFormat$ {0:n0}��]� $��5q�.+��� $��Miktar Ton :��$��Center=��� $
��Left=�$	��200��
����������� *KulceKontrat.MiktarTon+$ TwoWay=� �� TargetNullValue Z� $ {0:n0}��$ PropertyChanged=�]� $��5q�]� $��5q�