using JaguarRoutePlanner.Sources.Utils.Gestures;

namespace MultiTouchMap
{
    public partial class MainPage
    {
        private readonly MapGestureBase _rotateGesture = new MapRotationGesture();
        private readonly MapGestureBase _pitchGesture = new MapPitchGesture();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            map.CenterChanged += (s, e) => UpdateMetrics();
            map.HeadingChanged += (s, e) => UpdateMetrics();
            map.PitchChanged += (s, e) => UpdateMetrics();

            _rotateGesture.Map = map;
            _pitchGesture.Map = map;
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