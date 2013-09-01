using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DylanTools.Core.Project.ImportWizard
{
    /// <summary>
    /// Interaction logic for ImportWizardDialog.xaml
    /// </summary>
    internal partial class ImportWizardDialog : DialogWindow
    {
        private static readonly DependencyPropertyKey IsNextDefaultPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsNextDefault", typeof(bool), typeof(ImportWizardDialog),
                new PropertyMetadata(true)
            );

        public static readonly DependencyProperty IsNextDefaultProperty = 
            IsNextDefaultPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey IsFinishDefaultPropertyKey = 
            DependencyProperty.RegisterReadOnly(
                "IsFinishDefault", typeof(bool), typeof(ImportWizardDialog), 
                new PropertyMetadata(false)
            );

        public static readonly DependencyProperty IsFinishDefaultProperty = 
            IsFinishDefaultPropertyKey.DependencyProperty;

        private CollectionViewSource _pageCollectionViewSource;

        private static readonly DependencyPropertyKey PageSequencePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "PageSequence", typeof(ICollectionView), typeof(ImportWizardDialog),
                new PropertyMetadata()
            );

        public static readonly DependencyProperty PageSequenceProperty =
            PageSequencePropertyKey.DependencyProperty;

        public ICollectionView PageSequence
        {
            get { return (ICollectionView)GetValue(PageSequenceProperty); }
            private set { SetValue(PageSequencePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey PageCountPropertyKey = 
            DependencyProperty.RegisterReadOnly(
                "PageCount", typeof(int), typeof(ImportWizardDialog), new PropertyMetadata(0)
            );

        public static readonly DependencyProperty PageCountProperty = 
            PageCountPropertyKey.DependencyProperty;

        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            private set { SetValue(PageCountPropertyKey, value); }
        }

        public ImportWizardDialog()
        {
            _pageCollectionViewSource = new CollectionViewSource {
                Source = new ObservableCollection<Page>(
                    new Page[] {
                        new SourcePage { DataContext = this }
                    })
            };

            PageSequence = _pageCollectionViewSource.View;
            PageSequence.CurrentChanged += PageSequence_CurrentChanged;
            PageSequence.MoveCurrentToFirst();

            DataContext = this;

            this.InitializeComponent();
        }

        private void PageSequence_CurrentChanged(object sender, EventArgs e)
        {
            SetValue(IsNextDefaultPropertyKey, PageSequence.CurrentPosition < PageCount - 1);
            SetValue(IsFinishDefaultPropertyKey, PageSequence.CurrentPosition >= PageCount - 1);
        }
    }
}
