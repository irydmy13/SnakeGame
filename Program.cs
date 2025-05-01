using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false; // Прячем курсор
        Snake snake = new Snake();     // Создаём змею
        Food food = new Food();        // Создаём еду
        food.Spawn();                  // Показываем еду на экране

        while (true)
        {
            // Управление: если нажата клавиша — меняем направление
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                snake.ChangeDirection(key.Key);
            }

            snake.Move(); // Двигаем змею

            // Если змея "съела" еду — увеличиваем длину и генерируем новую еду
            if (snake.CheckCollision(food))
            {
                food.Spawn();
                snake.Grow();
            }

            // Проверка на столкновение (границы или сам с собой)
            if (snake.HasCollided())
            {
                Console.Clear();
                Console.WriteLine("Игра окончена!");
                Console.Write("Введите имя игрока: ");
                string name = Console.ReadLine();

                // Сохраняем результат
                ScoreManager.SaveScore(name, snake.Score);

                // Показываем топ игроков
                ScoreManager.ShowTopScores();
                break;
            }

            Thread.Sleep(150); // Задержка движения
        }
    }
}
