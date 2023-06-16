using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Base presenter for getting and saving data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePresenter<T> where T : class
    {
        protected T m_view;

        /// <summary>
        /// Bind view to presenter so we can update the view from the data loaded from the presenter.
        /// </summary>
        /// <param name="view"></param>
        public void Bind(T view)
        {
            m_view = view;
        }
    }
}
