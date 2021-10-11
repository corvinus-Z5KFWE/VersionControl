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
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace MintaZH_gyakorlas
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData("Summer_olympic_Medals.csv");
            CalculateOrder();
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

        private int CalculateResult(OlympicResult or)
        {
            int betterCountriesCount = 0;
            var betterCountries = (from y in results
                                   where y.Year == or.Year && y.Country != or.Country
                                   select y);
            foreach (var r in betterCountries)
            {
                if (r.Medals[0] > or.Medals[0])
                    betterCountriesCount++;
                else if (r.Medals[0] == or.Medals[0] && r.Medals[1] > or.Medals[1])
                    betterCountriesCount++;
                else if (r.Medals[0] == or.Medals[0] && r.Medals[1] == or.Medals[1] && r.Medals[2] > or.Medals[2])
                    betterCountriesCount++;
            }
            return betterCountriesCount + 1;
        }

        private void CalculateOrder()
        {
            foreach (var r in results)
                r.Position = CalculateResult(r);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateExcel();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateExcel()
        {
            var headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };
            for (int i = 0; i < headers.Length; i++)
                xlSheet.Cells[1, i + 1] = headers[i];

            var filteredresults = from r in results
                           where r.Year == (int)comboBox1.SelectedIndex
                           orderby r.Position
                           select r;

            var szamlalo = 2;
            foreach (var i in filteredresults)
            {
                xlSheet.Cells[szamlalo, 1] = i.Position;
                xlSheet.Cells[szamlalo, 2] = i.Country;
                for (int x = 0; x < 2; x++)
                    xlSheet.Cells[szamlalo, x + 3] = i.Medals[x];
                szamlalo++;
            }

        }
    }
}
