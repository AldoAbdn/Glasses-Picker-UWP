using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    partial class AppManager
    {
        /// <summary>
        /// Represets a patient(the user)
        /// Holds all the users choices in the framePicker section and images used for VirtualTryOn
        /// </summary>
        public partial class Patient
        {
            //Attributes
            private Prescription prescription;
            private List<Frame> frames;
            private PatientDetails patientDetails;
            private PatientImages patientImages;

            //Constructor
            public Patient()
            {
                patientDetails = new PatientDetails();
            }

            //Used by appmanager to add details 
            public void addPatientDetail(Detail detailType, Object detailValue)
            {
                if (detailType == Detail.Gender)
                {
                    this.patientDetails.gender = (String)detailValue;
                }
                else if (detailType == Detail.Age)
                {
                    this.patientDetails.age = (int)detailValue;
                }
                else if (detailType == Detail.FaceShape)
                {
                    this.patientDetails.faceShape = (String)detailValue;
                }
                else if (detailType == Detail.Colour)
                {
                    this.patientDetails.colours = (List<String>)detailValue;
                }
                else if (detailType == Detail.Material)
                {
                    this.patientDetails.materials = (List<String>)detailValue;
                }
                else if (detailType == Detail.Type)
                {
                    this.patientDetails.types = (List<String>)detailValue;
                }
                else if (detailType == Detail.Sunglass)
                {
                    this.patientDetails.isSunglass = (bool)detailValue;
                }
                else if (detailType == Detail.SideLength)
                {
                    this.patientDetails.sideLength = (String)detailValue;
                }
                else if (detailType == Detail.FaceWidth)
                {
                    this.patientDetails.faceWidth = (String)detailValue;
                }
            }

            //Setters
            public void setPatientDetails(PatientDetails patientDetails)
            {
                this.patientDetails = patientDetails;
            }

            public void setPrescription(Prescription prescription)
            {
                this.prescription = prescription;
            }

            public void setFrames(List<Frame> frames)
            {
                this.frames = frames;
            }

            public void setPatientImages(PatientImages patientImages)
            {
                this.patientImages = patientImages;
            }

            //Getters
            public PatientImages getPatientImages()
            {
                return this.patientImages;
            }

            public PatientDetails getPatientDetails()
            {
                return this.patientDetails;
            }

            public Prescription getPrescription()
            {
                return this.prescription;
            }
        }
    }
}
