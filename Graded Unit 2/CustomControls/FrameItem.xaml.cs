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
    /// Xaml template used to represet a Frame in the app
    /// Defines layout for the main icons used on FrameBrowserPage and ResultsPage
    /// </summary>
    public sealed partial class FrameItem : UserControl
    {
        public Frame Frame { get { return this.DataContext as Frame; } }
        public FrameItem()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
        
        public Frame getFrame()
        {
            return Frame;
        }
    }
}
