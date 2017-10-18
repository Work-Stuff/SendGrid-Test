using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SendGrid_Test.Responses
{
    [Serializable]
    public class SpamEmailResponse : IResponse
    {
        private long created;
        private string email;
        private string ip;

        public SpamEmailResponse(long created, string email, string ip)
        {
            this.created = created;
            this.email = email;
            this.ip = ip;
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
        public string Ip
        {
            get => ip;
            set => ip = value;
        }
    }
}
