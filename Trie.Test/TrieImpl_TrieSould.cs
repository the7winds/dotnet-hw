using Xunit;
using DotnetHW;

namespace DotnetHW.UnitTests
{
    public class TrieImpl_IsTrieShould
    {

        private readonly string WORD = "word";

        [Fact]
        public void AddWord()
        {
            var trie = new TrieImpl();
            Assert.True(trie.Add(WORD));
        }

        [Fact]
        public void DoNotAddWord()
        {
            var trie = new TrieImpl();
            trie.Add(WORD);
            Assert.False(trie.Add(WORD));
        }

        [Fact]
        public void FindWord()
        {
            var trie = new TrieImpl();
            trie.Add(WORD);
            Assert.True(trie.Contains(WORD));
        }

        [Fact]
        public void DoNotFindWord()
        {
            var trie = new TrieImpl();
            Assert.False(trie.Contains(WORD));
        }

        [Fact]
        public void RemoveWord()
        {
            var trie = new TrieImpl();
            trie.Add(WORD);
            Assert.True(trie.Remove(WORD));
        }

        [Fact]
        public void DoNotRemoveWord()
        {
            var trie = new TrieImpl();
            Assert.False(trie.Remove(WORD));
        }

        [Fact]
        public void GetCorrectSize()
        {
            var trie = new TrieImpl();
            var words = new[] { "a", "b", "c", "aa", "ab", "cc" };

            foreach (var word in words)
            {
                trie.Add(word);
            }

            Assert.Equal(words.Length, trie.Size());
        }

        [Fact]
        public void GetCorrectPrefixCount()
        {
            var trie = new TrieImpl();
            var words = new[] { "a", "b", "c", "aa", "ab", "cc" };

            foreach (var word in words)
            {
                trie.Add(word);
            }

            Assert.Equal(3, trie.HowManyStartsWithPrefix("a"));
        }
    }
}