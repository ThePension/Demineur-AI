using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demineur
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public Button Btn { get; set; }
        public bool IsBomb { get; set; }
        public bool IsShowed { get; set; }
        public bool IsFlagged { get; set; }
        public int NbBombsAroud { get; set; }

        public Cell(int X, int Y, int Width, Form frm)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.IsFlagged = false;
            Btn = new Button();
            Btn.Name = X + "." + Y;
            Btn.Size = new System.Drawing.Size(new System.Drawing.Point(Width, Width));
            Btn.Location = new System.Drawing.Point(X*Width, Y * Width);
            frm.Controls.Add(Btn);
        }
        public void Show(Image img)
        {
            if (!IsShowed)
            {
                IsShowed = true;
                if (IsBomb)
                {
                    Btn.Image = img;
                }
                else
                {
                    if(NbBombsAroud != 0) Btn.Text = NbBombsAroud.ToString();
                    Btn.BackColor = Color.DarkGray;
                }
            }
        }

    }
}
