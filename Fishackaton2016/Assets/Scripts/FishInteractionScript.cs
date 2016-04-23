using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum FishType
{
    ParrotFish,
    TrumpetFish,
    Groupers,
    Seahorse,
    ClownFish,
}

public class FishInteractionScript : MonoBehaviour
{
    private Collider playerObject;
    // The target we are following
    public Transform target;
    private bool isInteractionDone = false;

    public string[] interactionWithPlayer;
    private int currentInteractionElement = 0;

    public Text interactionTextUI;
    public FishType type;

    private bool followingPlayer;

    // The distance in the x-z plane to the target
    public float distance = 1.0f;
    // the height we want the camera to be above the target
    public float height = 0.0f;
    // How much we 
    public float heightDamping = 1.0f;
    public float rotationDamping = 1.0f;

    public void Start()
    {

    }

    public void OnTriggerEnter( Collider other )
    {
        if (!isInteractionDone)
        {
            interactionTextUI.text = interactionWithPlayer[currentInteractionElement];
        }
        
    }

    public void OnTriggerStay ( Collider other )
    {
        if(other.tag == "Player")
        {
            if (!isInteractionDone)
            {
                Debug.Log( "Player detected!" );
                playerObject = other;
                target = playerObject.transform;
                stayTrigger( );
            } 
        }
        if (other.tag == "SpawningGround")
        {
            if (other.GetComponent<SpawningGround>( ).SpawningGroundType == type)
            {
                Debug.Log( "FOUND A MATCH!" );
                stopFollowing( );
                goTowardsTarget( other.transform );
            }
        }
    }

    void FixedUpdate ( )
    {
        if (followingPlayer)
        {
            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles
            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y + height;
            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle( currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime );

            // Damp the height
            currentHeight = Mathf.Lerp( currentHeight, wantedHeight, heightDamping * Time.deltaTime );

            // Convert the angle into a rotation
            Quaternion currentRotation = Quaternion.Euler( 0, currentRotationAngle, 0 );

            // Set the position of the gameobject on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3( transform.position.x, currentHeight, transform.position.z );

            // Always look at the target
            transform.LookAt( target );
        }
    }

    public void Update()
    {
        if (playerObject != null)
        {
            if (Input.GetMouseButtonDown( 0 ))
            {
                if (currentInteractionElement == interactionWithPlayer.Length - 1)
                {
                    exitTrigger( );
                }
                else
                {
                    currentInteractionElement += 1;
                    interactionTextUI.text = interactionWithPlayer[currentInteractionElement];
                }  
            }
        } 
    }

    public void stayTrigger( )
    {
        playerObject.transform.position = Vector3.MoveTowards( playerObject.transform.position, transform.GetChild( 0 ).transform.position, 2 );
        playerObject.transform.LookAt( transform.position );
        playerObject.GetComponent<Rigidbody>( ).isKinematic = false;
        playerObject.GetComponent<TESTMovement>( ).canReceiveInput = false;
        playerObject.GetComponent<TESTMovement>( ).currentlyMoving = false;
        playerObject.GetComponent<TESTMovement>().interactionCamera.SetActive(true);
    }
    public void exitTrigger( )
    {
        playerObject.GetComponent<Rigidbody>( ).isKinematic = true;
        playerObject.GetComponent<TESTMovement>( ).canReceiveInput = true;
        playerObject.GetComponent<TESTMovement>( ).currentlyMoving = false;
        playerObject.GetComponent<TESTMovement>( ).interactionCamera.SetActive( false );
        isInteractionDone = true;
        interactionTextUI.text = "";
        followingPlayer = true;
        playerObject.GetComponent<TESTMovement>( ).distancePerFish += distance;
    }

    public void stopFollowing()
    {
        followingPlayer = false;
        playerObject.GetComponent<TESTMovement>( ).distancePerFish -= distance;
    }

    public void goTowardsTarget(Transform location)
    {
        transform.LookAt(location);
        //transform.Translate(location.position);
        Vector3.MoveTowards(transform.position, location.position,0.0f);
    }
}
