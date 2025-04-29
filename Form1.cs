using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private Snake snake;
        private Food food;
        private PowerUp currentPowerUp;
        private Random rnd = new Random();
        private int powerUpCounter = 0;
        private int score = 0;
        private Sounds sounds = new Sounds();
        private ScoreManager scoreManager = new ScoreManager();
        private Player player;

        public Form1()
        {
            InitializeComponent();
            new Settings(); // загружаем настройки
            StartGame();
        }

        private void StartGame()
        {
            snake = new Snake();
            food = new Food();
            GenerateFood();

            score = 0;
            powerUpCounter = 0;
            currentPowerUp = null;

            sounds.PlayBackground();

            timer1.Interval = 100;
            timer1.Start();
        }

        private void GenerateFood()
        {
            food.X = rnd.Next(0, this.Width / Settings.Width);
            food.Y = rnd.Next(0, this.Height / Settings.Height);
        }

        private void GeneratePowerUp()
        {
            string[] effects = { "speed", "slow", "bonusPoints" };
            currentPowerUp = new PowerUp
            {
                X = rnd.Next(0, this.Width / Settings.Width),
                Y = rnd.Next(0, this.Height / Settings.Height),
                Effect = effects[rnd.Next(effects.Length)]
            };
        }

        private void ApplyPowerUp(string effect)
        {
            switch (effect)
            {
                case "speed":
                    timer1.Interval = Math.Max(10, timer1.Interval - 20);
                    break;
                case "slow":
                    timer1.Interval += 20;
                    break;
                case "bonusPoints":
                    score += 5;
                    break;
            }
            sounds.PlayEat();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Движение змейки
            snake.Move();

            // Проверка съедания еды
            if (snake.Body[0].X == food.X && snake.Body[0].Y == food.Y)
            {
                snake.Grow();
                score++;
                GenerateFood();
                sounds.PlayEat();
            }

            // Бонус появляется раз в 150 тиков
            powerUpCounter++;
            if (powerUpCounter >= 150)
            {
                GeneratePowerUp();
                powerUpCounter = 0;
            }

            // Столкновение с бонусом
            if (currentPowerUp != null &&
                snake.Body[0].X == currentPowerUp.X &&
                snake.Body[0].Y == currentPowerUp.Y)
            {
                ApplyPowerUp(currentPowerUp.Effect);
                currentPowerUp = null;
            }

            // Проверка на проигрыш
            if (snake.HasCollision(this.Width / Settings.Width, this.Height / Settings.Height))
            {
                GameOver();
            }

            this.Invalidate();
        }

        private void GameOver()
        {
            timer1.Stop();
            sounds.StopBackground();
            sounds.PlayGameOver();

            string playerName = Prompt.ShowDialog("Введите ваше имя:", "Конец игры");
            player = new Player(playerName);
            scoreManager.SaveScore(player.Name, score);

            MessageBox.Show($"Игра окончена! Ваш счёт: {score}", "Игра окончена");

            // Показать ТОП игроков
            var topScores = scoreManager.LoadScores();
            string message = "🏆 ТОП игроков:\n";
            int count = 0;
            foreach (var item in topScores)
            {
                message += $"{item.Name}: {item.Score}\n";
                count++;
                if (count >= 5) break;
            }
            MessageBox.Show(message, "Таблица рекордов");

            Application.Exit();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            // Рисуем змейку
            for (int i = 0; i < snake.Body.Count; i++)
            {
                Brush color = (i == 0) ? Brushes.Black : Brushes.Green;
                canvas.FillEllipse(color,
                    new Rectangle(snake.Body[i].X * Settings.Width,
                                  snake.Body[i].Y * Settings.Height,
                                  Settings.Width, Settings.Height));
            }

            // Рисуем еду
            canvas.FillEllipse(Brushes.Red,
                new Rectangle(food.X * Settings.Width,
                              food.Y * Settings.Height,
                              Settings.Width, Settings.Height));

            // Рисуем бонус (PowerUp)
            if (currentPowerUp != null)
            {
                Brush powerBrush = Brushes.Purple;
                canvas.FillEllipse(powerBrush,
                    new Rectangle(currentPowerUp.X * Settings.Width,
                                  currentPowerUp.Y * Settings.Height,
                                  Settings.Width, Settings.Height));
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    snake.ChangeDirection("left");
                    break;
                case Keys.Right:
                    snake.ChangeDirection("right");
                    break;
                case Keys.Up:
                    snake.ChangeDirection("up");
                    break;
                case Keys.Down:
                    snake.ChangeDirection("down");
                    break;
            }
        }
    }

    // Вспомогательный класс для ввода имени игрока
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 250,
                Height = 150,
                Text = caption
            };
            Label textLabel = new Label() { Left = 10, Top = 10, Text = text };
            TextBox inputBox = new TextBox() { Left = 10, Top = 40, Width = 200 };
            Button confirmation = new Button() { Text = "OK", Left = 75, Width = 80, Top = 70 };
            confirmation.DialogResult = DialogResult.OK;
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "Игрок";
        }
    }
}