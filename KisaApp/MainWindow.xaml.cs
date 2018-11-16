
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace KisaApp
{
  /// <summary>
  /// MainWindow.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
    }

    private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      
    }
  }
}
