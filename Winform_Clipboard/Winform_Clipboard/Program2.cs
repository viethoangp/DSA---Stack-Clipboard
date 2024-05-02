using System;
using System.Collections.Generic;

namespace Clipboard
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
    }

    public class MyStack<T>
    {
        public Node<T> Top { get; set; }
        public int _count; 

        public MyStack()
        {
            _count = 0; 
        }

        public bool IsEmpty()
        {
            return Top == null;
        }

        public void Push(T ele)
        {
            Node<T> n = new Node<T>();
            n.Data = ele;
            n.Next = Top;
            Top = n;
            _count++; 
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                return default(T);
            }

            Node<T> d = Top;
            Top = Top.Next;
            _count--; 
            return d.Data;
        }
        public void Clear()
        {
            Top = null;
            _count = 0;
        }

        public int Count 
        {
            get { return _count; }
        }
        public T ElementAt(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is outside the bounds of the stack.");
            }

            // Traverse the stack from top to the target element
            int currentIndex = 0;
            Node<T> current = Top;
            while (current != null && currentIndex < index)
            {
                current = current.Next;
                currentIndex++;
            }

            if (current == null)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is outside the bounds of the stack.");
            }

            return current.Data;
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = Top;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

    }
    public class ClipboardSimulator
    {
        public readonly MyStack<string> activeStack;
        public readonly MyStack<string> historyStack;
        private int _maxCapacity;

        public ClipboardSimulator(int maxCapacity = 10)
        {
            activeStack = new MyStack<string>();
            historyStack = new MyStack<string>();
            _maxCapacity = maxCapacity;
        }

        public void Copy(string text)
        {
            if (activeStack.Count >= this._maxCapacity)
            {
                historyStack.Push(activeStack.Pop());
            }
            else
            {
                activeStack.Push(text);
            }
        }

        public string Paste(string value)
        {
            string word = "";
            string tempWord;

            if (activeStack.IsEmpty())
            {
                return "";
            }

            while (!activeStack.IsEmpty())
            {
                word = activeStack.Pop();
                historyStack.Push(word);
                if (word == value)
                {
                    break;
                }
            }

            while (!historyStack.IsEmpty())
            {
                tempWord = historyStack.Pop();
                activeStack.Push(tempWord);
            }

            return word;
        }

        public void ClearActiveStack()
        {
            activeStack.Clear(); 
        }

        public void PrintClipboard()
        {
            if (activeStack.IsEmpty())
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (string item in activeStack)
                {
                    Console.WriteLine($"{item}");
                }
            }
        }
    }

    

}
