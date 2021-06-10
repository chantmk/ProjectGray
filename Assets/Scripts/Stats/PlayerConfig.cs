using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConfig
{
    public static int DamageMultiplier = 1;
    public static int HealthPackCount = 0;
    public static int ResemblanceCount = 0;
    public static int healValue = 5;
    public static List<int> HaveWeapon = new List<int>{(int)WeaponIDEnum.Cheap, (int)WeaponIDEnum.Blue};
    public static bool IsWeaponBlueSpecial;
    public static bool IsWeaponBlackSpecial;

    public static SceneEnum CurrentScene = SceneEnum.MainMenuScene;
}
