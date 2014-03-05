using System.IO;
using SpellMe.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace SpellMe
{
    public class DictionaryService
    {
        private const String ApiUrl =
            //"http://www.dictionaryapi.com/api/v1/references/sd2/xml/{0}?key=b911bc58-a92c-4345-bf21-ce61f982db4a";
            "http://www.dictionaryapi.com/api/v1/references/collegiate/xml/{0}?key=971904ea-5c23-41de-b66b-d994a79e3a35";


        public void GetResponse(String word)
        {
            word = "bunker";
            var url = String.Format(ApiUrl, word);
            var exit = false;
            using (var reader = new XmlTextReader(url))
            {
                var serializer = new XmlSerializer(typeof(GeneratedSchema.collegiate));
                var entity = serializer.Deserialize(reader) as GeneratedSchema.collegiate;
                MessageBox.Show("here");
                
                //while (reader.Read())
                //{
                //    if (exit) break;
                //    if (reader.NodeType != XmlNodeType.Element) continue;
                //    switch (reader.Name)
                //    {
                //        case "dt":
                //            var text = reader.ReadInnerXml().Trim(':');
                //            if (text.Contains(word))
                //                MessageBox.Show(text);
                //            break;
                //        case "suggestion":
                //            exit = true;
                //            break;
                //    }
                //}
                //reader.Close();
                //if (!exit)
                //    MessageBox.Show("Sorry, Could not find usage");
            }
        }
    }
}
