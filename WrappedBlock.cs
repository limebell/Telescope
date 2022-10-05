using System.Globalization;
using Libplanet;
using Libplanet.Blocks;

namespace Telescope
{
    /// <summary>
    /// A thin wrapper for <see cref="Block{T}"/> used for rendering.  Mostly spits out
    /// formatted <see cref="string"/>s.
    /// </summary>
    public class WrappedBlock
    {
        public const int IndexItemIndex = 0;
        public const int HashItemIndex = 1;
        public const int TimestampItemIndex = 2;
        public const int MinerItemIndex = 3;
        public const int PublicKeyIndex = 4;
        public const int TxHashIndex = 5;
        public const int NonceIndex = 6;
        public const int PreEvaluationHashIndex = 7;
        public const int TransactionsCountIndex = 8;
        public const int StateRootHashIndex = 9;
        public const int SignatureIndex = 10;

        private const string TimestampFormat = "yyyy-MM-ddTHH:mm:ss.ffffffZ";
        private Block<MockAction> _block;

        public WrappedBlock(Block<MockAction> block)
        {
            _block = block;
        }

        public Block<MockAction> Block => _block;

        public List<WrappedTransaction> Transactions => _block.Transactions.Select(tx => new WrappedTransaction(tx)).ToList();

        public override string ToString() => Summary;

        /// <summary>
        /// A short single line summarized representation of a <see cref="Block{T}"/> to be used as a list item.
        /// </summary>
        public string Summary
        {
            get
            {
                return
                    $"{Utils.ToFixedWidth(Index, BlockChainView.IndexPaddingSize)} " +
                    $"{Block.Hash}";
            }
        }

        public List<string> Detail
        {
            get
            {
                List<string> lines = new List<string>();
                string label;
                string value; // Just to make it easier to copy-paste repeated code
                label = "Index:";
                value = Index;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Hash:";
                value = Hash;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Timestamp:";
                value = Timestamp;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Miner:";
                value = Miner;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Public Key:";
                value = PublicKey;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "TxHash:";
                value = TxHash;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Nonce:";
                value = Nonce;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Pre-Evaluation Hash:";
                value = PreEvaluationHash;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Transactions Count:";
                value = TransactionsCount;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "State Root Hash:";
                value = StateRootHash;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                label = "Signature:";
                value = Signature;
                lines.Add(
                    $"{Utils.ToFixedWidth(label, BlockView.LabelPaddingSize)} {value}");
                return lines;
            }
        }

        public string Index => Block.Index.ToString();

        public string Hash => Block.Hash.ToString();

        public string Timestamp => Block.Timestamp.ToString(TimestampFormat, CultureInfo.InvariantCulture);

        public string Miner => Block.Miner.ToString();

        public string PublicKey => Block.PublicKey is { } publicKey ? publicKey.ToString() : "null";

        public string TxHash => Block.TxHash is { } txHash ? txHash.ToString() : "null";

        public string Nonce => Block.Nonce.ToString();

        public string PreEvaluationHash => ByteUtil.Hex(Block.PreEvaluationHash);

        public string TransactionsCount => Block.Transactions.Count.ToString();

        public string StateRootHash => Block.StateRootHash.ToString();

        public string Signature => Block.Signature is { } signature ? ByteUtil.Hex(signature) : "null";
    }
}