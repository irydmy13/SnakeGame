using System;
using System.Collections.Generic;

class Snake
{
    // Список позиций тела змеи
    private List<(int x, int y)> body = new List<(int, int)> { (10, 10) };

    // Направление движения по X и Y
    private int dx = 1, dy = 0;

    // Свойство — очки (длина - 1)
    public int Score => body.Count - 1;

    // Перемещение змеи
    public void Move()
    {
        var head = body[^1]; // Голова
        var newHead = (head.x + dx, head.y + dy);
        body.Add(newHead); // Новая голова
        Console.SetCursorPosition(newHead.x, newHead.y);
        Console.Write("O");

        var tail = body[0]; // Стираем хвост
        Console.SetCursorPosition(tail.x, tail.y);
        Console.Write(" ");
        body.RemoveAt(0);
    }

    // Изменение направления при нажатии клавиши
    public void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow: dx = 0; dy = -1; break;
            case ConsoleKey.DownArrow: dx = 0; dy = 1; break;
            case ConsoleKey.LeftArrow: dx = -1; dy = 0; break;
            case ConsoleKey.RightArrow: dx = 1; dy = 0; break;
        }
    }

    // Увеличение длины
    public void Grow()
    {
        body.Insert(0, body[0]); // Копируем хвост
    }

    // Проверка столкновения с едой
    public bool CheckCollision(Food food)
    {
        var head = body[^1];
        return head == (food.X, food.Y);
    }

    // Проверка столкновения с собой или стеной
    public bool HasCollided()
    {
        var head = body[^1];

        // Столкновение с телом
        foreach (var part in body[..^1])
            if (part == head) return true;

        // Столкновение со стеной
        return head.x < 0 || head.y < 0 || head.x >= Console.WindowWidth || head.y >= Console.WindowHeight;
    }
}
