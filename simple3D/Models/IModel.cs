using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple3D.Models
{
    public interface IModel
    {
        public List<WrappedTriangle> Tranform();
        public void Rotate(double x, double y, double z);
        public void Translate(double x, double y, double z);
    }
}
