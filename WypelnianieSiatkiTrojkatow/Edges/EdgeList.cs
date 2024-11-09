using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow.Edges
{
    public class EdgeList
    {
        public Edge? head { get; set; }
        public Edge? tail { get; set; }

        public EdgeList(Edge? head = null, Edge? tail = null)
        {
            this.head = head;
            this.tail = head is not null && tail is null ? head : tail;
        }

        public void AddAtEnd(Edge e)
        {
            if (head is null)
            {
                head = e;
                tail = e;
            }
            else
            {
                tail!.next = e;
                tail = e;
            }
        }

        public void AddAtEnd(EdgeList el)
        {
            if (el.IsEmpty()) return;

            if (head is null)
            {
                head = el.head;
                tail = el.tail;
            }
            else
            {
                tail!.next = el.head;
                tail = el.tail;
            }
        }

        public void QSort()
        {
            if (IsEmpty()) return;

            Edge? e = head.next, n = head.next;
            Edge pivot = head;
            pivot.next = null;
            EdgeList less = new EdgeList();
            EdgeList more = new EdgeList();
            while (e is not null)
            {
                n = e.next;
                e.next = null;
                if (e.x <= pivot.x)
                    less.AddAtEnd(e);
                else
                    more.AddAtEnd(e);
                e = n;
            }
            less.QSort();
            more.QSort();

            less.AddAtEnd(pivot);
            less.AddAtEnd(more);

            head = less.head;
            tail = less.tail;
        }

        public void Delete(Edge e)
        {
            if (IsEmpty()) return;

            if (e == head)
            {
                if (e == tail)
                {
                    head = null; tail = null;
                }
                else
                    head = head!.next;
                return;
            }

            Edge? prev = head;
            while (prev is not null && prev.next != e) prev = prev.next;

            if (prev is null) return;

            prev.next = e.next;

            if (e == tail)
                tail = prev;
        }

        public void Clear()
        {
            head = null;
            tail = null;
        }

        public bool IsEmpty()
            => head is null && tail is null;
    }
}
