   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Pandap.UI.Modules.TeklifModulePandap.UI.Modules.TeklifModule  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  C&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  /    )Pandap.UI.Modules.TeklifModule.TeklifView   a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a<dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:Pandap.UI.Modules.TeklifModule     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  3
converters"clr-namespace:Pandap.UI.Converters  ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   U	 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes 	 ;controlHelpers&clr-namespace:Pandap.UI.ControlHelpers 	 $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�   MaterialDesignBody#���     MaterialDesignPaper#���    MaterialDesignFont#���  2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService  '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+  $ Unloaded�� �� *FormUnLoadedCommand+  $ Closing��   PassEventArgsToCommand$
 TrueX� �� *ClosingCommand+-  'DevExpress.Xpf.Core.DXMessageBoxService  &   DevExpress.Mvvm.UI.WindowService     WindowShowMode$ Dialog=�   WindowStyle ��  �� 
TargetType"	  DevExpress.Xpf.Core.DXWindow" 	 .��� 	 �� Property	 � ��
 �� Value
 ��  �� Source# Z�� �� 	Converter )
   #Pandap.UI.Converters.RatioConverter
   �� ConverterParameter$
 0,80���� 	 � ��
 �� # Z�� 
  $	 0,9�� � 	Resources ��%    .���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* � ��%/   '��       �� " ��.��� 	 � ��$

 Bold"�.+��� -   $��TempGorusmeKisi��$��0��$��4�� K� ZIndex$	 100��$y�	Collapsed=�0   *Pandap.UI.ControlHelpers.VisibilityHelpler   	IsVisible �� *IsOpenKisiEkleForm+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�����  �� Color$ Black�� �� Opacity$ .8Q�._��� 
�������$��Center=�$��Center=�.+��� $	��200��$��20q�.+��� -   $
��txt1��) 	 #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ad Soyad������ *TempCariGorusmeKisi.AdSoyad+$ TwoWay=�$ PropertyChanged=��� $ Email������ *TempCariGorusmeKisi.Email+$ TwoWay=�$ PropertyChanged=��� $	 Tel������ *TempCariGorusmeKisi.Tel+$ TwoWay=�$ PropertyChanged=��� $��28��$ Ünvan�� �� DisplayMemberPath$ UnvanTr�� �� SelectedValue$ UnvanTr��$��Stretch=� �� Text �� *TempCariGorusmeKisi.Unvan+$ TwoWay=�$ PropertyChanged=����� *
Unvanlar+�� $��Right=� �� Orientation$ 
Horizontal=�.+��� $��5q�  MaterialDesignFlatButton#���  $��İptal������ *KisiEkleCancelCommand+�� $��5q�$	��100��$
��Ekle������ *KisiEkleOkCommand+c�  +� IsFocusScope .  $��0��$��50��}�.    MaterialDesignToolBar#���  .@��� #���  ���� *YeniTeklifCommand+.���� $ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ FileDocument=��� $��8 0 0 0q�$��Center=�$
��Yeni���� #���  ���� *KaydetCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Kaydet���� $��0q�#���  ���� *EkranTazeleCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Refresh=��� $��8 0 0 0q�$��Center=�$��Ekran Tazele���� #���  ���� *SipariseDonusturCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Origin=��� $��8 0 0 0q�$��Center=�$��Siparişe Dönüştür���� #���  ���� *TeklifFormuViewCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ FileDocumentBox=��� $��8 0 0 0q�$��Center=�$��Teklif Formu���� #���  ���� *ProformaViewCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ FileDocumentBox=��� $��8 0 0 0q�$��Center=�$��Proforma���� #���  $y�Hidden=�.���� $ 
Horizontal=�.+�' 	 !MaterialDesignThemes.Wpf.PopupBox     PopupContent. �� .+��� $��4q�$��Proforma Invoice������ *ProformaViewCommand+�� $��4q�$��Packing List������ *PackingListCommand+�� $��4q�$��Commercial Invoice ������ *PackingListCommand+�� $��4q�$��Proforma Fatura������ *&$View.DataContext.ProformaViewCommand+�� $��4q�$��Çeki Listesi������ *%#View.DataContext.PackingListCommand+�� $��4q�$��Ticari Fatura ������ *%#View.DataContext.PackingListCommand+# 	 MaterialDesignThemes.Wpf.Card  $��1��$��4q�$��4q�.���  � ColumnDefinitions �� $��* ��� $��* � ��%�   '��       �� " ��.��� 	 � ��$	
 125���� 	 � ��$

 Bold"��� 	 � ��$
 0,0,10,0q��� 	 �@��TextAlignment$
 Right=�.+��� $��0��$��5q�.+�]� $��5q�.+��� $��Teklif Sıra No :��$��Center=��� 
