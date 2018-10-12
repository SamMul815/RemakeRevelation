using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField]
    private List<GameObject> _particlesList;

    private Dictionary<string, PoolObject> _particles = new Dictionary<string, PoolObject>();

	// Use this for initialization
	void Awake ()
    {
        for (int i = 0; i < _particlesList.Count; i++)
        {
            ParticleObject particle = _particlesList[i].GetComponent<ParticleObject>();
            
            if (particle != null)
            {
                _particles.Add(particle.pooltag, particle);
                particle.Awake();
            }

        }
    }

    public void PoolParticleEffectOn(string ParticleTag)
    {
        if (_particles.ContainsKey(ParticleTag))
        {
            GameObject Particle;
            PoolManager.Instance.PopObject(_particles[ParticleTag].gameObject, out Particle);
            if (Particle == null)
            {
                Debug.LogWarning("Not Found any particles in the object.");
            }
        }
    }

    public void PoolParticleEffectOn(string ParticleTag, Transform Parent)
    {
        if (_particles.ContainsKey(ParticleTag))
        {
            GameObject Particle;
            PoolManager.Instance.PopObject(_particles[ParticleTag].gameObject, out Particle);

            if (Particle != null)
            {
                Particle.transform.parent = Parent;
                Particle.transform.localRotation = Quaternion.identity;
                Particle.transform.localPosition = Vector3.zero;
                return;
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    public void PoolParticleEffectOn(string ParticleTag, Vector3 CreatePos, Vector3 CreateDir)
    {
        if (_particles.ContainsKey(ParticleTag))
        {
            GameObject Particle;

            Quaternion rot = Quaternion.LookRotation(CreateDir.normalized);
            PoolManager.Instance.PopObject(_particles[ParticleTag].gameObject, CreatePos, rot, out Particle);

            if(Particle != null)
            {
                Debug.LogWarning("Not Found any particles in the object.");
            }
        }

    }

    public void PoolParticleEffectOff(string ParticleTag)
    {

        if (_particles.ContainsKey(ParticleTag))
        {
            PoolManager.Instance.PushObject(_particles[ParticleTag].gameObject);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    public void PoolParticleEffectOn(PoolObject obj)
    {
        if (_particles.ContainsKey(obj.pooltag))
        {
            GameObject Particle;
            PoolManager.Instance.PopObject(obj, out Particle);

            if(Particle == null)
            {
                Debug.LogWarning("Not Found any particles in the object.");
                return;
            }
        }
    }

    public void PoolParticleEffectOn(PoolObject obj, Transform parent)
    {

        if (_particles.ContainsKey(obj.pooltag))
        {
            GameObject Particle;
            PoolManager.Instance.PopObject(obj, out Particle);

            if (Particle != null)
            {
                Particle.transform.parent = parent;
                Particle.transform.localRotation = Quaternion.identity;
                Particle.transform.localPosition = Vector3.zero;
                return;
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    public void PoolParticleEffectOn(PoolObject obj, Vector3 CreatePos, Vector3 CreateDir)
    {
        if(_particles.ContainsKey(obj.pooltag))
        {
            GameObject Particle;

            Quaternion rot = Quaternion.LookRotation(CreateDir.normalized);
            PoolManager.Instance.PopObject(_particles[obj.pooltag].gameObject, CreatePos, rot, out Particle);

            if (Particle == null)
            {
                Debug.LogWarning("Not Found any particles in the object.");
                return;
            }
        }
    }

    public void PoolParticleEffectOff(PoolObject obj)
    {
        if (_particles.ContainsKey(obj.pooltag))
        {
            PoolManager.Instance.PushObject(obj.gameObject);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    public void PoolParticleEffectOn(GameObject obj)
    {
        GameObject Particle;
        PoolManager.Instance.PopObject(obj, out Particle);

        if (Particle == null)
        {
            Debug.LogWarning("Not Found any particles in the object.");
            return;
        }
    }

    public void PoolParticleEffectOn(GameObject obj, Transform parent)
    {

        PoolObject ParticleObj = obj.GetComponent<ParticleObject>();

        if (ParticleObj != null)
        {
            if(_particles.ContainsValue(ParticleObj))
            {
                GameObject Particle;
                PoolManager.Instance.PopObject(obj, out Particle);

                if (Particle != null)
                {
                    Particle.transform.rotation = parent.rotation;
                    Particle.transform.parent = parent;
                    Particle.transform.position = Vector3.zero;
                    return;
                }
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");

    }

    public void PoolParticleEffectOn(GameObject obj, Vector3 CreatePos, Vector3 CreateDir)
    {

        PoolObject ParticleObj = obj.GetComponent<ParticleObject>();

        if (ParticleObj != null)
        {
            if (_particles.ContainsValue(ParticleObj))
            {
                GameObject Particle;
                Quaternion rot = Quaternion.LookRotation(CreateDir.normalized);
                PoolManager.Instance.PopObject(obj, CreatePos, rot, out Particle);

                if (Particle == null)
                {
                    Debug.LogWarning("Not Found any particles in the object.");
                    return;
                }
            }
        }
    }

    public void PoolParticleEffectOff(GameObject obj)
    {
        GameObject Particle;
        PoolManager.Instance.PopObject(obj, out Particle);

        if(Particle != null)
        {
            PoolManager.Instance.PushObject(obj);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");

    }

}
