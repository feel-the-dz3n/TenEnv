using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TenEnv.ExceptionHandler
{
    public partial class Form1 : Form
    {
        public Form1(string content)
        {
            InitializeComponent();
            tbLog.Text = content;
        }

        private void tbLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|Log Files (*.log)|*.log|All Files (*.*)|*.*"
            };

            if (s.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var w = new StreamWriter(s.FileName))
                    {
                        w.Write(tbLog.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Can't save file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            tbLog.Focus();
            tbLog.SelectAll();
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            tbLog.Focus();
            tbLog.SelectAll();
            tbLog.Copy();
        }

        private void btnPage_Click(object sender, EventArgs e)
        {
            Process.Start(Program.IssuesPage);
        }
    }
}
