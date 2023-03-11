using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace ТСПО_1_JSON
{
    public class JsonUtils
    {
        readonly string startupPath = Environment.CurrentDirectory;
        private readonly string fileName = "OPOP.json";

        public JsonUtils() { }

        public List<KeyValuePair<string, string>> ReadFileContent()
        {
            using var sr = new StreamReader(@startupPath + "\\" + fileName);
            var reader = new JsonTextReader(sr);
            var jObject = JObject.Load(reader);

            var contents = jObject["content"]["section4"]["professionalStandards"].Select(p => p);
            var list = new List<KeyValuePair<string, string>>();
            foreach (var content in contents)
            {
                var c = content["content"].ToString();
                var code = c.Substring(0, c.IndexOf(' '));
                if (IsCode(code))
                {
                    list.Add(new KeyValuePair<string, string>(code, c[(c.IndexOf(' ') + 1)..]));
                }
            }
            return list;
        }

        private bool IsCode(string content)
        {
            foreach (var ch in content)
            {
                if (char.IsDigit(ch) || ch == '.') continue;
                else return false;
            }
            return true;
        }

        public ObservableCollection<KeyValuePair<string, string>> GetUniversalCompetences()
        {
            return GetCompetences("universalCompetencyRows");
        }

        public ObservableCollection<KeyValuePair<string, string>> GetCommonCompetences()
        {
            return GetCompetences("commonCompetencyRows");
        }

        public ObservableCollection<KeyValuePair<string, string>> GetCompetences(string type)
        {
            using var sr = new StreamReader(@startupPath + "\\" + fileName);
            var list = new ObservableCollection<KeyValuePair<string, string>>();
            var reader = new JsonTextReader(sr);
            var jObject = JObject.Load(reader);
            var contents = jObject["content"]["section4"][type].Select(p => p);

            foreach (var content in contents)
            {
                var competence = content["competence"];
                var id = competence["id"].ToString();
                var code = competence["code"].ToString();
                var title = competence["title"].ToString();
                list.Add(new KeyValuePair<string, string>(id, code + ' ' + title));
            }

            return list;
        }

        public Indicators GetIndicatorsByCompenensy(string comp, string type)
        {
            using var sr = new StreamReader(@startupPath + "\\" + fileName);
            var reader = new JsonTextReader(sr);
            var jObject = JObject.Load(reader);
            var contents = jObject["content"]["section4"][type].Where(p => p["competence"]["id"].Value<string>().Equals(comp)).First();
            var values = new List<string>();

            var competence = contents["competence"];
            var id = competence["id"];
            var code = competence["code"].ToString();
            var title = competence["title"].ToString();
            values.Add(code);
            values.Add(title);

            var indicators = contents["indicators"].Select(p => p);

            foreach (var data in indicators)
            {
                var dataContent = data["content"].ToString();
                values.Add(dataContent.Substring(0, dataContent.IndexOf(':')));
                values.Add(dataContent[(dataContent.IndexOf(':') + 2)..]);
            }

            return new Indicators(values);
        }

        public ObservableCollection<KeyValuePair<string, string>> GetDisciplines()
        {
            var list = new ObservableCollection<KeyValuePair<string, string>>();

            using (var sr = new StreamReader(@startupPath + "\\" + fileName))
            {
                var reader = new JsonTextReader(sr);
                var jObject = JObject.Load(reader);
                var contents = jObject["content"]["section5"]["eduPlan"]["block1"]["subrows"].Select(p => p);

                foreach (var content in contents)
                {
                    var title = content["title"].ToString();
                    var index = content["index"].ToString();
                    list.Add(new KeyValuePair<string, string>(index, index + ' ' + title));
                }
            }
            return list;
        }

        public Discipline GetDisciplineInfo(string disciplineCode)
        {
            using var sr = new StreamReader(@startupPath + "\\" + fileName);
            var reader = new JsonTextReader(sr);
            var jObject = JObject.Load(reader);
            var contents = jObject["content"]["section5"]["eduPlan"]["block1"]["subrows"].Where(p => p["index"].Value<string>().Equals(disciplineCode)).First();

            var title = contents["title"].ToString();

            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", '<', '>'));
            var c = contents["description"].ToString();
            var description = regex.Replace(c, string.Empty);

            var unitsCost = contents["unitsCost"].ToString();
            var competencesAll = contents["competences"].Select(p => p["code"].ToString().TrimEnd('.'));
            var terms = contents["terms"].Select(p => p.ToObject<bool>()).ToList();
            var competences = string.Join(", ", competencesAll);

            return new Discipline(disciplineCode, title, description, competences, unitsCost, terms);
        }

        public ObservableCollection<Discipline> GetDisciplines(string term)
        {
            var list = new ObservableCollection<Discipline>();

            using (var sr = new StreamReader(@startupPath + "\\" + fileName))
            {
                var reader = new JsonTextReader(sr);
                var jObject = JObject.Load(reader);
                var contents = jObject["content"]["section5"]["eduPlan"]["block1"]["subrows"].Select(p => p).Where(p => p["terms"][int.Parse(term) - 1].Value<bool>());

                foreach (var content in contents)
                {
                    var title = content["title"].ToString();
                    var index = content["index"].ToString();
                    list.Add(new Discipline(index, title));
                }
            }
            return list;
        }

        public ObservableCollection<Schedule> GetSchedule(string course)
        {
            var schedules = new List<Schedule>();

            using (var sr = new StreamReader(@startupPath + "\\" + fileName))
            {
                var reader = new JsonTextReader(sr);
                var jObject = JObject.Load(reader);
                var contents = jObject["content"]["section5"]["calendarPlanTable"]["courses"].ToArray()[int.Parse(course) - 1]["weekActivityIds"].Select(p => p.ToString()).ToArray();
                var type = contents[0];
                var dateStart = new DateTime(2020 + int.Parse(course) - 1, 9, 1);
                var count = 1;

                for (var i = 1; i < contents.Length; i++)
                {
                    if (contents[i] != type || i == contents.Length - 1)
                    {
                        if (i == contents.Length - 1)
                        {
                            count++;
                        }
                        var position = schedules.FindIndex(p => p.Type == Schedule.ConvertType(type));
                        var dateEnd = DateEnd(dateStart, count);
                        if (position == -1)
                        {
                            schedules.Add(new Schedule(type, DateRange(dateStart, dateEnd), count));
                        }
                        else
                        {
                            var existing = schedules.Single(p => p.Type == Schedule.ConvertType(type));
                            schedules[position] = new Schedule(type, existing.Duration + ", " + DateRange(dateStart, dateEnd), existing.Weeks + count);
                        }
                        count = 1;
                        dateStart = dateEnd.AddDays(2);
                    }
                    else
                    {
                        count++;
                    }
                    type = contents[i];
                }
            }
            return new ObservableCollection<Schedule>(schedules);
        }

        private DateTime DateEnd(DateTime dateStart, int count)
        {
            return dateStart.AddDays(-(int)dateStart.DayOfWeek + 1).AddDays(7 * count - 2);
        }

        private string DateRange(DateTime dateStart, DateTime dateEnd)
        {
            return dateStart.ToString("dd.MM.yyy") + '-' + dateEnd.ToString("dd.MM.yyy");
        }
    }
}
