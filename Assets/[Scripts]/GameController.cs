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
    [SerializeField] Difficulty difficulty;
    [SerializeField] bool inUnlockRange;
    [SerializeField] float unlockRange = 5.0f;
    
    float rotationSpeed;

    public bool RotationEnabled;

    public int xVal;
    public int yVal;

    private Cursor cursor;
    private Vector3 TargetRotation;
    private float currentLockAngle = 0;
    private int locksRemaining;

    void Start()
    {
        cursor = GameObject.Find("Cursor").GetComponent<Cursor>();

        SetRotationSpeed();
        SetTotalLockCount();
        SetNewDestination();
        SetTargetPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckUnlockRange())
            {
                if (locksRemaining > 1)
                    DecrementLocksRemaining();
                else
                {
                    Debug.Log("Winner");
                }
            }
            else
            {
                Debug.Log("Not In Range");
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
                rotationSpeed = 15;
                break;
            case Difficulty.MEDIUM:
                rotationSpeed = 30;
                break;
            case Difficulty.HARD:
                rotationSpeed = 50;
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
    }

    private void ResetCursor()
    {
        cursor.isSelected = false;
        cursor.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    private void SetTargetPosition()
    {
        xVal = Random.Range(-210, 210);
        yVal = Random.Range(-210, 210);

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


    //public void AddToPointTracker(int pointsToAdd)
    //{
    //    pointTracker += pointsToAdd;
    //    pointsText.text = pointTracker.ToString();
    //}

}
