using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nomad.AnimaMundi.Utilities
{
    /// <summary>
    /// GenericEvents are useful for handling large numbers of event.
    /// </summary>
    [Serializable]
    public class GenericEvent
    {
        [SerializeField] private string name;
        [SerializeField] private List<string> assignedMethods = new List<string>();
        
        public delegate void EventDelegate();

        private event EventDelegate MyEvent;
        
        /// <summary>
        /// Assign Method to GenericEvent
        /// </summary>
        /// <param name="method">Method which we want to assign.</param>
        /// <returns></returns>
        public bool Assign(EventDelegate method)
        {
            assignedMethods.Add(method.Method.Name);
            MyEvent += method;
            return true;
        }

        /// <summary>
        /// Remove specified method from invocation list.
        /// </summary>
        /// <param name="method">Method which we want to delete from invocation list.</param>
        /// <returns>Is removing successful? </returns>
        public bool Remove(EventDelegate method)
        {
            if (MyEvent == null || MyEvent.GetInvocationList().Length <= 0) return false;
            assignedMethods.Remove(method.Method.Name);
            MyEvent -= method;
            return true;
        }


        /// <summary>
        /// Invoke event.
        /// </summary>
        /// <returns>Is invoke successful?</returns>
        public bool Invoke()
        {
            if (MyEvent == null || MyEvent.GetInvocationList().Length <= 0) return false;
            MyEvent.Invoke();
            return true;
        }


        /// <summary>
        /// Clear GenericEvent.
        /// </summary>
        public void Clear()
        {
            if (MyEvent == null || MyEvent.GetInvocationList().Length <= 0) return;
            foreach (var e in MyEvent.GetInvocationList())
            {
                MyEvent -= (EventDelegate)e;
            }
        }
    }
}