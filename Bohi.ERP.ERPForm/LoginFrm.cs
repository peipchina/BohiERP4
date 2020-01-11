using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bohi.ERP.MODEL;
using Bohi.ERP.BLL;

namespace Bohi.ERP.ERPForm
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();
            EnterButton();
        }
        private void EnterButton()
        {
            AcceptButton = btnLogin;
        }
        /// <summary>
        /// 取消登录，退出系统
        /// </summary>
        /// <param name="bt">取消按钮</param>
        private void CloseButton(Button bt)
        {
            bt.Click += Bt_Click;
        }

        private void Bt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contrastPassWord()
        {
            PubLoginMD su = new PubLoginMD();
            su.LoginName = txbUserName.Text.Trim();
            List<PubLoginMD> lsu = new List<PubLoginMD>();
            PubLoginManager sum = new PubLoginManager();
            lsu = sum.getPassWordByLoginName(txbUserName.Text.Trim());
            encry en = new encry();
            string PassWord = en.MD5Dec(txbPassword.Text.Trim());
            if (lsu.Count == 0)
            {
                MessageBox.Show("用户名不存在", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (PassWord == lsu[0].PassWord)
            {
                this.DialogResult = DialogResult.OK;
                pubDelIn = lsu[0];
                this.Close();
            }
            else
            {
                MessageBox.Show("密码不正确！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public static string loginName = "";
        public static PubLoginMD pubDelIn = null;
        
        private void LoginButton(Button bt)
        {
            bt.Click += Bt_Click1;
        }

        private void Bt_Click1(object sender, EventArgs e)
        {
            if (txbUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("用户名不能为空！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbUserName.Focus();
                return;
            }
            if (txbPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("密码不能为空！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbPassword.Focus();
                return;
            }
            contrastPassWord();
            loginName = txbUserName.Text.Trim();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            CloseButton(btnCacle);
            LoginButton(btnLogin);
        }
    }
}
