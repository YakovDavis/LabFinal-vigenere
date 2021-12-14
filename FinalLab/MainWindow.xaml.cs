using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.IO;

namespace FinalLab
{
    public partial class MainWindow : Window
    {
        public readonly List<string> modes;

        public MainWindow()
        {
            InitializeComponent();
            modes = new List<string>() { "Encryption", "Decryption" };
            modeComboBox.ItemsSource = modes;
            modeComboBox.SelectedItem = modes[0];
        }

        private void LoadFromFileCheckboxChecked(object sender, RoutedEventArgs e)
        {
            filePathTextBlock.IsEnabled = true;
            filePathTextBox.IsEnabled = true;
            filePathBrowseButton.IsEnabled = true;
            loadButton.IsEnabled = true;
            textTextBlock.IsEnabled = false;
            textTextBox.IsEnabled = false;
        }

        private void LoadFromFileCheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            filePathTextBlock.IsEnabled = false;
            filePathTextBox.IsEnabled = false;
            filePathBrowseButton.IsEnabled = false;
            loadButton.IsEnabled = false;
            textTextBlock.IsEnabled = true;
            textTextBox.IsEnabled = true;
        }

        private void UsePresetKeyCheckboxChecked(object sender, RoutedEventArgs e)
        {
            keyTextBlock.IsEnabled = false;
            keyTextBox.IsEnabled = false;
            keyTextBox.Text = "скорпион";
        }

        private void UsePresetKeyCheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            keyTextBlock.IsEnabled = true;
            keyTextBox.IsEnabled = true;
            keyTextBox.Text = "";
        }

        private void FilePathBrowseButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            if ((bool)result)
            {
                filePathTextBox.Text = fileDialog.FileName;
            }
        }

        private void SaveFilePathBrowseButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            var result = fileDialog.ShowDialog();
            if ((bool)result)
            {
                File.WriteAllText(fileDialog.FileName, resultTextBox.Text);
                saveFilePathTextBox.Text = fileDialog.FileName;
            }
        }

        private void LoadButtonClick(object sender, RoutedEventArgs e)
        {
            if (File.Exists(filePathTextBox.Text))
                textTextBox.Text = File.ReadAllText(filePathTextBox.Text);
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

            string res = "";
            int mult = 1;
            if (modeComboBox.SelectedItem.ToString() == "Decryption")
                mult = -1;
            KeyHelper keyHelper = new KeyHelper(keyTextBox.Text);

            int i = 0;

            foreach (char c in textTextBox.Text)
            {
                if (!alphabet.Contains(c))
                {
                    res += c;
                    continue;
                }
                if (!alphabet.Contains(keyHelper.CharAt(i)))
                {
                    res += c;
                    continue;
                }
                int tmp = alphabet.IndexOf(c) + mult * alphabet.IndexOf(keyHelper.CharAt(i));
                while (tmp < 0)
                    tmp += alphabet.Length;
                while (tmp >= alphabet.Length)
                    tmp -= alphabet.Length;
                res += alphabet[tmp];
                i++;
            }
            resultTextBox.Text = res;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(saveFilePathTextBox.Text, resultTextBox.Text);
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            textTextBox.Text = "";
        }

        private void LoadPresetTextButtonClick(object sender, RoutedEventArgs e)
        {
            textTextBox.Text = File.ReadAllText("Result_v5.txt");
        }
    }
}
