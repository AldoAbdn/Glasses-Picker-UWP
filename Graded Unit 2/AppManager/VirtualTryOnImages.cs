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
    /// Used to hold images for VirtualTryOnPage
    /// </summary>
    public class VirtualTryOnImages
    {
        //Attributes
        public WriteableBitmap frontImage;
        public WriteableBitmap sideImage;
        
        //Constructor
        public VirtualTryOnImages(WriteableBitmap frontImage, WriteableBitmap sideImage)
        {
            this.frontImage = frontImage;
            this.sideImage = sideImage;
        }

        //Getters
        public WriteableBitmap getFrontImage()
        {
            return this.frontImage;
        }

        public WriteableBitmap getSideImage()
        {
            return this.sideImage;
        }
            
        //Setters
        public void setFrontImage(WriteableBitmap frontImage)
        {
            this.frontImage = frontImage;
        }

        public void setSideImage(WriteableBitmap sideImage)
        {
            this.sideImage = sideImage;
        }
    }
}
