using UnityEngine;
using System.Collections.Generic;

public class CustomWindZone : MonoBehaviour
{
    public List<Rigidbody> rbList = new List<Rigidbody>();

    public void OnTriggerEnter ( Collider other )
    {
        if(other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,1),ForceMode.Force) ;
            if(!rbList.Contains(other.GetComponent<Rigidbody>()))
                rbList.Add(other.GetComponent<Rigidbody>());
        }
    }

    public void OnTriggerExit ( Collider other )
    {
        if(other.GetComponent<Rigidbody>())
        {
            if(rbList.Contains(other.GetComponent<Rigidbody>()))
                rbList.Remove(other.GetComponent<Rigidbody>());
        }
    }
}
