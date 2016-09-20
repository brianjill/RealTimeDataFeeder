using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataParser.Interface;

namespace DataParser
{
    public abstract class AbstractData: IParse
    {
        //xml, csv etc
        public string ItemType { get; set; }
        public string FilePath { get; set; }
        public string BasePath { get; set; }
        
        public abstract void Parse();

        public List<Item> Items { get; set; }

        public float GetDataAsFloat(string text)
        {
            return !string.IsNullOrEmpty(text) && text != "NULL" ? float.Parse(text, System.Globalization.CultureInfo.InvariantCulture) : 0;
        }

        public AbstractData()
        {
            BasePath = ConfigurationManager.AppSettings["XmlDataFilePath"];
        }
    }

    
}
