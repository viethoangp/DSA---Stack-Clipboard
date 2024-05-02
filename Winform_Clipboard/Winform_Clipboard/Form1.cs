using Clipboard;

namespace Winform_Clipboard
{
    public partial class Form1 : Form
    {
        ClipboardSimulator clipboard = new ClipboardSimulator();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
                MessageBox.Show("Input any text");
            else
            {
                clipboard.Copy(richTextBox1.Text);
                listBoxClipboard.Items.Clear();
                foreach (string item in clipboard.activeStack)
                {
                    listBoxClipboard.Items.Add(item + Environment.NewLine);

                }
                MessageBox.Show($"Copied '{richTextBox1.Text}' to clipboard ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = clipboard.Paste(richTextBox1.Text);
            MessageBox.Show("Text pasted");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            listBoxClipboard.Items.Clear();
            clipboard.activeStack.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBoxClipboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxClipboard.SelectedIndex == -1)
            {
                return;
            }
            int selectedIndex = listBoxClipboard.SelectedIndex;
            string pastedText;

            if (selectedIndex < clipboard.activeStack.Count)
            {
                pastedText = clipboard.activeStack.ElementAt(selectedIndex);
            }
            else
            {
                if (clipboard.historyStack.Count == 0)
                {
                    MessageBox.Show("No older items in clipboard history.");
                    return;
                }
                pastedText = clipboard.historyStack.Pop();
                clipboard.activeStack.Push(pastedText);
            }
            richTextBox1.Text = pastedText;
            MessageBox.Show($"'{pastedText}' selected");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
