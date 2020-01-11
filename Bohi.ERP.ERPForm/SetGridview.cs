using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.ERPForm
{
    public class SetGridview
    {
        #region 自定义GridView
        // <summary>
        /// 自定义GridView控件
        /// </summary>
        /// <param name="gridView"></param>
        public void CustomizeGridView(GridView gridView)
        {
            //foreach (GridView gridView in gridControl.Views)
            //{
            //1、设置属性
            gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;//选择框覆盖选中的一整行
            gridView.OptionsBehavior.Editable = false; //不可编辑
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;//设置选中单元格底色不变
            gridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;//点击选中整行
            gridView.OptionsFilter.AllowFilterEditor = false;//禁用FilterEdit(右下角的筛选编辑按钮)
            gridView.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.Text;
            gridView.OptionsFilter.AllowMRUFilterList = false;//禁用gridview底部筛选条件面板的下拉选择
            gridView.OptionsFilter.ColumnFilterPopupMode = DevExpress.XtraGrid.Columns.ColumnFilterPopupMode.Classic;//设置列筛选弹出菜单的样式
            gridView.OptionsView.ColumnAutoWidth = false;//取消列宽度自适应
            gridView.IndicatorWidth = 40;//设置初始行号列列宽

            //2、设置事件
            gridView.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;//在该事件中设置显示行号
            gridView.GridControl.DataSourceChanged += GridControl_DataSourceChanged;//在数据源变化事件中设置行号列的列宽

        }

        /// <summary>
        /// 数据源变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridControl_DataSourceChanged(object sender, EventArgs e)
        {
            GridControl gridControl = (sender as GridControl);
            object obj = gridControl.DataSource;
            if (obj == null) return;
            Type type = obj.GetType();
            if (type.IsGenericType)//如果数据源为"List<>"
            {
                int count = (int)type.GetProperty("Count").GetValue(obj, null);//利用反射获取列表的Count属性值
                count = count.ToString().Length;
                GridView gridView = (gridControl.FocusedView as GridView);
                gridView.IndicatorWidth = ((count < 3) ? 3 : count) * 12;
                gridView.BestFitColumns();
            }
        }

        private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //设置显示行号
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
            else
            {
                e.Info.DisplayText = "序号";
            }
        }
        #endregion
    }
}
