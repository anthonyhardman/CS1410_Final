using GlmSharp;

namespace Final.Logic
{
    public enum Camera_Movement
    {
        FORWARD,
        BACKWARD,
        LEFT,
        RIGHT
    }
    public class Camera : Entity
    {
        public static uint NumberOfCameras;


        static Camera()
        {
            NumberOfCameras = 0;
        }

        public Camera() : base()
        {
            Name = $"Camera{NumberOfCameras++}";

            AddComponent<TransformComponent>();
            GetComponent<TransformComponent>().Translate = new vec3(0.0f, 0.0f, 5.0f);
            GetComponent<TransformComponent>().Rotation.y = 90.0f;
            AddComponent<CameraComponent>();
        }
    }
}