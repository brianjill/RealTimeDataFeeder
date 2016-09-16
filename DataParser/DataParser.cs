﻿using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading;
using System.Xml;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.IO;
using System.Linq;

namespace DataParser
{
    public class DataParser<T> where T:AbstractData
    {
        private readonly T _data;
        private TimeSpan _elapsedSpan;
        
        public DataParser(T data)
        {
            _data = data;
        }

        public void Parse()
        {
            _data.Parse();
        }

        public string TransformToJson()
        {
            while (_data.Items.Count > 1)
            {
                _elapsedSpan = new TimeSpan(_data.Items[1].Ticks - _data.Items[0].Ticks);

                var list = _data.Items.FindAll(e => e.Ticks >= DateTime.Now.Ticks);

                var miliseconds = Convert.ToInt32(_elapsedSpan.TotalMilliseconds);
                Thread.Sleep(miliseconds);

                var json = new JavaScriptSerializer().Serialize(list[0]);

                return json;

            }
        }
    }
}
