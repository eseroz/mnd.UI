   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  ;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  [2clr-namespace:Pandap.UI.Modules.MusteriTakipModule$Pandap.UI.Modules.MusteriTakipModule  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ;    5Pandap.UI.Modules.MusteriTakipModule.MusteriDetayView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     AcontrolHelpers&clr-namespace:Pandap.UI.ControlHelpers     9
converters"clr-namespace:Pandap.UI.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   i	 eDevExpress.Xpf.PropertyGrid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxprg:http://schemas.devexpress.com/winfx/2008/xaml/propertygrid 	 >local2clr-namespace:Pandap.UI.Modules.MusteriTakipModule 	 U
 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 
 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
 0sys&clr-namespace:System;assembly=mscorlib 
    MaterialDesignPaper#���     MaterialDesignFont#���  $��13(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=� � 	Resources 5   /Pandap.UI.Converters.ImageFromAssemblyConverter  ImageFromAssemblyCnv��%   &	         .��� $��20q� � RowDefinitions �� $
��auto ��� $��* �.+��� $��0��$��Stretch=�.+���  �� Padding$ 5q�
����  �$��1q� �� CornerRadius$ 3��._��� $��Stretch=�.+��� $��Right=� �� Orientation$ 
Horizontal=�.+��� $��0q�  MaterialDesignFlatButton#���  0   *Pandap.UI.ControlHelpers.VisibilityHelpler   	IsVisible 1  +DevExpress.Xpf.DXBinding.DXBindingExtension  *!IsEditMode+   UpdateSourceTrigger$ PropertyChanged=����� *MusteriDataDuzenleCommand+.���� $ 
Horizontal=�.+�' 
 !MaterialDesignThemes.Wpf.PackIcon  $��25��$��25��$��Center=�	   Kind$	 Pencil=��� $��8,0,0,0q�$��Center=��� $��İptal��#���   �� *
IsEditMode+
 �� UpdateSourceTrigger$
 PropertyChanged=����� *MusteriDataGuncelleCommand+�� $��	Güncelle�� �� *
IsEditMode+$
 PropertyChanged=����� *MusteriDataGuncelleCommand+�� $��Center=�$ 
Horizontal=�.+��� $��Center=�$ 
Horizontal=�.+��� $��40��$��40��$	��Top=����� *Musteri.UlkeKod+ �� 	Converter# [ �� -    ��Name$
 Cari��$��50��$��20,0,0,0q�$
��Left=�$��Bottom=�$��30(�$��Italic$�$
��Bold"�$
��Wrap=����� *Musteri.CariIsim+� .+��� $	��650��.+�]� $��5q�.+��� $	��120��$��Fatura Adresi : �� �� TextAlignment$ Right=��� $��14(�$
��Bold"�   *!IsEditMode+$ PropertyChanged=����� *Musteri.Adres+%  DevExpress.Xpf.Editors.TextEdit  $	��500��$��50��$
��Left=�$��Center=�$	��Top=�)  #DevExpress.Xpf.Editors.TextEditBase   AcceptsReturn . 
������ "   VerticalScrollBarVisibility$ Visible=�   *
IsEditMode+$ PropertyChanged=�%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *EditMusteri.Adres+��Z� ]� $��5q�.+��� $	��120��$��Sevk Adresi : ��$ Right=��� $��14(�$
��Bold"�   *!IsEditMode+$ PropertyChanged=����� *Musteri.SevkAdres+  $	��500��$��50��$
��Left=�$��Center=�$	��Top=� . 
������ $ Visible=�   *
IsEditMode+$ PropertyChanged=� �� *EditMusteri.CariSevkAdres+��Z� ]� -   $��telefon��$��1��.+��� $	��120��$��Center=�$��
Telefon : ��$ Right=��� $	��120��$��20��$
��Left=�$��Center=�
������ $
��Bold"�   *!IsEditMode+$ PropertyChanged=���Z� ���� *Musteri.Tel+$
 PropertyChanged=�  $��25��$��Stretch=�$��Center=�
������    *
IsEditMode+$ PropertyChanged=� �� *EditMusteri.Tel+ �� Mode$ TwoWay=�$
 PropertyChanged=���Z� ]� -   $
