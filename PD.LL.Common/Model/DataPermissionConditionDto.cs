using System.Collections.Generic;
using System.Linq.Expressions;

namespace PD.LYY.UtilityLib.Model
{
    public class DataPermissionConditionDto : DataPermissionCondition
    {
        public IList<DataPermissionConditionDto> Conditions { get; set; } = new List<DataPermissionConditionDto>();
        public string ConditionName { get; set; }
        public ConditionType ConditionType { get; set; }
        public string[] ConditionValues { get; set; }
    }

    public class DataPermissionCondition 
    {
        public string ConditionId { get; set; }
        public Operator Operator { get; set; }
      
        public ConditionUnit ConditionUnit { get; set; }
        public BinaryExpression GroupOperator { get; set; }
        public string ParentId { get; set; }
 
        public bool IsConditionGroup { get; set; }
    }

    public enum ConditionUnit
    {
        Year,
        Month,
        Day,
    }

    public enum ConditionType
    {
        DateTime ,
        Boolean,
        PlainText,
        Number
    }








}