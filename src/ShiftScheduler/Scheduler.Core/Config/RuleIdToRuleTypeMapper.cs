using Scheduler.Core.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Config
{
    
     /// <summary>
    /// Singleton that implements configuration to classify rules into a Rule Scope
    /// </summary>
    public sealed class RuleIdToRuleTypeMapper
    {
        private static readonly Lazy<RuleIdToRuleTypeMapper> lazy =
            new Lazy<RuleIdToRuleTypeMapper>(() => new RuleIdToRuleTypeMapper());

        private List<RuleIdMapItem> m_MappingTable;

        public static RuleIdToRuleTypeMapper Instance { get { return lazy.Value; } }

        private RuleIdToRuleTypeMapper()
        {
            m_MappingTable = new List<RuleIdMapItem>();
            MapValues();
        }

        private void MapValues()
        {
            m_MappingTable.Add(new RuleIdMapItem() { MappedTo = RuleType.HardRule, RuleId = 2, ConstraintType = ConstraintType.ShiftRuleValue});
            m_MappingTable.Add(new RuleIdMapItem() { MappedTo = RuleType.HardRule, RuleId = 4, ConstraintType = ConstraintType.ShiftRuleValue });
            m_MappingTable.Add(new RuleIdMapItem() { MappedTo = RuleType.HardRule, RuleId = 7, ConstraintType = ConstraintType.ShiftRuleValue });
            m_MappingTable.Add(new RuleIdMapItem() { MappedTo = RuleType.SoftRule,  ConstraintType = ConstraintType.TimeOffRequest });
        }

        public bool TryGetRuleType(Func<RuleIdMapItem, bool> predicate, out RuleType ruleType)
        {
            bool itemFound = false;
            ruleType = default(RuleType);
            RuleIdMapItem ruleMapItem = null;

            ruleMapItem = m_MappingTable.SingleOrDefault(predicate);
            
            if(ruleMapItem != null)
            {
                itemFound = true;
                ruleType = ruleMapItem.MappedTo;
            }


            return itemFound;
        }
    }
}
