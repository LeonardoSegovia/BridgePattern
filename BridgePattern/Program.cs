using System;

namespace BridgePattern
{

    interface IRenderer
    {
        void RenderCircle(float radius);
    }

    class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    class RasterCircle : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle of radius {radius}");
        }
    }

    abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void Draw();

        public abstract void Resize(float factor);

    }

    class Circle:Shape
    {
        private float _radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            Renderer.RenderCircle(_radius);
    }

        public override void Resize(float factor)
        {
            _radius *= factor;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var render = new VectorRenderer();

            var objShape = new Circle(render,4);

            objShape.Draw();
            objShape.Resize(3);
            objShape.Draw();

            Console.ReadKey();

        }
    }
}
