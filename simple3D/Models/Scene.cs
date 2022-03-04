using simple3D.Maths;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple3D.Models
{
    public class Scene
    {
        public List<Model> models = new();
        public List<Light> lights = new();
        public Camera? currentCam;

        public Scene()
        {
        }

        public List<WrappedTriangle>? GetViewSpace()
        {
            if (currentCam == null) return null;
            var view = currentCam.GetView();
            List<WrappedTriangle> result = new List<WrappedTriangle>();
            foreach (var model in models)
            {
                model.Tranform().ForEach(x => result.Add(x.Transform(view)));
            }

            return result;
        }
        public List<WrappedTriangle>? GetClippingSpace(double aspect)
        {
            if (currentCam == null) return null;
            Mat4 proj = Transformations.Projection(currentCam.fov, aspect, currentCam.far, currentCam.near);
            List<WrappedTriangle>? result = GetViewSpace();
            if (result == null) return null;
            foreach (var triangle in result)
            {
                triangle.TransformThis(proj);
            }
            return result;
        }

        public List<WrappedTriangle>? GetClipped(double aspect)
        {
            List<WrappedTriangle>? clipping = GetClippingSpace(aspect);
            if (clipping == null) return null;
            List<WrappedTriangle> result = Clip(clipping);
            foreach (var triangle in result)
            {
                triangle.a.DivideW();
                triangle.b.DivideW();
                triangle.c.DivideW();
            }
            return result;
        }

        public List<Light> GetLightsInWorldSpace()
        {
            return lights;
        }

        private double ClipPlaneDst(Vec4 vec, int planeNo)
        {
            switch (planeNo)
            {
                case 0:
                    return vec.w - vec.x;
                case 1:
                    return vec.w + vec.x;
                case 2:
                    return vec.w - vec.y;
                case 3:
                    return vec.w + vec.y;
                case 4:
                    return vec.w - vec.z;
                case 5:
                    return vec.w + vec.z;
                default:
                    return 0;
            }
        }

        public List<WrappedTriangle> Triangulate(List<Vert> vecs, int[,]? texture, int? color)
        {
            List<WrappedTriangle> result = new List<WrappedTriangle>();
            if (vecs.Count < 3) return result;
            for (int i = 1; i < vecs.Count - 1; i++)
            {
                result.Add(new WrappedTriangle(new Vert(vecs[0]), new Vert(vecs[i]), new Vert(vecs[i + 1]), texture, color));
            }
            return result;
        }


        public List<WrappedTriangle> ClipTriangle(WrappedTriangle tr)
        {
            List<Vert> vert = new List<Vert> { tr.a, tr.b, tr.c };
            List<Vert> vertNew = new List<Vert>();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < vert.Count; j++)
                {
                    int jnext = (j + 1) % vert.Count;
                    double curDst = ClipPlaneDst(vert[j].v, i);
                    double nextDst = ClipPlaneDst(vert[jnext].v, i);

                    if (curDst >= 0 && nextDst >= 0) vertNew.Add(vert[jnext]);
                    if (curDst > 0 && nextDst < 0)
                    {
                        // MessageBox.Show($"Clipped {i} " + vert[jnext].ToString());
                        double p = (curDst) / (curDst - nextDst);
                        vertNew.Add(Vert.Compose(vert[j], vert[jnext], p));
                    }
                    if (curDst < 0 && nextDst > 0)
                    {
                        //  MessageBox.Show($"Clipped {i} "+vert[j].ToString());
                        double p = (curDst) / (curDst - nextDst);
                        vertNew.Add(Vert.Compose(vert[j], vert[jnext], p));
                        vertNew.Add(vert[jnext]);
                    }
                }
                vert = vertNew;
                vertNew = new List<Vert>();
            }
            return Triangulate(vert, tr.texture, tr.color);
        }

        public List<WrappedTriangle> Clip(List<WrappedTriangle> triangles)
        {
            List<WrappedTriangle> vert = new List<WrappedTriangle>();
            foreach (var tri in triangles) vert.AddRange(ClipTriangle(tri));
            return vert;
        }
    }
}
