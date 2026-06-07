using System.Windows;

namespace PasswordResetBruteForceDemo
{
    public partial class MainWindow : Window
    {
        private PasswordGenerator passwordGenerator = new PasswordGenerator();
        private HashService hashService = new HashService();
        private BruteForceAttackService attackService = new BruteForceAttackService();
        private PerformanceLogger performanceLogger = new PerformanceLogger();

        private AttackResult lastSingleThreadResult;
        private AttackResult lastMultiThreadResult;

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

            lastSingleThreadResult = null;
            lastMultiThreadResult = null;

            PerformanceLogTextBox.Text = "Password generated and hashed successfully.";
        }

        private void StartSingleThreadButton_Click(object sender, RoutedEventArgs e)
        {
            if (HashTextBox.Text == "")
            {
                MessageBox.Show("Please generate a password first.");
                return;
            }

            PerformanceLogTextBox.Text = "Single-thread brute force attack started...";
            AttackProgressBar.Value = 25;

            AttackResult result = attackService.RunSingleThreadAttack(HashTextBox.Text);
            lastSingleThreadResult = result;

            AttackProgressBar.Value = 100;
            ElapsedTimeTextBox.Text = result.ElapsedTime.ToString();
            FoundPasswordTextBox.Text = result.FoundPassword;

            PerformanceLogTextBox.Text =
                "Single-thread attack finished." +
                "\nPassword found: " + result.IsFound +
                "\nFound password: " + result.FoundPassword +
                "\nAttempts: " + result.Attempts +
                "\nElapsed time: " + result.ElapsedTime;

            ShowComparisonIfBothAttacksDone();
        }

        private void StartMultiThreadButton_Click(object sender, RoutedEventArgs e)
        {
            if (HashTextBox.Text == "")
            {
                MessageBox.Show("Please generate a password first.");
                return;
            }

            PerformanceLogTextBox.Text = "Multi-thread brute force attack started...";
            AttackProgressBar.Value = 25;

            AttackResult result = attackService.RunMultiThreadAttack(HashTextBox.Text);
            lastMultiThreadResult = result;

            AttackProgressBar.Value = 100;
            ElapsedTimeTextBox.Text = result.ElapsedTime.ToString();
            FoundPasswordTextBox.Text = result.FoundPassword;

            PerformanceLogTextBox.Text =
                "Multi-thread attack finished." +
                "\nPassword found: " + result.IsFound +
                "\nFound password: " + result.FoundPassword +
                "\nAttempts: " + result.Attempts +
                "\nElapsed time: " + result.ElapsedTime;

            ShowComparisonIfBothAttacksDone();
        }

        private void ShowComparisonIfBothAttacksDone()
        {
            if (lastSingleThreadResult != null && lastMultiThreadResult != null)
            {
                string comparisonLog = performanceLogger.CreateComparisonLog(
                    lastSingleThreadResult,
                    lastMultiThreadResult
                );

                PerformanceLogTextBox.Text = comparisonLog;
            }
        }
    }
}