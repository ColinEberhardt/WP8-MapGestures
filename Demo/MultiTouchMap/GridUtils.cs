using System.Windows;
using System.Windows.Controls;

namespace MultiTouchMap
{
    public class GridUtils
    {
        /// <summary>
        ///     Parses a string to create a GridLength
        /// </summary>
        private static GridLength ParseLength(string length)
        {
            length = length.Trim();

            if (length.ToLowerInvariant().Equals("auto"))
            {
                return new GridLength(0, GridUnitType.Auto);
            }
            if (length.Contains("*"))
            {
                length = length.Replace("*", "");
                if (string.IsNullOrEmpty(length)) length = "1";
                return new GridLength(double.Parse(length), GridUnitType.Star);
            }

            return new GridLength(double.Parse(length), GridUnitType.Pixel);
        }

        #region RowDefinitions attached property

        /// <summary>
        ///     Identified the RowDefinitions attached property
        /// </summary>
        public static readonly DependencyProperty RowDefinitionsProperty =
            DependencyProperty.RegisterAttached("RowDefinitions", typeof (string), typeof (GridUtils),
                new PropertyMetadata("", OnRowDefinitionsPropertyChanged));

        /// <summary>
        ///     Gets the value of the RowDefinitions property
        /// </summary>
        public static string GetRowDefinitions(DependencyObject d)
        {
            return (string) d.GetValue(RowDefinitionsProperty);
        }

        /// <summary>
        ///     Sets the value of the RowDefinitions property
        /// </summary>
        public static void SetRowDefinitions(DependencyObject d, string value)
        {
            d.SetValue(RowDefinitionsProperty, value);
        }

        /// <summary>
        ///     Handles property changed event for the RowDefinitions property, constructing
        ///     the required RowDefinitions elements on the grid which this property is attached to.
        /// </summary>
        private static void OnRowDefinitionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var targetGrid = d as Grid;

            // construct the required row definitions
            targetGrid.RowDefinitions.Clear();
            var rowDefs = e.NewValue as string;
            var rowDefArray = rowDefs.Split(',');
            foreach (var rowDefinition in rowDefArray)
            {
                if (rowDefinition.Trim() == "")
                {
                    targetGrid.RowDefinitions.Add(new RowDefinition());
                }
                else
                {
                    targetGrid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = ParseLength(rowDefinition)
                    });
                }
            }
        }

        #endregion

        #region ColumnDefinitions attached property

        /// <summary>
        ///     Identifies the ColumnDefinitions attached property
        /// </summary>
        public static readonly DependencyProperty ColumnDefinitionsProperty =
            DependencyProperty.RegisterAttached("ColumnDefinitions", typeof (string), typeof (GridUtils),
                new PropertyMetadata("", OnColumnDefinitionsPropertyChanged));

        /// <summary>
        ///     Gets the value of the ColumnDefinitions property
        /// </summary>
        public static string GetColumnDefinitions(DependencyObject d)
        {
            return (string) d.GetValue(ColumnDefinitionsProperty);
        }

        /// <summary>
        ///     Sets the value of the ColumnDefinitions property
        /// </summary>
        public static void SetColumnDefinitions(DependencyObject d, string value)
        {
            d.SetValue(ColumnDefinitionsProperty, value);
        }

        /// <summary>
        ///     Handles property changed event for the ColumnDefinitions property, constructing
        ///     the required ColumnDefinitions elements on the grid which this property is attached to.
        /// </summary>
        private static void OnColumnDefinitionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var targetGrid = d as Grid;

            // construct the required column definitions
            targetGrid.ColumnDefinitions.Clear();
            var columnDefs = e.NewValue as string;
            var columnDefArray = columnDefs.Split(',');
            foreach (var columnDefinition in columnDefArray)
            {
                if (columnDefinition.Trim() == "")
                {
                    targetGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                else
                {
                    targetGrid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = ParseLength(columnDefinition)
                    });
                }
            }
        }

        #endregion
    }
}