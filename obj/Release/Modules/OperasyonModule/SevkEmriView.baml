   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Pandap.UI.Modules.OperasyonModule!Pandap.UI.Modules.OperasyonModule  G CPandap.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_?clr-namespace:Pandap.Logic.Services._DTOs;assembly=Pandap.LogicPandap.Logic.Services._DTOs 3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  M ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089/$clr-namespace:System;assembly=SystemSystem 4    .Pandap.UI.Modules.OperasyonModule.SevkEmriView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa	 ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 =local/clr-namespace:Pandap.UI.Modules.OperasyonModule  	 ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  K_dtos?clr-namespace:Pandap.Logic.Services._DTOs;assembly=Pandap.Logic  ]
 YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  ?mvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  -helperclr-namespace:Pandap.UI.Helper 
  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  1system$clr-namespace:System;assembly=System  -     >�Name$  SevkiyatForm��$��10q� � 	Resources $ 	 DevExpress.Xpf.Grid.GridColumn �� 
TargetType$  DevExpress.Xpf.Grid.ColumnBase �� Property �� Value$  DevExpress.Xpf.Grid.BaseColumn2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter   editCellTemp �� CornerRadius%  DevExpress.Xpf.Editors.TextEdit&   DevExpress.Xpf.Core.ThemeManager   	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter��%(  '        &	  �     �� "  .���  �@ ReadOnly . ��  �@ FilterPopupMode$ Excel=���  �@ AutoFilterCriteria$ ContainsX���  �@ Width$ 70 �� .R��� $	��br1��
������ $ 1��$��1q�
��� � �$��1q�._�  $��PART_Editor��$��Center=�$	 XXX 2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -	  'DevExpress.Mvvm.UI.CurrentWindowService	   	  ClosingCommand �� *FormClosingCommand+'
  !DevExpress.Mvvm.UI.EventToCommand
  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]	   
