using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetrics
{
    public static float gravity = 10.0f;

    #region Map

    public static float tileHorizontalSize = 2f;
    public static float tileVerticalSize = 1f;
    public static int floorSize = 20;
    public static int numberOfTraps = floorSize / 3;
    public static float upperFloorY = 10f;
    public static float dropSpeed = 5f;
    public static float dropDuration = 1f;
    public static float timeForFloor = 30f;

    #endregion

    #region Player

    public static float playerSpeed = 10f;
    public static float playerJumpForce = 12f;
    public static float playerRaycastDistance = 2f;

    #endregion

}
