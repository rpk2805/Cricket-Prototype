using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    

    private float ballSpeedSliderValue;//balling speed 
    [Tooltip("Balling speed slider reference")]public GameObject ballSpeedSlider;
    private float ingameBallSpeed;//balling speed in game
    private float realWorldBallSpeed;//balling speed in real world
    public float minInGameBallSpeed;//minimum speed the ball should have
    public float maxInGameBallSpeed;//max speed the ball should have
    public float minRealWorldBallSpeed;
    public float maxRealWorldBallSpeed;
    [Tooltip("Balling speed text reference")] public TextMeshProUGUI ballSpeedText;
    // Start is called before the first frame update
   

    // Update is called once per frame
   
  

    //change the ball speed using slider
    public void ChangeBallSpeed()
    {
        ballSpeedSliderValue = ballSpeedSlider.GetComponent<Slider>().value;
        ingameBallSpeed = ScaleSpeedToIngame(ballSpeedSliderValue, minInGameBallSpeed, maxInGameBallSpeed, 0, 1);
        realWorldBallSpeed = ScaleSpeedToIngame(ballSpeedSliderValue, minRealWorldBallSpeed, maxRealWorldBallSpeed, 0, 1);
        ballSpeedText.text = realWorldBallSpeed.ToString("#.##") + "kmph";
        // Update ballSpeed to inGameBallSpeed
        BallingManager.instance.ballingSpeed = ingameBallSpeed;

        Debug.Log(BallingManager.instance.ballingSpeed);
    }

    //convert the realworld ball speed into in game speed
    private float ScaleSpeedToIngame(float speed, float scaleMinTo, float scaleMaxTo, float scaleMinFrom, float scaleMaxFrom)
    {
        return (scaleMaxTo - scaleMinTo) * (speed - scaleMinFrom) / (scaleMaxFrom - scaleMinFrom) + scaleMinTo;
    }
}
