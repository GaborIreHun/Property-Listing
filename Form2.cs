using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class Form2 : Form
    {
        // Creating datatable for the source of datagridview
        public DataTable dt2 = new DataTable();

        /// <summary>
        /// Constructor for Form2
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Method for task when loading Form2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            // Deactivating toolTip at Form loading
            toolTip1.Active = false;
        }


        /// <summary>
        /// Method to convert CSV to datatable
        /// </summary>
        /// <param name="filePath"></param>
        private void BindData(string filePath)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        /// <summary>
        /// Method for back button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Hiding current form and opening form1
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }


        /// <summary>
        /// Method for map button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            try
            {
                // Resizing picturebox to fill form2
                PictureBox ScreenPbx = pictureBox1;
                ScreenPbx.Dock = DockStyle.Fill;
                ScreenPbx.SizeMode = PictureBoxSizeMode.StretchImage;

                // Showing map
                pictureBox1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

 
        /// <summary>
        /// Method for picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Hiding picturebox
            pictureBox1.Visible = false;
        }


        /// <summary>
        /// Method for search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            string searchBroadband = comboBoxBroadband.Text;
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


        /// <summary>
        /// Method for refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Setting dt2 datatable to the source of the datagridview
            dataGridView1.DataSource = dt2;
        }


        /// <summary>
        /// Method for clear all button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ------------------------------------------- Methods for toolTip1 --------------------------------------------------------

     
        /// <summary>
        /// Radio button method to activate toolTip1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonOn_CheckedChanged(object sender, EventArgs e)
        {
            toolTip1.Active = true;
        }

 
        /// <summary>
        /// Radio button method to deactivate toolTip1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonOff_CheckedChanged(object sender, EventArgs e)
        {
            toolTip1.Active = false;
        }

       
        /// <summary>
        /// Method to Show guiding text for Area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxArea1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Area", comboBoxArea1);
        }

        
        /// <summary>
        /// Method to Show guiding text for District
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDistrict1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's District", comboBoxDistrict1);
        }

        
        /// <summary>
        /// Method to Show guiding text for Type 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Type", comboBoxType1);
        }

        
        /// <summary>
        /// Method to Show guiding text for Heating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxHeating1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Heating", comboBoxHeating1);
        }

       
        /// <summary>
        /// Method to Show guiding text for Room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxRoom1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the number of Rooms", comboBoxRoom1);
        }

        
        /// <summary>
        /// Method to Show guiding text for BER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxBer1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Ber rating", comboBoxBer1);
        }

        
        /// <summary>
        /// Method to Show guiding text for Minimum Size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMin_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the Minimum Size", comboBoxMin);
        }

        
        /// <summary>
        /// Method to Show guiding text for Maximum Size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMax_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the Maximum Size", comboBoxMax);
        }

        
        /// <summary>
        /// Method to Show guiding text for Minimum Price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMin1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the Minimum Price", comboBoxMin1);
        }

        
        /// <summary>
        /// Method to Show guiding text for Maximum Price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMax1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the Maximum Price", comboBoxMax1);
        }

        
        /// <summary>
        /// Method to Show guiding text for Map button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Show map", btnMap);
        }

         
        /// <summary>
        /// Method to Show guiding text for Parking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxParking_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Parking preference", comboBoxParking);
        }

         
        /// <summary>
        /// Method to Show guiding text for Alarm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxAlarm_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Alarm preference", comboBoxAlarm);
        }

        
        /// <summary>
        /// Method to Show guiding text for Aircondition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxAircondition_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Aircondition preference", comboBoxAircondition);
        }

        
        /// <summary>
        /// Method to Show guiding text for Proximity Area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxProximity_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Proximity Area preference", comboBoxProximity);
        }

        
        /// <summary>
        /// Method to Show guiding text for Clear Criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearAll_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Clear all fields", btnClearAll);
        }

        
        /// <summary>
        /// Method to Show guiding text for Back button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Go back to the first page", btnBack1);
        }

        
        /// <summary>
        /// Method to Show guiding text for Garden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxGarden_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Garden preference", comboBoxGarden);
        }

        
        /// <summary>
        /// Method to Show guiding text for Broadband
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxBroadband_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Broadband preference", comboBoxBroadband);
        }

         
        /// <summary>
        /// Method to Show guiding text for Solar Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSolarPanel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Solar Panel preference", comboBoxSolarPanel);
        }

        
        /// <summary>
        /// Method to Show guiding text for Wheelchair Access
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWheelchair_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose Wheelchair Access preference", comboBoxWheelchair);
        }

        
        /// <summary>
        /// Method to Show guiding text for Refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Refresh List", btnRefresh);
        }

        
        /// <summary>
        /// Method to Show guiding text for Search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearching_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Search Collection", btnSearching);
        }


        /// <summary>
        /// Method for help button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Property Listing Documentation.html");
        }
    }
    // author: https://github.com/GaborIreHun
}
