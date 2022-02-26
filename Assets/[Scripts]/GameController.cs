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
    [SerializeField] Difficulty difficulty;
    [SerializeField] bool inUnlockRange;
    [SerializeField] float unlockRange = 5.0f;
    [SerializeField] float rotationSpeed = 15.0f;

    public bool RotationEnabled;

    private Vector3 TargetRotation;
    private float currentLockAngle = 0;
    private int locksRemaining;

    void Start()
    {
        //PlaceDestination();
        SetTotalLockCount();
        SetNewDestination();
        SetTargetPosition();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(CheckUnlockRange())
            {
                if(locksRemaining > 1)
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
    }

    private void SetTargetPosition()
    {

    }

    private void PlaceDestination()
    {
        //Easy = 90-179
        //Med  = 180-269
        //Hard = 270-350
        switch (difficulty)
        {
            case Difficulty.EASY:
                TargetRotation = new Vector3(destinationTransform.rotation.x, destinationTransform.rotation.y,Random.Range(-90, -179));
                break;
            case Difficulty.MEDIUM:
                TargetRotation = new Vector3(destinationTransform.rotation.x, destinationTransform.rotation.y, Random.Range(-180, -269));
                break;
            case Difficulty.HARD:
                TargetRotation = new Vector3(destinationTransform.rotation.x, destinationTransform.rotation.y, Random.Range(-270, -350));
                break;
        }
        destinationTransform.rotation = Quaternion.Euler(TargetRotation);
    }


    //public void AddToPointTracker(int pointsToAdd)
    //{
    //    pointTracker += pointsToAdd;
    //    pointsText.text = pointTracker.ToString();
    //}

}
