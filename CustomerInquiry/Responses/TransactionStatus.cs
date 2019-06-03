using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CustomerInquiry.Responses
{
    public enum TransactionStatus
    {
        [EnumMember(Value = "Success")]
        Success,
        [EnumMember(Value = "Failed")]
        Failed,
        [EnumMember(Value = "Cancaled")]
        Cancaled
    }
}
