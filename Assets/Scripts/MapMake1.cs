using UnityEngine;
using System.Collections;

public class MapMake1 : MonoBehaviour
{
    public GameObject spawnGrass, spawnSand, spawnWater, spawnTree1, spawnTree2, spawnHill, spawnBuilding, spawnBuilding2;

    string[] terrain = {
     "          . . . . . . . . . W W",
     "         . . . . . . . . . . W W",
     "        . . . . . . . . . . . W W",
     "       b B . . . . . . b . W W W W",
     "      . . . . t T t T . . W t T W .",
     "     . d . . t T T T B . W T . t W .",
     "    d d d d H H t t W W B b . . . W .",
     "   d d d d d d H H W W . . . . . W b .",
     "  d d d d d H W W . . . . . . . W B . .",
     " d d d d d d d H H . . B b . . . W . . .",
     "  d d d d d H W W . . . b . . . W . . .",
     "   d d d d d d . . . . . . . . . W . .",
     "    d d d d d . . . . . . . . . W . .",
     "     d d d d d . . . . . . . . . W .",
     "      d d d d . . . . . . . . . W .",
     "       d d d d . . . . . . . . . W",
     "        d d d d d . . . . . . . W",
     "         d d d d . . . . . . . W",
     "          d d d . . . . . . . W" };

    void mapMake1()
    {
        {
            GameObject tile;
            GameObject typeObj;
            float zpos = 9.0f;

            foreach (string row in terrain)
            {
                float xpos = -20.0f;
                foreach (char tileChar in row)
                {
                    if (tileChar != ' ')
                    {
                        switch (tileChar)
                        {
                            case 'W':
                                typeObj = spawnWater;
                                break;
                            case 'd':
                                typeObj = spawnSand;
                                break;
                            case 'H':
                                typeObj = spawnHill;
                                break;
                            case 'B':
                                typeObj = spawnBuilding;
                                break;
                            case 'T':
                                typeObj = spawnTree2;
                                break;
                            case 't':
                                typeObj = spawnTree1;
                                break;
                            case 'b':
                                typeObj = spawnBuilding2;
                                break;
                            case '.':
                                typeObj = spawnGrass;
                                break;
                            default:
                                typeObj = null;
                                break;
                        }

                        if (typeObj)
                        {
                            tile = Instantiate(typeObj,
                                                new Vector3(xpos, 0.0f, zpos),
                                                typeObj.transform.rotation) as GameObject;
                            // var SphereCollider = tile.AddComponent<SphereCollider>();
                            tile.gameObject.tag = "HexTile";
                            tile.name = typeObj.name + "_" + xpos + "_" + zpos;
                            tile.transform.SetParent(this.transform);
                        }
                    }
                    xpos += 0.866f;
                }
                zpos -= 1.5f;
            }
        }
    }
}