������� �� 
IsReadOnly . $	��100��$
��Left=����� *SeciliTeklif.TeklifSiraKod+]� $��5q�.+��� $��Right=�$��Teklif Tarih /Tem. N:��$��Center=�(  "System.Windows.Controls.DatePicker  
�������$	��100��    Text  �� *SeciliTeklif.TeklifTarih+�� $	��200��$
��Left=�
�������$��15,0,0,0q����� *$"SeciliTeklif.PlasiyerTeklifSiraKod+]� $��5q�.+��� $��Son Geçerlilik Tarihi :��$��Center=�%  DevExpress.Xpf.Editors.DateEdit  $	��140��$��28��$
��Left=�
�������%  DevExpress.Xpf.Editors.TextEdit!   Mask$! dd/MM/yyyy HH:mm��"   MaskUseAsDisplayFormat" . #   MaskCulture$# tr-TR��%  DevExpress.Xpf.Editors.BaseEdit$   	EditValue$ �� *" SeciliTeklif.SonGecerlilikTarihi+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Cari Kod / Ad :��$��Center=��� $ 
Horizontal=�.+��� $	��100��$��30��
�������$��12(����� *SeciliTeklif.CariKod+$ TwoWay=�$ PropertyChanged=��� $��0q�$��32��#��[ ���� *MusteriSecCommand+.��  $ Magnify=�$��24��$��24���� ���	#FFA88CEA
����������� *SeciliTeklif.CariAd+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Potansiyel Müşteri :��$��Center=��� 
����������� *SeciliTeklif.PotansiyelCariAd+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $ ��Döviz Tip / Ölçü Brm :��$��Center=��� $	��100��$
��Left=����� *SeciliTeklif.CariDovizTipKod+$ PropertyChanged=��� $��15,0,0,0q�$	��100��$
��Left=�
�������$ Aciklama��% �� SelectedValuePath$% BirimTipKod������ *OlcuBirimleri+ �� *SeciliTeklif.MiktarOlcuBirim+]� $��5q�.+��� $	��Top=�$��İletişim Ad / Mail :���� .+�)  #DevExpress.Xpf.Editors.ComboBoxEdit  -   $
��cbo1��$��28��+  %DevExpress.Xpf.Editors.LookUpEditBase&   AutoComplete& . '   ImmediatePopup$
' TrueX�(   DisplayMember$( AdSoyad��$
��Left=�$	��270��)   ValueMember$) Id��*   SelectedItem* �� *SeciliCariEmail+$ TwoWay=�$ PropertyChanged=�$ �� *SeciliTeklif.IletisimKisiId+$ TwoWay=�$ PropertyChanged=�+   ItemsSource+ �� *CariGorusmeKisiListe+$ TwoWay=�$ PropertyChanged=�*  $DevExpress.Xpf.Editors.PopupBaseEdit,   PopupBottomAreaTemplate, �� .R�� .+��� 
�������$��1q�$��5,10,5,5q�$��12(�- �� 	IsDefault- . $��SemiBold"�$��0q�$��Stretch=�$��20��#���  ���� *KisiEkleFormAcCommand+.��+ Yeni Kişi Ekle�� $ 
Horizontal=�$��Stretch=�.+��� $��0,5,0,0q�
����������� *SeciliTeklif.IletisimKisiMail+$ PropertyChanged=��� $��5,5,0,0q�
����������� * SeciliTeklif.IletisimKisiUnvan+$ PropertyChanged=��� $��5,5,0,0q�
����������� *SeciliTeklif.IletisimKisiTel+$ PropertyChanged=��� $��5q�$��1��.+�]� $��5q�.+��� $��Center=�$��Taşıma Şekli/Yeri :���� $	��100��
�������$ TasimaSekliAdi_EN��$% TasimaSekliAdi_EN������ *TasimaSekilleri+ �� * SeciliTeklif.TasimaSekliAdi_EN+�� $	��320��
�������$ UlkeAdi��$% UlkeAdi������ *	Ulkeler+ �� *SeciliTeklif.GidecegiUlke+]� $��5q�.+��� $��Center=�$��Teslim Şekli/Yeri :���� $	��100��
�������$ TeslimTipKod��$% TeslimTipKod������ *TeslimTipleri+ �� *SeciliTeklif.TeslimTipKod+�� $��15,0,0,0q�
����������� *SeciliTeklif.TeslimYeri+$ TwoWay=�$ PropertyChanged=�]� $��5q�.+��� $��Teslim Not :��$��Center=��� 
����������� *SeciliTeklif.TeslimNot+]� $��5q�.+��� $��Center=�$��Ödeme Şekli / Not :���� $	��100��
�������$ Aciklama��$% OdemeTipKod������ *OdemeTipleri+ �� *SeciliTeklif.OdemeSekliKod+�� $��20,0,0,0q�$
��Left=�
����������� *SeciliTeklif.OdemeSekliDetay+]� $��5q�.+��� $��	Genel Not��$��Center=��� 
����������� *SeciliTeklif.TeklifGenelNot+$ PropertyChanged=�]� $��5q�.+��� $��Banka Hesabı��$��Center=��� 
�������$ BankaHesapListeAd��$% BankaKod������ *BankaHesaplari+ �� *SeciliTeklif.BankaHesapKod+  $��4q�$��4q�$��2��$��Stretch=�.��]� .+��� $��4q�$
��Left=�$	��Top=�$��0��#���  ���� *YeniKalemCommand+.���� $ 
Horizontal=�.+�  $��Center=�$
 Plus=��� $��8 0 8 0q�$��Center=�$��
