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



    public void SoundValue()
    {
        PPM.instance.SoundValue = soundSlider.value;
    }

    public void MusicValue()
    {
        PPM.instance.MusicValue = musicSlider.value;
    }

    public void AmbientValue()
    {
        PPM.instance.AmbientValue = ambientSlider.value;
    }




    public void MoralityDamage()
    {
        float.TryParse(damageText.text, out float result);
        PPM.instance.MoralityDamage = result;
    }

    public void MoralityHeal()
    {
        float.TryParse(regenText.text, out float result);
        PPM.instance.MoralityHeal = result;
    }

    public void MoralityPreset()
    {
        int.TryParse(moralityText.text, out int result);
        PPM.instance.MoralityPresetInt = result;
    }

    public void BoardArrangement()
    {
        int.TryParse(boardText.text, out int result);
        PPM.instance.BoardArrangement = result;
    }


    void SetGameSettings()
    {
        soundSlider.value = PPM.instance.SoundValue;
        musicSlider.value = PPM.instance.MusicValue;
        ambientSlider.value = PPM.instance.AmbientValue;
    }

    void SetBoardSettings()
	{
        damagePlaceholder.text = PPM.instance.MoralityDamage.ToString();
        regenPlaceholder.text = PPM.instance.MoralityHeal.ToString();
        moralityPlaceholder.text = PPM.instance.MoralityPreset.ToString();
        boardPlaceholder.text = PPM.instance.BoardArrangement.ToString();
    }

}
