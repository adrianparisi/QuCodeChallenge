namespace QuCodeChallenge
{
    public class WordFinder
    {
        private readonly char[][] _matrix;
        private readonly Dictionary<string, int> _result = [];

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = BuildMatrix(matrix);
        }

        /// <summary>
        /// Validates and formats the input matrix.
        /// </summary>
        /// <param name="matrix">The input matrix</param>
        private static char[][] BuildMatrix(IEnumerable<string> matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix, nameof(matrix));

            if (!matrix.Any())
                throw new ArgumentException("Matrix can not empty", nameof(matrix));

            var columns = matrix.First().Length;
            var matrixArray = matrix.ToArray();
            var rows = matrixArray.Length;
            var result = new char[rows][];

            for (var row = 0; row < rows; row++)
            {
                if (matrixArray[row].Length != columns)
                    throw new ArgumentException("Row lengths may be equals", nameof(matrix));

                for (var column = 0; column < columns; column++)
                {
                    result[row] = matrixArray[row].ToCharArray();
                }
            }

            return result;
        }

        /// <summary>
        /// Looks for the collection of words provided.
        /// </summary>
        /// <param name="words">The words to look for.</param>
        /// <returns>Returns a list of the most common words.</returns>
        public IEnumerable<string> Find(IEnumerable<string> words)
        {
            var root = BuildTree(words);
            int rows = _matrix.Length;
            int columns = _matrix[0].Length;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    SearchInMatrix(row, column, root, "");
                }
            }

            return _result
                .OrderByDescending(r => r.Value)
                .Take(10)
                .Select(r => r.Key);
        }

        /// <summary>
        /// Builds a tree with the words to look for.
        /// </summary>
        /// <returns>Returns the root node.</returns>
        private static TrieNode BuildTree(IEnumerable<string> words)
        {
            var root = new TrieNode();

            foreach (string word in words)
            {
                root.AddWord(word);
            }

            return root;
        }

        /// <summary>
        /// This is a recursive DFS that looks for the words using the tree
        /// from a certain position in the matrix.
        /// </summary>
        /// <param name="row">row index to validate</param>
        /// <param name="column">colum index to validate</param>
        /// <param name="node">Node of the tree.</param>
        /// <param name="word">word that was builded till now.</param>
        private void SearchInMatrix(int row, int column, TrieNode node, string word)
        {
            if (IsOutsideMatrixLimits(row, column)) return;

            var character = _matrix[row][column];
            if (!node.ChildrenContains(character)) return;

            node = node.Children[character];
            word += character;

            if (node.IsWord)
            {
                if (_result.TryGetValue(word, out int occurrences))
                {
                    _result[word] = ++occurrences;
                }
                else
                {
                    _result.Add(word, 1);
                }
            }

            SearchInMatrix(row + 1, column, node, word);
            SearchInMatrix(row, column + 1, node, word);
        }

        /// <summary>
        /// Validate that the row and column index are inside matrix limits.
        /// </summary>
        private bool IsOutsideMatrixLimits(int row, int column)
        {
            int rows = _matrix.Length;
            int columns = _matrix[0].Length;

            return row >= rows || column >= columns;
        }
    }
}
