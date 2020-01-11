using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using Bohi.ERP.BLL;
using Bohi.ERP.MODEL;
using Stimulsoft.Report;
using System.Threading;
using System.Net;
using System.IO.Ports;
using DevExpress.Utils.Extensions;
using DevExpress.XtraSplashScreen;
using System.Drawing.Printing;
using System.IO;

using CameForm;
using HaSdkWrapper;
using System.Runtime.InteropServices;

namespace Bohi.ERP.ERPForm.UserControls
{
    public partial class PoundDUCtrl : BaseUserControl
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
        public PoundDUCtrl()
        {
            InitializeComponent();
            LoadSet(); //加载窗体按钮设置
            Initialization_AllEvent();//初始化所需按钮事件
            
        }
        private static List<MatShortKeyMD> lm = null;
        string light = "";
        public static string poundNomber="";
        private List<HaCamera> _cameras = new List<HaCamera>();//车牌识别
        PictureBox picBoxVideo = new PictureBox();//车牌识别
        #region--初始化按钮--
        /// <summary>
        /// 初始化所有按钮
        /// </summary>
        private void Initialization_AllEvent()
        {
            tmOpenInLinght.Tick += TmOpenLinght_Tick;//暂停四秒后关闭
            tmOpenOutLinght.Tick += TmOpenOutLinght_Tick;//暂停5秒后关闭
            CbAutoCodeButton();//初始化车号输入框回车事件
            tedpoudnum();//地磅读数只能输入数字            
            AutoCodeTxtChange();//初始化车号输入框字变化事件
            cbAutoCode.Click += Tb_Click;//初始化车号输入框点击事件
            cbAutoCode.TextUpdate += Cb_TextUpdate;//初始化车号输入框，焦点离开事件
            btnCopy.Click += Bt_Click2;//初始化自动磅重按钮事件
            cbSoyPound.CheckedChanged += Cb_CheckedChanged;//初始化是否过大豆选择框事件。
            tedPoundWeight.GotFocus += Td_GotFocus;//初始化地磅数据输入框。
            tedPoundWeightOut.GotFocus += TedPoundWeightOut_GotFocus;//初始化地磅出数据输入框。
            sbtnSet.Click += Sb_Click;//初始化设置按钮事件
            sbtnRebackQty.Click += SbtnRebackQty_Click;//初始化添加补重按钮
            sbtnOpenLinght.Click += SbtnOpenLinght_Click;//初始化手工灯
            sbtnCloseLingth.Click += SbtnCloseLingth_Click;//初始化手动关灯

            sbtnShowAutoCode.Click += SbtnShowAutoCode_Click;//连接监控
            btnSave.Click += Bt_Click;//初始化保存事件
            sbtnOpenCom2.Click += SbtnOpenCom2_Click;//初始化打开按钮

            btnGetPoundWeigthOut.Click += BtnGetPoundWeigthOut_Click;//初始化获取地磅出事件
            btnGetPoundWeigth.Click += Bt_Click1;//初始化获取地磅数事件
            btnPrint.Click += Bt_Click4;//初始化打印
            btnEditPrint.Click += Bt_Click5;//初始化编辑打印模板
            btnShow.Click += Bt_Click6;//初始化根据班组查询事件
            btnSearch.Click += Bt_Click3;//初始化根据时间查询事件
            gvEndView.RowClick += Gv_RowClick1;//初始化输入窗口出厂显示Gridview行点击事件
            gridView2.RowClick += Gv_RowClick1;//初始化查询窗口Gridvew点击行事件
            cbbPrintSet.Click += Cb_Click;//初始化设置打印机事件
            sbtnTotal.Click += SbtnTotal_Click;//初始化统计按钮事件
            this.glueChooseMat.EditValueChanged += GlueChooseMat_EditValueChanged;//初始化同车多品物料选择窗事件
            sbtnOpenSerialPort.Click += Sb_Click1;//初始化打开、关闭串口事件
            sbtnCheckAutoCode.Click += Bt_Click7;//初始化检查车号事件。


            sbtnExcel.Click += SbtnExcel_Click;//初始化导出Excel按钮
            txtReback.KeyPress += TxtReback_KeyPress;
            
            tedFilter.TextChanged += Te_TextChanged;//初始化过滤输入框事件
            sbtnRefresh.Click += SbtnRefresh_Click;//初始化刷新事件
            glueMaterial.TextChanged += GlueMaterial_TextChanged;//初始化物料Text事件
            glueCust.TextChanged += GlueCust_TextChanged;//初始化收货单位输入框事件
            glueDelivery.TextChanged += GlueDelivery_TextChanged;//初始化发货单位输入框事件
            tedRemark.TextChanged += TedRemark_TextChanged;//初始化备注输入框事件            
            cbbPrintSet.SelectedValueChanged += CbbPrintSet_SelectedValueChanged;//下拉选择打印机的时候触发的事件
           
        }

        private void TmOpenOutLinght_Tick(object sender, EventArgs e)
        {
            try
            {
                if (xq_close_jdq2() > 0)//关灯
                {
                    lbAutoCodeError.Text = "";
                    tmOpenOutLinght.Enabled = false;
                }

                else
                    lbAutoCodeError.Text = "关灯失败";
            }
            catch (Exception)
            {

            }
        }

        private void SbtnOpenCom2_Click(object sender, EventArgs e)
        {
            if (sbtnOpenCom2.Text == "打开")
            {
                OpenSerialPort2();
                
            }
            else if (sbtnOpenCom2.Text == "关闭")
            {
                CloseCom2();
                
            }
        }

