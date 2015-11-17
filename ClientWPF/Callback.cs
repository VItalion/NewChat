using ClientWPF.ChatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientWPF
{
    public class Callback : IChatServiceCallback
    {
        private TextBox hystoryBox;
        private ListView hystoryListView;
        public Callback(ref TextBox box)
        {
            hystoryBox = box;
        }
        public Callback(ref ListView box)
        {
            hystoryListView = box;
        }
        public void NewMessage(string msg, string sender)
        {
            System.Windows.MessageBox.Show(sender + " say: " + msg);
            if (hystoryListView != null)
            {
                TextBlock block = new TextBlock();
                block.TextWrapping = System.Windows.TextWrapping.Wrap;
                block.Text = sender + " say: " + msg;

               /* ListBoxItem item = new ListBoxItem();
                item.Content = block;
                item.Width = hystoryListView.Width - 20;*/
                
                hystoryListView.Items.Add(block);
                return;
            }

            if (hystoryBox != null)
            {
                hystoryBox.Text += sender + " say: " + msg + '\n';
                hystoryBox.ScrollToEnd();
            }
        }
    }
}
