using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Family_Rewards_Bank.Models
{
    public class NewsModel
    {
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("id")]
        public string Id { get; set; }
        [XmlElement("updated")]
        public DateTime Updated { get; set; }
        [XmlElement("summary")]
        public string Description { get; set; }
        [XmlElement("link")]
        public string Url { get; set; }
        [XmlElement("author")]
        public string Name { get; set; }



    }
}
