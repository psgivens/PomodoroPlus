using PomodoroPlus.Data;
using PomodoroPlus.EntityFramework;
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


namespace PomodoroPlusDetails {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window {
        private readonly DetailsViewModel _viewModel = new DetailsViewModel();

        public MainWindow() {
            InitializeComponent();
            ItemsGrid.CurrentCellChanged += ItemsGrid_CurrentCellChanged;
            DataContext = _viewModel;
            refreshMenuItem_Click(null, null);
        }

        private void ItemsGrid_CurrentCellChanged(object sender, EventArgs e) {
            _viewModel.Save();
        }

        private void refreshMenuItem_Click(object sender, RoutedEventArgs e) {
            _viewModel.Refresh();
        }

        private void saveMenuItem_Click(object sender, RoutedEventArgs e) {
            _viewModel.Save();
        }
    }
}
