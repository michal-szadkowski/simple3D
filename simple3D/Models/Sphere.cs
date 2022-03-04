using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simple3D.Maths;

namespace simple3D.Models
{
    public class Sphere : IModel
    {
        public double radius;
        Vec4 pos;
        Vec4 top;
        public int precision;
        public List<WrappedTriangle> Tranform()
        {
            throw new NotImplementedException();
        }

        public void Rotate(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void Translate(double x, double y, double z)
        {
            throw new NotImplementedException();
        }
    }
}
