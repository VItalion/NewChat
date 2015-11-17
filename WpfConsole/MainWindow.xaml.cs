using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.ServiceModel;

namespace WpfConsole
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Settings setting;
        BinaryFormatter formatter = new BinaryFormatter();
        ServiceHost host;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeserializeSettrings();

            netProtocollComboBox.Text = setting.NetProtocol;
            ipTexBox.Text = setting.IpServer;
            portTexBox.Text = setting.Port.ToString();                        
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            setting.IpServer = ipTexBox.Text;
            setting.NetProtocol = netProtocollComboBox.SelectionBoxItem.ToString();
            setting.Port = int.Parse(portTexBox.Text);
            setting.CreateUri();

            try
            {
                host = new ServiceHost(typeof(ChatServiceLibrary.ChatService), setting.Uri);
                
                statusLabel.Content = "Server is running";

                okButton.IsEnabled = false;
                cancelButton.IsEnabled = true;
            }
            catch(Exception ex)
            {
                statusLabel.Content = ex.Message;
            }

            using (FileStream fs = new FileStream("settings.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, setting);
            }

            host.Open();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                host.Close();
                statusLabel.Content = "Server is stoped";

                okButton.IsEnabled = true;
                cancelButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                statusLabel.Content = ex.Message;
            }
        }

        private void DeserializeSettrings()
        {
            using (FileStream fs = new FileStream("settings.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    setting = (Settings)formatter.Deserialize(fs);
                    // settings.CreateUri();
                }
                catch
                {
                    setting = new Settings();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                host.Close();
            }
            catch { }
        }
    }
}
