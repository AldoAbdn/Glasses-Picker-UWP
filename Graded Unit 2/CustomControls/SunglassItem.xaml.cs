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
    /// xaml template used on SunglassPage 
    /// </summary>
    public sealed partial class SunglassItem : UserControl
    {
        //Bob Taber https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners/UWP-042-Utilizing-User-Controls-as-Data-Templates
        public Models.Sunglass Sunglass { get { return this.DataContext as Models.Sunglass; } }
        public SunglassItem()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
