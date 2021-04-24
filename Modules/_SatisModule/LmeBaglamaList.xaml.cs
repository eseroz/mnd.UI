using DevExpress.Data;
using DevExpress.Xpf.Grid;
using mnd.Logic.Model.Satis;
using System.Windows.Controls;

namespace mnd.UI.Modules._SatisModule
{
    /// <summary>
    /// Interaction logic for BankaListView.xaml
    /// </summary>
    public partial class LmeBaglamaList : UserControl
    {
        public LmeBaglamaList()
        {
            InitializeComponent();
        }

        public decimal t_BaglamaMiktar { get; set; }
        public decimal t_carpimMiktarLmeDeger { get; set; }

        public decimal tg_BaglamaMiktar { get; set; }
        public decimal tg_carpimMiktarLmeDeger { get; set; }

        private void GridControl_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsGroupSummary)
            {
                if (((GridSummaryItem)e.Item).FieldName == "MusteriKod")
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Start)
                    {

                        tg_BaglamaMiktar = 0;
                        tg_carpimMiktarLmeDeger = 0;
                    }

                    if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                    {
                        if (e.Row != null)
                        {
                            var lme = (LmeBaglama)e.Row;

                            tg_BaglamaMiktar += lme.BaglantiLfx_kg;
                            tg_carpimMiktarLmeDeger += lme.BaglantiLfx_kg * lme.LmeDeger;

                        }
                    }

                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        if (tg_BaglamaMiktar != 0)
                            e.TotalValue = tg_carpimMiktarLmeDeger / tg_BaglamaMiktar;
                    }

                }
            }

            if (e.IsTotalSummary)
            {
                if (((GridSummaryItem)e.Item).FieldName == "MusteriKod" && ((GridSummaryItem)e.Item).Tag.ToString() == "1")
                {
                    AgirlikliOrtalamaHesapla(e, "EUR");

                }
                if (((GridSummaryItem)e.Item).FieldName == "MusteriKod" && ((GridSummaryItem)e.Item).Tag.ToString() == "2")
                {
                    AgirlikliOrtalamaHesapla(e, "USD");
                }

                if (((GridSummaryItem)e.Item).FieldName == "MusteriKod" && ((GridSummaryItem)e.Item).Tag.ToString() == "3")
                {
                    AgirlikliOrtalamaHesapla(e, "GBP");
                }

            }
        }

        private void AgirlikliOrtalamaHesapla(CustomSummaryEventArgs e, string DovizTur)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                t_BaglamaMiktar = 0;
                t_carpimMiktarLmeDeger = 0;
            }

            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                if (e.Row != null)
                {
                    var lme = (LmeBaglama)e.Row;

                    if (lme.DovizTipKod == DovizTur)
                    {
                        t_BaglamaMiktar += lme.BaglantiLfx_kg;
                        t_carpimMiktarLmeDeger += lme.BaglantiLfx_kg * lme.LmeDeger;
                    }
                }
            }

            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (t_BaglamaMiktar != 0)
                    e.TotalValue = t_carpimMiktarLmeDeger / t_BaglamaMiktar;
            }
        }
    }

}