using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SendGrid_Test.Responses
{
    [Serializable]
    public class InvalidEmailResponse : IResponse
    {
        private long created;
        private string email;
        private string reason;

        public InvalidEmailResponse()
        {

        }

        private InvalidEmailResponse(long created, string email, string reason)
        {
            this.created = created;
            this.email = email;
            this.reason = reason;
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
    }
}
