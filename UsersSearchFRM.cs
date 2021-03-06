﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reception
{
    public partial class UsersSearchFRM : Form
    {
        public UsersSearchFRM()
        {
            InitializeComponent();
        }

        SqlDataAdapter da;
        SqlConnection con = new SqlConnection("Data Source=PEDRAM-PC;Initial Catalog=ProjectREC;Integrated Security=True");

        SqlCommandBuilder cb;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        private void UsersSearchFRM_Load(object sender, EventArgs e)
        {
           
            da = new SqlDataAdapter("SELECT * FROM Users", con);
            cb = new SqlCommandBuilder(da);
            da.Fill(ds, "Users");
            dataGridView1.DataBindings.Add(new Binding("DataSource", ds, "Users"));
            dataGridView1.ReadOnly = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try 
            {
                
                da = new SqlDataAdapter("SELECT * FROM Users WHERE UserID='" + txtId.Text + "' or Name='" + txtname.Text + "'or UserName='"+txtUName.Text+"'or ShiftID='"+ComboShiftId.Text+"'", con);
                cb = new SqlCommandBuilder(da);
                da.Fill(dt);
                
                if (dt.Rows.Count == 0)
                {
                    
                    MessageBox.Show("Nothing Found!!!");
                    //dataGridView1.Rows.Clear();
                }
                dataGridView1.DataSource = dt;
                

            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            try
             {
                da = new SqlDataAdapter("SELECT * FROM Users", con);
                cb = new SqlCommandBuilder(da);
                da.Fill(ds);
                string Filter;

                int Rows;

                string trimtext;

                trimtext = txtname.Text.Trim();

                if ((trimtext.Equals("")))

                    Filter = "";

                else

                    Filter = "Name Like '%" + txtname.Text.ToString() + "%'";
                ds.Tables["Users"].DefaultView.RowFilter = Filter;
                Rows = ds.Tables["Users"].DefaultView.Count;
                dataGridView1.DataSource = ds.Tables["Users"].DefaultView;
            }
            catch (Exception) { }
        }
    }
}
