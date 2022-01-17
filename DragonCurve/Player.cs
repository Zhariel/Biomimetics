using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Player : Node2D
{
    float origin = 512;
    int size = 2;
    int depth = 19;
    List<Tuple<Vector2, Vector2>> points = new List<Tuple<Vector2, Vector2>>();

    public override void _Draw()
    {
        Update();
        foreach (Tuple<Vector2, Vector2> t in points)
        {
            DrawLine(t.Item1, t.Item2, new Color(255, 0, 255));
        }
    }

    public override void _Ready()
    {
        GD.Print("Initiating");
        points.Add(Tuple.Create(new Vector2(origin-size/2f, origin-size/2f), new Vector2(origin+size/2f, origin-size/2f)));

        Unfold(depth, 90f);
    }

    Vector2 Rotate(Vector2 vec, Vector2 axis, float angle)
    {
        float rad = angle * Convert.ToSingle(Math.PI) / 180f;
        return new Vector2(Convert.ToSingle(Math.Cos(rad)) * (vec.x - axis.x) - Convert.ToSingle(Math.Sin(rad)) * (vec.y - axis.y) + axis.x,
                           Convert.ToSingle(Math.Sin(rad)) * (vec.x - axis.x) - Convert.ToSingle(Math.Cos(rad)) * (vec.y - axis.y) + axis.y);
    }

    void Unfold(int depth, float angle)
    {
        GD.Print(points.Count);
        if (depth == 0) return;

        List<Tuple<Vector2, Vector2>> newDragon = new List<Tuple<Vector2, Vector2>>();
        points.ForEach( x => newDragon.Add(new Tuple<Vector2, Vector2>(
            Rotate(x.Item1, points.Last().Item2, angle),
            Rotate(x.Item2, points.Last().Item2, angle)
            )));

        newDragon.Reverse();
        newDragon[newDragon.Count-1] = new Tuple<Vector2, Vector2>(newDragon.Last().Item2, newDragon.Last().Item1);

        points.AddRange(newDragon);
        
        Unfold(depth-1, angle);
    }
}





