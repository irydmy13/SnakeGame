using System.Collections.Generic;

namespace SnakeGame
{
    public class Snake
    {
        public List<Circle> Body { get; private set; }
        private string direction;

        public Snake()
        {
            Body = new List<Circle>();
            Body.Add(new Circle { X = 10, Y = 5 }); // начальная позиция
            direction = "right";
        }

        public void Move()
        {
            for (int i = Body.Count - 1; i > 0; i--)
            {
                Body[i].X = Body[i - 1].X;
                Body[i].Y = Body[i - 1].Y;
            }

            switch (direction)
            {
                case "up":
                    Body[0].Y -= 1;
                    break;
                case "down":
                    Body[0].Y += 1;
                    break;
                case "left":
                    Body[0].X -= 1;
                    break;
                case "right":
                    Body[0].X += 1;
                    break;
            }
        }

        public void ChangeDirection(string newDirection)
        {
            // Не даём змейке разворачиваться в обратную сторону
            if ((direction == "up" && newDirection != "down") ||
                (direction == "down" && newDirection != "up") ||
                (direction == "left" && newDirection != "right") ||
                (direction == "right" && newDirection != "left"))
            {
                direction = newDirection;
            }
        }

        public void Grow()
        {
            Circle tail = Body[Body.Count - 1];
            Body.Add(new Circle { X = tail.X, Y = tail.Y });
        }

        public bool HasCollision(int width, int height)
        {
            // Столкновение с границей
            if (Body[0].X < 0 || Body[0].Y < 0 || Body[0].X >= width || Body[0].Y >= height)
                return true;

            // Столкновение с собой
            for (int i = 1; i < Body.Count; i++)
            {
                if (Body[i].X == Body[0].X && Body[i].Y == Body[0].Y)
                    return true;
            }

            return false;
        }
    }
}
