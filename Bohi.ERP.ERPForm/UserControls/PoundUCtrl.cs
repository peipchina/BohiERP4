using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO.Ports;
using System.Threading;
using System.Net;
using Bohi.ERP.BLL;
using Bohi.ERP.MODEL;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Stimulsoft.Report;

namespace Bohi.ERP.ERPForm.UserControls
{
    public partial class PoundUCtrl : BaseUserControl
    {
        public PoundUCtrl()
        {
            InitializeComponent();
            SetText();
            ToolSet();
            setGridview();
            AddColumn(gvFirtView);
            ColumnsAdd(gvEndView);
            //SaveButton(btnSave);
            //OpenCom1();
            OpenSerialPort();
            GetHostName();
            MaterialBinding();
            CustomerBinding();
            TextMove(tedAtuoCode);
            SaveButton(btnSave);
            GetPoundWeight(btnGetPoundWeigth);
            CopyPoundWeigth(btnCopy);            
            FirstIn();
            EndOut();
            SearchButton(btnSearch);
            SetDataEdit(dteStartTime);
            SetDataEdit(dteEndTime);
            GridViewRowSelect(gvEndView);
            PrintButton(btnPrint);
            DesingnButton(btnEditPrint);
        }
        /// <summary>
        /// 根据电脑名获取地磅名称
        /// </summary>
        private string poundName = "";
        private int selectRow = -1;

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
        }

        #region 窗体设置
        private void SetText()
        {
            this.Text = "托利多";
        }
        private void ToolSet()
        {
            //设置主工具栏的所有按钮隐藏
            Bar mainMenu = barManager.MainMenu;
            foreach (LinkPersistInfo info in mainMenu.LinksPersistInfo)
            {
               // info.Item.Visibility = BarItemVisibility.Never;//设置可见
               // info.Item.Alignment = BarItemLinkAlignment.Right;//设置靠右显示
                this.barButtonItem4.Visibility = BarItemVisibility.Always;
            }
        }
        #endregion

