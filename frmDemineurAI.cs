using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demineur
{
    public partial class frmDemineurAI : Form
    {
        Cell[,] Grille;
        int nbRecursion = 0;
        List<Cell> lstCellToCheck = new List<Cell>();
        frmSettings frmSettings;
        int DefSize = 8;
        int nbBomb;
        public frmDemineurAI(int size, frmSettings frm)
        {
            InitializeComponent();
            frmSettings = frm;
            DefSize = size;
            nbBomb = DefSize * DefSize / 10;
            InitialisationGrille(DefSize);
        }
        public void RemoveAllButtons(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    // Supprime le bouton du formulaire
                    this.Controls.Remove(Controls.Find(Grille[x, y].Btn.Name, true)[0]);
                }
            }
        }
        public async void InitialisationGrille(int size)
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
            SetBomb(DefSize, nbBomb);
            SetNbBombAround(DefSize);
            await Task.Delay(TimeSpan.FromMilliseconds(1000));
            RandomMove();
        }
        public void SetBomb(int size, int nb)
        {
            Random rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(0, size - 1);
                y = rand.Next(0, size - 1);
                if (!Grille[x, y].IsBomb)
                {
                    Grille[x, y].IsBomb = true;
                    nb--;
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
                            if (xoff > -1 && xoff < size && yoff > -1 && yoff < size)
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
                    if (xoff < 0 || xoff >= size) continue;
                    for (int j = -1; j < 2; j++)
                    {
                        int yoff = y + j;
                        if (yoff < 0 || yoff >= size) continue;

                        if (!Grille[xoff, yoff].IsShowed)
                        {
                            ShowPropagation(xoff, yoff, size);
                        }
                    }
                }
            }
            else
            {
                Grille[x, y].Show(null);
                // Ajouter cette case dans la liste des cases à contrôler si celle-ci n'y est pas déjà
                if (!lstCellToCheck.Contains(Grille[x, y])) lstCellToCheck.Add(Grille[x, y]);
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
            if (countCellRevealed == (size * size) - nbBomb) return true;
            return false;
        }
        public async void btnCell_Click(object sender, MouseEventArgs e)
        {
            // create X,Y point (0,0) explicitly with System.Drawing 
            // System.Drawing.Point leftTop = new System.Drawing.Point(Location.X+20, Location.Y+50);
            // this.Location;
            // set mouse position
            // Cursor.Position = leftTop;
            nbRecursion++;
            Button btn = (Button)sender;
            MouseEventArgs me = (MouseEventArgs)e;
            Cell cellClicked;
            cellClicked = Grille[Convert.ToInt32(btn.Name.Split('.')[0]),
                                 Convert.ToInt32(btn.Name.Split('.')[1])];
            if (me.Button == MouseButtons.Right && !cellClicked.IsShowed)
            {
                if (cellClicked.Btn.Image != null)
                {
                    cellClicked.Btn.Image = null;
                    cellClicked.IsFlagged = false;
                }
                else
                {
                    cellClicked.Btn.Image = ilImages.Images[0];
                    cellClicked.IsFlagged = true;
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
                        // RemoveAllButtons(DefSize);
                        Grille = null;
                        this.Close();
                        frmDemineurAI f = new frmDemineurAI(DefSize, frmSettings);
                        f.Show();
                        await Task.Delay(TimeSpan.FromMilliseconds(100));
                        frmSettings.Hide();
                        return;
                    }
                }
                else if (cellClicked.Btn.Image == null)
                {
                    cellClicked.Show(ilImages.Images[1]);
                    // Ajouter la case dans la liste de case à contrôler
                    lstCellToCheck.Add(cellClicked);
                    ShowPropagation(cellClicked.X, cellClicked.Y, DefSize);
                }
            }
            if (CheckWin(DefSize))
            {
                DialogResult dr = new DialogResult();
                ShowAllCells(DefSize);
                dr = MessageBox.Show("Voulez-vous recommencer une partie ?", "Partie gagnée", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) Environment.Exit(0);
                else if (dr == DialogResult.Yes)
                {
                    // RemoveAllButtons(DefSize);
                    Grille = null;
                    this.Close();
                    frmDemineurAI f = new frmDemineurAI(DefSize, frmSettings);
                    f.Show();
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    frmSettings.Hide();
                    return;
                }
            }

            // Intelligence artificielle

            // Supprimer de la liste les cases à contrôler 
            // qui ont toutes leurs cases autour flaggées ou découvertes



            bool isIaBlocked = true;
            // Contrôler les 3 règles pour toutes les cases à contrôler
            foreach (Cell c in lstCellToCheck)
            {
                // await Task.Delay(TimeSpan.FromMilliseconds(1000));
                // Règle 1
                // Si toutes les cases cachées (drapeaux non compris) sont des bombes
                if (Regle1(c.X, c.Y))
                {
                    isIaBlocked = false;
                    // Mettre un drapeau sur toutes ces cases
                    for (int i = -1; i < 2; i++)
                    {
                        int xoff = c.X + i;
                        if (xoff < 0 || xoff >= DefSize) continue;
                        for (int j = -1; j < 2; j++)
                        {
                            int yoff = c.Y + j;
                            if (yoff < 0 || yoff >= DefSize) continue;
                            // Si la case n'est pas découverte et n'est pas déjà un drapeau
                            if(!Grille[xoff, yoff].IsShowed && !Grille[xoff, yoff].IsFlagged)
                            {
                                System.Drawing.Point cellLocation = new System.Drawing.Point(Location.X + 20 + 30 * xoff, Location.Y + 50 + 30 * yoff);

                                await Task.Delay(TimeSpan.FromMilliseconds(50));
                                // Placer la souris sur le bouton
                                Cursor.Position = cellLocation;
                                Grille[xoff, yoff].Btn.Image = ilImages.Images[0];
                                Grille[xoff, yoff].IsFlagged = true;
                            }

                        }
                    }
                }
            }
            foreach (Cell c in lstCellToCheck)
            {
                // Règle 2
                // Si toutes les cases cachées autour ne sont pas des bombes
                if (Regle2(c.X, c.Y))
                {
                    // Découvrir toutes ces cases
                    for (int i = -1; i < 2; i++)
                    {
                        int xoff = c.X + i;
                        if (xoff < 0 || xoff >= DefSize) continue;
                        for (int j = -1; j < 2; j++)
                        {
                            int yoff = c.Y + j;
                            if (yoff < 0 || yoff >= DefSize) continue;
                            // Si la case n'est pas découverte et n'est pas déjà un drapeau
                            if (!Grille[xoff, yoff].IsShowed && !Grille[xoff, yoff].IsFlagged)
                            {
                                System.Drawing.Point cellLocation = new System.Drawing.Point(Location.X + 20 + 30 * xoff, Location.Y + 50 + 30 * yoff);

                                await Task.Delay(TimeSpan.FromMilliseconds(50));
                                // Placer la souris sur le bouton
                                Cursor.Position = cellLocation;
                                MouseEventArgs me1 = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
                                btnCell_Click(Grille[xoff, yoff].Btn, me1);
                                nbRecursion--;
                                return;
                            }
                        }
                    }
                }
            }
            nbRecursion--;
            // MessageBox.Show(nbRecursion.ToString());
            // Si l'IA ne peut plus rien faire, cliquer sur une case au hasard
            //if (isIaBlocked) MessageBox.Show("IA bloquée");// RandomMove();
            //isIaBlocked = false;
            // Si le programme arrive ici et que c'est la racine des appels, c'est que l'IA est bloquée, il faut donc cliquer sur une case au hasard
            //if (nbRecursion == 0) {
                RandomMove();
                // MessageBox.Show("IA bloquée");
            //}
            
        }
        private void frmDemineur_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmSettings.Show();
        }
       
        public async Task<Cell> RandomMove()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            Random rand = new Random();
            int x, y;
            do
            {
                // Générer une case aléatoire
                x = rand.Next(0, DefSize - 1);
                y = rand.Next(0, DefSize - 1);
                
            } while (Grille[x, y].IsShowed || Grille[x, y].IsFlagged);
            // Créer un point au coordonnées de la case
            System.Drawing.Point cellLocation = new System.Drawing.Point(Location.X + 20 + 30 * x, Location.Y + 50 + 30 * y);

            await Task.Delay(TimeSpan.FromMilliseconds(100));
            // Placer la souris sur le bouton
            Cursor.Position = cellLocation;
            // Ajouter la case dans la liste de case à contrôler si celle-ci n'y est pas déjà
            if (!lstCellToCheck.Contains(Grille[x, y])) lstCellToCheck.Add(Grille[x, y]);
            // Simuler un clic de souris
            MouseEventArgs me = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
            btnCell_Click(Grille[x, y].Btn, me);
            if (Grille[x, y].IsBomb) return null;
            return Grille[x,y]; // Coordonnées de la case
        }

        private void frmDemineurAI_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmSettings.Show();
        }
        // 1 : Si une case a le même montant de case cachées autour d'elle 
        //     que de bombes qui ne sont pas des drapeaux,
        //     alors toutes les cases cachées sont des bombes
        public bool Regle1(int x, int y)
        {
            int nbHiddenCells = 0;
            int nbBombToFindAround = Grille[x, y].NbBombsAroud;

            for (int i = -1; i < 2; i++)
            {
                int xoff = x + i;
                if (xoff < 0 || xoff >= DefSize) continue;
                for (int j = -1; j < 2; j++)
                {
                    int yoff = y + j;
                    if (yoff < 0 || yoff >= DefSize) continue;
                    if(!Grille[xoff, yoff].IsShowed && !Grille[xoff, yoff].IsFlagged)
                    {
                        nbHiddenCells++;
                    } else if(Grille[xoff, yoff].IsFlagged)
                    {
                        nbBombToFindAround--;
                    }
                    
                }
            }
            if(nbHiddenCells == nbBombToFindAround)
            {
                return true;
            } else return false;
        }
        // 2 : Si une case a le même montant de drapeaux autour d'elle
        //     que son nombre de bombes,
        //     alors toutes les cases restantes autour d'elle ne sont pas des bombes
        public bool Regle2(int x, int y)
        {
            int nbFlagsAround= 0;
            int nbBomAround = Grille[x, y].NbBombsAroud;

            for (int i = -1; i < 2; i++)
            {
                int xoff = x + i;
                if (xoff < 0 || xoff >= DefSize) continue;
                for (int j = -1; j < 2; j++)
                {
                    int yoff = y + j;
                    if (yoff < 0 || yoff >= DefSize) continue;
                    if (Grille[xoff, yoff].IsFlagged)
                    {
                        nbFlagsAround++;
                    }

                }
            }
            if (nbFlagsAround == Grille[x, y].NbBombsAroud)
            {
                return true;
            }
            else return false;
        }
        // 3 : 
        public bool Regle3()
        {
            return false;
        }
    }
}
