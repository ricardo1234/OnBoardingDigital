namespace OnBoardingDigital.Contracts.Form;

public record FormReponse(string Id, string Name, List<FormSectionResponse> Sections);
public record FormSectionResponse(string Id, int Order, string Name, bool CanRepeat, int? NumberOfReapeats, List<FormFieldResponse> Fields);
public record FormFieldResponse(string Id, int Order, bool Required, string Description, FormFieldTypeResponse Type,
    FormFieldChoiceSettingsResponse? ChoiceSettings,
    FormFieldFileSettingsResponse? FileSettings,
    FormFieldNumberSettingsResponse? NumberSettings,
    FormFieldOptionsSettingsResponse? OptionsSettings,
    FormFieldTextSettingsResponse? TextSettings,
    FormFieldInformationSettingsResponse? InformationSettings,
    FormFieldDateTimeSettingsResponse? DateTimeSettings);
public record FormFieldTypeResponse(int Id, string Description);
public record FormFieldChoiceSettingsResponse(string Group);
public record FormFieldFileSettingsResponse(List<string> Extensions, long? MaxSize);
public record FormFieldNumberSettingsResponse(int? Minimum, int? Maximum, int? RequiredDigits);
public record FormFieldOptionsSettingsResponse(List<string> Options);
public record FormFieldTextSettingsResponse(int? CharMaximum, int? CharMinimum, string? ValidationExpression);
public record FormFieldInformationSettingsResponse(string HtmlValue);
public record FormFieldDateTimeSettingsResponse(bool HasTime, bool HasDate, bool IsMinimumToday, bool IsMaximumToday, DateTime? Minimum, DateTime? Maximum);