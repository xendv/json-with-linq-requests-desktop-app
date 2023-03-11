namespace ТСПО_1_JSON
{
    public class Schedule
    {
        public string Type { get; set; }
        public string Duration { get; set; }
        public int Weeks { get; set; }

        public Schedule(string type, string duration, int weeks)
        {
            this.Type = ConvertType(type);
            this.Duration = duration;
            this.Weeks = weeks;
        }

        public static string ConvertType(string type)
        {
            return type switch
            {
                "Б1" => "Теоретическое обучение",
                "Б2" => "Практика",
                "Э" => "Промежуточная аттестация",
                "К" => "Каникулы",
                "У" => "Учебная практика",
                "П" => "Производственная практика",
                "НИР" => "Научно-исследовательская работа",
                "Д" => "Государственная итоговая аттестация",
                _ => "",
            };
        }
    }
}
