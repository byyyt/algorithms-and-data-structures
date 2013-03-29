﻿using System;
using System.Collections.Generic;

namespace Utility
{
    public class SplayTree<T> : BinarySearchTree<T>
        where T : IComparable<T>
    {
        public SplayTree() { }

        public SplayTree(IEnumerable<T> collection) : base(collection) { }

        public override void Add(T item)
        {
            ValueTreeNode node = new ValueTreeNode(item);
            node = base.Add(node);
            this.Splay(node);
        }

        public override bool Remove(T item)
        {
            ValueTreeNode current = (ValueTreeNode)this.root, previous = null;
            while (current != null)
            {
                previous = current;
                if (item.CompareTo(current.Value) < 0)
                    current = current.Left;
                else if (item.CompareTo(current.Value) > 0)
                    current = current.Right;
                else
                    break;
            }

            if (current == null)
            {
                this.Splay(previous);
                return false;
            }

            if (current.Count > 1)
            {
                this.Splay(current);
                current.Count--;
                return true;
            }

            if (current.Left == null || current.Right == null)
                this.Delete(current);
            else
            {
                ValueTreeNode replace = (ValueTreeNode)this.Predecessor(current);
                if (replace == null)
                    replace = (ValueTreeNode)this.Successor(current);

                current.Value = replace.Value;
                current.Count = replace.Count;

                this.Delete(replace);
            }

            this.Count--;
            return true;
        }

        protected override void Delete(TreeNode current)
        {
            base.Delete(current);
            this.Splay((ValueTreeNode)current.Parent);
        }

        protected override ValueTreeNode Find(T item)
        {
            ValueTreeNode current = (ValueTreeNode)this.root, previous = null;
            while (current != null)
            {
                int c = item.CompareTo(current.Value);
                previous = current;
                if (c < 0)
                    current = current.Left;
                else if (c > 0)
                    current = current.Right;
                else
                    break;
            }

            this.Splay(previous);
            return current;
        }

        private void Splay(ValueTreeNode start)
        {
            bool leftC, leftPC;

            if (this.root == start)
                return;

            leftC = start == start.Parent.Left;

            if (start.Parent.Parent == null)
            {
                if (leftC)
                    this.RotateRight(start.Parent);
                else
                    this.RotateLeft(start.Parent);
                return;
            }

            leftPC = start.Parent == start.Parent.Parent.Left;

            if (leftC && leftPC)
            {
                this.RotateRight(start.Parent.Parent);
                this.RotateRight(start.Parent);
            }
            else if (!leftC && !leftPC)
            {
                this.RotateLeft(start.Parent.Parent);
                this.RotateLeft(start.Parent);
            }
            else if (!leftC && leftPC)
            {
                this.RotateLeft(start.Parent);
                this.RotateRight(start.Parent);
            }
            else
            {
                this.RotateRight(start.Parent);
                this.RotateLeft(start.Parent);
            }

            this.Splay(start);
        }
    }
}