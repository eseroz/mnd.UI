   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Eclr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip7Pandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip  Q-clr-namespace:Pandap.UI.GyModules.MesajModulePandap.UI.GyModules.MesajModule  N    HPandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip.ProformaEditView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     WlocalEclr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  ?mesajModule-clr-namespace:Pandap.UI.GyModules.MesajModule    � WindowStartupLocation$  CenterScreen=�   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Title$ ProformaEditView��$��601.4��$��773.2��2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService  '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+.���  � RowDefinitions �� $
��auto ��� $
��auto � � 	Resources 	 �� 
TargetType
 �� Property �� Value��%/   '��       �� "	 ��.��� 
 � ��$
 Bold"�.+�c�  +� IsFocusScope .  $��0��$��50��}�.    MaterialDesignToolBar#���  .@���   MaterialDesignFlatButton#���  ���� *KaydetCommand+.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� #���  ���� *TeklifFormGosterCommand+.���� $ 
Horizontal=�.+�  $��Center=�$��Right=�$ Cancel=��� $��8 0 0 0q�$��Center=�$��İptal��#  MaterialDesignThemes.Wpf.Card  $��1��$��4q�$��4,4,4,10q�.���  ��%�   '��       �� "	 ��.��� 
 � ��$	 125���� 
 � ��$
 Bold"��� 
 � ��$ 0,0,10,0q��� 
 �@��TextAlignment$ Right=�.+��� .@��� $��Proforma��7	   1Pandap.UI.GyModules.MesajModule.VisibilityHelpler 	  	IsVisible �� *SolSutunEnabled+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�.���� $��5q�.+�]� $��5q�.+��� $��Tarih :��$��Center=�(
  "System.Windows.Controls.DatePicker
  $��10,0,0,0q�$
��Left=�$	��100�� 
  Text �� *KulceProforma.ProformaTarih+]� $��5q�.+��� $��Prim BF ($) :��$��Center=��� 
�������$
��Left=�$	��200������ *KulceProforma.PrimTon_BF+$ TwoWay=�$ PropertyChanged=� �� StringFormat$ {0:n2}���� $��Metal Bulletin :��$��Center=��� $
��Left=�$	��200������ *KulceProforma.MetalBulletin+$ TwoWay=�$ PropertyChanged=�$ {0:n2}��]� $��5q�.+��� $��Miktar Ton :��$��Center=��� $
��Bold"�
���  ��$
��Left=�$	��200��
����������� *KulceProforma.MiktarTon+$ TwoWay=�$ PropertyChanged=��� 
�������.+�]� $��5q�.+��� $��Lme BF ($) :��$��Center=��� $
��Left=�$	��200��
����������� *KulceProforma.LmeTon_BF+$ TwoWay=�$ PropertyChanged=�$ {0:n2}��]� $��5q�.+��� $��Kontango($) :��$��Center=��� $
��Left=�$	��200��
����������� *KulceProforma.Kontango+$ TwoWay=�$ PropertyChanged=�$ {0:n0}��]� $��5q�.+��� $��Döviz Tip  :��$��Center=��� $	��200��$
��Left=�
������� �� SelectedValuePath$	 Tag�� �� SelectedValue �� *KulceProforma.DovizTip+$ TwoWay=�$ PropertyChanged=�.@��� $	��USD��
 � Tag$	 USD���� $	��EUR��$	 EUR���� $	��GBP��$	 GBP��]� $��5q�.+��� $��Parite (FX) :��$��Center=�%  DevExpress.Xpf.Editors.TextEdit  $
��Left=�$	��200��
�������)  #DevExpress.Xpf.Editors.TextEditBase   Text �� *KulceProforma.Parite+$ TwoWay=�$ PropertyChanged=��� $��Parite Tarih :��$��Center=�
  $	��200��
�������$��0,0,0,0q�$
��Left=� �� *KulceProforma.PariteTarih+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Center=����� *TextFormatDoviz+$ Lme BF ({0}) :���� $
��Left=�$	��200��
������� � ToolTip$, &(LME Birim Fiyat + Kontango) / Parite ������ *KulceProforma.LmeDovizTipBF+$ OneWay=�$ PropertyChanged=�$ {0:n2}���� $��Center=����� *TextFormatDoviz+$ Prim BF ({0}) :���� $
��Left=�$	��200��
����������� *KulceProforma.PrimDovizTipBF+$ OneWay=�$ PropertyChanged=�$ {0:n2}��]� $��5,20,0,0q�.+��� $��Center=����� *TextFormatDoviz+$ Kulce BF ({0}) :����  �� 
IsReadOnly . 
�������$
��Left=�$	��200������ *KulceProforma.KulceDovizTipBF+$ OneWay=�$ PropertyChanged=�$ {0:n2}��]� $��5q�.+��� $��Center=����� *TextFormatDoviz+$ Yaklaşık Tutar ({0}) :����  . 
�������$
��Left=�$	��200������ *+)KulceProforma.ProformaDovizTipToplamTutar+$ OneWay=�$ PropertyChanged=�$ {0:n2}���� $��Fiili Durum�� �� *SagSutunEnabled+$ PropertyChanged=�$ TwoWay=�.���  � ColumnDefinitions �� �� .+��� $��0��$��5q�{��� *SagSutunEnabled+.+�]� $��5q�.+��� $��FiiliGelenTarih :��$��Center=�
  $��10,0,0,0q�$
��Left=�$	��100�� �� *KulceProforma.FiiliGelenTarih+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��NakliyeFirmaAd :��$��Center=��� $
��Left=�$	��200������ *KulceProforma.NakliyeFirmaAd+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Nakliye Fatura No :��$��Center=��� $
��Left=�$	��200��
����������� *KulceProforma.NakliyeFaturaNo+$ TwoWay=�$ PropertyChanged=��� $��1��$��5q�{��� *SagSutunEnabled+.+�]� $��5q�.+��� $��Lme (Knt dahil) BF :��$��Center=��� $
��Left=�$	��200��
����������� *KulceProforma.FiilLmeTon_BF+$ TwoWay=�$ PropertyChanged=�$ {0:n2}��]� $��5q�.+��� $��	Prim BF :��$��Center=��� $
��Left=�$	��200��
����������� *KulceProforma.FiilPrimTon_BF+$ TwoWay=�$ PropertyChanged=�$ {0:n2}��]� $��5q�.+��� $��
Kulce BF :��$��Center=���  . 
�������$
��Left=�$	��200������ *KulceProforma.FiilKulceTon_BF+$ OneWay=�$ PropertyChanged=�$ {0:n2}��]� $��5q�.+��� $��FiiliGelenMiktarTon :��$��Center=��� $
��Bold"�
���  ��$
��Left=�$	��200������ *#!KulceProforma.FiiliGelenMiktarTon+$ TwoWay=�$ {0:n2}��]� $��5q�.+��� $��
Proforma :��$��Center=���  . 
�������$
��Left=�$	��200������ *" KulceProforma.FiilProformaTutari+$ OneWay=�$ PropertyChanged=�$ {0:n2}���� $��1��$��5q�{��� *SagSutunEnabled+.+�]� $��5q�.+��� $��Fark Miktar :��$��Center=��� $
��Left=�$	��200��
����������� *KulceProforma.FarkMiktar+$ OneWay=�$ {0:n0}��]� $��5q�.+��� $��Fark Tutar :��$��Center=��� $
��Left=�$	��200��
����������� *KulceProforma.FarkTutar+$ OneWay=�$ {0:n0}��