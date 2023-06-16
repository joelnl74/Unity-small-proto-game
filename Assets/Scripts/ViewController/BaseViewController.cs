using UnityEngine;

namespace Game
{
    /// <summary>
    /// Base view controller that holds a reference to the view.
    /// </summary>
    /// <typeparam name="T">View</typeparam>
    public class BaseViewController<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        [SerializeField] protected T m_view;
    }
}
