namespace Bohi.ERP.ERPForm.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, null, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.bsiUserName = new DevExpress.XtraBars.BarStaticItem();
            this.bbiPoundD = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAutomaticPound = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAddUser = new DevExpress.XtraBars.BarButtonItem();
            this.bbiChangePassword = new DevExpress.XtraBars.BarButtonItem();
            this.bbiChangePound = new DevExpress.XtraBars.BarButtonItem();
            this.bbiShortKey = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSearchOldData = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSoybeanInStore = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCustoms = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager
            // 
            splashScreenManager.ClosingDelay = 500;
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barStaticItem1,
            this.bsiUserName,
            this.bbiPoundD,
            this.bbiAutomaticPound,
            this.bbiAddUser,
            this.bbiChangePassword,
            this.bbiChangePound,
            this.bbiShortKey,
            this.bbiSearchOldData,
            this.bbiSoybeanInStore,
            this.bbiCustoms});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 14;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage4,
            this.ribbonPage1});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.ribbon.Size = new System.Drawing.Size(871, 147);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "登陆名";
            this.barStaticItem1.Id = 3;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // bsiUserName
            // 
            this.bsiUserName.Caption = "UserName";
            this.bsiUserName.Id = 4;
            this.bsiUserName.Name = "bsiUserName";
            // 
            // bbiPoundD
            // 
            this.bbiPoundD.Caption = "托利多";
            this.bbiPoundD.Id = 5;
            this.bbiPoundD.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiPoundD.ImageOptions.Image")));
            this.bbiPoundD.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiPoundD.ImageOptions.LargeImage")));
            this.bbiPoundD.Name = "bbiPoundD";
            // 
            // bbiAutomaticPound
            // 
            this.bbiAutomaticPound.Caption = "barButtonItem1";
            this.bbiAutomaticPound.Id = 6;
            this.bbiAutomaticPound.Name = "bbiAutomaticPound";
            // 
            // bbiAddUser
            // 
            this.bbiAddUser.Caption = "添加用户";
            this.bbiAddUser.Id = 7;
            this.bbiAddUser.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiAddUser.ImageOptions.Image")));
            this.bbiAddUser.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiAddUser.ImageOptions.LargeImage")));
            this.bbiAddUser.Name = "bbiAddUser";
            // 
            // bbiChangePassword
            // 
            this.bbiChangePassword.Caption = "修改密码";
            this.bbiChangePassword.Id = 8;
            this.bbiChangePassword.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiChangePassword.ImageOptions.Image")));
            this.bbiChangePassword.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiChangePassword.ImageOptions.LargeImage")));
            this.bbiChangePassword.Name = "bbiChangePassword";
            // 
            // bbiChangePound
            // 
            this.bbiChangePound.Caption = "修改单据";
            this.bbiChangePound.Id = 9;
            this.bbiChangePound.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiChangePound.ImageOptions.Image")));
            this.bbiChangePound.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiChangePound.ImageOptions.LargeImage")));
            this.bbiChangePound.Name = "bbiChangePound";
            // 
            // bbiShortKey
            // 
            this.bbiShortKey.Caption = "设置快捷键";
            this.bbiShortKey.Id = 10;
            this.bbiShortKey.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiShortKey.ImageOptions.Image")));
            this.bbiShortKey.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiShortKey.ImageOptions.LargeImage")));
            this.bbiShortKey.Name = "bbiShortKey";
            // 
            // bbiSearchOldData
            // 
            this.bbiSearchOldData.Caption = "原数据查询";
            this.bbiSearchOldData.Id = 11;
            this.bbiSearchOldData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiSearchOldData.ImageOptions.Image")));
            this.bbiSearchOldData.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSearchOldData.ImageOptions.LargeImage")));
            this.bbiSearchOldData.Name = "bbiSearchOldData";
            // 
            // bbiSoybeanInStore
            // 
            this.bbiSoybeanInStore.Caption = "3S大豆录入";
            this.bbiSoybeanInStore.Id = 12;
            this.bbiSoybeanInStore.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSoybeanInStore.ImageOptions.LargeImage")));
            this.bbiSoybeanInStore.Name = "bbiSoybeanInStore";
            // 
            // bbiCustoms
            // 
            this.bbiCustoms.Caption = "海关地磅";
            this.bbiCustoms.Id = 13;
            this.bbiCustoms.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.bbiCustoms.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.bbiCustoms.Name = "bbiCustoms";
            // 
            // ribbonPage4
            // 
            this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4});
            this.ribbonPage4.Name = "ribbonPage4";
            this.ribbonPage4.Text = "托利多称重";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.bbiPoundD);
            this.ribbonPageGroup4.ItemLinks.Add(this.bbiCustoms);
            this.ribbonPageGroup4.ItemLinks.Add(this.bbiAddUser);
            this.ribbonPageGroup4.ItemLinks.Add(this.bbiChangePassword);
            this.ribbonPageGroup4.ItemLinks.Add(this.bbiChangePound);
            this.ribbonPageGroup4.ItemLinks.Add(this.bbiShortKey);
            this.ribbonPageGroup4.ItemLinks.Add(this.bbiSearchOldData);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "托利多";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "3S软件相关";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiSoybeanInStore);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "3S相关资料";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.ItemLinks.Add(this.bsiUserName);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 579);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(871, 31);
            // 
            // xtraTabbedMdiManager
            // 
            this.xtraTabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabbedMdiManager.MdiParent = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 610);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem bsiUserName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem bbiPoundD;
        private DevExpress.XtraBars.BarButtonItem bbiAutomaticPound;
        private DevExpress.XtraBars.BarButtonItem bbiAddUser;
        private DevExpress.XtraBars.BarButtonItem bbiChangePassword;
        private DevExpress.XtraBars.BarButtonItem bbiChangePound;
        private DevExpress.XtraBars.BarButtonItem bbiShortKey;
        private DevExpress.XtraBars.BarButtonItem bbiSearchOldData;
        private DevExpress.XtraBars.BarButtonItem bbiSoybeanInStore;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem bbiCustoms;
    }
}