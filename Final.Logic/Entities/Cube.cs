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
            AddComponent<ShaderComponent>();
            AddComponent<MaterialComponent>();
        }
    }

    public class Terrain : Entity
    {
        public static uint NumberOfTerrains;

        static Terrain()
        {
            NumberOfTerrains = 0;
        }

        public Terrain() : base()
        {
            Name = $"Terrain{NumberOfTerrains++}";

            AddComponent<TransformComponent>();
            GetComponent<TransformComponent>().Rotation.x = -90;
            AddComponent<RenderComponent>();
            GetComponent<RenderComponent>().Model_ = new Model("Models\\terrain.fbx", new Shader("Shaders\\basicLightingVert.glsl", "Shaders\\basicLightingFrag.glsl"));
            AddComponent<MaterialComponent>();
        }
    }
}