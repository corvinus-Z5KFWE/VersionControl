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
        private List<Ball> _balls = new List<Ball>();
        private BallFactory _factory;
        private BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
        public form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosiion = 0;
            foreach (var ball in _balls)
            {
                ball.MoveBall();
                if (ball.Left > maxPosiion)
                {
                    maxPosiion = ball.Left;
                }
            }

            if (maxPosiion >1000)
            {
                var oldestBall = _balls[0];
                mainPanel.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }

        
    }
}
