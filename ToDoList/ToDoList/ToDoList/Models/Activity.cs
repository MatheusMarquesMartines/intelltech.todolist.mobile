using SQLite.Net.Attributes;

namespace ToDoList.Models
{
    public class Activity : BaseDataObject
    {

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
       
       // public int posSituation { get; set; }

        //private string[] situation = { "Não iniciada", "Em andamento", "Concluída" };

  //      public string getSituation()
    //    {
      //      return this.situation[posSituation];
        //}

        //public void setSituation(int posSituation)
        //{
        //    this.posSituation = posSituation;
        //}

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Id, Titulo, Descricao, DataHora, Synchronized);
        }
    }
}
