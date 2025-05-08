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
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();

            //listView顯示所有資料函式
            Refresh();

            //儲存listView_Detail函式
            Save_Detail("Detail");
        }

        //listView顯示所有資料函式
        public void Refresh()
        {
            label_Department.Text = "";
            label_Name.Text = "";
            label_Balance.Text = "";
            label_Hint.Text = "";

            //Deposit.txt
            //讀取資料
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
            //清空listView
            this.listView_Detail.Items.Clear();  //只移除資料列，保留標題列
            string[] department = [];
            string[] name = [];
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Deposit - 1; i++)
            {
                string str1 = r_Deposit.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Deposit, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行
                //listView寫入新資料
                ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], " ", "儲值", str2[3] });
                listView_Detail.Items.Add(b);

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


            //Consumption.txt
            //讀取資料
            string link_Consumption;
            link_Consumption = Application.StartupPath + "data\\Consumption.txt";
            StreamReader r_count_Consumption = new StreamReader(link_Consumption);
            string sepatator_Consumption = ",";  //以逗號分割字串
            char[] cgap_Consumption = sepatator_Consumption.ToCharArray();
            //逐行讀取到底，計算資料行數量
            int count_Consumption = 0;
            while (!r_count_Consumption.EndOfStream)
            {
                r_count_Consumption.ReadLine();  //讀取一列資料行
                count_Consumption += 1;
            }
            //close the file
            r_count_Consumption.Close();

            //開啟檔案
            StreamReader r_Consumption = new StreamReader(link_Consumption);
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Consumption - 1; i++)
            {
                string str1 = r_Consumption.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Consumption, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行
                //listView寫入新資料
                ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], " ", str2[4], (str2[5] + "：" + str2[2]), str2[6] });
                listView_Detail.Items.Add(b);
            }
            //close the file
            r_Consumption.Close();
        }

        //部門comboBox
        private void comboBox_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] Deposit = [];
            string[] Consumption = [];

            //Deposit.txt
            //讀取資料
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
            //清空listView
            this.listView_Detail.Items.Clear();  //只移除資料列，保留標題列
            comboBox_Name.Text = "";
            label_Department.Text = "";
            label_Name.Text = "";
            label_Balance.Text = "";
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
            //計算餘額
            string[] Deposit_balance = [];
            string[] Consumption_balance = [];

            //篩選日期
            string[] Deposit = [];
            string[] Consumption = [];

            //Deposit.txt
            //讀取資料
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
            //清空listView
            this.listView_Detail.Items.Clear();  //只移除資料列，保留標題列
            comboBox_Department.Text = "";
            label_Department.Text = "";
            label_Name.Text = comboBox_Name.Text;
            label_Balance.Text = "";
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

                //篩選姓名
                if (Convert.ToString(comboBox_Name.Text) == Convert.ToString(str2[1]))
                {
                    //Deposit
                    // 調整陣列的大小
                    System.Array.Resize(ref Deposit_balance, Deposit_balance.Length + 1);
                    // 指定新的陣列值
                    Deposit_balance[Deposit_balance.Length - 1] = str2[2];

                    //篩選日期
                    if ((Convert.ToDateTime(str2[3]) > Convert.ToDateTime(dateTimePicker_Start.Value)) && (Convert.ToDateTime(str2[3]) < Convert.ToDateTime(dateTimePicker_End.Value)))
                    {
                        //Deposit
                        // 調整陣列的大小
                        System.Array.Resize(ref Deposit, Deposit.Length + 1);
                        // 指定新的陣列值
                        Deposit[Deposit.Length - 1] = str2[2];

                        //listView寫入新資料
                        ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], " ", "儲值", str2[3] });
                        listView_Detail.Items.Add(b);

                        //label顯示部門
                        label_Department.Text = str2[0];
                    }
                }

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
                }
            }
            //close the file
            r.Close();


            //Consumption.txt
            //讀取資料
            string link_Consumption;
            link_Consumption = Application.StartupPath + "data\\Consumption.txt";
            StreamReader r_count_Consumption = new StreamReader(link_Consumption);
            string sepatator_Consumption = ",";  //以逗號分割字串
            char[] cgap_Consumption = sepatator_Consumption.ToCharArray();
            //逐行讀取到底，計算資料行數量
            int count_Consumption = 0;
            while (!r_count_Consumption.EndOfStream)
            {
                r_count_Consumption.ReadLine();  //讀取一列資料行
                count_Consumption += 1;
            }
            //close the file
            r_count_Consumption.Close();

            //開啟檔案
            StreamReader r_Consumption = new StreamReader(link_Consumption);
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Consumption - 1; i++)
            {
                string str1 = r_Consumption.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Consumption, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //篩選姓名
                if (comboBox_Name.Text == Convert.ToString(str2[1]))
                {
                    //Consumption
                    // 調整陣列的大小
                    System.Array.Resize(ref Consumption_balance, Consumption_balance.Length + 1);
                    // 指定新的陣列值
                    Consumption_balance[Consumption_balance.Length - 1] = str2[4];

                    //篩選日期
                    if ((Convert.ToDateTime(str2[6]) > Convert.ToDateTime(dateTimePicker_Start.Value)) && (Convert.ToDateTime(str2[6]) < Convert.ToDateTime(dateTimePicker_End.Value)))
                    {
                        //Consumption
                        // 調整陣列的大小
                        System.Array.Resize(ref Consumption, Consumption.Length + 1);
                        // 指定新的陣列值
                        Consumption[Consumption.Length - 1] = str2[4];

                        //listView寫入新資料
                        ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], " ", str2[4], (str2[5] + "：" + str2[2]), str2[6] });
                        listView_Detail.Items.Add(b);
                    }
                }
            }

            //close the file
            r_Consumption.Close();

            //計算餘額
            int balance = 0;
            for (int k = 0; k < Deposit_balance.Length; k++)
            {
                balance += Int16.Parse(Deposit_balance[k]);
            }
            for (int k = 0; k < Consumption_balance.Length; k++)
            {
                balance -= Int16.Parse(Consumption_balance[k]);
            }
            label_Balance.Text = Convert.ToString(balance);
        }

        //儲存listView_Detail函式
        public void Save_Detail(string fileName)
        {
            string s = "";
            for (int m = 0; m < listView_Detail.Items.Count; m++)
            {
                for (int n = 0; n < listView_Detail.Items[m].SubItems.Count; n++)
                {
                    s += listView_Detail.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            string link;
            link = Application.StartupPath + "data\\" + fileName + ".txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            //MessageBox.Show("儲存成功：" + link);
        }

        //下載個人Button函式
        public void Download_File_Person()
        {
            string Link_Name = "";

            //取得所有人名字
            //讀取資料
            string link;
            link = Application.StartupPath + "data\\Balance.txt";
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
            string Name = "";
            string Department = "";
            string Balance = "";
            string Date = "";
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                if (label_Name.Text != "")
                {
                    if (label_Name.Text == Convert.ToString(str2[1]))
                    {
                        Link_Name = label_Name.Text;
                        Name = str2[1];
                        Department = str2[0];
                        Balance = str2[2];
                        Date = str2[3];
                    }
                }
                else
                {
                    MessageBox.Show("請選擇部門或姓名");
                    return;
                }
            }
            //儲存listView新增資訊函式
            Save_listView_Add_Information(dateTimePicker_Start.Text, dateTimePicker_End.Text, Name, Department, Convert.ToString("$ " + string.Format("{0:0,0}", Balance)), Date);
            //儲存listView_Detail函式
            Save_Detail(Link_Name);
            //close the file
            r.Close();
        }

        //下載個人Button
        private void button_Download_Person_Click(object sender, EventArgs e)
        {
            //下載個人Button函式
            Download_File_Person();
        }

        //下載總表Button
        private void button_Download_allData_Click(object sender, EventArgs e)
        {
            //listView顯示所有資料函式
            Refresh();

            //儲存listView_Detail函式
            Save_Detail("Detail");
        }

        //下載所有人Button
        private void button_Download_People_Click(object sender, EventArgs e)
        {
            //下載所有人Button函式
            Download_File_People();

            label_Hint.Text = "已儲存";
        }

        //下載所有人Button函式
        public void Download_File_People()
        {
            //Deposit.txt
            //取得姓名
            //讀取資料
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
            //清空listView
            this.listView_Detail.Items.Clear();  //只移除資料列，保留標題列
            string[] Name = [];
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //Name
                // 調整陣列的大小
                System.Array.Resize(ref Name, Name.Length + 1);
                // 指定新的陣列值
                Name[Name.Length - 1] = str2[1];
            }
            //close the file
            r.Close();


            //儲存所有人資料
            for (int k = 0; k < Name.Length; k++)
            {
                comboBox_Name.Text = Name[k];

                //Deposit.txt
                //讀取資料
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
                //清空listView
                this.listView_Detail.Items.Clear();  //只移除資料列，保留標題列
                //不讀取檔案最後兩行空行
                for (int i = 0; i < count_Deposit - 1; i++)
                {
                    string str1 = r_Deposit.ReadLine();  //讀取一列資料行
                    if (str1 == null) break;  //若資料為空值，則跳過
                    string[] str2 = str1.Split(cgap_Deposit, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                    //篩選姓名
                    if (Convert.ToString(comboBox_Name.Text) == Convert.ToString(str2[1]))
                    {
                        //篩選日期
                        if ((Convert.ToDateTime(str2[3]) > Convert.ToDateTime(dateTimePicker_Setting.Value)) && (Convert.ToDateTime(str2[3]) < Convert.ToDateTime(dateTimePicker_End.Value)))
                        {
                            //listView寫入新資料
                            ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], " ", "儲值", str2[3] });
                            listView_Detail.Items.Add(b);
                        }
                    }
                }
                //close the file
                r_Deposit.Close();


                //Consumption.txt
                //讀取資料
                string link_Consumption;
                link_Consumption = Application.StartupPath + "data\\Consumption.txt";
                StreamReader r_count_Consumption = new StreamReader(link_Consumption);
                string sepatator_Consumption = ",";  //以逗號分割字串
                char[] cgap_Consumption = sepatator_Consumption.ToCharArray();
                //逐行讀取到底，計算資料行數量
                int count_Consumption = 0;
                while (!r_count_Consumption.EndOfStream)
                {
                    r_count_Consumption.ReadLine();  //讀取一列資料行
                    count_Consumption += 1;
                }
                //close the file
                r_count_Consumption.Close();

                //開啟檔案
                StreamReader r_Consumption = new StreamReader(link_Consumption);
                //不讀取檔案最後兩行空行
                for (int i = 0; i < count_Consumption - 1; i++)
                {
                    string str1 = r_Consumption.ReadLine();  //讀取一列資料行
                    if (str1 == null) break;  //若資料為空值，則跳過
                    string[] str2 = str1.Split(cgap_Consumption, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                    //篩選姓名
                    if (comboBox_Name.Text == Convert.ToString(str2[1]))
                    {
                        //篩選日期
                        if ((Convert.ToDateTime(str2[6]) > Convert.ToDateTime(dateTimePicker_Setting.Value)) && (Convert.ToDateTime(str2[6]) < Convert.ToDateTime(dateTimePicker_End.Value)))
                        {
                            //listView寫入新資料
                            ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], " ", str2[4], (str2[5] + "：" + str2[2]), str2[6] });
                            listView_Detail.Items.Add(b);
                        }
                    }
                }
                //close the file
                r_Consumption.Close();


                //Balance.txt
                //讀取資料
                string link_Balance;
                link_Balance = Application.StartupPath + "data\\Balance.txt";
                StreamReader r_count_Balance = new StreamReader(link_Balance);
                string sepatator_Balance = ",";  //以逗號分割字串
                char[] cgap_Balance = sepatator_Balance.ToCharArray();
                //逐行讀取到底，計算資料行數量
                int count_Balance = 0;
                while (!r_count_Balance.EndOfStream)
                {
                    r_count_Balance.ReadLine();  //讀取一列資料行
                    count_Balance += 1;
                }
                //close the file
                r_count_Balance.Close();

                //開啟檔案
                StreamReader r_Balance = new StreamReader(link_Balance);
                //不讀取檔案最後兩行空行
                for (int i = 0; i < count_Balance - 1; i++)
                {
                    string str1 = r_Balance.ReadLine();  //讀取一列資料行
                    if (str1 == null) break;  //若資料為空值，則跳過
                    string[] str2 = str1.Split(cgap_Balance, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                    //篩選姓名
                    if (comboBox_Name.Text == Convert.ToString(str2[1]))
                    {
                        //儲存listView新增資訊函式
                        Save_listView_Add_Information(dateTimePicker_Setting.Text, dateTimePicker_End.Text, Convert.ToString(str2[1]), Convert.ToString(str2[0]), Convert.ToString("$ " + string.Format("{0:0,0}", Convert.ToString(str2[2]))), Convert.ToString(str2[3]));
                    }
                }
                //close the file
                r_Balance.Close();

                //儲存listView_函式
                Save_Detail(comboBox_Name.Text);
            }
        }

        //儲存listView新增資訊函式
        public void Save_listView_Add_Information(string information_dateTimeStart, string information_dateTimeEnd, string information_Name, string information_Department, string information_Balance, string Date)
        {
            ListViewItem b6 = new ListViewItem(new string[] { "------------------", "------------------", "------------------", "------------------" });
            listView_Detail.Items.Add(b6);
            ListViewItem b7 = new ListViewItem(new string[] { "起始日期", information_dateTimeStart });
            listView_Detail.Items.Add(b7);
            ListViewItem b8 = new ListViewItem(new string[] { "終止日期", information_dateTimeEnd });
            listView_Detail.Items.Add(b8);
            listView_Detail.Items.Add("\n");
            ListViewItem b1 = new ListViewItem(new string[] { "------------------", "------------------" });
            listView_Detail.Items.Add(b1);
            ListViewItem b2 = new ListViewItem(new string[] { "姓名", information_Name });
            listView_Detail.Items.Add(b2);
            ListViewItem b3 = new ListViewItem(new string[] { "部門", information_Department });
            listView_Detail.Items.Add(b3);
            ListViewItem b4 = new ListViewItem(new string[] { "餘額", information_Balance });
            listView_Detail.Items.Add(b4);
            ListViewItem b5 = new ListViewItem(new string[] { "最後統計時間", Date });
            listView_Detail.Items.Add(b5);
        }

        //DateTimePicker函式
        public void DateTimePicker()
        {
            //listView顯示所有資料函式
            Refresh();

            //Detail.txt
            //讀取資料
            string link_Order;
            link_Order = Application.StartupPath + "data\\Detail.txt";
            StreamReader r_count_Order = new StreamReader(link_Order);
            string sepatator_Order = ",";  //以逗號分割字串
            char[] cgap_Order = sepatator_Order.ToCharArray();
            //逐行讀取到底，計算資料行數量
            int count_Order = 0;
            while (!r_count_Order.EndOfStream)
            {
                r_count_Order.ReadLine();  //讀取一列資料行
                count_Order += 1;
            }
            //close the file
            r_count_Order.Close();

            //開啟檔案
            StreamReader r_Order = new StreamReader(link_Order);
            this.listView_Detail.Items.Clear();  //只移除資料列，保留標題列                                                                                                
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Order - 1; i++)
            {
                string str1 = r_Order.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Order, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //篩選日期
                if ((Convert.ToDateTime(str2[5]) > Convert.ToDateTime(dateTimePicker_Start.Value)) && (Convert.ToDateTime(str2[5]) < Convert.ToDateTime(dateTimePicker_End.Value)))
                {
                    //listView寫入新資料
                    ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], str2[3], str2[4], str2[5] });
                    listView_Detail.Items.Add(b);
                }
            }
            //close the file
            r_Order.Close();
        }

        //開始日期dateTimePicker
        private void dateTimePicker_Start_ValueChanged(object sender, EventArgs e)
        {
            //DateTimePicker函式
            DateTimePicker();
        }

        //結束日期dateTimePicker
        private void dateTimePicker_End_ValueChanged(object sender, EventArgs e)
        {
            //DateTimePicker函式
            DateTimePicker();
        }

        //listView_Detail 排序Button
        private void listView_Detail_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_Detail.Sorting == SortOrder.Ascending)
            {
                listView_Detail.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_Detail.Sorting = SortOrder.Ascending;
            }
            listView_Detail.Sort();
        }

        //ComboBox樣式
        private void comboBox_Name_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Name.Cursor = Cursors.Hand;
        }

        private void comboBox_Department_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department.Cursor = Cursors.Hand;
        }

        //Button樣式
        private void button_Download_Person_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Download_Person.BackColor = Color.CadetBlue;
            button_Download_Person.ForeColor = Color.White;
            button_Download_Person.Cursor = Cursors.Hand;
        }

        private void button_Download_Person_Mouse_Leave(object sender, EventArgs e)
        {
            button_Download_Person.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Download_Person.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Download_People_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Download_People.BackColor = Color.CadetBlue;
            button_Download_People.ForeColor = Color.White;
            button_Download_People.Cursor = Cursors.Hand;
        }

        private void button_Download_People_Mouse_Leave(object sender, EventArgs e)
        {
            button_Download_People.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Download_People.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Download_allData_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Download_allData.BackColor = Color.CadetBlue;
            button_Download_allData.ForeColor = Color.White;
            button_Download_allData.Cursor = Cursors.Hand;
        }

        private void button_Download_allData_Mouse_Leave(object sender, EventArgs e)
        {
            button_Download_allData.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Download_allData.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        //dataTimePicker樣式
        private void dateTimePicker_Start_Mouse_Move(object sender, EventArgs e)
        {
            dateTimePicker_Start.Cursor = Cursors.Hand;
        }

        private void dateTimePicker_End_Mouse_Move(object sender, EventArgs e)
        {
            dateTimePicker_End.Cursor = Cursors.Hand;
        }

        private void dateTimePicker_Setting_Mouse_Move(object sender, EventArgs e)
        {
            dateTimePicker_Setting.Cursor = Cursors.Hand;
        }
    }
}