using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2.Models
{
    /// <summary>
    /// Used to hold data for MaterialItem Custom control using in MaterialPage
    /// </summary>
    public class Material
    {
        public String material { get; set; }
        public String imgSrc { get; set; }

        public Material(String material, String imgSrc)
        {
            this.material = material;
            this.imgSrc = imgSrc;
        }
    }
}
