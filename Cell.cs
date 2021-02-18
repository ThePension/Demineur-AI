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
        public int nbBombsAround;
        public int NbBombsAroud {
            get => nbBombsAround;
            set {
                nbBombsAround = value;
                switch (value)
                {
                    case 1:
                        Btn.ForeColor = Color.Blue;
                        break;
                    case 2:
                        Btn.ForeColor = Color.Green;
                        break;
                    case 3:
                        Btn.ForeColor = Color.Red;
                        break;
                    case 4:
                        Btn.ForeColor = Color.Violet;
                        break;
                    case 5:
                        Btn.ForeColor = Color.DarkRed;
                        break;
                    case 6:
                        Btn.ForeColor = Color.Turquoise;
                        break;
                    case 7:
                        Btn.ForeColor = Color.Black;
                        break;
                    case 8:
                        Btn.ForeColor = Color.Gray;
                        break;
                }
            }
        }

        public Cell(int X, int Y, int Width, Form frm)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.IsFlagged = false;
            Btn = new Button();
            Btn.Name = X + "." + Y;
            Btn.Font = new Font(FontFamily.GenericSansSerif, (float)8.25, FontStyle.Bold);
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
