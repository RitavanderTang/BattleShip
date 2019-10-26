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

    public partial class Form2 : Form
    {
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

            for (int i = 0; i < 16; i++)
            {
                player2Position[i].Enabled = false;
            }

            helpText.Top = 534;
            helpText.Left = 112;
            helpText.Text = "Player 1: Time to place your ships";

            doneButton2.Enabled = false;
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

        private void player1PicksPosition(object sender, EventArgs e)
        {
            if (totalShips1 > 0)
            {
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "player1Ship";
                button.BackgroundImage = Properties.Resources.battleship;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                totalShips1--;
            }
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

                for (int i = 0; i < 16; i++)
                {
                    player1Position[i].Enabled = false;
                    player2Position[i].Enabled = true;
                }

                               
                for (int i = 0; i < 16; i++)
                {
                    player2Position[i].Click += new EventHandler(player1Attack2);
                }

                for (int i = 0; i < 16; i++)
                {
                    player1Position[i].Click += new EventHandler(player2Attack1);
                }


            }
        }

        private void player1Attack2(object sender, EventArgs e)
        {
            if (totalShips1 == 0 && totalShips2 == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    player2Position[i].Enabled = true;
                }

                if (sender.GetType().Name == "Button")
                {
                    var button = sender as Button;
                    int index = player2Position.IndexOf(button);

                    if (player2Position[index].Enabled && rounds > 0)
                    {
                        rounds--;
                        roundsText.Text = "Rounds: " + rounds;

                        if (player2Position[index].Tag == "player2Ship")
                        {
                            player2Position[index].Enabled = false;
                            player2Position[index].BackgroundImage = Properties.Resources.fireIcon;
                            player2Position[index].BackColor = System.Drawing.Color.DarkBlue;
                            player1TotalScore++;
                            playerScore.Text = "" + player1TotalScore;

                        }
                        else
                        {
                            player2Position[index].Enabled = false;
                            player2Position[index].BackgroundImage = Properties.Resources.missIcon;
                            player2Position[index].BackColor = System.Drawing.Color.DarkBlue;

                        }

                        for (int i = 0; i < 16; i++)
                        {
                            player2Position[i].Enabled = false;
                            player1Position[i].Enabled = true;
                        }

                        helpText.Top = 534;
                        helpText.Left = 610;
                        helpText.Text = "Player 2: your turn to attack Player 1";
                    }
                }
                
            }
        }
        
        private void player2Attack1(object sender, EventArgs e)
        {
            if (totalShips1 == 0 && totalShips2 == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    player1Position[i].Enabled = true;
                }

                if (sender.GetType().Name == "Button")
                {
                    var button = sender as Button;
                    int index = player1Position.IndexOf(button);

                    if (player1Position[index].Enabled && rounds > 0)
                    {
                        rounds--;
                        roundsText.Text = "Rounds: " + rounds;

                        if (player1Position[index].Tag == "player1Ship")
                        {
                            player1Position[index].Enabled = false;
                            player1Position[index].BackgroundImage = Properties.Resources.fireIcon;
                            player1Position[index].BackColor = System.Drawing.Color.DarkBlue;
                            player2TotalScore++;
                            playerScore.Text = "" + player2TotalScore;

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
                }
            }
            if (rounds < 1 || player1TotalScore == 3 || player2TotalScore == 3)
            {
                for (int i = 0; i < 16; i++)
                {

                    if (player1Position[i].Tag == "player1Ship" && player1Position[i].BackgroundImage == null)
                    {

                        player1Position[i].BackgroundImage = Properties.Resources.battleship;
                    }
                    if (player1Position[i].BackgroundImage == Properties.Resources.fireIcon)
                    {
                        player1Position[i].BackgroundImage = Properties.Resources.battleshiphit;
                    }

                    if (player2Position[i].Tag == "player2Ship" && player2Position[i].BackgroundImage == null)
                    {

                        player2Position[i].BackgroundImage = Properties.Resources.battleship;
                    }

                    if (player2Position[i].BackgroundImage == Properties.Resources.fireIcon)
                    {
                        player2Position[i].BackgroundImage = Properties.Resources.battleshiphit;
                    }

                }

                if (player1TotalScore > player2TotalScore)
                {
                    MessageBox.Show("Player 1 wins the battle!", "Congrats player 1");
                }
                if (player1TotalScore == player2TotalScore)
                {
                    MessageBox.Show("No one wins this", "Draw");
                }
                if (player2TotalScore > player1TotalScore)
                {
                    MessageBox.Show("Player 2 wins the battle!", "Congrats player 2");
                }
            }
        }

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
        }
    }
}
