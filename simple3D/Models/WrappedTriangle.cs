using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;

namespace simple3D.Models
{
    public class WrappedTriangle : Triangle
    {

        public int[,]? texture;
        public int? color;

        public WrappedTriangle(Vec4 a, Vec4 b, Vec4 c, int[,]? texture, int? color) : base(a, b, c)
        {
            this.texture = texture;
            this.color = color;
        }
        public WrappedTriangle(Vert a, Vert b, Vert c, int[,]? texture, int? color) : base(a, b, c)
        {
            this.texture = texture;
            this.color = color;
        }
        public WrappedTriangle(Triangle triangle, int[,]? texture, int? color) : base(triangle.a, triangle.b, triangle.c)
        {
            this.texture = texture;
            this.color = color;
        }
        public WrappedTriangle(Triangle triangle, Vec4 wa, Vec4 wb, Vec4 wc, int[,]? texture, int? color) : this(triangle, texture, color)
        {
            this.a.wv = wa;
            this.b.wv = wb;
            this.c.wv = wc;
        }

        public new WrappedTriangle Transform(Mat4 mat)
        {
            return new WrappedTriangle(base.Transform(mat), a.wv, b.wv, c.wv, texture, color);
        }
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append('[');
            s.Append(a.ToString());
            s.Append(b.ToString());
            s.Append(c.ToString());
            if (texture != null) s.Append(" texture");
            else if (color != null) s.Append(Color.FromArgb((int)color).ToString());
            s.Append(']');
            return base.ToString();
        }
    }
}
