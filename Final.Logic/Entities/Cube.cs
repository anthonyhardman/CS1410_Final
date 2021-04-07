namespace Final.Logic
{
    public class Cube : Entity
    {
        public static uint NumberOfCubes;

        static Cube()
        {
            NumberOfCubes = 0;
        }

        public Cube() : base()
        {
            Name = $"Cube{NumberOfCubes++}";

            AddComponent<TransformComponent>();
            AddComponent<RenderComponent>();

            GetComponent<RenderComponent>().Model_ = new Model("Models\\cube.dae", new Shader("Shaders\\basicLightingVert.glsl", "Shaders\\basicLightingFrag.glsl"));
        }
    }
}