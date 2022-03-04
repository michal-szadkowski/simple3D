using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;

namespace simple3D.Models
{
	public class Vert
	{
		public Vec4 v;
		public Vec4 wv;
		public Vec3 n;
		public int? tx, ty;
		public int? c;


		public Vert()
		{
			v = new Vec4();
			wv = new Vec4();
			n = new Vec3();
		}
		public Vert(Vert vert)
		{
			this.v = new(vert.v);
			this.wv = new(vert.wv);
			this.n = new(vert.n);
			this.tx = vert.tx;
			this.ty = vert.ty;
			this.c = vert.c;
		}
		public Vert(Vec4 v) : this()
		{
			this.v = new(v);
		}
		public Vert(Vec3 v,Vec3 n) : this()
		{
			this.v = new(v);
			this.n = new(n);
		}
		public void DivideW()
		{
			v.DivideW();
		}

		public Vert Transform(Mat4 transform)
		{
			v = transform * v;
			//n = new Mat3(transform) * n;
			return this;
		}
		public Vert TransformNormals(Mat3 t)
        {
			n = t * n;
			return this;
		}
		public override string ToString()
		{
			return v.ToString();
		}
		public static Vert Compose(Vert v1, Vert v2, double p)
		{
			Vert ver = new(v1);
			ver.v = ver.v.Multiply(1 - p);
			ver.v = ver.v.Add(v2.v.Multiply(p));

			ver.wv = ver.wv.Multiply(1 - p);
			ver.wv = ver.wv.Add(v2.wv.Multiply(p));

			ver.n = ver.n.Multiply(1 - p);
			ver.n = ver.n.Add(v2.n.Multiply(p));

			if (ver.tx != null && v2.tx != null)
			{
				ver.tx = (int)(ver.tx * (1 - p));
				ver.tx += (int)(v2.tx * p);
			}
			if (ver.ty != null && v2.ty != null)
			{
				ver.ty = (int)(ver.ty * (1 - p));
				ver.ty += (int)(v2.ty * p);
			}
			if (ver.c != null && v2.c != null)
			{
				Color color1 = Color.FromArgb((int)ver.c);
				Color color2 = Color.FromArgb((int)v2.c);
				ver.c = Color.FromArgb((int)(color1.R * (1 - p) + color2.R * p), (int)(color1.G * (1 - p) + color2.G * p), (int)(color1.B * (1 - p) + color2.B * p)).ToArgb();
			}
			return ver;
		}
	}
}
