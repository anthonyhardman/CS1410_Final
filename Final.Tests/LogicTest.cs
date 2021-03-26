using NUnit.Framework;
using Final.Logic;

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
            Entity entity = new Entity();
            ComponentManager componentManager = new ComponentManager();

            componentManager.AddComponent<TransformComponent>(entity.ID);

            if (componentManager.TransformComponents.Count == 1)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AddRenderComponentTest()
        {
            Entity entity = new Entity();
            ComponentManager componentManager = new ComponentManager();
            componentManager.AddComponent<RenderComponent>(entity.ID);

            if (componentManager.RenderComponents.Count == 1)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetTransformCompnentTest()
        {
            Entity entity = new Entity();
            ComponentManager componentManager = new ComponentManager();
            componentManager.AddComponent<TransformComponent>(entity.ID);

            TransformComponent transform = componentManager.GetComponent<TransformComponent>(entity.ID);

            if (transform != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetRenderCompnentTest()
        {
            Entity entity = new Entity();
            ComponentManager componentManager = new ComponentManager();
            componentManager.AddComponent<RenderComponent>(entity.ID);

            RenderComponent renderComponent = componentManager.GetComponent<RenderComponent>(entity.ID);

            if (renderComponent != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetInvalidTransformComponentTest()
        {
            Entity entity = new Entity();
            ComponentManager componentManager = new ComponentManager();
            try
            {
                componentManager.GetComponent<TransformComponent>(entity.ID);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetInvalidRenderComponentTest()
        {
            Entity entity = new Entity();
            ComponentManager componentManager = new ComponentManager();

            try
            {
                componentManager.GetComponent<RenderComponent>(entity.ID);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Assert.Pass();
            }
        }
    }
}