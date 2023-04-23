using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Form.Entities;
using OnBoardingDigital.Domain.Form.ValueObjects;

namespace OnBoardingDigital.Domain.Form;

public sealed class Form : AggregateRoot<FormId, Guid>
{
    private readonly List<FormSection> _sections = new();
    public string Name { get; private set; }
    public IReadOnlyList<FormSection> Sections => _sections.AsReadOnly();

    private Form(FormId id, string name) : base(id)
    {
        Name = name;
    }

    public static Form Create(string name) => new(FormId.CreateUnique(), name);

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
