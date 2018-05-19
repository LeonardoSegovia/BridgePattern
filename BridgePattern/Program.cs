using System;
using Autofac;
using Autofac.Core;

namespace BridgePattern
{

    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterCircle : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle of radius {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void Draw();

        public abstract void Resize(float factor);

    }

    class Circle : Shape
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

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<VectorRenderer>().As<IRenderer>();

            containerBuilder.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));

            using (var buildedContainer = containerBuilder.Build())
            {
                var circleShape = buildedContainer.Resolve<Circle>(new PositionalParameter(0, 3.0f));


                circleShape.Draw();
                circleShape.Resize(3);
                circleShape.Draw();

                Console.ReadKey();
            }
        }
    }
}
