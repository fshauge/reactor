﻿using Reactor.Binding;
using Reactor.Extensions;
using Reactor.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reactor.Demo.Components {
    public class App : Component {
        private readonly State<int> _page = 1;

        public override IEnumerable<IBinding> Bindings => new[] { _page };

        private IView RenderTodos() =>
            new ScrollView(
                new VStack {
                    Children = Enumerable
                        .Range(1, Math.Max(_page.Value, 1))
                        .Select(id => new Todo(id))
                        .ToArray()
                }
            );

        public override IView Render() =>
            new GridLayout {
                Rows = new Number?[] { null, 1.Fr() },
                Children = new GridLayout.Item[] {
                    new GridLayout.Item {
                        Row = 0..1,
                        Body = new Pagination(_page)
                    },
                    new GridLayout.Item {
                        Row = 1..2,
                        Body = RenderTodos()
                    }
                }
            };
    }
}