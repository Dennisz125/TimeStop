using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class VictoryConditions : MonoBehaviour
{
    // team unit counters for victory conditions
    private int team1UnitCount = Int32.MaxValue;
    private int team2UnitCount = Int32.MaxValue;

    public (bool, int) checkIfTeamWon()
    {
        detectTeamUnits();
        if (team1UnitCount == 0)
        {
            return (true, 2);
        }
        else if (team2UnitCount == 0)
        {
            return (true, 1);
        }
        return (false, 0);
    }
    
    private void detectTeamUnits()
    {
        pawn[] allPawns = Object.FindObjectsOfType<pawn>();

        int tempTeam1Count = 0;
        int tempTeam2Count = 0;
        for (int i = 0; i < allPawns.Length; i++)
        {
            int currentTeam = allPawns[i].getTeamOwner();
            if (currentTeam == 1)
            {
                tempTeam1Count++;
            }
            else if(currentTeam == 2)
            {
                tempTeam2Count++;
            }
        }

        team1UnitCount = tempTeam1Count;
        team2UnitCount = tempTeam2Count;
    }
}
