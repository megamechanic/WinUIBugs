using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionViewTest.WinUIBugs;

public sealed partial class CollectionViewBug : Page
{
    public CollectionViewBug()
    {
        this.InitializeComponent();

        CollectionView = new()
        {
            ItemsPath = new(nameof(ISearchPerson.Items)),
            IsSourceGrouped = false
        };
    }

    private CollectionViewSource CollectionView
    {
        get;
    } = new();

    private void OnSetItemsAsSymplePersonClick(object sender, RoutedEventArgs e)
    {
        var items = new List<SymplePerson>()
        {
            new SymplePerson() { LastName = "Patient 1"},
            new SymplePerson() { LastName = "Patient 2"},
            new SymplePerson() { LastName = "Patient 3"},
        };

        CollectionView.Source = items;
    }

    private void OnSetItemsAsISearchPersonClick(object sender, RoutedEventArgs e)
    {
        var items = new List<ISearchPerson>()
        {
            new SymplePerson() { LastName = "Patient 1"},
            new SymplePerson() { LastName = "Patient 2"},
            new SymplePerson() { LastName = "Patient 3"},
        };

        CollectionView.Source = items;
    }

    private void OnCheckExceptionClick(object sender, RoutedEventArgs e)
    {
        ExceptionTextBlock.Text = string.Empty;

        try
        {
            var result = ItemsList.FindChild<ItemsControl>();
        }
        catch (Exception exc)
        {
            ExceptionTextBlock.Text += exc.Message;
        }

        try
        {
            var result1 = ItemsList.FindChildren()?.ToList();
        }
        catch (Exception exc)
        {
            ExceptionTextBlock.Text += "\n" + exc.Message;
        }
    }
}

public class SymplePerson : ISearchPerson
{
    public string FirstName
    {
        get; set;
    }

    public string LastName
    {
        get; set;
    }

    public IEnumerable<ISearchPerson> Items
    {
        get;
    }
}

public interface ISearchPerson
{
    string FirstName
    {
        get;
        set;
    }

    string LastName
    {
        get;
        set;
    }

    IEnumerable<ISearchPerson> Items
    {
        get;
    }
}