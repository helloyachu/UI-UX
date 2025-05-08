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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();

            //店家、部門、姓名ComboBox加入選項函式
            ComboBox_Item_Add();

            //數量預設值為1
            textBox_Count.Text = "1";
        }

        //店家comboBox，listView_Menu顯示菜單資料
        private void comboBox_Restaurant_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Product.Text = "";
            //textBox_Count = "1";

            //讀取檔案
            string link;
            link = Application.StartupPath + "data\\Restaurant.txt";
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
            this.listView_Menu.Items.Clear();  //只移除資料列，保留標題列
            //清空comboBox菜單選項
            comboBox_Product.Items.Clear();
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //依照店家，listView顯示菜單
                if (comboBox_Restaurant.Text == Convert.ToString(str2[0]))
                {
                    //listView_Menu寫入新資料
                    ListViewItem b = new ListViewItem(new string[] { str2[3], str2[4] });
                    listView_Menu.Items.Add(b);

                    //pictureBox顯示圖片
                    if (Convert.ToString(comboBox_Restaurant.SelectedItem) == Convert.ToString(str2[0]))
                    {
                        pictureBox_Menu.Image = Image.FromFile(str2[5]);
                    }
                    else
                    {
                        pictureBox_Menu.Image = null;
                        Debug.WriteLine("沒有圖片");
                        break;
                    }

                    //comboBox加入菜單選項
                    comboBox_Product.Items.Add(str2[3]);

                    //label顯示店家名稱
                    label_Restaurant.Text = comboBox_Restaurant.Text;
                }
            }
            //close the file
            r.Close();
        }

        //菜單圖片Button
        private void button_Menu_Click(object sender, EventArgs e)
        {
            SaveFileDialog upload_pictureBox = new SaveFileDialog();
            upload_pictureBox.InitialDirectory = "C:\\";  //顯示初始目錄
            upload_pictureBox.Filter = "txt files(*.txt)|*.txt|All files(*.*)|*.*";  //文件名稱篩選字串
            upload_pictureBox.FilterIndex = 2;
            upload_pictureBox.RestoreDirectory = true;
            //使用者在對話框點選確定，則結果為DialogResult.OK
            //否則結果為DialogResult.Cancel

            if (upload_pictureBox.ShowDialog() == DialogResult.OK)  //開啟檔案
            {
                MessageBox.Show("下載成功：" + upload_pictureBox.FileName);  //顯示選擇的檔案名稱

                //下載菜單圖片
                pictureBox_Menu.Image.Save(upload_pictureBox.FileName);
            }
        }

        //加入Button
        private void button_Add_Click(object sender, EventArgs e)
        {
            //若TextBox為空值，不執行
            if (string.IsNullOrEmpty(comboBox_Department.Text) || string.IsNullOrEmpty(comboBox_Name.Text) || string.IsNullOrEmpty(comboBox_Product.Text) || string.IsNullOrEmpty(textBox_Count.Text))
            {
                MessageBox.Show("請輸入內容");
                return;
            }

            //新增表格內容
            ListViewItem item = new ListViewItem(comboBox_Department.Text);
            item.SubItems.Add(comboBox_Name.Text);
            item.SubItems.Add(comboBox_Product.Text);
            item.SubItems.Add(textBox_Count.Text);
            item.SubItems.Add(Convert.ToString(label_Total_Price.Text));
            item.SubItems.Add(label_Restaurant.Text);
            //現在時間
            DateTime currentTime = DateTime.Now;
            item.SubItems.Add(Convert.ToString(currentTime));
            listView_Order.Items.Add(item);

            //清除comboBox、TextBox內容
            comboBox_Product.ResetText();

            //指定游標輸入框
            comboBox_Product.Focus();
        }

        //送出訂單Button
        private void button_Submit_Click(object sender, EventArgs e)
        {
            //若listView沒有資料列，顯示提示框。
            if (listView_Order.Items.Count == 0)
            {
                MessageBox.Show("尚未完成點餐");
                return;
            }

            //開啟檔案
            string link;
            link = Application.StartupPath + "data\\Consumption.txt";
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

            //設為空值
            string s = "";
            //開啟檔案
            StreamReader r = new StreamReader(link);
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //加入舊資料
                for (int m = 0; m < str2.Length; m++)
                {
                    s += str2[m] + ",";
                }
                //讀取舊資料最後一筆不換行
                if (i != (count - 1))
                {
                    s += "\r\n";
                }
            }
            //close the file
            r.Close();

            //加入新資料
            for (int m = 0; m < listView_Order.Items.Count; m++)
            {
                for (int n = 0; n < listView_Order.Items[m].SubItems.Count; n++)
                {
                    s += listView_Order.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            //開啟檔案
            string link_Save;
            link_Save = Application.StartupPath + "data\\Consumption.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
                //close the file
                sw.Close();
            }
            MessageBox.Show("儲存成功：" + link_Save);

            //只移除資料列，保留標題列
            this.listView_Order.Items.Clear();
        }

        //清除Button
        private void button_Clear_Click(object sender, EventArgs e)
        {     
            comboBox_Product.Text = "";
            textBox_Count.Text = "1";
        }

        //移除列Button
        private void button_Remove_Click(object sender, EventArgs e)
        {
            //若沒有資料列，顯示提示框。
            if (listView_Order.Items.Count > 0)
            {
                //若有資料列，但未選擇資料列，顯示提示框。
                if (listView_Order.SelectedItems.Count > 0)
                {
                    listView_Order.Items.Remove(listView_Order.SelectedItems[0]);
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
        private void listView_Menu_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_Menu.Sorting == SortOrder.Ascending)
            {
                listView_Menu.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_Menu.Sorting = SortOrder.Ascending;
            }
            listView_Menu.Sort();
        }

        //listView_Order 排序Button
        private void listView_Order_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_Order.Sorting == SortOrder.Ascending)
            {
                listView_Order.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_Order.Sorting = SortOrder.Ascending;
            }
            listView_Order.Sort();
        }

        //品名comboBox
        private void comboBox_Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            //計算價格
            string link;
            link = Application.StartupPath + "data\\Restaurant.txt";
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
            //當textBox為空值，顯示1。
            while (textBox_Count.Text == "")
            {
                textBox_Count.Text = "1";
            }
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                if (Convert.ToString(comboBox_Product.Text) == Convert.ToString(str2[3]))
                {
                    //顯示單價
                    label_Unit_Price.Text = str2[4];
                    //計算總額
                    Total_Price();
                }
            }
        }

        //計算總額函式
        public void Total_Price()
        {
            //若comboBox、TextBox為空值，不執行
            if (string.IsNullOrEmpty(label_Unit_Price.Text) || string.IsNullOrEmpty(textBox_Count.Text))
                return;

            int Unit_Price = Int32.Parse(label_Unit_Price.Text);
            int Count = Int32.Parse(textBox_Count.Text);
            int Total_Price = Unit_Price * Count;
            label_Total_Price.Text = Total_Price.ToString();
        }

        //數量textBox
        private void textBox_Count_TextChanged(object sender, EventArgs e)
        {
            //計算總額
            Total_Price();
        }

        //店家、部門、姓名ComboBox加入選項函式
        public void ComboBox_Item_Add()
        {
            //店家comboBox加入選項
            string link;
            link = Application.StartupPath + "data\\Restaurant.txt";
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
            string[] Restaurant = [];
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //Restaurant
                // 調整陣列的大小
                System.Array.Resize(ref Restaurant, Restaurant.Length + 1);
                // 指定新的陣列值
                Restaurant[Restaurant.Length - 1] = str2[0];

                //comboBox加入所有店家
                for (int d = 0; d < Restaurant.Length; d++)
                {
                    comboBox_Restaurant.Items.Add(Restaurant[d]);

                    //comboBox移除重複選項
                    for (int k = 0; k < comboBox_Restaurant.Items.Count; k++)
                    {
                        string cItem1 = comboBox_Restaurant.Items[k].ToString();
                        for (int m = k + 1; m < comboBox_Restaurant.Items.Count; m++)
                        {
                            string cItem2 = comboBox_Restaurant.Items[m].ToString();
                            if (cItem1 == cItem2)
                            {
                                comboBox_Restaurant.Items.RemoveAt(m);
                            }
                        }
                    }
                }
            }
            //close the file
            r.Close();
            //店家comboBox預設選取第一個選項
            comboBox_Restaurant.SelectedIndex = 0;


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
                comboBox_Department.Items.Add(str2[0]);
                comboBox_Name.Items.Add(str2[1]);

                //comboBox移除重複選項
                for (int k = 0; k < comboBox_Name.Items.Count; k++)
                {
                    string cItem1 = comboBox_Name.Items[k].ToString();
                    for (int m = k + 1; m < comboBox_Name.Items.Count; m++)
                    {
                        string cItem2 = comboBox_Name.Items[m].ToString();
                        if (cItem1 == cItem2)
                        {
                            comboBox_Name.Items.RemoveAt(m);
                            comboBox_Name.Text = "";
                        }
                    }
                }

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
            r_Deposit.Close();
        }

        //部門comboBox
        private void comboBox_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            //讀取檔案
            string link;
            link = Application.StartupPath + "data\\Deposit.txt";
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
            string[] department = [];
            string[] name = [];
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //department
                // 調整陣列的大小
                System.Array.Resize(ref department, department.Length + 1);
                // 指定新的陣列值
                department[department.Length - 1] = str2[0];

                //name
                // 調整陣列的大小
                System.Array.Resize(ref name, name.Length + 1);
                // 指定新的陣列值
                name[name.Length - 1] = str2[1];


                //清空comboBox選項
                comboBox_Name.Items.Clear();
                for (int d = 0; d < name.Length; d++)
                {
                    //依照部門，comboBox加入所屬姓名
                    if (comboBox_Department.Text == department[d])
                    {
                        comboBox_Name.Items.Add(name[d]);
                    }

                    //comboBox移除重複選項
                    for (int k = 0; k < comboBox_Name.Items.Count; k++)
                    {
                        string cItem1 = comboBox_Name.Items[k].ToString();
                        for (int m = k + 1; m < comboBox_Name.Items.Count; m++)
                        {
                            string cItem2 = comboBox_Name.Items[m].ToString();
                            if (cItem1 == cItem2)
                            {
                                comboBox_Name.Items.RemoveAt(m);
                                comboBox_Name.Text = "";
                            }
                        }
                    }
                }
            }
            //close the file
            r.Close();
        }

        //姓名comboBox
        private void comboBox_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            //讀取檔案
            string link;
            link = Application.StartupPath + "data\\Deposit.txt";
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
            string[] department = [];
            string[] name = [];
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //department
                // 調整陣列的大小
                System.Array.Resize(ref department, department.Length + 1);
                // 指定新的陣列值
                department[department.Length - 1] = str2[0];

                //name
                // 調整陣列的大小
                System.Array.Resize(ref name, name.Length + 1);
                // 指定新的陣列值
                name[name.Length - 1] = str2[1];


                //comboBox加入所有姓名
                for (int d = 0; d < name.Length; d++)
                {
                    comboBox_Name.Items.Add(name[d]);

                    //comboBox移除重複選項
                    for (int k = 0; k < comboBox_Name.Items.Count; k++)
                    {
                        string cItem1 = comboBox_Name.Items[k].ToString();
                        for (int m = k + 1; m < comboBox_Name.Items.Count; m++)
                        {
                            string cItem2 = comboBox_Name.Items[m].ToString();
                            if (cItem1 == cItem2)
                            {
                                comboBox_Name.Items.RemoveAt(m);
                            }
                        }
                    }

                    //依照姓名，comboBox顯示所屬部門
                    if (comboBox_Name.Text == name[d])
                    {
                        comboBox_Department.Text = department[d];
                        comboBox_Name.Text = name[d];
                    }
                }
            }
            //close the file
            r.Close();
        }

        //ComboBox樣式
        private void comboBox_Restaurant_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Restaurant.Cursor = Cursors.Hand;
        }

        private void comboBox_Department_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department.Cursor = Cursors.Hand;
        }

        private void comboBox_Name_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Name.Cursor = Cursors.Hand;
        }

        private void comboBox_Product_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Product.Cursor = Cursors.Hand;
        }

        //Button樣式
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

        private void button_Menu_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Menu.BackColor = Color.CadetBlue;
            button_Menu.ForeColor = Color.White;
            button_Menu.Cursor = Cursors.Hand;
        }

        private void button_Menu_Mouse_Leave(object sender, EventArgs e)
        {
            button_Menu.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Menu.ForeColor = Color.FromArgb(0, 0, 0, 0);
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

        private void button_Submit_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Submit.BackColor = Color.CadetBlue;
            button_Submit.ForeColor = Color.White;
            button_Submit.Cursor = Cursors.Hand;
        }

        private void button_Submit_Mouse_Leave(object sender, EventArgs e)
        {
            button_Submit.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Submit.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
