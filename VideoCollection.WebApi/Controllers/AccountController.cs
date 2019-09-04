using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;

namespace VideoCollection.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private static readonly Account Account;

        static AccountController()
        {
            Account = new Account();
            Account.Deposit(1000m);
        }
        
        // GET api/account
        [HttpGet]
        public decimal Get()
        {
            return Account.Amount;
        }

        // POST api/account/operation
        [HttpPost]
        public void Change([FromBody] AccoutOperation operation)
        {
            if (operation.OperationType == OperationType.Deposit)
            {
                Account.Deposit(operation.Value);
            }
            else if (operation.OperationType == OperationType.Withdraw)
            {
                Account.Withdraw(operation.Value);
            }
            else
            {
                throw new ArgumentException("operation");
            }
        }
    }

    public class Account
    {
        private readonly List<AccoutOperation> _operations;
        private readonly object _lock = new object();

        public Account()
        {
            _operations = new List<AccoutOperation>();
        }

        public decimal Amount { get; private set; }

        public ImmutableList<AccoutOperation> Operations
        {
            get
            {
                lock (_lock)
                {
                    return ImmutableList<AccoutOperation>.Empty.AddRange(_operations);
                }
            }
        }

        public void Deposit(decimal value)
        {
            lock (_lock)
            {
                Amount += value;

                _operations.Add(new AccoutOperation
                {
                    Date = DateTime.Now,
                    OperationType = OperationType.Deposit,
                    Value = value
                });
            }
        }

        public void Withdraw(decimal value)
        {
            lock (_lock)
            {
                Amount -= value;

                _operations.Add(new AccoutOperation
                {
                    Date = DateTime.Now,
                    OperationType = OperationType.Deposit,
                    Value = value
                });
            }
        }
    }

    public class AccoutOperation
    {
        public DateTime Date { get; set; }

        public OperationType OperationType { get; set; }

        public decimal Value { get; set; }
    }

    public enum OperationType
    {
        Deposit,
        Withdraw,
    }
}