   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni%Pandap.UI.Modules.SatinAlmaModuleYeni  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  C&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  D    >Pandap.UI.Modules.SatinAlmaModuleYeni.TalepViews.TalepEditView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:Pandap.UI.Modules.SatinAlmaModuleYeni     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  3
converters"clr-namespace:Pandap.UI.Converters  ]	 YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 	  h
 dDevExpress.Xpf.Spreadsheet.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aEdxsps9http://schemas.devexpress.com/winfx/2008/xaml/spreadsheet 
 ;controlHelpers&clr-namespace:Pandap.UI.ControlHelpers 
 -   $��talepEditView��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService  '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+  $ Unloaded�� �� *FormUnLoadedCommand+  $ Closing��   PassEventArgsToCommand$
 TrueX� �� *ClosingCommand+-  'DevExpress.Xpf.Core.DXMessageBoxService  &   DevExpress.Mvvm.UI.WindowService     WindowShowMode$ Dialog=�   WindowStyle ��  �� 
TargetType"	  DevExpress.Xpf.Core.DXWindow" 	 .��� 	 �� Property	 � ��
 �� Value
 ��  �� ConverterParameter$
 0,80�� �� 	Converter )
   #Pandap.UI.Converters.RatioConverter
   �� Source# Z���� 	 � ��
 �� $	 0,9�� 
  # Z��.���  � 	Resources ��%/   '��       �� " ��.��� 	 � ��$

 Bold"� � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+��� -    ��Name$ TempGorusmeKisi��$��0��$��4�� K� ZIndex$	 100��$y�	Collapsed=�0   *Pandap.UI.ControlHelpers.VisibilityHelpler   	IsVisible �� *IsOpenKisiEkleForm+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�����  �� Color$ Black�� �� Opacity$ .8Q�._��� 
�������$��Center=�$��Center=�.+��� $	��200��$��20q�.+��� -    ��Name$
 txt1��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ad Soyad������ *TempCariGorusmeKisi.AdSoyad+$ TwoWay=�$ PropertyChanged=��� $ Email������ *TempCariGorusmeKisi.Email+$ TwoWay=�$ PropertyChanged=��� $	 Tel������ *TempCariGorusmeKisi.Tel+$ TwoWay=�$ PropertyChanged=��� $��28��$ Ünvan�� �� DisplayMemberPath$ UnvanTr�� �� SelectedValue$ UnvanTr��$��Stretch=� �� Text �� *TempCariGorusmeKisi.Unvan+$ TwoWay=�$ PropertyChanged=����� *
Unvanlar+�� $��Right=� �� Orientation$ 
Horizontal=�.+��� $��5q�  MaterialDesignFlatButton#���  $��İptal������ *KisiEkleCancelCommand+�� $��5q�$	��100��$
��Ekle������ *KisiEkleOkCommand+c�  +� IsFocusScope .  $��0��$��50��}�.    MaterialDesignToolBar#���  .@��� #���  ���� *KaydetCommand+.���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� #���  ���� *KapatCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Cancel=��� $��8 0 0 0q�$��Center=�$��İptal��#  MaterialDesignThemes.Wpf.Card  $��1��$��4q�$��4q�.���  ��%�   '��       �� " ��.��� 	 � ��$	
 125���� 	 � ��$

 Bold"��� 	 � ��$
 0,0,10,0q��� 	 �@��TextAlignment$
 Right=� � ColumnDefinitions �� $��* ��� $��* �.+��� $��0��$��5q�.+�]� $��5q�.+��� $��
Talep No :��$��Center=��� 
�������  �� 
IsReadOnly  . $	��100��$
��Left=����� *SeciliTalep.TalepId+]� $��5q�.+��� $��Right=�$��Talep Tarih��$��Center=�(  "System.Windows.Controls.DatePicker  
�������$
��Left=�$	��100��!   Text! �� *SeciliTalep.TalepTarihi+]� $��5q�.+��� $��Right=�$	��Top=�$��İş Merkezi���� .+��� 
�������$��Stretch=�$	��240������ *SeciliTalep.IsMerkeziKod+�� $��Stretch=�$	��240������ *SeciliTalep.IsMerkeziAd+�� $	��Top=�$��Right=�$��40������ *IsMerkeziSecCommand+.��...]� $��5q�.+��� $��Talep Stok Grup :��$��Center=��� 
�������$ 
StokGrupAd��" �� SelectedValuePath$" StokGrupKod������ *StokGruplari+ �� *SeciliTalep.StokGrupKod+$ TwoWay=�$ PropertyChanged=�]� $��5q�]� $��5q�]� $��5q�.+��� $��Talep Eden :��$��Center=��� 
�������$ AdSoyad��$" 
TcKimlikNo������ *TalepPersonelListe+ �� *SeciliTalep.TalepEdenTc+]� $��5q�.+��� $��Talep Not :��$��Center=��� 
����������� *SeciliTalep.TalepNot+�� $��1��  $��4q�$��4q�$��2��$��Stretch=�.��]� .+��� $ 
Horizontal=�$
��Left=�$	��Top=�$��0��.+��� $��4q�#���  ���� *YeniKalemCommand+.���� $ 
Horizontal=�.+�  $��Center=�$
 Plus=��� $��8 0 8 0q�$��Center=�$��
Kalem Ekle��%  DevExpress.Xpf.Grid.GridControl  $	��500��)  #DevExpress.Xpf.Grid.DataControlBase#   ItemsSource# �� *SeciliTalep.TalepKalemler+ *  $DevExpress.Xpf.Grid.GridColumnHeader��%�   '        �� "  .��� 	 � ��

 �   ��� 	 � ��

 ������� 	 � ��$
 40���� 	 � ��$

 Bold"��� 	 �@ TextFormattingMode$
 Ideal=�$   View$ #  DevExpress.Xpf.Grid.TableView  %   	AutoWidth% . &   RowMinHeight$& 35Q�&   DevExpress.Xpf.Grid.GridViewBase'   ShowGroupPanel' .  &   DevExpress.Xpf.Grid.DataViewBase(   ColumnHeaderTemplate( �� .R��� ) �� TextAlignment$) Center=�$��Stretch=�$��DemiBold"�$��30��$
��Wrap=�$��Right=����� *   Columns.* $  DevExpress.Xpf.Grid.GridColumn  $  DevExpress.Xpf.Grid.BaseColumn+   MaxWidth$+ 60Q�$  DevExpress.Xpf.Grid.ColumnBase,   	FieldName$, SiraNo��  $, IstenilenTarih��  $, StokKod��  $, StokAd��  -   Header)  #DevExpress.Xpf.Core.ObjectConverter$- Talep Zamanı Depo Miktar $, TalepZamaniDepoMiktar��  $, Birim��  $, Miktar��  $- Tercih Marka $, TercihMarkaModel��  .   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$	. 200 $, Aciklama��  /   CellTemplate/ �� .R��� #���  ���� *DataContext.KalemSilCommand+0 �� RelativeSource0 �� 1 �� Mode$1 FindAncestor=�2 �� AncestorType$2 UserControlL����� *RowData.Row+.���� $ 
Horizontal=�.+�  $��Center=�$ Delete=��� $��8 0 0 0q�$��Center=�$	��Sil��    
MesajKolon#��[ $+ 30Q�3   MinWidth$3 30��$, MesajSayisi��$- Mesaj 