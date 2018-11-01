using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KisaApp.Classes
{
  public class InstrumentClusterSkin : UserControl
  {
    public Slider uxMPH;
    public Slider uxRPM;

    private double _MPH;
    private double _RPM;

    public InstrumentClusterSkin()
    {
      Initialized += new EventHandler(Skin_Initialized);
    }

    void Skin_Initialized(object sender, EventArgs e)
    {
      uxMPH = FindName("uxMPH") as Slider;
      uxRPM = FindName("uxRPM") as Slider;
    }

    public double MPH
    {
      get
      {
        return _MPH;
      }
      set
      {
        _MPH = value;
        if (uxMPH != null) uxMPH.Value = value;
      }
    }

    public double RPM
    {
      get
      {
        return _RPM;
      }
      set
      {
        _RPM = value;
        if (uxRPM != null) uxRPM.Value = value;
      }
    }


  }
}
