
namespace OnBoardingDigital.Contracts.Subscription;

public record SubscriptionResponse(string Id, string FormId, string Email, DateTime CreatedAtUtc, List<SubscriptionAnswerResponse> Answers);
public record SubscriptionAnswerResponse(string Id, string FieldId, int Index, string? TextValue, bool? ChoiceValue, string? OptionsValue, long? NumberValue, string? FileName);
