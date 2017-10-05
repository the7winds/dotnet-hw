using System.Collections.Generic;

namespace DotnetHW
{
    public class TrieImpl : ITrie
    {
        private TrieNode _root = new TrieNode();

        public bool Add(string element)
        {
            var current = _root;

            foreach (var c in element)
            {
                current.Size++;

                if (!current.Children.ContainsKey(c))
                {
                    var next = new TrieNode();
                    current.Children.Add(c, next);
                }

                current = (TrieNode)current.Children[c];
            }

            if (current.IsTerminate)
            {
                return false;
            }
            else
            {
                current.IsTerminate = true;
                current.Size = 1;
                return true;
            }
        }

        public bool Contains(string element)
        {
            var node = _root;

            foreach (var c in element)
            {
                if (!node.Children.ContainsKey(c))
                {
                    return false;
                }

                node = (TrieNode)node.Children[c];
            }

            return node.IsTerminate;
        }

        public bool Remove(string element)
        {
            var node = _root;

            foreach (var c in element)
            {
                if (!node.Children.ContainsKey(c))
                {
                    return false;
                }

                node = (TrieNode)node.Children[c];
            }

            if (node.IsTerminate == true) {
                node.IsTerminate = false;
                return true;
            }

            return false;
        }

        public int Size() => _root.Size;

        public int HowManyStartsWithPrefix(string prefix)
        {
            var node = _root;

            foreach (var c in prefix)
            {
                if (!node.Children.ContainsKey(c))
                {
                    return 0;
                }

                node = (TrieNode)node.Children[c];
            }

            return node.Size;
        }

        private class TrieNode
        {
            public bool IsTerminate = false;
            public int Size = 0;
            public readonly IDictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
        }
    }
}