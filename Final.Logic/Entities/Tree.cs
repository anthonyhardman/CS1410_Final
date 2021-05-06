namespace Final.Logic
{
    public class Tree : Entity
    {
        public static int NumberOfTrees;

        public Tree(uint treeType) : base()
        {
            Name = $"Tree{NumberOfTrees++}";

            AddComponent<TransformComponent>();
            GetComponent<TransformComponent>().Rotation.x = -90.0f;
            AddComponent<RenderComponent>();
            GetComponent<RenderComponent>().Model_ = new Model ($"Models\\tree{treeType}.FBX", new Shader("Shaders\\basicLightingVert.glsl", "Shaders\\basicLightingFrag.glsl"));
            AddComponent<MaterialComponent>();
        }
    }
}