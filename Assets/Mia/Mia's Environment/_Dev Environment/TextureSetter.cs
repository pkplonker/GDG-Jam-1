using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSetter : MonoBehaviour
{    
    public GameObject GroundTextureMask;
    public RuntimeGroundTexture RuntimeGroundTextureScript;
    public GroundTextureGenerator GroundTextureGeneratorScript;

    public Material shaderGraphMaterial;
    private Renderer rendererComponent;
    public string texturePropertyName = "_MaskCleaner";
    public MaterialPropertyBlock propertyBlock;

    public Texture tex2Fetch;  //Texture that is in the scene
    public Texture tex2Set; //Texture2D that is in the ShaderGraph

    void Start()
    {
        //Fetching the texture from the scene
        GroundTextureGeneratorScript = GroundTextureMask.GetComponent<GroundTextureGenerator>();
        tex2Fetch = GroundTextureGeneratorScript.savedTexture;
        if (tex2Fetch == null)
        {
            Debug.LogWarning("Texture to fetch component not found on GameObject: " + gameObject.name);
        } //basic debug warning

        rendererComponent = GetComponent<Renderer>();         
        if (rendererComponent == null)
        {
            Debug.LogWarning("Renderer component not found on GameObject: " + gameObject.name);
        } //basic debug warning

        shaderGraphMaterial = rendererComponent.material;
        if (shaderGraphMaterial == null)
        {
            Debug.LogWarning("Shader Graph Material not found on GameObject: " + gameObject.name);
        } //basic debug warning

        propertyBlock = new MaterialPropertyBlock();
        rendererComponent.GetPropertyBlock(propertyBlock);

        texturePropertyName = shaderGraphMaterial.GetTexture("_MaskCleaner").name;
        var texture = propertyBlock.GetTexture(texturePropertyName);
        tex2Set = texture;
        if (tex2Set != null) 
        {
            Debug.Log("Fetched Texture2D: " + tex2Set.name);
                        
            //Changing the texture
            propertyBlock.SetTexture(propertyBlock.GetTexture(texturePropertyName).name, tex2Fetch);
            rendererComponent.SetPropertyBlock(propertyBlock);
        } else
        {
            Debug.LogWarning("Texture with the name '" + texturePropertyName + "' not found in the shader on the GameObject " + gameObject.name);
        } //basic debug warning
    }

    void Update()
    {
        RuntimeGroundTextureScript = GroundTextureMask.GetComponent<RuntimeGroundTexture>();
        tex2Fetch = RuntimeGroundTextureScript.texture;
        if (tex2Fetch == null)
        {
            Debug.LogWarning("Texture to fetch component not found on GameObject: " + gameObject.name);
        } //basic debug warning

        if (tex2Fetch != null && rendererComponent != null)
        {
            //Changing the texture
            propertyBlock.SetTexture(texturePropertyName, tex2Fetch);
            rendererComponent.SetPropertyBlock(propertyBlock);

            shaderGraphMaterial.SetTexture(0, tex2Fetch);
        }

        else if (shaderGraphMaterial == null)
        {
            Debug.LogWarning("Shader Graph Material not found on GameObject: " + gameObject.name);
        } //basic debug warning
    }
}
