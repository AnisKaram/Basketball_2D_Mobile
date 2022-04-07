using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    #region public variables
    // Variables made public to access in the GameManager script
    public float speed;
    public float amplitude;
    #endregion public variables

    #region private variables
    // We could make it PUBLIC to access the position of the baskert inside the GameManager script
    Vector3 basketPos;
    #endregion private variables

    #region private methods
    void Start()
    {
        basketPos = transform.position;
    }
    void Update()
    {
        // Movement of the basket will depend on the Sin Wave (y axis) -> up and down
        // Mathf.Sin(Time.time) -> Time.time is the time since the game started
        // Amplitude is the slope in the sine wave (the height) 
        transform.position = new Vector3(basketPos.x, Mathf.Sin(Time.time * speed) * amplitude + basketPos.y, 0); 
    }
    #endregion private methods
}
