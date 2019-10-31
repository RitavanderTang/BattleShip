using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Diagnostics;

namespace Battleship
{

    public partial class Form2 : Form
    {
        // Define the variables used.
        List<Button> player1Position;
        List<Button> player2Position;
        Random rand = new Random();
        int totalShips1 = 3;
        int totalShips2 = 3;
        int rounds = 10;
        int player1TotalScore = 0;
        int player2TotalScore = 0;
        public Form2()
        {
            InitializeComponent();
            loadButtons();

            // Start with disabled P2 buttons.
            // Display comment, doneButton P2 disabled.
            for (int i = 0; i < 16; i++)
                player2Position[i].Enabled = false;
           
            helpText.Top = 534;
            helpText.Left = 112;
            helpText.Text = "Player 1: Time to place your ships";

            doneButton2.Enabled = false;
        }

        // Define lists of buttons for P1 and P2.
        private void loadButtons()
        {
            
            player1Position = new List<Button> { w1, w2, w3, w4, x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4 };
            player2Position = new List<Button> { a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4, d1, d2, d3, d4 };
        }

        private void player1PicksPosition(object sender, EventArgs e)
        {
            // Checks if P1 has ships to set.
            // If so: button clicked will get tag 'ship'.
            // Button is disabled, P1 unable to click it again.
            // Button will show image battleship.
            if (totalShips1 > 0)
            {
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "player1Ship";
                button.BackgroundImage = Properties.Resources.battleship;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                totalShips1--;
            }
            // When P1 has no more ships to set, all his buttons are disabled.
            if (totalShips1 == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    player1Position[i].Enabled = false;
                }
            }
        }

        private void player2PicksPosition(object sender, EventArgs e)
        {
            // Same for P2 as for P1.
            if (totalShips2 > 0)
            {

                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "player2Ship";
                button.BackgroundImage = Properties.Resources.battleship;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                totalShips2--;
            }
            if (totalShips2 == 0)
            {
                // All P1 buttons disabled.
                // All P2 buttons enabled for P1 to attack.
                for (int i = 0; i < 16; i++)
                {
                    player1Position[i].Enabled = false;
                    player2Position[i].Enabled = true;
                }
            }
        }

        private void player1Attack2(object sender, EventArgs e)
        {
            // Clears the HIT! label
            labelP1.Text = "";
            // Define clicked button as variable 'button'.
            // Search for the index of the button in the player2Position list.
            var button = sender as Button;
            int index = player2Position.IndexOf(button);

            // If button was enabled and rounds > 0.
            // Number of rounds -1 and is displayed.
            if (player2Position[index].Enabled && rounds > 0)
            {
                rounds--;
                roundsText.Text = "Rounds: " + rounds;

                // If clicked button has tag ship, display HIT!.
                // Button is disabled, background fireIcon and blue color.
                // Backgroundimage gets tag 'hit'.
                // Score P1 +1 and is displayed.
                if ((string)player2Position[index].Tag == "player2Ship")
                {
                    labelP2.Text = "HIT!";
                    player2Position[index].Enabled = false;
                    player2Position[index].BackgroundImage = Properties.Resources.fireIcon;
                    player2Position[index].BackColor = System.Drawing.Color.DarkBlue;
                    player2Position[index].BackgroundImage.Tag = "hit";
                    player1TotalScore++;
                    player1Score.Text = "" + player1TotalScore;
                }
                // If clicked button doesn't have tag, button is disabled.
                // Button background is miss Icon and blue color.
                else
                {
                    player2Position[index].Enabled = false;
                    player2Position[index].BackgroundImage = Properties.Resources.missIcon;
                    player2Position[index].BackColor = System.Drawing.Color.DarkBlue;
                }
                // P2 buttons disabled, P1 buttons enabled for P2 to attack P1.
                for (int i = 0; i < 16; i++)
                {
                    player2Position[i].Enabled = false;
                    player1Position[i].Enabled = true;
                }

                // Comment will be displayed.
                helpText.Top = 534;
                helpText.Left = 610;
                helpText.Text = "Player 2: your turn to attack Player 1";
            }
        }
        
