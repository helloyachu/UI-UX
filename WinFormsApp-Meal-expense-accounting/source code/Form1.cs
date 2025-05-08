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

            //�w�]�}�ҵn�J����
            openLogIn(new LogIn()); //�I�s�}�Ҥl����k�A�إ߷s��檫��

            //�n�X�禡
            LogOut();
        }

        //�n�X�禡
        public void LogOut()
        {
            button_LogIn.Text = "LogIn";

            if (true)
            {
                //�w�]�L�k�}�ҭ���
                button_Order.Enabled = false;
                button_Booking.Enabled = false;
                button_Restaurant.Enabled = false;
                button_Details.Enabled = false;
                button_Deposit.Enabled = false;
                button_People.Enabled = false;
                button_Information.Enabled = false;
                button_Setting.Enabled = false;

                //�w�]���í���
                button_Restaurant.Visible = false;
                button_Details.Visible = false;
                button_Deposit.Visible = false;
                button_People.Visible = false;
                button_Information.Visible = false;
                button_Setting.Visible = false;
            }

        }

        //LogIn����
        //�l������
        private LogIn activeForm = null;  //�T�{���O�_�}��
        private void openLogIn(LogIn Login)
        {
            if (activeForm != null)
                activeForm.Close();
            //activeForm = LogIn;  //�Y��楼�}�ҡA�N���W���w���ܼ�
            Login.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Login.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Login.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Login);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Login;  //�N���ҳ]���l���W
            Login.BringToFront();  //�N����m��e��
            Login.eventLogIntrigger = new LogIn.LogInEventHandler(triggered); // �ŧi�ӱ�event�e���ƥ�
            Login.Show();  //�}�Ҫ��
        }

        //�v���禡
        public void triggered(string Signal) //�ӱ��e����function
        {
            button_LogIn.Text = "LogOut";
            //�n�J��w�]�}�ҭ���
            openOrder(new Order()); //�I�s�}�Ҥl����k�A�إ߷s��檫��

            switch (Signal)
            {
                case "User":
                    //�}�񭶭�
                    button_Order.Enabled = true;
                    button_Booking.Enabled = true;
                    break;

                case "Manager":
                    //�}�񭶭�
                    button_Order.Enabled = true;
                    button_Booking.Enabled = true;
                    button_Restaurant.Enabled = true;
                    button_Details.Enabled = true;
                    button_Deposit.Enabled = true;
                    button_Information.Enabled = true;
                    //��ܭ���
                    button_Restaurant.Visible = true;
                    button_Details.Visible = true;
                    button_Deposit.Visible = true;
                    button_Information.Visible = true;
                    break;

                case "System":
                    //�}�񭶭�
                    button_Order.Enabled = true;
                    button_Booking.Enabled = true;
                    button_Restaurant.Enabled = true;
                    button_Details.Enabled = true;
                    button_Deposit.Enabled = true;
                    button_People.Enabled = true;
                    button_Information.Enabled = true;
                    button_Setting.Enabled = true;
                    //��ܭ���
                    button_Restaurant.Visible = true;
                    button_Details.Visible = true;
                    button_Deposit.Visible = true;
                    button_People.Visible = true;
                    button_Information.Visible = true;
                    button_Setting.Visible = true;
                    break;
                default:
                    Debug.WriteLine("�S���v��");
                    break;
            }
        }

        //LogIn���� ����Button
        private void button_LogIn_Click(object sender, EventArgs e)
        {
            switch (button_LogIn.Text)
            {
                case "LogOut":
                    //�n�X�禡
                    LogOut();
                    //�w�]�}�ҵn�J����
                    openLogIn(new LogIn()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
                    MessageBox.Show("�w�n�X�C");
                    break;

                case "LogIn":
                    openLogIn(new LogIn()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
                    break;

                default:
                    Debug.WriteLine("�S���v��");
                    break;
            }
        }

        //Deposit����
        //�l������
        private void openDeposit(Form Deposit)
        {
            Deposit.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Deposit.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Deposit.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Deposit);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Deposit;  //�N���ҳ]���l���W
            Deposit.BringToFront();  //�N����m��e��
            Deposit.Show();  //�}�Ҫ��
        }

        //Deposit���� ����Button
        private void button_Deposit_Click(object sender, EventArgs e)
        {
            openDeposit(new Deposit()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //Details����
        //�l������
        private void openDetails(Form Details)
        {
            Details.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Details.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Details.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Details);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Details;  //�N���ҳ]���l���W
            Details.BringToFront();  //�N����m��e��
            Details.Show();  //�}�Ҫ��
        }
        //Details���� ����Button
        private void button_Details_Click(object sender, EventArgs e)
        {
            openDetails(new Details()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //Order����
        //�l������
        private void openOrder(Form Order)
        {
            Order.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Order.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Order.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Order);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Order;  //�N���ҳ]���l���W
            Order.BringToFront();  //�N����m��e��
            Order.Show();  //�}�Ҫ��
        }

        //Order���� ����Button
        private void button_Order_Click(object sender, EventArgs e)
        {
            openOrder(new Order()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //Restaurant����
        //�l������
        private void openRestaurant(Form Restaurant)
        {
            Restaurant.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Restaurant.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Restaurant.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Restaurant);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Restaurant;  //�N���ҳ]���l���W
            Restaurant.BringToFront();  //�N����m��e��
            Restaurant.Show();  //�}�Ҫ��
        }

        //Restaurant���� ����Button
        private void button_Restaurant_Click(object sender, EventArgs e)
        {
            openRestaurant(new Restaurant()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //People����
        //�l������
        private void openPeople(Form People)
        {
            People.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            People.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            People.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(People);  //�N���[�Jpanel���
            panel_ChildForm.Tag = People;  //�N���ҳ]���l���W
            People.BringToFront();  //�N����m��e��
            People.Show();  //�}�Ҫ��
        }
        //People���� ����Button
        private void button_People_Click(object sender, EventArgs e)
        {
            openPeople(new People()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //Booking����
        //�l������
        private void openBooking(Form Booking)
        {
            Booking.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Booking.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Booking.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Booking);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Booking;  //�N���ҳ]���l���W
            Booking.BringToFront();  //�N����m��e��
            Booking.Show();  //�}�Ҫ��
        }

        //Booking���� ����Button
        private void button_Booking_Click(object sender, EventArgs e)
        {
            openBooking(new Booking()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //Information����
        //�l������
        private void openInformation(Form Information)
        {
            Information.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Information.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Information.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Information);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Information;  //�N���ҳ]���l���W
            Information.BringToFront();  //�N����m��e��
            Information.Show();  //�}�Ҫ��
        }

        //Information���� ����Button
        private void button_Information_Click(object sender, EventArgs e)
        {
            openInformation(new Information()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //Setting����
        //�l������
        private void openSetting(Form Setting)
        {
            Setting.TopLevel = false;  //�̤W�h�����q�`�Ψӧ@�����ε{�������D�n���A�G�l��椣��]���̤W�h���
            Setting.FormBorderStyle = FormBorderStyle.None;  //�]�w�l�����ج��L
            Setting.Dock = DockStyle.Fill;  //�]�w�l����m
            panel_ChildForm.Controls.Add(Setting);  //�N���[�Jpanel���
            panel_ChildForm.Tag = Setting;  //�N���ҳ]���l���W
            Setting.BringToFront();  //�N����m��e��
            Setting.Show();  //�}�Ҫ��
        }

        //Setting���� ����Button
        private void button_Setting_Click(object sender, EventArgs e)
        {
            openSetting(new Setting()); //�I�s�}�Ҥl����k�A�إ߷s��檫��
        }

        //Button�˦�
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
