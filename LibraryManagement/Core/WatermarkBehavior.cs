using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace LibraryManagement.Core
{
    public static class WatermarkBehavior
    {
        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.RegisterAttached("WatermarkText", typeof(string), typeof(WatermarkBehavior), new PropertyMetadata(string.Empty, OnWatermarkTextChanged));

        public static string GetWatermarkText(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkTextProperty);
        }

        public static void SetWatermarkText(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkTextProperty, value);
        }

        private static void OnWatermarkTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.GotFocus -= RemoveWatermark;
                textBox.LostFocus -= ShowWatermark;

                if (!string.IsNullOrEmpty((string)e.NewValue))
                {
                    textBox.GotFocus += RemoveWatermark;
                    textBox.LostFocus += ShowWatermark;
                    ShowWatermark(textBox, null);
                }
            }
        }

        private static void RemoveWatermark(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == GetWatermarkText(textBox))
            {
                textBox.Text = string.Empty;
                textBox.Foreground = SystemColors.ControlTextBrush;
            }
        }

        private static void ShowWatermark(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = GetWatermarkText(textBox);
                textBox.Foreground = SystemColors.GrayTextBrush;
            }
        }
    }
}
