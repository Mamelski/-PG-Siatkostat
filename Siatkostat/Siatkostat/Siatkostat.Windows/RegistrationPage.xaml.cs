using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Siatkostat
{
    /// <summary>
    /// Registration form page.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        private const int MIN_LOGIN_LENGTH = 6;
        private const int MIN_PASS_LENGTH = 6;
        private readonly Regex EmailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

        public RegistrationPage()
        {
            this.InitializeComponent();
        }

        private void RegisterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ValidateInput())
            { 
                // register action
            }
        }

        /// <summary>
        /// Returns true if user data is valid.
        /// </summary>
        private bool ValidateInput()
        {
            if (!PasswordsEquals())
            {
                ShowError("Password must be the same");
                return false;
            }
            if (LoginRegistrationTextBox.Text.Length < MIN_LOGIN_LENGTH)
            {
                ShowError("Login must be at least " + MIN_LOGIN_LENGTH.ToString() + " characters");
                return false;
            }
            if (PasswordRegistrationPasswordBox.Password.Length < MIN_PASS_LENGTH)
            {
                ShowError("Password must be at least " + MIN_PASS_LENGTH.ToString() + " characters");
                return false;
            }
            if(!EmailRegex.IsMatch(EmailRegistrationTextBox.Text))
            {
                ShowError("Email is invalid");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Displays error information
        /// </summary>
        private void ShowError(string info)
        {
            ErrorTextBox.Text = info;
            ErrorTextBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private bool PasswordsEquals()
        {
            return PasswordRegistrationPasswordBox.Password == ConfirmPasswordRegistrationPasswordBox.Password;
        }
    }
}
