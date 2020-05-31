﻿using System;
using System.Reflection;
using System.Windows.Controls;

namespace Reactor.WPF {
    public abstract class ViewHost : Grid {
        protected abstract Assembly Assembly { get; }
        protected abstract string Namespace { get; }

        public string View {
            set => Render(value);
        }

        protected void Render(string name) {
            var type = Assembly.GetType($"{Namespace}.{name}");

            if (type == null) {
                throw new Exception($"Could not find {name}");
            }

            var view = (IView)Activator.CreateInstance(type)!;
            Children.Clear();
            ViewHandlerRegistry.Default.Render(view, this);
        }
    }
}