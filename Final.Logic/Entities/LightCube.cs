namespace Final.Logic
{
    public class LightCube : Cube
    {
        public LightCube() : base()
        {
            AddComponent<LightComponent>();
        }
    }
}