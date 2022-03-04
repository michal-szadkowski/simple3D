using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;

namespace simple3D.Models
{
	public class Camera
	{

		public Vec4 pos = new();
		public Vec4 observedPos = new();
		public Vec3 upWorld { get => new Vec3(0, 1, 0); }
		public double fov = 1;
		public double far = 200, near = 1;
		public Vec3 observeddir { get => pos - observedPos; }


		public Mat4 GetView()
		{
			Vec3 zaxis = observedPos - pos;
			zaxis.Normalize();
			Vec3 xaxis = upWorld.Cross(zaxis);
			xaxis.Normalize();
			Vec3 yaxis = zaxis.Cross(xaxis);
			Mat4 mat = new Mat4();
			yaxis.Normalize();


			mat[0, 0] = xaxis.x;
			mat[0, 1] = xaxis.y;
			mat[0, 2] = xaxis.z;
			mat[1, 0] = yaxis.x;
			mat[1, 1] = yaxis.y;
			mat[1, 2] = yaxis.z;
			mat[2, 0] = -zaxis.x;
			mat[2, 1] = -zaxis.y;
			mat[2, 2] = -zaxis.z;
			mat[3, 3] = 1;

			Mat4 mat2 = Transformations.Eye();
			mat2[0, 3] = -pos.x;
			mat2[1, 3] = -pos.y;
			mat2[2, 3] = -pos.z;

			var m3 = Mat4.Multiply(mat, mat2);
			return m3;
		}

	}
}
