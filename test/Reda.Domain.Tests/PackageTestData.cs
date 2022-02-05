using Xunit;

namespace Reda.Domain.Tests;

public class PackageTestData : TheoryData<IEnumerable<Product>, Width>
{

// 1 photoBook consumes 19 mm of package width
// 1 calendar — 10 mm of package width
// 1 canvas — 16 mm
// 1 set of greeting cards — 4.7 mm
// 1 mug — 94 mm

    public static readonly ProductType PhotoBookType = new("photoBook", Width: 19, StackLimit: 1);
    public static readonly ProductType CalendarType = new("calendar", Width: 10, StackLimit: 1);
    public static readonly ProductType CanvasType = new("canvas", Width: 16, StackLimit: 1);
    public static readonly ProductType CardsType = new("cards", Width: 4.7, StackLimit: 1);
    public static readonly ProductType MugType = new("mug", Width: 94, StackLimit: 4);

    public PackageTestData()
    {
        AddSimple();
        AddTwoProducts();
        AddMugWithAnotherProducts();
        AddThreeMugs();
        AddFourMugs();
        AddSixMugs();
        AddNoProduct();
        AddFiveMugsInTwoProducts();
    }

    private void AddSimple() =>
        Add(new Product[] {new(PhotoBookType, quantity: 4)},
            4 * PhotoBookType.Width);

    private void AddTwoProducts() =>
        Add(new Product[] {new(PhotoBookType, quantity: 2), new(CalendarType, quantity: 1)},
            2 * PhotoBookType.Width + 1 * CalendarType.Width);
    
    private void AddMugWithAnotherProducts() =>
        Add(new Product[] {new(CardsType, quantity: 4), new(MugType, quantity: 1)},
            4 * CardsType.Width + 1 * MugType.Width);
    
    private void AddThreeMugs() =>
        Add(new Product[] {new(PhotoBookType, quantity: 2), new(CalendarType, quantity: 1), new(MugType, quantity: 3)},
            2 * PhotoBookType.Width + 1 * CalendarType.Width + 1 * MugType.Width);
    
    private void AddFourMugs() =>
        Add(new Product[] {new(PhotoBookType, quantity: 2), new(MugType, quantity: 4)},
            2 * PhotoBookType.Width + 1 * MugType.Width);
    
    private void AddSixMugs() =>
        Add(new Product[] {new(CanvasType, quantity: 2), new(MugType, quantity: 6)},
            2 * CanvasType.Width + 2 * MugType.Width);

    private void AddNoProduct() => Add(Enumerable.Empty<Product>(), 0);
    
    private void AddFiveMugsInTwoProducts() =>
        Add(new Product[] {new(MugType, quantity: 2), new(CalendarType, quantity: 3), new(MugType, quantity: 3)},
            3 * CalendarType.Width + 2 * MugType.Width);
}