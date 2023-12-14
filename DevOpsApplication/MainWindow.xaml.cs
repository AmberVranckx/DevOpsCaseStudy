using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevOpsApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            firstnameText.TabIndex = 0;
            lastnameText.TabIndex = 1;
            informatie.TabIndex = 2;
            btnClear.TabIndex = 3;
            teamText.TabIndex = 4;
            teamInfo.TabIndex = 5;
            btnClear1.TabIndex = 6;
            firstnameTextChange.TabIndex = 7;
            lastnameTextChange.TabIndex = 8;
            teamTextChange.TabIndex = 9;
            addressTextChange.TabIndex = 10;
            zipcodeTextChange.TabIndex = 11;
            cityTextChange.TabIndex = 12;
            emailTextChange.TabIndex = 13;
            imageTextChange.TabIndex = 14;
            add.TabIndex = 15;
            delete.TabIndex = 16;

            
        }


        public void GrantAccess()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void information_Click(object sender, RoutedEventArgs e)
        {

            using (var connection = new SqliteConnection("Data Source=Datafile.db"))
            {
                connection.Open();


                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT m.address, m.zipcode, m.city, m.email, t.name, m.image
                    FROM Members m join Teams t on t.Id = m.TeamId
                    WHERE m.name = $name and m.lastname = $lastname
                ";

                if (firstnameText.Text == "")
                {
                    MessageBox.Show("Enter a firstname");
                    return;
                }
                else
                {
                    command.Parameters.AddWithValue("$name", firstnameText.Text);
                    if (lastnameText.Text == "")
                    {
                        MessageBox.Show("Enter a lastname");
                        return;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("$lastname", lastnameText.Text);
                    }
                }



                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            var address = reader.GetString(0);
                            var zipcode = reader.GetString(1);
                            var city = reader.GetString(2);
                            var email = reader.GetString(3);
                            var teamName = reader.GetString(4);
                            var image = reader.GetString(5);

                            informationList.Visibility = Visibility.Visible;
                            imgMember.Visibility = Visibility.Visible;

                            informationList.Items.Add("Firstname: " + firstnameText.Text);
                            informationList.Items.Add("Lastname: " + lastnameText.Text);
                            informationList.Items.Add("Address: " + address);
                            informationList.Items.Add("zipcode: " + zipcode);
                            informationList.Items.Add("City: " + city);
                            informationList.Items.Add("Email: " + email);
                            informationList.Items.Add("Team: " + teamName);
                            imgMember.Source = new BitmapImage(new Uri(image, UriKind.Relative));
                            firstnameText.Clear();
                            lastnameText.Clear();
                            

                        }
                    }
                    else
                    {
                        MessageBox.Show("No member found with the name " + firstnameText.Text + " " + lastnameText.Text);
                        firstnameText.Clear();
                        lastnameText.Clear();
                    }
                }



            }
        }

        private void teamInfo_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=Datafile.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT m.Name, m.lastname, m.address, m.zipcode, m.city, m.email
                    FROM Teams t join Members m on m.TeamId = t.Id 
                    WHERE t.name = $name
                ";
                command.Parameters.AddWithValue("$name", teamText.Text);
                informationList.Items.Clear();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var firstname = reader.GetString(0);
                            var lastname = reader.GetString(1);


                            informationList.Visibility = Visibility.Visible;

                            informationList.Items.Add(firstname + " " + lastname);
                            teamText.Clear();

                        }
                    }
                    else
                    {
                        MessageBox.Show("No team found with the name " + teamText.Text);
                        teamText.Clear();
                    }
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {

            using (var connection = new SqliteConnection("Data Source=Datafile.db"))
            {
                connection.Open();

                var commandName = connection.CreateCommand();
                commandName.CommandText =
                @"
                    SELECT Id from Teams where name = $name
                ";
                commandName.Parameters.AddWithValue("$name", teamTextChange.Text);

                using (var reader = commandName.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetString(0);


                            var command = connection.CreateCommand();
                            command.CommandText =
                            @"
                                INSERT INTO Members (Name, Lastname, address, zipcode, city, email, image, teamId)
                                VALUES ($name, $lastname, $address, $zipcode, $city, $email, $image, $teamId)
                            ";

                            if (firstnameTextChange.Text == "")
                            {
                                MessageBox.Show("Enter a firstname");
                                return;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("$name", firstnameTextChange.Text);
                                if (lastnameTextChange.Text == "")
                                {
                                    MessageBox.Show("Enter a lastname");
                                    return;
                                    
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("$lastname", lastnameTextChange.Text);
                                    if (addressTextChange.Text == "")
                                    {
                                        MessageBox.Show("Enter an address");
                                        return;
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("$address", addressTextChange.Text);
                                        if (zipcodeTextChange.Text == "")
                                        {
                                            MessageBox.Show("Enter a zipcode");
                                            return;
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("$zipcode", zipcodeTextChange.Text);

                                            if (cityTextChange.Text == "")
                                            {
                                                MessageBox.Show("Enter a city");
                                                return;
                                            }
                                            else
                                            {
                                                command.Parameters.AddWithValue("$city", cityTextChange.Text);

                                                if (emailTextChange.Text == "")
                                                {
                                                    MessageBox.Show("Enter an email");
                                                    return;
                                                }
                                                else
                                                {
                                                    command.Parameters.AddWithValue("$email", emailTextChange.Text);

                                                    if (imageTextChange.Text == "")
                                                    {
                                                        MessageBox.Show("Enter a image");
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        command.Parameters.AddWithValue("$image", imageTextChange.Text);

                                                        if (teamTextChange.Text == null)
                                                        {
                                                            MessageBox.Show("Enter a team");
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            command.Parameters.AddWithValue("$teamId", id);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            command.ExecuteNonQuery();

                            MessageBox.Show("the user " + firstnameTextChange.Text + " " + lastnameTextChange.Text + " has been added");

                            firstnameTextChange.Clear();
                            lastnameTextChange.Clear();
                            addressTextChange.Clear();
                            zipcodeTextChange.Clear();
                            cityTextChange.Clear();
                            emailTextChange.Clear();
                            teamTextChange.Clear();
                            imageTextChange.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No team found with this id");
                    }

                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=Datafile.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM Members
                    WHERE Name = $name and Lastname = $lastname and address = $address;
                ";
                command.Parameters.AddWithValue("$name", firstnameTextChange.Text);
                command.Parameters.AddWithValue("$lastname", lastnameTextChange.Text);
                command.Parameters.AddWithValue("$address", addressTextChange.Text);
                command.ExecuteNonQuery();

                MessageBox.Show("the user " + firstnameTextChange.Text + " " + lastnameTextChange.Text + " has been deleted");
                firstnameTextChange.Clear();
                lastnameTextChange.Clear();
                addressTextChange.Clear();

               

            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            informationList.Items.Clear();
            imgMember.Visibility = Visibility.Hidden;
            imgMember.Source = null;
        }

        private void informatie_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void informatie_MouseLeave(object sender, MouseEventArgs e)
        {
           
        }

        private void informatie_GotFocus(object sender, RoutedEventArgs e)
        {
            informatie.Background = Brushes.White;
            informatie.Foreground = Brushes.DarkOliveGreen;
        }

        private void informatie_LostFocus(object sender, RoutedEventArgs e)
        {
            informatie.Background = Brushes.DarkOliveGreen;
            informatie.Foreground = Brushes.White;
        }

        private void btnClear_GotFocus(object sender, RoutedEventArgs e)
        {
            btnClear.Background = Brushes.White;
            btnClear.Foreground = Brushes.DarkOliveGreen;
        }

        private void btnClear_LostFocus(object sender, RoutedEventArgs e)
        {
            btnClear.Background = Brushes.DarkOliveGreen;
            btnClear.Foreground = Brushes.White;
        }

        private void teamInfo_GotFocus(object sender, RoutedEventArgs e)
        {
            teamInfo.Background = Brushes.White;
            teamInfo.Foreground = Brushes.DarkOliveGreen;
        }

        private void teamInfo_LostFocus(object sender, RoutedEventArgs e)
        {
            teamInfo.Background = Brushes.DarkOliveGreen;
            teamInfo.Foreground = Brushes.White;
        }

        private void btnClear1_GotFocus(object sender, RoutedEventArgs e)
        {
            btnClear1.Background = Brushes.White;
            btnClear1.Foreground = Brushes.DarkOliveGreen;
        }

        private void btnClear1_LostFocus(object sender, RoutedEventArgs e)
        {
            btnClear1.Background = Brushes.DarkOliveGreen;
            btnClear1.Foreground = Brushes.White;
        }

        private void add_GotFocus(object sender, RoutedEventArgs e)
        {
            add.Background = Brushes.White;
            add.Foreground = Brushes.DarkOliveGreen;
        }

        private void add_LostFocus(object sender, RoutedEventArgs e)
        {
            add.Background = Brushes.DarkOliveGreen;
            add.Foreground = Brushes.White;
        }

        private void delete_GotFocus(object sender, RoutedEventArgs e)
        {
            delete.Background = Brushes.White;
            delete.Foreground = Brushes.DarkOliveGreen;
        }

        private void delete_LostFocus(object sender, RoutedEventArgs e)
        {
            delete.Background = Brushes.DarkOliveGreen;
            delete.Foreground = Brushes.White;
        }
    }
}