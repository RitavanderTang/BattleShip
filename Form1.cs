using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Diagnostics;

namespace Battleship
{
   
    public partial class Form1 : Form
    {
        // Define the variables used.
        List<Button> playerPosition;
        List<Button> enemyPosition;
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

            // Start with disabled Computer(Comp) buttons.
            for (int i = 0; i < 16; i++)
                enemyPosition[i].Enabled = false;
        }
        // Define lists of buttons for P and Comp.
        private void loadButtons()
        {
            playerPosition = new List<Button> { w1, w2, w3, w4, x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4 };
            enemyPosition = new List<Button> { a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4, d1, d2, d3, d4 };

            // All Comp buttons tag = null, so Comp can place ships there
            for (int i = 0; i < enemyPosition.Count; i++)
                enemyPosition[i].Tag = null;
        }
        private void playerPicksPosition(object sender, EventArgs e)
        {
            // Checks if P has ships to set.
            // If so: button clicked is tagged 'ship'.
            // Button is disabled, P unable to click it again.
            // Button will show image battleship.
            if (totalShips > 0)
            {  
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "playerShip";
                button.BackgroundImage = Properties.Resources.battleship;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                totalShips--;
            }
            // When P has no more ships to set, Comps buttons are enabled for P to attack.
            if (totalShips == 0)
            {
                helpText.Top = 534;
                helpText.Left = 610;
                helpText.Text = "Now attack your enemy by clicking an enemy location";

                for (int i = 0; i < 16; i++)
                    enemyPosition[i].Enabled = true;
            }
        }

        private void enemyPicksPositions(object sender, EventArgs e)
        {
            // Random index from enemyPosition is retrieved while timer is on.
            // Timer is enabled from start game, so Comp places ships directly.
            int index = rand.Next(enemyPosition.Count);

            // If button is empty, tag enemyship on button.
            // No. ships -1
            if (enemyPosition[index].Enabled == true && enemyPosition[index].Tag == null)
            {
                enemyPosition[index].Tag = "enemyShip";
                totalEnemy--;

                Debug.WriteLine("Enemy Position  " + enemyPosition[index].Text);
            }
            // If button is not empty, retrieve another random index.
            else
                index = rand.Next(enemyPosition.Count);

            // When Comp has 0 ships to place, timer stops.
            if (totalEnemy < 1)
                enemyPositionPicker.Stop();
        }

        private void attackEnemyPosition(object sender, EventArgs e)
        {
            // Define clicked button as variable 'button'.
            // Search for the index of the button in the player2Position list.
            var button = sender as Button;
            int index = enemyPosition.IndexOf(button);

            // If button was enabled and rounds > 0.
            // Number of rounds -1 and is displayed.
            if (enemyPosition[index].Enabled && rounds > 0)
            {
                rounds--;
                roundsText.Text = "Rounds: " + rounds;

                // If clicked button has tag ship. Button is disabeld.
                // Button background is fireIcon and blue color.
                // Backgroundimage gets tag 'hit'.
                // Score P1 +1 and is displayed.
                if ((string)enemyPosition[index].Tag == "enemyShip")
                {
                    enemyPosition[index].Enabled = false;
                    enemyPosition[index].BackgroundImage = Properties.Resources.fireIcon;
                    enemyPosition[index].BackColor = System.Drawing.Color.DarkBlue;
                    enemyPosition[index].BackgroundImage.Tag = "hit";
                    playerTotalScore++;
                    playerScore.Text = "" + playerTotalScore;
                    enemyPlayTimer.Start();
                }
                // If clicked button doesn't have tag, button is disabled.
                // Button background is miss Icon and blue color.
                // Start enemyPlayTimer for Comp to attack.
                else
                {
                    enemyPosition[index].Enabled = false;
                    enemyPosition[index].BackgroundImage = Properties.Resources.missIcon;
                    enemyPosition[index].BackColor = System.Drawing.Color.DarkBlue;
                    enemyPlayTimer.Start();
                }

                // Comp buttons disabled for Comp to attack P.
                for (int i = 0; i < 16; i++)
                    enemyPosition[i].Enabled = false;
            }
        }
        // Same as attackEnemyPosition.
        // Executed when enemyPlayTimer starts.
        private void enemyAttackPlayer(object sender, EventArgs e)
        {
            if (playerPosition.Count > 0 && rounds > 0)
            {
                rounds--;
                roundsText.Text = "Rounds: " + rounds;

                int index = rand.Next(playerPosition.Count);

                if ((string)playerPosition[index].Tag == "playerShip")
                {
                    playerPosition[index].BackgroundImage = Properties.Resources.fireIcon;
                    playerPosition[index].Enabled = false;
                    playerPosition[index].BackColor = System.Drawing.Color.DarkBlue;
                    playerPosition[index].BackgroundImage.Tag = "hit";
                    // Remove index from list so Comp cannot retreive it again. 
                    playerPosition.RemoveAt(index);
                    enemyTotalScore++;
                    enemyScore.Text = "" + enemyTotalScore;
                    enemyPlayTimer.Stop();
                }
                else
                {
                    playerPosition[index].BackgroundImage = Properties.Resources.missIcon;
                    playerPosition[index].Enabled = false;
                    playerPosition[index].BackColor = System.Drawing.Color.DarkBlue;
                    playerPosition.RemoveAt(index);
                    enemyPlayTimer.Stop();
                }
                // Comp buttons enabled for P to attack.
                for (int i = 0; i < 16; i++)
                    enemyPosition[i].Enabled = true;
            }

            // When rounds = 0, or P1 has 3 pnts, or P2 has 3 pnts:
            // Ships are displayed when not hit.
            // Hit ships are displayed when hit.
            if (rounds < 1 || playerTotalScore > 2 || enemyTotalScore > 2)
            {
                for (int i = 0; i < 16; i++)
                {
                    if ((string)playerPosition[i].Tag == "playerShip" && playerPosition[i].BackgroundImage == null)
                        playerPosition[i].BackgroundImage = Properties.Resources.battleship;

                    if (playerPosition[i].BackgroundImage != null && (string)playerPosition[i].BackgroundImage.Tag == "hit")
                        playerPosition[i].BackgroundImage = Properties.Resources.battleshiphit;

                    if ((string)enemyPosition[i].Tag == "enemyShip" && enemyPosition[i].BackgroundImage == null)
                        enemyPosition[i].BackgroundImage = Properties.Resources.battleship;

                    if (enemyPosition[i].BackgroundImage != null && (string)enemyPosition[i].BackgroundImage.Tag == "hit")
                        enemyPosition[i].BackgroundImage = Properties.Resources.battleshiphit;
                }

                // Messagebox displayed with message dependent of winner.
                if (playerTotalScore > enemyTotalScore)
                    MessageBox.Show("You win!", "winning");

                if (playerTotalScore == enemyTotalScore)
                    MessageBox.Show("No one wins this", "Draw");

                if (enemyTotalScore > playerTotalScore)
                    MessageBox.Show("Haha! I sunk your battle ships", "Lost");
            }
        }
    }
}
