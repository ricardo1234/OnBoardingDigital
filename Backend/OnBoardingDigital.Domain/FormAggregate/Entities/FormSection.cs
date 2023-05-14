using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.FormAggregate.Entities;

public sealed class FormSection : Entity<FormSectionId>
{
    private readonly List<FormField> _fields= new();
    public string Name { get; private set; }
    public int Order { get; private set; }
    public Repeatable Repeatable { get; private set; }
    public FormSectionId? DefaultNextSection { get; private set; }

    public IReadOnlyList<FormField> Fields => _fields.AsReadOnly();

    private FormSection(FormSectionId id, string name, int order, Repeatable repeatable, FormSectionId? defaultNextSection) : base(id)
    {
        Name = name;
        Order = order;
        Repeatable = repeatable;
        DefaultNextSection = defaultNextSection;
    }

    public static FormSection CreateRepeatable(string name, int order, Repeatable repeatable, FormSectionId? defaultNextSection) => new(FormSectionId.CreateUnique(), name, order, repeatable, defaultNextSection);
    public static FormSection CreateNew(string name, int order, FormSectionId? defaultNextSection) => new(FormSectionId.CreateUnique(), name, order, Repeatable.Create(), defaultNextSection);
    public static FormSection Create(FormSectionId id,string name, int order, Repeatable repeatable, FormSectionId? defaultNextSection) => new(id, name, order, repeatable, defaultNextSection);

    public void AddFormField(FormField field)
    {
        _fields.Add(field);
    }
    public void AddMultipleFormFields(List<FormField> field)
    {
        _fields.AddRange(field);
    }

#pragma warning disable CS8618
    private FormSection()
    {
    }
#pragma warning restore CS8618
}
