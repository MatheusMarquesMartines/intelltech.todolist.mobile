using SQLite.Net.Attributes;

namespace ToDoList.Models
{

    public class Activity : BaseDataObject
    {
        [PrimaryKey] [AutoIncrement]
        public long Id { get; set; }

        string title = string.Empty;
        public string Titulo
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string description = string.Empty;
        public string Descricao
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        string date = string.Empty;
        public string DataHora
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }

        public bool Synchronized { get; set; }

        public bool Concluida { get; set; }

        public long IdFake { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", Id, Titulo, Descricao, DataHora, Concluida, GUID);
        }
    }
}
