using HomeManager.ViewModel;
using System.Collections.Concurrent;

namespace HomeManager.Helpers
{
    /// <summary>
    /// Een eenvoudige Messenger-implementatie voor communicatie tussen ViewModels zonder directe referenties.
    /// Ondersteunt context-gebaseerde berichtverzending en registratie.
    /// </summary>
    public class clsMessenger
    {
        private static readonly object CreationLock = new object();
        private static readonly ConcurrentDictionary<MessengerKey, object> Dictionary = new();

        private static clsMessenger _instance;

        /// <summary>
        /// Singleton-instantie van de Messenger.
        /// </summary>
        public static clsMessenger Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new clsMessenger();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor om singleton te forceren.
        /// </summary>
        private clsMessenger() { }

        /// <summary>
        /// Registreert een ontvanger voor een berichttype zonder context.
        /// </summary>
        /// <typeparam name="T">Het berichttype.</typeparam>
        /// <param name="recipient">De ontvanger.</param>
        /// <param name="action">De actie die uitgevoerd wordt bij ontvangst van het bericht.</param>
        public void Register<T>(object recipient, Action<T> action)
        {
            Register(recipient, action, null);
        }

        /// <summary>
        /// Registreert een ontvanger voor een berichttype met optionele context.
        /// </summary>
        /// <typeparam name="T">Het berichttype.</typeparam>
        /// <param name="recipient">De ontvanger.</param>
        /// <param name="action">De uit te voeren actie bij ontvangst van het bericht.</param>
        /// <param name="context">Een optionele context om te filteren op zendingen.</param>
        public void Register<T>(object recipient, Action<T> action, object context)
        {
            var key = new MessengerKey(recipient, context);
            Dictionary.TryAdd(key, action);
        }

        /// <summary>
        /// Verwijdert alle registratie van een ontvanger zonder context.
        /// </summary>
        /// <param name="recipient">De ontvanger die uit de lijst moet worden verwijderd.</param>
        public void Unregister(object recipient)
        {
            Unregister(recipient, null);
        }

        /// <summary>
        /// Verwijdert alle registratie van een ontvanger met de opgegeven context.
        /// </summary>
        /// <param name="recipient">De ontvanger.</param>
        /// <param name="context">De context (indien van toepassing).</param>
        public void Unregister(object recipient, object context)
        {
            var key = new MessengerKey(recipient, context);
            Dictionary.TryRemove(key, out _);
        }

        /// <summary>
        /// Stuurt een bericht naar alle geregistreerde ontvangers zonder contextfilter.
        /// </summary>
        /// <typeparam name="T">Het berichttype.</typeparam>
        /// <param name="message">Het te verzenden bericht.</param>
        public void Send<T>(T message)
        {
            Send(message, null);
        }

        /// <summary>
        /// Stuurt een bericht naar alle geregistreerde ontvangers met een specifieke context.
        /// </summary>
        /// <typeparam name="T">Het berichttype.</typeparam>
        /// <param name="message">Het te verzenden bericht.</param>
        /// <param name="context">De context om de ontvangers te filteren.</param>
        public void Send<T>(T message, object context)
        {
            IEnumerable<KeyValuePair<MessengerKey, object>> result = context == null
                ? Dictionary.Where(r => r.Key.Context == null)
                : Dictionary.Where(r => r.Key.Context != null && r.Key.Context.Equals(context));

            foreach (var action in result.Select(r => r.Value).OfType<Action<T>>())
            {
                action(message);
            }
        }

        /// <summary>
        /// Niet-geïmplementeerde helper (mogelijk voor specifieke login-messaging).
        /// </summary>
        internal void Register<T>(clsLogin clsLogin, string v, object newPassWord)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Interne sleutelstructuur voor identificatie van registraties met context.
        /// </summary>
        protected class MessengerKey
        {
            public object Recipient { get; private set; }
            public object Context { get; private set; }

            public MessengerKey(object recipient, object context)
            {
                Recipient = recipient;
                Context = context;
            }

            public bool Equals(MessengerKey other)
            {
                return Equals(Recipient, other.Recipient) && Equals(Context, other.Context);
            }

            public override bool Equals(object obj)
            {
                return obj is MessengerKey other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Recipient?.GetHashCode() ?? 0) * 397) ^ (Context?.GetHashCode() ?? 0);
                }
            }
        }
    }
}
