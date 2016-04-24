using UnityEngine;
using System.Collections;

public class TESTMovement : MonoBehaviour
{
    public bool canReceiveInput = true;
    public float turnSensitivity;
    public GameObject waterSurface;
    public GameObject interactionCamera;

    private float rotationX  = 0;
    private float rotationY  = 0;
    private Quaternion originalRotation;
    private Quaternion xQuaternion;
    private Quaternion yQuaternion;

    private Rigidbody rb;
    public float movementSlowingFriction;
    public Animator myAnimator;


    private float movementSpeed;
    [SerializeField]
    private float maxMovementSpeed;
    [SerializeField]
    private float minMovementSpeed;
    private float speedIncrement = 1.1f;
    private float speedFallOf = 0.9f;
    public bool currentlyMoving()
    {
        return movementSpeed > minMovementSpeed;
    }
    public void accelerate()
    {
        myAnimator.SetBool( "isMoving" , true );
        if (!currentlyMoving())
        {
            movementSpeed = minMovementSpeed;
        }
        else if ((movementSpeed *= speedIncrement) < maxMovementSpeed)
        {
            movementSpeed *= speedIncrement;
        }
    }
    public void decelerate()
    {
        myAnimator.SetBool( "isMoving" , false );
        rb.velocity *= speedFallOf;
        movementSpeed *= speedFallOf;

        if (Mathf.Approximately( movementSpeed, 0 ))
        {
            movementSpeed = 0;
        }
    }
    public float distancePerFish;
    public void Start ( )
    {
        myAnimator = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        originalRotation = transform.localRotation;
        rb = GetComponent<Rigidbody>( );
    }
    public void FixedUpdate ( )
    {
        //---------------------------------Locks position under the water--------------------------------------------------------
        if(transform.position.y >= waterSurface.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x,waterSurface.transform.position.y-0.5f, transform.position.z );
        }
        Debug.Log( "Movement Speed: " + movementSpeed );
        if (movementSpeed <= minMovementSpeed)
        {
            movementSpeed = minMovementSpeed;
        }
        if (movementSpeed >= maxMovementSpeed)
        {
            movementSpeed = maxMovementSpeed;
        }

        while (Input.GetKey( KeyCode.W ))
        {
            accelerate();
        }
        if (currentlyMoving() & !Input.GetKey( KeyCode.W ))
        {
            decelerate();
        }

        // Read the mouse input axis
        rotationX += Input.GetAxis( "Mouse X" ) * turnSensitivity;
        rotationY += Input.GetAxis( "Mouse Y" ) * turnSensitivity;

        xQuaternion = Quaternion.AngleAxis( rotationX, Vector3.up );
        yQuaternion = Quaternion.AngleAxis( rotationY, Vector3.left );

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;

        if (currentlyMoving())
        {
            transform.position += transform.forward + new Vector3(0,0,movementSpeed);
            //Debug.Log( transform.forward + new Vector3( 0, 0, movementSpeed ));
        }
    }
}
