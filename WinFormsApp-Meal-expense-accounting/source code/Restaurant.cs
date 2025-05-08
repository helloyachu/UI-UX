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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class Restaurant : Form
    {
        public Restaurant()
        {
            InitializeComponent();

            //清除TextBox內容
            textBox_Shop.Clear(); ;
            textBox_Phone.Clear(); ;
            textBox_Address.Clear(); ;
            label_link.ResetText(); ;
            textBox_Product_1.Clear();
            textBox_Product_2.Clear();
            textBox_Product_3.Clear();
            textBox_Product_4.Clear();
            textBox_Product_5.Clear();
            textBox_Product_6.Clear();
            textBox_Product_7.Clear();
            textBox_Product_8.Clear();
            textBox_Product_9.Clear();
            textBox_Product_10.Clear();
            textBox_Price_1.Clear();
            textBox_Price_2.Clear();
            textBox_Price_3.Clear();
            textBox_Price_4.Clear();
            textBox_Price_5.Clear();
            textBox_Price_6.Clear();
            textBox_Price_7.Clear();
            textBox_Price_8.Clear();
            textBox_Price_9.Clear();
            textBox_Price_10.Clear();
            //指定游標輸入框
            textBox_Shop.Focus();

            //listView顯示所有資料函式
            Refresh();
        }

        //listView顯示所有資料函式
        public void Refresh()
        {
            //讀取資料
            this.listView_Menu.Items.Clear();  //只移除資料列，保留標題列           
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
                //listView寫入新資料
                ListViewItem b = new ListViewItem(new string[] { str2[0], str2[1], str2[2], str2[3], str2[4], str2[5] });
                listView_Menu.Items.Add(b);
            }
            //close the file
            r.Close();
        }

        //儲存Button
        private void button_Save_Click(object sender, EventArgs e)
        {
            //若listView沒有資料列，顯示提示框。
            if (listView_Menu.Items.Count == 0)
            {
                MessageBox.Show("請還原原始資料");
                return;
            }

            string s = "";
            for (int m = 0; m < listView_Menu.Items.Count; m++)
            {
                for (int n = 0; n < listView_Menu.Items[m].SubItems.Count; n++)
                {
                    s += listView_Menu.Items[m].SubItems[n].Text + ",";
                }
                s += "\r\n";
            }

            string link;
            link = Application.StartupPath + "data\\Restaurant.txt";
            using (StreamWriter sw = new StreamWriter(link))
            {
                sw.WriteLine(s);  //將listView資料寫入檔案
            }
            MessageBox.Show("儲存成功：" + link);
        }

        //全部加入Button
        private void button_Add_All_Click(object sender, EventArgs e)
        {
            //若TextBox為空值，不執行
            if (string.IsNullOrEmpty(textBox_Shop.Text) || string.IsNullOrEmpty(textBox_Phone.Text) || string.IsNullOrEmpty(textBox_Address.Text) || (string.IsNullOrEmpty(textBox_Product_1.Text) || string.IsNullOrEmpty(textBox_Price_1.Text)) && (string.IsNullOrEmpty(textBox_Product_2.Text) || string.IsNullOrEmpty(textBox_Price_2.Text)) && (string.IsNullOrEmpty(textBox_Product_3.Text) || string.IsNullOrEmpty(textBox_Price_3.Text)) && (string.IsNullOrEmpty(textBox_Product_4.Text) || string.IsNullOrEmpty(textBox_Price_4.Text)) && (string.IsNullOrEmpty(textBox_Product_5.Text) || string.IsNullOrEmpty(textBox_Price_5.Text)) && (string.IsNullOrEmpty(textBox_Product_6.Text) || string.IsNullOrEmpty(textBox_Price_6.Text)) && (string.IsNullOrEmpty(textBox_Product_7.Text) || string.IsNullOrEmpty(textBox_Price_7.Text)) && (string.IsNullOrEmpty(textBox_Product_8.Text) || string.IsNullOrEmpty(textBox_Price_8.Text)) && (string.IsNullOrEmpty(textBox_Product_9.Text) || string.IsNullOrEmpty(textBox_Price_9.Text)) && (string.IsNullOrEmpty(textBox_Product_10.Text) || string.IsNullOrEmpty(textBox_Price_10.Text)))
            {
                MessageBox.Show("請輸入內容");
                return;
            }

            string[] Shop = [];
            string[] Phone = [];
            string[] Address = [];
            string[] Product = [textBox_Product_1.Text, textBox_Product_2.Text, textBox_Product_3.Text, textBox_Product_4.Text, textBox_Product_5.Text, textBox_Product_6.Text, textBox_Product_7.Text, textBox_Product_8.Text, textBox_Product_9.Text, textBox_Product_10.Text];
            string[] Price = [textBox_Price_1.Text, textBox_Price_2.Text, textBox_Price_3.Text, textBox_Price_4.Text, textBox_Price_5.Text, textBox_Price_6.Text, textBox_Price_7.Text, textBox_Price_8.Text, textBox_Price_9.Text, textBox_Price_10.Text];
            string[] link = [];

            for (int i = 0; i < Product.Length; i++)
            {
                //Shop
                // 調整陣列的大小
                System.Array.Resize(ref Shop, Shop.Length + 1);
                // 指定新的陣列值
                Shop[Shop.Length - 1] = textBox_Shop.Text;

                //Phone
                // 調整陣列的大小
                System.Array.Resize(ref Phone, Phone.Length + 1);
                // 指定新的陣列值
                Phone[Phone.Length - 1] = textBox_Phone.Text;

                //Address
                // 調整陣列的大小
                System.Array.Resize(ref Address, Address.Length + 1);
                // 指定新的陣列值
                Address[Address.Length - 1] = textBox_Address.Text;

                //link
                // 調整陣列的大小
                System.Array.Resize(ref link, link.Length + 1);
                // 指定新的陣列值
                link[link.Length - 1] = label_link.Text;
            }

            //listView_Memu寫入新資料
            for (int i = 0; i < Product.Length; i++)
            {
                //無輸入的產品資料不寫入listView
                if (Product[i] != "")
                {
                    //Trim()轉為純文字
                    ListViewItem c = new ListViewItem(new string[] { Shop[i].Trim(), Phone[i].Trim(), Address[i].Trim(), Product[i].Trim(), Price[i].Trim(), link[i].Trim() });
                    listView_Menu.Items.Add(c);
                }
            }

            //清除TextBox內容
            textBox_Product_1.Clear();
            textBox_Product_2.Clear();
            textBox_Product_3.Clear();
            textBox_Product_4.Clear();
            textBox_Product_5.Clear();
            textBox_Product_6.Clear();
            textBox_Product_7.Clear();
            textBox_Product_8.Clear();
            textBox_Product_9.Clear();
            textBox_Product_10.Clear();
            textBox_Price_1.Clear();
            textBox_Price_2.Clear();
            textBox_Price_3.Clear();
            textBox_Price_4.Clear();
            textBox_Price_5.Clear();
            textBox_Price_6.Clear();
            textBox_Price_7.Clear();
            textBox_Price_8.Clear();
            textBox_Price_9.Clear();
            textBox_Price_10.Clear();
        }

        //全部清除Button
        private void button_Clear_All_Click(object sender, EventArgs e)
        {
            textBox_Product_1.Clear();
            textBox_Product_2.Clear();
            textBox_Product_3.Clear();
            textBox_Product_4.Clear();
            textBox_Product_5.Clear();
            textBox_Product_6.Clear();
            textBox_Product_7.Clear();
            textBox_Product_8.Clear();
            textBox_Product_9.Clear();
            textBox_Product_10.Clear();
            textBox_Price_1.Clear();
            textBox_Price_2.Clear();
            textBox_Price_3.Clear();
            textBox_Price_4.Clear();
            textBox_Price_5.Clear();
            textBox_Price_6.Clear();
            textBox_Price_7.Clear();
            textBox_Price_8.Clear();
            textBox_Price_9.Clear();
            textBox_Price_10.Clear();
        }

        //移除列Button
        private void button_Remove_Click(object sender, EventArgs e)
        {
            //若沒有資料列，顯示提示框。
            if (listView_Menu.Items.Count > 0)
            {
                //若有資料列，但未選擇資料列，顯示提示框。
                if (listView_Menu.SelectedItems.Count > 0)
                {
                    listView_Menu.Items.Remove(listView_Menu.SelectedItems[0]);
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

        //還原Button
        private void button_Refresh_Click(object sender, EventArgs e)
        {
            //listView顯示所有資料函式
            Refresh();
        }

        //上傳菜單圖片Button
        private void button_Upload_Pic_Click(object sender, EventArgs e)
        {
            OpenFileDialog upload_pictureBox = new OpenFileDialog();
            string link;
            link = Application.StartupPath + "data";
            upload_pictureBox.InitialDirectory = link;  //顯示初始目錄
            upload_pictureBox.Filter = "txt files(*.txt)|*.txt|All files(*.*)|*.*";  //文件名稱篩選字串
            upload_pictureBox.FilterIndex = 2;
            upload_pictureBox.RestoreDirectory = true;
            //使用者在對話框點選確定，則結果為DialogResult.OK
            //否則結果為DialogResult.Cancel

            if (upload_pictureBox.ShowDialog() == DialogResult.OK)  //開啟檔案
            {
                MessageBox.Show("上傳成功：" + upload_pictureBox.FileName);  //顯示選擇的檔案名稱

                //上傳菜單圖片
                //pictureBox_Menu.Load(upload_pictureBox.FileName);
                pictureBox_Menu.Image = Image.FromFile(upload_pictureBox.FileName);

                //label顯示圖片路徑
                //文字過長以...顯示
                label_link.Text = upload_pictureBox.FileName.Substring(0, 30) + "...";
            }
        }

        //下載菜單圖片Button
        private void button_Download_Pic_Click(object sender, EventArgs e)
        {
            SaveFileDialog upload_pictureBox = new SaveFileDialog();
            string link;
            link = Application.StartupPath + "data";
            upload_pictureBox.InitialDirectory = link;  //顯示初始目錄
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

        //清除菜單圖片Button
        private void button_Clear_Pic_Click(object sender, EventArgs e)
        {
            //清除菜單圖片
            pictureBox_Menu.Image = null;
            //清除label顯示路徑
            label_link.ResetText();
        }

        //Button樣式
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

        private void button_Upload_Pic_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Upload_Pic.BackColor = Color.CadetBlue;
            button_Upload_Pic.ForeColor = Color.White;
            button_Upload_Pic.Cursor = Cursors.Hand;
        }

        private void button_Upload_Pic_Mouse_Leave(object sender, EventArgs e)
        {
            button_Upload_Pic.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Upload_Pic.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Add_All_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Add_All.BackColor = Color.CadetBlue;
            button_Add_All.ForeColor = Color.White;
            button_Add_All.Cursor = Cursors.Hand;
        }

        private void button_Add_All_Mouse_Leave(object sender, EventArgs e)
        {
            button_Add_All.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Add_All.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Clear_All_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Clear_All.BackColor = Color.CadetBlue;
            button_Clear_All.ForeColor = Color.White;
            button_Clear_All.Cursor = Cursors.Hand;
        }

        private void button_Clear_All_Mouse_Leave(object sender, EventArgs e)
        {
            button_Clear_All.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Clear_All.ForeColor = Color.FromArgb(0, 0, 0, 0);
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

        private void button_Download_Pic_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Download_Pic.BackColor = Color.CadetBlue;
            button_Download_Pic.ForeColor = Color.White;
            button_Download_Pic.Cursor = Cursors.Hand;
        }

        private void button_Download_Pic_Mouse_Leave(object sender, EventArgs e)
        {
            button_Download_Pic.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Download_Pic.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Clear_Pic_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Clear_Pic.BackColor = Color.CadetBlue;
            button_Clear_Pic.ForeColor = Color.White;
            button_Clear_Pic.Cursor = Cursors.Hand;
        }

        private void button_Clear_Pic_Mouse_Leave(object sender, EventArgs e)
        {
            button_Clear_Pic.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Clear_Pic.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
