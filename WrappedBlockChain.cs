using System.Collections;
using Libplanet;
using Libplanet.Blockchain;
using Libplanet.Blocks;
using Bencodex;

namespace Telescope
{
    public class WrappedBlockChain : IList
    {
        private BlockChain<MockAction> _blockChain;
        private object _syncRoot;

        public WrappedBlockChain(BlockChain<MockAction> blockChain)
        {
            _blockChain = blockChain;
            _syncRoot = new object();
        }

        public int IndexOf(object? obj) => throw new NotImplementedException("Indexing is not allowed.");

        public void Insert(int index, object? obj) => throw new NotImplementedException("Inserting is not allowed.");

        public void RemoveAt(int index) => throw new NotImplementedException("Removing is not allowed.");

        // FIXME: Index retrieval should use long.
        public object? this[int index]
        {
            get
            {
                return new WrappedBlock(_blockChain[index]);
            }
            set
            {
                throw new NotImplementedException("Setting is not allowed.");
            }
        }

        public WrappedBlock this[byte[] blockHash]
        {
            get
            {
                return new WrappedBlock(_blockChain[new BlockHash(blockHash)]);
            }
            set
            {
                throw new NotImplementedException("Setting is not allowed.");
            }
        }

        public int Add(object? obj) => throw new NotImplementedException("Adding is not allowed.");

        public void Clear() => throw new NotImplementedException("Clearing is not allowed.");

        public bool Contains(object? obj) => throw new NotImplementedException("Checking containment is not allowed.");

        // FIXME: Forced casting due to IList.
        public int Count => (int)_blockChain.Tip.Index;

        public void CopyTo(Array objs, int count) => throw new NotImplementedException("Copying is not allowed.");

        public void Remove(object? obj) => throw new NotImplementedException("Removing is not allowed.");

        public bool IsReadOnly => true;

        public bool IsFixedSize => true;

        public bool IsSynchronized => true;

        public object SyncRoot => _syncRoot;

        public IEnumerator<WrappedBlock> GetEnumerator() => throw new NotImplementedException("Enumerating is not allowed.");

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException("Enumerating is not allowed.");

        public Bencodex.Types.IValue GetState(string hash, string address)
        {
            return _blockChain.GetState(new Address(address), new BlockHash(ByteUtil.ParseHex(hash)));
        }
    }
}