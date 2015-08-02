using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules
{
    public static class ShiftRuleExtensions
    {
        public static IShiftRule SetEnabledProperty(this IShiftRule rule)
        {
            if (rule == null)
                return rule;
            rule.Enabled = rule.IsRuleEnabled();
            return rule;
        }

        public static bool IsRuleEnabled(this IShiftRule rule)
        {
            bool result = false;
            switch (rule.Id)
            {
                case 2:
                    if (Properties.Settings.Default.RULE_MAX_SHIFTS_Enabled)
                        result = true;
                    break;
                case 4:
                    if (Properties.Settings.Default.RULE_MIN_SHIFTS_Enabled)
                        result = true;
                    break;
                case 7:
                    if (Properties.Settings.Default.RULE_EMPLOYEES_PER_SHIFT_Enabled)
                        result = true;
                    break;
                default:
                    if (!Properties.Settings.Default.IgnoreTimeOffRequests)
                        result = true;
                    break;
            }

            return result;
        }
    }
}
