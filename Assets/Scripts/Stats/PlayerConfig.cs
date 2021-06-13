using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public static class PlayerConfig
{
    public static int DamageMultiplier = 1;
    public static int HealthPackCount = 0;
    public static int ResemblanceCount = 0;
    public static int healValue = 5;
    public static List<WeaponIDEnum> HaveWeapon = new List<WeaponIDEnum>
    {
        WeaponIDEnum.Cheap
        
    };
    // public static List<WeaponIDEnum> HaveWeapon = new List<WeaponIDEnum>
    // {
    //     WeaponIDEnum.Cheap, WeaponIDEnum.Black, WeaponIDEnum.Blue
    //     
    // };
    
    
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

    public static Dictionary<ColorEnum, bool> HaveResemblanceDict = new Dictionary<ColorEnum, bool>
    {
        {ColorEnum.Black, false},
        {ColorEnum.Blue, false}
    };
    
    // public static Dictionary<ColorEnum, bool> HaveResemblanceDict = new Dictionary<ColorEnum, bool>
    // {
    //     {ColorEnum.Black, true},
    //     {ColorEnum.Blue, true}
    // };

    public static SceneEnum CurrentScene = SceneEnum.MainMenuScene;

    public static void HardCode()
    {
        DamageMultiplier = 1;
        HealthPackCount = 0;
        ResemblanceCount = 0;
        healValue = 5;
        HaveWeapon = new List<WeaponIDEnum> 
        {
            WeaponIDEnum.Cheap
            
        };
        IsWeaponBlackSpecial = false;
        IsWeaponBlueSpecial = false;
        HaveResemblanceDict = new Dictionary<ColorEnum, bool>
        {
            {ColorEnum.Black, false},
            {ColorEnum.Blue, false}
        };

        CurrentScene = SceneEnum.MainMenuScene;
    }
}
