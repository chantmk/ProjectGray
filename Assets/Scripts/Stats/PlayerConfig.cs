using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConfig
{
    public static int DamageMultiplier = 1;
    public static int HealthPackCount = 0;
    public static List<int> HaveWeapon = new List<int>{(int)WeaponIDEnum.Cheap};
    public static bool IsWeaponBlueSpecial;
    public static bool IsWeaponBlackSpecial;
}
