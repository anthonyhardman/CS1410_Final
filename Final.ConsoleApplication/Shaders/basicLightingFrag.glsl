#version 330 core

out vec4 FragColor;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float constant;
    float linear;
    float quadratic;
};

in vec3 Normal;  
in vec3 FragPos;

uniform Light lights[100];
uniform int lightCount;
uniform Material material;
uniform vec3 viewPos;

vec3 CalcPointLight(Light light, Material material);

void main()
{
    vec3 result;

    for (int i = 0; i < lightCount; i++)
    {   
        result += CalcPointLight(lights[i], material);
    } 
        
    FragColor = vec4(result, 1.0);
}

vec3 CalcPointLight(Light light, Material material)
{
    // ambient
    vec3 ambient = light.ambient * material.ambient;
    
    // diffuse
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);

    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);

    // attenuation
    float distance = length(light.position - FragPos);
    float attenuation = 1.0f / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;

    return (ambient + diffuse + specular);
}