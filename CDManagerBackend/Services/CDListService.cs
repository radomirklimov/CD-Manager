namespace CDManagerBackend.Services
{
    public class CDListService
    {
        private readonly CDList cdList;

        public CDListService()
        {
            cdList = new CDList("My CD Collection");
        }

        public CD? GetCurrent() => cdList.GetCD();

        public CD? GetFirst() => cdList.First();

        public CD? GetNext() => cdList.Next();

        public void AddCD(CD cd) => cdList.Add(cd);

        public void RemoveCurrent() => cdList.Remove();

        public List<CD> FindByPerformer(string performer) => cdList.FindByPerformer(performer);

        public List<CD> FindByTitle(string title) => cdList.FindByTitle(title);

        public void Sort() => cdList.Sort();

        public void ExportJson(string path) => cdList.ExportJson(path);

        public void ImportJson(string path) => cdList.ImportJson(path);

        public void ExportCsv(string path) => cdList.ExportCsv(path);

        public void ImportCsv(string path) => cdList.ImportCsv(path);

        public void ExportYaml(string path) => cdList.ExportYaml(path);

        public void ImportYaml(string path) => cdList.ImportYaml(path);
    }
}
