using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}


public class GameController : MonoBehaviour
{
    // Variables
    [SerializeField] Transform destinationTransform;
    [SerializeField] Transform lockTransform;
    [SerializeField] RectTransform targetTransform;
    [SerializeField] bool inUnlockRange;
    [SerializeField] int remainingPicks = 5;
    
    float rotationSpeed;

    public Difficulty difficulty;
    public float unlockRange = 5.0f;
    public bool RotationEnabled;
    public bool gameEnd;


    private Cursor cursor;
    private Vector3 TargetRotation;
    private float currentLockAngle = 0;
    private int locksRemaining;

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI locksRemainingText;
    [SerializeField] TextMeshProUGUI remainingPicksText;

    void Start()
    {
        cursor = GameObject.Find("Cursor").GetComponent<Cursor>();

        SetRotationSpeed();
        SetTotalLockCount();
        SetNewDestination();
        SetTargetPosition();

        UpdateTextUI();
    }

    void Update()
    {
        if (gameEnd) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCursor();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CheckUnlockRange())
            {
                if (locksRemaining > 1)
                    DecrementLocksRemaining();
                else
                {
                    EndGameState(true);
                }
            }
            else
            {
                if(remainingPicks > 1)
                    DecrementPicks();
                else
                {
                    EndGameState(false);
                }
            }
        }

        RotateLock();
    }

    private void RotateLock()
    {

        if (RotationEnabled)
        {
            currentLockAngle -= Time.deltaTime * rotationSpeed;
            if (currentLockAngle < -360)
                currentLockAngle = -360;
        }
        else if (currentLockAngle < 0)
            currentLockAngle += Time.deltaTime * rotationSpeed;
        else
            currentLockAngle = 0;

        lockTransform.rotation = Quaternion.Euler(new Vector3(0, 0, currentLockAngle));

    }

    private bool CheckUnlockRange()
    {
        if (currentLockAngle > TargetRotation.z - unlockRange && currentLockAngle < TargetRotation.z + unlockRange)
            return true;
        else
            return false;
    }

    private void SetTotalLockCount()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                locksRemaining = 1;
                break;
            case Difficulty.MEDIUM:
                locksRemaining = 3;
                break;
            case Difficulty.HARD:
                locksRemaining = 5;
                break;
        }
    }

    private void SetRotationSpeed()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                rotationSpeed = 25;
                break;
            case Difficulty.MEDIUM:
                rotationSpeed = 50;
                break;
            case Difficulty.HARD:
                rotationSpeed = 75;
                break;
        }
    }

    private void SetNewDestination()
    {
        TargetRotation = new Vector3(destinationTransform.rotation.x, destinationTransform.rotation.y, Random.Range(-10, -350));
        destinationTransform.rotation = Quaternion.Euler(TargetRotation);
    }

    private void DecrementLocksRemaining()
    {
        locksRemaining--;
        SetNewDestination();
        SetTargetPosition();
        ResetCursor();
        lockTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        currentLockAngle = 0;
        RotationEnabled = false;
        UpdateTextUI();
    }

    private void DecrementPicks()
    {
        remainingPicks--;
        UpdateTextUI();
    }

    private void ResetCursor()
    {
        cursor.isSelected = false;
        cursor.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    private void SetTargetPosition()
    {
        int xVal = Random.Range(-210, 210);
        int yVal = Random.Range(-210, 210);

        if ((xVal < 160 && xVal > -160) && (yVal < 160 && yVal > -160))
        {
            //both are too far into minimum
            int Decider = Random.Range(1, 4);
            if (Decider == 1)
                xVal = 160;
            if (Decider == 2)
                xVal = -160;
            if (Decider == 3)
                yVal = 160;
            if (Decider == 4)
                yVal = -160;
        }
        targetTransform.anchoredPosition = new Vector3(xVal, yVal, 0);
    }

    private void EndGameState(bool Result)
    {
        gameEnd = true;
        ResetCursor();

        if (Result)
        {
            locksRemaining = 0;
        }
        else
        {
            remainingPicks = 0;
        }

        UpdateTextUI();
    }

    private void UpdateTextUI()
    {
        locksRemainingText.text = locksRemaining.ToString();
        remainingPicksText.text = remainingPicks.ToString();
    }

    public void RestartMinigame()
    {
        SetRotationSpeed();
        SetTotalLockCount();
        SetNewDestination();
        SetTargetPosition();
        remainingPicks = 5;
        UpdateTextUI();
        gameEnd = false;
    }

    //public void AddToPointTracker(int pointsToAdd)
    //{
    //    pointTracker += pointsToAdd;
    //    pointsText.text = pointTracker.ToString();
    //}

}
