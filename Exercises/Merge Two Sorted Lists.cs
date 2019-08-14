using System;
using System.Collections.Generic;

namespace Merge_Two_Sorted_Lists
{
    public class Solution
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();

            ListNode listNode1 = new ListNode(1) { next = new ListNode(2) { next = new ListNode(4) } };
            ListNode listNode2 = new ListNode(1) { next = new ListNode(3) { next = new ListNode(4) } };

            solution.MergeTwoLists(listNode1, listNode2);
        }
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null) return l2;
            if (l2 == null) return l1;
            if (l1.val < l2.val)
            {
                l1.next = MergeTwoLists(l2, l1.next);
                return l1;
            }
            else
            {
                l2.next = MergeTwoLists(l1, l2.next);
                return l2;
            }
        }
    }
}
