using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Extensions.Exceptions
{
    /// <summary>
    /// Custom exception to handle entity creation failure
    /// </summary>
    /// <typeparam name="T">Type of object that failed to create</typeparam>
    public class EntityPutException<T> : Exception
    {
        /// <summary>
        /// The entity
        /// </summary>
        public T Entity { get; private set; }
        /// <summary>
        /// Default CTOR
        /// </summary>
        public EntityPutException() { }
        /// <summary>
        /// Overloaded CTOR with message
        /// </summary>
        /// <param name="message">Message for exception</param>
        public EntityPutException(string message) : base(message) { }
        /// <summary>
        /// Overloaded CTOR with message and Inner Exception
        /// </summary>
        /// <param name="message">Message for exception</param>
        /// <param name="inner">Inner exception</param>
        public EntityPutException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Overloaded CTOR with message and Entity
        /// </summary>
        /// <param name="message">Message for exception</param>
        /// <param name="entity">Entity that caused the exception</param>
        public EntityPutException(string message, T entity) : base(message) => Entity = entity;
    }
}
