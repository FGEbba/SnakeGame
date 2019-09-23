using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<data> : IEnumerator, IEnumerable
{
    class Node
    {
        public data value;
        public Node nextNode;

        //Constructor, no arguments
        public Node() { }

        // Constructor, one argument - int
        public Node(data value)
        {
            this.value = value;
        }

        public Node(Node node)
        {
            value = node.value;
            nextNode = node.nextNode;
        }


        ~Node() { }
    }

    private Node head;
    private int iteratorPos;

    public data this[int index]
    {
        get => GetValue(index);
        set => SetValue(index, value);
    }

    public int Count
    { get; private set; }

    public object Current => GetValue(iteratorPos);


    private void SetValue(int index, data value)
    {
        if (index < 0 || index >= Count || head == null)
        {
            throw new NotImplementedException();
        }

        Node currentNode = head;
        for (int i = 0; i <= index && currentNode != null; i++)
        {
            if (index.Equals(i))
            {
                currentNode.value = value;
                return;
            }
            currentNode = currentNode.nextNode;
        }
    }

    private data GetValue(int index)
    {
        if (index < 0 || index >= Count || head == null)
        {
            throw new NotImplementedException();
        }

        Node currentNode = head;

        for (int i = 0; i <= index && currentNode != null; i++)
        {
            if (index.Equals(i))
            {
                return currentNode.value;
            }
            currentNode = currentNode.nextNode;
        }
        return default(data);
    }

    public void Push(data value)
    {
        Node newNode = new Node(value);
        newNode.nextNode = head;
        head = newNode;
        Count++;
    }

    public void Insert(int index, data value)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }

        Node newNode = new Node(value);
        if (index == 0)
        {
            newNode.nextNode = head;
            head = newNode;
            Count++;
            return;
        }

        Node node = GetNode(index - 1);
    }

    public void Add(data value)
    {
        Node node = new Node(value);
        if (head == null)
        {
            head = node;
            Count++;
            return;
        }

        Node CurrentNode = head;
        while (CurrentNode.nextNode != null)
        {
            CurrentNode = CurrentNode.nextNode;
        }
        CurrentNode.nextNode = node;
        Count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }

        if (index == 0)
        {
            head = head.nextNode;
            Count--;
            return;
        }

        Node previousNode = GetNode(index - 1);
        previousNode.nextNode = previousNode.nextNode?.nextNode;
    }

    private Node GetNode(int index)
    {
        if (index < 0 || index >= Count || head == null)
        { throw new NotImplementedException(); }

        Node currentNode = head;
        for (int i = 0; i <= index; i++)
        {
            if (currentNode == null)
            { throw new NotImplementedException(); }

            if (index == i)
            { return currentNode; }

            currentNode = currentNode.nextNode;

        }

        return null;
    }

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        iteratorPos++;
        return iteratorPos < Count;
    }

    public void Reset()
    {
        iteratorPos = 0;
    }

    public void AddRange(LinkedList<data> list)
    {
        foreach (data value in list)
        {
            Add(value);
        }
    }
}
