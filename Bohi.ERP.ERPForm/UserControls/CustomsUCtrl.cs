using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using DevExpress.XtraSplashScreen;
using System.Xml;
using Bohi.ERP.MODEL;
using Bohi.ERP.BLL;
using Stimulsoft.Report;

namespace Bohi.ERP.ERPForm.UserControls
{
    public partial class CustomsUCtrl : BaseUserControl
    {
        public CustomsUCtrl()
        {
            InitializeComponent();
            LoadSet();
            InitializeSet();
        }
        private void InitializeSet()
        {
            
            cbePrint.QueryPopUp += CbePrint_QueryPopUp;//初始化点击下拉事件
            cbePrint.SelectedValueChanged += CbePrint_SelectedValueChanged;//初始化选择事件
            cbeCom.QueryPopUp += CbeCom_QueryPopUp;//初始化串口下拉事件
            
            sbtnOpenCom.Click += SbtnOpenCom_Click;//初始化打开串口事件
            txtPound.KeyPress += TxtPound_KeyPress;//初始化只能输入数字

            txtAutoCode.EditValueChanged += TxtAutoCode_EditValueChanged;//初始化车号输入框
            sbtnRefresh.Click += SbtnRefresh_Click;//初始化刷新按钮
            sbtnGetPound.Click += SbtnGetPound_Click;//初始化获取地磅数量按钮
            sbtnPrint.Click += SbtnPrint_Click;//初始化打印按钮

            ckAutoRecord.CheckStateChanged += CkAutoRecord_CheckStateChanged; //初始化选择自动过磅
            txbShipName.EditValueChanged += TxbShipName_EditValueChanged;//初始化船号按钮
            tedPound.EditValueChanged += TedPound_EditValueChanged;//初始化重量输入框
            sbtnSave.Click += SbtnSave_Click;//初始化保存按钮
            tedPound.EditValueChanged += TedPound_EditValueChanged1; //初始化重量按钮
            gridView1.RowClick += GridView1_RowClick;//初始化rouw选择
        }

        









        #region -------------<<公用参数>>--------------
        string date = "";
        CustomsMD mD = new CustomsMD();

        #endregion

