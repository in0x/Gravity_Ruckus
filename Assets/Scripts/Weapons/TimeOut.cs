using System;
using UnityEngine;

public class TimeOut
{
    float m_timeLimit;
    float m_timePassed;
    
    public TimeOut(float timeLimitMS)
    {
        m_timeLimit = timeLimitMS;
    }

    public bool Update()
    {
        m_timePassed += Time.deltaTime;

        if (m_timePassed >= m_timeLimit)
        {
            m_timePassed = 0;
            return true;
        }

        return false;
    }
}
