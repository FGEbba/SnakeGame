using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public int minX = -19;
    public int maxX = 19;

    public int minY = -10;
    public int maxY = 10;


    public Vector3 CheckBounds(Vector3 oldPos, Vector3 newPos)
    {
        if (newPos.x > maxX)
        {
            newPos = new Vector3(minX, oldPos.y, oldPos.z);
        }
        else if (newPos.x < minX)
        {
            newPos = new Vector3(maxX, oldPos.y, oldPos.z);
        }

        if (newPos.y > maxY)
        {
            newPos = new Vector3(oldPos.x, minY, oldPos.z);
        }
        else if (newPos.y < minY)
        {
            newPos = new Vector3(oldPos.x, maxY, oldPos.z);
        }

        return newPos;
    }
}
