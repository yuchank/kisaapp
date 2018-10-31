using Quobject.SocketIoClientDotNet.Client;
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
    private Socket socket;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      WindowInteropHelper helper = new WindowInteropHelper(this);
      HwndSource source = HwndSource.FromHwnd(helper.Handle);
      source.AddHook(new HwndSourceHook(this.WndProc));

      initialize();

      this.socket = IO.Socket("http://localhost:2000");

      this.socket.On(Socket.EVENT_CONNECT, () => {
      });

      this.socket.On(Socket.EVENT_DISCONNECT, () => {
        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { initialize(); }));
      });

      this.socket.On("speed-cs", (v) =>
      {
        String s = String.Format("{0:F0}", Convert.ToDouble(v));
        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { speed.Content = s; }));
      });

      this.socket.On("gear-cs", (v) =>
      {
        String s = String.Format("{0:F0}", v);
        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { gear.Content = s; }));
      });
    }

    private void initialize()
    {
      speed.Content = "0";
      gear.Content = "0";
    }

    private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.socket.Disconnect();
    }

    IntPtr WndProc(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      UInt32 WM_DEVICECHANGE = 0x0219;
      UInt32 DBT_DEVTUP_VOLUME = 0x02;
      UInt32 DBT_DEVICEARRIVAL = 0x8000;
      UInt32 DBT_DEVICEREMOVECOMPLETE = 0x8004;

      // USB IN
      if ((nMsg == WM_DEVICECHANGE) && (wParam.ToInt32() == DBT_DEVICEARRIVAL))
      {
        int devType = Marshal.ReadInt32(lParam, 4);

        if (devType == DBT_DEVTUP_VOLUME)
        {
          this.socket.Emit("attack", "attack");
        }
      }

      // USB OUT
      if ((nMsg == WM_DEVICECHANGE) && (wParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE))
      {
        int devType = Marshal.ReadInt32(lParam, 4);
        if (devType == DBT_DEVTUP_VOLUME)
        {
        }
      }
      return IntPtr.Zero;
    }

    private void BtnReset_Click(object sender, RoutedEventArgs e)
    {
      this.socket.Emit("reset", "reset");
    }

    private void BtnAttack_Click(object sender, RoutedEventArgs e)
    {
      this.socket.Emit("attack", "attack");
    }
  }
}
