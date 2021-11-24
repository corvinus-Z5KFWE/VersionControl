using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Webszolgáltatás_gyakorlas.Entities;
using Webszolgáltatás_gyakorlas.MnbServiceReference;

namespace Webszolgáltatás_gyakorlas
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        string result = null;
        public Form1()
        {
            InitializeComponent();
            GetCurrencies();
            RefreshDate();
        }

        public void GetCurrencies()
        {
            comboBox1.DataSource = Currencies;
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;

            var xml = new XmlDocument();
            xml.LoadXml(result);
            foreach  (XmlElement item in xml.DocumentElement.ChildNodes[0])
            {
                string newItem = item.InnerText;
                Currencies.Add(newItem);
            }

        }

        private void GetExchangeRates()
        {
            var mnbServices = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = (dateTimePicker1.Value).ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };

            var response = mnbServices.GetExchangeRates(request);
            result = response.GetExchangeRatesResult;
        }

        public void XML()
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element  in xml.DocumentElement)
            {
                var rateData = new RateData();
                Rates.Add(rateData);
                //dátum
                rateData.Date = DateTime.Parse(element.GetAttribute("date"));
                //currency-valuta
                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null) continue;
                rateData.Currency = childElement.GetAttribute("curr");
                //value-érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0) rateData.Value = value / unit;

            }
           
        }

        public void Diagram()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        public void RefreshDate()
        {
            Rates.Clear();
            GetExchangeRates();
            XML();
            Diagram();
            dataGridView1.DataSource = Rates;
            richTextBox1.Text = result;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshDate();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshDate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDate();
        }
    }
}
