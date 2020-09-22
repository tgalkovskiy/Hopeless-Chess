using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualBoardController : MonoBehaviour
{
	public virtual void SwitchOnWhiteColliders() { }
	public virtual void SwitchOffWhiteColliders() { }
	public virtual void SwitchOnBlackColliders() { }
	public virtual void SwitchOffBlackColliders() { }

	public virtual void ShowFiguresMoves(CharacterController figur) { }
	public virtual void StopShowFiguresMoves(CharacterController figur) { }

	public virtual bool IsItShah(CharacterController figur, GameObject cell, bool isWhitesTurn) { return false; }
	public virtual bool IsItMate(CharacterController figur, GameObject cell, bool isWhitesTurn) { return false; }

	public virtual void MoveFigur(CharacterController figur, GameObject cell) { }
}
