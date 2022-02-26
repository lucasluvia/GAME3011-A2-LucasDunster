using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillLevel
{
    BEGINNER,
    INTERMEDIATE,
    ADVANCED,
    EXPERT
}

public class PlayerSkillManager : MonoBehaviour
{
    public SkillLevel LockPickSkill;

    private GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameplayPanel").GetComponent<GameController>();
        SetUnlockRange();
    }

    public void SetUnlockRange()
    {
        switch (LockPickSkill)
        {
            case SkillLevel.BEGINNER:
                gameController.unlockRange = 2.5f;
                break;
            case SkillLevel.INTERMEDIATE:
                gameController.unlockRange = 4.0f;
                break;
            case SkillLevel.ADVANCED:
                gameController.unlockRange = 5.5f;
                break;
            case SkillLevel.EXPERT:
                gameController.unlockRange = 6.0f;
                break;
        }
    }
}
