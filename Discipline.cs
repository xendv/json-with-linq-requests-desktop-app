using System.Collections.Generic;

namespace ТСПО_1_JSON
{
    public class Discipline
    {

        public string Index { get; set; }
        public string Title { get; set; }
        public string description;
        public string competences;
        public string unitsCost;
        public List<bool> terms;

        public Discipline(string index, string title, string description,
            string competences, string unitsCost, List<bool> terms)
        {
            this.Index = index;
            this.Title = title;
            this.description = description;
            this.competences = competences;
            this.unitsCost = unitsCost;
            this.terms = terms;
        }

        public Discipline(string index, string title)
        {
            this.Index = index;
            this.Title = title;
        }
    }
}
