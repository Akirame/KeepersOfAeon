using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{

    ParticleSystem ps;
    ParticleSystem.Particle[] m_Particles;
    int numParticlesAlive;
    float timer;
    public float stopTime = 0.2f;
    private bool groundTouched = false;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (!GetComponent<Transform>())
        {
            GetComponent<Transform>();
        }
    }
    void Update()
    {
        if (groundTouched)
        {
            print(1);
            m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
            numParticlesAlive = ps.GetParticles(m_Particles);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                m_Particles[i].velocity = new Vector2();
            }
            ps.SetParticles(m_Particles, numParticlesAlive);
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Ground" && !groundTouched)
        {
            groundTouched = true;
        }
    }

}
