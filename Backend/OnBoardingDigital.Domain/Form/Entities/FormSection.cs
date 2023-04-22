using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Form.ValueObjects;

namespace OnBoardingDigital.Domain.Form.Entities;

public sealed class FormSection : Entity<FormSectionId>
{
    private readonly List<FormField> _items= new();
    public string Name { get; }
    public int Order { get; }
    public Repeatable Repeatable { get; }
    public IReadOnlyList<FormField> Items => _items.AsReadOnly();

    private FormSection(FormSectionId id, string name, int order, Repeatable repeatable) : base(id)
    {
        Name = name;
        Order = order;
        Repeatable = repeatable;
    }

    public static FormSection Create(string name, int order, Repeatable repeatable) => new(FormSectionId.CreateUnique(), name, order, repeatable);

}
