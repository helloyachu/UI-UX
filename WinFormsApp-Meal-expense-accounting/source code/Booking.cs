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

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class Booking : Form
    {
        public Booking()
        {
            InitializeComponent();

            //更新listView函式
            Refresh();

            //儲存listView_Booking函式
            Save_Booking();

            //儲存listView_Order函式
            Save_Order();
        }

        //更新listView函式
        public void Refresh()
        {
            //清空店家數量label
            label_Restaurant.Text = "";
            //清空電話數量label
            label_Phone.Text = "";
            //清空地址數量label
            label_Address.Text = "";
            //清空數量label
            label_Count.Text = "";
            //清空總金額label
            label_Price_Total.Text = "";
            //清空總數量label
            label_Count_Total.Text = "";


            //Consumption.txt
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

            //開啟檔案
            StreamReader r = new StreamReader(link);
            string[] department = [];
            string[] name = [];
            string[] product = [];
            int[] product_count_booking = [];
            int[] product_price_booking = [];
            int[] product_count_order = [];
            string[] restaurant = [];
            string[] date = [];
            this.listView_Booking.Items.Clear();  //只移除資料列，保留標題列
            this.listView_Order.Items.Clear();  //只移除資料列，保留標題列                                                
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //listView_Booking
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

                //product
                // 調整陣列的大小
                System.Array.Resize(ref product, product.Length + 1);
                // 指定新的陣列值
                product[product.Length - 1] = str2[2];

                //product_count_booking
                // 調整陣列的大小
                System.Array.Resize(ref product_count_booking, product_count_booking.Length + 1);
                // 指定新的陣列值
                product_count_booking[product_count_booking.Length - 1] = 0;

                //product_price_booking
                // 調整陣列的大小
                System.Array.Resize(ref product_price_booking, product_price_booking.Length + 1);
                // 指定新的陣列值
                product_price_booking[product_price_booking.Length - 1] = 0;

                //product_count_order
                // 調整陣列的大小
                System.Array.Resize(ref product_count_order, product_count_order.Length + 1);
                // 指定新的陣列值
                product_count_order[product_count_order.Length - 1] = 0;

                //restaurant
                // 調整陣列的大小
                System.Array.Resize(ref restaurant, restaurant.Length + 1);
                // 指定新的陣列值
                restaurant[restaurant.Length - 1] = str2[5];

                //date
                // 調整陣列的大小
                System.Array.Resize(ref date, date.Length + 1);
                // 指定新的陣列值
                date[date.Length - 1] = str2[6];

                for (int j = 0; j < product.Length; j++)
                {
                    //Booking
                    //訂單現有數量及金額 加入 新增數量及金額
                    if (str2[2] == product[j])
                    {
                        product_count_booking[j] += Int32.Parse(str2[3]);
                        product_price_booking[j] += Int32.Parse(str2[4]);
                    }

                    //Order
                    //姓名及品名相同，合併訂單
                    if ((str2[1] == name[j]) && (str2[2] == product[j]))
                    {
                        product_count_order[j] += Int32.Parse(str2[3]);
                    }
                }
            }

            //找出重複資料
            int[] remove_num_booking = [];
            int[] remove_num_order = [];
            for (int k = 0; k < product.Length; k++)
            {
                for (int m = k + 1; m < product.Length; m++)
                {
                    //booking
                    if (product[k] == product[m])
                    {
                        //Debug.WriteLine(product[k] + m);

                        // 調整陣列的大小
                        System.Array.Resize(ref remove_num_booking, remove_num_booking.Length + 1);
                        // 指定新的陣列值
                        remove_num_booking[remove_num_booking.Length - 1] = m;
                    }

                    //order
                    if ((name[k] == name[m]) && (product[k] == product[m]))
                    {
                        //Debug.WriteLine(product[k] + m);

                        // 調整陣列的大小
                        System.Array.Resize(ref remove_num_order, remove_num_order.Length + 1);
                        // 指定新的陣列值
                        remove_num_order[remove_num_order.Length - 1] = m;
                    }
                }
            }

            //booking
            //將重複資料的儲值變0
            for (int h = 0; h < remove_num_booking.Length; h++)
            {
                //將重複資料的資料變0
                product_count_booking[remove_num_booking[h]] = 0;
                product_price_booking[remove_num_booking[h]] = 0;
            }

            //order
            //將重複資料的儲值變0
            for (int h = 0; h < remove_num_order.Length; h++)
            {
                //將重複資料的資料變0
                product_count_order[remove_num_order[h]] = 0;
            }

            //listView_Booking寫入新資料
            for (int d = 0; d < product.Length; d++)
            {
                //日期為2024/04/17
                //listView日期
                string date_Data = Convert.ToString(date[d].Substring(0, 10));
                //dateTimePicker日期 Substring(a, b) 從第a個字元抓b個字元
                string date_Selected = Convert.ToString(dateTimePicker.Value).Substring(0, 10);
                
                //數量為0的資料不寫入listView
                if (product_count_booking[d] != 0)
                {
                    //篩選日期
                    if (date_Selected == date_Data)
                    {
                        //listView_Booking寫入新資料
                        ListViewItem c = new ListViewItem(new string[] { product[d], Convert.ToString(product_count_booking[d]), Convert.ToString(product_price_booking[d]), restaurant[d], date[d] });
                        listView_Booking.Items.Add(c);
                    }
                }

                //數量為0的資料不寫入listView
                if (product_count_order[d] != 0)
                {
                    //篩選日期
                    if (date_Selected == date_Data)
                    {
                        //listView_Order寫入新資料
                        ListViewItem b = new ListViewItem(new string[] { department[d], name[d], product[d], Convert.ToString(product_count_order[d]), restaurant[d], date[d] });
                        listView_Order.Items.Add(b);
                    }
                }

                //comboBox加入選項
                comboBox_Restaurant.Items.Add(restaurant[d]);
                comboBox_Department.Items.Add(department[d]);

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
                            comboBox_Restaurant.Text = "";
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
            r.Close();
        }

        //儲存listView_Booking函式
        public void Save_Booking()
        {
            string s = "";
            for (int m = 0; m < listView_Booking.Items.Count; m++)
            {
                for (int n = 0; n < listView_Booking.Items[m].SubItems.Count; n++)
                {
                    s += listView_Booking.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }


            string link;
            link = Application.StartupPath + "data\\Booking.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            //MessageBox.Show("儲存成功：" + link);
        }

        //儲存listView_Order函式
        public void Save_Order()
        {
            string s = "";
            for (int m = 0; m < listView_Order.Items.Count; m++)
            {
                for (int n = 0; n < listView_Order.Items[m].SubItems.Count; n++)
                {
                    s += listView_Order.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            string link;
            link = Application.StartupPath + "data\\Order.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            //MessageBox.Show("儲存成功：" + link);
        }

        //店家comboBox
        private void comboBox_Restaurant_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空數量label
            label_Count.Text = "";
            //清空總金額label
            label_Price_Total.Text = "";
            //清空總數量label
            label_Count_Total.Text = "";
            //部門comboBox顯示空值
            comboBox_Department.Text = "";
            this.listView_Order.Items.Clear();  //只移除資料列，保留標題列


            //Restaurant.txt
            //顯示店家名稱、電話、地址0
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
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                if (comboBox_Restaurant.Text == Convert.ToString(str2[0]))
                {
                    //顯示店家名稱、電話、地址
                    label_Restaurant.Text = Convert.ToString(str2[0]);
                    label_Phone.Text = Convert.ToString(str2[1]);
                    label_Address.Text = Convert.ToString(str2[2]);
                }
            }
            //close the file
            r.Close();


            //Booking.txt
            //讀取資料
            string link_Booking;
            link_Booking = Application.StartupPath + "data\\Booking.txt";
            StreamReader r_count_Booking = new StreamReader(link_Booking);
            string sepatator_Booking = ",";  //以逗號分割字串
            char[] cgap_Booking = sepatator_Booking.ToCharArray();
            //逐行讀取到底，計算資料行數量
            int count_Booking = 0;
            while (!r_count_Booking.EndOfStream)
            {
                r_count_Booking.ReadLine();  //讀取一列資料行
                count_Booking += 1;
            }
            //close the file
            r_count_Booking.Close();

            //開啟檔案
            StreamReader r_Booking = new StreamReader(link_Booking);
            int[] product_count_booking = [];
            int[] product_price_booking = [];
            this.listView_Booking.Items.Clear();  //只移除資料列，保留標題列
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Booking - 1; i++)
            {
                string str1 = r_Booking.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Booking, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //dateTimePicker日期 Substring(a, b) 從第a個字元抓b個字元
                string date_Selected = Convert.ToString(dateTimePicker.Value).Substring(0, 10);
                //listView日期
                string date_Data = Convert.ToString(str2[4][0]) + Convert.ToString(str2[4][1]) + Convert.ToString(str2[4][2]) + Convert.ToString(str2[4][3]) + Convert.ToString(str2[4][4]) + Convert.ToString(str2[4][5]) + Convert.ToString(str2[4][6]) + Convert.ToString(str2[4][7]) + Convert.ToString(str2[4][8]) + Convert.ToString(str2[4][9]);
                  
                //篩選店家
                if (comboBox_Restaurant.Text == Convert.ToString(str2[3]))
                {
                    //篩選日期
                    if (date_Selected == date_Data)
                    {
                        //listView寫入新資料
                        ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], str2[3], str2[4] });
                        listView_Booking.Items.Add(b);

                        //product_count_booking
                        // 調整陣列的大小
                        System.Array.Resize(ref product_count_booking, product_count_booking.Length + 1);
                        // 指定新的陣列值
                        product_count_booking[product_count_booking.Length - 1] = Int16.Parse(str2[1]);

                        //product_price_booking
                        // 調整陣列的大小
                        System.Array.Resize(ref product_price_booking, product_price_booking.Length + 1);
                        // 指定新的陣列值
                        product_price_booking[product_price_booking.Length - 1] = Int16.Parse(str2[2]);

                        //booking
                        //顯示訂單總金額
                        int Price_sum = 0;
                        for (int k = 0; k < product_price_booking.Length; k++)
                        {
                            Price_sum += product_price_booking[k];
                        }
                        //label_Price_Total.Text = Convert.ToString(Price_sum);
                        label_Price_Total.Text = Convert.ToString("$ " + string.Format("{0:0,0}", Price_sum));

                        //booking
                        //顯示訂單總數量
                        int Count_sum = 0;
                        for (int k = 0; k < product_count_booking.Length; k++)
                        {
                            Count_sum += product_count_booking[k];
                        }
                        label_Count_Total.Text = Convert.ToString(Count_sum);
                    }                
                }
            }
            //close the file
            r_Booking.Close();
        }

        //部門comboBox
        private void comboBox_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空數量label
            label_Count.Text = "";

            //Order.txt
            //讀取資料
            string link_Order;
            link_Order = Application.StartupPath + "data\\Order.txt";
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
            int[] product_count_order = [];
            this.listView_Order.Items.Clear();  //只移除資料列，保留標題列                                                                                                
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count_Order - 1; i++)
            {
                string str1 = r_Order.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap_Order, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                //dateTimePicker日期 Substring(a, b) 從第a個字元抓b個字元
                string date_Selected = Convert.ToString(dateTimePicker.Value).Substring(0, 10);
                //listView日期
                string date_Data = Convert.ToString(str2[5][0]) + Convert.ToString(str2[5][1]) + Convert.ToString(str2[5][2]) + Convert.ToString(str2[5][3]) + Convert.ToString(str2[5][4]) + Convert.ToString(str2[5][5]) + Convert.ToString(str2[5][6]) + Convert.ToString(str2[5][7]) + Convert.ToString(str2[5][8]) + Convert.ToString(str2[5][9]);
                
                //篩選店家
                if (comboBox_Restaurant.Text == Convert.ToString(str2[4]))
                {
                    //篩選部門
                    if (comboBox_Department.Text == Convert.ToString(str2[0]))
                    {
                        //篩選日期
                        if (date_Selected == date_Data)
                        {
                            //listView寫入新資料
                            ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], str2[3], str2[4], str2[5] });
                            listView_Order.Items.Add(b);

                            //listView_Booking
                            //department
                            // 調整陣列的大小
                            System.Array.Resize(ref product_count_order, product_count_order.Length + 1);
                            // 指定新的陣列值
                            product_count_order[product_count_order.Length - 1] = Int16.Parse(str2[3]);

                            //Order
                            //顯示訂單總數量
                            int Count_Total = 0;
                            for (int k = 0; k < product_count_order.Length; k++)
                            {
                                Count_Total += product_count_order[k];
                            }
                            label_Count.Text = Convert.ToString(Count_Total);
                        }
                    }
                }
            }
            //close the file
            r_Order.Close();
        }

        //日期dataTimePicker
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //更新listView函式
            Refresh();

            //儲存listView_Booking函式
            Save_Booking();

            //儲存listView_Order函式
            Save_Order();
        }

        //listView_Booking 排序Button
        private void listView_Booking_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_Booking.Sorting == SortOrder.Ascending)
            {
                listView_Booking.Sorting = SortOrder.Descending;
            }
            else
            {
                listView_Booking.Sorting = SortOrder.Ascending;
            }
            listView_Booking.Sort();
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

        //ComboBox樣式
        private void comboBox_Restaurant_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Restaurant.Cursor = Cursors.Hand;
        }

        private void comboBox_Department_Mouse_Move(object sender, MouseEventArgs e)
        {
            comboBox_Department.Cursor = Cursors.Hand;
        }

        //dataTimePicker樣式
        private void dateTimePicker_Mouse_Move(object sender, EventArgs e)
        {
            dateTimePicker.Cursor = Cursors.Hand;
        }
    }
}
