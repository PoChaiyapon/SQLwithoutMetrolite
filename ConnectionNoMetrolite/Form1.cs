using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionNoMetrolite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                string ftypeName = txtInput.Text.Trim();
                //string sql = string.Format("select*From UXFMUNI..FABRIC_TYPE where FABRIC_TYPE_NAME like '{0}%'", ftypeName);
                string sql = "EXEC UXFMUNI..Z_POTEST @TYPE = 'GET_FABRIC_TYPE'";
                sql = sql + string.Format(",@FTYPENAME = '{0}'", ftypeName);
                DataSet ds = DB.execSQL(sql);
                dataGridView1.DataSource = ds.Tables[0];

                txtInput.Text = string.Empty;
                txtInput.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Find_Click(null, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = txtID.Text.Trim();
                string tName = txtName.Text.Trim();
                string qty = txtPrice.Text.Trim();
                if(string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(tName))
                {
                    MessageBox.Show("Please define Id and Name!","Error!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    VarModel data = new VarModel()
                    {
                        ID = Convert.ToInt32(Id),
                        TITLE = tName,
                        PRICE = Convert.ToInt32(qty)
                    };
                    Insert_Data(data);
                }
                txtID.Text = string.Empty;
                txtName.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtID.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Insert_Data(VarModel data)
        {
            try
            {
                string sql = "EXEC UXFMUNI..Z_POTEST @TYPE = 'INSERT_TITLE'";
                sql = sql + string.Format(",@ID = '{0}'", data.ID);
                sql = sql + string.Format(",@FTYPENAME = '{0}'", data.TITLE);
                sql = sql + string.Format(",@QTY = '{0}'", data.PRICE);
                DB.execSQL(sql);
                MessageBox.Show("Insert completed!","Successful!");
                tabControl1.SelectedTab = tabPage1;
                btn_Find_Click(null,null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = dataGridView1.SelectedCells[0].Value.ToString();
            MessageBox.Show(str);
        }
    }
}
