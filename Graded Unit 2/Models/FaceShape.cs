using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2.Models
{
    /// <summary>
    /// Used to hold data for faceshapeitem used in faceshapepage
    /// </summary>
    public class FaceShape
    {
        public String faceshape { get; set; }
        public String imgSrc { get; set; }

        public FaceShape(String faceshape, String imgSrc)
        {
            this.faceshape = faceshape;
            this.imgSrc = imgSrc;
        }
    }
}

