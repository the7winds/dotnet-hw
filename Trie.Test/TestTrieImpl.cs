using Xunit;
using DotnetHW;
using System;

namespace DotnetHW.UnitTests
{
    public class TestTrieImpl : IDisposable
    {
        private const string WORD = "word";
        private ITrie trie;

        public TestTrieImpl()
        {
            trie = new TrieImpl();
        }

        public void Dispose()
        {
            trie = null;
        }

        [Fact]
        public void ShouldAddWord()
        {
            var added = trie.Add(WORD);
            Assert.True(added);
        }

        [Fact]
        public void ShouldDoNotAddWord()
        {
            trie.Add(WORD);
            var added = trie.Add(WORD);
            Assert.False(added);
        }

        [Fact]
        public void ShouldFindWord()
        {
            trie.Add(WORD);
            var contains = trie.Contains(WORD);
            Assert.True(contains);
        }

        [Fact]
        public void ShouldDoNotFindWord()
        {
            var contains = trie.Contains(WORD);
            Assert.False(contains);
        }

        [Fact]
        public void ShouldRemoveWord()
        {
            trie.Add(WORD);
            var removed = trie.Remove(WORD);
            Assert.True(removed);
        }

        [Fact]
        public void ShouldDoNotRemoveWord()
        {
            var removed = trie.Remove(WORD);
            Assert.False(removed);
        }

        [Fact]
        public void ShouldGetCorrectSize()
        {
            var words = new[] { "a", "b", "c", "aa", "ab", "cc" };

            foreach (var word in words)
            {
                trie.Add(word);
            }

            var size = trie.Size();

            Assert.Equal(words.Length, size);
        }

        [Fact]
        public void ShouldGetCorrectPrefixCount()
        {
            var words = new[] { "a", "b", "c", "aa", "ab", "cc" };

            foreach (var word in words)
            {
                trie.Add(word);
            }

            var aPrefixed = trie.HowManyStartsWithPrefix("a");

            Assert.Equal(3, aPrefixed);
        }
    }
}