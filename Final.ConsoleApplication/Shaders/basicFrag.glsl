#version 460 core

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

out vec4 fragColor;

uniform Material material;

void main()
{
    fragColor = vec4(material.diffuse, 1.0f);
}