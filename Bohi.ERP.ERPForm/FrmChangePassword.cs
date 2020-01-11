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
    public partial class FrmChangePassword : Form
    {
        public FrmChangePassword()
        {
            InitializeComponent();
            CancleButton(btnCacle);
            ChangeButton(btnChange);
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="bt"></param>
        private void CancleButton(Button bt)
        {
            bt.Click += Bt_Click;
        }

        private void Bt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChangeButton(Button bt)
        {
            bt.Click += Bt_Click1;
        }

        private void Bt_Click1(object sender, EventArgs e)
        {
            string info = "";
            info += String.IsNullOrEmpty(txbOldPassWord.Text.Trim()) ? "旧密码不能为空!\r\n" : "";
            info += String.IsNullOrEmpty(txbNewPassword.Text.Trim()) ? "新密码不能为空!\r\n" : "";
            info += String.IsNullOrEmpty(txbCheckPassword.Text.Trim()) ? "确认密码不能为空!\r\n" : "";
            info += txbNewPassword.Text.Trim() != txbCheckPassword.Text.Trim() ? "确认密码不一致!\r\n" : "";
            if (!String.IsNullOrEmpty(info))
            {
                MessageBox.Show(info, "提示框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<PubLoginMD> lsu = new List<PubLoginMD>();
            PubLoginManager sum = new PubLoginManager();
            lsu = sum.getPassWordByLoginName(LoginFrm.loginName);
            encry en = new encry();
            string PassWord = en.MD5Dec(txbOldPassWord.Text.Trim());
            string ou = "";
            ou += PassWord != lsu[0].PassWord ? "登录密码不正确" : "";
            if (!string.IsNullOrEmpty(ou))
            {
                MessageBox.Show(ou, "提示框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PubLoginMD pdi = new PubLoginMD { LoginName = LoginFrm.loginName, PassWord = en.MD5Dec(txbNewPassword.Text.Trim()) };
            bool a = sum.UpPassWord(pdi);
            if (a == true)
            {
                MessageBox.Show("密码修改成功", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
            else
            {
                MessageBox.Show("密码修改失败", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
