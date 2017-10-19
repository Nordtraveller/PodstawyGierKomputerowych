using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int[] positionOnFloor;
    public int size;
    
    public void SetPositionOnFloor(int position)
    {
        positionOnFloor = new int[size];
        for(int i = 0; i < size; i++)
        {
            positionOnFloor[i] = position + i;
        }
    }
}
