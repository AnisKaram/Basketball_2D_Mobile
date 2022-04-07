using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    #region private variables
    [SerializeField] GameObject dotParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] int dot_amount;
    [SerializeField] float dot_spacing;
    Vector2 dotPosition;
    float timeStamp;
    //List<Transform> listOfDots;
    Transform[] arrayOfDots;
    #endregion private variables

    #region private methods
    void Start()
    {
        HideTrajectory();
        InstantiateDots();
    }

    void InstantiateDots()
    {
        //listOfDots = new List<Transform>(dot_amount);
        arrayOfDots = new Transform[dot_amount];
        for (int i = 0; i < dot_amount; i++)
        {
            arrayOfDots[i] = Instantiate(dotPrefab, null).transform;
            arrayOfDots[i].parent = dotParent.transform;
        }
    }
    #endregion private methods

    #region public methods
    public void ShowTrajectory()
    {
        dotParent.SetActive(true);
    }
    public void HideTrajectory()
    {
        dotParent.SetActive(false);
    }
    public void ShowUpdatedDots(Vector3 baskeBallPosition, Vector2 force)
    {
        timeStamp = dot_spacing;

        for (int i = 0; i < dot_amount; i++)
        {
            dotPosition.x = baskeBallPosition.x + baskeBallPosition.x * timeStamp;
            dotPosition.y = (baskeBallPosition.y + force.y * timeStamp)
                               - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            arrayOfDots[i].position = dotPosition;
            timeStamp += dot_spacing;
        }
    }
    #endregion public methods
}
