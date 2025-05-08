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
    public partial class LogIn : Form
    {
        public delegate void LogInEventHandler(string Signal); //宣告委派
        public LogInEventHandler eventLogIntrigger; //傳遞資料event

        public LogIn()
        {
            InitializeComponent();
        }

        //確認button
        private void button_Confirm_Click(object sender, EventArgs e)
        {
            //若TextBox為空值，不執行
            if (string.IsNullOrEmpty(textBox_Access.Text) || string.IsNullOrEmpty(textBox_Password.Text))
            {
                MessageBox.Show("請輸入帳號或密碼");
                return;
            }

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
            bool Signal = true;
            //不讀取檔案最後兩行空行
            for (int i = 0; i < count - 1; i++)
            {
                string str1 = r.ReadLine();  //讀取一列資料行
                if (str1 == null) break;  //若資料為空值，則跳過
                string[] str2 = str1.Split(cgap, StringSplitOptions.RemoveEmptyEntries);  //基於數組字串，將字串拆分為多少個子字串  //StringSplitOptions.RemoveEmptyEntries忽略空行  //StringSplitOptions.None不忽略空行

                if ((textBox_Access.Text == str2[1]) && (textBox_Password.Text == str2[2]))
                {
                    textBox_Access.ResetText();
                    textBox_Password.ResetText();
                    MessageBox.Show("登入成功。");
                    Signal = false;
                    switch (str2[0])
                    {
                        case "User":
                            eventLogIntrigger("User"); //傳遞資料至Form1
                            break;

                        case "Manager":
                            eventLogIntrigger("Manager"); //傳遞資料至Form1
                            break;

                        case "System":
                            eventLogIntrigger("System"); //傳遞資料至Form1
                            break;
                        default:
                            Debug.WriteLine("沒有權限");
                            break;
                    }
                }
            }
            if (Signal)
            {
                MessageBox.Show("無法登入，請重新輸入帳號或密碼。");
            }
        }
        //清除button
        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_Access.ResetText();
            textBox_Password.ResetText();
        }

        //Button樣式
        private void button_Confirm_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Confirm.BackColor = Color.CadetBlue;
            button_Confirm.ForeColor = Color.White;
            button_Confirm.Cursor = Cursors.Hand;
        }

        private void button_Confirm_Mouse_Leave(object sender, EventArgs e)
        {
            button_Confirm.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Confirm.ForeColor = Color.FromArgb(0, 0, 0, 0);
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
    }
}
