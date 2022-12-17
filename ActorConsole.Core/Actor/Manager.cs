using System;
using System.Collections.Generic;

namespace ActorConsole.Core.Actor
{
    /// <summary>
    /// Main entrypoint for managing actors which includes adding, deleting, and keeping track of actor objects in a list.
    /// </summary>
    public static class Manager
    {
        /// <summary>
        /// Invoked whenever a attribute of an actor is modified.
        /// </summary>
        public static event EventHandler OnActorAttributeModified;

        internal static void RaiseOnActorAttributeModified(object sender) => OnActorAttributeModified?.Invoke(sender, EventArgs.Empty);

        private static readonly List<Actor> ActorsList = new List<Actor>();
        /// <summary>
        /// A copy array of the current list of all actors in the scene.
        /// </summary>
        public static Actor[] Actors => ActorsList.ToArray();
        /// <summary>
        /// Adds an actor to the current scene.
        /// </summary>
        public static void Add()
        {
            ActorsList.Add(new Actor());
        }
        /// <summary>
        /// Deletes an actor from the current scene.
        /// </summary>
        /// <param name="index">Index of actor in list.</param>
        public static void Delete(int index)
        {
            Actors[index].Delete();
            ActorsList.RemoveAt(index);
        }
        /// <summary>
        /// Deletes an actor from the current scene.
        /// </summary>
        /// <param name="name">Name of the actor.</param>
        public static void Delete(string name)
        {
            Delete(Search(name));
        }
        /// <summary>
        /// Calls "actorback" to reset the scene.
        /// </summary>
        public static void ActorBack()
        {
            Memory.IW4.SendDvar("actorback");
        }
        /// <summary>
        /// Searches for an actor in the main list.
        /// </summary>
        /// <param name="name">Name of the actor.</param>
        /// <returns>Index of actor in the main list and returns -1 if not found.</returns>
        public static int Search(string name)
        {
            for (int i = 0; i < ActorsList.Count; i++)
                if (Actors[i].Name == name)
                    return i;
            return -1;
        }
        /// <summary>
        /// Completely resets the memory of the manager.
        /// Actor list of cleared.
        /// </summary>
        public static void Reset()
        {
            ActorsList.Clear();
            Actor.Amount = 1;
        }
    }
}