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

namespace Bohi.ERP.ERPForm.UserControls
{
    public partial class BaseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public BaseUserControl()
        {
            InitializeComponent();
            Initialize_Control();//执行自定义初始化设置
        }

        /// <summary>
        /// 自定义初始化设置入口
        /// </summary>
        private void Initialize_Control()
        {
            Initialize_BarManager(this.barManager);
        }

        #region 【>------------------------>【自定义属性】<------------------------<】
        private string text = "显示名称";
        /// <summary>
        /// 控件的标题（显示名称）
        /// </summary>
        public override string Text
        {
            get { return text; }
            set { text = value; }
        }

        #endregion

        #region 【>------------------------>【初始化“BarManager 工具栏控件”】【Initialize_BarManager（）】<------------------------<】
        /// <summary>
        /// 初始化“BarManager 工具栏控件”
        /// </summary>
        /// <param name="barManager"></param>
        private void Initialize_BarManager(BarManager barManager)
        {
            barManager.MainMenu.OptionsBar.AllowQuickCustomization = false;//禁用右边下拉箭头（禁止自定义工具栏）
            barManager.MainMenu.OptionsBar.DrawDragBorder = false;//禁止左边竖线（禁止拖动工具栏）

            //barManager.RightToLeft = DevExpress.Utils.DefaultBoolean.True;//设置工具栏图标靠右显示

            foreach (Bar bar in barManager.Bars)//设置除了主工具栏之外的工具栏隐藏
            {
                if (bar.IsMainMenu) continue;
                bar.Visible = false;
            }

            //设置主工具栏的所有按钮隐藏
            Bar mainMenu = barManager.MainMenu;
            foreach (LinkPersistInfo info in mainMenu.LinksPersistInfo)
            {
                info.Item.Visibility = BarItemVisibility.Never;//设置不可见
                info.Item.Alignment = BarItemLinkAlignment.Right;//设置靠右显示
            }
        }
        #endregion
    }
}
