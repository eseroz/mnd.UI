   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Pandap.UI.ControlHelpersPandap.UI.ControlHelpers  q=clr-namespace:Pandap.UI.Modules.UretimOperatorEkranlariModule/Pandap.UI.Modules.UretimOperatorEkranlariModule  C    =Pandap.UI.Modules.UretimOperatorEkranlariModule.BobinItemView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     AcontrolHelpers&clr-namespace:Pandap.UI.ControlHelpers     @d2http://schemas.microsoft.com/expression/blend/2008     ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  Ilocal=clr-namespace:Pandap.UI.Modules.UretimOperatorEkranlariModule  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  $	��330��$��323.925��  � 	Resources  M   GPandap.UI.Modules.UretimOperatorEkranlariModule.AktifBobinRenkConverter   aktifBobinRenkConverter��%   &	          .����  �� Padding$ 12q�+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth4=�   ShadowEdges$	 All=�
����+��$��2q� �� CornerRadius$ 6������ *IslemGorenBobinMi+ �� 	Converter# [  ._��� $	��Top=�.+�]� .+��� $��Center=�$��18(�
���   ����� *
IslemDurum+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��0q�$��16��$
��Left=�$��26(�  MaterialDesignFlatButton#��[  � ToolTip$ Barkod Yazdır������ *!DataContext.BarkodYazdirCommand+	 �� RelativeSource	 �� 
 �� AncestorType$
 UserControlL� �� Mode$ FindAncestor=����� *RowData.Row+�� $��15(�$��0q�$��0q�$��20��$��20��$��Right=�
������ 
���  ��$��X������ *BobinIslemIptalCommand+]� $	��0,4q�.+��� $��Center=�$��20(�$��Bobin No : ���� $��Right=�$��Courier New)�$��28(�$��DemiBold"�
���  ������ *Bobin.PlanBobinNo+�� $��0q�$��Right=�$��50������ *BarkodYazdirCommand+��Z� .���� $��Center=�.+�"  FontAwesome.WPF.ImageAwesome  $��20��$��Center=�$	��Top=�   
Foreground
 �   �   Icon$ Barcode=���  �� Orientation$ 
Horizontal=�.+��� $	��100��.+��� $��25��$��0q�$��Stretch=�$��Center=�$��Center=�$��Center=�
�������
�������$��1q�$��Alaşım��$��16(�%  DevExpress.Xpf.Editors.TextEdit  $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�   CharacterCasing$ Upper=�$��22(�$��DemiBold"�%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *Bobin.AlasimTipKod+$ PropertyChanged=��� $	��100��.+��� $��25��$��0q�$��Stretch=�$��Center=�$��Center=�$��Center=�
�������
�������$��1q�$��
Kalınlık��$��16(�  $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�$ Upper=�$��24(�$��DemiBold"� �� *Bobin.PlanKalinlik+$ PropertyChanged=��� $	��100��.+��� $��25��$��0q�$��Stretch=�$��Center=�$��Center=�$��Center=�
�������
�������$��1q�$��En��$��16(�  $��36��$��Stretch=�$��Center=�
�������
�������$��1,0,1,1q�$ Upper=�$��24(�$��DemiBold"� �� *Bobin.PlanEn+$ PropertyChanged=�]� $
��0,10q�$��Center=�.+��� $	��100��.+��� $��Center=�$��16(�$��Başlama���� $��Center=�$��Center=�.+��� $��26(�$
��Bold"�
��� � � �� TextAlignment$ Center=����� *IslemBaslamaTarihi+ �� StringFormat$ HH:mm���� $��12(�$ Center=����� *IslemBaslamaTarihi+$ 	dd/MM ddd���� $	��100��$��Center=�.+��� $��Center=�$��16(�$��Bitiş���� $��Center=�$��Center=�.+��� $��26(�$
��Bold"�
��� � �$ Center=����� *IslemBitisTarihi+$ HH:mm���� $��12(�$ Center=����� *IslemBitisTarihi+$ 	dd/MM ddd���� $	��100��$��Center=�.+��� $��Center=�$��16(�$��Süre (h:m:s)���� $��Center=�$��Center=�.+��� $��24(�$
��Bold"�
��� � �$ Center=����� *GecenSureStr+� $��8,0,0,0q�$��Center=�$��Center=� � Orientation$ 
Horizontal=�{��� *IslemGorenBobinMi+.+��� 3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   IsIndeterminate .    IsIndicatorVisible .    Value$ -1Q� & "MaterialDesignFloatingActionButton#��[ 0   *Pandap.UI.ControlHelpers.VisibilityHelpler   	IsVisible �� *CalisiyorMu+ �� TargetNullValue$
 true����0  *MaterialDesignThemes.Wpf.PackIconExtension  *DotsHorizontal+�� $	��130��$��50��$��Center=�$��24(� �� *	IsEnabled+	 �� *Self+���� * DataContext.IslemBaslatCommand+	 �� $
 UserControlL�.���� $ 
Horizontal=�.+�'	  !MaterialDesignThemes.Wpf.PackIcon	  $��30��$��30��$
��Left=�$��Center=� 	  Kind$
 Play=��� $��Başlat���� $��70��$��50��$��30,0,0,0q�$��Center=�.
  (MaterialDesignThemes.Wpf.ColorZoneAssist 
  Mode$ PrimaryLight=�
���  ��$��20(�
������� $  MaterialDesignRaisedAccentButton#��[  �� *	IsEnabled+	 �� *Self+���� * DataContext.IslemDurdurCommand+	 �� $
 UserControlL�.���� $ 
Horizontal=�.+�	  $��30��$��30��$
��Left=�$��Center=�$ Pause=��� �� $��70��$��50��$��Center=�$��24(� �� *	IsEnabled+	 �� *Self+���� *!DataContext.IslemDevamEtCommand+	 �� $
 UserControlL�.���� $ 
Horizontal=�.+�	  $��30��$��30��$
��Left=�$��Center=�$ StepForward=��� $	��120��$��50��$��10,0,0,0q�$��Center=�$ PrimaryLight=�$��20(�#��[  �� *	IsEnabled+	 �� *Self+���� *DataContext.IslemBitirCommand+	 �� $
 UserControlL�.���� $ 
Horizontal=�.+�	  $��30��$��30��$
��Left=�$��Center=�$
 Stop=��� $��Bitir���� $	��120��$��50��$��10,0,0,0q�$��Center=�$ PrimaryLight=�$��20(� �� *	IsEnabled+	 �� *Self+���� *DataContext.BobinCikarCommand+	 �� $
 UserControlL�.���� $ 
Horizontal=�.+�	  $��30��$��30��$
��Left=�$��Center=�$! ArrowRightBoldCircleOutline=��� $��Çıkar���� $��0q�$��26(�#��[ $ Barkod Yazdır������ *!DataContext.BarkodYazdirCommand+	 �� $
 UserControlL�$ FindAncestor=����� *RowData.Row+