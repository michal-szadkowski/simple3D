using System.Text;

namespace simple3D.Maths
{
	public class Vec4 : Vec3
	{
		public double w = 1;

		public Vec4() { }
		public Vec4(double x, double y, double z, double w = 1) : base(x, y, z)
		{
			this.w = w;
		}
		public Vec4(Vec3 b) : base(b.x, b.y, b.z)
		{
			this.w = 0;
		}
		public Vec4(Vec4 b) : this(b.x, b.y, b.z, b.w) { }

		public double Dot(Vec4 b)
		{
			return this.x * b.x + this.y * b.y + this.z * b.z + this.w * b.w;
		}

		public new Vec4 Multiply(double p)
		{
			return new Vec4(p*x, p*y, p*z, p*w);
		}
		public Vec4 Add(Vec4 a)
		{
			return new Vec4(a.x+x,a.y+y,a.z+z,a.w+w);
		}
		public override double LengthSquared()
		{
			return x * x + y * y + z * z + w * w;
		}

		public override Vec4 Normalized()
		{
			double l = Math.Sqrt(this.LengthSquared());
			return new Vec4(x / l, y / l, z / l, w / l);
		}

		public override Vec4 Normalize()
		{
			double l = Math.Sqrt(this.LengthSquared());
			x /= l;
			y /= l;
			z /= l;
			w /= l;
			return this;
		}
		public Vec4 DivideW()
		{
			x /= this.w;
			y /= this.w;
			z /= this.w;
			w /= this.w;
			return this;
		}
		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			s.Append('{');
			s.Append($"{x}, {y}, {z}, {w}");
			s.Append('}');
			return s.ToString();
		}

	}

}
