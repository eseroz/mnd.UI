   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Pandap.UI.Modules.TeklifModulePandap.UI.Modules.TeklifModule  Q-clr-namespace:Pandap.UI.GyModules.MesajModulePandap.UI.GyModules.MesajModule  4    .Pandap.UI.Modules.TeklifModule.TeklifKalemView   j fDevExpress.Xpf.LayoutControl.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:Pandap.UI.Modules.TeklifModule     U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  ] YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  f	 bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa
 ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 	 
 AmesajModule-clr-namespace:Pandap.UI.GyModules.MesajModule 	 
   � WindowStartupLocation$  CenterScreen=�   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  $��481.87��$��635.162��2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors -  'DevExpress.Mvvm.UI.CurrentWindowService  '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *FormLoadedCommand+-  'DevExpress.Xpf.Core.DXMessageBoxService  .��]� .+�c� $	��Top=�$��50��}�.    MaterialDesignToolBar#���  .@���   MaterialDesignFlatButton#���  ���� *KaydetCommand+.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ ContentSave=��� $��8 0 0 0q�$��Center=�$��Tamam���� #���  ���� *IptalCommand+.���� $ 
Horizontal=�.+�  $��Center=�$ Delete=��� $��8 0 0 0q�$��Center=�$��İptal���� $ 
Horizontal=�$��	130,0,0,0q�.+��� $��Normal"�$	��120��$��Center=�$��14(�$��Teklif Kalem Kod : ���� $
��0,13q�$��DemiBold"�$��0q�$	��140��$
��Left=�$��14(���Z� ���� *&$SeciliTeklifKalem.TeklifKalemSiraKod+ �� UpdateSourceTrigger$ PropertyChanged=�+	  %DevExpress.Xpf.LayoutControl.GroupBox	  ,
  &DevExpress.Xpf.Core.ContentControlBase	 
  Content.	 � 
 � RowDefinitions
 �� $
��auto ���  � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       �� " ��.���  � ��$	 125����  � ��$
 Bold"���  � ��$ 0,0,10,0q���  �@��TextAlignment$ Right=�.+��� $��1��  �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%\  '��       �� " ��.���  � �� �� " ��.R��� $��Border��$��1,1,1,0q�
���  ��$ 4,4,0,0��$	��2,0q�._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,2q� P�  �@��
IsSelected . . ��� $ Border�� � �� �	#FFDEFF00P�  �@��
IsSelected .  . ��� $ Border�� � ��
 �����.@��� $��Teslim Dönem������ .R��� $ 
Horizontal=�.+�  $��24��$��Center=�$��25��$��Right=�$ CalendarRange=��� $��Center=� �� Padding$ 5q�$��14(�$��Teslim Dönem��.���� $��8q�.+�]� $��5q�.+��� $	��100��$��Center=�$��Teslim Yıl���� $	��100��$
��Left=�
����������� *SeciliTeklifKalem.TeslimYil+]� $��5q�.+��� $��Dönem Grup :��$��Center=�$	��100���� 
������� �� DisplayMemberPath$ DonemGrupAdi�� �� SelectedValuePath$ DonemGrupAdi������ *DonemGrupListesi+ �� SelectedValue �� *SeciliTeklifKalem.DonemGrup+]� $��5q�.+��� $��Dönem :��$��Center=�$	��100���� 
�������$ DonemAdi��$ DonemAdi������ *DonemListesi+ �� Mode$ TwoWay=�$ PropertyChanged=� �� *SeciliTeklifKalem.Donem+�� $��Ürün Seçim������ .R��� $ 
Horizontal=�.+�  $��30��$��Center=�$��24��$��Right=�$ Album=��� $��Center=�$ 5q�$��14(�$��Ürün Seçim��.���� $��8q�.+��� .+��� $ 
Horizontal=�.+�]� -   $��lookupUrunKod��$
��Left=�$	��300��$��0,0,0,5q�.+��� $��Center=�$
��Bold"�$��Ürün Kod:��$	��130��+ 
 %DevExpress.Xpf.Grid.LookUp.LookUpEdit  $��25��   AutoPopulateColumns .  +  %DevExpress.Xpf.Editors.LookUpEditBase   DisplayMember$ UrunKod��   ValueMember$ UrunKod��$��12(�*  $DevExpress.Xpf.Editors.PopupBaseEdit   PopupMinWidth$	 500Q�   PopupMinHeight$	 400Q�%  DevExpress.Xpf.Editors.BaseEdit    	EditValue  �� *SeciliTeklifKalem.UrunKod+$ TwoWay=�$ PropertyChanged=�"! �� ValidatesOnNotifyDataErrors! . "   SelectedItem" �� *
SeciliUrun+$ TwoWay=�$ PropertyChanged=�#   ItemsSource# �� *	Urunler+$   PopupContentTemplate$ �� .R�� 
 �� $��* ��� $
��auto �.+�% 
 DevExpress.Xpf.Grid.GridControl  %  Name$% PART_GridControl��$��0��$	��500�� $ 
 DevExpress.Xpf.Grid.GridColumn$ 	 DevExpress.Xpf.Grid.ColumnBase��%o   '        �� "  .���  �@ FilterPopupMode$ Excel=���  �@ AutoFilterCriteria$ ContainsX�&   View& # 
 DevExpress.Xpf.Grid.TableView  '   ShowAutoFilterRow' . (   	AutoWidth( . & 	  DevExpress.Xpf.Grid.DataViewBase)   ColumnHeaderTemplate) �� .R��� $��30��$��Center=�* �� TextAlignment$* Center=�$
��Wrap=����� * ++   Columns.+   ,   ColumnFilterMode$, DisplayText=�$ 	 DevExpress.Xpf.Grid.BaseColumn-   Header)  #DevExpress.Xpf.Core.ObjectConverter$- Ürün Kodu .   Width2 	 ,DevExpress.Xpf.Grid.GridColumnWidthConverter$. 60 /   	FieldName$/ UrunKod��  $- Ürün Adı $/ 	UrunAdiEN��  $- Ağırlık(gr.) $. 50 $/ GR���� $y�Hidden=�$* Right=�$��Center=�$
��Bold"�$��Teslim Tarihi���� $y�Hidden=�.+�%  DevExpress.Xpf.Editors.DateEdit  $��28��$
��Left=�
�������%  DevExpress.Xpf.Editors.TextEdit0   Mask$0 dd/MM/yyyy HH:mm��1   MaskUseAsDisplayFormat1 . 2   MaskCulture$2 tr-TR��  �� * SeciliTeklifKalem.TeslimTarihi+$ TwoWay=�$ PropertyChanged=�]� $	��550��$
��Left=�$��5q�.+��� $��Center=�$
��Bold"�$��Ürün Adı:��$	��130���� 3 �� 
IsReadOnly3 . ���� *SeciliUrun.UrunAdiEN+$ TwoWay=�$ PropertyChanged=�]� $	��550��$
��Left=�$��5q�.+��� $��Center=�$
��Bold"�$�� ��$	��130���� $ 
Horizontal=�.+��� .+��� $��Center=�$* Center=�$
��Bold"�$	��GR.��$��60���� $��0,0,5,0q�3 . $��Center=����� *SeciliUrun.GR+$ TwoWay=�$ PropertyChanged=��� .+��� $��Center=�$* Center=�$
��Bold"�$	��PCS��$��60���� $��0,0,5,0q�3 . $��Center=����� *SeciliUrun.PCS+$ TwoWay=�$ PropertyChanged=��� .+��� $��Center=�$* Center=�$
��Bold"�$
��BOX.��$��60���� $��0,0,5,0q�3 . $��Center=����� *SeciliUrun.BOX+$ TwoWay=�$ PropertyChanged=��� .+��� $��Center=�$* Center=�$
��Bold"�$	��M3.��$��80���� $��0,0,5,0q�3 . $��Center=����� *SeciliUrun.M3+$ TwoWay=�$ PropertyChanged=��� $ 
Horizontal=�.+��� .+��� $��Center=�$* Center=�$
��Bold"�$��	Dip Fiyat��$	��130���� $��0,0,5,0q�3 . $��Center=�$��Center=����� *SeciliUrun.BirimFiyat+$ TwoWay=�$ PropertyChanged=�4 �� StringFormat$4 N2��]� $	��700��$��10��$
��Left=�$��5q�]� -   5 ]�Name$5 naklye��.+��� $	��130��$��Nakliye (Freight) :��$��Center=�$
��Bold"��� $	��200��$ NakliyeDurumTipAdi��$ NakliyeDurumTip������ *NakliyeDurumTipleri+ �� *#!SeciliTeklifKalem.NakliyeDurumTip+�� -   $��nakliyedetay��$��10,0,0,0q�$ 
Horizontal=�
������ �� $ Vertical=�$��0,0,0,0q�.+��� $ 
Horizontal=�.+��� $��Center=�$* Right=�$
��Bold"�$��Birim Fiyatı :��$	��130���� $	��200��$
��Left=�$
��Bold"����� *SeciliTeklifKalem.SatisFiyati+$ TwoWay=�$ PropertyChanged=�$4 N2���� $ 
Horizontal=�$	��0,5q�.+��� $��Center=�$* Right=�$
��Bold"�$��Miktar(Koli) :��$	��130���� $	��200��$
��Bold"�$
��Left=����� *SeciliTeklifKalem.Miktar+$ TwoWay=�$ PropertyChanged=��� $ 
Horizontal=�$	��0,5q�.+��� $��Center=�$* Right=�$
��Bold"�$��Tutar :��$	��130���� $	��200��$
��Bold"�$��Center=�$
��Left=����� *SeciliTeklifKalem.Tutar+$ TwoWay=�$ PropertyChanged=�$4 N2��