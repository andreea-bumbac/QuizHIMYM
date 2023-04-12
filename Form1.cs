using accesa.src.domain;
using accesa.src.repo;
using accesa.src.service;
using log4net.Config;
using System.Configuration;

namespace accesa
{
    public partial class Form1 : Form
    {

        private IDictionary<string, string> props;
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            XmlConfigurator.Configure(new FileInfo("app.config"));
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            Console.WriteLine("Configuration Settings for DB {0}", GetConnectionStringByName("quiz"));
            props = new SortedList<string, string?>();
            props.Add("ConnectionString", GetConnectionStringByName("quiz"));
        }

        private static string? GetConnectionStringByName(string name)
        {
            string? result = null;
            var stringSettings = ConfigurationManager.ConnectionStrings[name];
            if (stringSettings != null)
            {
                result = stringSettings.ConnectionString;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = this.textBox1.Text;
            String password = this.textBox2.Text;

            if (textBox1.Text == "" & textBox2.Text == "")
            {
                label4.Text = "Wrong username and password!";
            }
            else if (textBox1.Text == "")
            {
                label4.Text = "Wrong username!";

            }
            else if (textBox2.Text == "")
            {
                label4.Text = "Wrong password!";

            }
            else
            {
                label4.Text = "Good!";
                UserDbRepo userDbRepo = new UserDbRepo(props);
                ServiceUser serviceUser = new ServiceUser(userDbRepo);
                User crtuser = new User("", "", 0, 0);
                foreach (var usr in serviceUser.FindAll())
                {
                    if (usr.username.Equals(textBox1.Text) & usr.parola.Equals(textBox2.Text))
                    {
                        crtuser = usr;
                    }
                }
                
                var mainForm = new Form2(props, crtuser);
                this.Hide();
                mainForm.Show();
            }
        }
    }
}