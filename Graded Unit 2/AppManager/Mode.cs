using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_2
{
    /// <summary>
    /// These are the 3 modes that the app can possibly be in
    /// This is used by nav page to choose what page to navigate to
    /// Browser = FrameBrowserPage
    /// FrameSelector = FramePickerPage
    /// Results = ResultsPage
    /// </summary>
    enum Mode
    {
        Browse, FrameSelector, Results
    };
}
