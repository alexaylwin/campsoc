using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;


namespace aspx_site.Models
{
    public class FacebookPageModel
    {
        private JToken jToken;

        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }

        public FacebookPageModel(JObject jsonModel)
        {
            Name = (string)jsonModel["name"];
            AccessToken = (string)jsonModel["access_token"];
            Category = (string)jsonModel["category"];
            Id = (string)jsonModel["id"];
        }
    }
}