        /// <summary>
        /// 显示当天出厂车辆信息
        /// </summary>
        private void EndOut()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();
            lp = ptm.GetPoundListByDateTime(DateTime.Now);
            this.gcEndControl.DataSource = lp;
        }
        /// <summary>
        /// 设置GridView
        /// </summary>
        private void setGridview()
        {
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(gvFirtView);
            sg.CustomizeGridView(gvEndView);
        }

        /// <summary>
        /// 手动添加Gridview列名
        /// </summary>
        /// <param name="gridview"></param>
        private void ColumnsAdd(DevExpress.XtraGrid.Views.Grid.GridView gridview)
        {
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "No", FieldName = "No", Caption = "No", VisibleIndex = 2, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车号", VisibleIndex = 3, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYTare", FieldName = "QTYTare", Caption = "皮重（KG）", VisibleIndex = 4, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareTime", FieldName = "TareTime", Caption = "皮重时间", VisibleIndex = 5, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundTareName", FieldName = "PoundTareName", Caption = "毛重磅名", VisibleIndex = 6, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYGross", FieldName = "QTYGross", Caption = "毛重（KG）", VisibleIndex = 7, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "GrossTime", FieldName = "GrossTime", Caption = "毛重时间", VisibleIndex = 8, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYNet", FieldName = "QTYNet", Caption = "统计（KG）", VisibleIndex = 9, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "MatName", FieldName = "MatName", Caption = "物料名", VisibleIndex = 10, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "CutName", FieldName = "CutName", Caption = "客户名", VisibleIndex = 11, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipName", FieldName = "ShipName", Caption = "船名", VisibleIndex = 12, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipNo", FieldName = "ShipNo", Caption = "船仓号", VisibleIndex = 13, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundGrossName", FieldName = "PoundGrossName", Caption = "皮重磅名", VisibleIndex = 14, Visible = Enabled });
           // gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYNet", FieldName = "QTYNet", Caption = "统计", VisibleIndex = 15, Visible = Enabled });
            gridview.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridview.Columns["GrossTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
                    
        }
        /// <summary>
        /// 查询某一时段的车辆出厂情况
        /// </summary>
        /// <param name="bt"></param>
        private void SearchButton(Button bt)
        {
            bt.Click += Bt_Click3;
        }

        private void Bt_Click3(object sender, EventArgs e)
        {
            SearchAutoCode();
        }

        private void SearchAutoCode()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lpt= ptm.GetPoundListByDateTime(dteStartTime.DateTime,dteEndTime.DateTime);
            this.gcEndControl.DataSource = lpt;
        }
       
        /// <summary>
        /// 绑定Gridview，显示第一次过磅的车辆
        /// </summary>
        private void FirstIn()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();
            lp= ptm.GetPoundListFirst();
            this.gcFirstControl.DataSource = lp;
        }
        /// <summary>
        /// 手动添加第一次过磅显示Gridview列
        /// </summary>
        /// <param name="gridView"></param>
        private void AddColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView )
        {
            //gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "pur01", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled };
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "No", FieldName = "No", Caption = "No", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车号", VisibleIndex = 3, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYTare", FieldName = "QTYTare", Caption = "皮重(KG)", VisibleIndex = 4, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareTime", FieldName = "TareTime", Caption = "皮重时间", VisibleIndex = 5, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundTareName", FieldName = "PoundTareName", Caption = "地磅名", VisibleIndex = 6, Visible = Enabled });
            gridView.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridView.OptionsView.ColumnAutoWidth = true;
        }
        /// <summary>
        /// 焦点离开Text时发生的事件
        /// </summary>
        /// <param name="tb"></param>
        private void TextMove(TextBox tb)
        {
            tb.LostFocus += Tb_LostFocus;
        }

        private void Tb_LostFocus(object sender, EventArgs e)
        {
            if (cbSoyPound.Checked==false)
            {
                ReachArriveManager ram = new ReachArriveManager();
                List<ReachArriveMD> ls = ram.GetReachArrive(tedAtuoCode.Text.Trim());
                if (ls.Count > 0)
                {
                    glueCustomer.EditValue = ls[0].CustID;
                    glueMaterial.EditValue = ls[0].MatID;
                    lbAutoCodeError.Text = "";
                }
                else
                {
                    lbAutoCodeError.Text = tedAtuoCode.Text + "车号有误！";
                }
            }
            else
            {
                switch (tedAtuoCode.Text.Trim())
                {
                    case "1":
                        tedAtuoCode.Text = "桂E10101";
                        break;
                    case "2":
                        tedAtuoCode.Text = "桂E10111";
                        break;
                    case "3":
                        tedAtuoCode.Text = "桂E11105";
                        break;
                    case "4":
                        tedAtuoCode.Text = "桂E11151";
                        break;
                    case "5":
                        tedAtuoCode.Text = "桂E11079";
                        break;
                    case "6":
                        tedAtuoCode.Text = "桂E11065";
                        break;
                    case "7":
                        tedAtuoCode.Text = "桂E10795";
                        break;
                    case "8":
                        tedAtuoCode.Text = "桂E11075";
                        break;
                    case "9":
                        tedAtuoCode.Text = "桂N07989";
                        break;
                    case "10":
                        tedAtuoCode.Text = "桂E05766";
                        break;
                    case "11":
                        tedAtuoCode.Text = "桂AB9801";
                        break;
                    case "12":
                        tedAtuoCode.Text = "桂N62906";
                        break;
                    case "13":
                        tedAtuoCode.Text = "桂E08370";
                        break;
                    case "14":
                        tedAtuoCode.Text = "桂E12201";
                        break;
                    case "15":
                        tedAtuoCode.Text = "桂N35828";
                        break;
                    case "16":
                        tedAtuoCode.Text = "桂E60302";
                        break;
                    case "17":
                        tedAtuoCode.Text = "桂N36061";
                        break;
                    case "18":
                        tedAtuoCode.Text = "桂D12866";
                        break;
                    case "19":
                        tedAtuoCode.Text = "桂P03515";
                        break;
                    case "20":
                        tedAtuoCode.Text = "桂E05080";
                        break;
                    case "21":
                        tedAtuoCode.Text = "桂E03907";
                        break;
                    case "22":
                        tedAtuoCode.Text = "桂N36775";
                        break;
                    default:
                        tedAtuoCode.Text = tedAtuoCode.Text;
                        break;
                }
            }
             
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="bt">保存按钮</param>
        private void SaveButton(Button bt)
        {
            bt.Click += Bt_Click;
        }

        private void Bt_Click(object sender, EventArgs e)
        {
            JudgeIn();
            FirstIn();
            EndOut();            
        }
        /// <summary>
        /// 插入或更新数据方法
        /// </summary>
        private void JudgeIn()
        {
            if (tedAtuoCode.Text.Trim()==string.Empty)
            {
                MessageBox.Show("车号不能为空");
                return;
            }
            if (glueMaterial.EditValue == null)
            {
                glueMaterial.EditValue = 0;
            }
            if (glueCustomer.EditValue == null)
            {
                glueCustomer.EditValue = 0;
            }
            PoundTotalManager ptm = new PoundTotalManager();
            int a = ptm.Judge(tedAtuoCode.Text.Trim(),float.Parse( tedTare.Text.Trim()),poundName,long.Parse( glueMaterial.EditValue.ToString()),long.Parse( glueCustomer.EditValue.ToString()));
            switch (a)
            {
                case 0:
                    lbStatus.Text = "失败";
                    break;
                case 1:
                    lbStatus.Text = "第一次过磅成功";
                    break;
                case 2:
                    lbStatus.Text = "毛重过磅成功";
                    List<PoundTotalMD> lp = new List<PoundTotalMD>();
                    lp = ptm.GetAllPoundList(tedAtuoCode.Text.Trim());
                    if (lp.Count > 0)
                    {
                        tedGross.Text = lp[0].QTYGross.ToString();
                        tedNet.Text = lp[0].QTYNet.ToString();
                        tedTare.Text = lp[0].QTYTare.ToString();
                    }
                    break;
                case 3:
                    lbStatus.Text = "皮重毛重过磅成功";
                    List<PoundTotalMD> lp1 = new List<PoundTotalMD>();
                    lp1 = ptm.GetAllPoundList(tedAtuoCode.Text.Trim());
                    if (lp1.Count > 0)
                    {
                        tedGross.Text = lp1[0].QTYGross.ToString();
                        tedNet.Text = lp1[0].QTYNet.ToString();
                        tedTare.Text = lp1[0].QTYTare.ToString();
                    }
                    break;
                default:
                    break;
            }
            
            
        }
        /// <summary>
        /// 点击Gridview，获取点击行
        /// </summary>
        /// <param name="gv"></param>
        private void GridViewRowSelect(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            gv.RowClick += Gv_RowClick;
        }

        private void Gv_RowClick(object sender, RowClickEventArgs e)
        {
            selectRow = e.RowHandle;            
        }
        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="bt">打印按钮</param>
        private void PrintButton(Button bt)
        {
            bt.Click += Bt_Click4;
        }

        private void Bt_Click4(object sender, EventArgs e)
        {
            Print();
        }

        /// <summary>
        /// 编辑打印模板
        /// </summary>        
        private void  Print()
        {
            List<PoundTotalMD> lpt = this.gcEndControl.DataSource as List<PoundTotalMD>;
            if (selectRow < 0) return;
            PoundTotalMD pt = lpt[selectRow];
            StiReport sr = new StiReport();
            sr.Load("Report.mrt");
            sr.Dictionary.Clear();
            sr.Dictionary.BusinessObjects.Clear();
            sr.RegBusinessObject("SacOutSupervise",pt);
            sr.Dictionary.Synchronize();
            sr.Dictionary.SynchronizeBusinessObjects();
            sr.Compile();
            sr.Show();
            //sr.Print();
            //sr.Design();
        }
        /// <summary>
        /// 编辑打印模板
        /// </summary>
        /// <param name="bt"></param>
        private void DesingnButton(Button bt)
        {
            bt.Click += Bt_Click5;
        }

        private void Bt_Click5(object sender, EventArgs e)
        {
            PrintDesign();
        }

        /// <summary>
        /// 编辑模板
        /// </summary>
        private void PrintDesign()
        {
            List<PoundTotalMD> lpt = this.gcEndControl.DataSource as List<PoundTotalMD>;
            if (selectRow < 0) return;
            PoundTotalMD pt = lpt[selectRow];
            StiReport sr = new StiReport();
            sr.Load("Report.mrt");
            sr.Dictionary.Clear();
            sr.Dictionary.BusinessObjects.Clear();
            sr.RegBusinessObject("SacOutSupervise", pt);
            sr.Dictionary.Synchronize();
            sr.Dictionary.SynchronizeBusinessObjects();
            sr.Design();
        }

        #region 关闭窗体，退出Com串口连接
        /// <summary>
        /// 关闭窗体，退出Com串口连接
        /// </summary>
        /// <param name="fm"></param>
        private void CloseForm(Form fm)
        {
            fm.FormClosing += Fm_FormClosing;
        }

        private void Fm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCom1();
        }




        private void CloseCom1()
        {
            if (!spCom1.IsOpen)
            {
                MessageBox.Show("地磅未链接");
            }
            else
            {
                spCom1.DataReceived -= Sp_DataReceived;
                Thread.Sleep(1000);
                spCom1.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// 复制地磅数到剪切板
        /// </summary>
        /// <param name="bt">复制按钮</param>
        private void CopyPoundWeigth(Button bt)
        {
            bt.Click += Bt_Click2;
        }

        private void Bt_Click2(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(tedPoundWeight.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("空的数据");
                return ;
                //throw;
            }
        }

        #region---------------绑定物料及客户信息----------
        /// <summary>
        /// 绑定物料资料
        /// </summary>
        private void MaterialBinding()
        {
            List<PubMaterialMD> lpm = new List<PubMaterialMD>();
            PubMaterialManager pmm = new PubMaterialManager();
            lpm = pmm.GetPubMaterial();
            this.glueMaterial.Properties.DataSource = lpm;
            this.glueMaterial.Properties.DisplayMember = "Name";
            this.glueMaterial.Properties.ValueMember = "ID";
        }
        /// <summary>
        /// 绑定客户资料
        /// </summary>
        private void CustomerBinding()
        {
            List<PubcustomerMD> lp = new List<PubcustomerMD>();
            PubcustomerManager pcm = new PubcustomerManager();
            lp = pcm.GetPubCustomer();
            this.glueCustomer.Properties.DataSource = lp;
            this.glueCustomer.Properties.DisplayMember = "Name";
            this.glueCustomer.Properties.ValueMember = "ID";
        }

        #endregion

        #region ------------------------地磅重量相关-----------------
     
        /// <summary>
        /// 获取地磅名称
        /// </summary>
        private void GetHostName()
        {
            string HostName = Dns.GetHostName();
            switch (HostName)
            {
                case "DESKTOP-2K9ON4V": 
                    poundName="办公楼";
                    break;
                case "BHDB02":
                    poundName="机修楼";
                    break;
                case "BHDB04":
                    poundName="办公楼";
                    break;
                default:
                    poundName="办公楼";
                    break;
            }
        }
        /// <summary>
        /// 获取地磅数
        /// </summary>
        /// <param name="bt">读数按钮</param>
        private void GetPoundWeight(Button bt)
        {
            bt.Click += Bt_Click1;
        }

        private void Bt_Click1(object sender, EventArgs e)
        {
           // MessageBox.Show(tedPoundWeight.Text.Trim());
            tedTare.Text = tedPoundWeight.Text.Trim(); 
        }

        #region 打开地磅串口
        /// <summary>
        /// 串口打开
        /// </summary>
        private void OpenSerialPort()
        {
            if (!spCom1.IsOpen)
            {
                spCom1.BaudRate = 1200;
                spCom1.StopBits = System.IO.Ports.StopBits.One;
                spCom1.DataBits = 8;
                spCom1.Parity = System.IO.Ports.Parity.None;
                spCom1.PortName = SerialPort.GetPortNames()[0];//串口号默认是第一个，如果需要修改，几个修改成其他的
                spCom1.Open();
                SerialPoundData(spCom1);//初始化串口字符发送事件
            }
            else
            {
                MessageBox.Show("地磅已经链接！");
            }
        }
        /// <summary>
        /// 串口字符发送触发事件
        /// </summary>
        /// <param name="sp"></param>
        private void SerialPoundData(SerialPort sp)
        {
            sp.DataReceived += Sp_DataReceived;
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPortDataReceived();
        }

        /// <summary>
        /// 获取地磅重量
        /// </summary>
        private void SerialPortDataReceived()
        {
            if (spCom1.ReadExisting() == string.Empty)
            {
                //MessageBox.Show("没有数据！");
                return;
            }
            byte[] _byte = new byte[18];
            // float[] f = null;
            while (spCom1.ReadByte().ToString() != "141")
            {
                continue;
            }
            for (int i = 0; i < 17; i++)
            {
                _byte[i] = (byte)spCom1.ReadByte();
            }
            string wd = string.Empty;
            for (int i = 5; i < 11; i++)
            {
                int c = 0;
                if (_byte[i] > 45 && _byte[i] < 59)
                {
                    c = _byte[i] - 48;
                    wd += c;
                }
                else
                {
                    if (_byte[i] - 176 < 0)
                    {
                        c = 0;
                        wd += 0;
                    }
                    else
                    {
                        c = _byte[i] - 176;
                        wd += c;
                    }
                }
            }
            float a = float.Parse(wd);            
            tedPoundWeight.Text = a.ToString();                    
        }
        #endregion



        #endregion

        #region -------------------插入一行数据---------------
        /// <summary>
        /// 插入一行数据
        /// </summary>
        private void SavePoundDate()
        {
           

        } 
        #endregion
    }
}
