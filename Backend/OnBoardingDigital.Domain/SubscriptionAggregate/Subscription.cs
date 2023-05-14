﻿using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.SubscriptionAggregate.Entities;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.SubscriptionAggregate;

public sealed class Subscription : AggregateRoot<SubscriptionId, Guid>
{
    private readonly List<SubscriptionAnswer> _answers = new();
    public FormId FormId { get; private set; }
    public string Email { get; private set; }
    public IReadOnlyList<SubscriptionAnswer> Answers => _answers.AsReadOnly();

    private Subscription(SubscriptionId id, string email, FormId form) : base(id)
    {
        Email = email;
        FormId = form;
    }

    public static Subscription CreateNew(string email, FormId form) => new(SubscriptionId.CreateUnique(), email, form);
    public static Subscription Create(SubscriptionId id, string email, FormId form) => new(id, email, form);

    public void AddAnswer(SubscriptionAnswer answers)
    {
        _answers.Add(answers);
    }
    public void AddMultipleAddAnswers(List<SubscriptionAnswer> answers)
    {
        _answers.AddRange(answers);
    }

#pragma warning disable CS8618
    private Subscription()
    {
    }
#pragma warning restore CS8618

}
