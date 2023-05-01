using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using ACDM.Bindings.Hooks;

namespace ACDM.Bindings.DataSheet
{
    public class DataSheet
    {
        public static JObject obj;
        public static IDictionary<string, string> dict;

        public static void ExecuteJsonQuery(string sheetname, string iteration)
        {
            ArrayList sheet = new ();
            String currentTc = HookInitialization.scenarioName;
            dict = new Dictionary<string, string>();
            String path = sheetname + "." + currentTc + "_" + iteration;
            String AbsoluteDirectoryPath = (HookInitialization.strRelativepath).Replace("\\", "/");
            using StreamReader file = File.OpenText(AbsoluteDirectoryPath);
            using JsonTextReader reader = new (file);
            obj = (JObject)JToken.ReadFrom(reader);
            foreach (JProperty property in obj.Properties())
            {
                sheet.Add(property.Name);

            }
            if (sheet.Cast<String>().Any(tc => tc.Equals(sheetname)))
            {
                JToken token = GetPropValue(path);
                foreach (JProperty property in ((JObject)token).Properties())
                {
                    dict.Add(property.Name, property.Value.ToString());

                }
            }
        }

        public static string GetData(string fieldname)
        {
            return dict[fieldname];
        }

        public static JToken GetPropValue(string path)
        {
            var val = obj.SelectToken(path);
            return val;
        }
    }
}