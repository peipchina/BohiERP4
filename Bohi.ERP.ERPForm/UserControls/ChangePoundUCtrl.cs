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
    public partial class ChangePoundUCtrl : BaseUserControl
    {
        public ChangePoundUCtrl()
        {
            InitializeComponent();
            Loding();
        }
        /// <summary>
        /// 加载
        /// </summary>
        private void Loding()
        {
            setGridview();
            ColumnsAdd(gvChange);
            SearchAutoCode(sbtSearch);
            GridViewRow(gvChange);//点击gridivew，获取数据
            DeleteButton(sbtnDelete);//删除
            CancleButton(sbtnCancle);
            ModifyButton(sbtnModify);
            InNumber(tedQTYGross);
            InNumber(tedQTYNet);
            InNumber(tedQTYTare);
            TextChange(tedFiter);
            TxtChange(tedQTYGross);
            TxtChange(tedQTYTare);
            CheckChange(ckFirst);
            sbtnPrint.Click += SbtnPrint_Click;//初始化打印事件
            sbtnSearch.Click += SbtnSearch_Click;//初始化查询按钮
        }

        private void SbtnSearch_Click(object sender, EventArgs e)
        {
            if (teSearch.Text.Trim() == string.Empty) return;
            SplashScreenManager.ShowDefaultWaitForm();
            SearchPoundList();
            SplashScreenManager.CloseDefaultWaitForm();
        }



        #region 初始化界面

        /// <summary>
        /// 设置GridView
        /// </summary>
        private void setGridview()
        {
            this.Text = "修改单据";
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(gvChange);
            dtStartTime.DateTime = DateTime.Now.Date;
            dtEndTime.DateTime = DateTime.Now.Date;
        }
        /// <summary>
        /// 手动添加Gridview列名
        /// </summary>
        /// <param name="gridview"></param>
        private void ColumnsAdd(DevExpress.XtraGrid.Views.Grid.GridView gridview)
        {
            //gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "No", FieldName = "No", Caption = "No", VisibleIndex = 2, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车号", VisibleIndex = 3, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYTare", FieldName = "QTYTare", Caption = "皮重（kg）", VisibleIndex = 4, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareTime", FieldName = "TareTime", Caption = "皮重时间", VisibleIndex = 5, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundTareName", FieldName = "PoundTareName", Caption = "毛重磅名", VisibleIndex = 6, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYGross", FieldName = "QTYGross", Caption = "毛重（kg）", VisibleIndex = 7, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "GrossTime", FieldName = "GrossTime", Caption = "毛重时间", VisibleIndex = 8, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYNet", FieldName = "QTYNet", Caption = "统计（kg）", VisibleIndex = 9, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "MatName", FieldName = "MatName", Caption = "物料名", VisibleIndex = 10, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Customer", FieldName = "Customer", Caption = "客户名", VisibleIndex = 11, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipName", FieldName = "ShipName", Caption = "船名", VisibleIndex = 12, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipNo", FieldName = "ShipNo", Caption = "船仓号", VisibleIndex = 13, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundGrossName", FieldName = "PoundGrossName", Caption = "皮重磅名", VisibleIndex = 14, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareStfName", FieldName = "TareStfName", Caption = "司磅员", VisibleIndex = 15, Visible = Enabled });
            gridview.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridview.Columns["GrossTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";

        }

        #endregion

        List<PoundTotalMD> lpt = new List<PoundTotalMD>();
        int select=-1;
        #region ------方法------
        /// <summary>
        /// 绑定地磅列表到修改Gridview 中
        /// </summary>
        private void SearchPoundList()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp= ptm.GetPoundListByAutoCode("%"+ teSearch.Text.Trim()+"%");
            this.gcChange.DataSource = lp;
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            List<PoundTotalMD> lpt = this.gcChange.DataSource as List<PoundTotalMD>;
            if (select < 0)
            {
                MessageBox.Show("请选择需要打印的列！");
                return;                
            }
            else
            {
                PoundTotalMD pt = lpt[select];
                StiReport sr = new StiReport();
                sr.Load("ReportSoy.mrt");
                sr.Dictionary.Clear();
                sr.Dictionary.BusinessObjects.Clear();
                sr.RegBusinessObject("SacOutSupervise", pt);
                sr.Dictionary.Synchronize();
                sr.Dictionary.SynchronizeBusinessObjects();
                sr.Compile();
                sr.Show();
                //sr.Show();
            }
        }
        /// <summary>
        /// 过滤Gridview内容
        /// </summary>
        /// <param name="te"></param>
        private void TextChange(DevExpress.XtraEditors.TextEdit te)
        {
            te.TextChanged += Te_TextChanged;
        }

        private void Te_TextChanged(object sender, EventArgs e)
        {
            gvChange.FindFilterText=tedFiter.Text.Trim();
        }

        /// <summary>
        /// 输入框中只能输入数字
        /// </summary>
        /// <param name="te"></param>
        private void InNumber(DevExpress.XtraEditors.TextEdit te)
        {
            te.KeyPress += Te_KeyPress;
        }

        private void Te_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                if (e.KeyChar != '.' || (sender as DevExpress.XtraEditors.TextEdit).Text.Length == 0)//小数点  
                {
                    e.Handled = true;
                }
                if ((sender as DevExpress.XtraEditors.TextEdit).Text.LastIndexOf('.') != -1)
                {
                    e.Handled = true;
                }
            }
        }
        /// <summary>
        /// 皮重和毛重变化，净重跟着变化
        /// </summary>
        /// <param name="te"></param>
        private void TxtChange(DevExpress.XtraEditors.TextEdit te)
        {
            te.TextChanged += Te_TextChanged1;
        }

        private void Te_TextChanged1(object sender, EventArgs e)
        {
            if (tedQTYTare.Text.Trim() == "" || tedQTYGross.Text.Trim() == "") return;
            if (float.Parse(tedQTYGross.Text) > float.Parse(tedQTYTare.Text))
            {
                tedQTYNet.Text = (float.Parse(tedQTYGross.Text) - float.Parse(tedQTYTare.Text)).ToString();
            }
            else
            {
                tedQTYNet.Text = (float.Parse(tedQTYTare.Text) - float.Parse(tedQTYGross.Text)).ToString();
            }
            
        }

        /// <summary>
        /// 重置输入框
        /// </summary>
        private void LoadSet()
        {
            tedAutoCode.Text = "";
            tedCustomer.Text = "";
            select = -1;
            tedDeliveryCo.Text = "";
            tedMatName.Text = "";
            tedNo.Text = "";
            tedQTYGross.Text = "";
            tedQTYTare.Text = "";
            tedQTYNet.Text = "";
            tedRemark.Text = "";
            tedShipName.Text = "";
            tedShipNo.Text = "";
            tedTransportCo.Text = "";
        }

        /// <summary>
        /// 绑定Gridview，显示第一次过磅的车辆
        /// </summary>
        private void FirstIn()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();            
            if (cbSoyPound.CheckState == CheckState.Checked)
            {
                lp = ptm.GetPoundListFirstSoybean();//显示大豆车正在过磅车辆
            }
            else
            {
                lp = ptm.GetPoundListFirstNoSoybean();//显示非大豆车正在过磅车辆
            }
            this.gcChange.DataSource = lp;
        }
        /// <summary>
        /// 根据时间查询车辆进厂信息（已出厂）
        /// </summary>
        private void SearchAutoCodeByDate()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            lpt = ptm.GetPoundListByDateTime(dtStartTime.DateTime, dtEndTime.DateTime);
            this.gcChange.DataSource = lpt;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        private void DeletePound()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            if (select < 0) 
            {
                MessageBox.Show("请选择需要删除的行！","提示框");
                return;
            }
            List<PoundTotalMD> ptl = new List<PoundTotalMD>();
            ptl = (List<PoundTotalMD>)this.gcChange.DataSource;
            ptl[select].DeletStfName = LoginFrm.pubDelIn.Name;
            ptm.DeletePound(ptl[select]);
            select = -1;
            if (ckFirst.CheckState == CheckState.Checked)
            {
                FirstIn();
            }
            else
            {
                SearchAutoCodeByDate();
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        private void UpdataPound()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            if (select < 0)
            {
                MessageBox.Show("请选择需要更新的行！", "提示框");
                return;
            }
            List<PoundTotalMD> ptl = new List<PoundTotalMD>();
            ptl = (List<PoundTotalMD>)this.gcChange.DataSource;
            PoundTotalMD pt = ptl[select];
            pt.ChangeStfName = LoginFrm.pubDelIn.Name;
            pt.ChangeTime = DateTime.Now;
            if (tedQTYTare.Text.Trim()!=string.Empty)
            {
                pt.QTYTare = decimal.Parse(tedQTYTare.Text.Trim());
            }
            else
            {
                pt.QTYTare = null;
            }
            if (tedQTYGross.Text.Trim()!=string.Empty)
            {
                pt.QTYGross = decimal.Parse(tedQTYGross.Text.Trim());
            }
            else
            {
                pt.QTYGross = null;
            }
            if (tedQTYNet.Text.Trim()!=string.Empty)
            {
                pt.QTYNet = decimal.Parse(tedQTYNet.Text.Trim());
            } 
            else
            {
                pt.QTYNet = null;
            }
            pt.TransportCo = tedTransportCo.Text.Trim();
            pt.MatName = tedMatName.Text.Trim();
            pt.Customer = tedCustomer.Text.Trim();
            pt.Remark = tedRemark.Text.Trim();
            pt.ShipName = tedShipName.Text.Trim();
            if (tedShipNo.Text.Trim()!=string.Empty)
            {
                pt.ShipNo = int.Parse(tedShipNo.Text.Trim());
            } 
            else
            {
                pt.ShipNo = null;
            }
            pt.DeliveryCo = tedDeliveryCo.Text.Trim();
            ptm.UpdataPound(pt);
            select = -1;
            if (ckFirst.CheckState == CheckState.Checked)
            {
                FirstIn();
            }
            else
            {
                SearchAutoCodeByDate();
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 打印选择的列内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }
        /// <summary>
        /// 更改选择“检查厂内车辆按钮”事件
        /// </summary>
        /// <param name="ce"></param>
        private void CheckChange(DevExpress.XtraEditors.CheckEdit ce)
        {
            ce.CheckedChanged += Ce_CheckedChanged;
        }

        private void Ce_CheckedChanged(object sender, EventArgs e)
        {
            if (ckFirst.CheckState== CheckState.Checked)
            {
                tedQTYGross.Text = "";
                tedQTYGross.ReadOnly = true;
            }
            else
            {
                tedQTYGross.ReadOnly = false;
            }
        }

        /// <summary>
        /// 点击Gridview，获取选择行数据泛型
        /// </summary>
        /// <param name="gv">gridview</param>
        private void GridViewRow(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            gv.RowClick += Gv_RowClick;
        }

        private void Gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                List<PoundTotalMD> lp = (List<PoundTotalMD>)this.gcChange.DataSource;
                select = (sender as DevExpress.XtraGrid.Views.Grid.GridView).GetDataSourceRowIndex(e.RowHandle);
                tedAutoCode.Text = lp[select].AutoCode;
                tedNo.Text = lp[select].No.ToString();
                tedQTYTare.Text = lp[select].QTYTare.ToString();
                tedQTYGross.Text = lp[select].QTYGross.ToString();
                tedQTYNet.Text = lp[select].QTYNet.ToString();
                tedMatName.Text = lp[select].MatName;
                tedShipName.Text = lp[select].ShipName;
                tedShipNo.Text = lp[select].ShipNo.ToString();
                tedTransportCo.Text = lp[select].TransportCo;
                tedCustomer.Text = lp[select].Customer;
                tedDeliveryCo.Text = lp[select].DeliveryCo;
                tedRemark.Text = lp[select].Remark;
            }
            catch (Exception )
            {
                
               
            }
        }

        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        /// <param name="sb"></param>
        private void SearchAutoCode(DevExpress.XtraEditors.SimpleButton sb)
        {
            sb.Click += Sb_Click;
        }

        private void Sb_Click(object sender, EventArgs e)
        {
            if (ckFirst.CheckState== CheckState.Checked)
            {
                FirstIn();
            }
            else
            {
                SplashScreenManager.ShowDefaultWaitForm();
                SearchAutoCodeByDate();
                SplashScreenManager.CloseDefaultWaitForm();
            }
            
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sb"></param>
        private void DeleteButton(DevExpress.XtraEditors.SimpleButton sb)
        {
            sb.Click += Sb_Click1;
        }

        private void Sb_Click1(object sender, EventArgs e)
        {
            DeletePound();
            LoadSet();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        private void CancleButton(DevExpress.XtraEditors.SimpleButton sb)
        {
            sb.Click += Sb_Click2;
        }

        private void Sb_Click2(object sender, EventArgs e)
        {
            LoadSet();
        }
        private void ModifyButton(DevExpress.XtraEditors.SimpleButton sb)
        {
            sb.Click += Sb_Click3;
        }

        private void Sb_Click3(object sender, EventArgs e)
        {
            UpdataPound();
            LoadSet();
        }
        #endregion
    }
}
