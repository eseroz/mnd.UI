   M S B A M L   `   `   `  ���� A  =Pandap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:Pandap.UI.GyModules.MesajModule.Converters*Pandap.UI.GyModules.MesajModule.Converters  Q-clr-namespace:Pandap.UI.GyModules.MesajModulePandap.UI.GyModules.MesajModule  6    0Pandap.UI.GyModules.MesajModule.MesajlasmaWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  :d2http://schemas.microsoft.com/expression/blend/2008  R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  I
converters8clr-namespace:Pandap.UI.GyModules.MesajModule.Converters  ?mesajModule-clr-namespace:Pandap.UI.GyModules.MesajModule  -     �Name$  MesajWindow�� � ShowInTaskbar .  $��442.925��$��557.477�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanResizeWithGrip=� � 	Resources E   ?Pandap.UI.GyModules.MesajModule.Converters.Base64ImageConverter   MyImageConverterS   MPandap.UI.GyModules.MesajModule.Converters.HorizontalToTextAlignmentConverter  H2TAlignmentConverterM   GPandap.UI.GyModules.MesajModule.Converters.ListboxControlColorConverter  ColorControlConverterR   LPandap.UI.GyModules.MesajModule.Converters.MyBoolToVisibilityInvertConverter  MyVisibilityConverter  placeHolder �� 
TargetType �� BasedOn �� Property �� Value	 �� 
TargetType
 ��Name K� ZIndex �� Mode �� RelativeSource �� UpdateSourceTrigger
 � Tag �� Triggers �� Binding �� Source%  System.Windows.Markup.Reference �� Value  MyLinkButton �� Orientation ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	      &	 
     &	      &	      7��&	 d            �� " ��8   .���  � �� �� "	 ��.R�� .+��� $
 
textSource��
������ $ 2������ *Text+$ TwoWay=� �� *TemplatedParent+$ PropertyChanged=��� #��z #��z��$ 1��.������ " ��.���  � ��
 ����  ��  �� *Text+   *
textSource+  .a���  � ��
 �����.���� " ��.���  � ��
 ��  ���  � �� �� "	 ��.R��� $ 
Horizontal=�.+��� $ 	TextBlock��$
��None{�$��Center=����	#FF2B7AD2��  P�  �@J�IsMouseOver . . ��� $ 	TextBlock�� � ��$ 	Underline{���  �@�Cursor$
 Hand����  � ��
 �""��.��� -    J� 	AllowDrop .  � RowDefinitions �� $
��auto ��� �� $
��78.4 �.+�'  !MaterialDesignThemes.Wpf.Snackbar  -     Name$ Snackbar��$
 1000��$��Stretch=�$��0��$	��Top=�$��50��   MessageQueue �� *BoundMessageQueue+]� 
�������$��
0,0,-0.2,0q�.+��� $��4,0,0,0q�$��SemiBold"�$!��Exporta isim ve tarihi ekle�� d� 	IsChecked �� *MesajIsimBilgiSec+�� $��Right=�  MaterialDesignFlatButton#���  ���� *WordExportCommand+.���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�   Kind$ FileWord=��� $��DemiBold"�$��4 0 0 0q�$��Center=�   MaterialDesignBody1TextBlock#��[ $��Export���� -     ��Name$
  lst1��$��1��5   /Pandap.UI.GyModules.MesajModule.ListBoxBehavior!   ScrollOnNewItem! . ��.  $��0,0,0,20q�$��Stretch=�$��0,0,-0.2,6.6q����� *
Mesajlar+$ TwoWay=�$ PropertyChanged=�" �� SelectedItem" �� *SeciliMesaj+$ TwoWay=�$ PropertyChanged=����� .R��� $��1,1,1,1q�# �� CornerRadius$# 4,4,4,4������ *	GidenMi+$ �� 	Converter#$ [ ���� *	GidenMi+#$ [ % J� Effect% 3	  -System.Windows.Media.Effects.DropShadowEffect	  & 	  ShadowDepth$& 1Q�' 	  Color$' 	#FF206946��._��� $ Vertical=�.+��� $ 
Horizontal=�.+���  �� *
SeciliMi+$ PropertyChanged=��� $
��Bold"�$	��250��$
��Wrap=�( �� Padding$
( 20,3q����� *
Gonderen+�� $	��250��$
��Wrap=�$
( 20,5q����� *MesajIcerik+�� $	��250��$
��auto��) � Cursor$
) Hand������ *DokumanIcerik+#$ [  * J� InputBindings* d� + d� Gesture$+ 	LeftClicka�, �� Command, �� *DataContext.DosyaAcCommand+- �� ElementName$- MesajWindow��. �� CommandParameter. �� �� #��[ $	��250��$
) Hand��$��Center=�$
��20,5q�7
   1Pandap.UI.GyModules.MesajModule.VisibilityHelpler/ 
  	IsVisible/ �� *	EkVarMi+���� *
DokumanAdi+���� *DataContext.DosyaAcCommand+ �� 0 �� AncestorType"0 ����� �� $	��250��$
��Wrap=�$
( 20,5q����� *MesajTarihi+1 �� StringFormat$1 dd/MM/yyyy HH:mm:ss��� $��8,4.4,7.8,8q�$��2�����	#FFF5F1EE.+��� -   $
 txtMetin��)  #MaterialDesignThemes.Wpf.HintAssist2   Hint$2 Bir mesaj yazın��
�������.  (MaterialDesignThemes.Wpf.TextFieldAssist3   HasOutlinedTextField3 .   MaterialDesignTextBox#��[ $	��Top=�4 �� TextWrapping$
4 Wrap=�$��0,0,30,0q�$��14(����� *AktifMesaj.MesajIcerik+$ TwoWay=�$ PropertyChanged=�.��5 J� CommandBindings5 )  #System.Windows.Input.CommandBinding  -   6   Command$6 ApplicationCommands.Paste��.��* �� 
7 �� Key$7 Return��, �� *SendMessageCommand+.���� $��17q�$��Right=�$��Right=�8 J� RenderTransformOrigin$8 1.716,0.452&�$��0,5,11.6,4.6q�$��Right=�$��24������ *DosyaEkleCommand+���� .R��� ��*  $DevExpress.Xpf.Core.DXImageExtension  9   Image1  +DevExpress.Xpf.Core.Native.DXImageConverter$9 Attachment_32x32.png 