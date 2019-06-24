﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Widget;
using AView = Android.Views.View;

namespace HotUI.Android
{
    public class StackHandler : LinearLayout, IViewHandler, IView
    {
        public StackHandler() : base(AndroidContext.CurrentContext)
        {
            Orientation = Orientation.Vertical;
            base.SetBackgroundColor(Color.Green);
        }
        
        public AView View => this;

        public void Remove(View view)
        {
        }

        Stack stack;

        public void SetView(View view)
        {
            stack = view as Stack;
            UpdateChildren(stack);
            stack.ChildrenChanged += Stack_ChildrenChanged;
        }

        private void Stack_ChildrenChanged(object sender, EventArgs e)
        {
            UpdateChildren(stack);
        }

        public void UpdateValue(string property, object value)
        {
        }

        List<AView> views = new List<AView>();

        protected void UpdateChildren(Stack stack)
        {
            var children = stack.GetChildren();
            if (views.Count == children.Count)
            {
                bool areSame = false;
                for (var i = 0; i < views.Count; i++)
                {
                    var v = views[i];
                    var c = children[i].ToView();
                    areSame = c == v;
                    if (!areSame)
                    {
                        break;
                    }
                }

                if (areSame)
                    return;
            }

            foreach (var v in views)
            {
                base.RemoveView(v);
            }

            views.Clear();
            foreach (var child in children)
            {
                var cview = child.ToView();
                views.Add(cview);
                //cview.ContentMode = UIViewContentMode.Top;
                base.AddView(cview);
            }
        }
    }
}