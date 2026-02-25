namespace NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel
{
    public abstract class AggregateRoot : Entity
    {
        private List<IDomainEvent> _domainEvents = [];

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void Raise(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        protected void ClearDomainEvents() => _domainEvents.Clear();
    }
}
