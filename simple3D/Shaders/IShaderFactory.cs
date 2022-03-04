using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Models;
using simple3D.Maths;

namespace simple3D.Shaders
{
    public interface IShaderFactory
    {
        public IShader GetShader((Vert, Vec3) v1, (Vert, Vec3) v2, (Vert, Vec3) v3, Vec3 camPos, List<Light> lights);
    }
}
