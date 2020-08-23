using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2.Models
{
    /// <summary>
    /// Used to hold data for TypeItem custom control used on FrameTypePage
    /// </summary>
    public class FrameType
    {
        public String type { get; set; }
        public String imgSrc { get; set; }
        

        public FrameType(String type, String imgSrc)
        {
            this.type = type;
            this.imgSrc = imgSrc;
           
        }
    }
}
