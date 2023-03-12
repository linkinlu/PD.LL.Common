using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.LYY.UtilityLib
{
    [Flags]
    public enum StringFilter
    {
        /// <summary>
        /// Alpha characters
        /// </summary>
        Alpha = 1,

        /// <summary>
        /// Numeric characters
        /// </summary>
        Numeric = 2,

        /// <summary>
        /// Numbers with period, basically allows for decimal point
        /// </summary>
        FloatNumeric = 4,

        /// <summary>
        /// Multiple spaces
        /// </summary>
        ExtraSpaces = 8
    }
}
