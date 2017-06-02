using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraController
{
    public partial class NameEntryDialog : Form
    {
        public string GroupName
        {
            get { return groupNameTextbox.Text; }
            set { groupNameTextbox.Text = value; }
        }
        
        public NameEntryDialog(string dialogTitle)
        {
            InitializeComponent();
            this.Text = dialogTitle;
        }

        private void groupNameTextbox_Validating(object sender, CancelEventArgs e)
        {
            //Group name must contain at least one word character
            var match = Regex.Match(GroupName, @".*\w.*");
            if (!match.Success)
            {
                e.Cancel = true;
            }
        }

        private void PresetGroupNameDialog_Shown(object sender, EventArgs e)
        {
            groupNameTextbox.SelectAll();
        }
    }
}
