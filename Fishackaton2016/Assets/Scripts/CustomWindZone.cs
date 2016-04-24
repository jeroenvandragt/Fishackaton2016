using UnityEngine;
using System.Collections.Generic;

public class CustomWindZone : MonoBehaviour
{
    public List<Rigidbody> rbList = new List<Rigidbody>();

    public void listMan(Rigidbody other)
    {
        other = other.GetComponent<Rigidbody>();
        if (!rbList.Contains(other))
            rbList.Add(other);
        if (rbList.Contains(other))
            rbList.Remove(other);
    }

    public void OnTriggerEnter(Collider other)
    {
        Rigidbody enterer = other.GetComponent<Rigidbody>();
        if (enterer)
        {
            enterer.AddForce(new Vector3(0, 0, 1), ForceMode.Force);
            listMan(enterer);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Rigidbody leaver = other.GetComponent<Rigidbody>();
        if (leaver)
        {
            leaver.velocity = Vector3.zero;
            listMan(leaver);
        }
    }
}
