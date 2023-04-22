using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Form.Entities;
using OnBoardingDigital.Domain.Form.ValueObjects;

namespace OnBoardingDigital.Domain.Form;

public sealed class Form : AggregateRoot<FormId, Guid>
{
    private readonly List<FormSection> _sections = new();
    public string Name { get; set; }
    public IReadOnlyList<FormSection> Sections => _sections.AsReadOnly();

    private Form(FormId id, string name) : base(id)
    {
        Name = name;
    }

    public static Form Create(string name) => new(FormId.CreateUnique(), name);

}
