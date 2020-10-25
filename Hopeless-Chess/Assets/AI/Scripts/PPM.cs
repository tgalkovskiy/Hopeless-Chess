using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Prefabs Manager
/// </summary>
public class PPM : MonoBehaviour
{
    public static PPM instance;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;
    }





    public float SoundValue
    {
        get { return PlayerPrefs.GetFloat("SoundsValue", 100); }
        set { PlayerPrefs.SetFloat("SoundsValue", value); }
    }

    public float MusicValue
    {
        get { return PlayerPrefs.GetFloat("MusicValue", 100); }
        set { PlayerPrefs.SetFloat("MusicValue", value); }
    }

    public float AmbientValue
    {
        get { return PlayerPrefs.GetFloat("AmbientValue", 100); }
        set { PlayerPrefs.SetFloat("AmbientValue", value); }
    }





    public float MoralityDamage
    {
        get { return PlayerPrefs.GetFloat("MoralityDamage", 15); }
        set { PlayerPrefs.SetFloat("MoralityDamage", value); }
    }

    public float MoralityHeal
    {
        get { return PlayerPrefs.GetFloat("MoralityHeal", 10); }
        set { PlayerPrefs.SetFloat("MoralityHeal", value); }
    }

    public int MoralityPresetInt
    {
        get { return PlayerPrefs.GetInt("MoralityPreset", 1); }
        set { PlayerPrefs.SetInt("MoralityPreset", value); Debug.Log(value); }
    }

    public GameController.MoralityPreset MoralityPreset
    {
        get 
        {
            int value = PlayerPrefs.GetInt("MoralityPreset");
            Debug.Log("value= " + value);
            object presetObject = Enum.GetValues(typeof(GameController.MoralityPreset)).GetValue(value);
           // Debug.Log("presetObject= " + presetObject);
            GameController.MoralityPreset moralityPreset;
            Enum.TryParse(presetObject.ToString(), true, out moralityPreset);
            // Debug.Log("moralityPreset= " + moralityPreset);
            return moralityPreset;
        }
    }

    public int BoardArrangement
    {
        get { return PlayerPrefs.GetInt("BoardArrangement", 5); }
        set { PlayerPrefs.SetInt("BoardArrangement", value); }
    }

}
