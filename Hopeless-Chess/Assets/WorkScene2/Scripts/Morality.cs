﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Morality : MonoBehaviour
{
    #region Singleton
    public static Morality instance;

    protected Morality()
    {

    }
    public static Morality GetInstance()
    {
        if(instance == null)
        {
            instance = new Morality();
        }
        return instance;
    }

    #endregion

    

    /// <summary>
    /// Добавляет мораль фигурам
    /// </summary>
    /// <param name="pieces"></param>
    /// <param name="moralityCount"></param>
    public void AddMorality(CharacterController[] pieces, float moralityCount)
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            pieces[i].moralityCount += moralityCount;
            pieces[i].gameObject.GetComponent<PieceView>().ShowChangeMorality(moralityCount);
            pieces[i].gameObject.GetComponent<PieceView>().ChangeMoralityBar();
        }
    }

    /// <summary>
    /// Добавляет мораль фигуре
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="moralityCount"></param>
    public void AddMorality(CharacterController piece, float moralityCount)
    {
            piece.moralityCount += moralityCount;
            piece.gameObject.GetComponent<PieceView>().ShowChangeMorality(moralityCount);
            piece.gameObject.GetComponent<PieceView>().ChangeMoralityBar();
    }

    /// <summary>
    /// Добавление морали при превращении фигуры
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="pieces"></param>
    public void OnTransformPiece(CharacterController piece, CharacterController[] pieces, float moralityCount)
    {
        piece.moralityCount += moralityCount;
        AddMorality(pieces, 10f);
        piece.gameObject.GetComponent<PieceView>().ShowChangeMorality(moralityCount);
    }



   /// <summary>
   /// Изменение текстуры хода при отсутствии морали
   /// </summary>
   /// <param name="piece"></param>
    public void Givingup(CharacterController piece)
    {
    }

    /// <summary>
    /// Переключение на обычную текстуру хода
    /// </summary>
    /// <param name="piece"></param>
    public void DisableGivingup(CharacterController piece)
    {
        //if(piece.oldMoveTexture != null && piece.moveTexture.name.Contains("givingup"))
        //{
            //piece.moveTexture = piece.oldMoveTexture;
            //piece.oldMoveTexture = null;
        //}
    }

    public void CheckMorality(CharacterController[] pieces, BoardController2 board)
    {

        for(int i = 0; i < pieces.Length; i++)
        {
            if(pieces[i].moralityCount <= 0 && pieces[i].movesToRemoveAffliction == -1)
            {
                if(board.IsMyQweenOrKingNear(pieces[i]))
                {
                    pieces[i].gameObject.AddComponent<HeroismEffect>();
                    EffectsController.GetEffects().CreateEffect(pieces[i], GameModule.instance.Effects.heroismEffect);
                    pieces[i].affliction = GameModule.instance.Afflictions.heroism;
                }
                else if(board.AlliesCount(pieces[i]) >= 3)
                {
                    pieces[i].gameObject.AddComponent<OvercomingEffect>();
                    EffectsController.GetEffects().CreateEffect(pieces[i], GameModule.instance.Effects.overcomingEffect, 7f);
                    pieces[i].affliction = GameModule.instance.Afflictions.overcoming;
                }
                else if(board.EmenyCount(pieces[i]) >= 3)
                {
                    pieces[i].gameObject.AddComponent<EscapeEffect>();
                    EffectsController.GetEffects().CreateEffect(pieces[i], GameModule.instance.Effects.escapeEffect, 0f, false);
                    pieces[i].affliction = GameModule.instance.Afflictions.escape;
                }
                else if(board.IsAlliesDieNear(pieces[i]))
                {
                    pieces[i].gameObject.AddComponent<RageEffect>();
                    EffectsController.GetEffects().CreateEffect(pieces[i], GameModule.instance.Effects.rageEffect, 0f, false);
                    pieces[i].affliction = GameModule.instance.Afflictions.rage;
                }
                else if(board.IsMyQweenDie(pieces[i]))
                {
                    pieces[i].gameObject.AddComponent<PanicEffect>();
                    EffectsController.GetEffects().CreateEffect(pieces[i], GameModule.instance.Effects.panicEffect, 0f, false);
                    pieces[i].affliction = GameModule.instance.Afflictions.panic;
                }
            }
            else if(pieces[i].movesToRemoveAffliction > 0)
            {
                pieces[i].movesToRemoveAffliction--;
            }
            if(pieces[i].movesToRemoveAffliction == 0)
            {
                if(pieces[i].currentMoveTexture != null)
                {
                    pieces[i].currentMoveTexture = pieces[i].moveTexture;
                }
                pieces[i].movesToRemoveAffliction = -1;
                pieces[i].moralityCount = pieces[i].character.MaxMorality / 2;
                pieces[i].affliction = null;
                Destroy(pieces[i].currentEffect, 2f);
                EffectsController.GetEffects().StartEndAnimation(pieces[i].currentEffect);
            }
        }

    }
}
