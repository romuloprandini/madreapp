using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadreApp.Models
{
    public class User
    {
        [JsonProperty("nome")]
        public string Name { get; set; }
        [JsonProperty("telefone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonIgnore]
        public string FirebaseUid { get; set; }
    }
}
