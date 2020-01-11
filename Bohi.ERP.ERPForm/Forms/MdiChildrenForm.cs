using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Bohi.ERP.ERPForm.Forms
{
    public partial class MdiChildrenForm : DevExpress.XtraEditors.XtraForm
    {
        public MdiChildrenForm()
        {
            InitializeComponent();
        }

        private Control formControl = null;
        /// <summary>
        /// 界面的主控件（即界面的其它控件，都应包含在该控件中，而不是窗体中）
        /// </summary>
        public Control FormControl
        {
            get { return formControl; }
            set
            {
                //若界面中已存在该控件，则表示要替换该控件，先将其从界面中删除
                if (formControl != null)
                    this.Controls.Remove(formControl);

                //将该控件加入到控件集合中
                formControl = value;
                if (formControl != null)
                {
                    formControl.Dock = DockStyle.Fill;
                    this.Text = formControl.Text;//设置窗口标题为控件的标题
                    this.Controls.Add(formControl);
                }
            }
        }
    }
}