   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  i9clr-namespace:Pandap.UI.Modules.PlanlamaModule.DokumBolum+Pandap.UI.Modules.PlanlamaModule.DokumBolum  ?    9Pandap.UI.Modules.PlanlamaModule.DokumBolum.BobinEkleForm   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9
converters"clr-namespace:Pandap.UI.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  Elocal9clr-namespace:Pandap.UI.Modules.PlanlamaModule.DokumBolum  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006    � Title$  Döküm Planı Ekle��$	��460��$��475.9��$��16(� � WindowStartupLocation$ CenterScreen=� � 	Resources .   (Pandap.UI.Converters.AlasimRenkConverter   alasimRenkConverter2   ,Pandap.UI.Converters.AlasimFontRenkConverter  alasimFontRenkConverter��%   &	        &	          .���� $��10q�.+��� $��0��$��10q�$	��Top=�.+�]� .+��� $	��200��$��Right=�$��Center=�$��Tarih :���� $	��200��
����������� *DokumKafileEdit.PlanTarihi+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� StringFormat$ 
dd.MM.yyyy��]� $��0,15,0,0q�.+��� 
���  ��$	��200��$��Center=�$��
Alaşım :���� $	��200�� �� DisplayMemberPath$ Aciklama�� �� SelectedValuePath$ 	AlasimKod������ *AlasimTipleri+ �� SelectedItem �� *SeciliAlasim+	 �� SelectedValue	 �� *DokumKafileEdit.AlasimTipKod+$ TwoWay=�$ PropertyChanged=�]� $��0,15,0,0q�.+��� $	��200��
���  ��$��Center=�$��Kondisyon :���� $	��200��
����������� *DokumKafileEdit.Kondisyon+$ TwoWay=�$ PropertyChanged=�]� .+��� $	��200��
���  ��$��Center=�$!��SH Nihai Kalınlık (µm) :���� $	��200��
����������� *'%DokumKafileEdit.ShReceteNihaiKalinlik+$ TwoWay=�$ PropertyChanged=�]� .+��� $	��200��$��Center=�$��Kalınlık :���� $	��200��
����������� *DokumKafileEdit.PlanKalinlik+$ TwoWay=�$ PropertyChanged=�]� .+��� $	��200��$��Center=�$
��En :���� $	��200��
����������� *DokumKafileEdit.PlanEn+$ TwoWay=�$ PropertyChanged=�]� .+��� $	��200��$��Center=�$��Bobin Adedi :���� $	��200��
����������� *DokumKafileEdit.BobinAdet+$ TwoWay=�$ PropertyChanged=�]� .+��� $	��200��$��Center=�$��Plan Miktar (kg) :���� $	��200��
�������$
��Bold"�
���  ������ *DokumKafileEdit.PlanMiktar+$ TwoWay=�$ PropertyChanged=��� $��	50,30,0,0q�$��Right=�
 �� Orientation$
 
Horizontal=�.+��� -     MaterialDesignFlatButton#��[ .��İptal�� -   $��10,0,0,0q�.��Tamam