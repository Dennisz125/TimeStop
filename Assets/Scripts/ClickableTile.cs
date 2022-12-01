using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour
{

    public int tileX;
    public int tileY;
    public Type type;

    void OnMouseUp()
    {
        Debug.Log("Click!");

        //map.GeneratePathTo(tileX, tileY);
    }

    public void setTilePositionAndType(int x, int y, Type type)
    {
        this.tileX = x;
        this.tileY = y;
        this.type = type;
    }
    public Vector2Int getTileXYPosition()
    {
        return new Vector2Int(this.tileX, this.tileY);
    }
}
