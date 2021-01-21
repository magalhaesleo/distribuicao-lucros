using System;

namespace distribuicao_lucros_domain.Features.ProfitSharing
{
    public class InsufficientValueProfitSharingException : Exception
    {
        public InsufficientValueProfitSharingException(string message) : base(message)
        {
        }
    }
}
