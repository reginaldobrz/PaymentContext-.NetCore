using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        //red green refactor
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(),new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
             command.FirstName ="bruce";
             command.LastName ="wayne";
             command.Document ="99999999999";
             command.Email ="hello@reginaldo.io2";
             command.BarCode ="123456789";
             command.BoletoNumber ="1234567";
             command.PaymentNumber ="123121";
             command.PaidDate =DateTime.Now;
             command.ExpireDate =DateTime.Now.AddMonths(1);
             command.Total =60;
             command.TotalPaid =60;
             command.Payer ="WAYNE CORP";
             command.PayerDocumentType =EDocumentType.CPF; 
             command.PayerEmail ="1234567891011";
             command.PayerDocument ="superman@dc.com";          
             command.Street ="asas";
             command.Number ="asasd";
             command.Neighborhood ="asdasd";
             command.City ="asdasd";
             command.State ="asdasd";
             command.Country ="asdasd";
             command.ZipCode ="12345678";

             handler.Handle(command);
             Assert.AreEqual(false,handler.Valid);
        }
    }
}
