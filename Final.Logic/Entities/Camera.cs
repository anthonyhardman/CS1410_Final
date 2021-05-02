using GlmSharp;
using System;

namespace Final.Logic
{
    public enum Camera_Movement
    {
        FORWARD,
        BACKWARD,
        LEFT,
        RIGHT
    }
    public class CameraComponent : Component
    {
        TransformComponent TransformComponent_;
        public vec3 Front = new vec3(-1.0f, 0.0f, 0.0f);
        public vec3 Up;
        public vec3 Right;
        public vec3 WorldUp = new vec3(0.0f, 1.0f, 0.0f);

        float MovementSpeed = 2.5f;
        float MouseSensitivity = 0.05f;
        public float Zoom = 45.0f;

        public CameraComponent(TransformComponent transformComponent)
        {
            TransformComponent_ = transformComponent;
        }

        public mat4 GetViewMatrix()
        {
            vec3 position = TransformComponent_.Translate;
            return mat4.LookAt(position, position + Front, Up);
        }

        public void ProcessKeyboard(Camera_Movement direction, float deltaTime)
        {
            vec3 position = TransformComponent_.Translate;
            float velocity = MovementSpeed * deltaTime;
            switch (direction)
            {
                case Camera_Movement.FORWARD:
                    TransformComponent_.Translate += Front * velocity;
                    break;
                case Camera_Movement.BACKWARD:
                    TransformComponent_.Translate -= Front * velocity;
                    break;
                case Camera_Movement.RIGHT:
                    TransformComponent_.Translate -= Right * velocity;
                    break;
                case Camera_Movement.LEFT:
                    TransformComponent_.Translate += Right * velocity;
                    break;
            }
        }

        public void ProcessMouseMovement(float xoffset, float yoffset, bool constrainPitch = true)
        {
            xoffset *= MouseSensitivity;
            yoffset *= MouseSensitivity;

            TransformComponent_.Rotation.x -= yoffset;
            TransformComponent_.Rotation.y -= xoffset;

            if (constrainPitch)
            {
                if (TransformComponent_.Rotation.x > 89.0f)
                {
                    TransformComponent_.Rotation.x = 89.0f;
                }
                if (TransformComponent_.Rotation.x < -89.0f)
                {
                    TransformComponent_.Rotation.x = -89.0f;
                }
            }

            UpdateCameraVectors();
        }

        public void ProcessMouseScroll(float yoffset)
        {
            Zoom -= (float)yoffset * 0.01f;
            if (Zoom < 1.0f)
                Zoom = 1.0f;
            if (Zoom > 45.0f)
                Zoom = 45.0f;
        }

        private void UpdateCameraVectors()
        {
            vec3 front;
            vec3 rotation = TransformComponent_.Rotation;
            front.x = (float)Math.Cos(glm.Radians(rotation.y)) * (float)Math.Cos(glm.Radians(rotation.x));
            front.y = (float)Math.Sin(glm.Radians(rotation.x));
            front.z = -(float)Math.Sin(glm.Radians(rotation.y)) * (float)Math.Cos(glm.Radians(rotation.x));

            Front = glm.Normalized(front);

            Right = glm.Normalized(glm.Cross(Front, WorldUp));
            Up = glm.Normalized(glm.Cross(Right, Front));
        }

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
            AddComponent<CameraComponent>();

            GetComponent<TransformComponent>().Translate = new vec3(0.0f, 0.0f, 5.0f);
            GetComponent<TransformComponent>().Rotation.y = 90.0f;

            //UpdateCameraVectors();
        }
    }
}