﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Resolution;

namespace Unity
{
    /// <summary>
    /// Interface defining the behavior of the Unity dependency injection container.
    /// </summary>
    public partial interface IUnityContainerAsync
    {
        /// <summary>
        /// Lists all registrations available at this container.
        /// </summary>
        /// <remarks>
        /// This collection contains all registrations from this container as well
        /// as from all predecessor containers if this is a child container. Registrations
        /// from child containers override registrations with same type and name from
        /// parent containers.
        /// The sort order of returned registrations is not guaranteed in any way.
        /// </remarks>
        IAsyncEnumerable<ContainerRegistration> RegistrationsAsync { get; }


        /// <summary>
        /// Resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type"><see cref="Type"/> of object to get typeFrom the container.</param>
        /// <param name="regex">Pattern to match names to. Only these with successful 
        /// <see cref="Regex.IsMatch(string name)"/> will be resolved</param>
        /// <param name="overrides">Any overrides for the resolve call.</param>
        /// <returns>The retrieved object.</returns>
        IAsyncEnumerable<object?> ResolveAsync(Type type, Regex regex, params ResolverOverride[] overrides);
    }
}
