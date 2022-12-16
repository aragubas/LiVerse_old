using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework
{
    public struct EventKey
    {

        public string DomainName { get; }
        public string EventName { get; }

        public EventKey(string domainName, string eventName)
        {
            DomainName = domainName;
            EventName = eventName;
        }

        public EventKey(string eventKeyString)
        {
            if (!eventKeyString.Contains('.')) { throw new ArgumentException("Invalid EventKey string"); }
            string[] keySplit = eventKeyString.Split('.');
            
            if (keySplit.Length < 2)
            {
                throw new ArgumentException("Invalid EventKey string");
            }

            DomainName = keySplit[0];
            EventName = keySplit[1];
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is EventKey key)
            {
                return key.DomainName == DomainName && key.EventName == EventName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return DomainName.GetHashCode() + EventName.GetHashCode();
        }

        public override string ToString()
        {
            return $"{DomainName}.{EventName}";
        }
    }

    public class EventListener
    {
        /// <summary>
        /// Plugin Domain Name ex: <c>maxwell.twitchIntegration</c>.<br></br>The domain name <c>core</c> is reserved for the default plugins and core events.
        /// </summary>
        public string? DomainName { get; set; }

        /// <summary>
        /// Event Name ex: <c>onSpeak</c> or <c>onUpsideDownRedeem</c>
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Description text that will be shown on UI's
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// EventKey as registred on <seealso cref="GlobalEventManager"/> ex: <c>core.OnSpeak</c>
        /// </summary>
        public string EventKey
        {
            get => $"{DomainName}.{Name}";
        }

        /// <summary>
        /// Function to call after the event is invoked
        /// </summary>
        public Action? Callback;

        public EventListener(string domainName, string eventName, string description, Action callback)
        {
            DomainName = domainName;
            Name = eventName;
            Description = description;
            Callback = callback;
        }

        public EventListener(string description, Action callback)
        {
            Description = description;
            Callback = callback;
        }

    }

    public static class GlobalEventManager
    {
        public static Dictionary<EventKey, EventListener> EventListeners = new();

        /// <summary>
        /// Invokes a registered event
        /// </summary>
        /// <param name="eventKey">EventKey Object</param>
        /// <returns><c>True</c> if the operating was successful</returns>
        public static bool InvokeEvent(EventKey eventKey)
        {
            if (EventListeners.TryGetValue(eventKey, out EventListener eventListener))
            {
                if (eventListener.Callback == null)
                {
                    Console.WriteLine($"GlobalEventManager] Warning: ${eventKey} has a null callback.");
                    return false;
                }

                eventListener.Callback.Invoke();

                return true;
            }

            return false;

        }

        /// <summary>
        /// Invokes a registered event
        /// </summary>
        /// <param name="eventKey">EventKey ex: <c>core.onSpeaking</c></param>
        /// <returns><c>True</c> if the operating was successful</returns>
        public static bool InvokeEvent(string eventKey)
        {
            return InvokeEvent(new EventKey(eventKey));
        }

        /// <summary>
        /// Invokes a registered event
        /// </summary>
        /// <param name="domainName">Domain Name</param>
        /// <param name="keyName">Key Name</param>
        /// <returns><c>True</c> if the operating was successful</returns>
        public static bool InvokeEvent(string domainName, string keyName)
        {
            return InvokeEvent(new EventKey(domainName, keyName));
        }

        /// <summary>
        /// Adds an Event to the <seealso cref="EventListeners"/> dictionary.<br></br>Use this method and the <c>description, callback</c> overload to add an <seealso cref="EventListener"/> without having to type the name and domain of the event twice
        /// </summary>
        /// <param name="eventKey">EventKey Object</param>
        /// <param name="eventListener"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void AddEventListener(EventKey eventKey, EventListener eventListener)
        {
            if (!EventListeners.ContainsKey(eventKey))
            {
                eventListener.DomainName = eventKey.DomainName;
                eventListener.Name = eventKey.EventName;

                EventListeners.Add(eventKey, eventListener);
                return;
            }

            throw new ArgumentException($"EventListener {eventKey} already exists.");
        }

        /// <summary>
        /// Adds an Event to the <seealso cref="EventListeners"/> dictionary.<br></br>Use this method and the <c>description, callback</c> overload to add an <seealso cref="EventListener"/> without having to type the name and domain of the event twice
        /// </summary> 
        /// <param name="eventKey">EventKey String ex: <c>core.onSpeak</c></param>
        /// <param name="eventListener"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void AddEventListener(string eventKey, EventListener eventListener)
        {
            AddEventListener(new EventKey(eventKey), eventListener);
        }


    }
}
