using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    //s[Serializable]
    public partial class Form2 : Form
    {
        // Creating datatable for the source of datagridview
        public DataTable dt2 = new DataTable();


        public Form2()
        {
            InitializeComponent();
        }


        // Initiating tasks when loading form2
        private void Form2_Load(object sender, EventArgs e)
        {
            // Calling method to convert CSV created on form1 to datatable for datagridview
            BindData(@"temp.csv");
            // Deleting temporary CSV file that was created when form 2 was called
            System.IO.File.Delete(@"temp.csv");

            // Setting default values for comboboxes that are holding int values
            comboBoxMin1.SelectedIndex = 0;
            comboBoxMax1.SelectedIndex = 0;
            comboBoxMin.SelectedIndex = 0;
            comboBoxMax.SelectedIndex = 0;
        }


        // Method to convert CSV to datatable
        private void BindData(string filePath)
        {
            // Create local datatable to store info
            DataTable dt = new DataTable();
            // Reading given file on given path
            string[] lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                // First line to create header
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                // For Data
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            // If there are any rows in the created datatable use it as the source of datagridview
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }

            dt2 = dt;
        }


        // Creating function for back button
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Hiding current form and opening form1
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }


        // Creating function for clicking on map button
        private void btnMap_Click(object sender, EventArgs e)
        {
            // Resizing picturebox to fill form2
            PictureBox ScreenPbx = pictureBox1;
            ScreenPbx.Dock = DockStyle.Fill;
            ScreenPbx.SizeMode = PictureBoxSizeMode.StretchImage;
            
            // Showing map
            pictureBox1.Visible = true;
        }

        // Creating function for clicking on picturebox
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Hiding picturebox
            pictureBox1.Visible = false;
        }


        // Search button functionalities
        private void btnSearching_Click(object sender, EventArgs e)
        {
            // Creating variables for each search area values
            string searchValueArea = comboBoxArea1.Text;
            string searchValueType = comboBoxType1.Text;
            string searchValueRoom = comboBoxRoom1.Text;
            string searchValueDistrict = comboBoxDistrict1.Text;
            string searchValueHeating = comboBoxHeating1.Text;
            string searchValueBer = comboBoxBer1.Text;
            int searchPriceMin = int.Parse(comboBoxMin1.Text);
            int searchPriceMax = int.Parse(comboBoxMax1.Text);                
            int searchSizeMin = int.Parse(comboBoxMin.Text);
            int searchSizeMax = int.Parse(comboBoxMax.Text);
            string searchParking = comboBoxParking.Text;
            string searchGarden = comboBoxGarden.Text;
            string searchAlarm = comboBoxAlarm.Text;
            string searchBroadband = comboBoxBroadand.Text;
            string searchAircondition = comboBoxAircondition.Text;
            string searchSolarPanel = comboBoxSolarPanel.Text;
            string searchProximityArea = comboBoxProximity.Text;
            string searchWheelchairAccess = comboBoxWheelchair.Text;


            // Creating try and catch statements to handle possible errors while creating search results
            try
            {
                // Creating variable to store appropriate values in dt2 datatable as intermediate list
                var re = from row in dt2.AsEnumerable()
                         where row[4].ToString().Contains(searchValueArea)
                         where row[6].ToString().Contains(searchValueType)
                         where row[8].ToString().Contains(searchValueRoom)
                         where row[5].ToString().Contains(searchValueDistrict)
                         where row[7].ToString().Contains(searchValueHeating)
                         where row[9].ToString().Contains(searchValueBer)
                         where (searchPriceMin <= int.Parse(row[3].ToString()) && searchPriceMax >= int.Parse(row[3].ToString()))
                         where (searchSizeMin <= int.Parse(row[2].ToString()) && searchSizeMax >= int.Parse(row[2].ToString()))
                         where row[10].ToString().Contains(searchParking)
                         where row[11].ToString().Contains(searchGarden)
                         where row[12].ToString().Contains(searchAlarm)
                         where row[13].ToString().Contains(searchBroadband)
                         where row[14].ToString().Contains(searchAircondition)
                         where row[15].ToString().Contains(searchSolarPanel)
                         where row[16].ToString().Contains(searchProximityArea)
                         where row[17].ToString().Contains(searchWheelchairAccess)
                         select row;

                // Creating if statements when a set minimum search value is larger than the maximum value
                if (searchPriceMin > searchPriceMax)
                {
                    MessageBox.Show("The minimum search price should be lower than the maximum search price!", "Cauction");
                }
                else if (searchSizeMin > searchSizeMax)
                {
                    MessageBox.Show("The minimum search size should be lower than the maximum search size!", "Cauction");
                }

                // Creating if else statement to build datatable with new relevant record 
                if (re.Count() == 0)
                {
                    MessageBox.Show("There is no match!", "Info");
                }
                else
                {
                    dataGridView1.DataSource = re.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        // List refresh button function
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Setting dt2 datatable to the source of the datagridview
            dataGridView1.DataSource = dt2;
        }


        // Clear criteria button functions
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            // Setting all facilities comboboxes to null selection
            foreach (Control ctrl in groupBox11.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)ctrl;
                    comboBox.SelectedIndex = -1;
                }
            }

            // Setting all size comboboxes to default value which is the first in the collection
            foreach (Control item in groupBox2.Controls)
            {
                if (item is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)item;
                    comboBox.SelectedIndex = default;
                }
            }

            // Setting all price comboboxes to default value which is the first in the collection
            foreach (Control item in groupBox3.Controls)
            {
                if (item is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)item;
                    comboBox.SelectedIndex = default;
                }
            }

            // Setting all other comboboxes to null selection
            foreach (Control item in Controls)
            {
                if (item is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)item;
                    comboBox.SelectedIndex = -1;
                }
            }
        }
    }
    // author: https://github.com/GaborIreHun
}