        private void BtnGetPoundWeigthOut_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(tedPoundWeight.Text.Trim());
            poundName = "筒仓地磅 出";
            light = "out";
            tedPound.Text = tedPoundWeightOut.Text.Trim();
            if (tedPoundWeightOut.Tag != null)
            {
                tedPound.Tag = 0;
                //btnGetPoundWeigth.Enabled = false;
            }
            else
            {
                tedPound.Tag = null;
            }
            //tedPoundWeight.Tag = null;
            GetTarePoundByPound(cbAutoCode.Text.Trim());
        }
        private void SbtnCloseLingth_Click(object sender, EventArgs e)
        {
            try
            {                
                xq_close_jdq2();//关灯                 
            }
            catch (Exception)
            {

            }
            try
            {
                xq_close_jdq1();//关灯
            }
            catch (Exception)
            {

            }            
        }
        private void SbtnOpenLinght_Click(object sender, EventArgs e)
        {
            try
            {
                xq_open_usb_device();//打开usb灯连接
            }
            catch (Exception)
            {

            }
            try
            {
                if (light == "in")
                {                    
                    if (xq_open_jdq1() > 0)//开灯
                    {
                        lbState.Text = "开灯";
                        tmOpenInLinght.Enabled = true;
                    }                        
                    else
                    {
                        lbAutoCodeError.Text = "开灯失败";
                    }
                        
                }
                else if(light=="out")
                {
                    if (xq_open_jdq2() > 0)//开灯
                    {
                        lbState.Text = "开灯";
                        tmOpenOutLinght.Enabled = true;
                    }
                        
                    else
                        lbAutoCodeError.Text = "开灯失败";
                }
            }
            catch (Exception)
            {


            }
            
        }

        private void TmOpenLinght_Tick(object sender, EventArgs e)
        {            
            try
            {
                if (xq_close_jdq1() > 0)//关灯
                {
                    lbAutoCodeError.Text = "";
                    tmOpenInLinght.Enabled = false;
                }
                    
                else
                    lbAutoCodeError.Text = "关灯失败";
            }
            catch (Exception)
            {

            }            
           // MessageBox.Show(tmOpenLinght.Enabled.ToString());
        }

        private void TxtReback_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        

        private void SbtnRebackQty_Click(object sender, EventArgs e)
        {
            List<PoundTotalMD> lp = (List<PoundTotalMD>)this.gcFirstControl.DataSource;
            if (lp.Count <= 0) return;//如果厂内没车辆，退出
            if (cbAutoCode.Text == "") return;
            var result = lp.Find(delegate (PoundTotalMD poundTotalMD)
            {
                return poundTotalMD.AutoCode == cbAutoCode.Text;
            }
            );
            if (result == null)
            {
                return;
            } 
            RebackCheckMD rebackCheckMD = new RebackCheckMD();
            rebackCheckMD.AutoCode = result.AutoCode;
            rebackCheckMD.IsFinished = false;
            rebackCheckMD.MatName = result.MatName;
            rebackCheckMD.QtyTare = result.QTYTare;
            rebackCheckMD.RebackStf = LoginFrm.pubDelIn.Name;
            rebackCheckMD.RebackTime = DateTime.Now;
            rebackCheckMD.TareTime = result.TareTime;
            decimal qty = 0;
            decimal.TryParse(txtReback.Text.Trim(),out qty);
            rebackCheckMD.LestQty = qty;
            AddRebackQty(rebackCheckMD);
            txtReback.Text = "";
            cbAutoCode.Text = "";
            tedTare.Text = "";
        }
        #endregion

        #region ----------------------摄像头、车牌相关------------------

        #region 关闭摄像头链接

        private void SbtnShowAutoCode_Click(object sender, EventArgs e)
        {
            if(sbtnShowAutoCode.Text== "打开监控")
            {
                if (_cameras.Count > 0)
                {
                    MessageBox.Show("摄像头已链接，请断开摄像头！", "提示");
                    return;
                }
                PictureBox pictureBox = new PictureBox();
                //if (rbMechanic.Checked == true)
                //{
                //    IP = "192.168.69.200";
                //}
                //else if (rbOffice.Checked == true)
                //{
                //    IP = "192.168.69.201";
                //}
                //poundName = "办公楼";
                //break;
                //case "BHDB02":
                //    poundName = "机修楼";
                if (poundName == "机修楼")
                {
                    AddCame("192.168.69.200", pictureBox);//打开摄像头
                    AddCame("192.168.69.203", pictureBox);//打开摄像头
                    
                }
                else if(poundName == "办公楼")
                {
                    AddCame("192.168.69.201", pictureBox);//打开摄像头
                    AddCame("192.168.69.202", pictureBox);//打开摄像头
                }
                sbtnShowAutoCode.Text = "关闭监控";
            }
            else if(sbtnShowAutoCode.Text == "关闭监控")
            {
                if (_cameras.Count == 0)
                {
                    MessageBox.Show("未链接摄像头！");
                    return;
                }
                for (int i = 0; i < _cameras.Count+1; i++)
                {
                    _cameras[0].DisConnect();
                    _cameras.Remove(_cameras[0]);
                }  
                picBoxVideo.Controls.Clear();
                picBoxVideo.Image = null;
                sbtnShowAutoCode.Text = "打开监控";
            }
        }
       

        #endregion

        #region 选择摄像头
       
        private void AddCame(string ip, PictureBox pictureBox)
        {
            var cam = new HaCamera();
            cam.Ip = ip;
            cam.Port = 9527;
            cam.Username = "admin";
            cam.Password = "admin";
            cam.Connect(pictureBox.Handle);
            _cameras.Add(cam);
            cam.VehicleCaptured += Cam_VehicleCaptured;

        }

        private void Cam_VehicleCaptured(object sender, VehicleCapturedEventArgs e)
        {
            var cam = sender as HaCamera;
            if (InvokeRequired)
            {
                Action<HaCamera, VehicleCapturedEventArgs> ac = ShowPlate;
                this.BeginInvoke(ac, cam, e);
            }
        }
        public object imga;




        private void ShowPlate(HaCamera sender, VehicleCapturedEventArgs e)
        {
            string AutoCode = string.Format("{0}", string.IsNullOrEmpty(e.License) ? "无牌" : e.License);
            cbAutoCode.Text = AutoCode;
            if (poundName == "机修楼")
            {
                if (e.Ip == "192.168.69.200")
                {
                    lbView.Text = "出厂--";
                }
                else if (e.Ip == "192.168.69.203")
                {
                    lbView.Text = "进厂--";
                    CheckTareQty();
                }
            }else if(poundName == "办公楼")
            {
                if (e.Ip == "192.168.69.201")
                {
                    lbView.Text = "出厂--";
                }
                else if (e.Ip == "192.168.69.202")
                {
                    lbView.Text = "进厂--";
                    CheckTareQty();
                }
            }
                

            // var img = picBoxCut.Image;
            //var imgNew = Image.FromStream(new MemoryStream(e.JpegBuffer));
            imga = Image.FromStream(new MemoryStream(e.PlateJpegBuffer));
            pictureEdit1.Image = (Image)imga;
            //if (img != null)
            //{
            //    img.Dispose();
            //}
        }
        #endregion

        #endregion

        #region ----界面设置相关---


        public List<PoundTotalMD> lp = new List<PoundTotalMD>();//定义一个空的泛型

        #region 窗体设置

        /// <summary>
        /// 首次加载窗体内组件属性相关
        /// </summary>
        private void LoadSet()
        {
            
            cbSoyPound.Checked = true;
           // lcgShowAutoCodePic.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.Text = "托利多";//设置Form抬头
            btnSave.Enabled = false;//设置启动时，保存按钮为不可用
            lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;//设置启动时，选择多品物料不可用
            ToolSet();//工具栏设置
            setGridview();//设置Gridview显示特性
            AddColumn(gvFirtView);//进厂车辆Gridview添加显示字段
            ColumnsAdd(gvEndView);//出厂车辆Gridview添加显示字段
            ColumnsAdd(gridView2);//查询车辆Gridview添加显示字段
            SetPubAutoCode();//获取大豆车信息
            GetHostName();//获取电脑名
            FirstIn();//绑定第一次进厂车辆信息到Gridview中
            EndOut();//绑定最近500次车辆进厂信息到Gridview中
            SetDataEdit(dteStartTime);//设置时间可以选择到秒
            SetDataEdit(dteEndTime);//设置时间可以选择到秒
            SetDataEdit(dteSearchTime);//设置查询时间选择到秒
            SetTexb();//窗体加载，初始化输入框为只读
           // textSource();//把大豆车信息绑定在Combobox中，            
            ComBinding();//把已经进出过厂的车辆信息绑定到车辆输入框中，
            closeclose();//关闭窗口，关闭串口
            GetSerialPortNo();//获取Com名称
            GetSerialPortNo2();//获取Com名称
            SearchOut();//搜索Gridview数据绑定
            ReadTxt();//绑定txt文件中的打印机名到Combobox中
            GetMatShortKeyList();//获取所有的物料快捷方式
            this.gridView2.OptionsView.ShowFooter = true;//显示底部列
            TotalSet();//统计净重
            
        }
        /// <summary>
        /// 窗体加载，设置输入框属性
        /// </summary>
        private void SetTexb()
        {
            tedNet.ReadOnly = true;
            tedGross.ReadOnly = true;
            tedTare.ReadOnly = true;
            txbShipName.ReadOnly = true;
            txbShipNo.ReadOnly = true;
            tedPound.ReadOnly = false;
            tedRemark.Text = "加工原料为转基因大豆";
            dteSearchTime.DateTime = DateTime.Now.Date;
            dteStartTime.DateTime = DateTime.Now.Date;
            dteEndTime.DateTime = DateTime.Now.Date;
            //glueDelivery.Enabled = false;
        }
        private void ToolSet()
        {
            //设置主工具栏的所有按钮隐藏
            Bar mainMenu = barManager.MainMenu;
            foreach (LinkPersistInfo info in mainMenu.LinksPersistInfo)
            {
                // info.Item.Visibility = BarItemVisibility.Never;//设置可见
                // info.Item.Alignment = BarItemLinkAlignment.Right;//设置靠右显示
                //this.barButtonItem4.Visibility = BarItemVisibility.Always;
            }
        }
        /// <summary>
        /// 设置GridView
        /// </summary>
        private void setGridview()
        {
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(gvFirtView);
            sg.CustomizeGridView(gvEndView);
            sg.CustomizeGridView(gridView2);
        }
        #endregion

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
        #region 【>------------------------><【 设置DateEdit显示时间（时分秒）】【ShowDateEditTime（）】><-(未用)----------------<】
        /// <summary>
        /// 设置DateEdit显示时间（时分秒）
        /// <para>【yyyy-MM-dd HH:mm:ss】的说明:</para>
        /// <para>1、yyyy：表示四位数的年份；2、MM：表示两位数月份；3、dd：表示两位数的天数；</para>
        /// <para>3、HH(hh)：表示两位数的小时(大写表示24小时制，小写表示12小时制)；4、mm：表示两位数分；5、ss：表示两位数的秒；</para>
        /// <para>6、其它（-、 、：）均为分隔符，可随意；</para>
        /// </summary>
        /// <param name="dateEdit"></param>
        /// <param name="formatString"></param>
        public void ShowDateEditTime(DateEdit dateEdit, string formatString = "yyyy-MM-dd HH:mm:ss")
        {
            dateEdit.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            dateEdit.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            dateEdit.Properties.DisplayFormat.FormatString = formatString;
            dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.EditFormat.FormatString = formatString;
            dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.Mask.EditMask = formatString;
        }
        #endregion
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
        /// 根据电脑名获取地磅名称
        /// </summary>
        private string poundName = "";
        private int selectRow = -1;

        /// <summary>
        /// 设置大豆车数据源
        /// </summary>
        /// <returns>数据源</returns>
        private void textSource()
        {

            PubAutoCodeListManager paclm = new PubAutoCodeListManager();
            List<PubAutoCodeListMD> lp = paclm.GetAllAutoCode();
            AutoCompleteStringCollection ts = new AutoCompleteStringCollection();

            for (int i = 0; i < lp.Count; i++)
            {
                ts.Add(lp[i].AutoCode.ToString());
            }

            cbAutoCode.AutoCompleteCustomSource = ts;
            cbAutoCode.AutoCompleteMode = AutoCompleteMode.Suggest;
            cbAutoCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        
        /// <summary>
        /// 在车号输入框中按下回车
        /// </summary>
        private void CbAutoCodeButton()
        {
            cbAutoCode.KeyDown += CbAutoCode_KeyDown;
        }
        #endregion

        #region-------------------------功能---------------------

        private void CheckTareQty()
        {
            lbTareQty.ForeColor = Color.Black;
            lbTareQty.Text = "";
            
            //Decimal ddd = 0;
            Decimal dddd = 0;
            Decimal dd = 0;
            int c12 = 0;
            PoundTotalManager poundTotalManager = new PoundTotalManager();
            List<TareQtyMD> tareQtyMDs = poundTotalManager.GetAllTareQty(cbAutoCode.Text.Trim());
            if (tareQtyMDs.Count > 0)
            {
                dddd = Decimal.Parse(tareQtyMDs[0].QTYTare.ToString());
                for (int i = 0; i < tareQtyMDs.Count; i++)
                {
                   
                    if (tareQtyMDs[i].QTYTare < dddd) dddd =  Decimal.Parse(tareQtyMDs[i].QTYTare.ToString());
                    
                    if(tareQtyMDs[i].QTYTare - dddd > 500)
                    {
                        c12++;
                        dd=dd- Decimal.Parse(tareQtyMDs[i].QTYTare.ToString());
                    }
                    dd += Decimal.Parse(tareQtyMDs[i].QTYTare.ToString());

                }
                //ddd = dddd;
                //for (int i = 0; i < tareQtyMDs.Count; i++)
                //{
                   
                //    if (tareQtyMDs[i].QTYTare > ddd && tareQtyMDs[i].QTYTare-dddd < 500)
                //    {
                //        ddd = Decimal.Parse(tareQtyMDs[i].QTYTare.ToString());

                //    }
                //}
                dd = dd / (tareQtyMDs.Count-c12);
            }

            lbTareQty.Text = "历史皮重：最小皮重 ：" + dddd.ToString() + "KG；平均皮重 " +Math.Round(dd).ToString() + "KG；";
            Decimal df = 0;
            Decimal.TryParse(tedPoundWeight.Text.Trim(), out df);
            if ((df - dd) >= 200) lbTareQty.ForeColor = Color.Red;
        }

        #region  --统计数据功能--
        /// <summary>
        /// 统计出厂车辆货品重量
        /// </summary>
        private void TotalQtyNet()
        {
            lbTotal.Text = "";
            if (cmbList.Text.Trim() == string.Empty) return;
            List<PoundTotalMD> lpt = this.gridControl2.DataSource as List<PoundTotalMD>;
            double total = 0;
            int a = 0;
            foreach (PoundTotalMD item in lpt)
            {
                if (item.MatName != cmbList.Text) continue;
                a++;
                total += (double.Parse(item.QTYNet.ToString()) / 1000);
            }
            lbTotal.Text = "物料名称： “" + cmbList.Text + "”" + "\r\n" + "合计： " + total.ToString() + "吨。" + "\r\n" + "运输总共 " + a + "车次。";
        }
        #endregion

        #region --添加物料列表--
        /// <summary>
        /// 添加物料名称到列表中
        /// </summary>
        private void AddMatList()
        {
            cmbList.Items.Clear();
            List<PoundTotalMD> lpt = this.gridControl2.DataSource as List<PoundTotalMD>;
            if (lpt == null) return;
            if (lpt.Count < 1) return;
            List<string> ls = new List<string>();
            ls.Add(lpt[0].MatName);
            foreach (PoundTotalMD item in lpt)
            {
                if (ls.Contains(item.MatName) == true) continue;
                ls.Add(item.MatName);
            }
            cmbList.Items.AddRange(ls.ToArray());
        }

        #endregion

        #region -------打印相关------

        #region --打印--
        /// <summary>
        /// 打印功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_Click4(object sender, EventArgs e)
        {
            if (cbSoyPound.CheckState == CheckState.Checked)
            {
                PrintSoy();//打印大豆磅单
            }
            else
            {
                Print();//打印非大豆磅单
            }
            
        }
        /// <summary>
        /// 列出所有本机的打印机
        /// </summary>
        private void PrintSet()
        {
            cbbPrintSet.Items.Clear();
            foreach (string sprint in PrinterSettings.InstalledPrinters)
            {
                cbbPrintSet.Items.Add(sprint);
            }
        }
        /// <summary>
        /// 打印磅单（大豆）
        /// </summary>   
        private void PrintSoy()
        {
            try
            {
                if (tabPane1.SelectedPage.Caption == "单据录入")
                {
                    List<PoundTotalMD> lpt = this.gcEndControl.DataSource as List<PoundTotalMD>;
                    if (selectRow > 0)
                    {
                        PoundTotalMD pt = lpt[selectRow];
                        StiReport sr = new StiReport();
                        sr.Load("ReportSoy.mrt");
                        sr.Dictionary.Clear();
                        sr.Dictionary.BusinessObjects.Clear();
                        sr.RegBusinessObject("SacOutSupervise", pt);
                        sr.Dictionary.Synchronize();
                        sr.Dictionary.SynchronizeBusinessObjects();
                        sr.Compile();
                        sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
                        //sr.Show();
                    }
                    else
                    {
                        PoundTotalMD pt = lpt[0];
                        StiReport sr = new StiReport();
                        sr.Load("ReportSoy.mrt");
                        sr.Dictionary.Clear();
                        sr.Dictionary.BusinessObjects.Clear();
                        sr.RegBusinessObject("SacOutSupervise", pt);
                        sr.Dictionary.Synchronize();
                        sr.Dictionary.SynchronizeBusinessObjects();
                        sr.Compile();
                        sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
                       // sr.Show();
                    }
                }
                else
                {
                    List<PoundTotalMD> lpt = this.gridControl2.DataSource as List<PoundTotalMD>;
                    PoundTotalMD pt = lpt[selectRow];
                    StiReport sr = new StiReport();
                    sr.Load("ReportSoy.mrt");
                    sr.Dictionary.Clear();
                    sr.Dictionary.BusinessObjects.Clear();
                    sr.RegBusinessObject("SacOutSupervise", pt);
                    sr.Dictionary.Synchronize();
                    sr.Dictionary.SynchronizeBusinessObjects();
                    sr.Compile();
                    sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
                    //sr.Show();
                }

            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }
        /// <summary>
        /// 打印磅单（非大豆）
        /// </summary>   
        private void Print()
        {
            try
            {
                if (tabPane1.SelectedPage.Caption == "单据录入")
                {
                    List<PoundTotalMD> lpt = this.gcEndControl.DataSource as List<PoundTotalMD>;
                    if (selectRow > 0)
                    {
                        if (cbbPrintSet.Text == string.Empty) return;
                        PoundTotalMD pt = lpt[selectRow];
                        if (string.IsNullOrEmpty(pt.PrintStfName))
                        {
                            pt.PrintStfName = pt.GrossStfName;
                        }
                        StiReport sr = new StiReport();
                        sr.Load("Report.mrt");
                        sr.Dictionary.Clear();
                        sr.Dictionary.BusinessObjects.Clear();
                        sr.RegBusinessObject("SacOutSupervise", pt);
                        sr.Dictionary.Synchronize();
                        sr.Dictionary.SynchronizeBusinessObjects();
                        sr.PrinterSettings.PrinterName = cbbPrintSet.Text;
                        sr.Compile();                        
                        sr.Show();
                       // sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
                        
                    }
                    else
                    {
                        if (cbbPrintSet.Text == string.Empty) return;
                        PoundTotalMD pt = lpt[0];
                        if (string.IsNullOrEmpty(pt.PrintStfName))
                        {
                            pt.PrintStfName = pt.GrossStfName;
                        }
                        StiReport sr = new StiReport();
                        sr.Load("Report.mrt");
                        sr.Dictionary.Clear();
                        sr.Dictionary.BusinessObjects.Clear();
                        sr.RegBusinessObject("SacOutSupervise", pt);
                        sr.Dictionary.Synchronize();
                        sr.Dictionary.SynchronizeBusinessObjects();
                        sr.PrinterSettings.PrinterName = cbbPrintSet.Text;
                        sr.Compile();
                        //sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
                        sr.Show();
                    }
                }
                else
                {
                    if (cbbPrintSet.Text == string.Empty) return;
                    List<PoundTotalMD> lpt = this.gridControl2.DataSource as List<PoundTotalMD>;
                    PoundTotalMD pt = lpt[selectRow];
                    if(string.IsNullOrEmpty(pt.PrintStfName))
                    {
                        pt.PrintStfName = lpt[selectRow].GrossStfName;
                    }
                    StiReport sr = new StiReport();
                    sr.Load("Report.mrt");
                    sr.Dictionary.Clear();
                    sr.Dictionary.BusinessObjects.Clear();
                    sr.RegBusinessObject("SacOutSupervise", pt);
                    sr.Dictionary.Synchronize();
                    sr.Dictionary.SynchronizeBusinessObjects();
                    sr.PrinterSettings.PrinterName = cbbPrintSet.Text;
                    sr.Compile();
                    sr.Show();
                    //sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
                    //sr.Show();
                }

            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }
        #endregion


        #region --编辑--
        /// <summary>
        /// 编辑打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_Click5(object sender, EventArgs e)
        {
            if (cbSoyPound.CheckState == CheckState.Checked)
            {
                PrintDesignSoy();
            }
            else
            {
                PrintDesign();
            }
            // PrintDesign();
        }
        private void PrintDirect(PoundTotalMD pt)
        {
            StiReport sr = new StiReport();
            sr.Load("Report.mrt");
            sr.Dictionary.Clear();
            sr.Dictionary.BusinessObjects.Clear();
            sr.RegBusinessObject("SacOutSupervise", pt);
            sr.Dictionary.Synchronize();
            sr.Dictionary.SynchronizeBusinessObjects();
            //sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
            sr.PrinterSettings.PrinterName = cbbPrintSet.Text;
            sr.Compile();
            sr.Show();
            //sr.Show();
        }
        private void PrintDirectSoybean(PoundTotalMD pt)
        {
            StiReport sr = new StiReport();
            sr.Load("ReportSoy.mrt");
            sr.Dictionary.Clear();
            sr.Dictionary.BusinessObjects.Clear();
            sr.RegBusinessObject("SacOutSupervise", pt);
            sr.Dictionary.Synchronize();
            sr.Dictionary.SynchronizeBusinessObjects();
            sr.PrinterSettings.PrinterName = cbbPrintSet.Text;
            sr.Compile();
            sr.Show();
            //sr.Print(true, new System.Drawing.Printing.PrinterSettings() { PrinterName = cbbPrintSet.Text });
            //sr.Show();
        }
        /// <summary>
        /// 编辑模板
        /// </summary>
        private void PrintDesign()
        {
            PoundTotalMD pt = new PoundTotalMD();
            StiReport sr = new StiReport();
            sr.Load("Report.mrt");
            sr.Dictionary.Clear();
            sr.Dictionary.BusinessObjects.Clear();
            sr.RegBusinessObject("SacOutSupervise", pt);
            sr.Dictionary.Synchronize();
            sr.Dictionary.SynchronizeBusinessObjects();
            sr.Design();
        }
        /// <summary>
        /// 编辑模板
        /// </summary>
        private void PrintDesignSoy()
        {
            PoundTotalMD pt = new PoundTotalMD();
            StiReport sr = new StiReport();
            sr.Load("ReportSoy.mrt");
            sr.Dictionary.Clear();
            sr.Dictionary.BusinessObjects.Clear();
            sr.RegBusinessObject("SacOutSupervise", pt);
            sr.Dictionary.Synchronize();
            sr.Dictionary.SynchronizeBusinessObjects();
            sr.Design();
        }
        #endregion

        #endregion

        #region ------查询功能------

        /// <summary>
        /// 根据班次查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_Click6(object sender, EventArgs e)
        {
            GridViewBingdingByClasses();
        }
        private void GridViewBingdingByClasses()
        {
            PoundTotalManager ptm = new PoundTotalManager();

            if (rbtnMorning.Checked == true)
            {
                DateTime StartTime = DateTime.Parse(dteSearchTime.DateTime.ToString("yyyy-MM-dd") + " " + "08:00:00");
                DateTime EndTime = DateTime.Parse(dteSearchTime.DateTime.ToString("yyyy-MM-dd") + " " + "15:59:59");
                List<PoundTotalMD> lp = ptm.GetPoundListByDateTime(StartTime, EndTime);
                this.gridControl2.DataSource = lp;
                cmbList.Text = "";
                AddMatList();
            }
            if (rbtnNoon.Checked == true)
            {
                DateTime StartTime = DateTime.Parse(dteSearchTime.DateTime.ToString("yyyy-MM-dd") + " " + "16:00:00");
                DateTime EndTime = DateTime.Parse(dteSearchTime.DateTime.ToString("yyyy-MM-dd") + " " + "23:59:59");
                List<PoundTotalMD> lp = ptm.GetPoundListByDateTime(StartTime, EndTime);
                this.gridControl2.DataSource = lp;
                cmbList.Text = "";
                AddMatList();
            }
            if (rbtnSmallHours.Checked == true)
            {
                DateTime StartTime = DateTime.Parse(dteSearchTime.DateTime.ToString("yyyy-MM-dd") + " " + "0:00:00");
                DateTime EndTime = DateTime.Parse(dteSearchTime.DateTime.ToString("yyyy-MM-dd") + " " + "7:59:59");
                List<PoundTotalMD> lp = ptm.GetPoundListByDateTime(StartTime, EndTime);
                this.gridControl2.DataSource = lp;
                cmbList.Text = "";
                AddMatList();
            }
        }

        /// <summary>
        /// 绑定Gridview，显示厂内车辆信息
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

            this.gcFirstControl.DataSource = lp;
        }

        /// <summary>
        /// 显示最后500次出厂车辆信息
        /// </summary>
        private void EndOut()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();
            lp = ptm.GetPoundListByTop500();
            this.gcEndControl.DataSource = lp;
        }
        /// <summary>
        /// 显示最后500次出厂车辆信息
        /// </summary>
        private void SearchOut()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();
            lp = ptm.GetPoundListByTop200();
            this.gridControl2.DataSource = lp;
        }

        /// <summary>
        /// 查询某一时段的车辆出厂情况
        /// </summary>
        /// <param name="bt"></param>       
        private void Bt_Click3(object sender, EventArgs e)
        {
            SearchAutoCode();
        }
        private void SearchAutoCode()
        {
            PoundTotalManager ptm = new PoundTotalManager();
            List<PoundTotalMD> lpt = ptm.GetPoundListByDateTime(dteStartTime.DateTime, dteEndTime.DateTime);
            this.gridControl2.DataSource = lpt;
            cmbList.Text = "";
            AddMatList();
        }
        #endregion        

        #region -----地磅重量相关-----

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
            PrintDocument pd = new PrintDocument();
            cbbPrintSet.Text = pd.PrinterSettings.PrinterName;
        }
        #endregion

        #region --获取电脑串口名--
        /// <summary>
        /// 获取电脑上的所有串口
        /// </summary>
        private void GetSerialPortNo()
        {
            if (SerialPort.GetPortNames().Count() < 1) return;
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                this.cmbCOMName.Items.Add(vPortName);
            }
            this.cmbCOMName.SelectedIndex = 0;
        }
        #endregion
        #region --获取电脑串口名--
        /// <summary>
        /// 获取电脑上的所有串口
        /// </summary>
        private void GetSerialPortNo2()
        {
            if (SerialPort.GetPortNames().Count() < 1) return;
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                this.cmbComName2.Properties.Items.Add(vPortName);
            }
            this.cmbComName2.SelectedIndex = 0;
        }
        #endregion

        #region --打开地磅串口--

        /// <summary>
        /// 串口打开
        /// </summary>
        private void OpenSerialPort()
        {
            try
            {
                if (!spCom1.IsOpen)
                {
                    spCom1.BaudRate = 1200;
                    spCom1.StopBits = System.IO.Ports.StopBits.One;
                    spCom1.DataBits = 7;
                    spCom1.Parity = System.IO.Ports.Parity.None;
                    spCom1.PortName = cmbCOMName.Text.Trim();//串口号默认是第一个，如果需要修改，几个修改成其他的
                    spCom1.Open();
                    SerialPoundData(spCom1);//初始化串口字符发送事件
                    btnSave.Enabled = true;
                    tedPound.Text = "";
                    sbtnOpenSerialPort.Text = "关闭";
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
                SerialPortDataReceived();
            }
            catch (Exception )
            {               

               // MessageBox.Show(ex.ToString());
            }
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
            while (spCom1.ReadByte().ToString() != "13")
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
                    if (_byte[i] - 176 > 0)
                    {
                        c = _byte[i] - 176;
                        wd += c;
                    }
                }
            }
            float a=0;
            try
            {
                a = float.Parse(wd);
            }
            catch (Exception)
            {
                string path = Application.StartupPath+"\\log.txt";
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write(wd+"\r\n");
            }
            //poundNomber = a.ToString();
            if (tedPoundWeight.Tag == null)
            {
                tedPoundWeight.Text = a.ToString();
            }
        }
        #endregion

        #region --打开地磅串口--

        /// <summary>
        /// 串口打开
        /// </summary>
        private void OpenSerialPort2()
        {
            try
            {
                if (!spCom2.IsOpen)
                {
                    spCom2.BaudRate = 1200;
                    spCom2.StopBits = System.IO.Ports.StopBits.One;
                    spCom2.DataBits = 7;
                    spCom2.Parity = System.IO.Ports.Parity.None;
                    spCom2.PortName = cmbComName2.Text.Trim();//串口号默认是第一个，如果需要修改，几个修改成其他的
                    spCom2.Open();
                    SerialPoundData2(spCom2);//初始化串口字符发送事件
                    btnSave.Enabled = true;
                    tedPound.Text = "";
                    sbtnOpenCom2.Text = "关闭";
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
        private void SerialPoundData2(SerialPort sp)
        {
            sp.DataReceived += Sp_DataReceived2;
        }

        private void Sp_DataReceived2(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPortDataReceived2();
            }
            catch (Exception)
            {

                // MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 获取地磅重量
        /// </summary>
        private void SerialPortDataReceived2()
        {
            if (spCom2.ReadExisting() == string.Empty)
            {
                //MessageBox.Show("没有数据！");
                return;
            }
            byte[] _byte2 = new byte[18];
            // float[] f = null;
            while (spCom2.ReadByte().ToString() != "13")
            {
                continue;
            }
            for (int i = 0; i < 17; i++)
            {
                _byte2[i] = (byte)spCom2.ReadByte();
            }
            string wd2 = string.Empty;
            for (int i = 5; i < 11; i++)
            {
                int c2 = 0;
                if (_byte2[i] > 45 && _byte2[i] < 59)
                {
                    c2 = _byte2[i] - 48;
                    wd2 += c2;
                }
                else
                {
                    if (_byte2[i] - 176 > 0)
                    {
                        c2 = _byte2[i] - 176;
                        wd2 += c2;
                    }
                }
            }
            float a2 = 0;
            try
            {
                a2 = float.Parse(wd2);
            }
            catch (Exception)
            {
                string path = Application.StartupPath + "\\log.txt";
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write(wd2 + "\r\n");
            }
           // poundNomber = a.ToString();
            if (tedPoundWeightOut.Tag == null)
            {
                tedPoundWeightOut.Text = a2.ToString();
            }
        }
        #endregion

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
            CloseCom2();
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
                btnSave.Enabled = false;
                sbtnOpenSerialPort.Text = "打开";
            }
        }
        private void CloseCom2()
        {
            if (!spCom2.IsOpen)
            {
                // MessageBox.Show("地磅未链接");
                return;
            }
            else
            {
                SplashScreenManager.ShowDefaultWaitForm();
                spCom2.DataReceived -= Sp_DataReceived;
                Thread.Sleep(1000);
                spCom2.Dispose();
                SplashScreenManager.CloseDefaultWaitForm();
                btnSave.Enabled = false;
                sbtnOpenCom2.Text = "打开";
            }
        }
        #endregion

        #endregion


        #endregion

        #region -------------------------------事件-----------------------
        /// <summary>
        /// 车号输入框回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbAutoCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbSoyPound.CheckState == CheckState.Unchecked) return;
            if (e.KeyCode == Keys.Enter)
            {
                Bt_Click7(sender, e);
            }
        }
        /// <summary>
        /// 地磅读数只接受数字读数
        /// </summary>
        private void tedpoudnum()
        {
            tedPoundWeight.KeyPress += TedPoundWeight_KeyPress;
            tedPoundWeightOut.KeyPress += TedPoundWeightOut_KeyPress;
        }

        private void TedPoundWeightOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 地磅读数只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param
        private void TedPoundWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

     



        /// <summary>
        /// 车号输入框文字变化事件
        /// </summary>
        private void AutoCodeTxtChange()
        {
            cbAutoCode.LostFocus += CbAutoCode_LostFocus;
        }
        /// <summary>
        /// 车号输入框文字变化事件
        /// </summary>
        private void CbAutoCode_LostFocus(object sender, EventArgs e)
        {
            if (cbSoyPound.CheckState == CheckState.Unchecked) return;            
            lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
           // GetDataSours();
            GetTarePound(cbAutoCode.Text.Trim());
        }

        
        /// <summary>
        /// 物料TXT输入框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlueMaterial_TextChanged(object sender, EventArgs e)
        {
            //if (glueMaterial.Text.Trim() == "1")
            //{
            //    glueMaterial.Text = "盘站回皮";
            //    glueDelivery.Text = "广西渤海农业发展有限公司";
            //    glueCust.Text = "";
            //    tedRemark.Text = "";
            //}
            var result = lm.Find(delegate (MatShortKeyMD matShortKey)
            {
                return matShortKey.ShortKey == glueMaterial.Text;
            }
            );
            if (result == null) return;            
            glueMaterial.Text=result.Name;            
            tedTransport.Text= result.TransportCo;
            tedRemark.Text= result.Remarks;
            txbShipName.Text = result.ShipAndVoyage;
            glueDelivery.Text= result.DeliveryCo;
            glueCust.Text= result.Customer;
        }
        /// <summary>
        /// 把打印机名写到txt中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbbPrintSet_SelectedValueChanged(object sender, EventArgs e)
        {
            WriteTxt();
        }
        /// <summary>
        /// 备注输入框快捷键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TedRemark_TextChanged(object sender, EventArgs e)
        {
            if (tedRemark.Text.Trim()=="1")
            {
                tedRemark.Text = "转基因大豆";
            }
            if (tedRemark.Text.Trim()=="2")
            {
                tedRemark.Text = "加工原料为转基因大豆";
            }
        }
        /// <summary>
        /// 发货单位输入框快捷键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlueDelivery_TextChanged(object sender, EventArgs e)
        {
            if (glueDelivery.Text.Trim() == "1")
            {
                glueDelivery.Text = "广西渤海农业发展有限公司";
            }
        }
        /// <summary>
        /// 客户输入框快捷键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlueCust_TextChanged(object sender, EventArgs e)
        {
            if (glueCust.Text.Trim()=="1")
            {
                glueCust.Text = "广西渤海农业发展有限公司";
            }
        }
        /// <summary>
        /// 刷新按钮，触发跟新Gridview绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnRefresh_Click(object sender, EventArgs e)
        {
            FirstIn();//绑定第一次进厂车辆信息到Gridview中
            EndOut();//绑定最近500次车辆进厂信息到Gridview中
            GetMatShortKeyList();//绑定快捷键
        }
        /// <summary>
        /// 导出到Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }
        /// <summary>
        /// 统计数据按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnTotal_Click(object sender, EventArgs e)
        {
            TotalQtyNet();
            this.gridView2.FindFilterText = cmbList.Text;
        }
        /// <summary>
        /// 同车多品物料选择组件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlueChooseMat_EditValueChanged(object sender, EventArgs e)
        {
            var ls= this.glueChooseMat.GetSelectedDataRow() as ReachArriveMD;            
            glueCust.Text = ls.ToCust;
            if (ls.SuppName == string.Empty)
            {
                glueDelivery.Text = "广西渤海农业发展有限公司";
            }
            else
            {
                glueDelivery.Text = ls.SuppName;

            }
            if (ls.StockID == 1224884)
            {
                tedRemark.Text = "加工原料为转基因大豆";
            }
            else
            {
                tedRemark.Text = "";
            }
            if (ls.MatID == 2057380)
            {
                glueCust.Text = "广西渤海农业发展有限公司";
            }
            glueMaterial.Text = ls.MatName;
            lbAutoCodeError.Text = "";
            //this.glueChooseMat.Properties.DataSource = null;
        }
        /// <summary>
        /// 打开关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sb_Click1(object sender, EventArgs e)
        {
            if (sbtnOpenSerialPort.Text== "打开")
            {
                OpenSerialPort();
                
            }
            else if (sbtnOpenSerialPort.Text=="关闭")
            {
                CloseCom1();
                
            }
        }

        /// <summary>
        /// 设置打印机事件
        /// </summary>
        /// <param name="cb"></param>
        
        private void Cb_Click(object sender, EventArgs e)
        {
            PrintSet();
        }

        

        /// <summary>
        /// 设置界面显示及隐藏
        /// </summary>
        /// <param name="sb">设置按钮</param>       
        private void Sb_Click(object sender, EventArgs e)
        {
            if (lcgSet.Visible==true)
            {
                lcgSet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                lcgSet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        private void TedPoundWeightOut_GotFocus(object sender, EventArgs e)
        {
            //(sender as TextEdit).Tag = "0";
            tedPoundWeightOut.Tag = "0";
            //tedPoundWeight.Tag = "0";
            //tedPound.Tag = "0";
            lbState.Text = "手动磅重调整";
            lbState.ForeColor = Color.Red;
        }
        /// <summary>
        /// 磅重text输入焦点事件
        /// </summary>
        /// <param name="td"></param>

        private void Td_GotFocus(object sender, EventArgs e)
        {
            //(sender as TextEdit).Tag = "0";
            tedPoundWeight.Tag = "0";
            //tedPoundWeight.Tag = "0";
            //tedPound.Tag = "0";
            lbState.Text = "手动磅重调整";
            lbState.ForeColor = Color.Red;
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="bt">保存按钮</param>

        private void Bt_Click(object sender, EventArgs e)
        {
            if (btnSave.Text== "保  存")
            {
                if (tedPound.Text == String.Empty)
                {
                    MessageBox.Show("请读取地磅数据！");
                    return;
                }
                if (cbAutoCode.Text.Trim() == string.Empty) return;
                JudgeIn();//添加数据
                FirstIn();//绑定进厂车辆数据到Gridview
                EndOut();//绑定最近500次出厂车辆信息
                //tedPound.Tag = null;
                //lbState.Text = "自动磅重";
                //lbState.ForeColor = Color.Black;
                pictureEdit1.Image =Image.FromFile(Environment.CurrentDirectory+"\\imge\\bohi.png");
                this.glueChooseMat.Properties.DataSource = null;
                this.lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                btnGetPoundWeigth.Enabled = false;
                if(cbSoyPound.Checked==true)
                {
                    try
                    {
                        xq_open_usb_device();//打开usb灯连接
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        if (light == "in")
                        {
                            if (xq_open_jdq1() > 0)//开灯
                            {
                                lbState.Text = "开灯";
                                tmOpenInLinght.Enabled = true;
                            }
                                
                            else
                                lbAutoCodeError.Text = "开灯失败";
                        }
                        else if (light == "out")
                        {
                            if (xq_open_jdq2() > 0)//开灯
                            {
                                lbState.Text = "开灯";
                                tmOpenOutLinght.Enabled = true;
                            }
                                
                            else
                                lbAutoCodeError.Text = "开灯失败";
                        }
                    }
                    catch (Exception)
                    {


                    }
                    
                }
                
            }
            else if (btnSave.Text == "继  续")
            {
                //tedPound.Tag = null;
                //lbState.Text = "自动磅重";
                //lbState.ForeColor = Color.Black;
                cbAutoCode.Text = "";                
                tedPound.Text = "";
                tedNet.Text = "";
                tedGross.Text = "";
                tedTare.Text = "";
                btnSave.Text = "保  存";
                //light = "";
                //tedPoundWeight.Text = "";
                btnGetPoundWeigth.Enabled = true;
            }
            
        }

        //单击全选车号信息

        private void Tb_Click(object sender, EventArgs e)
        {
           // (sender as System.Windows.Forms.ComboBox).SelectAll();
        }

        private void Tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            (sender as System.Windows.Forms.ComboBox).SelectAll();
            
        }
        /// <summary>
        /// 车号检查按钮事件
        /// </summary>
        /// <param name="bt"></param>
        
        private void Bt_Click7(object sender, EventArgs e)
        {
            
            if (cbSoyPound.CheckState == CheckState.Checked)
            {
                lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tedGross.Text = "";
                lbView.Text = "";
                tedNet.Text = "";
                tedTare.Text = "";
               // GetDataSours();
                GetTarePound(cbAutoCode.Text.Trim());
            }
            else
            {                
                tedGross.Text = "";
                lbView.Text = "";
                lbStatus.Text = "";
                txbShipName.Text = "";
                tedRemark.Text = "";
                tedNet.Text = "";
                tedTare.Text = "";
                lbAutoCodeError.Text = "";
                tedTransport.Text = "";
                glueMaterial.Text = "";
                glueCust.Text = "";
                glueDelivery.Text = "";
                lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
               // GetDataSours();
                GetTarePound(cbAutoCode.Text.Trim());
            }  
        }
        
        /// <summary>
        /// 把地磅数据复制到剪切板中
        /// </summary>
        private void CopyClipboard()
        {
            try
            {
                if (tedPound.Text.Trim() == string.Empty) return;
                Clipboard.SetText((float.Parse(tedPound.Text.Trim()) / 1000).ToString("#0.00"));
            }
            catch (Exception)
            {                
            }
        }
        /// <summary>
        /// 切换到自动磅重
        /// </summary>
        /// <param name="bt">复制按钮</param>
        
        private void Bt_Click2(object sender, EventArgs e)
        {
            poundName = "";
            tedPound.Tag = null;
            tedPound.Text = "";            
            tedPoundWeight.Tag = null;
            tedPoundWeight.Text = "0";
            tedPoundWeightOut.Text = "0";
            lbState.Text = "自动磅重";
            lbState.ForeColor = Color.Black;
            btnGetPoundWeigth.Enabled = true;
            //try
            //{
            //    if (tedPoundWeight.Text.Trim() == string.Empty) return;
            //    Clipboard.SetText((float.Parse(tedPoundWeight.Text.Trim()) / 1000).ToString("#0.00"));
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("空的数据");
            //    return;
            //    //throw;
            //}
        }


        /// <summary>
        /// 过滤输入框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_TextChanged(object sender, EventArgs e)
        {
            this.gvFirtView.FindFilterText = tedFilter.Text.Trim();
            this.gridView2.FindFilterText = tedFilter.Text.Trim();
        }

       
        
        /// <summary>
        /// 过大豆选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSoyPound.CheckState == CheckState.Checked)
            {
                cbAutoCode.DropDownStyle = 0;
                cbAutoCode.Text = "";
                txbShipName.ReadOnly = false;
                txbShipNo.ReadOnly = false;
                glueCust.ReadOnly = true;
                glueDelivery.ReadOnly = false;
                glueDelivery.Text = "";
                glueMaterial.Text = "";
                glueCust.Text = "广西渤海农业发展有限公司";
                tedTransport.Text = "广西北海港物流有限公司";
                tedRemark.Text = "转基因大豆";
                tedTransport.ReadOnly = true;
                lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                try
                {
                    xq_open_usb_device();//打开USB连接
                }
                catch (Exception)
                {

                }
                //List<PubMaterialMD> lpm = new List<PubMaterialMD>();
                //PubMaterialManager pmm = new PubMaterialManager();
                //lpm = pmm.GetPubSoybeanMaterial();
                // glueMaterial.Properties.DataSource = lpm;
            }
            else if (cbSoyPound.CheckState != CheckState.Checked)
            {
                cbAutoCode.DropDownStyle = ComboBoxStyle.DropDown;
                tedTransport.Text = "";
                cbAutoCode.Text = "";
                glueDelivery.ReadOnly = false;
                glueCust.ReadOnly = false;
                glueCust.Text = "";
                glueDelivery.Text = "广西渤海农业发展有限公司";
                glueCust.Text = "";
                glueDelivery.ReadOnly = false;
                txbShipName.ReadOnly = true;
                txbShipNo.ReadOnly = true;
                tedRemark.Text = "加工原料为转基因大豆";
                //List<PubMaterialMD> lpm = new List<PubMaterialMD>();
                //PubMaterialManager pmm = new PubMaterialManager();
                txbShipNo.Text = string.Empty;
                txbShipName.Text = string.Empty;
                //lpm = pmm.GetPubMaterial();
               // glueMaterial.Properties.DataSource = lpm;
            }
            FirstIn();
        }
       


        /// <summary>
        /// 车号输入框事件
        /// </summary>
        /// <param name="cb"></param>

        private void Cb_TextUpdate(object sender, EventArgs e)
        {
            if (cbSoyPound.CheckState == CheckState.Checked) return;
            if(cbAutoCode.Text.Trim()=="")
            {
                cbAutoCode.DroppedDown = false;
                return;
            }
            //cbAutoCode.
            cbAutoCode.DroppedDown = true;
            cbAutoCode.Items.Clear();            
            listNew.Clear();
            foreach(string item in ls)
            {
                if (item.Contains(cbAutoCode.Text))
                {
                    listNew.Add(item);
                }
            }
            if (listNew.Count!=0)
            {
                
                cbAutoCode.Items.AddRange(listNew.ToArray());
                cbAutoCode.Select(cbAutoCode.Text.Length, 0);
                Cursor = Cursors.Default;
                
            }
            else
            {
                cbAutoCode.Text = cbAutoCode.Text;
                cbAutoCode.Select(cbAutoCode.Text.Length, 0);
                cbAutoCode.Items.Add("");
                cbAutoCode.DroppedDown = false;
            }
        }
        List<string> ls = new List<string>();
        List<string> listNew = new List<string>();
        /// <summary>
        /// 车号输入框中车辆信息绑定，所有已经完成进出厂的车辆都可以在Combobox中查询到
        /// </summary>
        private void ComBinding()
        {            
            PubAutoCodeListManager pclm = new PubAutoCodeListManager();
            List<PubAutoCodeListMD> lpcl = pclm.GetAllAutoCode();            
            ls.Clear();
            string[] data = new string[lpcl.Count] ;            
            for (int i = 0; i < lpcl.Count; i++)
            {
                ls.Add( lpcl[i].AutoCode.ToString());                
            }
            if (cbSoyPound.CheckState== CheckState.Checked)
            {
                cbAutoCode.Items.Clear();
            }
            
        }

        /// <summary>
        /// 统计净重
        /// </summary>
        private void TotalSet()
        {
            gridView2.Columns["QTYNet"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "QTYNet", "统计：{0:n2}");
        }

        /// <summary>
        /// 获取地磅数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_Click1(object sender, EventArgs e)
        {
            // MessageBox.Show(tedPoundWeight.Text.Trim());
            poundName = "筒仓地磅 进";
            light = "in";
            tedPound.Text = tedPoundWeight.Text.Trim();
            if (tedPoundWeight.Tag!=null)
            {
                tedPound.Tag = 0;
                //btnGetPoundWeigth.Enabled = false;
            } 
            else
            {
                tedPound.Tag = null;
            }
            //tedPoundWeight.Tag = null;
            GetTarePoundByPound(cbAutoCode.Text.Trim());
        }

        #endregion

        #region---------------------方法------------------------
       /// <summary>
       /// 获取物料输入框快捷方式泛型
       /// </summary>
        private void GetMatShortKeyList()
        {
            PubMatShortKeyManager ps = new PubMatShortKeyManager();
            lm = ps.GetAllMatShortKey();
        }
        
        /// <summary>
        /// 把打印机名字写到文本中
        /// </summary>
        private void WriteTxt()
        {
            string str = cbbPrintSet.Text.Trim();
            StreamWriter sw = new StreamWriter(Environment.CurrentDirectory+ "\\setPrint.txt", false);
            sw.WriteLine(str);
            sw.Close();
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
            cbbPrintSet.Text = str;
        }

        #region ---插入及更新方法--
        /// <summary>
        /// 插入或更新车号量磅重方法
        /// </summary>
        private void JudgeIn()
        {
            if (cbAutoCode.Text.Trim() == string.Empty)
            {
                MessageBox.Show("车号不能为空");
                return;
            }            
            PoundTotalManager ptm = new PoundTotalManager();
            bool isSoybean = cbSoyPound.CheckState == CheckState.Checked ? true : false;
            int a = ptm.Judge(cbAutoCode.Text.Trim(), decimal.Parse(tedPound.Text.Trim()), poundName, glueMaterial.Text.Trim(),
                glueCust.Text.Trim(), txbShipName.Text.Trim(), txbShipNo.Text.Trim(), LoginFrm.pubDelIn.ID,
                LoginFrm.pubDelIn.Name, tedRemark.Text.Trim(), tedTransport.Text.Trim(), glueDelivery.Text.Trim(), tedPound.Tag, isSoybean);
            switch (a)
            {
                case 0:
                    lbStatus.Text = "失败";
                    // ComBinding();
                    break;
                case 1:
                    lbStatus.Text = "第一次过磅成功";
                    btnSave.Text = "继  续";
                    //cbAutoCode.Text = "";                    
                    ComBinding();
                    CopyClipboard();
                    
                    // tedPound.Text = "";
                    break;
                case 2:
                    lbStatus.Text = "毛重过磅成功";
                    btnSave.Text = "继  续";
                    //List<PoundTotalMD> lp = new List<PoundTotalMD>();
                    lp = ptm.GetAllPoundList(cbAutoCode.Text.Trim());
                    if (lp.Count > 0)
                    {
                        tedGross.Text = lp[0].QTYGross.ToString();
                        tedNet.Text = lp[0].QTYNet.ToString();
                        tedTare.Text = lp[0].QTYTare.ToString();                       
                        lp[0].GrossStfName = LoginFrm.pubDelIn.Name;
                        lp[0].PrintStfName = LoginFrm.pubDelIn.Name;
                        if (cbSoyPound.CheckState == CheckState.Unchecked)
                        {

                            PrintDirect(lp[0]);
                        }
                        else
                        {
                            PrintDirectSoybean(lp[0]);
                        }
                        EndOut();
                        ComBinding();
                        CopyClipboard();
                        //PrintDirect(lp[0]);
                    }                    
                    break;
                case 3:
                    lbStatus.Text = "皮重毛重过磅成功";
                    btnSave.Text = "继  续";
                    // List<PoundTotalMD> lp1 = new List<PoundTotalMD>();
                    lp = ptm.GetAllPoundList(cbAutoCode.Text.Trim());
                    if (lp.Count > 0)
                    {
                        tedGross.Text = lp[0].QTYGross.ToString();
                        tedNet.Text = lp[0].QTYNet.ToString();
                        tedTare.Text = lp[0].QTYTare.ToString();                       
                        lp[0].GrossStfName = LoginFrm.pubDelIn.Name;
                        lp[0].PrintStfName = LoginFrm.pubDelIn.Name;
                        if (cbSoyPound.CheckState == CheckState.Unchecked)
                        {
                            PrintDirect(lp[0]);
                           // Print();
                        }
                        else
                        {
                            PrintDirectSoybean(lp[0]);
                        }                        
                        EndOut();
                        ComBinding();
                        CopyClipboard();                        
                    }

                    break;
                default:
                    break;
            }


        }
        #endregion

        #region --Gridview相关--





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
            gridview.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "IsManual", FieldName = "IsManual", Caption = "是否为手动磅重", VisibleIndex = 16, Visible =Enabled });
            gridview.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridview.Columns["GrossTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";

        }
        /// <summary>
        /// 手动添加第一次过磅显示Gridview列
        /// </summary>
        /// <param name="gridView"></param>
        private void AddColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            //gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "pur01", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled };
            //gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "No", FieldName = "No", Caption = "No", VisibleIndex = 1, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "AutoCode", FieldName = "AutoCode", Caption = "车号", VisibleIndex = 2, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "MatName", FieldName = "MatName", Caption = "物料名", VisibleIndex = 3, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "QTYTare", FieldName = "QTYTare", Caption = "皮重(kg)", VisibleIndex = 4, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TareTime", FieldName = "TareTime", Caption = "皮重时间", VisibleIndex = 5, Visible = Enabled });
            gridView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "PoundTareName", FieldName = "PoundTareName", Caption = "地磅名", VisibleIndex = 0, Visible = Enabled });
            gridView.Columns["TareTime"].DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";
            gridView.OptionsView.ColumnAutoWidth = true;
        }

        /// <summary>
        /// 获取Gridview中行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gv_RowClick1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            selectRow = (sender as DevExpress.XtraGrid.Views.Grid.GridView).GetDataSourceRowIndex(e.RowHandle);
        }
        #endregion


        /// <summary>
        /// 检查车辆是否在同一磅上过磅
        /// </summary>
        private void GetDataSours()
        {
            PubBlackAutoCodeManager pubBlackAutoCodeManager = new PubBlackAutoCodeManager();
            List<PubBlackAutoCodeMD> pubAutoCodeMDs = pubBlackAutoCodeManager.getPubBlackCodeByAutocode(cbAutoCode.Text.Trim());
            if(pubAutoCodeMDs.Count > 0)
            {
                MessageBox.Show("黑名单车辆："+pubAutoCodeMDs[0].BlackTime.ToShortDateString()+",违规内容："+pubAutoCodeMDs[0].reason,"黑名单");
            } 
            List<PoundTotalMD> lp = (List<PoundTotalMD>)this.gcFirstControl.DataSource;
            if (lp.Count <= 0) return;//如果厂内没车辆，退出

            var result = lp.Find(delegate (PoundTotalMD poundTotalMD)
            {
                return poundTotalMD.AutoCode == cbAutoCode.Text;
            }
            );
            if (result == null) return;//如果车辆没有在进厂车辆内，退出
            else
            {
                
                if (result.PoundTareName.ToString() != poundName)
                {
                    DialogResult DR = MessageBox.Show("该车进厂过磅为：  " + result.PoundTareName.ToString() + " !是否确定从此磅过磅？", "提示框", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (DR == DialogResult.OK) return;
                    cbAutoCode.Text = "";
                };
            }
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
                this.gridView2.ExportToXls(filename);
                MessageBox.Show("数据导出成功!", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }
        /// <summary>
        /// 插入一行车辆补重量信息
        /// </summary>
        /// <param name="rebackCheckMD"></param>
        private void AddRebackQty(RebackCheckMD rebackCheckMD)
        {
            RebackCheckManager rebackCheckManager = new RebackCheckManager();
            if( rebackCheckManager.AddReback(rebackCheckMD)==true)
            {
                lbView.Text=cbAutoCode.Text+"记录添加成功！";
            }
        }
        /// <summary>
        /// 检查车号，判断是否有通知单，并且从通知单中读取相关资料
        /// </summary>
        private void CheckAutoCode()
        {
            if (cbAutoCode.Text == string.Empty)
            {
                return;
            }
            if (cbSoyPound.Checked == false)//非大豆车过磅
            {
                if (ckManual.CheckState == CheckState.Checked) return;
                //此处需要增加一个是否连接3S服务器选项，否则，如果网络故障，软件没有商品信息、客户等信息带出
                try
                {
                    ReachArriveManager ram = new ReachArriveManager();
                    List<ReachArriveMD> ls = ram.GetReachArrive(cbAutoCode.Text.Trim());
                    if (ls.Count > 1)
                    {
                        lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        GLUEMatBinding();
                        this.glueChooseMat.ShowPopup();

                    }
                    else if (ls.Count == 1)
                    {

                        this.lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        glueCust.Text = ls[0].ToCust;
                        if (ls[0].SuppName == string.Empty)
                        {
                            glueDelivery.Text = "广西渤海农业发展有限公司";
                        }
                        else
                        {
                            glueDelivery.Text = ls[0].SuppName;

                        }
                        if (ls[0].StockID == 1224884)
                        {
                            tedRemark.Text = "加工原料为转基因大豆";
                        }
                        else
                        {
                            tedRemark.Text = "";
                        }
                        if (ls[0].MatID == 2057380)
                        {
                            glueCust.Text = "广西渤海农业发展有限公司";
                        }
                        glueMaterial.Text = ls[0].MatName;
                        lbAutoCodeError.Text = "";
                    }
                    else
                    {
                        lbAutoCodeError.Text = "车号 “" + cbAutoCode.Text + "” 通知单检查失败！";
                        glueCust.Text = "";
                        glueMaterial.Text = "";
                    }
                }
                catch (Exception)
                {
                    lbAutoCodeError.Text = "网络故障，信息无法带出！";
                    glueCust.Text = "";
                    glueMaterial.Text = "";
                }
            }
            else
            {
                lbAutoCodeError.Text = "";
                tedNet.Text = "";
                tedPound.Text = "";
                tedTare.Text = "";
                tedGross.Text = "";
                for (int i = 0; i < lpc.Count; i++)
                {
                    if (cbAutoCode.Text.Trim() == lpc[i].Number.ToString())
                    {
                        cbAutoCode.Text = lpc[i].autocode;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 检查车号，判断是否有通知单，并且从通知单中读取相关资料
        /// </summary>
        private void CheckAutoCodeByPound()
        {
            if (cbAutoCode.Text == string.Empty)
            {
                return;
            }
            if (cbSoyPound.Checked == false)//非大豆车过磅
            {
                if (ckManual.CheckState == CheckState.Checked) return;
                //此处需要增加一个是否连接3S服务器选项，否则，如果网络故障，软件没有商品信息、客户等信息带出
                try
                {
                    ReachArriveManager ram = new ReachArriveManager();
                    List<ReachArriveMD> ls = ram.GetReachArrive(cbAutoCode.Text.Trim());
                    if (ls.Count > 1)
                    {
                        lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        GLUEMatBinding();
                        //this.glueChooseMat.ShowPopup();

                    }
                    else if (ls.Count == 1)
                    {

                        this.lclChooseMat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                       // glueCust.Text = ls[0].ToCust;
                        //if (ls[0].SuppName == string.Empty)
                        //{
                        //    glueDelivery.Text = "广西渤海农业发展有限公司";
                        //}
                        //else
                        //{
                        //    glueDelivery.Text = ls[0].SuppName;

                        //}
                        //if (ls[0].StockID == 1224884)
                        //{
                        //    tedRemark.Text = "加工原料为转基因大豆";
                        //}
                        //else
                        //{
                        //    tedRemark.Text = "";
                        //}
                        //if (ls[0].MatID == 2057380)
                        //{
                        //    glueCust.Text = "广西渤海农业发展有限公司";
                        //}
                        //glueMaterial.Text = ls[0].MatName;
                        lbAutoCodeError.Text = "";
                    }
                    else
                    {
                        lbAutoCodeError.Text = "车号 “" + cbAutoCode.Text + "” 通知单检查失败！";
                        //glueCust.Text = "";
                        //glueMaterial.Text = "";
                    }
                }
                catch (Exception)
                {
                    lbAutoCodeError.Text = "网络故障，信息无法带出！";
                    glueCust.Text = "";
                    glueMaterial.Text = "";
                }
            }
            else
            {
                lbAutoCodeError.Text = "";
                //tedNet.Text = "";
                //tedGross.Text = "";
                //tedPound.Text = "";
                //tedTare.Text = "";
                for (int i = 0; i < lpc.Count; i++)
                {
                    if (cbAutoCode.Text.Trim() == lpc[i].Number.ToString())
                    {
                        cbAutoCode.Text = lpc[i].autocode;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 根据车号获取第一次皮重信息
        /// </summary>
        /// <param name="AutoCode"></param>
        private void GetTarePound(string AutoCode)
        {
            FirstIn();
            List<PoundTotalMD> lp = new List<PoundTotalMD>();
            lp = (List<PoundTotalMD>)this.gcFirstControl.DataSource;
            PoundTotalMD pt = lp.Find(delegate (PoundTotalMD poundTotalMD)
              {
                  return poundTotalMD.AutoCode == AutoCode;
              });
            if (pt == null)
            {
                CheckTareQty();//查询历史皮重
                CheckAutoCode();
            }
            else //if(cbSoyPound.CheckState== CheckState.Unchecked)
            {               
                tedTare.Text = pt.QTYTare.ToString();
                glueMaterial.Text = pt.MatName;
                glueCust.Text = pt.Customer;
                txbShipName.Text = pt.ShipName;
                txbShipNo.Text = pt.ShipNo.ToString();
                tedTransport.Text = pt.TransportCo;
                glueDelivery.Text = pt.DeliveryCo;
                tedRemark.Text = pt.Remark;
                lbStatus.Text = "车辆出厂。";
                if (cbAutoCode.Text == string.Empty) return;
                if (tedPound.Text == string.Empty) return;
                if (pt.QTYTare > decimal.Parse(tedPound.Text.Trim()))
                {
                    tedNet.Text = (pt.QTYTare - decimal.Parse(tedPound.Text.Trim())).ToString();
                }
                else
                {
                    tedNet.Text = (decimal.Parse(tedPound.Text.Trim()) - pt.QTYTare).ToString();
                }

            }   
           
        }
        /// <summary>
        /// 绑定数据到选择物料框
        /// </summary>
        private void GLUEMatBinding()
        {
            ReachArriveManager ram = new ReachArriveManager();
            List<ReachArriveMD> lr= ram.GetReachArrive(cbAutoCode.Text);
            this.glueChooseMat.Properties.DataSource = lr;
            this.glueChooseMat.Properties.ValueMember = "ID";
            this.glueChooseMat.Properties.DisplayMember = "BilNo";
        }

        /// <summary>
        /// 根据车号获取第一次皮重信息
        /// </summary>
        /// <param name="AutoCode"></param>
        private void GetTarePoundByPound(string AutoCode)
        {
            List<PoundTotalMD> lp = new List<PoundTotalMD>();
            lp = (List<PoundTotalMD>)this.gcFirstControl.DataSource;
            PoundTotalMD pt = lp.Find(delegate (PoundTotalMD poundTotalMD)
            {
                return poundTotalMD.AutoCode == AutoCode;
            });
            if (pt == null)
            {
                return;
               // CheckAutoCodeByPound();
            }
            else //if (cbSoyPound.CheckState == CheckState.Unchecked)
            {
                tedTare.Text = pt.QTYTare.ToString();
                //glueMaterial.Text = pt.MatName;
               // glueCust.Text = pt.Customer;
               // txbShipName.Text = pt.ShipName;
               // txbShipNo.Text = pt.ShipNo.ToString();
               // tedTransport.Text = pt.TransportCo;
               // glueDelivery.Text = pt.DeliveryCo;
               // tedRemark.Text = pt.Remark;
                lbStatus.Text = "车辆出厂。";
                if (cbAutoCode.Text == string.Empty) return;
                if (tedPound.Text == string.Empty) return;
                if (pt.QTYTare > decimal.Parse(tedPound.Text.Trim()))
                {
                    tedNet.Text = (pt.QTYTare - decimal.Parse(tedPound.Text.Trim())).ToString();
                }
                else
                {
                    tedNet.Text = (decimal.Parse(tedPound.Text.Trim()) - pt.QTYTare).ToString();
                }

            }
        }


        #endregion

        
    }
}
