using Microsoft.Phone.Maps.Controls;
using System;
using System.Windows.Input;

namespace MultiTouchMap
{
  /// <summary>
  /// Adds a two-finger rotation gesture to a Map control.
  /// </summary>
  public class MapRotationGesture : MapGestureBase
  {
    /// <summary>
    /// Gets or sets the minimuum rotation that the user must apply in order to initiate this gesture.
    /// </summary>
    public double MinimumRotation { get; set; }

    private double? _previousAngle;

    private bool _isRotating;
    
    public MapRotationGesture(Map map)
      : base(map)
    {
      MinimumRotation = 10.0;
      Touch.FrameReported += Touch_FrameReported;
    }
    

    private void Touch_FrameReported(object sender, TouchFrameEventArgs e)
    {
      var touchPoints = e.GetTouchPoints(Map);

      if (touchPoints.Count == 2)
      {
        // for the initial touch, record the angle between the fingers
        if (!_previousAngle.HasValue)
        {
          _previousAngle = AngleBetweenPoints(touchPoints[0], touchPoints[1]);
        }
        
        // should we rotate?
        if (!_isRotating)
        {
          double angle = AngleBetweenPoints(touchPoints[0], touchPoints[1]);
          double delta = angle - _previousAngle.Value;
          if (Math.Abs(delta) > MinimumRotation)
          {
            _isRotating = true;
            SuppressMapGestures = true;
          }
        }

        // rotate me
        if (_isRotating)
        {
          double angle = AngleBetweenPoints(touchPoints[0], touchPoints[1]);
          double delta = angle - _previousAngle.Value;
          Map.Heading -= delta;
          _previousAngle = angle;
        }
      }
      else
      {
        _previousAngle = null;
        _isRotating = false;
        SuppressMapGestures = false;
      }
    }

    private double AngleBetweenPoints(TouchPoint p1, TouchPoint p2)
    {
      return Math.Atan2(p1.Position.Y - p2.Position.Y, p1.Position.X - p2.Position.X)
              *(180 / Math.PI);
    }
  }
}
