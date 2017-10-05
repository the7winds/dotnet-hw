using System.Collections;

namespace DotnetHW
{
    public class TrieImpl : ITrie
    {
        TrieNode root = new TrieNode();

        public bool Add(string element)
        {
            var current = root;

            foreach (var c in element)
            {
                current.size++;

                if (!current.children.Contains(c))
                {
                    var next = new TrieNode();
                    current.children.Add(c, next);
                }

                current = (TrieNode)current.children[c];
            }

            if (current.terminate)
            {
                return false;
            }
            else
            {
                current.terminate = true;
                current.size = 1;
                return true;
            }
        }

        public bool Contains(string element)
        {
            var node = root;

            foreach (var c in element)
            {
                if (!node.children.Contains(c))
                {
                    return false;
                }

                node = (TrieNode)node.children[c];
            }

            return node.terminate;
        }

        public bool Remove(string element)
        {
            var node = root;

            foreach (var c in element)
            {
                if (!node.children.Contains(c))
                {
                    return false;
                }

                node = (TrieNode)node.children[c];
            }

            node.terminate = false;

            return true;
        }

        public int Size() => root.size;

        public int HowManyStartsWithPrefix(string prefix)
        {
            var node = root;

            foreach (var c in prefix)
            {
                if (!node.children.Contains(c))
                {
                    return 0;
                }

                node = (TrieNode)node.children[c];
            }

            return node.size;
        }

        class TrieNode
        {
            public bool terminate = false;
            public int size = 0;
            public Hashtable children = new Hashtable();
        }
    }
}