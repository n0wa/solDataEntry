using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEntry
{
    public partial class frmMain : Form
    {
        //public string server = System.Configuration.ConfigurationManager.AppSettings["Server"];
        //public string database =System.Configuration.ConfigurationManager.AppSettings["Database"];

        SqlConnection con = new SqlConnection("Data Source=Lenovo;Initial Catalog=Sample;Integrated Security=true;");
        //SqlConnection con = new SqlConnection("Data Source=ARCUDB;Initial Catalog=SavvyDBSolarity;Integrated Security=true;");

        SqlCommand cmd;
        SqlDataAdapter adapt;
        DataSet ds;

        //public SqlConnection con = new SqlConnection("Server=Lenovo;Database=DataEntry;Trusted_Connection=true");
        
        public frmMain()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            //Display();
        }

 

        private void Display()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter("SELECT [AccountNumber],[LoanID],[EffectiveDate],[FundedAmount],[LoanTypeDescription],[MarkForDelete] "
                                           + "FROM [dbo].[loanFundedLIP] "
                                           + "where [EffectiveDate]= '" + dateTimePicker1.Value.ToString() + "'"
                                           + "And [MarkForDelete] = '" + 0 + "'"
                                           , con); // 
                ds = new System.Data.DataSet();
                adapt.Fill(ds, "Loans");
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder cmdbl = new SqlCommandBuilder(adapt);
                adapt.Update(ds, "Loans");
                MessageBox.Show( "Update Completed", "Update",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        

    }
}
