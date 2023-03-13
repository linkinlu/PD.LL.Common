using System;
using System.Collections.Generic;
using System.Text;

namespace PD.LYY.UtilityLib.Audit
{
    //标记接口
    public interface IAudit
    {

    }

    public interface ICreationAudited<TKey> : IAudit
    {
        /// <summary>
        /// 创建人标识
        /// </summary>
        TKey CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreationTime { get; set; }
    }



    public interface IModificationAudited<TKey> : IAudit
    {
        /// <summary>
        /// 创建人标识
        /// </summary>
        TKey ModifyId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? UpdateTime { get; set; }
    }


}
