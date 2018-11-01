using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace KisaApp.Classes
{
  public class Needle : UserControl
  {
    private const double SLOWNESS = 5;

    Storyboard uxSB_GameLoop;
    FrameworkElement uxNeedle;
    public RotateTransform Needle_RotateTransform = new RotateTransform();
    public TransformGroup tg = new TransformGroup();
    private double desiredAngle;

    public Needle()
    {
      Loaded += new System.Windows.RoutedEventHandler(Needle_Loaded);
      Initialized += new EventHandler(Needle_Initialized);
    }

    void Needle_Initialized(object sender, EventArgs e)
    {
      uxNeedle = (FrameworkElement)FindName("uxNeedle");
      uxSB_GameLoop = (Storyboard)TryFindResource("uxSB_GameLoop");
    }

    void Needle_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      if (uxNeedle != null) uxNeedle.RenderTransform = Needle_RotateTransform;
      if (uxSB_GameLoop != null)
      {
        uxSB_GameLoop.Completed += new EventHandler(uxSB_GameLoop_Completed);
        uxSB_GameLoop.Begin();
      }
    }

    void uxSB_GameLoop_Completed(object sender, EventArgs e)
    {
      Needle_RotateTransform.Angle += (desiredAngle - Needle_RotateTransform.Angle) / Math.Max(SLOWNESS, 1);
      FrameworkElement uxNeedle = (FrameworkElement)FindName("uxNeedle");
      if (uxNeedle != null)
      {
        uxSB_GameLoop.Begin();
      }
    }

    public void UpdateNeedle()
    {
      desiredAngle = Maximum == Minimum ? Maximum : MinAngle + (MaxAngle - MinAngle) * (Math.Min(Math.Max(Value, Minimum), Maximum) - Minimum) / (Maximum - Minimum);
      if (uxSB_GameLoop == null)
      {
        Needle_RotateTransform.Angle = desiredAngle;
      }
    }

    private static void OnValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Needle ln = (Needle)d;
      ln.UpdateNeedle();
    }

    [Category("Common Properties")]
    public double Value
    {
      get { return (double)GetValue(ValueProperty); }
      set { SetValue(ValueProperty, value); }
    }
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(double), typeof(Needle), new PropertyMetadata(new Double(), new PropertyChangedCallback(OnValuesChanged)));

    [Category("Common Properties")]
    public double Minimum
    {
      get { return (double)GetValue(MinimumProperty); }
      set { SetValue(MinimumProperty, value); }
    }
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register("Minimum", typeof(double), typeof(Needle), new PropertyMetadata(new Double(), new PropertyChangedCallback(OnValuesChanged)));


    [Category("Common Properties")]
    public double Maximum
    {
      get { return (double)GetValue(MaximumProperty); }
      set { SetValue(MaximumProperty, value); }
    }
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register("Maximum", typeof(double), typeof(Needle), new PropertyMetadata(new Double(), new PropertyChangedCallback(OnValuesChanged)));


    [Category("Common Properties")]
    public double MinAngle
    {
      get { return (double)GetValue(MinAngleProperty); }
      set { SetValue(MinAngleProperty, value); }
    }
    public static readonly DependencyProperty MinAngleProperty =
        DependencyProperty.Register("MinAngle", typeof(double), typeof(Needle), new PropertyMetadata(new Double(), new PropertyChangedCallback(OnValuesChanged)));


    [Category("Common Properties")]
    public double MaxAngle
    {
      get { return (double)GetValue(MaxAngleProperty); }
      set { SetValue(MaxAngleProperty, value); }
    }
    public static readonly DependencyProperty MaxAngleProperty =
        DependencyProperty.Register("MaxAngle", typeof(double), typeof(Needle), new PropertyMetadata(new Double(), new PropertyChangedCallback(OnValuesChanged)));
  }
}

