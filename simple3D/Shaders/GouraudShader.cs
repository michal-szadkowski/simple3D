using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Models;
using simple3D.Maths;

namespace simple3D.Shaders
{
    public class GouraudShader : IShader
    {
        (Vert, Vec3) v1, v2, v3;
        (int, int, int, int) c1, c2, c3;
        int m = 5;
        public GouraudShader((Vert, Vec3) v1, (Vert, Vec3) v2, (Vert, Vec3) v3, Vec3 camPos, List<Light> lights)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            if (v1.Item1.c != null && v2.Item1.c != null && v3.Item1.c != null)
            {

                c1 = LightMod.int2bytes(LightMod.LightModel(v1.Item1.n.Normalized(), v1.Item1.wv, lights, camPos, (int)v1.Item1.c, v1.Item2.z));
                c2 = LightMod.int2bytes(LightMod.LightModel(v2.Item1.n.Normalized(), v2.Item1.wv, lights, camPos, (int)v2.Item1.c, v2.Item2.z));
                c3 = LightMod.int2bytes(LightMod.LightModel(v3.Item1.n.Normalized(), v3.Item1.wv, lights, camPos, (int)v3.Item1.c, v3.Item2.z));
            }

        }

        public int GetColor(int x, int y, (double, double, double) barCoords)
        {
            double z = v1.Item2.z * barCoords.Item1 + v2.Item2.z * barCoords.Item2 + v3.Item2.z * barCoords.Item3;
            return LightMod.bytes2int((int)(z * 255),
                            (int)(c1.Item2 * barCoords.Item1 + c2.Item2 * barCoords.Item2 + c3.Item2 * barCoords.Item3),
                            (int)(c1.Item3 * barCoords.Item1 + c2.Item3 * barCoords.Item2 + c3.Item3 * barCoords.Item3),
                            (int)(c1.Item4 * barCoords.Item1 + c2.Item4 * barCoords.Item2 + c3.Item4 * barCoords.Item3));
        }

    }
}
