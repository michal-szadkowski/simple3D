using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;
using simple3D.Models;

namespace simple3D.Shaders
{

    public class LightMod
    {
        public static double eps = 1E-10;
        public static double ka = 0.1, kd = 0.6, ks = 0.9;
        public static int m = 2;
        public static Color fogcolor = Color.Gray;
        public static int LightModel(Vec3 normal, Vec3 pos, List<Light> lights, Vec3 camPos, int pColor, double z)
        {

            Vec3 toCam = camPos.Subtract(pos);
            toCam.Normalize();
            Color ocol = Color.FromArgb(pColor);
            var objCol = (ocol.A, ocol.R, ocol.G, ocol.B);

            int R = 0, G = 0, B = 0;
            R = (int)Math.Round(ka * objCol.Item2);
            G = (int)Math.Round(ka * objCol.Item3);
            B = (int)Math.Round(ka * objCol.Item4);

            for (int i = 0; i < lights.Count; i++)
            {
                //
                var ligCol = int2bytes(lights[i].Color.ToArgb());
                Vec3 toLight = lights[i].pos.Subtract(pos);
                double ligDistanceSq = toLight.LengthSquared();
                double intensity = 1.0 / (1 + 0.03 * ligDistanceSq);

                if (lights[i].isRef == true)
                {
                    var d = toLight.Normalized().Dot(lights[i].orientation.Normalized());
                    intensity *= Math.Clamp(-d, 0, 1);
                }


                toLight.Normalize();

                double nlDot = Math.Max(0, toLight.Dot(normal));
                double D = kd * nlDot / 255.0 * intensity;
                R += (int)Math.Round(D * (ligCol.Item2) * (objCol.Item2));
                G += (int)Math.Round(D * (ligCol.Item3) * (objCol.Item3));
                B += (int)Math.Round(D * (ligCol.Item4) * (objCol.Item4));

                //
                double inter = 0;
                if (nlDot > 0)
                    inter = Math.Max(0, toCam.Dot(normal.Multiply(2 * nlDot) - toLight));
                double power = Math.Pow(inter, m);
                double S = ks * power / 255.0 * intensity;
                R += (int)Math.Round(S * (ligCol.Item2));
                G += (int)Math.Round(S * (ligCol.Item3));
                B += (int)Math.Round(S * (ligCol.Item4));

            }
            double fog = Math.Pow(2, z * z * z * z * z * z) - 1;
            R = Math.Clamp((int)(R * (1 - fog) + fogcolor.R * fog), 0, 255);
            G = Math.Clamp((int)(G * (1 - fog) + fogcolor.G * fog), 0, 255);
            B = Math.Clamp((int)(B * (1 - fog) + fogcolor.B * fog), 0, 255);
            return bytes2int((int)(255 * z), R, G, B);
        }

        public static (int, int, int, int) int2bytes(int a)
        {
            return ((a >> 24) & 0xFF, (a >> 16) & 0xFF, (a >> 8) & 0xFF, (a) & 0xFF);
        }
        public static int bytes2int(int a, int b, int c, int d)
        {
            return (a << 24) + (b << 16) + (c << 8) + (d << 0);
        }
        public static (double, double, double) barycentric(double x1, double y1, double x2, double y2, double x3, double y3, double xp, double yp)
        {
            double p12p = triangleArea(x1, y1, x2, y2, xp, yp);
            double p23p = triangleArea(x2, y2, x3, y3, xp, yp);
            double p31p = triangleArea(x3, y3, x1, y1, xp, yp);
            double p123 = triangleArea(x1, y1, x2, y2, x3, y3);
            if (p123 < eps) return (1 / 3, 1 / 3, 1 / 3);
            if (p12p + p23p + p31p > p123 + eps) p123 = p12p + p23p + p31p;
            return (p23p / p123, (p123 - p12p - p23p) / p123, p12p / p123);
        }
        public static double triangleArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return 0.5 * Math.Abs(x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));
        }
    }
}
