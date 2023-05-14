using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.Entities;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.FormAggregate;

public sealed class Form : AggregateRoot<FormId, Guid>
{
    private readonly List<FormSection> _sections = new();
    public string Name { get; private set; }
    public FormSectionId FirstSection { get; private set; }
    public IReadOnlyList<FormSection> Sections => _sections.AsReadOnly();

    private Form(FormId id, string name, FormSectionId firstSection) : base(id)
    {
        Name = name;
        FirstSection = firstSection;
    }

    public static Form CreateNew(string name, FormSectionId firstSection) => new(FormId.CreateUnique(), name, firstSection);
    public static Form Create(FormId id, string name, FormSectionId firstSection) => new(id, name, firstSection);

    public void AddFormSection(FormSection section)
    {
        _sections.Add(section);
    }
    public void AddMultipleFormSection(List<FormSection> section)
    {
        _sections.AddRange(section);
    }

#pragma warning disable CS8618
    private Form()
    {
    }
#pragma warning restore CS8618

}
