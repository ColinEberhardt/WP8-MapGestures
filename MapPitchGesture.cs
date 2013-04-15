using Microsoft.Phone.Maps.Controls;
using System;
using System.Windows.Input;

namespace MultiTouchMap
{
  /// <summary>
  /// Adds a three-finger pitch gesture to a Map control.
  /// </summary>
  public class MapPitchGesture : MapGestureBase
  {
    /// <summary>
    /// Gets or sets the sensitivity of this gesture
    /// </summary>
    public double Sensitivity { get; set; }

    private double? _initialPitchYLocation;

    public MapPitchGesture(Map map)
      : base(map)
    {
      Sensitivity = 0.5;
      Touch.FrameReported += Touch_FrameReported;
    }

    private void Touch_FrameReported(object sender, TouchFrameEventArgs e)
    {
      var touchPoints = e.GetTouchPoints(Map);

      SuppressMapGestures = touchPoints.Count == 3;

      if (touchPoints.Count == 3)
      {
        if (!_initialPitchYLocation.HasValue)
        {
          _initialPitchYLocation = touchPoints[0].Position.Y;
        }

        double delta = touchPoints[0].Position.Y - _initialPitchYLocation.Value;
        double newPitch = Math.Max(0, Math.Min(75, (Map.Pitch + delta * Sensitivity)));
        Map.Pitch = newPitch;
        _initialPitchYLocation = touchPoints[0].Position.Y;
      }
      else
      {
        _initialPitchYLocation = null;
      }
    }
  }
}
