using System.Runtime.Serialization;

namespace BLL.Enum
{
    public enum PurchaseStatus
    {
        [EnumMember(Value = "opened")]
        Opened,

        [EnumMember(Value = "returned")]
        Returned,

        [EnumMember(Value = "canceled")]
        Canceled,

        [EnumMember(Value = "completed")]
        Completed
    }
}
