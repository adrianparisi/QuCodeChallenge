namespace QuCodeChallenge
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children = [];
        public bool IsWord = false;

        /// <summary>
        /// Add a new word to the tree
        /// </summary>
        public void AddWord(string word)
        {
            var current = this;

            foreach (char character in word)
            {
                if (!current.Children.TryGetValue(character, out TrieNode? next))
                {
                    next = new TrieNode();
                    current.Children[character] = next;
                }

                current = next;
            }

            current.IsWord = true;
        }

        /// <summary>
        /// Validate if children contains a certain character
        /// </summary>
        public bool ChildrenContains(char character)
        {
            return Children.ContainsKey(character);
        }
    }
}
