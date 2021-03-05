﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace ColorClippy.Views
{
    /// <inheritdoc cref="System.Windows.Controls.UserControl" />
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        /// <inheritdoc />
        /// <summary>
        /// Creates a new instance
        /// </summary>
        private AboutWindow()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="owner">Owner of the window</param>
        public AboutWindow(Window owner) : this()
        {
            Owner = owner;
            _textBlockVersion.Text = $"Version: {GetVersion().ToString(3)}";
        }

        /// <summary>
        /// Sets the DialogResult to true to close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        /// <summary>
        /// Loads the version number from the click once manifest
        /// or returns "1.0.0.0" if not click once published
        /// </summary>
        /// <returns>The version of the assembly</returns>
        private static Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}