using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic;

namespace Notepad
{
    public partial class Form1 : Form
    {
        #region fieds
        private bool isfilealreadysaved;
        private bool isfiledirty;
        private string curropenfilename;
        #endregion
 
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        /// <summary>
        /// code for new filecode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isfiledirty)
            {
                DialogResult res = MessageBox.Show("Do You Want To Save Changes?", "File Save", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                switch (res)
                {
                    case DialogResult.Yes: SaveFile();

                        richTextBox1.Clear();
                        this.Text = "Untitled - Notepad";
                        isfiledirty = false;

                        break;
                    case DialogResult.No: richTextBox1.Clear();
                        this.Text = "Untitled - Notepad";
                        isfiledirty = false;
                        break;

                }
            }
                richTextBox1.Clear();
                this.Text = "Untitled - Notepad";
                isfiledirty = false;
                UndoRedo(false);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// code for open file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt| Rich Text Format (*.rtf)|*.rtf";
            DialogResult res=ofd.ShowDialog();

            if (res == DialogResult.OK)
            {
                if(Path.GetExtension(ofd.FileName)==".txt")
                    richTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);
                if(Path.GetExtension(ofd.FileName)==".rtf")
                    richTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.RichText);
                this.Text = Path.GetFileName(ofd.FileName) + " - Notepad";
                isfilealreadysaved = true;
                isfiledirty = false;
                curropenfilename = ofd.FileName;

                UndoRedo(false);

            }
            
        }

        private void UndoRedo(bool enable)
        {
            undoToolStripMenuItem.Enabled = enable;
            redotoolStripMenuItem1.Enabled = enable;
        }
        /// <summary>
        /// code for save file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        
        // implementing the savefile function
        private void SaveFile()
        {
            if (isfilealreadysaved)
            {
                if (Path.GetExtension(curropenfilename) == ".txt")
                    richTextBox1.SaveFile(curropenfilename, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(curropenfilename) == ".rtf")
                    richTextBox1.SaveFile(curropenfilename, RichTextBoxStreamType.RichText);
                isfiledirty = false;
            }

            else
            {
                if (isfiledirty)
                {
                    SaveAsFile();

                }
                else
                {
                    richTextBox1.Clear();
                    this.Text = "Untitled - Notepad";
                    isfiledirty = false;
                }
            }
        }
        /// <summary>
        /// code for SaveAs file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }
        // implementing the saveas function
        private void SaveAsFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt| Rich Text Format (*.rtf)|*.rtf";

            DialogResult res = sfd.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (Path.GetExtension(sfd.FileName) == ".txt")
                    richTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(sfd.FileName) == ".rtf")
                    richTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
                this.Text = Path.GetFileName(sfd.FileName) + " - Notepad";

                isfilealreadysaved = true;
                isfiledirty = false;
                curropenfilename = sfd.FileName;
            }
        }

        private void pageSetuoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// code for exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            fd.ShowColor = true;

            DialogResult res = fd.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (richTextBox1.SelectionLength > 0)
                {
                    richTextBox1.SelectionFont = fd.Font;
                    richTextBox1.SelectionColor = fd.Color;
                }

            }

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All rights reserved with the Author","About Notepad",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isfilealreadysaved=false;
            isfiledirty=false;
            curropenfilename="";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isfiledirty = true;
            undoToolStripMenuItem.Enabled = true;
           
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            redotoolStripMenuItem1.Enabled=true;
            undoToolStripMenuItem.Enabled = true;
        }

        private void redotoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
            redotoolStripMenuItem1.Enabled = false;
            undoToolStripMenuItem.Enabled = true;

            
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText += DateTime.Now.ToString();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            redotoolStripMenuItem1.Enabled = true;
            undoToolStripMenuItem.Enabled = true;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
            redotoolStripMenuItem1.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            fd.ShowColor = true;

            DialogResult res = fd.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (richTextBox1.SelectionLength > 0)
                {
                    richTextBox1.SelectionFont = fd.Font;
                    richTextBox1.SelectionColor = fd.Color;
                }
            }

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Line Number", "Go To", "1");
            try
            {
                int line = Convert.ToInt32(input);
                if (line > richTextBox1.Lines.Length)
                {
                    MessageBox.Show("Total lines in the file is" + richTextBox1.Lines.Length, "can't reach", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else {
                    string[] lines = richTextBox1.Lines;
                    int len = 0;
                    for (int i = 0; i < line - 1; i++)
                    {
                        len = len + lines[i].Length+1;
                    }
                    richTextBox1.Focus();
                    richTextBox1.Select(len,0);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Enter a valid Integer", "Wrong Input",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }

        }

    }
 }
