using UnityEngine;


interface IBuff
{
    //bool isPermanentBuff { get; }
    //float maxBuffDuration { get; }
    //float buffDuration { get; }
    //bool isApplied { get; }


    void End();
    void Apply(CharacterStats characterStats);
}
