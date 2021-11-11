using Fejlesztési_minták.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fejlesztési_minták.Entities
{
    class Present : Toy
    {
        public SolidBrush PresentColor { get; set; }
        public SolidBrush SzalagColor { get; set; }
        public Present(Color doboz, Color szalag)
        {
            PresentColor = new SolidBrush(doboz);
            SzalagColor = new SolidBrush(szalag);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(PresentColor,0,0,70,70);
            g.FillRectangle(SzalagColor, 20, 0, 10,70 );
            g.FillRectangle(SzalagColor, 0, 20, 70, 10);
        }
    }
}
