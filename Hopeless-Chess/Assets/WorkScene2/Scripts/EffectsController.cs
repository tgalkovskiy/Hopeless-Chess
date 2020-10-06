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

    

    public GameObject overcomingEffect;
    public GameObject heroismEffect;
    public GameObject rageEffect;


    public void CreateEffect(CharacterController character, GameObject effectPrefab, float removeTime = 1f, bool isRemovingByTime = true)
    {
        GameObject piece = character.gameObject;
        GameObject effect = Instantiate(effectPrefab, piece.transform.position, Quaternion.identity, piece.transform);
        effect.transform.rotation = Quaternion.Euler(-90, 0, 0);
        character.currentEffect = effect;
        if(isRemovingByTime)
        {
            Destroy(effect, removeTime);
        }
    }

}
