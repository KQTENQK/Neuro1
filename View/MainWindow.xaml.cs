using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;

namespace NeuroLab1.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel.MainWindowViewModel();
        }

        private void OnErrorButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowViewModel viewModel = (ViewModel.MainWindowViewModel)DataContext;

            //viewModel.BeginLearning(1);
            //Chart.Update(true);
            //_misInfoText.Text = viewModel.MisInfo;

            viewModel.Check(new List<byte[]> { new Model.Types.Zero().Image, new Model.Types.One().Image,
                                               new Model.Types.Two().Image, new Model.Types.Three().Image,
                                                new Model.Types.Four().Image, new Model.Types.Five().Image,
                                                new Model.Types.Six().Image, new Model.Types.Seven().Image,
                                                new Model.Types.Eight().Image, new Model.Types.Nine().Image});
        }

        private void OnLearningButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowViewModel viewModel = (ViewModel.MainWindowViewModel)DataContext;

            viewModel.BeginLearning(18);
            _misInfoText.Text = viewModel.MisInfo;
            Chart.Update(true);
        }
    }
}
