namespace Bohi.ERP.ERPForm.UserControls
{
    partial class PoundUCtrl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoundUCtrl));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dteEndTime = new DevExpress.XtraEditors.DateEdit();
            this.dteStartTime = new DevExpress.XtraEditors.DateEdit();
            this.cbSoyPound = new System.Windows.Forms.CheckBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnGetPoundWeigth = new System.Windows.Forms.Button();
            this.tedPoundWeight = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.glueCustomer = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glueMaterial = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbAutoCodeError = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnEditPrint = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tedNet = new System.Windows.Forms.TextBox();
            this.tedGross = new System.Windows.Forms.TextBox();
            this.tedTare = new System.Windows.Forms.TextBox();
            this.tedAtuoCode = new System.Windows.Forms.TextBox();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcFirstControl = new DevExpress.XtraGrid.GridControl();
            this.gvFirtView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcEndControl = new DevExpress.XtraGrid.GridControl();
            this.gvEndView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.bsiUserName = new DevExpress.XtraBars.BarStaticItem();
            this.spCom1 = new System.IO.Ports.SerialPort(this.components);
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tedPoundWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glueCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glueMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFirstControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFirtView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcEndControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEndView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1441, 573);
            this.splitContainerControl1.SplitterPosition = 193;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dteEndTime);
            this.groupBox1.Controls.Add(this.dteStartTime);
            this.groupBox1.Controls.Add(this.cbSoyPound);
            this.groupBox1.Controls.Add(this.groupControl1);
            this.groupBox1.Controls.Add(this.glueCustomer);
            this.groupBox1.Controls.Add(this.glueMaterial);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbAutoCodeError);
            this.groupBox1.Controls.Add(this.lbStatus);
            this.groupBox1.Controls.Add(this.btnEditPrint);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tedNet);
            this.groupBox1.Controls.Add(this.tedGross);
            this.groupBox1.Controls.Add(this.tedTare);
            this.groupBox1.Controls.Add(this.tedAtuoCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1441, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dteEndTime
            // 
            this.dteEndTime.EditValue = null;
            this.dteEndTime.Location = new System.Drawing.Point(603, 147);
            this.dteEndTime.Name = "dteEndTime";
            this.dteEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEndTime.Size = new System.Drawing.Size(131, 20);
            this.dteEndTime.TabIndex = 16;
            // 
            // dteStartTime
            // 
            this.dteStartTime.EditValue = null;
            this.dteStartTime.Location = new System.Drawing.Point(385, 147);
            this.dteStartTime.MenuManager = this.barManager;
            this.dteStartTime.Name = "dteStartTime";
            this.dteStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStartTime.Size = new System.Drawing.Size(134, 20);
            this.dteStartTime.TabIndex = 16;
            // 
            // cbSoyPound
            // 
            this.cbSoyPound.AutoSize = true;
            this.cbSoyPound.Location = new System.Drawing.Point(970, 30);
            this.cbSoyPound.Name = "cbSoyPound";
            this.cbSoyPound.Size = new System.Drawing.Size(74, 18);
            this.cbSoyPound.TabIndex = 15;
            this.cbSoyPound.Text = "大豆过磅";
            this.cbSoyPound.UseVisualStyleBackColor = true;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCopy);
            this.groupControl1.Controls.Add(this.btnGetPoundWeigth);
            this.groupControl1.Controls.Add(this.tedPoundWeight);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(3, 18);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(306, 172);
            this.groupControl1.TabIndex = 14;
            this.groupControl1.Text = "磅重";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(154, 105);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "复 制";
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // btnGetPoundWeigth
            // 
            this.btnGetPoundWeigth.Location = new System.Drawing.Point(48, 105);
            this.btnGetPoundWeigth.Name = "btnGetPoundWeigth";
            this.btnGetPoundWeigth.Size = new System.Drawing.Size(75, 23);
            this.btnGetPoundWeigth.TabIndex = 1;
            this.btnGetPoundWeigth.Text = "读 数";
            this.btnGetPoundWeigth.UseVisualStyleBackColor = true;
            // 
            // tedPoundWeight
            // 
            this.tedPoundWeight.EditValue = "";
            this.tedPoundWeight.Location = new System.Drawing.Point(6, 29);
            this.tedPoundWeight.MenuManager = this.barManager;
            this.tedPoundWeight.Name = "tedPoundWeight";
            this.tedPoundWeight.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.tedPoundWeight.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 35F, System.Drawing.FontStyle.Bold);
            this.tedPoundWeight.Properties.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.tedPoundWeight.Properties.Appearance.Options.UseBackColor = true;
            this.tedPoundWeight.Properties.Appearance.Options.UseFont = true;
            this.tedPoundWeight.Properties.Appearance.Options.UseForeColor = true;
            this.tedPoundWeight.Properties.AutoHeight = false;
            this.tedPoundWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tedPoundWeight.Size = new System.Drawing.Size(223, 58);
            this.tedPoundWeight.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Iskoola Pota", 25F);
            this.label5.Location = new System.Drawing.Point(235, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 39);
            this.label5.TabIndex = 2;
            this.label5.Text = "KG";
            // 
            // glueCustomer
            // 
            this.glueCustomer.Location = new System.Drawing.Point(672, 103);
            this.glueCustomer.Name = "glueCustomer";
            this.glueCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glueCustomer.Properties.View = this.gridView1;
            this.glueCustomer.Size = new System.Drawing.Size(253, 20);
            this.glueCustomer.TabIndex = 13;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ID";
            this.gridColumn3.FieldName = "ID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "客户名";
            this.gridColumn4.FieldName = "Name";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // glueMaterial
            // 
            this.glueMaterial.Location = new System.Drawing.Point(385, 103);
            this.glueMaterial.MenuManager = this.barManager;
            this.glueMaterial.Name = "glueMaterial";
            this.glueMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glueMaterial.Properties.View = this.gridLookUpEdit1View;
            this.glueMaterial.Size = new System.Drawing.Size(226, 20);
            this.glueMaterial.TabIndex = 13;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(621, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 14);
            this.label7.TabIndex = 12;
            this.label7.Text = "客户：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(534, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 14);
            this.label9.TabIndex = 12;
            this.label9.Text = "结束时间：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(316, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "开始时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(315, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "货品名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(783, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "净重：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(621, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "毛重：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(463, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "皮重：";
            // 
            // lbAutoCodeError
            // 
            this.lbAutoCodeError.AutoSize = true;
            this.lbAutoCodeError.Font = new System.Drawing.Font("Tahoma", 15F);
            this.lbAutoCodeError.ForeColor = System.Drawing.Color.Red;
            this.lbAutoCodeError.Location = new System.Drawing.Point(1053, 30);
            this.lbAutoCodeError.Name = "lbAutoCodeError";
            this.lbAutoCodeError.Size = new System.Drawing.Size(0, 24);
            this.lbAutoCodeError.TabIndex = 10;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(1078, 127);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(38, 14);
            this.lbStatus.TabIndex = 10;
            this.lbStatus.Text = "label1";
            // 
            // btnEditPrint
            // 
            this.btnEditPrint.Location = new System.Drawing.Point(1072, 70);
            this.btnEditPrint.Name = "btnEditPrint";
            this.btnEditPrint.Size = new System.Drawing.Size(75, 23);
            this.btnEditPrint.TabIndex = 6;
            this.btnEditPrint.Text = "编 辑";
            this.btnEditPrint.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(959, 123);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打 印";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(756, 144);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查 询";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(959, 70);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "车号：";
            // 
            // tedNet
            // 
            this.tedNet.Location = new System.Drawing.Point(829, 46);
            this.tedNet.Name = "tedNet";
            this.tedNet.Size = new System.Drawing.Size(96, 22);
            this.tedNet.TabIndex = 3;
            // 
            // tedGross
            // 
            this.tedGross.Location = new System.Drawing.Point(672, 46);
            this.tedGross.Name = "tedGross";
            this.tedGross.Size = new System.Drawing.Size(96, 22);
            this.tedGross.TabIndex = 2;
            // 
            // tedTare
            // 
            this.tedTare.Location = new System.Drawing.Point(515, 46);
            this.tedTare.Name = "tedTare";
            this.tedTare.Size = new System.Drawing.Size(96, 22);
            this.tedTare.TabIndex = 1;
            // 
            // tedAtuoCode
            // 
            this.tedAtuoCode.Location = new System.Drawing.Point(358, 46);
            this.tedAtuoCode.Name = "tedAtuoCode";
            this.tedAtuoCode.Size = new System.Drawing.Size(96, 22);
            this.tedAtuoCode.TabIndex = 0;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gcFirstControl);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.gcEndControl);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1441, 375);
            this.splitContainerControl2.SplitterPosition = 728;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcFirstControl
            // 
            this.gcFirstControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcFirstControl.Location = new System.Drawing.Point(0, 0);
            this.gcFirstControl.MainView = this.gvFirtView;
            this.gcFirstControl.MenuManager = this.barManager;
            this.gcFirstControl.Name = "gcFirstControl";
            this.gcFirstControl.Size = new System.Drawing.Size(728, 375);
            this.gcFirstControl.TabIndex = 0;
            this.gcFirstControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFirtView});
            // 
            // gvFirtView
            // 
            this.gvFirtView.GridControl = this.gcFirstControl;
            this.gvFirtView.Name = "gvFirtView";
            this.gvFirtView.OptionsView.ShowGroupPanel = false;
            // 
            // gcEndControl
            // 
            this.gcEndControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcEndControl.Location = new System.Drawing.Point(0, 0);
            this.gcEndControl.MainView = this.gvEndView;
            this.gcEndControl.MenuManager = this.barManager;
            this.gcEndControl.Name = "gcEndControl";
            this.gcEndControl.Size = new System.Drawing.Size(708, 375);
            this.gcEndControl.TabIndex = 0;
            this.gcEndControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEndView});
            // 
            // gvEndView
            // 
            this.gvEndView.GridControl = this.gcEndControl;
            this.gvEndView.Name = "gvEndView";
            this.gvEndView.OptionsView.ShowGroupPanel = false;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "登陆名";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // bsiUserName
            // 
            this.bsiUserName.Caption = "bsiUserName";
            this.bsiUserName.Id = 1;
            this.bsiUserName.Name = "bsiUserName";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem3.Caption = "刷新";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem4.Caption = "刷新";
            this.barButtonItem4.Id = 2;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // PoundUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "PoundUCtrl";
            this.Size = new System.Drawing.Size(1441, 597);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tedPoundWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glueCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glueMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFirstControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFirtView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcEndControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEndView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem bsiUserName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tedAtuoCode;
        private System.IO.Ports.SerialPort spCom1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tedNet;
        private System.Windows.Forms.TextBox tedGross;
        private System.Windows.Forms.TextBox tedTare;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.GridLookUpEdit glueCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GridLookUpEdit glueMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Button btnGetPoundWeigth;
        private DevExpress.XtraEditors.TextEdit tedPoundWeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbSoyPound;
        private System.Windows.Forms.Label lbAutoCodeError;
        private System.Windows.Forms.Button btnCopy;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraGrid.GridControl gcFirstControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFirtView;
        private DevExpress.XtraGrid.GridControl gcEndControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEndView;
        private DevExpress.XtraEditors.DateEdit dteEndTime;
        private DevExpress.XtraEditors.DateEdit dteStartTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnEditPrint;
    }
}
