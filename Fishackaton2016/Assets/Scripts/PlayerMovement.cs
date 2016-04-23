using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 forwardForce;
    public Vector3 backwardForce;
    public Vector3 turnForceLeft;
    public Vector3 turnForceRight;

	void Start ()
    {
	   rb = GetComponent<Rigidbody>(); 
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(forwardForce,ForceMode.Acceleration );  
        }
	    if(Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce( backwardForce, ForceMode.Acceleration);  
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate( turnForceLeft );
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate( turnForceRight );
        }

	}
}
