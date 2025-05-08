using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class People : Form
    {
        public People()
        {
            InitializeComponent();

            //listView顯示所有資料函式
            Refresh();
            
            //全部清除函式
            Clear();           
        }

        //listView顯示所有資料函式
        public void Refresh()
        {
            //讀取資料
            string link;
            link = Application.StartupPath + "data\\People.txt";
            StreamReader r_count = new StreamReader(link);
            string sepatator = ",";  //以逗號分割字串
            char[] cgap = sepatator.ToCharArray();
            //逐行讀取到底，計算資料行數量
            int count = 0;
            while (!r_count.EndOfStream)
            {
                r_count.ReadLine();  //讀取一列資料行
                count += 1;
            }
            //close the file
            r_count.Close();

            //開啟檔案
            StreamReader r = new StreamReader(link);
            this.listView_People.Items.Clear();  //只移除資料列，保留標題列           
                                                 //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行
                //listView寫入新資料
                ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], str2[3], str2[4] });
                listView_People.Items.Add(b);

                //comboBox加入選項
                comboBox_Department.Items.Add(str2[2]);

                //comboBox移除重複選項
                for (int k = 0; k < comboBox_Department.Items.Count; k++)
                {
                    string cItem1 = comboBox_Department.Items[k].ToString();
                    for (int m = k + 1; m < comboBox_Department.Items.Count; m++)
                    {
                        string cItem2 = comboBox_Department.Items[m].ToString();
                        if (cItem1 == cItem2)
                        {
                            comboBox_Department.Items.RemoveAt(m);
                            comboBox_Department.Text = "";
                        }
                    }
                }
            }
            //close the file
            r.Close();
        }

        //全部加入Button
        private void button_Add_Click(object sender, EventArgs e)
        {
            //若TextBox為空值，不執行
            if ((string.IsNullOrEmpty(textBox_Num_1.Text) || string.IsNullOrEmpty(textBox_Name_1.Text) || string.IsNullOrEmpty(textBox_Department_1.Text) || string.IsNullOrEmpty(textBox_Job_Title_1.Text) || string.IsNullOrEmpty(textBox_Position_1.Text)) && (string.IsNullOrEmpty(textBox_Num_2.Text) || string.IsNullOrEmpty(textBox_Name_2.Text) || string.IsNullOrEmpty(textBox_Department_2.Text) || string.IsNullOrEmpty(textBox_Job_Title_2.Text) || string.IsNullOrEmpty(textBox_Position_2.Text)) && (string.IsNullOrEmpty(textBox_Num_3.Text) || string.IsNullOrEmpty(textBox_Name_3.Text) || string.IsNullOrEmpty(textBox_Department_3.Text) || string.IsNullOrEmpty(textBox_Job_Title_3.Text) || string.IsNullOrEmpty(textBox_Position_3.Text)) && (string.IsNullOrEmpty(textBox_Num_4.Text) || string.IsNullOrEmpty(textBox_Name_4.Text) || string.IsNullOrEmpty(textBox_Department_4.Text) || string.IsNullOrEmpty(textBox_Job_Title_4.Text) || string.IsNullOrEmpty(textBox_Position_4.Text)))
            {
                MessageBox.Show("請輸入內容");
                return;
            }

            string[] Num = [textBox_Num_1.Text, textBox_Num_2.Text, textBox_Num_3.Text, textBox_Num_4.Text];
            string[] Name = [textBox_Name_1.Text, textBox_Name_2.Text, textBox_Name_3.Text, textBox_Name_4.Text];
            string[] Department = [textBox_Department_1.Text, textBox_Department_2.Text, textBox_Department_3.Text, textBox_Department_4.Text];
            string[] Job_Title = [textBox_Job_Title_1.Text, textBox_Job_Title_2.Text, textBox_Job_Title_3.Text, textBox_Job_Title_4.Text];
            string[] Position = [textBox_Position_1.Text, textBox_Position_2.Text, textBox_Position_3.Text, textBox_Position_4.Text];
            //listView_Memu寫入新資料
            for (int i = 0; i < Name.Length; i++)
            {
                //無輸入的產品資料不寫入listView
                if (Name[i] != "")
                {
                    ListViewItem c = new ListViewItem(new string[] { Num[i], Name[i], Department[i], Job_Title[i], Position[i] });
                    listView_People.Items.Add(c);
                }
            }
            //全部清除函式
            Clear();
        }

        //儲存Button
        private void button_Save_Click(object sender, EventArgs e)
        {
            //若listView沒有資料列，顯示提示框。
            if (listView_People.Items.Count == 0)
            {
                MessageBox.Show("請還原原始資料");
                return;
            }

            string s = "";
            for (int m = 0; m < listView_People.Items.Count; m++)
            {
                for (int n = 0; n < listView_People.Items[m].SubItems.Count; n++)
                {
                    s += listView_People.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            string link;
            link = Application.StartupPath + "data\\People.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            MessageBox.Show("儲存成功：" + link);

        }

        //部門ComboBox
        private void comboBox_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            //讀取檔案
            string link;
            link = Application.StartupPath + "data\\People.txt";
            StreamReader r_count = new StreamReader(link);
            string sepatator = ",";  //以逗號分割字串
            char[] cgap = sepatator.ToCharArray();
            //逐行讀取到底，計算資料行數量
            int count = 0;
            while (!r_count.EndOfStream)
            {
                r_count.ReadLine();  //讀取一列資料行
                count += 1;
            }
            //close the file
            r_count.Close();

            //開啟檔案
            StreamReader r = new StreamReader(link);
            //清空listView
            this.listView_People.Items.Clear();  //只移除資料列，保留標題列
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //依照店家，listView顯示菜單
                if (comboBox_Department.Text == Convert.ToString(str2[2]))
                {
                    //listView_Menu寫入新資料
                    ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], str2[3], str2[4] });
                    listView_People.Items.Add(b);
                }
            }
            //close the file
            r.Close();
        }

        //全部清除函式
        public void Clear()
        {
            //清除TextBox內容
            textBox_Num_1.ResetText();
            textBox_Num_2.ResetText();
            textBox_Num_3.ResetText();
            textBox_Num_4.ResetText();
            textBox_Name_1.ResetText();
            textBox_Name_2.ResetText();
            textBox_Name_3.ResetText();
            textBox_Name_4.ResetText();
            textBox_Department_1.ResetText();
            textBox_Department_2.ResetText();
            textBox_Department_3.ResetText();
            textBox_Department_4.ResetText();
            textBox_Job_Title_1.ResetText();
            textBox_Job_Title_2.ResetText();
            textBox_Job_Title_3.ResetText();
            textBox_Job_Title_4.ResetText();
            textBox_Position_1.ResetText();
            textBox_Position_2.ResetText();
            textBox_Position_3.ResetText();
            textBox_Position_4.ResetText();
        }

        //還原Button
        private void button_Refresh_Click(object sender, EventArgs e)
        {
            //listView顯示所有資料函式
            Refresh();
        }

        //全部清除Button
        private void button_Clear_Click(object sender, EventArgs e)
        {
            //全部清除函式
            Clear();
        }

        //移除列Button
        private void button_Remove_Click(object sender, EventArgs e)
        {
            //若沒有資料列，顯示提示框。
            if (listView_People.Items.Count > 0)
            {
                //若有資料列，但未選擇資料列，顯示提示框。
                if (listView_People.SelectedItems.Count > 0)
                {
                    listView_People.Items.Remove(listView_People.SelectedItems[0]);
                }
                else
                {
                    MessageBox.Show("請點選要移除的資料列。");
                    return;
                }
            }
            else
            {
                MessageBox.Show("沒有可移除的資料列。");
                return;
            }
        }

        //listView)Menu 排序Button
        private void listView_People_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_People.Sorting == SortOrder.Ascending)
            {
                listView_People.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_People.Sorting = SortOrder.Ascending;
            }
            listView_People.Sort();
        }

        //ComboBox樣式
        private void comboBox_Department_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department.Cursor = Cursors.Hand;
        }

        //Button樣式
        private void button_Clear_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Clear.BackColor = Color.CadetBlue;
            button_Clear.ForeColor = Color.White;
            button_Clear.Cursor = Cursors.Hand;
        }

        private void button_Clear_Mouse_Leave(object sender, EventArgs e)
        {
            button_Clear.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Clear.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Add_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Add.BackColor = Color.CadetBlue;
            button_Add.ForeColor = Color.White;
            button_Add.Cursor = Cursors.Hand;
        }

        private void button_Add_Mouse_Leave(object sender, EventArgs e)
        {
            button_Add.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Add.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Remove_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Remove.BackColor = Color.CadetBlue;
            button_Remove.ForeColor = Color.White;
            button_Remove.Cursor = Cursors.Hand;
        }

        private void button_Remove_Mouse_Leave(object sender, EventArgs e)
        {
            button_Remove.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Remove.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Save_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Save.BackColor = Color.CadetBlue;
            button_Save.ForeColor = Color.White;
            button_Save.Cursor = Cursors.Hand;
        }

        private void button_Save_Mouse_Leave(object sender, EventArgs e)
        {
            button_Save.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Save.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Refresh_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Refresh.BackColor = Color.CadetBlue;
            button_Refresh.ForeColor = Color.White;
            button_Refresh.Cursor = Cursors.Hand;
        }

        private void button_Refresh_Mouse_Leave(object sender, EventArgs e)
        {
            button_Refresh.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Refresh.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
