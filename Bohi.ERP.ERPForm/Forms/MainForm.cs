using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using Bohi.ERP.ERPForm.UserControls;

namespace Bohi.ERP.ERPForm.Forms
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();  
            InitializeSet();
        }
        private void InitializeSet()
        {
            Initialize_BarButtonItem(bbiAutomaticPound);
            Initialize_BarButtonItem(bbiPoundD);
            Initialize_BarButtonItem(bbiAddUser);
            Initialize_BarButtonItem(bbiChangePound);
            Initialize_BarButtonItem(bbiShortKey);
            Initialize_BarButtonItem(bbiSearchOldData);
            Initialize_BarButtonItem(bbiSoybeanInStore);
            Initialize_BarButtonItem(bbiCustoms);
            ChangePassword(bbiChangePassword);
            SetLoginName();
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;//添加该属性，为了解决调用集成在dll中的dev窗口显示不正常问题
            this.Ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;//设置Ribbon控件的展开按钮一直显示
        }
        

        private void SetLoginName()
        {
            bsiUserName.Caption = LoginFrm.pubDelIn.Name;
            bsiUserName.Tag = LoginFrm.pubDelIn.ID;

        }
        #region -------------------主界面打开按钮事件及方法-----------------------
        /// <summary>
        /// 打开子界面
        /// </summary>
        /// <param name="barButtonItem">主界面按钮</param>
        private void Initialize_BarButtonItem(BarButtonItem barButtonItem)
        {
            barButtonItem.ItemClick += BarButtonItem_ItemClick;
        }

        private void BarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            //打开等待窗体
            SplashScreenManager.ShowDefaultWaitForm();
            //获取用户控件的类型
            string userControlName = e.Item.Name.Substring(3);//用户控件的简单名称           
            string userControlAssemblyName = String.Format("Bohi.ERP.ERPForm.UserControls.{0}UCtrl", userControlName);//用户控件的全名
            Type type = Type.GetType(userControlAssemblyName);
            if (type == null)
            {
                MessageBox.Show(String.Format("无法加载用户控件：{0}！", userControlAssemblyName));
                return;
            }

            //遍历打开的所有子窗体，判断是否含有与（type）相同类型用户控件的子窗体
            foreach (Form frm in this.MdiChildren)
            {
                MdiChildrenForm form = frm as MdiChildrenForm;
                if (form.FormControl.GetType() == type)//若含有与（type）相同类型的用户控件，则激活，并退出
                {
                    frm.Activate();
                    SplashScreenManager.CloseDefaultWaitForm();
                    return;
                }
            }

            //新建一个基本窗体
            MdiChildrenForm mdiChildrenForm = new MdiChildrenForm();
            mdiChildrenForm.MdiParent = this;
            mdiChildrenForm.Text = e.Item.Caption;
            mdiChildrenForm.Show();

            //基本窗体上添加该用户控件
            BaseUserControl control = (BaseUserControl)Activator.CreateInstance(type);
            mdiChildrenForm.FormControl = control;            
            //关闭等待窗体
            SplashScreenManager.CloseDefaultWaitForm();
        } 
        #endregion
        /// <summary>
        /// 打开修改密码页面
        /// </summary>
        /// <param name="bbi"></param>
        private void ChangePassword(DevExpress.XtraBars.BarBaseButtonItem bbi)
        {
            bbi.ItemClick += Bbi_ItemClick;
        }

        private void Bbi_ItemClick(object sender, ItemClickEventArgs e)
        {            
            FrmChangePassword fcp = new FrmChangePassword();
            fcp.ShowDialog();
        }

        
    }
}