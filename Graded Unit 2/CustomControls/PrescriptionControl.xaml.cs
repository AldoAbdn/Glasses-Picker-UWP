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
using static Graded_Unit_2.Prescription;
using Graded_Unit_2.CustomControls;
using Graded_Unit_2.Pages.PrescriptionPages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Graded_Unit_2.CustomControls
{
    /// <summary>
    /// This is used to enter someones precription on PrescriptionPage. It uses the NumericUpDownScroll custom control
    /// It represents the prescription for ONE eye. So you need two to get the full prescription
    /// </summary>
    public sealed partial class PrescriptionControl : UserControl
    {
        //Dependency properties
        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(PrescriptionControl), null);

        //Properties
        private String VADistance;
        private String VANear;
        private List<ComboBoxItem> sphItems;
        private List<ComboBoxItem> cylItems;
        private List<ComboBoxItem> axisItems;
        private List<ComboBoxItem> prismItems;
        private List<ComboBoxItem> nearAddItems;
        private List<ComboBoxItem> intAddItems;

        public PrescriptionControl()
        {
            this.InitializeComponent();
            sphItems = generatePlusMinusCBItems();
            cylItems = generatePlusMinusCBItems();
            axisItems = generateAxisCBItems();
            prismItems = generatePlusCBItems();
            nearAddItems = generatePlusCBItems();
            intAddItems = generatePlusCBItems();
        }

        public EyeRX getEyeRX()
        {
            double sph = nudSph.getValue();
            double cyl = nudCyl.getValue();
            int axis = Convert.ToInt32(nudAxis.getValue());
            double nearAdd = nudNear.getValue();
            double intAdd = nudInt.getValue();
            double prism = nudPrism.getValue();
            ComboBoxItem cb = (ComboBoxItem)txtBase.SelectedItem;
            String prismBase = "";
            if (cb != null)
                prismBase = cb.Content.ToString();
            EyeRX eyeRX = new EyeRX(sph, cyl, axis, nearAdd, intAdd, prism, prismBase, this.VADistance, this.VANear);
            return eyeRX;
        }

        private void axis_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
                int returnValue;
                //Removes characters 
                if (!int.TryParse(sender.Text, out returnValue))
                {
                    foreach (var cha in sender.Text)
                    {
                        if (!int.TryParse(cha.ToString(), out returnValue))
                        {
                            sender.Text = sender.Text.Remove(sender.Text.IndexOf(cha));
                            sender.SelectionStart = sender.Text.Length;
                            return;
                        }
                    }
                }
                else if (returnValue > 180)
                {
                    sender.Text = "180";
                }
        }

        public bool isNull()
        {
            return nudSph.isNull();     
        }

        private void nudSph_valChanged(object sender, EventArgs e)
        {
            nudCyl.IsEnabled = true;
            nudPrism.IsEnabled = true;
            nudNear.IsEnabled = true;
            nudInt.IsEnabled = true;
            btnAdvanced.IsEnabled = true;
        }

        private void nudPrism_valChanged(object sender, EventArgs e)
        {
            var prismUpDown = (NumericUpDownScroll)sender;
            if (prismUpDown != null)
            {
                if (prismUpDown.getValue() == 0)
                {
                    txtBase.IsEnabled = false;
                    txtBase.SelectedIndex = -1;
                }
                else
                {
                    txtBase.IsEnabled = true;
                    if (txtBase.SelectedIndex == -1)
                        //Defaults to UP if not set, as you always need a base
                        txtBase.SelectedIndex = 0;
                }
            }
        }

        private void nudCyl_valChanged(object sender, EventArgs e)
        {
            var prismUpDown = (NumericUpDownScroll)sender;
            if (prismUpDown != null)
            {
                if (prismUpDown.getValue() == 0)
                    nudAxis.IsEnabled = false;
                else
                    nudAxis.IsEnabled = true;
            }
        }

        private async void btnAdvanced_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PrescriptionDialog();
            dialog.VADistance = VADistance;
            dialog.VANear = VANear;
            dialog.Title = Text + " Advanced";
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                VADistance = dialog.VADistance;
                VANear = dialog.VANear;
            }
        }

        //Used to populate numericUpDownScolls
        private List<ComboBoxItem> generatePlusMinusCBItems()
        {
            List<ComboBoxItem> list = new List<ComboBoxItem>();
            for (double i = 20; i >= -20; i -= 0.25)
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Content = NumericUpDownScroll.convertToPrescription(i);
                cbItem.Tag = i;
                list.Add(cbItem);
            }
            return list;
        }

        private List<ComboBoxItem> generatePlusCBItems()
        {
            List<ComboBoxItem> list = new List<ComboBoxItem>();
            for (double i = 20; i >= 0; i -= 0.25)
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Content = NumericUpDownScroll.convertToPrescription(i);
                cbItem.Tag = i;
                list.Add(cbItem);
            }
            return list;
        }

        private List<ComboBoxItem> generateAxisCBItems()
        {
            List<ComboBoxItem> list = new List<ComboBoxItem>();
            for (double i = 180; i >= 0; i -= 1)
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Content = i;
                cbItem.Tag = i;
                list.Add(cbItem);
            }
            return list;
        }
    }
}
