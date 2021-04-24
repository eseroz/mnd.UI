using DevExpress.Data.Filtering;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Helper.GridFilterHelper
{ 
    public class PopupBaseEditEx : PopupBaseEdit {
        public PopupBaseEditEx() {
            this.PopupClosed += PopupBaseEditEx_PopupClosed;
        }

        protected override void OnPopupOpened() {

            base.OnPopupOpened();

            var info = DataContext as ExcelDateTimeColumnFilterInfoEx;
            if (info != null) {
                ((TableViewEx)info.Column.View).UseExternalFilter = true;
                info.UpdateData(this, null);
            }
        }

        private void PopupBaseEditEx_PopupClosed(object sender, ClosePopupEventArgs e) {
            if(e.CloseMode == PopupCloseMode.Normal) {
                var info = DataContext as ExcelDateTimeColumnFilterInfoEx;
                if(info != null)
                    info.OnPopupClosed();
            }
        }
    }

    public class TableViewEx : TableView {
        public bool UseExternalFilter {
            get { return (bool)GetValue(UseExternalFilterProperty); }
            set { SetValue(UseExternalFilterProperty, value); }
        }
        public static readonly DependencyProperty UseExternalFilterProperty =
            DependencyProperty.Register("UseExternalFilter", typeof(bool), typeof(TableViewEx), new PropertyMetadata(false));


        protected override CriteriaOperator GetCheckedFilterPopupFilterCriteria(ColumnBase column, List<object> selectedItems) {
            if (UseExternalFilter) {
               // UseExternalFilter = false;
                return ((GridColumnEx)Columns[0]).FilterInfo.GetFilterCriteriaEx();
            }
            else return base.GetCheckedFilterPopupFilterCriteria(column, selectedItems);
        }
        protected override IEnumerable GetCheckedFilterPopupSelectedItems(ColumnBase column, IEnumerable items, CriteriaOperator filterCriteria) {
            if (UseExternalFilter) {
                //UseExternalFilter = false;
                return ((GridColumnEx)Columns[0]).FilterInfo.GetSelectedItemsEx(items, filterCriteria);
            }
            else return base.GetCheckedFilterPopupSelectedItems(column, items, filterCriteria);
        }
    }

    public class ExcelDateTimeColumnFilterInfoEx : ExcelDateTimeColumnFilterInfo {
        protected override bool ImmediateUpdateFilter { get { return false; } }

        public ExcelDateTimeColumnFilterInfoEx(ColumnBase column) : base(column) { }
        public void UpdateData(PopupBaseEdit popup, object[] values) {
            this.UpdatePopupData(popup, values);
            this.AfterPopupOpening(popup);
        }

        public void OnPopupClosed() {
            CriteriaOperator op = GetFilterCriteriaCore();

            Column.View.DataControl.MergeColumnFilters(op);
          
        }

        public CriteriaOperator GetFilterCriteriaEx() {
            return GetFilterCriteria();
        }

        public IEnumerable GetSelectedItemsEx(IEnumerable items, CriteriaOperator op) {
            return GetSelectedItems(items, op);
        }
    }
    public class GridColumnEx : GridColumn {
        public ExcelDateTimeColumnFilterInfoEx FilterInfo {
            get { return (ExcelDateTimeColumnFilterInfoEx)GetValue(FilterInfoProperty); }
            set { SetValue(FilterInfoProperty, value); }
        }

        public static readonly DependencyProperty FilterInfoProperty =
            DependencyProperty.Register("FilterInfo", typeof(ExcelDateTimeColumnFilterInfoEx), typeof(GridColumnEx), new PropertyMetadata(null));

        private GridControl Grid { get { return ((GridControl)this.View.DataControl); } }

        public GridColumnEx() {
            FilterInfo = new ExcelDateTimeColumnFilterInfoEx(this);
        }
    }
}
