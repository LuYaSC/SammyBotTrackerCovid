using System.Runtime.Serialization;

namespace TC.Core.Connectors.Models
{
    [DataContract]
    public class ResultContract<BODY>
    {
        [DataMember(Name = "isOk")]
        public bool IsOk { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "body")]
        public BODY Body { get; set; }
    }
}