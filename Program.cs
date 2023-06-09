/*
Generates image filenames for Obsidian MarkDown notes by appending an incr. number

    E.g. image.jpg -> image.jpg, image-1.jpg, ..., image-12.jpg, etc.

Has Console.WriteLine() for filenames as a redundancy; commented out by default
*/

using System;
using System.IO;
using System.Windows.Forms;

namespace ImageFileNamer
{
    public class Program : Form
    {
        private TextBox inputTextBox;
        private TextBox inputLimitBox;
        private CheckBox closeCheckBox;
        private CheckBox confirmationMsg;
        private Button generateButton;

        [STAThread]
        static void Main()
        {
            Application.Run(new Program());
        }

        public Program()
        {
            InitializeComponent();
        }

        // GUI Data
        private void InitializeComponent()
        {
            inputTextBox = new TextBox();
            inputLimitBox = new TextBox();
            generateButton = new Button();
            closeCheckBox = new CheckBox();
            confirmationMsg = new CheckBox();
            SuspendLayout();
            // 
            // inputTextBox
            // 
            inputTextBox.Location = new Point(12, 12);
            inputTextBox.Name = "inputTextBox";
            inputTextBox.Size = new Size(200, 23);
            inputTextBox.TabIndex = 0;
            inputTextBox.Text = "Filename (incl. ext.)";
            inputTextBox.KeyPress += InputBox_KeyPress;
            // 
            // inputLimitBox
            // 
            inputLimitBox.Location = new Point(12, 37);
            inputLimitBox.Name = "inputLimitBox";
            inputLimitBox.Size = new Size(200, 23);
            inputLimitBox.TabIndex = 1;
            inputLimitBox.Text = "Limit";
            inputLimitBox.KeyPress += InputBox_KeyPress;
            // 
            // generateButton
            // 
            generateButton.FlatStyle = FlatStyle.Flat;
            generateButton.Location = new Point(12, 63);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(200, 82);
            generateButton.TabIndex = 2;
            generateButton.Text = "Generate";
            generateButton.Click += GenerateButton_Click;
            // 
            // closeCheckBox
            // 
            closeCheckBox.AutoSize = true;
            closeCheckBox.Checked = true;
            closeCheckBox.CheckState = CheckState.Checked;
            closeCheckBox.Location = new Point(12, 148);
            closeCheckBox.Name = "closeCheckBox";
            closeCheckBox.Size = new Size(128, 19);
            closeCheckBox.TabIndex = 3;
            closeCheckBox.Text = "Close after copying";
            closeCheckBox.UseVisualStyleBackColor = true;
            // 
            // confirmationMsg
            // 
            confirmationMsg.AutoSize = true;
            confirmationMsg.Checked = true;
            confirmationMsg.CheckState = CheckState.Checked;
            confirmationMsg.Location = new Point(12, 168);
            confirmationMsg.Name = "confirmationMsg";
            confirmationMsg.Size = new Size(187, 19);
            confirmationMsg.TabIndex = 4;
            confirmationMsg.Text = "Don't show confirmation msg.";
            confirmationMsg.UseVisualStyleBackColor = true;
            // 
            // Program
            // 
            BackColor = SystemColors.Desktop;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(224, 200);
            Controls.Add(confirmationMsg);
            Controls.Add(closeCheckBox);
            Controls.Add(inputTextBox);
            Controls.Add(inputLimitBox);
            Controls.Add(generateButton);
            ForeColor = SystemColors.Control;
            Name = "Program";
            ResumeLayout(false);
            PerformLayout();
        }

        // Main method
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string input = inputTextBox.Text.Trim();
            int limit;
            string filename = Path.GetFileNameWithoutExtension(input);
            string ext = Path.GetExtension(input);
            int counter = 1;

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please enter a valid input");
                return;
            }

            if (string.IsNullOrEmpty(ext) || ext == ".")
            {
                MessageBox.Show("Please include a filename extension (e.g. .jpg)");
                return;
            }

            if (!int.TryParse(inputLimitBox.Text, out limit) || string.IsNullOrEmpty(inputLimitBox.Text))
            {
                MessageBox.Show("Please enter a valid limit");
                return;
            }

            using (StringWriter stringWriter = new StringWriter())
            {
                // Console.WriteLine(input); // Prints as redundancy
                stringWriter.WriteLine(input); // Add orig. filename to clipboard list

                // Iter. up to the limit
                while (counter <= limit)
                {
                    string newFilename = $"{filename}-{counter}{ext}";
                    // Console.WriteLine(newFilename); // Redundancy in case clipboard fails
                    stringWriter.WriteLine(newFilename); // Adds filename iter. to clipboard
                    counter++; // Incr. counter to reach user req. limit
                }

                Clipboard.SetText(stringWriter.ToString()); // The whole enchilada
            }

            if (!confirmationMsg.Checked)
            {
                MessageBox.Show("Print output copied to clipboard.");
            }

            if (closeCheckBox.Checked)
            {
                Close(); // Close the program
            }
        }

        private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                GenerateButton_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
