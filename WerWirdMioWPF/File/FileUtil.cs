using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WerWirdMioWPF.Model;

namespace WerWirdMioWPF.File
{
    internal class FileUtil
    {

        public static async Task WriteQuestions(string filePath, List<Question> products)
        {


            String json = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

        }
        public static async Task<List<Question>> ReadJsonFromFileAsync(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                List<Question> items = JsonConvert.DeserializeObject<List<Question>>(json);
                return items;
            }
        }
    }
}

