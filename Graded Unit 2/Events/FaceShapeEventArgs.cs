using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Graded_Unit_2.Events
{
    /// <summary>
    /// Used on faceShapeControl to pass clicked value back to FaceShapePage
    /// </summary>
    public class FaceShapeEventArgs : RoutedEventArgs
    {
        public string value { get; set; } = "";

        public FaceShapeEventArgs() : base()
        {

        }

    }
}
