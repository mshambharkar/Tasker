using Hardcodet.Wpf.TaskbarNotification;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using Tasker.ViewModel;

namespace Tasker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskbarIcon tbi;
        public MainWindow(MainViewModel mainviewModel)
        {
            InitializeComponent();
            DataContext = mainviewModel;
            //Top right corner
            Left = SystemParameters.WorkArea.Width - Width;
            Top = SystemParameters.WorkArea.Height - Height;
            tbi = new TaskbarIcon
            {
                Icon = System.Drawing.SystemIcons.Application,
                ToolTipText = "Tasker",
                Visibility = Visibility.Visible
            };

            tbi.TrayMouseDoubleClick += Tbi_TrayMouseDoubleClick;
        }
        private void Tbi_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Minimized;
            else
            {
               
                WindowState = WindowState.Normal;
            }

        }

        /*private System.Drawing.Point GetWindowPositon(System.Drawing.Point cursorPosition)
        {
            System.Drawing.Point result = new System.Drawing.Point(0, 0);
            System.Drawing.Point point = Control.MousePosition;
            var targetPoint = GetWindowPositon(point);
            result = cursorPosition;
            return result;
        }
        */

    }
}
