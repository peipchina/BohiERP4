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
    public partial class ShortKeyUCtrl :BaseUserControl
    {
        public ShortKeyUCtrl()
        {
            InitializeComponent();
            LoadSet();
            Initialize_All();
        }

        #region--------------------整体页面设置-------------------------

        #region --页面初始化--
        /// <summary>
        /// 页面加载时初始化内容
        /// </summary>
        private void LoadSet()
        {
            this.Text = "设置快捷键";
            tedModifyID.ReadOnly = true;
            tedModifyShortKey.ReadOnly = true;            
        }
        #endregion

        #region--功能--
        //判断输入框是否合法
        private bool CheckLegal()
        {
            if (tedAddShortKey.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(tedAddShortKey, "快捷键不能为空！");
                tedAddShortKey.Focus();
                return false;
            }
            if (tedAddName.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(tedAddName, "名称不能为空不能为空！");
                tedAddName.Focus();
                return false;
            }
            else return true;
        }
        /// <summary>
        /// 快捷键清除错误提示
        /// </summary>
        private void ClearnJudgeError()
        {
            errorProvider1.Clear();
        }
        #endregion

        #region--事件--
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnDelete_Click(object sender, EventArgs e)
        {
            if (rbtSoybean.Checked == true)
            {
                DeleteSoybean();//删除大豆车
            }
            if (rbtMatName.Checked==true)
            {
                DeleteMatShortKey();//删除物料
            }            
        }
        /// <summary>
        /// 取消事件，清空错误及输入内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnCancel_Click(object sender, EventArgs e)
        {
            tedAddName.Text = "";
            tedAddShortKey.Text = "";
            errorProvider1.Dispose();
        }
        int select = -1;//gridview列选择为-1；
        /// <summary>
        /// 点击列，获取列序列号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            select = this.gridView1.GetDataSourceRowIndex(e.RowHandle);
            ChangeTextBinding();
        }     
        /// <summary>
        /// 选择物料单选框，触发绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtMatName_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.Columns.Clear();
            SetGrivewMatName();
            GridviewDataBinding();
            lciAddLable.Text = "名      称：";
            lciModifyLable.Text = "名      称：";
            tedAddDeliveryCo.ReadOnly = false;
            tedAddCustomer.ReadOnly = false;
            tedAddTransportCo.ReadOnly = false;
            tedAddRemarks.ReadOnly = false;
            tedAddCustomer.ReadOnly = false;
            tedModifyCustomer.ReadOnly = false;
            tedModifyDeliveryCo.ReadOnly = false;
            tedModifyRemarks.ReadOnly = false;
            tedModifyTransportCo.ReadOnly = false;
            tedModifyID.Text = "";
            tedModifyCustomer.Text = "";
            tedModifyName.Text = "";
            tedModifyShortKey.Text = "";
            tedModifyTransportCo.Text = "";
            tedModifyRemarks.Text = "";
            tedModifyDeliveryCo.Text = "";
            GridviewDataBinding();
        }
        /// <summary>
        /// 选择大豆单选框，触发绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtSoybean_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.Columns.Clear();//清除Gridview列表表头内容
            SetGrivewSoybean();
            GridviewDataBinding();
            lciAddLable.Text = "车      号：";
            lciModifyLable.Text = "车      号：";
            tedAddDeliveryCo.ReadOnly = true;
            tedAddCustomer.ReadOnly = true;
            tedAddTransportCo.ReadOnly = true;
            tedAddRemarks.ReadOnly = true;
            tedAddCustomer.ReadOnly = true;
            tedModifyCustomer.ReadOnly = true;
            tedModifyDeliveryCo.ReadOnly = true;
            tedModifyRemarks.ReadOnly = true;
            tedModifyTransportCo.ReadOnly = true;
            tedModifyID.Text = "";
            tedModifyCustomer.Text = "";
            tedModifyName.Text = "";
            tedModifyShortKey.Text = "";
            tedModifyTransportCo.Text = "";
            tedModifyRemarks.Text = "";
            tedModifyDeliveryCo.Text = "";
            GridviewDataBinding();
        }
        
        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnAdd_Click(object sender, EventArgs e)
        {           
            if (CheckLegal()==true && rbtSoybean.Checked==true)
            {
                InsertSoybeanShortKey();
            }
            if (CheckLegal() == true && rbtMatName.Checked == true)
            {
                InsertMatShortKey();
            }
           
        }
        /// <summary>
        /// 修改快捷键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnModify_Click(object sender, EventArgs e)
        {
            if ( rbtSoybean.Checked == true)
            {
                ModifySoybean();
            }
            if (rbtMatName.Checked==true)
            {
                ModifyMatShortKey();
            }
           
        }
        /// <summary>
        /// 清除添加快捷输入框错误提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TedAddShortKey_TextChanged(object sender, EventArgs e)
        {
            ClearnJudgeError();
        }
        /// <summary>
        /// 添加名称输入框清除错误提示
        /// </summary>
        private void TedAddName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        /// <summary>
        /// 修改名称输入框清除错误提示
        /// </summary>
        private void TedModifyName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        #endregion

        #region--方法--
        /// <summary>
        /// 把数据绑定在GridView中
        /// </summary>
        private void GridviewDataBinding()
        {
            select = -1;//初始化点击gridviewd点击行列值
            if (rbtSoybean.Checked == true)
            {
                SoybeanAutoCodeBinding();
            }
            if (rbtMatName.Checked == true)
            {
                MatShortKeyBinding();
            }            
        }
        /// <summary>
        /// 设置大豆Gridview页面列表头内容
        /// </summary>
        private void SetGrivewSoybean()
        {
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(gridView1);
            this.gridView1.OptionsView.ColumnAutoWidth = true;
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShortKey", FieldName = "ShortKey", Caption = "快捷键", VisibleIndex = 2, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Name", FieldName = "Name", Caption = "名称", VisibleIndex = 3, Visible = Enabled });
        }
        /// <summary>
        /// 设置物料Gridview页面列表头内容
        /// </summary>
        private void SetGrivewMatName()
        {
            SetGridview sg = new SetGridview();
            sg.CustomizeGridView(gridView1);
            this.gridView1.OptionsView.ColumnAutoWidth = true;
            //gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ID", FieldName = "ID", Caption = "ID", VisibleIndex = 1, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShortKey", FieldName = "ShortKey", Caption = "快捷键", VisibleIndex = 2, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Name", FieldName = "Name", Caption = "名称", VisibleIndex = 3, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "TransportCo", FieldName = "TransportCo", Caption = "运输单位", VisibleIndex = 4, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "DeliveryCo", FieldName = "DeliveryCo", Caption = "发货单位", VisibleIndex = 5, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Customer", FieldName = "Customer", Caption = "客户", VisibleIndex = 6, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "ShipAndVoyage", FieldName = "ShipAndVoyage", Caption = "船号/航次", VisibleIndex = 6, Visible = Enabled });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Name = "Remaker", FieldName = "Remarks", Caption = "备注", VisibleIndex = 7, Visible = Enabled });
        }
        /// <summary>
        /// 把Gridview选择的列绑定到修改页面上
        /// </summary>
        private void ChangeTextBinding()
        {
            if (select < 0) return;
            if (rbtSoybean.Checked==true)//大豆车
            {
                var ls = this.gcShortKey.DataSource as List<ShortKeyNameMD>;
                tedModifyID.Text = ls[select].ID.ToString();
                tedModifyShortKey.Text = ls[select].ShortKey;
                tedModifyName.Text = ls[select].Name;                
            }
            if(rbtMatName.Checked==true)//物料
            {
                var ls = this.gcShortKey.DataSource as List<MatShortKeyMD>;
                tedModifyID.Text = ls[select].ID.ToString();
                tedModifyShortKey.Text = ls[select].ShortKey;
                tedModifyName.Text = ls[select].Name;
                tedModifyCustomer.Text = ls[select].Customer;
                tedModifyDeliveryCo.Text = ls[select].DeliveryCo;
                tedModifyTransportCo.Text = ls[select].TransportCo;
                tedModifyRemarks.Text = ls[select].Remarks;
                tedShipVoyageM.Text = ls[select].ShipAndVoyage;
            }
           
        }
        #endregion

        #region--初始化--

        private void Initialize_All()
        {
            sbtnAdd.Click += SbtnAdd_Click;//初始化添加按钮事件           
            tedAddShortKey.TextChanged += TedAddShortKey_TextChanged;//初始化第快捷键字符串变化事件
            tedAddName.TextChanged += TedAddName_TextChanged;//初始化添加名称输入框事件
            rbtSoybean.CheckedChanged += RbtSoybean_CheckedChanged;//初始化大豆单选框事件
            rbtMatName.CheckedChanged += RbtMatName_CheckedChanged;//初始化物料单选框事件           
            this.gridView1.RowClick += GridView1_RowClick;//初始化Gridview行点击事件
            sbtnCancel.Click += SbtnCancel_Click;//初始化取消点击事件
            sbtnDelete.Click += SbtnDelete_Click;//初始化删除事件
            sbtnModify.Click += SbtnModify_Click;//初始化修改事件
            tedModifyName.TextChanged += TedModifyName_TextChanged;//修改名称输入框清除错误提示
        }

        




        #endregion

        #endregion

        #region--------------------大豆车快捷方式增删改----------------------

        /// <summary>
        /// 把车辆信息绑定到Gridview中
        /// </summary>
        private void SoybeanAutoCodeBinding()
        {
            PubAutoCodeManager pacm = new PubAutoCodeManager();
            List<ShortKeyNameMD> ls = pacm.GetAllAutoShortKey();
            this.gcShortKey.DataSource = ls;
        }
        /// <summary>
        /// 添加一大豆车快捷键
        /// </summary>
        private void InsertSoybeanShortKey()
        {
            PubAutoCodeManager pacm = new PubAutoCodeManager();
            if (pacm.ChekShortKey(tedAddShortKey.Text.Trim()) == true)
            {
                errorProvider1.SetError(tedAddShortKey,"快捷方式已经存在！");
                tedAddShortKey.Focus();
                return;
            }
            if (pacm.ChekAutoCode(tedAddName.Text.Trim()) == true)
            {
               errorProvider1.SetError(tedAddName,"车号已经存在！");
                tedAddName.Focus();
                return;
            }
            PubAutoCodeMD pac = new PubAutoCodeMD();
            pac.autocode = tedAddName.Text.Trim();
            pac.Number = tedAddShortKey.Text.Trim();
            if (pacm.InsertSoybean(pac) == true)
            {
                MessageBox.Show("添加成功！");
            }
            tedAddName.Text = "";
            tedAddShortKey.Text = "";            
            GridviewDataBinding();
        } 
       
        /// <summary>
        /// 修改车辆信息
        /// </summary>
        private void ModifySoybean()
        {
            List<ShortKeyNameMD> ls = this.gcShortKey.DataSource as List<ShortKeyNameMD>;
            if (ls == null) return;
            if (select < 0) return;            
            PubAutoCodeManager pacm = new PubAutoCodeManager();
            if (pacm.ChekAutoCode(tedModifyName.Text.Trim()) == true)
            {
                errorProvider1.SetError(tedModifyName,"车号已经存在或未做修改！");
                tedModifyName.Focus();
                return;
            }
            ShortKeyNameMD sk = ls[select];
            PubAutoCodeMD pac = new PubAutoCodeMD();
            pac.ID = sk.ID;
            pac.Number = sk.ShortKey;
            pac.autocode = tedModifyName.Text.Trim();
            if( pacm.UpdataSoybean(pac)==true)
            {
                MessageBox.Show("修改成功！");
            }
            tedModifyID.Text = "";
            tedModifyName.Text = "";
            tedModifyShortKey.Text = "";
            GridviewDataBinding();
        }
        /// <summary>
        /// 删除车辆信息
        /// </summary>
        private void DeleteSoybean()
        {
            List<ShortKeyNameMD> ls = this.gcShortKey.DataSource as List<ShortKeyNameMD>;
            if (ls == null) return;
            if (select < 0) return;
            PubAutoCodeManager pacm = new PubAutoCodeManager();            
            ShortKeyNameMD sk = ls[select];
            PubAutoCodeMD pac = new PubAutoCodeMD();
            pac.ID = sk.ID;            
            if (pacm.DeleteSoybean(pac) == true)
            {
                MessageBox.Show("删除成功！");
            }
            tedModifyID.Text = "";
            tedModifyName.Text = "";
            tedModifyShortKey.Text = "";
            GridviewDataBinding();
        } 
        #endregion

        #region------------------物料快捷键增删改-------------------

        
        /// <summary>
        /// 把车辆信息绑定到Gridview中
        /// </summary>
        private void MatShortKeyBinding()
        {
            PubMatShortKeyManager pacm = new PubMatShortKeyManager();
            List<MatShortKeyMD> ls = pacm.GetAllMatShortKey();
            this.gcShortKey.DataSource = ls;
        }
        /// <summary>
        /// 添加一个物料快捷方式
        /// </summary>
        private void InsertMatShortKey()
        {
            PubMatShortKeyManager pacm = new PubMatShortKeyManager();
            if (pacm.ChekShortKey(tedAddShortKey.Text.Trim()) == true)
            {
                errorProvider1.SetError(tedAddShortKey, "快捷方式已经存在！");
                tedAddShortKey.Focus();
                return;
            }
            if (pacm.ChekMatName(tedAddName.Text.Trim()) == true)
            {
                errorProvider1.SetError(tedAddName, "物料名已经存在！");
                tedAddName.Focus();
                return;
            }
            MatShortKeyMD pac = new MatShortKeyMD();
            pac.Name = tedAddName.Text.Trim();
            pac.ShortKey = tedAddShortKey.Text.Trim();
            pac.Customer = tedAddCustomer.Text.Trim();
            pac.DeliveryCo = tedAddDeliveryCo.Text.Trim();
            pac.Remarks = tedAddRemarks.Text.Trim();
            pac.TransportCo = tedAddTransportCo.Text.Trim();
            pac.ShipAndVoyage = tedShipVoyage.Text.Trim();
            if (pacm.InsertMatName(pac) == true)
            {
                MessageBox.Show("添加成功！");
            }
            tedAddName.Text = "";
            tedAddShortKey.Text = "";
            tedAddTransportCo.Text = "";
            tedAddRemarks.Text = "";
            tedAddCustomer.Text = "";
            tedAddDeliveryCo.Text = "";
            tedShipVoyage.Text = "";
            GridviewDataBinding();
        }

        /// <summary>
        /// 修改物料信息
        /// </summary>
        private void ModifyMatShortKey()
        {
            List<MatShortKeyMD> ls = this.gcShortKey.DataSource as List<MatShortKeyMD>;
            if (ls == null) return;
            if (select < 0) return;
            PubMatShortKeyManager pacm = new PubMatShortKeyManager();
            //if (pacm.ChekMatName(tedModifyName.Text.Trim()) == true)
            //{
            //    errorProvider1.SetError(tedModifyName, "物料名已经存在或未做修改！");
            //    tedModifyName.Focus();
            //    return;
            //}
            MatShortKeyMD sk = ls[select];
            sk.Name = tedModifyName.Text.Trim();
            sk.Customer = tedModifyCustomer.Text.Trim();
            sk.DeliveryCo = tedModifyDeliveryCo.Text.Trim();
            sk.Remarks = tedModifyRemarks.Text.Trim();
            sk.TransportCo = tedModifyTransportCo.Text.Trim();
            sk.ShipAndVoyage = tedShipVoyageM.Text.Trim();
            if (pacm.UpdataMatName(sk) == true)
            {
                MessageBox.Show("修改成功！");
            }
            tedModifyID.Text = "";
            tedModifyCustomer.Text = "";
            tedModifyName.Text = "";
            tedModifyShortKey.Text = "";
            tedModifyTransportCo.Text = "";
            tedModifyRemarks.Text = "";
            tedModifyDeliveryCo.Text = "";
            tedShipVoyageM.Text = "";
            GridviewDataBinding();
        }
        /// <summary>
        /// 删除物料信息
        /// </summary>
        private void DeleteMatShortKey()
        {
            List<MatShortKeyMD> ls = this.gcShortKey.DataSource as List<MatShortKeyMD>;
            if (ls == null) return;
            if (select < 0) return;
            PubMatShortKeyManager pacm = new PubMatShortKeyManager();
            MatShortKeyMD sk = ls[select];            
            if (pacm.DeleteMatName(sk) == true)
            {
                MessageBox.Show("删除成功！");
            }
            tedModifyID.Text = "";
            tedModifyCustomer.Text = "";
            tedModifyName.Text = "";
            tedModifyShortKey.Text = "";
            tedModifyTransportCo.Text = "";
            tedModifyRemarks.Text = "";
            tedShipVoyageM.Text = "";
            tedModifyDeliveryCo.Text = "";
            GridviewDataBinding();
        }


        #endregion

       

    }
}
