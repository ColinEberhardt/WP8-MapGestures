using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MultiTouchMap.Resources;
using System.Windows.Input;
using System.Diagnostics;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;

namespace MultiTouchMap
{
  public partial class MainPage : PhoneApplicationPage
  {

    // Constructor
    public MainPage()
    {
      InitializeComponent();

      map.CenterChanged += (s, e) => UpdateMetrics();
      map.HeadingChanged += (s, e) => UpdateMetrics();
      map.PitchChanged += (s, e) => UpdateMetrics();

      new MapRotationGesture(map);
      new MapPitchGesture(map);     
    }

    private void UpdateMetrics()
    {
      latText.Text = map.Center.Latitude.ToString("00.00");
      longText.Text = map.Center.Longitude.ToString("00.00");
      zoomText.Text = map.ZoomLevel.ToString("00.00");
      headingText.Text = map.Heading.ToString("00.00");
      pitchText.Text = map.Pitch.ToString("00.00");
    }
  }
}