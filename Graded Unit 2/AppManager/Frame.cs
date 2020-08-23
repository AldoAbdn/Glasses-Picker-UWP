using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    /// <summary>
    /// Represents a frame
    /// Holds attribtes about a frame and images associated with the frame
    /// </summary>
    public partial class Frame
        {
            //Attributes
            public FrameProperties frameProperties;
            public FrameImages frameImages;

            //Constructor
            public Frame(FrameProperties frameProperties, FrameImages frameImages)
            {
                this.frameProperties = frameProperties;
                this.frameImages = frameImages;
            }   

            //Setters
            public void setFrameImages(FrameImages frameImages)
            {
                this.frameImages = frameImages;
            }

            public void setFrameProperties(FrameProperties frameProperties)
            {
                this.frameProperties = frameProperties;
            }

            //Getters
            public FrameImages getFrameImages()
            {
                return this.frameImages;
            }

            public FrameProperties getFrameProperties()
            {
                return this.frameProperties;
            }
        }
    
}
