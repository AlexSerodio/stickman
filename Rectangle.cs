using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace stick_man
{
    public class Rectangle : GameObject
    {

        private int texture;
        protected List<Ponto4D> extrudedVertices = new List<Ponto4D>();

        // UR, UL, DL, DR
        public Rectangle(List<Ponto4D> vertices, double extrudeDistance = 100) : base() {
            base.SetPrimitive(PrimitiveType.Polygon);
            base.SetVertices(vertices);
            base.SetHasGravity(false);

            texture = Texture.GetTexture("ground.jpg");
            Extrude(extrudeDistance);
        }

        private void Extrude(double extrudeDistance)
        {
            foreach (Ponto4D vertice in base.GetVertices())
                extrudedVertices.Add(new Ponto4D(vertice.X, vertice.Y, vertice.Z - extrudeDistance));
        }

        public override void Draw()
        {
            GL.PushMatrix();
            GL.MultMatrix(GetTransform().GetData());

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.Begin(PrimitiveType.Quads);
            // front
            GL.Color3(Color.White);
            GL.Normal3(0, 0, 1);
            GL.TexCoord2(1.0f, 0.0f);GL.Vertex3(GetVertices()[0].X, GetVertices()[0].Y, GetVertices()[0].Z);    // UR
            GL.TexCoord2(0.0f, 0.0f);GL.Vertex3(GetVertices()[1].X, GetVertices()[1].Y, GetVertices()[1].Z);    // UL
            GL.TexCoord2(0.0f, 1.0f);GL.Vertex3(GetVertices()[2].X, GetVertices()[2].Y, GetVertices()[2].Z);    // DL
            GL.TexCoord2(1.0f, 1.0f);GL.Vertex3(GetVertices()[3].X, GetVertices()[3].Y, GetVertices()[3].Z);    // DR
            // back
            GL.Normal3(0, 0, -1);
            GL.TexCoord2(1.0f, 0.0f);GL.Vertex3(extrudedVertices[0].X, extrudedVertices[0].Y, extrudedVertices[0].Z);
            GL.TexCoord2(0.0f, 0.0f);GL.Vertex3(extrudedVertices[1].X, extrudedVertices[1].Y, extrudedVertices[1].Z);
            GL.TexCoord2(0.0f, 1.0f);GL.Vertex3(extrudedVertices[2].X, extrudedVertices[2].Y, extrudedVertices[2].Z);
            GL.TexCoord2(1.0f, 1.0f);GL.Vertex3(extrudedVertices[3].X, extrudedVertices[3].Y, extrudedVertices[3].Z);
            // up
            GL.Normal3(0, 1, 0);
            GL.TexCoord2(1.0f, 0.0f);GL.Vertex3(GetVertices()[0].X, GetVertices()[0].Y, GetVertices()[0].Z);
            GL.TexCoord2(0.0f, 0.0f);GL.Vertex3(extrudedVertices[0].X, extrudedVertices[0].Y, extrudedVertices[0].Z);
            GL.TexCoord2(0.0f, 1.0f);GL.Vertex3(extrudedVertices[1].X, extrudedVertices[1].Y, extrudedVertices[1].Z);
            GL.TexCoord2(1.0f, 1.0f);GL.Vertex3(GetVertices()[1].X, GetVertices()[1].Y, GetVertices()[1].Z);
            // down
            GL.Normal3(0, -1, 0);
            GL.TexCoord2(1.0f, 0.0f);GL.Vertex3(GetVertices()[2].X, GetVertices()[2].Y, GetVertices()[2].Z);
            GL.TexCoord2(0.0f, 0.0f);GL.Vertex3(extrudedVertices[2].X, extrudedVertices[2].Y, extrudedVertices[2].Z);
            GL.TexCoord2(0.0f, 1.0f);GL.Vertex3(extrudedVertices[3].X, extrudedVertices[3].Y, extrudedVertices[3].Z);
            GL.TexCoord2(1.0f, 1.0f);GL.Vertex3(GetVertices()[3].X, GetVertices()[3].Y, GetVertices()[3].Z);
            // right
            GL.Normal3(1, 0, 0);
            GL.TexCoord2(1.0f, 0.0f);GL.Vertex3(extrudedVertices[0].X, extrudedVertices[0].Y, extrudedVertices[0].Z);
            GL.TexCoord2(0.0f, 0.0f);GL.Vertex3(GetVertices()[0].X, GetVertices()[0].Y, GetVertices()[0].Z);
            GL.TexCoord2(0.0f, 1.0f);GL.Vertex3(GetVertices()[3].X, GetVertices()[3].Y, GetVertices()[3].Z);
            GL.TexCoord2(1.0f, 1.0f);GL.Vertex3(extrudedVertices[3].X, extrudedVertices[3].Y, extrudedVertices[3].Z);
            // left
            GL.Normal3(-1, 0, 0);
            GL.TexCoord2(1.0f, 0.0f);GL.Vertex3(GetVertices()[1].X, GetVertices()[1].Y, GetVertices()[1].Z);
            GL.TexCoord2(0.0f, 0.0f);GL.Vertex3(extrudedVertices[1].X, extrudedVertices[1].Y, extrudedVertices[1].Z);
            GL.TexCoord2(0.0f, 1.0f);GL.Vertex3(extrudedVertices[2].X, extrudedVertices[2].Y, extrudedVertices[2].Z);
            GL.TexCoord2(1.0f, 1.0f);GL.Vertex3(GetVertices()[2].X, GetVertices()[2].Y, GetVertices()[2].Z);
            GL.End();

            GL.Disable(EnableCap.Texture2D);

            GetBoundBox().DesenharBBox();

            // GL.Begin(PrimitiveType.Quads);
            // // front
            // GL.Color3(base.GetColor());
            // GL.Normal3(0, 0, 1);
            // foreach (Ponto4D vertex in GetVertices())
            //     GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
            // // back
            // GL.Normal3(0, 0, -1);
            // foreach (Ponto4D vertex in extrudedVertices)
            //     GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
            // // up
            // GL.Normal3(0, 1, 0);
            // GL.Vertex3(GetVertices()[0].X, GetVertices()[0].Y, GetVertices()[0].Z);
            // GL.Vertex3(extrudedVertices[0].X, extrudedVertices[0].Y, extrudedVertices[0].Z);
            // GL.Vertex3(extrudedVertices[1].X, extrudedVertices[1].Y, extrudedVertices[1].Z);
            // GL.Vertex3(GetVertices()[1].X, GetVertices()[1].Y, GetVertices()[1].Z);
            // // down
            // GL.Normal3(0, -1, 0);
            // GL.Vertex3(GetVertices()[2].X, GetVertices()[2].Y, GetVertices()[2].Z);
            // GL.Vertex3(extrudedVertices[2].X, extrudedVertices[2].Y, extrudedVertices[2].Z);
            // GL.Vertex3(extrudedVertices[3].X, extrudedVertices[3].Y, extrudedVertices[3].Z);
            // GL.Vertex3(GetVertices()[3].X, GetVertices()[3].Y, GetVertices()[3].Z);
            // // right
            // GL.Normal3(1, 0, 0);
            // GL.Vertex3(extrudedVertices[0].X, extrudedVertices[0].Y, extrudedVertices[0].Z);
            // GL.Vertex3(GetVertices()[0].X, GetVertices()[0].Y, GetVertices()[0].Z);
            // GL.Vertex3(GetVertices()[3].X, GetVertices()[3].Y, GetVertices()[3].Z);
            // GL.Vertex3(extrudedVertices[3].X, extrudedVertices[3].Y, extrudedVertices[3].Z);
            // // left
            // GL.Normal3(-1, 0, 0);
            // GL.Vertex3(GetVertices()[1].X, GetVertices()[1].Y, GetVertices()[1].Z);
            // GL.Vertex3(extrudedVertices[1].X, extrudedVertices[1].Y, extrudedVertices[1].Z);
            // GL.Vertex3(extrudedVertices[2].X, extrudedVertices[2].Y, extrudedVertices[2].Z);
            // GL.Vertex3(GetVertices()[2].X, GetVertices()[2].Y, GetVertices()[2].Z);
            // GL.End();

            GL.PopMatrix();

            GetBoundBox().DesenharBBox();
        }

    }
}