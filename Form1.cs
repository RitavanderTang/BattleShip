using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace Battleship
{
   
    public partial class Form1 : Form
    {
        List<Button> player1Position;
        List<Button> player2Position;
        Random rand = new Random();
        int totalShips = 3;
        int totalEnemy = 3;
        int rounds = 10;
        int playerTotalScore = 0;
        int enemyTotalScore = 0;
        public Form1()
        {
            InitializeComponent();
            loadButtons();

            for (int i = 0; i < 16; i++)
            {
                player2Position[i].Enabled = false;
            }
        }

        private void playerPicksPosition(object sender, EventArgs e)
        {
            if (totalShips > 0)
            {
                    
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "playerShip";
                button.BackgroundImage = Properties.Resources.battleship;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                totalShips--;
            }
            if (totalShips == 0)
            {
                
                helpText.Top = 534;
                helpText.Left = 610;
                helpText.Text = "Now attack your enemy by clicking an enemy location";

                for (int i = 0; i < 16; i++)
                {
                    player2Position[i].Enabled = true;
                }
            }


        }

        public void attackplayer2Position(object sender, EventArgs e)
        {
            var button = sender as Button;
            int index = player2Position.IndexOf(button);

            

            if ( player2Position[index].Enabled && rounds > 0)
            {
                rounds--;
                roundsText.Text = "Rounds: " + rounds;

                if (player2Position[index].Tag == "enemyShip")
                {
                    player2Position[index].Enabled = false;
                    player2Position[index].BackgroundImage = Properties.Resources.fireIcon;
                    player2Position[index].BackColor = System.Drawing.Color.DarkBlue;
                    playerTotalScore++;
                    playerScore.Text = "" + playerTotalScore;
                    enemyPlayTimer.Start();
                }
                else
                {
                    player2Position[index].Enabled = false;
                    player2Position[index].BackgroundImage = Properties.Resources.missIcon;
                    player2Position[index].BackColor = System.Drawing.Color.DarkBlue;
                    enemyPlayTimer.Start();
                }

                for (int i = 0; i < 16; i++)
                {   
                    player2Position[i].Enabled = false;
                }
            }
        }

        private void enemyAttackPlayer(object sender, EventArgs e)
        {
            if (player1Position.Count > 0 && rounds > 0)
            {
                
                rounds--;
                roundsText.Text = "Rounds: " + rounds;

                int index = rand.Next(player1Position.Count);

                if (player1Position[index].Tag == "playerShip")
                {
                    player1Position[index].BackgroundImage = Properties.Resources.fireIcon;
                    player1Position[index].Enabled = false;
                    player1Position[index].BackColor = System.Drawing.Color.DarkBlue;
                    player1Position.RemoveAt(index);
                    enemyTotalScore++;
                    enemyScore.Text = "" + enemyTotalScore;
                    enemyPlayTimer.Stop();
                }
                else
                {
                    player1Position[index].BackgroundImage = Properties.Resources.missIcon;
                    //enemyMoves.Text = "" + player1Position[index].Text;
                    player1Position[index].Enabled = false;
                    player1Position[index].BackColor = System.Drawing.Color.DarkBlue;
                    player1Position.RemoveAt(index);
                    enemyPlayTimer.Stop();
                }


                for (int i = 0; i < 16; i++)
                {
                    player2Position[i].Enabled = true;
                }
            }

            if (rounds < 1 || playerTotalScore > 2 || enemyTotalScore > 2)
            {
                if (playerTotalScore > enemyTotalScore)
                {
                    MessageBox.Show("You win", "winning");
                }
                if (playerTotalScore == enemyTotalScore)
                {
                    MessageBox.Show("No one wins this", "Draw");
                }
                if (enemyTotalScore > playerTotalScore)
                {
                    MessageBox.Show("Haha! I sunk your battle ships", "Lost");
                }
            }
        }

        private void enemyPicksPositions(object sender, EventArgs e)
        {
            int index = rand.Next(player2Position.Count);

            if (player2Position[index].Enabled == true && player2Position[index].Tag == null)
            {
                player2Position[index].Tag = "enemyShip";
                totalEnemy--;

                Debug.WriteLine("Enemy Position  " + player2Position[index].Text);
            }
            else
            {
                index = rand.Next(player2Position.Count);
            }

            if (totalEnemy < 1)
            {
                
                player2PositionPicker.Stop();
            }
        }

        private void loadButtons()
        {
            player1Position = new List<Button> { w1, w2, w3, w4, x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4 };
            player2Position = new List<Button> { a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4, d1, d2, d3, d4 };

            for (int i = 0; i < player2Position.Count; i++)
            {
                player2Position[i].Tag = null;
                
            }
        }
    }
}
