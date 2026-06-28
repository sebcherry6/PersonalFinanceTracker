using System;
using System.Collections.Generic;
using System.Text;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Services;


    public class FinanceService
    {
        private readonly List<Transaction> _transactions;

        public FinanceService(List<Transaction> transactions)
        {
            _transactions = transactions;
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<Transaction> GetAll()
        {
            return _transactions;
        }

        public decimal GetBalance()
        {
            decimal income = _transactions
                .Where(t => t.TransactionType == TransactionType.Income)
                .Sum(t => t.Amount);

            decimal expense = _transactions
                .Where(t => t.TransactionType == TransactionType.Expense)
                .Sum(t => t.Amount);

            return income - expense;
        }
    }
