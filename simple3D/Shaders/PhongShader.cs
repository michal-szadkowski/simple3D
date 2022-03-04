using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Models;
using simple3D.Maths;

namespace simple3D.Shaders
{
    public class PhongShader : IShader
    {

        (Vert, Vec3) v1, v2, v3;
        double eps = 1E-15;
        List<Light> lights;
        Vec3 camPos;
        public PhongShader((Vert, Vec3) v1, (Vert, Vec3) v2, (Vert, Vec3) v3, Vec3 camPos, List<Light> lights)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.lights = lights;
            this.camPos = camPos;
        }

        public int GetColor(int x, int y, (double, double, double) barCoords)
        {
            Vec3 normal = v1.Item1.n.Multiply(barCoords.Item1) + v2.Item1.n.Multiply(barCoords.Item2) + v3.Item1.n.Multiply(barCoords.Item3);
            normal.Normalize();
            Vec3 pos = v1.Item1.wv.Multiply(barCoords.Item1) + v2.Item1.wv.Multiply(barCoords.Item2) + v3.Item1.wv.Multiply(barCoords.Item3);
            int pcol = 0;
            if (v1.Item1.c != null && v2.Item1.c != null && v3.Item1.c != null)
            {
                var c1 = LightMod.int2bytes((int)v1.Item1.c);
                var c2 = LightMod.int2bytes((int)v2.Item1.c);
                var c3 = LightMod.int2bytes((int)v3.Item1.c);
                pcol = LightMod.bytes2int((int)(c1.Item1 * barCoords.Item1 + c2.Item1 * barCoords.Item2 + c3.Item1 * barCoords.Item3),
                            (int)(c1.Item2 * barCoords.Item1 + c2.Item2 * barCoords.Item2 + c3.Item2 * barCoords.Item3),
                            (int)(c1.Item3 * barCoords.Item1 + c2.Item3 * barCoords.Item2 + c3.Item3 * barCoords.Item3),
                            (int)(c1.Item4 * barCoords.Item1 + c2.Item4 * barCoords.Item2 + c3.Item4 * barCoords.Item3));
            }
            double z = barCoords.Item1 * v1.Item2.z + barCoords.Item2 * v2.Item2.z + barCoords.Item3 * v3.Item2.z;
            return LightMod.LightModel(normal, pos, lights, camPos, pcol,z);
        }

    }
}
