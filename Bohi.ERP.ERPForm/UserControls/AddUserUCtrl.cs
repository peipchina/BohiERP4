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

namespace Bohi.ERP.ERPForm.UserControls
{
    public partial class AddUserUCtrl : BaseUserControl
    {
        public AddUserUCtrl()
        {
            InitializeComponent();
            CancleButton(btnCancle);
            AddUser(btnAddUser);
            FrmSet();
        }

        #region----------界面设置--------
        private void FrmSet()
        {
            this.Text = "添加用户";
        }
        #endregion

        #region --------------事件---------------------
        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="bt">取消按钮</param>
        private void CancleButton(Button bt)
        {
            bt.Click += Bt_Click;
        }

        private void Bt_Click(object sender, EventArgs e)
        {
            this.Parent.Dispose();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="bt">添加按钮</param>
        private void AddUser(Button bt)
        {
            bt.Click += Bt_Click1;
        }

        private void Bt_Click1(object sender, EventArgs e)
        {
            if (LoginFrm.loginName == "test" || LoginFrm.loginName == "yuanxb" || LoginFrm.loginName == "yuanxb")
            {
                AddUser();
                
            }
            else
            {
                MessageBox.Show("权限不足！"); return;
            }
        }
        #endregion

        #region --------------------------方法------------------------------

        private void AddUser()
        {
            if (txbLoginName.Text==string.Empty)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            if (txbPassWord.Text.Trim()==string.Empty)
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            if (txbPassWord.Text.Trim() != txbPassWordCheck.Text.Trim())
            {
                MessageBox.Show("两次密码不正确！");
                return;
            }
            if (txbUserName.Text.Trim()==string.Empty)
            {
                MessageBox.Show("姓名不能为空！");
                return;
            }
            PubLoginManager plm = new PubLoginManager();
            if (plm.CheckLoginName(txbLoginName.Text.Trim())==true)
            {
                MessageBox.Show("登陆名已经存在");
                return;
            }
            else
            {
                PubLoginMD pl = new PubLoginMD();
                pl.AddUser = false;
                pl.LoginName = txbLoginName.Text.Trim();
                pl.Name = txbUserName.Text.Trim();
                encry ec = new encry();
                pl.PassWord = ec.MD5Dec(txbPassWord.Text.Trim());                
                bool a= plm.AddNewUser(pl);
                if (a==true)
                {
                    MessageBox.Show("用户添加成功！");
                    txbUserName.Text = "";
                    txbPassWord.Text = "";
                    txbPassWordCheck.Text = "";
                    txbLoginName.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("用户添加失败！");
                    return;
                }
            }
        }

        #endregion
    }
}
