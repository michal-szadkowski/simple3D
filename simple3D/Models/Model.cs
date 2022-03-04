using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;

namespace simple3D.Models
{
    public class Model : IModel
    {
        public List<Triangle> triangles = new List<Triangle>();
        public int[,]? texture;
        public int[,]? normal;
        public int? color = Color.SlateGray.ToArgb();
        public Mat4 modelMat = Transformations.Eye();

        public Model()
        {
        }

        public void ColorVert(int color)
        {
            foreach (Triangle triangle in triangles)
            {
                triangle.a.c = color;
                triangle.b.c = color;
                triangle.c.c = color;
            }
        }
        public void CalcNorm()
        {
            foreach (var t in triangles) t.CalculateNormals();
        }
        public List<WrappedTriangle> Tranform()
        {
            List<WrappedTriangle> t = new List<WrappedTriangle>();
            foreach (Triangle triangle in triangles)
            {
                var tr = triangle.Transform(modelMat);
                tr = tr.TransformNormals(new(modelMat));
                t.Add(new WrappedTriangle(tr, tr.a.v, tr.b.v, tr.c.v, texture, color));

            }

            return t;
        }

        public void Rotate(double x, double y, double z)
        {
            modelMat *= Transformations.RotationX(x) * Transformations.RotationY(y) * Transformations.RotationZ(z);
        }

        public void Translate(double x, double y, double z)
        {
            modelMat *= Transformations.Translation(x, y, z);
        }

        public static Model ModSquare()
        {
            Model m = new();

            m.triangles.Add(new Triangle(new Vec4(-1, -1, 1), new Vec4(1, -1, 1), new Vec4(1, 1, 1)));
            m.triangles.Add(new Triangle(new Vec4(-1, -1, 1), new Vec4(1, 1, 1), new Vec4(-1, 1, 1)));
            m.triangles.Add(new Triangle(new Vec4(1, -1, 1), new Vec4(1, -1, -1), new Vec4(1, 1, -1)));
            m.triangles.Add(new Triangle(new Vec4(1, -1, 1), new Vec4(1, 1, -1), new Vec4(1, 1, 1)));
            m.triangles.Add(new Triangle(new Vec4(1, -1, -1), new Vec4(-1, -1, -1), new Vec4(-1, 1, -1)));
            m.triangles.Add(new Triangle(new Vec4(1, -1, -1), new Vec4(-1, 1, -1), new Vec4(1, 1, -1)));
            m.triangles.Add(new Triangle(new Vec4(-1, -1, -1), new Vec4(-1, -1, 1), new Vec4(-1, 1, 1)));
            m.triangles.Add(new Triangle(new Vec4(-1, -1, -1), new Vec4(-1, 1, 1), new Vec4(-1, 1, -1)));
            m.triangles.Add(new Triangle(new Vec4(-1, 1, 1), new Vec4(1, 1, 1), new Vec4(1, 1, -1)));
            m.triangles.Add(new Triangle(new Vec4(-1, 1, 1), new Vec4(1, 1, -1), new Vec4(-1, 1, -1)));
            m.triangles.Add(new Triangle(new Vec4(1, -1, 1), new Vec4(-1, -1, -1), new Vec4(1, -1, -1)));
            m.triangles.Add(new Triangle(new Vec4(1, -1, 1), new Vec4(-1, -1, 1), new Vec4(-1, -1, -1)));
            return m;
        }
    }
}
