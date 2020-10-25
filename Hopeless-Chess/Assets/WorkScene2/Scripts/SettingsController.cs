using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SettingsController : MonoBehaviour
{
    public GameController gameController;

    private SaveSettings saveSettings = SaveSettings.GetInstance();
    public void Start() 
    {
        if (gameController!=null) InitializeSettings();
    }

    public void InitializeSettings()
    {
        gameController.moralityDamage = saveSettings.GetDamage();
        gameController.moralityRegen = saveSettings.GetRegeneration();
        gameController.moralityPreset = saveSettings.GetMoralityPreset();
    }
}

public class SaveSettings : MonoBehaviour
{
    private static SaveSettings instance;

    protected SaveSettings()
    {

    }

    public static SaveSettings GetInstance()
    {
        if(instance == null)
        {
            instance = new SaveSettings();
        }
        return instance;
    }

    public void SaveDamage(float value)
    {
        PlayerPrefs.SetFloat("MoralityDamage", value);
    }
    public float GetDamage()
    {
        return PlayerPrefs.GetFloat("MoralityDamage", 5);
    }

    public void SaveRegeneration(float value)
    {
        PlayerPrefs.SetFloat("MoralityRegen", value);
    }
    public float GetRegeneration()
    {
        return PlayerPrefs.GetFloat("MoralityRegen", 5);
    }
    public void SaveMoralityPreset(int value)
    {
        PlayerPrefs.SetInt("MoralityPreset", value);
    }

    public GameController.MoralityPreset GetMoralityPreset()
    {
        int value = PlayerPrefs.GetInt("MoralityPreset", 0);
        object presetObject = Enum.GetValues(typeof(GameController.MoralityPreset)).GetValue(value);
        GameController.MoralityPreset moralityPreset;
        Enum.TryParse(presetObject.ToString(), true, out moralityPreset);
        return moralityPreset;

        
    }
    public void SaveBoardArrangement(int value)
    {
        PlayerPrefs.SetInt("BoardArrangement", value);
    }
    public int GetBoardArrangement()
    {
        return PlayerPrefs.GetInt("BoardArrangement", 0);
    }

    /// <summary>
    /// Сохраняет общую громкость
    /// </summary>
    /// <param name="value"></param>
    public void SaveSounds(float value)
    {
        PlayerPrefs.SetFloat("SoundsValue", value);
    }

    public float GetSounds()
    {
        return PlayerPrefs.GetFloat("SoundsValue", 50);
    }

    /// <summary>
    /// Сохраняет громкость музыки
    /// </summary>
    /// <param name="value"></param>
    public void SaveMusic(float value)
    {
        PlayerPrefs.SetFloat("MusicValue", value);
    }
    public float GetMusic()
    {
        return PlayerPrefs.GetFloat("MusicValue", 100);
    }

    /// <summary>
    /// Сохраняет звуки окружения
    /// </summary>
    /// <param name="value"></param>
    public void SaveAmbient(float value)
    {
        PlayerPrefs.SetFloat("AmbientValue", value);
    }
    public float GetAmbient()
    {
        return PlayerPrefs.GetFloat("AmbientValue", 100);
    }

    
}