using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser
{
    public abstract class AbstractData
    {
        //xml, csv etc
        //public string Type { get; set; }
        public string FilePath { get; set; }
        public List<AbstractItem> Items { get; set; }

        public abstract void Parse();
        
    }

    
}
