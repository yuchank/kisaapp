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

using Quobject.SocketIoClientDotNet.Client;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Threading;

using System.Timers;

using KisaApp.Classes;

namespace KisaApp
{
    /// <summary>
    /// SkinSelector.xaml에 대한 상호 작용 논리
    /// </summary>
  public partial class SkinSelector : UserControl
  {
    private Socket socket;

    private readonly InstrumentClusterSkin[] _skins = new InstrumentClusterSkin[1];

    public SkinSelector()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      this.socket = IO.Socket("http://192.168.0.182:2000");

      //if (Window.GetWindow(this) != null)
      //{
      //  WindowInteropHelper helper = new WindowInteropHelper(Window.GetWindow(this));
      //  HwndSource.FromHwnd(helper.Handle).AddHook(new HwndSourceHook(this.WndProc));
      //}

      _skins[0] = uxSkin2011;
      _skins[0].Fuel = 90;
      _skins[0].Temperature = 20;

      uxSkin2011.uxSettings.Click += uxSettings_Click;

      this.socket.On(Socket.EVENT_CONNECT, () => {
      });

      this.socket.On(Socket.EVENT_DISCONNECT, () => {
        // Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { initialize(); }));
      });

      this.socket.On("speed-cs", (v) =>
      {
        // String s = String.Format("{0:F0}", Convert.ToDouble(v));
        if (v != null)
        { 
          Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
            Random r = new Random();
            int rand = r.Next(100, 300);
            _skins[0].MPH = Convert.ToInt32(Convert.ToDouble(v));
            _skins[0].RPM = Convert.ToInt32(Convert.ToDouble(v)*33 + rand);   // RPM = MPH * 33, it's fake
          }));
        }
      });

      this.socket.On("gear-cs", (v) =>
      {
        // String s = String.Format("{0:F0}", v);
        if (v != null)
        {
          // Do nothing
          // Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { _skins[0].RPM = Convert.ToInt32(Convert.ToDouble(v)); }));
        }
      });
    }

    void uxSettings_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
      this.socket.Disconnect();
      this.socket.Close();
    }

    //IntPtr WndProc(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam, ref bool handled)
    //{
    //  UInt32 WM_DEVICECHANGE = 0x0219;
    //  UInt32 DBT_DEVTUP_VOLUME = 0x02;
    //  UInt32 DBT_DEVICEARRIVAL = 0x8000;
    //  UInt32 DBT_DEVICEREMOVECOMPLETE = 0x8004;

    //  // USB IN
    //  if ((nMsg == WM_DEVICECHANGE) && (wParam.ToInt32() == DBT_DEVICEARRIVAL))
    //  {
    //    int devType = Marshal.ReadInt32(lParam, 4);

    //    if (devType == DBT_DEVTUP_VOLUME)
    //    {
    //      // this.socket.Emit("attack", "attack");
    //    }
    //  }

    //  // USB OUT
    //  if ((nMsg == WM_DEVICECHANGE) && (wParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE))
    //  {
    //    int devType = Marshal.ReadInt32(lParam, 4);
    //    if (devType == DBT_DEVTUP_VOLUME)
    //    {
    //    }
    //  }
    //  return IntPtr.Zero;
    //}
  }
}
