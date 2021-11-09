using Fejlesztési_minták.Abstractions;
using Fejlesztési_minták.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fejlesztési_minták
{
    public partial class form1 : Form
    {
        public List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;

        private IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
            }
        }
        public form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        public void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        public void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosiion = 0;
            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left > maxPosiion)
                    maxPosiion = toy.Left;
            }

            if (maxPosiion >1000)
            {
                var oldestToy = _toys[0];
                mainPanel.Controls.Remove(oldestToy);
                _toys.Remove(oldestToy);
            }
        }

        
    }
}
