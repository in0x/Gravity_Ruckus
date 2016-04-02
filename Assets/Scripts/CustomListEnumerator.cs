using UnityEngine;
using System.Collections.Generic;

// This is a custom iterator that has the capability of going back. 
// It also wraps back to 0 after going over the end of the list.
// If possible still always IEnumerator<T>, only use this one 
// if a circular traversal make sense such as with
// the players current weapons.
public class CustomListEnumerator<T>
{
    List<T> m_list;
    int m_Index = 0;

    public T Current
    {
        get
        {
            return m_list[m_Index];
        }    
        private set { }
    }

    public CustomListEnumerator(List<T> list)
    {
        m_list = list;
    }

    public void MoveNext()
    {
        m_Index++;

        if (m_Index == m_list.Count)
        {
            m_Index = 0;
        }
    }

    public void MoveBack()
    {
        m_Index--;

        if (m_Index == -1)
        {
            m_Index = m_list.Count - 1;
        }
    }
}
