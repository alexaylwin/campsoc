using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace aspx_site.Models
{
    public class FacebookUserModel
    {
        public string Name {get; set;}
        public string Id { get; set; }

        public FacebookUserModel(JObject jsonModel)
        {
            Name = (string)jsonModel["name"];
            Id = (string)jsonModel["id"];
        }
        
    }
}