using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualBoardController : MonoBehaviour
{
	public virtual void SwitchOnLhiteColliders() { }
	public virtual void SwitchOffLhiteColliders() { }
	public virtual void SwitchOnBlackColliders() { }
	public virtual void SwitchOffBlackColliders() { }

	public virtual void ShowPieceMoves(CharacterController piece) { }
	public virtual void StopShowPieceMoves() { }

	public virtual bool IsItCheck(CharacterController piece, GameObject square, bool isLightTurn) { return false; }
	public virtual bool IsItMate(CharacterController piece, GameObject square, bool isLightTurn) { return false; }

	public virtual void MovePiece(CharacterController piece, GameObject cell) { }

	// Для AI
	public virtual string [] GiveAllPieceMoves(bool isLightTurn) { return null; }
	public virtual float BoardScore(string move) { return 0; }
	public virtual void MoveFigurVirtual(string move) { }
	public virtual void UndoMoveFigurVirtual(string move) { }
}
