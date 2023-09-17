using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PD.LL.Common.Model
{
    public enum Operator
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        Greater,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        GreaterEqual,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        Less,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        LessEqual,
        /// <summary>
        /// 头匹配
        /// </summary>
        [Description("头匹配")]
        Starts,
        /// <summary>
        /// 尾匹配
        /// </summary>
        [Description("尾匹配")]
        Ends,
        /// <summary>
        /// 模糊匹配
        /// </summary>
        [Description("模糊匹配")]
        Contains,
        /// <summary>
        /// In
        /// </summary>
        [Description("In")]
        In,
        /// <summary>
        /// Not In
        /// </summary>
        [Description("Not In")]
        NotIn,
    }

    public enum ConditionUnit
    {
        Year,
        Month,
        Day,
    }

    public enum ConditionType
    {
        DateTime,
        Boolean,
        PlainText,
        Number
    }
    
    
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
