using System;
using System.Collections.Generic;
using System.Text;

namespace PD.LYY.UtilityLib.Audit
{
    public interface IDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
