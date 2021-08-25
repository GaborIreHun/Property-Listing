using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Project
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Creating list for storing records
        /// </summary>
        private List<Record> list1 = new List<Record>();
        private int currentIndex = 0; // index of currently displayed record
        private BinaryFormatter formatter = new BinaryFormatter();
        private BinaryFormatter reader = new BinaryFormatter();
        private String fileName;
        private FileStream output;
        private FileStream input;
        
        
        /// <summary>
        /// Constructor for Form1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        ///  Setting timer to null
        /// </summary>
        System.Windows.Forms.Timer t = null;
        /// <summary>
        /// Method for working clock
        /// </summary>
        private void StartTimer()   // Ready for project and tested
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }


        
        /// <summary>
        /// Method for the clock display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void t_Tick(object sender, EventArgs e)   // Ready for project and tested
        {
            this.Text = "Property Listing   " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }
        
                
        /// <summary>
        /// Form1 loading method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Calling StartTimer method to display clock
            StartTimer();

            // Deactivating toolTip at Form loading
            toolTip1.Active = false;

            // Calling the SelectLoadRecords method to load the appropriate records
            SelectLoadRecords();

            // Sorting Area list in aplhabetical order
            comboBoxArea.Sorted = true;

            // Calling DisplayRecord to display the records
            DisplayRecord(0);

        }


        /// <summary>
        /// Method for loading initial records
        /// </summary>
        private void SelectLoadRecords()
        {
            // Checking if application was run before - if yes, saved values from last state shall be loaded
            if (File.Exists(@"loadingText.ser") == true)
                OpenFile(@"loadingText.ser");
            else
                CreateRandomRecords();
        }

        /// <summary>
        /// Method to create random data for functionality tests
        /// </summary>
        private void CreateRandomRecords()
        {
            // Creating new random object
            Random rnd = new Random();


            // Lists for random street names
            string[] streetPart1 = { "Lake", "Church", "Mountain", "Railway", "Oak", "Willow", "Birch", "Elm", "Hazel", "Ashington", "Navan", "Ashfield", "Bedford", "Fitzwilliam", "Sarsfield", "Barry", "Belclare", "Castlewood", "Belgrove" };
            string[] streetPart2 = { "Road", "Park", "Avenue", "Green", "Cresent", "View", "Lane", "Rise", "Court", "Cove", "Street", "Boulevard", "Place", "Square", "Terrace", "Upper", "Lower", "Grove", "Hill", "Terrace" };
            int numRecords = 200;

            // Declaration of necessary variables
            string k, hn, st, sz, pr, area, district, type, heating, room, ber;
            int a, b, c, d, f, g;


            Record rec;

            try
            {
                //Record list;
                for (int lcv = 0; lcv < numRecords; lcv++)
                {

                    // Detemining the random values for the indexes of the characteristics on the pre-populated list
                    a = rnd.Next(0, comboBoxDistrict.Items.Count);
                    c = rnd.Next(0, comboBoxType.Items.Count);
                    d = rnd.Next(0, comboBoxHeating.Items.Count);
                    f = rnd.Next(0, comboBoxRoom.Items.Count);
                    g = rnd.Next(0, comboBoxBer.Items.Count);

                    st = $"{streetPart1[rnd.Next(streetPart1.Length)]} {streetPart2[rnd.Next(streetPart2.Length)]}";
                    pr = $"{rnd.Next(50, 1000) * 100}";
                    hn = $"{ rnd.Next(100) + 1}";
                    sz = $"{rnd.Next(5, 30) * 5}";

                    district = comboBoxDistrict.Items[a].ToString();
                    k = district;
                    b = 0;

                    // Switch statement to create appropriate area value for the district values (for pre populated data)
                    switch (k)
                    {
                        case "D1":
                            b = rnd.Next(0, 12);
                            break;
                        case "D3":
                            b = rnd.Next(12, 22);
                            break;
                        case "D5":
                            b = rnd.Next(22, 27);
                            break;
                        case "D7":
                            b = rnd.Next(27, 36);
                            break;
                        case "D9":
                            b = rnd.Next(36, 50);
                            break;
                        case "D11":
                            b = rnd.Next(50, 72);
                            break;
                        case "D13":
                            b = rnd.Next(72, 79);
                            break;
                        case "D15":
                            b = rnd.Next(79, 89);
                            break;
                        case "D17":
                            b = rnd.Next(89, 94);
                            break;
                        case "D2":
                            b = rnd.Next(94, 110);
                            break;
                        case "D4":
                            b = rnd.Next(110, 120);
                            break;
                        case "D6":
                            b = rnd.Next(120, 126);
                            break;
                        case "D6w":
                            b = rnd.Next(126, 130);
                            break;
                        case "D8":
                            b = rnd.Next(130, 139);
                            break;
                        case "D10":
                            b = rnd.Next(139, 142);
                            break;
                        case "D12":
                            b = rnd.Next(142, 148);
                            break;
                        case "D14":
                            b = rnd.Next(148, 154);
                            break;
                        case "D16":
                            b = rnd.Next(154, 161);
                            break;
                        case "D18":
                            b = rnd.Next(161, 171);
                            break;
                        case "D20":
                            b = rnd.Next(171, 173);
                            break;
                        case "D22":
                            b = rnd.Next(173, 178);
                            break;
                        case "D24":
                            b = rnd.Next(178, 182);
                            break;
                    }


                    // Detemining the random values for the attributes on the pre-populated list
                    area = comboBoxArea.Items[b].ToString();
                    type = comboBoxType.Items[c].ToString();
                    heating = comboBoxHeating.Items[d].ToString();
                    room = comboBoxRoom.Items[f].ToString();
                    ber = comboBoxBer.Items[g].ToString();


                    // Determining the outlay of records
                    rec = new Record(hn, st, sz, pr, area, district, type, heating, room, ber);

                    // facilities
                    bool[] facilities = { rnd.Next(2) == 0, rnd.Next(2) == 0, rnd.Next(2) == 0, rnd.Next(2) == 0, rnd.Next(2) == 0, rnd.Next(2) == 0, rnd.Next(2) == 0, rnd.Next(2) == 0 };
                    rec.FacilitiesSettings = facilities.ToList();

                    // Adding recors to the list
                    list1.Add(rec);

                }

                // saving state for future use
                SimpleSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Method to clear all fields
        /// </summary>
        private void ClearAll()
        {
            try
            {
                foreach (Control ctrl in Controls)
                {
                    if (ctrl is TextBox)
                    {
                        TextBox textBox = (TextBox)ctrl;
                        textBox.Clear();
                    }
                }

                // Clear checkboxes
                foreach (Control ctrl in groupBox1.Controls)
                {
                    if (ctrl is CheckBox)
                    {
                        CheckBox checkButton = (CheckBox)ctrl;
                        checkButton.Checked = false;
                    }
                }

                // Clear ComboBoxes
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


        /// <summary>
        /// Method to display records
        /// </summary>
        /// <param name="index"></param>
        private void DisplayRecord(int index)
        {
            try
            {
                if (index < 0) // correct "too low" index
                    index = 0;

                if (index >= list1.Count) // correct "too high" index
                    index = list1.Count - 1;

                currentIndex = index;

                // Checking if list is not empty and if not, building each index of record
                if (list1.Count != 0)
                {
                    txtHouseNumber.Text = list1[index].HouseNumber;
                    txtStreet.Text = list1[index].Street;
                    txtSize.Text = list1[index].Size;
                    txtPrice.Text = list1[index].Price;
                    comboBoxArea.Text = list1[index].Area;
                    comboBoxDistrict.Text = list1[index].District;
                    comboBoxType.Text = list1[index].Type;
                    comboBoxHeating.Text = list1[index].Heating;
                    comboBoxRoom.Text = list1[index].Room;
                    comboBoxBer.Text = list1[index].Ber;
                }


                //list to store setting of facilities checkboxes 
                List<bool> checkedBoxes = list1[index].FacilitiesSettings;

                // For loop to set checkboxes 
                for (int lcv = 0; lcv < groupBox1.Controls.Count; lcv++)
                {
                    if (groupBox1.Controls[lcv] is CheckBox)
                    {
                        CheckBox ctrl = (CheckBox)groupBox1.Controls[lcv];

                        ctrl.Checked = checkedBoxes[lcv];
                    }
                }
                txtIndex.Text = (currentIndex + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        /// <summary>
        /// Constructor for recording dataset
        /// </summary>
        /// <returns></returns>
        private Record ReadFormValues()
        {
            // read values from textBoxes
            string hn = txtHouseNumber.Text;
            string st = txtStreet.Text;
            string sz = txtSize.Text;
            string pr = txtPrice.Text;
            string area = comboBoxArea.Text;
            string district = comboBoxDistrict.Text;
            string type = comboBoxType.Text;
            string heating = comboBoxHeating.Text;
            string room = comboBoxRoom.Text;
            string ber = comboBoxBer.Text;


            // Create record object
            Record rec = new Record(hn, st, sz, pr, area, district, type, heating, room, ber);


            // Creating list for the values of checkboxes
            List<bool> checkedBoxes = new List<bool>();
            for (int lcv = 0; lcv < groupBox1.Controls.Count; lcv++)
            {
                if (groupBox1.Controls[lcv] is CheckBox)
                {
                    CheckBox ctrl = (CheckBox)groupBox1.Controls[lcv];
                    checkedBoxes.Add(ctrl.Checked);
                }
            }

            rec.FacilitiesSettings = checkedBoxes;
                
            return rec;            
        }


        //----------------------------------------- Button functionalities ----------------------------------------------------

        /// <summary>
        /// Method for saving file
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveFile(String fileName)
        {
            // Checking if file name is given, if yes save file
            if (string.IsNullOrEmpty(fileName))
                MessageBox.Show("Invalid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                formatter.Serialize(output, list1); // The process of turning the record object
                                                    //(what is stored in the list)from the english like code
                                                    // into binary structure--> save record objects(type info)       
                output.Close();
                this.Text = "GuestBook" + "[" + fileName + "]";
            }
        }

        /// <summary>
        /// Method for saving status of dataset in temporary file
        /// </summary>
        private void SimpleSave()
        {
            fileName = "loadingText.ser";
            output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(output, list1); // The process of turning the record object
                                                //(what is stored in the list)from the english like code
                                                // into binary structure--> save record objects(type info)       
            output.Close();
            this.Text = "GuestBook" + "[" + fileName + "]";
        }


        /// <summary>
        /// Method for opening file
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenFile(string fileName)
        {
            input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            list1.Clear();

            list1 = (List<Record>)reader.Deserialize(input);
            input.Close();
            this.Text = "GuestBook" + "[" + fileName + "]";

            // Calling DisplayRecord method to display opened file
            DisplayRecord(0);
        }


        /// <summary>
        /// Method for saving file as CSV
        /// </summary>
        /// <param name="csvfileName"></param>
        private void SaveCSVFile(string csvfileName)
        {
            // Checking if file name is given, before saving to CSV
            if (string.IsNullOrEmpty(csvfileName))
                MessageBox.Show("Invalid file Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                FileStream csvOutput = new FileStream(csvfileName, FileMode.Create, FileAccess.Write);
                StreamWriter csvfileWriter = new StreamWriter(csvOutput);

                // Creating headers
                csvfileWriter.WriteLine("HouseNumber, Street, Size, Price, Facilities, Area, District, Type, Heating, Room, BER");

                foreach (Record rec in list1)
                {
                    // Creating list for facilities 
                    List<bool> facilities = rec.FacilitiesSettings;
                    string facilitiesString = $"Parking:{facilities[0]} Garden:{facilities[1]} Alarm:{facilities[2]} Broadband:{facilities[3]} Aircondition:{facilities[4]} Solar_Panel:{facilities[5]} Proximity_Area:{facilities[6]} Wheelchair_Access:{facilities[7]}";
                    
                    // Creating rows from records
                    csvfileWriter.WriteLine($"{rec.HouseNumber}, {rec.Street}, {rec.Size}, {rec.Price}, {facilitiesString}, {rec.Area}, {rec.District}, {rec.Type}, {rec.Heating}, {rec.Room}, {rec.Ber}");
                }
                csvfileWriter.Close();
            }
        }


        /// <summary>
        /// Method to create CSV from list
        /// </summary>
        /// <param name="csvfileName"></param>
        private void CreateCSVFile(string csvfileName) 
        {
            FileStream csvOutput = new FileStream(csvfileName, FileMode.Create, FileAccess.Write);
            StreamWriter csvfileWriter = new StreamWriter(csvOutput);

            csvfileWriter.WriteLine("HouseNumber, Street, Size, Price, Area, District, Type, Heating, Room, BER, Parking, Garden, Alarm, Broadband, Aircondition, Solar_Panel, Proximity_Area, Wheelchair_Access");

            foreach (Record rec in list1)
            {
                List<bool> facilities = rec.FacilitiesSettings;
                string facilitiesParking = $"{facilities[0]}";       
                string facilitiesGarden = $"{facilities[1]}";
                string facilitiesAlarm = $"{facilities[2]}";
                string facilitiesBroadband = $"{facilities[3]}";
                string facilitiesAircondition = $"{facilities[4]}";
                string facilitiesSolarPanel = $"{facilities[5]}";
                string facilitiesProximityArea = $"{facilities[6]}";
                string facilitiesWheelchairAccess = $"{facilities[7]}"; 

                csvfileWriter.WriteLine($"{rec.HouseNumber}, {rec.Street}, {rec.Size}, {rec.Price}, {rec.Area}, {rec.District}, {rec.Type}, {rec.Heating}, {rec.Room}, {rec.Ber}, {facilitiesParking}, {facilitiesGarden}, {facilitiesAlarm}, {facilitiesBroadband}, {facilitiesAircondition}, {facilitiesSolarPanel}, {facilitiesProximityArea}, {facilitiesWheelchairAccess}");
            }
            csvfileWriter.Close();
        }


        /// <summary>
        /// Method to create temporary CSV file from list for form2
        /// </summary>
        private void CreateCSV()
        {
            SaveFileDialog fileChooser = new SaveFileDialog();
            fileChooser.InitialDirectory = "c:\\";
            fileChooser.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            fileChooser.FilterIndex = 1;
            fileChooser.RestoreDirectory = true;
            string csvfileName = "temp.csv";
            CreateCSVFile(csvfileName);
        }

        // ---------------------------------------------- Button click events --------------------------------------------------------

        /// <summary>
        /// Method for the open file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.InitialDirectory = "c:\\";
            fileChooser.Filter = "ser files (*.ser)|*.ser|All files (*.*)|*.*";
            fileChooser.FilterIndex = 1;
            fileChooser.RestoreDirectory = true;
            DialogResult result = fileChooser.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileName = fileChooser.FileName;
                OpenFile(fileName);
            }
            SimpleSave();
        }


        /// <summary>
        /// Function for the close file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseFile_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            DialogResult result = fileChooser.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileName = fileChooser.FileName;
                OpenFile(fileName);
            }
            SimpleSave();
        }


        /// <summary>
        /// Method for the save as button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAs_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog fileChooser = new SaveFileDialog();

            fileChooser.Filter = "ser files (*.ser)|*.ser|All files (*.*)|*.*";
            fileChooser.FilterIndex = 1; // if 2 then all file shall be default.
            fileChooser.RestoreDirectory = true;

            DialogResult result = fileChooser.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileName = fileChooser.FileName;
                SaveFile(fileName);
            }
            SimpleSave();
        }


        /// <summary>
        /// Method for the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SaveFile(fileName);
            SimpleSave();
        }


        /// <summary>
        /// Method for the save as CSV button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAsCsv_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog fileChooser = new SaveFileDialog();
            fileChooser.InitialDirectory = "c:\\";
            fileChooser.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            fileChooser.FilterIndex = 1;
            fileChooser.RestoreDirectory = true;
            DialogResult result = fileChooser.ShowDialog();

            if (result == DialogResult.OK)
            {
                string csvfileName = fileChooser.FileName;
                SaveCSVFile(csvfileName);
            }
            SimpleSave();
        }


        /// <summary>
        /// Method for first button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (list1.Count == 0)
                    MessageBox.Show("The list is empty!", "Warning");
                else if (currentIndex == 0)
                    MessageBox.Show("The current record is the first one!", "Information");
                else
                    DisplayRecord(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Method for previous button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (list1.Count == 0)
                    MessageBox.Show("The list is empty!", "Warning");
                else if (currentIndex == 0)
                    MessageBox.Show("There are no items before the current one", "Information");
                else
                    DisplayRecord(currentIndex - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Method for next button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (list1.Count == 0)
                    MessageBox.Show("The list is empty!", "Warning");
                else if (currentIndex == list1.Count - 1)
                    MessageBox.Show("There are no more item beyond the current one", "Information");
                else
                    DisplayRecord(currentIndex + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Method for last button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (list1.Count == 0)
                    MessageBox.Show("The list is empty!", "Warning");
                else if (currentIndex == list1.Count - 1)
                    MessageBox.Show("The current record is the last one!", "Information");
                else
                    DisplayRecord(list1.Count - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Method for add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Ensuring all field has value for the new record
                if (txtHouseNumber.Text == "" || txtPrice.Text == "" || txtSize.Text == "" || txtStreet.Text == ""
                    || comboBoxArea.SelectedIndex == -1 || comboBoxRoom.SelectedIndex == -1 || comboBoxBer.SelectedIndex == -1
                    || comboBoxDistrict.SelectedIndex == -1 || comboBoxHeating.SelectedIndex == -1 || comboBoxType.SelectedIndex == -1)
                    MessageBox.Show("All fields are mandatory!", "Warning");
                // Preventing duplicated input
                else if (list1.Any(l => l.Area == comboBoxArea.SelectedItem.ToString() && l.Ber == comboBoxBer.SelectedItem.ToString()
                && l.District == comboBoxDistrict.SelectedItem.ToString() && l.Heating == comboBoxHeating.SelectedItem.ToString()
                && l.HouseNumber == txtHouseNumber.Text && l.Room == comboBoxRoom.SelectedItem.ToString()
                && l.Street == txtStreet.Text && l.Size == txtSize.Text && l.Type == comboBoxType.SelectedItem.ToString()))
                    MessageBox.Show("The record already exists!", "Warning");
                // Ensuring no null value inputted
                else if (txtHouseNumber.Text == "0" || txtPrice.Text == "0" || txtSize.Text == "0")
                    MessageBox.Show("Input value should not be 0!", "Warning");
                // Ensuring no string input instead of numerical ones
                else if (Regex.IsMatch(txtHouseNumber.Text, @"^[a-zA-Z]+$") || Regex.IsMatch(txtPrice.Text, @"^[a-zA-Z]+$") || Regex.IsMatch(txtSize.Text, @"^[a-zA-Z]+$"))
                    MessageBox.Show("Only numbers allowed for numerical inputs!", "Warning");
                // Ensuring no invalid input for size
                else if (txtSize.Text.Length <= 1)
                    MessageBox.Show("The size of the property is too small!", "Warning");
                // Ensuring no single character input for street name
                else if (txtStreet.Text.Length <= 1)
                    MessageBox.Show("Insufficient number of caracters for the name of the street!", "Warning");
                // Ensuring to low price input to be confirmed
                else if (txtPrice.Text.Length < 5)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to proceed?", "The price you have entered is very low!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        list1.Add(ReadFormValues());
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        // Empty as no actions needed
                    }
                }
                else
                    list1.Add(ReadFormValues());
                SimpleSave();
                DisplayRecord(list1.Count-1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Method for update button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            list1[currentIndex] = ReadFormValues();
            SimpleSave();
        }


        /// <summary>
        /// Method for delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            list1.RemoveAt(currentIndex);
            currentIndex++;
            DisplayRecord(currentIndex);
            SimpleSave();
        }

 
        /// <summary>
        /// Method for clear button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearAll_Click_1(object sender, EventArgs e)
        {
            // Calling ClearAll method to clear all fields
            ClearAll();
            SimpleSave();
        }


        /// <summary>
        /// Method when search button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Creating temporary CSV for the source of dataGridView1 on Form2
            CreateCSV();
            
            // Opening Form2
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }


        /// <summary>
        /// Method for cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (list1.Count == 0)
                ClearAll();
            else
                DisplayRecord(currentIndex);
        }


        /// <summary>
        /// Method for exit button to close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }


        /// <summary>
        /// Creating method for button that clears all records(list1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearRecord_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "You will delete all records!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                list1.Clear();
                ClearAll();
            }
            else if (dialogResult == DialogResult.No)
            {
                // Empty as no actions needed
            }
            SimpleSave();
        }


        // ---------------------------------------- Methods for toolTip1 -----------------------------------------------------------

     
        /// <summary>
        /// Method to Show guiding text for House Number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHouseNumber_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Enter the House number", txtHouseNumber);
        }


        /// <summary>
        /// Method to Show guiding text for Street name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStreet_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Enter the name of the Street", txtHouseNumber);
        }


        /// <summary>
        /// Method to Show guiding text for Area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxArea_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Area", comboBoxArea);
        }


        /// <summary>
        /// Method to Show guiding text for District
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDistrict_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's District", comboBoxDistrict);
        }


        /// <summary>
        /// Method to Show guiding text for House Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Type", comboBoxType);
        }


        /// <summary>
        /// Method to Show guiding text for Heating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxHeating_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Heating", comboBoxHeating);
        }


        /// <summary>
        /// Method to Show guiding text for Room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxRoom_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the number of Rooms", comboBoxRoom);
        }


        /// <summary>
        /// Method to Show guiding text for Ber
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxBer_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Choose the property's Ber rating", comboBoxBer);
        }


        /// <summary>
        /// Method to Show guiding text for Facilities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Select the property's Facilities", groupBox1);
        }


        /// <summary>
        /// Method to Show guiding text for Size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSize_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Enter the Size of the property", txtSize);
        }


        /// <summary>
        /// Method to Show guiding text for Price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrice_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Enter the Price of the property", txtPrice);
        }


        /// <summary>
        /// Method to Show guiding text for Add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Add new record", btnAdd);
        }


        /// <summary>
        /// Method to Show guiding text for Update button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Update edited record", btnUpdate);
        }


        /// <summary>
        /// Method to Show guiding text for Delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Delete current record", btnDelete);
        }


        /// <summary>
        /// Show guiding text for Clear Records button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearRecord_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Clear all records", btnClearRecords);
        }

 
        /// <summary>
        /// Method to Show guiding text for Search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Search Records", btnSearch);
        }


        /// <summary>
        /// Method to Show guiding text for Clear button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Clear current record", btnClear);
        }


        /// <summary>
        /// Method to Show guiding text for Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Cancel modifications on current record", btnCancel);
        }


        /// <summary>
        /// Method to Show guiding text for Exit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Exit application", btnExit);
        }


        /// <summary>
        /// Method to Show guiding text for First button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("First record", btnFirst);
        }


        /// <summary>
        /// Method to Show guiding text for Previous button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Previous record", btnPrevious);
        }

 
        /// <summary>
        /// Method to Show guiding text for Index button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIndex_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Current index", txtIndex);
        }


        /// <summary>
        /// Method to Show guiding text for Next button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Next record", btnNext);
        }


        /// <summary>
        /// Method to Show guiding text for Last button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Last record", btnLast);
        }


        /// <summary>
        /// Radio button to activate toolTip1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            toolTip1.Active = true;
        }


        /// <summary>
        /// Method to Radio button to deactivate toolTip1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            toolTip1.Active = false;
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
