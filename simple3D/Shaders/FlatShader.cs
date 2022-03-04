using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;
using simple3D.Models;

namespace simple3D.Shaders
{
    public class FlatShader : IShader
    {
        (Vert, Vec3) v1, v2, v3;
        int color = 0;
        public FlatShader((Vert, Vec3) v1, (Vert, Vec3) v2, (Vert, Vec3) v3, Vec3 camPos, List<Light> lights)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            Vec3 norm = v1.Item1.n.Add(v2.Item1.n.Add(v3.Item1.n));
            norm.Normalize();

            Vec3 pos = (v1.Item1.wv.Add(v2.Item1.wv.Add(v3.Item1.wv)));
            pos = pos.Multiply(1.0 / 3);

            int pcol = Color.White.ToArgb();
            if (v1.Item1.c != null && v2.Item1.c != null && v3.Item1.c != null)
            {
                var c1 = LightMod.int2bytes((int)v1.Item1.c);
                var c2 = LightMod.int2bytes((int)v2.Item1.c);
                var c3 = LightMod.int2bytes((int)v3.Item1.c);
                pcol = LightMod.bytes2int((int)((c1.Item1 + c2.Item1 + c3.Item1) / 3), (int)((c1.Item2 + c2.Item2 + c3.Item2) / 3),
                    (int)((c1.Item3 + c2.Item3 + c3.Item3) / 3), (int)((c1.Item4 + c2.Item4 + c3.Item4) / 3));
            }

            double z = v1.Item2.z + v2.Item2.z + v3.Item2.z;
            z /= 3;
            color = LightMod.LightModel(norm, pos, lights, camPos, pcol, z);

        }

        public int GetColor(int x, int y, (double, double, double) barCoords)
        {
            var c = LightMod.int2bytes((int)color);
            double z = v1.Item2.z * barCoords.Item1 + v2.Item2.z * barCoords.Item2 + v3.Item2.z * barCoords.Item3;
            return LightMod.bytes2int((int)(z * 255),c.Item2,c.Item3,c.Item4);
        }

    }
}
