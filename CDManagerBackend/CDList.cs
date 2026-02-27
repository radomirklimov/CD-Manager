using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CDManagerBackend
{
    public class CDList
    {
        public string Name { get; set; }

        private List<CD> cDs;
        private int currentIndex;

        public CDList(string name)
        {
            Name = name;
            cDs = new List<CD>();
            currentIndex = -1;   
        }

        public void Add(CD cd)
        {
            cDs.Add(cd);
        }

        public void Remove() {
            cDs.RemoveAt(currentIndex);

            if (currentIndex >= cDs.Count)
                currentIndex = cDs.Count - 1;
        }

        public List<CD> FindByPerformer(string peformer)
        {
            List<CD> list = new List<CD>();
            for (int i = 0; i < cDs.Count; i++)
            {
                if (cDs[i].Performer == peformer)
                {
                    list.Add(cDs[i]);
                }
            }
            return list;
        }

        public List<CD> FindByTitle(string title)
        {
            List<CD> list = new List<CD>();
            for (int i = 0; i < cDs.Count; i++)
            {
                if (cDs[i].Title == title)
                {
                    list.Add(cDs[i]);
                }
            }
            return list;
        }

        public CD? First()
        {
            if (cDs.Count == 0)
                return null;

            currentIndex = 0;
            return cDs[currentIndex];
        }

        public CD? Next()
        {
            if (currentIndex + 1 >= cDs.Count)
                return null;

            if (currentIndex >= cDs.Count)
                currentIndex = 0;

            currentIndex++;
            return cDs[currentIndex];
        }

        public CD? GetCD()
        {
            if (currentIndex < 0 || currentIndex >= cDs.Count)
                return null;

            return cDs[currentIndex];
        }

        public void Sort()
        {
            cDs.Sort();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, cDs);
        }

        // =========================
        // JSON
        // =========================

        public void ExportJson(string path)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(cDs, options);
            File.WriteAllText(path, json);
        }

        public void ImportJson(string path)
        {
            string json = File.ReadAllText(path);
            var fileCDs = JsonSerializer.Deserialize<List<CD>>(json);

            if (fileCDs != null)
            {
                cDs.Clear();
                cDs.AddRange(fileCDs);
            }
        }

        // =========================
        // CSV
        // =========================

        public void ExportCsv(string path)
        {
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(cDs);
        }

        public void ImportCsv(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var fileCDs = csv.GetRecords<CD>().ToList();

            cDs.Clear();
            cDs.AddRange(fileCDs);
        }

        // =========================
        // YAML
        // =========================

        public void ExportYaml(string path)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            string yaml = serializer.Serialize(cDs);
            File.WriteAllText(path, yaml);
        }

        public void ImportYaml(string path)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            string yaml = File.ReadAllText(path);
            var fileCDs = deserializer.Deserialize<List<CD>>(yaml);

            if (fileCDs != null)
            {
                cDs.Clear();
                cDs.AddRange(fileCDs);
            }
        }
    }
}
