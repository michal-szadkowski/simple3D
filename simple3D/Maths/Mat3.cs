using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple3D.Maths
{
	public class Mat3
	{
		public double[,] v;
		public Mat3()
		{
			v = new double[3, 3];
		}
		public Mat3(Vec3 v1, Vec3 v2, Vec3 v3) : this()
		{
			v[0, 0] = v1.x;
			v[1, 0] = v1.y;
			v[2, 0] = v1.z;
			v[0, 1] = v2.x;
			v[1, 1] = v2.y;
			v[2, 1] = v2.z;
			v[0, 2] = v3.x;
			v[1, 2] = v3.y;
			v[2, 2] = v3.z;
		}

		public Mat3(Mat4 m) : this()
		{
			for(int i = 0; i < 3; i++)
			{
				for(int j=0;j<3; j++)
				{
					v[i, j] = m[i, j];
				}
			}
		}

		public double this[int i, int j]
		{
			get { return v[i, j]; }
			set { v[i, j] = value; }
		}

		public static Mat3 Multiply(Mat3 m1, Mat3 m2)
		{
			Mat3 m3 = new Mat3();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					m3[i, j] = 0;
					for (int k = 0; k < 3; k++)
					{
						m3[i, j] += m1[i, k] * m2[k, j];
					}
				}
			}
			return m3;
		}

		public static Mat3 MultiplyByDouble(Mat3 m1, double a)
		{
			Mat3 m3 = new Mat3();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					m3[i, j] = m1[i, j] * a;
				}
			}
			return m3;
		}

		public static Vec3 MultiplyByVec3(Mat3 m1, Vec3 v)
		{
			Vec3 vec = new Vec3();
			vec.x = m1[0, 0] * v.x + m1[0, 1] * v.y + m1[0, 2] * v.z;
			vec.y = m1[1, 0] * v.x + m1[1, 1] * v.y + m1[1, 2] * v.z;
			vec.z = m1[2, 0] * v.x + m1[2, 1] * v.y + m1[2, 2] * v.z;
			return vec;
		}

		public static Mat3 operator *(Mat3 m1, Mat3 m2)
		{
			return Multiply(m1, m2);
		}

		public static Mat3 operator *(Mat3 m1, double a)
		{
			return MultiplyByDouble(m1, a);
		}
		public static Vec3 operator *(Mat3 m1, Vec3 v)
		{
			return MultiplyByVec3(m1, v);
		}


		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					s.Append($"{v[i, j]} ");
				}
				s.AppendLine(";");
			}
			return s.ToString();
		}

	}
}
