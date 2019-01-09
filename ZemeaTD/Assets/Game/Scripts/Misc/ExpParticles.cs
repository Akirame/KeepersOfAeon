﻿using System.Collections;
using UnityEngine;

public class ExpParticles : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.Particle[] m_Particles;
    public Transform target;
    public float speed = 5f;
    int numParticlesAlive;
    float timer;
    public float chaseTime = 1;
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
        timer += Time.deltaTime;
        if (timer >= chaseTime)
        {
            m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
            numParticlesAlive = ps.GetParticles(m_Particles);
            float step = speed * Time.deltaTime;
            for (int i = 0; i < numParticlesAlive; i++)
            {
                m_Particles[i].position = Vector3.LerpUnclamped(m_Particles[i].position, target.position, step);
            }
            ps.SetParticles(m_Particles, numParticlesAlive);
        }

    }
}
