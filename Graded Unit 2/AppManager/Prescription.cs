using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    /// <summary>
    /// Holds patients prescription for both eyes
    /// </summary>
    public partial class Prescription
    {
        //Attributes
        private EyeRX rightEye { get; }
        private EyeRX leftEye { get; }
        private bool isHigh;
        private bool isPrism;
        private bool isAstig;
        private bool isVari;

        //Constructor
        public Prescription(EyeRX rightEye, EyeRX leftEye)
        {
            this.rightEye = rightEye;
            this.leftEye = leftEye;
            evaluatePrescription();
        }

        //Sets important attributes based on current prescripton
        private void evaluatePrescription()
        {
            //If right eye or left eye has a sph value of greater thatn 4 or less than -5
            if (rightEye.sph > 4 || leftEye.sph > 4 || rightEye.sph < -5 || leftEye.sph < -5)
                isHigh = true;
            else
                isHigh = false;
            //If right eye or left eye has a prism greater than 1
            if (rightEye.prism > 0 || leftEye.prism > 0)
                isPrism = true;
            else
                isPrism = false;
            //If right eye or left eye has a cyl 
            if (rightEye.cyl > 0 || leftEye.cyl > 0)
                isAstig = true;
            else
                isAstig = false;
            //If prescription has an add
            if (rightEye.nearAdd > 0 || leftEye.nearAdd > 0 || rightEye.intAdd > 0 || leftEye.intAdd > 0)
                isVari = true;
            else
                isVari = false;
        }

        //Getters
        public EyeRX getRightEye()
        {
            return this.rightEye;
        }

        public EyeRX getLeftEye()
        {
            return this.leftEye;
        }

        public bool isPrescriptionHigh()
        {
            return this.isHigh;
        }

        public bool isPrescriptionPrism()
        {
            return this.isPrism;
        }

        public bool isPrescriptionAstig()
        {
            return this.isAstig;
        }

        public bool isPrescriptionVari()
        {
            return this.isVari;
        }      
    }   
}