��mail��$��1��.+��� $	��120��$��20��$��Center=�$��Firma Mail : ��$ Right=�&	   DevExpress.Xpf.Core.SimpleButton	  $
��Left=�  MyLinkButtonAc#��[    *!IsEditMode+$ PropertyChanged=����� *MailAcCommand+���� *Musteri.Email+  $��25��$��Stretch=�$��Center=�
������    *
IsEditMode+$ PropertyChanged=� �� *EditMusteri.Email+$ TwoWay=�$
 PropertyChanged=���Z� ]� -   $	��web��$��1��.+��� $	��120��$��20��$��Center=�$��Firma Web : ��$ Right=�	  $
��Left=�#��[    *!IsEditMode+$ PropertyChanged=����� *WebSiteAcCommand+���� *Musteri.Web+  $��25��$��Stretch=�$��Center=�
������    *
IsEditMode+$ PropertyChanged=� �� *EditMusteri.Web+$ TwoWay=�$
 PropertyChanged=���Z� �� $��5q�.+�]� $��1��$��30��.+��� $	��120��$��Right=�$��Center=�$��Müşteri Sorumlusu :��$ Right=��� $��Center=�$��Center=�$
��Bold"�   *!IsEditMode+$ PropertyChanged=����� *Musteri.MusteriSorumlusu+)
  #DevExpress.Xpf.Editors.ComboBoxEdit
  +  %DevExpress.Xpf.Editors.LookUpEditBase   DisplayMember$ AdSoyad��   ValueMember$ PlasiyerKod��   *
IsEditMode+$ PropertyChanged=�   *EditMusteri.PlasiyerKod+   Mode$ TwoWay=�$ PropertyChanged=�   ItemsSource �� *Plasiyerler+]� $��30��.+��� $��3��$	��120��$��Right=�$��Center=�$��Saha Sorumlusu :��$ Right=��� $��Center=�$��Center=�$
��Bold"�   *!IsEditMode+$ PropertyChanged=����� *Musteri.PandaSahaSorumlusu+
  $ AdSoyad��$ KullaniciId��   *
IsEditMode+$ PropertyChanged=�   *EditMusteri.SahaSorumlusuId+$ TwoWay=�$ PropertyChanged=� �� *SahaSorumlulari+]� $��2��$��30��.+��� $	��120��$��Right=�$��Center=�$��Agent :��$ Right=��� $��Center=�$��Center=�$
��Bold"�   *!IsEditMode+$ PropertyChanged=����� *Musteri.Agent+
  $ AdSoyad��$ KullaniciId��   *
