using Microsoft.VisualBasic.Logging;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace WinFormsApp_Meal_expense_accounting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //預設開啟登入頁面
            openLogIn(new LogIn()); //呼叫開啟子表單方法，建立新表單物件

            //登出函式
            LogOut();
        }

        //登出函式
        public void LogOut()
        {
            button_LogIn.Text = "LogIn";

            if (true)
            {
                //預設無法開啟頁面
                button_Order.Enabled = false;
                button_Booking.Enabled = false;
                button_Restaurant.Enabled = false;
                button_Details.Enabled = false;
                button_Deposit.Enabled = false;
                button_People.Enabled = false;
                button_Information.Enabled = false;
                button_Setting.Enabled = false;

                //預設隱藏頁面
                button_Restaurant.Visible = false;
                button_Details.Visible = false;
                button_Deposit.Visible = false;
                button_People.Visible = false;
                button_Information.Visible = false;
                button_Setting.Visible = false;
            }

        }

        //LogIn頁面
        //子表單切換
        private LogIn activeForm = null;  //確認表單是否開啟
        private void openLogIn(LogIn Login)
        {
            if (activeForm != null)
                activeForm.Close();
            //activeForm = LogIn;  //若表單未開啟，將表單名指定給變數
            Login.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Login.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Login.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Login);  //將表單加入panel顯示
            panel_ChildForm.Tag = Login;  //將標籤設為子表單名
            Login.BringToFront();  //將表單放置到前面
            Login.eventLogIntrigger = new LogIn.LogInEventHandler(triggered); // 宣告承接event委派事件
            Login.Show();  //開啟表單
        }

        //權限函式
        public void triggered(string Signal) //承接委派的function
        {
            button_LogIn.Text = "LogOut";
            //登入後預設開啟頁面
            openOrder(new Order()); //呼叫開啟子表單方法，建立新表單物件

            switch (Signal)
            {
                case "User":
                    //開放頁面
                    button_Order.Enabled = true;
                    button_Booking.Enabled = true;
                    break;

                case "Manager":
                    //開放頁面
                    button_Order.Enabled = true;
                    button_Booking.Enabled = true;
                    button_Restaurant.Enabled = true;
                    button_Details.Enabled = true;
                    button_Deposit.Enabled = true;
                    button_Information.Enabled = true;
                    //顯示頁面
                    button_Restaurant.Visible = true;
                    button_Details.Visible = true;
                    button_Deposit.Visible = true;
                    button_Information.Visible = true;
                    break;

                case "System":
                    //開放頁面
                    button_Order.Enabled = true;
                    button_Booking.Enabled = true;
                    button_Restaurant.Enabled = true;
                    button_Details.Enabled = true;
                    button_Deposit.Enabled = true;
                    button_People.Enabled = true;
                    button_Information.Enabled = true;
                    button_Setting.Enabled = true;
                    //顯示頁面
                    button_Restaurant.Visible = true;
                    button_Details.Visible = true;
                    button_Deposit.Visible = true;
                    button_People.Visible = true;
                    button_Information.Visible = true;
                    button_Setting.Visible = true;
                    break;
                default:
                    Debug.WriteLine("沒有權限");
                    break;
            }
        }

        //LogIn頁面 切換Button
        private void button_LogIn_Click(object sender, EventArgs e)
        {
            switch (button_LogIn.Text)
            {
                case "LogOut":
                    //登出函式
                    LogOut();
                    //預設開啟登入頁面
                    openLogIn(new LogIn()); //呼叫開啟子表單方法，建立新表單物件
                    MessageBox.Show("已登出。");
                    break;

                case "LogIn":
                    openLogIn(new LogIn()); //呼叫開啟子表單方法，建立新表單物件
                    break;

                default:
                    Debug.WriteLine("沒有權限");
                    break;
            }
        }

        //Deposit頁面
        //子表單切換
        private void openDeposit(Form Deposit)
        {
            Deposit.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Deposit.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Deposit.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Deposit);  //將表單加入panel顯示
            panel_ChildForm.Tag = Deposit;  //將標籤設為子表單名
            Deposit.BringToFront();  //將表單放置到前面
            Deposit.Show();  //開啟表單
        }

        //Deposit頁面 切換Button
        private void button_Deposit_Click(object sender, EventArgs e)
        {
            openDeposit(new Deposit()); //呼叫開啟子表單方法，建立新表單物件
        }

        //Details頁面
        //子表單切換
        private void openDetails(Form Details)
        {
            Details.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Details.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Details.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Details);  //將表單加入panel顯示
            panel_ChildForm.Tag = Details;  //將標籤設為子表單名
            Details.BringToFront();  //將表單放置到前面
            Details.Show();  //開啟表單
        }
        //Details頁面 切換Button
        private void button_Details_Click(object sender, EventArgs e)
        {
            openDetails(new Details()); //呼叫開啟子表單方法，建立新表單物件
        }

        //Order頁面
        //子表單切換
        private void openOrder(Form Order)
        {
            Order.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Order.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Order.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Order);  //將表單加入panel顯示
            panel_ChildForm.Tag = Order;  //將標籤設為子表單名
            Order.BringToFront();  //將表單放置到前面
            Order.Show();  //開啟表單
        }

        //Order頁面 切換Button
        private void button_Order_Click(object sender, EventArgs e)
        {
            openOrder(new Order()); //呼叫開啟子表單方法，建立新表單物件
        }

        //Restaurant頁面
        //子表單切換
        private void openRestaurant(Form Restaurant)
        {
            Restaurant.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Restaurant.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Restaurant.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Restaurant);  //將表單加入panel顯示
            panel_ChildForm.Tag = Restaurant;  //將標籤設為子表單名
            Restaurant.BringToFront();  //將表單放置到前面
            Restaurant.Show();  //開啟表單
        }

        //Restaurant頁面 切換Button
        private void button_Restaurant_Click(object sender, EventArgs e)
        {
            openRestaurant(new Restaurant()); //呼叫開啟子表單方法，建立新表單物件
        }

        //People頁面
        //子表單切換
        private void openPeople(Form People)
        {
            People.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            People.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            People.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(People);  //將表單加入panel顯示
            panel_ChildForm.Tag = People;  //將標籤設為子表單名
            People.BringToFront();  //將表單放置到前面
            People.Show();  //開啟表單
        }
        //People頁面 切換Button
        private void button_People_Click(object sender, EventArgs e)
        {
            openPeople(new People()); //呼叫開啟子表單方法，建立新表單物件
        }

        //Booking頁面
        //子表單切換
        private void openBooking(Form Booking)
        {
            Booking.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Booking.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Booking.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Booking);  //將表單加入panel顯示
            panel_ChildForm.Tag = Booking;  //將標籤設為子表單名
            Booking.BringToFront();  //將表單放置到前面
            Booking.Show();  //開啟表單
        }

        //Booking頁面 切換Button
        private void button_Booking_Click(object sender, EventArgs e)
        {
            openBooking(new Booking()); //呼叫開啟子表單方法，建立新表單物件
        }

        //Information頁面
        //子表單切換
        private void openInformation(Form Information)
        {
            Information.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Information.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Information.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Information);  //將表單加入panel顯示
            panel_ChildForm.Tag = Information;  //將標籤設為子表單名
            Information.BringToFront();  //將表單放置到前面
            Information.Show();  //開啟表單
        }

        //Information頁面 切換Button
        private void button_Information_Click(object sender, EventArgs e)
        {
            openInformation(new Information()); //呼叫開啟子表單方法，建立新表單物件
        }

        //Setting頁面
        //子表單切換
        private void openSetting(Form Setting)
        {
            Setting.TopLevel = false;  //最上層視窗通常用來作為應用程式中的主要表單，故子表單不能設為最上層表單
            Setting.FormBorderStyle = FormBorderStyle.None;  //設定子表單邊框為無
            Setting.Dock = DockStyle.Fill;  //設定子表單位置
            panel_ChildForm.Controls.Add(Setting);  //將表單加入panel顯示
            panel_ChildForm.Tag = Setting;  //將標籤設為子表單名
            Setting.BringToFront();  //將表單放置到前面
            Setting.Show();  //開啟表單
        }

        //Setting頁面 切換Button
        private void button_Setting_Click(object sender, EventArgs e)
        {
            openSetting(new Setting()); //呼叫開啟子表單方法，建立新表單物件
        }

        //Button樣式
        private void button_LogIn_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_LogIn.BackColor = Color.SandyBrown;
            button_LogIn.ForeColor = Color.White;
            button_LogIn.Cursor = Cursors.Hand;
        }

        private void button_LogIn_Mouse_Leave(object sender, EventArgs e)
        {
            button_LogIn.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_LogIn.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Order_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Order.BackColor = Color.SandyBrown;
            button_Order.ForeColor = Color.White;
            button_Order.Cursor = Cursors.Hand;
        }

        private void button_Order_Mouse_Leave(object sender, EventArgs e)
        {
            button_Order.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Order.ForeColor = Color.FromArgb(0, 0, 0, 0);
            button_LogIn.FlatAppearance.BorderColor = Color.FromArgb(0, 100, 100, 100);
        }

        private void button_Booking_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Booking.BackColor = Color.SandyBrown;
            button_Booking.ForeColor = Color.White;
            button_Booking.Cursor = Cursors.Hand;
        }

        private void button_Booking_Mouse_Leave(object sender, EventArgs e)
        {
            button_Booking.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Booking.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Restaurant_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Restaurant.BackColor = Color.SandyBrown;
            button_Restaurant.ForeColor = Color.White;
            button_Restaurant.Cursor = Cursors.Hand;
        }

        private void button_Restaurant_Mouse_Leave(object sender, EventArgs e)
        {
            button_Restaurant.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Restaurant.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Details_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Details.BackColor = Color.SandyBrown;
            button_Details.ForeColor = Color.White;
            button_Details.Cursor = Cursors.Hand;
        }

        private void button_Details_Mouse_Leave(object sender, EventArgs e)
        {
            button_Details.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Details.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Deposit_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Deposit.BackColor = Color.SandyBrown;
            button_Deposit.ForeColor = Color.White;
            button_Deposit.Cursor = Cursors.Hand;
        }

        private void button_Deposit_Mouse_Leave(object sender, EventArgs e)
        {
            button_Deposit.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Deposit.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_People_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_People.BackColor = Color.SandyBrown;
            button_People.ForeColor = Color.White;
            button_People.Cursor = Cursors.Hand;
        }

        private void button_People_Mouse_Leave(object sender, EventArgs e)
        {
            button_People.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_People.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Information_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Information.BackColor = Color.SandyBrown;
            button_Information.ForeColor = Color.White;
            button_Information.Cursor = Cursors.Hand;
        }

        private void button_Information_Mouse_Leave(object sender, EventArgs e)
        {
            button_Information.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Information.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void button_Setting_Mouse_Move(object sender, MouseEventArgs e)
        {
            button_Setting.BackColor = Color.SandyBrown;
            button_Setting.ForeColor = Color.White;
            button_Setting.Cursor = Cursors.Hand;
        }

        private void button_Setting_Mouse_Leave(object sender, EventArgs e)
        {
            button_Setting.BackColor = Color.FromArgb(0, 150, 150, 150);
            button_Setting.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
