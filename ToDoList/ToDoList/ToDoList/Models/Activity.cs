using SQLite.Net.Attributes;

namespace ToDoList.Models
{
    public class Activity : BaseDataObject
    {

        string title = string.Empty;
        public string titulo
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string content = string.Empty;
        public string descricao
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        string date = string.Empty;
        public string dataHora
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }

       
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
            return string.Format("{0} {1} {2} {3}", Id, titulo, descricao, dataHora);
        }
    }
}
