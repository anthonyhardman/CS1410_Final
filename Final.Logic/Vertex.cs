using GlmSharp;

namespace Final.Logic
{
    public struct Vertex
    {
        public vec3 Position;
        public vec3 Normal;
        public vec2 TextureCoordinate;

        public Vertex(vec3 position, vec3 normal, vec2 texCoord)
        {
            Position = position;
            Normal = normal;
            TextureCoordinate = texCoord;
        }

        public Vertex(float pX, float pY, float pZ, float nX, float nY, float nZ, float tX, float tY)
        {
            Position = new vec3(pX, pY, pZ);
            Normal = new vec3(nX, nY, nZ);
            TextureCoordinate = new vec2(tX, tY);
        }
    }
}