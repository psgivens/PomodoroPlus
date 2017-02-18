using PomodoroPlus.Infrastructure;
using PomodoroPlus.ViewModels;
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
using d = System.Drawing;

namespace PomodoroPlus {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window {

        public MainWindow() {
            InitializeComponent();
            var pomodoro = new PomodoroViewModel();
            DataContext = pomodoro;
            pomodoro.PropertyChanged += pomodoro_PropertyChanged;

            TrayMinimizer.EnableMinimizeToTray(this);
        }

        void pomodoro_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "MinutesRemaining":
                    var pomodoro = sender as PomodoroViewModel;
                    UpdateIcon(pomodoro.MinutesRemaining);
                    break;
                default:
                    break;
            }
        }

        #region Draw Icon
        private d.Bitmap _bitmap;
        private d.Icon _icon;
        private void UpdateIcon(int minutesRemaining) {
            // TODO: Try to draw icon and set it.

            //var graphics = System.Drawing.Graphics.FromImage(_bitmap);
            //var pen = new d.Pen(d.Color.Red);
            //graphics.DrawRectangle(pen, 0, 0, _icon.Width, _icon.Height);

            //IntPtr hIcon = _bitmap.GetHicon();
            //System.Drawing.Icon newIcon = System.Drawing.Icon.FromHandle(hIcon);

            //_icon = newIcon;            
        }
        #endregion
    }
}
