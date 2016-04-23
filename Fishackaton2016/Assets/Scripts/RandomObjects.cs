using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RandomObjects : MonoBehaviour
{
    public Terrain terrain;
    public int numberOfObjects; // number of objects to place

    public GameObject[] coralToPlace; // GameObject to place
    private GameObject[] coralPlaced;
    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z

    public bool repopulateTerrain;
    public GameObject parentObject;

    void Start ( )
    {
        // terrain size x
        terrainWidth = (int)terrain.terrainData.size.x;
        // terrain size z
        terrainLength = (int)terrain.terrainData.size.z;
        // terrain x position
        terrainPosX = (int)terrain.transform.position.x;
        // terrain z position
        terrainPosZ = (int)terrain.transform.position.z;


    }
    // Update is called once per frame

    #if UNITY_EDITOR
    void Update ( )
    {
        if(repopulateTerrain)
        {
            repopulate();
            repopulateTerrain = false;
        }  
    }
    #endif


    public void repopulate()
    {      
        for (int i = 0; i < numberOfObjects; i++)
        {
            // generate random x position
            int posx = Random.Range( terrainPosX, terrainPosX + terrainWidth );
            // generate random z position
            int posz = Random.Range( terrainPosZ, terrainPosZ + terrainLength );
            // get the terrain height at the random position
            float posy = Terrain.activeTerrain.SampleHeight( new Vector3( posx, 0, posz ) );
            // create new gameObject on random position
            GameObject newObject = (GameObject)Instantiate( coralToPlace[Random.Range( 0, coralToPlace.Length - 1 )], new Vector3( posx, posy, posz ), Quaternion.identity );
            newObject.transform.parent = parentObject.transform;
        }

    }
}