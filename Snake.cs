using System.Collections.Generic;

public class Snake
{
    public List<Circle> Body { get; private set; }

    public Snake()
    {
        Body = new List<Circle>();
        Body.Add(new Circle { X = 10, Y = 5 }); // голова
    }

    public void Move(string direction)
    {
        for (int i = Body.Count - 1; i > 0; i--)
        {
            Body[i].X = Body[i - 1].X;
            Body[i].Y = Body[i - 1].Y;
        }

        switch (direction)
        {
            case "right": Body[0].X++; break;
            case "left": Body[0].X--; break;
            case "up": Body[0].Y--; break;
            case "down": Body[0].Y++; break;
        }
    }

    public void Grow()
    {
        var tail = Body[Body.Count - 1];
        Body.Add(new Circle { X = tail.X, Y = tail.Y });
    }
}
