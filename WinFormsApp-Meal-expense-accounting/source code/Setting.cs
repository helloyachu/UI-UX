using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class Setting : Form
    {
        public Setting()
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
            //Setting.txt
            //讀取資料
            string link;
            link = Application.StartupPath + "data\\Setting.txt";
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
            this.listView_Setting.Items.Clear();  //只移除資料列，保留標題列           
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行
                //listView寫入新資料
                ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], "******", str2[3], str2[4] });
                listView_Setting.Items.Add(b);

                //comboBox加入選項
                comboBox_Permissions.Items.Add(str2[0]);
                comboBox_Permissions_1.Items.Add(str2[0]);
                comboBox_Permissions_2.Items.Add(str2[0]);
                comboBox_Permissions_3.Items.Add(str2[0]);
                comboBox_Permissions_4.Items.Add(str2[0]);

                //comboBox移除重複選項
                for (int k = 0; k < comboBox_Permissions.Items.Count; k++)
                {
                    string cItem1 = comboBox_Permissions.Items[k].ToString();
                    for (int m = k + 1; m < comboBox_Permissions.Items.Count; m++)
                    {
                        string cItem2 = comboBox_Permissions.Items[m].ToString();
                        if (cItem1 == cItem2)
                        {
                            comboBox_Permissions.Items.RemoveAt(m);
                            comboBox_Permissions.Text = "";
                            comboBox_Permissions_1.Items.RemoveAt(m);
                            comboBox_Permissions_1.Text = "";
                            comboBox_Permissions_2.Items.RemoveAt(m);
                            comboBox_Permissions_2.Text = "";
                            comboBox_Permissions_3.Items.RemoveAt(m);
                            comboBox_Permissions_3.Text = "";
                            comboBox_Permissions_4.Items.RemoveAt(m);
                            comboBox_Permissions_4.Text = "";
                        }
                    }
                }
            }
            //close the file
            r.Close();



            //部門、姓名comboBox加入選項
            string link_Deposit;
            link_Deposit = Application.StartupPath + "data\\Deposit.txt";
            StreamReader r_count_Deposit = new StreamReader(link_Deposit);
            string sepatator_Deposit = ",";  //以逗號分割字串
            char[] cgap_Deposit = sepatator_Deposit.ToCharArray();
            //逐行讀取到底，計算資料行數量
            int count_Deposit = 0;
            while (!r_count_Deposit.EndOfStream)
            {
                r_count_Deposit.ReadLine();  //讀取一列資料行
                count_Deposit += 1;
            }
            //close the file
            r_count_Deposit.Close();

            //開啟檔案
            StreamReader r_Deposit = new StreamReader(link_Deposit);
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Deposit - 1; i++)
            {
                string str1 = r_Deposit.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Deposit, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //comboBox加入選項
                comboBox_Department_1.Items.Add(str2[0]);
                comboBox_Department_2.Items.Add(str2[0]);
                comboBox_Department_3.Items.Add(str2[0]);
                comboBox_Department_4.Items.Add(str2[0]);
                comboBox_Name_1.Items.Add(str2[1]);
                comboBox_Name_2.Items.Add(str2[1]);
                comboBox_Name_3.Items.Add(str2[1]);
                comboBox_Name_4.Items.Add(str2[1]);

                //comboBox移除重複選項
                for (int k = 0; k < comboBox_Name_1.Items.Count; k++)
                {
                    string cItem1 = comboBox_Name_1.Items[k].ToString();
                    for (int m = k + 1; m < comboBox_Name_1.Items.Count; m++)
                    {
                        string cItem2 = comboBox_Name_1.Items[m].ToString();
                        if (cItem1 == cItem2)
                        {
                            comboBox_Name_1.Items.RemoveAt(m);
                            comboBox_Name_1.Text = "";
                            comboBox_Name_2.Items.RemoveAt(m);
                            comboBox_Name_2.Text = "";
                            comboBox_Name_3.Items.RemoveAt(m);
                            comboBox_Name_3.Text = "";
                            comboBox_Name_4.Items.RemoveAt(m);
                            comboBox_Name_4.Text = "";
                        }
                    }
                }

                //comboBox移除重複選項
                for (int k = 0; k < comboBox_Department_1.Items.Count; k++)
                {
                    string cItem1 = comboBox_Department_1.Items[k].ToString();
                    for (int m = k + 1; m < comboBox_Department_1.Items.Count; m++)
                    {
                        string cItem2 = comboBox_Department_1.Items[m].ToString();
                        if (cItem1 == cItem2)
                        {
                            comboBox_Department_1.Items.RemoveAt(m);
                            comboBox_Department_1.Text = "";
                            comboBox_Department_2.Items.RemoveAt(m);
                            comboBox_Department_2.Text = "";
                            comboBox_Department_3.Items.RemoveAt(m);
                            comboBox_Department_3.Text = "";
                            comboBox_Department_4.Items.RemoveAt(m);
                            comboBox_Department_4.Text = "";
                        }
                    }
                }
            }
            //close the file
            r_Deposit.Close();
        }

        //全部加入Button
        private void button_Add_Click(object sender, EventArgs e)
        {
            //若TextBox為空值，不執行
            if ((string.IsNullOrEmpty(comboBox_Permissions_1.Text) || string.IsNullOrEmpty(textBox_Access_1.Text) || string.IsNullOrEmpty(textBox_Password_1.Text) || string.IsNullOrEmpty(comboBox_Department_1.Text) || string.IsNullOrEmpty(comboBox_Name_1.Text)) && (string.IsNullOrEmpty(comboBox_Permissions_2.Text) || string.IsNullOrEmpty(textBox_Access_2.Text) || string.IsNullOrEmpty(textBox_Password_2.Text) || string.IsNullOrEmpty(comboBox_Department_2.Text) || string.IsNullOrEmpty(comboBox_Name_2.Text)) && (string.IsNullOrEmpty(comboBox_Permissions_3.Text) || string.IsNullOrEmpty(textBox_Access_3.Text) || string.IsNullOrEmpty(textBox_Password_3.Text) || string.IsNullOrEmpty(comboBox_Department_3.Text) || string.IsNullOrEmpty(comboBox_Name_3.Text)) && (string.IsNullOrEmpty(comboBox_Permissions_4.Text) || string.IsNullOrEmpty(textBox_Access_4.Text) || string.IsNullOrEmpty(textBox_Password_4.Text) || string.IsNullOrEmpty(comboBox_Department_4.Text) || string.IsNullOrEmpty(comboBox_Name_4.Text)))
            {
                MessageBox.Show("請輸入內容");
                return;
            }

            string[] Permissions = [comboBox_Permissions_1.Text, comboBox_Permissions_2.Text, comboBox_Permissions_3.Text, comboBox_Permissions_4.Text];
            string[] Access = [textBox_Access_1.Text, textBox_Access_2.Text, textBox_Access_3.Text, textBox_Access_4.Text];
            string[] Password = [textBox_Password_1.Text, textBox_Password_2.Text, textBox_Password_3.Text, textBox_Password_4.Text];
            string[] Department = [comboBox_Department_1.Text, comboBox_Department_2.Text, comboBox_Department_3.Text, comboBox_Department_4.Text];
            string[] Name = [comboBox_Name_1.Text, comboBox_Name_2.Text, comboBox_Name_3.Text, comboBox_Name_4.Text];
            //listView_Memu寫入新資料
            for (int i = 0; i < Name.Length; i++)
            {
                //無輸入的產品資料不寫入listView
                if (Name[i] != "")
                {
                    ListViewItem c = new ListViewItem(new string[] { Permissions[i], Access[i], Password[i], Department[i], Name[i] });
                    listView_Setting.Items.Add(c);
                }
            }
            //全部清除函式
            Clear();
        }

        //儲存Button
        private void button_Save_Click(object sender, EventArgs e)
        {
            //若listView沒有資料列，顯示提示框。
            if (listView_Setting.Items.Count == 0)
            {
                MessageBox.Show("請還原原始資料");
                return;
            }

            string s = "";
            for (int m = 0; m < listView_Setting.Items.Count; m++)
            {
                for (int n = 0; n < listView_Setting.Items[m].SubItems.Count; n++)
                {
                    s += listView_Setting.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            string link;
            link = Application.StartupPath + "data\\Setting.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            MessageBox.Show("儲存成功：" + link);

        }

        //權限ComboBox
        private void comboBox_Permissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //讀取檔案
            string link;
            link = Application.StartupPath + "data\\Setting.txt";
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
            this.listView_Setting.Items.Clear();  //只移除資料列，保留標題列
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //依照權限，listView顯示資料
                if (comboBox_Permissions.Text == Convert.ToString(str2[0]))
                {
                    //listView_Menu寫入新資料
                    ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], "******", str2[3], str2[4] });
                    listView_Setting.Items.Add(b);
                }
            }
            //close the file
            r.Close();
        }

        //全部清除函式
        public void Clear()
        {
            //清除TextBox內容
            comboBox_Permissions_1.ResetText();
            comboBox_Permissions_2.ResetText();
            comboBox_Permissions_3.ResetText();
            comboBox_Permissions_4.ResetText();
            textBox_Access_1.ResetText();
            textBox_Access_2.ResetText();
            textBox_Access_3.ResetText();
            textBox_Access_4.ResetText();
            textBox_Password_1.ResetText();
            textBox_Password_2.ResetText();
            textBox_Password_3.ResetText();
            textBox_Password_4.ResetText();
            comboBox_Department_1.ResetText();
            comboBox_Department_2.ResetText();
            comboBox_Department_3.ResetText();
            comboBox_Department_4.ResetText();
            comboBox_Name_1.ResetText();
            comboBox_Name_2.ResetText();
            comboBox_Name_3.ResetText();
            comboBox_Name_4.ResetText();
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
            if (listView_Setting.Items.Count > 0)
            {
                //若有資料列，但未選擇資料列，顯示提示框。
                if (listView_Setting.SelectedItems.Count > 0)
                {
                    listView_Setting.Items.Remove(listView_Setting.SelectedItems[0]);
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
        private void listView_Setting_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_Setting.Sorting == SortOrder.Ascending)
            {
                listView_Setting.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_Setting.Sorting = SortOrder.Ascending;
            }
            listView_Setting.Sort();
        }

        //ComboBox樣式
        private void comboBox_Department_1_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department_1.Cursor = Cursors.Hand;
        }

        private void comboBox_Department_2_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department_2.Cursor = Cursors.Hand;
        }

        private void comboBox_Department_3_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department_3.Cursor = Cursors.Hand;
        }

        private void comboBox_Department_4_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department_4.Cursor = Cursors.Hand;
        }

        private void comboBox_Name_1_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Name_1.Cursor = Cursors.Hand;
        }

        private void comboBox_Name_2_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Name_2.Cursor = Cursors.Hand;
        }

        private void comboBox_Name_3_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Name_3.Cursor = Cursors.Hand;
        }

        private void comboBox_Name_4_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Name_4.Cursor = Cursors.Hand;
        }

        private void comboBox_Permissions_1_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Permissions_1.Cursor = Cursors.Hand;
        }

        private void comboBox_Permissions_2_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Permissions_2.Cursor = Cursors.Hand;
        }

        private void comboBox_Permissions_3_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Permissions_3.Cursor = Cursors.Hand;
        }

        private void comboBox_Permissions_4_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Permissions_4.Cursor = Cursors.Hand;
        }
        private void comboBox_Permissions_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Permissions.Cursor = Cursors.Hand;
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
