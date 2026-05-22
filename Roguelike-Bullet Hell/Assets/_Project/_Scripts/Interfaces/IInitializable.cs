using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IInitializable
    {
        bool IsInitialized { get; }

        /// <summary>
        /// Sets up required data, references, or state before the object can be used.
        /// </summary>
        void Initialize();
    }
}