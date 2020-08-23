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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Graded_Unit_2.CustomControls
{
    /// <summary>
    /// Xaml template used to represent a colour on colourPage. 
    /// </summary>
    public sealed partial class ColourItem : UserControl
    {
        //Bob Taber https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners/UWP-042-Utilizing-User-Controls-as-Data-Templates
        public Models.Colour Colour { get { return this.DataContext as Models.Colour; } }

        public ColourItem()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
