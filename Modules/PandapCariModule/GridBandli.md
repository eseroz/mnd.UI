<UserControl
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:dxg="http://schemas.devexpress.com/winfx/2008/xaml/grid"
    xmlns:dxmvvm="http://schemas.devexpress.com/winfx/2008/xaml/mvvm"
    xmlns:helper="clr-namespace:mnd.UI.Helper"
     xmlns:controlhelpers="clr-namespace:Gok.UI.ControlHelpers"
    xmlns:local="clr-namespace:mnd.UI.AppModule._PandapCari"
    xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:dxe="http://schemas.devexpress.com/winfx/2008/xaml/editors"
    xmlns:dxgt="http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys"
    xmlns:dx="http://schemas.devexpress.com/winfx/2008/xaml/core"
    xmlns:converters="clr-namespace:mnd.UI.Converters"
    x:Class="mnd.UI.AppModule._PandapCari.PandapCariListView"
    mc:Ignorable="d"
     
    d:DataContext="{d:DesignInstance IsDesignTimeCreatable=False, Type={x:Type local:PandapCariListViewModel}}"
    d:DesignHeight="556.497"
    d:DesignWidth="1105.917">



    <UserControl.Resources>
        <converters:ImageFromAssemblyConverter  x:Key="ImageFromAssemblyCnv" />


        <Style x:Key="grayCell" TargetType="{x:Type dxg:LightweightCellEditor}"
                        BasedOn="{StaticResource {dxgt:GridRowThemeKey ResourceKey=LightweightCellStyle}}">
            <Setter Property="Background" Value="WhiteSmoke" />
        </Style>

        <Style x:Key="grayCell1" TargetType="{x:Type dxg:LightweightCellEditor}"
                        BasedOn="{StaticResource {dxgt:GridRowThemeKey ResourceKey=LightweightCellStyle}}">
            <Setter Property="Background" Value="LightYellow" />
        </Style>

        <Style x:Key="lightGreenCell" TargetType="{x:Type dxg:LightweightCellEditor}"
                        BasedOn="{StaticResource {dxgt:GridRowThemeKey ResourceKey=LightweightCellStyle}}">
            <Setter Property="Background" Value="WhiteSmoke" />
        </Style>


        <Style x:Key="grayColumnHeader" TargetType="{x:Type dxg:BaseGridHeader}">
            <Setter Property="Background">
                <Setter.Value>
                    <LinearGradientBrush EndPoint="0.5,1" StartPoint="0.5,0">
                        <GradientStop Color="Black" Offset="0" />
                        <GradientStop Color="#FF5266BA" Offset="1" />
                        <GradientStop Color="#FE4868C7" Offset="0.137" />
                    </LinearGradientBrush>
                </Setter.Value>
            </Setter>
            <Setter Property="Foreground" Value="White" />
        </Style>

    </UserControl.Resources>

    <dxmvvm:Interaction.Behaviors>
        <dxmvvm:EventToCommand  EventName="Loaded" Command="{Binding FormLoadedCommand}" />
        <helper:ExportService View="{x:Reference View1}" />
    </dxmvvm:Interaction.Behaviors>


    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>



        <ToolBarTray Grid.Row="0">
            <ToolBar MinHeight="50" ClipToBounds="False" Style="{DynamicResource MaterialDesignToolBar}">

                <Separator />

                <Button Command="{Binding KaydetCommand}" Style="{DynamicResource MaterialDesignFlatButton}">
                    <StackPanel Orientation="Horizontal">
                        <materialDesign:PackIcon VerticalAlignment="Center" Kind="ContentSave" />
                        <TextBlock Margin="8 0 0 0" VerticalAlignment="Center" Text="Kaydet" />
                    </StackPanel>
                </Button>

                <Button  Padding="0" Command="{Binding EkranYenileCommand}" Style="{DynamicResource MaterialDesignFlatButton}">
                    <StackPanel Orientation="Horizontal">
                        <materialDesign:PackIcon VerticalAlignment="Center" Kind="Refresh" />
                        <TextBlock Margin="4 0 0 0" VerticalAlignment="Center" Text="Yenile" />
                    </StackPanel>
                </Button>

                <Button Command="{Binding ExcelExportCommand}" CommandParameter="XLSX" Style="{DynamicResource MaterialDesignFlatButton}">
                    <StackPanel Orientation="Horizontal">
                        <materialDesign:PackIcon VerticalAlignment="Center" Kind="FileExcel" />
                        <TextBlock Margin="8 0 0 0" VerticalAlignment="Center" Style="{StaticResource MaterialDesignBody1TextBlock}" Text="Excele Aktar" />
                    </StackPanel>
                </Button>

                <Button Command="{Binding YerlesimKaydetCommand}" Style="{DynamicResource MaterialDesignFlatButton}">
                    <StackPanel Orientation="Horizontal">
                        <materialDesign:PackIcon VerticalAlignment="Center" Kind="Barcode" />
                        <TextBlock Margin="8 0 0 0" VerticalAlignment="Center" Text="Yerleşim Kaydet" />
                    </StackPanel>
                </Button>

                <StackPanel Margin="100,0,0,0" Orientation="Horizontal">
                    <TextBlock FontWeight="DemiBold" VerticalAlignment="Center" Margin="0,0,10,0" Text="Risk Hesap Şekli"/>
                    <dxe:ComboBoxEdit FontWeight="DemiBold" IsTextEditable="False" Height="25" Width="200" ItemsSource="{Binding RiskTakipYerListe}" 
                                  EditValue="{Binding RiskTakipYer,Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}"
                     />


                    <TextBlock Margin="10,0,0,0" Foreground="Red" VerticalAlignment="Center" FontWeight="DemiBold" Text="{Binding FormUyariMesaj}"/>
                </StackPanel>







            </ToolBar>
        </ToolBarTray>

        <materialDesign:Card
            Grid.Row="1"
            Margin="5"
            VerticalAlignment="Stretch"
            materialDesign:ShadowAssist.ShadowDepth="Depth1"
            Padding="5">
            <dxg:GridControl SelectionMode="Row"
                SelectedItem="{Binding SeciliPandapCari,UpdateSourceTrigger=PropertyChanged}"
                ItemsSource="{Binding PandapCariler, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}">

                <dxg:GridControl.TotalSummary>
                    <dxg:GridSummaryItem FieldName="EximBankLimit" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="GarantiFactoringLimit" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="IngFactoringLimit" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="BankaTeminati" DisplayFormat=" {0:n0}"  SummaryType="Sum" />

                    <dxg:GridSummaryItem FieldName="YoneticiLimit" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="AcikHesap" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="NetsisMusteriCekRiski" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="NetsisKendiCekRiski" DisplayFormat=" {0:n0}"  SummaryType="Sum" />

                    <dxg:GridSummaryItem FieldName="PandapSiparisRiski" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="ToplamLimit" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="ToplamRisk" DisplayFormat=" {0:n0}"  SummaryType="Sum" />
                    <dxg:GridSummaryItem FieldName="KullanilabilirLimit" DisplayFormat=" {0:n0}"  SummaryType="Sum" />

                    <dxg:GridSummaryItem FieldName="MusteriDepoRiski" DisplayFormat=" {0:n0}"  SummaryType="Sum" />

                </dxg:GridControl.TotalSummary>

                <dxg:GridControl.Resources>
                    <Style TargetType="dxg:GridColumn">
                        <Setter Property="ReadOnly" Value="False"/>
                    </Style>
                </dxg:GridControl.Resources>


                <dxg:GridControl.View>
                    <dxg:TableView AutoWidth="True" x:Name="View1" AllowPerPixelScrolling="True"  NewItemRowPosition="Bottom"
                                  ShowGroupPanel="False" ShowTotalSummary="True">
                        <dxmvvm:Interaction.Behaviors>
                            <controlhelpers:ItemRowBehavior NewItemRowUpdated="{Binding NewItemAddedCommand}" />
                        </dxmvvm:Interaction.Behaviors>

                        <dxg:TableView.ColumnHeaderTemplate>
                            <DataTemplate>
                                <TextBlock Height="35" HorizontalAlignment="Center"
                                                               TextAlignment="Center" TextWrapping="Wrap" Text="{Binding ''}"
                                                               Margin="0,-4" />
                            </DataTemplate>
                        </dxg:TableView.ColumnHeaderTemplate>

                        <dxg:TableView.FormatConditions>
                            <dxg:FormatCondition Expression="[KullanilabilirLimit] &lt; 0" FieldName="KullanilabilirLimit">
                                <dx:Format Foreground="Red" TextDecorations="{x:Null}"/>
                            </dxg:FormatCondition>
                            <dxg:FormatCondition Expression="[HataMetinUzunluk] &gt;0" FieldName="CariIsim">
                                <dx:Format Foreground="Red" TextDecorations="{x:Null}"/>
                            </dxg:FormatCondition>
                            <dxg:FormatCondition FieldName="EximBitisTarihi_BitisKalanGun" Value1="15" ValueRule="LessOrEqual" ApplyToRow="False">
                                <dx:Format  Icon="{dx:IconSet Name=Arrows3_3}" TextDecorations="{x:Null}"/>

                            </dxg:FormatCondition>



                        </dxg:TableView.FormatConditions>

                    </dxg:TableView>
                </dxg:GridControl.View>

                <dxg:GridControl.Bands>
                    <dxg:GridControlBand Header="Cari Genel">
                        <dxg:GridColumn MaxWidth="55" FilterPopupMode="Excel" FieldName="CariKod"  />

                        <dxg:GridColumn Width="60" AutoFilterCriteria="Contains" FilterPopupMode="Excel" FieldName="UlkeAd"  IsSmart="True" />



                        <dxg:GridColumn Width="*" ToolTip="CariFinansalNot" FilterPopupMode="Excel" MinWidth="140" AutoFilterCondition="Contains" FieldName="CariIsim" Header="Cari İsim">
                            <dxg:GridColumn.CellTemplate>
                                <DataTemplate>
                                    <StackPanel  Orientation="Horizontal">
                                        <Image Width="24" Source="{Binding RowData.Row.UlkeKod,Converter={StaticResource ImageFromAssemblyCnv},ConverterParameter='Binding  RowData.Row'}" />
                                        <TextBlock 
                                           Padding="2"  Text="{Binding RowData.Row.CariIsim}" />
                                    </StackPanel>
                                </DataTemplate>
                            </dxg:GridColumn.CellTemplate>
                        </dxg:GridColumn>

                        <dxg:GridColumn  FieldName="DovizAd" IsSmart="True" />

                        <dxg:GridColumn Header="Müş.Kullanım Alan" dxg:BandBase.GridRow="1"  AutoFilterCriteria="Contains" FilterPopupMode="Excel"  FieldName="MusteriKullanimAlanTipKod" IsSmart="True" />

                        <dxg:GridColumn Header="Müş.Sorumlusu" dxg:BandBase.GridRow="1"  AutoFilterCriteria="Contains" FilterPopupMode="Excel"  FieldName="PandaMusteriSorumlusu" IsSmart="True" />
                        <dxg:GridColumn Header="Agent" dxg:BandBase.GridRow="1"  AutoFilterCriteria="Contains" FilterPopupMode="Excel"  FieldName="PandaAgent" IsSmart="True" />
                        <dxg:GridColumn Header="Saha Sorumlusu" dxg:BandBase.GridRow="1"  AutoFilterCriteria="Contains" FilterPopupMode="Excel"  FieldName="PandaSahaSorumlusu" IsSmart="True" />


                    </dxg:GridControlBand>

                    <dxg:GridControlBand Header="Exim Data">

                        <dxg:GridColumn  Header="Exim Kod" FieldName="EximBankKod" />

                        <dxg:GridColumn  Header="Limit" FieldName="EximBankLimit">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn  Header=" "  />

                        <dxg:GridColumn ReadOnly="False" dxg:BandBase.GridRow="1" AutoFilterCriteria="Contains" FilterPopupMode="Excel" Header="Başlangıç Tarih" 
                                FieldName="EximBaslangicTarih"  IsSmart="True" >
                            <dxg:GridColumn.EditSettings>
                                <dxe:DateEditSettings/>
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn ReadOnly="False"  dxg:BandBase.GridRow="1"  AutoFilterCriteria="Contains" FilterPopupMode="Excel" Header="Bitiş Tarih" 
                                FieldName="EximBitisTarih"  IsSmart="True" >
                            <dxg:GridColumn.EditSettings>
                                <dxe:DateEditSettings/>
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn   dxg:BandBase.GridRow="1" Header="Kalan Gün" FieldName="EximBitisTarihi_BitisKalanGun" />





                    </dxg:GridControlBand>

                    <dxg:GridControlBand Header="Risk Alacak">
                        <dxg:GridColumn CellStyle="{StaticResource lightGreenCell}"  Header="Exim Limit" FieldName="EximBankLimit">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>



                        <dxg:GridColumn CellStyle="{StaticResource lightGreenCell}"  FieldName="GarantiFactoringLimit">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn CellStyle="{StaticResource lightGreenCell}"  FieldName="IngFactoringLimit" >
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn dxg:BandBase.GridRow="1"  CellStyle="{StaticResource lightGreenCell}" FieldName="BankaTeminati">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn   dxg:BandBase.GridRow="1" CellStyle="{StaticResource lightGreenCell}"  Header="Yeni Sevk Limit" FieldName="YoneticiSevkLimit">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn  dxg:BandBase.GridRow="1" CellStyle="{StaticResource lightGreenCell}" Header="Yeni Sip Limit" FieldName="YoneticiSiparisLimit">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                    </dxg:GridControlBand>

                    <dxg:GridControlBand Header="Risk Borç">
                        <dxg:GridColumn   Header="Açık Hesap" ReadOnly="True" CellStyle="{StaticResource grayCell1}"  FieldName="AcikHesap">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>



                        <dxg:GridColumn ReadOnly="True" CellStyle="{StaticResource grayCell1}"  Header="Müşteri Çeki" FieldName="NetsisMusteriCekRiski">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn  dxg:BandBase.GridRow="1" ReadOnly="True" CellStyle="{StaticResource grayCell1}"  
                                         Header="Kendi Çeki" FieldName="NetsisKendiCekRiski">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn  dxg:BandBase.GridRow="1" ReadOnly="True" CellStyle="{StaticResource grayCell1}"  
                                         Header="  " FieldName="">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                    </dxg:GridControlBand>

                    <dxg:GridControlBand Header="Risk Toplamları">
                        <dxg:GridColumn   ReadOnly="True"  FilterPopupMode="Excel" CellStyle="{StaticResource grayCell}" 
                                           FieldName="ToplamLimit">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn   ReadOnly="True" FilterPopupMode="Excel" CellStyle="{StaticResource grayCell}" 
                                         FieldName="ToplamRisk">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn ReadOnly="True" dxg:BandBase.GridRow="1" CellToolTipBinding="{Binding CariFinansalNot}"   FilterPopupMode="Excel" 
                                CellStyle="{StaticResource grayCell}" 
                                FieldName="KullanilabilirLimit" >

                            <dxg:GridColumn.CellTemplate>
                                <DataTemplate>
                                    <StackPanel HorizontalAlignment="Right"  Orientation="Horizontal">
                                        <TextBlock  HorizontalAlignment="Right"  ToolTip="{Binding RowData.Row.CariFinansalNot}" 
                                           Padding="2"  Text="{Binding RowData.Row.KullanilabilirLimit, UpdateSourceTrigger=PropertyChanged, StringFormat=' {0:n0}'}" />
                                    </StackPanel>
                                </DataTemplate>
                            </dxg:GridColumn.CellTemplate>



                        </dxg:GridColumn>

                        <dxg:GridColumn dxg:BandBase.GridRow="1"  ReadOnly="True" FilterPopupMode="Excel" Header=""
                                         >

                        </dxg:GridColumn>

                    </dxg:GridControlBand>

                    <dxg:GridControlBand Header="Diğer Risk">
                        <dxg:GridColumn Header="Yeni Satis Döv.T" ReadOnly="True" CellStyle="{StaticResource grayCell1}"  FieldName="SatisYeniKayitRiski">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn Header="Depo Döv.T" ReadOnly="True" CellStyle="{StaticResource grayCell1}"  FieldName="MusteriDepoRiski">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn dxg:BandBase.GridRow="1" Header="Sevk Emri Döv.T" ReadOnly="True" 
                                        CellStyle="{StaticResource grayCell1}"  FieldName="MusteriSevkEmirleriRiski">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn dxg:BandBase.GridRow="1" Header="Üretim  Döv.T" ReadOnly="True" CellStyle="{StaticResource grayCell1}" 
                                        FieldName="UretimRiski">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>





                    </dxg:GridControlBand>

                    <dxg:GridControlBand Header="Miktarlar">
                        <dxg:GridColumn Header="Depo Kg" ReadOnly="True" FieldName="DepodakiUrunMiktarKg"  IsSmart="True" >
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>


                        <dxg:GridColumn   ReadOnly="True" FieldName="UretimdekiMiktar">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn  dxg:BandBase.GridRow="1" ReadOnly="True" FieldName="SevkEmrindekiMiktar">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                        <dxg:GridColumn dxg:BandBase.GridRow="1" Header="  "   ReadOnly="True" FieldName="">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings Mask="n0" DisplayFormat="n0" />
                            </dxg:GridColumn.EditSettings>
                        </dxg:GridColumn>

                    </dxg:GridControlBand>

                    <dxg:GridControlBand Header="Son Güncelleme Bilgileri">
                        <dxg:GridColumn Visible="True" Header="Kullanıcı"  FieldName="SonGuncelleyen">

                        </dxg:GridColumn>

                        <dxg:GridColumn Visible="True" Header="Tarih" dxg:BandBase.GridRow="1"  FieldName="SonGuncellemeTar">
                            <dxg:GridColumn.EditSettings>
                                <dxe:TextEditSettings DisplayFormat="dd/MM/yyyy HH:mm"/>
                            </dxg:GridColumn.EditSettings>

                        </dxg:GridColumn>
                    </dxg:GridControlBand>

                </dxg:GridControl.Bands>




            </dxg:GridControl>
        </materialDesign:Card>
    </Grid>
</UserControl>