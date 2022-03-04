using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using simple3D.Models;
using simple3D.Maths;
using simple3D.Shaders;

namespace simple3D
{
    public class Renderer
    {
        double eps = 1E-15;
        public int width = 1200, height = 900;
        public string lastRenderTime = String.Empty;
        public IShaderFactory shaderFactory = new FlatShaderFactory();

        private void Fill(WrappedTriangle triangle, int[,] bitmap, Vec3 camPos, List<Light> lights)
        {
            (Vert, Vec3) v1 = (triangle.a, ToScreenCoord(triangle.a.v));
            (Vert, Vec3) v2 = (triangle.b, ToScreenCoord(triangle.b.v));
            (Vert, Vec3) v3 = (triangle.c, ToScreenCoord(triangle.c.v));
            if (v1.Item2.y > v3.Item2.y) Swap(ref v1, ref v3);
            if (v1.Item2.y > v2.Item2.y) Swap(ref v1, ref v2);
            if (v2.Item2.y > v3.Item2.y) Swap(ref v2, ref v3);

            IShader shader = shaderFactory.GetShader(v1, v2, v3, camPos, lights);

            if (Math.Abs(v1.Item2.y - v2.Item2.y) < eps)
            {
                double slope1 = (double)(v1.Item2.x - v3.Item2.x) / (v1.Item2.y - v3.Item2.y);
                double slope2 = (double)(v2.Item2.x - v3.Item2.x) / (v2.Item2.y - v3.Item2.y);

                double x1 = v1.Item2.x;
                double x2 = v2.Item2.x;
                for (int y = (int)v1.Item2.y; y < (int)v3.Item2.y; y++)
                {
                    for (int x = (int)Math.Round(Math.Min(x1, x2)); x < (int)Math.Round(Math.Max(x1, x2)); x++)
                    {
                        var p = barycentric(v1.Item2.x, v1.Item2.y, v2.Item2.x, v2.Item2.y, v3.Item2.x, v3.Item2.y, x, y);
                        double z = p.Item1 * v1.Item2.z + p.Item2 * v2.Item2.z + p.Item3 * v3.Item2.z;
                        z *= 255;
                        var a = LightMod.int2bytes(bitmap[x, y]);
                        if (a.Item1 > z)
                            bitmap[x, y] = shader.GetColor(x, y, p);
                    }
                    x1 += slope1;
                    x2 += slope2;
                }
                return;
            }
            else if (Math.Abs(v2.Item2.y - v3.Item2.y) < eps)
            {
                double slope1 = (double)(v2.Item2.x - v1.Item2.x) / (v2.Item2.y - v1.Item2.y);
                double slope2 = (double)(v3.Item2.x - v1.Item2.x) / (v3.Item2.y - v1.Item2.y);
                double x1 = v1.Item2.x;
                double x2 = v1.Item2.x;
                for (int y = (int)v1.Item2.y; y < (int)v3.Item2.y; y++)
                {
                    for (int x = (int)Math.Round(Math.Min(x1, x2)); x < (int)Math.Round(Math.Max(x1, x2)); x++)
                    {
                        var p = barycentric(v1.Item2.x, v1.Item2.y, v2.Item2.x, v2.Item2.y, v3.Item2.x, v3.Item2.y, x, y);
                        double z = p.Item1 * v1.Item2.z + p.Item2 * v2.Item2.z + p.Item3 * v3.Item2.z;
                        var a = LightMod.int2bytes(bitmap[x, y]);
                        z *= 255;
                        if (a.Item1 > z)
                            bitmap[x, y] = shader.GetColor(x, y, p);
                    }
                    x1 += slope1;
                    x2 += slope2;
                }
                return;
            }
            else
            {
                double slope1 = (double)(v2.Item2.x - v1.Item2.x) / (v2.Item2.y - v1.Item2.y);
                double slope2 = (double)(v3.Item2.x - v1.Item2.x) / (v3.Item2.y - v1.Item2.y);
                double x1 = v1.Item2.x;
                double x2 = v1.Item2.x;
                for (int y = (int)v1.Item2.y; y < (int)v2.Item2.y; y++)
                {
                    for (int x = (int)Math.Round(Math.Min(x1, x2)); x < (int)Math.Round(Math.Max(x1, x2)); x++)
                    {
                        var p = barycentric(v1.Item2.x, v1.Item2.y, v2.Item2.x, v2.Item2.y, v3.Item2.x, v3.Item2.y, x, y);
                        double z = p.Item1 * v1.Item2.z + p.Item2 * v2.Item2.z + p.Item3 * v3.Item2.z;
                        var a = LightMod.int2bytes(bitmap[x, y]);
                        z *= 255;
                        if (a.Item1 > z)
                            bitmap[x, y] = shader.GetColor(x, y, p);
                    }
                    x1 += slope1;
                    x2 += slope2;
                }
                x1 = v2.Item2.x;

                slope1 = (double)(v3.Item2.x - v2.Item2.x) / (v3.Item2.y - v2.Item2.y);

                for (int y = (int)v2.Item2.y; y < (int)v3.Item2.y; y++)
                {
                    for (int x = (int)Math.Round(Math.Min(x1, x2)); x < (int)Math.Round(Math.Max(x1, x2)); x++)
                    {
                        var p = barycentric(v1.Item2.x, v1.Item2.y, v2.Item2.x, v2.Item2.y, v3.Item2.x, v3.Item2.y, x, y);
                        double z = p.Item1 * v1.Item2.z + p.Item2 * v2.Item2.z + p.Item3 * v3.Item2.z;
                        z *= 255;
                        var a = LightMod.int2bytes(bitmap[x, y]);
                        if (a.Item1 > z)
                            bitmap[x, y] = shader.GetColor(x, y, p);
                    }
                    x1 += slope1;
                    x2 += slope2;
                }
            }

        }



