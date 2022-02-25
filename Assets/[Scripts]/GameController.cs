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
    [SerializeField] Difficulty difficulty;

    void Start()
    {
        PlaceDestination();
    }

    void Update()
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
                destinationTransform.rotation = Quaternion.Euler(new Vector3(destinationTransform.rotation.x, destinationTransform.rotation.y,Random.Range(-90, -179)));
                break;
            case Difficulty.MEDIUM:
                destinationTransform.rotation = Quaternion.Euler(new Vector3(destinationTransform.rotation.x, destinationTransform.rotation.y, Random.Range(-180, -269)));
                break;
            case Difficulty.HARD:
                destinationTransform.rotation = Quaternion.Euler(new Vector3(destinationTransform.rotation.x, destinationTransform.rotation.y, Random.Range(-270, -350)));
                break;
        }
    }


    //public void AddToPointTracker(int pointsToAdd)
    //{
    //    pointTracker += pointsToAdd;
    //    pointsText.text = pointTracker.ToString();
    //}

}
