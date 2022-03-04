using System;
using System.Text;

namespace simple3D.Maths
{
	public class Vec3
	{

		public double x = 0, y = 0, z = 0;


		public Vec3() { }
		public Vec3(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public Vec3(Vec3 b) : this(b.x, b.y, b.z) { }

		public double Dot(Vec3 b)
		{
			return this.x * b.x + this.y * b.y + this.z * b.z;
		}

		public Vec3 Cross(Vec3 b)
		{
			return new Vec3(y * b.z - b.y * z, b.x * z - x * b.z, x * b.y - y * b.x);
		}
		public Vec3 Subtract(Vec3 b)
		{
			Vec3 res = new(x, y, z);
			res.x -= b.x;
			res.y -= b.y;
			res.z -= b.z;
			return res;
		}
		public Vec3 Add(Vec3 b)
		{
			Vec3 res = new(x, y, z);
			res.x += b.x;
			res.y += b.y;
			res.z += b.z;
			return res;
		}
		public Vec3 Multiply(double p)
		{
			Vec3 res = new(p * x, p * y, p * z);
			return res;
		}

		public virtual double LengthSquared()
		{
			return x * x + y * y + z * z;
		}

		public virtual Vec3 Normalized()
		{
			double l = Math.Sqrt(this.LengthSquared());
			return new Vec3(x / l, y / l, z / l);
		}

		public virtual Vec3 Normalize()
		{
			double l = Math.Sqrt(this.LengthSquared());
			x /= l;
			y /= l;
			z /= l;
			return this;
		}

		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			s.Append($"{{ {x}, {y}, {z} }}");
			return s.ToString();
		}

		public static Vec3 operator -(Vec3 a, Vec3 b)
		{
			return a.Subtract(b);
		}
		public static Vec3 operator +(Vec3 a, Vec3 b)
		{
			return a.Add(b);
		}

	}
}
