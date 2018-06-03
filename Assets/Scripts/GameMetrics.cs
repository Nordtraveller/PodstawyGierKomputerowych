using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetrics
{
    #region Map

    public static float tileSize = 2f;
    public static int floorSize = 20;
    public static int numberofTrappedTiles = floorSize/2;
    public static float upperFloorY = 11f;
    public static float dropSpeed = 5f;
    public static float dropDuration = 1f;

    #endregion

    #region Player

    public static float playerSpeed = 8f;
    public static float playerJumpForce = 13f;
    public static float playerRaycastDistance = 2f;
    public static int floorsUnlocked = 2;
    public static float gravity = 10.0f;

    #endregion

    #region Traps

    public static float fallingTrapXOffset = 0.15f * upperFloorY;

    #endregion

    #region High Score

    public static List<int> maxLevelsPassedCountList = new List<int>(new int[5]);
    public static int DeathsCount = 0;

    #endregion

    #region Menu

    public static bool isTimer = true;
    public static bool isAbilities = true;
    public static bool isMiniMap = true;
    public static bool isLvlCount = true;
    public static bool isMute = false;

    #endregion


}
