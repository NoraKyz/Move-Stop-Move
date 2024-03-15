using System.Collections.Generic;
using UnityEngine;

namespace _SDK.SerializableDictionary
{
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		private TKey[] _mKeys;
		private TValue[] _mValues;

		public SerializableDictionary()
		{
			
		}

		public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict.Count)
		{
			foreach (var kvp in dict)
			{
				this[kvp.Key] = kvp.Value;
			}
		}

		public void CopyFrom(IDictionary<TKey, TValue> dict)
		{
			this.Clear();
			foreach (var kvp in dict)
			{
				this[kvp.Key] = kvp.Value;
			}
		}

		public void OnAfterDeserialize()
		{
			if(_mKeys != null && _mValues != null && _mKeys.Length == _mValues.Length)
			{
				this.Clear();
				int n = _mKeys.Length;
				for(int i = 0; i < n; ++i)
				{
					this[_mKeys[i]] = _mValues[i];
				}

				_mKeys = null;
				_mValues = null;
			}

		}

		public void OnBeforeSerialize()
		{
			int n = this.Count;
			_mKeys = new TKey[n];
			_mValues = new TValue[n];

			int i = 0;
			foreach(var kvp in this)
			{
				_mKeys[i] = kvp.Key;
				_mValues[i] = kvp.Value;
				++i;
			}
		}
	}
}
