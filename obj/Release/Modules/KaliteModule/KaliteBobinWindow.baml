   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  6    0Pandap.UI.Modules.KaliteModule.KaliteBobinWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  >dx5http://schemas.devexpress.com/winfx/2008/xaml/editors  +helperclr-namespace:Pandap.UI.Helper    � Title$  KaliteBobinWindow�� � 	Resources ��%    .��� $��10q� � RowDefinitions �� $
��Auto ��� .+�%  DevExpress.Xpf.Grid.GridControl  $��1��)  #DevExpress.Xpf.Grid.DataControlBase   IsFilterEnabled .     SelectedItem �� *SeciliBobin+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   ItemsSource �� * AktifUretimEmri.UretimBobinler+   Bands )  #DevExpress.Xpf.Grid.GridControlBand  $  DevExpress.Xpf.Grid.BaseColumn	   Header)  #DevExpress.Xpf.Core.ObjectConverter$	 FH 
   Columns.
 $  DevExpress.Xpf.Grid.GridColumn     MinWidth$ 65��$  DevExpress.Xpf.Grid.ColumnBase   	FieldName$ 
FH_CikisNo��$	 FH Çıkış No    	CellStyle ��  �� 
TargetType/  )DevExpress.Xpf.Grid.LightweightCellEditor"  .���  �� Property �@ 
Background �� Value
 � ���  .
   $ 65��'    HorizontalHeaderContentAlignment$ Stretch=�$ BobinNo�� �� "  .���  �@ 
Background
 � ���   HeaderTemplate �� .R��� $��Stretch=� �� Orientation$ Vertical=�.+��� $��70��$
��Wrap=�$��Bobin No / Coil No���� $��2q����� *0.DataContext.AktifUretimEmri.KaliteSinirlari.Id+ �� RelativeSource ��  �� AncestorType"  $ TwoWay=�$ PropertyChanged=��� $��2q�
�������$��Min-Max��  $ 40��$ FirinNo��$	 
Fırın No  �� "  .���  �@ 
Background
 � ���  $ 40��$ TavNo��$	 Tav No  �� "  .���  �@ 
Background
 � ���  $ 40��$ SehpaNo��$	 Sehpa No  �� "  .���  �@ 
Background
 � ���  $ 40��$ 	BobinAdet��$	 B. Adet    Width2	  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$ 60	  �� "  .���  �@ 
Background
 �����  $	 Kimyasal Bileşim    Bands   $	 % .
   $ 55	 $ Stretch=�$ KB_Si_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Si��%
  DevExpress.Xpf.Editors.TextEdit
  $��2q�
�������%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Si_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=�   CellTemplate �� .R�
   
 Name$ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Si_yuzde+  $ 55	 $ Stretch=�$ KB_Fe_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Fe��
  $��2q�
������� �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Fe_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Fe_yuzde+  $ 55	 $ Stretch=�$ KB_Cu_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Cu��
  $��2q�
������� �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Cu_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Cu_yuzde+  $ 55	 $ Stretch=�$ KB_Mn_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Mn��
  $��2q�
������� �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Mn_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Mn_yuzde+  $ 55	 $ Stretch=�$ KB_Ti_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Ti��
  $��2q�
������� �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Ti_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Ti_yuzde+  $ 55	 $ Stretch=�$ KB_Zn_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Zn��
  $��2q�
������� �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Zn_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Zn_yuzde+  $ 55	 $ Stretch=�$ KB_Mg_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Mg��
  $��2q�
������� �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Mg_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Mg_yuzde+  $		 ppm .
   $ 55	 $ Stretch=�$ 	KB_Pb_ppm�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Pb��
  $��2q�
������� �� *=;DataContext.AktifUretimEmri.KaliteSinirlari.KB_Pb_ppm_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Pb_yuzde+  $ 55	 $ Stretch=�$ 	KB_Cr_ppm�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Cr��
  $��2q�
������� �� *=;DataContext.AktifUretimEmri.KaliteSinirlari.KB_Cr_ppm_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Cr_yuzde+  $ 55	 $ Stretch=�$ 	KB_Cd_ppm�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Cd��
  $��2q�
������� �� *=;DataContext.AktifUretimEmri.KaliteSinirlari.KB_Cd_ppm_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Cd_yuzde+  $
	 Al % .
   $ 55	 $ Stretch=�$ KB_Al_yuzde�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��Al��
  $��2q�
������� �� *?=DataContext.AktifUretimEmri.KaliteSinirlari.KB_Al_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.KB_Al_yuzde+  $1	 +Mekanik Özellikler / Mechanical Properties    $	 Rolling Direction (0) .
   $  MO_RD_AkmaMukavemeti_N_mm2��$ Stretch=�$ 55	  �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��10(� �� TextAlignment$ Center=�$
��Wrap=�$/��)Akma Mukavemeti / Yield Strength (N/mm^2)��
  $��2q�
������� �� *NLDataContext.AktifUretimEmri.KaliteSinirlari.MO_RD_AkmaMukavemeti_N_mm2_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *(&RowData.Row.MO_RD_AkmaMukavemeti_N_mm2+  $ MO_RD_MekMukavemet_N_mm2��$ Stretch=�$ 55	  �� .R��� $��Stretch=�$ Vertical=�.+��� $��10(�$��70��$ Center=�$
��Wrap=�$/��)Mek.Mukavemet / Tensile Strength (N/mm^2)��
  $��2q�
������� �� *LJDataContext.AktifUretimEmri.KaliteSinirlari.MO_RD_MekMukavemet_N_mm2_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *&$RowData.Row.MO_RD_MekMukavemet_N_mm2+  $ MO_RD_Uzama_yuzde��$ Stretch=�$ 55	  �� .R��� $��Stretch=�$ Vertical=�.+��� $��10(�$��70��$ Center=�$
��Wrap=�$!��Uzama / Elongation (%) A100��
  $��2q�
������� �� *ECDataContext.AktifUretimEmri.KaliteSinirlari.MO_RD_Uzama_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.MO_RD_Uzama_yuzde+  $	 Transverse Direction (90) .
   $  MO_TD_AkmaMukavemeti_N_mm2��$ Stretch=�$ 55	  �� .R��� $��Stretch=�$ Vertical=�.+��� $��70��$��10(�$ Center=�$
��Wrap=�$/��)Akma Mukavemeti / Yield Strength ( N/mm^2��
  $��2q�
������� �� *NLDataContext.AktifUretimEmri.KaliteSinirlari.MO_TD_AkmaMukavemeti_N_mm2_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *(&RowData.Row.MO_TD_AkmaMukavemeti_N_mm2+  $ MO_TD_MekMukavemet_N_mm2��$ Stretch=�$ 55	  �� .R��� $��Stretch=�$ Vertical=�.+��� $��10(�$ Center=�$
��Wrap=�$��70��$/��)Mek.Mukavemet / Tensile Strength (N/mm^2)��
  $��2q�
������� �� *LJDataContext.AktifUretimEmri.KaliteSinirlari.MO_TD_MekMukavemet_N_mm2_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *&$RowData.Row.MO_TD_MekMukavemet_N_mm2+  $ MO_TD_Uzama_yuzde��$ Stretch=�$ 55	  �� .R��� $��Stretch=�$ Vertical=�.+��� $��10(�$ Center=�$
��Wrap=�$��70��$!��Uzama / Elongation (%) A100��
  $��2q�
������� �� *ECDataContext.AktifUretimEmri.KaliteSinirlari.MO_TD_Uzama_yuzde_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.MO_TD_Uzama_yuzde+  $!	 Diğer Özellikler / Others .
   $ 55	 $ Stretch=�$ DI_Erichsen_mm�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��10(�$ Center=�$
��Wrap=�$��70��$��Erichsen (mm)��
  $��2q�
������� �� *B@DataContext.AktifUretimEmri.KaliteSinirlari.DI_Erichsen_mm_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *RowData.Row.DI_Erichsen_mm+  $ 55	 $ Stretch=�$  DI_KaydiriciMiktari_mgr_m2�� �� .R��� $��Stretch=�$ Vertical=�.+��� $��10(�$ Center=�$
��Wrap=�$��70��$;��5Kaydırıcı Miktarı / Amount of Lubricant (mgr/m^2)��
  $��2q�
������� �� *NLDataContext.AktifUretimEmri.KaliteSinirlari.DI_KaydiriciMiktari_mgr_m2_range+ �� "  $ TwoWay=�$ PropertyChanged=� �� .R�
  $ PART_Editor��$��40��$��1q�
�������$��5q� �� *(&RowData.Row.DI_KaydiriciMiktari_mgr_m2+   View #  DevExpress.Xpf.Grid.TableView     AllowPerPixelScrolling . &   DevExpress.Xpf.Grid.DataViewBase   ShowTotalSummary .    	AutoWidth . &   DevExpress.Xpf.Grid.GridViewBase    AllowGrouping  .  !   ShowGroupPanel! .  "   AllowColumnFiltering" .  #   AllowFilterEditor] YDevExpress.Data.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$# False $   AllowSorting$ .  %   ShowFilterPanelMode$% Never=�&   EditFormPostMode$& 	Immediate=�'   EditFormShowMode$
' None=�(   NewItemRowPosition$( Bottom=�)   ErrorsWatchMode$
) Rows=�]� ��.  $��5q�.+��� $��6q�$
��Auto��$
��Left=�$��Sertifika Yazdır��$��42������ *KapatCommand+�� -   $��6q�$
��Auto��$��Right=�$��Kaydet Kapat��$��42������ *KapatCommand+