        // Same as player1Attack2.
        private void player2Attack1(object sender, EventArgs e)
        {

            labelP2.Text = "";
            var button = sender as Button;
            int index = player1Position.IndexOf(button);

            if (player1Position[index].Enabled && rounds > 0)
            {
                rounds--;
                roundsText.Text = "Rounds: " + rounds;

                if ((string)player1Position[index].Tag == "player1Ship")
                {
                    labelP1.Text = "HIT!";
                    player1Position[index].Enabled = false;
                    player1Position[index].BackgroundImage = Properties.Resources.fireIcon;
                    player1Position[index].BackColor = System.Drawing.Color.DarkBlue;
                    player1Position[index].BackgroundImage.Tag = "hit";
                    player2TotalScore++;
                    player2Score.Text = "" + player2TotalScore;
                }
                else
                {
                    player1Position[index].Enabled = false;
                    player1Position[index].BackgroundImage = Properties.Resources.missIcon;
                    player1Position[index].BackColor = System.Drawing.Color.DarkBlue;
                }

                for (int i = 0; i < 16; i++)
                {
                    player1Position[i].Enabled = false;
                    player2Position[i].Enabled = true;
                }

                helpText.Top = 534;
                helpText.Left = 112;
                helpText.Text = "Player 1: your turn to attack Player 2";
            }
          
            // When rounds = 0, or P1 has 3 pnts, or P2 has 3 pnts:
            // Ships are displayed when not hit.
            // Hit ships are displayed when hit.
            if (rounds < 1 || player1TotalScore == 3 || player2TotalScore == 3)
            {
                for (int i = 0; i < 16; i++)
                {
                    if ((string)player1Position[i].Tag == "player1Ship" && player1Position[i].BackgroundImage == null) 
                        player1Position[i].BackgroundImage = Properties.Resources.battleship;
                   
                    if (player1Position[i].BackgroundImage != null && (string)player1Position[i].BackgroundImage.Tag == "hit") 
                        player1Position[i].BackgroundImage = Properties.Resources.battleshiphit;
                  
                    if ((string)player2Position[i].Tag == "player2Ship" && player2Position[i].BackgroundImage == null) 
                        player2Position[i].BackgroundImage = Properties.Resources.battleship;
                
                    if (player2Position[i].BackgroundImage != null && (string)player2Position[i].BackgroundImage.Tag == "hit")
                        player2Position[i].BackgroundImage = Properties.Resources.battleshiphit;
                }

                // Messagebox displayed with message dependent of winner.
                if (player1TotalScore > player2TotalScore)
                    MessageBox.Show("Player 1 wins the battle!", "Congrats player 1");
           
                if (player1TotalScore == player2TotalScore)
                    MessageBox.Show("No one wins this", "Draw");

                if (player2TotalScore > player1TotalScore)
                    MessageBox.Show("Player 2 wins the battle!", "Congrats player 2");

                string btnClicked = MyMessageBox.ShowBox("Do you want to restart or exit the game?", "End");

                if (btnClicked == "1")
                {
                    this.Close();
                }
            }
        }

        // When P1 clicks 'done' his ships will disappear.
        // P2 buttons enabled.
        // Helptext displayed, P1 donebutton disappears, P2s enabled.
        private void doneButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                player1Position[i].BackgroundImage = null;
                player2Position[i].Enabled = true;
            }

            helpText.Top = 534;
            helpText.Left = 610;
            helpText.Text = "Player 2: Time to place your ships";

            doneButton2.Enabled = true;
            doneButton1.Visible = false;
        }

        // Sames as doneButton1.
        private void doneButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                player2Position[i].BackgroundImage = null;
                player2Position[i].Enabled = true;
            }

            helpText.Top = 534;
            helpText.Left = 112;
            helpText.Text = "Player 1: your turn to attack Player 2";

            doneButton2.Visible = false;

            // EventHandler of buttons is changed to attack ships instead of place ships.
            // player1Attack2 or player2Attack1 will be excecuted at button click.
            for (int i = 0; i < 16; i++)
            {
                player2Position[i].Click -= new EventHandler(player2PicksPosition);
                player2Position[i].Click += new EventHandler(player1Attack2);
            }

            for (int i = 0; i < 16; i++)
            {
                player1Position[i].Click -= new EventHandler(player1PicksPosition);
                player1Position[i].Click += new EventHandler(player2Attack1);
            }

        }
    }
}
