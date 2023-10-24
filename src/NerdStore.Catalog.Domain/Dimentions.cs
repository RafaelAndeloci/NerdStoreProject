using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain;

public class Dimensions
{
    public decimal Height { get; private set; }
    public decimal Width { get; private set; }
    public decimal Depth { get; private set; }

    protected Dimensions() {}
    public Dimensions(decimal height, decimal width, decimal depth)
    {
        AssertionConcern.ValidateIfLess(height, 1, "O campo Altura não pode ser menor ou igual a 0");
        AssertionConcern.ValidateIfLess(width,1,"O campo Largura não pod ser menor ou igual a 0");
        AssertionConcern.ValidateIfLess(depth, 1, "O campo Profundidade não pode ser menor ou igual a 0");
        
        Height = height;
        Width = width;
        Depth = depth;
    }

    public string FormattedDescription()
    {
        return $"LxAxP: {Width} x {Height} x {Depth}";
    }

    public override string ToString()
    {
        return FormattedDescription();
    }
}