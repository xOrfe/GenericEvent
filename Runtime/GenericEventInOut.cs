using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nomad.AnimaMundi.Utilities
{
    /// <summary>
    /// GenericEvents are useful for handling large numbers of event.
    /// </summary>
    /// <typeparam name="TIn"> Type of events input parameter.</typeparam>
    /// <typeparam name="TOut"> Type of events output parameter.</typeparam>
    [Serializable]
    public class GenericEventInOut<TIn, TOut> where TIn : EventArgs where TOut : EventArgs
    {
        [SerializeField] private string name;
        [SerializeField] private List<string> assignedMethods = new List<string>();
        
        public delegate TOut EventDelegate(TIn args);

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
        /// <param name="ieArgsInput">Input for invoke process.</param>
        /// <param name="ieArgsOutput">Output reference for invoke process.</param>
        /// <returns>Is invoke successful?</returns>
        public bool Invoke(TIn ieArgsInput, ref TOut ieArgsOutput)
        {
            if (MyEvent == null || MyEvent.GetInvocationList().Length <= 0) return false;
            ieArgsOutput = MyEvent.Invoke(ieArgsInput);
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