using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    /// <summary>
    /// This holds data from filter and sort dropdowns
    /// This is used to generate frames for a frameBrowserPage
    /// </summary>
    public class BrowserDetails
        {
            //Attributes
            public List<String> gender { get; set; }
            public List<String> faceShape { get; set; }
            public List<String> colours { get; set; }
            public List<String> materials { get; set; }
            public List<String> types { get; set; }
            public bool isSunglass { get; set; }
            public List<String> sideLengths { get; set; }
            public List<String> faceWidths { get; set; }
            public List<String> brands { get; set; }
            public bool isVari { get; set; }
            public List<String> sortWords { get; set; }

            //Constructor
            public BrowserDetails()
            {
                gender = new List<String>() { "Male", "Female" };
                faceShape = new List<String>() { };
                colours = new List<String>() { };
                materials = new List<String>() { };
                types = new List<String>() { };
                isSunglass = false;
                sideLengths = new List<String>() { };
                faceWidths = new List<String>() { };
                brands = new List<String>() { };
                isVari = false;
                sortWords = new List<String>() { };
            }
        }
    
}
