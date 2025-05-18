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


namespace Simo_Says__Game
{
    public partial class Form1 : Form
    {
        int blocksX = 160;
        int blocksY = 80;
        int score = 0;
        int level = 3;

        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<PictureBox> chosenBoxes = new List<PictureBox>();
        Random rand = new Random();

        Color temp;

        int index = 0;
        int tries = 0;
        int timeLimit = 0;
        bool selectingColors = false;

        string correctOrder = string.Empty;
        string playerOrder = string.Empty;


        public Form1()
        {
            InitializeComponent();
            SetUpBlocks();

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (selectingColors == true)
            {
                timeLimit++;

                switch (timeLimit)
                {
                    case 10:
                        temp = chosenBoxes[index].BackColor;
                        chosenBoxes[index].BackColor = Color.White;
                        break;
                    case 20:
                        chosenBoxes[index].BackColor = temp;
                        break;
                    case 30:
                        chosenBoxes[index].BackColor = Color.White;
                        break;
                    case 40:
                        chosenBoxes[index].BackColor = temp;
                        break;
                    case 50:
                        if (index < chosenBoxes.Count - 1)
                        {
                            index++;
                            timeLimit = 0;
                        }
                        else
                        {
                            selectingColors = false;
                        }
                        break;

                }
            }
                if (tries >= level)
                {
                    
                    if (correctOrder == playerOrder)
                    {
                        tries = 0;
                        Gametimer.Stop();
                        MessageBox.Show("Well done, you got them correctly", "moo Says: ");
                        score++;
                    }
                    else
                    {
                        tries = 0;
                        Gametimer.Stop();
                        MessageBox.Show("Your gusses did not match,try again", "moo Says:");

                    }
                }


                lblInfo.Text = "Click on " + level + "blocks in the same sequence";



            }

        

            private void ButtonClickEvent(object sender, EventArgs e)
            {
            if (score == 3 && level < 7)
            {
                level++;
                score = 0;
            }

            correctOrder = string.Empty;
            playerOrder = string.Empty;
            chosenBoxes.Clear();
            chosenBoxes = pictureBoxes.OrderBy(x => rand.Next()).Take(level).ToList();

            for (int i = 0; i < chosenBoxes.Count; i++)
            {
                correctOrder += chosenBoxes[i].Name + " ";
            }

            foreach (PictureBox x in pictureBoxes)
            {
                x.BackColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            }
            Debug.WriteLine(correctOrder);
            index = 0;
            timeLimit = 0;
            selectingColors = true;
            Gametimer.Start();





            }

        private void SetUpBlocks()
        {
            for (int i = 1; i < 17; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Name = "Pic_" + i;
                newPic.Height = 60;
                newPic.Width = 60;
                newPic.BackColor = Color.Black;
                newPic.Left = blocksX;
                newPic.Top = blocksY;
                newPic.Click += ClickOnPictureBox;

                if (i == 4 || i == 8 || i == 12)
                {
                    blocksY += 65;
                    blocksX = 160;
                }
                else
                {
                    blocksX += 65;
                }

                this.Controls.Add(newPic);
                pictureBoxes.Add(newPic);
            }


        }

        private void ClickOnPictureBox(object sender, EventArgs e)
        {
            if (!selectingColors && chosenBoxes.Count > 1)
            {
                PictureBox temp = sender as PictureBox;
                temp.BackColor = Color.Black;
                playerOrder += temp.Name + " ";
                Debug.WriteLine(playerOrder);
                tries++;
            }
            else
            {
                return ;
            }

        }
    }
}
