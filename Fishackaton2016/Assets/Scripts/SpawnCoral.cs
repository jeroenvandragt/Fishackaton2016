using UnityEngine;
using System.Collections;

public class SpawnCoral : MonoBehaviour
{
    public NavMesh nav;

    public GameObject[] coralTypes;

    public void Start()
    {
       // Instantiate(coralTypes[Random.Range(0,coralTypes.Length)],NavMesh.FindClosestEdge())
    }
}
