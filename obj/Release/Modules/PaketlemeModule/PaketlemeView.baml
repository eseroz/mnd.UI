   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Pandap.UI.ConvertersPandap.UI.Converters  3clr-namespace:Pandap.UI.HelperPandap.UI.Helper  M IResponsiveGrid.Wpf, Version=0.3.3.0, Culture=neutral, PublicKeyToken=nullcDclr-namespace:SourceChord.ResponsiveGrid;assembly=ResponsiveGrid.WpfSourceChord.ResponsiveGrid 5    /Pandap.UI.Modules.PaketlemeModule.PaketlemeView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9
converters"clr-namespace:Pandap.UI.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   j	 fDevExpress.Xpf.LayoutControl.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	 ]
 YDevExpress.Mvvm.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  +helperclr-namespace:Pandap.UI.Helper  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  MrgDclr-namespace:SourceChord.ResponsiveGrid;assembly=ResponsiveGrid.Wpf  $	��600��$	��500��   MaterialDesignPaper#���     MaterialDesignFont#���  $��13(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=� � 	Resources *  $DevExpress.Xpf.Grid.GridColumnHeader �� 
TargetType �� Property �� Value-   'Pandap.UI.Converters.PaketlemeConverter  PaketlemeConverter��%\   '        &	 C     �� "  .���  � �� �	#FFAEEA00��  � ��
 �   �  2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors ,  &DevExpress.Mvvm.UI.NotificationService  -     Name$ AppNotificationService��&   UseWin8NotificationsIfAvailable .  -  'DevExpress.Xpf.Core.DXMessageBoxService  .��A� 	 A� Columns$	 2��
 A� Rows$
 1��.+�#  MaterialDesignThemes.Wpf.Card  $��0��$��0q�$��5q�$��Stretch=�.��)  #MaterialDesignThemes.Wpf.DialogHost  paletDialog��$��Stretch=�$��Stretch=�   IsOpen �� *IsOpenPaletDialog+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   DialogContent �� $	��300��$��16q�   MaterialDesignTextBox �� BasedOn)	  #MaterialDesignThemes.Wpf.HintAssist��%T   '��       7  �� 8   " ��.���  �@	 
