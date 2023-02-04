﻿using System.Collections.Generic;
using Better.Diagnostics.Runtime.DrawingModule.Interfaces;
using UnityEngine;

namespace Better.Diagnostics.Runtime.DrawingModule
{
    public abstract class BaseWrapper : IRendererWrapper
    {
        private List<Line> _lines;

        public abstract bool IsMarkedForRemove { get; }

        public abstract void MarkForRemove();
        public abstract void OnRemoved();

        public void Initialize()
        {
            _lines = GenerateBaseLines(GetColor());
        }

        protected abstract Color GetColor();

        protected List<Line> GetBaseLines()
        {
            return _lines;
        }

        private protected abstract List<Line> GenerateBaseLines(Color color);

        public abstract IEnumerable<Line> GetLines();
    }
    
    public abstract class BaseWrapper<T> : BaseWrapper, ISettable<ITrackableData<T>, IRendererWrapper>
    {
        private protected ITrackableData<T> _data;
        public override bool IsMarkedForRemove => _data.IsMarkedForRemove;

        public IRendererWrapper Set(ITrackableData<T> data)
        {
            _data = data;
            return this;
        }

        protected override Color GetColor()
        {
            return _data.Color;
        }

        public override void MarkForRemove()
        {
            _data.MarkForRemove();
        }
    }
}