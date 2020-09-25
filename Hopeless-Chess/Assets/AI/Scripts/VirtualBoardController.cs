using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualBoardController : MonoBehaviour
{
	public virtual void SwitchOnLhiteColliders() { }
	public virtual void SwitchOffLhiteColliders() { }
	public virtual void SwitchOnBlackColliders() { }
	public virtual void SwitchOffBlackColliders() { }

	public virtual void ShowFiguresMoves(CharacterController piece) { }
	public virtual void StopShowFiguresMoves(CharacterController piece) { }

	public virtual bool IsItShah(CharacterController piece, GameObject square, bool isLightTurn) { return false; }
	public virtual bool IsItMate(CharacterController piece, GameObject square, bool isLightTurn) { return false; }

	public virtual void MoveFigur(CharacterController figpieceur, GameObject cell) { }

	// Для AI
	public virtual string [] GiveAllFiguresMoves(bool isLightTurn) { return null; }
	public virtual float BoardScore(string move) { return 0; }
	public virtual void MoveFigurVirtual(string move) { }
	public virtual void UndoMoveFigurVirtual(string move) { }
}
