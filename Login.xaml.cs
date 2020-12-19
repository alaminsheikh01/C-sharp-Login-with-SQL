using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginDemo
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string userErr;
        string passErr;
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Uname.Text == "")
            {
                userErr = "UserName is required";
                UserError.Content = userErr;
            }
            else
            {
                UserError.Content = "";
            }
            if(Upass.Password == "")
            {
                passErr = "Password is required";
                PassError.Content = passErr;
            }
            else
            {
                PassError.Content = "";
            }
            if(Uname.Text!="" && Upass.Password != "")
            {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PM9NGM3\SQLEXPRESS;Initial Catalog=LoginDemoDatabase;Integrated Security=True");

            try
            {
                con.Open();
                string query = "Select count(*) from [User] where UserName = '" + Uname.Text + "' AND Password= '" + Upass.Password + "' ";

                SqlCommand sqlcmd = new SqlCommand(query, con);
                int a = Convert.ToInt32(sqlcmd.ExecuteScalar());

                if(a == 1)
                {
                        // MessageBox.Show("Valid Credentials");
                        string userName = Uname.Text.ToString();
                        UserView u = new UserView(userName);
                        u.Show();
                        this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            }

        }
    }
}
