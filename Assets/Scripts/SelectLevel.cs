using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    public TextMeshProUGUI selectedLevel;
    public LevelButtons[] levelButtons;

    private static int selected;
    public static string levelName;

    public static int levelReached;

    void Start()
    {
        levelReached = PlayerPrefs.GetInt("levelReached", 0);
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= levelReached)
            {
                levelButtons[i].isLocked = false;
            }
            
            if (levelButtons[i].isLocked)
                levelButtons[i].levelText.color = Color.red;
        }
        
        selected = 0;
        levelName = levelButtons[selected].levelName;
        MoveDown();
    }
    
    public void Select(int level)
    {
        if (levelButtons[level].isLocked)
            return;
        
        MoveUp();

        selected = level;
        levelName = levelButtons[selected].levelName;
        
        MoveDown();
        
        selectedLevel.SetText("Level " + (level + 1) + "?");
    }

    void MoveDown()
    {
        levelButtons[selected].levelButton.transform.Translate(0, -0.2f, 0);
        levelButtons[selected].levelText.transform.Translate(0, -0.2f, 0);
    }

    void MoveUp()
    {
        levelButtons[selected].levelButton.transform.Translate(0, 0.2f, 0);
        levelButtons[selected].levelText.transform.Translate(0, 0.2f, 0);
    }
}
