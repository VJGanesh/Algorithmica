using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NodeChains
{
    class LinkedList<T>
    {
        public SinglyLinkedListNode<T> Head = null;
        private SinglyLinkedListNode<T> Tail = null;
        private int count = 0;
        public void AddFirst(SinglyLinkedListNode<T> node)
        {
            if (Head==null)
            {
                Head = node;
                Tail = null;
            }
            else
            {
                node.Next = Head;
                Head = node;
            }
            count++;
        }

        public void Add(SinglyLinkedListNode<T> node)
        {
            Tail.Next = node;
            Tail = node;
            count++;
        }
        //If the node added is already part of List?? :(
        public void AddLast(SinglyLinkedListNode<T> node)
        {
            SinglyLinkedListNode<T> temp = null;
            if (Head==null)
            {
                Head = Tail = node;
                count++;
                return;
            }

            if (count==1)
            {
                Head.Next = node;
                count++;
            }
            else
            {
                while (Head.Next != null)
                {
                    temp = Head.Next;
                }
                temp.Next = node;
                count++;
            }
            
        }

        public void RemoveLast(SinglyLinkedListNode<T> node)
        {
            SinglyLinkedListNode<T> prev = null;
            SinglyLinkedListNode<T> curr = null;

            while (Head.Next !=null)
            {
                prev = Head;
                curr = Head.Next;
            }
            Tail = prev;
            count--;
        }

        public bool Remove(T item)
        {
            SinglyLinkedListNode<T> cur = Head;

            if (cur.Value.Equals(item))
            {
                Head = Head.Next;
                count--;
                return true;
            }

            while (cur.Next != null)
            {
                
            }
           
            count--;
            return false;
        }

        public void Clear()
        {
            
        }

        //
    }

    class SinglyLinkedListNode<T>
    {
        public SinglyLinkedListNode(T value)
        {
            
        }
        public T Value { get; set; }
        public SinglyLinkedListNode<T> Next { get; set; }
    }
}
