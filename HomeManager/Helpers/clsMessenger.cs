using HomeManager.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Helpers
{
    public class clsMessenger
    {
        private static readonly object CreationLock = new object();
        private static readonly ConcurrentDictionary<MessengerKey, Object> Dictionary = new ConcurrentDictionary<MessengerKey, Object>();

        private static clsMessenger _instance;

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

        private clsMessenger()
        {
        }
        public void Register<T>(Object recipient, Action<T> action)
        {
            Register(recipient, action, null);
        }

        public void Register<T>(Object recipient, Action<T> action, object context)
        {
            var key = new MessengerKey(recipient, context);
            Dictionary.TryAdd(key, action);
        }

        public void Unregister(Object recipient)
        {
            Unregister(recipient, null);
        }

        public void Unregister(Object recipient, object context)
        {
            object action;
            var key = new MessengerKey(recipient, context);
            Dictionary.TryRemove(key, out action);
        }

        public void Send<T>(T message)
        {
            Send(message, null);
        }

        public void Send<T>(T message, object context)
        {
            IEnumerable<KeyValuePair<MessengerKey, Object>> result;
            if (context == null)
            {
                result = from r in Dictionary where r.Key.Context == null select r;
            }
            else
            {
                result = from r in Dictionary where r.Key.Context != null && r.Key.Context.Equals(context) select r;
            }

            foreach (var action in result.Select(r => r.Value).OfType<Action<T>>())
            {
                action(message);
            }
        }

        internal void Register<T>(clsLogin clsLogin, string v, object newPassWord)
        {
            throw new NotImplementedException();
        }

        protected class MessengerKey
        {
            public Object Recipient { get; private set; }
            public Object Context { get; private set; }

            public MessengerKey(Object recipient, Object context)
            {
                Recipient = recipient;
                Context = context;
            }

            public bool Equals(MessengerKey other)
            {
                return Equals(Recipient, other.Recipient) && Equals(Context, other.Context);
            }

            public override bool Equals(Object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((MessengerKey)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Recipient != null ? Recipient.GetHashCode() : 0) * 397) ^ (Context != null ? Context.GetHashCode() : 0);
                }
            }
        }




    }


}
