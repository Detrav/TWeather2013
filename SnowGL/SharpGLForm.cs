using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace SnowGL
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {

        public struct particle
{
	public float x,y,z;
    public float r, g, b;
    public float xd, yd, zd;
    public float cs;
};
        int len;
        particle[] p;
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm(int l)
        {
            len = l;
            InitializeComponent();
        }

        float windspeed = 0.005f;
        int winddir = 45;
       

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, PaintEventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            //gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            
            //  Draw a coloured pyramid.
            gl.Begin(OpenGL.GL_QUADS);
           
            for (int i = 0; i < p.Length; i++)
            {
                p[i].y += p[i].yd;
                p[i].x += p[i].xd;
                if (p[i].y > Screen.PrimaryScreen.Bounds.Height)
                {
                    p[i].y -= Screen.PrimaryScreen.Bounds.Height;
                    p[i].xd = ((float)rand.NextDouble() - 0.5f) * 5;
                    p[i].yd = ((float)rand.NextDouble()) * 5;
                }

                if (p[i].x > Screen.PrimaryScreen.Bounds.Width)
                {
                    p[i].x -= Screen.PrimaryScreen.Bounds.Width;
                }
                else if (p[i].x <0 )
                    {
                        p[i].x += Screen.PrimaryScreen.Bounds.Width;
                    }
                

                gl.Color(p[i].r, p[i].g, p[i].b);
                gl.Vertex(p[i].x, p[i].y);
                gl.Vertex(p[i].x+p[i].xd, p[i].y);
                gl.Vertex(p[i].x+p[i].xd, p[i].y+p[i].yd);
                gl.Vertex(p[i].x, p[i].y+p[i].yd);
            }
            gl.End();

            //  Nudge the rotation.
            //rotation += 3.0f;
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;

                baseParams.ExStyle |= (int)(
                  
                  Win32.WindowStylesEx.WS_EX_LAYERED |
                  Win32.WindowStylesEx.WS_EX_TRANSPARENT |
                  Win32.WindowStylesEx.WS_EX_NOACTIVATE |
                  Win32.WindowStylesEx.WS_EX_TOOLWINDOW);

                return baseParams;
            }
        }

        Random rand = new Random();

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {

            p = new particle[len];
            for (int i = 0; i < p.Length; i++)
            {
                p[i].xd = ((float)rand.NextDouble() - 0.5f) * 5;
                p[i].yd = ((float)rand.NextDouble()) * 5;
                p[i].x = (float)rand.NextDouble() *Screen.PrimaryScreen.Bounds.Width;
                p[i].y = (float)rand.NextDouble() * Screen.PrimaryScreen.Bounds.Height;
                p[i].b = (float)rand.NextDouble() * 0.2f +0.8f ;
                p[i].g = p[i].b;
                p[i].r = p[i].b;
            }

            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            gl.ShadeModel(0x1D01);
            gl.ClearColor(0, 0, 0, 0);
            gl.ClearDepth(1.0);
            gl.Enable(0x0B71);
            gl.DepthFunc(0x0203);
            gl.Hint(0x0C50, 0x1102);

        }

        void ni1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            gl.Ortho(0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, 0, 0, 1);
            //gl.Ortho2D(0, 0, ClientSize.Width, ClientSize.Height);
            //  Create a perspective transformation.
            //gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
           // gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;

        private void SharpGLForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }
    }
}
