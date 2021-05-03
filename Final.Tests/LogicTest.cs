using NUnit.Framework;
using Final.Logic;
using System.Collections.Generic;

namespace Final.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddTransformComponentTest()
        {
            ComponentManager componentManager = new ComponentManager();

            componentManager.AddComponent<TransformComponent>(0);

            Assert.AreEqual(componentManager.TransformComponents.Count, 1);
        }

        [Test]
        public void AddRenderComponentTest()
        {
            ComponentManager componentManager = new ComponentManager();

            componentManager.AddComponent<TransformComponent>(0);
            componentManager.AddComponent<RenderComponent>(0);

            Assert.AreEqual(componentManager.RenderComponents.Count, 1);
        }

        [Test]
        public void GetTransformCompnentTest()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity();

            componentManager.AddComponent<TransformComponent>(entity.ID);

            TransformComponent transformComponent = componentManager.GetComponent<TransformComponent>(entity.ID);

            Assert.AreNotEqual(transformComponent, null);
        }

        [Test]
        public void GetRenderCompnentTest()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity();

            componentManager.AddComponent<TransformComponent>(entity.ID);
            componentManager.AddComponent<RenderComponent>(entity.ID);

            RenderComponent renderComponent = componentManager.GetComponent<RenderComponent>(entity.ID);

            Assert.AreNotEqual(renderComponent, null);
        }

        [Test]
        public void GetInvalidTransformComponentTest()
        {
            ComponentManager componentManager = new ComponentManager();
            try
            {
                componentManager.GetComponent<TransformComponent>(0);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetInvalidRenderComponentTest()
        {
            ComponentManager componentManager = new ComponentManager();

            try
            {
                componentManager.GetComponent<RenderComponent>(0);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetWidgetsTest()
        {
            ComponentManager componentManager = new ComponentManager();

            componentManager.Widgets = new Dictionary<uint, List<IImguiWidget>>()
            {
                {0, new List<IImguiWidget>()}
            };

            
            List<IImguiWidget> widgets = componentManager.GetWidgets(0);

            Assert.AreNotEqual(widgets, null);
        }

        [Test]
        public void AddWidgetsTest()
        {
            ComponentManager componentManager = new ComponentManager();

            componentManager.Widgets = new Dictionary<uint, List<IImguiWidget>>()
            {
                {0, new List<IImguiWidget>()}
            };

            componentManager.AddWidget(0, new TransformWidget(new TransformComponent()));

            Assert.AreEqual(componentManager.Widgets[0].Count, 1);        
        }

        [Test]
        public void AddCameraComponentTest()
        {
            ComponentManager componentManager = new ComponentManager();

            componentManager.AddComponent<TransformComponent>(0);
            componentManager.AddComponent<CameraComponent>(0);

            Assert.AreEqual(componentManager.CameraComponents.Count, 1);
        }

        [Test]
        public void GetCameraComponentTest()
        {
            ComponentManager componentManager = new ComponentManager();

            componentManager.CameraComponents = new Dictionary<uint, CameraComponent>()
            {
                {0, new CameraComponent(new TransformComponent())}
            };

            Assert.AreNotEqual(componentManager.GetComponent<CameraComponent>(0), null);
        }
    }
}