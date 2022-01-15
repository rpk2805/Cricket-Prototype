using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour
{
    //singleton intance;
    public static GameReset instance;
    private static bool gameReset;

    
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
    private void Start()
    {
        gameReset = false;
    }




    public IEnumerator ResetGame(float time)
    {
        yield return new WaitForSeconds(time);
        if(!gameReset)
        {
            gameReset = true;
            transform.rotation = BattingManager.instance.defaultRotation;
            //BattingManager.instance.bat. transform.position = BattingManager.instance.defaultPosition;
            BattingManager.instance.IsBatSwinged = false;

            BallingManager.instance.IsfirstBounce = false;
            BallingManager.instance.IsBallHit = false;
            BallingManager.instance.IsBallThrown = false;
            BallingManager.instance.ballRigidBody.velocity = Vector3.zero;
            //BallingManager.instance.ballRigidBody.freezeRotation = true;
            BallingManager.instance.ballRigidBody.useGravity = false;
            //BallingManager.instance.ball.transform.position = BallingManager.instance.initialPosition;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
       
       
    }
}
