using System;
using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
        }

        public Name Name{ get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get
            {
                return _subscriptions.ToArray();
            } 
        }
        public void AddSubscription(Subscription subscription){
            //Primeira opcao - Se tiver uma assinatura ativa, cancela esta
            //Segunda opcao - Cancela todas as outras assinaturas e seta esta como principal

            //segunda opcao ->
            foreach (var sub in Subscriptions)
            {
                sub.Inactivate();
            }
            _subscriptions.Add(subscription);
        }
    }
}