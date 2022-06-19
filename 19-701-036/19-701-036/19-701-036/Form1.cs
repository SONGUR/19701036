using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Omer\Desktop\WindowsFormsApp1\WindowsFormsApp1\Database1.mdf;Integrated Security=True");
            cn.Open();
            GetAllCar();
        }

        private void GetAllCar()
        {
            cmd = new SqlCommand("Select * from CarOperation", cn);
            
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                SaveInfo();
            }
            else
            {
                MessageBox.Show("Id boş olamaz!");
                txtID.Focus();
            }

            GetAllCar();
        }

        
        protected void SaveInfo()
        {
            string QUERY = "INSERT INTO CarOperation " +
                "(Carid, CarName, CarModel, CarPrice)" +
                "VALUES (@Carid, @CarName, @CarModel, @CarPrice)";

            SqlCommand cmd = new SqlCommand(QUERY, cn);
            cmd.Parameters.AddWithValue("@Carid", txtID.Text);
            cmd.Parameters.AddWithValue("@CarName", txtCarName.Text);
            cmd.Parameters.AddWithValue("@CarModel", txtCarModel.Text);
            cmd.Parameters.AddWithValue("@CarPrice", txtCarPrice.Text);
            cmd.ExecuteNonQuery();
        }

        protected void UpdateInfo()
        {
            string QUERY = "Update CarOperation" +
                "Set CarName = @CarName, CarModel = @CarModel, CarPrice = @CarPrice" +
                "where Carid = @Carid";

            SqlCommand cmd = new SqlCommand(QUERY, cn);
            cmd.Parameters.AddWithValue("@Carid", txtID.Text);
            cmd.Parameters.AddWithValue("@CarName", txtCarName.Text);
            cmd.Parameters.AddWithValue("@CarModel", txtCarModel.Text);
            cmd.Parameters.AddWithValue("@CarPrice", txtCarPrice.Text);
            cmd.ExecuteNonQuery();
        }

        protected void DeleteInfo()
        {
            string QUERY = "Delete from CarOperation " +
                "where Carid = @Carid";
            SqlCommand cmd = new SqlCommand(QUERY, cn);
            cmd.Parameters.AddWithValue("@Carid", txtID.Text);
            cmd.ExecuteNonQuery ();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from CarOperation where Carid=" + txtID.Text, cn);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource= dt;
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateInfo();
            GetAllCar();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
            GetAllCar();
        }
    }
}
