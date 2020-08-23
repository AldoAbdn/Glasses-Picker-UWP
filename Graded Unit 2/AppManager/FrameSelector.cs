using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using static Graded_Unit_2.Frame;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.UI.Core;
using static Graded_Unit_2.AppManager.Patient;

namespace Graded_Unit_2
{
    partial class AppManager
    {
        /// <summary>
        /// Holds all the frames in the app
        /// Generates lists of frames based on BrowserDetails and PatientDetails objects
        /// Used for FrameBrowserPage and ResultsPage
        /// Hidden inside of AppManager and accessed through app manager so that no frames are edited or deleted
        /// </summary>
        partial class FrameSelector
        {
            //This holds all the frames in the app
            private List<Frame> frames;

            //Constructor
            public FrameSelector(List<Frame> frames)
            { 
                this.frames = frames;
            }

            //Overloaded function
            //Generates a list based on patient object
            public List<Frame> generateFrameList(Patient patient)
            {
                List<Frame> frames = getFramesByQuery(patient);
                return frames;
            }
            //Generates a list based on browser details object
            public List<Frame> generateFrameList(BrowserDetails browserDetails)
            {
                List<Frame> frames = getFramesByQuery(browserDetails);
                return frames;
            }

            //LINQ Query for patient object
            private List<Frame> getFramesByQuery(Patient patient)
            {
                var patientDetails = patient.getPatientDetails();
                var patientPrescription = patient.getPrescription();
                var frames = (from frame in this.frames select frame);
                frames = filterFramesByPatientDetails(frames, patientDetails);
                if (frames.Count() == 0)
                {
                    frames = filterFrameByPatientDetailsSpecialOrder(frames, patientDetails);
                    frames = filterFrameByPrescription(frames, patientPrescription);
                    if (frames.Count() == 0)
                    {
                        frames = filterFrameByPatientDetailsSpecialOrder(frames, patientDetails);
                    }
                }
                return frames.ToList<Frame>();
            }

            //Basic filtering for patient
            private IEnumerable<Frame> filterFramesByPatientDetails(IEnumerable<Frame> frames,PatientDetails patientDetails)
            {
                //Selection
                frames = (from frame in this.frames
                                      where frame.frameProperties.gender == patientDetails.gender &&
                                            frame.frameProperties.ageKeywords.Contains(convertAgeToKeyword(patientDetails.age)) &&
                                            frame.frameProperties.patientFaceWidth == patientDetails.faceWidth &&
                                            frame.frameProperties.patientSideLength == patientDetails.sideLength &&
                                            frame.frameProperties.faceShapes.Contains(patientDetails.faceShape)
                                      select frame);

                //Ordering
                frames = frames.OrderByDescending(frame => patientDetails.materials.Contains(frame.frameProperties.material))
                                .ThenByDescending(frame => patientDetails.colours.Contains(frame.frameProperties.colour))
                                .ThenByDescending(frame => patientDetails.types.Contains(frame.frameProperties.type));
                
                //Sunglass ordering is done last
                if (patientDetails.isSunglass)
                    frames = frames.OrderByDescending(frame => frame.frameProperties.isSunglass);

                return frames;
            }

            //Specific filtering for patient, used when basic returns 0 results
            private IEnumerable<Frame> filterFrameByPatientDetailsSpecialOrder(IEnumerable<Frame> frames, PatientDetails patientDetails)
            {
                //Selection
                frames = (from frame in this.frames
                              where frame.frameProperties.gender == patientDetails.gender &&
                                    frame.frameProperties.ageKeywords.Contains(convertAgeToKeyword(patientDetails.age))
                              select frame);

                //Ordering
                frames = frames.OrderByDescending(frame => frame.frameProperties.faceShapes.Contains(patientDetails.faceShape))
                               .ThenByDescending(frame => frame.frameProperties.patientFaceWidth == patientDetails.faceWidth)
                               .ThenByDescending(frame => frame.frameProperties.patientSideLength == patientDetails.sideLength)
                               .ThenByDescending(frame => patientDetails.types.Contains(frame.frameProperties.type))
                               .ThenByDescending(frame => patientDetails.materials.Contains(frame.frameProperties.material))
                               .ThenByDescending(frame => patientDetails.colours.Contains(frame.frameProperties.colour));

                //Sunglass ordering is done last
                if (patientDetails.isSunglass)
                    frames = frames.OrderByDescending(frame => frame.frameProperties.isSunglass);
                    
                return frames;
            }

