   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  k:clr-namespace:Pandap.UI.Modules._SatisModule.KapasiteTakip,Pandap.UI.Modules._SatisModule.KapasiteTakip  H    BPandap.UI.Modules._SatisModule.KapasiteTakip.KapasiteTakipListView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   f	 bDevExpress.Xpf.PivotGrid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxpg7http://schemas.devexpress.com/winfx/2008/xaml/pivotgrid 	 +helperclr-namespace:Pandap.UI.Helper 	 Flocal:clr-namespace:Pandap.UI.Modules._SatisModule.KapasiteTakip 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 U
 QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes 
 2  ,DevExpress.Mvvm.UI.Interactivity.Interaction    	Behaviors  )   #Pandap.UI.Helper.ExportPivotService     
PivotNesne %  System.Windows.Markup.Reference  *pvt1+.���  � RowDefinitions �� $
��auto ��� $��* �.+�c� $��50��}�.     MaterialDesignToolBar#���   .@��� $��0q�  MaterialDesignFlatButton#���  ���� *EkraniYenileCommand+.����  �� Orientation$ 
Horizontal=�.+�' 
 !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ Refresh=��� $��8,0,0,0q�$��Center=�$��Ekran Yenile���� $
��XLSX��#���  ���� *ExcelExportCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ 	FileExcel=��� $��8,0,0,0q�$��Center=�$��Excele Aktar���� -    ��Name$ sumText��$	��100��$��Center=�
�������$��14(�$��DemiBold"��� $��1��$��8q�.@��� ���� .R��� $ 
Horizontal=�.+�  $��30��$��35��$��Right=�$��Center=�$ ChartBarStacked=���  �� Padding$ 5q�$��Center=�$��18(�$��Planlama Rapor��.��/ 	 )DevExpress.Xpf.PivotGrid.PivotGridControl  -     Name$
 pvt1��$��1��   FilterString$) #[SiparisDurum]=='MÜŞTERİ ONAYLI'��	   RowTreeWidth$		 115��
   
DataSource
 �� *Planlanacaklar+   Fields - 	 'DevExpress.Xpf.PivotGrid.PivotGridField  -     Name$ 	miktar_kg��   Area$ 
FilterArea=�   
CellFormat$ n0��   	FieldName$ MiktarKg��   ValueFormat$ n0��   Visible .    -   $ kalite_red_miktar��$ 
FilterArea=�$ n0��$ KaliteRedMiktar��$ n0�� .    -   $ 
paketlenen��$ 
FilterArea=�   Caption$ 
Paketlenen��$ n0��$ PaketlenenMiktar��$ n0�� .    -   $ planlan��$ 
FilterArea=�$ 	Planlanan��$ n0��$ PlanlananMiktar��$ n0�� .    -   $ teslimHafta��$ 
FilterArea=�$ TeslimHafta�� .    $ 
FilterArea=�   	AreaIndex$ 1��$ 	MusteriAd��  $ 
FilterArea=�$ 1��$ SiparisDurum��  $ 
FilterArea=�$ 0��$ Planlanacak��$ n0��$ Bakiye��   GrandTotalCellFormat$ n0��$ n0��  $ 
FilterArea=�$ 1��$ KalemKod��$ SiparisKalemKod��  $ 
FilterArea=�$ 1��$ Alasim��  $ 
FilterArea=�$ 0��$ KullanimAlani��   FilterPopupMode$ Legacy=�  $ DataArea=�$ 0��$ Planlanacak��$ n0��$ Bakiye��$ n0��$ n0��  -   $	 u_y��$ DataArea=�$ 1��$ Üretimdeki M.��$ n0��$ UretimdeYuruyenMiktar��$ n0��$ n0��  -	   $ t_u_y��$ DataArea=�$ 3��$ 	Toplam PU��$ n0��$# ToplamPlanlanacakVeUretimdeki��$ n0��$ n0��     Width$ 60Q�$ 
FilterArea=�$ 0��$ 	SevkHafta��  $ 
ColumnArea=�$ 1��$ KapasitifDurum��  $ RowArea=�$ 1��$ 	SevkYilAy���� ���� .R��� $ 
Horizontal=�.+�  $��30��$��35��$��Right=�$��Center=�$ Calendar=��� $ 5q�$��Center=�$��18(�$��Haftalık Kapasite��.��  -
   $
 pvt2��$��1��   DataProcessingEngine$ 	Optimized=�$K E[SiparisDurum]=='MÜŞTERİ ONAYLI' And [KullanimAlani] In ('BURKAP')��$		 115��
 �� *Planlanacaklar+   -   $ 
miktar_kg1��$ 
FilterArea=�$ n0��$ MiktarKg��$ n0�� .    -   $ kalite_red_miktar1��$ 
FilterArea=�$ n0��$ KaliteRedMiktar��$ n0�� .    -   $ paketlenen1��$ 
FilterArea=�$ 
Paketlenen��$ n0��$ PaketlenenMiktar��$ n0�� .    -   $ planlan1��$ 
FilterArea=�$ 	Planlanan��$ n0��$ PlanlananMiktar��$ n0�� .    -   $ teslimHafta1��$ 
FilterArea=�$ TeslimHafta�� .    $ 
FilterArea=�$ 1��$ 	MusteriAd��  $ 
FilterArea=�$ 3��$ SiparisDurum��  $ 
FilterArea=�$ 0��$ Planlanacak��$ n0��$ Bakiye��$ n0��$ n0��  $ 
FilterArea=�$ 4��$ KalemKod��$ SiparisKalemKod��  $ 
FilterArea=�$ 5��$ Alasim��  $ 
FilterArea=�$ 2��$ KullanimAlani��$ Default=�  -   $ t_u_y1��$ DataArea=�$ 0��$ Toplam��$ n0��$# ToplamPlanlanacakVeUretimdeki��$ n0��$ n0��  $ 
FilterArea=�$ 6��$ KapasitifDurum��  $ DataArea=�$ 1��$ n0��$ HaftalikTonaj��$ n0��   SummaryType$	 Max=�$ n0��  -   $ kalanKapasite��$ DataArea=�$ 2��$ Kalan Kapasite��$ n0��$ n0��   UnboundExpression$@ :max([HaftalikTonaj])- sum([ToplamPlanlanacakVeUretimdeki])��   UnboundExpressionMode$ UseSummaryValues=�   UnboundFieldName$ fieldFullName��   UnboundType$ Integer=�$ n0��  $ RowArea=�$ 0��$ 	SevkHafta��