IsEditMode+$ PropertyChanged=�   *EditMusteri.AgentId+$ TwoWay=�$ PropertyChanged=� �� *
Agentlar+]� -   $��sektör��$��2��.+��� $	��120��$��20��$��Center=�$��
Sektör : ��$ Right=��� $��20��$
��Left=�$��Center=�
������ $
��Bold"�   *!IsEditMode+$ PropertyChanged=���Z� ���� *Musteri.FirmaGrupTip+$
 PropertyChanged=�
  $��30��$��14(�   *
IsEditMode+$ PropertyChanged=�   *EditMusteri.FirmaGrupTip+ �� *	Sektorler+]� -   $��durumu��$��3��.+��� $	��120��$��20��$��Center=�$��Durumu: ��$ Right=��� $��20��$
��Left=�$��Center=�
������ $
��Bold"�   *!IsEditMode+$ PropertyChanged=���Z� ���� *Musteri.Durumu+$
 PropertyChanged=�
  $��30��$��14(�   *
IsEditMode+$ PropertyChanged=�   *EditMusteri.Durumu+ �� *MusteriDurumlar+�� $��0,20,0,0q�
�������$��0q�._�� $
��Left=�.+��� $	��200��$��80��$��Center=����� $��Center=�$ 
Horizontal=�.+��� $��Center=�$
��Bold"�
�������Yıllık Kapasite ?.��]� $��Center=�.+��� $��40��$��30(�$
��Bold"�   *!IsEditMode+$ PropertyChanged=����� *Musteri.YillikTonaj+ �� StringFormat$ {0:n0}��  $��90��$��40��$��Center=�$��30(�$
��Bold"�   *
IsEditMode+$ PropertyChanged=� �� *EditMusteri.YillikTonaj+�� $��Center=�$��20(�$
��Bold"�$�� ���� $	��200��$��80��$��30,0,0,0q�$��Center=����� $��Center=�$ 
Horizontal=�.+��� $��Center=�$
��Bold"�
�������Kullanım Alanı.���� .+��� $��Center=�$��20(�$
��Bold"�   *!IsEditMode+$ PropertyChanged=����� *Musteri.KullanimAlanAd+
  $��30��$ Aciklama_EN��$��20(�$ KullanimAlanKod��   *
IsEditMode+$ PropertyChanged=�   * EditMusteri.KullanimAlanTipKod+$ TwoWay=�$ PropertyChanged=� �� *KullanimAlanlari+�� $	��200��$��80��$��30,0,0,0q�$��Center=����� $��Center=�$ 
Horizontal=�.+��� $��Center=�$
��Bold"�
�������Açık Hesap.��]� $��Center=�.+��� $��20(�$
��Bold"����� * Musteri.RiskBilgiDto.AcikHesap+$ {0:n0}���� $��4,0,0,0q�$��20(�$
��Bold"����� *Musteri.DovizTipSimge+�� $
��auto��$��80��$	��200��$��30,0,0,0q�$��Center=����� $��Center=�$ 
Horizontal=�.+��� $��Center=�$
��Bold"�
�������Kontrat Dönem Tip.���� .+��� $ 
Horizontal=�   *!IsEditMode+$ PropertyChanged=�.+��� $	��120��$��20��$��Center=�$��20(�$
��Bold"�$ Center=����� *Musteri.KontratDonemTip+$
 PropertyChanged=��� $	�� - ���� $	��120��$��20��$��Center=�$��20(�$
��Bold"�$ Center=����� *Musteri.KontratDonemDeger+$
 PropertyChanged=��� $ 
Horizontal=�   *
IsEditMode+$ PropertyChanged=�.+�
  $��80��$��30��$��14(�'  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable$ FalseX�   *EditMusteri.KontratDonemTip+$ PropertyChanged=� �� *KontratDonemleri+��    *KontratDonemiYillikMi+$ PropertyChanged=�.+�
  $��80��$��30��$��10,0,0,0q�$��14(�$ FalseX�   *EditMusteri.KontratDonemDeger+$ PropertyChanged=� �� *KontratDonemleriAlt+�� $ 
Horizontal=�   *KontratDonemiCegrekMi+$ PropertyChanged=�.+��� $��10,0,0,0q�$��Center=�$��1.��
  $��80��$��30��$��14(�$ FalseX�   *'%EditMusteri.KontratDonemDeger_Cegrek1+$ PropertyChanged=� �� *KontratDonemleriAlt+�� $��10,0,0,0q�$��Center=�$��2.��
  $��80��$��30��$��14(�$ FalseX�   *'%EditMusteri.KontratDonemDeger_Cegrek2+$ PropertyChanged=� �� *KontratDonemleriAlt+�� $��10,0,0,0q�$��Center=�$��3.��
  $��80��$��30��$��14(�$ FalseX�   *'%EditMusteri.KontratDonemDeger_Cegrek3+$ PropertyChanged=� �� *KontratDonemleriAlt+�� $��10,0,0,0q�$��Center=�$��4.��
  $��80��$��30��$��14(�$ FalseX�   *'%EditMusteri.KontratDonemDeger_Cegrek4+$ PropertyChanged=� �� *KontratDonemleriAlt+�� $��1��$��0,20,0,0q�  �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value  �� 
TargetName��%\  '��       �� " ��.���  � �� �� " ��.R��� $��Border��$	��2,0q�
���  ��$��1,1,1,0q�$ 4,4,0,0��._��� $ ContentSite��$
��10,2q�$��Center=�$��Center=�$��Header�� P�  �@��
IsSelected . . ��� $  Border�� � �� �	#FFDEFF00P�  �@��
IsSelected .  . ��� $  Border�� � ��
 �����.@��� ���� .R��� $ 
Horizontal=�.+�  $��30��$��35��$��Right=�$��Center=�$	 AccountMultiple=��� ! �� Padding$! 5q�$��Center=�$��18(�$��Müşteri Sorumluları��.���  �� $
��auto ��� .+��� $��5q�$
��Left=�$��Firma Yetkilisi Ekle��$y�Visible=����� *KisiEkleFormAcCommand+%  DevExpress.Xpf.Grid.GridControl  $��1��$��4q�)  #DevExpress.Xpf.Grid.DataControlBase"   AutoGenerateColumns$
" None=�#   FilterString$# [Durum] != 'Arşiv'��$   ItemsSource$ �� *Musteri.CariEmailListe+%   View% #  DevExpress.Xpf.Grid.TableView  &   	AutoWidth& . $��14(�'   RowMinHeight$' 30Q�(   ShowAutoFilterRow( .  &   DevExpress.Xpf.Grid.GridViewBase)   ShowGroupPanel) .  &   DevExpress.Xpf.Grid.DataViewBase*   ColumnHeaderTemplate* �� .R��� $��30��$��DemiBold"�$
��Wrap=����� +   Columns.+ $  DevExpress.Xpf.Grid.GridColumn  $  DevExpress.Xpf.Grid.BaseColumn,   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$, 30 -   MaxWidth$- 30Q�$  DevExpress.Xpf.Grid.ColumnBase.   AutoFilterCriteria$. ContainsX�/   FilterPopupMode$/ Excel=�0   Header)  #DevExpress.Xpf.Core.ObjectConverter$0 # 1   ReadOnly1 . 2   CellTemplate2 �� .R��� $��25��$��30��$��25��$��2q�$��0q�
������ #��[ ���� *+)View.DataContext.KisiDuzenleFormAcCommand+���� *RowData.Row+.��  $��20��$��20��$��Center=�$
��Bold"�$	 Pencil=�  3   	FieldName$3 YetkiliKisi��$0 AdSoyad 4   	SortIndex$4 1��  $3 Email��  $3 Unvan��  $3 Birim��  $	3 Tel��  $3 Durum��$4 0��5   	SortOrder] YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a9  3DevExpress.Data.Design.ColumnSortOrderTypeConverter$5 	Ascending �� ���� .R��� $ 
Horizontal=�.+�  $��30��$��35��$��Right=�$��Center=�$	 PhoneLog=��� $! 5q�$��Center=�$��18(�$��Görüşmeler��.���  �� $
��auto ��� .+��� $��5q�$
��Left=�$��Görüşme Ekle��$y�Visible=����� *GorusmeEkleFormAcCommand+  $��1��$��4q�$ �� *Musteri.Gorusmeler+$ TwoWay=�$
 PropertyChanged=� ��%4   '        �� "  .���  �@ ReadOnly .  %   -   6  Name$6 View1��7   AllowPerPixelScrolling7 . & . $��14(�8   NewItemRowPosition$
8 None=�$' 30Q�( . ) .  9   ShowTotalSummary9 . * �� .R��� $��35��$
��0,-4q�$��Center=�$��SemiBold"�$ Center=�$
��Wrap=����� * +.+   $, 20 $- 20Q�$. ContainsX�$/ Excel=�$0 # 1 . 2 �� .R��� $��35��$��30��$��2q�$��0q�
������ ���� *!View.DataContext.DuzenleCommand+���� *RowData.Row+��Z� .��  $��Center=�$	 Magnify=�  $, 45 $3 GorusmeTarih��$/ Excel=�$0 Görüşme Tarihi   $, 40 $/ Excel=�$0 Görüşülen Kişi :   IsSmart: . 2 �� .R�  ;  Name$; PART_Editor�� �� *RowData.Row.GorusulenKisi+$ TwoWay=�$
 PropertyChanged=�  $, 50 $/ Excel=�$0 Görüşme Konusu : . <   Visible< .  2 �� .R�  $; PART_Editor�� �� *RowData.Row.GorusmeKonuTip.Ad+$ TwoWay=�$
 PropertyChanged=�  $, 35 $3 GorusmeTip.Ad��$/ Excel=�$0 Görüşme Tip : . < .  2 �� .R�  $; PART_Editor�� �� *RowData.Row.GorusmeTip.Ad+$ TwoWay=�$
 PropertyChanged=�  $, 40 $3 KullanimAlanTipKod��$/ Excel=�$0 	K. Alanı : . < .    $	, 100 $3 GorusmeDetay��$/ Excel=�$0 Detay : . 2 �� .R�  $; PART_Editor��$��20��$��20��$	��Top=� �� *RowData.Row.GorusmeDetay+  $, 30 $3 Ekleyen��$/ Excel=�$0 Ekleyen : .   $, 50 =   MinWidth$= 50��$- 50Q�$3 MesajSayisi��$0 Mesaj   
MesajKolon#��[   $, 50 $3 MusteriCariKod��$/ Excel=�< .  �� ���� .R��� $ 
Horizontal=�.+�  $��30��$��35��$��Right=�$��Center=�$	 StackOverflow=��� $! 5q�$��Center=�$��18(�$��Cari Hareketler��.��  $��4q�$
" None=�$��12(�$ �� *YaslandirilmisFaturalar+>   TotalSummary> )  #DevExpress.Xpf.Grid.GridSummaryItem  )  #DevExpress.Xpf.Grid.SummaryItemBase?   DisplayFormat$? {0:n0}��@   	FieldName$@ 	BORCTUTAR��A   ShowInColumn$A  ��B   SummaryType5  /DevExpress.Data.Design.SummaryItemTypeConverter$	B Sum   $? Ort: {0:n0}��$	@ GUN��$A  ��$B Average %   ) .  9 . * �� .R��� $��30��$��DemiBold"�$
��Wrap=����� .+   $3 BELGE_NO��$/ Excel=�$0 Belge No   $3 TARIH��$/ Excel=�$0 Fatura Tarihi C   EditSettingsC 6  0DevExpress.Xpf.Editors.Settings.DateEditSettings  6  0DevExpress.Xpf.Editors.Settings.BaseEditSettingsD   DisplayFormat$D 
dd/MM/yyyy��  $3 VADE_TARIHI��$/ Excel=�$0 Vade Tarihi   $, 70 $	3 GUN��$/ Excel=�$0 	Vade Gün   $3 	BORCTUTAR��$/ Excel=�$0 Para Birimi C   $D n2���� ���� .R��� $ 
Horizontal=�.+�  $��30��$��35��$��Right=�$��Center=�$
	 Bomb=��� $! 5q�$��Center=�$��18(�$��
Risk Durum��.��5 	 /DevExpress.Xpf.PropertyGrid.PropertyGridControl  $��4q�$��14(�E   HighlightingMode$
E None=�F   ShowCategoriesJ 	 DDevExpress.Xpf.PropertyGrid.Internal.CategoriesShowModeTypeConverter$F false G   SelectedObjectG �� *Musteri.RiskBilgiDto+H   PropertyDefinitionsH 4 	 .DevExpress.Xpf.PropertyGrid.PropertyDefinition  I   Type"I ��J   EditSettingsJ   6   0DevExpress.Xpf.Editors.Settings.TextEditSettingsK    Mask$K T��L    MaskUseAsDisplayFormatL .   "I ��J    $D n0��L . �� ���� .R��� $ 
Horizontal=�.+�  $��30��$��35��$��Right=�$��Center=�$	 FileDocument=��� $! 5q�$��Center=�$��18(�$��Dökümanlar��.���  �� $
��auto ��� .+��� $��5q�$
��Left=�$��Döküman Ekle��$y�Visible=����� *DosyaEkleCommand+  $��1��$��4q�$
" None=�$ �� *Musteri.CariDokumanlar+%   $��14(�$' 30Q�) .  9 . * �� .R��� $��30��$��DemiBold"�$
��Wrap=����� .+   $, 75 $- 75Q�M   AutoFilterCondition$M Contains=�$3 
SiparisKod��$/ Excel=�N   
FixedWidthN . 2 �� .R�	  $��90��#��[ ���� *#!View.DataContext.DosyaEkleCommand+���� *RowData.Row.DosyaAd+  $	, 300 $3 Aciklama��$0 
Açıklama   $	, 200 $3 Ekleyen��  $	, 100 $3 Tarih��