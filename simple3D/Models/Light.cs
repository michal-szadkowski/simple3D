using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;

namespace simple3D.Models
{
	public class Light
	{
		public Vec4 pos = new();
		public Vec3 orientation = new();
		public Color Color = Color.White;
		public bool isRef = false;

		public Light(){}
		public Light(Vec4 pos)
		{
			this.pos = new(pos);
		}
		public Light(Vec4 pos,Vec3 orientation)
		{
			this.pos = new(pos);
			this.orientation = new(orientation);
			isRef = true;
		}
	}
}