IsFloating . ��  � ��$ 8q�.+�(
  "MaterialDesignThemes.Wpf.ColorZone
  .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ PrimaryDark=�.���� $��Palet Ekleme���� $��8q� 	  Hint$ Cari seçiniz�� �� DisplayMemberPath$ CariIsim�� �� SelectedValuePath$ CariKod������  �� Path$ PaleteHazirCariler�$ OneWay=�$ PropertyChanged=� �� SelectedValue �� *TempPalet.CariKod+$ TwoWay=�$ PropertyChanged=��� $��Stretch=�$ Palet Ağırlık��$��16(����� *TempPalet.PaletDara_Kg+$ TwoWay=�$ PropertyChanged=��� $��Stretch=�$ 
Palet Ebat��$��16(����� *TempPalet.PaletEbat+$ TwoWay=�$ PropertyChanged=��� $��Right=� �� Orientation$ 
Horizontal=�.+��� $��0,8,8,0q�$��Cancel��  MaterialDesignFlatButton#��[ ���� *ClosePaletDialogCommand+.��İptal�� $��0,8,8,0q�$��Ok�� " MaterialDesignRaisedDarkButton#��[ ���� *ClosePaletDialogCommand+.��Tamam��� *  $DevExpress.Xpf.Grid.GridBorderHelper   
ShowBorder .  � UseLayoutRounding .  � RowDefinitions �� $��40 ��� $��1* ��� $��5 ��� $��1* �.+��� -   $��paletler�� K� ZIndex$	 100�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$	 0.5Q�   Color$ 	#FF206946��._�]� -    ]�Name$ header��+  %MaterialDesignThemes.Wpf.ShadowAssist   Darken .     ShadowDepth$  Depth3=�
�������.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��32��$��32��$��3q�$��Center=�!   Kind$! ViewGrid=��� $��Center=�$��16(�$
��Bold"�$��	Paletler ���� $��
0,0,20,-20q�$��Right=� 0 ,MaterialDesignFloatingActionMiniAccentButton#��[ ���� *$"DataContext.OpenPaletDialogCommand+" �� RelativeSource" �� # �� AncestorType"# >�.��  $��24��$��24��$��Center=�$
! Plus=��� -   $ ��Name$$ lstPanel��$��1��$	��100����.  ���� *
Paletler+$ TwoWay=�$ PropertyChanged=�% �� SelectedItem% �� *SeciliPalet+$ TwoWay=�$ PropertyChanged=����� " ��.���  � ��$ Stretch=����� .R�  $��5q�$��6q�
���   �$��1q�.���� $��0��$��Stretch=�.+�/  )SourceChord.ResponsiveGrid.ResponsiveGrid  .+��� 	&   XS$& 10��._��� $��4��.+��� $
��Left=�$��16(�$
��Bold"�$
��Wrap=����� *CariKartNav.CariIsim+' �� StringFormat$	' {0}���� $ 
Horizontal=�.+��� ���� *Id+$' Palet No:{0}���� $��10,0,0,0q����� *	PaletEbat+$' Ebat:{0}���� $ 
Horizontal=�.+��� ���� *UretimEmriKod+$' Üretim Emri Kod:{0}���� $�� ���� 
���  ������ *
Aciklama+$ TwoWay=�$ PropertyChanged=�$' Not :{0}���� $ 
Horizontal=�.+��� $ 
Horizontal=�.+��� $
��Bold"����� *PaletNet_Kg+$' Net :{0} kg���� $��10,0,0,0q�$
��Bold"����� *PaletDara_Kg+$' Dara:{0} kg���� $��10,0,0,0q�$
��Bold"����� *PaletBrut_Kg+$' Brüt:{0} kg���� $ 
Horizontal=�.+��� $
��Bold"����� *53UretimEmriKodNav.SiparisKalemKodNav.Kalinlik_micron+$' Kalınlık:{0}���� $��10,0,0,0q�$
��Bold"����� *+)UretimEmriKodNav.SiparisKalemKodNav.En_mm+$' En:{0}���� $��10,0,0,0q�$
��Bold"����� *20UretimEmriKodNav.SiparisKalemKodNav.AlasimTipKod+$
' :{0}���� $��10,0,0,0q�$
��Bold"����� *31UretimEmriKodNav.SiparisKalemKodNav.SertlikTipKod+$
' :{0}���� $��10,0,0,0q�$
��Bold"����� *	Ekleyen+�� $& 2���� $
��0,10q�
�������$��1q�._�&   System.Windows.Controls.DataGrid  $��5q�-  'MaterialDesignThemes.Wpf.DataGridAssist(   CellPadding$( 6q�)   ColumnHeaderPadding$) 4q�*   AutoGenerateColumns* .  +   CanUserAddRows+ .  $��14(�,   	RowHeight$, 40Q����� *
Bobinler+-   Columns- 4  .System.Windows.Controls.DataGridTemplateColumn  .   CellTemplate. �� .R��� $��40��$��20��$��3q�$��0q�$
��Bold"�
���  ��#���  ���� *" DataContext.PaletBobinSilCommand+" �� $# UserControlL���`� / `� 	Converter#/ [ .1��� $ TwoWay=�$ DataContext�$ PropertyChanged=�" �� 0 �� Mode$0 FindAncestor=�$# ListBoxItemL��� $ DataContext�" �� $0 FindAncestor=�$# DataGridRowL�.��"  FontAwesome.WPF.ImageAwesome  $��12��$��Center=�1   
Foreground
1 �  ��2   Icon$2 Minus=�0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn3   Header$3 UretimEmri Kod��1  +System.Windows.Controls.DataGridBoundColumn4   Binding4 �� *UretimEmriKod+  $3 Bobin No��4 �� *	BobinNo+  $3 Bobin Ağırlık��4 �� *
Agirlik_kg+  $3 FH Çıkış No��4 �� *
FH_CikisNo+  .+��� $& 6��._��� $��3q�$
��Left=�$��26(�#��[ 5 � ToolTip$5 	Palet Sil������ *DataContext.PaletSilCommand+" �� "# >�$0 FindAncestor=����� .���� $ 
Horizontal=�.+�  $��18��$��1q�
1 �  ��$2 Remove=��� $��5,0,0,0q�$��16(�$��
Paleti Sil���� $& 6��._��� $��Right=�$ 
Horizontal=�.+��� $��80��$��2q�$��Right=�$��26(�  MaterialDesignRaisedButton#��[ $5 Barkod Yazdır������ *&$DataContext.PaletBarkodYazdirCommand+" �� "# >�$0 FindAncestor=����� .��  $��Center=�$	��Top=�
1 �����$2 Barcode=��� $��3q�$��Depo Onaya Gönder�� $	  MaterialDesignRaisedAccentButton#��[	 ���� *$"DataContext.DepoOnayaGonderCommand+" �� "# >�$0 FindAncestor=��� $��2��$��5��$��Stretch=��� $��3��$��5q�$ Standard=�$  Depth1=�$��2q�$��Kullanılabilir Bobinler�� 
 MaterialDesignGroupBox#��� 
  . ���� .R�]� ��. .+�  $��32��$��32��$��Center=�$! Album=��� $��10,0,0,0q�$��Center=�$��16(�$��Kullanılabilir Bobinler��.��  $( 6q�$) 4q�* .  + .  $��14(����� *!CariyeAitKullanilabilirBobinler+$ TwoWay=�$ PropertyChanged=�% �� *SeciliBobin+$ TwoWay=�$ PropertyChanged=�-   $3 UretimEmri Kod��4 �� *UretimEmriKod+  $3 Bobin No��4 �� *	BobinNo+$ OneWay=�  $3 
Ağırlık��. �� .R��� $ 
Horizontal=�.+�%  DevExpress.Xpf.Editors.TextEdit  $��50��$��30��$	��Top=�{��� * DataContext.AgirlikGirebilirMi+" �� $# UserControlL�)  #DevExpress.Xpf.Editors.TextEditBase6   Text6 �� *
Agirlik_kg+$ TwoWay=�$ 	LostFocus=��� $��30��$��2,0,0,2q�$��5q�$��Kantardan Al��$
��Bold"�
����������� *,*DataContext.BobinAgirlikKantardanAlCommand+" �� $# UserControlL�  $3 
FH_CikisNo��. �� .R�  $	��100��$��30��$	��Top=�6 �� *
FH_CikisNo+$ TwoWay=�$ 	LostFocus=�  . �� .R��� $��32��$��32��#��[ $5 Palete Ekle������ *&$DataContext.BobiniPaleteAktarCommand+" �� "# >�$0 FindAncestor=����� *SelectedItem+" �� *FindAncestor+"#  .��  $��24��$��24��$! PlusBox=��� $��2��$��5q�$  Depth3=�$��1q�$��Depo Onay Bekleyenler��#��� 
  . ���� .R�]� ��. .+�  $��32��$��32��$��Center=�$! 	OpenInApp=��� $��Center=�$��16(�$��Depo Onay Bekleyenler������ -   $$ cboDepoOnayiBekleyenler��$��0��$	��100��$7 �� IsSynchronizedWithCurrentItem$
7 TrueY����� *DepoOnayBekleyenPaletler+% �� *DepoSeciliPalet+$ TwoWay=�$ PropertyChanged=����� " ��.���  � ��$ Stretch=����� .R��� 8 �� Padding$8 6q�
���   �$��1q�9 �� CornerRadius$9 5��._�� : � ColumnDefinitions: �� $��* ��� $
��auto �.+��� $��0��$��Stretch=�.+��� $
��Bold"����� *UretimEmriKod+$' Üretim Emri Kod:{0}���� $ 
Horizontal=�.+��� ���� *Id+$' Palet No:{0}���� ���� *DepoyaAktarilmaTarihi+$-' 'DepoyaAktarılmaT.:{0:dd/MM/yyyy HH:mm}���� $��10,0,0,0q����� *	PaletEbat+$' Ebat:{0}���� $ 
Horizontal=�.+��� $
��Bold"����� *PaletNet_Kg+$' 
Net Kg:{0}���� $��10,0,0,0q����� *PaletDara_Kg+$' Dara:{0}���� $��10,0,0,0q����� *PaletBrut_Kg+$' Brüt-Ağırlık:{0}���� 
���  ������ *PaketlemeNot+$' Paket Not:{0}���� 
���  ������ *
Aciklama+$' Paket Not:{0}���� $ 
Horizontal=�.+��� $
��Bold"����� *53UretimEmriKodNav.SiparisKalemKodNav.Kalinlik_micron+$' Kalınlık:{0}���� $��10,0,0,0q�$
��Bold"����� *+)UretimEmriKodNav.SiparisKalemKodNav.En_mm+$' En:{0}���� $��10,0,0,0q�$
��Bold"����� *20UretimEmriKodNav.SiparisKalemKodNav.AlasimTipKod+$
' :{0}���� $��10,0,0,0q�$
��Bold"����� *31UretimEmriKodNav.SiparisKalemKodNav.SertlikTipKod+$
' :{0}���� $
��Left=�$
��Bold"�$
��Wrap=����� *CariKartNav.CariIsim+$' Cari:{0}���� $��1��.+��� $��80��$��4q�$��26(�#��[ $5 Barkod Yazdır������ *&$DataContext.PaletBarkodYazdirCommand+" �� $# UserControlL�$0 FindAncestor=����� .��  $��Center=�$	��Top=�
1 �����$2 Barcode=��� $��40��$��4q�$��26(�#��[ $5 	Palet Sil������ *)'DataContext.DepoOnayindanbGeriAlCommand+" �� $# UserControlL�$0 FindAncestor=����� .��  $��1q�$��Center=�$	��Top=�
1 �����$2 Remove=