using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Graded_Unit_2
{
    /// <summary>
    /// Holds and gives access to images related to a frame
    /// </summary>
    public class FrameImages
    {
        //Attributes
        public List<ImageSource> views;
        private ImageSource frontView;
        private ImageSource sideView;

        //Constructor
        public FrameImages(List<ImageSource> views, ImageSource frontView, ImageSource sideView)
        {
            this.views = views;
            this.frontView = frontView;
            this.sideView = sideView;
        }

        //Getters
        public List<ImageSource> getViews()
        {
            return this.views;
        }

        public ImageSource getFrontView()
        {
            return this.frontView;
        }

        public ImageSource getSideView()
        {
            return this.sideView;
        }
    }
}
