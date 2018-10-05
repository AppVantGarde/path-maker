using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    public SharedInt sharedMinTileValue;
    public SharedInt sharedMaxTileValue;
    public SharedColor sharedBoardColor;
    public SharedColor sharedScoredTileColor;

    public Color boardColor;

    public Color scoredTileColor;

    public int minTileValue;
    public int maxTileValue;

    public void Awake( )
    {
        sharedMinTileValue.value = minTileValue;
        sharedMaxTileValue.value = maxTileValue;

        sharedBoardColor.value = boardColor;
        sharedScoredTileColor.value = scoredTileColor;
    }
}
