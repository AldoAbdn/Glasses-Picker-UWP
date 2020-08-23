using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    partial class Frame
    {
        /// <summary>
        /// Model holds details about a frame
        /// Public attributes to make data binding easier for FrameViewerPage
        /// </summary>
        public class FrameProperties
        {
            //Attributes
            public String poleNo { get; set; }
            public String brand { get; set; }
            public String model { get; set; }
            public String material { get; set; }
            public String type { get; set; }
            public String colour { get; set; }
            public String gender { get; set; }
            public int eyeSize { get; set; }
            public int bridgeSize { get; set; }
            public int sideLength { get; set; }
            public long barcode { get; set; }
            public String patientSideLength { get; set; }
            public String patientFaceWidth { get; set; }
            public bool isSunglass { get; set; }
            public bool vari { get; set; }
            public String[] faceShapes { get; set; }
            public String[] ageKeywords { get; set; }

            //Constructor
            public FrameProperties(String poleNo, String brand, String model, String material, String type, String colour, String gender, int eyeSize, int bridgeSize, int sideLength, long barcode, String patientSideLength, String patientFaceWidth, bool isSunglass, bool vari, String[] faceShapes, String[] ageKeywords)
            {
                this.poleNo = poleNo;
                this.brand = brand;
                this.model = model;
                this.material = material;
                this.type = type;
                this.colour = colour;
                this.gender = gender;
                this.eyeSize = eyeSize;
                this.bridgeSize = bridgeSize;
                this.sideLength = sideLength;
                this.barcode = barcode;
                this.patientSideLength = patientSideLength;
                this.patientFaceWidth = patientFaceWidth;
                this.isSunglass = isSunglass;
                this.faceShapes = faceShapes;
                this.vari = vari;
                this.ageKeywords = ageKeywords;
            }
        }
    }
            
        
    
}
