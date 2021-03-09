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
using System.Net.Mail;
using System.Net;


namespace Library_Management_System
{
    public partial class Home : Form
    {
        private int childFormNumber = 0;

        public Home()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void Books_Click(object sender, EventArgs e)
        {

        }

        private void Students_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox1.Items.Add("CSE");
            ComboBox1.Items.Add("Agriculture");
            ComboBox1.Items.Add("BBA");
            ComboBox1.Items.Add("Fisheries");
            ComboBox1.Items.Add("Land Management");
            ComboBox1.Items.Add("Disaster Management");
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                ImageTextBox.Text = ofd.FileName;


                StudentPictureBox.Image = Image.FromFile(@ofd.FileName);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {


                SqlConnection dbCon = new SqlConnection("Data Source=DESKTOP-FQBMN3R\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=true");

                SqlCommand insertCommand = new SqlCommand("INSERT INTO [dbo].[tbl_student_info] (name,roll,reg,faculty,img) VALUES (@StudentName,@StudentRoll,@StudentReg,@StudentFaculty,@StudentImage)", dbCon);

                insertCommand.Parameters.AddWithValue("@StudentName", StudentNametextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentRoll", StudentRolltextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentReg", RegtextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentFaculty", FacultyComboBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentImage", bImageData);


                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();
                insertCommand.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");



            }
            catch (Exception ex)
            { }





        }

        private void button16_Click(object sender, EventArgs e)
        {

            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection("Data Source = DESKTOP-FQBMN3R\\SQLEXPRESS ;Initial Catalog = StudentDB; Integrated Security=true");

            connection.Open();

            SqlCommand selectCommand = new SqlCommand("select * from tbl_student_info where name like '" + ViewTextBox.Text + "%'", connection);


            //selectCommand.Parameters.Add("@NAME", ViewTextBox.Text);
            SqlDataReader dataFromDb = selectCommand.ExecuteReader();


            while (dataFromDb.Read())
            {

                try
                {
                    var index = ViewDataGridView.Rows.Add();


                    ViewDataGridView.Rows[index].Cells[0].Value = dataFromDb["name"].ToString();
                    ViewDataGridView.Rows[index].Cells[1].Value = dataFromDb["roll"].ToString();
                    ViewDataGridView.Rows[index].Cells[2].Value = dataFromDb["reg"].ToString();
                    ViewDataGridView.Rows[index].Cells[3].Value = dataFromDb["faculty"].ToString();




                    byte[] storedImage = (byte[])dataFromDb["img"];

                    Image newImage;
                    MemoryStream stream = new MemoryStream(storedImage);
                    newImage = Image.FromStream(stream);



                    ViewDataGridView.Rows[index].Cells[4].Value = newImage;


                    ((DataGridViewImageColumn)ViewDataGridView.Columns[4]).ImageLayout = DataGridViewImageCellLayout.Stretch;

                    ViewDataGridView.Rows[index].Height = 100;

                }
                catch (Exception esadsad)
                { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                

                SqlConnection dbCon = new SqlConnection("Data Source=DESKTOP-FQBMN3R\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=true");

                SqlCommand updateCommandWithoutImage = new SqlCommand("Update tbl_student_info SET name=@StudentName,roll=@StudentRoll,reg=@StudentReg,faculty=@StudentFaculty where reg='" + updateSerachBox.Text + "'", dbCon);

                    updateCommandWithoutImage.Parameters.AddWithValue("@StudentName", updateNameBox.Text);
                    updateCommandWithoutImage.Parameters.AddWithValue("@StudentRoll", updateRollBox.Text);
                    updateCommandWithoutImage.Parameters.AddWithValue("@StudentReg", updateRegBox.Text);
                    updateCommandWithoutImage.Parameters.AddWithValue("@StudentFaculty", updateComboBox.Text);
                    if (dbCon.State == ConnectionState.Closed)
                        dbCon.Open();
                    updateCommandWithoutImage.ExecuteNonQuery();
                    MessageBox.Show("Record Updated  Successfully");


                }





            }
            catch (Exception ex)

        private void button2_Click(object sender, EventArgs e)
        {
            string deletee = DeleteTextBox.Text;



            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-FQBMN3R\\SQLEXPRESS;Initial Catalog=StudentDB; Integrated Security=true");
            connection.Open();

            SqlCommand selectCommand = new SqlCommand(" delete from tbl_student_info where reg = '" + deletee + "'", connection);


            selectCommand.ExecuteNonQuery();
            MessageBox.Show("Data deleted successfully!!");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            try
            {
                byte[] bImageData = GetImageData();


                SqlConnection dbCon = new SqlConnection("Data Source=DESKTOP-FQBMN3R\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=true");

                SqlCommand insertCommand = new SqlCommand("INSERT INTO [dbo].[tbl_student_info] (name,roll,reg,faculty,img) VALUES (@StudentName,@StudentRoll,@StudentReg,@StudentFaculty,@StudentImage)", dbCon);

                insertCommand.Parameters.AddWithValue("@StudentName", StudentNametextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentRoll", StudentRolltextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentReg", RegtextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentFaculty", FacultyComboBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentImage", bImageData);


                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();
                insertCommand.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");



            }
            catch (Exception ex)
            { }




        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            

            SqlConnection connection = new SqlConnection("Data Source = DESKTOP-FQBMN3R\\SQLEXPRESS ;Initial Catalog = StudentDB; Integrated Security=true");

            //ViewDataGridView.Update();

            ViewDataGridView.Rows.Clear();
            connection.Open();

            SqlCommand selectCommand = new SqlCommand("select * from tbl_student_info", connection);


            SqlDataReader dataFromDb = selectCommand.ExecuteReader();


            while (dataFromDb.Read())
            {

                try
                {
                    var index = ViewDataGridView.Rows.Add();


                    ViewDataGridView.Rows[index].Cells[0].Value = dataFromDb["name"].ToString();
                    ViewDataGridView.Rows[index].Cells[1].Value = dataFromDb["roll"].ToString();
                    ViewDataGridView.Rows[index].Cells[2].Value = dataFromDb["reg"].ToString();
                    ViewDataGridView.Rows[index].Cells[3].Value = dataFromDb["faculty"].ToString();




                    byte[] storedImage = (byte[])dataFromDb["img"];

                    Image newImage;
                    MemoryStream stream = new MemoryStream(storedImage);
                    newImage = Image.FromStream(stream);



                    ViewDataGridView.Rows[index].Cells[4].Value = newImage;


                    ((DataGridViewImageColumn)ViewDataGridView.Columns[4]).ImageLayout = DataGridViewImageCellLayout.Stretch;

                    ViewDataGridView.Rows[index].Height = 100;

                }
                catch (Exception esadsad)
                { }
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            

            try
            {
                byte[] bImageData = GetImageData();


                SqlConnection dbCon = new SqlConnection("Data Source=DESKTOP-FQBMN3R\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=true");

                SqlCommand insertCommand = new SqlCommand("INSERT INTO [dbo].[tbl_student_info] (name,roll,reg,faculty,img) VALUES (@StudentName,@StudentRoll,@StudentReg,@StudentFaculty,@StudentImage)", dbCon);

                insertCommand.Parameters.AddWithValue("@StudentName", StudentNametextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentRoll", StudentRolltextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentReg", RegtextBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentFaculty", FacultyComboBox.Text);
                insertCommand.Parameters.AddWithValue("@StudentImage", bImageData);


                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();
                insertCommand.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");



            }
            catch (Exception ex)
            { }



        }

        private void button11_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
            { 
               textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void Id_Click(object sender, EventArgs e)
        {
        
        }

        }
    }
}
