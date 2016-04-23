using UnityEngine;
using System.Collections;


public class UnderwaterScript : MonoBehaviour
{

    //This script enables underwater effects. Attach to main camera.

    //Define variable
    public GameObject underwaterLevel;
    public GameObject player;
    public Color fogColor;
    public float fogDensity;

    //The scene's default fog settings
    private bool defaultFog = RenderSettings.fog;
    private Color defaultFogColor = RenderSettings.fogColor;
    private float defaultFogDensity = RenderSettings.fogDensity;
    private Material defaultSkybox = RenderSettings.skybox;
    private Material noSkybox;

    private Camera myCamera;

    void Start ( )
    {
        myCamera = Camera.main;
        
        //Set the background color
        //myCamera.backgroundColor = new Color( 0, 0.4f, 0.7f, 1 );
    }

    void Update ( )
    {
        if (transform.position.y < underwaterLevel.transform.position.y)
        {   
            
            RenderSettings.fog = true;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        }
        else
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
        }
    }
}