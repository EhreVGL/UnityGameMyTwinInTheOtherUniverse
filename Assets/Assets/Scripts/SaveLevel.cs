using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SaveLevel
{
    private static SaveLevel instance;
    public static SaveLevel singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveLevel();
                PlayerPrefs.GetInt("Level", 1);
                PlayerPrefs.GetInt("Repeat", 0);
                PlayerPrefs.GetInt("ResetLevel", 0);

            }
            return instance;
        }
    }
    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level", 1);
    }
    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
    }
    public int GetRepeat()
    {
        return PlayerPrefs.GetInt("Repeat", 1);
    }
    public void LevelUp()
    {
        PlayerPrefs.SetInt("Level", GetLevel() + 1);
        if(PlayerPrefs.GetInt("Level", 1) == 6)
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Repeat", GetRepeat() + 1);

        }
    }
    public int GetResetLevel()
    {
        return PlayerPrefs.GetInt("ResetLevel", 1);
    }
    public void ResetResetLevel()
    {
        PlayerPrefs.SetInt("ResetLevel", 0);
    }
    public void ResetLevel()
    {
        PlayerPrefs.SetInt("ResetLevel", GetResetLevel() + 1);
    }
    public void SetResetLevel()
    {
        PlayerPrefs.SetInt("ResetLevel", GetResetLevel() - 1);
    }
}
