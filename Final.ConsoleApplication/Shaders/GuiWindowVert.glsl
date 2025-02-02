#version 460 core

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNorm;
layout (location = 2) in vec3 aTexCoord;

uniform mat4 model;

void main()
{
    gl_Position = model * vec4(aPos, 1.0f);
}