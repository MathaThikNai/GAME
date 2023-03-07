 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Runner
{
    public partial class trex1 : Form
    {
        bool jump = false;
        int jumpingSpeed;
        int frc = 12;
        int scoreofthegame = 0;
        int obstSpeed = 10;
        Random random = new Random();
        int pos;
        bool isgameover = false;



        public trex1()
        {
            InitializeComponent();

            GameReset();
        }

        private void TrexGameEvent(object sender, EventArgs e)
        {
            trex.Top += jumpingSpeed;

            txtScore.Text = "Score - " + scoreofthegame;

            if (jump == true && frc < 0)
            {
                jump = false;
            }

            if (jump == true)
            {
                jumpingSpeed = -12;
                frc -= 1;
            }
            else
            {
                jumpingSpeed = 12;
            }


            if (trex.Top > 366 && jump == false)
            {
                frc = 12;
                trex.Top = 367;
                jumpingSpeed = 0;
            }


            foreach(Control t in this.Controls)
            {
                if (t is PictureBox && (string)t.Tag == "obstacle")
                {
                    t.Left -= obstSpeed;

                    if (t.Left < -100)
                    {
                        t.Left = this.ClientSize.Width + random.Next(200, 500) + (t.Width * 15);
                        scoreofthegame++;
                    }

                    if (trex.Bounds.IntersectsWith(t.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        txtScore.Text += " Press T to restart the game!";
                        isgameover = true;
                    }
                }
            }

            if (scoreofthegame > 5)
            {
                obstSpeed = 15;
            }



        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jump == false)
            {
                jump = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jump == true)
            {
                jump = false;
            }

            if (e.KeyCode == Keys.T && isgameover == true)
            {
                GameReset();
            }
        }

        private void GameReset()
        {
            frc = 12;
            jumpingSpeed = 0;
            jump = false;
            scoreofthegame = 0;
            obstSpeed = 10;
            txtScore.Text = "Score -  " + scoreofthegame;
            trex.Image = Properties.Resources.running;
            isgameover = false;
            trex.Top = 367;

            foreach (Control t in this.Controls)
            {

                if (t is PictureBox && (string)t.Tag == "obstacle")
                {
                    pos = this.ClientSize.Width + random.Next(500, 800) + (t.Width * 10);

                    t.Left = pos;
                }
            }

            gameTimer.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trex_Click(object sender, EventArgs e)
        {

        }
    }
}
