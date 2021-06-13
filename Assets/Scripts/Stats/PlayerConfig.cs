using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConfig
{
    public static int DamageMultiplier = 1;
    public static int HealthPackCount = 0;
    public static int ResemblanceCount = 0;
    public static int healValue = 5;
    public static List<WeaponIDEnum> HaveWeapon = new List<WeaponIDEnum>
    {
        WeaponIDEnum.Cheap, WeaponIDEnum.Black, WeaponIDEnum.Blue
        
    };
    
    public static List<WeaponIDEnum> HaveWeaponBlackPreset = new List<WeaponIDEnum>
    {
        WeaponIDEnum.Cheap
        
    };
    
    public static List<WeaponIDEnum> HaveWeaponBluePreset = new List<WeaponIDEnum>
    {
        WeaponIDEnum.Cheap, WeaponIDEnum.Black
        
    };
        
    public static List<WeaponIDEnum> HaveWeaponYellowPreset = new List<WeaponIDEnum>
    {
        WeaponIDEnum.Cheap, WeaponIDEnum.Black, WeaponIDEnum.Blue
        
    };
        
    public static bool IsWeaponBlueSpecial;
    public static bool IsWeaponBlackSpecial;

    public static bool HaveBlackResemblance;
    public static bool HaveBlueResemblance;

    public static SceneEnum CurrentScene = SceneEnum.MainMenuScene;
}
