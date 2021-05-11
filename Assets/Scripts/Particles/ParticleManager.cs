using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public Dictionary<ParticleEnum, GameObject> ParticleDict;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start particle manager");
        ParticleDict = new Dictionary<ParticleEnum, GameObject>();
        //Add particle prefab
        ParticleDict[ParticleEnum.HealingParticle] = Resources.Load("Particle/HealingParticle") as GameObject;
        ParticleDict[ParticleEnum.BlackBulletParticle] = Resources.Load("Particle/BlackBulletParticle") as GameObject;
        ParticleDict[ParticleEnum.BlueBulletSplashParticle] = Resources.Load("Particle/BlueBulletSplashParticle") as GameObject;
        


        EventPublisher.ParticleSpawn += particleSpawn;
    }

    private void particleSpawn(ParticleEnum particleEnum, Vector2 position)
    {
        //Spawn particle
        Debug.Log("Spawn particle");
        ParticleSystem ps = Instantiate(ParticleDict[particleEnum], position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(ps.gameObject, ps.startLifetime);
    }
}
