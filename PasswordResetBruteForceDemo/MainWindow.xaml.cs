using System;
using System.Threading;
using System.Threading.Tasks;
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

        private CancellationTokenSource cancellationTokenSource;

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

        private async void StartSingleThreadButton_Click(object sender, RoutedEventArgs e)
        {
            if (HashTextBox.Text == "")
            {
                MessageBox.Show("Please generate a password first.");
                return;
            }

            try
            {
                string targetHash = HashTextBox.Text;

                cancellationTokenSource = new CancellationTokenSource();

                PerformanceLogTextBox.Text = "Single-thread brute force attack started...";
                AttackProgressBar.Value = 25;

                AttackResult result = await Task.Run(() =>
                    attackService.RunSingleThreadAttack(targetHash, cancellationTokenSource.Token)
                );

                lastSingleThreadResult = result;

                AttackProgressBar.Value = 100;
                ElapsedTimeTextBox.Text = result.ElapsedTime.ToString();
                FoundPasswordTextBox.Text = result.FoundPassword;

                if (result.IsFound)
                {
                    PerformanceLogTextBox.Text =
                        "Single-thread attack finished." +
                        "\nPassword found: " + result.IsFound +
                        "\nFound password: " + result.FoundPassword +
                        "\nAttempts: " + result.Attempts +
                        "\nElapsed time: " + result.ElapsedTime;
                }
                else
                {
                    AttackProgressBar.Value = 0;

                    PerformanceLogTextBox.Text =
                        "Single-thread attack stopped or password not found." +
                        "\nAttempts: " + result.Attempts +
                        "\nElapsed time: " + result.ElapsedTime;
                }

                ShowComparisonIfBothAttacksDone();

                MessageBox.Show("Single-thread attack finished. You can take screenshot now.");
            }
            catch (Exception ex)
            {
                AttackProgressBar.Value = 0;
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void StartMultiThreadButton_Click(object sender, RoutedEventArgs e)
        {
            if (HashTextBox.Text == "")
            {
                MessageBox.Show("Please generate a password first.");
                return;
            }

            try
            {
                string targetHash = HashTextBox.Text;

                cancellationTokenSource = new CancellationTokenSource();

                PerformanceLogTextBox.Text = "Multi-thread brute force attack started...";
                AttackProgressBar.Value = 25;

                AttackResult result = await Task.Run(() =>
                    attackService.RunMultiThreadAttack(targetHash, cancellationTokenSource.Token)
                );

                lastMultiThreadResult = result;

                AttackProgressBar.Value = 100;
                ElapsedTimeTextBox.Text = result.ElapsedTime.ToString();
                FoundPasswordTextBox.Text = result.FoundPassword;

                if (result.IsFound)
                {
                    PerformanceLogTextBox.Text =
                        "Multi-thread attack finished." +
                        "\nPassword found: " + result.IsFound +
                        "\nFound password: " + result.FoundPassword +
                        "\nAttempts: " + result.Attempts +
                        "\nElapsed time: " + result.ElapsedTime;
                }
                else
                {
                    AttackProgressBar.Value = 0;

                    PerformanceLogTextBox.Text =
                        "Multi-thread attack stopped or password not found." +
                        "\nAttempts: " + result.Attempts +
                        "\nElapsed time: " + result.ElapsedTime;
                }

                ShowComparisonIfBothAttacksDone();

                MessageBox.Show("Multi-thread attack finished. You can take screenshot now.");
            }
            catch (Exception ex)
            {
                AttackProgressBar.Value = 0;
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                AttackProgressBar.Value = 0;
                PerformanceLogTextBox.Text = "Stop requested. Running attack will stop safely.";
            }
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