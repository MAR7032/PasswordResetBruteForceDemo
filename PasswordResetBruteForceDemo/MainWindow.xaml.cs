using System.Windows;

namespace PasswordResetBruteForceDemo
{
    public partial class MainWindow : Window
    {
        private PasswordGenerator passwordGenerator = new PasswordGenerator();
        private HashService hashService = new HashService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GeneratePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string password = passwordGenerator.GeneratePassword();
            string hash = hashService.ComputeSha256Hash(password);

            GeneratedPasswordTextBox.Text = password;
            HashTextBox.Text = hash;

            FoundPasswordTextBox.Text = "";
            ElapsedTimeTextBox.Text = "";
            AttackProgressBar.Value = 0;
            PerformanceLogTextBox.Text = "Password generated and hashed successfully.";
        }
    }
}