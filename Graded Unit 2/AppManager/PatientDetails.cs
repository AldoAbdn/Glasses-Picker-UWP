using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    partial class AppManager
    {
        partial class Patient
        {
            /// <summary>
            /// Holds details about patients
            /// </summary>
            public class PatientDetails
            {
                //Attributes
                public String gender { get; set; }
                public int age { get; set; }
                public String faceShape { get; set; }
                public List<String> colours { get; set; }
                public List<String> materials { get; set; }
                public List<String> types { get; set; }
                public bool isSunglass { get; set; }
                public String sideLength { get; set; }
                public String faceWidth { get; set; }

                //Constructor
                public PatientDetails()
                {
                }
            }
        }
    }
}