        #region -----------------<<方法>>------------------
        public static List<PubAutoCodeMD> lpc = null;
        /// <summary>
        /// 获取大豆车信息
        /// </summary>
        private void SetPubAutoCode()
        {
            PubAutoCodeManager pam = new PubAutoCodeManager();
            lpc = pam.GetAllAutoCode();
        }
        /// <summary>
        /// 绑定Gridview，显示厂内车辆信息
        /// </summary>
        private void FirstIn()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();            
            lp = ptm.GetPoundListFirstSoybean();//显示大豆车正在过磅车辆
            for (int i = 0; i < lp.Count; i++)
            {
                for (int j = 0; j < lpc.Count; j++)
                {
                    if (lp[i].AutoCode == lpc[j].autocode)
                    {
                        lp[i].CarNo = lpc[j].Number;
                        break;
                    }
                }
            }
            this.gridControl1.DataSource = lp;
        }
        #endregion
        /// <summary>
        /// 显示大豆最后500次出厂车辆信息
        /// </summary>
        private void EndOut()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();
            lp = ptm.GetSybeanPoundListByTop500();
            this.gridControl2.DataSource = lp;
        }



        #region ----------------<<事件>>--------------
        int rowSelect = -1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            rowSelect = this.gridView1.GetDataSourceRowIndex(e.RowHandle);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnPrint_Click(object sender, EventArgs e)
        {
            PoundTotalMD mD = new PoundTotalMD();
            List<PoundTotalMD> lp = this.gridControl1.DataSource as List<PoundTotalMD>;
            mD = lp[rowSelect];
            PrintSoy(mD);
        }
        /// <summary>
        /// 最后一个赋值重量，触发自动保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TedPound_EditValueChanged1(object sender, EventArgs e)
        {
            if (ckAutoRecord.CheckState == CheckState.Checked)
            {
                if (tedPound.Text.Trim() == "") return;
                if (mD.AutoCode != "" && (mD.InOrOut == "O"||mD.InOrOut=="o"))
                {
                    InsertSybean(mD);
                }
                else if(mD.AutoCode!="" && (mD.InOrOut=="I"||mD.InOrOut=="i"))
                {
                    UpdataSybean(mD);
                }
            }

        }
        /// <summary>
        /// 手动保存信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnSave_Click(object sender, EventArgs e)
        {
            if (txtAutoCode.Text.Trim() == "") return;
            if (mD.AutoCode != "" && (mD.InOrOut == "O"||mD.InOrOut=="o"))
            {
                InsertSybean(mD);
            }
            else
            {
                UpdataSybean(mD);
            }
        }
        /// <summary>
        /// 当检查到有皮重，用毛重-皮重，得到净重，否则不出来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TedPound_EditValueChanged(object sender, EventArgs e)
        {
            if(tedTare.Text.Trim()!="")
            {
              tedNet.Text=  (mD.PountNet - decimal.Parse(tedTare.Text.Trim())).ToString();
            }
        }

        /// <summary>
        /// 输入船名，去除错误信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbShipName_EditValueChanged(object sender, EventArgs e)
        {
            erro.SetError(txbShipName,"");
        }
        /// <summary>
        /// 自动过磅选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkAutoRecord_CheckStateChanged(object sender, EventArgs e)
        {
            if(txbShipName.Text.Trim()=="")
            {
                erro.SetError(txbShipName,"船名不能为空！");                
                txbShipName.Focus();
                ckAutoRecord.Checked = false;
                return;
            }
            if(ckAutoRecord.Checked==true)
            {
                sbtnGetPound.Enabled = false;
                sbtnSave.Enabled = false;
                txtAutoCode.ReadOnly = true;
                glueCust.ReadOnly = true;
                tedTransport.ReadOnly = true;
                glueMaterial.ReadOnly = true;
                txbShipName.ReadOnly = true;
                tedRemark.ReadOnly = true;
            }else
            {
                sbtnGetPound.Enabled = true;
                sbtnSave.Enabled = true;
                txtAutoCode.ReadOnly = false;
                glueCust.ReadOnly = false;
                tedTransport.ReadOnly = false;
                glueMaterial.ReadOnly = false;
                txbShipName.ReadOnly = false;
                tedRemark.ReadOnly = false;
            }
        }
        /// <summary>
        /// 手动获取地磅数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnGetPound_Click(object sender, EventArgs e)
        {
            if (txtPound.Text.Trim() == "") return;
            tedPound.Text = txtPound.Text.Trim();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnRefresh_Click(object sender, EventArgs e)
        {            
            FirstIn();
            EndOut();
        }
        /// <summary>
        /// 车号更新，查找车辆是否已经过毛重，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAutoCode_EditValueChanged(object sender, EventArgs e)
        {
            if (txtAutoCode.Text.Trim() == "") return;           
            lbState.Text = "";
            tedPound.Text = "";
            tedNet.Text = "";
            tedGross.Text = "";
            tedTare.Text = "";
            tedGross.Text = "";
            if (mD.InOrOut == "O"||mD.InOrOut=="o") return;
            FirstIn();
            List<PoundTotalMD> lp = this.gridControl1.DataSource as List<PoundTotalMD>;
            //查询是否是已过皮重，如果已经过皮重，把皮重信息提取出来
            foreach (PoundTotalMD item in lp)
            {
                if (item.AutoCode==txtAutoCode.Text.Trim())
                {
                    tedTare.Text = item.QTYTare.ToString();
                    break;
                }
            }
        }
        /// <summary>
        /// 只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPound_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 打开串口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnOpenCom_Click(object sender, EventArgs e)
        {
            if (sbtnOpenCom.Text=="打开串口")
            {
                OpenSerialPort();
                sbtnOpenCom.Text = "关闭串口";
            }else
            {
                CloseCom1();
                sbtnOpenCom.Text = "打开串口";
            }
        }
        /// <summary>
        /// 选择本地串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbeCom_QueryPopUp(object sender, CancelEventArgs e)
        {
            GetSerialPortNo();
        }

        /// <summary>
        /// 添加打印机到本地种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbePrint_SelectedValueChanged(object sender, EventArgs e)
        {
            WriteTxt();
        }
        /// <summary>
        /// 选择打印机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbePrint_QueryPopUp(object sender, CancelEventArgs e)
        {
            PrintSet(); ;
        }
        #endregion


        /// <summary>
        /// 装载
        /// </summary>
        private void LoadSet()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            SetPubAutoCode();
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(gridView1);
            sg.CustomizeGridView(gridView2);
            AddColumn(gridView1);
            ColumnsAdd(gridView2);
            this.Text = "海关地磅";
            this.bar2.Visible = false;
            ReadTxt();//绑定上次选择的打印机
            FirstIn();
            EndOut();
        }


        #region ----------------------------<<设置>>------------------------
        
        /// <summary>
        /// 手动添加第一次过磅显示Gridview列
        /// </summary>
        /// <param name="gridView"></param>
        private void AddColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            //gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "pur01", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled };
            //gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "No", FieldName = "No", Caption = "No", VisibleIndex = 1, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "CarNo", FieldName = "CarNo", Caption = "车号", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车牌", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "MatName", FieldName = "MatName", Caption = "物料名", VisibleIndex = 3, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYTare", FieldName = "QTYTare", Caption = "皮重(kg)", VisibleIndex = 4, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareTime", FieldName = "TareTime", Caption = "皮重时间", VisibleIndex = 5, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundTareName", FieldName = "PoundTareName", Caption = "地磅名", VisibleIndex = 0, Visible = Enabled });
            gridView.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridView.OptionsView.ColumnAutoWidth = true;
        }
        /// <summary>
        /// 手动添加Gridview列名
        /// </summary>
        /// <param name="gridview"></param>
        private void ColumnsAdd(DevExpress.XtraGrid.Views.Grid.GridView gridview)
        {
            //gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "No", FieldName = "No", Caption = "No", VisibleIndex = 2, Visible = Enabled });
            
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车牌", VisibleIndex = 3, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYTare", FieldName = "QTYTare", Caption = "皮重（kg）", VisibleIndex = 4, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareTime", FieldName = "TareTime", Caption = "皮重时间", VisibleIndex = 5, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundTareName", FieldName = "PoundTareName", Caption = "皮重磅名", VisibleIndex = 6, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYGross", FieldName = "QTYGross", Caption = "毛重（kg）", VisibleIndex = 7, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "GrossTime", FieldName = "GrossTime", Caption = "毛重时间", VisibleIndex = 8, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYNet", FieldName = "QTYNet", Caption = "统计（kg）", VisibleIndex = 9, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "MatName", FieldName = "MatName", Caption = "物料名", VisibleIndex = 10, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Customer", FieldName = "Customer", Caption = "客户名", VisibleIndex = 11, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipName", FieldName = "ShipName", Caption = "船名", VisibleIndex = 12, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipNo", FieldName = "ShipNo", Caption = "船仓号", VisibleIndex = 13, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundGrossName", FieldName = "PoundGrossName", Caption = "毛重磅名", VisibleIndex = 14, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareStfName", FieldName = "TareStfName", Caption = "皮重司磅员", VisibleIndex = 8, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "GrossStfName", FieldName = "GrossStfName", Caption = "毛重司磅员", VisibleIndex = 15, Visible = Enabled });
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "IsManual", FieldName = "IsManual", Caption = "是否为手动磅重", VisibleIndex = 16, Visible = Enabled });
            gridview.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridview.Columns["GrossTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";

        }
        /// <summary>
        /// 串口打开
        /// </summary>
        private void OpenSerialPort()
        {
            try
            {
                if (!spCom1.IsOpen)
                {
                    spCom1.BaudRate = 9600;
                    spCom1.StopBits = System.IO.Ports.StopBits.One;
                    spCom1.DataBits = 8;
                    spCom1.Parity = System.IO.Ports.Parity.None;
                    spCom1.PortName = cbeCom.Text.Trim();//串口号默认是第一个，如果需要修改，几个修改成其他的
                    spCom1.Open();
                    SerialPoundData(spCom1);//初始化串口字符发送事件
                    spCom1.Encoding = System.Text.Encoding.GetEncoding("GB2312");
                }
                else
                {
                    MessageBox.Show("地磅已经链接！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            try
            {
                while (true)
                {
                    string aaa = spCom1.ReadExisting();
                    if (aaa.Length == 0) break;
                    if (aaa.Length >= 8 && aaa.Substring(aaa.Length - 8, 8) =="R_INFO>")
                    {
                        date += aaa;
                        break;
                    }
                    date += aaa;
                    aaa = "";
                }
                
                if (date.Substring(date.Length - 8, 8).Trim() == "R_INFO>")
                {
                    
                    DataBinding(date);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        
        /// <summary>
        /// 根据收到的串口Xml信息，进行数据处理
        /// </summary>
        /// <param name="data"></param>
        public void DataBinding(string data)
        {

            try
            {
                
                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(data);
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                XmlNode root = xml.DocumentElement;
               // XmlNode isInOrOut = root.SelectSingleNode("/GATHER_INFO/I_E_FLAG");
               // XmlNode isInOrOut = xml.SelectSingleNode("/GATHER_INFO/I_E_FLAG");//进出标志            
                mD.InOrOut = root.SelectSingleNode(@"/GATHER_INFO/I_E_FLAG").FirstChild.Value;                
                this.Invoke(new Action(() =>
                {
                    txtAutoCode.Text = "";
                    if (mD.InOrOut == "I"||mD.InOrOut=="I")//测试用
                    {
                        piceIn.Image = Image.FromFile(Environment.CurrentDirectory + "\\imge\\in.png");
                        piceOut.Image = Image.FromFile(Environment.CurrentDirectory + "\\imge\\null.png");
                    }
                    else if (mD.InOrOut == "O"||mD.InOrOut=="o")
                    {
                        piceOut.Image = Image.FromFile(Environment.CurrentDirectory + "\\imge\\out.png");
                        piceIn.Image = Image.FromFile(Environment.CurrentDirectory + "\\imge\\null.png");
                    }                    
                    //XmlNode VE_LICENSE_NO = root.SelectSingleNode("/GATHER_INFO/VE_LICENSE_NO");//车牌号
                    mD.AutoCode = root.SelectSingleNode(@"/GATHER_INFO/VE_LICENSE_NO").FirstChild.Value;
                    txtAutoCode.Text = mD.AutoCode;                    
                    //XmlNode OPERATE_TIME = root.SelectSingleNode("/GATHER_INFO/OPERATE_TIME");//过磅时间
                    mD.CreateTime = DateTime.Parse(root.SelectSingleNode(@"/GATHER_INFO/OPERATE_TIME").FirstChild.Value);                    
                    //XmlNode VE_WT = root.SelectSingleNode("/GATHER_INFO/VE_RFID/VE_WT");//重量
                    mD.PountNet = decimal.Parse(root.SelectSingleNode(@"/GATHER_INFO/VE_RFID/VE_WT").FirstChild.Value);
                    txtPound.Text = mD.PountNet.ToString();
                    tedPound.Text = mD.PountNet.ToString();                    
                }));
                
            }
            catch (Exception EX)
            {                
                MessageBox.Show(EX.Message); ;
            }
            finally
            {
                date = "";
            }
            

        }
        

        #region-- 关闭窗体，退出Com串口连接 --
        /// <summary>
        /// 关闭窗体，退出Com串口连接
        /// </summary>
        /// <param name="fm"></param>        

        private void closeclose()
        {
            this.Load += PoundDUCtrl_Load;
        }

        private void PoundDUCtrl_Load(object sender, EventArgs e)
        {
            this.ParentForm.FormClosing += ParentForm_FormClosing;
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCom1();
        }

        private void CloseCom1()
        {
            if (!spCom1.IsOpen)
            {
                // MessageBox.Show("地磅未链接");
                return;
            }
            else
            {
                SplashScreenManager.ShowDefaultWaitForm();
                spCom1.DataReceived -= Sp_DataReceived;
                Thread.Sleep(1000);
                spCom1.Dispose();
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }
        #endregion
        /// <summary>
        /// 获取电脑上的所有串口
        /// </summary>
        private void GetSerialPortNo()
        {
            cbeCom.Properties.Items.Clear();
            if (SerialPort.GetPortNames().Count() < 1) return;
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                this.cbeCom.Properties.Items.Add(vPortName);
            }
            this.cbeCom.SelectedIndex = 0;
        }

        /// <summary>
        /// 列出所有本机的打印机
        /// </summary>
        private void PrintSet()
        {
            cbePrint.Properties.Items.Clear();
            foreach (string sprint in PrinterSettings.InstalledPrinters)
            {
                cbePrint.Properties.Items.Add(sprint);
            }
        }
        /// <summary>
        /// 打印磅单（大豆）
        /// </summary>   
        private void PrintSoy(PoundTotalMD pt)
        {
            try
            {                
                StiReport sr = new StiReport();
                sr.Load("ReportSoy.mrt");
                sr.Dictionary.Clear();
                sr.Dictionary.BusinessObjects.Clear();
                sr.RegBusinessObject("SacOutSupervise", pt);
                sr.Dictionary.Synchronize();
                sr.Dictionary.SynchronizeBusinessObjects();
                sr.Compile();
                sr.PrinterSettings.Collate = true;
                //sr.Print(false, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbePrint.Text });
                sr.PrinterSettings.PrinterName=  cbePrint.Text ;
                sr.PrinterSettings.ShowDialog = false;
                //sr.PrinterSettings.Collate = true;
                sr.Compile();
                sr.Print();
                //sr.Show();
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }
        /// <summary>
        /// 从文本中读取打印机名字
        /// </summary>
        private void ReadTxt()
        {
            string str;
            StreamReader sr = new StreamReader(Environment.CurrentDirectory + "\\setPrint.txt", false);
            str = sr.ReadToEnd().ToString();
            sr.Close();
            cbePrint.Text = str;
        }
        /// <summary>
        /// 把打印机名字写到文本中
        /// </summary>
        private void WriteTxt()
        {
            string str = cbePrint.Text.Trim();
            StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\setPrint.txt", false);
            sw.WriteLine(str);
            sw.Close();
        }

        #endregion

        #region -----------------------<<方法>>-------------------
        /// <summary>
        /// 更新毛重信息
        /// </summary>
        /// <param name="cm"></param>
        private void UpdataSybean(CustomsMD cm)
        {
            PoundTotalManager pm = new PoundTotalManager();
            PoundTotalMD pt = new PoundTotalMD();
            List<PoundTotalMD> lpt = pm.GetPoundList(cm.AutoCode);
            if(lpt.Count<1)
            {
                lbState.Text = cm.AutoCode+"  车辆未过皮重，无法更新毛重信息，请手工输入皮重信息！";
                lbState.ForeColor = Color.Red;
                return;
            }
            pt.IsFinished = true;
            pt.ID = lpt[0].ID;
            pt.No = lpt[0].No;
            pt.QTYTare = lpt[0].QTYTare;
            pt.QTYNet = lpt[0].QTYNet;
            pt.ShipName = lpt[0].ShipName;
            pt.Remark = lpt[0].Remark;
            pt.TareTime = lpt[0].TareTime;
            pt.TransportCo = lpt[0].TransportCo;
            pt.Customer = lpt[0].Customer;
            pt.AutoCode = lpt[0].AutoCode;
            pt.MatName = lpt[0].MatName;
            pt.PrintStfName = LoginFrm.pubDelIn.Name;
            pt.PoundGrossName = "海关地磅 " + "进";
            pt.PrintTime = DateTime.Now;
            pt.QTYGross = cm.PountNet;
            pt.QTYNet = Decimal.Parse(tedNet.Text.Trim());
            pt.GrossTime = cm.CreateTime;                     
            if (ckAutoRecord.Tag == null)
            {
                pt.IsManual = false;
            }
            else
            {
                pt.IsManual = true;
            }
            if (ckAutoRecord.Checked == true)
            {
                pt.GrossStfName = "自动过磅";
            }
            else
            {
                pt.GrossStfName = LoginFrm.pubDelIn.Name;
                pt.GrossStfID = LoginFrm.pubDelIn.ID;
            }
            if (pm.UpdataSobearnGross(pt) == true)
            {
                lbState.Text = cm.AutoCode + " 车辆毛重过磅成功！";
                lbState.ForeColor = Color.Green;
                txtPound.Text = "";
                tedPound.Text = "";
                txtAutoCode.Text = "";
                FirstIn();
                EndOut();
                PrintSoy(pt);

            }
            else
            {
                lbState.Text = cm.AutoCode + " 车辆毛重过磅失败！";
                lbState.ForeColor = Color.Red;
            }
        }
        /// <summary>
        /// 插入皮重信息
        /// </summary>
        /// <param name="cm"></param>
        private void InsertSybean(CustomsMD cm)
        {
            PoundTotalManager pm1 = new PoundTotalManager();
            List<PoundTotalMD> lpt = pm1.GetPoundList(cm.AutoCode);
            if (lpt.Count >0)
            {
                lbState.Text = cm.AutoCode + "  车辆未过毛重，无法更新皮重信息，请手工输入毛重信息！";
                lbState.ForeColor = Color.Red;
                return;
            }
            PoundTotalManager pm = new PoundTotalManager();
            PoundTotalMD pt = new PoundTotalMD();
            pt.AutoCode = cm.AutoCode;
            pt.QTYTare = cm.PountNet;
            pt.TareTime = cm.CreateTime;
            pt.PoundTareName = "海关地磅 " + "出";            
            pt.Customer = glueCust.Text.Trim();
            pt.DeliveryCo = tedTransport.Text.Trim();
            pt.IsDelete = false;
            pt.IsFinished = false;
            if (ckAutoRecord.Tag==null)
            {
                pt.IsManual = false;
            }
            else
            {
                pt.IsManual = true;
            }
            pt.IsPrint = false;
            pt.IsSoybean = true;
            pt.MatName = glueMaterial.Text.Trim();
            pt.Remark = tedRemark.Text.Trim();
            pt.ShipName = txbShipName.Text.Trim();
            if (ckAutoRecord.Checked==true)
            {
                pt.TareStfName = "自动过磅";
            }else
            {
                pt.TareStfName = LoginFrm.pubDelIn.Name;
               // pt.TareStfID = LoginFrm.pubDelIn.ID;
            }
            pt.TransportCo = tedTransport.Text.Trim();
            if( pm.InsertSobean(pt)==true)
            {
                lbState.Text = cm.AutoCode+" 车辆皮重过磅成功！";                
                lbState.ForeColor = Color.Green;
                txtPound.Text = "";
                tedPound.Text = "";
                txtAutoCode.Text = "";
                //date = "";
                FirstIn();
                EndOut();
               // mD = null;
            }
            else
            {
                lbState.Text = cm.AutoCode + " 车辆皮重过磅成功！";
                lbState.ForeColor = Color.Red;
            }
        }
        #endregion
    }
}
