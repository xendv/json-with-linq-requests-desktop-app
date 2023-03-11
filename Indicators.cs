using System;
using System.Collections.Generic;
using System.Text;

namespace ТСПО_1_JSON
{
    public class Indicators
    {
        public string code;
        public string title;
        public string znatTitle;
        public string umetTitle;
        public string vladetTitle;
        public string znat;
        public string umet;
        public string vladet;

        public Indicators(List<string> values)
        {
            this.code = values[0];
            this.title = values[1];
            this.znatTitle = values[2];
            this.znat = values[3];
            this.umetTitle = values[4];
            this.umet = values[5];
            this.vladetTitle = values[6];
            this.vladet = values[7];
        }
    }
}
