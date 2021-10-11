using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MintaZH_gyakorlas
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        public Form1()
        {
            InitializeComponent();
            LoadData("Summer_olympic_Medals.csv");
        }

        private void LoadData(string practiceFile)
        {
            using (var sr = new StreamReader(practiceFile, Encoding.Default))
            {
                sr.ReadLine(); // első sor kimarad
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(',');
                    var or = new OlympicResult()
                    {
                        Year = int.Parse(line[0]),
                        Country = line[3],
                        Medals = new int[]
                        {
                            int.Parse(line[5]),
                            int.Parse(line[6]),
                            int.Parse(line[7])
                        }
                    };

                    results.Add(or);
                }
           }

        }

        private void CreateYearFilter()
        {
            var years = (from x in results
                         orderby x.Year
                         select x.Year).Distinct();
            comboBox1.DataSource = years.ToList();
        }
    }
}
