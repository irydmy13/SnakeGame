using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private PowerUp currentPowerUp; // текущий бонус
        private Random rnd = new Random(); // для генерации координат и эффекта
        private int powerUpCounter = 0; // счётчик времени до появления бонуса
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void GeneratePowerUp()
        {
            string[] effects = { "speed", "slow", "bonusPoints" }; // возможные эффекты
            currentPowerUp = new PowerUp
            {
                X = rnd.Next(0, Settings.Width),
                Y = rnd.Next(0, Settings.Height),
                Effect = effects[rnd.Next(effects.Length)]
            };
        }
    }
}

