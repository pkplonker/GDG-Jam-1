using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSetter : MonoBehaviour
{    
    public GameObject GroundTextureMask;
    public RuntimeGroundTexture RuntimeGroundTextureScript;
    public GroundTextureGenerator GroundTextureGeneratorScript;
    
    public Texture tex2Fetch;  //Texture that is in the scene
    public Texture tex2Set; //Texture2D that is in the ShaderGraph
    public Vector2Int textureSize { get; set; } = new Vector2Int(1024, 1024);

    public Material shaderGraphMaterial; 

    void Start()
    {
        GroundTextureGeneratorScript = GroundTextureMask.GetComponent<GroundTextureGenerator>();
        tex2Fetch = GroundTextureGeneratorScript.savedTexture;
        tex2Set = tex2Fetch;

        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null)
        {
            shaderGraphMaterial = renderer.material;
            if(shaderGraphMaterial == null)
            {
                Debug.LogWarning("Shader Graph Material not found on GameObject: " + gameObject.name);
            }
            else
            {
                Debug.LogWarning("Shader Graph Material is found: " + shaderGraphMaterial.name);
            }
        }
        else
        {
            Debug.LogWarning("Renderer component not found on GameObject: " + gameObject.name);
        }
    }

    void Update()
    {
        RuntimeGroundTextureScript = GroundTextureMask.GetComponent<RuntimeGroundTexture>();
        tex2Fetch = RuntimeGroundTextureScript.texture;
        tex2Set = tex2Fetch;

        if (shaderGraphMaterial != null && tex2Set != null)
        {
            //shaderGraphMaterial.SetTexture("Mask Cleaner", tex2Set);
            shaderGraphMaterial.mainTexture = tex2Set;
        }
        else if (shaderGraphMaterial == null)
        {
            Debug.LogWarning("Shader Graph Material not found on GameObject: " + gameObject.name);
        }
        else if (tex2Set == null)
        {
            Debug.LogWarning("Texture to set component not found on GameObject: " + gameObject.name);
        }
    }
}
