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
using System.IO;
using System.IO.Ports;
using System.Net;
using Stimulsoft.Report;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;

namespace Bohi.ERP.ERPForm.UserControls
{
    public partial class SoybeanInStoreUCtrl : BaseUserControl
    {
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_open_usb_device();
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_close_usb_device();
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_open_jdq1();
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_open_jdq2();
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_close_jdq1();
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_close_jdq2();
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_get_d1_d2_and_jdq_status(ref byte buf);
        [DllImport("xq_usb_hid.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int xq_get_a1_a2(ref int a1, ref int a2, ref float fa1, ref float fa2);
        public SoybeanInStoreUCtrl()
        {
            InitializeComponent();
            FrmSet();
            Initialize_Tool();
        }

        #region 加载设置
        /// <summary>
        /// 加载设置
        /// </summary>
        private void FrmSet()
        {
            GetShortKeyNameMDs();//页面加载，绑定皮重车辆信息
            SetPubAutoCode();
            SetGricviewDisplay();
            AddTareColumn(this.gridView1);
            AddGrossColumn(this.gridView2);
            TareBinding();
            GrossBinding();                       
            sbtnBack.Enabled = false;           
            sbtnEnterT.Enabled = false;
            sbtnEnterG.Enabled = false;
            this.Text = "3S大豆录入";
            GetHostName();//过去地磅名
            tedDelivery.Text = "广西渤海农业发展有限公司";
            tedRemarks.Text = "转基因大豆";
            tedTransportCo.Text = "广西北海港物流有限公司";
        }
        #endregion


        #region 初始化按钮事件
        /// <summary>
        /// 初始化按钮事件
        /// </summary>
        private void Initialize_Tool()
        {
            TareGridviewRowClick();//初始化皮重gridview点击行事件
            GrossGridviewRowClick();//初始化毛重Gridview点击行事件
            GrossEnter();//初始化毛重确认按钮
            TareEnter();//初始化皮重确认按钮
            RefreshTare();//刷新皮重输入框
            RefreshGross();//刷新毛重输入框
            GrossTxtChange();//获取车辆净重
            PoundShow();//显示地磅数据
            GetTarePound();//初始化获取皮重
            GetGrossPound();//初始化获取毛重            
            AutoShipGTextChange();//初始化车号输入框（毛重）
            ErroClearn();//初始化车号为空错误
            ErroClearnTarePound();//初始化地磅数为空的错误
            ErroClearnGrossPound();//初始化地磅数为空错误
            ErroClearnGrossAutoCode();//初始化毛重车号错误提示
            BackNexButton();//初始化退回上一步按钮
            tedQTYTare.KeyPress += TedQTYTare_KeyPress;//只能输入数字
            tedQTYGrossG.KeyPress += TedQTYGrossG_KeyPress;//只能输入数字
            sbtnOpenLight.Click += SbtnOpenLight_Click;//初始化开灯
            tmOpenLinght.Tick += TmOpenLinght_Tick;//初始化关灯
            sbtnCheckNet.Click += SbtnCheckNet_Click;//测试网络是否通顺
        }

        private void SbtnCheckNet_Click(object sender, EventArgs e)
        {
            PingIp();
        }

        private void TmOpenLinght_Tick(object sender, EventArgs e)
        {
            try
            {
                // MessageBox.Show("sdfsdf");
                if (xq_close_jdq1() > 0)//关灯
                    lbState.Text = "";
                else
                    lbErroState.Text = "关灯失败";
            }
            catch (Exception)
            {

            }
            tmOpenLinght.Enabled = false;
        }

        private void SbtnOpenLight_Click(object sender, EventArgs e)
        {
            try
            {
                if (xq_open_jdq1() > 0)//开灯
                    lbState.Text = "开灯";
                else
                    lbErroState.Text = "开灯失败";
            }
            catch (Exception)
            {


            }
            tmOpenLinght.Enabled = true;
        }





        #endregion

        #region 公用常量
        private static int tareRow=-1;
        private static int grossRow = -1;
        private static string poundName = "";
        #endregion


        #region  ---------------------初始化事件--------------

        /// <summary>
        /// 毛重输入框只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TedQTYGrossG_KeyPress(object sender, KeyPressEventArgs e)
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
        /// 皮重输入框只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TedQTYTare_KeyPress(object sender, KeyPressEventArgs e)
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
        /// 毛重表中上一步事件
        /// </summary>
        private void BackNexButton()
        {
            sbtnBack.Click += SbtnBack_Click;
        }

        private void SbtnBack_Click(object sender, EventArgs e)
        {
            BackNext();
        }

        /// <summary>
        /// 清除地磅数量为空的错误
        /// </summary>
        private void ErroClearnGrossAutoCode()
        {
            tedAutoCodeG.TextChanged += TedAutoCodeG_TextChanged;
        }

        private void TedAutoCodeG_TextChanged(object sender, EventArgs e)
        {
            dxError.SetError(tedAutoCodeG, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
        }

        /// <summary>
        /// 清除地磅数量为空的错误
        /// </summary>
        private void ErroClearnGrossPound()
        {
            tedQTYGrossG.TextChanged += TedQTYGrossG_TextChanged1;
        }

        private void TedQTYGrossG_TextChanged1(object sender, EventArgs e)
        {
            dxError.SetError(tedQTYGrossG, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
        }

        /// <summary>
        /// 清除地磅数量为空的错误（皮重）
        /// </summary>
        private void ErroClearnTarePound()
        {
            tedQTYTare.TextChanged += TedQTYTare_TextChanged;
        }

        private void TedQTYTare_TextChanged(object sender, EventArgs e)
        {
            dxError.SetError(tedQTYTare, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
        }

        

        /// <summary>
        /// 清除车号为空的错误（毛重）
        /// </summary>
        private void ErroClearn()
        {
            tedAutoCode.TextChanged += TedAutoCode_TextChanged;
        }

        private void TedAutoCode_TextChanged(object sender, EventArgs e)
        {
            dxError.SetError(tedAutoCode, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
        }

        
        /// <summary>
        /// 毛重过滤框，过滤车号（毛重）
        /// </summary>
        private void AutoShipGTextChange()
        {
            tedAutoShipG.TextChanged += TedAutoShipG_TextChanged;
        }

        private void TedAutoShipG_TextChanged(object sender, EventArgs e)
        {
            this.gridView2.FindFilterText = tedAutoShipG.Text.Trim();
        }

        /// <summary>
        /// 从读取地磅数字到毛重
        /// </summary>
        private void GetGrossPound()
        {
            sbtnReadPoundG.Click += SbtnReadPoundG_Click;
        }

        private void SbtnReadPoundG_Click(object sender, EventArgs e)
        {
            lbState.Text = "";
            if (tedPoundG.Text.Trim()==string.Empty)
            {
                lbState.Text = "请在“托利多”页面连接地磅！";
                return;
            }
            tedQTYGrossG.Text = (Decimal.Parse(tedPoundG.Text)/1000).ToString();
        }

        /// <summary>
        /// 从读取地磅数字到皮重
        /// </summary>
        private void GetTarePound()
        {
            sbtnReadPoundT.Click += SbtnReadPound_Click;            
        }

        private void SbtnReadPound_Click(object sender, EventArgs e)
        {
            lbState.Text = "";
            if (tedPound.Text.Trim() == string.Empty)
            {
                lbState.Text = "请在“托利多”页面连接地磅！";
                return;
            }
            tedQTYTare.Text =(Decimal.Parse( tedPound.Text)/1000).ToString();
        }

        /// <summary>
        /// 实时显示地磅数据
        /// </summary>
        private void PoundShow()
        {
            timer1.Tick += Timer1_Tick;            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                tedPound.Text = PoundDUCtrl.poundNomber;
                tedPoundG.Text = PoundDUCtrl.poundNomber;
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// 输入毛重后，获取净重量
        /// </summary>
        /// 
        private void GrossTxtChange()
        {
            tedQTYGrossG.TextChanged += TedQTYGrossG_TextChanged;
        }

        private void TedQTYGrossG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tedQTYNetG.Text = (decimal.Parse(tedQTYGrossG.Text.Trim()) - decimal.Parse(tedQTYTareG.Text.Trim())).ToString();
                
            }
            catch (Exception)
            {                
            }
        }

        /// <summary>
        /// 刷新毛重显示窗口
        /// </summary>
        private void RefreshGross()
        {
            sbtnRefelashG.Click += SbtnRefelashG_Click;
        }

        private void SbtnRefelashG_Click(object sender, EventArgs e)
        {
            tedAutoCode.Text = "";
            tedQTYTare.Text = "";
            tedAutoCodeG.Text = "";
            tedQTYTareG.Text = "";
            tedQTYGrossG.Text = "";
            tedQTYNetG.Text = "";
            lbState.Text = "";
            GrossBinding();
            TareBinding();
        }

        /// <summary>
        /// 刷新皮重显示窗口
        /// </summary>
        private void RefreshTare()
        {
            sbtnRefresh.Click += SbtnRefresh_Click;
        }

        private void SbtnRefresh_Click(object sender, EventArgs e)
        {
            tedAutoCode.Text = "";
            tedQTYTare.Text = "";
            tedAutoCodeG.Text = "";
            tedQTYTareG.Text = "";
            tedQTYGrossG.Text = "";
            tedQTYNetG.Text = "";
            lbState.Text = "";
            TareBinding();//绑定皮重显示窗口
            GrossBinding();
        }

        /// <summary>
        /// 皮重输入按钮事件
        /// </summary>
        private void TareEnter()
        {
            sbtnEnterT.Click += SbtnEnter_Click;
        }

        private void SbtnEnter_Click(object sender, EventArgs e)
        {
            if(tedAutoCode.Text.Trim()==string.Empty)
            {
                dxError.SetError(tedAutoCode,"车号不能为空！");
                tedAutoCode.Focus();
                return;
            }
            if (tedQTYTare.Text.Trim() == string.Empty)
            {
                dxError.SetError(tedQTYTare, "重量不能为空！");
                tedQTYTare.Focus();
                return;
            }
            UpdataQTYTare();
            tedAutoCode.Text = string.Empty;
        }

        /// <summary>
        /// 毛重输入按钮事件
        /// </summary>
        private void GrossEnter()
        {
            sbtnEnterG.Click += SbtnEnterG_Click;
        }

        private void SbtnEnterG_Click(object sender, EventArgs e)
        {
            if (tedAutoCodeG.Text.Trim() == string.Empty)
            {
                dxError.SetError(tedAutoCodeG, "车号不能为空！");
                tedAutoCodeG.Focus();
                return;
            }
            if (tedQTYGrossG.Text.Trim() == string.Empty)
            {
                dxError.SetError(tedQTYGrossG, "重量不能为空！");
                tedQTYGrossG.Focus();
                return;
            }
            UpdataQTYGross();
            tedAutoCodeG.Text = string.Empty;
        }

        /// <summary>
        /// 点击皮重Gridview列，获取列行号
        /// </summary>
        private void TareGridviewRowClick()
        {
            this.gridView1.RowClick += GridView1_RowClick;
        }
        /// <summary>
        /// 获取列的行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            tareRow = this.gridView1.GetDataSourceRowIndex(e.RowHandle);
            GetTareRowData();
            lbState.Text = "";
            lbErroState.Text = "";
        }
        /// <summary>
        /// 点击皮重Gridview列，获取列行号
        /// </summary>
        private void GrossGridviewRowClick()
        {
            this.gridView2.RowClick += GridView2_RowClick;
        }
        /// <summary>
        /// 获取列的行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            grossRow = this.gridView2.GetDataSourceRowIndex(e.RowHandle);
            GetGrossRowData();
            lbState.Text = "";
            lbErroState.Text = "";
        }




        #endregion

        #region-----------------------方法--------------------
        //页面加载定义快捷键
        private void GetShortKeyNameMDs()
        {
            PubMatShortKeyManager pubMatShortKeyManager = new PubMatShortKeyManager();
            List<MatShortKeyMD> sk = new List<MatShortKeyMD>();
            sk = pubMatShortKeyManager.GetSoybeanMatShortKey();
            if(sk.Count>0)
            {
                txtDeliveryT.Text = sk[0].Customer;
                txtTransportCoT.Text = sk[0].TransportCo;
                txtMatNameT.Text = sk[0].Name;
                //txtShipNameT.Text = sk[0].ShipAndVoyage;
                txtRemT.Text = sk[0].Remarks;
            }
        }
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
        /// 从文本中读取打印机名字
        /// </summary>
        private string ReadTxt()
        {
            string str;
            StreamReader sr = new StreamReader(Environment.CurrentDirectory + "\\setPrint.txt", false);
            str = sr.ReadToEnd().ToString();
            sr.Close();
            return str;
        }
        /// <summary>
        /// 打印磅单（大豆）
        /// </summary>   
        private void PrintSoy(PoundTotalMD pt)
        {
            try
            {
                StiReport sr = new StiReport();
                sr.Load("Report.mrt");
                sr.Dictionary.Clear();
                sr.Dictionary.BusinessObjects.Clear();
                sr.RegBusinessObject("SacOutSupervise", pt);
                sr.Dictionary.Synchronize();
                sr.Dictionary.SynchronizeBusinessObjects();
                sr.PrinterSettings.PrinterName = ReadTxt();
                sr.Compile();
                //sr.Design();
                // sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = ReadTxt() });
                //sr.Show();
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }
        /// <summary>
        /// 打印磅单（大豆）
        /// </summary>   
        private void EditPrintSoy()
        {
            PoundTotalMD pt=new PoundTotalMD();
            try
            {
                StiReport sr = new StiReport();
                sr.Load("Report.mrt");
                sr.Dictionary.Clear();
                sr.Dictionary.BusinessObjects.Clear();
                sr.RegBusinessObject("SacOutSupervise", pt);
                sr.Dictionary.Synchronize();
                sr.Dictionary.SynchronizeBusinessObjects();
                sr.PrinterSettings.PrinterName = ReadTxt();
                sr.Compile();
                sr.Design();
                // sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = ReadTxt() });
                //sr.Show();
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }
        /// <summary>
        /// 更新车辆皮重信息
        /// </summary>
        private void UpdataQTYTare()
        {
            if (tareRow < 0) return;
            var TareOboject = this.gridView1.DataSource as List<SoybeanAutoCodeMD>;
            SoybeanAutoCodeManager sacm = new SoybeanAutoCodeManager();
            SoybeanAutoCodeMD sac = TareOboject[tareRow];
            long stfID=0;
            if (LoginFrm.pubDelIn.StfNameID!=null)
            {
                stfID =long.Parse( LoginFrm.pubDelIn.StfNameID.ToString());
            }
            sac.TareTime = DateTime.Now;
            sac.QtyTare = decimal.Parse(tedQTYTare.Text.Trim());
            TarePoundNameMD ptn = new TarePoundNameMD();
            ptn.IDFrom = sac.PIPID;
            ptn.TarePoundName = poundName;
            ptn.TareStfName = LoginFrm.pubDelIn.Name;
            if (sacm.TranTareAndTarnPound(decimal.Parse(tedQTYTare.Text.Trim()),stfID, DateTime.Now, long.Parse(sac.PIPID.ToString()),ptn) == true)
            {
                lbState.Text = "";
                lbState.ForeColor = Color.Black;
                lbState.Text+="3S皮重更新成功！";
            }
            else
            {
                lbState.Text = "";
                lbState.ForeColor = Color.Red;
                lbState.Text += "3S皮重更新失败";
            }
            TareBinding();
            GrossBinding();
            tedQTYTare.Text = "";
            tedAutoCode.Text = "";
        }
        /// <summary>
        /// 上一步，（删除根据ID删除皮重信息，且删除TarePoundName表中的信息）
        /// </summary>
        private void BackNext()
        {
            if (grossRow < 0) return;
            var TareOboject = this.gridView2.DataSource as List<SoybeanAutoCodeMD>;
            SoybeanAutoCodeManager sacm = new SoybeanAutoCodeManager();
            SoybeanAutoCodeMD sac = TareOboject[grossRow];            
            
            if (sacm.BackNext(long.Parse(sac.PIPID.ToString()))==true)
            {
                lbState.Text = "";
                lbState.ForeColor = Color.Black;
                lbState.Text += "3S皮重退回上一步成功！";
            }
            else
            {
                lbState.Text = "";
                lbState.ForeColor = Color.Red;
                lbState.Text += "3S皮重退回失败";
            }
            TareBinding();
            GrossBinding();
            tedQTYTareG.Text = "";
            tedAutoShipG.Text = "";
            tedAutoCodeG.Text = "";            
        }
        /// <summary>
        /// 更新车辆毛重信息
        /// </summary>
        private void UpdataQTYGross()
        {
            if (grossRow < 0) return;
            var GrossOboject = this.gridView2.DataSource as List<SoybeanAutoCodeMD>;
            SoybeanAutoCodeManager sacm = new SoybeanAutoCodeManager();
            SoybeanAutoCodeMD sac = GrossOboject[grossRow];
            long stfID = 0;
            if (LoginFrm.pubDelIn.StfNameID!=null)
            {
                stfID = long.Parse(LoginFrm.pubDelIn.StfNameID.ToString());
            }
            sac.GrossTime = DateTime.Now;
            sac.QtyGross =decimal.Parse(tedQTYGrossG.Text.Trim());
            if (sacm.UpdataQTYGross(decimal.Parse(tedQTYGrossG.Text.Trim())/1000,stfID,DateTime.Now,long.Parse(sac.PIPID.ToString())) ==true)
            {
                lbState.Text = "";
                lbState.ForeColor = Color.Black;
                lbState.Text +=tedAutoCodeG.Text + "  车辆毛重更新成功！"+ "\n\t" + " 皮重"+ tedQTYTareG.Text + "  (kg)";
            }
            else
            {
                lbState.Text = "";
                lbState.ForeColor = Color.Red;
                lbState.Text += tedAutoCodeG.Text + "  车辆毛重更新失败！";
                return;
            }
            if (InsertTLD() == true)
            {
                lbState.ForeColor = Color.Black;
                lbState.Text +="\n\t"+ "托利多数据更新成功！";
            }
            else
            {
                lbState.ForeColor = Color.Red;
                lbState.Text += "\n\t" + "托利多数据更新失败！";
            }
            TareBinding();
            GrossBinding();
            tedAutoCodeG.Text = "";
            tedQTYGrossG.Text = "";
        }
        /// <summary>
        /// 点击皮重列表，获取车号
        /// </summary>
        private void GetTareRowData()
        {
            if (tareRow < 0) return;
            var selectObject = this.gridControl1.DataSource as List<SoybeanAutoCodeMD>;
            tedAutoCode.Text=selectObject[tareRow].AutoCode ;
            txtShipNameT.Text = selectObject[tareRow].ShipName;
        }
        /// <summary>
         /// 点击毛重列表，获取车号
         /// </summary>
        private void GetGrossRowData()
        {
            if (grossRow < 0) return;
            var selectObject = this.gridControl2.DataSource as List<SoybeanAutoCodeMD>;
            tedAutoCodeG.Text = selectObject[grossRow].AutoCode;
            decimal a = 0;
            decimal.TryParse(selectObject[grossRow].QtyTare.ToString(),out a);
            Decimal b= decimal.Round(a,2);
            if (tedQTYGrossG.Text.Trim()!=string.Empty)
            {
                tedQTYNetG.Text=(decimal.Parse(tedQTYGrossG.Text.Trim())- b).ToString();
            }
            tedQTYTareG.Text = b.ToString();
            tedMatName.Text = "大豆";
            tedShipName.Text = selectObject[grossRow].ShipName;
        }
        /// <summary>
        /// 加载Gridview显示
        /// </summary>
        private void SetGricviewDisplay()
        {
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(this.gridView1);
            sg.CustomizeGridView(this.gridView2);
            
        }
        /// <summary>
        /// 手动添加皮重过磅显示Gridview列
        /// </summary>
        /// <param name="gridView"></param>
        private void AddTareColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            //gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "pur01", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled };
            //gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "BilNo", FieldName = "BilNo", Caption = "采购订单", VisibleIndex = 1, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "CarNo", FieldName = "CarNo", Caption = "车号", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车牌号", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipName", FieldName = "ShipName", Caption = "船号", VisibleIndex = 3, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QtyTare", FieldName = "QtyTare", Caption = "皮重(kg)", VisibleIndex = 4, Visible = Enabled });
           // gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Qty", FieldName = "Qty", Caption = "采购数量（kg）", VisibleIndex = 5, Visible = Enabled });
            //gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QtyTo", FieldName = "QtyTo", Caption = "已交数量", VisibleIndex = 6, Visible = Enabled });
            //gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QtyUnRece", FieldName = "QtyUnRece", Caption = "未交数量", VisibleIndex = 7, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "SealsTime", FieldName = "SealsTime", Caption = "铅封时间", VisibleIndex = 8, Visible = Enabled });           
            gridView.Columns["SealsTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridView.OptionsView.ColumnAutoWidth = true;
            //gridView.Columns["QtyTo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gridView.Columns["QtyTo"].DisplayFormat.FormatString = "{0:N2}";
            gridView.Columns["QtyTare"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["QtyTare"].DisplayFormat.FormatString = "{0:N2}";
            //gridView.Columns["Qty"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gridView.Columns["Qty"].DisplayFormat.FormatString = "{0:N2}";
            //gridView.Columns["QtyUnRece"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gridView.Columns["QtyUnRece"].DisplayFormat.FormatString = "{0:N2}";
        }
        /// <summary>
        /// 手动添加毛重过磅显示Gridview列
        /// </summary>
        /// <param name="gridView"></param>
        private void AddGrossColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {            
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "BilNo", FieldName = "BilNo", Caption = "采购订单", VisibleIndex = 1, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "CarNo", FieldName = "CarNo", Caption = "车号", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车牌号", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipName", FieldName = "ShipName", Caption = "船号", VisibleIndex = 3, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QtyTare", FieldName = "QtyTare", Caption = "皮重(kg)", VisibleIndex = 4, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareTime", FieldName = "TareTime", Caption = "皮重时间", VisibleIndex = 5, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QtyGross", FieldName = "QtyGross", Caption = "毛重（kg）", VisibleIndex = 5, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Qty", FieldName = "Qty", Caption = "采购数量", VisibleIndex = 6, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QtyTo", FieldName = "QtyTo", Caption = "已交数量", VisibleIndex = 6, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QtyUnRece", FieldName = "QtyUnRece", Caption = "未交数量", VisibleIndex = 7, Visible = Enabled });
           // gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "SealsTime", FieldName = "SealsTime", Caption = "铅封时间", VisibleIndex = 8, Visible = Enabled });
            gridView.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.Columns["QtyTo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["QtyTo"].DisplayFormat.FormatString = "{0:N2}";
            gridView.Columns["Qty"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["Qty"].DisplayFormat.FormatString = "{0:N2}";
            gridView.Columns["QtyUnRece"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["QtyUnRece"].DisplayFormat.FormatString = "{0:N2}";
            gridView.Columns["QtyTare"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["QtyTare"].DisplayFormat.FormatString = "{0:N2}";
        }
        /// <summary>
        /// 绑定皮重显示Gridview方法
        /// </summary>
        private void TareBinding()
        {
            SoybeanAutoCodeManager sacm = new SoybeanAutoCodeManager();
            List<SoybeanAutoCodeMD> lb = new List<SoybeanAutoCodeMD>();
            lb = sacm.GetTareAutoCode();
            for (int i = 0; i < lb.Count; i++)
            {
                for (int j = 0; j < lpc.Count; j++)
                {
                    if (lpc[j].autocode == lb[i].AutoCode)
                    {
                        lb[i].CarNo = lpc[j].Number;
                        break;
                    }
                }
            }
            this.gridControl1.DataSource = lb;
        }
        /// <summary>
        /// 测试网络是否正常
        /// </summary>
        public void PingIp()
        {
            try
            {
                Ping pi = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                string data = "";
                byte[] vs = Encoding.UTF8.GetBytes(data);
                PingReply pingReply = pi.Send("172.168.1.39", 300, vs, options);
                string strinfo = pingReply.Status.ToString();
                if (strinfo == "Success")
                {
                    lbState.Text = "3S服务器测试正常！"+"\r\n";
                    for (int i = 0; i < 9; i++)
                    {
                        PingReply pid= pi.Send("172.168.1.39", 1000, vs, options);
                        lbState.Text += pid.RoundtripTime +",";
                    }
                }
                else
                {
                    lbErroState.Text = "3S服务器测试失败！";
                }
            }
            catch (Exception)
            {
                
            }
        }
        /// <summary>
        /// 绑定皮重显示Gridview方法
        /// </summary>
        private void GrossBinding()
        {
            SoybeanAutoCodeManager sacm = new SoybeanAutoCodeManager();
            List<SoybeanAutoCodeMD> lb = new List<SoybeanAutoCodeMD>();
            lb = sacm.GetGrossAutoCode();
            for (int i = 0; i < lb.Count; i++)
            {
                for (int j = 0; j < lpc.Count; j++)
                {
                    if (lpc[j].autocode == lb[i].AutoCode)
                    {
                        lb[i].CarNo = lpc[j].Number;
                        break;
                    } 
                }
            }
            this.gridControl2.DataSource = lb;
        }

        #region --根据电脑名获取地磅名称--
        /// <summary>
        /// 获取地磅名称
        /// </summary>
        private void GetHostName()
        {
            string HostName = Dns.GetHostName();
            switch (HostName)
            {
                case "DESKTOP-2K9ON4V":
                    poundName = "办公楼";
                    break;
                case "BHDB02":
                    poundName = "机修楼";
                    break;
                case "BHDB04":
                    poundName = "办公楼";
                    break;
                default:
                    poundName = "机修楼";
                    break;
            }            
        }
        #endregion
       
        /// <summary>
        /// 插入托利多数据
        /// </summary>
        /// <returns></returns>
        private bool InsertTLD()
        {
            if (grossRow < 0) return false;
            var soybean = this.gridControl2.DataSource as List<SoybeanAutoCodeMD>;
            
            PoundTotalMD pt = new PoundTotalMD();
            if (soybean[grossRow].TareStfID==null)
            {
                pt.TareStfID = null;
            }
            else
            {
                pt.TareStfID = int.Parse(soybean[grossRow].TareStfID.ToString());
            }
            pt.TareStfName = soybean[grossRow].TareStfName;
            pt.TareTime = soybean[grossRow].TareTime;
            pt.IsSoybean = true;
            pt.TransportCo = tedTransportCo.Text.Trim();
            pt.PoundGrossName = poundName;
            PoundTotalManager ptm = new PoundTotalManager();
            pt.No= ptm.GetLastNo();
            pt.QTYNet =decimal.Parse(tedQTYNetG.Text.Trim());
            pt.AutoCode = tedAutoCodeG.Text;
            pt.MatName = tedMatName.Text;
            pt.GrossTime = DateTime.Now; 
            pt.GrossStfID = LoginFrm.pubDelIn.ID;
            if (soybean[grossRow].QtyTare!=null)
            {
                pt.QTYTare = decimal.Parse( soybean[grossRow].QtyTare.ToString());
            }
            else
            {
                pt.QTYTare = null;
            }            
            pt.QTYGross = decimal.Parse(tedQTYTareG.Text.Trim());
            pt.PoundTareName = soybean[grossRow].TarePoundName;
            pt.Remark = tedRemarks.Text;
            pt.IsSoybean = true;
            pt.Customer = tedDelivery.Text.Trim();
            pt.TransportCo = tedTransportCo.Text.Trim();
            pt.GrossStfName = LoginFrm.pubDelIn.Name;
            pt.ShipName = tedShipName.Text.Trim()+" 航次号 "+tedVoyage.Text;
            SoybeanAutoCodeManager scm = new SoybeanAutoCodeManager();
            bool a= scm.InsertTareAndGross(pt);
            if (a==true)
            {
                PrintSoy(pt);
            }
            return a;
        }


        #endregion

        private void tedAutoShip_Leave(object sender, EventArgs e)
        {
           
        }

       
    }
}
