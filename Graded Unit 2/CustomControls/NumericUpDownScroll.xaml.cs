using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Graded_Unit_2.CustomControls
{
    /// <summary>
    /// This is used on Prescription page as the method for inputing values of the prescription
    /// </summary>
    public sealed partial class NumericUpDownScroll : UserControl
    {
        //Dependency properties
        public String PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value);  }
        }
        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register("PlaceholderText", typeof(String), typeof(NumericUpDownScroll), null);

        public List<ComboBoxItem> Items
        {
            get { return (List<ComboBoxItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(List<ComboBoxItem>), typeof(NumericUpDownScroll), null);

        public bool wasChanged { get; set; }

        //Properties
        private double _val;

        public double val
        {
            get { return _val; }
            set
            {
                _val = value;
            }
        }

        //Events
        public event EventHandler valChanged;

        public NumericUpDownScroll()
        {
            this.InitializeComponent();
            for(double i = 20; i >= -20; i -= 0.25)
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Content = convertToPrescription(i);
                cbItem.Tag = i;
                cbMain.Items.Add(cbItem);
            }
        }

        public void positiveOnly()
        {
            cbMain.Items.Clear();
            for (double i = 20; i >= 0; i -= 0.25)
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Content = convertToPrescription(i);
                cbItem.Tag = i;
                cbMain.Items.Add(cbItem);
            }
        }

        public void setValue(double value)
        {
            if (value % 0.25 == 0)
            {
                val = value;
                cbMain.SelectedIndex = cbMain.Items.IndexOf(convertToPrescription(val));
            }           
        }

        public double getValue()
        {
            return val;
        }

        public bool isNull()
        {
            return !wasChanged;
        }

        public static String convertToPrescription(double val)
        {
            String str = "";
            if (val == 0)
            {
                return "0.00";
            }
            if (val > 0)
            {
                str = "+" + val.ToString();
            }
            else
            {
                str = val.ToString();
            }
            if (val % 1 == 0)
            {
                str += ".00";
            }
            else if (val % 0.50 == 0)
            {
                str += "0";
            }
            return str;
        }

        private void cbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            val = Convert.ToDouble(((ComboBoxItem)cbMain.SelectedItem).Tag);
            if (valChanged != null)
                valChanged(this, new EventArgs());
            wasChanged = true;
        }
    }
}
