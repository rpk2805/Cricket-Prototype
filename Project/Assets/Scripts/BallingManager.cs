using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallingManager : MonoBehaviour
{

    public static BallingManager instance; //singleton refrence

    #region ballproperties
   
    [HideInInspector]public GameObject ball; // ball's default beginning position
    [Header("Ball initial Pos")]
    public Vector3 initialPosition; // ball's default beginning position
    [Header("Ball properties")]
    [Tooltip("Speed of the ball")] public float ballingSpeed;//Speed of the ball
    [Tooltip("Ball pitch Marker")]private GameObject pitchPositionMarker;//Position of the ball to be pitched
    [Tooltip("Bounce angle value changer")] [SerializeField]private float bounceMultiplier; // the bounce scalar value to change the bounce angle after the ball hits the ground
    [Tooltip("spin value changer")] [SerializeField] private float spinValueMultiplier; // change the ball's spin value
    [HideInInspector]public Rigidbody ballRigidBody; // rigidbody of the ball
    #endregion

    #region ballPhysicsValues
    private float bounceAngle; // the bounce angle of the ball after the ball hits the ground
    private Vector3 startPosition; // ball's startPosition for the lerp function
    private Vector3 targetPosition; // ball's targetPosition for the lerp function
    private Vector3 direction; // the direction vector the ball is going in
    private float spinBy; // value to spin the ball by
    #endregion

    #region gamePlayStateBooleans
    private bool firstBounce; // whether ball's hit the ground once or not
    private bool isBallThrown; // whether the ball is thrown or not
    private bool isBallHit; // whether the bat hitted the ball
    #endregion

    #region public properties
    public float BallSpeed { set { ballingSpeed = value; } }
    public bool IsBallThrown { set { isBallThrown = value; } get { return isBallThrown; } }
    public bool IsBallHit { set { isBallHit = value; } get { return isBallHit; } }
    public bool IsfirstBounce { set { firstBounce = value; } get { return firstBounce; } }
    #endregion

    #region event
    
    #endregion




    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        pitchPositionMarker = GameObject.FindGameObjectWithTag("Marker");
        ball = this.gameObject;
        ballRigidBody = gameObject.GetComponent<Rigidbody>();
        initialPosition = transform.position; // set defaultPosition to the balls beginning position
        startPosition = transform.position;  // set the startPosition to the balls beginning position

    }
    #region ball physics application
    void OnCollisionEnter(Collision collision)
    {
        if (!isBallHit && collision.gameObject.CompareTag("Field"))
        {
            //randomize the ball swing
            int ballType = Random.Range(0,3);
            switch (ballType)
            { // check the ballType and set the spinBy value depending on the ball's speed
                case 0:
                    spinBy = direction.x; // no spin ball
                    break;
                case 1:
                    spinBy = spinValueMultiplier / ballingSpeed; // change spinBy to a positive value 
                    break;
                case 2:
                    spinBy = -spinValueMultiplier / ballingSpeed; // change spinBy to a negative value 
                    break;
                default:
                    break;
            }

            if (!firstBounce)
            { // if firstBounce is false i.e. when the ball hits the ground for the first time then the expression returns true 
                firstBounce = true; // set the firstBounce bool to true
                ballRigidBody.useGravity = true; // allow the gravity to affect the ball
              

                // change the y value of the direction to the negative of it's present value multiplied by the bounceScalar and ball's speed
                // of the ball i.e. the bounce will be more if the ball's speed is more compared to a slower one
                direction = new Vector3(spinBy, -direction.y * (bounceMultiplier * ballingSpeed), direction.z);
                direction = Vector3.Normalize(direction); // normalize the direction value

                bounceAngle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg; // calculte the bounce angle from the direction vector

                // Add an instant force impulse in the direction vector multiplied by ballSpeed to the ball considering its mass
                ballRigidBody.AddForce(direction * ballingSpeed, ForceMode.Impulse);
               
            }
            
        }
       

        if (collision.gameObject.CompareTag("Stump"))
        { // if the ball has hit the stump then the expression returns true
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true; // set the stump's rigidbody to be affected by gravity
        }
    }
    #endregion
    //Balling Action method
    public void BallAction()
    {

        if (!IsBallThrown)
        {
            //ballRigidBody.freezeRotation = false;
              isBallThrown = true;
            //CanvasManagerScript.instance.EnableBatSwipePanel(); // Enable the bat swipe panel 
            targetPosition = pitchPositionMarker.transform.position; // make the balls target position to the markers position
            direction = Vector3.Normalize(targetPosition - startPosition); // calculate the direction vector
            ballRigidBody.AddForce(direction * ballingSpeed, ForceMode.Impulse); // Add an instant force impulse in the direction vector multiplied by ballSpeed to the ball considering its mass
            StartCoroutine(GameReset.instance.ResetGame(6));
        }
    }
   
   


}
