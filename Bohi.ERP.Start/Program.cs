using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using Bohi.ERP.ERPForm;
using Bohi.ERP.ERPForm.Forms;
using System.Diagnostics;
using UpConfig;

namespace Bohi.ERP.Start
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UpDateConfig udc = new UpDateConfig();
            if (udc.judgeUpdate() == true)
            {
                Process.Start(Environment.CurrentDirectory + "\\AutoUpdate.exe");///更新程序
            }
            else
            {
                LoginFrm lg = new LoginFrm();
                lg.ShowDialog();
                if (lg.DialogResult == DialogResult.OK)
                {
                    BonusSkins.Register();
                    SkinManager.EnableFormSkins();
                    UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
                    MainForm mf = new MainForm();
                    Application.Run(mf);
                    //Application.Run(new MainForm());//密码正确，进入主界面。
                }
            } 
        }
    }
}