SourceName$	 SevkiyatForm��
   	EventName$
 Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��334* �.+��� $��-10,-10,-10,0q�$��0��.+�`� $��0��.�c� $��50��}�.    MaterialDesignToolBar#���  .@���  �� 	IsDefault .   MaterialDesignFlatButton#���  ���� *KaydetKapatCommand+.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ ContentSave=��� $��8 0 0 0q�$��Center=�   MaterialDesignBody1TextBlock#��[ $��Kaydet����  �� IsCancel . #���  ���� *IptalCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Close=��� $��8 0 0 0q�$��Center=�#��[ $��İptal��c� $��50��#���  .@��� $��Center=�$ 
Horizontal=�.+��� $ 
Horizontal=�.+���  �� 
IsReadOnly . $	��Top=�$
��Left=�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Fatura Cari Kod�� % !MaterialDesignFloatingHintTextBox#��[  �� TextWrapping$
 Wrap=�$��50��$	��100��$��0,0,20,0q����� *'%SeciliSevkiyatEmri.FaturaKesimCariKod+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��0q�$��32��#��[ ���� *MusteriSecCommand+.��  $ Magnify=�$��24��$��24���� $	��Top=�$
��Left=�$ Kantar Dara��#��[ $
 Wrap=�$��50��$��80��$��0,0,20,0q����� *SeciliSevkiyatEmri.KantarDara+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Kantar Brüt��#��[ $
 Wrap=�$��50��$��80��$��0,0,20,0q����� *SeciliSevkiyatEmri.KantarBrut+$ TwoWay=�$ PropertyChanged=���  . $	��Top=�$
��Left=�
�������$ 
Kantar Net��#��[ $
 Wrap=�$��50��$	��110��$��0,0,20,0q����� *SeciliSevkiyatEmri.KantarNet+$ OneWay=��� $
��Bold"�
���  �� . 
�������$	��Top=�$
��Left=�$ Fark=K.Net-Paket Brut��#��[ $
 Wrap=�$��50��$	��110��$��0,0,20,0q����� *SeciliSevkiyatEmri.KantarFark+$ OneWay=��� $��	Tüm Depo�� d� 	IsChecked �� *DataContext.TumDepoGelsinMi+ �� ElementName$ SevkiyatForm��$ TwoWay=�$ PropertyChanged=�c� $��50��#���  .@��� $ 
Horizontal=�.+��� $	��Top=�$
��Left=�$ Ambar Sorumlusu��#��[ $
 Wrap=�$��50��$	��110��$��0,0,20,0q����� *#!SeciliSevkiyatEmri.AmbarSorumlusu+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Lojistik��#��[ $
 Wrap=�$��50��$	��110��$��0,0,20,0q����� *SeciliSevkiyatEmri.Lojistik+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Muhasebe��#��[ $
 Wrap=�$��50��$	��110��$��0,0,20,0q����� *SeciliSevkiyatEmri.Muhasebe+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ 	Güvenlik��#��[ $
 Wrap=�$��50��$	��110��$��0,0,20,0q����� *SeciliSevkiyatEmri.Guvenlik+$ TwoWay=�$ PropertyChanged=��� 
�������$��1��.+��� $
��0,10q�$ 
Horizontal=� � DataContext �� *SeciliSevkiyatEmri+.+��� $��0,0,20,0q�$��50�� & "MaterialDesignFloatingHintComboBox#��[  �� DisplayMemberPath$ UlasimTipAd�� �� SelectedValuePath$ UlasimTipAd��$ Ulaşım Şekli��$	��100������ *DataContext.UlasimTipleri+$ SevkiyatForm�� �� SelectedValue �� *	UlasimTip+���� .R�$� �� $	��Top=�$
��Left=�$ 	Nakliyeci��#��[ $
 Wrap=�$��50��$	��110��$��0,0,20,0q����� *	Nakliyeci+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ 	Nak.FatNo��#��[ $
 Wrap=�$��50��$	��100��$��0,0,20,0q����� *NakliyeciFaturaNo+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Nakliye Fiyatı��#��[ $
 Wrap=�$��50��$��70��$��0,0,20,0q����� *NakliyeFiyati+$ TwoWay=�$ PropertyChanged=��� $��50��$��50��$��0,0,20,0q�#��[ $ N.Döviz C.������ *DataContext.DovizCinsleri+ �� RelativeSource ��  �� AncestorType$ UserControlL� �� *NakliyeDovizCinsi+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Plaka��#��[ $
 Wrap=�$��50��$��80��$��0,0,20,0q����� *Plaka+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Dorse��#��[ $
 Wrap=�$��50��$��80��$��0,0,10,0q����� *Dorse+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Şofor Adı��#��[ $
 Wrap=�$��50��$	��120��$��0,0,10,0q����� *AracSoforAd+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ 
Şofor Tel��#��[ $
 Wrap=�$��50��$��80��$��0,0,10,0q����� *
SoforTel+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ Kontainer No��#��[ $
 Wrap=�$��50��$	��120��$��0,0,10,0q����� *KontainerNo+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$
��Left=�$ 
Mühür No��#��[ $
 Wrap=�$��50��$��80��$��0,0,10,0q����� *	MuhurNo+$ TwoWay=�$ PropertyChanged=�% 	 DevExpress.Xpf.Grid.GridControl  )  #DevExpress.Xpf.Grid.DataControlBase   AllowLiveDataShaping$
 TrueX�$��2��   AutoExpandAllGroups .     ItemsSource  �� *$"SeciliSevkiyatEmri.CariIrsaliyeler+ * 	 $DevExpress.Xpf.Grid.GridColumnHeader  MaviSutunBaslikSitil! �� BasedOn.  (DevExpress.Xpf.Grid.BaseGridColumnHeader��%y   '        7  �� "  8!   .���  � ��$ 20����  �@ ShowFilterButtonOnHover .  ��  � ��$	 Top=�"   TotalSummary" ) 	 #DevExpress.Xpf.Grid.GridSummaryItem  )  #DevExpress.Xpf.Grid.SummaryItemBase#   	FieldName$# IrsPaletNet_TKg��$   ShowInColumn$$ CariAd��%   DisplayFormat$% Net   : {0:n0}��&   SummaryType] YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Data.Design.SummaryItemTypeConverter$	& Sum   $# IrsPaletDara_TKg��$$ CariAd��$% Dara  : {0:n0}��$	& Sum   $# IrsPaletBrut_TKg��$$ CariAd��$% Brut  : {0:n0}��$	& Sum '   View' # 	 DevExpress.Xpf.Grid.TableView  (   	AutoWidth( .  )   ShowVerticalLines) .  & 	  DevExpress.Xpf.Grid.GridViewBase*   ShowGroupPanel* .  +   AllowPerPixelScrolling+ . &   DevExpress.Xpf.Grid.DataViewBase,   ShowTotalSummary, . -   Columns.-   .   Width$. * /   Header)  #DevExpress.Xpf.Core.ObjectConverter$
/ Cari 0   	FieldName$0 CariAd��1   	SortOrder9  3DevExpress.Data.Design.ColumnSortOrderTypeConverter$1 	Ascending 2   	SortIndex$2 0��3   DisplayTemplate3 �� .R��� $��PART_Editor��$��14(�$��DemiBold"�4 �� TextAlignment$
4 Left=�5   DetailDescriptor5 1  +DevExpress.Xpf.Grid.ContentDetailDescriptor  .  (DevExpress.Xpf.Grid.DetailDescriptorBase6   ContentTemplate6 �� .R�  $
 TrueX� . $	��500��7   SelectedItem7 �� *!DataContext.SeciliIrsaliyePalet+$ SevkiyatForm��$ TwoWay=�$ PropertyChanged=�  �� *IrsaliyePaletler+'   ( . * .  8   ShowGroupFooters8 . 9   EnableImmediatePosting9 . , . :   GroupSummary:   $# PaletNet_Kg��;   ShowInGroupColumnFooter$; PaletNet_Kg��$%  {0:n0}��$	& Sum $<   GroupColumnFooterElementStyle< �� " ��.���  � ��$
 Bold"�  $# PaletDara_Kg��$%  {0:n0}��$; PaletDara_Kg��$	& Sum < �� " ��.���  � ��$
 Bold"�  $# PaletBrut_Kg��$%  {0:n0}��$; PaletBrut_Kg��$	& Sum < �� " ��.���  � ��$
 Bold"�"   $# PaletNet_Kg��$%  {0:n0}��$	& Sum < �� " ��.���  � ��$ 	UltraBold"�  $# PaletDara_Kg��$%  {0:n0}��$	& Sum   $# PaletBrut_Kg��$%  {0:n0}��$	& Sum   $# PaletToplamTutar��$%  {0:n0}��$	& Sum   $# PaletKdvTutar��$%  {0:n0}��$	& Sum   $# PaletGenelToplamTutar��$%  {0:n0}��$	& Sum =   Bands= ) 	 #DevExpress.Xpf.Grid.GridControlBand  >   Visible> . ?   Columns.?   $. 50 @   VisibleIndex$@ 1��$/ # A   CellTemplateA �� .R��� $��0q�$��40��$��20��$��3q�$
��Bold"�$��X��
���  ��#���  ���� *DataContext.PaletCikarCommand+ �� $ UserControlL����� B �� Path$B RowData.Row�  $. 50 C   ShowGroupedColumn5  /DevExpress.Utils.Design.DefaultBooleanConverter$C False $1 	Ascending D   
GroupIndex$D 0��$0 PaletGrupKey��E   GroupValueTemplateE �� .R��� $��Medium"�$��Right=�$��Center=����� $B RowData.Row.PaletGrupKey�  $. 50 $0 PaletId��  $. 30 F   ReadOnlyF .  $0 
YuklendiMi��3 �� .R��� $��24��$��24��$
��Fill=����� $B 	EditValue� �� *TemplatedParent+G �� 	ConverterG 1  +DevExpress.Mvvm.UI.BooleanToObjectConverter  H   	TrueValueH @�!/Content/BoolImages/truck.png?�  $. 50 $	/ Net $0 PaletNet_Kg��  F .  $. 50 $
/ Dara $0 PaletDara_Kg��A �� .R��� 
��� ���$��1q�._�  %   DevExpress.Xpf.Editors.BaseEditI    	EditValueI �� *RowData.Row.PaletDara_Kg+$ TwoWay=�$ PropertyChanged=�  $. 50 $
/ Brut $0 PaletBrut_Kg��  $. 50 $/ KartNo $0 KartNo��  $. 50 $0 Kalinlik��  $. 50 $0 En��  $. 50 $0 UrunKod��  $. 50 $0 BobinlerBirlesik��  $. 50 J   AutoFilterCriteria$J ContainsX�$0 FiyatKalemKod��  $. 50 $J ContainsX�$0 SiparisKalemKod��  $. 50 $J ContainsX�$0 UretimEmriKod��  $. 50 $J ContainsX�$0 
SiparisKod��  $. 50 $0 FirinNo��  $. 50 $0 TavNo��  $. 50 $0 SehpaNo��  $. 50 $0 Aciklama��  > .  .?   $. 50 $0 LfxKod��  $. 50 $0 	LmeBF_Ton��  $. 50 $0 IscilikBF_Ton��  $. 50 $0 BirimFiyat_Kg��  $. 50 $0 KdvOran��  $. 50 $0 PaletToplamTutar��  $. 50 $0 PaletKdvTutar��  $. 50 $0 PaletGenelToplamTutar��  $. 50 $0 DovizTipKod��� $��2��K � ColumnDefinitionsK �� �� $��5 ��� $	��155 �.+��� 
���  ��$��1��$	��170��$��5��$��Stretch=�#!  MaterialDesignThemes.Wpf.Card!  $
��auto��+"  %MaterialDesignThemes.Wpf.ShadowAssistL "  ShadowDepth$L Depth3=�$��Stretch=�$��5q�$��2��.��  -   M  Name$M grdMamulDepo��  �� *MamulDepoStoklar+$ TwoWay=�$ PropertyChanged=�7 �� *SeciliMamulDepoStok+   YesilSutunBaslikSitil$# 	 DevExpress.Xpf.Grid.RowControl��%�   '        7  '       '#  a     �� "  8!   �� "  .���  �@ Width$ 60 ��  �@ FilterPopupMode$ Excel=��� " # .���  � ��$ 40��"   $# PaletId��$% Say: {0:n0}��$& Count   $# Uretim_PaketlenenMiktar��$%  {0:n0}��$	& Sum   $# PaletNet_Kg��$%  {0:n0}��$	& Sum   $# PaletDara_Kg��$%  {0:n0}��$	& Sum   $# PaletBrut_Kg��$%  {0:n0}��$	& Sum   $# Kalinlik_micron��$% Ort: {0:n0}��$& Average   $# En_mm��$% Ort: {0:n0}��$& Average '   N   ShowAutoFilterRowN . , . ( .  O   ClipboardCopyOptions$O Excel=�P   AllowEditingP .  Q   AllowGroupingQ . * .  R   ColumnHeaderTemplateR �� .R��� $��35��$��Center=�$4 Center=�$
��Wrap=�$
��0,-4q����� * +.-   $/ # A �� .R��� $��22(�$��0q�$��50��$��30��$��3q�$
��Bold"�$��+��
��� � �#���  ���� *(&DataContext.PaletiSeykiyataEkleCommand+ �� $ UserControlL����� $B RowData.Row�  $0 PaletId��  $0 PaletNet_Kg��  $J ContainsX�$0 
SiparisKod��  $	. 100 $0 CariIsim��S   FilterPopupMode$S Excel=�$J ContainsX�  $0 Kalinlik_micron��$/ 
Kalınlık   $0 En_mm��$/ En   $0 AlasimTipKod��  $S Excel=�$0 SevkYilHafta��  $0 DepoKabulTarihi��$/ Depo Kabul T.   $0 StokGunSuresi��  $J ContainsX�$0 FirmaSiparisNo��  $J ContainsX�$0 SiparisKalemKod��  $J ContainsX�$0 KartNo��  $0 BobinlerBirlesik��  $0 SertlikTipKod��  $0 PaletDara_Kg��  $0 PaletBrut_Kg��  $0 	PaletEbat��  $0 UlkeAd��  $0 Aciklama��T   AllowResizing$
T True $/ Açklma 