﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ТСПО_1_JSON
{
    public class Competence
    {
        public string title;
        public string type;

        public Competence(string title, string type)
        {
            this.title = title;
            this.type = type;
        }

        public Competence(List<string> values)
        {
            title = values[1];
            type = values[2];
        }
    }
}
