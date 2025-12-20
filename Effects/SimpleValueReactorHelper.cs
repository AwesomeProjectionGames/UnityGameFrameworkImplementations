using System.Collections.Generic;
using GameFramework.Effects;
using UnityEngine;
using System.Linq;

namespace GameFramework.Spectating
{
    /// <summary>
    /// Helper class for working with simple value reactors.
    /// </summary>
    public class SimpleValueReactorHelper
    {
        /// <summary>
        /// Gets all reactors of type T in the children of the specified parent GameObject that match the given reactor channel.
        /// </summary>
        public static IEnumerable<ISimpleValueReactor<T>> GetReactorsInChildren<T>(GameObject parent,string reactorChannel)
        {
            return parent.GetComponentsInChildren<ISimpleValueReactor<T>>()
                .Where(reactor => reactor.ReactorChannel == reactorChannel);
        }
    }
}