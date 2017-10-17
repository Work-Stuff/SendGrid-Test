using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SendGrid_Test.Responses
{
    [Serializable]
    public class BounceResponse : IResponse
    {
        private long created;
        private string email;
        private string reason;
        private string version;

        public BounceResponse()
        {
            
        }

        private BounceResponse(long created, string email, string reason, string version)
        {
            this.created = created;
            this.email = email;
            this.reason = reason;
            this.version = version;
        }
        [JsonProperty]
        public long Created
        {
            get => created;
            set => created = value;
        }

        [JsonProperty]
        public string Email
        {
            get => email;
            set => email = value;
        }

        [JsonProperty]
        public string Reason
        {
            get => reason;
            set => reason = value;
        }

        [JsonProperty]
        public string Version
        {
            get => version;
            set => version = value;
        }
    }
}
