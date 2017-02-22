
using System;
using SQLite.Net.Attributes;

namespace todolist
{
    public class Activity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public int posSituation { get; set; }

        private string[] situation = { "Não iniciada", "Em andamento", "Concluída" };

        public string getSituation()
        {
            return this.situation[posSituation];
        }

        public void setSituation(int posSituation)
        {
            this.posSituation = posSituation;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", ID, Title, Content, Date, Hour, posSituation);
        }

    }
}
