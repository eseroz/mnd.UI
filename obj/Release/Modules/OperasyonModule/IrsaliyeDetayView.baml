   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Pandap.UI.Modules.OperasyonModule!Pandap.UI.Modules.OperasyonModule  9    3Pandap.UI.Modules.OperasyonModule.IrsaliyeDetayView   a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   ?mvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ;local/clr-namespace:Pandap.UI.Modules.OperasyonModule  -     >�Name$  ucIrsaliyeDetay��$��Stretch=�2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService     ClosingCommand �� *FormClosingCommand+'  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   
SourceName$ SevkiyatForm��   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+.��� $��10q�
������� � RowDefinitions �� $
��auto ��� $��* � � ColumnDefinitions �� $��* ��� $��* �.+��� $��0��.+�`� $��0��.�c� }�.     MaterialDesignToolBar#���   .@���  �� 	IsDefault .   MaterialDesignFlatButton#���  ���� *KaydetKapatCommand+.���� 	 �� Orientation$	 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�
   Kind$
 ContentSave=��� $��8 0 0 0q�$��Center=�   MaterialDesignBody1TextBlock#��[ $��Kaydet����  �� IsCancel . #���  ���� *IptalCommand+.���� $	 
Horizontal=�.+�  $��Center=�$
 Close=��� $��8 0 0 0q�$��Center=�#��[ $��İptal����  . #���  ���� * FiyatlariYenidenHesaplaCommand+.���� $	 
Horizontal=�.+�  $��Center=�$
 
Calculator=��� $��8 0 0 0q�$��Center=�#��[ $ ��Fiyatları Yeniden Hesapla��#  MaterialDesignThemes.Wpf.Card  $��5q�$��1��.���� $��10q� � DataContext �� *SeciliCariIrsaliye+.+��� $��0,10,0,0q�$!��İhracat Fatura No / Tarih:���� $	 
Horizontal=�.+��� $	��Top=�$
��Left=� �� TextWrapping$
 Wrap=�$	��200����Z� ���� *IhracatFaturaNo+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�%  DevExpress.Xpf.Editors.DateEdit  $��10,0,0,0q�$	��200��$
��Left=�%	  DevExpress.Xpf.Editors.TextEdit 	  MaskType$ DateTimeAdvancingCaret=�%
  DevExpress.Xpf.Editors.BaseEdit 
  	EditValue �� *IhracatFaturaTarih+$ TwoWay=�$ PropertyChanged=� 
  StyleSettings ;  5DevExpress.Xpf.Editors.DateEditNavigatorStyleSettings  �� $��0,10,0,0q�$��Fatura Adresi :����  �� AcceptsReturn . " �� VerticalScrollBarVisibility$ Visible=�$
 Wrap=�$��83����Z� ���� *FaturaAdresi+$ TwoWay=�$ PropertyChanged=��� $��0,10,0,0q�$��İrsaliye Adresi 1 :����  . $ Visible=�$
 Wrap=�$��83����Z� ���� *IrsaliyeAdresi1+$ TwoWay=�$ PropertyChanged=��� $��0,10,0,0q�$��İrsaliye Adresi 2 :����  . $ Visible=�$
 Wrap=�$��83����Z� ���� *IrsaliyeAdresi2+$ TwoWay=�$ PropertyChanged=��� $��0,10,0,0q�$��Firma İlgili Kişi :���� $	 
Horizontal=�.+��� $	��160��$
��Left=�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ad Soyad�� % !MaterialDesignFloatingHintTextBox#��[ $
 Wrap=�$��50��$��0,0,20,0q����� *FirmaIlgiliKisiAdSoyad+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$	��160��$
��Left=�$ Firma Telefon��#��[ $
 Wrap=�$��50��$��0,0,20,0q����� *FirmaIlgiliKisiTel+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$	��180��$
��Left=�$ Eposta��#��[ $
 Wrap=�$��50��$��0,0,20,0q����� *FirmaIlgiliKisiEposta+$ TwoWay=�$ PropertyChanged=��� $��0,15,0,0q�$��Teslim Şekli :����  . $
 Wrap=���Z� ���� *TeslimSekli+$ TwoWay=�$ PropertyChanged=��� $	 
Horizontal=�.+��� $	��300��.+��� $��Odeme Şekli :����  . $
 Wrap=���Z� ���� *
OdemeSekli+$ TwoWay=�$ PropertyChanged=��� $��20,0,0,0q�.+��� $	��200��$��Finansal Garantör :����  . $
 Wrap=���Z� ���� *FinansalGarantorAd+$ TwoWay=�$ PropertyChanged=��� $��0,15,0,0q�$��Banka :��)  #DevExpress.Xpf.Editors.ComboBoxEdit  +  %DevExpress.Xpf.Editors.LookUpEditBase   AutoComplete .    DisplayMember$ BankaAd��   ValueMember$ BankaKod�� �� *OdemeBankaKod+$ TwoWay=�$ PropertyChanged=�   ItemsSource �� *DataContext.Bankalar+ �� RelativeSource ��  �� AncestorType$ UserControlL�   ItemTemplate �� .R��� $��Stretch=�
�������$��2q�._��� $��Stretch=�.+��� $
��Bold"�$��12(����� *	BankaAd+�� ���� *Sube+�� $	 
Horizontal=�.+��� ���� *	ParaCinsi+�� $	�� - ���� $	��200������ *Iban+�� $	��200������ *
SwiftKod+  $��5q�$��1��$��1��.���� $	 Vertical=�.+��� $��5q�$��12(�$��DemiBold"�$��Lme Detayları��&   System.Windows.Controls.DataGrid     CanUserAddRows .     AutoGenerateColumns .   �� SelectedItem �� *SeciliLmeInfo+$ TwoWay=����� *LmeBilgiler+    Columns  4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$! KalemKod��"   Width5  /System.Windows.Controls.DataGridLengthConverter$" SizeToCells #   
IsReadOnly# . $   CellTemplate$ �� .R��� ���� *
KalemKod+  $! LmeBirimFiyat��$" SizeToCells $ �� .R�	  $��80����Z�  �� *LmeBirimFiyat+$ TwoWay=�$ PropertyChanged=�  $! LmeNot��# . $ �� .R��� ���� *LmeDurumKod+  $	! LFX��# . $ �� .R��� ���� *LfxKod+  $" 70 $! LmeNot��$ �� .R��� ���� *LmeNot+  %   
Visibility$% Hidden=�$" 70 $
! Yıl��$ �� .R��� ���� *Yil+$ TwoWay=�  $% Hidden=�$" 70 $! Ay��$ �� .R��� ���� *Ay+$ TwoWay=�  $% Hidden=�$" 70 $! Doviz��$ �� .R��� ���� *
DovizTip+$ TwoWay=�  $% Hidden=�$! İşlem��$ �� .R��� $��Ortalamalar������ *%#DataContext.LmeOrtalamaGetirCommand+& �� ElementName$& ucIrsaliyeDetay������ ��Z� 