using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2.Models
{
    /// <summary>
    /// Model to hold data for ColourItem custom control. Used on ColourPage
    /// </summary>
    public class Colour
    {
        public String colour { get; set; }
        public String text { get; set; }

        public Colour(String colour, String text)
        {
            this.colour = colour;
            this.text = text;
        }
    }

    /// <summary>
    /// Model for sunglass custom control. Inherits from Colour
    /// </summary>
    public class Sunglass : Colour
    {
        public bool value { get; set; }

        public Sunglass(String colour, String text, bool value) : base(colour, text)
        {
            this.value = value;
        }
    }
}
