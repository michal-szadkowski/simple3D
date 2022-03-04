using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple3D
{
    public interface IShader
    {
        public int GetColor(int x, int y,(double,double,double) barCoords);
    }
}
