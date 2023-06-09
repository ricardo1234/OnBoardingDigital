﻿namespace OnBoardingDigital.Domain.Common;

public abstract class AggregateRoot<TId, TIdType>
    where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected set; }

    protected AggregateRoot(TId id)
    {
        Id = id;
    }
#pragma warning disable CS8618
    protected AggregateRoot()
    { }
#pragma warning restore CS8618
}
