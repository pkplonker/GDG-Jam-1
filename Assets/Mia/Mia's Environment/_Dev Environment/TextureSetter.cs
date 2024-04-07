using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSetter : MonoBehaviour
{    
    public GameObject GroundTextureMask;
    public RuntimeGroundTexture RuntimeGroundTextureScript;
    public GroundTextureGenerator GroundTextureGeneratorScript;

    public Material shaderGraphMaterial;
    private Renderer renderer;
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
        }
        else
        {
            //Debug.Log("Fetched Texture from Scene: " + tex2Fetch.name);

            //Changing the texture
            //propertyBlock.SetTexture(texturePropertyName, tex2Fetch);
            //renderer.SetPropertyBlock(propertyBlock);

            //shaderGraphMaterial.SetTexture(texturePropertyName, tex2Fetch);
        }
        //tex2Set = tex2Fetch;

        renderer = GetComponent<Renderer>();         
        if (renderer == null)
        {
            Debug.LogWarning("Renderer component not found on GameObject: " + gameObject.name);
            return;
        }

        shaderGraphMaterial = renderer.material;
        if (shaderGraphMaterial == null)
        {
            Debug.LogWarning("Shader Graph Material not found on GameObject: " + gameObject.name);
        }
        //Debug.Log("Shader Graph Material is " + renderer.material);

        propertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propertyBlock);
        var texture = propertyBlock.GetTexture(texturePropertyName);
        tex2Set = texture;
        
        texturePropertyName = shaderGraphMaterial.GetTexture("_MaskCleaner").name;
        //tex2Set = renderer.material.GetTexture(texturePropertyName);
        //texturePropertyID = Shader.PropertyToID("_MaskCleaner");       
        if (tex2Set != null)
        {
            Debug.Log("Fetched Texture2D: " + tex2Set.name);
            //Debug.Log("Fetched texturePropertyID: " + texturePropertyID);
                        
            //Changing the texture
            propertyBlock.SetTexture(propertyBlock.GetTexture(texturePropertyName).name, tex2Fetch);
            renderer.SetPropertyBlock(propertyBlock);
        } else
        {
            Debug.LogWarning("Texture with the name '" + texturePropertyName + "' not found in the shader on the GameObject " + gameObject.name);            
        }
    }

    void Update()
    {
        RuntimeGroundTextureScript = GroundTextureMask.GetComponent<RuntimeGroundTexture>();
        tex2Fetch = RuntimeGroundTextureScript.texture;
        if (tex2Fetch == null)
        {
            Debug.LogWarning("Texture to fetch component not found on GameObject: " + gameObject.name);
        }

        if (tex2Fetch != null && renderer != null)
        {
            //Changing the texture
            propertyBlock.SetTexture(texturePropertyName, tex2Fetch);
            renderer.SetPropertyBlock(propertyBlock);

            shaderGraphMaterial.SetTexture(0, tex2Fetch);
        }

        if (shaderGraphMaterial != null && tex2Fetch != null)
        {
            //this one doesn't work
            //shaderGraphMaterial.SetTexture(texturePropertyName, tex2Fetch);
            //Debug.Log("Trying to set the texture on a Shader Graph"); //We do get to this part
            //shaderGraphMaterial.mainTexture = tex2Set;                     
        }
        else if (shaderGraphMaterial == null)
        {
            //Debug.LogWarning("Shader Graph Material not found on GameObject: " + gameObject.name);
        }
    }
}
