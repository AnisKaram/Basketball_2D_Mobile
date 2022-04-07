using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region private variables
    [SerializeField] Rigidbody2D playerRb2;
    [SerializeField] LineRenderer playerLr;
    [SerializeField] GameObject x_axis_1; // x axis point 1 boundary
    [SerializeField] GameObject x_axis_2; // x axis point 2 boundary
    [SerializeField] GameObject y_axis_1; // y axis point 1 boundary
    [SerializeField] GameObject y_axis_2; // y axis point 2 boundary
    static GameManager gameManager;
    float dragPower = 15f; // Ball force
    Touch touchInput;
    new Camera camera;
    Vector2 movedDragPosition;
    Vector2 endedDragPosition;

    #region Trajectory Variables | Not Used
    Vector2 startpos;
    Vector2 direction;
    Vector2 force;
    float distance;
    //[SerializeField] TrajectoryController trajectoryController;
    #endregion Trajectory Variables | Not Used

    #endregion private variables

    #region private methods
    void Awake()
    {
        playerRb2 = GetComponent<Rigidbody2D>();

        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
        }
    }
    void Start()
    {
        transform.position = new Vector3(1, 1, 0); // Default Player Position
        camera = Camera.main; // Initialzing Camera
        playerRb2.gravityScale = 0f; // Setting gravity scale to 0
    }
    void Update()
    {
          // On update we need to check if the player is touching the screen
          if (Input.touchCount > 0) {
            touchInput = Input.GetTouch(0); // Get the first touch

            // Touch Started
            if (touchInput.phase == TouchPhase.Began) { TouchStarted(); }
            // Touch Moved
            if (touchInput.phase == TouchPhase.Moved) { TouchMoved(); }
            // Touch Ended
            if (touchInput.phase == TouchPhase.Ended) { TouchEnded(); }
        }
    }
    void TouchStarted()
    {
        startpos = camera.ScreenToWorldPoint(touchInput.position);
        //startPos.z = 0f; // no need to drag on z (avoid weird dragging)
        playerLr.positionCount = 1; // increasing the count from 0 to 1 (counted as start)
        playerLr.SetPosition(0, startpos); // position from 0 to the start position
        //trajectoryController.ShowTrajectory(); // Show Trajectory when player start dragging
    }
    void TouchMoved() 
    {
        movedDragPosition = camera.ScreenToWorldPoint(touchInput.position);
        //movedDragPosition.z = 0f; // no need to drag on z (avoid weird dragging)
        playerLr.positionCount = 2; // increasing the count 2
        playerLr.SetPosition(1, movedDragPosition); // position from 2 to the drag position

        #region Trajectory | Not Used
        distance = Vector2.Distance(startpos, endedDragPosition);
        direction = (startpos - endedDragPosition);
        force = direction * distance * dragPower;
        #endregion Trajectory | Not Used

        //trajectoryController.ShowUpdatedDots(transform.position, force); // Update dots when the player moves his finger
    }
    void TouchEnded() 
    {
        playerRb2.gravityScale = 5f; // After the player release his finger, set gravity to 5
        playerRb2.freezeRotation = false; // Let the ball rotate
        playerLr.positionCount = 0; // setting the count again to 0, when the player release

        #region TO USE
        endedDragPosition = camera.ScreenToWorldPoint(touchInput.position);
        //endedDragPosition.z = 0f;
        //trajectoryController.HideTrajectory(); // Hide trajectory when the plaer releases his finger

        // Calculating the vector, where the touch started until when it ended
        Vector3 forceToAdd = startpos - endedDragPosition;
        //Vector3 clampToAdd = Vector3.ClampMagnitude(forceToAdd, maxDrag) * dragPower;

        // Add Instant Force (impulse)
        //playerRb2.AddForce(clampToAdd, ForceMode2D.Impulse);
        playerRb2.AddForce(forceToAdd*dragPower, ForceMode2D.Impulse);
        #endregion TO USE

        #region Trajectory | Not Used
        //playerRb2.AddForce(force, ForceMode2D.Impulse);
        //trajectoryController.HideTrajectory();
        #endregion Trajectory | Not Used

        float x_val = Random.Range(x_axis_1.transform.position.x, x_axis_2.transform.position.x);
        float y_val = Random.Range(y_axis_1.transform.position.y, y_axis_2.transform.position.y);
        float z_val = 0;

        StartCoroutine(ResetPlayerPosition(x_val, y_val, z_val));
    }

    IEnumerator ResetPlayerPosition(float x_val, float y_val, float z_val)
    {
        yield return new WaitForSeconds(1.5f); // Wait 2seconds then implement the below logic
        playerRb2.freezeRotation = true; // Freeze the rotation to prevent the ball keep rotating when the position reset
        playerRb2.velocity = Vector2.zero; // Prevent the ball from rolling all over
        playerRb2.gravityScale = 0f; // Reset the Gravity to keep the ball in air before shooting
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(x_val, y_val, z_val);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ToIncreaseScore") {
            gameManager.AddScoreTime();
        }
    }
    #endregion private methods
}