            //Filters frames based on prescription
            private IEnumerable<Frame> filterFrameByPrescription(IEnumerable<Frame> frames,Prescription patientPrescription)
            {
                //Filtering based on prescription 
                if (patientPrescription != null)
                {
                    if (patientPrescription.isPrescriptionHigh())
                        frames = frames.Where(frame => frame.frameProperties.type == "Fullrim");
                    else
                        frames = frames.Where(frame => frame.frameProperties.type == "Fullrim" || frame.frameProperties.type == "Supra" || frame.frameProperties.type == "Rimless");

                    if (patientPrescription.isPrescriptionVari())
                        frames = frames.Where(frame => frame.frameProperties.vari);
                    //This will skip filtering by prescription if it means there will be 0 results
                }
                return frames;
            }

            //LINQ Query for browser details object
            private List<Frame> getFramesByQuery(BrowserDetails browserDetails)
            {
                //Select
                var frames = from frame in this.frames
                             where browserDetails.gender.Contains(frame.getFrameProperties().gender) &&
                                   //frame.getFrameProperties().faceShapes.All(temp => browserDetails.faceShape.Contains(temp)) &&
                                   browserDetails.faceShape.Any(temp => frame.getFrameProperties().faceShapes.Contains(temp)) &&
                                   browserDetails.colours.Contains(frame.getFrameProperties().colour) &&
                                   browserDetails.materials.Contains(frame.getFrameProperties().material) &&
                                   browserDetails.types.Contains(frame.getFrameProperties().type) &&
                                   browserDetails.sideLengths.Contains(frame.getFrameProperties().patientSideLength) &&
                                   browserDetails.faceWidths.Contains(frame.getFrameProperties().patientFaceWidth)
                             select frame;
                //Only filter by vari if true, else just all frames are returned
                //I.e when you filter by vari you get all suitable vari frames and 
                //when you dont you get all frames
                if (browserDetails.isVari) 
                    frames = from frame in frames
                             where frame.getFrameProperties().vari == browserDetails.isVari
                             select frame;
                if (browserDetails.isSunglass)
                    frames = from frame in frames
                             where frame.getFrameProperties().isSunglass == browserDetails.isSunglass
                             select frame;

                //Sorting
                foreach (var sortWord in browserDetails.sortWords)
                {
                    frames = orderBySortWord(frames, sortWord);
                }

                return frames.ToList<Frame>();
            }

            //Orders frame list for browser details based on currently selected sort word
            private IEnumerable<Frame> orderBySortWord(IEnumerable<Frame> list, String sortWord)
            {
                if (sortWord == "Brand")
                    list = list.OrderBy(frame => frame.getFrameProperties().brand);
                else if (sortWord == "Colour")
                    list = list.OrderBy(frame => frame.getFrameProperties().colour);
                else if (sortWord == "Type")
                    list = list.OrderBy(frame => frame.getFrameProperties().type);
                else if (sortWord == "Width")
                    list = list.OrderBy(frame => frame.getFrameProperties().eyeSize);
                else if (sortWord == "Side Length")
                    list = list.OrderBy(frame => frame.getFrameProperties().sideLength);
                else if (sortWord == "Pole Number")
                    list = list.OrderBy(frame => frame.getFrameProperties().poleNo);
                return list;
            }

            //Used for patient, converts the patients age to the relavent keyword
            private String convertAgeToKeyword(int age)
            {
                if (age <= 45)
                {
                    return "Modern";
                }
                else
                {
                    return "Traditional";
                }
            }
        }
    }
}
