using accesa.src.domain;
using accesa.src.repo;
using accesa.src.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace accesa
{
    public partial class Form2 : Form
    {
        IDictionary<string, string> props;
        private IUserService userService;
        private IQuestService questService;
        private User currentUser;

        public Form2(IDictionary<string, string> props, User currentUser)
        {
            InitializeComponent();
            this.props = props;
            this.currentUser = currentUser;
            IUserRepo userRepo = new UserDbRepo(props);
            IQuestRepo questRepo = new QuestDbRepo(props);

            this.userService = new ServiceUser(userRepo);
            this.questService = new QuestService(questRepo);
            IEnumerable<Quest> quests = questService.FindAll();

            label2.Text = currentUser.username.ToString();
            label3.Text = currentUser.tokens.ToString();
            label7.Text = currentUser.intr.ToString();
            label1.Text = quests.ElementAt(currentUser.intr).question.ToString();
            button1.Text = quests.ElementAt(currentUser.intr).firstAnswer.ToString();
            button2.Text = quests.ElementAt(currentUser.intr).secondAnswer.ToString();
            button3.Text = quests.ElementAt(currentUser.intr).thirdAnswer.ToString();
            if (currentUser.tokens >= 0)
            {
                pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox8.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\lily.jpg");

            }

            if (currentUser.tokens >= 0)
            {
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\marshal.jpg");
            }
            if (currentUser.tokens >= 0)
            {
                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox5.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\barney.jpg");
            }
            if (currentUser.tokens >= 12)
            {
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox4.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\robin.jpg");
            }
            if (currentUser.tokens >= 15)
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\ted.jpg");
            }


            rankView();
        }

        private void rankView()
        {
            listView1.View = View.List;
            listView1.Items.Clear();
            IEnumerable<User> users = userService.FindAll();

            if (users.Count() == 0)
            {
                Console.WriteLine("is null");
            }
            else
            {
                for (int i = 0; i < users.Count() - 1; i++)
                {
                    for (int j = 0; j < users.Count() - i - 1; j++)
                    {
                        if (users.ElementAt(j).tokens > users.ElementAt(j + 1).tokens)
                        {
                            User newuser = new User("", "", 0, 0);
                            newuser = users.ElementAt(j);
                            users.ElementAt(j).Id = users.ElementAt(j + 1).Id;
                            users.ElementAt(j).username = users.ElementAt(j + 1).username;
                            users.ElementAt(j).parola = users.ElementAt(j + 1).parola;
                            users.ElementAt(j).tokens = users.ElementAt(j + 1).tokens;
                            users.ElementAt(j).intr = users.ElementAt(j + 1).intr;
                            users.ElementAt(j + 1).Id = newuser.Id;
                            users.ElementAt(j + 1).username = newuser.username;
                            users.ElementAt(j + 1).parola = newuser.parola;
                            users.ElementAt(j + 1).tokens = newuser.tokens;
                            users.ElementAt(j + 1).intr = newuser.intr;
                        }
                    }
                }
            }



            foreach (User user in users)
            {
                listView1.Items.Add("[" + user.tokens + "]    ->    " + user.username);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            IEnumerable<Quest> quests = questService.FindAll();
            Quest qu = new Quest("", "", "", "", "");
            foreach (Quest quest in quests)
            {
                if (quest.question.ToString() == label1.Text)
                {

                    qu = quest;
                }
            }

            if (button1.Text == qu.correctAnswer.ToString())
            {
                button1.BackColor = Color.Green;
                
                int nr_tokens = Convert.ToInt32(label3.Text);
                nr_tokens += 1;
                label3.Text = nr_tokens.ToString();

                int nr_intr = Convert.ToInt32(label7.Text);
                nr_intr += 1;
                label7.Text = nr_intr.ToString();

                Thread.Sleep(2000);
                
                button3.BackColor = Color.White;
                label1.Text = quests.ElementAt(nr_intr).question.ToString();
                button1.Text = quests.ElementAt(nr_intr).firstAnswer.ToString();
                button2.Text = quests.ElementAt(nr_intr).secondAnswer.ToString();
                button3.Text = quests.ElementAt(nr_intr).thirdAnswer.ToString();

                if (nr_tokens >= 3)
                {
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox8.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\lily.jpg");
                }

                if (nr_tokens >= 6)
                {
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\marshal.jpg");
                }
                if (nr_tokens >= 9)
                {
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox5.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\barney.jpg");
                }
                if (nr_tokens >= 12)
                {
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox4.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\robin.jpg");
                }
                if (nr_tokens >= 15)
                {
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\ted.jpg");
                }

            }
            else
            {
                button1.BackColor = Color.Red;
                

                int nr_intr = Convert.ToInt32(label7.Text);
                nr_intr += 1;
                label7.Text = nr_intr.ToString();

                Thread.Sleep(2000);
                
                button3.BackColor = Color.White;
                label1.Text = quests.ElementAt(nr_intr).question.ToString();
                button1.Text = quests.ElementAt(nr_intr).firstAnswer.ToString();
                button2.Text = quests.ElementAt(nr_intr).secondAnswer.ToString();
                button3.Text = quests.ElementAt(nr_intr).thirdAnswer.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IEnumerable<Quest> quests = questService.FindAll();
            Quest qu = new Quest("", "", "", "", "");
            foreach (Quest quest in quests)
            {
                if (quest.question.ToString() == label1.Text)
                {

                    qu = quest;
                }
            }

            if (button2.Text == qu.correctAnswer.ToString())
            {
                button2.BackColor = Color.Green;
                
                int nr_tokens = Convert.ToInt32(label3.Text);
                nr_tokens += 1;
                label3.Text = nr_tokens.ToString();

                int nr_intr = Convert.ToInt32(label7.Text);
                nr_intr += 1;
                label7.Text = nr_intr.ToString();

                Thread.Sleep(2000);
                
                button3.BackColor = Color.White;
                label1.Text = quests.ElementAt(nr_intr).question.ToString();
                button1.Text = quests.ElementAt(nr_intr).firstAnswer.ToString();
                button2.Text = quests.ElementAt(nr_intr).secondAnswer.ToString();
                button3.Text = quests.ElementAt(nr_intr).thirdAnswer.ToString();

                if (nr_tokens >= 3)
                {
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox8.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\lily.jpg");
                }

                if (nr_tokens >= 6)
                {
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\marshal.jpg");
                }
                if (nr_tokens >= 9)
                {
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox5.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\barney.jpg");
                }
                if (nr_tokens >= 12)
                {
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox4.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\robin.jpg");
                }
                if (nr_tokens >= 15)
                {
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\ted.jpg");
                }
            }
            else
            {
                button2.BackColor = Color.Red;
                
                int nr_intr = Convert.ToInt32(label7.Text);
                nr_intr += 1;
                label7.Text = nr_intr.ToString();

                Thread.Sleep(2000);
                
                button3.BackColor = Color.White;
                label1.Text = quests.ElementAt(nr_intr).question.ToString();
                button1.Text = quests.ElementAt(nr_intr).firstAnswer.ToString();
                button2.Text = quests.ElementAt(nr_intr).secondAnswer.ToString();
                button3.Text = quests.ElementAt(nr_intr).thirdAnswer.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IEnumerable<Quest> quests = questService.FindAll();
            Quest qu = new Quest("", "", "", "", "");
            foreach (Quest quest in quests)
            {
                if (quest.question.ToString() == label1.Text)
                {

                    qu = quest;
                }
            }

            if (button3.Text == qu.correctAnswer.ToString())
            {
                button3.BackColor = Color.Green;
                
                int nr_tokens = Convert.ToInt32(label3.Text);
                nr_tokens += 1;
                label3.Text = nr_tokens.ToString();

                int nr_intr = Convert.ToInt32(label7.Text);
                nr_intr += 1;
                label7.Text = nr_intr.ToString();

                Thread.Sleep(2000);
                
                button3.BackColor = Color.White;
                label1.Text = quests.ElementAt(nr_intr).question.ToString();
                button1.Text = quests.ElementAt(nr_intr).firstAnswer.ToString();
                button2.Text = quests.ElementAt(nr_intr).secondAnswer.ToString();
                button3.Text = quests.ElementAt(nr_intr).thirdAnswer.ToString();

                if (nr_tokens >= 3)
                {
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox8.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\lily.jpg");
                }

                if (nr_tokens >= 6)
                {
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\marshal.jpg");
                }
                if (nr_tokens >= 9)
                {
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox5.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\barney.jpg");
                }
                if (nr_tokens >= 12)
                {
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox4.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\robin.jpg");
                }
                if (nr_tokens >= 15)
                {
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.Image = Image.FromFile("D:\\lenovo1\\D\\Facultate\\AN2 SEMESTRU2\\MPP\\accesa\\poze\\ted.jpg");
                }
            }
            else
            {
                button3.BackColor = Color.Red;
                
                int nr_intr = Convert.ToInt32(label7.Text);
                nr_intr += 1;
                label7.Text = nr_intr.ToString();

                Thread.Sleep(2000);
                
                button3.BackColor = Color.White;
                label1.Text = quests.ElementAt(nr_intr).question.ToString();
                button1.Text = quests.ElementAt(nr_intr).firstAnswer.ToString();
                button2.Text = quests.ElementAt(nr_intr).secondAnswer.ToString();
                button3.Text = quests.ElementAt(nr_intr).thirdAnswer.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Quest newQuest = new Quest(textBox1.Text, textBox5.Text, textBox4.Text, textBox3.Text, textBox2.Text);
            questService.Save(newQuest);
        }
    }
}
