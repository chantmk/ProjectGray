using UnityEngine;


interface IBuff
{
    bool isPermanentBuff { get; }
    float buffTimeOut { get; }

    void Apply();
}
