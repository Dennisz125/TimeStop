using UnityEngine;
using System.Collections;

public class UnitType
{
    string name;
    string type;
    int healthPoints;
    int damageToEnemy;

    /*
    int XPostion;
    int YPostion;
    */

    UnitType(string name, string type, int startingHealthPoints, int defaultDamageToEnemy)
    {
        this.name = name;
        this.type = type;
        healthPoints = startingHealthPoints;
        damageToEnemy = defaultDamageToEnemy;

    }

}
