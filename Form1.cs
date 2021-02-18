using System;
using System.Windows.Forms;

namespace Demineur
{
    public partial class frmDemineur : Form
    {
        Cell[,] Grille;
        frmSettings frmSettings;
        int DefSize = 8;
        bool IsFirstClicked = true;
        int nbBomb;
        public frmDemineur(int size, frmSettings frm)
        {
            InitializeComponent();
            frmSettings = frm;
            DefSize = size;
            nbBomb = DefSize * DefSize / 5;
            InitialisationGrille(DefSize);
        }
        public void RemoveAllButtons(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    // Supprime le bouton du formulaire
                    this.Controls.Remove(Controls.Find(Grille[x, y].Btn.Name,true)[0]);
                }
            }
        }
        public void InitialisationGrille(int size)
        {
            Grille = new Cell[DefSize, DefSize];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Grille[x, y] = new Cell(x, y, 30, this);
                    Grille[x, y].Btn.MouseDown += new MouseEventHandler(btnCell_Click);
                }
            }
        }
        public void SetBomb(int size,int nb, int xClicked, int yClicked)
        {
            Random rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(0, size - 1);
                y = rand.Next(0, size - 1);
                if (!Grille[x, y].IsBomb)
                {
                    // Si la case n'est pas la case cliquée
                    if (x != xClicked && y != yClicked)
                    {
                        // Si la case n'est pas une case autour de la case cliquée
                        if ((x != xClicked - 1 && y != yClicked - 1) &&
                            (x != xClicked && y != yClicked - 1) &&
                            (x != xClicked + 1 && y != yClicked - 1) &&
                            (x != xClicked - 1 && y != yClicked) &&
                            (x != xClicked + 1 && y != yClicked) &&
                            (x != xClicked - 1 && y != yClicked + 1) &&
                            (x != xClicked && y != yClicked + 1) &&
                            (x != xClicked + 1 && y != yClicked + 1))
                        {

                            Grille[x, y].IsBomb = true;
                            nb--;
                        }
                    }
                }
            } while (nb != 0);
        }
        public void SetNbBombAround(int size)
        {
            int count = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    count = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            int xoff = x + i;
                            int yoff = y + j;
                            if(xoff > -1 && xoff < size && yoff > -1 && yoff < size)
                            {
                                if (Grille[xoff, yoff].IsBomb) count++;
                            }
                            Grille[x, y].NbBombsAroud = count;
                        }     
                    }
                }
            }    
        }
        public void ShowAllCells(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Grille[x, y].Show(ilImages.Images[1]);
                }
            }
        }
        public void ShowPropagation(int x, int y, int size)
        {
            if (Grille[x, y].NbBombsAroud == 0)
            {
                Grille[x, y].Show(null);
                for (int i = -1; i < 2; i++)
                {
                    int xoff = x + i;
                    if(xoff < 0 || xoff >= size) continue;
                    for (int j = -1; j < 2; j++)
                    {
                        int yoff = y + j;
                        if (yoff < 0 || yoff >= size) continue;

                        if(!Grille[xoff, yoff].IsShowed)
                        {
                            ShowPropagation(xoff, yoff, size);
                        }
                    }
                }
            }
            else
            {
                Grille[x, y].Show(null);
            }
        }
        public bool CheckWin(int size)
        {
            int countCellRevealed = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (Grille[x, y].IsShowed) countCellRevealed++;
                }
            }
            if (countCellRevealed == (size * size) - nbBomb )return true;
            return false;
        }
        public void btnCell_Click(object sender, MouseEventArgs e)
        {
            // create X,Y point (0,0) explicitly with System.Drawing 
            // System.Drawing.Point leftTop = new System.Drawing.Point(Location.X+20, Location.Y+50);
            // this.Location;
            // set mouse position
            // Cursor.Position = leftTop;

            Button btn = (Button)sender;
            MouseEventArgs me = (MouseEventArgs)e;
            Cell cellClicked;
            cellClicked = Grille[Convert.ToInt32(btn.Name.Split('.')[0]), 
                                 Convert.ToInt32(btn.Name.Split('.')[1])];

            // Si c'est le premier coup
            if (IsFirstClicked)
            {
                IsFirstClicked = false;
                SetBomb(DefSize, nbBomb, cellClicked.X, cellClicked.Y);
                SetNbBombAround(DefSize);
            }

            if (me.Button == MouseButtons.Right && !cellClicked.IsShowed)
            {
                if(cellClicked.Btn.Image != null)
                {
                    cellClicked.Btn.Image = null;
                }
                else
                {
                    cellClicked.Btn.Image = ilImages.Images[0];
                }
            }
            else
            {
                if (cellClicked.IsBomb && cellClicked.Btn.Image == null)
                {
                    DialogResult dr = new DialogResult();
                    ShowAllCells(DefSize);
                    dr = MessageBox.Show("Voulez-vous recommencer une partie ?", "Partie perdue", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No) Environment.Exit(0);
                    else if (dr == DialogResult.Yes)
                    {
                        RemoveAllButtons(DefSize);
                        Grille = null;
                        InitialisationGrille(DefSize);
                        IsFirstClicked = true;
                    }
                }
                else if(cellClicked.Btn.Image == null)
                {
                    cellClicked.Show(ilImages.Images[1]);
                    ShowPropagation(cellClicked.X, cellClicked.Y, DefSize);
                }
            }
            if (CheckWin(DefSize))
            {
                DialogResult dr = new DialogResult();
                ShowAllCells(DefSize);
                dr = MessageBox.Show("Voulez-vous recommencer une partie ?", "Partie gagnée", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) Environment.Exit(0);
                else if(dr == DialogResult.Yes)
                {
                    RemoveAllButtons(DefSize);
                    Grille = null;
                    InitialisationGrille(DefSize);
                    IsFirstClicked = true;
                }
                return;
            }
        }
        private void frmDemineur_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmSettings.Show();
        }
    }
}
