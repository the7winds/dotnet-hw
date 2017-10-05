using Xunit;
using DotnetHW;
using System;

namespace DotnetHW.UnitTests
{
    public class TestTrieImpl : IDisposable
    {
        private const string WORD = "word";
        private ITrie _trie;

        public TestTrieImpl()
        {
            _trie = new TrieImpl();
        }

        public void Dispose()
        {
            _trie = null;
        }

        [Fact]
        public void ShouldAddWord()
        {
            var added = _trie.Add(WORD);
            Assert.True(added);
        }

        [Fact]
        public void ShouldDoNotAddWord()
        {
            _trie.Add(WORD);
            var added = _trie.Add(WORD);
            Assert.False(added);
        }

        [Fact]
        public void ShouldFindWord()
        {
            _trie.Add(WORD);
            var contains = _trie.Contains(WORD);
            Assert.True(contains);
        }

        [Fact]
        public void ShouldDoNotFindWord()
        {
            var contains = _trie.Contains(WORD);
            Assert.False(contains);
        }

        [Fact]
        public void ShouldRemoveWord()
        {
            _trie.Add(WORD);
            var removed = _trie.Remove(WORD);
            Assert.True(removed);
        }

        [Fact]
        public void ShouldDoNotRemoveWord()
        {
            var removed = _trie.Remove(WORD);
            Assert.False(removed);
        }

        [Fact]
        public void ShouldGetCorrectSize()
        {
            var words = new[] { "a", "b", "c", "aa", "ab", "cc" };

            foreach (var word in words)
            {
                _trie.Add(word);
            }

            var size = _trie.Size();

            Assert.Equal(words.Length, size);
        }

        [Fact]
        public void ShouldGetCorrectPrefixCount()
        {
            var words = new[] { "a", "b", "c", "aa", "ab", "cc" };

            foreach (var word in words)
            {
                _trie.Add(word);
            }

            var aPrefixed = _trie.HowManyStartsWithPrefix("a");

            Assert.Equal(3, aPrefixed);
        }

        [Fact]
        public void ShouldRemoveString()
        {
            _trie.Add("");
            
            var removed = _trie.Remove("");

            Assert.True(removed);
        }

        [Fact]
        public void ShouldNotRemoveString()
        {
            var removed = _trie.Remove("");
            Assert.False(removed);
        }
    }
}