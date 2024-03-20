using System;
using System.IO;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace FinalYearProject;
{
    public partial class Form1 : Form
    {
        private Label filePathLabel;
        private TextBox filePathTextBox;
        private Button browseButton;
        private Label outputPathLabel;
        private TextBox outputPathTextBox;
        private Button outputPathButton;
        private Button convertButton;

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Initialize Controls
            filePathLabel = new Label();
            filePathLabel.Text = "Select Text File:";
            filePathLabel.Location = new System.Drawing.Point(20, 20);
            this.Controls.Add(filePathLabel);

            filePathTextBox = new TextBox();
            filePathTextBox.Location = new System.Drawing.Point(150, 20);
            filePathTextBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(filePathTextBox);

            browseButton = new Button();
            browseButton.Text = "Browse";
            browseButton.Location = new System.Drawing.Point(370, 20);
            browseButton.Click += new EventHandler(browseButton_Click);
            this.Controls.Add(browseButton);

            outputPathLabel = new Label();
            outputPathLabel.Text = "Output Audio File:";
            outputPathLabel.Location = new System.Drawing.Point(20, 60);
            this.Controls.Add(outputPathLabel);

            outputPathTextBox = new TextBox();
            outputPathTextBox.Location = new System.Drawing.Point(150, 60);
            outputPathTextBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(outputPathTextBox);

            outputPathButton = new Button();
            outputPathButton.Text = "Select";
            outputPathButton.Location = new System.Drawing.Point(370, 60);
            outputPathButton.Click += new EventHandler(outputPathButton_Click);
            this.Controls.Add(outputPathButton);

            convertButton = new Button();
            convertButton.Text = "Convert";
            convertButton.Location = new System.Drawing.Point(150, 100);
            convertButton.Click += new EventHandler(convertButton_Click);
            this.Controls.Add(convertButton);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            string inputFilePath = filePathTextBox.Text;
            string outputFilePath = outputPathTextBox.Text;

            if (File.Exists(inputFilePath))
            {
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    synth.SetOutputToWaveFile(outputFilePath);
                    synth.Speak(File.ReadAllText(inputFilePath));
                    MessageBox.Show("Text converted to speech successfully!", "Conversion Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid input text file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void outputPathButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Wave files (*.wav)|*.wav";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputPathTextBox.Text = saveFileDialog.FileName;
            }
        }
    }
}