using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using JetBrains.Annotations;
using Microsoft.Phone.Maps.Controls;

namespace JaguarRoutePlanner.Sources.Utils.Gestures
{
    /// <summary>
    ///     A base class for map gestures, which allows them to suppress the built-in map gestures.
    ///     source https://github.com/ColinEberhardt/WP8-MapGestures
    /// </summary>
    public abstract class MapGestureBase
    {
        private Map _map;

        protected MapGestureBase()
        {
            Touch.FrameReported += OnFrameReported;
        }

        /// <summary>
        ///     Gets or sets whether to suppress the existing gestures/
        /// </summary>
        public bool SuppressMapGestures { get; set; }

        public Map Map
        {
            get { return _map; }
            set
            {
                UnregisterMapCrawler();

                _map = value;

                if (_map != null)
                    _map.Loaded += OnMapLoaded;
            }
        }

        /// <summary>
        ///     unregister crawler on previous map
        /// </summary>
        private void UnregisterMapCrawler()
        {
            if (_map != null)
                _map.Loaded -= OnMapLoaded;
        }

        private void OnMapLoaded(object sender, RoutedEventArgs e)
        {
            CrawlTree(Map);
        }

        ~MapGestureBase()
        {
            UnregisterMapCrawler();
            Touch.FrameReported -= OnFrameReported;
        }

        private void CrawlTree([NotNull] UIElement el)
        {
            el.ManipulationDelta += MapElement_ManipulationDelta;
            for (var c = 0; c < VisualTreeHelper.GetChildrenCount(el); c++)
            {
                CrawlTree((FrameworkElement) VisualTreeHelper.GetChild(el, c));
            }
        }

        private void MapElement_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (SuppressMapGestures)
                e.Handled = true;
        }

        private void OnFrameReported(object sender, TouchFrameEventArgs e)
        {
            var touchPoints = e.GetTouchPoints(null);
            if (touchPoints != null)
                OnTouch(touchPoints);
        }

        protected abstract void OnTouch([NotNull] TouchPointCollection touchPoints);
    }
}