        private (double, double, double) barycentric(double x1, double y1, double x2, double y2, double x3, double y3, double xp, double yp)
        {
            double p12p = triangleArea(x1, y1, x2, y2, xp, yp);
            double p23p = triangleArea(x2, y2, x3, y3, xp, yp);
            double p31p = triangleArea(x3, y3, x1, y1, xp, yp);
            double p123 = triangleArea(x1, y1, x2, y2, x3, y3);
            if (p123 < eps) return (1 / 3, 1 / 3, 1 / 3);
            if (p12p + p23p + p31p > p123 + eps) p123 = p12p + p23p + p31p;
            return (p23p / p123, (p123 - p12p - p23p) / p123, p12p / p123);
        }
        private double triangleArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return 0.5 * Math.Abs(x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));
        }

        private void Swap(ref (Vert, Vec3) a, ref (Vert, Vec3) b)
        {
            (Vert, Vec3) t = a;
            a = b;
            b = t;
        }
        private void BresenhamDrawLine(Vec3 p1, Vec3 p2, int[,] bitmap)
        {
            int x0 = (int)p1.x;
            int x1 = (int)p2.x;
            int y0 = (int)p1.y;
            int y1 = (int)p2.y;
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                bitmap[x0, y0] = Color.Black.ToArgb();
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }
        private Vec3 ToScreenCoord(Vec4 v)
        {
            Vec3 vec = new Vec3(((-v.y + 1) * (height - 1)) / 2, ((v.x + 1) * (width - 1)) / 2, (v.z + 1) / 2);
            return vec;
        }
        public int[,] Render(Scene scene, int[,]? bitmap)
        {
            Stopwatch sw = new Stopwatch();
            long ticks = 0;

            if (bitmap == null)
                bitmap = new int[height, width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++) bitmap[i, j] = Color.Gray.ToArgb();
            sw.Start();
            List<WrappedTriangle> triangles = scene.GetClipped((double)(height) / (double)(width));
            List<Light> lights = scene.GetLightsInWorldSpace();
            sw.Stop();

            lastRenderTime = sw.Elapsed.ToString();
            ticks += sw.ElapsedTicks;
            sw.Restart();
            Parallel.ForEach(triangles, triangle =>
             {
                 var d = triangle.Orientation();
                 if (-d >= 0)
                 {
                     Fill(triangle, bitmap, scene.currentCam.pos, lights);
                     //BresenhamDrawLine(ToScreenCoord(triangle.a.v), ToScreenCoord(triangle.b.v), bitmap);
                     //BresenhamDrawLine(ToScreenCoord(triangle.b.v), ToScreenCoord(triangle.c.v), bitmap);
                     //BresenhamDrawLine(ToScreenCoord(triangle.c.v), ToScreenCoord(triangle.a.v), bitmap);
                 }
             });

            //foreach (var triangle in triangles)
            //{
            //    var d = triangle.Orientation();
            //    if (-d < 0) continue;
            //    Fill(triangle, bitmap, scene.currentCam.pos, lights);
            //    //    BresenhamDrawLine(ToScreenCoord(triangle.a.v), ToScreenCoord(triangle.b.v), bitmap);
            //    //    BresenhamDrawLine(ToScreenCoord(triangle.b.v), ToScreenCoord(triangle.c.v), bitmap);
            //    //    BresenhamDrawLine(ToScreenCoord(triangle.c.v), ToScreenCoord(triangle.a.v), bitmap);
            //}
            sw.Stop();
            lastRenderTime += "  " + sw.Elapsed.ToString();
            lastRenderTime += "  " + TimeSpan.TicksPerSecond / (sw.ElapsedTicks + ticks);

            return bitmap;
        }
    }
}
