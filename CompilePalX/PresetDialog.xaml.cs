﻿using System.Windows;
using System.Windows.Input;

namespace CompilePalX
{
    /// <summary>
    /// Interaction logic for PresetDialog.xaml
    /// </summary>
    public partial class PresetDialog 
    {
        public PresetDialog(string title, Map? selectedMap)
        {
            InitializeComponent();
            Title = title;

            IsMapSpecificCheckbox.IsEnabled = selectedMap != null;
            IsMapSpecificCheckbox.IsHitTestVisible = selectedMap != null;
            if (selectedMap != null)
                MapToolTip = $"Preset will only apply to {selectedMap.MapName}";
        }

        public bool Result = false;
        public string Text { get { return InputTextBox.Text; } }
        public bool IsMapSpecific { get { return IsMapSpecificCheckbox.IsChecked ?? false; } }
        public string? MapToolTip { get; set; }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Result = true;
                Close();
            }
        }
    }
}
