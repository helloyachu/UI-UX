using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();

            //表格設定
            listView_Deposit.View = View.Details;  //設定顯示方式
            listView_Deposit.FullRowSelect = true;  //是否選中整行
            listView_Deposit.Scrollable = true;  //是否自動顯示滾動條
            listView_Deposit.MultiSelect = true;  //是否可以選擇多行
            listView_Deposit.GridLines = true;  //是否顯示表格框線

            //更新listView_Deposit函式
            Refresh_Deposit();

            //儲存listView函式
            Save_Balance();

            //更新listView_Balance函式
            Refresh_Balance();

            //儲存listView函式
            Save_Balance();
        }

        //更新listView_Deposit函式
        public void Refresh_Deposit()
        {
            this.listView_Deposit.Items.Clear();  //只移除資料列，保留標題列
            this.listView_Balance.Items.Clear();  //只移除資料列，保留標題列

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
            string[] department_Consumption = [];
            string[] name_Consumption = [];
            int[] Consumption = [];
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Consumption - 1; i++)
            {
                string str1 = r_Consumption.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Consumption, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行
                
                //department_Consumption
                // 調整陣列的大小
                System.Array.Resize(ref department_Consumption, department_Consumption.Length + 1);
                // 指定新的陣列值
                department_Consumption[department_Consumption.Length - 1] = str2[0];

                //name_Consumption
                // 調整陣列的大小
                System.Array.Resize(ref name_Consumption, name_Consumption.Length + 1);
                // 指定新的陣列值
                name_Consumption[name_Consumption.Length - 1] = str2[1];

                //Consumption
                // 調整陣列的大小
                System.Array.Resize(ref Consumption, Consumption.Length + 1);
                // 指定新的陣列值
                Consumption[Consumption.Length - 1] = 0;

                //原消費金額加入新增消費金額
                for (int j = 0; j < name_Consumption.Length; j++)
                {
                    if (str2[1] == name_Consumption[j])
                    {
                        Consumption[j] += Int32.Parse(str2[4]);
                    }
                }
            }

            //找出重複資料
            int[] remove_num = [];
            for (int k = 0; k < name_Consumption.Length; k++)
            {
                for (int m = k + 1; m < name_Consumption.Length; m++)
                {
                    if (name_Consumption[k] == name_Consumption[m])
                    {
                        //Debug.WriteLine(name_Consumption[k] + m);

                        // 調整陣列的大小
                        System.Array.Resize(ref remove_num, remove_num.Length + 1);
                        // 指定新的陣列值
                        remove_num[remove_num.Length - 1] = m;
                    }
                }
            }

            //將重複資料的儲值變0
            for (int h = 0; h < remove_num.Length; h++)
            {
                //將重複資料的儲值變0
                Consumption[remove_num[h]] = 0;
                name_Consumption[remove_num[h]] = null;
            }

            //listView_Balance寫入新資料
            for (int d = 0; d < name_Consumption.Length; d++)
            {
                //儲值變0的資料不寫入listView
                if (Consumption[d] != 0)
                {
                    //現在時間
                    DateTime currentTime = DateTime.Now;
                    //listView_Balance寫入新資料
                    ListViewItem c = new ListViewItem(new string[] { department_Consumption[d], name_Consumption[d], Convert.ToString(Consumption[d]*(-1)), Convert.ToString(currentTime) });
                    listView_Balance.Items.Add(c);
                }
            }
            //close the file
            r_Consumption.Close();


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
            string[] department = [];
            string[] name_Deposit = [];
            int[] deposit = [];
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Deposit - 1; i++)
            {
                string str1 = r_Deposit.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Deposit, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //listView_Deposit寫入新資料
                ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], str2[3] });
                listView_Deposit.Items.Add(b);

                //listView_Balance
                //department
                // 調整陣列的大小
                System.Array.Resize(ref department, department.Length + 1);
                // 指定新的陣列值
                department[department.Length - 1] = str2[0];

                //name_Deposit
                // 調整陣列的大小
                System.Array.Resize(ref name_Deposit, name_Deposit.Length + 1);
                // 指定新的陣列值
                name_Deposit[name_Deposit.Length - 1] = str2[1];

                //deposit
                // 調整陣列的大小
                System.Array.Resize(ref deposit, deposit.Length + 1);
                // 指定新的陣列值
                deposit[deposit.Length - 1] = 0;

                //原儲值金額加入新增儲值金額
                for (int j = 0; j < name_Deposit.Length; j++)
                {
                    if (str2[1] == name_Deposit[j])
                    {
                        deposit[j] += Int32.Parse(str2[2]);
                    }
                }
            }

            //找出重複資料
            int[] remove_num_Deposit = [];
            for (int k = 0; k < name_Deposit.Length; k++)
            {
                for (int m = k + 1; m < name_Deposit.Length; m++)
                {
                    if (name_Deposit[k] == name_Deposit[m])
                    {
                        //Debug.WriteLine(name_Deposit[k] + m);

                        // 調整陣列的大小
                        System.Array.Resize(ref remove_num_Deposit, remove_num_Deposit.Length + 1);
                        // 指定新的陣列值
                        remove_num_Deposit[remove_num_Deposit.Length - 1] = m;
                    }
                }
            }

            //將重複資料的儲值變0
            for (int h = 0; h < remove_num_Deposit.Length; h++)
            {
                //將重複資料的儲值變0
                deposit[remove_num_Deposit[h]] = 0;
                name_Deposit[remove_num_Deposit[h]] = null;
                department[remove_num_Deposit[h]] = null;
            }

            //listView_Balance寫入新資料
            for (int d = 0; d < name_Deposit.Length; d++)
            {
                //儲值變0的資料不寫入listView
                if (deposit[d] != 0)
                {
                    //現在時間
                    DateTime currentTime = DateTime.Now;
                    //listView_Balance寫入新資料
                    ListViewItem c = new ListViewItem(new string[] { department[d], name_Deposit[d], Convert.ToString(deposit[d]), Convert.ToString(currentTime) });
                    listView_Balance.Items.Add(c);

                    //comboBox加入選項
                    comboBox_Department.Items.Add(department[d]);
                    comboBox_Name.Items.Add(name_Deposit[d]);

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
            }
            //close the file
            r_Deposit.Close();
        }


        public void Refresh_Balance()
        {
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
            string[] department_Balance = [];
            string[] name_Balance = [];
            int[] Balance = [];
            this.listView_Balance.Items.Clear();  //只移除資料列，保留標題列
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Balance - 1; i++)
            {
                string str1 = r_Balance.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Balance, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //department_Balance
                // 調整陣列的大小
                System.Array.Resize(ref department_Balance, department_Balance.Length + 1);
                // 指定新的陣列值
                department_Balance[department_Balance.Length - 1] = str2[0];

                //name_Balance
                // 調整陣列的大小
                System.Array.Resize(ref name_Balance, name_Balance.Length + 1);
                // 指定新的陣列值
                name_Balance[name_Balance.Length - 1] = str2[1];

                //Balance
                // 調整陣列的大小
                System.Array.Resize(ref Balance, Balance.Length + 1);
                // 指定新的陣列值
                //close the file
                Balance[Balance.Length - 1] = 0;

                //原餘額金額加入新增餘額金額
                for (int j = 0; j < name_Balance.Length; j++)
                {
                    if (str2[1] == name_Balance[j])
                    {
                        Balance[j] += Int32.Parse(str2[2]);
                    }
                }
            }

            //找出重複資料
            int[] remove_num_Balance = [];
            for (int k = 0; k < name_Balance.Length; k++)
            {
                for (int m = k + 1; m < name_Balance.Length; m++)
                {
                    if (name_Balance[k] == name_Balance[m])
                    {
                        //Debug.WriteLine(name_Balance[k] + m);

                        // 調整陣列的大小
                        System.Array.Resize(ref remove_num_Balance, remove_num_Balance.Length + 1);
                        // 指定新的陣列值
                        remove_num_Balance[remove_num_Balance.Length - 1] = m;
                    }
                }
            }

            //將重複資料的儲值變0
            for (int h = 0; h < remove_num_Balance.Length; h++)
            {
                //將重複資料的儲值變0
                Balance[remove_num_Balance[h]] = 0;
                name_Balance[remove_num_Balance[h]] = null;
            }

            //listView_Balance寫入新資料
            for (int d = 0; d < name_Balance.Length; d++)
            {
                //儲值變0的資料不寫入listView
                if (Balance[d] != 0)
                {
                    //現在時間
                    DateTime currentTime = DateTime.Now;
                    //listView_Balance寫入新資料
                    ListViewItem c = new ListViewItem(new string[] { department_Balance[d], name_Balance[d], Convert.ToString(Balance[d]), Convert.ToString(currentTime) });
                    listView_Balance.Items.Add(c);
                }
            }
            //close the file
            r_Balance.Close();


            //label統計資料
            //零用金總額label_Total
            int sum = 0;
            //平均值label_Average
            int average = 0;
            //總人數label_Num
            int num = 0;
            //標準值label_Standard
            int sd = 500;
            //低於標準值人數label_Standard_Num
            int sd_num = 0;
            for (int k = 0; k < name_Balance.Length; k++)
            {
                //零用金總額label_Total
                sum += Balance[k];

                if (Balance[k] != 0)
                {
                    //總人數label_Num
                    num++;
                    //低於標準值人數label_Standard_Num
                    if (Balance[k] < sd)
                    {
                        sd_num++;
                    }
                }
            }
            //零用金總額label_Total
            //10100
            //Debug.WriteLine(sum);
            //10,100
            //Debug.WriteLine(string.Format("{0:0,0}", sum));
            //10,100.00
            //Debug.WriteLine(string.Format("{0:N}", sum));
            //平均值label_Average
            average = sum / num;

            //label顯示數值
            label_Total.Text = Convert.ToString("$ " + string.Format("{0:0,0}", sum));
            label_Average.Text = Convert.ToString("$ " + string.Format("{0:0,0}", average));
            label_Num.Text = Convert.ToString(string.Format("{0:D}", num) + "人");
            label_Standard.Text = Convert.ToString("$ " + string.Format("{0:0,0}", sd));
            label_Standard_Num.Text = Convert.ToString(string.Format("{0:D}", sd_num) + "人");
        }

        //儲存listView函式
        public void Save_Balance()
        {
             //若listView沒有資料列，顯示提示框。
            if (listView_Balance.Items.Count == 0)
            {
                MessageBox.Show("請還原原始資料");
                return;
            }

            string s = "";
            for (int m = 0; m < listView_Balance.Items.Count; m++)
            {
                for (int n = 0; n < listView_Balance.Items[m].SubItems.Count; n++)
                {
                    s += listView_Balance.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            //讀取資料
            string link;
            link = Application.StartupPath + "data\\Balance.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            //MessageBox.Show("儲存成功：" + link);
        }

        //寫入Button
        private void button_Write_Click(object sender, EventArgs e)
        {
            //若comboBox、TextBox為空值，不執行
            if (string.IsNullOrEmpty(comboBox_Department.Text) || string.IsNullOrEmpty(comboBox_Name.Text) || string.IsNullOrEmpty(textBox_Deposit.Text))
            {
                MessageBox.Show("請輸入內容");
                return;
            }

            //新增表格內容
            ListViewItem item = new ListViewItem(comboBox_Department.Text);
            item.SubItems.Add(comboBox_Name.Text);
            item.SubItems.Add(textBox_Deposit.Text);
            //現在時間
            DateTime currentTime = DateTime.Now;
            item.SubItems.Add(Convert.ToString(currentTime));
            listView_Deposit.Items.Add(item);

            //清除comboBox、TextBox內容
            comboBox_Department.ResetText();
            comboBox_Name.ResetText();
            textBox_Deposit.Clear();

            //指定游標輸入框
            comboBox_Department.Focus();
        }

        //儲存Button
        private void button_Save_Click(object sender, EventArgs e)
        {
            //若listView沒有資料列，顯示提示框。
            if (listView_Deposit.Items.Count == 0)
            {
                MessageBox.Show("請還原原始資料");
                return;
            }

            string s = "";
            for (int m = 0; m < listView_Deposit.Items.Count; m++)
            {
                for (int n = 0; n < listView_Deposit.Items[m].SubItems.Count; n++)
                {
                    s += listView_Deposit.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            string link;
            link = Application.StartupPath + "data\\Deposit.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            MessageBox.Show("儲存成功：" + link);

            //更新listView_Deposit函式
            Refresh_Deposit();

            //儲存listView函式
            Save_Balance();

            //更新listView_Balance函式
            Refresh_Balance();

            //儲存listView函式
            Save_Balance();
        }

        //部門comboBox
        private void comboBox_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        //listView_Deposit 排序Button
        private void listView_Deposit_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_Deposit.Sorting == SortOrder.Ascending)
            {
                listView_Deposit.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_Deposit.Sorting = SortOrder.Ascending;
            }
            listView_Deposit.Sort();
        }

        //listView_Balance 排序Button
        private void listView_Balance_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_Balance.Sorting == SortOrder.Ascending)
            {
                listView_Balance.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_Balance.Sorting = SortOrder.Ascending;
            }
            listView_Balance.Sort();
        }

        //listView_Deposit 移除列Button
        private void button_Remove_Deposit_Click(object sender, EventArgs e)
        {
            //若沒有資料列，顯示提示框。
            if (listView_Deposit.Items.Count > 0)
            {
                //若有資料列，但未選擇資料列，顯示提示框。
                if (listView_Deposit.SelectedItems.Count > 0)
                {
                    listView_Deposit.Items.Remove(listView_Deposit.SelectedItems[0]);
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

        //還原Button
        private void button_Refresh_Click(object sender, EventArgs e)
        {
            //更新listView_Deposit函式
            Refresh_Deposit();

            //儲存listView函式
            Save_Balance();

            //更新listView_Balance函式
            Refresh_Balance();

            //儲存listView函式
            Save_Balance();
        }

        //ComboBox樣式
        private void comboBox_Department_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department.Cursor = Cursors.Hand;
        }

        private void comboBox_Name_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Name.Cursor = Cursors.Hand;
        }

        //Button樣式
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

        private void button_Remove_Deposit_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Remove_Deposit.BackColor = Color.CadetBlue;
            button_Remove_Deposit.ForeColor = Color.White;
            button_Remove_Deposit.Cursor = Cursors.Hand;
        }

        private void button_Remove_Deposit_Mouse_Leave(object sender, EventArgs e)
        {
            button_Remove_Deposit.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Remove_Deposit.ForeColor = Color.FromArgb(0, 0, 0, 0);
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

        private void button_Write_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Write.BackColor = Color.CadetBlue;
            button_Write.ForeColor = Color.White;
            button_Write.Cursor = Cursors.Hand;
        }

        private void button_Write_Mouse_Leave(object sender, EventArgs e)
        {
            button_Write.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Write.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}


