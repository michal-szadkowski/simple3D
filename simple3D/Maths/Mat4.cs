using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple3D.Maths
{
	public class Mat4
	{
		public double[,] v;
		public Mat4()
		{
			v = new double[4, 4];
		}
		public Mat4(Vec4 v1, Vec4 v2, Vec4 v3, Vec4 v4) : this()
		{
			v[0, 0] = v1.x;
			v[1, 0] = v1.y;
			v[2, 0] = v1.z;
			v[3, 0] = v1.w;
			v[0, 1] = v2.x;
			v[1, 1] = v2.y;
			v[2, 1] = v2.z;
			v[3, 1] = v2.w;
			v[0, 2] = v3.x;
			v[1, 2] = v3.y;
			v[2, 2] = v3.z;
			v[3, 2] = v3.w;
			v[0, 3] = v4.x;
			v[1, 3] = v4.y;
			v[2, 3] = v4.z;
			v[3, 3] = v4.w;
		}

		public Mat4 Transposed()
		{
			Mat4 transposed = new Mat4();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
					transposed[i, j] = this[j, j];
			}
			return transposed;
		}


		public double this[int i, int j]
		{
			get { return v[i, j]; }
			set { v[i, j] = value; }
		}

		public static Mat4 Multiply(Mat4 m1, Mat4 m2)
		{
			Mat4 m3 = new Mat4();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					m3[i, j] = 0;
					for (int k = 0; k < 4; k++)
					{
						m3[i, j] += m1[i, k] * m2[k, j];
					}
				}
			}
			return m3;
		}

		public static Mat4 MultiplyByDouble(Mat4 m1, double a)
		{
			Mat4 m3 = new Mat4();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					m3[i, j] = m1[i, j] * a;
				}
			}
			return m3;
		}

		public static Vec4 MultiplyByVec4(Mat4 m1, Vec4 v)
		{
			Vec4 vec = new Vec4();
			vec.x = m1[0, 0] * v.x + m1[0, 1] * v.y + m1[0, 2] * v.z + m1[0, 3] * v.w;
			vec.y = m1[1, 0] * v.x + m1[1, 1] * v.y + m1[1, 2] * v.z + m1[1, 3] * v.w;
			vec.z = m1[2, 0] * v.x + m1[2, 1] * v.y + m1[2, 2] * v.z + m1[2, 3] * v.w;
			vec.w = m1[3, 0] * v.x + m1[3, 1] * v.y + m1[3, 2] * v.z + m1[3, 3] * v.w;
			return vec;
		}

		public static Mat4 operator *(Mat4 m1, Mat4 m2)
		{
			return Multiply(m1, m2);
		}

		public static Mat4 operator *(Mat4 m1, double a)
		{
			return MultiplyByDouble(m1, a);
		}
		public static Vec4 operator *(Mat4 m1, Vec4 v)
		{
			return MultiplyByVec4(m1, v);
		}


		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					s.Append($"{Math.Round(v[i, j],4)} ");
				}
				s.AppendLine("   ;");
			}
			return s.ToString();
		}

	}
}
