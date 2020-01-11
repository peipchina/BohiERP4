using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bohi.ERP.BLL;
using Bohi.ERP.MODEL;
using Stimulsoft.Report;
using DevExpress.XtraSplashScreen;

namespace Bohi.ERP.ERPForm.UserControls
{
    public partial class SearchOldDataUCtrl : BaseUserControl
    {
        public SearchOldDataUCtrl()
        {
            InitializeComponent();
            LoadSet();
            Initialize_Event();
        }
        public int select=-1;
        public TradeMD td = null;
        /// <summary>
        /// 窗体加载数据
        /// </summary>
        private void LoadSet()
        {
            this.Text = "原数据查询";
            SetDataEdit(deEndTime);//设置时间格式及初始化时间为当前日期
            deStartTime.DateTime = DateTime.Now.Date;
            SetDataEdit(deStartTime);
            deEndTime.DateTime = DateTime.Now.Date;            
            Setview();//设置Girdvew显示格式
            ColumnsAdd(this.gridView1);
            DataBinding();//Gridview绑定数据
            tedPrint.ReadOnly = true;//打印显示只读
            Export();//导出Excel按钮事件
            this.gridView1.OptionsView.ShowFooter=true;
            TotalSet();
        }
        /// <summary>
        /// 初始化查询按钮事件
        /// </summary>
        private void Initialize_Event()
        {
            sbtnSearch.Click += SbtnSearch_Click;//初始化搜索按钮
            tedFilter.TextChanged += TedFilter_TextChanged;//初始化过滤输入框
            this.gridView1.RowClick += GridView1_RowClick;//初始化Gridview列点击事件
            sbtnPrint.Click += SbtnPrint_Click;
        }
        #region --------事件--------
        /// <summary>
        /// 导出Excel按钮事件
        /// </summary>
        private void Export()
        {
            sbtnExportExcel.Click += SbtnExportExcel_Click;
        }

        private void SbtnExportExcel_Click(object sender, EventArgs e)
        {
            ExportExcel() ;
        }

        /// <summary>
        /// 打印磅单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnPrint_Click(object sender, EventArgs e)
        {
            PrintPount();
        }
        /// <summary>
        /// 点击Gridview，获取需要打印的列名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            select = this.gridView1.GetDataSourceRowIndex(e.RowHandle);
            GetRowSelect();
        }
        /// <summary>
        /// 过滤输入框文字变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TedFilter_TextChanged(object sender, EventArgs e)
        {
            this.gridView1.FindFilterText = tedFilter.Text.Trim();
        }
        /// <summary>
        /// 搜索按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnSearch_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            DataSeracheByDate();
            SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion

        #region--------方法--------
        private void TotalSet()
        {
            gridView1.Columns["net"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "net", "统计：{0:n2}");
        }
        /// <summary>
        /// 导出Excel方法
        /// </summary>
        public void ExportExcel()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel文件|*.XLS|所有文件|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                this.gridView1.ExportToXls(filename);
                MessageBox.Show("数据导出成功!", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }
        /// <summary>
        /// 点击Gridview，获取列对象
        /// </summary>
        private void GetRowSelect()
        {
            if (select < 0) return;
            var data = this.gridControl1.DataSource as List<TradeMD>;
            TradeMD tm = data[select];
            tedPrint.Text = tm.ticketno1;
        }
        /// <summary>
        /// 设定Gridview显示格式
        /// </summary>
        private void Setview()
        {
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(this.gridView1);
        }
        /// <summary>
        /// 手动添加Gridview列名
        /// </summary>
        /// <param name="gridview"></param>
        private void ColumnsAdd(DevExpress.XtraGrid.Views.Grid.GridView gridview)
        {
            //gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ticketno1", FieldName = "ticketno1", Caption = "No", VisibleIndex = 0, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "truckno", FieldName = "truckno", Caption = "车号", VisibleIndex = 1, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "product", FieldName = "product", Caption = "物料名", VisibleIndex = 2, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "specification", FieldName = "specification", Caption = "船名/船舱号", VisibleIndex = 11, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "receiver", FieldName = "receiver", Caption = "收货单位", VisibleIndex = 12, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "transporter", FieldName = "transporter", Caption = "运输单位", VisibleIndex = 13, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "tare", FieldName = "tare", Caption = "皮重（kg）", VisibleIndex = 3, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "taredatetime", FieldName = "taredatetime", Caption = "皮重时间", VisibleIndex = 4, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "username1", FieldName = "username1", Caption = "司磅员", VisibleIndex = 5, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "gross", FieldName = "gross", Caption = "毛重（kg）", VisibleIndex = 6, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "grossdatetime", FieldName = "grossdatetime", Caption = "毛重时间", VisibleIndex = 7, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "username2", FieldName = "username2", Caption = "司磅员", VisibleIndex = 8, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "net", FieldName = "net", Caption = "净重（kg）", VisibleIndex = 9, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "sender", FieldName = "sender", Caption = "发货单位", VisibleIndex = 10, Visible = Enabled });
            gridview.Columns["taredatetime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridview.Columns["grossdatetime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";

        }
        /// <summary>
        /// 设置DateEdit格式
        /// </summary>
        /// <param name="de">DateEdit输入框</param>
        private void SetDataEdit(DevExpress.XtraEditors.DateEdit de)
        {
            de.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            de.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            de.Properties.DisplayFormat.FormatString = "G";
            de.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            de.Properties.EditFormat.FormatString = "G";
            de.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            de.Properties.Mask.EditMask = "G";
        }
        /// <summary>
        /// 绑定数据到GridView中
        /// </summary>
        private void DataBinding()
        {
            if (radioGroup1.SelectedIndex==1)
            {
                //radioGroup1.
                TradeManager tm = new TradeManager();
                List<TradeMD> lt = tm.GetTrade_M();
                this.gridControl1.DataSource = lt;
            }
            else
            {
                TradeManager tm = new TradeManager();
                List<TradeMD> lt = tm.GetTrade_O();
                this.gridControl1.DataSource = lt;
            }
            
        }
        /// <summary>
        /// 根据时间绑定查询数据到Gridview中
        /// </summary>
        private void DataSeracheByDate()
        {
            if (radioGroup1.SelectedIndex == 1)
            {
                try
                {
                    TradeManager tm = new TradeManager();
                    List<TradeMD> lt = tm.GetTrade_mByDate(deStartTime.DateTime, deEndTime.DateTime);
                    this.gridControl1.DataSource = lt;
                }
                catch (Exception)
                {                    
                }
            }
            else
            {
                try
                {
                    TradeManager tm = new TradeManager();
                    List<TradeMD> lt = tm.GetTrade_OByDate(deStartTime.DateTime, deEndTime.DateTime);
                    this.gridControl1.DataSource = lt;
                }
                catch (Exception)
                {                    
                }
            }
        }
        /// <summary>
        /// 打印磅单
        /// </summary>
        private void PrintPount()
        {
            if (select < 0) return;
            var data = this.gridControl1.DataSource as List<TradeMD>;
            TradeMD tm = data[select];
            StiReport sr = new StiReport();
            sr.Load("ReportOld.mrt");
            sr.Dictionary.Clear();
            sr.Dictionary.BusinessObjects.Clear();
            sr.RegBusinessObject("SacOutSupervise", tm);
            sr.Dictionary.Synchronize();
            sr.Dictionary.SynchronizeBusinessObjects();
            //sr.PrinterSettings.PrinterName = cbbPrintSet.Text;
            sr.Compile();
            sr.Show();
            //sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
            //sr.Show();
            //sr.Design();
        }
        #endregion
    }
}
