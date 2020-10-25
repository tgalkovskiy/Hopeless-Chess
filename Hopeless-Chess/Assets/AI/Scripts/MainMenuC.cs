using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuC : MonoBehaviour
{

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject gameSettings;

    [SerializeField]
    GameObject boardSettings;

    [Space]

    [SerializeField]
    Text damageText;
    [SerializeField]
    Text regenText;
    [SerializeField]
    Text moralityText;
    [SerializeField]
    Text boardText;

    [Space]

    [SerializeField]
    Text damagePlaceholder;
    [SerializeField]
    Text regenPlaceholder;
    [SerializeField]
    Text moralityPlaceholder;
    [SerializeField]
    Text boardPlaceholder;

    [Space]
    [SerializeField]
    Slider soundSlider;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider ambientSlider;



    public void GameStart ()
	{
        SceneManager.LoadScene(1);
	}

    public void OpenGameSettings()
	{
        mainMenu.SetActive(false);
        gameSettings.SetActive(true);
        SetGameSettings();
    }

    public void OpenBoardSettings()
    {
        mainMenu.SetActive(false);
        boardSettings.SetActive(true);
        SetBoardSettings();
    }
    public void OpenMainMenu()
    {
        gameSettings.SetActive(false);
        boardSettings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }



    public void SaveSounds()
    {
        SaveSettings.GetInstance().SaveSounds(soundSlider.value);
    }

    public void SaveMusic()
    {
        SaveSettings.GetInstance().SaveMusic(musicSlider.value);
    }

    public void SaveAmbient()
    {
        SaveSettings.GetInstance().SaveAmbient(ambientSlider.value);
    }




    public void SaveDamage()
    {
        float.TryParse(damageText.text, out float result);
        SaveSettings.GetInstance().SaveDamage(result);
    }

    public void SaveRegeneration()
    {
        float.TryParse(regenText.text, out float result);
        SaveSettings.GetInstance().SaveRegeneration(result);
    }

    public void SaveMoralityPreset()
    {
        int.TryParse(moralityText.text, out int result);
        SaveSettings.GetInstance().SaveMoralityPreset(result);
    }

    public void SaveBoardArrangement()
    {
        int.TryParse(boardText.text, out int result);
        SaveSettings.GetInstance().SaveBoardArrangement(result);
    }


    void SetGameSettings()
    {
        if (SaveSettings.GetInstance().GetSounds() == 0) SaveSettings.GetInstance().SaveSounds(1);
        if (SaveSettings.GetInstance().GetMusic() == 0) SaveSettings.GetInstance().SaveMusic(1);
        if (SaveSettings.GetInstance().GetAmbient() == 0) SaveSettings.GetInstance().SaveAmbient(1);

        soundSlider.value = SaveSettings.GetInstance().GetSounds();
        musicSlider.value = SaveSettings.GetInstance().GetMusic();
        ambientSlider.value = SaveSettings.GetInstance().GetAmbient();
    }

    void SetBoardSettings()
	{
        if (SaveSettings.GetInstance().GetDamage() == 0) SaveSettings.GetInstance().SaveDamage(15);
        if (SaveSettings.GetInstance().GetRegeneration() == 0) SaveSettings.GetInstance().SaveRegeneration(5);
        if (PlayerPrefs.GetInt("MoralityPreset", 0) == 0) SaveSettings.GetInstance().SaveMoralityPreset(2);
        if (SaveSettings.GetInstance().GetBoardArrangement() == 0) SaveSettings.GetInstance().SaveBoardArrangement(0);

        damagePlaceholder.text = SaveSettings.GetInstance().GetDamage().ToString();
        regenPlaceholder.text = SaveSettings.GetInstance().GetRegeneration().ToString();
        moralityPlaceholder.text = PlayerPrefs.GetInt("MoralityPreset", 0).ToString();
        boardPlaceholder.text = SaveSettings.GetInstance().GetBoardArrangement().ToString();
    }

}
