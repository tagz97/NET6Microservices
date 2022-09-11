using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Extensions.Exceptions
{
    /// <summary>
    /// Custom exception to handle entities failing deletion
    /// </summary>
    public class EntityDeleteException : Exception
    {
        /// <summary>
        /// Id of the entity
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Default CTOR
        /// </summary>
        public EntityDeleteException() { }
        /// <summary>
        /// Overloaded CTOR with Exception message
        /// </summary>
        /// <param name="message">Exception message</param>
        public EntityDeleteException(string message) : base(message) { }
        /// <summary>
        /// Overloaded CTOR with Exception message and Inner <see cref="Exception"/>
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception<see cref="Exception"/></param>
        public EntityDeleteException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Overloaded CTOR with Exception message and Id of the entity
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="id">Id of the entity</param>
        public EntityDeleteException(string message, string id) : base(message) => Id = id;
    }
}
