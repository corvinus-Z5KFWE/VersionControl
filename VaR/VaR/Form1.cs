using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VaR
{
    public partial class Form1 : Form
    
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;
        List<PortfolioItem> portfolio = new List<PortfolioItem>();
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Tick.ToList();
            dataGridView1.DataSource = Ticks;
        }

        private void CreatePortfolio()
        {
            portfolio.Add(new PortfolioItem() {Index = "OTP" , Volume = 10 });
            portfolio.Add(new PortfolioItem() {Index = "ZWACK" , Volume = 10 });
            portfolio.Add(new PortfolioItem() {Index = "ELMU" , Volume = 10 });

            dataGridView2.DataSource = new PortfolioItem();
        }
        
    }
}
