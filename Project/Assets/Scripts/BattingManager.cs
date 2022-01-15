using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattingManager : MonoBehaviour
{
    //Singleton reference
    public static BattingManager instance;

   
    

    //GameObjectReferences
    [HideInInspector] public GameObject bat;// the bat gameObject
    [Tooltip("Ball gameobject regerence")]public GameObject ball; // the ball gameObject
    [Tooltip("max x value the bat can cover")] public float boundaryPointX; // max x value the bat can cover

    [Header("BatsMan Properties")]
    [Tooltip("batsman x range")] public float batsManXMax; // the ball cannot be hit once it gets outside this limit
    private bool isBatSwinged; // has the bat swinged
    [HideInInspector]public Vector3 defaultPosition; // bat's default beginning position
    [HideInInspector] public Quaternion defaultRotation; // bat's default beginning roation
    public bool IsBatSwinged { set { isBatSwinged = value; } get { return isBatSwinged; } }

    private void Awake()
    {
        //create instance of the class
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
        bat = this.gameObject;
        defaultPosition = bat.transform.position; // set defaultPosition to the bats beginning position
        defaultRotation = bat.transform.rotation; // set defaultPosition to the bats beginning position
    }


    // Update is called once per frame
    void Update()
    {
        // if the bat has not swinged once and the ball is thrown and inside the bats hit range then 
        if (!isBatSwinged && BallingManager.instance.IsBallThrown && ball.transform.position.z <= batsManXMax)
        {
            transform.transform.position = new Vector3(ball.transform.position.x,
                transform.transform.position.y,
                transform.transform.position.z);
        }

        // Clamp the bats position withing the pitch width
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundaryPointX, boundaryPointX), transform.position.y, transform.position.z);
        if (!BallingManager.instance.IsBallHit)
            bat.transform.position = new Vector3(bat.transform.position.x, ball.transform.position.y, bat.transform.position.z);





    }
    public void SwipeBat(SwipeDetector.swipeDirection dir)
    {
        if (BallingManager.instance.IsfirstBounce)
        {
           

            if (dir==SwipeDetector.swipeDirection.right)
            {
               
                float roattionAngle = Random.Range(-20, -46);
                bat.transform.localRotation = Quaternion.Euler(bat.transform.eulerAngles.x, roattionAngle, bat.transform.eulerAngles.z);

            }
           else if (dir == SwipeDetector.swipeDirection.left)
            {
                float roattionAngle = Random.Range(20, 46);
                bat.transform.localRotation = Quaternion.Euler(bat.transform.eulerAngles.x, roattionAngle, bat.transform.eulerAngles.z);

            }


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Debug.Log("Collided");
            BallingManager.instance.IsBallHit = true;
            StartCoroutine(GameReset.instance.ResetGame(5));
        }
          

    }

}
