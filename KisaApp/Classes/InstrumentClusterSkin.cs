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
    public Slider uxFuel;
    public Slider uxTemp;
    public Slider uxOil;
    public Slider uxBattery;

    private double _MPH;
    private double _RPM;
    private double _Fuel;
    private double _Temperature;
    private double _Oil;
    private double _BatteryLevel;

    public InstrumentClusterSkin()
    {
      Initialized += new EventHandler(Skin_Initialized);
    }

    void Skin_Initialized(object sender, EventArgs e)
    {
      uxMPH = FindName("uxMPH") as Slider;
      uxRPM = FindName("uxRPM") as Slider;
      uxFuel = FindName("uxFuel") as Slider;
      uxTemp = FindName("uxTemp") as Slider;
      uxOil = FindName("uxOil") as Slider;
      uxBattery = FindName("uxBattery") as Slider;
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

    public double Fuel
    {
      get { return _Fuel; }
      set
      {
        _Fuel = value;
        if (uxFuel != null) uxFuel.Value = value;
      }
    }

    public double Temperature
    {
      get { return _Temperature; }
      set
      {
        _Temperature = value;
        if (uxTemp != null) uxTemp.Value = value;
      }
    }

    public double Oil
    {
      get { return _Oil; }
      set
      {
        _Oil = value;
        if (uxOil != null) uxOil.Value = value;
      }
    }

    public double BatteryLevel
    {
      get { return _BatteryLevel; }
      set
      {
        _BatteryLevel = value;
        if (uxBattery != null) uxBattery.Value = value;
      }
    }

  }
}
