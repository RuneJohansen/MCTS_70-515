using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class RoundInformation
{
    public int RoundNumber;
    public int Weapon;
    public int Shield;
    public bool WasHit;

    public RoundInformation(int roundNumber, int weapon, int shield, bool wasHit)
    {
        this.RoundNumber = roundNumber;
        this.Weapon = weapon;
        this.Shield = shield;
        this.WasHit = wasHit;
    }
}