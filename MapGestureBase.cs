using Microsoft.Phone.Maps.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MultiTouchMap
{
  /// <summary>
  /// A base class for map gestures, which allows them to suppress the built-in map gestures.
  /// </summary>
  public class MapGestureBase
  {
    /// <summary>
    /// Gets or sets whether to suppress the existing gestures/
    /// </summary>
    public bool SuppressMapGestures { get; set; }

    protected Map Map { get; private set; }

    public MapGestureBase(Map map)
    {
      Map = map;
      map.Loaded += (s,e) => CrawlTree(Map);
    }

    private void CrawlTree(FrameworkElement el)
    {
      el.ManipulationDelta += MapElement_ManipulationDelta;
      for (int c = 0; c < VisualTreeHelper.GetChildrenCount(el); c++)
      {
        CrawlTree(VisualTreeHelper.GetChild(el, c) as FrameworkElement);
      }
    }

    private void MapElement_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      if (SuppressMapGestures)
        e.Handled = true;
    }
  }
}
