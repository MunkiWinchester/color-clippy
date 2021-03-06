using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfUtility.Services;

namespace ColorClippy.Views.Tray
{
    /// <summary>
    /// Interaction logic for ContextMenu.xaml
    /// </summary>
    public partial class ContextMenu : UserControl
    {
        /// <summary>
        /// DependencyProperty for the progress bar color
        /// </summary>
        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
            nameof(CloseCommand), typeof(ICommand), typeof(ContextMenu),
            new PropertyMetadata(new DelegateCommand(() => { })));

        /// <summary>
        /// Value of the top label
        /// </summary>
        public ICommand CloseCommand
        {
            get => (ICommand)GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }

        public ContextMenu()
        {
            InitializeComponent();
        }
    }
}
