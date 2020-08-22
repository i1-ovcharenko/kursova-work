using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace HAS
{
    public partial class Form1 : Form
    {
        Heater heater = new Heater();

        List<Heater> heaters = new List<Heater>();

        string fileName = "";//шлях до файлу для читання/запису
        
        //делегат, для запуску подій в різних місцях коду
        public delegate void forAction(object sender, EventArgs e);

        public Form1()
        {
            InitializeComponent();
        }

        public static void autoResizeColumns(ListView lv)
        {//метод підстраює розмір колонок в таблицях
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = lv.Columns;
            for (int i = 0; i < cc.Count; i++)
            {
                int colWidth = TextRenderer.MeasureText(cc[i].Text, lv.Font).Width + 10;
                if (colWidth > cc[i].Width)
                {
                    cc[i].Width = colWidth;
                }
            }
        }

        private void ShowDataInList()
        {
            listView.Items.Clear();
            foreach (Heater h in heaters)
            {
                string[] split = h.OutputInfo().Split(new Char[] { '*' });
                ListViewItem item = new ListViewItem(split);
                listView.Items.Add(item);
            }
            autoResizeColumns(listView);
        }

        private void OnlyNum(KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
                return;
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {

            var xmlFormatter = new XmlSerializer(typeof(List<Heater>));
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML document (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FileName = "heaters.xml";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            try
            {
                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate))
                    {
                        var newHeater = xmlFormatter.Deserialize(fs) as List<Heater>;
                        heaters = newHeater;
                    }
                    fileName = openFileDialog1.FileName;
                    ShowDataInList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message), "Сталася помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveFileMenuItem_Click(object sender, EventArgs e)
        {
            //збереження даних в існуючий файл
            if (fileName != "")
            {//якщо шлях до файлу відомий, записуємо в нього
                XmlSerializer formatter = new XmlSerializer(typeof(List<Heater>));
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, heaters);
                }
            }
            else
            {//якщо ні, викликаємо наступний метод "Зберегти як"
                forAction action = SaveAsMenuItem_Click;
                action(sender, e);
            }
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFD = new SaveFileDialog();
            SaveFD.InitialDirectory = "d:\\";
            SaveFD.Filter = "XML document (*.xml)|*.xml|All files (*.*)|*.*";
            SaveFD.FileName = "heaters.xml";
            SaveFD.FilterIndex = 1;
            SaveFD.RestoreDirectory = true;
            if (SaveFD.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Heater>));
                using (FileStream fs = new FileStream(SaveFD.FileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, heaters);
                }
                fileName = SaveFD.FileName;
            }
        }

        private void AddNewMenuItem_Click(object sender, EventArgs e)
        {
            Size = new Size(1120, Size.Height);
            EditPanel.Visible = false;
            SearchPanel.Visible = false;
            AddPanel.Visible = true;
        }

        private void SeachMenuItem_Click(object sender, EventArgs e)
        {
            Size = new Size(1120, Size.Height);
            AddPanel.Visible = false;
            EditPanel.Visible = false;
            SearchPanel.Visible = true;
            foreach(Heater heat in heaters)
            {
                SearchManufactCombo.Items.Add(heat.Manufacturer);
                SearchAreaCombo.Items.Add(heat.Service_area);
            }

        }

        private void EditMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                Size = new Size(1120, Size.Height);
                AddPanel.Visible = false;
                SearchPanel.Visible = false;
                EditPanel.Visible = true;
                foreach(Heater heat in heaters)
                {
                    if(Convert.ToInt32(listView.SelectedItems[0].SubItems[0].Text) == heat.Id)
                    {
                        EditManufacturer.Text = heat.Manufacturer;
                        EditModel.Text = heat.Model;
                        EditArea.Text = heat.Service_area;
                        EditPowerCombo.Text = Convert.ToString(heat.Power);
                        EditSupplyCombo.Text = heat.Power_suply;
                        EditPlacingCombo.Text = heat.Placing;
                        EditPurposeCombo.Text = heat.Purpose;
                        EditControlCombo.Text = heat.Control;
                        EditElementCombo.Text = heat.Heating_element;
                        EditDimms.Text = heat.Dimensions;
                        EditCost.Text = Convert.ToString(heat.Cost);
                        EditCount.Text = Convert.ToString(heat.Count);
                        EditSectionCombo.Text = Convert.ToString(heat.Section_count);
                    }
                }
                listView.Enabled = false;
            } else
                MessageBox.Show("Виберіть елемент для редагування!");
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 0)
            {
                DialogResult dialogRes = MessageBox.Show("Ви дійсно бажаєте видалити вибраний запис?",
                    "Видалення запису", MessageBoxButtons.YesNo);
                if (dialogRes == DialogResult.Yes)
                {
                    heaters.RemoveAt(listView.SelectedIndices[0]);
                    listView.Items.RemoveAt(listView.SelectedItems[0].Index);
                }                
            }else
                MessageBox.Show("Виберіть елемент для видалення");
        }

        private void HowMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addCancelButton_Click(object sender, EventArgs e)
        {
            Size = new Size(825, Size.Height);
            AddPanel.Visible = false;
            EditPanel.Visible = false;
        }

        private void editCancelButton_Click(object sender, EventArgs e)
        {
            Size = new Size(825, Size.Height);
            AddPanel.Visible = false;
            EditPanel.Visible = false;
            listView.Enabled = true;
        }

        private void editClearButton_Click(object sender, EventArgs e)
        {
            EditControlCombo.Text = EditArea.Text = EditCost.Text = EditCount.Text = EditDimms.Text =
            EditElementCombo.Text = EditManufacturer.Text = EditModel.Text = EditPlacingCombo.Text =
            EditPowerCombo.Text = EditPurposeCombo.Text = EditSectionCombo.Text = EditSupplyCombo.Text = "";
            listView.Enabled = true;
        }

        private void addClearButton_Click(object sender, EventArgs e)
        {
            addAreaTextBox.Text = addControlCombo.Text = addCostTextBox.Text = addCounTextBox.Text = addDimmsTextBox.Text =
            addElementCombo.Text = addManufacturerTextBox.Text = addModelTextBox.Text = addPlacingCombo.Text =
            addPowerCombo.Text = addPurposeCombo.Text = addSectCombo.Text = addSuplyCombo.Text = "";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            addAreaTextBox.Enabled = addControlCombo.Enabled = addCostTextBox.Enabled = addCounTextBox.Enabled = addDimmsTextBox.Enabled =
            addElementCombo.Enabled = addManufacturerTextBox.Enabled = addModelTextBox.Enabled = addPlacingCombo.Enabled =
            addPowerCombo.Enabled = addPurposeCombo.Enabled = addSectCombo.Enabled = addSuplyCombo.Enabled = true;

            if (comboBox1.Text == "Масляний радіатор")
            {

                addSectCombo.Visible = true;
                labelSections.Visible = true;
            }
            else
            {
                addSectCombo.Visible = false;
                labelSections.Visible = false;
            }
        }

        private void EditCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (e.KeyChar == '.') || Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void addCounTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        private void addCostTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (e.KeyChar == '.') || Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void editCounTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView.GridLines = true;
            listView.FullRowSelect = true;
            listView.View = View.Details;
            listView.Columns.Add("ID");
            listView.Columns.Add("Тип");
            listView.Columns.Add("Виробник");
            listView.Columns.Add("Модель");
            listView.Columns.Add("Площа");
            listView.Columns.Add("Потужність");
            listView.Columns.Add("Живлення");
            listView.Columns.Add("Монтаж");
            listView.Columns.Add("Призначення");
            listView.Columns.Add("Керування");
            listView.Columns.Add("Гріючий елемент");
            listView.Columns.Add("Габарити");
            listView.Columns.Add("Вартість");
            listView.Columns.Add("Кількість");
            listView.Columns.Add("К-сть секцій");
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Тепловентилятор")
                {
                    heaters.Add(new FanHeater(
                        comboBox1.Text,
                        addManufacturerTextBox.Text,
                        addModelTextBox.Text,
                        addAreaTextBox.Text,
                        Convert.ToInt32(addPowerCombo.Text),
                        addSuplyCombo.Text,
                        addPlacingCombo.Text,
                        addPurposeCombo.Text,
                        addControlCombo.Text,
                        addElementCombo.Text,
                        addDimmsTextBox.Text,
                        Convert.ToDouble(addCostTextBox.Text),
                        Convert.ToInt32(addCounTextBox.Text)));
                }
                else if (comboBox1.Text == "Інфрачервоний обігрівач")
                {
                    heaters.Add(new InfraRedHeater(
                        comboBox1.Text,
                        addManufacturerTextBox.Text,
                        addModelTextBox.Text,
                        addAreaTextBox.Text,
                        Convert.ToInt32(addPowerCombo.Text),
                        addSuplyCombo.Text,
                        addPlacingCombo.Text,
                        addPurposeCombo.Text,
                        addControlCombo.Text,
                        addElementCombo.Text,
                        addDimmsTextBox.Text,
                        Convert.ToDouble(addCostTextBox.Text),
                        Convert.ToInt32(addCounTextBox.Text)));
                }
                else if (comboBox1.Text == "Конвектор")
                {
                    heaters.Add(new Convector(
                        comboBox1.Text,
                        addManufacturerTextBox.Text,
                        addModelTextBox.Text,
                        addAreaTextBox.Text,
                        Convert.ToInt32(addPowerCombo.Text),
                        addSuplyCombo.Text,
                        addPlacingCombo.Text,
                        addPurposeCombo.Text,
                        addControlCombo.Text,
                        addElementCombo.Text,
                        addDimmsTextBox.Text,
                        Convert.ToDouble(addCostTextBox.Text),
                        Convert.ToInt32(addCounTextBox.Text)));
                }
                else if (comboBox1.Text == "Масляний радіатор" && addSectCombo.Text != "")
                {
                    heaters.Add(new OilRadiator(
                        comboBox1.Text,
                        addManufacturerTextBox.Text,
                        addModelTextBox.Text,
                        addAreaTextBox.Text,
                        Convert.ToInt32(addPowerCombo.Text),
                        addSuplyCombo.Text,
                        addPlacingCombo.Text,
                        addPurposeCombo.Text,
                        addControlCombo.Text,
                        addElementCombo.Text,
                        addDimmsTextBox.Text,
                        Convert.ToDouble(addCostTextBox.Text),
                        Convert.ToInt32(addCounTextBox.Text),
                        Convert.ToInt32(addSectCombo.Text)));
                }
                else if (comboBox1.Text == "Керамічна панель")
                {
                    heaters.Add(new CeramicPanel(
                        comboBox1.Text,
                        addManufacturerTextBox.Text,
                        addModelTextBox.Text,
                        addAreaTextBox.Text,
                        Convert.ToInt32(addPowerCombo.Text),
                        addSuplyCombo.Text,
                        addPlacingCombo.Text,
                        addPurposeCombo.Text,
                        addControlCombo.Text,
                        addElementCombo.Text,
                        addDimmsTextBox.Text,
                        Convert.ToDouble(addCostTextBox.Text),
                        Convert.ToInt32(addCounTextBox.Text)));
                }
                else if (comboBox1.Text == "Теплова гармата")
                {
                    heaters.Add(new HeatGun(
                        comboBox1.Text,
                        addManufacturerTextBox.Text,
                        addModelTextBox.Text,
                        addAreaTextBox.Text,
                        Convert.ToInt32(addPowerCombo.Text),
                        addSuplyCombo.Text,
                        addPlacingCombo.Text,
                        addPurposeCombo.Text,
                        addControlCombo.Text,
                        addElementCombo.Text,
                        addDimmsTextBox.Text,
                        Convert.ToDouble(addCostTextBox.Text),
                        Convert.ToInt32(addCounTextBox.Text)));
                }
                else
                    MessageBox.Show("Виберіть тип обігрівача із списку!");
                ShowDataInList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message), "Сталася помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Heater heat in heaters)
                {
                    if (Convert.ToInt32(listView.SelectedItems[0].SubItems[0].Text) == heat.Id)
                    {
                        heat.Manufacturer = EditManufacturer.Text;
                        heat.Model = EditModel.Text;
                        heat.Service_area = EditArea.Text;
                        heat.Power = Convert.ToInt32(EditPowerCombo.Text);
                        heat.Power_suply = EditSupplyCombo.Text;
                        heat.Placing = EditPlacingCombo.Text;
                        heat.Purpose = EditPurposeCombo.Text;
                        heat.Control = EditControlCombo.Text;
                        heat.Heating_element = EditElementCombo.Text;
                        heat.Dimensions = EditDimms.Text;
                        heat.Cost = Convert.ToInt32(EditCost.Text);
                        heat.Count = Convert.ToInt32(EditCount.Text);
                        heat.Section_count = Convert.ToInt32(EditSectionCombo.Text);
                    }
                }
                listView.Enabled = true;
                ShowDataInList();

                forAction act = editCancelButton_Click;
                act(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message), "Сталася помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addAreaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        private void EditArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        private void EditPowerCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        private void addPowerCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogRes = MessageBox.Show("Ви дійсно бажаєте закрити поточну сесію?\n" +
                "Зміни не будуть збережені.","Закрити поточну сесію", MessageBoxButtons.YesNo);
            if (dialogRes == DialogResult.Yes)
            {
                heaters.Clear();
                listView.Items.Clear();
            }
        }

        private void SearchClearButton_Click(object sender, EventArgs e)
        {
            SearchAreaCombo.Text = SearchControlCombo.Text = SearchPlacingCombo.Text = SearchPowerCombo.Text =
                SearchPurposeCombo.Text = SearchSectCombo.Text = SearchSupplyCombo.Text = SearchTypeCombo.Text = SearchManufactCombo.Text = "";
        }

        private void SearchCancelButton_Click(object sender, EventArgs e)
        {
            Size = new Size(825, Size.Height);
            SearchPanel.Visible = false;
            ShowDataInList();
        }

        private void SearchTypeCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach(Heater heat in heaters)
                    {
                        if(heat.HeaterType == SearchTypeCombo.Text)
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchManufactCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Manufacturer == SearchManufactCombo.Text)
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchAreaCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Service_area == SearchAreaCombo.Text)
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchPowerCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Power == Convert.ToInt32(SearchPowerCombo.Text))
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchSupplyCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Power_suply == SearchSupplyCombo.Text)
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchPlacingCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Placing == SearchPlacingCombo.Text)
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchPurposeCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Purpose == SearchPurposeCombo.Text)
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchControlCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Control == SearchControlCombo.Text)
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }

        private void SearchSectCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    listView.Items.Clear();
                    foreach (Heater heat in heaters)
                    {
                        if (heat.Section_count == Convert.ToInt32(SearchSectCombo.Text))
                        {
                            string[] split = heat.OutputInfo().Split(new Char[] { '*' });
                            ListViewItem item = new ListViewItem(split);
                            listView.Items.Add(item);
                        }
                    }
                    autoResizeColumns(listView);
                }
                return;
            }
        }
    }
}