using ColorClippy.Business;
using ColorClippy.DataObjects;
using Hardcodet.Wpf.TaskbarNotification;
using System.Threading;
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


        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(ContextMenu),
                new PropertyMetadata(false));
        /// <summary>
        ///     Gets or sets a value indicating whether this animation is shown.
        /// </summary>
        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        /// <summary>
        /// DependencyProperty for the progress bar color
        /// </summary>
        public static readonly DependencyProperty ColorItemCommandProperty = DependencyProperty.Register(
            nameof(ColorItemCommand), typeof(ICommand), typeof(ContextMenu),
            new PropertyMetadata(new DelegateCommand(() => { })));

        /// <summary>
        /// Value of the top label
        /// </summary>
        public ICommand ColorItemCommand
        {
            get => (ICommand)GetValue(ColorItemCommandProperty);
            set => SetValue(ColorItemCommandProperty, value);
        }

        public ContextMenu()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                IsLoading = true;
                var obMenuItem = e.OriginalSource as MenuItem;
                var colorItem = obMenuItem.Tag as ColorItem;
                Clipboard.SetText(colorItem.HexCode);

                Thread.Sleep(200);
                IsLoading = false;
            }
        }
    }
}
