using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Utilities
{
    public class LoadJson
    {
        public List<JObject> JsonObjects { get; set; }
        public LoadJson(string source)
        {
            JsonObjects = new List<JObject>();
            using (StreamReader r = new StreamReader(source))
            {
                foreach (var j in JArray.Parse(r.ReadToEnd())) JsonObjects.Add(JObject.Parse(j.ToString()));
            }
        }
    }
}
