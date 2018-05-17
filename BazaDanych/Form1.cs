using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BazaDanych
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // dodawanie usera i hasla do bazy
        private void buttonDopiszDoBazy_Click(object sender, EventArgs e)
        {
            User user = new User(textBoxNewUser.Text, inputPassword.Text);
            user.Add();
        }

        // usuwanie usera z tabeli
        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            User user = new User(textUserToDelete.Text);
            user.Delete();
        }

        // aktualizacja usera w bazie
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            User oldUser = new User(textBoxOldUserName.Text);
            User newUser = new User(textBoxNewUserName.Text);

            User.Update(oldUser, newUser);
        }

        // usuwanie wszystkich danych z tabeli
        private void buttonDeleteTable_Click(object sender, EventArgs e)
        {
            string deleteFromDB = "DELETE FROM dbo.Users";
            User.openCloseDBconnection(deleteFromDB);
            string setMainKey = "DBCC CHECKIDENT('dbo.Users', RESEED, 0)";
            User.openCloseDBconnection(setMainKey);
            MessageBox.Show("Wszystkie dane z tabeli usunięte !");
        }

        // metoda odpowiadajaca za wyswietlenie userow z tabeli 
        private void DisplayUsers()
        {
            List<String> userList = new List<String>();
            userList = User.Display();

            for (int i = 0; i < userList.Count; i++)
            {
                this.listBox1.Items.Add(userList[i]);
            }

        }

        // wyswietlenie userow z tabeli po nacisnieciu zakladki Wyświetlanie uzytkowników
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (tabControl.SelectedTab.Name == "tabPage5")
            {
                DisplayUsers();
            }
        }
    }
}
