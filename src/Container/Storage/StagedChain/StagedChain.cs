﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Unity.Storage
{
    /// <summary>
    /// Represents a chain of builder strategies partitioned by stages.
    /// </summary>
    /// <typeparam name="TStageEnum">The stage enumeration to partition the strategies.</typeparam>
    /// <typeparam name="TStrategyType"><see cref="Type"/> of strategy</typeparam>
    public partial class StagedChain<TStageEnum, TStrategyType> : IDictionary<TStageEnum, TStrategyType>,
                                                                  IEnumerable<TStrategyType>
        where TStageEnum    : Enum 
        where TStrategyType : class
    {
        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string ERROR_MESSAGE = "An element with the same key already exists";

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)] 
        private static readonly int _size = Enum.GetNames(typeof(TStageEnum)).Length;
        
        private readonly TStrategyType?[] _stages = new TStrategyType[_size];

        #endregion


        #region Constructors

        public StagedChain()
            => Type = typeof(TStageEnum);

        public StagedChain(Type type) 
            => Type = type;

        #endregion


        #region Properties

        public Type Type { get; }

        public int Count
            => _stages.Aggregate(0, (a, s) => null == s ? a : a + 1);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsReadOnly => false;

        #endregion

        
        #region Add

        public void Add(TStageEnum key, TStrategyType value)
        {
            ref var position = ref _stages[Convert.ToInt32(key)];
            
            if (null != position) throw new ArgumentException(ERROR_MESSAGE);

            position = value;

            ChainChanged?.Invoke(this, Type);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(KeyValuePair<TStageEnum, TStrategyType> item)
            => Add(item.Key, item.Value);

        public void Add(params KeyValuePair<TStageEnum, TStrategyType>[] items)
        {
            for (var i = 0; i < items.Length; i++)
            {
                ref var pair = ref items[i];
                ref var position = ref _stages[Convert.ToInt32(pair.Key)];
                
                if (null != position) throw new ArgumentException(ERROR_MESSAGE);

                position = pair.Value;
            }
            
            ChainChanged?.Invoke(this, Type);
        }

        #endregion


        #region Get/Set

        public bool TryGetValue(TStageEnum key, out TStrategyType value)
        {
            value = _stages[Convert.ToInt32(key)]!;

            return null != value;
        }

        public TStrategyType this[TStageEnum key]
        {
            get => _stages[Convert.ToInt32(key)] ?? throw new KeyNotFoundException();
            set
            {
                _stages[Convert.ToInt32(key)] = value;
                ChainChanged?.Invoke(this, Type);
            }
        }

        #endregion


        #region Remove

        public bool Remove(TStageEnum key)
        {
            ref var position = ref _stages[Convert.ToInt32(key)];

            if (null != position)
            {
                position = null;
                ChainChanged?.Invoke(this, Type);
                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TStageEnum, TStrategyType> item)
        {
            ref var position = ref _stages[Convert.ToInt32(item.Key)];

            if (item.Value == position)
            {
                position = null;
                ChainChanged?.Invoke(this, Type);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            for(var i = 0; i < _size; i++) _stages[i] = null;
        }

        #endregion


        #region Contains

        public bool Contains(KeyValuePair<TStageEnum, TStrategyType> item) 
            => item.Value == _stages[Convert.ToInt32(item.Key)];

        public bool ContainsKey(TStageEnum key) 
            => null != _stages[Convert.ToInt32(key)];

        #endregion

        
        #region Keys/Values collections

        public ICollection<TStageEnum> Keys
        {
            get
            {
                return enumerable().ToArray();

                IEnumerable<TStageEnum> enumerable()
                {
                    foreach (TStageEnum stage in Enum.GetValues(typeof(TStageEnum)))
                    {
                        if (null != _stages[Convert.ToInt32(stage)])
                            yield return stage!;
                    }
                }

            }
        }

        public ICollection<TStrategyType> Values
            => (from stage in _stages where null != stage select stage).ToArray();

        #endregion


        #region Not Supported

        IEnumerator<KeyValuePair<TStageEnum, TStrategyType>> IEnumerable<KeyValuePair<TStageEnum, TStrategyType>>.GetEnumerator() 
            => throw new NotSupportedException();

        public void CopyTo(KeyValuePair<TStageEnum, TStrategyType>[] array, int arrayIndex) 
            => throw new NotSupportedException();

        #endregion
    }
}
