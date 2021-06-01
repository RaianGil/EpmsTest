using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace UtilitiesComponents.RES
{
    public class checked_return
    {
        public checked_return() { }
        public static int intValueS(CheckBox chkInput)
        {
            int intOutput = 0;
            if (chkInput.Checked)
                intOutput = 1;
            return intOutput;
        }
        public int intValue(CheckBox chkInput)
        {
            int intOutput = 0;
            if (chkInput.Checked)
                intOutput = 1;
            return intOutput;
        }

    }
}