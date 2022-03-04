using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple3D.Maths
{
    public static class Transformations
    {
        public static Mat4 Eye()
        {
            Mat4 mat = new Mat4();
            for (int i = 0; i < 4; i++) mat[i, i] = 1;
            return mat;
        }
        public static Mat4 Translation(double x, double y, double z)
        {
            Mat4 mat = Eye();
            mat[0, 3] = x;
            mat[1, 3] = y;
            mat[2, 3] = z;
            return mat;
        }
        public static Mat4 Scale(double x, double y, double z)
        {
            Mat4 mat = Eye();
            mat[0, 0] = x;
            mat[1, 1] = y;
            mat[2, 2] = z;
            return mat;
        }
        public static Mat4 RotationX(double x)
        {
            Mat4 mat = Eye();
            mat[1, 1] = Math.Cos(x);
            mat[2, 2] = Math.Cos(x);
            mat[2, 1] = -Math.Sin(x);
            mat[1, 2] = Math.Sin(x);
            return mat;
        }
        public static Mat4 RotationY(double x)
        {
            Mat4 mat = Eye();
            mat[0, 0] = Math.Cos(x);
            mat[2, 2] = Math.Cos(x);
            mat[0, 2] = -Math.Sin(x);
            mat[2, 0] = Math.Sin(x);
            return mat;
        }
        public static Mat4 RotationZ(double x)
        {
            Mat4 mat = Eye();
            mat[0, 0] = Math.Cos(x);
            mat[1, 1] = Math.Cos(x);
            mat[1, 0] = -Math.Sin(x);
            mat[0, 1] = Math.Sin(x);
            return mat;
        }


        public static Mat4 Projection(double fov, double aspect, double far, double near)
        {
            double ctg = 1.0 / Math.Tan(fov / 2);
            Mat4 mat = new();
            mat[0, 0] = ctg;
            mat[1, 1] = ctg / aspect;
            mat[2, 2] = -(far + near) / (far - near);
            mat[3, 2] = -1;
            mat[2, 3] = (-2 * far * near) / (far - near);
            return mat;
        }
    }
}
