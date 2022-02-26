using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    private GameController gameController;
    private PlayerSkillManager skillManager;

    [SerializeField] Dropdown diffDropdown;
    [SerializeField] Dropdown skillDropdown;

    void Start()
    {
        gameController = GameObject.Find("GameplayPanel").GetComponent<GameController>();
        skillManager = GameObject.Find("SkillManager").GetComponent<PlayerSkillManager>();
        SetDifficulty();
        SetSkillLevel();
    }

    public void SetDifficulty()
    {
        string newDiff = diffDropdown.options[diffDropdown.value].text;

        if (newDiff == "EASY")
            gameController.difficulty = Difficulty.EASY;
        if (newDiff == "MEDIUM")
            gameController.difficulty = Difficulty.MEDIUM;
        if (newDiff == "HARD")
            gameController.difficulty = Difficulty.HARD;
    }

    public void SetSkillLevel()
    {
        string newDiff = skillDropdown.options[skillDropdown.value].text;

        if (newDiff == "BEGINNER")
            skillManager.LockPickSkill = SkillLevel.BEGINNER;
        if (newDiff == "INTERMEDIATE")
            skillManager.LockPickSkill = SkillLevel.INTERMEDIATE;
        if (newDiff == "ADVANCED")
            skillManager.LockPickSkill = SkillLevel.ADVANCED;
        if (newDiff == "EXPERT")
            skillManager.LockPickSkill = SkillLevel.EXPERT;
    }
}
