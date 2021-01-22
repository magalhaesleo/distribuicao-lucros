using distribuicao_lucros_domain.Helpers;

using System;
using System.Diagnostics.CodeAnalysis;

namespace distribuicao_lucros_infra.Helpers
{
    [ExcludeFromCodeCoverage]
    public class DatetimeHelper : IDateTimeHelper
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
