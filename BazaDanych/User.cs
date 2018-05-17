using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace BazaDanych
{
    public class User
    {
        private String name;
        private String password;

        public User(String name, String password)
        {
            this.name = name;
            this.password = password;
        }

        public User(String name)
        {
            this.Name = name;
        }


        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }


        public String Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        static SqlCommand sqlCommand;
        static string connectionString = "Server=KAROL;Database=TEST_CS;Trusted_Connection=true";
        static SqlConnection connection = new SqlConnection(connectionString);

        public static void openCloseDBconnection(string value)
        {
            using (sqlCommand = new SqlCommand(value, connection))
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Add()
        {
            try
            {
                if (String.IsNullOrEmpty(this.Name) || String.IsNullOrEmpty(this.Password))
                {
                    MessageBox.Show("Wpisz imie i haslo !");

                }
                else
                {
                    string newNameAndPassword = string.Format("INSERT INTO dbo.Users(Name,MyPassword) VALUES ('" + this.Name + "','" + this.Password + "')");
                    openCloseDBconnection(newNameAndPassword);
                    MessageBox.Show("Imie i hasło zapisane do bazy !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(this.Name))
                {
                    string userToDelete = string.Format("DELETE FROM dbo.Users WHERE Name = '" + this.Name + "' ");
                    openCloseDBconnection(userToDelete);

                    MessageBox.Show("User usunięty z bazy !");
                }
                else
                {
                    MessageBox.Show("Pole uzytkownika do usuniecia nie moze byc puste!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Update(User user1, User user2)
        {
            try
            {
                if (String.IsNullOrEmpty(user1.Name) || String.IsNullOrEmpty(user2.Name))
                {
                    MessageBox.Show("Wpisz stare imie i nowe imie usera !");

                }
                else
                {
                    string userToUpdate = string.Format("UPDATE dbo.Users SET Name = '" + user2.Name + "' WHERE Name = '" + user1.Name + "' ");
                    openCloseDBconnection(userToUpdate);
                    MessageBox.Show("User zaktualizowany!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public static List<String> Display()
        {
            List<string> usersList = new List<string>();
            try
            {

                string query = "SELECT[Name] FROM dbo.Users";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usersList.Add(reader[0].ToString());
                        }
                    }
                }

                connection.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return usersList;
        }
    }
}

