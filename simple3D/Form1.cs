using simple3D.Maths;
using simple3D.Models;

namespace simple3D
{
    public partial class Form1 : Form
    {
        Scene scene = new();
        Renderer renderer = new();
        FastBitmap bitmap = new(1200, 900);
        int[,] bmp = new int[900, 1200];

        double x = 0;
        Camera cam1;
        Camera cam2;
        Camera cam3;

        Light reflector;
        public Form1()
        {
            InitializeComponent();

            Model m = Model.ModSquare();
            Model m2 = Model.ModSquare();
            Model m3 = Model.ModSquare();
            Model m4 = Model.ModSquare();
            // m.Translate(-2, 3, 0);
            m.CalcNorm();
           // m.Rotate(0.1, 0, 0);
            m.Translate(0, 1, 0);
            m.ColorVert(Color.White.ToArgb());
            m2.CalcNorm();
            m2.ColorVert(Color.White.ToArgb());
            m2.Translate(0, -3, 0);
            m3.CalcNorm();
            m3.Translate(0, 0, 7);
            m3.ColorVert(Color.White.ToArgb());
            m4.CalcNorm();
            m4.Translate(-2.3, 2,0);
            m4.Rotate(1, 2, 3);
            m4.ColorVert(Color.White.ToArgb());

            scene.models.Add(m);
            scene.models.Add(m2);
            scene.models.Add(m3);
            scene.models.Add(m4);

            cam1 = new();
            cam1.fov = 1.6;
            cam1.pos = new Vec4(8, 1, 4);
            cam1.observedPos = new Vec4(0, 0, 0);
            scene.currentCam = cam1;

            cam2 = new();
            cam2.fov = 2;
            cam2.pos = scene.models[2].Tranform()[1].c.v;
            cam2.observedPos = cam2.pos.Add(new Vec4(0, 0, -4));

            cam3 = new();
            cam3.fov = 2;
            cam3.pos = new Vec4(2, 2, -6);
            cam3.observedPos = scene.models[1].Tranform()[1].a.v;




            Light l = new Light(new Vec4(4, 1, 4));
            l.Color = Color.White;
            scene.lights.Add(l);
            Light l2 = new Light(new Vec4(4, 4, -4));
            l2.Color = Color.Green;
            scene.lights.Add(l2);

            reflector = new Light(new Vec4(0, 0, 8), new Vec3(0, 0, -2));
            reflector.Color = Color.Red;
            scene.lights.Add(reflector);


            var b = renderer.Render(scene, bmp);
            bitmap.Cpy(bmp);
            pictureBox1.Image = bitmap.Bitmap;
            pictureBox1.Refresh();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            
            scene.models[1].Translate(Math.Cos(x) * 0.28, Math.Sin(x) * 0.28, 0);
            scene.models[1].Rotate(0.01, 0, 0);
            scene.models[2].Translate(0, 0, Math.Cos(x) * 0.3);
            x += 0.07;
            if (x > 2 * Math.PI) x -= 2 * Math.PI;

            cam2.pos = scene.models[2].Tranform()[1].c.v;
            cam2.observedPos = cam2.pos.Add(new Vec4(0, 0, -4));

            cam3.observedPos = scene.models[1].Tranform()[0].a.v;

            reflector.pos = cam2.pos;


            label1.Text = scene.currentCam.pos.ToString() + "\n";
            label1.Text += scene.currentCam.observedPos.ToString() + "\n";
            refOri.Text = reflector.orientation.ToString();

            var b = renderer.Render(scene, bmp);
            label2.Text = renderer.lastRenderTime;
            bitmap.Cpy(b);
            pictureBox1.Image = bitmap.Bitmap;
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cam1.pos.y += 0.5;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cam1.pos.y -= 0.5;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cam1.pos.x += 0.5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cam1.pos.x -= 0.5;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cam1.pos.z += 0.5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cam1.pos.z -= 0.5;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cam1.observedPos.y += 0.5;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cam1.observedPos.y -= 0.5;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cam1.observedPos.x += 0.5;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            cam1.observedPos.x -= 0.5;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cam1.observedPos.z += 0.5;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cam1.observedPos.z -= 0.5;
        }

        private void buttonFlat_Click(object sender, EventArgs e)
        {
            renderer.shaderFactory = new Shaders.FlatShaderFactory();
        }

        private void buttonGour_Click(object sender, EventArgs e)
        {
            renderer.shaderFactory = new Shaders.GouraudShaderFactory();
        }

        private void buttonPhong_Click(object sender, EventArgs e)
        {
            renderer.shaderFactory = new Shaders.PhongShaderFactory();
        }

        private void buttoncam1_Click(object sender, EventArgs e)
        {
            scene.currentCam = cam1;
        }

        private void buttoncam2_Click(object sender, EventArgs e)
        {
            scene.currentCam = cam2;
        }

        private void buttoncam3_Click(object sender, EventArgs e)
        {
            scene.currentCam = cam3;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            reflector.orientation.y += 0.5;
        }

        private void refx_Click(object sender, EventArgs e)
        {
            reflector.orientation.x += 0.5;
        }

        private void refz_Click(object sender, EventArgs e)
        {
            reflector.orientation.z += 0.5;
        }

        private void refmy_Click(object sender, EventArgs e)
        {
            reflector.orientation.y -= 0.5;
        }

        private void refmz_Click(object sender, EventArgs e)
        {
            reflector.orientation.z -= 0.5;
        }

        private void refmx_Click(object sender, EventArgs e)
        {
            reflector.orientation.x -= 0.5;
        }
    }
}