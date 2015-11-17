using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using ClientWPF.ChatService;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DeserializeSettrings();

            //ip = newIp;
            //room = nameRoom;
            //isCreated = isCreate;
        }
        ChatServiceClient client;

        #region data
        string login;
        string pwd;
        string ip;
        string room;
        string roomPwd;
        #endregion

        bool isCreated;
       
        #region settings
        Settings settings;
        BinaryFormatter formatter = new BinaryFormatter();
        #endregion
                
        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            client.SendMessage(login, room, messageTextBox.Text);
            messageTextBox.Clear();
            messageTextBox.Focus();
        }

        /*private async void loginTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client == null)
                {
                    joinButton.IsEnabled = false;
                    client = new ChatServiceClient(new System.ServiceModel.InstanceContext(new Callback(ref hystoryListView)), "testChat", ip);
                    if (await client.JoinAsync(room, loginTextBox.Text))
                    {
                        login = loginTextBox.Text;
                        //MessageBox.Show("Authorization complete");
                        loginTextBox.IsEnabled = false;
                        messageTextBox.Focus();
                    }
                    joinButton.IsEnabled = true;
                }
            }
        }*/

        private async void registerButton_Click(object sender, RoutedEventArgs e)
        {
            registrButton.IsEnabled = false;
            if (registrPwdBox.Password == registrConfimPwdBox.Password)
            {
                if (client == null)
                    client = new ChatServiceClient(new System.ServiceModel.InstanceContext(new Callback(ref hystoryListView)), "testChat", ip);

                login = registrLoginBox.Text;
                pwd = registrPwdBox.Password;

                if(await client.SignUpAsync(login, pwd))
                {
                    chat.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Incorrect Login/Password!");
                }
            }
            registrButton.IsEnabled = true;
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (client != null)
            {
                try
                {
                    client.LogOut(login, pwd);
                    client.Close();                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            try
            {
                using (FileStream fs = new FileStream("settings.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, settings);
                }
            }
            catch { }
        }

        private void messageTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                client.SendMessage(login, room, messageTextBox.Text);
                messageTextBox.Clear();
                messageTextBox.Focus();
            }
        }

        private void hystoryListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange > 0.0)
            {
                ((ScrollViewer)e.OriginalSource).ScrollToEnd();
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                netProtocollComboBox.Text = settings.NetProtocol;
                ipTexBox.Text = settings.IpServer;
                portTexBox.Text = settings.Port.ToString();
                                
                ip = settings.Uri.ToString();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Need config setting.");
                chat.IsEnabled = false;
                account.IsEnabled = false;
                setting.Focus();
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            hystoryListView.Height -= 61;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            hystoryListView.Height += 61;
        }

        private async void joinButton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            room = roomBox.Text;
            roomPwd = roomPwdBox.Password;

            if (await client.InsideAsync(room, roomPwd, login))
            {
                //login = loginTextBox.Text;
                //MessageBox.Show("Authorization complete");
                // loginTextBox.IsEnabled = false;

                hystoryListView.IsEnabled = true;
                messageTextBox.IsEnabled = true;
                sendButton.IsEnabled = true;

                roomBox.IsEnabled = false;
                roomPwdBox.IsEnabled = false;

                messageTextBox.Focus();
            }
            else
            {
                MessageBox.Show("Incorrect Room`s name/password!");
                ((Button)sender).IsEnabled = true;
            }            
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (client == null)
                client = new ChatServiceClient(new System.ServiceModel.InstanceContext(new Callback(ref hystoryListView)), "testChat", ip);

            login = loginBox.Text;
            pwd = pwdBox.Password;

            if(await client.SignInAsync(login, pwd))
            {
                chat.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Incorrect login/password!");
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            settings.IpServer = ipTexBox.Text;
            settings.NetProtocol = netProtocollComboBox.SelectionBoxItem.ToString();
            settings.Port = int.Parse(portTexBox.Text);
            settings.CreateUri();

            ip = settings.Uri.ToString();

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("settings.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, settings);
            }

            if (chat.IsEnabled || account.IsEnabled)
            {
                chat.IsEnabled = true;
                account.IsEnabled = true;
            }

            account.Focus();

            //this.Close();
        }

        private void DeserializeSettrings()
        {
            using (FileStream fs = new FileStream("settings.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    settings = (Settings)formatter.Deserialize(fs);
                    // settings.CreateUri();
                }
                catch
                {
                    settings = new Settings();
                }
            }
        }

        private void leaveButton_Click(object sender, RoutedEventArgs e)
        {
            client.Leave(room, login);

            roomBox.IsEnabled = true;
            roomPwdBox.IsEnabled = true;
            leaveButton.IsEnabled = false;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            room = roomBox.Text;
            roomPwd = roomPwdBox.Password;

            client.CreateRoom(room, roomPwd);
        }
    }
}
