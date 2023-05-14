
namespace OnBoardingDigital.Contracts.Subscription;

public record SubscriptionRequest(string FormId, string Email, List<SubscriptionAnswerRequest> Answers);
public record SubscriptionAnswerRequest(string FieldId, int Index, string Answer);
