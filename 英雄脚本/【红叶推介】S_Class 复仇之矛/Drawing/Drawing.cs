using SharpDX;
using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace S_Class_Kalista
{
    internal class Drawing
    {
        public struct Circle
        {
            public Vector3 Position;
            public LeagueSharp.Common.Circle NativeCircle;
            public float LifeTime;
        }

        public struct Line
        {
            public int Thinkness;
            public LeagueSharp.Common.Render.Line NativeLine;
            public float LifeTime;
        }

        private static readonly Dictionary<String, Line> Lines = new Dictionary<String, Line>();
        private static readonly Dictionary<String, Circle> Circles = new Dictionary<String, Circle>();

        public static bool Delete(string name, char type)
        {
            switch (type)
            {
                case 'c':
                    return Circles.Remove(name);

                case 'l':
                    return Lines.Remove(name);

                default:
                    return false;
            }
        }

        public static void Add(string name, Circle c)
        {
            Circles.Add(name, c);
        }

        public static void Add(string name, Line l)
        {
            Lines.Add(name, l);
        }

        private static void Draw(Circle c)
        {
            LeagueSharp.Drawing.DrawCircle(c.Position, c.NativeCircle.Radius, c.NativeCircle.Color);
        }

        private static Color GetManagedColor(ColorBGRA c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        private static void Draw(Line c)
        {
            LeagueSharp.Drawing.DrawLine(c.NativeLine.Start, c.NativeLine.End, c.Thinkness, GetManagedColor(c.NativeLine.Color));
        }

        public static void Draw_onDraw()
        {
            foreach (var c in Circles)
            {
                if (Properties.Time.TickCount > c.Value.LifeTime)
                    if (Delete(c.Key, 'c')) continue;

                Draw(c.Value);
            }

            foreach (var l in Lines)
            {
                if (Properties.Time.TickCount > l.Value.LifeTime)
                    if (Delete(l.Key, 'l')) continue;

                Draw(l.Value);
            }
        }
    }
}