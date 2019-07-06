using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public abstract class GameObject
    {

        private string name;
        private Tag tag;

        private BoundBox boundBox;
        private Transformacao4D transform;
        private List<Ponto4D> vertices;
        private PrimitiveType primitive;

        public GameObject(Tag tag = Tag.UNTTAGED) {
            transform = new Transformacao4D();
            boundBox = new BoundBox();
            primitive = PrimitiveType.Lines;
            this.vertices = new List<Ponto4D>();
        }

        public GameObject(List<Ponto4D> vertices, Tag tag = Tag.UNTTAGED) {
            transform = new Transformacao4D();
            boundBox = new BoundBox();
            primitive = PrimitiveType.Lines;
            SetVertices(vertices);
        }

        public List<Ponto4D> GetVertices() => this.vertices;

        public void SetVertices(List<Ponto4D> vertices) {
            this.vertices = new List<Ponto4D>();
            this.vertices.AddRange(vertices);
            boundBox.AtualizarBBox(this.vertices);
        }

        public void SetPrimitive(PrimitiveType primitive) => this.primitive = primitive;

        public void AddVertice(Ponto4D newVertice)
        {
            List<Ponto4D> vertices = GetVertices();
            vertices.Add(newVertice);
            SetVertices(vertices);
        }

        public void UpdateVertice(Ponto4D vertice, int position)
        {
            List<Ponto4D> vertices = GetVertices();
            vertices[position] = vertice;
            SetVertices(vertices);
        }

        public void SetTag(Tag tag) {
            this.tag = tag;
        }

        public Tag GetTag() => tag;

        public BoundBox GetBoundBox() => this.boundBox;

        public Transformacao4D GetTransform() => transform;

        public virtual void Translate(double tx, double ty, double tz)
        {
            Transformacao4D newTransform = new Transformacao4D();
            newTransform.AtribuirTranslacao(tx, ty, tz);

            transform = newTransform.TransformMatrix(transform);
            boundBox.AtualizarBBox(this.vertices, transform);
        }

        public virtual void Scale(double factor)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Rotate(double factor)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Draw()
        {
            GL.PushMatrix();
            GL.MultMatrix(transform.GetData());
            GL.Color3(Color.Black);
            GL.LineWidth(3);
            GL.Begin(primitive);
                foreach(Ponto4D vertex in this.vertices)
                    GL.Vertex2(vertex.X, vertex.Y);
            GL.End();

            GL.PopMatrix();

            boundBox.DesenharBBox();
        }

        public bool IsColliding(GameObject other) {
            return boundBox.IsColliding(other);
        }
    }
}