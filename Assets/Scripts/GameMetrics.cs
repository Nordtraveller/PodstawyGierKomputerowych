﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetrics
{
    #region Map

    public static float tileHorizontalSize = 2f;
    public static float tileVerticalSize = 1f;
    public static int floorSize = 20;
    public static int numberOfTraps = floorSize / 3;
    public static float upperFloorY = 10f;
    public static float dropSpeed = 5f;
    public static float dropDuration = 1f;

    #endregion

    #region Player

    public static float playerSpeed = 5f;
    public static float playerJumpForce = 8f;
    public static float playerRaycastDistance = 2f;

    #endregion

}
