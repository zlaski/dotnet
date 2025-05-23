// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

// Used to store mapping information for names occuring
// within the logical tree section.

using System.Collections;
using System.Collections.Specialized;
using System.Windows.Markup;

namespace System.Xaml
{
    /// <summary>
    /// Used to store mapping information for names occuring
    /// within the logical tree section.
    /// </summary>
    internal class NameScope : INameScopeDictionary
    {
        /// <summary>
        /// Register Name-Object Map
        /// </summary>
        /// <param name="name">name to be registered</param>
        /// <param name="scopedElement">object mapped to name</param>
        public void RegisterName(string name, object scopedElement)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(scopedElement);

            if (name.Length == 0)
            {
                throw new ArgumentException(SR.NameScopeNameNotEmptyString);
            }

            if (!NameValidationHelper.IsValidIdentifierName(name))
            {
                throw new ArgumentException(SR.Format(SR.NameScopeInvalidIdentifierName, name));
            }

            if (_nameMap is null)
            {
                _nameMap = new HybridDictionary();
                _nameMap[name] = scopedElement;
            }
            else
            {
                object nameContext = _nameMap[name];
                // first time adding the Name, set it
                if (nameContext is null)
                {
                    _nameMap[name] = scopedElement;
                }
                else if (scopedElement != nameContext)
                {
                    throw new ArgumentException(SR.Format(SR.NameScopeDuplicateNamesNotAllowed, name));
                }
            }
        }

        /// <summary>
        /// Unregister Name-Object Map
        /// </summary>
        /// <param name="name">name to be registered</param>
        public void UnregisterName(string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            if (name.Length == 0)
            {
                throw new ArgumentException(SR.NameScopeNameNotEmptyString);
            }

            if (_nameMap?[name] is null)
            {
                throw new ArgumentException(SR.Format(SR.NameScopeNameNotFound, name));
            }

            _nameMap.Remove(name);
        }

        /// <summary>
        /// Find - Find the corresponding object given a Name
        /// </summary>
        /// <param name="name">Name for which context needs to be retrieved</param>
        /// <returns>corresponding Context if found, else null</returns>
        public object FindName(string name)
        {
            if (_nameMap is null || string.IsNullOrEmpty(name))
            {
                return null;
            }

            return _nameMap[name];
        }

        // This is a HybridDictionary of Name-Object maps
        private HybridDictionary _nameMap;

        private IEnumerator<KeyValuePair<string, object>> GetEnumerator() => new Enumerator(_nameMap);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() => GetEnumerator();

        public int Count => _nameMap?.Count ?? 0;

        public bool IsReadOnly => false;

        public void Clear() => _nameMap = null;

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            if (_nameMap is null)
            {
                array = null;
                return;
            }

            foreach (DictionaryEntry entry in _nameMap)
            {
                array[arrayIndex++] = new KeyValuePair<string, object>((string)entry.Key, entry.Value);
            }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            if (!Contains(item))
            {
                return false;
            }

            if (item.Value != this[item.Key])
            {
                return false;
            }

            return Remove(item.Key);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            if (item.Key is null)
            {
                throw new ArgumentException(SR.Format(SR.ReferenceIsNull, "item.Key"), nameof(item));
            }

            if (item.Value is null)
            {
                throw new ArgumentException(SR.Format(SR.ReferenceIsNull, "item.Value"), nameof(item));
            }

            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            if (item.Key is null)
            {
                throw new ArgumentException(SR.Format(SR.ReferenceIsNull, "item.Key"), nameof(item));
            }

            return ContainsKey(item.Key);
        }

        public object this[string key]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(key);
                return FindName(key);
            }
            set
            {
                ArgumentNullException.ThrowIfNull(key);
                ArgumentNullException.ThrowIfNull(value);

                RegisterName(key, value);
            }
        }

        public void Add(string key, object value)
        {
            ArgumentNullException.ThrowIfNull(key);

            RegisterName(key, value);
        }

        public bool ContainsKey(string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            object value = FindName(key);
            return value is not null;
        }

        public bool Remove(string key)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            UnregisterName(key);
            return true;
        }

        public bool TryGetValue(string key, out object value)
        {
            if (!ContainsKey(key))
            {
                value = null;
                return false;
            }

            value = FindName(key);
            return true;
        }

        public ICollection<string> Keys
        {
            get
            {
                if (_nameMap is null)
                {
                    return null;
                }

                var list = new List<string>(_nameMap.Keys.Count);
                foreach (string key in _nameMap.Keys)
                {
                    list.Add(key);
                }

                return list;
            }
        }

        public ICollection<object> Values
        {
            get
            {
                if (_nameMap is null)
                {
                    return null;
                }

                var list = new List<object>(_nameMap.Values.Count);
                foreach (object value in _nameMap.Values)
                {
                    list.Add(value);
                }

                return list;
            }
        }

        private class Enumerator : IEnumerator<KeyValuePair<string, object>>
        {
            private IDictionaryEnumerator _enumerator;

            public Enumerator(HybridDictionary nameMap)
            {
                _enumerator = nameMap?.GetEnumerator();
            }

            public void Dispose() => GC.SuppressFinalize(this);

            public KeyValuePair<string, object> Current
            {
                get
                {
                    if (_enumerator is null)
                    {
                        return default(KeyValuePair<string, object>);
                    }

                    return new KeyValuePair<string, object>((string)_enumerator.Key, _enumerator.Value);
                }
            }

            public bool MoveNext() => _enumerator?.MoveNext() ?? false;

            object IEnumerator.Current => Current;

            void IEnumerator.Reset() => _enumerator?.Reset();
        }
    }
}
