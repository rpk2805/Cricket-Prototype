using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallThrowEvent : MonoBehaviour
{
    private Button throwButton;
    // Start is called before the first frame update
    void Start()
    {
        throwButton = this.gameObject.GetComponent<Button>();
        throwButton.onClick.AddListener(FindObjectOfType<BallingManager>().BallAction);
    }

   
       
}
