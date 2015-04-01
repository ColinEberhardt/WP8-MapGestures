using System;
using System.Windows.Input;

namespace JaguarRoutePlanner.Sources.Utils.Gestures
{
    /// <summary>
    ///     Adds a three-finger pitch gesture to a Map control.
    /// </summary>
    public class MapPitchGesture : MapGestureBase
    {
        private double? _initialPitchYLocation;

        public MapPitchGesture()
        {
            Sensitivity = 0.5;
        }

        /// <summary>
        ///     Gets or sets the sensitivity of this gesture
        /// </summary>
        public double Sensitivity { get; set; }

        protected override void OnTouch(TouchPointCollection touchPoints)
        {
            SuppressMapGestures = touchPoints.Count == 3;

            if (touchPoints.Count == 3)
            {
                if (!_initialPitchYLocation.HasValue)
                {
                    _initialPitchYLocation = touchPoints[0].Position.Y;
                }

                var delta = touchPoints[0].Position.Y - _initialPitchYLocation.Value;
                var newPitch = Math.Max(0, Math.Min(75, (Map.Pitch + delta*Sensitivity)));
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