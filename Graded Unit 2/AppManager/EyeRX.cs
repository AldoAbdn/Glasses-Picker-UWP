using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    public partial class Prescription
    {
        /// <summary>
        /// Model to hold data about one eye
        /// Evaluates prescription to get other information
        /// </summary>
        public class EyeRX
        {
            //Attributes
            public double sph { get; }
            public double cyl { get; }
            public int axis { get; }
            public double nearAdd { get; }
            public double intAdd { get; }
            public double prism { get; }
            public String prismBase { get; }
            public String VADistance { get; }
            public String VANear { get; }

            //Basic Constructor
            public EyeRX()
            {
                this.sph = 0.00;
                this.cyl = 0.00;
                this.axis = 0;
                this.nearAdd = 0.00;
                this.intAdd = 0.00;
                this.prism = 0.00;
                this.prismBase = "";
                this.VADistance = "";
                this.VANear = "";
            }

            //Constructor
            public EyeRX(double sph, double cyl, int axis, double nearAdd, double intAdd, double prism, String prismBase, String VADistance, String VANear)
            {
                this.sph = sph;
                this.cyl = cyl;
                this.axis = axis;
                this.nearAdd = nearAdd;
                this.intAdd = intAdd;
                this.prism = prism;
                this.prismBase = prismBase;
                this.VADistance = VADistance;
                this.VANear = VANear;
            }
        }
    }
}
