using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{

    private static EffectsController instance;
    protected EffectsController()
    {

    }

    public static EffectsController GetEffects()
    {
        if(instance == null)
        {
            instance = new EffectsController();
        }
        return instance;
    }

    public GameObject OvercomingEffect;
    public GameObject HeroismEffect;

    public void CreateEffect(CharacterController character,  float removeTime = 1f)
    {
        GameObject piece = character.gameObject;
        Debug.Log(piece.transform.position);
        GameObject effect = Instantiate(OvercomingEffect, piece.transform.position, Quaternion.identity, piece.transform);
        Destroy(effect, removeTime);
    }

}
