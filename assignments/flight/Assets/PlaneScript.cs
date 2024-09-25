using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneScript : MonoBehaviour {
    public GameObject cameraObject;
    public Terrain terrain;
    public TMP_Text scoreText;
    public TMP_Text loseText;
    public TMP_Text winText;
    public GameObject startScreen;
    public float hAxis;
    public float vAxis;
    float forwardSpeed = 10f;
    public float speedBoostAmount = 15f;
    float xRotationSpeed = 90f;
    float yRotationSpeed = 90f;
    int score = 0;

    private bool gameStarted = false;
    private Vector3 startPosition = new Vector3(7.21f, 6.37f, 17.44f);
    private Quaternion startRotation = Quaternion.Euler(0f, 50.48f, 0f);

    void Start() 
    {
        loseText.gameObject.SetActive(false);
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            if (loseText.gameObject.activeSelf)
            {
                RestartGame();
            }
            else
            {
                Destroy(startScreen);
                gameStarted = true;
            }
        }
        else if (gameStarted)
        {
            PlaneMovement();
        }
    }

    void PlaneMovement()
    {
        // Plane Movements
        hAxis = Input.GetAxis("Horizontal"); 
        vAxis = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * vAxis);
        transform.Translate(Vector3.right * Time.deltaTime * hAxis);
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        // Plane Controls
        Vector3 amountToRotate = new Vector3(0, 0, 0);
        amountToRotate.x = -vAxis * xRotationSpeed;
        amountToRotate.y = hAxis * yRotationSpeed;
        amountToRotate *= Time.deltaTime;
        transform.Rotate(amountToRotate, Space.Self);

        // Position the camera
        Vector3 cameraPosition = transform.position;
        cameraPosition += -transform.forward * 10f; 
        cameraPosition += Vector3.up * 8f; 
        cameraObject.transform.position = cameraPosition;

        // Make the camera look at the plane
        cameraObject.transform.LookAt(transform.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectable"))
        {
            score++;
            scoreText.text = "Score: " + score;
            Destroy(other.gameObject);
            forwardSpeed += speedBoostAmount;
            Invoke("ResetSpeed", 3f);

            if (score >= 35)
            {
                scoreText.text = "Winner!";
                winText.text = "Congratulations, Soldier!\nThe enemy forces have been stopped thanks to your efforts.\nYou are a true winner!!";
                gameStarted = false;
            }
        }
        else if (other.CompareTag("wall"))
        {
            loseText.text = "We've lost you, Soldier!\nIt's a sad day for us all.\nBut Good news - we still got the money and are deploying a new soldier.\nYour progress has been saved!\nTry Again?\n\nPress (spacebar) twice to try again.";
            loseText.gameObject.SetActive(true);
            gameStarted = false;
        }
    }
    void ResetSpeed()
    {
        forwardSpeed -= speedBoostAmount;
    }

    void RestartGame()
    {
        loseText.gameObject.SetActive(false);
        scoreText.text = "Score: " + score;
        forwardSpeed = 10f;
        gameStarted = false;
        Start();
    }
}
