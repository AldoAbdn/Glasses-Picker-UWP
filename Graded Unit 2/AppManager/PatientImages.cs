using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Graded_Unit_2
{
    /// <summary>
    /// Used to hold images for VirtualTryOnPage for patient
    /// </summary>
    public class PatientImages : VirtualTryOnImages
        {
            public PatientImages(WriteableBitmap frontImage, WriteableBitmap sideImage) : base(frontImage, sideImage)
            {
                this.frontImage = frontImage;
                this.sideImage = sideImage;
            }
        }   
}
