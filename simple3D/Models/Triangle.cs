using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;

namespace simple3D.Models
{
    public class Triangle
    {
        public Vert a, b, c;


        public Triangle()
        {
            a = new Vert();
            b = new Vert();
            c = new Vert();
        }
        public Triangle(Triangle tr)
        {
            a = new(tr.a);
            b = new(tr.b);
            c = new(tr.c);
        }
        public Triangle(Vec4 a, Vec4 b, Vec4 c) : this()
        {
            this.a = new Vert(a);
            this.b = new Vert(b);
            this.c = new Vert(c);
        }
        public Triangle(Vert a, Vert b, Vert c) : this()
        {
            this.a = new Vert(a);
            this.b = new Vert(b);
            this.c = new Vert(c);
        }
        public void CalculateNormals()
        {
            Vec3 norm = c.v.Subtract(b.v).Cross(a.v.Subtract(b.v));
            a.n = norm;
            b.n = norm;
            c.n = norm;
        }

        public Triangle Transform(Mat4 transform)
        {
            Triangle t = new Triangle(this);
            t.a.Transform(transform);
            t.b.Transform(transform);
            t.c.Transform(transform);

            return t;
        }
        public Triangle TransformNormals(Mat3 transform)
        {
            Triangle tr = new Triangle(this);
            tr.a.TransformNormals(transform);
            tr.b.TransformNormals(transform);
            tr.c.TransformNormals(transform);

            return tr;
        }
        public Triangle TransformThis(Mat4 transform)
        {
            a.Transform(transform);
            b.Transform(transform);
            c.Transform(transform);
            return this;
        }
        public double Orientation()
        {
            var d = (a.v.x - c.v.x) * (b.v.y - c.v.y) - (a.v.y - c.v.y) * (b.v.x - c.v.x);
            return d;
        }
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append('[');
            s.Append(a.ToString());
            s.Append(b.ToString());
            s.Append(c.ToString());
            s.Append(']');
            return s.ToString();
        }

    }
}
