using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MovieTestInLog.UI.Controls
{
    public class InfinityCarrouselControl : Behavior<CollectionView>
    {
        public static readonly BindableProperty IsLoadingMoreProperty;

        public InfinityCarrouselControl() { }

        public bool IsLoadingMore { get; }

        protected override void OnAttachedTo(CollectionView bindable) { }
        protected override void OnDetachingFrom(CollectionView bindable) { }
    }
}


