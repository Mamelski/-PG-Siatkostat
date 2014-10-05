using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// Login page.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void RegisterHyperlink_RegistrationPage(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistrationPage));
            //NavigationService.Navigate(new Uri("/Page2.xaml?msg=" + txtName.text, UriKind.Relative)); 
            //LoginTextBox.Text = "dupa";
        }

        private void LoginButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (IsLoginDataValid())
            {
                this.Frame.Navigate(typeof(RegistrationPage));
            }
            else
            {
                AuthenticationFailed();
            }
        }

        /// <summary>
        /// Displays "Invalid user or password!" on the screen
        /// </summary>
        private void AuthenticationFailed()
        {
            InvalidLoginDataTextBlock.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Returns true if login and password is not empty.
        /// </summary>
        private bool IsLoginDataValid()
        {
            return !(string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password));
        }
    }
}
