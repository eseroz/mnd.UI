   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Pandap.UI.Modules.TeklifModulePandap.UI.Modules.TeklifModule  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  C&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  ;    5Pandap.UI.Modules._SatisModule.Sikayetler.SikayetView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:Pandap.UI.Modules.TeklifModule     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  3
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

 Bold"�.+��� -   $��TempGorusmeKisi��$��0��$��4�� K� ZIndex$	 100��$y�	Collapsed=�0   *Pandap.UI.ControlHelpers.VisibilityHelpler   	IsVisible �� *IsOpenKisiEkleForm+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�����  �� Color$ Black�� �� Opacity$ .8Q�._��� 
�������$	��Top=�$��Center=�.+��� $	��200��$��20q�.+��� -   $
��txt1��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ad Soyad������ *TempCariGorusmeKisi.AdSoyad+$ TwoWay=�$ PropertyChanged=��� $ Email������ *TempCariGorusmeKisi.Email+$ TwoWay=�$ PropertyChanged=��� $	 Tel������ *TempCariGorusmeKisi.Tel+$ TwoWay=�$ PropertyChanged=��� $��28��$ Ünvan�� �� DisplayMemberPath$ UnvanTr�� �� SelectedValue$ UnvanTr��$��Stretch=� �� Text �� *TempCariGorusmeKisi.Unvan+$ TwoWay=�$ PropertyChanged=����� *
Unvanlar+�� $��Right=� �� Orientation$ 
Horizontal=�.+��� $��5q�  MaterialDesignFlatButton#���  $��İptal������ *KisiEkleCancelCommand+�� $��5q�$	��100��$
��Ekle������ *KisiEkleOkCommand+c�  +� IsFocusScope .  $��0��$��50��}�.    MaterialDesignToolBar#���  .@��� #���  ���� *YeniTeklifCommand+.���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ FileDocument=��� $��8 0 0 0q�$��Center=�$
��Yeni���� #���  ���� *KaydetCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� $��0q�#���  ���� *EkranTazeleCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Refresh=��� $��8 0 0 0q�$��Center=�$��Ekran Tazele���� #���  $
��XLSX������ *ExcelExportCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ 	FileExcel=��� $��8 0 0 0q�$��Center=�$��Excele Aktar��#  MaterialDesignThemes.Wpf.Card  $	��Top=�$��1��$��4q�$��4q�.���  ��%�   '��       �� " ��.��� 	 � ��$	
 125���� 	 � ��$

 Bold"��� 	 � ��$
 0,0,10,0q��� 	 �@��TextAlignment$
 Right=�.+��� $	��Top=�$��0��$��5q�.+�]� $��5q�.+��� $��Şikayet Sıra No :��$��Center=��� 
������� �� 
IsReadOnly . $	��100��$
��Left=����� *SeciliSikayet.Id+]� $��5q�.+��� $��Geliş Yolu :��$��Center=��� 
����������� *SeciliSikayet.GelisYolu_Ref+]� $��5q�.+��� $��Right=�$��Tarih /Tem. N:��$��Center=�(  "System.Windows.Controls.DatePicker  
�������$	��100��   Text �� *SeciliSikayet.SikayetTarihi+�� $	��200��$
��Left=�$��15,0,0,0q����� *$"SeciliTeklif.PlasiyerTeklifSiraKod+]� $��5q�.+��� $��Müşteri Adı :��$��Center=�]� .+��� $��0q�$��32��#��[ ���� *MusteriSecCommand+.��  $ Magnify=�$��24��$��24���� $��Stretch=����	#FFA88CEA
����������� *SeciliSikayet.SikayetFirmaAd+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Şikayet Eden Kişi :��$��Center=��� 
����������� *SeciliSikayet.SikayetEdenKisi+]� $��5q�.+��� $��Konu Kategori :��$��Center=�)  #DevExpress.Xpf.Editors.ComboBoxEdit  $��16(�$	��Top=�+  %DevExpress.Xpf.Editors.LookUpEditBase   DisplayMember$ KategoriAdi��%  DevExpress.Xpf.Editors.BaseEdit    ValidateOnTextInput  .  !   ValidateOnEnterKeyPressed! . "   ValueMember$" KategoriAdi��#   ImmediatePopup$
# TrueX�$   ItemsSource$ �� *KonuKategorileri+%   	EditValue% �� *SeciliSikayet.KonuKategoriAdi+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Konu:��$��Center=�%  DevExpress.Xpf.Editors.TextEdit  $	��Top=�)  #DevExpress.Xpf.Editors.TextEditBase&   TextWrapping$
& Wrap=�$��14(�
�������% �� *SeciliSikayet.Konusu+]� $��5q�.+��� $��Şikayet Bölümleri :��$��Center=�  $��16(�$	��Top=�  . ! . $
# TrueX�$ �� *SikayetBolumleri+% �� *&$SeciliSikayet.SeciliSikayetBolumleri+$ TwoWay=�$ PropertyChanged=�'   StyleSettings' 7  1DevExpress.Xpf.Editors.TokenComboBoxStyleSettings  (   NewTokenPosition$	( FarX�]� $��5q�.+��� $��Şikayet Miktarı (kg) :��$��Center=��� 
����������� *SeciliSikayet.SikayetMiktari+]� $��5q�.+��� $��Panda Sipariş Kod :��$��Center=��� 
����������� *SeciliSikayet.PandaSiparisKod+]� $��5q�.+��� $��İlgili Paletler :��$��Center=�  $	��Top=�)   NullText$D) >Birden fazla ise , ile ayrılarak yazılması gerekmektedir...��$
& Wrap=�$��14(�
�������$��58��% �� *SeciliSikayet.PaletListe+]� $��5q�.+��� $��Teslim Not :��$��Center=��� 
����������� *SeciliSikayet.TeslimNot+