Kalem Ekle��%  DevExpress.Xpf.Grid.GridControl  )  #DevExpress.Xpf.Grid.DataControlBase.   AllowLiveDataShaping$
. TrueX�/   SelectedItem/ �� *SeciliTeklifKalemEditModel+$ PropertyChanged=�0   ItemsSource0 �� * SeciliTeklif.TeklifKalemlerDTO+ *  $DevExpress.Xpf.Grid.GridColumnHeader$  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.BaseColumn��%�   '        '  �     �� "  .��� 	 � ��

 �   ��� 	 � ��

 ������� 	 � ��$
 40���� 	 � ��$

 Bold"��� 	 �@ TextFormattingMode$
 Ideal=��� "  .��� (	 �@  HorizontalHeaderContentAlignment$
 Center=�1   View1 #  DevExpress.Xpf.Grid.TableView  2   	AutoWidth2 . 3   ShowIndicator3 .  &   DevExpress.Xpf.Grid.GridViewBase4   ShowGroupPanel4 .  5   AllowPerPixelScrolling5 . &   DevExpress.Xpf.Grid.DataViewBase6   ColumnHeaderTemplate6 �� .R��� 7 �� TextAlignment$7 Center=�$��Stretch=�$��DemiBold"�$��30��$
��Wrap=�$��Right=����� 8   Columns.8   9   Header)  #DevExpress.Xpf.Core.ObjectConverter$9 Sıra No :   MaxWidth$	: 130Q�;   
FixedWidth; . <   Width2   ,DevExpress.Xpf.Grid.GridColumnWidthConverter$	< 130  $!  DevExpress.Xpf.Grid.ColumnBase= !  FilterPopupMode$= Excel=�> !  AutoFilterCondition$> Contains=�? !  CellTemplate? �� .R�&"   DevExpress.Xpf.Core.SimpleButton"  $	��130��  MyLinkButtonAc#��[ ���� * RowData.Row.TeklifKalemSiraKod+���� *!View.DataContext.DuzenleCommand+���� *RowData.Row+  $9 Teslim Tarihi @ !  	FieldName$@ TeslimTarihi��A !  IsSmartA .   $9 Ürün Kodu $@ UrunKod��A .   $9 Ürün Adı $@ 	UrunAdiEN��A . $	< 500    $9 Birim Fiyatı $@ SatisFiyati��A . B !  EditSettingsB 6#  0DevExpress.Xpf.Editors.Settings.TextEditSettings#  C #  Mask$C n1��D #  MaskType$D Numeric=�E #  MaskUseAsDisplayFormatE .   $9 Miktar $@ Miktar��A .   $9 Tutar $@ Tutar��A . B #  $C n1��$D Numeric=�E .   $@ GR��A . $< 60    $	@ PCS��A . $< 60    $	@ BOX��A . $< 60    A . ? �� .R��� $��60��$��30��$��4q�$	��Sil����Z� ���� *View.DataContext.SilCommand+