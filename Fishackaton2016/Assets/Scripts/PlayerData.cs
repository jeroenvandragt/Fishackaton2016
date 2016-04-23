using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{

    public GameObject[] fishesFollowing;
    [Space(10)]
    public int maxAmountOfFish;
    [Space( 10 )]
    public int amountOfSpaceBetweenFish;

    public void Update()
    {
        foreach(GameObject fish in fishesFollowing)
        {                                              

            if(System.Array.IndexOf(fishesFollowing,fish) == 0)
            {
                
            }
        }
    }
	
}
